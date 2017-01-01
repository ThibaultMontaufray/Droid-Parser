/*
 * Created by SharpDevelop.
 * User: C357555
 * Date: 24/08/2011
 * Time: 12:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Droid_FlatFile
{
	partial class GUI
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.imageListManager = new System.Windows.Forms.ImageList(this.components);
            this.progressBarManager = new System.Windows.Forms.ProgressBar();
            this.labelProgressBarManager = new System.Windows.Forms.Label();
            this.imageListState = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // imageListManager
            // 
            this.imageListManager.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListManager.ImageStream")));
            this.imageListManager.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListManager.Images.SetKeyName(0, "update");
            this.imageListManager.Images.SetKeyName(1, "table");
            this.imageListManager.Images.SetKeyName(2, "key");
            this.imageListManager.Images.SetKeyName(3, "barchart");
            this.imageListManager.Images.SetKeyName(4, "text");
            this.imageListManager.Images.SetKeyName(5, "tree");
            this.imageListManager.Images.SetKeyName(6, "dico");
            this.imageListManager.Images.SetKeyName(7, "convert");
            this.imageListManager.Images.SetKeyName(8, "hexa");
            this.imageListManager.Images.SetKeyName(9, "binary");
            // 
            // progressBarManager
            // 
            this.progressBarManager.Location = new System.Drawing.Point(12, 36);
            this.progressBarManager.Name = "progressBarManager";
            this.progressBarManager.Size = new System.Drawing.Size(389, 23);
            this.progressBarManager.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarManager.TabIndex = 0;
            this.progressBarManager.Value = 100;
            // 
            // labelProgressBarManager
            // 
            this.labelProgressBarManager.BackColor = System.Drawing.Color.Transparent;
            this.labelProgressBarManager.Location = new System.Drawing.Point(12, 10);
            this.labelProgressBarManager.Name = "labelProgressBarManager";
            this.labelProgressBarManager.Size = new System.Drawing.Size(389, 23);
            this.labelProgressBarManager.TabIndex = 1;
            this.labelProgressBarManager.Text = "Loading completed";
            // 
            // imageListState
            // 
            this.imageListState.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListState.ImageStream")));
            this.imageListState.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListState.Images.SetKeyName(0, "default");
            this.imageListState.Images.SetKeyName(1, "ok");
            this.imageListState.Images.SetKeyName(2, "warn");
            this.imageListState.Images.SetKeyName(3, "ko");
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 71);
            this.Controls.Add(this.labelProgressBarManager);
            this.Controls.Add(this.progressBarManager);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GUI";
            this.Text = "GUI";
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.Label labelProgressBarManager;
		private System.Windows.Forms.ProgressBar progressBarManager;
		public System.Windows.Forms.ImageList imageListState;
		public System.Windows.Forms.ImageList imageListManager;
	}
}
