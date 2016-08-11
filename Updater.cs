using System;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WebMCam
{
    class Updater
    {
        private static string versionUrl = "https://raw.githubusercontent.com/thetarkus/WebMCam/master/VERSION";
        private static string downloadPageUrl = "https://github.com/thetarkus/WebMCam/releases";

        public static async Task CheckAsync(string oldVersionStr)
        {
            await Task.Run(() => Check(oldVersionStr));
        }

        public static void Check(string oldVersionStr)
        {
            try
            {
                var newVersionStr = new WebClient().DownloadString(new Uri(versionUrl));

                var newVersion = new Version(newVersionStr);
                var oldVersion = new Version(oldVersionStr);

                if(newVersion.CompareTo(oldVersion) < 0)
                {
                    var result = MessageBox.Show(
                        string.Format(
                            "Version {0} is available for download. You are running version {1}." +
                            Environment.NewLine + "Would you like to be sent to the download page?",
                            newVersion, oldVersion
                        ),

                        "New Version Available", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                    );

                    if(result == DialogResult.Yes)
                        Process.Start(downloadPageUrl);
                }
            }
            catch
            {
                /* Suppress */
            }
        }
    }
}
