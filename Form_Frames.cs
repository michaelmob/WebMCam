using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace WebMCam
{
	public partial class Form_Frames : Form
	{
		string directory_path;
		string file_format;
        List<FileInfo> files;
		
		public Form_Frames(string _directory_path, string _file_format)
		{
			InitializeComponent();
			directory_path = _directory_path;
            file_format = _file_format;
		}
		
		void Form_FramesLoad(object sender, EventArgs e)
		{
            // Load FileInfos into list, ordered by write time
            files = new DirectoryInfo(directory_path)
                .GetFiles("*." + file_format).OrderBy(f => f.LastWriteTime).ToList();

            // Check that files exist
            if (files.Count < 1)
            {
                MessageBox.Show(
                    "No files could be found in your temp folder. Check that the directory is writable.", "Uh-oh!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error
                );

                this.DialogResult = DialogResult.Cancel;
                this.Close();

                return;
            }

            // Add the file names without extensions to the listbox
            foreach(FileInfo file in files) {
                list_frames.Items.Add(file.Name.Split('.')[0]);
            }

            // Select first item
            list_frames.SetSelected(0, true);
		}
		
		void List_framesSelectedIndexChanged(object sender, EventArgs e)
		{
            // Load image into picturebox from memory so no file lock
            if (list_frames.SelectedIndex > -1)
                using (var temp_bitmap = new Bitmap(files[list_frames.SelectedIndex].FullName))
                    picture_frame.Image = new Bitmap(temp_bitmap);
		}
		
		void DeleteToolStripMenuItemClick(object sender, EventArgs e)
		{
            string path = files[list_frames.SelectedIndex].FullName;

            // Delete File
            if (File.Exists(path))
                File.Delete(path);

            // Remove from FileInfo list
            files.RemoveAt(list_frames.SelectedIndex);

            // Remove from List
            list_frames.Items.RemoveAt(list_frames.SelectedIndex);
		}

        void List_framesKeyDown(object sender, KeyEventArgs e)
        {
            // Delete Key
            if (e.KeyValue == (char)Keys.Delete)
                DeleteToolStripMenuItemClick(sender, e);
        }
		
		void Btn_doneClick(object sender, System.EventArgs e)
        {
            // Rename files so they're in numerical in order
            int name = 1;
            foreach (var file in files)
            {
                File.Move(
                    file.FullName,
                    Path.Combine(
                        directory_path,
                        Convert.ToString(name) + file.Extension
                    )
                );
                name++;
            }

            this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
