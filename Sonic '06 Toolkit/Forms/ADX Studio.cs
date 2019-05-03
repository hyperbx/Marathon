using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace Sonic_06_Toolkit
{
    public partial class ADX_Studio : Form
    {
        public ADX_Studio()
        {
            InitializeComponent();
        }

        void ADX_Studio_Load(object sender, EventArgs e)
        {
            combo_Mode.SelectedIndex = 0;
        }

        void Btn_SelectAll_Click(object sender, EventArgs e)
        {
            //Checks all available checkboxes.
            for (int i = 0; i < clb_ADX.Items.Count; i++) clb_ADX.SetItemChecked(i, true);
            btn_Convert.Enabled = true;
        }

        private void Btn_DeselectAll_Click(object sender, EventArgs e)
        {
            //Unchecks all available checkboxes.
            for (int i = 0; i < clb_ADX.Items.Count; i++) clb_ADX.SetItemChecked(i, false);
            btn_Convert.Enabled = false;
        }

        void Btn_Convert_Click(object sender, EventArgs e)
        {
            //In the odd chance that someone is ever able to click Extract without anything selected, this will prevent that.
            if (clb_ADX.CheckedItems.Count == 0) MessageBox.Show("Please select an ADX file.", "No ADX files specified", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (Global.csbState == "adx")
            {
                try
                {
                    #region Getting selected ADX files...
                    //Gets all checked boxes from the CheckedListBox and builds a string for each ADX.
                    foreach (string selectedADX in clb_ADX.CheckedItems)
                    {
                        var checkedBuildSession = new StringBuilder();
                        checkedBuildSession.Append(Path.Combine(Global.currentPath, selectedADX));

                        #region Converting ADX files...
                        //Sets up the BASIC application and executes the converting process.
                        var convertSession = new ProcessStartInfo(Properties.Settings.Default.adx2wavFile, "\"" + checkedBuildSession.ToString() + "\" \"" + Path.GetDirectoryName(checkedBuildSession.ToString()) + @"\" + Path.GetFileNameWithoutExtension(checkedBuildSession.ToString()) + ".wav\"");
                        convertSession.WorkingDirectory = Global.currentPath;
                        convertSession.WindowStyle = ProcessWindowStyle.Hidden;
                        var Convert = Process.Start(convertSession);
                        var convertDialog = new Converting_ADX();
                        var parentLeft = Left + ((Width - convertDialog.Width) / 2);
                        var parentTop = Top + ((Height - convertDialog.Height) / 2);
                        convertDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                        convertDialog.Show();
                        Convert.WaitForExit();
                        Convert.Close();
                        convertDialog.Close();
                        #endregion
                    }
                    #endregion
                }
                catch { MessageBox.Show("An error occurred when converting the selected ADX files.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else if (Global.csbState == "wav")
            {
                try
                {
                    #region Getting selected WAV files...
                    //Gets all checked boxes from the CheckedListBox and builds a string for each WAV.
                    foreach (string selectedWAV in clb_ADX.CheckedItems)
                    {
                        var checkedBuildSession = new StringBuilder();
                        checkedBuildSession.Append(Path.Combine(Global.currentPath, selectedWAV));

                        #region Converting WAV files...
                        //Sets up the BASIC application and executes the converting process.
                        var convertSession = new ProcessStartInfo(Properties.Settings.Default.criconverterFile, "\"" + checkedBuildSession.ToString() + "\" \"" + Path.GetDirectoryName(checkedBuildSession.ToString()) + @"\" + Path.GetFileNameWithoutExtension(checkedBuildSession.ToString()) + ".adx\" -codec=adx");
                        convertSession.WorkingDirectory = Global.currentPath;
                        convertSession.WindowStyle = ProcessWindowStyle.Hidden;
                        var Convert = Process.Start(convertSession);
                        var convertDialog = new Converting_WAV();
                        var parentLeft = Left + ((Width - convertDialog.Width) / 2);
                        var parentTop = Top + ((Height - convertDialog.Height) / 2);
                        convertDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                        convertDialog.Show();
                        Convert.WaitForExit();
                        Convert.Close();
                        convertDialog.Close();
                        #endregion
                    }
                    #endregion
                }
                catch { MessageBox.Show("An error occurred when converting the selected WAV files.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        void Combo_Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_Mode.SelectedIndex == 0)
            {
                Global.csbState = "adx";

                clb_ADX.Items.Clear();

                #region Getting ADX files to convert...
                foreach (string ADX in Directory.GetFiles(Global.currentPath, "*.adx", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(ADX))
                    {
                        clb_ADX.Items.Add(Path.GetFileName(ADX));
                    }
                }

                ////Checks if there are any ADX files in the directory.
                //if (clb_ADX.Items.Count == 0)
                //{
                //    MessageBox.Show("There are no ADX files to convert in this directory.", "No ADX files available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    combo_Mode.SelectedIndex = 1;
                //}
                #endregion
            }
            else if (combo_Mode.SelectedIndex == 1)
            {
                Global.csbState = "wav";

                clb_ADX.Items.Clear();

                #region Getting WAV files to convert...
                foreach (string WAV in Directory.GetFiles(Global.currentPath, "*.wav", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(WAV))
                    {
                        clb_ADX.Items.Add(Path.GetFileName(WAV));
                    }
                }

                ////Checks if there are any WAV files in the directory.
                //if (clb_ADX.Items.Count == 0)
                //{
                //    MessageBox.Show("There are no WAV files to convert in this directory.", "No WAV files available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    combo_Mode.SelectedIndex = 0;
                //}
                #endregion
            }
        }

        void Clb_ADX_SelectedIndexChanged(object sender, EventArgs e)
        {
            clb_ADX.ClearSelected(); //Removes the blue highlight on recently checked boxes.

            //Enables/disables the Decompile button, depending on whether a box has been checked.
            if (clb_ADX.CheckedItems.Count > 0)
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
