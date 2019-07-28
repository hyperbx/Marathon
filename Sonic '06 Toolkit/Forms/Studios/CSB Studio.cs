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
    public partial class CSB_Studio : Form
    {
        public CSB_Studio()
        {
            InitializeComponent();
        }

        void Btn_SelectAll_Click(object sender, System.EventArgs e)
        {
            //Checks all available checkboxes.
            for (int i = 0; i < clb_CSBs.Items.Count; i++) clb_CSBs.SetItemChecked(i, true);
            btn_Extract.Enabled = true;
        }

        void Btn_DeselectAll_Click(object sender, System.EventArgs e)
        {
            //Unchecks all available checkboxes.
            for (int i = 0; i < clb_CSBs.Items.Count; i++) clb_CSBs.SetItemChecked(i, false);
            btn_Extract.Enabled = false;
        }

        void Clb_CSBs_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            clb_CSBs.ClearSelected(); //Removes the blue highlight on recently checked boxes.

            //Enables/disables the Extract button, depending on whether a box has been checked.
            if (clb_CSBs.CheckedItems.Count > 0)
            {
                btn_Extract.Enabled = true;
            }
            else
            {
                btn_Extract.Enabled = false;
            }
        }

        void Btn_Extract_Click(object sender, System.EventArgs e)
        {
            //In the odd chance that someone is ever able to click Extract without anything selected, this will prevent that.
            if (clb_CSBs.CheckedItems.Count == 0) MessageBox.Show("Please select a file.", "No files specified", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (modes_UnpackToAIF.Checked)
            {
                try
                {
                    //Gets all checked boxes from the CheckedListBox and builds a string for each CSB.
                    foreach (string selectedCSB in clb_CSBs.CheckedItems)
                    {
                        Tools.CSB.Packer(1, null, selectedCSB);
                    }
                }
                catch
                {
                    MessageBox.Show("An error occurred when unpacking the selected CSBs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Tools.Notification.Dispose();
                }
            }
            else if (modes_UnpackToWAV.Checked)
            {
                try
                {
                    //Gets all checked boxes from the CheckedListBox and builds a string for each CSB.
                    foreach (string selectedCSB in clb_CSBs.CheckedItems)
                    {
                        Tools.CSB.Packer(3, null, selectedCSB);
                    }
                }
                catch
                {
                    MessageBox.Show("An error occurred when unpacking the selected CSBs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Tools.Notification.Dispose();
                }
            }
            else if (modes_RepackToCSB.Checked)
            {
                try
                {
                    //Gets all checked boxes from the CheckedListBox and builds a string for each CSB.
                    foreach (string selectedCSB in clb_CSBs.CheckedItems)
                    {
                        Tools.CSB.Packer(2, null, selectedCSB);
                    }
                }
                catch
                {
                    MessageBox.Show("An error occurred when repacking the selected CSBs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Tools.Notification.Dispose();
                }
            }
        }

        private void Modes_UnpackToAIF_CheckedChanged(object sender, EventArgs e)
        {
            if (modes_UnpackToAIF.Checked)
            {
                Properties.Settings.Default.csbUnpackMode = 0;

                clb_CSBs.Items.Clear();
                btn_Extract.Text = "Unpack";

                #region Getting CSB files to convert...
                foreach (string CSB in Directory.GetFiles(Tools.Global.currentPath, "*.csb", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(CSB))
                    {
                        clb_CSBs.Items.Add(Path.GetFileName(CSB));
                    }
                }
                #endregion

                modes_UnpackToAIF.Checked = true;
                modes_UnpackToWAV.Checked = false;
                modes_RepackToCSB.Checked = false;
                btn_Extract.Enabled = false;

                if (Directory.GetFiles(Tools.Global.currentPath, "*.csb").Length == 0)
                {
                    MessageBox.Show("There are no CSB files to unpack in this directory.", "No CSB files available", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    foreach (string CSB in Directory.GetDirectories(Tools.Global.currentPath))
                    {
                        if (Directory.Exists(CSB))
                        {
                            clb_CSBs.Items.Add(Path.GetFileName(CSB));
                        }
                    }

                    //Checks if there are any CSBs in the directory.
                    if (clb_CSBs.Items.Count == 0)
                    {
                        MessageBox.Show("There are no CSB directories to repack in this directory.", "No CSB directories available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                }
            }
            Properties.Settings.Default.Save();
        }

        private void Modes_UnpackToWAV_CheckedChanged(object sender, EventArgs e)
        {
            if (modes_UnpackToWAV.Checked)
            {
                Properties.Settings.Default.csbUnpackMode = 1;

                clb_CSBs.Items.Clear();
                btn_Extract.Text = "Unpack";

                #region Getting CSB files to convert...
                foreach (string CSB in Directory.GetFiles(Tools.Global.currentPath, "*.csb", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(CSB))
                    {
                        clb_CSBs.Items.Add(Path.GetFileName(CSB));
                    }
                }
                #endregion

                modes_UnpackToAIF.Checked = false;
                modes_UnpackToWAV.Checked = true;
                modes_RepackToCSB.Checked = false;
                btn_Extract.Enabled = false;

                if (Directory.GetFiles(Tools.Global.currentPath, "*.csb").Length == 0)
                {
                    MessageBox.Show("There are no CSB files to unpack in this directory.", "No CSB files available", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    foreach (string CSB in Directory.GetDirectories(Tools.Global.currentPath))
                    {
                        if (Directory.Exists(CSB))
                        {
                            clb_CSBs.Items.Add(Path.GetFileName(CSB));
                        }
                    }

                    //Checks if there are any CSBs in the directory.
                    if (clb_CSBs.Items.Count == 0)
                    {
                        MessageBox.Show("There are no CSB directories to repack in this directory.", "No CSB directories available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                }
            }
            Properties.Settings.Default.Save();
        }

        private void Modes_RepackToCSB_CheckedChanged(object sender, EventArgs e)
        {
            if (modes_RepackToCSB.Checked)
            {
                clb_CSBs.Items.Clear();
                btn_Extract.Text = "Repack";

                #region Getting CSB files to convert...
                foreach (string CSB in Directory.GetDirectories(Tools.Global.currentPath))
                {
                    if (Directory.Exists(CSB))
                    {
                        clb_CSBs.Items.Add(Path.GetFileName(CSB));
                    }
                }
                #endregion

                modes_UnpackToAIF.Checked = false;
                modes_UnpackToWAV.Checked = false;
                modes_RepackToCSB.Checked = true;
                btn_Extract.Enabled = false;

                if (clb_CSBs.Items.Count == 0)
                {
                    MessageBox.Show("There are no CSB directories to repack in this directory.", "No CSB directories available", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    foreach (string CSB in Directory.GetFiles(Tools.Global.currentPath, "*.csb", SearchOption.TopDirectoryOnly))
                    {
                        if (File.Exists(CSB))
                        {
                            clb_CSBs.Items.Add(Path.GetFileName(CSB));
                        }
                    }

                    //Checks if there are any CSBs in the directory.
                    if (clb_CSBs.Items.Count == 0)
                    {
                        MessageBox.Show("There are no CSB files to unpack in this directory.", "No CSB files available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                }
            }
        }

        private void CSB_Studio_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.csbUnpackMode == 0) { modes_UnpackToAIF.Checked = true; modes_UnpackToWAV.Checked = false; }
            else { modes_UnpackToAIF.Checked = false; modes_UnpackToWAV.Checked = true; }
        }
    }
}
