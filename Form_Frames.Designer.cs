/*
 * Created by SharpDevelop.
 * User: Mike
 * Date: 4/26/2014
 * Time: 4:00 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace WebMCam
{
	partial class Form_Frames
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Frames));
            this.picture_frame = new System.Windows.Forms.PictureBox();
            this.list_frames = new System.Windows.Forms.ListBox();
            this.cms_frame_options = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_done = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picture_frame)).BeginInit();
            this.cms_frame_options.SuspendLayout();
            this.SuspendLayout();
            // 
            // picture_frame
            // 
            this.picture_frame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picture_frame.Location = new System.Drawing.Point(212, 1);
            this.picture_frame.Name = "picture_frame";
            this.picture_frame.Size = new System.Drawing.Size(651, 478);
            this.picture_frame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picture_frame.TabIndex = 0;
            this.picture_frame.TabStop = false;
            // 
            // list_frames
            // 
            this.list_frames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.list_frames.ContextMenuStrip = this.cms_frame_options;
            this.list_frames.FormattingEnabled = true;
            this.list_frames.Location = new System.Drawing.Point(1, 1);
            this.list_frames.Name = "list_frames";
            this.list_frames.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.list_frames.Size = new System.Drawing.Size(210, 524);
            this.list_frames.TabIndex = 1;
            this.list_frames.SelectedIndexChanged += new System.EventHandler(this.List_framesSelectedIndexChanged);
            this.list_frames.KeyDown += new System.Windows.Forms.KeyEventHandler(this.List_framesKeyDown);
            // 
            // cms_frame_options
            // 
            this.cms_frame_options.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.deleteToolStripMenuItem});
            this.cms_frame_options.Name = "cms_frame_options";
            this.cms_frame_options.Size = new System.Drawing.Size(108, 32);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(104, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItemClick);
            // 
            // btn_done
            // 
            this.btn_done.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_done.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_done.Location = new System.Drawing.Point(212, 481);
            this.btn_done.Name = "btn_done";
            this.btn_done.Size = new System.Drawing.Size(651, 44);
            this.btn_done.TabIndex = 0;
            this.btn_done.Text = "Done";
            this.btn_done.UseVisualStyleBackColor = true;
            this.btn_done.Click += new System.EventHandler(this.Btn_doneClick);
            // 
            // Form_Frames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 529);
            this.Controls.Add(this.btn_done);
            this.Controls.Add(this.list_frames);
            this.Controls.Add(this.picture_frame);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Frames";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WebMCam Frames";
            this.Load += new System.EventHandler(this.Form_FramesLoad);
            ((System.ComponentModel.ISupportInitialize)(this.picture_frame)).EndInit();
            this.cms_frame_options.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ContextMenuStrip cms_frame_options;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.Button btn_done;
		private System.Windows.Forms.PictureBox picture_frame;
		private System.Windows.Forms.ListBox list_frames;
	}
}
