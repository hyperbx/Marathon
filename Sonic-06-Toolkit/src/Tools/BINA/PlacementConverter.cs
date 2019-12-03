using System;
using System.IO;
using System.Linq;
using Toolkit.Text;
using HedgeLib.Sets;
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
    public partial class PlacementConverter : Form
    {
        private Main mainForm = null;
        private string location = Paths.currentPath;

        public PlacementConverter(Form callingForm) {
            mainForm = callingForm as Main;
            InitializeComponent();
            Console.WriteLine(location);
        }

        private void PlacementConverter_Load(object sender, EventArgs e) {
            btn_Process.Text = "Export";
            clb_SETs.Items.Clear();
            btn_Process.Enabled = false;

            if (Directory.GetFiles(location, "*.set").Length > 0) {
                modes_Export.Checked = true;
                modes_Import.Checked = false;
                options_DeleteXML.Visible = false;
                options_CreateBackupSET.Visible = false;
            } else if (Directory.GetFiles(location, "*.xml").Length > 0) {
                modes_Export.Checked = false;
                modes_Import.Checked = true;
                options_DeleteXML.Visible = true;
                options_CreateBackupSET.Visible = true;
            } else {
                MessageBox.Show(SystemMessages.msg_NoConvertableFiles, SystemMessages.tl_NoFilesAvailable(string.Empty), MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }

            options_CreateBackupSET.Checked = Properties.Settings.Default.set_backupSET;
            options_DeleteXML.Checked = Properties.Settings.Default.set_deleteXML;
        }

        private void Btn_Convert_Click(object sender, EventArgs e) {
            List<object> filesToProcess = clb_SETs.CheckedItems.OfType<object>().ToList();
            if (modes_Export.Checked) {
                try {
                    foreach (string SET in filesToProcess)
                        if (File.Exists(Path.Combine(location, SET)) && Verification.VerifyMagicNumberExtended(Path.Combine(location, SET))) {
                            mainForm.Status = StatusMessages.cmn_Exporting(SET, false);
                            var readSET = new S06SetData();
                            readSET.Load(Path.Combine(location, SET));
                            readSET.ExportXML(Path.Combine(location, $"{Path.GetFileNameWithoutExtension(SET)}.xml"));
                        } else { mainForm.Status = StatusMessages.ex_InvalidFile(SET, "SET", false); }
                } catch (Exception ex) {
                    MessageBox.Show($"{SystemMessages.ex_SETExportError}\n\n{ex}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } else if (modes_Import.Checked) {
                try {
                    foreach (string XML in filesToProcess) {
                        if (File.Exists(Path.Combine(location, XML))) {
                            mainForm.Status = StatusMessages.cmn_Importing(XML, false);
                            var readXML = new S06SetData();
                            readXML.ImportXML(Path.Combine(location, XML));

                            if (options_CreateBackupSET.Checked)
                                if (File.Exists(Path.Combine(location, $"{Path.GetFileNameWithoutExtension(XML)}.set")))
                                    File.Copy(Path.Combine(location, $"{Path.GetFileNameWithoutExtension(XML)}.set"), Path.Combine(location, $"{Path.GetFileNameWithoutExtension(XML)}.set.bak"), true);
                        
                            readXML.Save(Path.Combine(location, $"{Path.GetFileNameWithoutExtension(XML)}.set"), true);

                            if (options_DeleteXML.Checked)
                                if (File.Exists(Path.Combine(location, XML)))
                                    try { File.Delete(Path.Combine(location, XML)); }
                                    catch { MessageBox.Show(SystemMessages.ex_XMLDeleteError, SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                    }
                } catch (Exception ex) {
                    MessageBox.Show($"{SystemMessages.ex_XMLImportError}\n\n{ex}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Modes_Export_CheckedChanged(object sender, EventArgs e) {
            if (modes_Export.Checked) {
                modes_Import.Checked = false;
                options_DeleteXML.Visible = false;
                options_CreateBackupSET.Visible = false;
                btn_Process.Enabled = false;
                btn_Process.Text = "Export";
                clb_SETs.Items.Clear();

                foreach (string SET in Directory.GetFiles(location, "*.set", SearchOption.TopDirectoryOnly))
                    if (File.Exists(SET) && Verification.VerifyMagicNumberExtended(SET))
                        clb_SETs.Items.Add(Path.GetFileName(SET));

                if (clb_SETs.Items.Count == 0) {
                    MessageBox.Show(SystemMessages.msg_NoSETsInDir, SystemMessages.tl_NoFilesAvailable("SET"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Directory.GetFiles(location, "*.xml").Length == 0) Close();
                    else {
                        modes_Export.Checked = false;
                        modes_Import.Checked = true;
                    }
                }
            } else if (!modes_Export.Checked) modes_Import.Checked = true;
        }

        private void Modes_Import_CheckedChanged(object sender, EventArgs e) {
            if (modes_Import.Checked) {
                modes_Export.Checked = false;
                options_DeleteXML.Visible = true;
                options_CreateBackupSET.Visible = true;
                btn_Process.Enabled = false;
                btn_Process.Text = "Import";
                clb_SETs.Items.Clear();

                foreach (string XML in Directory.GetFiles(location, "*.xml", SearchOption.TopDirectoryOnly))
                    if (File.Exists(XML) && Verification.VerifyXML(XML, "SET"))
                        clb_SETs.Items.Add(Path.GetFileName(XML));

                if (clb_SETs.Items.Count == 0) {
                    MessageBox.Show(SystemMessages.msg_NoXMLsInDir, SystemMessages.tl_NoFilesAvailable("XML"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Directory.GetFiles(location, "*.set").Length == 0) Close();
                    else {
                        modes_Export.Checked = true;
                        modes_Import.Checked = false;
                    }
                }
            } else if (!modes_Import.Checked) modes_Export.Checked = true;
        }

        private void clb_SETs_SelectedIndexChanged(object sender, EventArgs e) {
            clb_SETs.ClearSelected();
            btn_Process.Enabled = clb_SETs.CheckedItems.Count > 0;
        }

        private void btn_SelectAll_Click(object sender, EventArgs e) {
            for (int i = 0; i < clb_SETs.Items.Count; i++) clb_SETs.SetItemChecked(i, true);
            btn_Process.Enabled = true;
        }

        private void btn_DeselectAll_Click(object sender, EventArgs e) {
            for (int i = 0; i < clb_SETs.Items.Count; i++) clb_SETs.SetItemChecked(i, false);
            btn_Process.Enabled = false;
        }

        private void Options_CreateBackupSET_CheckedChanged(object sender, EventArgs e) { Properties.Settings.Default.set_backupSET = options_CreateBackupSET.Checked; }

        private void Options_DeleteXML_CheckedChanged(object sender, EventArgs e) { Properties.Settings.Default.set_deleteXML = options_DeleteXML.Checked; }

        private void PlacementConverter_FormClosing(object sender, FormClosingEventArgs e) { Properties.Settings.Default.Save(); }
    }
}
