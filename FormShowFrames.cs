using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WebMCam
{
    public partial class FormShowFrames : Form
    {
        public bool Result = false;
        public int framesCount = 0;

        private string framesPath;
        private string imageExtension;
        private bool allowDeletion;

        public FormShowFrames(string framesPath, ImageFormat imageFormat, bool allowDeletion = true)
        {
            this.framesPath = framesPath;
            this.imageExtension = "." + imageFormat.ToString().ToLower();
            this.allowDeletion = allowDeletion;

            InitializeComponent();
        }

        private void formShowFrames_Load(object sender, EventArgs e)
        {
            // Get all frames from framesPath directory
            List<FileInfo> framesList = new DirectoryInfo(framesPath)
                .GetFiles("*" + imageExtension).OrderBy(f => f.LastWriteTime).ToList();

            // Return if no frames are found
            if (framesList.Count < 1)
            {
                Result = false;
                Close();
                return;
            }

            // Add frames to listbox
            foreach (FileInfo frame in framesList)
                listBoxFrames.Items.Add(frame.Name.Substring(1).Split('.')[0]);

            listBoxFrames.SetSelected(0, true);
        }

        private void listBoxFrames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxFrames.SelectedIndex < -1)
                return;

            var path = Path.Combine(framesPath, "_" + listBoxFrames.SelectedItem + imageExtension);

            // Be sure file exists before deleting it
            if (!File.Exists(path))
                return;

            // Set the preview box to the image
            using (var bmp = new Bitmap(path))
                pictureBoxFrame.Image = new Bitmap(bmp);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!allowDeletion)
                return;

            // Loop MUST be inverse otherwise it will delete the wrong frames
            for (var i = listBoxFrames.SelectedItems.Count - 1; i > -1; i--)
            {
                var frame = listBoxFrames.SelectedItems[i];
                var path = Path.Combine(framesPath, "_" + frame + imageExtension);
                listBoxFrames.Items.Remove(frame);

                // Be sure file exists before deleting it
                if (File.Exists(path))
                    File.Delete(path);
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            List<FileInfo> framesList = new DirectoryInfo(framesPath)
                .GetFiles("*" + imageExtension).OrderBy(f => f.LastWriteTime).ToList();

            int i = 0;
            foreach(var frame in framesList)
            {
                frame.MoveTo(Path.Combine(framesPath, i.ToString() + imageExtension));
                i++;
            }

            framesCount = framesList.Count;
            Result = true;
            Close();
        }

        private void listBoxFrames_KeyDown(object sender, KeyEventArgs e)
        {
            // Delete Key
            if (e.KeyValue == (char)Keys.Delete)
                deleteToolStripMenuItem_Click(sender, e);
        }
    }
}
