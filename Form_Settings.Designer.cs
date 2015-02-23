/*
 * Created by SharpDevelop.
 * User: Mike
 * Date: 4/25/2014
 * Time: 11:48 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace WebMCam
{
      partial class Form_Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Settings));
            this.label_ffmpeg = new System.Windows.Forms.Label();
            this.text_ffmpeg = new System.Windows.Forms.TextBox();
            this.text_temp = new System.Windows.Forms.TextBox();
            this.label_temp = new System.Windows.Forms.Label();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.text_args = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label_file = new System.Windows.Forms.Label();
            this.combo_image_format = new System.Windows.Forms.ComboBox();
            this.combo_pixel_format = new System.Windows.Forms.ComboBox();
            this.label_pixelformat = new System.Windows.Forms.Label();
            this.btn_reset = new System.Windows.Forms.Button();
            this.chk_delete_frames = new System.Windows.Forms.CheckBox();
            this.btn_open_temp = new System.Windows.Forms.Button();
            this.threads_label = new System.Windows.Forms.Label();
            this.numeric_threads = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_threads)).BeginInit();
            this.SuspendLayout();
            // 
            // label_ffmpeg
            // 
            this.label_ffmpeg.Location = new System.Drawing.Point(4, 3);
            this.label_ffmpeg.Name = "label_ffmpeg";
            this.label_ffmpeg.Size = new System.Drawing.Size(268, 18);
            this.label_ffmpeg.TabIndex = 0;
            this.label_ffmpeg.Text = "FFmpeg Location:";
            this.label_ffmpeg.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // text_ffmpeg
            // 
            this.text_ffmpeg.Location = new System.Drawing.Point(12, 24);
            this.text_ffmpeg.Name = "text_ffmpeg";
            this.text_ffmpeg.Size = new System.Drawing.Size(265, 20);
            this.text_ffmpeg.TabIndex = 1;
            // 
            // text_temp
            // 
            this.text_temp.Location = new System.Drawing.Point(12, 66);
            this.text_temp.Name = "text_temp";
            this.text_temp.Size = new System.Drawing.Size(265, 20);
            this.text_temp.TabIndex = 5;
            // 
            // label_temp
            // 
            this.label_temp.Location = new System.Drawing.Point(4, 45);
            this.label_temp.Name = "label_temp";
            this.label_temp.Size = new System.Drawing.Size(268, 18);
            this.label_temp.TabIndex = 4;
            this.label_temp.Text = "Temp Folder Location:";
            this.label_temp.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // btn_save
            // 
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_save.Location = new System.Drawing.Point(147, 291);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(130, 25);
            this.btn_save.TabIndex = 7;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.Btn_saveClick);
            // 
            // btn_cancel
            // 
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_cancel.Location = new System.Drawing.Point(12, 291);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(130, 25);
            this.btn_cancel.TabIndex = 8;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.Btn_cancelClick);
            // 
            // text_args
            // 
            this.text_args.Location = new System.Drawing.Point(12, 107);
            this.text_args.Name = "text_args";
            this.text_args.Size = new System.Drawing.Size(265, 20);
            this.text_args.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(268, 18);
            this.label1.TabIndex = 9;
            this.label1.Text = "FFmpeg Arguments:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label_file
            // 
            this.label_file.Location = new System.Drawing.Point(4, 127);
            this.label_file.Name = "label_file";
            this.label_file.Size = new System.Drawing.Size(268, 18);
            this.label_file.TabIndex = 11;
            this.label_file.Text = "Image Files Format:";
            this.label_file.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // combo_image_format
            // 
            this.combo_image_format.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.combo_image_format.FormattingEnabled = true;
            this.combo_image_format.Items.AddRange(new object[] {
            "png",
            "bmp",
            "jpg"});
            this.combo_image_format.Location = new System.Drawing.Point(12, 148);
            this.combo_image_format.Name = "combo_image_format";
            this.combo_image_format.Size = new System.Drawing.Size(265, 21);
            this.combo_image_format.TabIndex = 12;
            // 
            // combo_pixel_format
            // 
            this.combo_pixel_format.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.combo_pixel_format.FormattingEnabled = true;
            this.combo_pixel_format.Items.AddRange(new object[] {
            "16bppRgb555",
            "24bppRgb",
            "32bppRgb",
            "48bppRgb"});
            this.combo_pixel_format.Location = new System.Drawing.Point(12, 190);
            this.combo_pixel_format.Name = "combo_pixel_format";
            this.combo_pixel_format.Size = new System.Drawing.Size(265, 21);
            this.combo_pixel_format.TabIndex = 14;
            // 
            // label_pixelformat
            // 
            this.label_pixelformat.Location = new System.Drawing.Point(4, 169);
            this.label_pixelformat.Name = "label_pixelformat";
            this.label_pixelformat.Size = new System.Drawing.Size(268, 18);
            this.label_pixelformat.TabIndex = 13;
            this.label_pixelformat.Text = "Pixel Format:";
            this.label_pixelformat.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // btn_reset
            // 
            this.btn_reset.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_reset.Location = new System.Drawing.Point(220, 0);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(70, 21);
            this.btn_reset.TabIndex = 15;
            this.btn_reset.Text = "Reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.Btn_resetClick);
            // 
            // chk_delete_frames
            // 
            this.chk_delete_frames.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chk_delete_frames.Location = new System.Drawing.Point(12, 261);
            this.chk_delete_frames.Name = "chk_delete_frames";
            this.chk_delete_frames.Size = new System.Drawing.Size(175, 24);
            this.chk_delete_frames.TabIndex = 16;
            this.chk_delete_frames.Text = "Delete frames after conversion";
            this.chk_delete_frames.UseVisualStyleBackColor = true;
            // 
            // btn_open_temp
            // 
            this.btn_open_temp.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_open_temp.Location = new System.Drawing.Point(182, 259);
            this.btn_open_temp.Name = "btn_open_temp";
            this.btn_open_temp.Size = new System.Drawing.Size(95, 27);
            this.btn_open_temp.TabIndex = 17;
            this.btn_open_temp.Text = "Open Temp";
            this.btn_open_temp.UseVisualStyleBackColor = true;
            this.btn_open_temp.Click += new System.EventHandler(this.Btn_open_tempClick);
            // 
            // threads_label
            // 
            this.threads_label.Location = new System.Drawing.Point(4, 212);
            this.threads_label.Name = "threads_label";
            this.threads_label.Size = new System.Drawing.Size(268, 18);
            this.threads_label.TabIndex = 18;
            this.threads_label.Text = "Recording Threads";
            this.threads_label.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // numeric_threads
            // 
            this.numeric_threads.Location = new System.Drawing.Point(12, 233);
            this.numeric_threads.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numeric_threads.Name = "numeric_threads";
            this.numeric_threads.Size = new System.Drawing.Size(264, 20);
            this.numeric_threads.TabIndex = 19;
            this.numeric_threads.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Form_Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 326);
            this.Controls.Add(this.numeric_threads);
            this.Controls.Add(this.threads_label);
            this.Controls.Add(this.btn_open_temp);
            this.Controls.Add(this.chk_delete_frames);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.combo_pixel_format);
            this.Controls.Add(this.label_pixelformat);
            this.Controls.Add(this.combo_image_format);
            this.Controls.Add(this.label_file);
            this.Controls.Add(this.text_args);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.text_temp);
            this.Controls.Add(this.label_temp);
            this.Controls.Add(this.text_ffmpeg);
            this.Controls.Add(this.label_ffmpeg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Settings";
            this.ShowInTaskbar = false;
            this.Text = "WebMCam Settings";
            this.Load += new System.EventHandler(this.Form_Settings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numeric_threads)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            }
            private System.Windows.Forms.NumericUpDown numeric_threads;
            private System.Windows.Forms.Label threads_label;
            private System.Windows.Forms.CheckBox chk_delete_frames;
            private System.Windows.Forms.Button btn_open_temp;
            private System.Windows.Forms.Button btn_reset;
            private System.Windows.Forms.Label label_file;
            private System.Windows.Forms.ComboBox combo_image_format;
            private System.Windows.Forms.ComboBox combo_pixel_format;
            private System.Windows.Forms.Label label_pixelformat;
            private System.Windows.Forms.TextBox text_args;
            private System.Windows.Forms.Label label1;
            private System.Windows.Forms.Button btn_cancel;
            private System.Windows.Forms.TextBox text_temp;
            private System.Windows.Forms.Label label_temp;
            private System.Windows.Forms.Button btn_save;
            private System.Windows.Forms.Label label_ffmpeg;
        private System.Windows.Forms.TextBox text_ffmpeg;
      }
}
