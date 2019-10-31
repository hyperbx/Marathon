using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.IO.Compression;

namespace Sonic_06_Toolkit
{
    public partial class Updater : Form
    {
        string versionString;
        string urlString;
        bool enabledBool;
        WebClient clientApplication = new WebClient();

        public Updater(string versionNumber, string url, bool enabled)
        {
            InitializeComponent();

            versionString = versionNumber;
            urlString = url;
            enabledBool = enabled;
        }

        private void Updater_Load(object sender, System.EventArgs e)
        {
            label1.Text = $"Updating Sonic '06 Toolkit to {versionString}...";
            Width = label1.Width + 40;

            if (enabledBool)
            {
                UpdateVersion();
            }
            else
            {
                Text = "Sonic '06 Toolkit - Debug Mode";
            }
        }

        private void UpdateVersion()
        {
            using (clientApplication = new WebClient())
            {
                clientApplication.DownloadProgressChanged += (s, e) => { pgb_Progress.Value = e.ProgressPercentage; };
                clientApplication.DownloadFileAsync(new Uri(urlString), Application.ExecutablePath + ".pak");
                clientApplication.DownloadFileCompleted += (s, e) => {
                    using (ZipArchive archive = new ZipArchive(new MemoryStream(File.ReadAllBytes($"{Application.ExecutablePath}.pak")))) {
                        Tools.ZipArchiveExtensions.ExtractToDirectory(archive, Application.StartupPath, true);
                        File.Replace($"{Application.ExecutablePath}.new", Application.ExecutablePath, $"{Application.ExecutablePath}.bak");
                        MessageBox.Show("Update complete! Please restart Sonic '06 Toolkit.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Exit();
                    }
                    File.Delete($"{Application.ExecutablePath}.pak");
                };
            }
        }

        public int UpdateProgressValue
        {
            get { return pgb_Progress.Value; }
            set { pgb_Progress.Value = value; }
        }
    }
}
