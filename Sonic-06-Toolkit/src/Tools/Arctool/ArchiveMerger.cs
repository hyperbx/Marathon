using System;
using System.IO;
using Toolkit.Text;
using System.Drawing;
using System.Diagnostics;
using Toolkit.EnvironmentX;
using System.Windows.Forms;

// Sonic '06 Toolkit is licensed under the MIT License:
/*
 * MIT License

 * Copyright (c) 2020 Gabriel (HyperPolygon64)

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
    public partial class ArchiveMerger : Form
    {
        private Main mainForm = null;
        private int pathsSpecified = 0;

        public ArchiveMerger(Form callingForm) {
            mainForm = callingForm as Main;
            InitializeComponent();
        }

        private void ArchiveMerger_Resize(object sender, EventArgs e) {
            MaximumSize = new Size(int.MaxValue, 369);
        }

        private void Btn_BrowseARC1_Click(object sender, EventArgs e) {
            string getPath = Browsers.CommonBrowser(false, SystemMessages.tl_SelectArchive, Filters.Archives);

            if (getPath != string.Empty)
                text_ARC1.Text = getPath;
        }

        private void Btn_BrowseARC2_Click(object sender, EventArgs e) {
            string getPath = Browsers.CommonBrowser(false, SystemMessages.tl_SelectArchive, Filters.Archives);

            if (getPath != string.Empty)
                text_ARC2.Text = getPath;
        }

        private void Btn_BrowseOutput_Click(object sender, EventArgs e) {
            string getPath = Browsers.SaveFile(SystemMessages.tl_RepackAs, Filters.Archives);

            if (getPath != string.Empty)
                text_Output.Text = getPath;
        }

        private void Btn_Merge_Click(object sender, EventArgs e) {
            try {
                if (text_ARC1.Text != string.Empty && text_ARC2.Text != string.Empty && text_Output.Text != string.Empty) {
                    mainForm.Status = StatusMessages.cmn_Processing(text_Output.Text, false);
                    MergeARCs(text_ARC1.Text, text_ARC2.Text, text_Output.Text);
                    mainForm.Status = StatusMessages.cmn_Repacked(text_Output.Text, false);
                }
            } catch (Exception ex) { MessageBox.Show($"{SystemMessages.ex_MergeError}\n\n{ex}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public static async void MergeARCs(string arc1, string arc2, string output) {
            string tempPath = $"{Program.applicationData}\\Temp\\{Path.GetRandomFileName()}"; // Defines the temporary path.
            var tempData = new DirectoryInfo(tempPath); // Gets directory information on the temporary path.

            Directory.CreateDirectory(tempPath);
            File.Copy(arc1, Path.Combine(tempPath, Path.GetFileName(arc1)), true); // Copies the input ARC to the temporary path.

            await ProcessAsyncHelper.ExecuteShellCommand(Paths.Unpack,
                  $"\"{Path.Combine(tempPath, Path.GetFileName(arc1))}\"",
                  Application.StartupPath,
                  100000);

            if (File.Exists(arc2))
                File.Copy(arc2, Path.Combine(tempPath, Path.GetFileName(arc1)), true); // Copies the input ARC to the temporary path.

            await ProcessAsyncHelper.ExecuteShellCommand(Paths.Unpack,
                  $"\"{Path.Combine(tempPath, Path.GetFileName(arc1))}\"",
                  Application.StartupPath,
                  100000);

            File.Delete(Path.Combine(tempPath, Path.GetFileName(arc1))); // Deletes the temporary merge ARC.

            await ProcessAsyncHelper.ExecuteShellCommand(Paths.Repack,
                  $"\"{Path.Combine(tempPath, Path.GetFileNameWithoutExtension(arc1))}\"",
                  Application.StartupPath,
                  100000);

            if (File.Exists(Path.Combine(tempPath, Path.GetFileName(arc1))))
                File.Copy(Path.Combine(tempPath, Path.GetFileName(arc1)), output, true);

            // Erase temporary files on completion.
            try {
                if (Directory.Exists(tempPath))
                    foreach (FileInfo file in tempData.GetFiles())
                        file.Delete();
                    foreach (DirectoryInfo directory in tempData.GetDirectories())
                        directory.Delete(true);
            } catch { return; }
        }

        private void Text_ARC1_TextChanged(object sender, EventArgs e) { 
            if (text_ARC1.Text != string.Empty && File.Exists(text_ARC1.Text)) pathsSpecified++;
            else pathsSpecified--;

            if (pathsSpecified == 3) btn_Merge.Enabled = true;
            else btn_Merge.Enabled = false;
        }

        private void Text_ARC2_TextChanged(object sender, EventArgs e) {
            if (text_ARC2.Text != string.Empty && File.Exists(text_ARC2.Text)) pathsSpecified++;
            else pathsSpecified--;

            if (pathsSpecified == 3) btn_Merge.Enabled = true;
            else btn_Merge.Enabled = false;
        }

        private void Text_Output_TextChanged(object sender, EventArgs e) {
            if (text_Output.Text != string.Empty) pathsSpecified++;
            else pathsSpecified--;

            if (pathsSpecified == 3) btn_Merge.Enabled = true;
            else btn_Merge.Enabled = false;
        }
    }
}
