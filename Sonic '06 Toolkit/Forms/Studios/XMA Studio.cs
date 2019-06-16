using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Sonic_06_Toolkit
{
    public partial class XMA_Studio : Form
    {
        public XMA_Studio()
        {
            InitializeComponent();
        }

        public static string wholeLoop = "/L";

        void Modes_XMAtoWAV_CheckedChanged(object sender, EventArgs e)
        {
            if (modes_XMAtoWAV.Checked == true)
            {
                Tools.Global.xmaState = "xma";

                clb_XMA.Items.Clear();

                #region Getting XMAs to convert...
                foreach (string XMA in Directory.GetFiles(Tools.Global.currentPath, "*.xma", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(XMA))
                    {
                        clb_XMA.Items.Add(Path.GetFileName(XMA));
                    }
                }
                #endregion

                modes_WAVtoXMA.Checked = false;
                options_Looping.Visible = false;
                //options_PatchXMA.Visible = false;
                btn_Convert.Enabled = false;

                if (Directory.GetFiles(Tools.Global.currentPath, "*.xma").Length == 0)
                {
                    MessageBox.Show("There are no XMAs to encode in this directory.", "No XMAs available", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Directory.GetFiles(Tools.Global.currentPath, "*.wav").Length == 0)
                    {
                        Close();
                    }
                    else
                    {
                        modes_XMAtoWAV.Checked = false;
                        modes_WAVtoXMA.Checked = true;
                    }
                }
            }
        }

        void Modes_WAVtoXMA_CheckedChanged(object sender, EventArgs e)
        {
            if (modes_WAVtoXMA.Checked == true)
            {
                Tools.Global.xmaState = "wav";

                clb_XMA.Items.Clear();

                #region Getting WAV files to convert...
                foreach (string WAV in Directory.GetFiles(Tools.Global.currentPath, "*.wav", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(WAV))
                    {
                        clb_XMA.Items.Add(Path.GetFileName(WAV));
                    }
                }
                #endregion

                modes_XMAtoWAV.Checked = false;
                //options_Looping.Visible = true;
                options_PatchXMA.Visible = true;
                btn_Convert.Enabled = false;

                if (Directory.GetFiles(Tools.Global.currentPath, "*.wav").Length == 0)
                {
                    MessageBox.Show("There are no WAV files to encode in this directory.", "No WAV files available", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Directory.GetFiles(Tools.Global.currentPath, "*.xma").Length == 0)
                    {
                        Close();
                    }
                    else
                    {
                        modes_XMAtoWAV.Checked = true;
                        modes_WAVtoXMA.Checked = false;
                    }
                }
            }
        }

        void XMA_Studio_Load(object sender, EventArgs e)
        {
            Tools.Global.xmaState = "xma";

            clb_XMA.Items.Clear();

            if (Directory.GetFiles(Tools.Global.currentPath, "*.xma").Length > 0)
            {
                modes_XMAtoWAV.Checked = true;
                modes_WAVtoXMA.Checked = false;
                //options_Looping.Visible = false;
            }
            else if (Directory.GetFiles(Tools.Global.currentPath, "*.wav").Length > 0)
            {
                modes_XMAtoWAV.Checked = false;
                modes_WAVtoXMA.Checked = true;
                //options_Looping.Visible = true;
            }
            else { MessageBox.Show("There are no encodable files in this directory.", "No files available", MessageBoxButtons.OK, MessageBoxIcon.Information); Close(); }

            if (Properties.Settings.Default.XMAwholeLoop == true) looping_Whole.Checked = true; else looping_Whole.Checked = false;
            if (Properties.Settings.Default.patchXMA == true) options_PatchXMA.Checked = true; else options_PatchXMA.Checked = false;
        }

        void Clb_XMA_SelectedIndexChanged(object sender, EventArgs e)
        {
            clb_XMA.ClearSelected(); //Removes the blue highlight on recently checked boxes.

            //Enables/disables the Decompile button, depending on whether a box has been checked.
            if (clb_XMA.CheckedItems.Count > 0)
            {
                btn_Convert.Enabled = true;
            }
            else
            {
                btn_Convert.Enabled = false;
            }
        }

        void XMA_Studio_FormClosing(object sender, FormClosingEventArgs e)
        {
            Tools.Global.xmaState = null;
        }

        void Btn_SelectAll_Click(object sender, EventArgs e)
        {
            //Checks all available checkboxes.
            for (int i = 0; i < clb_XMA.Items.Count; i++) clb_XMA.SetItemChecked(i, true);
            btn_Convert.Enabled = true;
        }

        void Btn_DeselectAll_Click(object sender, EventArgs e)
        {
            //Unchecks all available checkboxes.
            for (int i = 0; i < clb_XMA.Items.Count; i++) clb_XMA.SetItemChecked(i, false);
            btn_Convert.Enabled = false;
        }

        void Btn_Convert_Click(object sender, EventArgs e)
        {
            //In the odd chance that someone is ever able to click Extract without anything selected, this will prevent that.
            if (clb_XMA.CheckedItems.Count == 0) MessageBox.Show("Please select a file.", "No files specified", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (Tools.Global.xmaState == "xma")
            {
                try
                {
                    //Gets all checked boxes from the CheckedListBox and builds a string for each AT3.
                    foreach (string selectedXMA in clb_XMA.CheckedItems)
                    {
                        Tools.Global.xmaState = "xma";
                        Tools.XMA.ConvertToWAV(string.Empty, selectedXMA);
                    }
                }
                catch
                {
                    MessageBox.Show("An error occurred when encoding the selected XMAs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Tools.Notification.Dispose();
                }
            }
            else if (Tools.Global.xmaState == "wav")
            {
                try
                {
                    //Gets all checked boxes from the CheckedListBox and builds a string for each WAV.
                    foreach (string selectedWAV in clb_XMA.CheckedItems)
                    {
                        Tools.Global.xmaState = "wav";
                        Tools.XMA.ConvertToXMA(selectedWAV);
                    }
                }
                catch
                {
                    MessageBox.Show("An error occurred when encoding the selected WAV files.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Tools.Notification.Dispose();
                }
            }
            else
            {
                MessageBox.Show("XMA State set to invalid value: " + Tools.Global.xmaState + "\nLine information: " + new System.Diagnostics.StackTrace(true).GetFrame(1).GetFileLineNumber(), "Developer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void Options_PatchXMA_CheckedChanged(object sender, EventArgs e)
        {
            if (options_PatchXMA.Checked == true) Properties.Settings.Default.patchXMA = true;
            else Properties.Settings.Default.patchXMA = false;
            Properties.Settings.Default.Save();
        }

        void Looping_Whole_CheckedChanged(object sender, EventArgs e)
        {
            if (looping_Whole.Checked == true)
            {
                wholeLoop = "/L";
                Properties.Settings.Default.XMAwholeLoop = true;
            }
            else
            {
                wholeLoop = "/LR";
                Properties.Settings.Default.XMAwholeLoop = false;
            }
            Properties.Settings.Default.Save();
        }
    }
}
