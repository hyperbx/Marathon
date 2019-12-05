using System;
using System.IO;
using System.Web;
using System.Text;
using System.Linq;
using NAudio.Wave;
using System.Media;
using Toolkit.Text;
using Toolkit.Tools;
using HedgeLib.Sets;
using System.Drawing;
using Microsoft.Win32;
using VGAudio.Formats;
using SonicAudioLib.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using VGAudio.Containers.Adx;
using VGAudio.Containers.Wave;
using System.Collections.Generic;

// Sonic '06 Toolkit is licensed under the MIT License:
/*
 * MIT License

 * Copyright (c) 2019 Gabriel (HyperPolygon64)

 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:

 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

namespace Toolkit.EnvironmentX
{
    public partial class Main : Form
    {
        public static readonly string versionNumber = "Version 3.11"; // Defines the version number to be used globally
        public static List<string> sessionLog = new List<string>();
        public static string repackBuildSession = string.Empty;
        public static string serverStatus = string.Empty;
        bool registryWarnWorkaround = false;
        string extractQueue = string.Empty;
        bool folderMenu = false;

        public Main(string[] args, int sessionID) {
            InitializeComponent();
            if (Program.RunningAsAdmin()) SystemMessages.tl_DefaultTitleVersion += " <Administrator>";
            Text = SystemMessages.tl_DefaultTitleVersion;
            btn_SessionID.Text = sessionID.ToString();
            tm_CheapFix.Start();
            NewTab(true);

            if (args.Length > 0) {
                if (Path.GetExtension(args[0]).ToLower() == ".arc")
                    UnpackARC(args[0]);
            }

            Logs.ToolkitSessionLog openLogs = Application.OpenForms["ToolkitSessionLog"] != null ? (Logs.ToolkitSessionLog)Application.OpenForms["ToolkitSessionLog"] : null;
            if (openLogs == null)
                if (!Properties.Settings.Default.log_Startup)
                    if (Properties.Settings.Default.env_firstLaunch) {
                        lbl_Status.Text = "Welcome to Sonic '06 Toolkit! Click here to view the Session Log...";
                        Properties.Settings.Default.env_firstLaunch = false;
                    } else
                        lbl_Status.Text = "Welcome back! Click here to view the Session Log...";
                else new Logs.ToolkitSessionLog().Show();

            try {
                registryWarnWorkaround = true;
                RegistryKey key = Registry.ClassesRoot.OpenSubKey(".arc_auto_file", false);
                RegistryKey getLocation = Registry.ClassesRoot.OpenSubKey(".arc_auto_file\\shell\\open\\command");
                if (key == null)
                    preferences_AssociateARCs.Checked = false;
                else
                    if (getLocation.GetValue(null).ToString() != $"\"{Application.ExecutablePath}\" \"%1\"")
                        preferences_AssociateARCs.Checked = false;
                    else
                        preferences_AssociateARCs.Checked = true;
            } catch { }

            if (Properties.Settings.Default.env_gameDirDisabled) preferences_DisableGameDirectory.Checked = true;
            if (Properties.Settings.Default.env_updaterDisabled) preferences_DisableSoftwareUpdater.Checked = true;
        }

        public string Status {
            get { return lbl_Status.Text; }
            set {
                lbl_Status.Text = value;
                if (Properties.Settings.Default.log_timestamps)
                    sessionLog.Add($"[{DateTime.Now.ToString("HH:mm:ss tt")}] {value}");
                else sessionLog.Add(value);
                tm_ResetStatus.Start();
            }
        }

        private void NewTab(bool navigateToGame) {
            //Creates a new web browser instance (which hooks into File Explorer).
            var nextTab = new TabPage();
            var nextBrowser = new WebBrowser();
            nextTab.Text = "New Tab";
            unifytb_Main.TabPages.Add(nextTab);
            nextTab.Controls.Add(nextBrowser);
            unifytb_Main.SelectedTab = nextTab;
            nextBrowser.Dock = DockStyle.Fill;
            CurrentARC().AllowWebBrowserDrop = false;

            if (navigateToGame)
                if (Properties.Settings.Default.env_gameDirectory != string.Empty) {
                    if (Directory.Exists(Properties.Settings.Default.env_gameDirectory)) {
                        pic_Logo.Visible = false;
                        lbl_SetDefault.Visible = false;
                        CurrentARC().Navigate(Properties.Settings.Default.env_gameDirectory);
                    } else {
                        pic_Logo.Visible = true;
                        if (!Properties.Settings.Default.env_gameDirDisabled) lbl_SetDefault.Visible = true;
                    }
                } else {
                    pic_Logo.Visible = true;
                    if (!Properties.Settings.Default.env_gameDirDisabled) lbl_SetDefault.Visible = true;
                }
        }

        private void ResetTab(bool navigateToGame) {
            //Creates a new web browser instance (which hooks into File Explorer).
            CurrentARC().Dispose();
            var nextBrowser = new WebBrowser();
            unifytb_Main.SelectedTab.Text = "New Tab";
            unifytb_Main.SelectedTab.Controls.Add(nextBrowser);
            unifytb_Main.SelectedTab.ToolTipText = string.Empty;
            nextBrowser.Dock = DockStyle.Fill;
            CurrentARC().AllowWebBrowserDrop = false;
            Text = SystemMessages.tl_DefaultTitleVersion;

            if (navigateToGame)
                if (Properties.Settings.Default.env_gameDirectory != string.Empty) {
                    if (Directory.Exists(Properties.Settings.Default.env_gameDirectory)) {
                        pic_Logo.Visible = false;
                        lbl_SetDefault.Visible = false;
                        CurrentARC().Navigate(Properties.Settings.Default.env_gameDirectory);
                    } else {
                        pic_Logo.Visible = true;
                        if (!Properties.Settings.Default.env_gameDirDisabled) lbl_SetDefault.Visible = true;
                    }
                } else {
                    pic_Logo.Visible = true;
                    if (!Properties.Settings.Default.env_gameDirDisabled) lbl_SetDefault.Visible = true;
                }
        }

        private WebBrowser CurrentARC() {
            //Returns the active web browser in the selected tab.
            return (WebBrowser)unifytb_Main.SelectedTab.Controls[0];
        }

        private void Window_NewTab_Click(object sender, EventArgs e) { NewTab(true); }

        private void Window_NewWindow_Click(object sender, EventArgs e) {
            try { Process.Start(Application.ExecutablePath); }
            catch (Exception ex) { MessageBox.Show($"{SystemMessages.ex_NewWindowError}\n\n{ex}", SystemMessages.tl_DefaultTitle, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Window_CloseTab_Click(object sender, EventArgs e) {
            if (unifytb_Main.SelectedTab.ToolTipText == string.Empty || unifytb_Main.SelectedTab.ToolTipText.StartsWith("Zm9sZGVy")) {
                if (unifytb_Main.TabPages.Count > 1) {
                    int leftMostTab = unifytb_Main.SelectedIndex - 1;
                    unifytb_Main.TabPages.Remove(unifytb_Main.SelectedTab);
                    unifytb_Main.SelectedIndex = leftMostTab; //Sets the selected tab to the tab to the left of the current selected one after removing it.
                } else
                    ResetTab(true);
                Text = SystemMessages.tl_DefaultTitleVersion;
            } else {
                switch (MessageBox.Show(SystemMessages.msg_CloseTab, SystemMessages.tl_AreYouSure, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
                    case DialogResult.Yes:
                        if (unifytb_Main.TabPages.Count > 1) {
                            int leftMostTab = unifytb_Main.SelectedIndex - 1;
                            unifytb_Main.TabPages.Remove(unifytb_Main.SelectedTab);
                            unifytb_Main.SelectedIndex = leftMostTab;
                        } else {
                            unifytb_Main.TabPages.Remove(unifytb_Main.SelectedTab);
                            NewTab(true);
                        }
                        Text = SystemMessages.tl_DefaultTitleVersion;
                        break;
                }
            }
        }

        private void Unifytb_Main_MouseClick(object sender, MouseEventArgs e) {
            var mainTab = sender as TabControl;
            var tabs = mainTab.TabPages;

            if (e.Button == MouseButtons.Middle) {
                if (tabs.Cast<TabPage>().Where((t, i) => mainTab.GetTabRect(i).Contains(e.Location)).First().ToolTipText == string.Empty ||
                    tabs.Cast<TabPage>().Where((t, i) => mainTab.GetTabRect(i).Contains(e.Location)).First().ToolTipText.StartsWith("Zm9sZGVy")) {
                    if (unifytb_Main.TabPages.Count > 1)
                        tabs.Remove(tabs.Cast<TabPage>().Where((t, i) => mainTab.GetTabRect(i).Contains(e.Location)).First());
                    else
                        ResetTab(true);
                    Text = SystemMessages.tl_DefaultTitleVersion;
                } else {
                    switch (MessageBox.Show(SystemMessages.msg_CloseTab, SystemMessages.tl_AreYouSure, MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
                        case DialogResult.Yes:
                            if (unifytb_Main.TabPages.Count > 1)
                                tabs.Remove(tabs.Cast<TabPage>().Where((t, i) => mainTab.GetTabRect(i).Contains(e.Location)).First());
                            else {
                                tabs.Remove(tabs.Cast<TabPage>().Where((t, i) => mainTab.GetTabRect(i).Contains(e.Location)).First());
                                NewTab(true);
                            }
                            Text = SystemMessages.tl_DefaultTitleVersion;
                            break;
                    }
                }
            }
        }

        private void Window_CloseAllTabs_Click(object sender, EventArgs e) {
            bool warning = false;
            if (unifytb_Main.TabPages.Count != 1 || unifytb_Main.SelectedTab.ToolTipText != string.Empty || !unifytb_Main.SelectedTab.ToolTipText.StartsWith("Zm9sZGVy")) {
                foreach (TabPage tabID in unifytb_Main.TabPages)
                    if (tabID.ToolTipText != string.Empty && !unifytb_Main.SelectedTab.ToolTipText.StartsWith("Zm9sZGVy")) warning = true;

                if (warning) {
                    switch (MessageBox.Show(SystemMessages.msg_CloseAllTabs, SystemMessages.tl_AreYouSure, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)) {
                        case DialogResult.Yes:
                            foreach (TabPage tab in unifytb_Main.TabPages) unifytb_Main.TabPages.Remove(tab);
                            Text = SystemMessages.tl_DefaultTitleVersion;
                            NewTab(true);
                            break;
                    }
                }
                else {
                    foreach (TabPage tab in unifytb_Main.TabPages) unifytb_Main.TabPages.Remove(tab);
                    Text = SystemMessages.tl_DefaultTitleVersion;
                    NewTab(true);
                }
            } else
                ResetTab(true);
        }

        private void File_OpenFolder_Click(object sender, EventArgs e) {
            string getPath = Browsers.CommonBrowser(true, "Please select a folder...", string.Empty);

            if (getPath != string.Empty) {
                if (unifytb_Main.SelectedTab.ToolTipText == string.Empty) ResetTab(false);
                else NewTab(false);

                CurrentARC().Navigate(getPath);
                unifytb_Main.SelectedTab.Text = Path.GetFileName(getPath);
                unifytb_Main.SelectedTab.ToolTipText = $"Zm9sZGVy - {getPath}";

                if (Path.GetFileName(getPath).ToLower() == "new tab" || Path.GetFileName(getPath).EndsWith(".arc"))
                    unifytb_Main.SelectedTab.Text += " (Folder)";

                Text = SystemMessages.tl_Exploring(getPath);
            }
        }

        private async void UnpackARC(string ARC) {
            if (File.Exists(ARC) && Verification.VerifyMagicNumberCommon(ARC)) {
                try {
                    string failsafeCheck = Path.GetRandomFileName();
                    string arcBuildSession = Path.Combine(Program.applicationData, Paths.Archives, Program.sessionID.ToString(), failsafeCheck);

                    if (File.Exists(ARC)) {
                        Directory.CreateDirectory(arcBuildSession);
                        File.Copy(ARC, Path.Combine(arcBuildSession, Path.GetFileName(ARC)), true); 
                    }

                    Status = StatusMessages.cmn_Unpacking(ARC, false);
                    await ProcessAsyncHelper.ExecuteShellCommand(Paths.Unpack,
                            $"\"{Path.Combine(arcBuildSession, Path.GetFileName(ARC))}\"",
                            Application.StartupPath,
                            100000);

                    //Writes metadata to the unpacked directory to ensure the original path is remembered.
                    var metadataWrite = File.Create(Path.Combine(arcBuildSession, "metadata.ini"));
                    byte[] metadataSession = new UTF8Encoding(true).GetBytes(ARC);
                    metadataWrite.Write(metadataSession, 0, metadataSession.Length);
                    metadataWrite.Close();

                    if (unifytb_Main.SelectedTab.ToolTipText == string.Empty) ResetTab(false);
                    else NewTab(false);

                    CurrentARC().Navigate(Path.Combine(arcBuildSession, Path.GetFileNameWithoutExtension(ARC)));
                    unifytb_Main.SelectedTab.Text = Path.GetFileName(ARC);
                    unifytb_Main.SelectedTab.ToolTipText = failsafeCheck;
                    repackBuildSession = Path.Combine(Program.applicationData, Paths.Archives, Program.sessionID.ToString(), unifytb_Main.SelectedTab.ToolTipText);

                    string metadata = File.ReadAllText(Path.Combine(repackBuildSession, "metadata.ini"));
                    Text = SystemMessages.tl_Exploring(metadata);
                    Status = StatusMessages.cmn_Unpacked(ARC, false);
                } catch { Status = StatusMessages.cmn_UnpackFailed(ARC, false); }
            } else
                MessageBox.Show(SystemMessages.ex_InvalidARC, SystemMessages.tl_DefaultTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void File_OpenARC_Click(object sender, EventArgs e) {
            string getPath = Browsers.CommonBrowser(false, SystemMessages.tl_SelectArchive, Filters.Archives);

            if (getPath != string.Empty)
                UnpackARC(getPath);
        }

        private void Unifytb_Main_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                RefreshPath();
                if (unifytb_Main.SelectedTab.ToolTipText == string.Empty)
                    Text = SystemMessages.tl_DefaultTitleVersion;
                else if (unifytb_Main.SelectedTab.ToolTipText.StartsWith("Zm9sZGVy"))
                    Text = SystemMessages.tl_Exploring(HttpUtility.UrlDecode(CurrentARC().Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\").Replace("file:", ""));
                else {
                    //Reads the metadata to get the original location of the ARC.
                    if (File.Exists(Path.Combine(repackBuildSession, "metadata.ini"))) {
                        string metadata = File.ReadAllText(Path.Combine(repackBuildSession, "metadata.ini"));
                        Text = SystemMessages.tl_Exploring(metadata);
                    } else {
                        MessageBox.Show(SystemMessages.ex_MetadataMissing, SystemMessages.tl_MetadataError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        string getPath = Browsers.SaveFile(SystemMessages.tl_RepackAs, Filters.Archives);
                        if (getPath != string.Empty) FixMetadata(repackBuildSession, getPath);
                        else unifytb_Main.TabPages.Remove(unifytb_Main.SelectedTab);
                    }
                }
            }
            catch { }
        }

        void FixMetadata(string repackBuildSession, string filename) {
            //Writes metadata to the unpacked directory to ensure the original path is remembered.
            var metadataWrite = File.Create(Path.Combine(repackBuildSession, "metadata.ini"));
            var metadataSession = new UTF8Encoding(true).GetBytes(filename);
            metadataWrite.Write(metadataSession, 0, metadataSession.Length);
            metadataWrite.Close();

            unifytb_Main.SelectedTab.Text = Path.GetFileName(filename);
            Text = SystemMessages.tl_Exploring(filename);
        }

        private async void Btn_Repack_Click(object sender, EventArgs e) {
            string metadata = string.Empty;
            
            try {
                if (!unifytb_Main.SelectedTab.ToolTipText.StartsWith("Zm9sZGVy")) {
                    repackBuildSession = Path.Combine(Program.applicationData, Paths.Archives, Program.sessionID.ToString(), unifytb_Main.SelectedTab.ToolTipText);

                    metadata = File.ReadAllText(Path.Combine(repackBuildSession, "metadata.ini"));
                    Status = StatusMessages.cmn_Repacking(metadata, false);
                    await ProcessAsyncHelper.ExecuteShellCommand(Paths.Repack,
                          $"\"{Path.Combine(repackBuildSession, Path.GetFileNameWithoutExtension(metadata))}\"",
                          Application.StartupPath,
                          100000);
                    if (File.Exists(Path.Combine(repackBuildSession, Path.GetFileName(metadata))))
                        File.Copy(Path.Combine(repackBuildSession, Path.GetFileName(metadata)), metadata, true); //Copies the repacked ARC back to the original location.
                    Status = StatusMessages.cmn_Repacked(metadata, false);
                } else {
                    string getPath = Browsers.SaveFile(SystemMessages.tl_RepackAs, Filters.Archives);
                    if (getPath != string.Empty) RepackAs(getPath);
                }
            } catch { Status = StatusMessages.cmn_RepackFailed(metadata, false); }
        }

        private void Btn_OpenFolder_Click(object sender, EventArgs e) {
            try { Process.Start(HttpUtility.UrlDecode(CurrentARC().Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\").Replace("file:", "")); }
            catch (Exception ex) { MessageBox.Show($"{SystemMessages.ex_OpenFolderError}\n\n{ex}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Folder_CopyToClipboard_Click(object sender, EventArgs e) {
            try {
                Status = StatusMessages.msg_Clipboard;
                Clipboard.SetText(HttpUtility.UrlDecode(CurrentARC().Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\").Replace("file:", ""));
            } catch (Exception ex) { MessageBox.Show($"{SystemMessages.ex_ClipboardFolderError}\n\n{ex}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Btn_OpenFolderOptions_Click(object sender, EventArgs e) {
            if (folderMenu == false) {
                cms_Folder.Show(btn_OpenFolder, new Point(0, btn_OpenFolder.Height));
                btn_OpenFolderOptions.FlatAppearance.BorderSize = 0;
                btn_OpenFolderOptions.Height = 25;
                btn_OpenFolderOptions.BackColor = Color.FromArgb(173, 127, 62);
                btn_OpenFolder.BackColor = Color.FromArgb(173, 127, 62);
                folderMenu = true;
            } else {
                cms_Folder.Hide();
                btn_OpenFolderOptions.FlatAppearance.BorderSize = 1;
                btn_OpenFolderOptions.Height = 26;
                btn_OpenFolderOptions.BackColor = Color.FromArgb(232, 171, 83);
                btn_OpenFolder.BackColor = Color.FromArgb(232, 171, 83);
                folderMenu = false;
            }
        }

        private void Cms_Folder_Closed(object sender, ToolStripDropDownClosedEventArgs e) {
            btn_OpenFolderOptions.FlatAppearance.BorderSize = 1;
            btn_OpenFolderOptions.Height = 26;
            btn_OpenFolderOptions.BackColor = Color.FromArgb(232, 171, 83);
            btn_OpenFolder.BackColor = Color.FromArgb(232, 171, 83);
            folderMenu = false;
        }

        private void Btn_Back_Click(object sender, EventArgs e) { CurrentARC().GoBack(); }

        private void Btn_Forward_Click(object sender, EventArgs e) { CurrentARC().GoForward(); }

        private void File_RepackAs_Click(object sender, EventArgs e) {
            string getPath = Browsers.SaveFile(SystemMessages.tl_RepackAs, Filters.Archives);
            if (getPath != string.Empty) RepackAs(getPath); 
        }

        private async void RepackAs(string filename) {
            string metadata = string.Empty;

            if (filename != string.Empty) {
                try {
                    if (!unifytb_Main.SelectedTab.ToolTipText.StartsWith("Zm9sZGVy")) {
                        repackBuildSession = Path.Combine(Program.applicationData, Paths.Archives, Program.sessionID.ToString(), unifytb_Main.SelectedTab.ToolTipText);
                        
                        metadata = File.ReadAllText(Path.Combine(repackBuildSession, "metadata.ini"));
                        Status = StatusMessages.cmn_RepackingAs(metadata, filename, false);
                        await ProcessAsyncHelper.ExecuteShellCommand(Paths.Repack,
                              $"\"{Path.Combine(repackBuildSession, Path.GetFileNameWithoutExtension(metadata))}\"",
                              Application.StartupPath,
                              100000);
                        if (File.Exists(Path.Combine(repackBuildSession, Path.GetFileName(metadata))))
                            File.Copy(Path.Combine(repackBuildSession, Path.GetFileName(metadata)), filename, true); //Copies the repacked ARC back to the original location.

                        //Writes metadata to the unpacked directory to ensure the original path is remembered.
                        var metadataWrite = File.Create(Path.Combine(repackBuildSession, "metadata.ini"));
                        var metadataSession = new UTF8Encoding(true).GetBytes(filename);
                        metadataWrite.Write(metadataSession, 0, metadataSession.Length);
                        metadataWrite.Close();

                        unifytb_Main.SelectedTab.Text = Path.GetFileName(filename);
                        Status = StatusMessages.cmn_RepackedAs(metadata, filename, false);
                    } else {
                        string getPathFromTabMeta = unifytb_Main.SelectedTab.ToolTipText.Substring(11);
                        Status = StatusMessages.cmn_RepackingAs(getPathFromTabMeta, filename, false);
                        await ProcessAsyncHelper.ExecuteShellCommand(Paths.Arctool,
                              $"-i \"{getPathFromTabMeta}\" -c \"{filename}\"",
                              Application.StartupPath,
                              100000);
                        Status = StatusMessages.cmn_RepackedAs(getPathFromTabMeta, filename, false);
                    }
                } catch { Status = StatusMessages.cmn_RepackFailed(metadata, false); }
            }
        }

        private void InterfaceEnabled(bool enabled) {
            if (enabled) {
                pic_Logo.Visible = false;
                lbl_SetDefault.Visible = false;
                btn_OpenFolder.Enabled = true;
                btn_OpenFolderOptions.Enabled = true;
                btn_Repack.Enabled = true;
                btn_Back.Enabled = true;
                btn_Forward.Enabled = true;
                file_Repack.Enabled = true;
                file_RepackAs.Enabled = true;
                file_RepackAll.Enabled = true;
                foreach (ToolStripDropDownItem item in main_SDK.DropDownItems.OfType<ToolStripMenuItem>()) item.Enabled = true;
                foreach (ToolStripDropDownItem item in main_Shortcuts.DropDownItems.OfType<ToolStripMenuItem>()) item.Enabled = true;
            } else {
                if (unifytb_Main.SelectedTab.Text == "New Tab" && Properties.Settings.Default.env_gameDirectory != string.Empty) {
                    pic_Logo.Visible = false;
                    lbl_SetDefault.Visible = false;
                } else {
                    pic_Logo.Visible = true;
                    if (!Properties.Settings.Default.env_gameDirDisabled) lbl_SetDefault.Visible = true;
                }
                btn_OpenFolder.Enabled = false;
                btn_OpenFolderOptions.Enabled = false;
                btn_Repack.Enabled = false;
                btn_Back.Enabled = false;
                btn_Forward.Enabled = false;
                file_Repack.Enabled = false;
                file_RepackAs.Enabled = false;
                file_RepackAll.Enabled = false;
                foreach (ToolStripDropDownItem item in main_SDK.DropDownItems.OfType<ToolStripMenuItem>())
                    if (item != sdk_ArchiveMerger) item.Enabled = false;
                foreach (ToolStripDropDownItem item in main_Shortcuts.DropDownItems.OfType<ToolStripMenuItem>()) item.Enabled = false;
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e) {
            Properties.Settings.Default.Save();

            bool warning = false;
            foreach (TabPage tabID in unifytb_Main.TabPages)
                if (tabID.ToolTipText != string.Empty && !tabID.ToolTipText.StartsWith("Zm9sZGVy")) warning = true;

            if (warning) {
                switch (e.CloseReason) {
                    case CloseReason.UserClosing:
                        WarnOnClose(SystemMessages.msg_UserClosing, e);
                        break;
                    case CloseReason.WindowsShutDown:
                        WarnOnClose(SystemMessages.msg_WindowsShutDown, e);
                        break;
                    case CloseReason.ApplicationExitCall:
                        WarnOnClose(SystemMessages.msg_UserClosing, e);
                        break;
                }
            }
            else EraseData();
        }

        private void WarnOnClose(string message, FormClosingEventArgs e) {
            if (Program.CheckProcessStateByName(Paths.Repack, true)) {
                if (MessageBox.Show(SystemMessages.msg_StillRepacking, SystemMessages.tl_Repacking, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)  {
                    if (MessageBox.Show(message, SystemMessages.tl_DefaultTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        EraseData();
                    else e.Cancel = true;
                } else e.Cancel = true;
            } else {
                if (MessageBox.Show(message, SystemMessages.tl_DefaultTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    EraseData();
                else e.Cancel = true;
            }
        }

        private void EraseData() {
            var archiveData = new DirectoryInfo(Path.Combine(Program.applicationData, Paths.Archives, Program.sessionID.ToString()));

            try {
                if (Directory.Exists(Path.Combine(Program.applicationData, Paths.Archives, Program.sessionID.ToString()))) {
                    foreach (FileInfo file in archiveData.GetFiles())
                        file.Delete();
                    foreach (DirectoryInfo directory in archiveData.GetDirectories())
                        directory.Delete(true);
                    Directory.Delete(Path.Combine(Program.applicationData, Paths.Archives, Program.sessionID.ToString()));
                }
            }
            catch { }
        }

        private void Lbl_SetDefault_Click(object sender, EventArgs e) {
            string getPath = Browsers.CommonBrowser(true, SystemMessages.msg_SelectGameDirectory, string.Empty);
            if (getPath != string.Empty)
                if (Directory.Exists(getPath))
                    Properties.Settings.Default.env_gameDirectory = getPath;
                    if (unifytb_Main.SelectedTab.ToolTipText == string.Empty && unifytb_Main.SelectedTab.Text == "New Tab")
                        ResetTab(true);
        }

        private void Paths_ClearGameDirectory_Click(object sender, EventArgs e) { 
            Properties.Settings.Default.env_gameDirectory = string.Empty;
            if (unifytb_Main.SelectedTab.ToolTipText == string.Empty && unifytb_Main.SelectedTab.Text == "New Tab")
                ResetTab(true);
        }

        private void File_NewARC_Click(object sender, EventArgs e) {
            string getPath = Browsers.SaveFile(SystemMessages.tl_CreateARC, Filters.Archives);

            if (getPath != string.Empty) {
                try {
                    string failsafeCheck = Path.GetRandomFileName();
                    string arcBuildSession = Path.Combine(Program.applicationData, Paths.Archives, Program.sessionID.ToString(), unifytb_Main.SelectedTab.ToolTipText, failsafeCheck, Path.GetFileNameWithoutExtension(getPath));
                    Directory.CreateDirectory(arcBuildSession);

                    //Writes metadata to the unpacked directory to ensure the original path is remembered.
                    var metadataWrite = File.Create(Path.Combine(Path.GetDirectoryName(arcBuildSession), "metadata.ini"));
                    var metadataSession = new UTF8Encoding(true).GetBytes(getPath);
                    metadataWrite.Write(metadataSession, 0, metadataSession.Length);
                    metadataWrite.Close();

                    if (unifytb_Main.SelectedTab.ToolTipText == string.Empty) ResetTab(false);
                    else NewTab(false);

                    CurrentARC().Navigate(arcBuildSession);
                    unifytb_Main.SelectedTab.Text = Path.GetFileName(getPath);
                    unifytb_Main.SelectedTab.ToolTipText = failsafeCheck;
                    repackBuildSession = Path.GetDirectoryName(arcBuildSession);

                    string metadata = File.ReadAllText(Path.Combine(repackBuildSession, "metadata.ini"));
                    Text = SystemMessages.tl_Exploring(metadata);
                } catch (Exception ex) {
                    MessageBox.Show($"An error occurred when creating a new archive.\n\n{ex}", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Tm_CheapFix_Tick(object sender, EventArgs e) {
            try {
                RefreshPath();
                if (unifytb_Main.SelectedTab.ToolTipText != string.Empty && !unifytb_Main.SelectedTab.ToolTipText.StartsWith("Zm9sZGVy")) {
                    //Reads the metadata to get the original location of the ARC.
                    if (File.Exists(Path.Combine(repackBuildSession, "metadata.ini"))) {
                        string metadata = File.ReadAllText(Path.Combine(repackBuildSession, "metadata.ini"));
                        Text = SystemMessages.tl_Exploring(metadata);
                    } else { Text = SystemMessages.tl_DefaultTitleVersion; }
                }
            } catch { Text = SystemMessages.tl_DefaultTitleVersion; }

            try {
                RefreshPath();
                if (unifytb_Main.SelectedTab.Text != "New Tab" || unifytb_Main.SelectedTab.ToolTipText != string.Empty || Properties.Settings.Default.env_gameDirectory != string.Empty)
                    InterfaceEnabled(true);
                else if (unifytb_Main.SelectedTab.Text == "New Tab" && unifytb_Main.SelectedTab.ToolTipText == string.Empty)
                    InterfaceEnabled(false);
            }
            catch { }
        }

        private void RefreshPath() {
            try {
                repackBuildSession = Path.Combine(Program.applicationData, Paths.Archives, Program.sessionID.ToString(), unifytb_Main.SelectedTab.ToolTipText);
                string getCurrentPath = string.Empty;

                if (CurrentARC().Url != null) {
                    getCurrentPath = HttpUtility.UrlDecode(CurrentARC().Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\").Replace("file:", "");
                    Paths.currentPath = getCurrentPath;
                }

                //Console.WriteLine(repackBuildSession);
                //Console.WriteLine(getCurrentPath);
            } catch { }
        }

        private void Sdk_PlacementConverter_Click(object sender, EventArgs e) {
            if (ModifierKeys == Keys.Shift) {
                string getUserChoice = Chooser.UserChoice.Show("Please select an option...\n\n" +
                                                               "" +
                                                               "Common:\n" +
                                                               "► Import - Encode a XML to SET (experimental).\n" +
                                                               "► Export - Decode a SET to XML.", "Placement Converter", "Import", "Export");
                if (getUserChoice != "Abort")
                    switch (getUserChoice) {
                        case "Import":
                            try {
                                string getXML = Browsers.CommonBrowser(false, SystemMessages.tl_SelectFile, Filters.XML);
                                if (getXML != string.Empty)
                                    if (File.Exists(getXML) && Verification.VerifyXML(getXML, "SET")) {
                                        Status = StatusMessages.cmn_Exporting(getXML, false);
                                        var readXML = new S06SetData();
                                        readXML.ImportXML(getXML);
                                        readXML.Save(Path.Combine(Path.GetDirectoryName(getXML), $"{Path.GetFileNameWithoutExtension(getXML)}.set"), true);
                                    } else { Status = StatusMessages.ex_InvalidFile(getXML, "SET", false); }
                            } catch (Exception ex) {
                                MessageBox.Show($"{SystemMessages.ex_SETExportError}\n\n{ex}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;
                        case "Export":
                            try {
                                string getSET = Browsers.CommonBrowser(false, SystemMessages.tl_SelectFile, Filters.SET);
                                if (getSET != string.Empty)
                                    if (File.Exists(getSET) && Verification.VerifyMagicNumberExtended(getSET)) {
                                        Status = StatusMessages.cmn_Importing(getSET, false);
                                        var readSET = new S06SetData();
                                        readSET.Load(getSET);
                                        readSET.ExportXML(Path.Combine(Path.GetDirectoryName(getSET), $"{Path.GetFileNameWithoutExtension(getSET)}.xml"));
                                    } else { Status = StatusMessages.ex_InvalidFile(getSET, "SET", false); }
                            } catch (Exception ex) {
                                MessageBox.Show($"{SystemMessages.ex_SETExportError}\n\n{ex}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;
                    }
            } else new PlacementConverter(this).ShowDialog();
        }

        private void File_Exit_Click(object sender, EventArgs e) { Application.Exit(); }

        private void Lbl_Status_Click(object sender, EventArgs e) {
            Logs.ToolkitSessionLog openLogs = Application.OpenForms["ToolkitSessionLog"] != null ? (Logs.ToolkitSessionLog)Application.OpenForms["ToolkitSessionLog"] : null;
            if (openLogs != null) {
                try {
                    openLogs = (Logs.ToolkitSessionLog)Application.OpenForms["ToolkitSessionLog"];
                    openLogs.Close();
                } catch { }
            }
            
            if (Properties.Settings.Default.env_firstLaunch)
                sessionLog.Remove("Welcome to Sonic '06 Toolkit! Click here to view the Session Log...");
            else
                sessionLog.Remove("Welcome back! Click here to view the Session Log...");
            new Logs.ToolkitSessionLog().Show();
        }

        private void Tm_ResetStatus_Tick(object sender, EventArgs e) {
            lbl_Status.Text = StatusMessages.msg_DefaultStatus;
            tm_ResetStatus.Stop();
        }

        private void File_ExtractISO_Click(object sender, EventArgs e) {
            string getPath = Browsers.CommonBrowser(false, SystemMessages.tl_XISO, Filters.All);

            if (getPath != string.Empty) {
                file_ExtractISO.Enabled = false;
                bw_Worker.RunWorkerCompleted += SingleExtractCompleted;
                bw_Worker.DoWork += SingleExtractDoWork;
                var bwargs = new BwArgs {
                    Source = getPath,
                    Target = Path.Combine(Path.GetDirectoryName(getPath), Path.GetFileNameWithoutExtension(getPath)),
                    SkipSystemUpdate = true,
                    GenerateFileList = false,
                    DeleteIsoOnCompletion = false
                };
                extractQueue = getPath;
                Status = StatusMessages.iso_Extracting(extractQueue, false);
                bw_Worker.RunWorkerAsync(bwargs);
            }
        }

        private void SingleExtractDoWork(object sender, DoWorkEventArgs e) {
            if (!(e.Argument is BwArgs)) {
                e.Cancel = true;
                return;
            }
            var args = e.Argument as BwArgs;
            e.Result = XisoExtractor.ExtractXiso(new XisoOptions {
                Source = args.Source,
                Target = args.Target,
                ExcludeSysUpdate = args.SkipSystemUpdate,
                GenerateFileList = args.GenerateFileList,
                DeleteIsoOnCompletion = args.DeleteIsoOnCompletion
            });
        }

        private void SingleExtractCompleted(object sender, RunWorkerCompletedEventArgs e) {
            bw_Worker.RunWorkerCompleted -= SingleExtractCompleted;
            bw_Worker.DoWork -= SingleExtractDoWork;
            file_ExtractISO.Enabled = true;
            if (XisoExtractor._errorlevel == 0) {
                Status = XisoExtractor.GetLastError(extractQueue);
                SystemSounds.Asterisk.Play();
            } else MessageBox.Show(XisoExtractor.GetLastError(extractQueue), SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            extractQueue = string.Empty;
        }

        public sealed class BwArgs
        {
            internal bool DeleteIsoOnCompletion;
            internal bool GenerateFileList;
            internal bool SkipSystemUpdate;
            internal string Source;
            internal string Target;
        }

        private void Shortcuts_DecompileLUBinDirectory_Click(object sender, EventArgs e) {
            Batch.DecompileLUB(Paths.currentPath, SearchOption.TopDirectoryOnly, "*.lub", false);
        }

        private void Shortcuts_DecompileLUBinSubdirectories_Click(object sender, EventArgs e) {
            Batch.DecompileLUB(Paths.currentPath, SearchOption.AllDirectories, "*.lub", true);
        }

        private void Shortcuts_DecompileMissionLUBinDirectory_Click(object sender, EventArgs e) {
            Batch.DecompileLUB(Paths.currentPath, SearchOption.TopDirectoryOnly, "mission*.lub", false);
        }

        private void shortcuts_DecompileMissionLUBinSubdirectories_Click(object sender, EventArgs e) {
            Batch.DecompileLUB(Paths.currentPath, SearchOption.AllDirectories, "mission*.lub", true);
        }

        private async void Sdk_LuaCompilation_Click(object sender, EventArgs e) {
            if (ModifierKeys == Keys.Shift) {
                string getUserChoice = Chooser.UserChoice.Show("Please select an option...\n\n" +
                                                               "" +
                                                               "Common:\n" +
                                                               "► Compile - Compile a Lua binary with Lua 5.1.\n" +
                                                               "► Decompile - Decompile a Lua binary (experimental).", "Lua Compilation", "Decompile", "Compile");
                if (getUserChoice != "Abort")
                    switch (getUserChoice) {
                        case "Compile":
                            if (Verification.VerifyApplicationIntegrity(Paths.LuaCompiler)) {
                                string getLUBcompile = Browsers.CommonBrowser(false, SystemMessages.tl_SelectFile, Filters.LUB);
                                if (getLUBcompile != string.Empty) {
                                    Status = StatusMessages.lua_Compiling(getLUBcompile, false);
                                    var compile = await ProcessAsyncHelper.ExecuteShellCommand(Paths.LuaCompiler,
                                                        $"-o \"{getLUBcompile}\" \"{getLUBcompile}\"",
                                                        Application.StartupPath,
                                                        100000);
                                    if (compile.Completed)
                                        if (compile.ExitCode != 0)
                                            MessageBox.Show($"{SystemMessages.ex_LUBCompileError}\n\n{compile.Error}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            break;
                        case "Decompile":
                            if (Verification.VerifyApplicationIntegrity(Paths.LuaDecompiler)) {
                                string getLUBdecompile = Browsers.CommonBrowser(false, SystemMessages.tl_SelectFile, Filters.LUB);
                                if (getLUBdecompile != string.Empty)
                                    if (Verification.VerifyMagicNumberExtended(getLUBdecompile)) {
                                        Status = StatusMessages.lua_Decompiling(getLUBdecompile, false);
                                        var decompile = await ProcessAsyncHelper.ExecuteShellCommand("java.exe",
                                                              $"-jar \"{Paths.LuaDecompiler}\" \"{getLUBdecompile}\"",
                                                              Application.StartupPath,
                                                              100000);
                                        if (decompile.Completed)
                                            if (decompile.ExitCode == 0)
                                                File.WriteAllText(getLUBdecompile, decompile.Output);
                                            else
                                                MessageBox.Show($"{SystemMessages.ex_LUBDecompileError}\n\n{decompile.Error}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                            }
                            break;
                    }
            } else new LuaCompilation(this).ShowDialog();
        }

        private async void Sdk_ExecutableDecryptor_Click(object sender, EventArgs e) {
            if (ModifierKeys == Keys.Shift) {
                string getUserChoice = Chooser.UserChoice.Show("Please select an option...\n\n" +
                                                               "" +
                                                               "Common:\n" +
                                                               "► Encrypt - Encrypt an Xbox Executable.\n" +
                                                               "► Decrypt - Decrypt an Xbox Executable.", "Executable Modification", "Decrypt", "Encrypt");
                if (getUserChoice != "Abort")
                    switch (getUserChoice) {
                        case "Encrypt":
                            string getXEXencrypt = Browsers.CommonBrowser(false, SystemMessages.tl_SelectFile, Filters.XEX);
                            if (getXEXencrypt != string.Empty)
                                if (Verification.VerifyMagicNumberCommon(getXEXencrypt)) {
                                    Status = StatusMessages.cmn_Processing(getXEXencrypt, false);
                                    var process = await ProcessAsyncHelper.ExecuteShellCommand(Paths.XexTool,
                                                        $"-e e \"{getXEXencrypt}\"",
                                                        Application.StartupPath,
                                                        100000);
                                    if (process.Completed)
                                        if (process.ExitCode != 0)
                                            MessageBox.Show($"{SystemMessages.ex_XEXModificationError}\n\n{process.Output}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            break;
                        case "Decrypt":
                            string getXEXdecrypt = Browsers.CommonBrowser(false, SystemMessages.tl_SelectFile, Filters.XEX);
                            if (getXEXdecrypt != string.Empty)
                                if (Verification.VerifyMagicNumberCommon(getXEXdecrypt)) {
                                    Status = StatusMessages.cmn_Processing(getXEXdecrypt, false);
                                    var process = await ProcessAsyncHelper.ExecuteShellCommand(Paths.XexTool,
                                                        $"-e u \"{getXEXdecrypt}\"",
                                                        Application.StartupPath,
                                                        100000);
                                    if (process.Completed)
                                        if (process.ExitCode != 0)
                                            MessageBox.Show($"{SystemMessages.ex_XEXModificationError}\n\n{process.Output}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            break;
                    }
            } else new ExecutableModification(this).ShowDialog();
        }

        private async void Sdk_SonicSoundStudio_Click(object sender, EventArgs e) {
            if (ModifierKeys == Keys.Shift) {
                string getUserChoice = Chooser.UserChoice.Show("Please select an option...\n\n" +
                                                               "" +
                                                               "Common:\n" +
                                                               "► Encode - Encode audio to ADX/AT3/XMA.\n" +
                                                               "► Decode - Decode audio to WAV.", "Sonic Sound Studio", "Decode", "Encode");
                if (getUserChoice != "Abort")
                    switch (getUserChoice) {
                        case "Encode":
                            string getAudio = Browsers.CommonBrowser(false, SystemMessages.tl_SelectFile, Filters.AudioEncodeCompat);
                            if (getAudio != string.Empty) {
                                string getAudioencode = Browsers.SaveFile(SystemMessages.tl_SelectFile, Filters.AudioEncode);
                                if (getAudioencode != string.Empty) {
                                    var process = new ProcessAsyncHelper.ProcessResult();
                                    if (File.Exists(getAudio)) {
                                        if (Path.GetExtension(getAudioencode).ToLower() != ".xma") {
                                            if (Path.GetExtension(getAudio).ToLower() == ".wav") {
                                                if (Path.GetExtension(getAudioencode).ToLower() == ".adx") {
                                                    try {
                                                        Status = StatusMessages.cmn_Converting(getAudio, "ADX", false);
                                                        byte[] wavFile = File.ReadAllBytes(getAudio);
                                                        AudioData audio = new WaveReader().Read(wavFile);
                                                        byte[] adxFile = new AdxWriter().GetFile(audio);
                                                        File.WriteAllBytes(getAudioencode, adxFile);
                                                    } catch { Status = StatusMessages.cmn_ConvertFailed(getAudio, "ADX", false); }
                                                } else if (Path.GetExtension(getAudioencode).ToLower() == ".at3") {
                                                    process = await ProcessAsyncHelper.ExecuteShellCommand(Paths.AT3Tool,
                                                                    $"-e \"{getAudio}\" \"{getAudioencode}\"",
                                                                    Path.GetDirectoryName(getAudioencode),
                                                                    100000);
                                                    if (process.ExitCode != 0)
                                                        Status = StatusMessages.cmn_ConvertFailed(getAudio, "AT3", false);
                                                }
                                            } else if (Path.GetExtension(getAudio).ToLower() == ".mp3") {
                                                if (Path.GetExtension(getAudioencode).ToLower() == ".adx") {
                                                    try {
                                                        Status = StatusMessages.cmn_Converting(getAudio, "ADX", false);
                                                        string wav = MP3.CreateTemporaryWAV(getAudio);
                                                        byte[] wavFile = File.ReadAllBytes(wav);
                                                        AudioData audio = new WaveReader().Read(wavFile);
                                                        byte[] adxFile = new AdxWriter().GetFile(audio);
                                                        File.WriteAllBytes(getAudioencode, adxFile);
                                                        File.Delete(wav);
                                                    } catch { Status = StatusMessages.cmn_ConvertFailed(getAudio, "ADX", false); }
                                                } else if (Path.GetExtension(getAudioencode).ToLower() == ".at3") {
                                                    Status = StatusMessages.cmn_Converting(getAudio, "AT3", false);
                                                    string wav = MP3.CreateTemporaryWAV(getAudio);
                                                    process = await ProcessAsyncHelper.ExecuteShellCommand(Paths.AT3Tool,
                                                                    $"-e \"{wav}\" \"{getAudioencode}\"",
                                                                    Path.GetDirectoryName(getAudioencode),
                                                                    100000);
                                                    if (process.ExitCode != 0)
                                                        Status = StatusMessages.cmn_ConvertFailed(getAudio, "AT3", false);
                                                    else File.Delete(wav);
                                                }
                                            }
                                        } else {
                                            string wav = string.Empty;
                                            Status = StatusMessages.cmn_Converting(getAudio, "XMA", false);
                                            try {
                                                string preXMA = Path.Combine(Path.GetDirectoryName(getAudio), $"{Path.GetFileNameWithoutExtension(getAudio)}.xma"); 
                                                if (File.Exists(preXMA)) File.Delete(preXMA);
                                            } catch { }

                                            if (Path.GetExtension(getAudio).ToLower() == ".mp3") {
                                                wav = MP3.CreateTemporaryWAV(getAudio);
                                                process = await ProcessAsyncHelper.ExecuteShellCommand(Paths.XMATool,
                                                                $"\"{wav}\" /L",
                                                                Path.GetDirectoryName(getAudioencode),
                                                                100000);
                                            } else if (Path.GetExtension(getAudio).ToLower() == ".wav")
                                                process = await ProcessAsyncHelper.ExecuteShellCommand(Paths.XMATool,
                                                                $"\"{getAudio}\" /L",
                                                                Path.GetDirectoryName(getAudioencode),
                                                                100000);

                                            if (process.ExitCode != 0)
                                                Status = StatusMessages.cmn_ConvertFailed(getAudio, "XMA", false);
                                            else {
                                                try {
                                                    if (wav != string.Empty) {
                                                        File.Copy(wav, getAudioencode, true);
                                                        File.Delete(Path.Combine(Path.GetDirectoryName(wav), $"{Path.GetFileNameWithoutExtension(wav)}.xma"));
                                                        File.Delete(wav);
                                                        wav = string.Empty;
                                                    } else
                                                        File.Move(Path.Combine(Path.GetDirectoryName(getAudio), $"{Path.GetFileNameWithoutExtension(getAudio)}.xma"), getAudioencode);
                                                } catch { }
                                                try {
                                                    if (MessageBox.Show("Patch XMA to play in-game?", "Patch XMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                                                        byte[] xmaBytes = File.ReadAllBytes(getAudioencode).ToArray();
                                                        string hexString = BitConverter.ToString(xmaBytes).Replace("-", "");
                                                        if (!hexString.Contains(Properties.Resources.xma_Patch)) {
                                                            ByteArray.ByteArrayToFile(getAudioencode, ByteArray.StringToByteArray(Properties.Resources.xma_Patch));
                                                            Status = StatusMessages.xma_Patched(getAudioencode, false);
                                                        } else break;
                                                    }
                                                } catch { Status = StatusMessages.xma_EncodeFooterError(getAudio, false); return; }
                                            }
                                        }
                                    }
                                }
                            } else return;
                            break;
                        case "Decode":
                            string getAudiodecode = Browsers.CommonBrowser(false, SystemMessages.tl_SelectFile, Filters.AudioEncode);
                            if (getAudiodecode != string.Empty) {
                                if (File.Exists(getAudiodecode)) {
                                    Status = StatusMessages.cmn_Converting(getAudiodecode, "WAV", false);
                                    if (Path.GetExtension(getAudiodecode).ToLower() == ".adx") {
                                        try {
                                            byte[] adxFile = File.ReadAllBytes(getAudiodecode);
                                            AudioData audio = new AdxReader().Read(adxFile);
                                            byte[] wavFile = new WaveWriter().GetFile(audio);
                                            File.WriteAllBytes(Path.Combine(Path.GetDirectoryName(getAudiodecode), $"{Path.GetFileNameWithoutExtension(getAudiodecode)}.wav"), wavFile);
                                        } catch { Status = StatusMessages.cmn_ConvertFailed(getAudiodecode, "WAV", false); }
                                    } else if (Path.GetExtension(getAudiodecode).ToLower() == ".at3") {
                                        var process = await ProcessAsyncHelper.ExecuteShellCommand(Paths.AT3Tool,
                                                            $"-d \"{getAudiodecode}\" \"{Path.Combine(Path.GetDirectoryName(getAudiodecode), Path.GetFileNameWithoutExtension(getAudiodecode))}.wav\"",
                                                            Application.StartupPath,
                                                            100000);
                                        if (process.ExitCode != 0)
                                            Status = StatusMessages.cmn_ConvertFailed(getAudiodecode, "WAV", false);
                                    } else if (Path.GetExtension(getAudiodecode).ToLower() == ".csb") {
                                        try {
                                            Status = StatusMessages.cmn_Unpacking(getAudiodecode, false);
                                            var extractor = new DataExtractor {
                                                MaxThreads = 1,
                                                BufferSize = 4096,
                                                EnableThreading = false
                                            };
                                            Status = StatusMessages.cmn_Unpacked(getAudiodecode, false);
                                            Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(getAudiodecode), Path.GetFileNameWithoutExtension(getAudiodecode)));
                                            CSBTools.ExtractCSBNodes(extractor, getAudiodecode, Path.Combine(Path.GetDirectoryName(getAudiodecode), Path.GetFileNameWithoutExtension(getAudiodecode)));
                                            extractor.Run();
                                        } catch { Status = StatusMessages.cmn_UnpackFailed(getAudiodecode, false); }
                                    } else if (Path.GetExtension(getAudiodecode).ToLower() == ".mp3") {
                                        using (Mp3FileReader mp3 = new Mp3FileReader(getAudiodecode))
                                            using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
                                                WaveFileWriter.CreateWaveFile(Path.Combine(Path.GetDirectoryName(getAudiodecode), $"{getAudiodecode}.wav"), pcm);
                                    } else if (Path.GetExtension(getAudiodecode).ToLower() == ".xma") {
                                        try {
                                            byte[] xmaBytes = File.ReadAllBytes(getAudiodecode).ToArray();
                                            string hexString = BitConverter.ToString(xmaBytes).Replace("-", "");
                                            if (hexString.Contains(Properties.Resources.xma_Patch)) {
                                                FileInfo fi = new FileInfo(getAudiodecode);
                                                FileStream fs = fi.Open(FileMode.Open);
                                                long bytesToDelete = 56;
                                                fs.SetLength(Math.Max(0, fi.Length - bytesToDelete));
                                                fs.Close();
                                            }
                                        } catch { Status = StatusMessages.xma_DecodeFooterError(getAudiodecode, false); return; }
                                        try {
                                            if (File.Exists($"{Path.Combine(Path.GetDirectoryName(getAudiodecode), Path.GetFileNameWithoutExtension(getAudiodecode))}.wav"))
                                                File.Delete($"{Path.Combine(Path.GetDirectoryName(getAudiodecode), Path.GetFileNameWithoutExtension(getAudiodecode))}.wav");
                                        } catch { }
                                        var process = await ProcessAsyncHelper.ExecuteShellCommand(Paths.XMADecoder,
                                                            $"\"{getAudiodecode}\"",
                                                            Path.GetDirectoryName(getAudiodecode),
                                                            100000);
                                        if (process.ExitCode != 0)
                                            Status = StatusMessages.cmn_ConvertFailed(getAudiodecode, "WAV", false);
                                    }
                                }
                            }
                            break;
                    }
            } else new SonicSoundStudio(this).ShowDialog();
        }

        private async void Sdk_TextureRasteriser_Click(object sender, EventArgs e) {
            if (ModifierKeys == Keys.Shift) {
                string getUserChoice = Chooser.UserChoice.Show("Please select an option...\n\n" +
                                                               "" +
                                                               "Common:\n" +
                                                               "► Encode - Convert a PNG to the DDS format (A8R8G8B8).\n" +
                                                               "► Decode - Convert a DDS to the PNG format.", "Texture Converter", "Decode", "Encode");
                if (getUserChoice != "Abort")
                    switch (getUserChoice) {
                        case "Encode":
                            string getPNG = Browsers.CommonBrowser(false, SystemMessages.tl_SelectFile, Filters.PNG);
                            if (getPNG != string.Empty) {
                                if (File.Exists(getPNG) && Verification.VerifyMagicNumberCommon(getPNG)) {
                                    Status = StatusMessages.cmn_Converting(getPNG, "DDS", false);
                                    var convert = await ProcessAsyncHelper.ExecuteShellCommand(Paths.DDSTool,
                                                        $"-ft dds -srgb -f R8G8B8A8_UNORM \"{getPNG}\" " +
                                                        $"\"{Path.GetFileNameWithoutExtension(getPNG)}.dds\"",
                                                        Path.GetDirectoryName(getPNG),
                                                        100000);
                                    if (convert.Completed)
                                        if (convert.ExitCode != 0)
                                            MessageBox.Show($"{SystemMessages.ex_DDSConvertError}\n\n{convert.Output}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            break;
                        case "Decode":
                            string getDDS = Browsers.CommonBrowser(false, SystemMessages.tl_SelectFile, Filters.DDS);
                            if (getDDS != string.Empty) {
                                if (File.Exists(getDDS) && Verification.VerifyMagicNumberCommon(getDDS)) {
                                    Status = StatusMessages.cmn_Converting(getDDS, "PNG", false);
                                    var convert = await ProcessAsyncHelper.ExecuteShellCommand(Paths.DDSTool,
                                                        $"-ft png -srgb \"{getDDS}\" " +
                                                        $"\"{Path.GetFileNameWithoutExtension(getDDS)}.png\"",
                                                        Path.GetDirectoryName(getDDS),
                                                        100000);
                                    if (convert.Completed)
                                        if (convert.ExitCode != 0)
                                            MessageBox.Show($"{SystemMessages.ex_DDSConvertError}\n\n{convert.Output}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            break;
                    }
            } else new TextureConverter(this).ShowDialog();
        }

        private void Sdk_ArchiveMerger_Click(object sender, EventArgs e) {
            if (ModifierKeys == Keys.Shift) {
                string ARC1 = string.Empty;
                string ARC2 = string.Empty;
                string Output = string.Empty;

                string getARC1 = Browsers.CommonBrowser(false, SystemMessages.tl_SelectArchive, Filters.Archives);
                if (getARC1 != string.Empty) {
                    ARC1 = getARC1;
                    string getARC2 = Browsers.CommonBrowser(false, SystemMessages.tl_SelectArchive, Filters.Archives);
                    if (getARC2 != string.Empty) {
                        ARC2 = getARC2;
                        string getOutput = Browsers.SaveFile(SystemMessages.tl_RepackAs, Filters.Archives);
                        if (getOutput != string.Empty) {
                            Output = getOutput;
                            Status = StatusMessages.cmn_Processing(Output, false);
                            ArchiveMerger.MergeARCs(ARC1, ARC2, Output);
                            Status = StatusMessages.cmn_Repacked(Output, false);
                        } else return;
                    } else return;
                } else return;
            } else new ArchiveMerger(this).ShowDialog();
        }

        private async void Shortcuts_UnpackARCtoToolkit_Click(object sender, EventArgs e) {
            string location = Paths.currentPath;
            foreach (string ARC in Directory.GetFiles(location, "*.arc", SearchOption.TopDirectoryOnly))
                if (File.Exists(ARC) && Verification.VerifyMagicNumberCommon(ARC)) {
                    try {
                        string failsafeCheck = Path.GetRandomFileName();
                        string arcBuildSession = Path.Combine(Program.applicationData, Paths.Archives, Program.sessionID.ToString(), failsafeCheck);

                        if (File.Exists(ARC)) {
                            Directory.CreateDirectory(arcBuildSession);
                            File.Copy(ARC, Path.Combine(arcBuildSession, Path.GetFileName(ARC)), true);
                        }

                        Status = StatusMessages.cmn_Unpacking(ARC, false);
                        await ProcessAsyncHelper.ExecuteShellCommand(Paths.Unpack,
                              $"\"{Path.Combine(arcBuildSession, Path.GetFileName(ARC))}\"",
                              Application.StartupPath,
                              100000);

                        //Writes metadata to the unpacked directory to ensure the original path is remembered.
                        var metadataWrite = File.Create(Path.Combine(arcBuildSession, "metadata.ini"));
                        byte[] metadataSession = new UTF8Encoding(true).GetBytes(ARC);
                        metadataWrite.Write(metadataSession, 0, metadataSession.Length);
                        metadataWrite.Close();

                        if (unifytb_Main.SelectedTab.ToolTipText == string.Empty) ResetTab(false);
                        else NewTab(false);

                        CurrentARC().Navigate(Path.Combine(arcBuildSession, Path.GetFileNameWithoutExtension(ARC)));
                        unifytb_Main.SelectedTab.Text = Path.GetFileName(ARC);
                        unifytb_Main.SelectedTab.ToolTipText = failsafeCheck;
                        repackBuildSession = Path.Combine(Program.applicationData, Paths.Archives, Program.sessionID.ToString(), unifytb_Main.SelectedTab.ToolTipText);

                        string metadata = File.ReadAllText(Path.Combine(repackBuildSession, "metadata.ini"));
                        Text = SystemMessages.tl_Exploring(metadata);
                        Status = StatusMessages.cmn_Unpacked(ARC, false);
                    } catch { Status = StatusMessages.cmn_UnpackFailed(ARC, false); }
                }
        }

        private async void Shortcuts_UnpackARCtoDirectory_Click(object sender, EventArgs e) {
            string location = Paths.currentPath;
            foreach (string ARC in Directory.GetFiles(location, "*.arc", SearchOption.TopDirectoryOnly))
                if (File.Exists(ARC) && Verification.VerifyMagicNumberCommon(ARC)) {
                    try {
                        Status = StatusMessages.cmn_Unpacking(ARC, false);
                        string dirPath = Path.Combine(location, Path.GetFileNameWithoutExtension(ARC));
                        await ProcessAsyncHelper.ExecuteShellCommand(Paths.Unpack,
                              $"\"{ARC}\"",
                              Application.StartupPath,
                              100000);
                        Status = StatusMessages.cmn_Unpacked(ARC, false);
                    } catch { Status = StatusMessages.cmn_UnpackFailed(ARC, false); }
                }
        }

        private void Shortcuts_DecodeADXinDirectory_Click(object sender, EventArgs e) {
            Batch.DecodeADX(Paths.currentPath, SearchOption.TopDirectoryOnly, false);
        }

        private void Shortcuts_DecodeADXinSubdirectories_Click(object sender, EventArgs e) {
            Batch.DecodeADX(Paths.currentPath, SearchOption.AllDirectories, true);
        }

        private void Shortcuts_DecodeAT3inDirectory_Click(object sender, EventArgs e) {
            Batch.DecodeAT3(Paths.currentPath, SearchOption.TopDirectoryOnly, false);
        }

        private void Shortcuts_DecodeAT3inSubdirectories_Click(object sender, EventArgs e) {
            Batch.DecodeAT3(Paths.currentPath, SearchOption.AllDirectories, true);
        }

        private void Shortcuts_UnpackCSBinDirectory_Click(object sender, EventArgs e) {
            Batch.UnpackCSB(false, Paths.currentPath, SearchOption.TopDirectoryOnly, false);
        }

        private void Shortcuts_UnpackCSBinSubdirectories_Click(object sender, EventArgs e) {
            Batch.UnpackCSB(false, Paths.currentPath, SearchOption.AllDirectories, true);
        }

        private void Shortcuts_UnpackCSBinDirectoryToWAV_Click(object sender, EventArgs e) {
            Batch.UnpackCSB(true, Paths.currentPath, SearchOption.TopDirectoryOnly, false);
        }

        private void Shortcuts_UnpackCSBinSubdirectoriesToWAV_Click(object sender, EventArgs e) {
            Batch.UnpackCSB(true, Paths.currentPath, SearchOption.AllDirectories, true);
        }

        private void Shortcuts_ConvertDDSinDirectory_Click(object sender, EventArgs e) {
            Batch.DecodeDDS(Paths.currentPath, SearchOption.TopDirectoryOnly, false);
        }

        private void Shortcuts_ConvertDDSinSubdirectories_Click(object sender, EventArgs e) {
            Batch.DecodeDDS(Paths.currentPath, SearchOption.AllDirectories, true);
        }

        private void Shortcuts_DecodeSETinDirectory_Click(object sender, EventArgs e) {
            Batch.DecodeSET(Paths.currentPath, SearchOption.TopDirectoryOnly, false);
        }

        private void Shortcuts_DecodeSETinSubdirectories_Click(object sender, EventArgs e) {
            Batch.DecodeSET(Paths.currentPath, SearchOption.AllDirectories, true);
        }

        private void Shortcuts_DecryptXEXinDirectory_Click(object sender, EventArgs e) {
            Batch.DecryptXEX(Paths.currentPath, SearchOption.TopDirectoryOnly, false);
        }

        private void Shortcuts_DecryptXEXinSubdirectories_Click(object sender, EventArgs e) {
            Batch.DecryptXEX(Paths.currentPath, SearchOption.AllDirectories, true);
        }

        private void Shortcuts_DecodeXMAinDirectory_Click(object sender, EventArgs e) {
            Batch.DecodeXMA(Paths.currentPath, SearchOption.TopDirectoryOnly, false);
        }

        private void Shortcuts_DecodeXMAinSubdirectories_Click(object sender, EventArgs e) {
            Batch.DecodeXMA(Paths.currentPath, SearchOption.AllDirectories, true);
        }

        private async void Sdk_TextEditor_Click(object sender, EventArgs e) {
            if (ModifierKeys == Keys.Shift) {
                string getUserChoice = Chooser.UserChoice.Show("Please select an option...\n\n" +
                                                               "" +
                                                               "Common:\n" +
                                                               "► Import - Encode a XML to MST.\n" +
                                                               "► Export - Decode a MST to XML.", "Text Encoding", "Export", "Import");
                if (getUserChoice != "Abort")
                    switch (getUserChoice) {
                        case "Import":
                            string getXML = Browsers.CommonBrowser(false, SystemMessages.tl_SelectFile, Filters.XML);
                            if (getXML != string.Empty) {
                                if (File.Exists(getXML) && Verification.VerifyXML(getXML, "MST")) {
                                    Status = StatusMessages.cmn_Exporting(getXML, false);
                                    var export = await ProcessAsyncHelper.ExecuteShellCommand(Paths.MSTTool,
                                                       $"\"{getXML}\"",
                                                       Application.StartupPath,
                                                       100000);
                                    if (export.Completed)
                                        if (export.ExitCode != 0)
                                            MessageBox.Show($"{SystemMessages.ex_XMLImportError}\n\n{export.Output}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            break;
                        case "Export":
                            string getMST = Browsers.CommonBrowser(false, SystemMessages.tl_SelectFile, Filters.MST);
                            if (getMST != string.Empty) {
                                if (File.Exists(getMST) && Verification.VerifyMagicNumberExtended(getMST)) {
                                    Status = StatusMessages.cmn_Exporting(getMST, false);
                                    var export = await ProcessAsyncHelper.ExecuteShellCommand(Paths.MSTTool,
                                                       $"\"{getMST}\"",
                                                       Application.StartupPath,
                                                       100000);
                                    if (export.Completed)
                                        if (export.ExitCode != 0)
                                            MessageBox.Show($"{SystemMessages.ex_MSTExportError}\n\n{export.Output}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            break;
                    }
            } else new TextEncoding(this).ShowDialog();
        }

        private void Shortcuts_DecodeMSTinDirectory_Click(object sender, EventArgs e) {
            Batch.DecodeMST(Paths.currentPath, SearchOption.TopDirectoryOnly, false);
        }

        private void Shortcuts_DecodeMSTinSubdirectories_Click(object sender, EventArgs e) {
            Batch.DecodeMST(Paths.currentPath, SearchOption.AllDirectories, true);
        }

        private async void Sdk_ModelAnimationExporter_Click(object sender, EventArgs e) {
            if (ModifierKeys == Keys.Shift) {
                string getUserChoice = Chooser.UserChoice.Show("Please select an option...\n\n" +
                                                               "" +
                                                               "Common:\n" +
                                                               "► Model - Decode a XNO mesh to DAE.\n" +
                                                               "► Animation - Decode a XNM animation to DAE.", "Model/Animation Exporter", "Model", "Animation");
                if (getUserChoice != "Abort")
                    switch (getUserChoice) {
                        case "Model":
                            string getXNO = Browsers.CommonBrowser(false, SystemMessages.tl_SelectFile, Filters.XNO);
                            if (getXNO != string.Empty)
                                if (File.Exists(getXNO) && Verification.VerifyMagicNumberCommon(getXNO)) {
                                    Status = StatusMessages.cmn_Converting(getXNO, "DAE", false);
                                    var convert = await ProcessAsyncHelper.ExecuteShellCommand(Paths.XNODecoder,
                                                        $"\"{getXNO}\"",
                                                        Path.GetDirectoryName(getXNO),
                                                        100000);
                                    if (convert.Completed)
                                        if (convert.ExitCode != 0)
                                            MessageBox.Show(SystemMessages.ex_XNOConvertError, SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            break;
                        case "Animation":
                            string XNO = string.Empty;

                            string getXNObase = Browsers.CommonBrowser(false, SystemMessages.tl_SelectFile, Filters.XNO);
                            if (getXNObase != string.Empty) {
                                XNO = getXNObase;
                                string getXNM = Browsers.CommonBrowser(false, SystemMessages.tl_SelectFile, Filters.XNM);
                                if (getXNM != string.Empty) {
                                        if (File.Exists(getXNM) && Verification.VerifyMagicNumberCommon(getXNM)) {
                                            Status = StatusMessages.cmn_Converting(getXNM, "DAE", false);
                                            var convert = await ProcessAsyncHelper.ExecuteShellCommand(Paths.XNODecoder,
                                                                $"\"{XNO}\" \"{getXNM}\"",
                                                                Path.GetDirectoryName(getXNM),
                                                                100000);
                                            if (convert.Completed)
                                                if (convert.ExitCode != 0)
                                                    MessageBox.Show(SystemMessages.ex_XNOConvertError, SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                } else return;
                            } else return;
                            break;
                    }
            } else new XNOTool(this).ShowDialog();
        }

        private void Shortcuts_DecodeXNOinDirectory_Click(object sender, EventArgs e) {
            Batch.DecodeXNO(Paths.currentPath, SearchOption.TopDirectoryOnly, false);
        }

        private void shortcuts_DecodeXNOinSubdirectories_Click(object sender, EventArgs e) {
            Batch.DecodeXNO(Paths.currentPath, SearchOption.AllDirectories, true);
        }

        private async void Sdk_CollisionGenerator_Click(object sender, EventArgs e) {
            if (ModifierKeys == Keys.Shift) {
                string getUserChoice = Chooser.UserChoice.Show("Please select an option...\n\n" +
                                                               "" +
                                                               "Common:\n" +
                                                               "► Import - Encode a OBJ to the BIN format.\n" +
                                                               "► Export - Decode a BIN to the OBJ format.", "Collision Generator", "Export", "Import");
                if (getUserChoice != "Abort")
                    switch (getUserChoice) {
                        case "Import":
                            string getOBJ = Browsers.CommonBrowser(false, SystemMessages.tl_SelectFile, Filters.OBJ);
                            if (getOBJ != string.Empty)
                                if (File.Exists(getOBJ)) {
                                    string[] getTags = File.ReadAllLines(getOBJ);
                                    int i = 0;

                                    foreach (string line in getTags)
                                        if (line.Contains("_at_"))
                                            i++;

                                    if (i != 0)
                                        if (MessageBox.Show(SystemMessages.msg_CollisionTagsDetected(i, Path.GetFileName(getOBJ)), SystemMessages.tl_CollisionTagsDetected, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                                            int lineNum = 0;
                                            foreach (string line in getTags) {
                                                if (line.Contains("_at_")) {
                                                    string temp = line.Replace("_at_", "@");
                                                    getTags[lineNum] = temp;
                                                }
                                                lineNum++;
                                            }
                                            File.WriteAllLines(getOBJ, getTags);
                                        }

                                    Status = StatusMessages.cmn_Importing(Path.GetFileName(getOBJ), false);
                                    var import = await ProcessAsyncHelper.ExecuteShellCommand(Paths.BINEncoder,
                                                       $"\"{getOBJ}\"",
                                                       Path.GetDirectoryName(getOBJ),
                                                       100000);
                                    if (import.Completed)
                                        if (import.ExitCode != 0)
                                            MessageBox.Show($"{SystemMessages.ex_OBJImportError}\n\n{import.Output}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            break;
                        case "Export":
                            if (Verification.VerifyApplicationIntegrity(Paths.BINDecoder)) {
                                string getBIN = Browsers.CommonBrowser(false, SystemMessages.tl_SelectFile, Filters.BIN);
                                if (getBIN != string.Empty)
                                    if (File.Exists(getBIN) && Verification.VerifyMagicNumberExtended(getBIN)) {
                                        Status = StatusMessages.cmn_Exporting(Path.GetFileName(getBIN), false);
                                        var export = await ProcessAsyncHelper.ExecuteShellCommand(Paths.BINDecoder,
                                                           $"\"{getBIN}\"",
                                                           Path.GetDirectoryName(getBIN),
                                                           100000);
                                        if (export.Completed)
                                            if (export.ExitCode != 0)
                                                MessageBox.Show($"{SystemMessages.ex_BINExportError}\n\n{export.Output}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                            } else
                                MessageBox.Show(SystemMessages.ex_IntegrityCheckFailed, SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
            } else new CollisionGenerator(this).ShowDialog();
        }

        private void Shortcuts_DecodeBINinDirectory_Click(object sender, EventArgs e) {
            Batch.DecodeBIN(Paths.currentPath, SearchOption.TopDirectoryOnly, false);
        }

        private void Shortcuts_DecodeBINinSubdirectories_Click(object sender, EventArgs e) {
            Batch.DecodeBIN(Paths.currentPath, SearchOption.AllDirectories, true);
        }

        private void Help_About_Click(object sender, EventArgs e) {
            new ToolkitAbout().ShowDialog();
        }

        private void Advanced_ResetToolkit_Click(object sender, EventArgs e) {
            DialogResult reset = MessageBox.Show("This will completely reset Sonic '06 Toolkit...\n\n" +
                                                 "" +
                                                 "The following data will be erased:\n" +
                                                 "► Your selected settings.\n" +
                                                 "► Archives in application data.\n" +
                                                 "► Tools in application data.\n\n" +
                                                 "" +
                                                 "Are you sure you want to continue?",
                                                 "Reset?",
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Warning);

            if (reset == DialogResult.Yes) {
                var toolkitData = new DirectoryInfo(Path.Combine(Program.applicationData, Paths.Root));
                try {
                    if (Directory.Exists(Path.Combine(Program.applicationData, Paths.Root))) {
                        foreach (FileInfo file in toolkitData.GetFiles())
                            file.Delete();
                        foreach (DirectoryInfo directory in toolkitData.GetDirectories())
                            directory.Delete(true);
                    }
                } catch { }
                Properties.Settings.Default.Reset();
                Program.Restart();
            }
        }

        private void Help_ReportBug_Click(object sender, EventArgs e) {
            Process.Start("https://github.com/HyperPolygon64/Sonic-06-Toolkit/issues/new/choose");
        }

        private void Help_Documentation_Click(object sender, EventArgs e) {
            Process.Start("https://github.com/HyperPolygon64/Sonic-06-Toolkit/wiki");
        }

        private void Preferences_AssociateARCs_CheckedChanged(object sender, EventArgs e) {
            if (preferences_AssociateARCs.Checked) {
                if (Program.RunningAsAdmin()) {
                    try {
                        var key = Registry.ClassesRoot.OpenSubKey(".arc_auto_file\\shell\\open\\command");

                        key = Registry.ClassesRoot.OpenSubKey(".arc_auto_file", true);
                        if (key == null)
                            key = Registry.ClassesRoot.CreateSubKey(".arc_auto_file");
                        var prevkey = key;
                        key = key.OpenSubKey("shell", true);
                        if (key == null)
                            key = prevkey.CreateSubKey("shell");
                        prevkey = key;
                        key = key.OpenSubKey("open", true);
                        if (key == null)
                            key = prevkey.CreateSubKey("open");
                        prevkey = key;
                        key = key.OpenSubKey("command", true);
                        if (key == null)
                            key = prevkey.CreateSubKey("command");
                        key.SetValue("", $"\"{Application.ExecutablePath}\" \"%1\"");
                        key.Close();

                        Windows.SetAssociation(".arc");
                    } catch (Exception ex) { MessageBox.Show($"{SystemMessages.ex_RegistryError}\n\n{ex}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error); }
                } else {
                    if (!registryWarnWorkaround) {
                        DialogResult admin = MessageBox.Show(SystemMessages.msg_RestartAsAdmin, SystemMessages.tl_DefaultTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (admin == DialogResult.Yes) {
                            var runAsAdmin = new ProcessStartInfo(Application.ExecutablePath);
                            runAsAdmin.Verb = "runas";
                            if (Process.Start(runAsAdmin) != null) { Application.Exit(); }
                        } else {
                            registryWarnWorkaround = true;
                            preferences_AssociateARCs.Checked = false;
                        }
                    } else registryWarnWorkaround = false;
                }
            } else {
                if (Program.RunningAsAdmin()) {
                    try {
                        var key = Registry.ClassesRoot.OpenSubKey(".arc_auto_file\\shell\\open\\command");

                        key = Registry.ClassesRoot.OpenSubKey(".arc_auto_file", true);
                        if (key == null)
                            key = Registry.ClassesRoot.CreateSubKey(".arc_auto_file");
                        var prevkey = key;
                        key = key.OpenSubKey("shell", true);
                        if (key == null)
                            key = prevkey.CreateSubKey("shell");
                        prevkey = key;
                        key = key.OpenSubKey("open", true);
                        if (key == null)
                            key = prevkey.CreateSubKey("open");
                        prevkey = key;
                        key = key.OpenSubKey("command", true);
                        if (key == null)
                            key = prevkey.CreateSubKey("command");
                        key.SetValue("", "");
                        key.Close();

                        Windows.SetAssociation(".arc");
                    } catch (Exception ex) { MessageBox.Show($"{SystemMessages.ex_RegistryError}\n\n{ex}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error); }
                } else {
                    if (!registryWarnWorkaround) {
                        DialogResult admin = MessageBox.Show(SystemMessages.msg_RestartAsAdmin, SystemMessages.tl_DefaultTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        if (admin == DialogResult.Yes) {
                            var runAsAdmin = new ProcessStartInfo(Application.ExecutablePath);
                            runAsAdmin.Verb = "runas";
                            if (Process.Start(runAsAdmin) != null) { Application.Exit(); }
                        } else {
                            registryWarnWorkaround = true;
                            preferences_AssociateARCs.Checked = true;
                        }
                    } else registryWarnWorkaround = false;
                }
            }
        }

        private void Help_CheckForUpdates_Click(object sender, EventArgs e) {
            if (serverStatus == "offline") { MessageBox.Show("Unable to establish a connection to SEGA Carnival.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else if (serverStatus == "down") { MessageBox.Show("The update servers are currently undergoing maintenance. Apologies for the inconvenience.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else Updater.CheckForUpdates(versionNumber, "https://segacarnival.com/hyper/updates/sonic-06-toolkit/Sonic_06_Toolkit.zip", "https://segacarnival.com/hyper/updates/sonic-06-toolkit/latest_master.txt", true);
        }

        private void Main_Shown(object sender, EventArgs e) {
            if (!Properties.Settings.Default.env_updaterDisabled &&
                !versionNumber.Contains("beta")                  &&
                !versionNumber.Contains("alpha")                 &&
                !versionNumber.Contains("indev")                 &&
                !versionNumber.Contains("test"))
                    Updater.CheckForUpdates(versionNumber, "https://segacarnival.com/hyper/updates/sonic-06-toolkit/Sonic_06_Toolkit.zip", "https://segacarnival.com/hyper/updates/sonic-06-toolkit/latest_master.txt", false);
        }

        private void Preferences_DisableGameDirectory_CheckedChanged(object sender, EventArgs e) {
            if (preferences_DisableGameDirectory.Checked) {
                lbl_SetDefault.Visible = false;
                Properties.Settings.Default.env_gameDirDisabled = true;
            } else {
                lbl_SetDefault.Visible = true;
                Properties.Settings.Default.env_gameDirDisabled = false;
            }
        }

        private void Preferences_DisableSoftwareUpdater_CheckedChanged(object sender, EventArgs e) {
            if (preferences_DisableSoftwareUpdater.Checked)
                Properties.Settings.Default.env_updaterDisabled = true;
            else
                Properties.Settings.Default.env_updaterDisabled = false;
        }

        private void Help_GitHub_Click(object sender, EventArgs e) {
            Process.Start("https://github.com/HyperPolygon64/Sonic-06-Toolkit/tree/3.0");
        }

        private void Btn_SessionID_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left)
                Process.Start(Path.Combine(Program.applicationData, Paths.Root));
            else if (e.Button == MouseButtons.Right)
                Process.Start(Application.StartupPath);
        }

        private void Advanced_ResetLog_Click(object sender, EventArgs e) {
            Logs.ToolkitSessionLog openLogs = Application.OpenForms["ToolkitSessionLog"] != null ? (Logs.ToolkitSessionLog)Application.OpenForms["ToolkitSessionLog"] : null;
            if (openLogs != null) {
                try {
                    openLogs = (Logs.ToolkitSessionLog)Application.OpenForms["ToolkitSessionLog"];
                    openLogs.Close();
                }
                catch { }
            }
            
            if (Properties.Settings.Default.env_firstLaunch)
                sessionLog.Remove("Welcome to Sonic '06 Toolkit! Click here to view the Session Log...");
            else
                sessionLog.Remove("Welcome back! Click here to view the Session Log...");
            Properties.Settings.Default.log_X = Properties.Settings.Default.log_Y = 25;
            Properties.Settings.Default.log_refreshTimer = 1000;
            Properties.Settings.Default.log_timestamps = true;
            sessionLog.Clear();
            new Logs.ToolkitSessionLog().Show();
        }

        private async void Shortcuts_UnpackARCforModManager_Click(object sender, EventArgs e) {
            if (MessageBox.Show(SystemMessages.msg_UnpackForModManager, SystemMessages.tl_AreYouSure, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
                string location = Paths.currentPath;
                foreach (string ARC in Directory.GetFiles(location, "*.arc", SearchOption.TopDirectoryOnly))
                    if (File.Exists(ARC) && Verification.VerifyMagicNumberCommon(ARC)) {
                        try {
                            Status = StatusMessages.cmn_Unpacking(ARC, false);
                            string dirPath = Path.Combine(location, Path.GetFileNameWithoutExtension(ARC));
                            await ProcessAsyncHelper.ExecuteShellCommand(Paths.Unpack,
                                  $"\"{ARC}\"",
                                  Application.StartupPath,
                                  100000);
                            File.Delete(ARC);
                            Directory.Move(dirPath, $"{dirPath}.arc");
                            Status = StatusMessages.cmn_Unpacked(ARC, false);
                        } catch { Status = StatusMessages.cmn_UnpackFailed(ARC, false); }
                    }
            }
        }

        private async void File_RepackAll_Click(object sender, EventArgs e) {
            string metadata = string.Empty;

            try {
                foreach (TabPage tab in unifytb_Main.TabPages) {
                    if (!tab.ToolTipText.StartsWith("Zm9sZGVy")) {
                        repackBuildSession = Path.Combine(Program.applicationData, Paths.Archives, Program.sessionID.ToString(), tab.ToolTipText);

                        metadata = File.ReadAllText(Path.Combine(repackBuildSession, "metadata.ini"));
                        Status = StatusMessages.cmn_Repacking(metadata, false);
                        await ProcessAsyncHelper.ExecuteShellCommand(Paths.Repack,
                              $"\"{Path.Combine(repackBuildSession, Path.GetFileNameWithoutExtension(metadata))}\"",
                              Application.StartupPath,
                              100000);
                        if (File.Exists(Path.Combine(repackBuildSession, Path.GetFileName(metadata))))
                            File.Copy(Path.Combine(repackBuildSession, Path.GetFileName(metadata)), metadata, true); //Copies the repacked ARC back to the original location.
                        Status = StatusMessages.cmn_Repacked(metadata, false);
                    }
                }
            } catch { Status = StatusMessages.cmn_RepackFailed(metadata, false); }
        }
    }
}
