using System;
using System.Drawing;
using System.IO;

using BCnEncoder.Decoder;
using BCnEncoder.Encoder;
using BCnEncoder.ImageSharp;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace InsomniacGamesPS3EngineEditor
{
	public static class TextureHelper
	{
		private static readonly byte[] ddsHeader = new byte[0x80]
		{
			0x44, 0x44, 0x53, 0x20,		// "DDS "
			0x7C, 0x00, 0x00, 0x00,		// Version Info
			0x07, 0x10, 0x0A, 0x00,		// More Version Info
			0x00, 0x00, 0x00, 0x00,		// Height
			0x00, 0x00, 0x00, 0x00,		// Width
			0x00, 0x00, 0x00, 0x00,		// Size
			0x00, 0x00, 0x00, 0x00,		// Depth
			0x00, 0x00, 0x00, 0x00,		// Mipmaps 
			0x00, 0x00, 0x00, 0x00,		// 
			0x00, 0x00, 0x00, 0x00,		// 
			0x00, 0x00, 0x00, 0x00,		// 
			0x00, 0x00, 0x00, 0x00,		// 
			0x00, 0x00, 0x00, 0x00,		// 
			0x00, 0x00, 0x00, 0x00,		// 
			0x00, 0x00, 0x00, 0x00,		// 
			0x00, 0x00, 0x00, 0x00,		// 
			0x00, 0x00, 0x00, 0x00,		// 
			0x00, 0x00, 0x00, 0x00,		// 
			0x00, 0x00, 0x00, 0x00,		// 
			0x20, 0x00, 0x00, 0x00,		// dwSize
			0x00, 0x00, 0x00, 0x00,		// dwFlags
			0x00, 0x00, 0x00, 0x00,		// dwFourCC
			0x00, 0x00, 0x00, 0x00,		// dwRGBBitCount
			0x00, 0x00, 0x00, 0x00,		// dwRBitMask
			0x00, 0x00, 0x00, 0x00,		// dwGBitMask
			0x00, 0x00, 0x00, 0x00,		// dwBBitMask
			0x00, 0x00, 0x00, 0x00,		// dwABitMask
			0x08, 0x10, 0x40, 0x00,		// 
			0x00, 0x00, 0x00, 0x00,		// 
			0x00, 0x00, 0x00, 0x00,		// 
			0x00, 0x00, 0x00, 0x00,		// 
			0x00, 0x00, 0x00, 0x00 		// 
		};
		public static Bitmap BitmapFromDDS(Stream dds)
		{
			BcDecoder dec = new BcDecoder();
			Image<Rgba32> image = dec.DecodeToImageRgba32(dds);
			System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(image.Width, image.Height);
			for(int y = 0; y < image.Height; y++)
			{
		        Span<Rgba32> pixelRowSpan = image.GetPixelRowSpan(y);
				for(int x = 0; x < image.Width; x++)
				{
					bmp.SetPixel(x, image.Height - y - 1, System.Drawing.Color.FromArgb(pixelRowSpan[x].A, pixelRowSpan[x].R, pixelRowSpan[x].G, pixelRowSpan[x].B));
				}
			}
			return bmp;
		}
		public static void Extract(Stream src, Stream dst, uint width, uint height, uint size, uint mipmapCount, uint format, bool leaveOpen = false)
		{
			dst.Write(ddsHeader, 0x00, 0x80);
			dst.Seek(0x0C, SeekOrigin.Begin);
			dst.Write(BitConverter.GetBytes((uint)height), 0x00, 0x04);
			dst.Write(BitConverter.GetBytes((uint)width), 0x00, 0x04);
			dst.Write(BitConverter.GetBytes(size), 0x00, 0x04);
			dst.Seek(0x1C, SeekOrigin.Begin);
			dst.Write(BitConverter.GetBytes((uint)mipmapCount), 0x00, 0x04);
			dst.Seek(0x50, SeekOrigin.Begin);
			switch(format)
			{
//				case 0x03:
//					break;
				case 0x05:
					dst.Write(BitConverter.GetBytes(0x00000041));
					dst.Write(BitConverter.GetBytes(0x00000000));
					dst.Write(BitConverter.GetBytes(0x00000020));
					dst.Write(BitConverter.GetBytes(0x000000FF));
					dst.Write(BitConverter.GetBytes(0x0000FF00));
					dst.Write(BitConverter.GetBytes(0x00FF0000));
					dst.Write(BitConverter.GetBytes(0xFF000000));
					break;
				case 0x06:
					dst.Write(BitConverter.GetBytes(0x00000004));
					dst.Write(System.Text.Encoding.ASCII.GetBytes("DXT1"));
					break;
				case 0x08:
				case 0x0B:
					dst.Write(BitConverter.GetBytes(0x00000004));
					dst.Write(System.Text.Encoding.ASCII.GetBytes("DXT5"));
					break;
				default: throw new Exception($"Format {format.ToString("X02")} isn't supported");
			}
			dst.Seek(0x80, SeekOrigin.Begin);
			byte[] srcBytes = new byte[size];
			src.Read(srcBytes, 0x00, (int)size);
			dst.Write(srcBytes, 0x00, (int)size);
			dst.Flush();
			if(!leaveOpen)
			{
				dst.Close();
			}
		}

		public static uint CalculateTextureSize(uint format, uint width, uint height, uint mipmapCount = 1)
		{
			switch(format)
			{
				case 0x06:
				case 0x08:
				case 0x0B:
					uint basicSize = (uint)(Math.Max( 1, ((width+3)/4) ) * Math.Max(1, ( (height + 3) / 4 ) ));		//Taken from the DDS programming guide
					uint finalSize = 0;
					for(uint i = 0; i < mipmapCount; i++)
					{
						finalSize += (uint)Math.Max(basicSize / Math.Pow(4, i), (format == 0x06 ? 0x08 : 0x10));
					}
					return finalSize * (uint)(format == 0x06 ? 0x08 : 0x10);
				case 0x05:
					return width * height * 3;
				default:
					throw new NotImplementedException("This format is unsupported");
			}
		}
		public static void Replace(Stream src, Stream dst, uint width, uint height, uint size, uint mipmapCount, uint format)
		{
			src.Seek(0x00, SeekOrigin.Begin);
			byte[] magic = new byte[4];
			src.Read(magic, 0x00, 0x04);
			Image<Rgba32> image = null;
			src.Seek(0x00, SeekOrigin.Begin);
			if(System.Text.Encoding.ASCII.GetString(magic) == " SDD")
			{
				BcDecoder dec = new BcDecoder();
				image = dec.DecodeToImageRgba32(src);
			}
			else
			{
				image = SixLabors.ImageSharp.Image.Load<Rgba32>(src);
			}

			image.Mutate(n => n.Resize((int)width, (int)height));

			BcEncoder enc = new BcEncoder();
			enc.OutputOptions.GenerateMipMaps = true;
			enc.OutputOptions.MaxMipMapLevel = (int)mipmapCount;
			enc.OutputOptions.Quality = CompressionQuality.BestQuality;
			enc.OutputOptions.FileFormat = BCnEncoder.Shared.OutputFileFormat.Dds;
			if(format == 0x06)
			{
				enc.OutputOptions.Format = BCnEncoder.Shared.CompressionFormat.Bc1;
			}
			else if(format == 0x08 || format == 0x0B)
			{
				enc.OutputOptions.Format = BCnEncoder.Shared.CompressionFormat.Bc3;
			}
			else if(format == 0x05)
			{
				enc.OutputOptions.Format = BCnEncoder.Shared.CompressionFormat.Rgba;
			}
			else
			{
				throw new Exception("Replacing this texture isn't yet supported");
			}
			MemoryStream oms = new MemoryStream((int)size + 0x80);
			//(int)CalculateTextureSize(IGZ_TextureFormat.dxt1, width, height, mipmapCount)
			enc.EncodeToStream(image, oms);
			oms.Seek(0x80, SeekOrigin.Begin);
			oms.CopyTo(dst);
			oms.Close();
			dst.Flush();
		}
	}
}