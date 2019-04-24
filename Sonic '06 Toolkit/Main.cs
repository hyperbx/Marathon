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

                    #region Building location data...
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
                }
                catch { MessageBox.Show("An error occurred when unpacking the archive.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
            catch { MessageBox.Show("An error occurred when repacking the archive.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        void Btn_Repack_Click(object sender, EventArgs e)
        {
            repackARC();
        }

        void Main_Load(object sender, EventArgs e)
        {
            #region Session ID...
            var generateSessionID = new Random();
            Global.sessionID = generateSessionID.Next(1, 99999); //Generates a random number between 1 to 99999 for a unique Session ID.
            btn_SessionID.Text = Global.sessionID.ToString();
            #endregion

            #region Directory Check...
            try
            {
                //The below code checks if the directories in the Global class exist; if not, they will be created.
                if (!Directory.Exists(Global.tempPath)) Directory.CreateDirectory(Global.tempPath);
                if (!Directory.Exists(Global.archivesPath)) Directory.CreateDirectory(Global.archivesPath);
                if (!Directory.Exists(Global.toolsPath)) Directory.CreateDirectory(Global.toolsPath);
                if (!Directory.Exists(Global.unlubPath)) Directory.CreateDirectory(Global.unlubPath);
                if (!Directory.Exists(Global.xnoPath)) Directory.CreateDirectory(Global.xnoPath);
            }
            catch { MessageBox.Show("An error occurred when writing a directory.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); Application.Exit(); }
            #endregion

            #region File Check...
            try
            {
                //The below code checks if the files in the Global class exist; if not, they will be created.
                if (!File.Exists(Global.unpackFile)) File.WriteAllBytes(Global.unpackFile, Properties.Resources.unpack);
                if (!File.Exists(Global.repackFile)) File.WriteAllBytes(Global.repackFile, Properties.Resources.repack);
                if (!File.Exists(Global.xnoFile)) File.WriteAllBytes(Global.xnoFile, Properties.Resources.xno2dae);
            }
            catch { MessageBox.Show("An error occurred when writing a file.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); Application.Exit(); }
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

            #region Tab Check...
            //Temporary solution.
            if (tab_Main.SelectedTab.Text != "New Tab")
            {
                #region Enable controls...
                btn_Back.Enabled = true;
                btn_Forward.Enabled = true;
                btn_Repack.Enabled = true;
                file_RepackARC.Enabled = true;
                btn_OpenFolder.Enabled = true;
                sdk_DecompileLUBs.Enabled = true;
                sdk_LUBStudio.Enabled = true;
                #endregion

                Global.currentPath = currentARC().Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\";
            }
            else
            {
                #region Disable controls...
                btn_Back.Enabled = false;
                btn_Forward.Enabled = false;
                btn_Repack.Enabled = false;
                file_RepackARC.Enabled = false;
                btn_OpenFolder.Enabled = false;
                sdk_DecompileLUBs.Enabled = false;
                sdk_LUBStudio.Enabled = false;
                #endregion
            }
            #endregion
        }

        void File_RepackARC_Click(object sender, EventArgs e)
        {
            repackARC();
        }

        void Sdk_DecompileLUBs_Click(object sender, EventArgs e)
        {
            //This process needs work. It would be better to decompile directly with a C# decompiler, rather than depending on a Java decompiler.
            //It's based on Lua's own source, so it wouldn't be too difficult to set up (if you know what you're doing).
            if (!Directory.Exists(@"C:\Program Files\Java\")) if (!Directory.Exists(@"C:\Program Files (x86)\Java\")) MessageBox.Show("Sonic '06 Toolkit requires Java to decompile Lua binaries. Please install Java and restart your computer.", "Java Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    if (Directory.GetFiles(Global.currentPath, "*.lub").Length == 0) MessageBox.Show("There are no Lua binaries to decompile in this directory.", "No Lua binaries available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        try
                        {
                            #region Getting current ARC failsafe...
                            if (!Directory.Exists(Global.unlubPath + Global.sessionID)) Directory.CreateDirectory(Global.unlubPath + Global.sessionID);
                            var failsafeBuildSession = new StringBuilder();
                            failsafeBuildSession.Append(Global.archivesPath);
                            failsafeBuildSession.Append(Global.sessionID);
                            failsafeBuildSession.Append(@"\");
                            string failsafeCheck = File.ReadAllText(failsafeBuildSession.ToString() + tab_Main.SelectedIndex);
                            #endregion

                            #region Writing decompiler...
                            if (!Directory.Exists(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck)) Directory.CreateDirectory(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck);
                            if (!Directory.Exists(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\lubs")) Directory.CreateDirectory(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\lubs");
                            if (!File.Exists(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.jar")) File.WriteAllBytes(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.jar", Properties.Resources.unlub);
                            if (!File.Exists(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.bat")) File.WriteAllBytes(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.bat", Properties.Resources.unlubBASIC);
                            #endregion

                            #region Verifying Lua binaries...
                            foreach (string LUB in Directory.GetFiles(Global.currentPath, "*.lub", SearchOption.TopDirectoryOnly))
                            {
                                if (File.Exists(LUB))
                                {
                                    if (File.ReadAllLines(LUB)[0].Contains("LuaP"))
                                    {
                                        File.Copy(LUB, Path.Combine(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\lubs\", Path.GetFileName(LUB)), true);
                                    }
                                }
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
                        catch { MessageBox.Show("An error occurred when decompiling the Lua binaries.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
            }
        }

        void Btn_OpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start(currentARC().Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\");
        }

        void Sdk_LUBStudio_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(@"C:\Program Files\Java\")) if (!Directory.Exists(@"C:\Program Files (x86)\Java\")) MessageBox.Show("Sonic '06 Toolkit requires Java to decompile Lua binaries. Please install Java and restart your computer.", "Java Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    new LUB_Studio().ShowDialog();
                }
        }
        void Tab_Main_SelectedIndexChanged(object sender, EventArgs e)
        {
            Global.getIndex = tab_Main.SelectedIndex;
        }
    }
}
