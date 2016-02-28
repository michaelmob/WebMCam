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

        public void formatArguments(float duration, string frameFormat, float fps, float avg)
        {
            var audio = File.Exists(Path.Combine(framesPath, "audio.wav")) ? "-i audio.wav" : "";

            ffmpegArguments = ffmpegArguments
                .Replace("{duration}", duration.ToString())
                .Replace("{format}", frameFormat)
                .Replace("{fps}", fps.ToString())
                .Replace("{avg:fps}", avg.ToString())
                .Replace("{audio}", audio);
        }

        private void FormProcess_Load(object sender, EventArgs e)
        {
            BringToFront();
            sText = Text;

            if (outputLocation == null)
                outputLocation = getSaveLocation();

            if (outputLocation == null || outputLocation.Length < 5)
                return;

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

            Console.WriteLine(info.FileName + " " + info.Arguments);

            process.StartInfo = info;
            process.OutputDataReceived += process_DataReceived;
            process.ErrorDataReceived += process_DataReceived;

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            BringToFront();
        }

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

        private void process_DataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null)
                return;
            
            textBoxData.Invoke(new MethodInvoker(delegate() {
                textBoxData.AppendText(e.Data + Environment.NewLine); }));
        }

        private void textBoxData_TextChanged(object sender, EventArgs e)
        {
            string line = textBoxData.Lines[textBoxData.Lines.Length - 2];
            
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

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            Process.Start(outputLocation);
        }
    }
}
