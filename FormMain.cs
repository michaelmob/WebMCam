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
        private Recorder recorder;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
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
                // Set Text
                ((Button)sender).Text = "Stop";
                Text = "WebMCam (Recording)";

                recorder = new Recorder();
                FormMain_Move(sender, e);
                recorder.Start();

                return;
            }

            // Stop Recording
            {
                ((Button)sender).Text = "Record";
                Text = "WebMCam";
                recorder.Stop();
            }
        }

        private void buttonOptions_Click(object sender, EventArgs e)
        {
            TopMost = false;
            formOptions.ShowDialog();
            TopMost = true;
        }

        private void FormMain_Move(object sender, EventArgs e)
        {
            if (recorder == null)
                return;

            recorder.region = new Rectangle(
                displayBox.PointToScreen(Point.Empty),
                displayBox.Size);
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
