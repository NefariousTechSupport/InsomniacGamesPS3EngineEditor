using System;
using System.Windows.Forms;

namespace InsomniacGamesPS3EngineEditor
{
	public partial class Form1 : Form
	{
		AssetLookup assetLookup;

		public Form1()
		{
			InitializeComponent();

			using(OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.Filter = "assetlookup.dat|assetlookup.dat|All Files (*.*)|*.*";

				if(ofd.ShowDialog() == DialogResult.OK)
				{
					assetLookup = new AssetLookup(ofd.FileName);
				}
				else Close();
			}
		}

		public void OpenTextureWindow(object sender, EventArgs e)
		{
			TextureForm texf = new TextureForm(assetLookup);
			texf.Show();
		}
	}
}