using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace WebMCam
{
	public partial class Form_Main : Form
    {
        static List<Bitmap> frames = new List<Bitmap>();
		static Int16 frame_count, saved_frame_count;
		Int32 time_elapsed;
		String temp_storage, image_format;
		Boolean recording;
		
		public Form_Main()
		{
			InitializeComponent();
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
            // Restore window size from previous session
            var size = Ini_File.Exists("Frm", "size", "0,0");

            if (size != "0,0")
            {
                var size_split = size.Split(',');
                this.Size = new Size(Convert.ToInt16(size_split[0]), Convert.ToInt16(size_split[1]));
            }

            // Hit the method so the title sets the size
			MainFormResize(sender, e);
			
			Ini_File.Exists("Loc", "ffmpeg", String.Format("\"{0}\\ffmpeg.exe\"", Environment.CurrentDirectory));
			Ini_File.Exists("Loc", "temp", Environment.CurrentDirectory + "\\temp\\");
			Ini_File.Exists("Cmd", "args", "-r %rfps% -i \"f_%d.%format%\" -r %fps% -vb %bitrate%");
			Ini_File.Exists("Fmt", "pixel", "32bppRgb");
			Ini_File.Exists("Fmt", "image", "png");
            Ini_File.Exists("Fmt", "delete", "True");
            Ini_File.Exists("Rec", "threads", "False");
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

        static Point pt;
		
		void Timer_frameTick(object sender, EventArgs e)
        {
            pt = panel_record.PointToScreen(new Point(0, 0));
            new Thread(delegate()
            {
                // We need to get panel_records absolute position

                frames.Add(
                    Image_Capture.region(
                        new Rectangle(pt.X, pt.Y, panel_record.Width, panel_record.Height),
                        chk_cursor.Checked,
                        pixel_format_format()
                    )
                );

                frame_count++;
            }).Start();
		}

        private void timer_save_Tick(object sender, EventArgs e)
        {
            while(frames.Count > 0) {
                Bitmap bmp = frames[0];
                bmp.Save(String.Format("{0}{1}.{2}", temp_storage, saved_frame_count, image_format), image_format_format());
                bmp.Dispose();
                frames.RemoveAt(0);
                saved_frame_count++;
            }
        }
		
		void start_record(int fps)
		{
			temp_storage = String.Format(@"{0}{1}\", Ini_File.Read("Loc", "temp"), DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
			
			// If our temp dir doesn't exist we need to create it
			if(!Directory.Exists(temp_storage))
				Directory.CreateDirectory(temp_storage);

            frame_count = 0;
            saved_frame_count = 0;
			time_elapsed = 1;
			
			timer_frame.Interval = 1000 / fps;
			
			timer_elapsed.Start();
			timer_frame.Start();
            timer_save.Start();
		}
		
		void stop_record()
		{
			timer_elapsed.Stop();
			timer_frame.Stop();
            
            // While we still have frames that need to be saved, lets keep running
            // the save timer
            while (frames.Count < 0) {
                timer_save.Stop();
            }
			
			// Show dialog and only continue if OK.
			var save = new SaveFileDialog();
			save.Title = "Select a location and name for your webm";
			save.Filter = "WebM (*.webm)|*.webm|All files (*.*)|*.*";
			
			if (save.ShowDialog() == DialogResult.OK)
			{
				Visible = false;

				// The SaveFileDialog handled overwrite requesting
				if (File.Exists(save.FileName))
					File.Delete(save.FileName);

				new Form_Frames(temp_storage).ShowDialog();
				
				// Now time for the conversion			
				var ffmpeg = new ProcessStartInfo();
				ffmpeg.WorkingDirectory = temp_storage;
				ffmpeg.FileName = Ini_File.Read("Loc", "ffmpeg");
				ffmpeg.Arguments = Ini_File.Read("Cmd", "args")
                    .Replace("%temp%", "")
					.Replace("%duration%", Convert.ToString(time_elapsed + 1))
					.Replace("%bitrate%", Convert.ToString((3 * 8192) / time_elapsed) + "k")
					.Replace("%format%", Ini_File.Read("Fmt", "image"))
					.Replace("%rfps%", Convert.ToString(frame_count / time_elapsed))
					.Replace("%fps%", Convert.ToString(numeric_fps.Value)) + " \"" + save.FileName + "\"";

                Debug.WriteLine(ffmpeg.FileName + " " + ffmpeg.Arguments);
				
				var process = new Process {StartInfo = ffmpeg};
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
					"ffmpeg.exe",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				
				return;
			}
			
			if(recording) {
                // Now that we're done recording, set back to Record
                // and allow any maximum size
                this.MinimumSize = new Size(107, 155);
                this.MaximumSize = new Size(0, 0);
				stop_record();
				MainFormResize(sender, e);
				btn_record.Text = "Record";
			}
			else
            {
                // If we're recording, let's set the minimum and maximum size
                // limits so we don't get an error while trying to convert
                // with ffmpeg due to different image sizes
                this.MinimumSize = new Size(this.Width, this.Height);
                this.MaximumSize = new Size(this.Width, this.Height);
				start_record(Convert.ToInt32(1000 / numeric_fps.Value));
                btn_record.Text = "Stop";
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
		
		void Timer_elapsedTick(object sender, EventArgs e)
		{
			Text = String.Format("{0}s, RECORDING ({1} frames)", time_elapsed, frame_count);
			time_elapsed += 1;
		}

        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Ini_File.Write("Frm", "size", this.Width.ToString() + "," + this.Height.ToString());
        }

        #region Links
        private void link_author_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://mikeserver.org/");
        }

        private void link_github_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/TheTarkus/WebMCam");
        }

        private void link_ffmpeg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://ffmpeg.org/");
        }
        #endregion
    }
}
