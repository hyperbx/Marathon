using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Sonic_06_Toolkit
{
    public partial class AT3_Studio : Form
    {
        public AT3_Studio()
        {
            InitializeComponent();
        }

        public static string wholeLoop = "-wholeloop ";
        public static string beginLoop = "";
        public static string startLoop = "";
        public static string endLoop = "";

        void AT3_Studio_Load(object sender, EventArgs e)
        {
            Global.at3State = "at3";

            clb_AT3.Items.Clear();

            if (Directory.GetFiles(Global.currentPath, "*.at3").Length > 0)
            {
                modes_AT3toWAV.Checked = true;
                modes_WAVtoAT3.Checked = false;
                options_Looping.Visible = false;
            }
            else if (Directory.GetFiles(Global.currentPath, "*.wav").Length > 0)
            {
                modes_AT3toWAV.Checked = false;
                modes_WAVtoAT3.Checked = true;
                options_Looping.Visible = true;
            }
            else { MessageBox.Show("There are no encodable files in this directory.", "No files available", MessageBoxButtons.OK, MessageBoxIcon.Information); Close(); }

            tm_wholeLoopCheck.Start();

            if (Properties.Settings.Default.wholeLoop == true) looping_Whole.Checked = true;
        }

        void Btn_SelectAll_Click(object sender, EventArgs e)
        {
            //Checks all available checkboxes.
            for (int i = 0; i < clb_AT3.Items.Count; i++) clb_AT3.SetItemChecked(i, true);
            btn_Convert.Enabled = true;
        }

        void Btn_DeselectAll_Click(object sender, EventArgs e)
        {
            //Unchecks all available checkboxes.
            for (int i = 0; i < clb_AT3.Items.Count; i++) clb_AT3.SetItemChecked(i, false);
            btn_Convert.Enabled = false;
        }

        void Modes_AT3toWAV_CheckedChanged(object sender, EventArgs e)
        {
            if (modes_AT3toWAV.Checked == true)
            {
                Global.at3State = "at3";

                clb_AT3.Items.Clear();

                #region Getting AT3 files to convert...
                foreach (string AT3 in Directory.GetFiles(Global.currentPath, "*.at3", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(AT3))
                    {
                        clb_AT3.Items.Add(Path.GetFileName(AT3));
                    }
                }
                #endregion

                modes_WAVtoAT3.Checked = false;
                options_Looping.Visible = false;
                btn_Convert.Enabled = false;

                if (Directory.GetFiles(Global.currentPath, "*.at3").Length == 0)
                {
                    MessageBox.Show("There are no AT3 files to encode in this directory.", "No AT3 files available", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Directory.GetFiles(Global.currentPath, "*.wav").Length == 0)
                    {
                        Close();
                    }
                    else
                    {
                        modes_AT3toWAV.Checked = false;
                        modes_WAVtoAT3.Checked = true;
                    }
                }
            }
        }

        void Modes_WAVtoAT3_CheckedChanged(object sender, EventArgs e)
        {
            if (modes_WAVtoAT3.Checked == true)
            {
                Global.at3State = "wav";

                clb_AT3.Items.Clear();

                #region Getting WAV files to convert...
                foreach (string WAV in Directory.GetFiles(Global.currentPath, "*.wav", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(WAV))
                    {
                        clb_AT3.Items.Add(Path.GetFileName(WAV));
                    }
                }
                #endregion

                modes_AT3toWAV.Checked = false;
                options_Looping.Visible = true;
                btn_Convert.Enabled = false;

                if (Directory.GetFiles(Global.currentPath, "*.wav").Length == 0)
                {
                    MessageBox.Show("There are no WAV files to encode in this directory.", "No WAV files available", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (Directory.GetFiles(Global.currentPath, "*.at3").Length == 0)
                    {
                        Close();
                    }
                    else
                    {
                        modes_AT3toWAV.Checked = true;
                        modes_WAVtoAT3.Checked = false;
                    }
                }
            }
        }

        void Clb_AT3_SelectedIndexChanged(object sender, EventArgs e)
        {
            clb_AT3.ClearSelected(); //Removes the blue highlight on recently checked boxes.

            //Enables/disables the Decompile button, depending on whether a box has been checked.
            if (clb_AT3.CheckedItems.Count > 0)
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
            if (clb_AT3.CheckedItems.Count == 0) MessageBox.Show("Please select a file.", "No files specified", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (Global.at3State == "at3")
            {
                try
                {
                    Global.at3State = "at3";

                    var convertDialog = new Status();
                    var parentLeft = Left + ((Width - convertDialog.Width) / 2);
                    var parentTop = Top + ((Height - convertDialog.Height) / 2);
                    convertDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                    convertDialog.Show();

                    #region Getting selected AT3 files...
                    //Gets all checked boxes from the CheckedListBox and builds a string for each AT3.
                    foreach (string selectedAT3 in clb_AT3.CheckedItems)
                    {
                        var checkedBuildSession = new StringBuilder();
                        checkedBuildSession.Append(Path.Combine(Global.currentPath, selectedAT3));

                        #region Converting ADX files...
                        //Sets up the BASIC application and executes the converting process.
                        var convertSession = new ProcessStartInfo(Properties.Settings.Default.at3File, "-d \"" + checkedBuildSession.ToString() + "\" \"" + Path.GetDirectoryName(checkedBuildSession.ToString()) + @"\" + Path.GetFileNameWithoutExtension(checkedBuildSession.ToString()) + ".wav\"");
                        convertSession.WorkingDirectory = Global.currentPath;
                        convertSession.WindowStyle = ProcessWindowStyle.Hidden;
                        var Convert = Process.Start(convertSession);
                        Convert.WaitForExit();
                        Convert.Close();

                        Global.at3State = null;
                        #endregion
                    }
                    #endregion

                    convertDialog.Close();
                }
                catch
                {
                    MessageBox.Show("An error occurred when encoding the selected AT3 files.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    escapeStatus();
                }
            }
            else if (Global.at3State == "wav")
            {
                try
                {
                    Global.at3State = "wav";

                    var convertDialog = new Status();
                    var parentLeft = Left + ((Width - convertDialog.Width) / 2);
                    var parentTop = Top + ((Height - convertDialog.Height) / 2);
                    convertDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                    convertDialog.Show();

                    #region Getting selected WAV files...
                    //Gets all checked boxes from the CheckedListBox and builds a string for each WAV.
                    foreach (string selectedWAV in clb_AT3.CheckedItems)
                    {
                        var checkedBuildSession = new StringBuilder();
                        checkedBuildSession.Append(Path.Combine(Global.currentPath, selectedWAV));

                        #region Converting WAV files...
                        //Sets up the BASIC application and executes the converting process.
                        var convertSession = new ProcessStartInfo(Properties.Settings.Default.at3File, "-e " + wholeLoop + beginLoop + startLoop + endLoop + "\"" + checkedBuildSession.ToString() + "\" \"" + Path.GetDirectoryName(checkedBuildSession.ToString()) + @"\" + Path.GetFileNameWithoutExtension(checkedBuildSession.ToString()) + ".at3\"");
                        convertSession.WorkingDirectory = Global.currentPath;
                        convertSession.WindowStyle = ProcessWindowStyle.Hidden;
                        var Convert = Process.Start(convertSession);
                        Convert.WaitForExit();
                        Convert.Close();

                        Global.at3State = null;
                        #endregion
                    }
                    #endregion

                    convertDialog.Close();
                }
                catch
                {
                    MessageBox.Show("An error occurred when encoding the selected WAV files.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    escapeStatus();
                }
            }
            else
            {
                MessageBox.Show("AT3 State set to invalid value: " + Global.adxState + "\nLine information: " + new System.Diagnostics.StackTrace(true).GetFrame(1).GetFileLineNumber(), "Developer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        void Looping_Whole_CheckedChanged(object sender, EventArgs e)
        {
            if (looping_Whole.Checked == true)
            {
                wholeLoop = "-wholeloop ";
                Properties.Settings.Default.wholeLoop = true;
            }
            else
            {
                wholeLoop = "";
                Properties.Settings.Default.wholeLoop = false;
            }
            Properties.Settings.Default.Save();
        }

        //Opens the unused looping form. This feature was removed because it doesn't work correctly.
        //void Looping_SetLoop_Click(object sender, EventArgs e)
        //{
        //    new Loop().ShowDialog();
        //}

        void Tm_wholeLoopCheck_Tick(object sender, EventArgs e)
        {
            if (wholeLoop != "") looping_Whole.Checked = true; else looping_Whole.Checked = false;
        }

        void AT3_Studio_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.at3State = null;
        }
    }
}
