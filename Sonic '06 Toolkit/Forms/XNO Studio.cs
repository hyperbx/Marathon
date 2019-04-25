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
        }

        void Btn_SelectAll_Click(object sender, EventArgs e)
        {
            //Checks all available checkboxes.
            for (int i = 0; i < clb_XNOs.Items.Count; i++) clb_XNOs.SetItemChecked(i, true);
            btn_Decompile.Enabled = true;
        }

        void Btn_DeselectAll_Click(object sender, EventArgs e)
        {
            //Unchecks all available checkboxes.
            for (int i = 0; i < clb_XNOs.Items.Count; i++) clb_XNOs.SetItemChecked(i, false);
            btn_Decompile.Enabled = false;
        }

        void Btn_Decompile_Click(object sender, EventArgs e)
        {
            try
            {
                #region Getting current ARC failsafe...
                //Gets the failsafe directory.
                if (!Directory.Exists(Global.unlubPath + Global.sessionID)) Directory.CreateDirectory(Global.unlubPath + Global.sessionID);
                var failsafeBuildSession = new StringBuilder();
                failsafeBuildSession.Append(Global.archivesPath);
                failsafeBuildSession.Append(Global.sessionID);
                failsafeBuildSession.Append(@"\");
                string failsafeCheck = File.ReadAllText(failsafeBuildSession.ToString() + Global.getIndex);
                #endregion

                #region Writing converter...
                //Writes the decompiler to the failsafe directory to ensure any XNOs left over from other open archives aren't copied over to the selected archive.
                if (!Directory.Exists(Global.xnoPath + Global.sessionID + @"\" + failsafeCheck)) Directory.CreateDirectory(Global.xnoPath + Global.sessionID + @"\" + failsafeCheck);
                if (!Directory.Exists(Global.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xnos")) Directory.CreateDirectory(Global.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xnos");
                if (!File.Exists(Global.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xno2dae.exe")) File.WriteAllBytes(Global.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xno2dae.exe", Properties.Resources.xno2dae);
                #endregion

                #region Getting selected XNOs...
                //Gets all checked boxes from the CheckedListBox and builds a string for each XNO.
                foreach (string selectedXNO in clb_XNOs.CheckedItems)
                {
                    #region Building XNOs...
                    //Gets the location of the converter and writes a BASIC application.
                    string convertPath = Path.Combine(Global.currentPath, selectedXNO);
                    var checkedBuildSession = new StringBuilder();
                    checkedBuildSession.Append(Global.xnoPath);
                    checkedBuildSession.Append(Global.sessionID);
                    checkedBuildSession.Append(@"\");
                    checkedBuildSession.Append(failsafeCheck);
                    checkedBuildSession.Append(@"\xno2dae.exe");
                    var checkedWrite = File.Create(Global.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xno2dae.bat");
                    var checkedText = new UTF8Encoding(true).GetBytes("\"" + checkedBuildSession.ToString() + "\" \"" + selectedXNO + "\"");
                    checkedWrite.Write(checkedText, 0, checkedText.Length);
                    checkedWrite.Close();
                    #endregion

                    #region Converting XNOs...
                    //Sets up the BASIC application and executes the conversion process.
                    var convertSession = new ProcessStartInfo(Global.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xno2dae.bat");
                    convertSession.WorkingDirectory = Global.currentPath;
                    convertSession.WindowStyle = ProcessWindowStyle.Hidden;
                    var Convert = Process.Start(convertSession);
                    var convertDialog = new Converting();
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
            catch { MessageBox.Show("An error occurred when converting the selected XNOs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        void Clb_XNOs_SelectedIndexChanged(object sender, EventArgs e)
        {
            clb_XNOs.ClearSelected(); //Removes the blue highlight on recently checked boxes.

            //Enables/disables the Decompile button, depending on whether a box has been checked.
            if (clb_XNOs.CheckedItems.Count > 0)
            {
                btn_Decompile.Enabled = true;
            }
            else
            {
                btn_Decompile.Enabled = false;
            }
        }
    }
}
