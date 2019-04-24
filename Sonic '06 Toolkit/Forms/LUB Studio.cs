using System;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace Sonic_06_Toolkit
{
    public partial class LUB_Studio : Form
    {
        public LUB_Studio()
        {
            InitializeComponent();
        }

        void LUB_Studio_Load(object sender, EventArgs e)
        {
            #region Verifying Lua binaries...
            foreach (string LUB in Directory.GetFiles(Global.currentPath, "*.lub", SearchOption.TopDirectoryOnly))
            {
                if (File.Exists(LUB))
                {
                    if (File.ReadAllLines(LUB)[0].Contains("LuaP"))
                    {
                        clb_LUBs.Items.Add(Path.GetFileName(LUB));
                    }
                }
            }
            if (clb_LUBs.Items.Count == 0)
            {
                MessageBox.Show("There are no Lua binaries to decompile in this directory.", "No Lua binaries available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            #endregion
        }

        void Btn_SelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clb_LUBs.Items.Count; i++) clb_LUBs.SetItemChecked(i, true);
        }

        void Btn_DeselectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clb_LUBs.Items.Count; i++) clb_LUBs.SetItemChecked(i, false);
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

                #region Writing decompiler...
                if (!Directory.Exists(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck)) Directory.CreateDirectory(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck);
                if (!Directory.Exists(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\lubs")) Directory.CreateDirectory(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\lubs");
                if (!File.Exists(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.jar")) File.WriteAllBytes(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.jar", Properties.Resources.unlub);
                if (!File.Exists(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.bat")) File.WriteAllBytes(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.bat", Properties.Resources.unlubBASIC);
                #endregion

                #region Getting selected Lua binaries...
                foreach (string selectedLUB in clb_LUBs.CheckedItems)
                {
                    var checkedBuildSession = new StringBuilder();
                    checkedBuildSession.Append(Path.Combine(Global.currentPath, selectedLUB));
                    File.Copy(checkedBuildSession.ToString(), Path.Combine(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\lubs\", Path.GetFileName(selectedLUB)), true);
                }
                #endregion

                #region Decompiling Lua binaries...
                var decompileSession = new ProcessStartInfo(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.bat");
                decompileSession.WorkingDirectory = Global.unlubPath + Global.sessionID + @"\" + failsafeCheck;
                decompileSession.WindowStyle = ProcessWindowStyle.Hidden;
                var Decompile = Process.Start(decompileSession);
                var decompileDialog = new Decompiling();
                var parentLeft = Left + ((Width - decompileDialog.Width) / 2);
                var parentTop = Top + ((Height - decompileDialog.Height) / 2);
                decompileDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                decompileDialog.Show();
                Decompile.WaitForExit();
                Decompile.Close();
                #endregion

                #region Moving decompiled Lua binaries...
                foreach (string LUB in Directory.GetFiles(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\luas\", "*.lub", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(LUB))
                    {
                        File.Copy(Path.Combine(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\luas\", Path.GetFileName(LUB)), Path.Combine(Global.currentPath, Path.GetFileName(LUB)), true);
                        File.Delete(LUB);
                    }
                }
                decompileDialog.Close();
                #endregion
            }
            catch { MessageBox.Show("An error occurred when decompiling the selected Lua binaries.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        void Clb_LUBs_SelectedIndexChanged(object sender, EventArgs e)
        {
            clb_LUBs.ClearSelected();
        }
    }
}
