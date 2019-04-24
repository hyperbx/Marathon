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
            #region Verifying XNOs...
            foreach (string XNO in Directory.GetFiles(Global.currentPath, "*.xno", SearchOption.TopDirectoryOnly))
            {
                if (File.Exists(XNO))
                {
                    clb_XNOs.Items.Add(Path.GetFileName(XNO));
                }
            }
            if (clb_XNOs.Items.Count == 0)
            {
                MessageBox.Show("There are no XNOs to convert in this directory.", "No XNOs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            #endregion
        }

        void Btn_SelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clb_XNOs.Items.Count; i++) clb_XNOs.SetItemChecked(i, true);
            btn_Decompile.Enabled = true;
        }

        void Btn_DeselectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clb_XNOs.Items.Count; i++) clb_XNOs.SetItemChecked(i, false);
            btn_Decompile.Enabled = false;
        }

        void Btn_Decompile_Click(object sender, EventArgs e)
        {
            try
            {
                #region Getting current ARC failsafe...
                if (!Directory.Exists(Global.unlubPath + Global.sessionID)) Directory.CreateDirectory(Global.unlubPath + Global.sessionID);
                var failsafeBuildSession = new StringBuilder();
                failsafeBuildSession.Append(Global.archivesPath);
                failsafeBuildSession.Append(Global.sessionID);
                failsafeBuildSession.Append(@"\");
                string failsafeCheck = File.ReadAllText(failsafeBuildSession.ToString() + Global.getIndex);
                #endregion

                #region Writing converter...
                if (!Directory.Exists(Global.xnoPath + Global.sessionID + @"\" + failsafeCheck)) Directory.CreateDirectory(Global.xnoPath + Global.sessionID + @"\" + failsafeCheck);
                if (!Directory.Exists(Global.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xnos")) Directory.CreateDirectory(Global.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xnos");
                if (!File.Exists(Global.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xno2dae.exe")) File.WriteAllBytes(Global.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xno2dae.exe", Properties.Resources.xno2dae);
                #endregion

                #region Getting selected XNOs...
                foreach (string selectedXNO in clb_XNOs.CheckedItems)
                {
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
                }
                #endregion

                #region Converting XNOs...
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
            catch { MessageBox.Show("An error occurred when converting the selected XNOs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        void Clb_XNOs_SelectedIndexChanged(object sender, EventArgs e)
        {
            clb_XNOs.ClearSelected();

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
