using System;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using Amib.Threading;

namespace WebMCam
{
    public partial class Form_Main : Form
	{
		static int fps;
        static List<Bitmap> frames = new List<Bitmap>();
        static int frame_count, saved_frame_count;
        int time_elapsed;
        const double version = 1.40;
        string temp_storage;
        Rectangle record_rect;
        Boolean recording, saving;

		
		PixelFormat Pixel_Format;
		ImageFormat Image_Format;
		string Image_Extension;

		// Amib Smart Thread Pool
        static SmartThreadPool threadPool = new SmartThreadPool();

        public Form_Settings settings;
		Audio_Capture Audio_Capture = new Audio_Capture();

        public Form_Main()
        {
            InitializeComponent();
        }

        void MainFormLoad(object sender, EventArgs e)
        {

            // Load Settings
            settings = new Form_Settings();

            // Restore window size from previous session
            var size = Ini_File.Exists("Frm", "size", "0,0");

            // Catch if size is incorrect and fix it if it is
            try
            {
                if (size != "0,0")
                {
                    var size_split = size.Split(',');
                    Size = new Size(Convert.ToInt32(size_split[0]), Convert.ToInt32(size_split[1]));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Ini_File.Write("Frm", "size", "0,0");
            }

            // Hit the method so the title sets the size
            MainFormResize(sender, e);
        }

        void MainFormResize(object sender, EventArgs e)
        {
            Text = string.Format("WebMCam {0} | {1}x{2}", version, panel_record.Size.Width, panel_record.Size.Height);
            Form_MainMove(sender, e);
        }

        void Form_MainMove(object sender, EventArgs e)
        {
            var pt = panel_record.PointToScreen(new Point(0, 0));
            record_rect = new Rectangle(pt.X, pt.Y, panel_record.Width, panel_record.Height);
        }

        void Chk_top_mostCheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = chk_top_most.Checked;
        }

        void bgw_captureDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (timer_elapsed.Enabled)
            {
                threadPool.QueueWorkItem(new Amib.Threading.Func<PixelFormat, int>(capture_screen), Pixel_Format);
                Thread.Sleep(1000 / fps);
            }
        }

        int capture_screen(PixelFormat format)
        {
            try
            {
				frames.Add(Image_Capture.region(record_rect, chk_cursor.Checked, format));
                frame_count++;
            }
            catch
            {
				frames.RemoveAt(1);
				Debug.WriteLine("Out of memory!");
				GC.Collect();
				//MessageBox.Show("Ran out of memory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return 0;
        }

        void Bgw_saveDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (saving || timer_elapsed.Enabled)
            {

                if (frames.Count > 1)
                {
                    try
                    {
                        // Save First Frame
                        frames[0].Save(
                            Path.Combine(
                                temp_storage, string.Format("{0}.{1}", saved_frame_count + 1, Image_Extension)
                            ),
							Image_Format
                        );

						// Remove Frame now
						frames.RemoveAt(0);
						saved_frame_count++;
					}
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
				else if (!timer_elapsed.Enabled)
				{
					saving = false;
				}
            }
        }

        void Bgw_saveProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progress_bar.Value = e.ProgressPercentage;
        }

        void start_record(int _fps)
        {
            // Set record rectangle
            Form_MainMove(null, null);

            // Dispose of each frame, if it's not empty
            if (frames.Count > 1)
            {
                for (int i = 0; i < frames.Count; i++)
                {
                    frames[i].Dispose();
                }
            }

            temp_storage = Path.Combine(
                settings.loc_temp, string.Format("WebMCam-{0}\\", DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond)
            );

            // If our temp dir doesn't exist we need to create it
            if (!Directory.Exists(temp_storage))
                Directory.CreateDirectory(temp_storage);
            
            int threads = Convert.ToInt32(settings.rec_threads);
			threadPool.Concurrency = threads;
			
			// FPS
			fps = _fps;

			// Framerate
            frame_count = 0;
            saved_frame_count = 0;
            time_elapsed = 1;

			// Set Formats
			Image_Format = Image_Capture.Image_Format(settings.fmt_image);
			Pixel_Format = Image_Capture.Pixel_Format(settings.fmt_pixel);
			Image_Extension = settings.fmt_image;

			// Set Saving & Recording variables
			saving = true;
			recording = true;

			// Record Screen
            timer_elapsed.Start();
            bgw_capture.RunWorkerAsync();
            bgw_save.RunWorkerAsync();

			// Record Audio
			if(chk_sound.Checked)
				this.Audio_Capture.Start(Path.Combine(temp_storage, "audio.wav"));
        }

        void stop_record()
        {
			recording = false;

			// Stop Screen Recording
			threadPool.Concurrency = 0;
            timer_elapsed.Stop();
            bgw_capture.CancelAsync();
            progress_bar.Value = 0;

			// Stop Sound Recording
			this.Audio_Capture.Stop();

            progress_bar.Visible = true;
            while (saving)
            {
                progress_bar.Value = Convert.ToInt32(((float)saved_frame_count / (float)frame_count) * 100);
                Thread.Sleep(1000);
            }

            // Show dialog and only continue if OK.
            var save_dialog = new SaveFileDialog();
            save_dialog.Title = "Select a location and name for your webm";
            save_dialog.Filter = "WebM (*.webm)|*.webm|All files (*.*)|*.*";

            this.Visible = false;

            Form_Frames frames_form = new Form_Frames(temp_storage, settings.fmt_image);
            bool save_frames = frames_form.ShowDialog() == DialogResult.OK;

            if (save_frames)
            {
                if (save_dialog.ShowDialog() == DialogResult.OK)
                {
                    // The SaveFileDialog handled overwrite requesting
                    if (File.Exists(save_dialog.FileName))
                        File.Delete(save_dialog.FileName);

                    Form_Output form_output = new Form_Output(
                        temp_storage,
                        settings.loc_ffmpeg,
                        save_dialog.FileName,
                        settings.cmd_args
                            .Replace("%temp%", "")
                            .Replace("%duration%", Convert.ToString(time_elapsed + 1))
                            .Replace("%bitrate%", Convert.ToString((3 * 8192) / time_elapsed) + "k")
                            .Replace("%format%", settings.fmt_image)
                            .Replace("%rfps%", Convert.ToString(frame_count / time_elapsed))
							.Replace("%audio%", chk_sound.Checked ? "-i audio.wav" : "")
                            .Replace("%fps%", Convert.ToString(numeric_fps.Value))
							+ " \"" + save_dialog.FileName + "\"",
                        frame_count
                    );

                    form_output.ShowDialog();
                    form_output.BringToFront();
                }
            }

            // Delete our temp storage folder?
            if (settings.fmt_delete == "True")
                Directory.Delete(temp_storage, true);

            // Restart WebMCam so no errors
            Application.Restart();
        }

        void Btn_recordClick(object sender, EventArgs e)
        {
            if (!File.Exists(settings.loc_ffmpeg))
            {
                MessageBox.Show(
                    "Could not find FFmpeg.exe, please change your settings to set the FFmpeg.exe location.",
                    "ffmpeg.exe",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            if (recording)
            {
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
                start_record(Convert.ToInt32(numeric_fps.Value));
                btn_record.Text = "Stop";
            }
        }

        void Btn_settingsClick(object sender, EventArgs e)
        {
            this.TopMost = false;
            settings.ShowDialog();
            this.TopMost = chk_top_most.Checked;
        }

        void Timer_elapsedTick(object sender, EventArgs e)
		{
			fps = frame_count / time_elapsed;
            this.Text = string.Format(
                "{0}s, RECORDING | {3} fps ({1} frames / {2} saved) (Threads: {4})",
                time_elapsed, frame_count, saved_frame_count,
                fps, threadPool.ActiveThreads
            );
            time_elapsed += 1;
        }

        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Ini_File.Write("Frm", "size", Width + "," + Height);
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
