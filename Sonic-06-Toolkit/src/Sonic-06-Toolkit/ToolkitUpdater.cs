using System;
using System.IO;
using System.Net;
using Toolkit.Text;
using Toolkit.EnvironmentX;
using System.Windows.Forms;
using System.IO.Compression;

// Sonic '06 Toolkit is licensed under the MIT License:
/*
 * MIT License

 * Copyright (c) 2019 Gabriel (HyperPolygon64)

 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:

 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

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
            try {
                using (clientApplication = new WebClient()) {
                    clientApplication.DownloadProgressChanged += (s, e) => { pgb_Progress.Value = e.ProgressPercentage; };
                    clientApplication.DownloadFileAsync(new Uri(urlString), $"{Application.ExecutablePath}.pak");
                    clientApplication.DownloadFileCompleted += (s, e) => {
                        using (ZipArchive archive = new ZipArchive(new MemoryStream(File.ReadAllBytes($"{Application.ExecutablePath}.pak")))) {
                            Updater.ExtractToDirectory(archive, Application.StartupPath, true);
                            File.Replace($"{Application.ExecutablePath}.new", Application.ExecutablePath, $"{Application.ExecutablePath}.bak");
                            MessageBox.Show(SystemMessages.msg_UpdateComplete, SystemMessages.tl_Success, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Program.Restart();
                        }
                        File.Delete($"{Application.ExecutablePath}.pak");
                    };
                }
            } catch (Exception ex) {
                MessageBox.Show($"{SystemMessages.ex_UpdateError}\n\n{ex}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }
    }
}
