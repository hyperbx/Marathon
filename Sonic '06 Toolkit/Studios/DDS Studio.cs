using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

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
            Global.ddsState = "dds";

            clb_DDS.Items.Clear();

            if (Directory.GetFiles(Global.currentPath, "*.dds").Length > 0)
            {
                modes_DDStoPNG.Checked = true;
                modes_PNGtoDDS.Checked = false;
            }
            else if (Directory.GetFiles(Global.currentPath, "*.png").Length > 0)
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
                Global.ddsState = "dds";

                clb_DDS.Items.Clear();

                #region Getting DDS files to convert...
                foreach (string DDS in Directory.GetFiles(Global.currentPath, "*.dds", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(DDS))
                    {
                        clb_DDS.Items.Add(Path.GetFileName(DDS));
                    }
                }
                #endregion

                modes_PNGtoDDS.Checked = false;
                btn_Convert.Enabled = false;

                if (Directory.GetFiles(Global.currentPath, "*.dds").Length == 0)
                {
                    MessageBox.Show("There are no DDS files to convert in this directory.", "No DDS files available", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Directory.GetFiles(Global.currentPath, "*.png").Length == 0)
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
                Global.ddsState = "png";

                clb_DDS.Items.Clear();

                #region Getting PNG files to convert...
                foreach (string PNG in Directory.GetFiles(Global.currentPath, "*.png", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(PNG))
                    {
                        clb_DDS.Items.Add(Path.GetFileName(PNG));
                    }
                }
                #endregion

                modes_DDStoPNG.Checked = false;
                btn_Convert.Enabled = false;

                if (Directory.GetFiles(Global.currentPath, "*.png").Length == 0)
                {
                    MessageBox.Show("There are no PNG files to convert in this directory.", "No PNG files available", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Directory.GetFiles(Global.currentPath, "*.dds").Length == 0)
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
            if (Global.ddsState == "dds")
            {
                try
                {
                    Global.ddsState = "dds";

                    var convertDialog = new Status();
                    var parentLeft = Left + ((Width - convertDialog.Width) / 2);
                    var parentTop = Top + ((Height - convertDialog.Height) / 2);
                    convertDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                    convertDialog.Show();

                    #region Getting selected DDS files...
                    //Gets all checked boxes from the CheckedListBox and builds a string for each ADX.
                    foreach (string selectedDDS in clb_DDS.CheckedItems)
                    {
                        var checkedBuildSession = new StringBuilder();
                        checkedBuildSession.Append(Path.Combine(Global.currentPath, selectedDDS));

                        #region Converting DDS files...
                        //Sets up the BASIC application and executes the converting process.
                        var convertSession = new ProcessStartInfo(Properties.Settings.Default.directXFile, "\"" + checkedBuildSession.ToString() + "\" -ft PNG" + useGPU + " -singleproc" + forceDirectX10 + " -f R8G8B8A8_UNORM");
                        convertSession.WorkingDirectory = Global.currentPath;
                        convertSession.WindowStyle = ProcessWindowStyle.Hidden;
                        var Convert = Process.Start(convertSession);
                        Convert.WaitForExit();
                        Convert.Close();

                        Global.ddsState = null;
                        #endregion
                    }
                    #endregion

                    convertDialog.Close();
                }
                catch
                {
                    MessageBox.Show("An error occurred when converting the selected DDS files.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    escapeStatus();
                }
            }
            else if (Global.ddsState == "png")
            {
                try
                {
                    Global.ddsState = "png";

                    var convertDialog = new Status();
                    var parentLeft = Left + ((Width - convertDialog.Width) / 2);
                    var parentTop = Top + ((Height - convertDialog.Height) / 2);
                    convertDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                    convertDialog.Show();

                    #region Getting selected PNG files...
                    //Gets all checked boxes from the CheckedListBox and builds a string for each WAV.
                    foreach (string selectedPNG in clb_DDS.CheckedItems)
                    {
                        var checkedBuildSession = new StringBuilder();
                        checkedBuildSession.Append(Path.Combine(Global.currentPath, selectedPNG));

                        #region Converting PNG files...
                        //Sets up the BASIC application and executes the converting process.
                        var convertSession = new ProcessStartInfo(Properties.Settings.Default.directXFile, "\"" + checkedBuildSession.ToString() + "\" -ft DDS" + useGPU + " -singleproc" + forceDirectX10 + " -f R8G8B8A8_UNORM");
                        convertSession.WorkingDirectory = Global.currentPath;
                        convertSession.WindowStyle = ProcessWindowStyle.Hidden;
                        var Convert = Process.Start(convertSession);
                        Convert.WaitForExit();
                        Convert.Close();

                        Global.ddsState = null;
                        #endregion
                    }
                    #endregion

                    convertDialog.Close();
                }
                catch
                {
                    MessageBox.Show("An error occurred when converting the selected PNG files.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    escapeStatus();
                }
            }
            else
            {
                MessageBox.Show("DDS State set to invalid value: " + Global.ddsState + "\nLine information: " + new System.Diagnostics.StackTrace(true).GetFrame(1).GetFileLineNumber(), "Developer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void escapeStatus()
        {
            try
            {
                Status statusForm = (Status)Application.OpenForms["Status"];
                statusForm.Close();
            }
            catch { }
        }

        void DDS_Studio_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.ddsState = null;
        }
    }
}
