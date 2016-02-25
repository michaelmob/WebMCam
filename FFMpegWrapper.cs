using System;
using System.Diagnostics;
using System.Drawing;

namespace WebMCam
{
    class FFMpegWrapper
    {
        public string ffmpegLocation = "ffmpeg.exe";
        public string args = "-f gdigrab -r {fps} {size} -i desktop -y \"{output}\"";

        public string title = "Mirror";
        public int fps = 30;

        private string saveLocation;
        private Process process;

        public FFMpegWrapper(string saveLocation)
        {
            this.saveLocation = saveLocation;
        }

        public void Start(Rectangle rect)
        {
            var fArgs = args
                .Replace("{title}", title)
                .Replace("{fps}", fps.ToString())
                .Replace("{output}", saveLocation)
                .Replace("{size}",
                    string.Format("-offset_x {0} -offset_y {1} -video_size {2}x{3}",
                        rect.X, rect.Y, rect.Width, rect.Height));

            process = new Process();

            var info = new ProcessStartInfo();
            info.WindowStyle = ProcessWindowStyle.Minimized;
            info.FileName = ffmpegLocation;
            info.Arguments = fArgs;

            Console.WriteLine(info.FileName + " " + fArgs);

            //info.UseShellExecute = false;
            //info.RedirectStandardInput = true;
            //info.RedirectStandardOutput = true;
            //info.RedirectStandardError = true;

            process.StartInfo = info;
            process.Start();
        }

        public void Stop()
        {
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.CloseMainWindow();
        }
    }
}
