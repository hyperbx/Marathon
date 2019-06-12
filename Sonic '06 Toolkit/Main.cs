using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

// Welcome to Sonic '06 Toolkit!

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

namespace Sonic_06_Toolkit
{
    public partial class Main : Form
    {
        public Main(string[] args)
        {
            InitializeComponent();

            newTab(); //Opens a new tab on launch.
            tm_tabCheck.Start(); //Starts the timer that watches tab activity.

            #region Session ID...
            var generateSessionID = new Random();
            Tools.Global.sessionID = generateSessionID.Next(1, 99999); //Generates a random number between 1 to 99999 for a unique Session ID.
            btn_SessionID.Text = Tools.Global.sessionID.ToString();
            #endregion

            //The below code checks the command line arguments and unpacks the file that was dragged into the application.
            if (args.Length > 0)
            {
                #region ADX
                if (Path.GetExtension(args[0]) == ".adx")
                {
                    try
                    {
                        Tools.Global.adxState = "launch-adx";
                        Tools.ADX.ConvertToWAV(args[0], string.Empty);

                        Close();
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when encoding the ADX file.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Tools.Notification.Dispose();
                        Close();
                    }
                }
                #endregion

                #region ARC
                else if (Path.GetExtension(args[0]) == ".arc")
                {
                    if (Debugger.unsafeState == true) { MessageBox.Show("ARC tools are missing. Please restart Sonic '06 Toolkit and try again.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    else
                    {
                        try
                        {
                            if (File.Exists(Properties.Settings.Default.unpackFile) || File.Exists(Properties.Settings.Default.repackFile) || File.Exists(Properties.Settings.Default.arctoolFile))
                            {
                                Tools.Global.arcState = "launch-typical";
                                Tools.ARC.Unpack(args[0], string.Empty);

                                if (Properties.Settings.Default.unpackAndLaunch)
                                {

                                    //Creates a new tab if the selected one is being used.
                                    if (tab_Main.SelectedTab.Text == "New Tab")
                                    {
                                        navigateToGame = false;
                                        resetTab();

                                        currentARC().Navigate(Tools.ARC.getLocation);
                                        tab_Main.SelectedTab.Text = Path.GetFileName(args[0]);
                                        navigateToGame = true;
                                    }
                                    else
                                    {
                                        navigateToGame = false;
                                        newTab();

                                        currentARC().Navigate(Tools.ARC.getLocation);
                                        tab_Main.SelectedTab.Text = Path.GetFileName(args[0]);
                                        navigateToGame = true;
                                    }

                                    //Writes a file to store the failsafe directory to be referenced later.
                                    //var storageWrite = File.Create($"{Properties.Settings.Default.archivesPath}{Tools.Global.sessionID}\\{Tools.Global.getIndex}");
                                    //var storageSession = new UTF8Encoding(true).GetBytes(Tools.ARC.failsafeCheck);
                                    //storageWrite.Write(storageSession, 0, storageSession.Length);
                                    //storageWrite.Close();

                                    tab_Main.SelectedTab.ToolTipText = Tools.ARC.failsafeCheck;
                                    Tools.Global.getStorage = Tools.ARC.failsafeCheck;
                                    Tools.Global.getIndex = tab_Main.SelectedIndex;

                                    Text = "Sonic '06 Toolkit - '" + args[0] + "'";
                                }
                                else { Close(); }
                            }
                            else { MessageBox.Show("ARC tools are missing. Please restart Sonic '06 Toolkit and try again.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); Close(); }
                        }
                        catch
                        {
                            MessageBox.Show("An error occurred when unpacking the archive.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Tools.Notification.Dispose();
                            Close();
                        }
                    }
                }
                #endregion

                #region AT3
                else if (Path.GetExtension(args[0]) == ".at3")
                {
                    try
                    {
                        Tools.Global.at3State = "launch-at3";
                        Tools.AT3.ConvertToWAV(args[0], string.Empty);

                        Close();
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when converting the AT3.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Tools.Notification.Dispose();
                        Close();
                    }
                }
                #endregion

                #region CSB
                else if (Path.GetExtension(args[0]) == ".csb")
                {
                    try
                    {
                        Tools.Global.csbState = "launch-unpack";
                        Tools.CSB.Packer(args[0], string.Empty);

                        Close();
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when unpacking the CSB.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Tools.Notification.Dispose();
                        Close();
                    }
                }
                #endregion

                #region DDS
                else if (Path.GetExtension(args[0]) == ".dds")
                {
                    try
                    {
                        Tools.Global.xnoState = "launch-dds";
                        Tools.DDS.Convert(args[0], string.Empty);

                        Close();
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when converting the DDS.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Tools.Notification.Dispose();
                        Close();
                    }
                }
                #endregion

                #region LUB
                else if (Path.GetExtension(args[0]) == ".lub")
                {
                    try
                    {
                        Tools.Global.lubState = "launch-decompile";
                        Tools.LUB.Decompile(args[0], string.Empty);

                        Close();
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when decompiling the Lua binaries.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Tools.Notification.Dispose();
                        Close();
                    }
                }
                #endregion

                #region MST
                else if (Path.GetExtension(args[0]) == ".mst")
                {
                    try
                    {
                        Tools.Global.mstState = "launch-mst";
                        Tools.MST.Export(args[0], string.Empty);

                        Close();
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when decoding the MST.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Tools.Notification.Dispose();
                        Close();
                    }
                }
                #endregion

                #region PNG
                else if (Path.GetExtension(args[0]) == ".png")
                {
                    try
                    {
                        Tools.Global.ddsState = "launch-png";
                        Tools.PNG.Convert(args[0], string.Empty);

                        Close();
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when converting the PNG.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Tools.Notification.Dispose();
                        Close();
                    }
                }
                #endregion

                #region SET
                else if (Path.GetExtension(args[0]) == ".set")
                {
                    try
                    {
                        if (File.Exists(args[0]))
                        {
                            Tools.Global.setState = "launch-export";
                            Tools.SET.Export(args[0], string.Empty);

                            Close();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when converting the SETs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Tools.Notification.Dispose();
                        Close();
                    }
                }
                #endregion

                #region XNO
                else if (Path.GetExtension(args[0]) == ".xno")
                {
                    try
                    {
                        Tools.Global.xnoState = "launch-xno";
                        Tools.XNO.Convert(args[0], string.Empty);

                        Close();
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when converting the XNO.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Tools.Notification.Dispose();
                        Close();
                    }
                }
                #endregion
            }
        }

        public static int FormTop;
        public static int FormLeft;
        public static int FormHeight;
        public static int FormWidth;

        void CheckForUpdates(string currentVersion, string newVersionDownloadLink, string libDownloadLink, string versionInfoLink)
        {
            try
            {
                var latestVersion = new Tools.TimedWebClient { Timeout = 100000 }.DownloadString(versionInfoLink);
                var changeLogs = new Tools.TimedWebClient { Timeout = 100000 }.DownloadString("https://segacarnival.com/hyper/updates/changelogs.txt");
                if (latestVersion.Contains("Version"))
                {
                    if (latestVersion != currentVersion)
                    {
                        if (Tools.Global.updateState == "check")
                        {
                            lbl_UpdateNotif.Visible = true;
                        }
                        else
                        {
                            DialogResult confirmUpdate = MessageBox.Show("Sonic '06 Toolkit - " + latestVersion + " is now available!\n\nChangelogs:\n" + changeLogs + "\n\nDo you wish to download it?", "New update available!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            switch (confirmUpdate)
                            {
                                case DialogResult.Yes:
                                    tm_updateCheck.Stop();

                                    mainPreferences_DisableSoftwareUpdater.Enabled = false;
                                    lbl_UpdateNotif.Visible = false;
                                    pnl_Updater.Visible = true;

                                    if (themes_Original.Checked == true) { btn_Backdrop.Visible = true; btn_Backdrop.Left += 186; }
                                    try
                                    {
                                        if (File.Exists(Path.GetDirectoryName(Application.ExecutablePath) + @"\HedgeLib.dll"))
                                        {
                                            var clientHedgeLib = new WebClient();
                                            clientHedgeLib.DownloadProgressChanged += (s, e) => { pgb_updateStatus.Value = e.ProgressPercentage; };
                                            clientHedgeLib.DownloadFileAsync(new Uri(libDownloadLink), Path.GetDirectoryName(Application.ExecutablePath) + @"\HedgeLib.pak");
                                            clientHedgeLib.DownloadFileCompleted += (s, e) =>
                                            {
                                                File.Replace(Path.GetDirectoryName(Application.ExecutablePath) + @"\HedgeLib.pak", Path.GetDirectoryName(Application.ExecutablePath) + @"\HedgeLib.dll", Path.GetDirectoryName(Application.ExecutablePath) + @"\HedgeLib.bak");
                                            };
                                        }
                                        else
                                        {
                                            var clientHedgeLib = new WebClient();
                                            clientHedgeLib.DownloadProgressChanged += (s, e) => { pgb_updateStatus.Value = e.ProgressPercentage; };
                                            clientHedgeLib.DownloadFileAsync(new Uri(libDownloadLink), Path.GetDirectoryName(Application.ExecutablePath) + @"\HedgeLib.dll");
                                        }

                                        if (File.Exists(Application.ExecutablePath))
                                        {
                                            var clientApplication = new WebClient();
                                            clientApplication.DownloadProgressChanged += (s, e) => { pgb_updateStatus.Value = e.ProgressPercentage; };
                                            clientApplication.DownloadFileAsync(new Uri(newVersionDownloadLink), Application.ExecutablePath + ".pak");
                                            clientApplication.DownloadFileCompleted += (s, e) =>
                                            {
                                                File.Replace(Application.ExecutablePath + ".pak", Application.ExecutablePath, Application.ExecutablePath + ".bak");
                                                Process.Start(Application.ExecutablePath);
                                                Application.Exit();
                                            };
                                        }
                                        else { MessageBox.Show("Sonic '06 Toolkit doesn't exist... What?!", "Stupid Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                    }
                                    catch
                                    {
                                        tm_updateCheck.Start();

                                        lbl_UpdateNotif.Visible = false;
                                        pnl_Updater.Visible = false;
                                        btn_Backdrop.Visible = false;

                                        MessageBox.Show("An error occurred when updating Sonic '06 Toolkit.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    break;

                                case DialogResult.No:
                                    tm_updateCheck.Start();
                                    break;
                            }
                        }
                    }
                    else if (Tools.Global.updateState == "user") MessageBox.Show("There are currently no updates available.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else { tm_updateCheck.Start(); }
                }
                else
                {
                    Tools.Global.serverStatus = "down";
                    tm_updateCheck.Stop();
                    if (Properties.Settings.Default.disableSoftwareUpdater == true) MessageBox.Show("The update servers are currently undergoing maintenance. Apologies for the inconvenience.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                Tools.Global.serverStatus = "offline";
                tm_updateCheck.Stop();
            }

            Tools.Global.updateState = null;
            mainPreferences_DisableSoftwareUpdater.Enabled = true;
        }

        void Main_Load(object sender, EventArgs e)
        {
            #region Directory Check...

            #region Validating Paths...
            if (Properties.Settings.Default.rootPath == string.Empty) Properties.Settings.Default.rootPath = Tools.Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\";
            if (Properties.Settings.Default.toolsPath == string.Empty) Properties.Settings.Default.toolsPath = Tools.Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Tools\";
            if (Properties.Settings.Default.archivesPath == string.Empty) Properties.Settings.Default.archivesPath = Tools.Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Archives\";
            if (Properties.Settings.Default.unlubPath == string.Empty) Properties.Settings.Default.unlubPath = Tools.Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Tools\unlub\";
            if (Properties.Settings.Default.xnoPath == string.Empty) Properties.Settings.Default.xnoPath = Tools.Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Tools\xno2dae\";
            Properties.Settings.Default.Save();
            #endregion

            try
            {
                //The below code checks if the directories in the Global class exist; if not, they will be created.
                if (!Directory.Exists(Properties.Settings.Default.rootPath)) Directory.CreateDirectory(Properties.Settings.Default.rootPath);
                if (!Directory.Exists(Properties.Settings.Default.toolsPath)) Directory.CreateDirectory(Properties.Settings.Default.toolsPath);
                if (!Directory.Exists(Properties.Settings.Default.toolsPath + @"Arctool\")) Directory.CreateDirectory(Properties.Settings.Default.toolsPath + @"Arctool\");
                if (!Directory.Exists(Properties.Settings.Default.toolsPath + @"Arctool\arctool\")) Directory.CreateDirectory(Properties.Settings.Default.toolsPath + @"Arctool\arctool\");
                if (!Directory.Exists(Properties.Settings.Default.toolsPath + @"GerbilSoft\")) Directory.CreateDirectory(Properties.Settings.Default.toolsPath + @"GerbilSoft\");
                if (!Directory.Exists(Properties.Settings.Default.toolsPath + @"CsbEditor\")) Directory.CreateDirectory(Properties.Settings.Default.toolsPath + @"CsbEditor\");
                if (!Directory.Exists(Properties.Settings.Default.toolsPath + @"CriWare\")) Directory.CreateDirectory(Properties.Settings.Default.toolsPath + @"CriWare\");
                if (!Directory.Exists(Properties.Settings.Default.toolsPath + @"DirectX\")) Directory.CreateDirectory(Properties.Settings.Default.toolsPath + @"DirectX\");
                if (!Directory.Exists(Properties.Settings.Default.toolsPath + @"exiso\")) Directory.CreateDirectory(Properties.Settings.Default.toolsPath + @"exiso\");
                if (!Directory.Exists(Properties.Settings.Default.toolsPath + @"SONY\")) Directory.CreateDirectory(Properties.Settings.Default.toolsPath + @"SONY\");
                if (!Directory.Exists(Properties.Settings.Default.archivesPath)) Directory.CreateDirectory(Properties.Settings.Default.archivesPath);
                if (!Directory.Exists(Properties.Settings.Default.unlubPath)) Directory.CreateDirectory(Properties.Settings.Default.unlubPath);
                if (!Directory.Exists(Properties.Settings.Default.xnoPath)) Directory.CreateDirectory(Properties.Settings.Default.xnoPath);
            }
            catch { MessageBox.Show("An error occurred when writing a directory.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); Application.Exit(); }
            #endregion

            #region File Check...

            #region Validating Files...
            if (Properties.Settings.Default.unpackFile == string.Empty) Properties.Settings.Default.unpackFile = Properties.Settings.Default.toolsPath + @"unpack.exe";
            if (Properties.Settings.Default.repackFile == string.Empty) Properties.Settings.Default.repackFile = Properties.Settings.Default.toolsPath + @"repack.exe";
            if (Properties.Settings.Default.arctoolFile == string.Empty) Properties.Settings.Default.arctoolFile = Properties.Settings.Default.toolsPath + @"Arctool\arctool.exe";
            if (Properties.Settings.Default.mstFile == string.Empty) Properties.Settings.Default.mstFile = Properties.Settings.Default.toolsPath + @"GerbilSoft\mst06.exe";
            if (Properties.Settings.Default.csbFile == string.Empty) Properties.Settings.Default.csbFile = Properties.Settings.Default.toolsPath + @"CsbEditor\CsbEditor.exe";
            if (Properties.Settings.Default.adx2wavFile == string.Empty) Properties.Settings.Default.adx2wavFile = Properties.Settings.Default.toolsPath + @"CriWare\ADX2WAV.exe";
            if (Properties.Settings.Default.criconverterFile == string.Empty) Properties.Settings.Default.criconverterFile = Properties.Settings.Default.toolsPath + @"CriWare\criatomencd.exe";
            if (Properties.Settings.Default.directXFile == string.Empty) Properties.Settings.Default.directXFile = Properties.Settings.Default.toolsPath + @"DirectX\texconv.exe";
            if (Properties.Settings.Default.exisoFile == string.Empty) Properties.Settings.Default.exisoFile = Properties.Settings.Default.toolsPath + @"exiso\exiso.exe";
            if (Properties.Settings.Default.at3File == string.Empty) Properties.Settings.Default.at3File = Properties.Settings.Default.toolsPath + @"SONY\at3tool.exe";
            Properties.Settings.Default.Save();
            #endregion

            try
            {
                //The below code checks if the files in the Global class exist; if not, they will be created.
                if (!File.Exists(Properties.Settings.Default.unpackFile)) File.WriteAllBytes(Properties.Settings.Default.unpackFile, Properties.Resources.unpack);
                if (!File.Exists(Properties.Settings.Default.repackFile)) File.WriteAllBytes(Properties.Settings.Default.repackFile, Properties.Resources.repack);
                if (!File.Exists(Properties.Settings.Default.arctoolFile)) File.WriteAllBytes(Properties.Settings.Default.arctoolFile, Properties.Resources.arctool);
                if (!File.Exists(Properties.Settings.Default.csbFile)) File.WriteAllBytes(Properties.Settings.Default.csbFile, Properties.Resources.CsbEditor);
                if (!File.Exists(Properties.Settings.Default.toolsPath + @"Arctool\arctool\arcc.php")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"Arctool\arctool\arcc.php", Properties.Resources.arcphp);
                if (!File.Exists(Properties.Settings.Default.toolsPath + @"Arctool\arctool\arctool.php")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"Arctool\arctool\arctool.php", Properties.Resources.arctoolphp);
                if (!File.Exists(Properties.Settings.Default.toolsPath + @"CsbEditor\SonicAudioLib.dll")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"CsbEditor\SonicAudioLib.dll", Properties.Resources.SonicAudioLib);
                if (!File.Exists(Properties.Settings.Default.toolsPath + @"CsbEditor\CsbEditor.exe.config")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"CsbEditor\CsbEditor.exe.config", Properties.Resources.CsbEditorConfig);
                /* if (!File.Exists(Properties.Settings.Default.toolsPath + @"GerbilSoft\mst06.exe")) */
                File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"GerbilSoft\mst06.exe", Properties.Resources.mst06);
                if (!File.Exists(Properties.Settings.Default.toolsPath + @"GerbilSoft\tinyxml2.dll")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"GerbilSoft\tinyxml2.dll", Properties.Resources.tinyxml2);
                if (!File.Exists(Properties.Settings.Default.adx2wavFile)) File.WriteAllBytes(Properties.Settings.Default.adx2wavFile, Properties.Resources.ADX2WAV);
                if (!File.Exists(Properties.Settings.Default.criconverterFile)) File.WriteAllBytes(Properties.Settings.Default.criconverterFile, Properties.Resources.criatomencd);
                if (!File.Exists(Properties.Settings.Default.toolsPath + @"CriWare\AsyncAudioEncoder.dll")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"CriWare\AsyncAudioEncoder.dll", Properties.Resources.AsyncAudioEncoder);
                if (!File.Exists(Properties.Settings.Default.toolsPath + @"CriWare\AudioStream.dll")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"CriWare\AudioStream.dll", Properties.Resources.AudioStream);
                if (!File.Exists(Properties.Settings.Default.toolsPath + @"CriWare\criatomencd.exe.config")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"CriWare\criatomencd.exe.config", Properties.Resources.criatomencdConfig);
                if (!File.Exists(Properties.Settings.Default.toolsPath + @"CriWare\CriAtomEncoderComponent.dll")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"CriWare\CriAtomEncoderComponent.dll", Properties.Resources.CriAtomEncoderComponent);
                if (!File.Exists(Properties.Settings.Default.toolsPath + @"CriWare\CriSamplingRateConverter.dll")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"CriWare\CriSamplingRateConverter.dll", Properties.Resources.CriSamplingRateConverter);
                if (!File.Exists(Properties.Settings.Default.toolsPath + @"CriWare\vsthost.dll")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"CriWare\vsthost.dll", Properties.Resources.vsthost);
                if (!File.Exists(Properties.Settings.Default.toolsPath + @"exiso\exiso.exe")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"exiso\exiso.exe", Properties.Resources.exiso);
                if (!File.Exists(Properties.Settings.Default.toolsPath + @"SONY\at3tool.exe")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"SONY\at3tool.exe", Properties.Resources.at3tool);
                if (!File.Exists(Properties.Settings.Default.toolsPath + @"DirectX\texconv.exe")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"DirectX\texconv.exe", Properties.Resources.texconv);
            }
            catch { MessageBox.Show("An error occurred when writing a file.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); Application.Exit(); }
            #endregion

            #region Setting saved properties...
            //Gets user-defined settings and sets them in runtime.
            if (Properties.Settings.Default.showLogo == true) pic_Logo.Visible = true; else pic_Logo.Visible = false;
            if (Properties.Settings.Default.showSessionID == true) mainPreferences_ShowSessionID.Checked = true; else mainPreferences_ShowSessionID.Checked = false;
            if (Properties.Settings.Default.theme == "Compact") mainThemes_Compact.Checked = true; else if (Properties.Settings.Default.theme == "Original") mainThemes_Original.Checked = true;
            if (Properties.Settings.Default.unpackAndLaunch == true) ARC_UnpackAndLaunch.Checked = true; else ARC_UnpackRoot.Checked = true;
            if (Properties.Settings.Default.gameDir == true) mainPreferences_DisableGameDirectory.Checked = false; else mainPreferences_DisableGameDirectory.Checked = true;
            if (Properties.Settings.Default.disableWarns == true) advanced_DisableWarnings.Checked = true; else advanced_DisableWarnings.Checked = false;
            if (Properties.Settings.Default.debugMode == true)
            {
                if (!debug) { advanced_DebugMode.Checked = true; }
            }
            else
            {
                if (debug) { advanced_DebugMode.Checked = false; }
            }
            if (Properties.Settings.Default.debugShow == true)
            {
                advanced_DebugMode.Visible = true;
                //advanced_Separator1.Visible = true;
            }
            else
            {
                advanced_DebugMode.Visible = false;
                //advanced_Separator1.Visible = false;
            }
            if (Properties.Settings.Default.disableSoftwareUpdater == true)
            {
                mainPreferences_DisableSoftwareUpdater.Checked = true;
            }
            else
            {
                mainPreferences_DisableSoftwareUpdater.Checked = false;
                CheckForUpdates(Tools.Global.latestVersion, "https://segacarnival.com/hyper/updates/latest-master.exe", "https://segacarnival.com/hyper/updates/lib.dll", "https://segacarnival.com/hyper/updates/latest_master.txt");
            }
            #endregion

            JavaCheck();
        }

        void JavaCheck()
        {
            try
            {
                var javaArg = new ProcessStartInfo("java", "-version");
                javaArg.WindowStyle = ProcessWindowStyle.Hidden;
                javaArg.RedirectStandardOutput = true;
                javaArg.RedirectStandardError = true;
                javaArg.UseShellExecute = false;
                javaArg.CreateNoWindow = true;
                var javaProcess = new Process();
                javaProcess.StartInfo = javaArg;
                javaProcess.Start();
            }
            catch { Tools.Global.javaCheck = false; }
        }

        static bool navigateToGame = true;

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
            if (navigateToGame)
            {
                if (Properties.Settings.Default.gamePath != "")
                {
                    if (Directory.Exists(Properties.Settings.Default.gamePath))
                    {
                        currentARC().Navigate(Properties.Settings.Default.gamePath);
                        lbl_SetDefault.Visible = false;
                        pic_Logo.Visible = false;
                    }
                }
            }
            currentARC().AllowWebBrowserDrop = false;
        }

        public string GetStorage
        {
            get { return tab_Main.SelectedTab.ToolTipText; }
        }

        private WebBrowser currentARC()
        {
            //Returns the active web browser in the selected tab.
            return (WebBrowser)tab_Main.SelectedTab.Controls[0];
        }

        #region File

        #region Unpack States
        void mainFile_OpenARC_Click(object sender, EventArgs e)
        {
            if (Debugger.unsafeState == true) { MessageBox.Show("ARC tools are missing. Please restart Sonic '06 Toolkit and try again.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                if (Paths.changes == true) { MessageBox.Show("A restart for Sonic '06 Toolkit is pending.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                else
                {
                    ofd_OpenFiles.Title = "Please select an ARC file...";
                    ofd_OpenFiles.Filter = "ARC Files|*.arc";
                    ofd_OpenFiles.DefaultExt = "arc";

                    if (ofd_OpenFiles.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            Tools.Global.arcState = "typical";

                            if (File.Exists(Properties.Settings.Default.unpackFile) || File.Exists(Properties.Settings.Default.repackFile) || File.Exists(Properties.Settings.Default.arctoolFile))
                            {
                                Tools.ARC.Unpack(null, ofd_OpenFiles.FileName);

                                //Creates a new tab if the selected one is being used.
                                if (tab_Main.SelectedTab.Text == "New Tab")
                                {
                                    navigateToGame = false;
                                    resetTab();

                                    currentARC().Navigate(Tools.ARC.getLocation);
                                    tab_Main.SelectedTab.Text = Path.GetFileName(ofd_OpenFiles.FileName);
                                    navigateToGame = true;
                                }
                                else
                                {
                                    navigateToGame = false;
                                    newTab();

                                    currentARC().Navigate(Tools.ARC.getLocation);
                                    tab_Main.SelectedTab.Text = Path.GetFileName(ofd_OpenFiles.FileName);
                                    navigateToGame = true;
                                }

                                //Writes a file to store the failsafe directory to be referenced later.
                                //var storageWrite = File.Create($"{Properties.Settings.Default.archivesPath}{Tools.Global.sessionID}\\{Tools.Global.getIndex}");
                                //var storageSession = new UTF8Encoding(true).GetBytes(Tools.ARC.failsafeCheck);
                                //storageWrite.Write(storageSession, 0, storageSession.Length);
                                //storageWrite.Close();

                                tab_Main.SelectedTab.ToolTipText = Tools.ARC.failsafeCheck;
                                Tools.Global.getStorage = Tools.ARC.failsafeCheck;
                                Tools.Global.getIndex = tab_Main.SelectedIndex;

                                Text = "Sonic '06 Toolkit - '" + ofd_OpenFiles.FileName + "'";
                            }
                            else { MessageBox.Show("ARC tools are missing. Please restart Sonic '06 Toolkit and try again.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                        catch
                        {
                            MessageBox.Show("An error occurred when unpacking the archive.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Tools.Notification.Dispose();
                        }
                    }
                }
            }
        }
        #endregion

        void MainFile_ExtractISO_Click(object sender, EventArgs e)
        {
            if (Paths.changes == true) { MessageBox.Show("A restart for Sonic '06 Toolkit is pending.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                ofd_OpenFiles.Title = "Please select an Xbox 360 ISO...";
                ofd_OpenFiles.Filter = null;

                if (ofd_OpenFiles.ShowDialog() == DialogResult.OK)
                {
                    fbd_BrowseFolders.Description = "Please select the path to extract your ISO. Click Cancel to extract the ISO in it's directory.";

                    switch (fbd_BrowseFolders.ShowDialog())
                    {
                        case DialogResult.OK:
                            Tools.Global.exisoState = "extract";

                            var extractSession = new ProcessStartInfo(Properties.Settings.Default.exisoFile, "-d \"" + fbd_BrowseFolders.SelectedPath + "\" -x \"" + ofd_OpenFiles.FileName + "\"");
                            extractSession.WorkingDirectory = Tools.Global.currentPath;
                            extractSession.WindowStyle = ProcessWindowStyle.Hidden;
                            var Extract = Process.Start(extractSession);
                            var extractDialog = new Status();
                            var parentLeft = Left + ((Width - extractDialog.Width) / 2);
                            var parentTop = Top + ((Height - extractDialog.Height) / 2);
                            extractDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                            extractDialog.Show();
                            Extract.WaitForExit();
                            Extract.Close();
                            extractDialog.Close();

                            MessageBox.Show("The selected Xbox 360 ISO has been extracted.", "Extract Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Tools.Global.exisoState = null;
                            break;

                        case DialogResult.Cancel:
                            Tools.Global.exisoState = "extract";

                            var extractRootSession = new ProcessStartInfo(Properties.Settings.Default.exisoFile, "-d \"" + Path.GetDirectoryName(ofd_OpenFiles.FileName) + @"\" + Path.GetFileNameWithoutExtension(ofd_OpenFiles.FileName) + "\" -x \"" + ofd_OpenFiles.FileName + "\"");
                            extractRootSession.WorkingDirectory = Tools.Global.currentPath;
                            extractRootSession.WindowStyle = ProcessWindowStyle.Hidden;
                            var ExtractRoot = Process.Start(extractRootSession);
                            var extractRootDialog = new Status();
                            var parentRootLeft = Left + ((Width - extractRootDialog.Width) / 2);
                            var parentRootTop = Top + ((Height - extractRootDialog.Height) / 2);
                            extractRootDialog.Location = new System.Drawing.Point(parentRootLeft, parentRootTop);
                            extractRootDialog.Show();
                            ExtractRoot.WaitForExit();
                            ExtractRoot.Close();
                            extractRootDialog.Close();

                            MessageBox.Show("The selected Xbox 360 ISO has been extracted.", "Extract Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Tools.Global.exisoState = null;
                            break;
                    }
                }
            }
        }

        public bool freeMode = false;

        void MainFile_OpenSonic_Click(object sender, EventArgs e)
        {
            if (Paths.changes == true) { MessageBox.Show("A restart for Sonic '06 Toolkit is pending.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                if (!freeMode)
                {
                    fbd_BrowseFolders.Description = "Please select the path to your extracted copy of SONIC THE HEDGEHOG.";

                    if (fbd_BrowseFolders.ShowDialog() == DialogResult.OK)
                    {
                        if (Directory.Exists(fbd_BrowseFolders.SelectedPath))
                        {

                            #region Xbox 360
                            if (File.Exists(fbd_BrowseFolders.SelectedPath + @"\default.xex"))
                            {
                                byte[] bytes = File.ReadAllBytes(fbd_BrowseFolders.SelectedPath + @"\default.xex").Take(4).ToArray();
                                var hexString = BitConverter.ToString(bytes); hexString = hexString.Replace("-", " ");
                                if (hexString == "58 45 58 32")
                                {
                                    lbl_SetDefault.Visible = false;
                                    pic_Logo.Visible = false;

                                    #region Building directory data...
                                    //Establishes the failsafe directory and copies the ARC prepare for the unpacking process.
                                    string failsafeCheck = Path.GetRandomFileName(); //Unpacked ARCs will have a unique directory to prevent overwriting.
                                    var arcBuildSession = new StringBuilder();
                                    arcBuildSession.Append(Properties.Settings.Default.archivesPath);
                                    arcBuildSession.Append(Tools.Global.sessionID);
                                    arcBuildSession.Append(@"\");
                                    arcBuildSession.Append(failsafeCheck);
                                    arcBuildSession.Append(@"\");
                                    if (!Directory.Exists(arcBuildSession.ToString())) Directory.CreateDirectory(arcBuildSession.ToString());
                                    #endregion

                                    #region Writing metadata...
                                    //Writes metadata to the unpacked directory to ensure the original path is remembered.
                                    var metadataWrite = File.Create(arcBuildSession.ToString() + "metadata.ini");
                                    var metadataSession = new UTF8Encoding(true).GetBytes(fbd_BrowseFolders.SelectedPath + @"\");
                                    metadataWrite.Write(metadataSession, 0, metadataSession.Length);
                                    metadataWrite.Close();
                                    #endregion

                                    //#region Building location data...
                                    ////Writes a file to store the failsafe directory to be referenced later.
                                    //var storageSession = new StringBuilder();
                                    //storageSession.Append(Properties.Settings.Default.archivesPath);
                                    //storageSession.Append(Tools.Global.sessionID);
                                    //storageSession.Append(@"\");
                                    //storageSession.Append(tab_Main.SelectedIndex);
                                    //var storageWrite = File.Create(storageSession.ToString());
                                    //var storageText = new UTF8Encoding(true).GetBytes(failsafeCheck);
                                    //storageWrite.Write(storageText, 0, storageText.Length);
                                    //storageWrite.Close();
                                    //#endregion

                                    #region Navigating...
                                    //Creates a new tab if the selected one is being used.
                                    if (tab_Main.SelectedTab.Text == "New Tab")
                                    {
                                        navigateToGame = false;
                                        resetTab();
                                    }
                                    else
                                    {
                                        navigateToGame = false;
                                        newTab();
                                    }
                                    #endregion

                                    tab_Main.SelectedTab.ToolTipText = failsafeCheck;
                                    Tools.Global.getStorage = failsafeCheck;
                                    Tools.Global.getIndex = tab_Main.SelectedIndex;

                                    currentARC().Navigate(fbd_BrowseFolders.SelectedPath);
                                    if (Path.GetFileName(fbd_BrowseFolders.SelectedPath) == "New Tab" || Path.GetFileName(fbd_BrowseFolders.SelectedPath).EndsWith(".arc"))
                                    {
                                        tab_Main.SelectedTab.Text = Path.GetFileName(fbd_BrowseFolders.SelectedPath) + " (Folder)";
                                    }
                                    else { tab_Main.SelectedTab.Text = Path.GetFileName(fbd_BrowseFolders.SelectedPath); }
                                    navigateToGame = true;

                                    Text = "Sonic '06 Toolkit - '" + fbd_BrowseFolders.SelectedPath + @"\'";
                                }
                                else { MessageBox.Show("I see you're trying to cheat the system...", "XEX Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                            }
                            #endregion

                            #region PlayStation 3
                            else if (File.Exists(fbd_BrowseFolders.SelectedPath + @"\PS3_DISC.SFB"))
                            {
                                byte[] bytes = File.ReadAllBytes(fbd_BrowseFolders.SelectedPath + @"\PS3_DISC.SFB").Take(4).ToArray();
                                var hexString = BitConverter.ToString(bytes); hexString = hexString.Replace("-", " ");
                                if (hexString == "2E 53 46 42")
                                {
                                    lbl_SetDefault.Visible = false;
                                    pic_Logo.Visible = false;

                                    #region Building directory data...
                                    //Establishes the failsafe directory and copies the ARC prepare for the unpacking process.
                                    string failsafeCheck = Path.GetRandomFileName(); //Unpacked ARCs will have a unique directory to prevent overwriting.
                                    var arcBuildSession = new StringBuilder();
                                    arcBuildSession.Append(Properties.Settings.Default.archivesPath);
                                    arcBuildSession.Append(Tools.Global.sessionID);
                                    arcBuildSession.Append(@"\");
                                    arcBuildSession.Append(failsafeCheck);
                                    arcBuildSession.Append(@"\");
                                    if (!Directory.Exists(arcBuildSession.ToString())) Directory.CreateDirectory(arcBuildSession.ToString());
                                    #endregion

                                    #region Writing metadata...
                                    //Writes metadata to the unpacked directory to ensure the original path is remembered.
                                    var metadataWrite = File.Create(arcBuildSession.ToString() + "metadata.ini");
                                    var metadataSession = new UTF8Encoding(true).GetBytes(fbd_BrowseFolders.SelectedPath + @"\");
                                    metadataWrite.Write(metadataSession, 0, metadataSession.Length);
                                    metadataWrite.Close();
                                    #endregion

                                    //#region Building location data...
                                    ////Writes a file to store the failsafe directory to be referenced later.
                                    //var storageSession = new StringBuilder();
                                    //storageSession.Append(Properties.Settings.Default.archivesPath);
                                    //storageSession.Append(Tools.Global.sessionID);
                                    //storageSession.Append(@"\");
                                    //storageSession.Append(tab_Main.SelectedIndex);
                                    //var storageWrite = File.Create(storageSession.ToString());
                                    //var storageText = new UTF8Encoding(true).GetBytes(failsafeCheck);
                                    //storageWrite.Write(storageText, 0, storageText.Length);
                                    //storageWrite.Close();
                                    //#endregion

                                    #region Navigating...
                                    //Creates a new tab if the selected one is being used.
                                    if (tab_Main.SelectedTab.Text == "New Tab")
                                    {
                                        navigateToGame = false;
                                        resetTab();
                                    }
                                    else
                                    {
                                        navigateToGame = false;
                                        newTab();
                                    }
                                    #endregion

                                    tab_Main.SelectedTab.ToolTipText = failsafeCheck;
                                    Tools.Global.getStorage = failsafeCheck;
                                    Tools.Global.getIndex = tab_Main.SelectedIndex;

                                    currentARC().Navigate(fbd_BrowseFolders.SelectedPath);
                                    if (Path.GetFileName(fbd_BrowseFolders.SelectedPath) == "New Tab" || Path.GetFileName(fbd_BrowseFolders.SelectedPath).EndsWith(".arc"))
                                    {
                                        tab_Main.SelectedTab.Text = Path.GetFileName(fbd_BrowseFolders.SelectedPath) + " (Folder)";
                                    }
                                    else { tab_Main.SelectedTab.Text = Path.GetFileName(fbd_BrowseFolders.SelectedPath); }
                                    navigateToGame = true;

                                    Text = "Sonic '06 Toolkit - '" + fbd_BrowseFolders.SelectedPath + @"\'";
                                }
                                else { MessageBox.Show("I see you're trying to cheat the system...", "SFB Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                            }
                            else if (File.Exists(fbd_BrowseFolders.SelectedPath + @"\PARAM.SFO"))
                            {
                                byte[] bytes = File.ReadAllBytes(fbd_BrowseFolders.SelectedPath + @"\PARAM.SFO").Take(4).ToArray();
                                var hexString = BitConverter.ToString(bytes); hexString = hexString.Replace("-", " ");
                                if (hexString == "00 50 53 46")
                                {
                                    lbl_SetDefault.Visible = false;
                                    pic_Logo.Visible = false;

                                    #region Building directory data...
                                    //Establishes the failsafe directory and copies the ARC prepare for the unpacking process.
                                    string failsafeCheck = Path.GetRandomFileName(); //Unpacked ARCs will have a unique directory to prevent overwriting.
                                    var arcBuildSession = new StringBuilder();
                                    arcBuildSession.Append(Properties.Settings.Default.archivesPath);
                                    arcBuildSession.Append(Tools.Global.sessionID);
                                    arcBuildSession.Append(@"\");
                                    arcBuildSession.Append(failsafeCheck);
                                    arcBuildSession.Append(@"\");
                                    if (!Directory.Exists(arcBuildSession.ToString())) Directory.CreateDirectory(arcBuildSession.ToString());
                                    #endregion

                                    #region Writing metadata...
                                    //Writes metadata to the unpacked directory to ensure the original path is remembered.
                                    var metadataWrite = File.Create(arcBuildSession.ToString() + "metadata.ini");
                                    var metadataSession = new UTF8Encoding(true).GetBytes(fbd_BrowseFolders.SelectedPath + @"\");
                                    metadataWrite.Write(metadataSession, 0, metadataSession.Length);
                                    metadataWrite.Close();
                                    #endregion

                                    //#region Building location data...
                                    ////Writes a file to store the failsafe directory to be referenced later.
                                    //var storageSession = new StringBuilder();
                                    //storageSession.Append(Properties.Settings.Default.archivesPath);
                                    //storageSession.Append(Tools.Global.sessionID);
                                    //storageSession.Append(@"\");
                                    //storageSession.Append(tab_Main.SelectedIndex);
                                    //var storageWrite = File.Create(storageSession.ToString());
                                    //var storageText = new UTF8Encoding(true).GetBytes(failsafeCheck);
                                    //storageWrite.Write(storageText, 0, storageText.Length);
                                    //storageWrite.Close();
                                    //#endregion

                                    #region Navigating...
                                    //Creates a new tab if the selected one is being used.
                                    if (tab_Main.SelectedTab.Text == "New Tab")
                                    {
                                        navigateToGame = false;
                                        resetTab();
                                    }
                                    else
                                    {
                                        navigateToGame = false;
                                        newTab();
                                    }
                                    #endregion

                                    tab_Main.SelectedTab.ToolTipText = failsafeCheck;
                                    Tools.Global.getStorage = failsafeCheck;
                                    Tools.Global.getIndex = tab_Main.SelectedIndex;

                                    currentARC().Navigate(fbd_BrowseFolders.SelectedPath);
                                    if (Path.GetFileName(fbd_BrowseFolders.SelectedPath) == "New Tab" || Path.GetFileName(fbd_BrowseFolders.SelectedPath).EndsWith(".arc"))
                                    {
                                        tab_Main.SelectedTab.Text = Path.GetFileName(fbd_BrowseFolders.SelectedPath) + " (Folder)";
                                    }
                                    else { tab_Main.SelectedTab.Text = Path.GetFileName(fbd_BrowseFolders.SelectedPath); }
                                    navigateToGame = true;

                                    Text = "Sonic '06 Toolkit - '" + fbd_BrowseFolders.SelectedPath + @"\'";
                                }
                                else { MessageBox.Show("I see you're trying to cheat the system...", "SFO Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                            }
                            else if (File.Exists(fbd_BrowseFolders.SelectedPath + @"\EBOOT.BIN"))
                            {
                                byte[] bytes = File.ReadAllBytes(fbd_BrowseFolders.SelectedPath + @"\EBOOT.BIN").Take(3).ToArray();
                                var hexString = BitConverter.ToString(bytes); hexString = hexString.Replace("-", " ");
                                if (hexString == "53 43 45")
                                {
                                    lbl_SetDefault.Visible = false;
                                    pic_Logo.Visible = false;

                                    #region Building directory data...
                                    //Establishes the failsafe directory and copies the ARC prepare for the unpacking process.
                                    string failsafeCheck = Path.GetRandomFileName(); //Unpacked ARCs will have a unique directory to prevent overwriting.
                                    var arcBuildSession = new StringBuilder();
                                    arcBuildSession.Append(Properties.Settings.Default.archivesPath);
                                    arcBuildSession.Append(Tools.Global.sessionID);
                                    arcBuildSession.Append(@"\");
                                    arcBuildSession.Append(failsafeCheck);
                                    arcBuildSession.Append(@"\");
                                    if (!Directory.Exists(arcBuildSession.ToString())) Directory.CreateDirectory(arcBuildSession.ToString());
                                    #endregion

                                    #region Writing metadata...
                                    //Writes metadata to the unpacked directory to ensure the original path is remembered.
                                    var metadataWrite = File.Create(arcBuildSession.ToString() + "metadata.ini");
                                    var metadataSession = new UTF8Encoding(true).GetBytes(fbd_BrowseFolders.SelectedPath + @"\");
                                    metadataWrite.Write(metadataSession, 0, metadataSession.Length);
                                    metadataWrite.Close();
                                    #endregion

                                    //#region Building location data...
                                    ////Writes a file to store the failsafe directory to be referenced later.
                                    //var storageSession = new StringBuilder();
                                    //storageSession.Append(Properties.Settings.Default.archivesPath);
                                    //storageSession.Append(Tools.Global.sessionID);
                                    //storageSession.Append(@"\");
                                    //storageSession.Append(tab_Main.SelectedIndex);
                                    //var storageWrite = File.Create(storageSession.ToString());
                                    //var storageText = new UTF8Encoding(true).GetBytes(failsafeCheck);
                                    //storageWrite.Write(storageText, 0, storageText.Length);
                                    //storageWrite.Close();
                                    //#endregion

                                    #region Navigating...
                                    //Creates a new tab if the selected one is being used.
                                    if (tab_Main.SelectedTab.Text == "New Tab")
                                    {
                                        navigateToGame = false;
                                        resetTab();
                                    }
                                    else
                                    {
                                        navigateToGame = false;
                                        newTab();
                                    }
                                    #endregion

                                    tab_Main.SelectedTab.ToolTipText = failsafeCheck;
                                    Tools.Global.getStorage = failsafeCheck;
                                    Tools.Global.getIndex = tab_Main.SelectedIndex;

                                    currentARC().Navigate(fbd_BrowseFolders.SelectedPath);
                                    if (Path.GetFileName(fbd_BrowseFolders.SelectedPath) == "New Tab" || Path.GetFileName(fbd_BrowseFolders.SelectedPath).EndsWith(".arc"))
                                    {
                                        tab_Main.SelectedTab.Text = Path.GetFileName(fbd_BrowseFolders.SelectedPath) + " (Folder)";
                                    }
                                    else { tab_Main.SelectedTab.Text = Path.GetFileName(fbd_BrowseFolders.SelectedPath); }
                                    navigateToGame = true;

                                    Text = "Sonic '06 Toolkit - '" + fbd_BrowseFolders.SelectedPath + @"\'";
                                }
                                else { MessageBox.Show("I see you're trying to cheat the system...", "BIN Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                            }
                            else { MessageBox.Show("Please select a valid SONIC THE HEDGEHOG directory.", "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                        #endregion

                        else { MessageBox.Show("This directory does not exist.", "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
                else
                {
                    fbd_BrowseFolders.Description = "Please select a path.";

                    if (fbd_BrowseFolders.ShowDialog() == DialogResult.OK)
                    {
                        if (Directory.Exists(fbd_BrowseFolders.SelectedPath))
                        {

                            #region Building directory data...
                            //Establishes the failsafe directory and copies the ARC prepare for the unpacking process.
                            string failsafeCheck = Path.GetRandomFileName(); //Unpacked ARCs will have a unique directory to prevent overwriting.
                            var arcBuildSession = new StringBuilder();
                            arcBuildSession.Append(Properties.Settings.Default.archivesPath);
                            arcBuildSession.Append(Tools.Global.sessionID);
                            arcBuildSession.Append(@"\");
                            arcBuildSession.Append(failsafeCheck);
                            arcBuildSession.Append(@"\");
                            if (!Directory.Exists(arcBuildSession.ToString())) Directory.CreateDirectory(arcBuildSession.ToString());
                            #endregion

                            #region Writing metadata...
                            //Writes metadata to the unpacked directory to ensure the original path is remembered.
                            var metadataWrite = File.Create(arcBuildSession.ToString() + "metadata.ini");
                            var metadataSession = new UTF8Encoding(true).GetBytes(fbd_BrowseFolders.SelectedPath + @"\");
                            metadataWrite.Write(metadataSession, 0, metadataSession.Length);
                            metadataWrite.Close();
                            #endregion

                            //#region Building location data...
                            ////Writes a file to store the failsafe directory to be referenced later.
                            //var storageSession = new StringBuilder();
                            //storageSession.Append(Properties.Settings.Default.archivesPath);
                            //storageSession.Append(Tools.Global.sessionID);
                            //storageSession.Append(@"\");
                            //storageSession.Append(tab_Main.SelectedIndex);
                            //var storageWrite = File.Create(storageSession.ToString());
                            //var storageText = new UTF8Encoding(true).GetBytes(failsafeCheck);
                            //storageWrite.Write(storageText, 0, storageText.Length);
                            //storageWrite.Close();
                            //#endregion

                            #region Navigating...
                            //Creates a new tab if the selected one is being used.
                            if (tab_Main.SelectedTab.Text == "New Tab")
                            {
                                navigateToGame = false;
                                resetTab();
                            }
                            else
                            {
                                navigateToGame = false;
                                newTab();
                            }
                            #endregion

                            tab_Main.SelectedTab.ToolTipText = failsafeCheck;
                            Tools.Global.getStorage = failsafeCheck;
                            Tools.Global.getIndex = tab_Main.SelectedIndex;

                            currentARC().Navigate(fbd_BrowseFolders.SelectedPath);
                            if (Path.GetFileName(fbd_BrowseFolders.SelectedPath) == "New Tab" || Path.GetFileName(fbd_BrowseFolders.SelectedPath).EndsWith(".arc"))
                            {
                                tab_Main.SelectedTab.Text = Path.GetFileName(fbd_BrowseFolders.SelectedPath) + " (Folder)";
                            }
                            else { tab_Main.SelectedTab.Text = Path.GetFileName(fbd_BrowseFolders.SelectedPath); }
                            navigateToGame = true;

                            Text = "Sonic '06 Toolkit - '" + fbd_BrowseFolders.SelectedPath + @"\'";
                        }
                    }
                }
            }
        }

        #region Repack States
        void MainFile_RepackARC_Click(object sender, EventArgs e)
        {
            if (tab_Main.SelectedTab.Text.Contains(".arc"))
            {
                Tools.Global.arcState = "save";
            }
            else { Tools.Global.arcState = "save-as"; }

            repackARC();
        }

        void Btn_Repack_Click(object sender, EventArgs e)
        {
            if (tab_Main.SelectedTab.Text.Contains(".arc"))
            {
                Tools.Global.arcState = "save";
            }
            else { Tools.Global.arcState = "save-as"; }

            repackARC();
        }

        void RepackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tab_Main.SelectedTab.Text.Contains(".arc"))
            {
                Tools.Global.arcState = "save";
            }
            else { Tools.Global.arcState = "save-as"; }

            repackARC();
        }

        void RepackARCAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tools.Global.arcState = "save-as";
            repackARC();
        }

        void RepackAndLaunchXeniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tab_Main.SelectedTab.Text.Contains(".arc"))
            {
                Tools.Global.arcState = "save";
            }
            else { Tools.Global.arcState = "save-as"; }

            if (Properties.Settings.Default.xeniaFile != "")
            {
                xenia = true;
                repackARC();
            }
            else
            {
                SpecifyXenia();
            }
        }

        void MainFile_RepackARCAs_Click(object sender, EventArgs e)
        {
            Tools.Global.arcState = "save-as";
            repackARC();
        }
        #endregion

        #region Preferences
        //[Preferences] - Disable software updater
        //Disables the software update function on launch.
        void MainPreferences_DisableSoftwareUpdater_CheckedChanged(object sender, EventArgs e)
        {
            if (mainPreferences_DisableSoftwareUpdater.Checked == true)
            {
                tm_updateCheck.Stop();
                lbl_UpdateNotif.Visible = false;
                Properties.Settings.Default.disableSoftwareUpdater = true;
            }
            else
            {
                tm_updateCheck.Start();
                Properties.Settings.Default.disableSoftwareUpdater = false;
            }
            Properties.Settings.Default.Save();
        }

        //Paths
        //Opens a form to allow the user to enter their own paths.
        void MainPreferences_Paths_Click(object sender, EventArgs e)
        {
            new Paths().ShowDialog();
        }

        //[Paths] - Clear game directory
        //Resets the game directory.
        void Paths_ClearGame_Click(object sender, EventArgs e)
        {
            ClearGameDirectory();
        }

        void MainPreferences_DisableGameDirectory_CheckedChanged(object sender, EventArgs e)
        {
            if (mainPreferences_DisableGameDirectory.Checked)
            {
                ClearGameDirectory();
                lbl_SetDefault.Visible = false;

                Properties.Settings.Default.gameDir = false;
            }
            else
            {
                lbl_SetDefault.Visible = true;

                Properties.Settings.Default.gameDir = true;
            }
            Properties.Settings.Default.Save();
        }

        void ClearGameDirectory()
        {
            Properties.Settings.Default.gamePath = "";
            Properties.Settings.Default.Save();

            foreach (TabPage tab in tab_Main.TabPages)
            {
                if (tab.Text == "New Tab")
                {
                    tab_Main.TabPages.Remove(tab);
                    newTab();
                }
            }
        }

        void Preferences_FreeMode_CheckedChanged(object sender, EventArgs e)
        {
            if (preferences_FreeMode.Checked == true)
            {
                freeMode = true;
                mainFile_OpenSonic.Text = "Open Folder";
            }
            else
            {
                freeMode = false;
                mainFile_OpenSonic.Text = "Open SONIC THE HEDGEHOG";
            }
        }

        //[Paths] - Clear Xenia directory
        //Resets the Xenia directory.
        void ClearXeniaDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.xeniaFile = null;
            Properties.Settings.Default.Save();
        }

        //[Themes] - Compact
        //Moves certain controls in runtime to switch to the Compact theme.
        void MainThemes_Compact_CheckedChanged(object sender, EventArgs e)
        {
            if (mainThemes_Compact.Checked == true)
            {
                if (Properties.Settings.Default.showSessionID == false)
                {
                    btn_Repack.Left += 48;
                    btn_OpenFolder.Left += 48;
                }
                Properties.Settings.Default.theme = "Compact";
                mainThemes_Original.Checked = false;
                mstrip_Main.Left += 106;
                tab_Main.Height += 28; tab_Main.Top -= 28;
                btn_Back.Width -= 4; btn_Back.Height += 3; btn_Back.Left -= 5; btn_Back.Top -= 29; btn_Back.FlatAppearance.BorderSize = 1;
                btn_Forward.Width -= 10; btn_Forward.Height += 3; btn_Forward.Left -= 14; btn_Forward.Top -= 29; btn_Forward.FlatAppearance.BorderSize = 1;
                btn_NewTab.Width += 2; btn_NewTab.Height += 2; btn_NewTab.Left += 229; btn_NewTab.Top -= 28; btn_NewTab.BackColor = SystemColors.ControlLightLight; btn_NewTab.FlatAppearance.BorderSize = 1;
                btn_OpenFolder.Width += 3; btn_OpenFolder.Height += 3; btn_OpenFolder.Left -= 16; btn_OpenFolder.Top -= 29; btn_OpenFolder.BackColor = Color.FromArgb(232, 171, 83); btn_OpenFolder.FlatAppearance.BorderSize = 1;
                btn_Repack.Text = "Repack"; btn_Repack.Width -= 24; btn_Repack.Height += 3; btn_Repack.Left -= 18; btn_Repack.Top -= 29; btn_Repack.FlatAppearance.BorderSize = 1;
                btn_RepackOptions.Height += 3; btn_RepackOptions.Left -= 44; btn_RepackOptions.Top -= 29; btn_RepackOptions.FlatAppearance.BorderSize = 1;
                btn_SessionID.Height += 3; btn_SessionID.Left += 193; btn_SessionID.Top -= 29; btn_SessionID.BackColor = SystemColors.ControlLightLight; btn_SessionID.FlatAppearance.BorderColor = SystemColors.ControlLight;
                pnl_Updater.Left -= 205;
                lbl_SetDefault.Top = 53;
                lbl_UpdateNotif.Left += 132;

                Properties.Settings.Default.Save();
            }
            else { mainThemes_Original.Checked = true; }
        }

        //[Themes] - Original
        //Moves certain controls in runtime to switch to the Original theme.
        void MainThemes_Original_CheckedChanged(object sender, EventArgs e)
        {
            if (mainThemes_Original.Checked == true)
            {
                if (Properties.Settings.Default.showSessionID == false)
                {
                    btn_Repack.Left -= 48;
                    btn_OpenFolder.Left -= 48;
                }
                Properties.Settings.Default.theme = "Original";
                mainThemes_Compact.Checked = false;
                mstrip_Main.Left -= 106;
                tab_Main.Height -= 28; tab_Main.Top += 28;
                btn_Back.Width += 4; btn_Back.Height -= 3; btn_Back.Left += 5; btn_Back.Top += 29; btn_Back.FlatAppearance.BorderSize = 0;
                btn_Forward.Width += 10; btn_Forward.Height -= 3; btn_Forward.Left += 14; btn_Forward.Top += 29; btn_Forward.FlatAppearance.BorderSize = 0;
                btn_NewTab.Width -= 2; btn_NewTab.Height -= 2; btn_NewTab.Left -= 229; btn_NewTab.Top += 28; btn_NewTab.BackColor = SystemColors.ControlLightLight; btn_NewTab.FlatAppearance.BorderSize = 0;
                btn_OpenFolder.Width -= 3; btn_OpenFolder.Height -= 3; btn_OpenFolder.Left += 16; btn_OpenFolder.Top += 29; btn_OpenFolder.BackColor = SystemColors.ControlLightLight; btn_OpenFolder.FlatAppearance.BorderSize = 0;
                btn_Repack.Text = "Quick Repack"; btn_Repack.Width += 24; btn_Repack.Height -= 3; btn_Repack.Left += 18; btn_Repack.Top += 29; btn_Repack.FlatAppearance.BorderSize = 0;
                btn_RepackOptions.Height -= 3; btn_RepackOptions.Left += 44; btn_RepackOptions.Top += 29; btn_RepackOptions.FlatAppearance.BorderSize = 0;
                btn_SessionID.Height -= 3; btn_SessionID.Left -= 193; btn_SessionID.Top += 29; btn_SessionID.BackColor = SystemColors.ControlLight; btn_SessionID.FlatAppearance.BorderColor = SystemColors.WindowFrame;
                pnl_Updater.Left += 205;
                lbl_SetDefault.Top = 81;
                lbl_UpdateNotif.Left -= 132;

                Properties.Settings.Default.Save();
            }
            else { mainThemes_Compact.Checked = true; }
        }

        //[Themes] - Show Logo
        //Disables the Sonic '06 Toolkit logo appearing on new tabs.
        //void MainPreferences_ShowLogo_CheckedChanged(object sender, EventArgs e)
        #region //{
        public static bool debug = false;

        void Free_Mode(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (!debug)
                {
                    debug = true;
                    if (!Properties.Settings.Default.disableWarns)
                    {
                        main_File.DropDown.Visible = false;
                        MessageBox.Show("Debug Mode is now enabled.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    new Debugger(this).Show();
                    advanced_DebugMode.Checked = true;
                    Properties.Settings.Default.debugMode = true;
                }
                else
                {
                    debug = false;
                    if (!Properties.Settings.Default.disableWarns)
                    {
                        main_File.DropDown.Visible = false;
                        MessageBox.Show("Debug Mode is now disabled.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    Debugger debugger = Application.OpenForms["Debugger"] != null ? (Debugger)Application.OpenForms["Debugger"] : null;

                    if (debugger != null)
                    {
                        try
                        {
                            debugger = (Debugger)Application.OpenForms["Debugger"];
                            debugger.Close();
                        }
                        catch { }
                    }

                    advanced_DebugMode.Checked = false;
                    Properties.Settings.Default.debugMode = false;
                }

                Properties.Settings.Default.Save();
            }
        }

        public bool UpdaterVisibility
        {
            get { return pnl_Updater.Visible; }
            set { pnl_Updater.Visible = value; }
        }

        public bool BackdropVisibility
        {
            get { return btn_Backdrop.Visible; }
            set { btn_Backdrop.Visible = value; }
        }

        public bool UpdateNotifVisibility
        {
            get { return lbl_UpdateNotif.Visible; }
            set { lbl_UpdateNotif.Visible = value; }
        }

        public bool UpdateTimerState
        {
            get { return tm_updateCheck.Enabled; }
            set { tm_updateCheck.Enabled = value; }
        }

        public int UpdateProgressValue
        {
            get { return pgb_updateStatus.Value; }
            set { pgb_updateStatus.Value = value; }
        }

        public bool DisableUpdaterState
        {
            get { return mainPreferences_DisableSoftwareUpdater.Enabled; }
            set { mainPreferences_DisableSoftwareUpdater.Enabled = value; }
        }

        void Advanced_DebugMode_CheckedChanged(object sender, EventArgs e)
        {
            if (advanced_DebugMode.Checked == true)
            {
                if (!debug)
                {
                    debug = true;
                    new Debugger(this).Show();
                    Properties.Settings.Default.debugMode = true;
                }
            }
            else
            {
                debug = false;
                Debugger debugger = Application.OpenForms["Debugger"] != null ? (Debugger)Application.OpenForms["Debugger"] : null;

                if (debugger != null)
                {
                    try
                    {
                        debugger = (Debugger)Application.OpenForms["Debugger"];
                        debugger.Close();
                    }
                    catch { }
                }

                advanced_DebugMode.Checked = false;
                Properties.Settings.Default.debugMode = false;
            }

            Properties.Settings.Default.Save();
        }
        #endregion
        //    if (mainPreferences_ShowLogo.Checked == true) Properties.Settings.Default.showLogo = true;
        //    else Properties.Settings.Default.showLogo = false;
        #region //    Properties.Settings.Default.Save();
        private void None(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                btn_Back.Text = "None";
                btn_Forward.Text = "None";
                btn_OpenFolder.Text = "None";
                btn_Repack.Text = "None";
                main_File.Text = "None";
                main_SDK.Text = "None";
                main_Shortcuts.Text = "None";
                main_Window.Text = "None";
                main_Help.Text = "None";
                Text = "None";
            }
        }
        #endregion
        //}

        //[Themes] - Show Session ID
        //Moves certain controls in runtime to hide the Session ID properly.
        void MainPreferences_ShowSessionID_CheckedChanged(object sender, EventArgs e)
        {
            if (mainPreferences_ShowSessionID.Checked == true)
            {
                if (Properties.Settings.Default.theme == "Compact")
                {
                    Properties.Settings.Default.showSessionID = true;
                    btn_SessionID.Visible = true;
                    btn_Repack.Left -= 48;
                    btn_OpenFolder.Left -= 48;
                }
                else if (Properties.Settings.Default.theme == "Original")
                {
                    Properties.Settings.Default.showSessionID = true;
                    btn_SessionID.Visible = true;
                }
            }
            else
            {
                if (Properties.Settings.Default.theme == "Compact")
                {
                    Properties.Settings.Default.showSessionID = false;
                    btn_SessionID.Visible = false;
                    btn_Repack.Left += 48;
                    btn_OpenFolder.Left += 48;
                }
                else if (Properties.Settings.Default.theme == "Original")
                {
                    Properties.Settings.Default.showSessionID = false;
                    btn_SessionID.Visible = false;
                }
            }
            Properties.Settings.Default.Save();
        }

        //[ARC] - Unpack and launch Sonic '06 Toolkit
        //Sets the ARC launch procedure to launch Sonic '06 Toolkit upon unpack.
        void ARC_UnpackAndLaunch_CheckedChanged(object sender, EventArgs e)
        {
            if (ARC_UnpackAndLaunch.Checked == true)
            {
                Properties.Settings.Default.unpackAndLaunch = true;
                ARC_UnpackAndLaunch.Checked = true;
                ARC_UnpackRoot.Checked = false;
            }
            else
            {
                ARC_UnpackAndLaunch.Checked = false;
                ARC_UnpackRoot.Checked = true;
            }
            Properties.Settings.Default.Save();
        }

        //[ARC] - Unpack in archive directory
        //Sets the ARC launch procedure to unpack and finish.
        void ARC_UnpackRoot_CheckedChanged(object sender, EventArgs e)
        {
            if (ARC_UnpackRoot.Checked == true)
            {
                Properties.Settings.Default.unpackAndLaunch = false;
                ARC_UnpackAndLaunch.Checked = false;
                ARC_UnpackRoot.Checked = true;
            }
            else
            {
                ARC_UnpackAndLaunch.Checked = true;
                ARC_UnpackRoot.Checked = false;
            }
            Properties.Settings.Default.Save();
        }

        //[Advanced] - Disable warning messages
        //Disables all warning messages.
        void Advanced_DisableWarnings_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.disableWarns == false)
            {
                DialogResult disableConfirm = MessageBox.Show("Are you sure you want to disable warnings and notifications?", "Sonic '06 Toolkit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                switch (disableConfirm)
                {
                    case DialogResult.Yes:
                        advanced_DisableWarnings.Checked = true;
                        Properties.Settings.Default.disableWarns = true;
                        break;

                    case DialogResult.No:
                        advanced_DisableWarnings.Checked = false;
                        Properties.Settings.Default.disableWarns = false;
                        break;
                }
            }
            else
            {
                advanced_DisableWarnings.Checked = false;
                Properties.Settings.Default.disableWarns = false;
            }

            Properties.Settings.Default.Save();
        }
        #endregion

        void MainFile_CloseARC_Click(object sender, EventArgs e)
        {
            //Asks for user confirmation before closing an archive.
            DialogResult confirmClosure = MessageBox.Show("Are you sure you want to close this archive? All unsaved changes will be lost if you haven't repacked.", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch (confirmClosure)
            {
                case DialogResult.Yes:
                    resetTab();
                    break;
            }
        }

        void resetTab()
        {
            //Creates a new web browser instance (which hooks into File Explorer).
            currentARC().Dispose();
            var nextBrowser = new WebBrowser();
            tab_Main.SelectedTab.Text = "New Tab";
            tab_Main.SelectedTab.Controls.Add(nextBrowser);
            nextBrowser.Dock = DockStyle.Fill;
            if (navigateToGame == true)
            {
                if (Properties.Settings.Default.gamePath != "")
                {
                    if (Directory.Exists(Properties.Settings.Default.gamePath))
                    {
                        currentARC().Navigate(Properties.Settings.Default.gamePath);
                        lbl_SetDefault.Visible = false;
                        pic_Logo.Visible = false;
                    }
                }
            }
            currentARC().AllowWebBrowserDrop = false;
            Text = "Sonic '06 Toolkit";
        }

        void MainFile_Exit_Click(object sender, EventArgs e)
        {
            //Checks if the only tab is called 'New Tab' before asking for confirmation.
            if (tab_Main.TabPages.Count == 1 && tab_Main.SelectedTab.Text == "New Tab")
            {
                Application.Exit();
            }
            else
            {
                DialogResult confirmUserClosure = MessageBox.Show("Are you sure you want to quit? All unsaved changes will be lost if you haven't repacked.", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                switch (confirmUserClosure)
                {
                    case DialogResult.Yes:
                        Application.Exit();
                        break;
                }
            }
        }
        #endregion

        #region SDK
        void MainSDK_ADXStudio_Click(object sender, EventArgs e)
        {
            if (Paths.changes == true) { MessageBox.Show("A restart for Sonic '06 Toolkit is pending.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                new ADX_Studio().ShowDialog();
            }
        }

        void MainSDK_ARCStudio_Click(object sender, EventArgs e)
        {
            if (Paths.changes == true) { MessageBox.Show("A restart for Sonic '06 Toolkit is pending.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                new ARC_Studio().ShowDialog();
            }
        }

        void MainSDK_AT3Studio_Click(object sender, EventArgs e)
        {
            if (Paths.changes == true) { MessageBox.Show("A restart for Sonic '06 Toolkit is pending.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                new AT3_Studio().ShowDialog();
            }
        }

        void MainSDK_CSBStudio_Click(object sender, EventArgs e)
        {
            if (Paths.changes == true) { MessageBox.Show("A restart for Sonic '06 Toolkit is pending.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                new CSB_Studio().ShowDialog();
            }
        }

        void MainSDK_DDSStudio_Click(object sender, EventArgs e)
        {
            if (Paths.changes == true) { MessageBox.Show("A restart for Sonic '06 Toolkit is pending.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                new DDS_Studio().ShowDialog();
            }
        }

        void MainSDK_LUBStudio_Click(object sender, EventArgs e)
        {
            //This process needs work. It would be better to decompile directly with a C# decompiler, rather than depending on a Java decompiler.
            //It's based on Lua's own source, so it wouldn't be too difficult to set up (if you know what you're doing).
            if (Tools.Global.javaCheck == false) MessageBox.Show("Java is required to decompile Lua binaries. Please install Java and restart Sonic '06 Toolkit.", "Java Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (Paths.changes == true) { MessageBox.Show("A restart for Sonic '06 Toolkit is pending.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                else
                {
                    new LUB_Studio().ShowDialog();
                }
            }
        }

        void MainSDK_MSTStudio_Click(object sender, EventArgs e)
        {
            if (Paths.changes == true) { MessageBox.Show("A restart for Sonic '06 Toolkit is pending.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                new MST_Studio().ShowDialog();
            }
        }

        void MainSDK_SETStudio_Click(object sender, EventArgs e)
        {
            if (Paths.changes == true) { MessageBox.Show("A restart for Sonic '06 Toolkit is pending.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                new SET_Studio().ShowDialog();
            }
        }

        void MainSDK_XNOStudio_Click(object sender, EventArgs e)
        {
            if (Paths.changes == true) { MessageBox.Show("A restart for Sonic '06 Toolkit is pending.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                new XNO_Studio().ShowDialog();
            }
        }
        #endregion

        #region Shortcuts
        void Shortcuts_ExtractCSBs_Click(object sender, EventArgs e)
        {
            if (Paths.changes == true) { MessageBox.Show("A restart for Sonic '06 Toolkit is pending.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                if (Directory.GetFiles(Tools.Global.currentPath, "*.csb").Length == 0) MessageBox.Show("There are no CSBs to unpack in this directory.", "No CSBs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    try
                    {
                        //Gets all checked boxes from the CheckedListBox and builds a string for each CSB.
                        foreach (string CSB in Directory.GetFiles(Tools.Global.currentPath, "*.csb", SearchOption.TopDirectoryOnly))
                        {
                            if (File.Exists(CSB))
                            {
                                Tools.Global.csbState = "unpack";
                                Tools.CSB.Packer(null, CSB);
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when unpacking the CSBs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Tools.Notification.Dispose();
                    }
                }
            }
        }

        void Shortcuts_ConvertDDS_Click(object sender, EventArgs e)
        {
            if (Paths.changes == true) { MessageBox.Show("A restart for Sonic '06 Toolkit is pending.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                //Checks if there are any DDSs in the directory.
                if (Directory.GetFiles(Tools.Global.currentPath, "*.dds").Length == 0) MessageBox.Show("There are no DDS files to convert in this directory.", "No DDS files available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    foreach (string DDS in Directory.GetFiles(Tools.Global.currentPath, "*.dds", SearchOption.TopDirectoryOnly))
                    {
                        Tools.Global.ddsState = "dds";
                        Tools.DDS.Convert(string.Empty, DDS);
                    }
                }
            }
        }

        void Shortcuts_DecompileLUBs_Click(object sender, EventArgs e)
        {
            if (Paths.changes == true) { MessageBox.Show("A restart for Sonic '06 Toolkit is pending.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                if (Tools.Global.javaCheck == false) MessageBox.Show("Java is required to decompile Lua binaries. Please install Java and restart Sonic '06 Toolkit.", "Java Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    //Checks if there are any DDSs in the directory.
                    if (Directory.GetFiles(Tools.Global.currentPath, "*.lub").Length == 0) MessageBox.Show("There are no Lua binaries to decompile in this directory.", "No Lua binaries available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        try
                        {
                            Tools.Global.lubState = "decompile-all";
                            Tools.LUB.Decompile(string.Empty, string.Empty);
                        }
                        catch
                        {
                            MessageBox.Show("An error occurred when decompiling the Lua binaries.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Tools.Notification.Dispose();
                        }
                    }
                }
            }
        }

        void Shortcuts_DecodeMSTs_Click(object sender, EventArgs e)
        {
            if (Paths.changes == true) { MessageBox.Show("A restart for Sonic '06 Toolkit is pending.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                //Checks if there are any MSTs in the directory.
                if (Directory.GetFiles(Tools.Global.currentPath, "*.mst").Length == 0) MessageBox.Show("There are no MSTs to decode in this directory.", "No MSTs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    try
                    {
                        foreach (string MST in Directory.GetFiles(Tools.Global.currentPath, "*.mst", SearchOption.TopDirectoryOnly))
                        {
                            Tools.Global.mstState = "mst";
                            Tools.MST.Export(string.Empty, MST);
                        }
                        MessageBox.Show("All MSTs have been exported in this directory.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when decoding the MSTs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Tools.Notification.Dispose();
                    }
                }
            }
        }

        void Shortcuts_ConvertSETs_Click(object sender, EventArgs e)
        {
            if (Paths.changes == true) { MessageBox.Show("A restart for Sonic '06 Toolkit is pending.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                //Checks if there are any SETs in the directory.
                if (Directory.GetFiles(Tools.Global.currentPath, "*.set").Length == 0) MessageBox.Show("There are no SETs to export in this directory.", "No SETs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    try
                    {
                        foreach (string SET in Directory.GetFiles(Tools.Global.currentPath, "*.set", SearchOption.TopDirectoryOnly))
                        {
                            Tools.Global.setState = "export";
                            Tools.SET.Export(string.Empty, SET);
                        }
                        MessageBox.Show("All SETs have been exported in this directory.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when exporting the SETs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Tools.Notification.Dispose();
                    }
                }
            }
        }

        void Shortcuts_ConvertXNOs_Click(object sender, EventArgs e)
        {
            if (Paths.changes == true) { MessageBox.Show("A restart for Sonic '06 Toolkit is pending.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            else
            {
                //Checks if there are any XNOs in the directory.
                if (Directory.GetFiles(Tools.Global.currentPath, "*.xno").Length == 0) MessageBox.Show("There are no XNOs to convert in this directory.", "No XNOs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    try
                    {
                        //Gets all checked boxes from the CheckedListBox and builds a string for each XNO.
                        foreach (string XNO in Directory.GetFiles(Tools.Global.currentPath, "*.xno", SearchOption.TopDirectoryOnly))
                        {
                            Tools.Global.xnoState = "xno";
                            Tools.XNO.Convert(string.Empty, XNO);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when converting the XNOs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Tools.Notification.Dispose();
                    }
                }
            }
        }

        void MainShortcuts_Xenia_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.xeniaFile != "")
            {
                if (File.Exists(Properties.Settings.Default.xeniaFile))
                {
                    var xeniaLocation = new ProcessStartInfo(Properties.Settings.Default.xeniaFile);
                    xeniaLocation.WorkingDirectory = Path.GetDirectoryName(Properties.Settings.Default.xeniaFile);
                    Process.Start(xeniaLocation);
                }
                else SpecifyXenia();
            }
            else
            {
                SpecifyXenia();
            }
        }

        void SpecifyXenia()
        {
            xenia = false;

            MessageBox.Show("Please specify your executable file for Xenia.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ofd_OpenFiles.Title = "Please select a Xenia executable...";
            ofd_OpenFiles.Filter = "Programs|*.exe";
            ofd_OpenFiles.DefaultExt = "exe";

            if (ofd_OpenFiles.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(ofd_OpenFiles.FileName)) Properties.Settings.Default.xeniaFile = ofd_OpenFiles.FileName;
                Properties.Settings.Default.Save();
            }
        }
        #endregion

        #region Window
        void MainWindow_NewTab_Click(object sender, EventArgs e)
        {
            newTab();
        }

        void MainWindow_NewWindow_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(Application.ExecutablePath);
            }
            catch { MessageBox.Show("Failed to open a new window.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        void MainWindow_CloseTab_Click(object sender, EventArgs e)
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

        void MainWindow_CloseWindow_Click(object sender, EventArgs e)
        {
            //Checks if the only tab is called 'New Tab' before asking for confirmation.
            if (tab_Main.TabPages.Count == 1 && tab_Main.SelectedTab.Text == "New Tab")
            {
                Application.Exit();
            }
            else
            {
                DialogResult confirmUserClosure = MessageBox.Show("Are you sure you want to quit? All unsaved changes will be lost if you haven't repacked.", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                switch (confirmUserClosure)
                {
                    case DialogResult.Yes:
                        Application.Exit();
                        break;
                }
            }
        }
        #endregion

        #region Help
        void MainHelp_Documentation_Click(object sender, EventArgs e)
        {
            //Opens the Documentation form in the centre of the parent window without disabling it.
            var documentation = new Documentation();
            var parentLeft = Left + ((Width - documentation.Width) / 2);
            var parentTop = Top + ((Height - documentation.Height) / 2);
            documentation.Location = new System.Drawing.Point(parentLeft, parentTop);
            documentation.Show();
        }

        void MainHelp_CheckForUpdates_Click(object sender, EventArgs e)
        {
            if (Tools.Global.serverStatus == "offline") MessageBox.Show("Unable to establish a connection to SEGA Carnival.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (Tools.Global.serverStatus == "down") MessageBox.Show("The update servers are currently undergoing maintenance. Apologies for the inconvenience.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else Tools.Global.updateState = "user"; CheckForUpdates(Tools.Global.latestVersion, "https://segacarnival.com/hyper/updates/latest-master.exe", "https://segacarnival.com/hyper/updates/lib.dll", "https://segacarnival.com/hyper/updates/latest_master.txt");
        }

        void Lbl_UpdateNotif_Click(object sender, EventArgs e)
        {
            if (Tools.Global.serverStatus == "offline") MessageBox.Show("Unable to establish a connection to SEGA Carnival.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (Tools.Global.serverStatus == "down") MessageBox.Show("The update servers are currently undergoing maintenance. Apologies for the inconvenience.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else Tools.Global.updateState = "user"; CheckForUpdates(Tools.Global.latestVersion, "https://segacarnival.com/hyper/updates/latest-master.exe", "https://segacarnival.com/hyper/updates/lib.dll", "https://segacarnival.com/hyper/updates/latest_master.txt");
        }

        void MainHelp_About_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }
        #endregion

        #region Controls
        void Btn_Back_Click(object sender, EventArgs e)
        {
            currentARC().GoBack();
        }

        void Btn_Forward_Click(object sender, EventArgs e)
        {
            currentARC().GoForward();
        }

        void Btn_NewTab_Click(object sender, EventArgs e)
        {
            newTab();
        }

        void Btn_OpenFolder_Click(object sender, EventArgs e)
        {
            Process.Start(currentARC().Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\");
        }

        static bool repackMenu = false;

        void Btn_RepackOptions_Click(object sender, EventArgs e)
        {
            if (repackMenu == false)
            {
                cms_Repack.Show(btn_Repack, new Point(0, btn_Repack.Height));
                if (Properties.Settings.Default.theme != "Original")
                {
                    btn_RepackOptions.FlatAppearance.BorderSize = 0;
                    btn_RepackOptions.Height = 25;
                }
                btn_RepackOptions.BackColor = Color.DarkSeaGreen;
                btn_Repack.BackColor = Color.DarkSeaGreen;
                repackMenu = true;
            }
            else
            {
                cms_Repack.Hide();
                if (Properties.Settings.Default.theme != "Original")
                {
                    btn_RepackOptions.FlatAppearance.BorderSize = 1;
                    btn_RepackOptions.Height = 26;
                }
                btn_RepackOptions.BackColor = Color.LightGreen;
                btn_Repack.BackColor = Color.LightGreen;
                repackMenu = false;
            }
        }

        void Cms_Repack_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (Properties.Settings.Default.theme != "Original")
            {
                btn_RepackOptions.FlatAppearance.BorderSize = 1;
                btn_RepackOptions.Height = 26;
            }
            btn_RepackOptions.BackColor = Color.LightGreen;
            btn_Repack.BackColor = Color.LightGreen;
            repackMenu = false;
        }

        void Tab_Main_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Tools.Global.getIndex = tab_Main.SelectedIndex;
                Tools.Global.getStorage = GetStorage;

                if (tab_Main.SelectedTab.Text != "New Tab")
                {
                    //Reads the metadata to get the original location of the ARC.
                    string repackBuildSession = $"{Properties.Settings.Default.archivesPath}{Tools.Global.sessionID}\\{GetStorage}\\";

                    if (File.Exists(repackBuildSession + "metadata.ini"))
                    {
                        string metadata = File.ReadAllText(repackBuildSession + "metadata.ini");
                        Text = "Sonic '06 Toolkit - '" + metadata + "'";
                    }
                    else
                    {
                        MessageBox.Show("This archive's metadata is missing or unreadable. It will be removed from the session.", "Metadata Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        resetTab();
                    }
                }
                else { Text = "Sonic '06 Toolkit"; }
            }
            catch { Text = "Sonic '06 Toolkit"; }
        }

        void Tm_updateCheck_Tick(object sender, EventArgs e)
        {
            if (Tools.Global.serverStatus == "offline") MessageBox.Show("Unable to establish a connection to SEGA Carnival.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (Tools.Global.serverStatus == "down") MessageBox.Show("The update servers are currently undergoing maintenance. Apologies for the inconvenience.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else Tools.Global.updateState = "check"; CheckForUpdates(Tools.Global.latestVersion, "https://segacarnival.com/hyper/updates/latest-master.exe", "https://segacarnival.com/hyper/updates/lib.dll", "https://segacarnival.com/hyper/updates/latest_master.txt");
        }

        void Tm_tabCheck_Tick(object sender, EventArgs e)
        {
            if (freeMode)
            {
                if (tab_Main.SelectedTab.Text != "New Tab")
                {
                    #region Enable controls...
                    //Enables all viable controls if the tab isn't empty.
                    btn_Back.Enabled = true;
                    btn_Forward.Enabled = true;
                    btn_OpenFolder.Enabled = true;
                    shortcuts_DecompileLUBs.Enabled = true;
                    mainSDK_LUBStudio.Enabled = true;
                    mainFile_CloseARC.Enabled = true;
                    mainSDK_XNOStudio.Enabled = true;
                    shortcuts_ConvertXNOs.Enabled = true;
                    mainSDK_CSBStudio.Enabled = true;
                    mainSDK_ADXStudio.Enabled = true;
                    shortcuts_ExtractCSBs.Enabled = true;
                    shortcuts_ConvertSETs.Enabled = true;
                    mainSDK_SETStudio.Enabled = true;
                    mainSDK_MSTStudio.Enabled = true;
                    shortcuts_DecodeMSTs.Enabled = true;
                    mainSDK_AT3Studio.Enabled = true;
                    mainSDK_DDSStudio.Enabled = true;
                    shortcuts_ConvertDDS.Enabled = true;
                    btn_Repack.Enabled = true;
                    btn_RepackOptions.Enabled = true;
                    mainFile_RepackARC.Enabled = true;
                    mainFile_RepackARCAs.Enabled = true;
                    #endregion

                    pic_Logo.Visible = false;
                    lbl_SetDefault.Visible = false;

                    try
                    {
                        if (currentARC().Url != null)
                        {
                            //Updates the currentPath global variable.
                            Tools.Global.currentPath = currentARC().Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\";
                        }
                    }
                    catch { }
                }
                else if (tab_Main.SelectedTab.Text == "New Tab" && Properties.Settings.Default.gamePath != "")
                {
                    #region Enable controls...
                    //Enables all viable controls if the tab isn't empty.
                    btn_Back.Enabled = true;
                    btn_Forward.Enabled = true;
                    btn_OpenFolder.Enabled = true;
                    shortcuts_DecompileLUBs.Enabled = true;
                    mainSDK_LUBStudio.Enabled = true;
                    mainFile_CloseARC.Enabled = true;
                    mainSDK_XNOStudio.Enabled = true;
                    shortcuts_ConvertXNOs.Enabled = true;
                    mainSDK_CSBStudio.Enabled = true;
                    mainSDK_ADXStudio.Enabled = true;
                    shortcuts_ExtractCSBs.Enabled = true;
                    shortcuts_ConvertSETs.Enabled = true;
                    mainSDK_SETStudio.Enabled = true;
                    mainSDK_MSTStudio.Enabled = true;
                    shortcuts_DecodeMSTs.Enabled = true;
                    mainSDK_AT3Studio.Enabled = true;
                    mainSDK_DDSStudio.Enabled = true;
                    shortcuts_ConvertDDS.Enabled = true;
                    btn_Repack.Enabled = false;
                    btn_RepackOptions.Enabled = false;
                    mainFile_RepackARC.Enabled = false;
                    mainFile_RepackARCAs.Enabled = false;
                    #endregion

                    pic_Logo.Visible = false;
                    lbl_SetDefault.Visible = false;

                    try
                    {
                        if (currentARC().Url != null)
                        {
                            //Updates the currentPath global variable.
                            Tools.Global.currentPath = currentARC().Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\";
                        }
                    }
                    catch { }
                }
                else
                {
                    #region Disable controls...
                    //Disables all viable controls if the tab is empty.
                    btn_Back.Enabled = false;
                    btn_Forward.Enabled = false;
                    btn_Repack.Enabled = false;
                    mainFile_RepackARC.Enabled = false;
                    btn_OpenFolder.Enabled = false;
                    shortcuts_DecompileLUBs.Enabled = false;
                    mainSDK_LUBStudio.Enabled = false;
                    mainFile_CloseARC.Enabled = false;
                    mainFile_RepackARCAs.Enabled = false;
                    mainSDK_XNOStudio.Enabled = false;
                    shortcuts_ConvertXNOs.Enabled = false;
                    mainSDK_CSBStudio.Enabled = false;
                    mainSDK_ADXStudio.Enabled = false;
                    shortcuts_ExtractCSBs.Enabled = false;
                    shortcuts_ConvertSETs.Enabled = false;
                    mainSDK_SETStudio.Enabled = false;
                    mainSDK_MSTStudio.Enabled = false;
                    shortcuts_DecodeMSTs.Enabled = false;
                    mainSDK_AT3Studio.Enabled = false;
                    btn_RepackOptions.Enabled = false;
                    mainSDK_DDSStudio.Enabled = false;
                    shortcuts_ConvertDDS.Enabled = false;
                    #endregion

                    pic_Logo.Visible = true;
                    if (mainPreferences_DisableGameDirectory.Checked == false) lbl_SetDefault.Visible = true;
                }
            }
            else
            {
                if (tab_Main.SelectedTab.Text != "New Tab" && !tab_Main.SelectedTab.Text.Contains(".arc"))
                {
                    #region Set controls...
                    //Enables all viable controls if the tab isn't empty.
                    btn_Back.Enabled = true;
                    btn_Forward.Enabled = true;
                    btn_Repack.Enabled = false;
                    mainFile_RepackARC.Enabled = false;
                    btn_OpenFolder.Enabled = true;
                    shortcuts_DecompileLUBs.Enabled = false;
                    mainSDK_LUBStudio.Enabled = false;
                    mainFile_CloseARC.Enabled = false;
                    mainFile_RepackARCAs.Enabled = false;
                    mainSDK_XNOStudio.Enabled = false;
                    shortcuts_ConvertXNOs.Enabled = false;
                    mainSDK_CSBStudio.Enabled = false;
                    mainSDK_ADXStudio.Enabled = false;
                    shortcuts_ExtractCSBs.Enabled = false;
                    shortcuts_ConvertSETs.Enabled = false;
                    mainSDK_SETStudio.Enabled = false;
                    mainSDK_MSTStudio.Enabled = false;
                    shortcuts_DecodeMSTs.Enabled = false;
                    mainSDK_AT3Studio.Enabled = true;
                    btn_RepackOptions.Enabled = false;
                    mainSDK_DDSStudio.Enabled = false;
                    shortcuts_ConvertDDS.Enabled = false;
                    #endregion

                    pic_Logo.Visible = false;
                    lbl_SetDefault.Visible = false;

                    try
                    {
                        if (currentARC().Url != null)
                        {
                            //Updates the currentPath global variable.
                            Tools.Global.currentPath = currentARC().Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\";
                        }
                    }
                    catch { }
                }
                else if (tab_Main.SelectedTab.Text != "New Tab" || tab_Main.SelectedTab.Text.Contains(".arc"))
                {
                    #region Enable controls...
                    //Enables all viable controls if the tab isn't empty.
                    btn_Back.Enabled = true;
                    btn_Forward.Enabled = true;
                    btn_Repack.Enabled = true;
                    mainFile_RepackARC.Enabled = true;
                    btn_OpenFolder.Enabled = true;
                    shortcuts_DecompileLUBs.Enabled = true;
                    mainSDK_LUBStudio.Enabled = true;
                    mainFile_CloseARC.Enabled = true;
                    mainFile_RepackARCAs.Enabled = true;
                    mainSDK_XNOStudio.Enabled = true;
                    shortcuts_ConvertXNOs.Enabled = true;
                    mainSDK_CSBStudio.Enabled = true;
                    mainSDK_ADXStudio.Enabled = true;
                    shortcuts_ExtractCSBs.Enabled = true;
                    shortcuts_ConvertSETs.Enabled = true;
                    mainSDK_SETStudio.Enabled = true;
                    mainSDK_MSTStudio.Enabled = true;
                    shortcuts_DecodeMSTs.Enabled = true;
                    mainSDK_AT3Studio.Enabled = false;
                    btn_RepackOptions.Enabled = true;
                    mainSDK_DDSStudio.Enabled = true;
                    shortcuts_ConvertDDS.Enabled = true;
                    #endregion

                    pic_Logo.Visible = false;
                    lbl_SetDefault.Visible = false;

                    try
                    {
                        if (currentARC().Url != null)
                        {
                            //Updates the currentPath global variable.
                            Tools.Global.currentPath = currentARC().Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\";
                        }
                    }
                    catch { }
                }
                else if (tab_Main.SelectedTab.Text == "New Tab" && Properties.Settings.Default.gamePath != "")
                {
                    #region Set controls...
                    //Enables all viable controls if the tab isn't empty.
                    btn_Back.Enabled = true;
                    btn_Forward.Enabled = true;
                    btn_Repack.Enabled = false;
                    mainFile_RepackARC.Enabled = false;
                    btn_OpenFolder.Enabled = true;
                    shortcuts_DecompileLUBs.Enabled = false;
                    mainSDK_LUBStudio.Enabled = false;
                    mainFile_CloseARC.Enabled = false;
                    mainFile_RepackARCAs.Enabled = false;
                    mainSDK_XNOStudio.Enabled = false;
                    shortcuts_ConvertXNOs.Enabled = false;
                    mainSDK_CSBStudio.Enabled = false;
                    mainSDK_ADXStudio.Enabled = false;
                    shortcuts_ExtractCSBs.Enabled = false;
                    shortcuts_ConvertSETs.Enabled = false;
                    mainSDK_SETStudio.Enabled = false;
                    mainSDK_MSTStudio.Enabled = false;
                    shortcuts_DecodeMSTs.Enabled = false;
                    mainSDK_AT3Studio.Enabled = true;
                    btn_RepackOptions.Enabled = false;
                    mainSDK_DDSStudio.Enabled = false;
                    shortcuts_ConvertDDS.Enabled = false;
                    #endregion

                    pic_Logo.Visible = false;
                    lbl_SetDefault.Visible = false;

                    try
                    {
                        if (currentARC().Url != null)
                        {
                            //Updates the currentPath global variable.
                            Tools.Global.currentPath = currentARC().Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\";
                        }
                    }
                    catch { }
                }
                else
                {
                    #region Disable controls...
                    //Disables all viable controls if the tab is empty.
                    btn_Back.Enabled = false;
                    btn_Forward.Enabled = false;
                    btn_Repack.Enabled = false;
                    mainFile_RepackARC.Enabled = false;
                    btn_OpenFolder.Enabled = false;
                    shortcuts_DecompileLUBs.Enabled = false;
                    mainSDK_LUBStudio.Enabled = false;
                    mainFile_CloseARC.Enabled = false;
                    mainFile_RepackARCAs.Enabled = false;
                    mainSDK_XNOStudio.Enabled = false;
                    shortcuts_ConvertXNOs.Enabled = false;
                    mainSDK_CSBStudio.Enabled = false;
                    mainSDK_ADXStudio.Enabled = false;
                    shortcuts_ExtractCSBs.Enabled = false;
                    shortcuts_ConvertSETs.Enabled = false;
                    mainSDK_SETStudio.Enabled = false;
                    mainSDK_MSTStudio.Enabled = false;
                    shortcuts_DecodeMSTs.Enabled = false;
                    mainSDK_AT3Studio.Enabled = false;
                    btn_RepackOptions.Enabled = false;
                    mainSDK_DDSStudio.Enabled = false;
                    shortcuts_ConvertDDS.Enabled = false;
                    #endregion

                    pic_Logo.Visible = true;
                    if (mainPreferences_DisableGameDirectory.Checked == false) lbl_SetDefault.Visible = true;
                }
            }

            if (Tools.Global.gameChanged == true)
            {
                foreach (TabPage tab in tab_Main.TabPages)
                {
                    if (tab.Text == "New Tab")
                    {
                        tab_Main.TabPages.Remove(tab);
                        newTab();
                    }
                }

                Tools.Global.gameChanged = false;
            }

            FormTop = Top;
            FormLeft = Left;
            FormWidth = Width;
            FormHeight = Height;
        }

        void Tab_Main_MouseClick(object sender, MouseEventArgs e)
        {
            var mainTab = sender as TabControl;
            var tabs = mainTab.TabPages;

            if (e.Button == MouseButtons.Middle)
            {
                if (tab_Main.TabPages.Count != 1)
                {
                    //Checks if the tab's text reads 'New Tab' (a name only assigned by the application).
                    if (tabs.Cast<TabPage>().Where((t, i) => mainTab.GetTabRect(i).Contains(e.Location)).First().Text == "New Tab")
                    {
                        tabs.Remove(tabs.Cast<TabPage>().Where((t, i) => mainTab.GetTabRect(i).Contains(e.Location)).First());
                    }
                    else
                    {
                        DialogResult confirmClosure = MessageBox.Show("Are you sure you want to close this tab? All unsaved changes will be lost if you haven't repacked.", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        switch (confirmClosure)
                        {
                            case DialogResult.Yes:
                                tabs.Remove(tabs.Cast<TabPage>().Where((t, i) => mainTab.GetTabRect(i).Contains(e.Location)).First());
                                break;
                        }
                    }
                }
                else if (tab_Main.TabPages.Count == 1)
                {
                    //Checks if the tab's text reads 'New Tab' (a name only assigned by the application).
                    if (tabs.Cast<TabPage>().Where((t, i) => mainTab.GetTabRect(i).Contains(e.Location)).First().Text == "New Tab")
                    {
                        tabs.Remove(tabs.Cast<TabPage>().Where((t, i) => mainTab.GetTabRect(i).Contains(e.Location)).First());
                        newTab();
                    }
                    else
                    {
                        DialogResult confirmClosure = MessageBox.Show("Are you sure you want to close this tab? All unsaved changes will be lost if you haven't repacked.", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        switch (confirmClosure)
                        {
                            case DialogResult.Yes:
                                tabs.Remove(tabs.Cast<TabPage>().Where((t, i) => mainTab.GetTabRect(i).Contains(e.Location)).First());
                                newTab();
                                break;
                        }
                    }
                }
            }
        }

        void Lbl_SetDefault_Click(object sender, EventArgs e)
        {
            fbd_BrowseFolders.Description = "Please select the path to your extracted copy of SONIC THE HEDGEHOG.";

            if (fbd_BrowseFolders.ShowDialog() == DialogResult.OK)
            {
                if (Directory.Exists(fbd_BrowseFolders.SelectedPath))
                {
                    #region Xbox 360
                    if (File.Exists(fbd_BrowseFolders.SelectedPath + @"\default.xex"))
                    {
                        byte[] bytes = File.ReadAllBytes(fbd_BrowseFolders.SelectedPath + @"\default.xex").Take(4).ToArray();
                        var hexString = BitConverter.ToString(bytes); hexString = hexString.Replace("-", " ");
                        if (hexString == "58 45 58 32")
                        {
                            Properties.Settings.Default.gamePath = fbd_BrowseFolders.SelectedPath;
                            lbl_SetDefault.Visible = false;
                            pic_Logo.Visible = false;
                            currentARC().Navigate(fbd_BrowseFolders.SelectedPath);
                            Properties.Settings.Default.Save();

                            foreach (TabPage tab in tab_Main.TabPages)
                            {
                                if (tab.Text == "New Tab")
                                {
                                    tab_Main.TabPages.Remove(tab);
                                    newTab();
                                }
                            }
                        }
                        else { MessageBox.Show("I see you're trying to cheat the system...", "XEX Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                    #endregion

                    #region PlayStation 3
                    else if (File.Exists(fbd_BrowseFolders.SelectedPath + @"\PS3_DISC.SFB"))
                    {
                        byte[] bytes = File.ReadAllBytes(fbd_BrowseFolders.SelectedPath + @"\PS3_DISC.SFB").Take(4).ToArray();
                        var hexString = BitConverter.ToString(bytes); hexString = hexString.Replace("-", " ");
                        if (hexString == "2E 53 46 42")
                        {
                            Properties.Settings.Default.gamePath = fbd_BrowseFolders.SelectedPath;
                            lbl_SetDefault.Visible = false;
                            pic_Logo.Visible = false;
                            currentARC().Navigate(fbd_BrowseFolders.SelectedPath);
                            Properties.Settings.Default.Save();

                            foreach (TabPage tab in tab_Main.TabPages)
                            {
                                if (tab.Text == "New Tab")
                                {
                                    tab_Main.TabPages.Remove(tab);
                                    newTab();
                                }
                            }
                        }
                        else { MessageBox.Show("I see you're trying to cheat the system...", "SFB Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                    else if (File.Exists(fbd_BrowseFolders.SelectedPath + @"\PARAM.SFO"))
                    {
                        byte[] bytes = File.ReadAllBytes(fbd_BrowseFolders.SelectedPath + @"\PARAM.SFO").Take(4).ToArray();
                        var hexString = BitConverter.ToString(bytes); hexString = hexString.Replace("-", " ");
                        if (hexString == "00 50 53 46")
                        {
                            Properties.Settings.Default.gamePath = fbd_BrowseFolders.SelectedPath;
                            lbl_SetDefault.Visible = false;
                            pic_Logo.Visible = false;
                            currentARC().Navigate(fbd_BrowseFolders.SelectedPath);
                            Properties.Settings.Default.Save();

                            foreach (TabPage tab in tab_Main.TabPages)
                            {
                                if (tab.Text == "New Tab")
                                {
                                    tab_Main.TabPages.Remove(tab);
                                    newTab();
                                }
                            }
                        }
                        else { MessageBox.Show("I see you're trying to cheat the system...", "SFO Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                    else if (File.Exists(fbd_BrowseFolders.SelectedPath + @"\EBOOT.BIN"))
                    {
                        byte[] bytes = File.ReadAllBytes(fbd_BrowseFolders.SelectedPath + @"\EBOOT.BIN").Take(3).ToArray();
                        var hexString = BitConverter.ToString(bytes); hexString = hexString.Replace("-", " ");
                        if (hexString == "53 43 45")
                        {
                            Properties.Settings.Default.gamePath = fbd_BrowseFolders.SelectedPath;
                            lbl_SetDefault.Visible = false;
                            pic_Logo.Visible = false;
                            currentARC().Navigate(fbd_BrowseFolders.SelectedPath);
                            Properties.Settings.Default.Save();

                            foreach (TabPage tab in tab_Main.TabPages)
                            {
                                if (tab.Text == "New Tab")
                                {
                                    tab_Main.TabPages.Remove(tab);
                                    newTab();
                                }
                            }
                        }
                        else { MessageBox.Show("I see you're trying to cheat the system...", "BIN Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                    #endregion

                    else { MessageBox.Show("Please select a valid SONIC THE HEDGEHOG directory.", "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else { MessageBox.Show("This directory does not exist.", "Path Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
        #endregion

        static bool xenia = false;

        void repackARC()
        {
            if (Tools.Global.arcState == "save")
            {
                try
                {
                    //string failsafeCheck = File.ReadAllText($"{Properties.Settings.Default.archivesPath}{Tools.Global.sessionID}\\{Tools.Global.getIndex}");
                    string repackBuildSession = $"{Properties.Settings.Default.archivesPath}{Tools.Global.sessionID}\\{GetStorage}\\";
                    string metadata = File.ReadAllText($"{repackBuildSession}metadata.ini");

                    Tools.Global.arcState = "save";
                    Tools.ARC.Repack(tab_Main.SelectedTab.Text, repackBuildSession, metadata);

                    string archivePath = $"{repackBuildSession}{Path.GetFileName(metadata)}";
                    if (File.Exists(archivePath)) File.Copy(archivePath, metadata, true); //Copies the repacked ARC back to the original location.

                    if (xenia == true)
                    {
                        if (File.Exists(Properties.Settings.Default.xeniaFile))
                        {
                            var xeniaLocation = new ProcessStartInfo(Properties.Settings.Default.xeniaFile);
                            xeniaLocation.WorkingDirectory = Path.GetDirectoryName(Properties.Settings.Default.xeniaFile);
                            Process.Start(xeniaLocation);
                            xenia = false;
                        }
                        else { SpecifyXenia(); }
                    }
                }
                catch
                {
                    MessageBox.Show("An error occurred when repacking the archive.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Tools.Notification.Dispose();
                }
            }
            else if (Tools.Global.arcState == "save-as")
            {
                sfd_SaveFiles.Title = "Repack ARC As...";
                sfd_SaveFiles.Filter = "ARC Files|*.arc";

                if (sfd_SaveFiles.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        //string failsafeCheck = File.ReadAllText($"{Properties.Settings.Default.archivesPath}{Tools.Global.sessionID}\\{Tools.Global.getIndex}");
                        string repackBuildSession = $"{Properties.Settings.Default.archivesPath}{Tools.Global.sessionID}\\{GetStorage}\\";
                        string metadata = File.ReadAllText($"{repackBuildSession}metadata.ini");

                        Tools.Global.arcState = "save-as";
                        Tools.ARC.RepackAs(tab_Main.SelectedTab.Text, repackBuildSession, metadata, sfd_SaveFiles.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when repacking the archive.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Tools.Notification.Dispose();
                    }
                }
            }
            else
            {
                MessageBox.Show("Repack State set to invalid value: " + Tools.Global.arcState + "\nLine information: " + new System.Diagnostics.StackTrace(true).GetFrame(1).GetFileLineNumber(), "Developer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void EraseData()
        {
            if (Properties.Settings.Default.archivesPath != "" && Properties.Settings.Default.unlubPath != "" && Properties.Settings.Default.xnoPath != "")
            {
                var archiveData = new DirectoryInfo(Properties.Settings.Default.archivesPath + Tools.Global.sessionID);
                var unlubData = new DirectoryInfo(Properties.Settings.Default.unlubPath + Tools.Global.sessionID);
                var xnoData = new DirectoryInfo(Properties.Settings.Default.xnoPath + Tools.Global.sessionID);

                try
                {
                    if (Directory.Exists(Properties.Settings.Default.archivesPath + Tools.Global.sessionID))
                    {
                        foreach (FileInfo file in archiveData.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo directory in archiveData.GetDirectories())
                        {
                            directory.Delete(true);
                        }
                    }

                    if (Directory.Exists(Properties.Settings.Default.unlubPath + Tools.Global.sessionID))
                    {
                        foreach (FileInfo file in unlubData.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo directory in unlubData.GetDirectories())
                        {
                            directory.Delete(true);
                        }
                    }

                    if (Directory.Exists(Properties.Settings.Default.xnoPath + Tools.Global.sessionID))
                    {
                        foreach (FileInfo file in xnoData.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo directory in xnoData.GetDirectories())
                        {
                            directory.Delete(true);
                        }
                    }
                }
                catch { }
            }
        }

        void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Properties.Settings.Default.disableWarns == false)
            {
                switch (e.CloseReason)
                {
                    case CloseReason.UserClosing:
                        //[UserClosing] - This method occurs if the user clicks Exit.
                        //Checks if the only tab is called 'New Tab' before asking for confirmation.
                        if (tab_Main.TabPages.Count == 1 && tab_Main.SelectedTab.Text == "New Tab")
                        {
                            pic_Logo.Dispose();
                            tab_Main.Dispose();
                            EraseData();
                        }
                        else
                        {
                            DialogResult confirmUserClosure = MessageBox.Show("Are you sure you want to quit? All unsaved changes will be lost if you haven't repacked.", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            switch (confirmUserClosure)
                            {
                                case DialogResult.Yes:
                                    pic_Logo.Dispose();
                                    tab_Main.Dispose();
                                    EraseData();
                                    break;
                                case DialogResult.No:
                                    e.Cancel = true;
                                    break;
                            }
                        }
                        break;
                    case CloseReason.WindowsShutDown:
                        //[WindowsShutDown] - This method occurs if the user clicks Shutdown without closing Sonic '06 Toolkit.
                        //Checks if the only tab is called 'New Tab' before asking for confirmation.
                        if (tab_Main.TabPages.Count == 1 && tab_Main.SelectedTab.Text == "New Tab")
                        {
                            pic_Logo.Dispose();
                            tab_Main.Dispose();
                            EraseData();
                        }
                        else
                        {
                            DialogResult confirmShutdownClosure = MessageBox.Show("Sonic '06 Toolkit is still running, are you sure you want to quit? All unsaved changes will be lost if you haven't repacked.", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            switch (confirmShutdownClosure)
                            {
                                case DialogResult.Yes:
                                    pic_Logo.Dispose();
                                    tab_Main.Dispose();
                                    EraseData();
                                    break;
                                case DialogResult.No:
                                    e.Cancel = true;
                                    break;
                            }
                        }
                        break;
                    case CloseReason.ApplicationExitCall:
                        //[ApplicationExitCall] - This method occurs if the application calls the Application.Exit() method.
                        pic_Logo.Dispose();
                        tab_Main.Dispose();
                        EraseData();
                        break;
                }
            }
            else { EraseData(); }
        }

        void Advanced_Reset_Click(object sender, EventArgs e)
        {
            DialogResult reset = MessageBox.Show("This will completely reset Sonic '06 Toolkit.\n\n" +
            "" +
            "The following data will be erased:\n" +
            "► Your selected settings.\n" +
            "► Your specified paths.\n" +
            "► Studio settings.\n" +
            "► Archives in the application data.\n" +
            "► Tools in the application data.\n\n" +
            "" +
            "Are you sure you want to continue?", "Reset?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            switch (reset)
            {
                case DialogResult.Yes:

                    var s06toolkitData = new DirectoryInfo(Tools.Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\");

                    try
                    {
                        if (Directory.Exists(Tools.Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\"))
                        {
                            foreach (FileInfo file in s06toolkitData.GetFiles())
                            {
                                file.Delete();
                            }
                            foreach (DirectoryInfo directory in s06toolkitData.GetDirectories())
                            {
                                directory.Delete(true);
                            }
                        }
                    }
                    catch { }

                    Properties.Settings.Default.Reset();

                    Application.Exit();
                    break;
            }
        }
    }
}
