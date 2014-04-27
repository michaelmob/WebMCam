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
			this.panel_record = new System.Windows.Forms.Panel();
			this.btn_record = new System.Windows.Forms.Button();
			this.chk_top_most = new System.Windows.Forms.CheckBox();
			this.timer_frame = new System.Windows.Forms.Timer(this.components);
			this.btn_settings = new System.Windows.Forms.Button();
			this.link_tarkus = new System.Windows.Forms.LinkLabel();
			this.numeric_fps = new System.Windows.Forms.NumericUpDown();
			this.label_fps = new System.Windows.Forms.Label();
			this.timer_elapsed = new System.Windows.Forms.Timer(this.components);
			this.chk_cursor = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.numeric_fps)).BeginInit();
			this.SuspendLayout();
			// 
			// panel_record
			// 
			this.panel_record.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.panel_record.BackColor = System.Drawing.Color.Fuchsia;
			this.panel_record.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel_record.Location = new System.Drawing.Point(0, 0);
			this.panel_record.Name = "panel_record";
			this.panel_record.Size = new System.Drawing.Size(247, 220);
			this.panel_record.TabIndex = 0;
			// 
			// btn_record
			// 
			this.btn_record.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_record.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btn_record.Location = new System.Drawing.Point(252, 74);
			this.btn_record.Name = "btn_record";
			this.btn_record.Size = new System.Drawing.Size(77, 39);
			this.btn_record.TabIndex = 5;
			this.btn_record.Text = "Record";
			this.btn_record.UseVisualStyleBackColor = true;
			this.btn_record.Click += new System.EventHandler(this.Btn_recordClick);
			// 
			// chk_top_most
			// 
			this.chk_top_most.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chk_top_most.Checked = true;
			this.chk_top_most.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk_top_most.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chk_top_most.Location = new System.Drawing.Point(254, 3);
			this.chk_top_most.Name = "chk_top_most";
			this.chk_top_most.Size = new System.Drawing.Size(76, 24);
			this.chk_top_most.TabIndex = 7;
			this.chk_top_most.Text = "Top Most";
			this.chk_top_most.UseVisualStyleBackColor = true;
			this.chk_top_most.CheckedChanged += new System.EventHandler(this.Chk_top_mostCheckedChanged);
			// 
			// timer_frame
			// 
			this.timer_frame.Tick += new System.EventHandler(this.Timer_frameTick);
			// 
			// btn_settings
			// 
			this.btn_settings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_settings.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btn_settings.Location = new System.Drawing.Point(252, 119);
			this.btn_settings.Name = "btn_settings";
			this.btn_settings.Size = new System.Drawing.Size(77, 39);
			this.btn_settings.TabIndex = 8;
			this.btn_settings.Text = "Settings";
			this.btn_settings.UseVisualStyleBackColor = true;
			this.btn_settings.Click += new System.EventHandler(this.Btn_settingsClick);
			// 
			// link_tarkus
			// 
			this.link_tarkus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.link_tarkus.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
			this.link_tarkus.Location = new System.Drawing.Point(252, 161);
			this.link_tarkus.Name = "link_tarkus";
			this.link_tarkus.Size = new System.Drawing.Size(77, 20);
			this.link_tarkus.TabIndex = 13;
			this.link_tarkus.TabStop = true;
			this.link_tarkus.Text = "Tarkus.co";
			this.link_tarkus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.link_tarkus.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_tarkusLinkClicked);
			// 
			// numeric_fps
			// 
			this.numeric_fps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.numeric_fps.Location = new System.Drawing.Point(280, 48);
			this.numeric_fps.Name = "numeric_fps";
			this.numeric_fps.Size = new System.Drawing.Size(49, 20);
			this.numeric_fps.TabIndex = 14;
			this.numeric_fps.Value = new decimal(new int[] {
			30,
			0,
			0,
			0});
			// 
			// label_fps
			// 
			this.label_fps.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label_fps.Location = new System.Drawing.Point(252, 46);
			this.label_fps.Name = "label_fps";
			this.label_fps.Size = new System.Drawing.Size(30, 22);
			this.label_fps.TabIndex = 15;
			this.label_fps.Text = "FPS:";
			this.label_fps.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// timer_elapsed
			// 
			this.timer_elapsed.Interval = 1000;
			this.timer_elapsed.Tick += new System.EventHandler(this.Timer_elapsedTick);
			// 
			// chk_cursor
			// 
			this.chk_cursor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chk_cursor.Checked = true;
			this.chk_cursor.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chk_cursor.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chk_cursor.Location = new System.Drawing.Point(254, 23);
			this.chk_cursor.Name = "chk_cursor";
			this.chk_cursor.Size = new System.Drawing.Size(76, 24);
			this.chk_cursor.TabIndex = 16;
			this.chk_cursor.Text = "Cursor";
			this.chk_cursor.UseVisualStyleBackColor = true;
			// 
			// Form_Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(335, 220);
			this.Controls.Add(this.chk_cursor);
			this.Controls.Add(this.numeric_fps);
			this.Controls.Add(this.label_fps);
			this.Controls.Add(this.link_tarkus);
			this.Controls.Add(this.btn_settings);
			this.Controls.Add(this.chk_top_most);
			this.Controls.Add(this.btn_record);
			this.Controls.Add(this.panel_record);
			this.DoubleBuffered = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(100, 100);
			this.Name = "Form_Main";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WebMCam";
			this.TopMost = true;
			this.TransparencyKey = System.Drawing.Color.Fuchsia;
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.Resize += new System.EventHandler(this.MainFormResize);
			((System.ComponentModel.ISupportInitialize)(this.numeric_fps)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.CheckBox chk_cursor;
		private System.Windows.Forms.Timer timer_elapsed;
		private System.Windows.Forms.NumericUpDown numeric_fps;
		private System.Windows.Forms.Label label_fps;
		private System.Windows.Forms.LinkLabel link_tarkus;
		private System.Windows.Forms.Button btn_settings;
		private System.Windows.Forms.Timer timer_frame;
		private System.Windows.Forms.CheckBox chk_top_most;
		private System.Windows.Forms.Button btn_record;
		private System.Windows.Forms.Panel panel_record;
	}
}
