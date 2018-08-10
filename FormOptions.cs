using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace WebMCam
{
    public partial class FormOptions : Form
    {
        private const string defaultArgs = "-framerate {fps:avg} -i {format} {audio} -c:v {codec} -auto-alt-ref 0 -b:v 2M -r {fps:avg} {output}";

        private Properties.Settings settings = Properties.Settings.Default;

        public FormOptions()
        {
            InitializeComponent();
        }

        private void FormOptions_Load(object sender, EventArgs e)
        {
            // Set Defaults if not set
            if (settings.FFmpegPath.Length < 1)
                settings.FFmpegPath = "ffmpeg.exe";

            if (settings.FFmpegArguments.Length < 1)
                settings.FFmpegArguments = defaultArgs;

            if (settings.ImageFormat.Length < 1)
                settings.ImageFormat = "PNG";

            textBoxFFmpegPath.Text = settings.FFmpegPath;
            textBoxFFmpegArguments.Text = settings.FFmpegArguments;
            comboBoxImageFormat.Text = settings.ImageFormat;
            checkBoxAltWindowTracking.Checked = settings.AltWindowTracking;
            checkBoxRememberSize.Checked = settings.RememberSize;
        }

        public string getFFmpegArguments()
        {
            return textBoxFFmpegArguments.Text;
        }

        public string getFFmpegPath()
        {
            return textBoxFFmpegPath.Text.Replace(Environment.NewLine, " ");
        }

        public ImageFormat getImageFormat()
        {
            switch(comboBoxImageFormat.SelectedText)
            {
                case "JPG":
                    return ImageFormat.Jpeg;

                case "BMP":
                    return ImageFormat.Bmp;

                case "GIF":
                    return ImageFormat.Gif;

                case "PNG":
                default:
                    return ImageFormat.Png;
            }
        }

        public Size getWindowSize()
        {
            if (!settings.RememberSize)
            {
                return new Size(0, 0);
            }
            
            return new Size(settings.SizeWidth, settings.SizeHeight);
        }

        public void saveWindowSize(Size size)
        {
            settings.SizeWidth = size.Width;
            settings.SizeHeight = size.Height;
            settings.Save();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "FFmpeg (*.exe)|*.exe",
                FileName = "ffmpeg.exe",
                DefaultExt = "exe"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
                textBoxFFmpegPath.Text = openFileDialog.FileName;
        }

        private void buttonResetArguments_Click(object sender, EventArgs e)
        {
            textBoxFFmpegArguments.Text = defaultArgs;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            settings.FFmpegPath = textBoxFFmpegPath.Text;
            settings.FFmpegArguments = textBoxFFmpegArguments.Text;
            settings.ImageFormat = comboBoxImageFormat.Text;
            settings.AltWindowTracking = checkBoxAltWindowTracking.Checked;
            settings.RememberSize = checkBoxRememberSize.Checked;
            settings.Save();
            Close();
        }
    }
}
