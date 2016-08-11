using System;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace WebMCam
{
    /*
     * Todo
     * - Write comments for entire source
     * - Implement better frame saving
     */

    public partial class FormMain : Form
    {
        private FormOptions formOptions;
        private Recorder recorder;
        private Attach attach;
        private Size previousSize;

        // Hotkeys
        Hotkeys toggleHotkey = new Hotkeys();
        Hotkeys pauseHotkey = new Hotkeys();

        /// <summary>
        /// Constructor
        /// </summary>
        public FormMain()
        {
            DoubleBuffered = true;
            InitializeComponent();

            try
            {
                // Register Record/Stop Hotkey
                toggleHotkey.KeyPressed += new EventHandler<KeyPressedEventArgs>(buttonToggle_Click);
                toggleHotkey.RegisterHotKey(WebMCam.ModifierKeys.Control, Keys.F12);

                // Register Pause/Resume Hotkey
                pauseHotkey.KeyPressed += new EventHandler<KeyPressedEventArgs>(buttonPause_Click);
                pauseHotkey.RegisterHotKey(WebMCam.ModifierKeys.Control, Keys.F11);
            }
            catch
            {
                Console.WriteLine("Hotkeys not registered!");
            }
        }

        /// <summary>
        /// On MainForm Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void FormMain_Load(object sender, EventArgs e)
        {
            FormMain_Resize(sender, e);
            formOptions = new FormOptions();
            await Updater.CheckAsync(linkGithub.Text);

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

            // Set FPS from previous
            numericUpDownFramerate.Value = Properties.Settings.Default.Framerate;
        }

        /// <summary>
        /// When the form moves, update the recorder region (if its not null)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Move(object sender, EventArgs e)
        {
            if (recorder == null)
                return;

            recorder.region = new Rectangle(
                displayBox.PointToScreen(Point.Empty),
                displayBox.Size);
        }

        /// <summary>
        /// When form resizes update Title
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Resize(object sender, EventArgs e)
        {
            FormMain_Move(null, e);
            Text = string.Format("WebMCam [{0}x{1}]",
                displayBox.Size.Width, displayBox.Size.Height);
        }

        /// <summary>
        /// Open the formShowFrames dialog
        /// </summary>
        /// <param name="allowDeletion">Results from formShowFrames dialog</param>
        /// <returns></returns>
        private Tuple<bool, int> showFrames(bool allowDeletion)
        {
            var formShowFrames = new FormShowFrames(recorder.tempPath, formOptions.getImageFormat(), allowDeletion);
            formShowFrames.ShowDialog();
            return new Tuple<bool, int>(formShowFrames.Result, formShowFrames.framesCount);
        }

        /// <summary>
        /// Open the FFmpeg wrapper to process the frames
        /// </summary>
        /// <param name="framesCount"></param>
        private void processFrames(int framesCount)
        {
            var format = formOptions.getImageFormat().ToString().ToLower();
            var formProcess = new FormProcess(recorder.tempPath, framesCount);

            formProcess.ffmpegPath = formOptions.getFFmpegPath();
            formProcess.ffmpegArguments = formOptions.getFFmpegArguments();
            formProcess.formatArguments(recorder.duration, "%d." + format,
                recorder.fps, recorder.averageFps);

            formProcess.ShowDialog();
        }

        /// <summary>
        /// Toggle Recording
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonToggle_Click(object sender, EventArgs e)
        {
            // Start Recording
            if(buttonToggle.Text == "Record")
            { 
                // Set Text
                buttonToggle.Text = "Stop";
                buttonPause.Visible = true;

                // Update Settings
                Properties.Settings.Default.Framerate = numericUpDownFramerate.Value;
                Properties.Settings.Default.Save();

                // Create recorder and set options
                recorder = new Recorder(formOptions.getImageFormat());
                recorder.fps = (float)numericUpDownFramerate.Value;
                recorder.drawCursor = checkBoxDrawCursor.Checked;

                // Trigger FormMain_Resize to get region of displayBox
                FormMain_Resize(sender, e);

                // Start
                recorder.Start(checkBoxCaptureAudio.Checked);
                timerRecord.Start();

                // Set Alt Window Tracking if the option is set
                if(Properties.Settings.Default.AltWindowTracking)
                    timerTracker.Start();

                FormBorderStyle = FormBorderStyle.FixedDialog;
                return;
            }

            // Stop Recording
            {
                // Stop
                recorder.Stop();
                timerRecord.Stop();
                timerTracker.Stop();

                // Set Text
                buttonToggle.Text = "Record";
                buttonPause.Visible = false;
                FormMain_Resize(sender, e);
                TopMost = false;

                // Edit
                checkBoxFollow.Checked = false;
                var showFramesResult = showFrames(!checkBoxCaptureAudio.Checked);

                // Process if showFrames was successful
                if(showFramesResult.Item1)
                    processFrames(showFramesResult.Item2);

                // Delete leftovers
                recorder.Flush();
                recorder = null;

                FormBorderStyle = FormBorderStyle.Sizable;
                TopMost = checkBoxTopMost.Checked;
            }

        }

        /// <summary>
        /// Initiate Attach class, and start timerAttach to attach to next clicked window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxAttach_CheckedChanged(object sender, EventArgs e)
        {
            attach = new Attach();
            
            if(checkBoxAttach.Checked)
                previousSize = Size;
            else
                Size = previousSize;

            timerAttach.Enabled = checkBoxAttach.Checked;
        }

        /// <summary>
        /// Pause/Resume active recording
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPause_Click(object sender, EventArgs e)
        {
            if (recorder != null)
                recorder.Pause();

            buttonPause.Text = buttonPause.Text == "Pause" ? "Resume" : "Pause";
        }

        /// <summary>
        /// Open options dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOptions_Click(object sender, EventArgs e)
        {
            TopMost = false;
            formOptions.ShowDialog();
            TopMost = true;
        }

        /// <summary>
        /// Form will always be top-most when checkbox is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxTopMost_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = ((CheckBox)sender).Checked;
        }

        /// <summary>
        /// Form will follow cursor when checkbox is checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxFollow_CheckedChanged(object sender, EventArgs e)
        {
            timerFollow.Enabled = checkBoxFollow.Checked;
        }

        /// <summary>
        /// Timer to update Title text while recording
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerRecord_Tick(object sender, EventArgs e)
        {
            if (recorder == null)
                return;

            Text = string.Format("WebMCam [{0}x{1}] [{2}f / {3:F1}s = {4:F2} FPS]",
                displayBox.Size.Width, displayBox.Size.Height,
                recorder.frames, recorder.duration, recorder.averageFps);

            if (recorder.isPaused)
                Text += " [PAUSED]";
        }

        /// <summary>
        /// Timer to make form follow cursor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerFollow_Tick(object sender, EventArgs e)
        {
            var realLocation = PointToScreen(Point.Empty);
            var cursor = displayBox.PointToClient(Cursor.Position);

            // Cursor is out of bounds on X-axis
            if (cursor.X < 1 || cursor.X > ClientSize.Width)
            {
                // Cursor hit the right
                if (Cursor.Position.X > realLocation.X + ClientSize.Width)
                    Location = new Point(Cursor.Position.X - ClientSize.Width, Location.Y);

                // Cursor hit the left
                else
                    Location = new Point(Cursor.Position.X - 10, Location.Y);
            }

            // Cursor is out of bounds on Y-axis
            if (cursor.Y < 1 || cursor.Y > ClientSize.Height)
            {
                // Cursor hit the bottom
                if (Cursor.Position.Y > realLocation.Y + ClientSize.Height)
                    Location = new Point(Location.X, Cursor.Position.Y - ClientSize.Height - 20);

                // Cursor hit the top
                else
                    Location = new Point(Location.X, Cursor.Position.Y - (realLocation.Y - Location.Y) - 3);
            }
        }

        /// <summary>
        /// Timer to make recording window attach/follow another window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerAttach_Tick(object sender, EventArgs e)
        {
            if (!attach.isSet)
            {
                Text = "Click any Window";
                attach.Wait();
                return;
            }

            var borderWidth = SystemInformation.BorderSize.Width;
            var titleHeight = SystemInformation.CaptionHeight;
            var location = attach.Size();

            Width = location.Width - location.X + 115;
            Height = location.Height - location.Y + titleHeight;
            Location = new Point(
                location.X - borderWidth,
                location.Y - titleHeight);
        }

        /// <summary>
        /// Show information of hotkeys
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkHotkeys_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Press Ctrl+F12 to start or stop recording." + Environment.NewLine +
                "Press Ctrl+F11 to pause or unpause a recording.", "Hotkey Information",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Open FFmpeg URL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelFFmpeg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.ffmpeg.org/");
        }

        /// <summary>
        /// Open repository URL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/thetarkus/WebMCam");
        }
    }
}
