/*
 * Created by SharpDevelop.
 * User: Mike
 * Date: 1/3/2015
 * Time: 9:51 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace WebMCam
{
	partial class Form_Output
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.TextBox output;
		private System.Windows.Forms.Button cancel;
		private System.Windows.Forms.ProgressBar progress_bar;
		private System.Windows.Forms.Button open;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Output));
			this.output = new System.Windows.Forms.TextBox();
			this.cancel = new System.Windows.Forms.Button();
			this.progress_bar = new System.Windows.Forms.ProgressBar();
			this.open = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// output
			// 
			this.output.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.output.BackColor = System.Drawing.SystemColors.Control;
			this.output.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.output.Location = new System.Drawing.Point(0, 0);
			this.output.Multiline = true;
			this.output.Name = "output";
			this.output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.output.Size = new System.Drawing.Size(558, 307);
			this.output.TabIndex = 0;
			this.output.WordWrap = false;
			this.output.TextChanged += new System.EventHandler(this.OutputTextChanged);
			// 
			// cancel
			// 
			this.cancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cancel.Location = new System.Drawing.Point(103, 313);
			this.cancel.Name = "cancel";
			this.cancel.Size = new System.Drawing.Size(453, 36);
			this.cancel.TabIndex = 1;
			this.cancel.Text = "Cancel";
			this.cancel.UseVisualStyleBackColor = true;
			this.cancel.Click += new System.EventHandler(this.CancelClick);
			// 
			// progress_bar
			// 
			this.progress_bar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.progress_bar.Location = new System.Drawing.Point(2, 350);
			this.progress_bar.Name = "progress_bar";
			this.progress_bar.Size = new System.Drawing.Size(554, 23);
			this.progress_bar.TabIndex = 2;
			// 
			// open
			// 
			this.open.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.open.Enabled = false;
			this.open.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.open.Location = new System.Drawing.Point(2, 313);
			this.open.Name = "open";
			this.open.Size = new System.Drawing.Size(101, 36);
			this.open.TabIndex = 3;
			this.open.Text = "Open";
			this.open.UseVisualStyleBackColor = true;
			this.open.Click += new System.EventHandler(this.OpenClick);
			// 
			// Form_Output
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(558, 375);
			this.Controls.Add(this.open);
			this.Controls.Add(this.progress_bar);
			this.Controls.Add(this.cancel);
			this.Controls.Add(this.output);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form_Output";
			this.Text = "WebMCam FFmpeg Output";
			this.Load += new System.EventHandler(this.Form_OutputLoad);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
