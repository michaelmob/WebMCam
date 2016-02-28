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
            this.textBoxFFmpegArguments = new System.Windows.Forms.TextBox();
            this.labelFFmpegArguments = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.labelFFmpegPath = new System.Windows.Forms.Label();
            this.textBoxFFmpegPath = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.labelImageFormat = new System.Windows.Forms.Label();
            this.comboBoxImageFormat = new System.Windows.Forms.ComboBox();
            this.buttonResetArguments = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxFFmpegArguments
            // 
            this.textBoxFFmpegArguments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFFmpegArguments.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFFmpegArguments.Location = new System.Drawing.Point(3, 59);
            this.textBoxFFmpegArguments.Multiline = true;
            this.textBoxFFmpegArguments.Name = "textBoxFFmpegArguments";
            this.textBoxFFmpegArguments.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxFFmpegArguments.Size = new System.Drawing.Size(319, 40);
            this.textBoxFFmpegArguments.TabIndex = 0;
            this.textBoxFFmpegArguments.WordWrap = false;
            // 
            // labelFFmpegArguments
            // 
            this.labelFFmpegArguments.AutoSize = true;
            this.labelFFmpegArguments.Location = new System.Drawing.Point(3, 43);
            this.labelFFmpegArguments.Name = "labelFFmpegArguments";
            this.labelFFmpegArguments.Size = new System.Drawing.Size(101, 13);
            this.labelFFmpegArguments.TabIndex = 1;
            this.labelFFmpegArguments.Text = "FFmpeg Arguments:";
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonSave.Location = new System.Drawing.Point(129, 143);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(193, 27);
            this.buttonSave.TabIndex = 0;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // labelFFmpegPath
            // 
            this.labelFFmpegPath.AutoSize = true;
            this.labelFFmpegPath.Location = new System.Drawing.Point(3, 3);
            this.labelFFmpegPath.Name = "labelFFmpegPath";
            this.labelFFmpegPath.Size = new System.Drawing.Size(73, 13);
            this.labelFFmpegPath.TabIndex = 4;
            this.labelFFmpegPath.Text = "FFmpeg Path:";
            // 
            // textBoxFFmpegPath
            // 
            this.textBoxFFmpegPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFFmpegPath.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFFmpegPath.Location = new System.Drawing.Point(3, 19);
            this.textBoxFFmpegPath.Multiline = true;
            this.textBoxFFmpegPath.Name = "textBoxFFmpegPath";
            this.textBoxFFmpegPath.Size = new System.Drawing.Size(254, 21);
            this.textBoxFFmpegPath.TabIndex = 0;
            this.textBoxFFmpegPath.WordWrap = false;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonBrowse.Location = new System.Drawing.Point(257, 19);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(65, 21);
            this.buttonBrowse.TabIndex = 0;
            this.buttonBrowse.Text = "...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // labelImageFormat
            // 
            this.labelImageFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelImageFormat.AutoSize = true;
            this.labelImageFormat.Location = new System.Drawing.Point(3, 102);
            this.labelImageFormat.Name = "labelImageFormat";
            this.labelImageFormat.Size = new System.Drawing.Size(74, 13);
            this.labelImageFormat.TabIndex = 6;
            this.labelImageFormat.Text = "Image Format:";
            // 
            // comboBoxImageFormat
            // 
            this.comboBoxImageFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxImageFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxImageFormat.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBoxImageFormat.FormattingEnabled = true;
            this.comboBoxImageFormat.Items.AddRange(new object[] {
            "PNG",
            "BMP",
            "JPG",
            "GIF"});
            this.comboBoxImageFormat.Location = new System.Drawing.Point(3, 118);
            this.comboBoxImageFormat.Name = "comboBoxImageFormat";
            this.comboBoxImageFormat.Size = new System.Drawing.Size(319, 21);
            this.comboBoxImageFormat.TabIndex = 0;
            // 
            // buttonResetArguments
            // 
            this.buttonResetArguments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonResetArguments.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonResetArguments.Location = new System.Drawing.Point(3, 143);
            this.buttonResetArguments.Name = "buttonResetArguments";
            this.buttonResetArguments.Size = new System.Drawing.Size(124, 27);
            this.buttonResetArguments.TabIndex = 7;
            this.buttonResetArguments.Text = "Reset Arguments";
            this.buttonResetArguments.UseVisualStyleBackColor = true;
            this.buttonResetArguments.Click += new System.EventHandler(this.buttonResetArguments_Click);
            // 
            // FormOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 172);
            this.Controls.Add(this.buttonResetArguments);
            this.Controls.Add(this.comboBoxImageFormat);
            this.Controls.Add(this.labelImageFormat);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.labelFFmpegPath);
            this.Controls.Add(this.textBoxFFmpegPath);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.labelFFmpegArguments);
            this.Controls.Add(this.textBoxFFmpegArguments);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
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

        private System.Windows.Forms.TextBox textBoxFFmpegArguments;
        private System.Windows.Forms.Label labelFFmpegArguments;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label labelFFmpegPath;
        private System.Windows.Forms.TextBox textBoxFFmpegPath;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Label labelImageFormat;
        private System.Windows.Forms.ComboBox comboBoxImageFormat;
        private System.Windows.Forms.Button buttonResetArguments;
    }
}