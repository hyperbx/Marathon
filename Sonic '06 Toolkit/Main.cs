using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Sonic_06_Toolkit
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
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
            }
            catch { MessageBox.Show("An error occurred when writing a file.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); Application.Exit(); }
            #endregion

            #region Setting saved properties...
            //Gets user-defined settings and sets them in runtime.
            if (Properties.Settings.Default.prop_ShowSessionID == true) preferences_ShowSessionID.Checked = true; else preferences_ShowSessionID.Checked = false;
            if (Properties.Settings.Default.prop_Theme == "Compact") themes_Compact.Checked = true; else if (Properties.Settings.Default.prop_Theme == "Original") themes_Original.Checked = true;
            #endregion

            newTab(); //Opens a new tab on launch.
            tm_tabCheck.Start(); //Starts the timer that watches tab activity.
        }

        #region Preferences
        //[Preferences] - Show Session ID
        //Moves certain controls in runtime to hide the Session ID properly.
        void Preferences_ShowSessionID_CheckedChanged(object sender, EventArgs e)
        {
            if (preferences_ShowSessionID.Checked == true)
            {
                Properties.Settings.Default.prop_ShowSessionID = true;
                btn_SessionID.Visible = true;
                btn_Repack.Left -= 48;
                btn_OpenFolder.Left -= 48;
            }
            else
            {
                Properties.Settings.Default.prop_ShowSessionID = false;
                btn_SessionID.Visible = false;
                btn_Repack.Left += 48;
                btn_OpenFolder.Left += 48;
            }
            Properties.Settings.Default.Save();
        }

        //[Themes] - Compact
        //Moves certain controls in runtime to switch to the Compact theme.
        void Themes_Compact_CheckedChanged(object sender, EventArgs e)
        {
            if (themes_Compact.Checked == true)
            {
                Properties.Settings.Default.prop_Theme = "Compact";
                themes_Original.Checked = false;
                mstrip_Main.Left += 106;
                tab_Main.Height += 28; tab_Main.Top -= 28;
                btn_Back.Width -= 4; btn_Back.Height += 3; btn_Back.Left -= 5; btn_Back.Top -= 29; btn_Back.FlatAppearance.BorderSize = 1;
                btn_Forward.Width -= 10; btn_Forward.Height += 3; btn_Forward.Left -= 14; btn_Forward.Top -= 29; btn_Forward.FlatAppearance.BorderSize = 1;
                btn_NewTab.Width += 2; btn_NewTab.Height += 3; btn_NewTab.Left += 139; btn_NewTab.Top -= 29; btn_NewTab.BackColor = Color.FromArgb(27, 161, 226); btn_NewTab.FlatAppearance.BorderSize = 1;
                btn_OpenFolder.Width += 3; btn_OpenFolder.Height += 3; btn_OpenFolder.Left -= 18; btn_OpenFolder.Top -= 29; btn_OpenFolder.BackColor = Color.FromArgb(232, 171, 83); btn_OpenFolder.FlatAppearance.BorderSize = 1;
                btn_Repack.Text = "Repack"; btn_Repack.Width -= 24; btn_Repack.Height += 3; btn_Repack.Left -= 20; btn_Repack.Top -= 29; btn_Repack.FlatAppearance.BorderSize = 1;
                btn_SessionID.Height += 3; btn_SessionID.Left += 173; btn_SessionID.Top -= 29; btn_SessionID.BackColor = SystemColors.ControlLightLight; btn_SessionID.FlatAppearance.BorderColor = SystemColors.ControlLight;
            }
            Properties.Settings.Default.Save();
        }

        //[Themes] - Original
        //Moves certain controls in runtime to switch to the Original theme.
        void Themes_Original_CheckedChanged(object sender, EventArgs e)
        {
            if (themes_Original.Checked == true)
            {
                Properties.Settings.Default.prop_Theme = "Original";
                themes_Compact.Checked = false;
                mstrip_Main.Left -= 106;
                tab_Main.Height -= 28; tab_Main.Top += 28;
                btn_Back.Width += 4; btn_Back.Height -= 3; btn_Back.Left += 5; btn_Back.Top += 29; btn_Back.FlatAppearance.BorderSize = 0; 
                btn_Forward.Width += 10; btn_Forward.Height -= 3; btn_Forward.Left += 14; btn_Forward.Top += 29; btn_Forward.FlatAppearance.BorderSize = 0; 
                btn_NewTab.Width -= 2; btn_NewTab.Height -= 3; btn_NewTab.Left -= 139; btn_NewTab.Top += 29; btn_NewTab.BackColor = SystemColors.ControlLightLight; btn_NewTab.FlatAppearance.BorderSize = 0;
                btn_OpenFolder.Width -= 3; btn_OpenFolder.Height -= 3; btn_OpenFolder.Left += 18; btn_OpenFolder.Top += 29; btn_OpenFolder.BackColor = SystemColors.ControlLightLight; btn_OpenFolder.FlatAppearance.BorderSize = 0;
                btn_Repack.Text = "Quick Repack"; btn_Repack.Width += 24; btn_Repack.Height -= 3; btn_Repack.Left += 20; btn_Repack.Top += 29; btn_Repack.FlatAppearance.BorderSize = 0;
                btn_SessionID.Height -= 3; btn_SessionID.Left -= 173; btn_SessionID.Top += 29; btn_SessionID.BackColor = SystemColors.ControlLight; btn_SessionID.FlatAppearance.BorderColor = SystemColors.WindowFrame;
            }
            Properties.Settings.Default.Save();
        }
        #endregion

        void newTab()
        {
            //Creates a new web browser instance (which hooks into File Explorer).
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
            //Returns the active web browser in the selected tab.
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
                    //Builds the main string which locates the ARC's final unpack directory.
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
                    //Establishes the failsafe directory and copies the ARC prepare for the unpacking process.
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
                    //Sets up the BASIC application and executes the unpacking process.
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
                    //Writes metadata to the unpacked directory to ensure the original path is remembered.
                    var metadataWrite = File.Create(arcBuildSession.ToString() + "metadata.ini");
                    var metadataSession = new UTF8Encoding(true).GetBytes(ofd_OpenARC.FileName);
                    metadataWrite.Write(metadataSession, 0, metadataSession.Length);
                    metadataWrite.Close();
                    unpackDialog.Close();
                    #endregion

                    #region Navigating...
                    //Creates a new tab if the selected one is being used.
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
                    //Writes a file to store the failsafe directory to be referenced later.
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

        #region Repack States
        void Btn_Repack_Click(object sender, EventArgs e)
        {
            Global.repackState = "save";
            repackARC();
        }

        void File_RepackARC_Click(object sender, EventArgs e)
        {
            Global.repackState = "save";
            repackARC();
        }

        void File_RepackARCAs_Click(object sender, EventArgs e)
        {
            Global.repackState = "save-as";
            repackARC();
        }
        #endregion

        void repackARC()
        {
            if (Global.repackState == "save")
            {
                try
                {
                    #region Building repack data...
                    //Reads the metadata to get the original location of the ARC.
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
                    //Sets up the BASIC application and executes the repacking process.
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
                    string archivePath = repackBuildSession.ToString() + Path.GetFileName(metadata);
                    if (File.Exists(archivePath)) File.Copy(archivePath, metadata, true); //Copies the repacked ARC back to the original location.
                    repackDialog.Close();
                    #endregion
                }
                catch { MessageBox.Show("An error occurred when repacking the archive.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else if (Global.repackState == "save-as")
            {
                if (sfd_RepackARCAs.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        #region Building repack data...
                        //Reads the metadata to get the original name of the ARC.
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
                        //Sets up the BASIC application and executes the repacking process.
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
                        string archivePath = repackBuildSession.ToString() + Path.GetFileName(metadata);
                        if (File.Exists(archivePath)) File.Copy(archivePath, sfd_RepackARCAs.FileName, true);
                        repackDialog.Close();
                        #endregion
                    }
                    catch { MessageBox.Show("An error occurred when repacking the archive.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
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
            //Checks if the tab's text reads 'New Tab' (a name only assigned by the application).
            if (tab_Main.SelectedTab.Text == "New Tab")
            {
                if (tab_Main.TabPages.Count >= 1) tab_Main.TabPages.Remove(tab_Main.SelectedTab);
            }
            else
            {
                DialogResult confirmClosure = MessageBox.Show("Are you sure you want to close this tab? All unsaved changes will be lost if you haven't repacked.", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                switch (confirmClosure)
                {
                    case DialogResult.Yes: if (tab_Main.TabPages.Count >= 1) tab_Main.TabPages.Remove(tab_Main.SelectedTab); break;
                }
            }
        }

        void Tm_tabCheck_Tick(object sender, EventArgs e)
        {
            //Ensures there's at least only one tab left on the control.
            if (tab_Main.TabPages.Count == 1) tabs_CloseTab.Enabled = false; else tabs_CloseTab.Enabled = true;

            #region Tab Check...
            if (tab_Main.SelectedTab.Text != "New Tab")
            {
                #region Enable controls...
                //Enables all viable controls if the tab isn't empty.
                btn_Back.Enabled = true;
                btn_Forward.Enabled = true;
                btn_Repack.Enabled = true;
                file_RepackARC.Enabled = true;
                btn_OpenFolder.Enabled = true;
                sdk_DecompileLUBs.Enabled = true;
                sdk_LUBStudio.Enabled = true;
                file_CloseARC.Enabled = true;
                file_RepackARCAs.Enabled = true;
                sdk_XNOStudio.Enabled = true;
                sdk_ConvertXNOs.Enabled = true;
                #endregion

                //Updates the currentPath global variable.
                Global.currentPath = currentARC().Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\";
            }
            else
            {
                #region Disable controls...
                //Disables all viable controls if the tab is empty.
                btn_Back.Enabled = false;
                btn_Forward.Enabled = false;
                btn_Repack.Enabled = false;
                file_RepackARC.Enabled = false;
                btn_OpenFolder.Enabled = false;
                sdk_DecompileLUBs.Enabled = false;
                sdk_LUBStudio.Enabled = false;
                file_CloseARC.Enabled = false;
                file_RepackARCAs.Enabled = false;
                sdk_XNOStudio.Enabled = false;
                sdk_ConvertXNOs.Enabled = false;
                #endregion
            }
            #endregion
        }

        void Sdk_DecompileLUBs_Click(object sender, EventArgs e)
        {
            //This process needs work. It would be better to decompile directly with a C# decompiler, rather than depending on a Java decompiler.
            //It's based on Lua's own source, so it wouldn't be too difficult to set up (if you know what you're doing).
            if (!Directory.Exists(@"C:\Program Files\Java\")) if (!Directory.Exists(@"C:\Program Files (x86)\Java\")) MessageBox.Show("Sonic '06 Toolkit requires Java to decompile Lua binaries. Please install Java and restart your computer.", "Java Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    //Temporary solution; would probably be better to use an array.
                    //Checks if the first file to be processed is blacklisted. If so, abort the operation to ensure the file doesn't get corrupted.
                    if (File.Exists(Global.currentPath + @"\standard.lub")) MessageBox.Show("File: standard.lub\n\nThis file cannot be decompiled; attempts to do so will render the file unusable.", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        //Checks if there are any LUBs in the directory.
                        if (Directory.GetFiles(Global.currentPath, "*.lub").Length == 0) MessageBox.Show("There are no Lua binaries to decompile in this directory.", "No Lua binaries available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                        {
                            //Checks if any of the blacklisted files are present. If so, warn the user about modifying the files.
                            if (File.Exists(Global.currentPath + @"\render_shadowmap.lub")) MessageBox.Show("File: render_shadowmap.lub\n\nEditing this file may render this archive unusable. Please edit this at your own risk!", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            if (File.Exists(Global.currentPath + @"\game.lub")) MessageBox.Show("File: game.lub\n\nEditing this file may render it unusable. Please edit this at your own risk!", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            if (File.Exists(Global.currentPath + @"\object.lub")) MessageBox.Show("File: object.lub\n\nEditing this file may render it unusable. Please edit this at your own risk!", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            try
                            {
                                #region Getting current ARC failsafe...
                                //Gets the failsafe directory.
                                if (!Directory.Exists(Global.unlubPath + Global.sessionID)) Directory.CreateDirectory(Global.unlubPath + Global.sessionID);
                                var failsafeBuildSession = new StringBuilder();
                                failsafeBuildSession.Append(Global.archivesPath);
                                failsafeBuildSession.Append(Global.sessionID);
                                failsafeBuildSession.Append(@"\");
                                string failsafeCheck = File.ReadAllText(failsafeBuildSession.ToString() + tab_Main.SelectedIndex);
                                #endregion

                                #region Writing decompiler...
                                //Writes the decompiler to the failsafe directory to ensure any LUBs left over from other open archives aren't copied over to the selected archive.
                                if (!Directory.Exists(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck)) Directory.CreateDirectory(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck);
                                if (!Directory.Exists(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\lubs")) Directory.CreateDirectory(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\lubs");
                                if (!File.Exists(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.jar")) File.WriteAllBytes(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.jar", Properties.Resources.unlub);
                                if (!File.Exists(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.bat")) File.WriteAllBytes(Global.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.bat", Properties.Resources.unlubBASIC);
                                #endregion

                                #region Verifying Lua binaries...
                                //Checks the header for each file to ensure that it can be safely decompiled.
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
                                //Sets up the BASIC application and executes the decompiling process.
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
                                //Copies all LUBs to the final directory, then erases leftovers.
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
        }

        void Btn_OpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start(currentARC().Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\");
        }

        void Sdk_LUBStudio_Click(object sender, EventArgs e)
        {
            //This process needs work. It would be better to decompile directly with a C# decompiler, rather than depending on a Java decompiler.
            //It's based on Lua's own source, so it wouldn't be too difficult to set up (if you know what you're doing).
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

        void File_CloseARC_Click(object sender, EventArgs e)
        {
            //Asks for user confirmation before closing an archive.
            DialogResult confirmClosure = MessageBox.Show("Are you sure you want to close this archive? All unsaved changes will be lost if you haven't repacked.", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch (confirmClosure)
            {
                case DialogResult.Yes: tab_Main.TabPages.Remove(tab_Main.SelectedTab); newTab(); break;
            }
        }

        void Help_Documentation_Click(object sender, EventArgs e)
        {
            //Opens the Documentation form in the centre of the parent window without disabling it.
            var documentation = new Documentation();
            var parentLeft = Left + ((Width - documentation.Width) / 2);
            var parentTop = Top + ((Height - documentation.Height) / 2);
            documentation.Location = new System.Drawing.Point(parentLeft, parentTop);
            documentation.Show();
        }

        void Sdk_XNOStudio_Click(object sender, EventArgs e)
        {
            new XNO_Studio().ShowDialog();
        }

        void Sdk_ConvertXNOs_Click(object sender, EventArgs e)
        {
            //Checks if there are any XNOs in the directory.
            if (Directory.GetFiles(Global.currentPath, "*.xno").Length == 0) MessageBox.Show("There are no XNOs to convert in this directory.", "No XNOs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
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
                    //Writes the converter to the failsafe directory to ensure any XNOs left over from other open archives aren't copied over to the selected archive.
                    if (!Directory.Exists(Global.xnoPath + Global.sessionID + @"\" + failsafeCheck)) Directory.CreateDirectory(Global.xnoPath + Global.sessionID + @"\" + failsafeCheck);
                    if (!Directory.Exists(Global.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xnos")) Directory.CreateDirectory(Global.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xnos");
                    if (!File.Exists(Global.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xno2dae.exe")) File.WriteAllBytes(Global.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xno2dae.exe", Properties.Resources.xno2dae);
                    #endregion

                    #region Getting XNOs...
                    //Gets all checked boxes from the CheckedListBox and builds a string for each XNO.
                    foreach (string XNO in Directory.GetFiles(Global.currentPath, "*.xno", SearchOption.TopDirectoryOnly))
                    {
                        if (File.Exists(XNO))
                        {
                            #region Building XNOs...
                            //Gets the location of the converter and writes a BASIC application.
                            string convertPath = Path.Combine(Global.currentPath, XNO);
                            var checkedBuildSession = new StringBuilder();
                            checkedBuildSession.Append(Global.xnoPath);
                            checkedBuildSession.Append(Global.sessionID);
                            checkedBuildSession.Append(@"\");
                            checkedBuildSession.Append(failsafeCheck);
                            checkedBuildSession.Append(@"\xno2dae.exe");
                            var checkedWrite = File.Create(Global.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xno2dae.bat");
                            var checkedText = new UTF8Encoding(true).GetBytes("\"" + checkedBuildSession.ToString() + "\" \"" + XNO + "\"");
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
                    }
                    #endregion
                }
                catch { MessageBox.Show("An error occurred when converting the XNOs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        void Btn_NewTab_Click(object sender, EventArgs e)
        {
            newTab();
        }
    }
}
