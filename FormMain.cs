using System;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace WebMCam
{
    /* TODO
        - Follow Cursor Mode
        - Hotkeys
        - Clarify some code, add comments
    */

    public partial class FormMain : Form
    {
        private FormOptions formOptions;
        private Recorder recorder;
        private string sText;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            sText = Text;
            formOptions = new FormOptions();

            // Just to load in the settings
            formOptions.Opacity = 0;
            formOptions.Show();
            formOptions.Hide();
            formOptions.Opacity = 100;

            // Check for FFmpeg otherwise warn the user
            if (!File.Exists(Properties.Settings.Default.FFmpegPath))
                MessageBox.Show("FFmpeg.exe does not exist, nothing will work properly. Please specify it's location in Options. " +
                    Environment.NewLine + Environment.NewLine + "You can download it by clicking the FFmpeg link on the main form.",
                    "FFmpeg Missing!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private Tuple<bool, int> showFrames(bool allowDeletion)
        {
            var formShowFrames = new FormShowFrames(recorder.tempPath, formOptions.getImageFormat(), allowDeletion);
            formShowFrames.ShowDialog();
            return new Tuple<bool, int>(formShowFrames.Result, formShowFrames.framesCount);
        }

        private void processFrames(int framesCount)
        {
            var format = formOptions.getImageFormat().ToString().ToLower();
            var formProcess = new FormProcess(recorder.tempPath, framesCount);

            formProcess.ffmpegArguments = formOptions.getFFmpegArguments();
            formProcess.formatArguments(recorder.duration, "%d." + format,
                (int)recorder.currentFps, (int)recorder.fps);

            formProcess.ShowDialog();
        }

        private void buttonToggle_Click(object sender, EventArgs e)
        {
            // Start Recording
            if(((Button)sender).Text == "Record")
            { 
                // Set Text
                ((Button)sender).Text = "Stop";

                // Create recorder and set options
                recorder = new Recorder(formOptions.getImageFormat());
                recorder.fps = (float)numericUpDownFramerate.Value;
                recorder.drawCursor = checkBoxDrawCursor.Checked;

                // Trigger FormMain_Move to get region of displayBox
                FormMain_Move(sender, e);

                // Start
                recorder.Start(checkBoxCaptureAudio.Checked);
                timerRecord.Start();

                return;
            }

            // Stop Recording
            {
                // Stop
                recorder.Stop();
                timerRecord.Stop();

                // Set Text
                ((Button)sender).Text = "Record";
                Text = sText;
                TopMost = false;

                // Edit
                var showFramesResult = showFrames(!checkBoxCaptureAudio.Checked);

                // Process if showFrames was successful
                if(showFramesResult.Item1)
                    processFrames(showFramesResult.Item2);

                // Delete leftovers
                recorder.Flush();

                TopMost = checkBoxTopMost.Checked;
            }

        }

        private void buttonOptions_Click(object sender, EventArgs e)
        {
            TopMost = false;
            formOptions.ShowDialog();
            TopMost = true;
        }

        private void timerRecord_Tick(object sender, EventArgs e)
        {
            if (recorder == null)
                return;

            Text = string.Format("WebMCam [{0}f / {1}s = {2} FPS]",
                recorder.frames, recorder.duration, recorder.currentFps);
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

        private void linkLabelFFmpeg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.ffmpeg.org/");
        }

        private void linkGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/thetarkus/WebMCam");
        }
    }
}
