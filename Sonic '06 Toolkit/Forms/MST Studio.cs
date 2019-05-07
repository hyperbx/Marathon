using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Sonic_06_Toolkit
{
    public partial class MST_Studio : Form
    {
        public MST_Studio()
        {
            InitializeComponent();
        }

        void MST_Studio_Load(object sender, EventArgs e)
        {
            #region Getting MSTs...
            //Adds all MSTs in the current path to the CheckedListBox.
            foreach (string MST in Directory.GetFiles(Global.currentPath, "*.mst", SearchOption.TopDirectoryOnly))
            {
                if (File.Exists(MST))
                {
                    clb_MSTs.Items.Add(Path.GetFileName(MST));
                }
            }
            //Checks if there are any MSTs in the directory.
            if (clb_MSTs.Items.Count == 0)
            {
                MessageBox.Show("There are no MSTs to decode in this directory.", "No MSTs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            #endregion

            split_MSTStudio.Visible = false;
            tm_ItemCheck.Start();
        }

        void Btn_SelectAll_Click(object sender, EventArgs e)
        {
            //Checks all available checkboxes.
            for (int i = 0; i < clb_MSTs.Items.Count; i++) clb_MSTs.SetItemChecked(i, true);
            btn_Decode.Enabled = true;
        }

        void Btn_DeselectAll_Click(object sender, EventArgs e)
        {
            //Unchecks all available checkboxes.
            for (int i = 0; i < clb_MSTs.Items.Count; i++) clb_MSTs.SetItemChecked(i, false);
            btn_Decode.Enabled = false;
        }

        void Clb_MSTs_SelectedIndexChanged(object sender, EventArgs e)
        {
            clb_MSTs.ClearSelected(); //Removes the blue highlight on recently checked boxes.

            //Enables/disables the Convert button, depending on whether a box has been checked.
            if (clb_MSTs.CheckedItems.Count > 0)
            {
                btn_Decode.Enabled = true;
            }
            else
            {
                btn_Decode.Enabled = false;
            }
        }

        void Btn_Decode_Click(object sender, EventArgs e)
        {
            //MST Decoder will go here...
        }
    }
}
