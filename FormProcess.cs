using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace WebMCam
{
    public partial class FormProcess : Form
    {
        public string ffmpegPath = "ffmpeg.exe";
        public string ffmpegArguments = null;
        public string outputLocation = null;

        private Process process = new Process();
        private string framesPath;
        private int framesCount;
        private string sText;

        public FormProcess(string framesPath, int framesCount = -1)
        {
            this.framesPath = framesPath;
            this.framesCount = framesCount;

            InitializeComponent();
        }

        /// <summary>
        /// Replace arguments with data from recorder
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="frameFormat"></param>
        /// <param name="fps"></param>
        /// <param name="avg"></param>
        public void formatArguments(float duration, string frameFormat, float fps, float avg)
        {
            var audio = File.Exists(Path.Combine(framesPath, "audio.wav")) ? "-i audio.wav" : "";

            ffmpegArguments = ffmpegArguments
                .Replace("{duration}", duration.ToString())
                .Replace("{format}", frameFormat)
                .Replace("{fps}", fps.ToString().Replace(',', '.'))
                .Replace("{avg:fps}", avg.ToString().Replace(',', '.'))
                .Replace("{fps:avg}", avg.ToString().Replace(',', '.'))
                .Replace("{audio}", audio);
        }

        /// <summary>
        /// Create SaveFileDialog to find where the user wants to put their file
        /// </summary>
        /// <returns>File Location</returns>
        private string getSaveLocation()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Choose File Location";
            saveFileDialog.FileName = "output.webm";
            saveFileDialog.Filter = "Video Formats (*.webm, *.mp4)|*.webm;*.mp4";
            saveFileDialog.DefaultExt = "webm";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                return saveFileDialog.FileName;

            return null;
        }

        private void FormProcess_Load(object sender, EventArgs e)
        {
            BringToFront();
            sText = Text;

            if (outputLocation == null)
                outputLocation = getSaveLocation();

            if (outputLocation == null || outputLocation.Length < 5)
            {
                Close();
                return;
            }

            var info = new ProcessStartInfo();
            info.CreateNoWindow = true;
            info.UseShellExecute = false;
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;

            info.WorkingDirectory = framesPath;
            info.FileName = ffmpegPath;
            info.Arguments = ffmpegArguments.Replace("{output}",
                string.Format("-y \"{0}\"", outputLocation));

            process.StartInfo = info;
            process.OutputDataReceived += process_DataReceived;
            process.ErrorDataReceived += process_DataReceived;

            textBoxData.AppendText("Directory: " + framesPath);
            textBoxData.AppendText(Environment.NewLine);
            textBoxData.AppendText("Command: " + info.FileName + " " + info.Arguments);
            textBoxData.AppendText(Environment.NewLine);
            textBoxData.AppendText("Output: " + outputLocation);
            textBoxData.AppendText(Environment.NewLine);
            textBoxData.AppendText("-");
            textBoxData.AppendText(Environment.NewLine);

            textBoxData.TextChanged += textBoxData_TextChanged;

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            BringToFront();
        }

        /// <summary>
        /// Write output from FFmpeg to textBoxData
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void process_DataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null)
                return;
            
            textBoxData.Invoke(new MethodInvoker(delegate() {
                textBoxData.AppendText(e.Data + Environment.NewLine);
            }));
        }

        /// <summary>
        /// When the textbox text changes: if it starts with 'frame=' we need to update
        /// the progress bar. If it starts with 'video=' then the processing is done. But
        /// update the percentage either way.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxData_TextChanged(object sender, EventArgs e)
        {
            string line = textBoxData.Lines[textBoxData.Lines.Length - 2];
            Console.WriteLine(line);
            
            if (line.StartsWith("frame=") && framesCount > -1)
            {
                var framesCurrent = Convert.ToInt32(line.Replace(" ", "").Substring(6).Split('f')[0]);
                var value = Convert.ToInt32(((float)framesCurrent / framesCount) * 100);

                if (value < 101)
                    progressBar.Value = value;
            }
            else if (line.StartsWith("video:"))
            {
                progressBar.Value = 100;
                buttonOpen.Enabled = true;
                buttonCancel.Text = "Done";
            }

            Text = string.Format("{0} [{1}%]", sText, progressBar.Value);
        }

        /// <summary>
        /// Cancel processing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (buttonCancel.Text == "Cancel")
                try
                {
                    process.Kill();
                }
                catch { }
            Close();
        }

        /// <summary>
        /// Open output file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            Process.Start(outputLocation);
        }
    }
}
