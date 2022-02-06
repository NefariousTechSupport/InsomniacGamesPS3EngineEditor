using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace InsomniacGamesPS3EngineEditor
{
	public class AssetLookup
	{
		public FileStream _fs;
		public StreamHelper _sh;

		public uint _numSections;
		public uint _headerLength;
		public uint _fileLength;

		public TextureGroup _textureParent;

		public AssetSectionHeader[] _sectionHeaders;
		public Dictionary<AssetSectionIdentifier, AssetPointer[]> _pointers;
		public Dictionary<AssetSectionIdentifier, StreamHelper> _sectionStreams;

		public AssetLookup(string filepath)
		{
			_fs = new FileStream(filepath, FileMode.Open, FileAccess.ReadWrite);
			_sh = new StreamHelper(_fs, StreamHelper.Endianness.Big);

			if(_sh.ReadUInt32() != 0x49474857) throw new System.Exception("Not an assetlookup.dat or main.dat");

			_numSections  = _sh.ReadUInt32AtOffset(0x08);
			_headerLength = _sh.ReadUInt32();				//This is here for the sake of completeness
			_fileLength   = _sh.ReadUInt32();				//This is here for the sake of completeness

			_sectionHeaders = new AssetSectionHeader[_numSections];
			_sectionStreams = new Dictionary<AssetSectionIdentifier, StreamHelper>();
			_pointers = new Dictionary<AssetSectionIdentifier, AssetPointer[]>();

			//Console.WriteLine(_numSections.ToString("X08"));

			//Read the section headers
			for(uint i = 0; i < _numSections; i++)
			{
				_sectionHeaders[i].indentifier = (AssetSectionIdentifier)_sh.ReadUInt32AtOffset(0x20 + i*0x10 + 0x00);
				_sectionHeaders[i].offset      = _sh.ReadUInt32AtOffset(0x20 + i*0x10 + 0x04);
				_sectionHeaders[i].one         = _sh.ReadUInt32AtOffset(0x20 + i*0x10 + 0x08);
				_sectionHeaders[i].length      = _sh.ReadUInt32AtOffset(0x20 + i*0x10 + 0x0C);

				_sh.BaseStream.Seek(_sectionHeaders[i].offset, SeekOrigin.Begin);

				_sectionStreams.Add(_sectionHeaders[i].indentifier, new StreamHelper(new MemoryStream(_sh.ReadBytes((int)_sectionHeaders[i].length)), StreamHelper.Endianness.Big));
			}

			ProcessAssetLookupSections();

			PopulateChildren();
		}

		public void ProcessAssetLookupSections()
		{
			for(uint i = 0; i < _numSections; i++)
			{
				if(((uint)_sectionHeaders[i].indentifier & 0x01) == 1) continue;							//IDs that end in 1 are literally just the number 1 repeated a bunch of times
				if(_sectionHeaders[i].indentifier == AssetSectionIdentifier.Unknown) continue;				//Literally the number 6
				if(_sectionHeaders[i].indentifier == AssetSectionIdentifier.TextureMetadata) continue;		//Processed in TextureGroup

				AssetPointer[] pointers = new AssetPointer[_sectionHeaders[i].length / 0x10];

				//Console.WriteLine($"{_sectionHeaders[i].indentifier}: {_sectionHeaders[i].length.ToString("X08")}");

				for(uint j = 0; j < pointers.Length; j++)
				{
					pointers[j] = new AssetPointer(_sectionStreams[_sectionHeaders[i].indentifier]);		//Reads a pointer from the current position in the stream
				}

				_pointers.Add(_sectionHeaders[i].indentifier, pointers);
			}
		}

		public void PopulateChildren()
		{
			string dirName = Path.GetDirectoryName(_fs.Name);
			FileStream texFS;
			FileStream hmFS = null;
			texFS = new FileStream(dirName + "/textures.dat", FileMode.Open, FileAccess.ReadWrite);
			try
			{
				hmFS = new FileStream(dirName + "/highmips.dat", FileMode.Open, FileAccess.ReadWrite);
			}
			catch(FileNotFoundException e)
			{
				MessageBox.Show("highmips.dat was not found.");
				using(OpenFileDialog ofd = new OpenFileDialog())
				{
					ofd.Title = "Select \"highmips.dat\"";
					ofd.Filter = "highmips.dat|highmips.dat|All Files (*.*) ps why did you rename it|*.*";
					ofd.RestoreDirectory = true;

					if(ofd.ShowDialog() == DialogResult.OK)
					{
						hmFS = new FileStream(ofd.FileName, FileMode.Open, FileAccess.ReadWrite);
					}
					else return;
				}
			}
			_textureParent = new TextureGroup(this, texFS, hmFS);

			//More to come
		}
	}
}