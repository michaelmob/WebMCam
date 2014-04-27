using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WebMCam
{
	public partial class Form_Frames : Form
	{
		string directory_path;
		string file_format;
		
		public Form_Frames(String _directory_path)
		{
			InitializeComponent();
			directory_path = _directory_path;
		}
		
		void Form_FramesLoad(object sender, EventArgs e)
		{
			directory_path = directory_path + "\\";
			var files = Directory.GetFiles(directory_path, "*.png");
			
			// Easiest way to sort files numerically
			file_format = files[1].Split('.')[1];
			
			var frame_list = new List<Int16> {};
			
			for(int i = 0; i < files.Length; i++)
				frame_list.Add(Convert.ToInt16(files[i].Replace(directory_path, "").Replace("." + file_format, "")));
			
			frame_list.Sort();
			
			foreach(var frame in frame_list)
				list_frames.Items.Add(Convert.ToString(frame));
			
			list_frames.SetSelected(0, true);
		}
		
		void List_framesSelectedIndexChanged(object sender, EventArgs e)
		{
			try {
				var bmp = new FileStream(directory_path + list_frames.SelectedItem + "." + file_format, FileMode.Open, FileAccess.Read);
				picture_frame.Image = Image.FromStream(bmp);
				bmp.Close();
			} catch (Exception ex) {
				list_frames.Items.RemoveAt(list_frames.SelectedIndex);
			}
		}
		
		void DeleteToolStripMenuItemClick(object sender, EventArgs e)
		{
			try { // If can't do it just forget about it, not a big deal
				
				for(var i = list_frames.SelectedIndices.Count - 1; i >= 0; i--)
				{
					var index = list_frames.SelectedIndices[i];
					File.Delete(directory_path + list_frames.Items[index] + "." + file_format);
					list_frames.Items.RemoveAt(index);
				}
			} catch { }
		}
		
		void Btn_doneClick(object sender, System.EventArgs e)
		{
			Close();
		}
		
		void List_framesKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == (char)Keys.Delete)
				DeleteToolStripMenuItemClick(sender, e);
		}
	}
}
