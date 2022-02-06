namespace InsomniacGamesPS3EngineEditor
{

	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			btnOpenTextureWindow = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnOpenTextureWindow
			// 
			this.btnOpenTextureWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOpenTextureWindow.Location = new System.Drawing.Point(12, 12);
			this.btnOpenTextureWindow.Name = "btnOpenTextureWindow";
			this.btnOpenTextureWindow.Size = new System.Drawing.Size(376, 30);
			this.btnOpenTextureWindow.TabIndex = 10;
			this.btnOpenTextureWindow.Text = "Edit Textures";
			this.btnOpenTextureWindow.UseVisualStyleBackColor = true;
			this.btnOpenTextureWindow.Click += new System.EventHandler(this.OpenTextureWindow);
			//
			// Form1
			//
			this.components = new System.ComponentModel.Container();
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(400, 400);
			this.Controls.Add(btnOpenTextureWindow);
			this.Text = "Insomniac Games PS3 Engine Editor";
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		public System.Windows.Forms.Button btnOpenTextureWindow;

		#endregion
	}
}