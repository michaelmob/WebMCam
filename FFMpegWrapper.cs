using System;
using System.IO;
using System.Diagnostics;
using System.Drawing;

namespace WebMCam
{
    class FFMpegWrapper
    {
        public string ffmpegLocation = "ffmpeg.exe";
        public string args = "{cursor} -f gdigrab {fps} {size} -i desktop {video} -y {output}";
        
        public int fps = 30;
        public bool drawCursor = false;

        private string saveLocation;
        private Process process;

        public FFMpegWrapper(string saveLocation)
        {
            this.saveLocation = saveLocation;
        }

        private string drawCursorStr()
        {
            return "-draw_mouse " + (drawCursor ? "1" : "0");
        }

        private string sizeStr(Rectangle rect)
        {
            return string.Format("-offset_x {0} -offset_y {1} -video_size {2}x{3}",
                rect.X, rect.Y, rect.Width, rect.Height);
        }

        private string fpsStr()
        {
            return "-r " + fps.ToString();
        }

        private string videoCodecStr()
        {
            if(Path.GetExtension(saveLocation).ToLower() == ".webm")
                return "-c:v libvpx";

            return "";
        }

        private string audioCodecStr()
        {
            //if (Path.GetExtension(saveLocation).ToLower() == ".webm")
            //    return "-f dshow -i audio=\"Microphone (Samson Meteor Mic)\"";

            return "";
        }

        public void Start(Rectangle rect)
        {
            var fArgs = args
                .Replace("{fps}", fpsStr())
                .Replace("{output}", string.Format("\"{0}\"", saveLocation))
                .Replace("{cursor}", drawCursorStr())
                .Replace("{size}", sizeStr(rect))
                .Replace("{video}", videoCodecStr())
                .Replace("{audio}", audioCodecStr());

            process = new Process();

            var info = new ProcessStartInfo();
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.CreateNoWindow = true;

            info.FileName = ffmpegLocation;
            info.Arguments = fArgs;

            Console.WriteLine(info.FileName + " " + fArgs);

            info.UseShellExecute = false;
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;

            process.StartInfo = info;
            process.Start();
            process.BeginOutputReadLine();
        }

        public void Stop()
        {
            try {
                process.CloseMainWindow();
            } catch (InvalidOperationException) {
                // Supress
            }
        }
    }
}
