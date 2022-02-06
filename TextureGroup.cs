using System;
using System.IO;
using System.Linq;

namespace InsomniacGamesPS3EngineEditor
{
	public class TextureGroup
	{
		public Texture[] _textures;
		public uint numTextures;
		public AssetLookup _parent;
		public Stream _strTextures;
		public Stream _strHighmips;

		bool useHighmips = false;

		public TextureGroup(AssetLookup parent, Stream strTextures, Stream strHighmips)
		{
			_parent = parent;
			_strTextures = strTextures;
			_strHighmips = strHighmips;

			numTextures = _parent._sectionHeaders.First(x => x.indentifier == AssetSectionIdentifier.TexturePointers).length / 0x10;
			_textures = new Texture[numTextures];

			for(uint i = 0; i < numTextures; i++)
			{
				_textures[i].format      = _parent._sectionStreams[AssetSectionIdentifier.TextureMetadata].ReadByte();
				_textures[i].mipmapCount = (uint)_parent._sectionStreams[AssetSectionIdentifier.TextureMetadata].ReadByte();
				_textures[i].width       = (uint)Math.Pow(2, _parent._sectionStreams[AssetSectionIdentifier.TextureMetadata].ReadByte());
				_textures[i].height      = (uint)Math.Pow(2, _parent._sectionStreams[AssetSectionIdentifier.TextureMetadata].ReadByte());
				_textures[i].name        = $"{i.ToString("X08")}";

				_textures[i].texturesPointer = _parent._pointers[AssetSectionIdentifier.TexturePointers][i];
				_textures[i].highmipsPointer = _parent._pointers[AssetSectionIdentifier.HighmipPointers][i];
			}
		}

		public bool IsTextureSupported(uint index)
		{
			return !(_textures[index].format != 0x06 && _textures[index].format != 0x08);
		}

		public void ExtractTexture(uint index, string output)
		{
			ExtractTexture(index, new FileStream(output, FileMode.Create, FileAccess.ReadWrite), false);
		}
		public void ExtractTexture(uint index, Stream output, bool leaveOpen = true)
		{
			_strHighmips.Seek(_textures[index].highmipsPointer.offset, SeekOrigin.Begin);
			if(!IsTextureSupported(index))
			{
				//Console.WriteLine("Unsupported Format");
				FileStream unsupportedFormatImage = new FileStream("Resources/UnsupportedFormat.dds", FileMode.Open, FileAccess.Read);
				unsupportedFormatImage.CopyTo(output);
				unsupportedFormatImage.Close();
				return;
			}
			//Console.WriteLine($"Highmips pointer: {_textures[index].highmipsPointer.offset.ToString("X08")}");
			TextureHelper.Extract(_strHighmips, output, _textures[index].width, _textures[index].height, _textures[index].highmipsPointer.length, 1, _textures[index].format, leaveOpen);
		}
		public void ReplaceTexture(uint index, string input)
		{
			ReplaceTexture(index, new FileStream(input, FileMode.Open, FileAccess.ReadWrite));
		}

		public void ReplaceTexture(uint index, Stream input)
		{
			_strHighmips.Seek(_textures[index].highmipsPointer.offset, SeekOrigin.Begin);
			_strTextures.Seek(_textures[index].texturesPointer.offset, SeekOrigin.Begin);
			TextureHelper.Replace(input, _strHighmips,     _textures[index].width,     _textures[index].height, _textures[index].highmipsPointer.length,                            1, _textures[index].format);
			TextureHelper.Replace(input, _strTextures, _textures[index].width / 2, _textures[index].height / 2, _textures[index].texturesPointer.length, _textures[index].mipmapCount, _textures[index].format);
		}
	}
}