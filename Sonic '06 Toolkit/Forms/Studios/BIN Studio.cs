using System;
using System.IO;
using System.Windows.Forms;

namespace Sonic_06_Toolkit
{
    public partial class BIN_Studio : Form
    {
        public BIN_Studio()
        {
            InitializeComponent();
        }

        private void Combo_Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_Mode.SelectedIndex == 0)
            {
                if (!Tools.Prerequisites.PythonCheck())
                {
                    MessageBox.Show("Python is required to export BIN files. Please install Python and restart Sonic '06 Toolkit.", "Python Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    combo_Mode.SelectedIndex = 1;
                }
                else
                {
                    btn_Convert.Text = "Export";

                    clb_BIN.Items.Clear();

                    foreach (string BIN in Directory.GetFiles(Tools.Global.currentPath, "*.bin", SearchOption.TopDirectoryOnly))
                    {
                        if (File.Exists(BIN))
                        {
                            clb_BIN.Items.Add(Path.GetFileName(BIN));
                        }
                    }

                    //Checks if there are any CSBs in the directory.
                    if (clb_BIN.Items.Count == 0)
                    {
                        MessageBox.Show("There are no BIN files to export in this directory.", "No BIN files available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                }
            }
            else if (combo_Mode.SelectedIndex == 1)
            {
                btn_Convert.Text = "Import";

                clb_BIN.Items.Clear();

                foreach (string FBX in Directory.GetFiles(Tools.Global.currentPath, "*.fbx", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(FBX))
                    {
                        clb_BIN.Items.Add(Path.GetFileName(FBX));
                    }
                }

                foreach (string OBJ in Directory.GetFiles(Tools.Global.currentPath, "*.obj", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(OBJ))
                    {
                        clb_BIN.Items.Add(Path.GetFileName(OBJ));
                    }
                }

                //Checks if there are any CSBs in the directory.
                if (clb_BIN.Items.Count == 0)
                {
                    if (!Tools.Prerequisites.PythonCheck())
                    {
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("There are no collision files to import in this directory.", "No collision files available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        combo_Mode.SelectedIndex = 0;
                    }
                }
            }
        }

        private void BIN_Studio_Load(object sender, EventArgs e)
        {
            btn_Convert.Text = "Export";

            clb_BIN.Items.Clear();

            if (Directory.GetFiles(Tools.Global.currentPath, "*.bin").Length > 0)
            {
                combo_Mode.SelectedIndex = 0;
            }
            else if (Directory.GetFiles(Tools.Global.currentPath, "*.fbx").Length > 0)
            {
                combo_Mode.SelectedIndex = 1;
            }
            else if (Directory.GetFiles(Tools.Global.currentPath, "*.obj").Length > 0)
            {
                combo_Mode.SelectedIndex = 1;
            }
            else { MessageBox.Show("There are no convertable files in this directory.", "No files available", MessageBoxButtons.OK, MessageBoxIcon.Information); Close(); }
        }

        private void Btn_SelectAll_Click(object sender, EventArgs e)
        {
            //Checks all available checkboxes.
            for (int i = 0; i < clb_BIN.Items.Count; i++) clb_BIN.SetItemChecked(i, true);
            btn_Convert.Enabled = true;
        }

        private void Btn_DeselectAll_Click(object sender, EventArgs e)
        {
            //Unchecks all available checkboxes.
            for (int i = 0; i < clb_BIN.Items.Count; i++) clb_BIN.SetItemChecked(i, false);
            btn_Convert.Enabled = false;
        }

        private void Clb_BIN_SelectedIndexChanged(object sender, EventArgs e)
        {
            clb_BIN.ClearSelected(); //Removes the blue highlight on recently checked boxes.

            //Enables/disables the Convert button, depending on whether a box has been checked.
            if (clb_BIN.CheckedItems.Count > 0)
            {
                btn_Convert.Enabled = true;
            }
            else
            {
                btn_Convert.Enabled = false;
            }
        }

        private void Btn_Convert_Click(object sender, EventArgs e)
        {
            //In the odd chance that someone is ever able to click Export without anything selected, this will prevent that.
            if (clb_BIN.CheckedItems.Count == 0) MessageBox.Show("Please select a file.", "No files specified", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (combo_Mode.SelectedIndex == 0)
            {
                try
                {
                    foreach (string selectedBIN in clb_BIN.CheckedItems)
                    {
                        Tools.BIN.Export(1, string.Empty, selectedBIN);
                    }
                    //if (Properties.Settings.Default.disableWarns == false) { MessageBox.Show("All selected BIN files have been exported.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred when exporting the BIN files.\n\n{ex}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Tools.Notification.Dispose();
                }
            }
            else if (combo_Mode.SelectedIndex == 1)
            {
                try
                {
                    foreach (string selectedCol in clb_BIN.CheckedItems)
                    {
                        Tools.BIN.Import(3, string.Empty, selectedCol);
                    }
                    //if (Properties.Settings.Default.disableWarns == false) { MessageBox.Show("All selected collision files have been imported.", "Import Complete", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred when importing the collision files.\n\n{ex}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Tools.Notification.Dispose();
                }
            }
        }
    }
}
