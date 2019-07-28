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
    public partial class XNO_Studio : Form
    {
        public XNO_Studio()
        {
            InitializeComponent();
        }

        void XNO_Studio_Load(object sender, EventArgs e)
        {
            #region Getting XNOs...
            //Adds all XNOs in the current path to the CheckedListBox.
            foreach (string XNO in Directory.GetFiles(Tools.Global.currentPath, "*.xno", SearchOption.TopDirectoryOnly))
            {
                if (File.Exists(XNO))
                {
                    clb_XNOs.Items.Add(Path.GetFileName(XNO));
                }
            }
            //Checks if there are any XNOs in the directory.
            if (clb_XNOs.Items.Count == 0)
            {
                MessageBox.Show("There are no XNOs to convert in this directory.", "No XNOs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            #endregion

            split_XNMStudio.Visible = false;
            tm_ItemCheck.Start();
        }

        void Btn_SelectAll_Click(object sender, EventArgs e)
        {
            //Checks all available checkboxes.
            for (int i = 0; i < clb_XNOs.Items.Count; i++) clb_XNOs.SetItemChecked(i, true);
            btn_Convert.Enabled = true;
        }

        void Btn_DeselectAll_Click(object sender, EventArgs e)
        {
            //Unchecks all available checkboxes.
            for (int i = 0; i < clb_XNOs_XNM.Items.Count; i++) clb_XNOs_XNM.SetItemChecked(i, false);
            for (int i = 0; i < clb_XNMs.Items.Count; i++) clb_XNMs.SetItemChecked(i, false);
            btn_Convert.Enabled = false;
        }

        void Btn_Decompile_Click(object sender, EventArgs e)
        {
            if (!check_XNM.Checked)
            {
                try
                {
                    //Gets all checked boxes from the CheckedListBox and builds a string for each XNO.
                    foreach (string selectedXNO in clb_XNOs.CheckedItems)
                    {
                        Tools.XNO.Convert(1, string.Empty, selectedXNO);
                    }
                }
                catch
                {
                    MessageBox.Show("An error occurred when converting the selected XNOs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Tools.Notification.Dispose();
                }
            }
            else
            {
                //In the odd chance that someone is ever able to click Convert without anything selected, this will prevent that.
                if (clb_XNOs_XNM.CheckedItems.Count == 0) MessageBox.Show("Please select an XNO.", "No XNO specified", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (clb_XNMs.CheckedItems.Count == 0) MessageBox.Show("Please select an XNM.", "No XNM specified", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    try
                    {
                        string XNO = string.Empty;
                        string XNM = string.Empty;

                        //Gets all checked boxes from the CheckedListBox and builds a string for each XNO.
                        foreach (string selectedXNO in clb_XNOs_XNM.CheckedItems)
                        {
                            XNO = selectedXNO;

                            foreach (string selectedXNM in clb_XNMs.CheckedItems)
                            {
                                XNM = selectedXNM;
                            }
                        }

                        Tools.XNO.Animate(1, XNO, XNM);
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when converting the selected XNOs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Tools.Notification.Dispose();
                    }
                }
            }
        }

        void Clb_XNOs_SelectedIndexChanged(object sender, EventArgs e)
        {
            clb_XNOs.ClearSelected(); //Removes the blue highlight on recently checked boxes.

            //Enables/disables the Convert button, depending on whether a box has been checked.
            if (clb_XNOs.CheckedItems.Count > 0)
            {
                btn_Convert.Enabled = true;
            }
            else
            {
                btn_Convert.Enabled = false;
            }
        }

        void Check_XNM_CheckedChanged(object sender, EventArgs e)
        {
            if (check_XNM.Checked == true)
            {
                //Sets form to XNM Studio.

                #region Controls...
                Text = "XNM Studio";
                lbl_Title.Text = "XNM Studio";

                MinimumSize = new System.Drawing.Size(714, 458);

                Width = 714;
                if (WindowState != System.Windows.Forms.FormWindowState.Maximized)
                {
                    var moveLeft = Location.X - 142;
                    Location = new System.Drawing.Point(moveLeft, Location.Y);
                }

                //Unchecks all available checkboxes for the XNOs CheckedListBox.
                for (int i = 0; i < clb_XNOs.Items.Count; i++) clb_XNOs.SetItemChecked(i, false);
                btn_Convert.Enabled = false;

                split_XNMStudio.Visible = true;
                btn_SelectAll.Enabled = false;
                clb_XNOs.Visible = false;
                #endregion

                #region Getting XNOs...
                //Adds all XNOs in the current path to the CheckedListBox.
                foreach (string XNO in Directory.GetFiles(Tools.Global.currentPath, "*.xno", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(XNO))
                    {
                        clb_XNOs_XNM.Items.Add(Path.GetFileName(XNO));
                    }
                }
                //Checks if there are any XNOs in the directory.
                if (clb_XNOs.Items.Count == 0)
                {
                    MessageBox.Show("There are no XNOs to convert in this directory.", "No XNOs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                #endregion

                #region Getting XNMs...
                //Adds all XNMs in the current path to the CheckedListBox.
                foreach (string XNM in Directory.GetFiles(Tools.Global.currentPath, "*.xnm", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(XNM))
                    {
                        clb_XNMs.Items.Add(Path.GetFileName(XNM));
                    }
                }
                //Checks if there are any XNOs in the directory.
                if (clb_XNMs.Items.Count == 0)
                {
                    MessageBox.Show("There are no XNMs to convert in this directory.", "No XNMs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    check_XNM.Checked = false;
                }
                #endregion
            }
            else
            {
                //Resets form back to XNO Studio.

                #region Controls...
                Text = "XNO Studio";
                lbl_Title.Text = "XNO Studio";

                MinimumSize = new System.Drawing.Size(429, 458);

                Width = 429;
                if (WindowState != System.Windows.Forms.FormWindowState.Maximized)
                {
                    var moveRight = Location.X + 142;
                    Location = new System.Drawing.Point(moveRight, Location.Y);
                }

                //Unchecks all available checkboxes.
                for (int i = 0; i < clb_XNOs_XNM.Items.Count; i++) clb_XNOs_XNM.SetItemChecked(i, false);
                btn_Convert.Enabled = false;

                //Unchecks all available checkboxes.
                for (int i = 0; i < clb_XNMs.Items.Count; i++) clb_XNMs.SetItemChecked(i, false);
                btn_Convert.Enabled = false;

                split_XNMStudio.Visible = false;
                btn_SelectAll.Enabled = true;
                clb_XNOs.Visible = true;

                clb_XNOs_XNM.Items.Clear();
                clb_XNMs.Items.Clear();
                #endregion
            }
        }

        void Clb_XNOs_XNM_SelectedIndexChanged(object sender, EventArgs e)
        {
            clb_XNOs_XNM.ClearSelected(); //Removes the blue highlight on recently checked boxes.
        }

        void Clb_XNMs_SelectedIndexChanged(object sender, EventArgs e)
        {
            clb_XNMs.ClearSelected(); //Removes the blue highlight on recently checked boxes.
        }

        void Clb_XNOs_XNM_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //Limits the CheckedListBox to only one selectable item.
            if (clb_XNOs_XNM.CheckedItems.Count == 1 && e.NewValue == CheckState.Checked)
            {
                e.NewValue = CheckState.Unchecked;
            }
        }

        void Clb_XNMs_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //Limits the CheckedListBox to only one selectable item.
            if (clb_XNMs.CheckedItems.Count == 1 && e.NewValue == CheckState.Checked)
            {
                e.NewValue = CheckState.Unchecked;
            }
        }

        void Tm_ItemCheck_Tick(object sender, EventArgs e)
        {
            if (check_XNM.Checked == true)
            {
                //Enables/disables the Convert button, depending on whether a box has been checked.
                if (clb_XNOs_XNM.CheckedItems.Count > 0 && clb_XNMs.CheckedItems.Count > 0)
                {
                    btn_Convert.Enabled = true;
                }
                else
                {
                    btn_Convert.Enabled = false;
                }
            }
        }
    }
}
