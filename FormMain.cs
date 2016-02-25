using System;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace WebMCam
{
    /* TODO
        - Read FFmpeg output and display details
        - Be sure FFmpeg has started and finished before doing anything else
        - (Maybe) Make a mirror to allow window moving
    */

    public partial class FormMain : Form
    {
        private FormOptions formOptions;
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

            formOptions = new FormOptions();
        }

        private string getSaveLocation()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Choose File Location";
            saveFileDialog.FileName = "output.webm";
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

                // Set Text
                ((Button)sender).Text = "Stop";
                Text = "WebMCam (Recording)";
                recording = true;

                // Set Points
                startingSize = Size;
                startingLocation = Location;

                // Create wrapper and start recording
                ffmpegWrapper = new FFMpegWrapper(saveLocation);
                ffmpegWrapper.ffmpegLocation = formOptions.getFFmpegLocation();
                //ffmpegWrapper.args = formOptions.getFFmpegArgs();
                ffmpegWrapper.fps = Convert.ToInt32(numericUpDownFramerate.Value);
                ffmpegWrapper.drawCursor = checkBoxDrawCursor.Checked;
                ffmpegWrapper.Start(
                    new Rectangle(
                        displayBox.PointToScreen(Point.Empty),
                        displayBox.Size));

                return;
            }

            // Stop Recording
            ffmpegWrapper.Stop();

            // Set Text
            ((Button)sender).Text = "Record";
            Text = "WebMCam";
            recording = false;
        }

        private void buttonOptions_Click(object sender, EventArgs e)
        {
            TopMost = false;
            formOptions.ShowDialog();
            TopMost = true;
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
