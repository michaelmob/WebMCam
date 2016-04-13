using System;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace WebMCam
{
    public partial class FormOptions : Form
    {
        private const string defaultArgs = "-framerate {avg:fps} -i {format} {audio} -c:v libvpx -b:v 2M -r {avg:fps} {output}";

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

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "FFmpeg (*.exe)|*.exe";
            openFileDialog.FileName = "ffmpeg.exe";
            openFileDialog.DefaultExt = "exe";

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
            settings.Save();
            Close();
        }
    }
}
