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
		}
		
		void Form_SettingsLoad(object sender, System.EventArgs e)
		{
			text_ffmpeg.Text = Ini_File.Read("Loc", "ffmpeg");
			text_temp.Text = Ini_File.Read("Loc", "temp");
			text_args.Text = Ini_File.Read("Cmd", "args");
			combo_image_format.Text = Ini_File.Read("Fmt", "image");
			combo_pixel_format.Text = Ini_File.Read("Fmt", "pixel");
            chk_delete_frames.Checked = Ini_File.Read("Fmt", "delete") == "True";
		}
		
		void Btn_cancelClick(object sender, System.EventArgs e)
		{
			Close();
		}
		
		void Btn_saveClick(object sender, System.EventArgs e)
		{
			Ini_File.Write("Loc", "ffmpeg", text_ffmpeg.Text);
			Ini_File.Write("Loc", "temp", text_temp.Text);
			Ini_File.Write("Cmd", "args", text_args.Text);
			Ini_File.Write("Fmt", "image", combo_image_format.Text);
			Ini_File.Write("Fmt", "pixel", combo_pixel_format.Text);
            Ini_File.Write("Fmt", "delete", Convert.ToString(chk_delete_frames.Checked));
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
	}
}
