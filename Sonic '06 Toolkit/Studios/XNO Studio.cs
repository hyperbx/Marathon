using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

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
            foreach (string XNO in Directory.GetFiles(Global.currentPath, "*.xno", SearchOption.TopDirectoryOnly))
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
            Global.xnoState = "xno";
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
            if (Global.xnoState == "xnm")
            {
                //Unchecks all available checkboxes.
                for (int i = 0; i < clb_XNOs.Items.Count; i++) clb_XNOs.SetItemChecked(i, false);
                btn_Convert.Enabled = false;
            }
            else if (Global.xnoState == "xnm")
            {
                //Unchecks all available checkboxes.
                for (int i = 0; i < clb_XNOs_XNM.Items.Count; i++) clb_XNOs_XNM.SetItemChecked(i, false);
                for (int i = 0; i < clb_XNMs.Items.Count; i++) clb_XNMs.SetItemChecked(i, false);
                btn_Convert.Enabled = false;
            }
        }

        void Btn_Decompile_Click(object sender, EventArgs e)
        {
            if (Global.xnoState == "xno")
            {
                try
                {
                    Global.xnoState = "xno";

                    var convertDialog = new Status();
                    var parentLeft = Left + ((Width - convertDialog.Width) / 2);
                    var parentTop = Top + ((Height - convertDialog.Height) / 2);
                    convertDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                    convertDialog.Show();

                    #region Getting current ARC failsafe...
                    //Gets the failsafe directory.
                    if (!Directory.Exists(Properties.Settings.Default.xnoPath + Global.sessionID)) Directory.CreateDirectory(Properties.Settings.Default.xnoPath + Global.sessionID);
                    var failsafeBuildSession = new StringBuilder();
                    failsafeBuildSession.Append(Properties.Settings.Default.archivesPath);
                    failsafeBuildSession.Append(Global.sessionID);
                    failsafeBuildSession.Append(@"\");
                    string failsafeCheck = File.ReadAllText(failsafeBuildSession.ToString() + Global.getIndex);
                    #endregion

                    #region Writing converter...
                    //Writes the decompiler to the failsafe directory to ensure any XNOs left over from other open archives aren't copied over to the selected archive.
                    if (!Directory.Exists(Properties.Settings.Default.xnoPath + Global.sessionID + @"\" + failsafeCheck)) Directory.CreateDirectory(Properties.Settings.Default.xnoPath + Global.sessionID + @"\" + failsafeCheck);
                    if (!File.Exists(Properties.Settings.Default.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xno2dae.exe")) File.WriteAllBytes(Properties.Settings.Default.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xno2dae.exe", Properties.Resources.xno2dae);
                    #endregion

                    #region Getting selected XNOs...
                    //Gets all checked boxes from the CheckedListBox and builds a string for each XNO.
                    foreach (string selectedXNO in clb_XNOs.CheckedItems)
                    {
                        #region Building XNOs...
                        //Gets the location of the converter and writes a BASIC application.
                        string convertPath = Path.Combine(Global.currentPath, selectedXNO);
                        var checkedBuildSession = new StringBuilder();
                        checkedBuildSession.Append(Properties.Settings.Default.xnoPath);
                        checkedBuildSession.Append(Global.sessionID);
                        checkedBuildSession.Append(@"\");
                        checkedBuildSession.Append(failsafeCheck);
                        checkedBuildSession.Append(@"\xno2dae.exe");
                        var checkedWrite = File.Create(Properties.Settings.Default.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xno2dae.bat");
                        var checkedText = new UTF8Encoding(true).GetBytes("\"" + checkedBuildSession.ToString() + "\" \"" + selectedXNO + "\"");
                        checkedWrite.Write(checkedText, 0, checkedText.Length);
                        checkedWrite.Close();
                        #endregion

                        #region Converting XNOs...
                        //Sets up the BASIC application and executes the conversion process.
                        var convertSession = new ProcessStartInfo(Properties.Settings.Default.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xno2dae.bat");
                        convertSession.WorkingDirectory = Global.currentPath;
                        convertSession.WindowStyle = ProcessWindowStyle.Hidden;
                        var Convert = Process.Start(convertSession);
                        Convert.WaitForExit();
                        Convert.Close();

                        Global.xnoState = null;
                        #endregion
                    }
                    #endregion

                    convertDialog.Close();
                }
                catch { MessageBox.Show("An error occurred when converting the selected XNOs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else if (Global.xnoState == "xnm")
            {
                //In the odd chance that someone is ever able to click Convert without anything selected, this will prevent that.
                if (clb_XNOs_XNM.CheckedItems.Count == 0) MessageBox.Show("Please select an XNO.", "No XNO specified", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (clb_XNMs.CheckedItems.Count == 0) MessageBox.Show("Please select an XNM.", "No XNM specified", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    try
                    {
                        Global.xnoState = "xnm";

                        var convertDialog = new Status();
                        var parentLeft = Left + ((Width - convertDialog.Width) / 2);
                        var parentTop = Top + ((Height - convertDialog.Height) / 2);
                        convertDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                        convertDialog.Show();

                        #region Getting current ARC failsafe...
                        //Gets the failsafe directory.
                        if (!Directory.Exists(Properties.Settings.Default.unlubPath + Global.sessionID)) Directory.CreateDirectory(Properties.Settings.Default.unlubPath + Global.sessionID);
                        var failsafeBuildSession = new StringBuilder();
                        failsafeBuildSession.Append(Properties.Settings.Default.archivesPath);
                        failsafeBuildSession.Append(Global.sessionID);
                        failsafeBuildSession.Append(@"\");
                        string failsafeCheck = File.ReadAllText(failsafeBuildSession.ToString() + Global.getIndex);
                        #endregion

                        #region Writing converter...
                        //Writes the decompiler to the failsafe directory to ensure any XNOs left over from other open archives aren't copied over to the selected archive.
                        if (!Directory.Exists(Properties.Settings.Default.xnoPath + Global.sessionID + @"\" + failsafeCheck)) Directory.CreateDirectory(Properties.Settings.Default.xnoPath + Global.sessionID + @"\" + failsafeCheck);
                        if (!Directory.Exists(Properties.Settings.Default.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xnos")) Directory.CreateDirectory(Properties.Settings.Default.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xnos");
                        if (!File.Exists(Properties.Settings.Default.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xno2dae.exe")) File.WriteAllBytes(Properties.Settings.Default.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xno2dae.exe", Properties.Resources.xno2dae);
                        #endregion

                        #region Pairing XNO with XNM...
                        //Gets all checked boxes from the CheckedListBox and builds a string for each XNO.
                        foreach (string selectedXNO in clb_XNOs_XNM.CheckedItems)
                        {
                            #region Building XNO...
                            //Gets the location of the converter and writes a BASIC application.
                            string XNOconvertPath = Path.Combine(Global.currentPath, selectedXNO);
                            var checkedBuildSession = new StringBuilder();
                            checkedBuildSession.Append(Properties.Settings.Default.xnoPath);
                            checkedBuildSession.Append(Global.sessionID);
                            checkedBuildSession.Append(@"\");
                            checkedBuildSession.Append(failsafeCheck);
                            checkedBuildSession.Append(@"\xno2dae.exe");
                            #endregion

                            foreach (string selectedXNM in clb_XNMs.CheckedItems)
                            {
                                #region Applying XNM...
                                //Gets the location of the converter and writes a BASIC application.
                                string XNMconvertPath = Path.Combine(Global.currentPath, selectedXNM);
                                var XNMcheckedWrite = File.Create(Properties.Settings.Default.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xno2dae.bat");
                                var XNMcheckedText = new UTF8Encoding(true).GetBytes("\"" + checkedBuildSession.ToString() + "\" \"" + selectedXNO + "\" \"" + selectedXNM + "\"");
                                XNMcheckedWrite.Write(XNMcheckedText, 0, XNMcheckedText.Length);
                                XNMcheckedWrite.Close();
                                #endregion
                            }
                        }

                        #region Converting XNOs...
                        //Sets up the BASIC application and executes the conversion process.
                        var convertSession = new ProcessStartInfo(Properties.Settings.Default.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xno2dae.bat");
                        convertSession.WorkingDirectory = Global.currentPath;
                        convertSession.WindowStyle = ProcessWindowStyle.Hidden;
                        var Convert = Process.Start(convertSession);
                        Convert.WaitForExit();
                        Convert.Close();

                        Global.xnoState = null;
                        #endregion

                        convertDialog.Close();
                        #endregion
                    }
                    catch { MessageBox.Show("An error occurred when converting the selected XNOs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            else
            {
                MessageBox.Show("XNO State set to invalid value: " + Global.xnoState + "\nLine information: " + new System.Diagnostics.StackTrace(true).GetFrame(1).GetFileLineNumber(), "Developer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                Global.xnoState = "xnm";
                #endregion

                #region Getting XNOs...
                //Adds all XNOs in the current path to the CheckedListBox.
                foreach (string XNO in Directory.GetFiles(Global.currentPath, "*.xno", SearchOption.TopDirectoryOnly))
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
                foreach (string XNM in Directory.GetFiles(Global.currentPath, "*.xnm", SearchOption.TopDirectoryOnly))
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

                Global.xnoState = "xno";

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
