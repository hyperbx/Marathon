using System;
using System.IO;
using System.Linq;
using Toolkit.Text;
using Toolkit.EnvironmentX;
using System.Windows.Forms;
using System.Collections.Generic;

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
    public partial class TextEncoding : Form
    {
        private Main mainForm = null;
        private string location = Paths.currentPath;

        public TextEncoding(Form callingForm) {
            mainForm = callingForm as Main;
            InitializeComponent();
            Console.WriteLine(location);
        }

        private void TextEditor_Load(object sender, EventArgs e) {
            clb_MSTs.Items.Clear();
            btn_Process.Enabled = false;

            if (Directory.GetFiles(location, "*.mst").Length > 0) {
                combo_Mode.SelectedIndex = 0;
            } else if (Directory.GetFiles(location, "*.xml").Length > 0) {
                combo_Mode.SelectedIndex = 1;
            } else {
                MessageBox.Show(SystemMessages.msg_NoConvertableFiles, SystemMessages.tl_NoFilesAvailable(string.Empty), MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }

        private void Clb_MSTs_SelectedIndexChanged(object sender, EventArgs e) {
            clb_MSTs.ClearSelected();
            btn_Process.Enabled = clb_MSTs.CheckedItems.Count > 0;
        }

        private void Combo_Mode_SelectedIndexChanged(object sender, EventArgs e) {
            clb_MSTs.Items.Clear();
            btn_Process.Enabled = false;

            if (combo_Mode.SelectedIndex == 0) {
                btn_Process.Text = "Export";
                foreach (string MST in Directory.GetFiles(location, "*.mst", SearchOption.TopDirectoryOnly))
                    if (File.Exists(MST) && Verification.VerifyMagicNumberExtended(MST))
                        clb_MSTs.Items.Add(Path.GetFileName(MST));

                if (Directory.GetFiles(location, "*.mst").Length == 0) {
                    MessageBox.Show(SystemMessages.msg_NoConvertableFiles, SystemMessages.tl_NoFilesAvailable("XML"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Directory.GetFiles(location, "*.xml").Length == 0) Close();
                    else combo_Mode.SelectedIndex = 1;
                }
            } else if (combo_Mode.SelectedIndex == 1) {
                btn_Process.Text = "Import";
                foreach (string XML in Directory.GetFiles(location, "*.xml", SearchOption.TopDirectoryOnly))
                    if (File.Exists(XML) && Verification.VerifyXML(XML, "MST"))
                        clb_MSTs.Items.Add(Path.GetFileName(XML));

                if (Directory.GetFiles(location, "*.xml").Length == 0) {
                    MessageBox.Show(SystemMessages.msg_NoConvertableFiles, SystemMessages.tl_NoFilesAvailable("MST"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Directory.GetFiles(location, "*.mst").Length == 0) Close();
                    else combo_Mode.SelectedIndex = 0;
                }
            }
        }

        private async void Btn_Process_Click(object sender, EventArgs e) {
            List<object> filesToProcess = clb_MSTs.CheckedItems.OfType<object>().ToList();
            if (combo_Mode.SelectedIndex == 0) {
                foreach (string MST in filesToProcess) {
                    if (File.Exists(Path.Combine(location, MST)) && Verification.VerifyMagicNumberExtended(Path.Combine(location, MST))) {
                        mainForm.Status = StatusMessages.cmn_Exporting(MST, false);
                        var export = await ProcessAsyncHelper.ExecuteShellCommand(Paths.MSTTool,
                                           $"\"{Path.Combine(location, MST)}\"",
                                           Application.StartupPath,
                                           100000);
                        if (export.Completed)
                            if (export.ExitCode != 0)
                                MessageBox.Show($"{SystemMessages.ex_MSTExportError}\n\n{export.Output}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            } else if (combo_Mode.SelectedIndex == 1) {
                foreach (string XML in filesToProcess) {
                    if (File.Exists(Path.Combine(location, XML)) && Verification.VerifyXML(Path.Combine(location, XML), "MST")) {
                        mainForm.Status = StatusMessages.cmn_Importing(XML, false);
                        var export = await ProcessAsyncHelper.ExecuteShellCommand(Paths.MSTTool,
                                           $"\"{Path.Combine(location, XML)}\"",
                                           Application.StartupPath,
                                           100000);
                        if (export.Completed)
                            if (export.ExitCode != 0)
                                MessageBox.Show($"{SystemMessages.ex_XMLImportError}\n\n{export.Output}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Btn_SelectAll_Click(object sender, EventArgs e) {
            for (int i = 0; i < clb_MSTs.Items.Count; i++) clb_MSTs.SetItemChecked(i, true);
            btn_Process.Enabled = true;
        }

        private void Btn_DeselectAll_Click(object sender, EventArgs e) {
            for (int i = 0; i < clb_MSTs.Items.Count; i++) clb_MSTs.SetItemChecked(i, false);
            btn_Process.Enabled = false;
        }
    }
}
