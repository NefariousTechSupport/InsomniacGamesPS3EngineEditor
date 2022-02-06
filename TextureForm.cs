using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace InsomniacGamesPS3EngineEditor
{
	public partial class TextureForm : Form
	{
		AssetLookup _assetLookup;
		MemoryStream msCurrentImage;
		int selectedItem = 0;

		public TextureForm(AssetLookup assetLookup)
		{
			InitializeComponent();
			_assetLookup = assetLookup;
			for(int i = 0; i < _assetLookup._textureParent.numTextures; i++)
			{
				lbFiles.Items.Add(_assetLookup._textureParent._textures[i].name);
			}
			ResizeForm(null, null);
		}
		public void ResizeForm(object sender, EventArgs e)
		{
			lbFiles.Size = new Size(this.Width / 4 - 18, this.Height - 64);
			pbCurrentItem.Location = new Point(this.Width / 4, 12);
			pbCurrentItem.Size = new Size(3 * this.Width / 4 - 24, this.Height - 99);
		}
		public void NewItemSelected(object sender, EventArgs e)
		{
			if(lbFiles.SelectedIndex >= 0)
			{
				selectedItem = lbFiles.SelectedIndex;
				msCurrentImage = new MemoryStream();
				_assetLookup._textureParent.ExtractTexture((uint)selectedItem, msCurrentImage);
				msCurrentImage.Seek(0x00, SeekOrigin.Begin);
				pbCurrentItem.Image = TextureHelper.BitmapFromDDS(msCurrentImage);
				msCurrentImage.Close();
			}
		}
		public void Extract(object sender, EventArgs e)
		{
			using(CommonOpenFileDialog cofd = new CommonOpenFileDialog())
			{
				cofd.IsFolderPicker = true;
				cofd.Title = "Select Output Path";
				cofd.RestoreDirectory = true;

				bool extractAll = (ModifierKeys & Keys.Control) == Keys.Control;

				if(cofd.ShowDialog() == CommonFileDialogResult.Ok)
				{
					if(extractAll)
					{
						for(uint i = 0; i < _assetLookup._textureParent.numTextures; i++)
						{
							_assetLookup._textureParent.ExtractTexture(i, cofd.FileName + "/" + _assetLookup._textureParent._textures[i].name + (_assetLookup._textureParent._textures[i].name.EndsWith(".dds") ? string.Empty : ".dds"));
						}
					}
					else
					{
						_assetLookup._textureParent.ExtractTexture((uint)selectedItem, cofd.FileName + "/" + _assetLookup._textureParent._textures[selectedItem].name + (_assetLookup._textureParent._textures[selectedItem].name.EndsWith(".dds") ? string.Empty : ".dds"));
					}
				}
			}
		}
		public void Replace(object sender, EventArgs e)
		{
			if(!_assetLookup._textureParent.IsTextureSupported((uint)selectedItem)) return;
			using(OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.Title = "Select Input Image";
				ofd.Filter = "Supported Image Files|*.dds;*.png;*.jpg;*.jpeg;*.gif;*.tga;*.bmp|All Files (*.*)|*.*";
				ofd.RestoreDirectory = true;

				if(ofd.ShowDialog() == DialogResult.OK)
				{
					_assetLookup._textureParent.ReplaceTexture((uint)selectedItem, ofd.FileName);
					lbFiles.SelectedIndex = selectedItem;
					NewItemSelected(null, null);
				}
			}
		}
	}
}
