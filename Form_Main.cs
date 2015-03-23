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
		static int FPS;
        static List<Bitmap> Frames = new List<Bitmap>();
        static int Frame_Count, Saved_Frame_Count;
        int Time_Elapsed;
        const double Version = 1.40;
        string Temp_Storage;
        Rectangle Record_Rectangle;
        Boolean Is_Recording, Is_Saving;

		
		PixelFormat Pixel_Format;
		ImageFormat Image_Format;
		string Image_Extension;

		// Amib Smart Thread Pool
        static SmartThreadPool Thread_Pool = new SmartThreadPool();

        public Form_Settings Settings;
		Audio_Capture Audio_Capture = new Audio_Capture();

        public Form_Main()
        {
            InitializeComponent();
        }

        void MainFormLoad(object sender, EventArgs e)
        {

            // Load Settings
            Settings = new Form_Settings();

            // Restore window size from previous session
            var Size = Ini_File.Exists("Frm", "size", "0,0");

            // Catch if size is incorrect and fix it if it is
            try
            {
                if (Size != "0,0")
                {
                    var Size_Split = Size.Split(',');
                    this.Size = new Size(Convert.ToInt32(Size_Split[0]), Convert.ToInt32(Size_Split[1]));
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
            Text = string.Format("WebMCam {0} | {1}x{2}", Version, Panel_Record.Size.Width, Panel_Record.Size.Height);
            Form_MainMove(sender, e);
        }

        void Form_MainMove(object sender, EventArgs e)
        {
            var pt = Panel_Record.PointToScreen(new Point(0, 0));
            Record_Rectangle = new Rectangle(pt.X, pt.Y, Panel_Record.Width, Panel_Record.Height);
        }

        void Chk_top_mostCheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = chk_Top_Most.Checked;
        }

        void bgw_captureDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (timer_Elapsed.Enabled)
            {
                Thread_Pool.QueueWorkItem(new Amib.Threading.Func<PixelFormat, int>(capture_screen), Pixel_Format);
                Thread.Sleep(1000 / FPS);
            }
        }

        int capture_screen(PixelFormat format)
        {
            try
            {
				Frames.Add(Image_Capture.region(Record_Rectangle, chk_Cursor.Checked, format));
                Frame_Count++;
            }
            catch
            {
				Frames.RemoveAt(1);
				Debug.WriteLine("Out of memory!");
				GC.Collect();
				//MessageBox.Show("Ran out of memory!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return 0;
        }

        void Bgw_saveDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while (Is_Saving || timer_Elapsed.Enabled)
            {

                if (Frames.Count > 1)
                {
                    try
                    {
                        // Save First Frame
                        Frames[0].Save(
                            Path.Combine(
                                Temp_Storage, string.Format("{0}.{1}", Saved_Frame_Count + 1, Image_Extension)
                            ),
							Image_Format
                        );

						// Remove Frame now
						Frames.RemoveAt(0);
						Saved_Frame_Count++;
					}
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
				else if (!timer_Elapsed.Enabled)
				{
					Is_Saving = false;
				}
            }
        }

        void Bgw_saveProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            Progress_Bar.Value = e.ProgressPercentage;
        }

        void Start_Record(int _fps)
        {
            // Set record rectangle
            Form_MainMove(null, null);

            // Dispose of each frame, if it's not empty
            if (Frames.Count > 1)
            {
                for (int i = 0; i < Frames.Count; i++)
                {
                    Frames[i].Dispose();
                }
            }

            Temp_Storage = Path.Combine(
                Settings.loc_temp, string.Format("WebMCam-{0}\\", DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond)
            );

            // If our temp dir doesn't exist we need to create it
            if (!Directory.Exists(Temp_Storage))
                Directory.CreateDirectory(Temp_Storage);
            
            int threads = Convert.ToInt32(Settings.rec_threads);
			Thread_Pool.Concurrency = threads;
			
			// FPS
			FPS = _fps;

			// Framerate
            Frame_Count = 0;
            Saved_Frame_Count = 0;
            Time_Elapsed = 1;

			// Set Formats
			Image_Format = Image_Capture.Image_Format(Settings.fmt_image);
			Pixel_Format = Image_Capture.Pixel_Format(Settings.fmt_pixel);
			Image_Extension = Settings.fmt_image;

			// Set Saving & Recording variables
			Is_Saving = true;
			Is_Recording = true;

			// Record Screen
            timer_Elapsed.Start();
            bgw_Capture.RunWorkerAsync();
            bgw_Save.RunWorkerAsync();

			// Record Audio
			if(chk_Sound.Checked)
				this.Audio_Capture.Start(Path.Combine(Temp_Storage, "audio.wav"));
        }

        void Stop_Record()
        {
			Is_Recording = false;

			// Stop Sound Recording
			this.Audio_Capture.Stop();

			// Stop Screen Recording
			Thread_Pool.Concurrency = 0;
            timer_Elapsed.Stop();
            bgw_Capture.CancelAsync();
            Progress_Bar.Value = 0;

            Progress_Bar.Visible = true;
            while (Is_Saving)
            {
                Progress_Bar.Value = Convert.ToInt32(((float)Saved_Frame_Count / (float)Frame_Count) * 100);
                Thread.Sleep(1000);
            }

            this.Visible = false;

			if (!chk_Sound.Checked)
			{
				Form_Frames Frames_Form = new Form_Frames(Temp_Storage, Settings.fmt_image);
				bool Save_Frames = Frames_Form.ShowDialog() == DialogResult.OK;
			}

			// Show dialog and only continue if OK.
			var Save_Dialog = new SaveFileDialog();
			Save_Dialog.Title = "Select a location and name for your webm";
			Save_Dialog.Filter = "WebM (*.webm)|*.webm|All files (*.*)|*.*";

			if (Save_Dialog.ShowDialog() == DialogResult.OK)
			{
				// The SaveFileDialog handled overwrite requesting
				if (File.Exists(Save_Dialog.FileName))
					File.Delete(Save_Dialog.FileName);

				Form_Output form_output = new Form_Output(
					Temp_Storage,
					Settings.loc_ffmpeg,
					Save_Dialog.FileName,
					Settings.cmd_args
						.Replace("%temp%", "")
						.Replace("%duration%", Convert.ToString(Time_Elapsed + 1))
						.Replace("%bitrate%", Convert.ToString((3 * 8192) / Time_Elapsed) + "k")
						.Replace("%format%", Settings.fmt_image)
						.Replace("%rfps%", Convert.ToString(Frame_Count / Time_Elapsed))
						.Replace("%audio%", chk_Sound.Checked ? "-i audio.wav" : "")
						.Replace("%fps%", Convert.ToString(num_FPS.Value))
						+ " \"" + Save_Dialog.FileName + "\"",
					Frame_Count
				);

				form_output.ShowDialog();
				form_output.BringToFront();
			}

            // Delete our temp storage folder?
            if (Settings.fmt_delete == "True")
                Directory.Delete(Temp_Storage, true);

            // Restart WebMCam so no errors
            Application.Restart();
        }

        void Btn_recordClick(object sender, EventArgs e)
        {
            if (!File.Exists(Settings.loc_ffmpeg))
            {
                MessageBox.Show(
                    "Could not find FFmpeg.exe, please change your settings to set the FFmpeg.exe location.",
                    "ffmpeg.exe",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            if (Is_Recording)
            {
                // Now that we're done recording, set back to Record
                // and allow any maximum size
                this.MinimumSize = new Size(107, 155);
                this.MaximumSize = new Size(0, 0);
                Stop_Record();
                MainFormResize(sender, e);
                btn_Record.Text = "Record";
            }
            else
            {
                // If we're recording, let's set the minimum and maximum size
                // limits so we don't get an error while trying to convert
                // with ffmpeg due to different image sizes
                this.MinimumSize = new Size(this.Width, this.Height);
                this.MaximumSize = new Size(this.Width, this.Height);
                Start_Record(Convert.ToInt32(num_FPS.Value));
                btn_Record.Text = "Stop";
            }
        }

        void Btn_settingsClick(object sender, EventArgs e)
        {
            this.TopMost = false;
            Settings.ShowDialog();
            this.TopMost = chk_Top_Most.Checked;
        }

        void Timer_elapsedTick(object sender, EventArgs e)
		{
			FPS = Frame_Count / Time_Elapsed;
            this.Text = string.Format(
                "{0}s, RECORDING | {3} fps ({1} frames / {2} saved) (Threads: {4})",
                Time_Elapsed, Frame_Count, Saved_Frame_Count,
                FPS, Thread_Pool.ActiveThreads
            );
            Time_Elapsed += 1;
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
