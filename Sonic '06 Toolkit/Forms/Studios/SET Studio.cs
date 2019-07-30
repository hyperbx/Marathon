using System;
using System.IO;
using System.Windows.Forms;

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

namespace Sonic_06_Toolkit
{
    public partial class SET_Studio : Form
    {
        public SET_Studio()
        {
            InitializeComponent();
        }

        void SET_Studio_Load(object sender, EventArgs e)
        {
            btn_Convert.Text = "Export";

            clb_SETs.Items.Clear();

            if (Directory.GetFiles(Tools.Global.currentPath, "*.set").Length > 0)
            {
                modes_Export.Checked = true;
                modes_Import.Checked = false;
                options_DeleteXML.Visible = false;
                options_CreateBackupSET.Visible = false;
            }
            else if (Directory.GetFiles(Tools.Global.currentPath, "*.xml").Length > 0)
            {
                modes_Export.Checked = false;
                modes_Import.Checked = true;
                options_DeleteXML.Visible = true;
                options_CreateBackupSET.Visible = true;
            }
            else { MessageBox.Show("There are no convertable files in this directory.", "No files available", MessageBoxButtons.OK, MessageBoxIcon.Information); Close(); }

            if (Properties.Settings.Default.backupSET == true) options_CreateBackupSET.Checked = true;
            if (Properties.Settings.Default.deleteXML == true) options_DeleteXML.Checked = true;
        }

        void Btn_SelectAll_Click(object sender, EventArgs e)
        {
            //Checks all available checkboxes.
            for (int i = 0; i < clb_SETs.Items.Count; i++) clb_SETs.SetItemChecked(i, true);
            btn_Convert.Enabled = true;
        }

        private void Btn_DeselectAll_Click(object sender, EventArgs e)
        {
            //Checks all available checkboxes.
            for (int i = 0; i < clb_SETs.Items.Count; i++) clb_SETs.SetItemChecked(i, false);
            btn_Convert.Enabled = false;
        }

        void Clb_SETs_SelectedIndexChanged(object sender, EventArgs e)
        {
            clb_SETs.ClearSelected(); //Removes the blue highlight on recently checked boxes.

            //Enables/disables the Convert button, depending on whether a box has been checked.
            if (clb_SETs.CheckedItems.Count > 0)
            {
                btn_Convert.Enabled = true;
            }
            else
            {
                btn_Convert.Enabled = false;
            }
        }

        void Btn_Convert_Click(object sender, EventArgs e)
        {
            //In the odd chance that someone is ever able to click Export without anything selected, this will prevent that.
            if (clb_SETs.CheckedItems.Count == 0) MessageBox.Show("Please select a file.", "No files specified", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (modes_Export.Checked)
            {
                try
                {
                    foreach (string selectedSET in clb_SETs.CheckedItems)
                    {
                        Tools.SET.Export(1, string.Empty, selectedSET);
                    }
                    if (Properties.Settings.Default.disableWarns == false) { MessageBox.Show("All selected SETs have been exported.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred when converting the SETs.\n\n{ex}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Tools.Notification.Dispose();
                }
            }
            else if (modes_Import.Checked)
            {
                try
                {
                    foreach (string selectedXML in clb_SETs.CheckedItems)
                    {
                        Tools.SET.Import(selectedXML);
                    }
                    if (Properties.Settings.Default.disableWarns == false) { MessageBox.Show("All selected XMLs have been imported.", "Import Complete", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred when importing the XMLs.\n\n{ex}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Tools.Notification.Dispose();
                }
            }
        }

        void Modes_Export_CheckedChanged(object sender, EventArgs e)
        {
            if (modes_Export.Checked == true)
            {
                modes_Import.Checked = false;
                options_DeleteXML.Visible = false;
                options_CreateBackupSET.Visible = false;
                btn_Convert.Enabled = false;

                btn_Convert.Text = "Export";

                clb_SETs.Items.Clear();

                #region Getting SETs to unpack...
                foreach (string SET in Directory.GetFiles(Tools.Global.currentPath, "*.set", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(SET))
                    {
                        clb_SETs.Items.Add(Path.GetFileName(SET));
                    }
                }
                #endregion

                if (Directory.GetFiles(Tools.Global.currentPath, "*.set").Length == 0)
                {
                    MessageBox.Show("There are no SETs to export in this directory.", "No SETs available", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Directory.GetFiles(Tools.Global.currentPath, "*.xml").Length == 0)
                    {
                        Close();
                    }
                    else
                    {
                        modes_Export.Checked = false;
                        modes_Import.Checked = true;
                    }
                }
            }
        }

        void Modes_Import_CheckedChanged(object sender, EventArgs e)
        {
            if (modes_Import.Checked == true)
            {
                modes_Export.Checked = false;
                options_DeleteXML.Visible = true;
                options_CreateBackupSET.Visible = true;
                btn_Convert.Enabled = false;

                btn_Convert.Text = "Import";

                clb_SETs.Items.Clear();

                #region Getting SETs to unpack...
                foreach (string XML in Directory.GetFiles(Tools.Global.currentPath, "*.xml", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(XML))
                    {
                        clb_SETs.Items.Add(Path.GetFileName(XML));
                    }
                }
                #endregion

                if (Directory.GetFiles(Tools.Global.currentPath, "*.xml").Length == 0)
                {
                    MessageBox.Show("There are no XMLs to import in this directory.", "No XMLs available", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Directory.GetFiles(Tools.Global.currentPath, "*.set").Length == 0)
                    {
                        Close();
                    }
                    else
                    {
                        modes_Export.Checked = true;
                        modes_Import.Checked = false;
                    }
                }
            }
        }

        void Options_CreateBackupSET_CheckedChanged(object sender, EventArgs e)
        {
            if (options_CreateBackupSET.Checked == true) Properties.Settings.Default.backupSET = true;
            else Properties.Settings.Default.backupSET = false;
            Properties.Settings.Default.Save();
        }

        void Options_DeleteXML_CheckedChanged(object sender, EventArgs e)
        {
            if (options_DeleteXML.Checked == true) Properties.Settings.Default.deleteXML = true;
            else Properties.Settings.Default.deleteXML = false;
            Properties.Settings.Default.Save();
        }
    }
}
