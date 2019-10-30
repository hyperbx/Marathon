using System;
using System.IO;
using System.Text;
using System.Linq;
using Toolkit.Text;
using System.Windows.Forms;
using Toolkit.EnvironmentX;
using System.Collections.Generic;

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
    public partial class ExecutableModification : Form
    {
        private Main mainForm = null;
        private string location = Paths.currentPath;

        public ExecutableModification(Form callingForm) {
            mainForm = callingForm as Main;
            InitializeComponent();
            tm_ProcessCheck.Start();

            switch (Properties.Settings.Default.xex_Encryption) {
                case 0:
                    encryption_Encrypt.Checked = true;
                    encryption_Decrypt.Checked = false;
                    break;
                case 1:
                    encryption_Encrypt.Checked = false;
                    encryption_Decrypt.Checked = true;
                    break;
            }

            switch (Properties.Settings.Default.xex_Compression) {
                case 0:
                    compression_Compress.Checked = true;
                    compression_Decompress.Checked = false;
                    break;
                case 1:
                    compression_Compress.Checked = false;
                    compression_Decompress.Checked = true;
                    break;
            }

            switch (Properties.Settings.Default.xex_System) {
                case 0:
                    system_Retail.Checked = true;
                    system_Developer.Checked = false;
                    break;
                case 1:
                    system_Retail.Checked = false;
                    system_Developer.Checked = true;
                    break;
            }
        }

        private void ExecutableModification_Load(object sender, EventArgs e) {
            clb_XEXs.Items.Clear();

            if (Directory.GetFiles(location, "*.xex").Length > 0) {
                foreach (string XEX in Directory.GetFiles(location, "*.xex", SearchOption.TopDirectoryOnly))
                    if (File.Exists(XEX) && Verification.VerifyMagicNumberCommon(XEX))
                        clb_XEXs.Items.Add(Path.GetFileName(XEX));
            } else {
                MessageBox.Show(SystemMessages.msg_NoEditableFiles, SystemMessages.tl_NoFilesAvailable(string.Empty), MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }

        private async void Btn_Process_Click(object sender, EventArgs e) {
            List<object> filesToProcess = clb_XEXs.CheckedItems.OfType<object>().ToList();

            StringBuilder getCommands = new StringBuilder();
            if (encryption_Encrypt.Checked) {
                if (getCommands.Length > 0) getCommands.Append(" ");
                getCommands.Append("-e e");
            }
            if (encryption_Decrypt.Checked) {
                if (getCommands.Length > 0) getCommands.Append(" ");
                getCommands.Append("-e u");
            }
            if (compression_Compress.Checked) {
                if (getCommands.Length > 0) getCommands.Append(" ");
                getCommands.Append("-c c"); 
            }
            if (compression_Decompress.Checked) {
                if (getCommands.Length > 0) getCommands.Append(" ");
                getCommands.Append("-c u"); 
            }
            if (system_Retail.Checked) {
                if (getCommands.Length > 0) getCommands.Append(" ");
                getCommands.Append("-m r"); 
            }
            if (system_Developer.Checked) {
                if (getCommands.Length > 0) getCommands.Append(" ");
                getCommands.Append("-m d"); 
            }

            foreach (string XEX in filesToProcess)
                if (Verification.VerifyMagicNumberCommon(Path.Combine(location, XEX))) {
                    mainForm.Status = StatusMessages.cmn_Processing(Path.Combine(location, XEX), false);
                    var process = await ProcessAsyncHelper.ExecuteShellCommand(Paths.XexTool,
                                        $"{getCommands.ToString()} \"{Path.Combine(location, XEX)}\"",
                                        location,
                                        100000);
                    if (process.Completed)
                        if (process.ExitCode != 0)
                            MessageBox.Show($"{SystemMessages.ex_XEXModificationError}\n\n{process.Output}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void Encryption_Encrypt_CheckedChanged(object sender, EventArgs e) {
            if (encryption_Encrypt.Checked) {
                encryption_Decrypt.Checked = false;
                Properties.Settings.Default.xex_Encryption = 0;
            } else { Properties.Settings.Default.xex_Encryption = -1; }
        }

        private void Encryption_Decrypt_CheckedChanged(object sender, EventArgs e) {
            if (encryption_Decrypt.Checked) {
                encryption_Encrypt.Checked = false;
                Properties.Settings.Default.xex_Encryption = -1;
            } else { Properties.Settings.Default.xex_Encryption = 0; }
        }

        private void Compression_Compress_CheckedChanged(object sender, EventArgs e) {
            if (compression_Compress.Checked) {
                compression_Decompress.Checked = false;
                Properties.Settings.Default.xex_Compression = 0;
            } else { Properties.Settings.Default.xex_Compression = -1; }
        }

        private void Compression_Decompress_CheckedChanged(object sender, EventArgs e) {
            if (compression_Decompress.Checked) {
                compression_Compress.Checked = false;
                Properties.Settings.Default.xex_Compression = -1;
            }
            else { Properties.Settings.Default.xex_Compression = 0; }
        }

        private void System_Retail_CheckedChanged(object sender, EventArgs e) {
            if (system_Retail.Checked) {
                system_Developer.Checked = false;
                Properties.Settings.Default.xex_System = -1;
            }
            else { Properties.Settings.Default.xex_System = 0; }
        }

        private void System_Developer_CheckedChanged(object sender, EventArgs e) {
            if (system_Developer.Checked) {
                system_Retail.Checked = false;
                Properties.Settings.Default.xex_System = 0;
            }
            else { Properties.Settings.Default.xex_System = -1; }
        }

        private void Btn_SelectAll_Click(object sender, EventArgs e) {
            for (int i = 0; i < clb_XEXs.Items.Count; i++) clb_XEXs.SetItemChecked(i, true);
        }

        private void Btn_DeselectAll_Click(object sender, EventArgs e) {
            for (int i = 0; i < clb_XEXs.Items.Count; i++) clb_XEXs.SetItemChecked(i, false);
            btn_Process.Enabled = false;
        }

        private void Clb_XEXs_SelectedIndexChanged(object sender, EventArgs e) {  clb_XEXs.ClearSelected(); }

        private void ExecutableModification_FormClosing(object sender, FormClosingEventArgs e) { Properties.Settings.Default.Save(); }

        private void Tm_ProcessCheck_Tick(object sender, EventArgs e) {
            if (clb_XEXs.CheckedItems.Count > 0)
                if (encryption_Encrypt.Checked     ||
                    encryption_Decrypt.Checked     ||
                    compression_Compress.Checked   ||
                    compression_Decompress.Checked ||
                    system_Retail.Checked          ||
                    system_Developer.Checked)
                        btn_Process.Enabled = true;
            else
                btn_Process.Enabled = false;
        }
    }
}
