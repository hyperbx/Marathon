using System;
using System.IO;
using Toolkit.Text;
using System.Drawing;
using System.Diagnostics;
using Toolkit.EnvironmentX;
using System.Windows.Forms;

namespace Toolkit.Tools
{
    public partial class ArchiveMerger : Form
    {
        private Main mainForm = null;
        private string location = Paths.currentPath;

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
                if (text_ARC1.Text != string.Empty && text_ARC2.Text != string.Empty && text_Output.Text != string.Empty)
                    MergeARCs(text_ARC1.Text, text_ARC2.Text, text_Output.Text);
            } catch (Exception ex) { MessageBox.Show($"{SystemMessages.ex_MergeError}\n\n{ex}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private async void MergeARCs(string arc1, string arc2, string output) {
            string tempPath = $"{Program.applicationData}\\Temp\\{Path.GetRandomFileName()}"; // Defines the temporary path.
            var tempData = new DirectoryInfo(tempPath); // Gets directory information on the temporary path.

            Directory.CreateDirectory(tempPath);
            File.Copy(arc1, Path.Combine(tempPath, Path.GetFileName(arc1)), true); // Copies the input ARC to the temporary path.

            mainForm.Status = StatusMessages.cmn_Unpacking(arc1, true);
            await ProcessAsyncHelper.ExecuteShellCommand(Paths.Unpack,
                  $"\"{Path.Combine(tempPath, Path.GetFileName(arc1))}\"",
                  Application.StartupPath,
                  100000);

            if (File.Exists(arc2))
                File.Copy(arc2, Path.Combine(tempPath, Path.GetFileName(arc1)), true); // Copies the input ARC to the temporary path.

            mainForm.Status = StatusMessages.cmn_Unpacking(arc2, true);
            await ProcessAsyncHelper.ExecuteShellCommand(Paths.Unpack,
                  $"\"{Path.Combine(tempPath, Path.GetFileName(arc1))}\"",
                  Application.StartupPath,
                  100000);

            File.Delete(Path.Combine(tempPath, Path.GetFileName(arc1))); // Deletes the temporary merge ARC.

            mainForm.Status = StatusMessages.cmn_Repacking(Path.Combine(tempPath, Path.GetFileName(arc1)), true);
            await ProcessAsyncHelper.ExecuteShellCommand(Paths.Repack,
                  $"\"{Path.Combine(tempPath, Path.GetFileNameWithoutExtension(arc1))}\"",
                  Application.StartupPath,
                  100000);

            if (File.Exists(Path.Combine(tempPath, Path.GetFileName(arc1))))
                File.Copy(Path.Combine(tempPath, Path.GetFileName(arc1)), text_Output.Text, true);

            // Erase temporary files on completion.
            try {
                if (Directory.Exists(tempPath))
                    foreach (FileInfo file in tempData.GetFiles())
                        file.Delete();
                    foreach (DirectoryInfo directory in tempData.GetDirectories())
                        directory.Delete(true);
            } catch { return; }
        }
    }
}
