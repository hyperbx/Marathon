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
    public partial class LUB_Studio : Form
    {
        public LUB_Studio()
        {
            InitializeComponent();
        }

        void LUB_Studio_Load(object sender, EventArgs e)
        {
            btn_Decompile.Text = "Decompile";

            clb_LUBs.Items.Clear();

            if (VerifyDecompilableLUBs())
            {
                combo_Mode.SelectedIndex = 0;
            }
            else if (VerifyCompilableLUBs())
            {
                combo_Mode.SelectedIndex = 1;
            }
            else { MessageBox.Show("There are no Lua binaries in this directory.", "No files available", MessageBoxButtons.OK, MessageBoxIcon.Information); Close(); }
        }

        bool VerifyDecompilableLUBs()
        {
            #region Getting and verifying Lua binaries...
            //Checks the header for each file to ensure that it can be safely decompiled then adds all decompilable LUBs in the current path to the CheckedListBox.
            foreach (string LUB in Directory.GetFiles(Tools.Global.currentPath, "*.lub", SearchOption.TopDirectoryOnly))
            {
                if (File.Exists(LUB))
                {
                    if (File.ReadAllLines(LUB)[0].Contains("LuaP"))
                    {
                        clb_LUBs.Items.Add(Path.GetFileName(LUB));
                    }
                }
            }

            //Checks if there are any LUBs in the directory.
            if (clb_LUBs.Items.Count == 0)
                return false;

            return true;
            #endregion
        }

        bool VerifyCompilableLUBs()
        {
            #region Getting and verifying Lua binaries...
            //Checks the header for each file to ensure that it can be safely decompiled then adds all decompilable LUBs in the current path to the CheckedListBox.
            foreach (string LUB in Directory.GetFiles(Tools.Global.currentPath, "*.lub", SearchOption.TopDirectoryOnly))
            {
                if (File.Exists(LUB))
                {
                    if (!File.ReadAllLines(LUB)[0].Contains("LuaP"))
                    {
                        clb_LUBs.Items.Add(Path.GetFileName(LUB));
                    }
                }
            }

            //Checks if there are any LUBs in the directory.
            if (clb_LUBs.Items.Count == 0)
                return false;

            return true;
            #endregion
        }

        void Btn_SelectAll_Click(object sender, EventArgs e)
        {
            //Checks all available checkboxes.
            for (int i = 0; i < clb_LUBs.Items.Count; i++) clb_LUBs.SetItemChecked(i, true);
            btn_Decompile.Enabled = true;
        }

        void Btn_DeselectAll_Click(object sender, EventArgs e)
        {
            //Unchecks all available checkboxes.
            for (int i = 0; i < clb_LUBs.Items.Count; i++) clb_LUBs.SetItemChecked(i, false);
            btn_Decompile.Enabled = false;
        }

        void Btn_Decompile_Click(object sender, EventArgs e)
        {
            //In the odd chance that someone is ever able to click Decompile without anything selected, this will prevent that.
            if (clb_LUBs.CheckedItems.Count == 0) MessageBox.Show("Please select a Lua binary.", "No Lua binaries specified", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (combo_Mode.SelectedIndex == 0)
            {
                try
                {
                    //Gets all checked boxes from the CheckedListBox and builds a string for each LUB.
                    foreach (string selectedLUB in clb_LUBs.CheckedItems)
                    {
                        Tools.LUB.Decompile(Path.Combine(Tools.Global.currentPath, selectedLUB));
                    }
                    if (Properties.Settings.Default.disableWarns == false) { MessageBox.Show("All selected LUBs have been decompiled.", "Decompilation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                    clb_LUBs.Items.Clear();

                    //Checks if there are any CSBs in the directory.
                    if (!VerifyDecompilableLUBs())
                    {
                        btn_Decompile.Enabled = false;
                        MessageBox.Show("There are no LUBs to decompile in this directory.", "No LUBs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show($"An error occurred when decompiling the selected Lua binaries.\n\n{ex}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (combo_Mode.SelectedIndex == 1)
            {
                try
                {
                    //Gets all checked boxes from the CheckedListBox and builds a string for each LUB.
                    foreach (string selectedLUB in clb_LUBs.CheckedItems)
                    {
                        Tools.LUB.Compile(Path.Combine(Tools.Global.currentPath, selectedLUB));
                    }
                    if (Properties.Settings.Default.disableWarns == false) { MessageBox.Show("All selected LUBs have been compiled.", "Compilation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                    clb_LUBs.Items.Clear();

                    //Checks if there are any CSBs in the directory.
                    if (!VerifyCompilableLUBs())
                    {
                        btn_Decompile.Enabled = false;
                        MessageBox.Show("There are no LUBs to compile in this directory.", "No LUBs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        combo_Mode.SelectedIndex = 0;
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show($"An error occurred when compiling the selected Lua binaries.\n\n{ex}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void Clb_LUBs_SelectedIndexChanged(object sender, EventArgs e)
        {
            clb_LUBs.ClearSelected(); //Removes the blue highlight on recently checked boxes.

            //Enables/disables the Decompile button, depending on whether a box has been checked.
            if (clb_LUBs.CheckedItems.Count > 0)
            {
                btn_Decompile.Enabled = true;
            }
            else
            {
                btn_Decompile.Enabled = false;
            }
        }

        private void Combo_Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_Mode.SelectedIndex == 0)
            {
                btn_Decompile.Text = "Decompile";

                clb_LUBs.Items.Clear();

                //Checks if there are any CSBs in the directory.
                if (!VerifyDecompilableLUBs())
                {
                    btn_Decompile.Enabled = false;
                    MessageBox.Show("There are no LUBs to decompile in this directory.", "No LUBs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            else if (combo_Mode.SelectedIndex == 1)
            {
                btn_Decompile.Text = "Compile";

                clb_LUBs.Items.Clear();

                //Checks if there are any CSBs in the directory.
                if (!VerifyCompilableLUBs())
                {
                    btn_Decompile.Enabled = false;
                    MessageBox.Show("There are no LUBs to compile in this directory.", "No LUBs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    combo_Mode.SelectedIndex = 0;
                }
            }
        }
    }
}
