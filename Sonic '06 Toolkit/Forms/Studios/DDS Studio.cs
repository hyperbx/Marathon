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
    public partial class DDS_Studio : Form
    {
        public DDS_Studio()
        {
            InitializeComponent();
        }

        public static string useGPU = " -nogpu";
        public static string forceDirectX10 = "";

        void DDS_Studio_Load(object sender, EventArgs e)
        {
            clb_DDS.Items.Clear();

            if (Directory.GetFiles(Tools.Global.currentPath, "*.dds").Length > 0)
            {
                modes_DDStoPNG.Checked = true;
                modes_PNGtoDDS.Checked = false;
            }
            else if (Directory.GetFiles(Tools.Global.currentPath, "*.png").Length > 0)
            {
                modes_DDStoPNG.Checked = false;
                modes_PNGtoDDS.Checked = true;
            }
            else { MessageBox.Show("There are no convertable files in this directory.", "No files available", MessageBoxButtons.OK, MessageBoxIcon.Information); Close(); }

            if (Properties.Settings.Default.useGPU == true) options_UseGPU.Checked = true;
            if (Properties.Settings.Default.forceDirectX10 == true) options_ForceDX10.Checked = true;
        }

        void Clb_DDS_SelectedIndexChanged(object sender, EventArgs e)
        {
            clb_DDS.ClearSelected(); //Removes the blue highlight on recently checked boxes.

            //Enables/disables the Convert button, depending on whether a box has been checked.
            if (clb_DDS.CheckedItems.Count > 0)
            {
                btn_Convert.Enabled = true;
            }
            else
            {
                btn_Convert.Enabled = false;
            }
        }

        void Modes_DDStoPNG_CheckedChanged(object sender, EventArgs e)
        {
            if (modes_DDStoPNG.Checked == true)
            {
                clb_DDS.Items.Clear();

                #region Getting DDS files to convert...
                foreach (string DDS in Directory.GetFiles(Tools.Global.currentPath, "*.dds", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(DDS))
                    {
                        clb_DDS.Items.Add(Path.GetFileName(DDS));
                    }
                }
                #endregion

                modes_PNGtoDDS.Checked = false;
                btn_Convert.Enabled = false;

                if (Directory.GetFiles(Tools.Global.currentPath, "*.dds").Length == 0)
                {
                    MessageBox.Show("There are no DDS files to convert in this directory.", "No DDS files available", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Directory.GetFiles(Tools.Global.currentPath, "*.png").Length == 0)
                    {
                        Close();
                    }
                    else
                    {
                        modes_DDStoPNG.Checked = false;
                        modes_PNGtoDDS.Checked = true;
                    }
                }
            }
        }

        void Modes_PNGtoDDS_CheckedChanged(object sender, EventArgs e)
        {
            if (modes_PNGtoDDS.Checked == true)
            {
                clb_DDS.Items.Clear();

                #region Getting PNG files to convert...
                foreach (string PNG in Directory.GetFiles(Tools.Global.currentPath, "*.png", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(PNG))
                    {
                        clb_DDS.Items.Add(Path.GetFileName(PNG));
                    }
                }
                #endregion

                modes_DDStoPNG.Checked = false;
                btn_Convert.Enabled = false;

                if (Directory.GetFiles(Tools.Global.currentPath, "*.png").Length == 0)
                {
                    MessageBox.Show("There are no PNG files to convert in this directory.", "No PNG files available", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Directory.GetFiles(Tools.Global.currentPath, "*.dds").Length == 0)
                    {
                        Close();
                    }
                    else
                    {
                        modes_DDStoPNG.Checked = true;
                        modes_PNGtoDDS.Checked = false;
                    }
                }
            }
        }

        void Options_UseGPU_CheckedChanged(object sender, EventArgs e)
        {
            if (options_UseGPU.Checked == true)
            {
                useGPU = "";
                Properties.Settings.Default.useGPU = true;
            }
            else
            {
                useGPU = " -nogpu";
                Properties.Settings.Default.useGPU = false;
            }
            Properties.Settings.Default.Save();
        }

        void Options_ForceDX10_CheckedChanged(object sender, EventArgs e)
        {
            if (options_ForceDX10.Checked == true)
            {
                forceDirectX10 = " -dx10";
                Properties.Settings.Default.forceDirectX10 = true;
            }
            else
            {
                forceDirectX10 = "";
                Properties.Settings.Default.forceDirectX10 = false;
            }
            Properties.Settings.Default.Save();
        }

        void Btn_SelectAll_Click(object sender, EventArgs e)
        {
            //Checks all available checkboxes.
            for (int i = 0; i < clb_DDS.Items.Count; i++) clb_DDS.SetItemChecked(i, true);
            btn_Convert.Enabled = true;
        }

        void Btn_DeselectAll_Click(object sender, EventArgs e)
        {
            //Unchecks all available checkboxes.
            for (int i = 0; i < clb_DDS.Items.Count; i++) clb_DDS.SetItemChecked(i, false);
            btn_Convert.Enabled = false;
        }

        void Btn_Convert_Click(object sender, EventArgs e)
        {
            //In the odd chance that someone is ever able to click Extract without anything selected, this will prevent that.
            if (clb_DDS.CheckedItems.Count == 0) MessageBox.Show("Please select a file.", "No files specified", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (modes_DDStoPNG.Checked)
            {
                try
                {
                    //Gets all checked boxes from the CheckedListBox and builds a string for each ADX.
                    foreach (string selectedDDS in clb_DDS.CheckedItems)
                    {
                        Tools.DDS.Convert(1, string.Empty, selectedDDS);
                    }
                }
                catch
                {
                    MessageBox.Show("An error occurred when converting the selected DDS files.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Tools.Notification.Dispose();
                }
            }
            else if (modes_PNGtoDDS.Checked)
            {
                try
                {
                    //Gets all checked boxes from the CheckedListBox and builds a string for each WAV.
                    foreach (string selectedPNG in clb_DDS.CheckedItems)
                    {
                        Tools.PNG.Convert(1, string.Empty, selectedPNG);
                    }
                }
                catch
                {
                    MessageBox.Show("An error occurred when converting the selected PNG files.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Tools.Notification.Dispose();
                }
            }
        }
    }
}
