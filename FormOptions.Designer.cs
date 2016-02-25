namespace WebMCam
{
    partial class FormOptions
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
            this.textBoxFFmpegArgs = new System.Windows.Forms.TextBox();
            this.labelFFmpegArgs = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelFFmpegLocation = new System.Windows.Forms.Label();
            this.textBoxFFmpegLocation = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxFFmpegArgs
            // 
            this.textBoxFFmpegArgs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFFmpegArgs.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFFmpegArgs.Location = new System.Drawing.Point(3, 59);
            this.textBoxFFmpegArgs.Multiline = true;
            this.textBoxFFmpegArgs.Name = "textBoxFFmpegArgs";
            this.textBoxFFmpegArgs.Size = new System.Drawing.Size(283, 159);
            this.textBoxFFmpegArgs.TabIndex = 0;
            this.textBoxFFmpegArgs.Text = "{cursor}\r\n-f gdigrab\r\n-r {fps}\r\n-i desktop\r\n{size}\r\n{video}\r\n-b:v 1M\r\n-fs 3M\r\n-y " +
    "{output}";
            // 
            // labelFFmpegArgs
            // 
            this.labelFFmpegArgs.AutoSize = true;
            this.labelFFmpegArgs.Location = new System.Drawing.Point(3, 43);
            this.labelFFmpegArgs.Name = "labelFFmpegArgs";
            this.labelFFmpegArgs.Size = new System.Drawing.Size(72, 13);
            this.labelFFmpegArgs.TabIndex = 1;
            this.labelFFmpegArgs.Text = "FFmpeg Args:";
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(3, 221);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(283, 27);
            this.buttonSave.TabIndex = 2;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelFFmpegLocation
            // 
            this.labelFFmpegLocation.AutoSize = true;
            this.labelFFmpegLocation.Location = new System.Drawing.Point(3, 3);
            this.labelFFmpegLocation.Name = "labelFFmpegLocation";
            this.labelFFmpegLocation.Size = new System.Drawing.Size(92, 13);
            this.labelFFmpegLocation.TabIndex = 4;
            this.labelFFmpegLocation.Text = "FFmpeg Location:";
            // 
            // textBoxFFmpegLocation
            // 
            this.textBoxFFmpegLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFFmpegLocation.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFFmpegLocation.Location = new System.Drawing.Point(3, 19);
            this.textBoxFFmpegLocation.Multiline = true;
            this.textBoxFFmpegLocation.Name = "textBoxFFmpegLocation";
            this.textBoxFFmpegLocation.Size = new System.Drawing.Size(283, 21);
            this.textBoxFFmpegLocation.TabIndex = 3;
            this.textBoxFFmpegLocation.Text = "ffmpeg.exe";
            this.textBoxFFmpegLocation.WordWrap = false;
            // 
            // FormOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 251);
            this.Controls.Add(this.labelFFmpegLocation);
            this.Controls.Add(this.textBoxFFmpegLocation);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelFFmpegArgs);
            this.Controls.Add(this.textBoxFFmpegArgs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormOptions";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "WebMCam Options";
            this.Load += new System.EventHandler(this.FormOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFFmpegArgs;
        private System.Windows.Forms.Label labelFFmpegArgs;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelFFmpegLocation;
        private System.Windows.Forms.TextBox textBoxFFmpegLocation;
    }
}