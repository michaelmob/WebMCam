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
        static List<Bitmap> frames = new List<Bitmap>();
        static Int16 frame_count, saved_frame_count;
        Int32 time_elapsed;
        String temp_storage, image_format;
        Rectangle record_rect;
        Boolean recording, saving;
        static SmartThreadPool threadPool = new SmartThreadPool();
        const double version = 1.34;

        public Form_Main()
        {
            InitializeComponent();
        }

        void MainFormLoad(object sender, EventArgs e)
        {
            // Restore window size from previous session
            var size = Ini_File.Exists("Frm", "size", "0,0");

            // Catch if size is incorrect and fix it if it is
            try
            {
                if (size != "0,0")
                {
                    var size_split = size.Split(',');
                    Size = new Size(Convert.ToInt16(size_split[0]), Convert.ToInt16(size_split[1]));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Ini_File.Write("Frm", "size", "0,0");
            }

            // Hit the method so the title sets the size
            MainFormResize(sender, e);

            Ini_File.Exists("Loc", "ffmpeg", String.Format("\"{0}\\ffmpeg.exe\"", Environment.CurrentDirectory));
            Ini_File.Exists("Loc", "temp", Environment.CurrentDirectory + "\\temp\\");
            Ini_File.Exists("Cmd", "args", "-r %rfps% -i \"f_%d.%format%\" -r %fps% -vb %bitrate%");
            Ini_File.Exists("Fmt", "pixel", "32bppRgb");
            Ini_File.Exists("Fmt", "image", "png");
            Ini_File.Exists("Fmt", "delete", "True");
            Ini_File.Exists("Rec", "threads", Convert.ToString(Environment.ProcessorCount));
        }

        void MainFormResize(object sender, EventArgs e)
        {
            Text = String.Format("WebMCam {0} | {1}x{2}", version, panel_record.Size.Width, panel_record.Size.Height);
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

        ImageFormat image_format_format()
        {
            image_format = Ini_File.Exists("Fmt", "image", "png");

            switch (image_format)
            {
                case "jpg":
                    return ImageFormat.Jpeg;
                case "bmp":
                    return ImageFormat.Bmp;
                default:
                    return ImageFormat.Png;
            }
        }

        PixelFormat pixel_format_format()
        {

            switch (Ini_File.Exists("Fmt", "pixel", "32bppRgb"))
            {
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

        void bgw_captureDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Int16 fps = Convert.ToInt16(e.Argument);
            var format = pixel_format_format();

            while (timer_elapsed.Enabled)
            {
                threadPool.QueueWorkItem(new Amib.Threading.Func<PixelFormat, int>(capture_screen), format);
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return 0;
        }

        void Bgw_saveDoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var format = image_format_format();

            while (saving || timer_elapsed.Enabled)
            {
                if (frames.Count > 1)
                {
                    try
                    {
                        frames[0].Save(String.Format("{0}{1}.{2}", temp_storage, saved_frame_count, image_format), format);
                        frames.RemoveAt(0);
                        saved_frame_count++;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
                else if (!timer_elapsed.Enabled)
                    saving = false;
            }
        }

        void Bgw_saveProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progress_bar.Value = e.ProgressPercentage;
        }

        void start_record(int fps)
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

            temp_storage = String.Format(@"{0}{1}\", Ini_File.Read("Loc", "temp"), DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);

            // If our temp dir doesn't exist we need to create it
            if (!Directory.Exists(temp_storage))
                Directory.CreateDirectory(temp_storage);

            Int32 t = Convert.ToInt32(Ini_File.Read("Rec", "threads"));
            threadPool.Concurrency = t;

            frame_count = 0;
            saved_frame_count = 0;
            time_elapsed = 1;
            saving = true;

            timer_elapsed.Start();
            bgw_capture.RunWorkerAsync(fps);
            bgw_save.RunWorkerAsync();
        }

        void stop_record()
        {
            threadPool.Concurrency = 0;

            timer_elapsed.Stop();
            bgw_capture.CancelAsync();
            progress_bar.Value = 0;

            progress_bar.Visible = true;
            while (saving)
            {
                progress_bar.Value = Convert.ToInt16(((float)saved_frame_count / (float)frame_count) * 100);
                Thread.Sleep(1000);
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

                new Form_Output(
                    temp_storage,
                    Ini_File.Read("Loc", "ffmpeg"),
                    save.FileName,
                    Ini_File.Read("Cmd", "args")
                        .Replace("%temp%", "")
                        .Replace("%duration%", Convert.ToString(time_elapsed + 1))
                        .Replace("%bitrate%", Convert.ToString((3 * 8192) / time_elapsed) + "k")
                        .Replace("%format%", Ini_File.Read("Fmt", "image"))
                        .Replace("%rfps%", Convert.ToString(frame_count / time_elapsed))
                        .Replace("%fps%", Convert.ToString(numeric_fps.Value)) + " \"" + save.FileName + "\"",
                    frame_count
                ).ShowDialog();

                Visible = true;
            }

            if (Ini_File.Read("Fmt", "delete") == "True")
                Directory.Delete(temp_storage, true);

            progress_bar.Visible = false;
        }

        void Btn_recordClick(object sender, EventArgs e)
        {
            if (!File.Exists(Ini_File.Read("Loc", "ffmpeg")))
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
                start_record(Convert.ToInt16(numeric_fps.Value));
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
            Text = String.Format("{0}s, RECORDING | {3} fps ({1} frames / {2} saved) (Threads: {4})", time_elapsed, frame_count, saved_frame_count, frame_count / time_elapsed, threadPool.ActiveThreads);
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
