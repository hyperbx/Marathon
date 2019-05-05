using System;
using System.IO;
using HedgeLib.Sets;
using System.Windows.Forms;

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
            if (modes_Export.Checked == true)
            {
                modes_Import.Checked = false;

                Global.setState = "export";
                btn_Convert.Text = "Export";

                clb_SETs.Items.Clear();

                #region Getting SETs to unpack...
                foreach (string SET in Directory.GetFiles(Global.currentPath, "*.set", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(SET))
                    {
                        clb_SETs.Items.Add(Path.GetFileName(SET));
                    }
                }

                //Checks if there are any CSBs in the directory.
                if (clb_SETs.Items.Count == 0)
                {
                    MessageBox.Show("There are no SETs to unpack in this directory.", "No SETs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                #endregion
            }
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
            //In the odd chance that someone is ever able to click Extract without anything selected, this will prevent that.
            if (clb_SETs.CheckedItems.Count == 0) MessageBox.Show("Please select a file.", "No files specified", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (Global.setState == "export")
            {
                try
                {
                    foreach (string selectedSET in clb_SETs.CheckedItems)
                    {
                        if (File.Exists(Global.currentPath + selectedSET))
                        {
                            var readSET = new S06SetData();
                            readSET.Load(Global.currentPath + selectedSET);
                            readSET.ExportXML(Global.currentPath + Path.GetFileNameWithoutExtension(selectedSET) + ".xml");
                        }
                    }
                }
                catch { MessageBox.Show("An error occurred when converting the SETs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else if (Global.setState == "import")
            {
                try
                {
                    foreach (string selectedXML in clb_SETs.CheckedItems)
                    {
                        if (File.Exists(Global.currentPath + selectedXML))
                        {
                            if (options_CreateBackupSET.Checked == true) if (File.Exists(Global.currentPath + Path.GetFileNameWithoutExtension(selectedXML) + ".set")) File.Copy(Global.currentPath + Path.GetFileNameWithoutExtension(selectedXML) + ".set", Global.currentPath + Path.GetFileNameWithoutExtension(selectedXML) + ".set.bak", true);

                            if (File.Exists(Global.currentPath + Path.GetFileNameWithoutExtension(selectedXML) + ".set")) File.Delete(Global.currentPath + Path.GetFileNameWithoutExtension(selectedXML) + ".set");

                            var readXML = new S06SetData();
                            readXML.ImportXML(Global.currentPath + selectedXML);
                            readXML.Save(Global.currentPath + Path.GetFileNameWithoutExtension(selectedXML) + ".set");

                            if (options_DeleteXML.Checked == true) if (File.Exists(Global.currentPath + selectedXML)) File.Delete(Global.currentPath + selectedXML);
                        }
                    }
                }
                catch { MessageBox.Show("An error occurred when importing the XMLs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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

                Global.setState = "export";
                btn_Convert.Text = "Export";

                clb_SETs.Items.Clear();

                #region Getting SETs to unpack...
                foreach (string SET in Directory.GetFiles(Global.currentPath, "*.set", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(SET))
                    {
                        clb_SETs.Items.Add(Path.GetFileName(SET));
                    }
                }

                //Checks if there are any CSBs in the directory.
                if (clb_SETs.Items.Count == 0)
                {
                    MessageBox.Show("There are no SETs to unpack in this directory.", "No SETs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                #endregion
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

                Global.setState = "import";
                btn_Convert.Text = "Import";

                clb_SETs.Items.Clear();

                #region Getting SETs to unpack...
                foreach (string XML in Directory.GetFiles(Global.currentPath, "*.xml", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(XML))
                    {
                        clb_SETs.Items.Add(Path.GetFileName(XML));
                    }
                }

                //Checks if there are any CSBs in the directory.
                if (clb_SETs.Items.Count == 0)
                {
                    MessageBox.Show("There are no XMLs to unpack in this directory.", "No XMLs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    modes_Import.Checked = false;
                    modes_Export.Checked = true;
                }
                #endregion
            }
        }
    }
}
