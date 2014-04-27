using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Diagnostics;

namespace WebMCam
{
	public partial class Form_Main : Form
	{
		Int16 frame_count;
		Int32 time_elapsed;
		String temp_storage, image_format;
		Boolean recording;
		
		public Form_Main()
		{
			InitializeComponent();
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			MainFormResize(sender, e);
			
			Ini_File.Exists("Loc", "ffmpeg", String.Format("\"{0}\\ffmpeg.exe\"", Environment.CurrentDirectory));
			Ini_File.Exists("Loc", "temp", Environment.CurrentDirectory + "\\temp\\");
			Ini_File.Exists("Cmd", "args", "-f image2 -i \"%temp%%d.png\" -r %fps% -t %duration% -vb 20M");
			Ini_File.Exists("Fmt", "pixel", "32bppRgb");
			Ini_File.Exists("Fmt", "image", "png");
			Ini_File.Exists("Fmt", "delete", "True");			
		}
		
		void MainFormResize(object sender, EventArgs e)
		{
			Text = String.Format("WebMCam {0}x{1}", panel_record.Size.Width, panel_record.Size.Height);
		}
		
		void Chk_top_mostCheckedChanged(object sender, EventArgs e)
		{
			this.TopMost = chk_top_most.Checked;
		}
		
		ImageFormat image_format_format() {
			image_format = Ini_File.Exists("Fmt", "image", "png");
			
			switch(image_format) {
				case "jpg":
					return ImageFormat.Jpeg;
				case "bmp":
					return ImageFormat.Bmp;
				default:
					return ImageFormat.Png;
			}
		}
		
		PixelFormat pixel_format_format() {
			
			switch(Ini_File.Exists("Fmt", "pixel", "32bppRgb")) {
				case "16bppRgb555":
					return PixelFormat.Format16bppRgb555;
				case "24bppRgb":
					return PixelFormat.Format24bppRgb;
				case "48bppRgb":
					return PixelFormat.Format48bppRgb;
				default:
					return PixelFormat.Format32bppRgb;
			}
		}
		
		void Timer_frameTick(object sender, EventArgs e)
		{
			// We need to get panel_records absolute position
			Point pt = panel_record.PointToScreen(new Point(0, 0));
			
			// Save our recently captured images to files instead of memory, otherwise theres a good chance we'll run out of memory	
			Bitmap bmp = Image_Capture.region(new Rectangle(pt.X, pt.Y, panel_record.Width, panel_record.Height), chk_cursor.Checked, pixel_format_format());		
			bmp.Save(String.Format("{0}{1}.{2}", temp_storage, frame_count, image_format), image_format_format());
			bmp.Dispose();
			
			frame_count++;
		}
		
		void start_record(int fps)
		{
			temp_storage = String.Format(@"{0}{1}\", Ini_File.Read("Loc", "temp"), DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
			
			// If our temp dir doesn't exist we need to create it
			if(!Directory.Exists(temp_storage))
				Directory.CreateDirectory(temp_storage);
			
			frame_count = 0;
			time_elapsed = 1;
			
			timer_frame.Interval = fps;
			
			timer_elapsed.Start();
			timer_frame.Start();
		}
		
		void stop_record()
		{
			timer_elapsed.Stop();
			timer_frame.Stop();
			
			// Show dialog and only continue if OK.
			var save = new SaveFileDialog();
			save.Title = "Select a location and name for your webm";
			save.Filter = "WebM (*.webm)|*.webm|All files (*.*)|*.*";
			
			if (save.ShowDialog() == DialogResult.OK)
			{
				Visible = false;
				
				if(MessageBox.Show("Edit before conversion?", "Would you like to remove frames before converting?",
						MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1 ) == DialogResult.Yes)
					new Form_Frames(temp_storage).ShowDialog();
				
				// Now time for the conversion			
				var ffmpeg = new ProcessStartInfo();
				ffmpeg.WorkingDirectory = Environment.CurrentDirectory;
				ffmpeg.FileName = Ini_File.Read("Loc", "ffmpeg");
				ffmpeg.Arguments = Ini_File.Read("Cmd", "args")
					.Replace("%temp%", temp_storage)
					.Replace("%duration%", Convert.ToString(time_elapsed + 1))
					.Replace("%fps%", Convert.ToString(numeric_fps.Value)) + " " + save.FileName;
				
				var process = new Process {StartInfo = ffmpeg};
				//Clipboard.SetText(ffmpeg.FileName + ffmpeg.Arguments);
				process.Start();
				process.WaitForExit();
				
				Visible = true;
			}
			
			if(Ini_File.Read("Fmt", "delete") == "True")
				Directory.Delete(temp_storage, true);
		}
		
		void Btn_recordClick(object sender, EventArgs e)
		{
			if(!File.Exists(Ini_File.Read("Loc", "ffmpeg"))) {
				MessageBox.Show(
					"Could not find FFmpeg.exe, please change your settings to set the FFmpeg.exe location.",
					"FFmpeg.exe",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				
				return;
			}
			
			if(!recording)
			{
				start_record(Convert.ToInt32(1000 / numeric_fps.Value));
				btn_record.Text = "Stop";
			}
			else
			{
				stop_record();
				MainFormResize(sender, e);
				btn_record.Text = "Record";
			}
			
			recording = !recording;
		}
		
		void Btn_settingsClick(object sender, EventArgs e)
		{
			TopMost = false;
			var form_settings = new Form_Settings();
			form_settings.ShowDialog();
			TopMost = chk_top_most.Checked;
		}
		
		void Link_tarkusLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://tarkus.co");
		}
		
		void Timer_elapsedTick(object sender, EventArgs e)
		{
			Text = String.Format("{0}s, RECORDING", time_elapsed);
			time_elapsed += 1;
		}
	}
}
