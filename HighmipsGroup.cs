using System;
using System.IO;
using System.Linq;

namespace InsomniacGamesPS3EngineEditor
{
	public class HighmipsGroup
	{
		public Texture[] _highmips;
		public uint numHighmips;
		public AssetLookup _parent;
		public Stream _strHighmips;

		public HighmipsGroup(AssetLookup parent, Stream strHighmips)
		{
			_parent = parent;
			_strHighmips = strHighmips;
			numHighmips = _parent._sectionHeaders.First(x => x.indentifier == AssetSectionIdentifier.HighmipPointers).length / 0x10;
			_highmips = new Texture[numHighmips];

			for(uint i = 0; i < numHighmips; i++)
			{
				_highmips[i].format      = _parent._sectionStreams[AssetSectionIdentifier.TextureMetadata].ReadByte();
				_highmips[i].mipmapCount = (uint)_parent._sectionStreams[AssetSectionIdentifier.TextureMetadata].ReadByte();
				_highmips[i].width       = (uint)Math.Pow(2, _parent._sectionStreams[AssetSectionIdentifier.TextureMetadata].ReadByte());
				_highmips[i].height      = (uint)Math.Pow(2, _parent._sectionStreams[AssetSectionIdentifier.TextureMetadata].ReadByte());
				_highmips[i].name        = $"{i.ToString("X08")}";

				_highmips[i].texturesPointer = _parent._pointers[AssetSectionIdentifier.TexturePointers][i];

				if(_highmips[i].texturesPointer.length % 0x10 == 0)
				{
					_highmips[i].format = 0x05;
				}
				else
				{
					_highmips[i].format = 0x01;
				}
			}
		}

		public void ExtractTexture(uint index, string output)
		{
			ExtractTexture(index, new FileStream(output, FileMode.Create, FileAccess.ReadWrite), false);
		}
		public void ExtractTexture(uint index, Stream output, bool leaveOpen = true)
		{
			_strHighmips.Seek(_highmips[index].texturesPointer.offset, SeekOrigin.Begin);
			TextureHelper.Extract(_strHighmips, output, _highmips[index].width, _highmips[index].height, _highmips[index].texturesPointer.length, 1, _highmips[index].format, leaveOpen);
		}
		public void ReplaceTexture(uint index, string input)
		{
			ReplaceTexture(index, new FileStream(input, FileMode.Open, FileAccess.ReadWrite));
		}

		public void ReplaceTexture(uint index, Stream input)
		{
			_strHighmips.Seek(_highmips[index].texturesPointer.offset, SeekOrigin.Begin);
			TextureHelper.Replace(input, _strHighmips, _highmips[index].width, _highmips[index].height, _highmips[index].texturesPointer.length, _highmips[index].mipmapCount, _highmips[index].format);
		}

	}
}