using System;
using System.IO;
using System.Net;
using Toolkit.Text;
using Toolkit.EnvironmentX;
using System.Windows.Forms;

namespace Toolkit.Tools
{
    public partial class ToolkitUpdater : Form
    {
        string versionString;
        string urlString;
        WebClient clientApplication = new WebClient();

        public ToolkitUpdater(string versionNumber, string url) {
            InitializeComponent();
            versionString = versionNumber;
            urlString = url;

            lbl_Update.Text = $"Updating Sonic '06 Toolkit to {versionString}...";
            Width = lbl_Update.Width + 40;

            UpdateVersion();
        }

        private void UpdateVersion() {
            using (clientApplication = new WebClient()) {
                clientApplication.DownloadProgressChanged += (s, e) => { pgb_Progress.Value = e.ProgressPercentage; };
                clientApplication.DownloadFileAsync(new Uri(urlString), Application.ExecutablePath + ".pak");
                clientApplication.DownloadFileCompleted += (s, e) => {
                    File.Replace(Application.ExecutablePath + ".pak", Application.ExecutablePath, Application.ExecutablePath + ".bak");
                    MessageBox.Show(SystemMessages.msg_UpdateComplete, SystemMessages.tl_Success, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Program.Restart();
                };
            }
        }
    }
}
