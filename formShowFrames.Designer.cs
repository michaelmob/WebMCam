namespace WebMCam
{
    partial class FormShowFrames
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormShowFrames));
            this.buttonDone = new System.Windows.Forms.Button();
            this.listBoxFrames = new System.Windows.Forms.ListBox();
            this.contextMenuStripFrames = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBoxFrame = new System.Windows.Forms.PictureBox();
            this.contextMenuStripFrames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFrame)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonDone
            // 
            this.buttonDone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDone.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonDone.Location = new System.Drawing.Point(-1, 198);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(422, 26);
            this.buttonDone.TabIndex = 0;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // listBoxFrames
            // 
            this.listBoxFrames.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxFrames.ContextMenuStrip = this.contextMenuStripFrames;
            this.listBoxFrames.FormattingEnabled = true;
            this.listBoxFrames.Location = new System.Drawing.Point(0, 0);
            this.listBoxFrames.Name = "listBoxFrames";
            this.listBoxFrames.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxFrames.Size = new System.Drawing.Size(120, 199);
            this.listBoxFrames.TabIndex = 0;
            this.listBoxFrames.SelectedIndexChanged += new System.EventHandler(this.listBoxFrames_SelectedIndexChanged);
            this.listBoxFrames.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBoxFrames_KeyDown);
            // 
            // contextMenuStripFrames
            // 
            this.contextMenuStripFrames.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.contextMenuStripFrames.Name = "contextMenuStripFrames";
            this.contextMenuStripFrames.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // pictureBoxFrame
            // 
            this.pictureBoxFrame.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxFrame.Location = new System.Drawing.Point(121, 0);
            this.pictureBoxFrame.Name = "pictureBoxFrame";
            this.pictureBoxFrame.Size = new System.Drawing.Size(300, 199);
            this.pictureBoxFrame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxFrame.TabIndex = 2;
            this.pictureBoxFrame.TabStop = false;
            // 
            // FormShowFrames
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 223);
            this.Controls.Add(this.pictureBoxFrame);
            this.Controls.Add(this.listBoxFrames);
            this.Controls.Add(this.buttonDone);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "FormShowFrames";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WebMCam Frames";
            this.Load += new System.EventHandler(this.formShowFrames_Load);
            this.contextMenuStripFrames.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFrame)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.ListBox listBoxFrames;
        private System.Windows.Forms.PictureBox pictureBoxFrame;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripFrames;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
    }
}