using System;
using System.IO;
using System.Web;
using System.Text;
using System.Linq;
using Toolkit.Text;
using Toolkit.Tools;
using HedgeLib.Sets;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
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
        public static string versionNumber = "Version 3.0-alpha-251019r1"; // Defines the version number to be used globally
        public static List<string> sessionLog = new List<string>();
        public static string repackBuildSession = string.Empty;
        string extractQueue = string.Empty;
        bool folderMenu = false;

        public Main(int sessionID) {
            InitializeComponent();
            Text = SystemMessages.tl_DefaultTitleVersion;
            Status = "Welcome to Sonic '06 Toolkit! Click here to view the Session Log...";
            btn_SessionID.Text = sessionID.ToString();
            tm_CheapFix.Start();
            NewTab(true);
        }

        public string Status {
            get { return lbl_Status.Text; }
            set {
                lbl_Status.Text = value;
                sessionLog.Add(value);
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
                        lbl_SetDefault.Visible = true;
                    }
                } else {
                    pic_Logo.Visible = true;
                    lbl_SetDefault.Visible = true;
                }
        }

        private void ResetTab(bool navigateToGame) {
            //Creates a new web browser instance (which hooks into File Explorer).
            CurrentARC().Dispose();
            var nextBrowser = new WebBrowser();
            unifytb_Main.SelectedTab.Text = "New Tab";
            unifytb_Main.SelectedTab.Controls.Add(nextBrowser);
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
                        lbl_SetDefault.Visible = true;
                    }
                } else {
                    pic_Logo.Visible = true;
                    lbl_SetDefault.Visible = true;
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
            if (unifytb_Main.SelectedTab.ToolTipText == string.Empty) {
                if (unifytb_Main.TabPages.Count > 1) {
                    int leftMostTab = unifytb_Main.SelectedIndex - 1;
                    unifytb_Main.TabPages.Remove(unifytb_Main.SelectedTab);
                    unifytb_Main.SelectedIndex = leftMostTab; //Sets the selected tab to the tab to the left of the current selected one after removing it.
                }
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
                if (tabs.Cast<TabPage>().Where((t, i) => mainTab.GetTabRect(i).Contains(e.Location)).First().ToolTipText == string.Empty) {
                    if (unifytb_Main.TabPages.Count > 1)
                        tabs.Remove(tabs.Cast<TabPage>().Where((t, i) => mainTab.GetTabRect(i).Contains(e.Location)).First());
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
            if (unifytb_Main.TabPages.Count != 1 || unifytb_Main.SelectedTab.ToolTipText != string.Empty) {
                foreach (TabPage tabID in unifytb_Main.TabPages)
                    if (tabID.ToolTipText != string.Empty) warning = true;

                if (warning) {
                    switch (MessageBox.Show(SystemMessages.msg_CloseAllTabs, SystemMessages.tl_AreYouSure, MessageBoxButtons.YesNo, MessageBoxIcon.Warning)) {
                        case DialogResult.Yes:
                            foreach (TabPage tab in unifytb_Main.TabPages) unifytb_Main.TabPages.Remove(tab);
                            break;
                    }
                }
                else
                    foreach (TabPage tab in unifytb_Main.TabPages) unifytb_Main.TabPages.Remove(tab);

                Text = SystemMessages.tl_DefaultTitleVersion;
                NewTab(true);
            }
        }

        private void File_OpenFolder_Click(object sender, EventArgs e) {
            string getPath = Browsers.CommonBrowser(true, "Please select a folder...", string.Empty);

            if (getPath != string.Empty)
            {
                if (unifytb_Main.SelectedTab.ToolTipText == string.Empty) ResetTab(false);
                else NewTab(false);

                CurrentARC().Navigate(getPath);
                unifytb_Main.SelectedTab.Text = Path.GetFileName(getPath);

                if (Path.GetFileName(getPath).ToLower() == "new tab" || Path.GetFileName(getPath).EndsWith(".arc"))
                    unifytb_Main.SelectedTab.Text += " (Folder)";

                Text = SystemMessages.tl_Exploring(getPath);
            }
        }

        private async void File_OpenARC_Click(object sender, EventArgs e) {
            string getPath = Browsers.CommonBrowser(false, SystemMessages.tl_SelectArchive, Filters.Archives);

            if (getPath != string.Empty) {
                if (Verification.VerifyMagicNumberCommon(getPath)) {
                    try {
                        string failsafeCheck = Path.GetRandomFileName();
                        string arcBuildSession = Path.Combine(Program.applicationData, Paths.Archives, Program.sessionID.ToString(), failsafeCheck);

                        if (File.Exists(getPath)) {
                            Directory.CreateDirectory(arcBuildSession);
                            File.Copy(getPath, Path.Combine(arcBuildSession, Path.GetFileName(getPath)), true); 
                        }

                        Status = StatusMessages.cmn_Unpacking(getPath, false);
                        await ProcessAsyncHelper.ExecuteShellCommand(Paths.Unpack,
                              $"\"{Path.Combine(arcBuildSession, Path.GetFileName(getPath))}\"",
                              Application.StartupPath,
                              100000);

                        //Writes metadata to the unpacked directory to ensure the original path is remembered.
                        var metadataWrite = File.Create(Path.Combine(arcBuildSession, "metadata.ini"));
                        byte[] metadataSession = new UTF8Encoding(true).GetBytes(getPath);
                        metadataWrite.Write(metadataSession, 0, metadataSession.Length);
                        metadataWrite.Close();

                        if (unifytb_Main.SelectedTab.ToolTipText == string.Empty) ResetTab(false);
                        else NewTab(false);

                        CurrentARC().Navigate(Path.Combine(arcBuildSession, Path.GetFileNameWithoutExtension(getPath)));
                        unifytb_Main.SelectedTab.Text = Path.GetFileName(getPath);
                        unifytb_Main.SelectedTab.ToolTipText = failsafeCheck;
                        repackBuildSession = Path.Combine(Program.applicationData, Paths.Archives, Program.sessionID.ToString(), unifytb_Main.SelectedTab.ToolTipText);

                        string metadata = File.ReadAllText(Path.Combine(repackBuildSession, "metadata.ini"));
                        Text = SystemMessages.tl_Exploring(metadata);
                        Status = StatusMessages.cmn_Unpacked(getPath, false);
                    } catch { Status = StatusMessages.cmn_UnpackFailed(getPath, false); }
                }
                else MessageBox.Show(SystemMessages.ex_InvalidARC, SystemMessages.tl_DefaultTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Unifytb_Main_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                repackBuildSession = Path.Combine(Program.applicationData, Paths.Archives, Program.sessionID.ToString(), unifytb_Main.SelectedTab.ToolTipText);

                if (unifytb_Main.SelectedTab.ToolTipText == string.Empty) {
                    if (unifytb_Main.SelectedTab.Text == "New Tab")
                        Text = SystemMessages.tl_DefaultTitleVersion;
                    else
                        Text = SystemMessages.tl_Exploring(CurrentARC().Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\");
                } else {
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

            RepackAs(filename);

            try {
                Directory.Move(Path.Combine(repackBuildSession, Path.GetFileNameWithoutExtension(unifytb_Main.SelectedTab.Text)), Path.Combine(repackBuildSession, Path.GetFileNameWithoutExtension(filename)));
            }
            catch { MessageBox.Show(SystemMessages.ex_MetadataWriteError, SystemMessages.tl_DefaultTitle, MessageBoxButtons.OK, MessageBoxIcon.Error); }

            ResetTab(false);
            CurrentARC().Navigate(Path.Combine(Program.applicationData, Paths.Archives, Program.sessionID.ToString(), unifytb_Main.SelectedTab.ToolTipText, Path.GetFileNameWithoutExtension(filename)));
            unifytb_Main.SelectedTab.Text = Path.GetFileName(File.ReadAllText(Path.Combine(repackBuildSession, "metadata.ini")));
            Text = SystemMessages.tl_Exploring(File.ReadAllText(Path.Combine(repackBuildSession, "metadata.ini"))); ;
        }

        private async void Btn_Repack_Click(object sender, EventArgs e) {
            string metadata = File.ReadAllText(Path.Combine(repackBuildSession, "metadata.ini"));

            try {
                Status = StatusMessages.cmn_Repacking(metadata, false);
                await ProcessAsyncHelper.ExecuteShellCommand(Paths.Repack,
                      $"\"{Path.Combine(repackBuildSession, Path.GetFileNameWithoutExtension(metadata))}\"",
                      Application.StartupPath,
                      100000);
                if (File.Exists(Path.Combine(repackBuildSession, Path.GetFileName(metadata)))) 
                    File.Copy(Path.Combine(repackBuildSession, Path.GetFileName(metadata)), metadata, true); //Copies the repacked ARC back to the original location.
                Status = StatusMessages.cmn_Repacked(metadata, false);
            } catch { Status = StatusMessages.cmn_RepackFailed(metadata, false); }
        }

        private void Btn_OpenFolder_Click(object sender, EventArgs e) {
            try { Process.Start(CurrentARC().Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\"); }
            catch (Exception ex) { MessageBox.Show($"{SystemMessages.ex_OpenFolderError}\n\n{ex}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Folder_CopyToClipboard_Click(object sender, EventArgs e) {
            try {
                Status = StatusMessages.msg_Clipboard;
                Clipboard.SetText(CurrentARC().Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\");
            }
            catch (Exception ex) { MessageBox.Show($"{SystemMessages.ex_ClipboardFolderError}\n\n{ex}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
            string metadata = File.ReadAllText(Path.Combine(repackBuildSession, "metadata.ini"));

            if (filename != string.Empty)
            {
                try {
                    Status = StatusMessages.cmn_Repacking(metadata, false);
                    await ProcessAsyncHelper.ExecuteShellCommand(Paths.Repack,
                          $"\"{Path.Combine(repackBuildSession, Path.GetFileNameWithoutExtension(metadata))}\"",
                          Application.StartupPath,
                          100000);
                    if (File.Exists(Path.Combine(repackBuildSession, Path.GetFileName(metadata))))
                        File.Copy(Path.Combine(repackBuildSession, Path.GetFileName(metadata)), filename, true); //Copies the repacked ARC back to the original location.
                    Status = StatusMessages.cmn_Repacked(metadata, false);
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
                foreach (ToolStripDropDownItem item in main_SDK.DropDownItems) item.Enabled = true;
                foreach (ToolStripDropDownItem item in main_Shortcuts.DropDownItems) item.Enabled = true;
            } else {
                if (unifytb_Main.SelectedTab.Text == "New Tab" && Properties.Settings.Default.env_gameDirectory != string.Empty) {
                    pic_Logo.Visible = false;
                    lbl_SetDefault.Visible = false;
                } else {
                    pic_Logo.Visible = true;
                    lbl_SetDefault.Visible = true;
                }
                btn_OpenFolder.Enabled = false;
                btn_OpenFolderOptions.Enabled = false;
                btn_Repack.Enabled = false;
                btn_Back.Enabled = false;
                btn_Forward.Enabled = false;
                file_Repack.Enabled = false;
                file_RepackAs.Enabled = false;
                foreach (ToolStripDropDownItem item in main_SDK.DropDownItems) item.Enabled = false;
                foreach (ToolStripDropDownItem item in main_Shortcuts.DropDownItems) item.Enabled = false;
            }
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e) {
            Properties.Settings.Default.Save();

            bool warning = false;
            foreach (TabPage tabID in unifytb_Main.TabPages)
                if (tabID.ToolTipText != string.Empty) warning = true;

            if (warning) {
                switch (e.CloseReason) {
                    case CloseReason.UserClosing:
                        if (MessageBox.Show(SystemMessages.msg_UserClosing, SystemMessages.tl_DefaultTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            EraseData();
                        else e.Cancel = true;
                        break;
                    case CloseReason.WindowsShutDown:
                        if (MessageBox.Show(SystemMessages.msg_WindowsShutDown, SystemMessages.tl_DefaultTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            EraseData();
                        else e.Cancel = true;
                        break;
                    case CloseReason.ApplicationExitCall:
                        if (MessageBox.Show(SystemMessages.msg_UserClosing, SystemMessages.tl_DefaultTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            EraseData();
                        else e.Cancel = true;
                        break;
                }
            }
            else EraseData();
        }

        private void EraseData() {
            var archiveData = new DirectoryInfo(Path.Combine(Program.applicationData, Paths.Archives, Program.sessionID.ToString()));

            try {
                if (Directory.Exists(Path.Combine(Program.applicationData, Paths.Archives, Program.sessionID.ToString()))) {
                    foreach (FileInfo file in archiveData.GetFiles())
                        file.Delete();
                    foreach (DirectoryInfo directory in archiveData.GetDirectories())
                        directory.Delete(true);
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
                string failsafeCheck = Path.GetRandomFileName();
                string arcBuildSession = Path.Combine(Program.applicationData, Paths.Archives, Program.sessionID.ToString(), unifytb_Main.SelectedTab.ToolTipText, Path.GetFileNameWithoutExtension(getPath));
                Directory.CreateDirectory(arcBuildSession);

                //Writes metadata to the unpacked directory to ensure the original path is remembered.
                var metadataWrite = File.Create(Path.Combine(Program.applicationData, Paths.Archives, Program.sessionID.ToString(), failsafeCheck, "metadata.ini"));
                var metadataSession = new UTF8Encoding(true).GetBytes(getPath);
                metadataWrite.Write(metadataSession, 0, metadataSession.Length);
                metadataWrite.Close();

                if (unifytb_Main.SelectedTab.ToolTipText == string.Empty) ResetTab(false);
                else NewTab(false);

                CurrentARC().Navigate(arcBuildSession);
                unifytb_Main.SelectedTab.Text = Path.GetFileName(getPath);
                unifytb_Main.SelectedTab.ToolTipText = failsafeCheck;
                repackBuildSession = Path.Combine(Program.applicationData, Paths.Archives, Program.sessionID.ToString(), unifytb_Main.SelectedTab.ToolTipText);

                string metadata = File.ReadAllText(Path.Combine(repackBuildSession, "metadata.ini"));
                Text = SystemMessages.tl_Exploring(metadata);
            }
        }

        private void Tm_CheapFix_Tick(object sender, EventArgs e) {
            try {
                if (unifytb_Main.SelectedTab.ToolTipText != string.Empty) {
                    //Reads the metadata to get the original location of the ARC.
                    if (File.Exists(Path.Combine(repackBuildSession, "metadata.ini"))) {
                        string metadata = File.ReadAllText(Path.Combine(repackBuildSession, "metadata.ini"));
                        Text = SystemMessages.tl_Exploring(metadata);
                    } else { Text = SystemMessages.tl_DefaultTitleVersion; }
                }
            } catch { Text = SystemMessages.tl_DefaultTitleVersion; }

            try {
                if (CurrentARC().Url != null)
                    Paths.currentPath = HttpUtility.UrlDecode(CurrentARC().Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\").Replace("file:", "");
                if (unifytb_Main.SelectedTab.Text != "New Tab" || unifytb_Main.SelectedTab.ToolTipText != string.Empty || Properties.Settings.Default.env_gameDirectory != string.Empty)
                    InterfaceEnabled(true);
                else if (unifytb_Main.SelectedTab.Text == "New Tab" && unifytb_Main.SelectedTab.ToolTipText == string.Empty)
                    InterfaceEnabled(false);
            }
            catch { }
        }

        private void Sdk_PlacementConverter_Click(object sender, EventArgs e) {
            new PlacementConverter(this).ShowDialog();
        }

        private void File_Exit_Click(object sender, EventArgs e) { Application.Exit(); }

        private void Shortcuts_DecodeSET_Click(object sender, EventArgs e) {
            string location = Paths.currentPath;
            try {
                foreach (string SET in Directory.GetFiles(location, "*.set"))
                    if (File.Exists(Path.Combine(location, Path.GetFileName(SET)))) {
                        if (Verification.VerifyMagicNumberExtended(SET)) {
                            Status = StatusMessages.cmn_Exporting(SET, false);
                            var readSET = new S06SetData();
                            readSET.Load(Path.Combine(location, Path.GetFileName(SET)));
                            readSET.ExportXML(Path.Combine(location, $"{Path.GetFileNameWithoutExtension(SET)}.xml"));
                        } else
                            Status = StatusMessages.ex_InvalidFile(SET, false, "SET");
                    }
            } catch (Exception ex) {
                MessageBox.Show($"{SystemMessages.ex_SETExportError}\n\n{ex}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Lbl_Status_Click(object sender, EventArgs e) {
            Logs.ToolkitSessionLog openLogs = Application.OpenForms["ToolkitSessionLog"] != null ? (Logs.ToolkitSessionLog)Application.OpenForms["ToolkitSessionLog"] : null;
            if (openLogs != null) {
                try {
                    openLogs = (Logs.ToolkitSessionLog)Application.OpenForms["ToolkitSessionLog"];
                    openLogs.Close();
                }
                catch { }
            }
            sessionLog.Remove("Welcome to Sonic '06 Toolkit! Click here to view the Session Log...");
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
            if (XisoExtractor._errorlevel == 0) Status = XisoExtractor.GetLastError(extractQueue);
            else MessageBox.Show(XisoExtractor.GetLastError(extractQueue), SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private async void Shortcuts_DecompileLUB_Click(object sender, EventArgs e) {
            string location = Paths.currentPath;
            try {
                if (Verification.VerifyApplicationIntegrity(Paths.LuaDecompiler))
                    foreach (string LUB in Directory.GetFiles(location, "*.lub"))
                        if (Verification.VerifyMagicNumberExtended(LUB)) {
                            if (Path.GetFileName(LUB) == "standard.lub") break;

                            Status = StatusMessages.lua_Decompiling(LUB, false);
                            var decompile = await ProcessAsyncHelper.ExecuteShellCommand("java.exe",
                                                  $"-jar \"{Paths.LuaDecompiler}\" \"{LUB}\"",
                                                  location,
                                                  100000);
                            if (decompile.Completed)
                                if (decompile.ExitCode == 0)
                                    File.WriteAllText(LUB, decompile.Output);
                                else if (decompile.ExitCode == -1)
                                    MessageBox.Show($"{SystemMessages.ex_LUBDecompileError}\n\n{decompile.Error}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else
                                    MessageBox.Show($"{SystemMessages.ex_LUBDecompileExceptionUnknown}\n\n{decompile.Error}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
            } catch (Exception ex) {
                MessageBox.Show($"{SystemMessages.ex_LUBDecompileError}\n\n{ex}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Shortcuts_DecompileLUBinArchive_Click(object sender, EventArgs e) {
            string location = Path.Combine(repackBuildSession, Path.GetFileNameWithoutExtension(unifytb_Main.SelectedTab.Text));
            try {
                if (Verification.VerifyApplicationIntegrity(Paths.LuaDecompiler))
                    foreach (string LUB in Directory.GetFiles(location, "*.lub", SearchOption.AllDirectories))
                        if (Verification.VerifyMagicNumberExtended(LUB)) {
                            if (Path.GetFileName(LUB) == "standard.lub") break;

                            Status = StatusMessages.lua_Decompiling(LUB, true);
                            var decompile = await ProcessAsyncHelper.ExecuteShellCommand("java.exe",
                                                  $"-jar \"{Paths.LuaDecompiler}\" \"{LUB}\"",
                                                  location,
                                                  100000);
                            if (decompile.Completed)
                                if (decompile.ExitCode == 0)
                                    File.WriteAllText(LUB, decompile.Output);
                                else if (decompile.ExitCode == -1)
                                    MessageBox.Show($"{SystemMessages.ex_LUBDecompileError}\n\n{decompile.Error}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else
                                    MessageBox.Show($"{SystemMessages.ex_LUBDecompileExceptionUnknown}\n\n{decompile.Error}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
            } catch (Exception ex) {
                MessageBox.Show($"{SystemMessages.ex_LUBDecompileError}\n\n{ex}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Shortcuts_DecompileMissionLUBinDirectory_Click(object sender, EventArgs e) {
            string location = Paths.currentPath;
            try {
                if (Verification.VerifyApplicationIntegrity(Paths.LuaDecompiler))
                    foreach (string LUB in Directory.GetFiles(location, "mission*.lub"))
                        if (Verification.VerifyMagicNumberExtended(LUB)) {
                            Status = StatusMessages.lua_Decompiling(LUB, false);
                            var decompile = await ProcessAsyncHelper.ExecuteShellCommand("java.exe",
                                                  $"-jar \"{Paths.LuaDecompiler}\" \"{LUB}\"",
                                                  location,
                                                  100000);
                            if (decompile.Completed)
                                if (decompile.ExitCode == 0)
                                    File.WriteAllText(LUB, decompile.Output);
                                else if (decompile.ExitCode == -1)
                                    MessageBox.Show($"{SystemMessages.ex_LUBDecompileError}\n\n{decompile.Error}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else
                                    MessageBox.Show($"{SystemMessages.ex_LUBDecompileExceptionUnknown}\n\n{decompile.Error}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
            } catch (Exception ex) {
                MessageBox.Show($"{SystemMessages.ex_LUBDecompileError}\n\n{ex}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void Shortcuts_DecompileMissionLUBinArchive_Click(object sender, EventArgs e) {
            string location = Path.Combine(repackBuildSession, Path.GetFileNameWithoutExtension(unifytb_Main.SelectedTab.Text));
            try {
                if (Verification.VerifyApplicationIntegrity(Paths.LuaDecompiler))
                    foreach (string LUB in Directory.GetFiles(location, "mission*.lub", SearchOption.AllDirectories))
                        if (Verification.VerifyMagicNumberExtended(LUB)) {
                            Status = StatusMessages.lua_Decompiling(LUB, true);
                            var decompile = await ProcessAsyncHelper.ExecuteShellCommand("java.exe",
                                                  $"-jar \"{Paths.LuaDecompiler}\" \"{LUB}\"",
                                                  location,
                                                  100000);
                            if (decompile.Completed)
                                if (decompile.ExitCode == 0)
                                    File.WriteAllText(LUB, decompile.Output);
                                else if (decompile.ExitCode == -1)
                                    MessageBox.Show($"{SystemMessages.ex_LUBDecompileError}\n\n{decompile.Error}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else
                                    MessageBox.Show($"{SystemMessages.ex_LUBDecompileExceptionUnknown}\n\n{decompile.Error}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
            } catch (Exception ex) {
                MessageBox.Show($"{SystemMessages.ex_LUBDecompileError}\n\n{ex}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Sdk_LuaCompilation_Click(object sender, EventArgs e) {
            new LuaCompilation(this).ShowDialog();
        }

        private void Sdk_ExecutableDecryptor_Click(object sender, EventArgs e) {
            new ExecutableModification(this).ShowDialog();
        }

        private async void Shortcuts_DecryptXEX_Click(object sender, EventArgs e) {
            string location = Paths.currentPath;
            foreach (string XEX in Directory.GetFiles(location, "*.xex"))
                if (Verification.VerifyMagicNumberCommon(Path.Combine(location, XEX))) {
                    Status = StatusMessages.cmn_Decrypting(Path.Combine(location, XEX), false);
                    var process = await ProcessAsyncHelper.ExecuteShellCommand(Paths.XexTool,
                                        $"-e u \"{Path.Combine(location, XEX)}\"",
                                        location,
                                        100000);
                    if (process.Completed)
                        if (process.ExitCode != 0)
                            MessageBox.Show($"{SystemMessages.ex_XEXModificationError}\n\n{process.Output}", SystemMessages.tl_FatalError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void Sdk_SonicSoundStudio_Click(object sender, EventArgs e) {
            new SonicSoundStudio(this).ShowDialog();
        }
    }
}
