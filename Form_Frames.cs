using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

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
			String[] files = { };
			String[] file_types = {"*.png", "*.jpg", "*.bmp"};
			
			// Find files
			for(int i = 0; i < file_types.Length; i++) {
				files = Directory.GetFiles(directory_path, file_types[i]);
				
				if(files.Length > 0)
					break;
			}
			
			if(files.Length < 1) {
				MessageBox.Show(
					"No images were able to be saved, make sure you have set your temp folder."
					+ Environment.NewLine + Environment.NewLine
					+ "Your save path: " + directory_path
					+ Environment.NewLine +
					"Is the above path a valid directory with write permissions?",
					
					"Uh-oh!",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				Close();
			}
			
			// Easiest way to sort files numerically
			file_format = files[1].Split('.')[1];
			
			var frame_list = new List<int> {};

            File.WriteAllLines(directory_path + "files.txt", files);

            for (int i = 0; i < files.Length; i++)
            {
                /*
                frame_list.Add(
                    Convert.ToInt16(
                        files[i]
                            .Replace(directory_path, "")
                            .Replace("." + file_format, "")
                    )
                );
                */

                frame_list.Add(i);
            }
			
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
			} catch {
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
			// Before finishing we need to rename all files to be in numerical order
			for(var i = 0; i < list_frames.Items.Count; i++) {
				try {
					File.Move(directory_path + list_frames.Items[i] + "." + file_format, directory_path + "f_" + Convert.ToString(i) + "." + file_format);
				} catch (Exception ex) {
					// Instead of checking for each file, just skip over it
					Debug.WriteLine(ex.Message);
				}
			}
			
			Close();
		}
		
		void List_framesKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == (char)Keys.Delete)
				DeleteToolStripMenuItemClick(sender, e);
		}
	}
}
