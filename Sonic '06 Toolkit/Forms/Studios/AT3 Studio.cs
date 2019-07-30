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
    public partial class AT3_Studio : Form
    {
        public AT3_Studio()
        {
            InitializeComponent();
        }

        public static string wholeLoop = "-wholeloop ";
        public static string beginLoop = "";
        public static string startLoop = "";
        public static string endLoop = "";

        void AT3_Studio_Load(object sender, EventArgs e)
        {
            clb_AT3.Items.Clear();

            if (Directory.GetFiles(Tools.Global.currentPath, "*.at3").Length > 0)
            {
                modes_AT3toWAV.Checked = true;
                modes_WAVtoAT3.Checked = false;
                options_Looping.Visible = false;
            }
            else if (Directory.GetFiles(Tools.Global.currentPath, "*.wav").Length > 0)
            {
                modes_AT3toWAV.Checked = false;
                modes_WAVtoAT3.Checked = true;
                options_Looping.Visible = true;
            }
            else { MessageBox.Show("There are no encodable files in this directory.", "No files available", MessageBoxButtons.OK, MessageBoxIcon.Information); Close(); }

            if (Properties.Settings.Default.AT3wholeLoop == true) looping_Whole.Checked = true; else looping_Whole.Checked = false;
        }

        void Btn_SelectAll_Click(object sender, EventArgs e)
        {
            //Checks all available checkboxes.
            for (int i = 0; i < clb_AT3.Items.Count; i++) clb_AT3.SetItemChecked(i, true);
            btn_Convert.Enabled = true;
        }

        void Btn_DeselectAll_Click(object sender, EventArgs e)
        {
            //Unchecks all available checkboxes.
            for (int i = 0; i < clb_AT3.Items.Count; i++) clb_AT3.SetItemChecked(i, false);
            btn_Convert.Enabled = false;
        }

        void Modes_AT3toWAV_CheckedChanged(object sender, EventArgs e)
        {
            if (modes_AT3toWAV.Checked == true)
            {
                clb_AT3.Items.Clear();

                #region Getting AT3 files to convert...
                foreach (string AT3 in Directory.GetFiles(Tools.Global.currentPath, "*.at3", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(AT3))
                    {
                        clb_AT3.Items.Add(Path.GetFileName(AT3));
                    }
                }
                #endregion

                modes_WAVtoAT3.Checked = false;
                options_Looping.Visible = false;
                btn_Convert.Enabled = false;

                if (Directory.GetFiles(Tools.Global.currentPath, "*.at3").Length == 0)
                {
                    MessageBox.Show("There are no AT3 files to encode in this directory.", "No AT3 files available", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Directory.GetFiles(Tools.Global.currentPath, "*.wav").Length == 0)
                    {
                        Close();
                    }
                    else
                    {
                        modes_AT3toWAV.Checked = false;
                        modes_WAVtoAT3.Checked = true;
                    }
                }
            }
        }

        void Modes_WAVtoAT3_CheckedChanged(object sender, EventArgs e)
        {
            if (modes_WAVtoAT3.Checked == true)
            {
                clb_AT3.Items.Clear();

                #region Getting WAV files to convert...
                foreach (string WAV in Directory.GetFiles(Tools.Global.currentPath, "*.wav", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(WAV))
                    {
                        clb_AT3.Items.Add(Path.GetFileName(WAV));
                    }
                }
                #endregion

                modes_AT3toWAV.Checked = false;
                options_Looping.Visible = true;
                btn_Convert.Enabled = false;

                if (Directory.GetFiles(Tools.Global.currentPath, "*.wav").Length == 0)
                {
                    MessageBox.Show("There are no WAV files to encode in this directory.", "No WAV files available", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Directory.GetFiles(Tools.Global.currentPath, "*.at3").Length == 0)
                    {
                        Close();
                    }
                    else
                    {
                        modes_AT3toWAV.Checked = true;
                        modes_WAVtoAT3.Checked = false;
                    }
                }
            }
        }

        void Clb_AT3_SelectedIndexChanged(object sender, EventArgs e)
        {
            clb_AT3.ClearSelected(); //Removes the blue highlight on recently checked boxes.

            //Enables/disables the Decompile button, depending on whether a box has been checked.
            if (clb_AT3.CheckedItems.Count > 0)
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
            //In the odd chance that someone is ever able to click Extract without anything selected, this will prevent that.
            if (clb_AT3.CheckedItems.Count == 0) MessageBox.Show("Please select a file.", "No files specified", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (modes_AT3toWAV.Checked)
            {
                try
                {
                    //Gets all checked boxes from the CheckedListBox and builds a string for each AT3.
                    foreach (string selectedAT3 in clb_AT3.CheckedItems)
                    {
                        Tools.AT3.ConvertToWAV(1, string.Empty, selectedAT3);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred when encoding the selected AT3 files.\n\n{ex}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Tools.Notification.Dispose();
                }
            }
            else if (modes_WAVtoAT3.Checked)
            {
                try
                {
                    //Gets all checked boxes from the CheckedListBox and builds a string for each WAV.
                    foreach (string selectedWAV in clb_AT3.CheckedItems)
                    {
                        Tools.AT3.ConvertToAT3(1, selectedWAV);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred when encoding the selected WAV files.\n\n{ex}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Tools.Notification.Dispose();
                }
            }
        }

        void Looping_Whole_CheckedChanged(object sender, EventArgs e)
        {
            if (looping_Whole.Checked == true)
            {
                wholeLoop = "-wholeloop ";
                Properties.Settings.Default.AT3wholeLoop = true;
            }
            else
            {
                wholeLoop = "";
                Properties.Settings.Default.AT3wholeLoop = false;
            }
            Properties.Settings.Default.Save();
        }
    }
}
