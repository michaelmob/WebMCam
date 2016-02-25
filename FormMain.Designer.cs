namespace WebMCam
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.buttonToggle = new System.Windows.Forms.Button();
            this.displayBox = new System.Windows.Forms.PictureBox();
            this.checkBoxShowCursor = new System.Windows.Forms.CheckBox();
            this.numericUpDownFramerate = new System.Windows.Forms.NumericUpDown();
            this.labelFPS = new System.Windows.Forms.Label();
            this.buttonOptions = new System.Windows.Forms.Button();
            this.checkBoxTopMost = new System.Windows.Forms.CheckBox();
            this.linkGithub = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.displayBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFramerate)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonToggle
            // 
            this.buttonToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonToggle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonToggle.Location = new System.Drawing.Point(304, 76);
            this.buttonToggle.Name = "buttonToggle";
            this.buttonToggle.Size = new System.Drawing.Size(110, 35);
            this.buttonToggle.TabIndex = 0;
            this.buttonToggle.Text = "Record";
            this.buttonToggle.UseVisualStyleBackColor = true;
            this.buttonToggle.Click += new System.EventHandler(this.buttonToggle_Click);
            // 
            // displayBox
            // 
            this.displayBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.displayBox.BackColor = System.Drawing.Color.Fuchsia;
            this.displayBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.displayBox.Location = new System.Drawing.Point(2, 2);
            this.displayBox.Name = "displayBox";
            this.displayBox.Size = new System.Drawing.Size(300, 200);
            this.displayBox.TabIndex = 1;
            this.displayBox.TabStop = false;
            // 
            // checkBoxShowCursor
            // 
            this.checkBoxShowCursor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxShowCursor.AutoSize = true;
            this.checkBoxShowCursor.Location = new System.Drawing.Point(308, 27);
            this.checkBoxShowCursor.Name = "checkBoxShowCursor";
            this.checkBoxShowCursor.Size = new System.Drawing.Size(86, 17);
            this.checkBoxShowCursor.TabIndex = 2;
            this.checkBoxShowCursor.Text = "Show Cursor";
            this.checkBoxShowCursor.UseVisualStyleBackColor = true;
            // 
            // numericUpDownFramerate
            // 
            this.numericUpDownFramerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDownFramerate.Location = new System.Drawing.Point(335, 50);
            this.numericUpDownFramerate.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDownFramerate.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownFramerate.Name = "numericUpDownFramerate";
            this.numericUpDownFramerate.Size = new System.Drawing.Size(79, 20);
            this.numericUpDownFramerate.TabIndex = 3;
            this.numericUpDownFramerate.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // labelFPS
            // 
            this.labelFPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFPS.Location = new System.Drawing.Point(305, 50);
            this.labelFPS.Name = "labelFPS";
            this.labelFPS.Size = new System.Drawing.Size(33, 18);
            this.labelFPS.TabIndex = 4;
            this.labelFPS.Text = "FPS:";
            this.labelFPS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonOptions
            // 
            this.buttonOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOptions.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonOptions.Location = new System.Drawing.Point(304, 117);
            this.buttonOptions.Name = "buttonOptions";
            this.buttonOptions.Size = new System.Drawing.Size(110, 35);
            this.buttonOptions.TabIndex = 5;
            this.buttonOptions.Text = "Options";
            this.buttonOptions.UseVisualStyleBackColor = true;
            this.buttonOptions.Click += new System.EventHandler(this.buttonOptions_Click);
            // 
            // checkBoxTopMost
            // 
            this.checkBoxTopMost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxTopMost.AutoSize = true;
            this.checkBoxTopMost.Checked = true;
            this.checkBoxTopMost.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxTopMost.Location = new System.Drawing.Point(308, 4);
            this.checkBoxTopMost.Name = "checkBoxTopMost";
            this.checkBoxTopMost.Size = new System.Drawing.Size(96, 17);
            this.checkBoxTopMost.TabIndex = 6;
            this.checkBoxTopMost.Text = "Always on Top";
            this.checkBoxTopMost.UseVisualStyleBackColor = true;
            this.checkBoxTopMost.CheckedChanged += new System.EventHandler(this.checkBoxTopMost_CheckedChanged);
            // 
            // linkGithub
            // 
            this.linkGithub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.linkGithub.Location = new System.Drawing.Point(304, 179);
            this.linkGithub.Name = "linkGithub";
            this.linkGithub.Size = new System.Drawing.Size(110, 23);
            this.linkGithub.TabIndex = 7;
            this.linkGithub.TabStop = true;
            this.linkGithub.Text = "v2.0 (WIP)";
            this.linkGithub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkGithub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkGithub_LinkClicked);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 204);
            this.Controls.Add(this.linkGithub);
            this.Controls.Add(this.checkBoxTopMost);
            this.Controls.Add(this.numericUpDownFramerate);
            this.Controls.Add(this.buttonOptions);
            this.Controls.Add(this.labelFPS);
            this.Controls.Add(this.checkBoxShowCursor);
            this.Controls.Add(this.displayBox);
            this.Controls.Add(this.buttonToggle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "WebMCam 2.0";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Move += new System.EventHandler(this.FormMain_Move);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.displayBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFramerate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonToggle;
        private System.Windows.Forms.PictureBox displayBox;
        private System.Windows.Forms.CheckBox checkBoxShowCursor;
        private System.Windows.Forms.NumericUpDown numericUpDownFramerate;
        private System.Windows.Forms.Label labelFPS;
        private System.Windows.Forms.Button buttonOptions;
        private System.Windows.Forms.CheckBox checkBoxTopMost;
        private System.Windows.Forms.LinkLabel linkGithub;
    }
}

