namespace InsomniacGamesPS3EngineEditor
{
	partial class TextureForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lbFiles = new System.Windows.Forms.ListBox();
			this.pbCurrentItem = new System.Windows.Forms.PictureBox();
			this.btnExtract = new System.Windows.Forms.Button();
			this.btnReplace = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lbFiles
			// 
			this.lbFiles.FormattingEnabled = true;
			this.lbFiles.Location = new System.Drawing.Point(12, 12);
			this.lbFiles.Name = "lbFiles";
			this.lbFiles.Size = new System.Drawing.Size(232, 476);
			this.lbFiles.TabIndex = 0;
			this.lbFiles.IntegralHeight = false;
			this.lbFiles.SelectedIndexChanged += new System.EventHandler(this.NewItemSelected);
			//
			// pbCurrentItem
			//
			this.pbCurrentItem.Location = new System.Drawing.Point(244, 12);
			this.pbCurrentItem.Name = "pbCurrentItem";
			this.pbCurrentItem.Size = new System.Drawing.Size(432, 476);
			this.pbCurrentItem.TabIndex = 0;
			this.pbCurrentItem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbCurrentItem.ClientSize = new System.Drawing.Size(432, 476);
			//
			// btnExtract
			//
			this.btnExtract.Location = new System.Drawing.Point(613, 465);
			this.btnExtract.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			this.btnExtract.Name = "btnExtract";
			this.btnExtract.Size = new System.Drawing.Size(75, 23);
			this.btnExtract.TabIndex = 2;
			this.btnExtract.Text = "Extract";
			this.btnExtract.UseVisualStyleBackColor = true;
			this.btnExtract.Click += new System.EventHandler(this.Extract);
			//
			// btnReplace
			//
			this.btnReplace.Location = new System.Drawing.Point(526, 465);
			this.btnReplace.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			this.btnReplace.Name = "btnReplace";
			this.btnReplace.Size = new System.Drawing.Size(75, 23);
			this.btnReplace.TabIndex = 2;
			this.btnReplace.Text = "Replace";
			this.btnReplace.UseVisualStyleBackColor = true;
			this.btnReplace.Click += new System.EventHandler(this.Replace);
			//
			// TextureForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(700, 500);
			this.Controls.Add(this.lbFiles);
			this.Controls.Add(this.pbCurrentItem);
			this.Controls.Add(this.btnExtract);
			this.Controls.Add(this.btnReplace);
			this.Name = "TextureForm";
			this.Text = "Texture Editor";
			this.Resize += new System.EventHandler(this.ResizeForm);
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.ListBox lbFiles;
		private System.Windows.Forms.PictureBox pbCurrentItem;
		private System.Windows.Forms.Button btnExtract;
		private System.Windows.Forms.Button btnReplace;
	}
}