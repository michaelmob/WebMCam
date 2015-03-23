using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace WebMCam
{
	/// <summary>
	/// Description of Form_Settings.
	/// </summary>
	public partial class Form_Settings : Form
	{
		public Form_Settings()
		{
			InitializeComponent();
            load_settings();
		}

        public string loc_ffmpeg;
        public string loc_temp;
        public string cmd_args;
        public string fmt_pixel;
        public string fmt_image;
        public string fmt_delete;
        public string rec_threads;

        void load_settings()
        {
            // Set Variables
            loc_ffmpeg = Ini_File.Exists("Loc", "ffmpeg", Path.Combine(Environment.CurrentDirectory, "ffmpeg.exe"));
            loc_temp = Ini_File.Exists("Loc", "temp", Path.GetTempPath());
            cmd_args = Ini_File.Exists("Cmd", "args", "-i \"%d.%format%\" %audio% -r %fps% -b:v 1M -fs 3M");
            fmt_image = Ini_File.Exists("Fmt", "image", "png");
            fmt_pixel = Ini_File.Exists("Fmt", "pixel", "32bppRgb");
            fmt_delete =  Ini_File.Exists("Fmt", "delete", "True");
            rec_threads = Ini_File.Exists("Rec", "threads", Convert.ToString(Environment.ProcessorCount));

            // Set Components
            text_ffmpeg.Text = loc_ffmpeg;
            text_temp.Text = loc_temp;
            text_args.Text = cmd_args;
            combo_image_format.Text = fmt_image;
            combo_pixel_format.Text = fmt_pixel;
            chk_delete_frames.Checked = fmt_delete == "True";

            numeric_threads.Value = Convert.ToInt32(rec_threads);
            numeric_threads.Maximum = Environment.ProcessorCount * 10;
        }
		
		void Btn_cancelClick(object sender, System.EventArgs e)
		{
			Close();
		}
		
		void Btn_saveClick(object sender, System.EventArgs e)
		{
            loc_ffmpeg = Ini_File.Write("Loc", "ffmpeg", text_ffmpeg.Text);
			loc_temp = Ini_File.Write("Loc", "temp", text_temp.Text);
			cmd_args = Ini_File.Write("Cmd", "args", text_args.Text);
			fmt_image = Ini_File.Write("Fmt", "image", combo_image_format.Text);
			fmt_pixel = Ini_File.Write("Fmt", "pixel", combo_pixel_format.Text);
            fmt_delete = Ini_File.Write("Fmt", "delete", Convert.ToString(chk_delete_frames.Checked));
            rec_threads = Ini_File.Write("Rec", "threads", Convert.ToString(numeric_threads.Value));
			Close();
		}
		
		void Btn_open_tempClick(object sender, System.EventArgs e)
		{
			if (!Directory.Exists(text_temp.Text))
				Directory.CreateDirectory(text_temp.Text);
			Process.Start(text_temp.Text);
		}

		void Btn_resetClick(object sender, System.EventArgs e)
		{
			File.Delete(Ini_File.path);
			Process.Start(Application.ExecutablePath);
		    Environment.Exit(-1);
		}

        private void Form_Settings_Load(object sender, EventArgs e)
        {

        }
	}
}
