using System;
using System.IO;
using HedgeLib.Headers;
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
    public partial class Debug_Studio : Form
    {
        public Debug_Studio()
        {
            InitializeComponent();
        }

        void Debug_Studio_Load(object sender, EventArgs e)
        {
            clb_Debug.Items.Clear();

            if (Directory.GetFiles(Tools.Global.currentPath, "*.sbk").Length > 0) { modes_SBKStudio.Checked = true; }
            else { MessageBox.Show("There are no encodable files in this directory.\n\nDebug Studio searches for the following:\n► SBK", "No files available", MessageBoxButtons.OK, MessageBoxIcon.Information); Close(); }
        }

        void Btn_SelectAll_Click(object sender, EventArgs e)
        {
            //Checks all available checkboxes.
            for (int i = 0; i < clb_Debug.Items.Count; i++) clb_Debug.SetItemChecked(i, true);
            btn_Convert.Enabled = true;
        }

        void Btn_DeselectAll_Click(object sender, EventArgs e)
        {
            //Unchecks all available checkboxes.
            for (int i = 0; i < clb_Debug.Items.Count; i++) clb_Debug.SetItemChecked(i, false);
            btn_Convert.Enabled = false;
        }

        void Modes_SBK_CheckedChanged(object sender, EventArgs e)
        {
            if (modes_SBKStudio.Checked == true)
            {
                foreach (string SBK in Directory.GetFiles(Tools.Global.currentPath, "*.sbk", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(SBK))
                    {
                        clb_Debug.Items.Add(Path.GetFileName(SBK));
                    }
                }
            }
            else
            {
                clb_Debug.Items.Clear();
                Close();
            }
        }

        void Clb_Debug_SelectedIndexChanged(object sender, EventArgs e)
        {
            clb_Debug.ClearSelected(); //Removes the blue highlight on recently checked boxes.

            //Enables/disables the Extract button, depending on whether a box has been checked.
            if (clb_Debug.CheckedItems.Count > 0)
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
            if (modes_SBKStudio.Checked == true)
            {
                /* UNFINISHED CODE - SBK USES BINA */
                try
                {
                    //Gets all checked boxes from the CheckedListBox and builds a string for each AT3.
                    foreach (string selectedSBK in clb_Debug.CheckedItems)
                    {
                        var readSBK = new BINAv1Header();
                    }
                }
                catch
                {
                    MessageBox.Show("An error occurred when decoding the selected SBKs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
