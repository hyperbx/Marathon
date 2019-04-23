using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Sonic_06_Toolkit
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        void newTab()
        {
            var nextTab = new TabPage();
            var nextBrowser = new WebBrowser();
            nextTab.Text = "New Tab";
            tab_Main.TabPages.Add(nextTab);
            nextTab.Controls.Add(nextBrowser);
            tab_Main.SelectedTab = nextTab;
            nextBrowser.Dock = DockStyle.Fill;
            currentARC().AllowWebBrowserDrop = false;
        }

        private WebBrowser currentARC()
        {
            return (WebBrowser)tab_Main.SelectedTab.Controls[0];
        }

        void File_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void Help_About_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

        void File_OpenARC_Click(object sender, EventArgs e)
        {
            if (ofd_OpenARC.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    #region Building unpack data...
                    string failsafeCheck = Path.GetRandomFileName(); //Unpacked ARCs will have a unique directory to prevent overwriting.
                    var unpackBuildSession = new StringBuilder();
                    unpackBuildSession.Append(Global.archivesPath);
                    unpackBuildSession.Append(Global.sessionID);
                    unpackBuildSession.Append(@"\");
                    unpackBuildSession.Append(failsafeCheck);
                    unpackBuildSession.Append(@"\");
                    unpackBuildSession.Append(Path.GetFileNameWithoutExtension(ofd_OpenARC.FileName));
                    unpackBuildSession.Append(@"\");
                    if (!Directory.Exists(unpackBuildSession.ToString())) Directory.CreateDirectory(unpackBuildSession.ToString());
                    var storageSession = new StringBuilder();
                    storageSession.Append(Global.archivesPath);
                    storageSession.Append(Global.sessionID);
                    storageSession.Append(@"\");
                    storageSession.Append(tab_Main.SelectedIndex);
                    var storageWrite = File.Create(storageSession.ToString());
                    var storageText = new UTF8Encoding(true).GetBytes(failsafeCheck);
                    storageWrite.Write(storageText, 0, storageText.Length);
                    storageWrite.Close();
                    #endregion

                    #region Building ARC data...
                    var arcBuildSession = new StringBuilder();
                    arcBuildSession.Append(Global.archivesPath);
                    arcBuildSession.Append(Global.sessionID);
                    arcBuildSession.Append(@"\");
                    arcBuildSession.Append(failsafeCheck);
                    arcBuildSession.Append(@"\");
                    if (!Directory.Exists(arcBuildSession.ToString())) Directory.CreateDirectory(arcBuildSession.ToString());
                    if (File.Exists(ofd_OpenARC.FileName)) File.Copy(ofd_OpenARC.FileName, arcBuildSession.ToString() + Path.GetFileName(ofd_OpenARC.FileName), true);
                    #endregion

                    #region Unpacking ARC...
                    var basicWrite = File.Create(Global.toolsPath + "unpack.bat");
                    var basicSession = new UTF8Encoding(true).GetBytes("\"" + Global.unpackFile + "\" \"" + arcBuildSession.ToString() + Path.GetFileName(ofd_OpenARC.FileName) + "\"");
                    basicWrite.Write(basicSession, 0, basicSession.Length);
                    basicWrite.Close();
                    var unpackSession = new ProcessStartInfo(Global.toolsPath + "unpack.bat");
                    unpackSession.WorkingDirectory = Global.toolsPath;
                    unpackSession.WindowStyle = ProcessWindowStyle.Hidden;
                    var Unpack = Process.Start(unpackSession);
                    var unpackDialog = new Unpacking();
                    var parentLeft = Left + ((Width - unpackDialog.Width) / 2);
                    var parentTop = Top + ((Height - unpackDialog.Height) / 2);
                    unpackDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                    unpackDialog.Show();
                    Unpack.WaitForExit();
                    Unpack.Close();
                    #endregion

                    #region Writing metadata...
                    var metadataWrite = File.Create(arcBuildSession.ToString() + "metadata.ini");
                    var metadataSession = new UTF8Encoding(true).GetBytes(ofd_OpenARC.FileName);
                    metadataWrite.Write(metadataSession, 0, metadataSession.Length);
                    metadataWrite.Close();
                    unpackDialog.Close();
                    #endregion

                    #region Navigating...
                    if (tab_Main.SelectedTab.Text == "New Tab")
                    {
                        currentARC().Navigate(unpackBuildSession.ToString());
                        tab_Main.SelectedTab.Text = Path.GetFileName(ofd_OpenARC.FileName);
                    }
                    else
                    {
                        newTab();
                        currentARC().Navigate(unpackBuildSession.ToString());
                        tab_Main.SelectedTab.Text = Path.GetFileName(ofd_OpenARC.FileName);
                    }
                    #endregion
                }
                catch
                {
                    MessageBox.Show("An error occurred when unpacking the archive.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void repackARC()
        {
            try
            {
                #region Building repack data...
                var repackBuildSession = new StringBuilder();
                repackBuildSession.Append(Global.archivesPath);
                repackBuildSession.Append(Global.sessionID);
                repackBuildSession.Append(@"\");
                string failsafeCheck = File.ReadAllText(repackBuildSession.ToString() + tab_Main.SelectedIndex);
                repackBuildSession.Append(@"\");
                repackBuildSession.Append(failsafeCheck);
                repackBuildSession.Append(@"\");
                string metadata = File.ReadAllText(repackBuildSession.ToString() + "metadata.ini");
                #endregion

                #region Repacking ARC...
                var basicWrite = File.Create(Global.toolsPath + "repack.bat");
                var basicSession = new UTF8Encoding(true).GetBytes("\"" + Global.repackFile + "\" \"" + repackBuildSession.ToString() + Path.GetFileNameWithoutExtension(metadata) + "\"");
                basicWrite.Write(basicSession, 0, basicSession.Length);
                basicWrite.Close();
                var repackSession = new ProcessStartInfo(Global.toolsPath + "repack.bat");
                repackSession.WorkingDirectory = Global.toolsPath;
                repackSession.WindowStyle = ProcessWindowStyle.Hidden;
                var Repack = Process.Start(repackSession);
                var repackDialog = new Repacking();
                var parentLeft = Left + ((Width - repackDialog.Width) / 2);
                var parentTop = Top + ((Height - repackDialog.Height) / 2);
                repackDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                repackDialog.Show();
                Repack.WaitForExit();
                Repack.Close();
                if (File.Exists(ofd_OpenARC.FileName)) File.Copy(repackBuildSession.ToString() + Path.GetFileName(metadata), metadata, true);
                repackDialog.Close();
                #endregion
            }
            catch
            {
                MessageBox.Show("An error occurred when repacking the archive.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void Btn_Repack_Click(object sender, EventArgs e)
        {
            repackARC();
        }

        void Main_Load(object sender, EventArgs e)
        {
            #region Session ID
            var generateSessionID = new Random();
            Global.sessionID = generateSessionID.Next(1, 99999); //Generates a random number between 1 to 99999 for a unique Session ID.
            btn_SessionID.Text = Global.sessionID.ToString();
            #endregion

            #region Directory Check
            try
            {
                //The below code checks if the directories in the Global class exist; if not, they will be created.
                if (!Directory.Exists(Global.tempPath)) Directory.CreateDirectory(Global.tempPath);
                if (!Directory.Exists(Global.archivesPath)) Directory.CreateDirectory(Global.archivesPath);
                if (!Directory.Exists(Global.toolsPath)) Directory.CreateDirectory(Global.toolsPath);
                if (!Directory.Exists(Global.unlubPath)) Directory.CreateDirectory(Global.unlubPath);
                if (!Directory.Exists(Global.xnoPath)) Directory.CreateDirectory(Global.xnoPath);
            }
            catch
            {
                MessageBox.Show("An error occurred when writing a directory.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            #endregion

            #region File Check
            try
            {
                //The below code checks if the files in the Global class exist; if not, they will be created.
                if (!File.Exists(Global.unpackFile)) File.WriteAllBytes(Global.unpackFile, Properties.Resources.unpack);
                if (!File.Exists(Global.repackFile)) File.WriteAllBytes(Global.repackFile, Properties.Resources.repack);
                if (!File.Exists(Global.xnoFile)) File.WriteAllBytes(Global.xnoFile, Properties.Resources.xno2dae);
            }
            catch
            {
                MessageBox.Show("An error occurred when writing a file.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            #endregion

            newTab();
            tm_tabCheck.Start();
        }

        void Btn_Back_Click(object sender, EventArgs e)
        {
            currentARC().GoBack();
        }

        void Btn_Forward_Click(object sender, EventArgs e)
        {
            currentARC().GoForward();
        }

        void Tabs_NewTab_Click(object sender, EventArgs e)
        {
            newTab();
        }

        void Tabs_CloseTab_Click(object sender, EventArgs e)
        {
            if (tab_Main.TabPages.Count < 1) ; else tab_Main.TabPages.Remove(tab_Main.SelectedTab);
        }

        void Tm_tabCheck_Tick(object sender, EventArgs e)
        {
            if (tab_Main.TabPages.Count == 1) tabs_CloseTab.Enabled = false; else tabs_CloseTab.Enabled = true;

            #region Check if tab is empty
            //Temporary solution.
            if (tab_Main.SelectedTab.Text != "New Tab")
            {
                btn_Back.Enabled = true;
                btn_Forward.Enabled = true;
                btn_Repack.Enabled = true;
                file_RepackARC.Enabled = true;
            }
            else
            {
                btn_Back.Enabled = false;
                btn_Forward.Enabled = false;
                btn_Repack.Enabled = false;
                file_RepackARC.Enabled = false;
            }
            #endregion
        }

        private void File_RepackARC_Click(object sender, EventArgs e)
        {
            repackARC();
        }
    }
}
