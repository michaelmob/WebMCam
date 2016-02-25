using System;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace WebMCam
{
    public partial class FormMain : Form
    {
        private FFMpegWrapper ffmpegWrapper;
        private bool recording = false;
        private Point startingLocation;
        private Size startingSize;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (!File.Exists("ffmpeg.exe"))
                MessageBox.Show("Missing ffmpeg.exe, recording will not work");
        }

        private string getSaveLocation()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Choose File Location";
            saveFileDialog.FileName = "output.mp4";
            saveFileDialog.Filter = "Video Formats (*.webm, *.mp4)|*.webm;*.mp4";
            saveFileDialog.DefaultExt = "mp4";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                return saveFileDialog.FileName;

            return null;
        }

        private void buttonToggle_Click(object sender, EventArgs e)
        {
            // Start Recording
            if(((Button)sender).Text == "Record")
            { 
                // Get Save Location
                var saveLocation = getSaveLocation();
                if (saveLocation == null)
                    return;

                // Set Messages
                ((Button)sender).Text = "Stop";
                Text = "WebMCam (Recording)";
                recording = true;

                // Set Points
                startingSize = Size;
                startingLocation = Location;

                // Create wrapper and start recording
                ffmpegWrapper = new FFMpegWrapper(saveLocation);
                ffmpegWrapper.fps = Convert.ToInt32(numericUpDownFramerate.Value);
                ffmpegWrapper.Start(
                    new Rectangle(
                        displayBox.PointToScreen(Point.Empty),
                        displayBox.Size));

                return;
            }

            // Stop Recording
            ffmpegWrapper.Stop();
            ((Button)sender).Text = "Record";
            Text = "WebMCam";
            recording = false;
        }

        private void buttonOptions_Click(object sender, EventArgs e)
        {

        }

        private void FormMain_Move(object sender, EventArgs e)
        {
            // Prevent Moving while recording
            if(recording)
                Location = startingLocation;
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            // Prevent Resizing while recording
            if (recording)
                Size = startingSize;
        }

        private void checkBoxTopMost_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = ((CheckBox)sender).Checked;
        }

        private void linkGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/thetarkus/WebMCam");
        }
    }
}
