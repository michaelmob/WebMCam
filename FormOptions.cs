using System;
using System.Windows.Forms;

namespace WebMCam
{
    public partial class FormOptions : Form
    {
        public FormOptions()
        {
            InitializeComponent();
        }

        private void FormOptions_Load(object sender, EventArgs e)
        {

        }

        public string getFFmpegArgs()
        {
            return textBoxFFmpegArgs.Text;
        }

        public string getFFmpegLocation()
        {
            return textBoxFFmpegLocation.Text.Replace(Environment.NewLine, " ");
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
