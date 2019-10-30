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
    public partial class CollisionGenerator : Form
    {
        private Main mainForm = null;
        private string location = Paths.currentPath;

        public CollisionGenerator(Form callingForm) {
            mainForm = callingForm as Main;
            InitializeComponent();
        }

        private void CollisionGenerator_Load(object sender, EventArgs e) {
            clb_MDLs.Items.Clear();

            if (Directory.GetFiles(location, "*.bin").Length > 0) {
                combo_Mode.SelectedIndex = 0;
            } else if (Directory.GetFiles(location, "*.obj").Length > 0) {
                combo_Mode.SelectedIndex = 1;
            } else {
                MessageBox.Show(SystemMessages.msg_NoConvertableFiles, SystemMessages.tl_NoFilesAvailable(string.Empty), MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }

        private void Clb_MDLs_SelectedIndexChanged(object sender, EventArgs e) {
            clb_MDLs.ClearSelected();
            btn_Process.Enabled = clb_MDLs.CheckedItems.Count > 0;
        }

        private void Combo_Mode_SelectedIndexChanged(object sender, EventArgs e) {
            if (combo_Mode.SelectedIndex == 0) {
                btn_Process.Text = "Export";

                clb_MDLs.Items.Clear();
                foreach (string BIN in Directory.GetFiles(location, "*.bin", SearchOption.TopDirectoryOnly))
                    if (File.Exists(BIN) && Verification.VerifyMagicNumberExtended(BIN))
                        clb_MDLs.Items.Add(Path.GetFileName(BIN));

                if (Directory.GetFiles(location, "*.bin").Length == 0) {
                    MessageBox.Show(SystemMessages.msg_NoConvertableFiles, SystemMessages.tl_NoFilesAvailable("BIN"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Directory.GetFiles(location, "*.obj").Length == 0) Close();
                    else combo_Mode.SelectedIndex = 1;
                }
            } else if (combo_Mode.SelectedIndex == 1) {
                btn_Process.Text = "Import";

                clb_MDLs.Items.Clear();
                foreach (string OBJ in Directory.GetFiles(location, "*.obj", SearchOption.TopDirectoryOnly))
                    if (File.Exists(OBJ) && Verification.VerifyMagicNumberCommon(OBJ))
                        clb_MDLs.Items.Add(Path.GetFileName(OBJ));

                if (Directory.GetFiles(location, "*.obj").Length == 0) {
                    MessageBox.Show(SystemMessages.msg_NoConvertableFiles, SystemMessages.tl_NoFilesAvailable("OBJ"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Directory.GetFiles(location, "*.bin").Length == 0) Close();
                    else combo_Mode.SelectedIndex = 0;
                }
            }
        }

        private async void Btn_Process_Click(object sender, EventArgs e) {
            List<object> filesToProcess = clb_MDLs.CheckedItems.OfType<object>().ToList();
            if (combo_Mode.SelectedIndex == 0) {
                if (Verification.VerifyApplicationIntegrity(Paths.BINDecoder)) {
                    foreach (string BIN in filesToProcess) {
                        if (File.Exists(Path.Combine(location, BIN)) && Verification.VerifyMagicNumberExtended(Path.Combine(location, BIN))) {
                            mainForm.Status = StatusMessages.cmn_Exporting(Path.Combine(location, BIN), false);
                            var export = await ProcessAsyncHelper.ExecuteShellCommand(Paths.BINDecoder,
                                               $"\"{Path.Combine(location, BIN)}\"",
                                               location,
                                               100000);
                            if (export.Completed)
                                if (export.ExitCode != 0)
                                    MessageBox.Show($"{SystemMessages.ex_BINExportError}\n\n{export.Output}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                } else
                    MessageBox.Show(SystemMessages.ex_IntegrityCheckFailed, SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else if (combo_Mode.SelectedIndex == 1) {
                foreach (string OBJ in filesToProcess) {
                    if (File.Exists(Path.Combine(location, OBJ)) && Verification.VerifyMagicNumberCommon(Path.Combine(location, OBJ))) {
                        string[] getTags = File.ReadAllLines(Path.Combine(location, OBJ));
                        int i = 0;

                        foreach (string line in getTags)
                            if (line.Contains("_at_"))
                                i++;

                        if (i != 0)
                            if (MessageBox.Show(SystemMessages.msg_CollisionTagsDetected(i, OBJ), SystemMessages.tl_CollisionTagsDetected, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                                int lineNum = 0;
                                foreach (string line in getTags) {
                                    if (line.Contains("_at_")) {
                                        string temp = line.Replace("_at_", "@");
                                        getTags[lineNum] = temp;
                                    }
                                    lineNum++;
                                }
                                File.WriteAllLines(Path.Combine(location, OBJ), getTags);
                            }

                        mainForm.Status = StatusMessages.cmn_Importing(Path.Combine(location, OBJ), false);
                        var import = await ProcessAsyncHelper.ExecuteShellCommand(Paths.BINEncoder,
                                           $"\"{Path.Combine(location, OBJ)}\"",
                                           location,
                                           100000);
                        if (import.Completed)
                            if (import.ExitCode != 0)
                                MessageBox.Show($"{SystemMessages.ex_OBJImportError}\n\n{import.Output}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void Btn_SelectAll_Click(object sender, EventArgs e) {
            for (int i = 0; i < clb_MDLs.Items.Count; i++) clb_MDLs.SetItemChecked(i, true);
            btn_Process.Enabled = true;
        }

        private void Btn_DeselectAll_Click(object sender, EventArgs e) {
            for (int i = 0; i < clb_MDLs.Items.Count; i++) clb_MDLs.SetItemChecked(i, false);
            btn_Process.Enabled = false;
        }
    }
}
