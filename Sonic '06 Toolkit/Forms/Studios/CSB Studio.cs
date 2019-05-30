using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace Sonic_06_Toolkit
{
    public partial class CSB_Studio : Form
    {
        public CSB_Studio()
        {
            InitializeComponent();
        }

        void ADX_Studio_Load(object sender, System.EventArgs e)
        {
            combo_Mode.SelectedIndex = 0;
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
            if (Tools.Global.csbState == "unpack")
            {
                try
                {
                    //Gets all checked boxes from the CheckedListBox and builds a string for each CSB.
                    foreach (string selectedCSB in clb_CSBs.CheckedItems)
                    {
                        Tools.Global.csbState = "unpack";
                        Tools.CSB.Packer(null, selectedCSB);
                    }
                }
                catch
                {
                    MessageBox.Show("An error occurred when unpacking the selected CSBs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Tools.Notification.Dispose();
                }
            }
            else if (Tools.Global.csbState == "repack")
            {
                try
                {
                    //Gets all checked boxes from the CheckedListBox and builds a string for each CSB.
                    foreach (string selectedCSB in clb_CSBs.CheckedItems)
                    {
                        Tools.Global.csbState = "repack";
                        Tools.CSB.Packer(null, selectedCSB);
                    }
                }
                catch
                {
                    MessageBox.Show("An error occurred when repacking the selected CSBs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Tools.Notification.Dispose();
                }
            }
            else
            {
                MessageBox.Show("CSB State set to invalid value: " + Tools.Global.csbState + "\nLine information: " + new System.Diagnostics.StackTrace(true).GetFrame(1).GetFileLineNumber(), "Developer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void Combo_Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_Mode.SelectedIndex == 0)
            {
                Tools.Global.csbState = "unpack";
                btn_Extract.Text = "Unpack";

                clb_CSBs.Items.Clear();

                #region Getting CSBs to unpack...
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
                    MessageBox.Show("There are no CSBs to unpack in this directory.", "No CSBs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                #endregion
            }
            else if (combo_Mode.SelectedIndex == 1)
            {
                Tools.Global.csbState = "repack";
                btn_Extract.Text = "Repack";

                clb_CSBs.Items.Clear();

                #region Getting CSBs to repack...
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
                    MessageBox.Show("There are no CSBs to repack in this directory.", "No CSBs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    combo_Mode.SelectedIndex = 0;
                }
                #endregion
            }
        }

        void CSB_Studio_FormClosing(object sender, FormClosingEventArgs e)
        {
            Tools.Global.csbState = null;
        }
    }
}
