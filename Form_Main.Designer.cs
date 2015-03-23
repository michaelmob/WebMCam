/*
 * Created by SharpDevelop.
 * User: Mike
 * Date: 4/25/2014
 * Time: 7:30 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace WebMCam
{
      partial class Form_Main
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
			this.Panel_Record = new System.Windows.Forms.Panel();
			this.Progress_Bar = new System.Windows.Forms.ProgressBar();
			this.btn_Record = new System.Windows.Forms.Button();
			this.chk_Top_Most = new System.Windows.Forms.CheckBox();
			this.btn_Settings = new System.Windows.Forms.Button();
			this.link_Author = new System.Windows.Forms.LinkLabel();
			this.num_FPS = new System.Windows.Forms.NumericUpDown();
			this.label_fps = new System.Windows.Forms.Label();
			this.timer_Elapsed = new System.Windows.Forms.Timer(this.components);
			this.chk_Sound = new System.Windows.Forms.CheckBox();
			this.link_GitHub = new System.Windows.Forms.LinkLabel();
			this.link_FFMpeg = new System.Windows.Forms.LinkLabel();
			this.timer_Save = new System.Windows.Forms.Timer(this.components);
			this.bgw_Capture = new System.ComponentModel.BackgroundWorker();
			this.bgw_Save = new System.ComponentModel.BackgroundWorker();
			this.chk_Cursor = new System.Windows.Forms.CheckBox();
			this.Panel_Record.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.num_FPS)).BeginInit();
			this.SuspendLayout();
			// 
			// Panel_Record
			// 
			this.Panel_Record.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Panel_Record.BackColor = System.Drawing.Color.Fuchsia;
			this.Panel_Record.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Panel_Record.Controls.Add(this.Progress_Bar);
			this.Panel_Record.ForeColor = System.Drawing.Color.Red;
			this.Panel_Record.Location = new System.Drawing.Point(0, 0);
			this.Panel_Record.Name = "Panel_Record";
			this.Panel_Record.Size = new System.Drawing.Size(251, 240);
			this.Panel_Record.TabIndex = 0;
			// 
			// Progress_Bar
			// 
			this.Progress_Bar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Progress_Bar.Location = new System.Drawing.Point(0, 216);
			this.Progress_Bar.Name = "Progress_Bar";
			this.Progress_Bar.Size = new System.Drawing.Size(251, 23);
			this.Progress_Bar.TabIndex = 0;
			this.Progress_Bar.Visible = false;
			// 
			// btn_Record
			// 
			this.btn_Record.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Record.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btn_Record.Location = new System.Drawing.Point(256, 88);
			this.btn_Record.Name = "btn_Record";
			this.btn_Record.Size = new System.Drawing.Size(77, 39);
			this.btn_Record.TabIndex = 5;
			this.btn_Record.Text = "Record";
			this.btn_Record.UseVisualStyleBackColor = true;
			this.btn_Record.Click += new System.EventHandler(this.Btn_recordClick);
			// 
			// chk_Top_Most
			// 
			this.chk_Top_Most.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chk_Top_Most.Checked = true;
			this.chk_Top_Most.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk_Top_Most.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chk_Top_Most.Location = new System.Drawing.Point(258, 3);
			this.chk_Top_Most.Name = "chk_Top_Most";
			this.chk_Top_Most.Size = new System.Drawing.Size(76, 20);
			this.chk_Top_Most.TabIndex = 7;
			this.chk_Top_Most.Text = "Top Most";
			this.chk_Top_Most.UseVisualStyleBackColor = true;
			this.chk_Top_Most.CheckedChanged += new System.EventHandler(this.Chk_top_mostCheckedChanged);
			// 
			// btn_Settings
			// 
			this.btn_Settings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Settings.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btn_Settings.Location = new System.Drawing.Point(256, 133);
			this.btn_Settings.Name = "btn_Settings";
			this.btn_Settings.Size = new System.Drawing.Size(77, 39);
			this.btn_Settings.TabIndex = 8;
			this.btn_Settings.Text = "Settings";
			this.btn_Settings.UseVisualStyleBackColor = true;
			this.btn_Settings.Click += new System.EventHandler(this.Btn_settingsClick);
			// 
			// link_Author
			// 
			this.link_Author.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.link_Author.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
			this.link_Author.Location = new System.Drawing.Point(256, 175);
			this.link_Author.Name = "link_Author";
			this.link_Author.Size = new System.Drawing.Size(77, 20);
			this.link_Author.TabIndex = 13;
			this.link_Author.TabStop = true;
			this.link_Author.Text = "MikeServer";
			this.link_Author.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.link_Author.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_author_LinkClicked);
			// 
			// num_FPS
			// 
			this.num_FPS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.num_FPS.Location = new System.Drawing.Point(284, 62);
			this.num_FPS.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
			this.num_FPS.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.num_FPS.Name = "num_FPS";
			this.num_FPS.Size = new System.Drawing.Size(49, 20);
			this.num_FPS.TabIndex = 14;
			this.num_FPS.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
			// 
			// label_fps
			// 
			this.label_fps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label_fps.Location = new System.Drawing.Point(256, 60);
			this.label_fps.Name = "label_fps";
			this.label_fps.Size = new System.Drawing.Size(30, 22);
			this.label_fps.TabIndex = 15;
			this.label_fps.Text = "FPS:";
			this.label_fps.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// timer_Elapsed
			// 
			this.timer_Elapsed.Interval = 1000;
			this.timer_Elapsed.Tick += new System.EventHandler(this.Timer_elapsedTick);
			// 
			// chk_Sound
			// 
			this.chk_Sound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chk_Sound.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chk_Sound.Location = new System.Drawing.Point(258, 41);
			this.chk_Sound.Name = "chk_Sound";
			this.chk_Sound.Size = new System.Drawing.Size(76, 20);
			this.chk_Sound.TabIndex = 16;
			this.chk_Sound.Text = "Sound";
			this.chk_Sound.UseVisualStyleBackColor = true;
			// 
			// link_GitHub
			// 
			this.link_GitHub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.link_GitHub.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
			this.link_GitHub.Location = new System.Drawing.Point(256, 195);
			this.link_GitHub.Name = "link_GitHub";
			this.link_GitHub.Size = new System.Drawing.Size(77, 20);
			this.link_GitHub.TabIndex = 17;
			this.link_GitHub.TabStop = true;
			this.link_GitHub.Text = "GitHub";
			this.link_GitHub.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.link_GitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_github_LinkClicked);
			// 
			// link_FFMpeg
			// 
			this.link_FFMpeg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.link_FFMpeg.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
			this.link_FFMpeg.Location = new System.Drawing.Point(256, 215);
			this.link_FFMpeg.Name = "link_FFMpeg";
			this.link_FFMpeg.Size = new System.Drawing.Size(77, 20);
			this.link_FFMpeg.TabIndex = 18;
			this.link_FFMpeg.TabStop = true;
			this.link_FFMpeg.Text = "FFmpeg";
			this.link_FFMpeg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.link_FFMpeg.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_ffmpeg_LinkClicked);
			// 
			// bgw_Capture
			// 
			this.bgw_Capture.WorkerSupportsCancellation = true;
			this.bgw_Capture.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_captureDoWork);
			// 
			// bgw_Save
			// 
			this.bgw_Save.WorkerReportsProgress = true;
			this.bgw_Save.WorkerSupportsCancellation = true;
			this.bgw_Save.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Bgw_saveDoWork);
			this.bgw_Save.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Bgw_saveProgressChanged);
			// 
			// chk_Cursor
			// 
			this.chk_Cursor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chk_Cursor.Checked = true;
			this.chk_Cursor.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk_Cursor.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chk_Cursor.Location = new System.Drawing.Point(258, 22);
			this.chk_Cursor.Name = "chk_Cursor";
			this.chk_Cursor.Size = new System.Drawing.Size(76, 20);
			this.chk_Cursor.TabIndex = 19;
			this.chk_Cursor.Text = "Cursor";
			this.chk_Cursor.UseVisualStyleBackColor = true;
			// 
			// Form_Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(339, 240);
			this.Controls.Add(this.chk_Sound);
			this.Controls.Add(this.chk_Cursor);
			this.Controls.Add(this.link_FFMpeg);
			this.Controls.Add(this.link_GitHub);
			this.Controls.Add(this.num_FPS);
			this.Controls.Add(this.label_fps);
			this.Controls.Add(this.link_Author);
			this.Controls.Add(this.btn_Settings);
			this.Controls.Add(this.chk_Top_Most);
			this.Controls.Add(this.btn_Record);
			this.Controls.Add(this.Panel_Record);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(107, 41);
			this.Name = "Form_Main";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WebMCam 1.40";
			this.TopMost = true;
			this.TransparencyKey = System.Drawing.Color.Fuchsia;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_Main_FormClosing);
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.Move += new System.EventHandler(this.Form_MainMove);
			this.Resize += new System.EventHandler(this.MainFormResize);
			this.Panel_Record.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.num_FPS)).EndInit();
			this.ResumeLayout(false);

            }
            private System.Windows.Forms.CheckBox chk_Sound;
            private System.Windows.Forms.Timer timer_Elapsed;
            private System.Windows.Forms.NumericUpDown num_FPS;
            private System.Windows.Forms.Label label_fps;
            private System.Windows.Forms.LinkLabel link_Author;
            private System.Windows.Forms.Button btn_Settings;
            private System.Windows.Forms.CheckBox chk_Top_Most;
            private System.Windows.Forms.Button btn_Record;
            private System.Windows.Forms.Panel Panel_Record;
        private System.Windows.Forms.LinkLabel link_GitHub;
        private System.Windows.Forms.LinkLabel link_FFMpeg;
        private System.Windows.Forms.Timer timer_Save;
        private System.ComponentModel.BackgroundWorker bgw_Capture;
        private System.ComponentModel.BackgroundWorker bgw_Save;
        private System.Windows.Forms.ProgressBar Progress_Bar;
		private System.Windows.Forms.CheckBox chk_Cursor;
      }
}
