using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using HedgeLib.IO;
using HedgeLib.Sets;
using System.Drawing;
using System.Diagnostics;
using HedgeLib.Exceptions;
using System.Windows.Forms;
using System.Collections.Generic;

// Welcome to Sonic '06 Toolkit!

// To-do list:
// TODO: Optimise the code and combine certain sections into single methods.

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
            Global.sessionID = generateSessionID.Next(1, 99999); //Generates a random number between 1 to 99999 for a unique Session ID.
            btn_SessionID.Text = Global.sessionID.ToString();
            #endregion

            //The below code checks the command line arguments and unpacks the file that was dragged into the application.
            if (args.Length > 0)
            {
                #region ADX
                if (Path.GetExtension(args[0]) == ".adx")
                {
                    try
                    {
                        #region Converting ADX files...
                        Global.adxState = "adx";

                        //Sets up the BASIC application and executes the converting process.
                        var convertSession = new ProcessStartInfo(Properties.Settings.Default.adx2wavFile, "\"" + args[0] + "\" \"" + Path.GetDirectoryName(args[0]) + @"\" + Path.GetFileNameWithoutExtension(args[0]) + ".wav\"");
                        convertSession.WorkingDirectory = Global.currentPath;
                        convertSession.WindowStyle = ProcessWindowStyle.Hidden;
                        var Convert = Process.Start(convertSession);
                        var convertDialog = new Status();
                        convertDialog.StartPosition = FormStartPosition.CenterScreen;
                        convertDialog.Show();
                        Convert.WaitForExit();
                        Convert.Close();
                        convertDialog.Close();

                        Global.adxState = null;
                        #endregion

                        Close();
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when encoding the ADX file.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        escapeStatus();
                        Close();
                    }
                }
                #endregion

                #region ARC
                else if (Path.GetExtension(args[0]) == ".arc")
                {
                    try
                    {
                        if (File.Exists(args[0]))
                        {
                            byte[] bytes = File.ReadAllBytes(args[0]).Take(4).ToArray();
                            var hexString = BitConverter.ToString(bytes); hexString = hexString.Replace("-", " ");
                            if (hexString != "55 AA 38 2D")
                            {
                                byte[] angryBytes = File.ReadAllBytes(args[0]).ToArray();
                                var angryHexString = BitConverter.ToString(angryBytes); angryHexString = angryHexString.Replace("-", " ");
                                if (angryHexString.Contains("4D 49 47 2E 30 30 2E 31 50 53 50"))
                                {
                                    MessageBox.Show("This is an Angry Birds ARC file... You need to use GitMO to extract these. Sonic '06 Toolkit will never support this format.", "Stupid Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Close();
                                }
                                else
                                {
                                    MessageBox.Show("Invalid ARC file detected.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    Close();
                                }
                            }
                            else
                            {
                                if (Properties.Settings.Default.unpackAndLaunch == false)
                                {
                                    #region Unpacking ARC...
                                    Global.arcState = "typical";

                                    //Sets up the BASIC application and executes the unpacking process.
                                    var basicWrite = File.Create(Properties.Settings.Default.toolsPath + "unpack.bat");
                                    var basicSession = new UTF8Encoding(true).GetBytes("\"" + Properties.Settings.Default.unpackFile + "\" \"" + args[0] + "\"");
                                    basicWrite.Write(basicSession, 0, basicSession.Length);
                                    basicWrite.Close();
                                    var unpackSession = new ProcessStartInfo(Properties.Settings.Default.toolsPath + "unpack.bat");
                                    unpackSession.WorkingDirectory = Properties.Settings.Default.toolsPath;
                                    unpackSession.WindowStyle = ProcessWindowStyle.Hidden;
                                    var Unpack = Process.Start(unpackSession);
                                    var unpackDialog = new Status();
                                    unpackDialog.StartPosition = FormStartPosition.CenterScreen;
                                    unpackDialog.Show();
                                    Unpack.WaitForExit();
                                    Unpack.Close();

                                    Global.arcState = null;
                                    #endregion

                                    Close();
                                }
                                else
                                {
                                    #region Building unpack data...
                                    //Builds the main string which locates the ARC's final unpack directory.
                                    string failsafeCheck = Path.GetRandomFileName(); //Unpacked ARCs will have a unique directory to prevent overwriting.
                                    var unpackBuildSession = new StringBuilder();
                                    unpackBuildSession.Append(Properties.Settings.Default.archivesPath);
                                    unpackBuildSession.Append(Global.sessionID);
                                    unpackBuildSession.Append(@"\");
                                    unpackBuildSession.Append(failsafeCheck);
                                    unpackBuildSession.Append(@"\");
                                    unpackBuildSession.Append(Path.GetFileNameWithoutExtension(args[0]));
                                    unpackBuildSession.Append(@"\");
                                    if (!Directory.Exists(unpackBuildSession.ToString())) Directory.CreateDirectory(unpackBuildSession.ToString());
                                    #endregion

                                    #region Building ARC data...
                                    //Establishes the failsafe directory and copies the ARC prepare for the unpacking process.
                                    var arcBuildSession = new StringBuilder();
                                    arcBuildSession.Append(Properties.Settings.Default.archivesPath);
                                    arcBuildSession.Append(Global.sessionID);
                                    arcBuildSession.Append(@"\");
                                    arcBuildSession.Append(failsafeCheck);
                                    arcBuildSession.Append(@"\");
                                    if (!Directory.Exists(arcBuildSession.ToString())) Directory.CreateDirectory(arcBuildSession.ToString());
                                    if (File.Exists(args[0])) File.Copy(args[0], arcBuildSession.ToString() + Path.GetFileName(args[0]), true);
                                    #endregion

                                    #region Unpacking ARC...
                                    Global.arcState = "typical";

                                    //Sets up the BASIC application and executes the unpacking process.
                                    var basicWrite = File.Create(Properties.Settings.Default.toolsPath + "unpack.bat");
                                    var basicSession = new UTF8Encoding(true).GetBytes("\"" + Properties.Settings.Default.unpackFile + "\" \"" + arcBuildSession.ToString() + Path.GetFileName(args[0]) + "\"");
                                    basicWrite.Write(basicSession, 0, basicSession.Length);
                                    basicWrite.Close();
                                    var unpackSession = new ProcessStartInfo(Properties.Settings.Default.toolsPath + "unpack.bat");
                                    unpackSession.WorkingDirectory = Properties.Settings.Default.toolsPath;
                                    unpackSession.WindowStyle = ProcessWindowStyle.Hidden;
                                    var Unpack = Process.Start(unpackSession);
                                    var unpackDialog = new Status();
                                    unpackDialog.StartPosition = FormStartPosition.CenterScreen;
                                    unpackDialog.Show();
                                    Unpack.WaitForExit();
                                    Unpack.Close();

                                    Global.arcState = null;
                                    #endregion

                                    #region Writing metadata...
                                    //Writes metadata to the unpacked directory to ensure the original path is remembered.
                                    var metadataWrite = File.Create(arcBuildSession.ToString() + "metadata.ini");
                                    var metadataSession = new UTF8Encoding(true).GetBytes(args[0]);
                                    metadataWrite.Write(metadataSession, 0, metadataSession.Length);
                                    metadataWrite.Close();
                                    unpackDialog.Close();
                                    #endregion

                                    #region Navigating...
                                    //Creates a new tab if the selected one is being used.
                                    if (tab_Main.SelectedTab.Text == "New Tab")
                                    {
                                        navigateToGame = false;
                                        resetTab();

                                        currentARC().Navigate(unpackBuildSession.ToString());
                                        tab_Main.SelectedTab.Text = Path.GetFileName(args[0]);
                                        navigateToGame = true;
                                    }
                                    else
                                    {
                                        navigateToGame = false;
                                        newTab();

                                        currentARC().Navigate(unpackBuildSession.ToString());
                                        tab_Main.SelectedTab.Text = Path.GetFileName(args[0]);
                                        navigateToGame = true;
                                    }
                                    #endregion

                                    #region Building location data...
                                    //Writes a file to store the failsafe directory to be referenced later.
                                    var storageSession = new StringBuilder();
                                    storageSession.Append(Properties.Settings.Default.archivesPath);
                                    storageSession.Append(Global.sessionID);
                                    storageSession.Append(@"\");
                                    storageSession.Append(tab_Main.SelectedIndex);
                                    var storageWrite = File.Create(storageSession.ToString());
                                    var storageText = new UTF8Encoding(true).GetBytes(failsafeCheck);
                                    storageWrite.Write(storageText, 0, storageText.Length);
                                    storageWrite.Close();
                                    #endregion
                                }
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when unpacking the archive.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        escapeStatus();
                        Close();
                    }
                }
                #endregion

                #region WAV
                else if (Path.GetExtension(args[0]) == ".wav")
                {
                    try
                    {
                        #region Converting WAV files...
                        Global.adxState = "wav";

                        //Sets up the BASIC application and executes the converting process.
                        var convertSession = new ProcessStartInfo(Properties.Settings.Default.criconverterFile, "\"" + args[0] + "\" \"" + Path.GetDirectoryName(args[0]) + @"\" + Path.GetFileNameWithoutExtension(args[0]) + ".adx\" -codec=adx -volume=1 -downmix=MONO");
                        convertSession.WorkingDirectory = Global.currentPath;
                        convertSession.WindowStyle = ProcessWindowStyle.Hidden;
                        var Convert = Process.Start(convertSession);
                        var convertDialog = new Status();
                        convertDialog.StartPosition = FormStartPosition.CenterScreen;
                        convertDialog.Show();
                        Convert.WaitForExit();
                        Convert.Close();
                        convertDialog.Close();

                        Global.adxState = null;
                        #endregion

                        Close();
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when encoding the WAV file.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        escapeStatus();
                        Close();
                    }
                }
                #endregion

                #region CSB
                else if (Path.GetExtension(args[0]) == ".csb")
                {
                    try
                    {
                        #region Extracting CSBs...
                        Global.csbState = "unpack";

                        //Sets up the BASIC application and executes the extracting process.
                        var unpackSession = new ProcessStartInfo(Properties.Settings.Default.csbFile, "\"" + args[0] + "\"");
                        unpackSession.WorkingDirectory = Global.currentPath;
                        unpackSession.WindowStyle = ProcessWindowStyle.Hidden;
                        var Unpack = Process.Start(unpackSession);
                        var unpackDialog = new Status();
                        unpackDialog.StartPosition = FormStartPosition.CenterScreen;
                        unpackDialog.Show();
                        Unpack.WaitForExit();
                        Unpack.Close();
                        unpackDialog.Close();

                        Global.csbState = null;
                        #endregion

                        Close();
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when unpacking the CSB.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        escapeStatus();
                        Close();
                    }
                }
                #endregion

                #region LUB
                else if (Path.GetExtension(args[0]) == ".lub")
                {
                    //This process needs work. It would be better to decompile directly with a C# decompiler, rather than depending on a Java decompiler.
                    //It's based on Lua's own source, so it wouldn't be too difficult to set up (if you know what you're doing).
                    if (Global.javaCheck == false) MessageBox.Show("Java is required to decompile Lua binaries. Please install Java and restart Sonic '06 Toolkit.", "Java Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        //Temporary solution; would probably be better to use an array.
                        //Checks if the first file to be processed is blacklisted. If so, abort the operation to ensure the file doesn't get corrupted.
                        if (Path.GetFileNameWithoutExtension(args[0]) == @"standard.lub") MessageBox.Show("File: standard.lub\n\nThis file cannot be decompiled; attempts to do so will render the file unusable.", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        else
                        {
                            //Checks if any of the blacklisted files are present. If so, warn the user about modifying the files.
                            if (Path.GetFileNameWithoutExtension(args[0]) == @"render_shadowmap.lub") MessageBox.Show("File: render_shadowmap.lub\n\nEditing this file may render this archive unusable. Please edit this at your own risk!", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            if (Path.GetFileNameWithoutExtension(args[0]) == @"game.lub") MessageBox.Show("File: game.lub\n\nEditing this file may render it unusable. Please edit this at your own risk!", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            if (Path.GetFileNameWithoutExtension(args[0]) == @"object.lub") MessageBox.Show("File: object.lub\n\nEditing this file may render it unusable. Please edit this at your own risk!", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            try
                            {
                                #region Writing decompiler...
                                //Writes the decompiler to the failsafe directory to ensure any LUBs left over from other open archives aren't copied over to the selected archive.
                                if (!Directory.Exists(Properties.Settings.Default.unlubPath + Global.sessionID + @"\")) Directory.CreateDirectory(Properties.Settings.Default.unlubPath + Global.sessionID + @"\");
                                if (!Directory.Exists(Properties.Settings.Default.unlubPath + Global.sessionID + @"\lubs")) Directory.CreateDirectory(Properties.Settings.Default.unlubPath + Global.sessionID + @"\lubs");
                                if (!File.Exists(Properties.Settings.Default.unlubPath + Global.sessionID + @"\unlub.jar")) File.WriteAllBytes(Properties.Settings.Default.unlubPath + Global.sessionID + @"\unlub.jar", Properties.Resources.unlub);
                                if (!File.Exists(Properties.Settings.Default.unlubPath + Global.sessionID + @"\unlub.bat"))
                                {
                                    var decompilerWrite = File.Create(Properties.Settings.Default.unlubPath + Global.sessionID + @"\unlub.bat");
                                    var decompilerText = new UTF8Encoding(true).GetBytes("cd \".\\lubs\"\nfor /r %%i in (*.lub) do java -jar ..\\unlub.jar \"%%~dpni.lub\" > \"%%~dpni.lua\"\nxcopy \".\\*.lua\" \"..\\luas\" /y /i\ndel \".\\*.lua\" /q\n@ECHO OFF\n:delete\ndel /q /f *.lub\n@ECHO OFF\n:rename\ncd \"..\\luas\"\nrename \"*.lua\" \"*.lub\"\nexit");
                                    decompilerWrite.Write(decompilerText, 0, decompilerText.Length);
                                    decompilerWrite.Close();
                                }
                                #endregion

                                #region Verifying Lua binaries...
                                //Checks the header for each file to ensure that it can be safely decompiled.
                                if (File.Exists(args[0]))
                                {
                                    if (File.ReadAllLines(args[0])[0].Contains("LuaP"))
                                    {
                                        Global.currentPath = Path.GetDirectoryName(args[0]);
                                        File.Copy(args[0], Path.Combine(Properties.Settings.Default.unlubPath + Global.sessionID + @"\lubs\", Path.GetFileName(args[0])), true);
                                    }
                                }
                                #endregion

                                #region Decompiling Lua binaries...
                                Global.lubState = "decompile";
                                //Sets up the BASIC application and executes the decompiling process.
                                var decompileSession = new ProcessStartInfo(Properties.Settings.Default.unlubPath + Global.sessionID + @"\unlub.bat");
                                decompileSession.WorkingDirectory = Properties.Settings.Default.unlubPath + Global.sessionID + @"\";
                                decompileSession.WindowStyle = ProcessWindowStyle.Hidden;
                                var Decompile = Process.Start(decompileSession);
                                var decompileDialog = new Status();
                                decompileDialog.StartPosition = FormStartPosition.CenterScreen;
                                decompileDialog.Show();
                                Decompile.WaitForExit();
                                Decompile.Close();

                                Global.lubState = null;
                                #endregion

                                #region Moving decompiled Lua binaries...
                                //Copies all LUBs to the final directory, then erases leftovers.
                                if (File.Exists(Path.Combine(Properties.Settings.Default.unlubPath + Global.sessionID + @"\luas\", Path.GetFileName(args[0]))))
                                {
                                    File.Copy(Path.Combine(Properties.Settings.Default.unlubPath + Global.sessionID + @"\luas\", Path.GetFileName(args[0])), Path.Combine(Global.currentPath, Path.GetFileName(args[0])), true);
                                    File.Delete(Path.Combine(Properties.Settings.Default.unlubPath + Global.sessionID + @"\luas\", Path.GetFileName(args[0])));
                                }
                                decompileDialog.Close();
                                #endregion

                                Close();
                            }
                            catch
                            {
                                MessageBox.Show("An error occurred when decompiling the Lua binaries.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                escapeStatus();
                                Close();
                            }
                        }
                    }
                }
                #endregion

                #region MST
                else if (Path.GetExtension(args[0]) == ".mst")
                {
                    try
                    {
                        Global.mstState = "decode";

                        UnicodeEncoding unicode = new UnicodeEncoding(true, false, false);
                        byte[] buffer = new byte[1024];

                        // arg0 is assumed to be a file to split
                        List<string> results = new List<string>();          // <- BTW Hyper this array contains ALL the lines read from the file, including any bugged non-unicode lines.

                        using (FileStream fs = new FileStream(args[0], FileMode.Open))
                        {
                            byte[] strbuf = new byte[1024];
                            int len = 0, pos = 0;

                            while ((len = fs.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                for (int i = 0; i < len; i += 2)
                                {

                                    // check if the next 2 bytes are an end token. Check second byte last because its most likely 0 anyway but we gotta be sure...
                                    if ((buffer[i + 1] == 0 || buffer[i + 1] == 0xA) && buffer[i] == 0)
                                    {
                                        // this is a null terminator or a newline
                                        results.Add(unicode.GetString(strbuf, 0, pos));
                                        pos = 0;

                                    }
                                    else
                                    {
                                        // if the buffer we allocated is too small, reallocate a larger array...
                                        if (pos >= strbuf.Length)
                                        {
                                            byte[] _sbuf = strbuf;
                                            strbuf = new byte[_sbuf.Length << 1];
                                            Array.Copy(_sbuf, strbuf, _sbuf.Length);
                                        }

                                        // copy next 2 bytes into strbuffer... If file size is not divisible by 2, this is gonna be a real problem here
                                        strbuf[pos++] = buffer[i];
                                        strbuf[pos++] = buffer[i + 1];
                                    }
                                }
                            }

                            // if the last string is not terminated, print it anyway
                            if (pos > 0) results.Add(unicode.GetString(strbuf, 0, pos));
                        }

                        File.WriteAllLines(Global.currentPath + Path.GetFileNameWithoutExtension(args[0]) + ".txt", results);

                        Global.mstState = null;

                        Close();
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when decoding the MST.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        escapeStatus();
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
                            var readSET = new S06SetData();
                            readSET.Load(args[0]);
                            readSET.ExportXML(Global.currentPath + Path.GetFileNameWithoutExtension(args[0]) + ".xml");

                            Close();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when converting the SETs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        escapeStatus();
                        Close();
                    }
                }
                #endregion

                #region XML
                else if (Path.GetExtension(args[0]) == ".xml")
                {
                    try
                    {
                        if (File.Exists(args[0]))
                        {
                            if (File.Exists(Path.GetFileNameWithoutExtension(args[0]) + ".set")) File.Copy(Path.GetFileNameWithoutExtension(args[0]) + ".set", Path.GetDirectoryName(args[0]) + @"\" + Path.GetFileNameWithoutExtension(args[0]) + ".set.bak", true);

                            if (File.Exists(Path.GetFileNameWithoutExtension(args[0]) + ".set")) File.Delete(Path.GetFileNameWithoutExtension(args[0]) + ".set");

                            var readXML = new S06SetData();
                            readXML.ImportXML(args[0]);
                            readXML.Save(Path.GetFileNameWithoutExtension(args[0]) + ".set");

                            if (File.Exists(args[0])) File.Delete(args[0]);

                            Close();
                        }
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when importing the XMLs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        escapeStatus();
                        Close();
                    }
                }
                #endregion

                #region XNO
                else if (Path.GetExtension(args[0]) == ".xno")
                {
                    try
                    {
                        #region Writing converter...
                        //Writes the decompiler to the failsafe directory to ensure any XNOs left over from other open archives aren't copied over to the selected archive.
                        if (!Directory.Exists(Properties.Settings.Default.xnoPath + Global.sessionID + @"\")) Directory.CreateDirectory(Properties.Settings.Default.xnoPath + Global.sessionID + @"\");
                        if (!Directory.Exists(Properties.Settings.Default.xnoPath + Global.sessionID + @"\xnos")) Directory.CreateDirectory(Properties.Settings.Default.xnoPath + Global.sessionID + @"\xnos");
                        if (!File.Exists(Properties.Settings.Default.xnoPath + Global.sessionID + @"\xno2dae.exe")) File.WriteAllBytes(Properties.Settings.Default.xnoPath + Global.sessionID + @"\xno2dae.exe", Properties.Resources.xno2dae);
                        #endregion

                        #region Building XNOs...
                        //Gets the location of the converter and writes a BASIC application.
                        var checkedBuildSession = new StringBuilder();
                        checkedBuildSession.Append(Properties.Settings.Default.xnoPath);
                        checkedBuildSession.Append(Global.sessionID);
                        checkedBuildSession.Append(@"\xno2dae.exe");
                        var checkedWrite = File.Create(Properties.Settings.Default.xnoPath + Global.sessionID + @"\xno2dae.bat");
                        var checkedText = new UTF8Encoding(true).GetBytes("\"" + checkedBuildSession.ToString() + "\" \"" + args[0] + "\"");
                        checkedWrite.Write(checkedText, 0, checkedText.Length);
                        checkedWrite.Close();
                        #endregion

                        #region Converting XNOs...
                        Global.xnoState = "xno";

                        //Sets up the BASIC application and executes the conversion process.
                        var convertSession = new ProcessStartInfo(Properties.Settings.Default.xnoPath + Global.sessionID + @"\xno2dae.bat");
                        convertSession.WorkingDirectory = Global.currentPath;
                        convertSession.WindowStyle = ProcessWindowStyle.Hidden;
                        var Convert = Process.Start(convertSession);
                        var convertDialog = new Status();
                        convertDialog.StartPosition = FormStartPosition.CenterScreen;
                        convertDialog.Show();
                        Convert.WaitForExit();
                        Convert.Close();
                        convertDialog.Close();

                        Global.xnoState = null;
                        #endregion

                        Close();
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when converting the selected XNOs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        escapeStatus();
                        Close();
                    }
                }
                #endregion
            }
        }

        void CheckForUpdates(string currentVersion, string newVersionDownloadLink, string libDownloadLink, string versionInfoLink)
        {
            try
            {
                var latestVersion = new TimedWebClient { Timeout = 100000 }.DownloadString(versionInfoLink);
                var changeLogs = new TimedWebClient { Timeout = 100000 }.DownloadString("https://segacarnival.com/hyper/updates/changelogs.txt");
                if (latestVersion.Contains("Version"))
                {
                    if (latestVersion != currentVersion)
                    {
                        DialogResult confirmUpdate = MessageBox.Show("Sonic '06 Toolkit - " + latestVersion + " is now available!\n\nChangelogs:\n" + changeLogs + "\n\nDo you wish to download it?", "New update available!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        switch (confirmUpdate)
                        {
                            case DialogResult.Yes:
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
                                    if (themes_Original.Checked == true) { btn_Backdrop.Visible = false; btn_Backdrop.Left -= 186; }
                                    MessageBox.Show("An error occurred when updating Sonic '06 Toolkit.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                break;
                        }
                    }
                    else if (Global.updateState == "user") MessageBox.Show("There are currently no updates available.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Global.serverStatus = "down";
                    if (Properties.Settings.Default.disableSoftwareUpdater == true) MessageBox.Show("The update servers are currently undergoing maintenance. Apologies for the inconvenience.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch { Global.serverStatus = "offline"; }
        }

        void Main_Load(object sender, EventArgs e)
        {
            #region Directory Check...

            #region Validating Paths...
            if (Properties.Settings.Default.rootPath == string.Empty) Properties.Settings.Default.rootPath = Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\";
            if (Properties.Settings.Default.toolsPath == string.Empty) Properties.Settings.Default.toolsPath = Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Tools\";
            if (Properties.Settings.Default.archivesPath == string.Empty) Properties.Settings.Default.archivesPath = Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Archives\";
            if (Properties.Settings.Default.unlubPath == string.Empty) Properties.Settings.Default.unlubPath = Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Tools\unlub\";
            if (Properties.Settings.Default.xnoPath == string.Empty) Properties.Settings.Default.xnoPath = Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Tools\xno2dae\";
            Properties.Settings.Default.Save();
            #endregion

            try
            {
                //The below code checks if the directories in the Global class exist; if not, they will be created.
                if (!Directory.Exists(Properties.Settings.Default.rootPath)) Directory.CreateDirectory(Properties.Settings.Default.rootPath);
                if (!Directory.Exists(Properties.Settings.Default.toolsPath)) Directory.CreateDirectory(Properties.Settings.Default.toolsPath);
                if (!Directory.Exists(Properties.Settings.Default.toolsPath + @"Arctool\")) Directory.CreateDirectory(Properties.Settings.Default.toolsPath + @"Arctool\");
                if (!Directory.Exists(Properties.Settings.Default.toolsPath + @"Arctool\arctool\")) Directory.CreateDirectory(Properties.Settings.Default.toolsPath + @"Arctool\arctool\");
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
            if (Properties.Settings.Default.disableSoftwareUpdater == true)
            {
                mainPreferences_DisableSoftwareUpdater.Checked = true;
            }
            else
            {
                mainPreferences_DisableSoftwareUpdater.Checked = false;
                CheckForUpdates(Global.latestVersion, "https://segacarnival.com/hyper/updates/latest-master.exe", "https://segacarnival.com/hyper/updates/lib.dll", "https://segacarnival.com/hyper/updates/latest_master.txt");
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
            catch { Global.javaCheck = false; }
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

        private WebBrowser currentARC()
        {
            //Returns the active web browser in the selected tab.
            return (WebBrowser)tab_Main.SelectedTab.Controls[0];
        }

        #region File

        #region Unpack States
        void mainFile_OpenARC_Click(object sender, EventArgs e)
        {
            ofd_OpenFiles.Title = "Please select an ARC file...";
            ofd_OpenFiles.Filter = "ARC Files|*.arc";
            ofd_OpenFiles.DefaultExt = "arc";

            if (ofd_OpenFiles.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Global.arcState = "typical";
                    unpackARC();
                }
                catch
                {
                    MessageBox.Show("An error occurred when unpacking the archive.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    escapeStatus();
                }
            }
        }
        #endregion

        void MainFile_ExtractISO_Click(object sender, EventArgs e)
        {
            ofd_OpenFiles.Title = "Please select an Xbox 360 ISO...";
            ofd_OpenFiles.Filter = null;

            if (ofd_OpenFiles.ShowDialog() == DialogResult.OK)
            {
                fbd_BrowseFolders.Description = "Please select the path to extract your ISO. Click Cancel to extract the ISO in it's directory.";

                switch (fbd_BrowseFolders.ShowDialog())
                {
                    case DialogResult.OK:
                        Global.exisoState = "extract";

                        var extractSession = new ProcessStartInfo(Properties.Settings.Default.exisoFile, "-d \"" + fbd_BrowseFolders.SelectedPath + "\" -x \"" + ofd_OpenFiles.FileName + "\"");
                        extractSession.WorkingDirectory = Global.currentPath;
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

                        Global.exisoState = null;
                        break;

                    case DialogResult.Cancel:
                        Global.exisoState = "extract";

                        var extractRootSession = new ProcessStartInfo(Properties.Settings.Default.exisoFile, "-d \"" + Path.GetDirectoryName(ofd_OpenFiles.FileName) + @"\" + Path.GetFileNameWithoutExtension(ofd_OpenFiles.FileName) + "\" -x \"" + ofd_OpenFiles.FileName + "\"");
                        extractRootSession.WorkingDirectory = Global.currentPath;
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

                        Global.exisoState = null;
                        break;
                }
            }
        }

        public bool freeMode = false;

        void MainFile_OpenSonic_Click(object sender, EventArgs e)
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
                                arcBuildSession.Append(Global.sessionID);
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

                                #region Building location data...
                                //Writes a file to store the failsafe directory to be referenced later.
                                var storageSession = new StringBuilder();
                                storageSession.Append(Properties.Settings.Default.archivesPath);
                                storageSession.Append(Global.sessionID);
                                storageSession.Append(@"\");
                                storageSession.Append(tab_Main.SelectedIndex);
                                var storageWrite = File.Create(storageSession.ToString());
                                var storageText = new UTF8Encoding(true).GetBytes(failsafeCheck);
                                storageWrite.Write(storageText, 0, storageText.Length);
                                storageWrite.Close();
                                #endregion

                                #region Navigating...
                                //Creates a new tab if the selected one is being used.
                                if (tab_Main.SelectedTab.Text == "New Tab")
                                {
                                    navigateToGame = false;
                                    resetTab();

                                    currentARC().Navigate(fbd_BrowseFolders.SelectedPath);
                                    tab_Main.SelectedTab.Text = Path.GetFileName(fbd_BrowseFolders.SelectedPath);
                                    navigateToGame = true;
                                }
                                else
                                {
                                    navigateToGame = false;
                                    newTab();

                                    currentARC().Navigate(fbd_BrowseFolders.SelectedPath);
                                    tab_Main.SelectedTab.Text = Path.GetFileName(fbd_BrowseFolders.SelectedPath);
                                    navigateToGame = true;
                                }
                                #endregion

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
                                arcBuildSession.Append(Global.sessionID);
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

                                #region Building location data...
                                //Writes a file to store the failsafe directory to be referenced later.
                                var storageSession = new StringBuilder();
                                storageSession.Append(Properties.Settings.Default.archivesPath);
                                storageSession.Append(Global.sessionID);
                                storageSession.Append(@"\");
                                storageSession.Append(tab_Main.SelectedIndex);
                                var storageWrite = File.Create(storageSession.ToString());
                                var storageText = new UTF8Encoding(true).GetBytes(failsafeCheck);
                                storageWrite.Write(storageText, 0, storageText.Length);
                                storageWrite.Close();
                                #endregion

                                #region Navigating...
                                //Creates a new tab if the selected one is being used.
                                if (tab_Main.SelectedTab.Text == "New Tab")
                                {
                                    navigateToGame = false;
                                    resetTab();

                                    currentARC().Navigate(fbd_BrowseFolders.SelectedPath);
                                    tab_Main.SelectedTab.Text = Path.GetFileName(fbd_BrowseFolders.SelectedPath);
                                    navigateToGame = true;
                                }
                                else
                                {
                                    navigateToGame = false;
                                    newTab();

                                    currentARC().Navigate(fbd_BrowseFolders.SelectedPath);
                                    tab_Main.SelectedTab.Text = Path.GetFileName(fbd_BrowseFolders.SelectedPath);
                                    navigateToGame = true;
                                }
                                #endregion

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
                                arcBuildSession.Append(Global.sessionID);
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

                                #region Building location data...
                                //Writes a file to store the failsafe directory to be referenced later.
                                var storageSession = new StringBuilder();
                                storageSession.Append(Properties.Settings.Default.archivesPath);
                                storageSession.Append(Global.sessionID);
                                storageSession.Append(@"\");
                                storageSession.Append(tab_Main.SelectedIndex);
                                var storageWrite = File.Create(storageSession.ToString());
                                var storageText = new UTF8Encoding(true).GetBytes(failsafeCheck);
                                storageWrite.Write(storageText, 0, storageText.Length);
                                storageWrite.Close();
                                #endregion

                                #region Navigating...
                                //Creates a new tab if the selected one is being used.
                                if (tab_Main.SelectedTab.Text == "New Tab")
                                {
                                    navigateToGame = false;
                                    resetTab();

                                    currentARC().Navigate(fbd_BrowseFolders.SelectedPath);
                                    tab_Main.SelectedTab.Text = Path.GetFileName(fbd_BrowseFolders.SelectedPath);
                                    navigateToGame = true;
                                }
                                else
                                {
                                    navigateToGame = false;
                                    newTab();

                                    currentARC().Navigate(fbd_BrowseFolders.SelectedPath);
                                    tab_Main.SelectedTab.Text = Path.GetFileName(fbd_BrowseFolders.SelectedPath);
                                    navigateToGame = true;
                                }
                                #endregion

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
                                arcBuildSession.Append(Global.sessionID);
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

                                #region Building location data...
                                //Writes a file to store the failsafe directory to be referenced later.
                                var storageSession = new StringBuilder();
                                storageSession.Append(Properties.Settings.Default.archivesPath);
                                storageSession.Append(Global.sessionID);
                                storageSession.Append(@"\");
                                storageSession.Append(tab_Main.SelectedIndex);
                                var storageWrite = File.Create(storageSession.ToString());
                                var storageText = new UTF8Encoding(true).GetBytes(failsafeCheck);
                                storageWrite.Write(storageText, 0, storageText.Length);
                                storageWrite.Close();
                                #endregion

                                #region Navigating...
                                //Creates a new tab if the selected one is being used.
                                if (tab_Main.SelectedTab.Text == "New Tab")
                                {
                                    navigateToGame = false;
                                    resetTab();

                                    currentARC().Navigate(fbd_BrowseFolders.SelectedPath);
                                    tab_Main.SelectedTab.Text = Path.GetFileName(fbd_BrowseFolders.SelectedPath);
                                    navigateToGame = true;
                                }
                                else
                                {
                                    navigateToGame = false;
                                    newTab();

                                    currentARC().Navigate(fbd_BrowseFolders.SelectedPath);
                                    tab_Main.SelectedTab.Text = Path.GetFileName(fbd_BrowseFolders.SelectedPath);
                                    navigateToGame = true;
                                }
                                #endregion

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
                        arcBuildSession.Append(Global.sessionID);
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

                        #region Building location data...
                        //Writes a file to store the failsafe directory to be referenced later.
                        var storageSession = new StringBuilder();
                        storageSession.Append(Properties.Settings.Default.archivesPath);
                        storageSession.Append(Global.sessionID);
                        storageSession.Append(@"\");
                        storageSession.Append(tab_Main.SelectedIndex);
                        var storageWrite = File.Create(storageSession.ToString());
                        var storageText = new UTF8Encoding(true).GetBytes(failsafeCheck);
                        storageWrite.Write(storageText, 0, storageText.Length);
                        storageWrite.Close();
                        #endregion

                        #region Navigating...
                        //Creates a new tab if the selected one is being used.
                        if (tab_Main.SelectedTab.Text == "New Tab")
                        {
                            navigateToGame = false;
                            resetTab();

                            currentARC().Navigate(fbd_BrowseFolders.SelectedPath);
                            tab_Main.SelectedTab.Text = Path.GetFileName(fbd_BrowseFolders.SelectedPath);
                            navigateToGame = true;
                        }
                        else
                        {
                            navigateToGame = false;
                            newTab();

                            currentARC().Navigate(fbd_BrowseFolders.SelectedPath);
                            tab_Main.SelectedTab.Text = Path.GetFileName(fbd_BrowseFolders.SelectedPath);
                            navigateToGame = true;
                        }
                        #endregion

                        Text = "Sonic '06 Toolkit - '" + fbd_BrowseFolders.SelectedPath + @"\'";
                    }
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

        #region Repack States
        void MainFile_RepackARC_Click(object sender, EventArgs e)
        {
            if (tab_Main.SelectedTab.Text.Contains(".arc"))
            {
                Global.arcState = "save";
            }
            else { Global.arcState = "save-as"; }

            repackARC();
        }

        void Btn_Repack_Click(object sender, EventArgs e)
        {
            if (tab_Main.SelectedTab.Text.Contains(".arc"))
            {
                Global.arcState = "save";
            }
            else { Global.arcState = "save-as"; }

            repackARC();
        }

        void RepackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tab_Main.SelectedTab.Text.Contains(".arc"))
            {
                Global.arcState = "save";
            }
            else { Global.arcState = "save-as"; }

            repackARC();
        }

        void RepackARCAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.arcState = "save-as";
            repackARC();
        }

        void RepackAndLaunchXeniaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tab_Main.SelectedTab.Text.Contains(".arc"))
            {
                Global.arcState = "save";
            }
            else { Global.arcState = "save-as"; }

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
            Global.arcState = "save-as";
            repackARC();
        }
        #endregion

        #region Preferences
        //[Preferences] - Disable software updater
        //Disables the software update function on launch.
        void MainPreferences_DisableSoftwareUpdater_CheckedChanged(object sender, EventArgs e)
        {
            if (mainPreferences_DisableSoftwareUpdater.Checked == true) Properties.Settings.Default.disableSoftwareUpdater = true;
            else Properties.Settings.Default.disableSoftwareUpdater = false;
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
                pnl_Updater.Left -= 186;
                lbl_SetDefault.Top = 53;

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
                pnl_Updater.Left += 186;
                lbl_SetDefault.Top = 81;

                Properties.Settings.Default.Save();
            }
            else { mainThemes_Compact.Checked = true; }
        }

        //[Themes] - Show Logo
        //Disables the Sonic '06 Toolkit logo appearing on new tabs.
        //void MainPreferences_ShowLogo_CheckedChanged(object sender, EventArgs e)
        #region //{
        static bool debug = false;

        void Free_Mode(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var debugger = new Debugger();

                if (!debug)
                {
                    debug = true;
                    MessageBox.Show("Debug Mode is now enabled.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    debugger.Show();
                }
                else
                {
                    debug = false;
                    MessageBox.Show("Debug Mode is now disabled.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    try
                    {
                        Debugger debugForm = (Debugger)Application.OpenForms["Debugger"];
                        debugForm.Close();
                    }
                    catch { }
                }
            }
        }
        #endregion
        //    if (mainPreferences_ShowLogo.Checked == true) Properties.Settings.Default.showLogo = true;
        //    else Properties.Settings.Default.showLogo = false;
        #region //    Properties.Settings.Default.Save();
        void None(object sender, EventArgs e)
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
            new ADX_Studio().ShowDialog();
        }

        void MainSDK_ARCStudio_Click(object sender, EventArgs e)
        {
            new ARC_Studio().ShowDialog();
        }

        void MainSDK_AT3Studio_Click(object sender, EventArgs e)
        {
            new AT3_Studio().ShowDialog();
        }

        void MainSDK_CSBStudio_Click(object sender, EventArgs e)
        {
            new CSB_Studio().ShowDialog();
        }

        void MainSDK_DDSStudio_Click(object sender, EventArgs e)
        {
            new DDS_Studio().ShowDialog();
        }

        void MainSDK_LUBStudio_Click(object sender, EventArgs e)
        {
            //This process needs work. It would be better to decompile directly with a C# decompiler, rather than depending on a Java decompiler.
            //It's based on Lua's own source, so it wouldn't be too difficult to set up (if you know what you're doing).
            if (Global.javaCheck == false) MessageBox.Show("Java is required to decompile Lua binaries. Please install Java and restart Sonic '06 Toolkit.", "Java Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                new LUB_Studio().ShowDialog();
            }
        }

        void MainSDK_MSTStudio_Click(object sender, EventArgs e)
        {
            new MST_Studio().ShowDialog();
        }

        void MainSDK_SETStudio_Click(object sender, EventArgs e)
        {
            new SET_Studio().ShowDialog();
        }

        void MainSDK_XNOStudio_Click(object sender, EventArgs e)
        {
            new XNO_Studio().ShowDialog();
        }
        #endregion

        #region Shortcuts
        void Shortcuts_ExtractCSBs_Click(object sender, EventArgs e)
        {
            if (Directory.GetFiles(Global.currentPath, "*.csb").Length == 0) MessageBox.Show("There are no CSBs to unpack in this directory.", "No CSBs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                try
                {
                    Global.csbState = "unpack";

                    var unpackDialog = new Status();
                    var parentLeft = Left + ((Width - unpackDialog.Width) / 2);
                    var parentTop = Top + ((Height - unpackDialog.Height) / 2);
                    unpackDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                    unpackDialog.Show();

                    #region Getting selected CSBs...
                    //Gets all checked boxes from the CheckedListBox and builds a string for each CSB.
                    foreach (string CSB in Directory.GetFiles(Global.currentPath, "*.csb", SearchOption.TopDirectoryOnly))
                    {
                        if (File.Exists(CSB))
                        {
                            var checkedBuildSession = new StringBuilder();
                            checkedBuildSession.Append(Path.Combine(Global.currentPath, CSB));

                            #region Extracting CSBs...
                            //Sets up the BASIC application and executes the extracting process.
                            var unpackSession = new ProcessStartInfo(Properties.Settings.Default.csbFile, "\"" + checkedBuildSession.ToString() + "\"");
                            unpackSession.WorkingDirectory = Global.currentPath;
                            unpackSession.WindowStyle = ProcessWindowStyle.Hidden;
                            var Unpack = Process.Start(unpackSession);
                            Unpack.WaitForExit();
                            Unpack.Close();

                            Global.csbState = null;
                            #endregion
                        }
                    }
                    #endregion

                    unpackDialog.Close();
                }
                catch
                {
                    MessageBox.Show("An error occurred when unpacking the CSBs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    escapeStatus();
                }
            }
        }

        void Shortcuts_ConvertDDS_Click(object sender, EventArgs e)
        {
            //Checks if there are any DDSs in the directory.
            if (Directory.GetFiles(Global.currentPath, "*.dds").Length == 0) MessageBox.Show("There are no DDS files to convert in this directory.", "No DDS files available", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                Global.ddsState = "dds";

                var convertDialog = new Status();
                var parentLeft = Left + ((Width - convertDialog.Width) / 2);
                var parentTop = Top + ((Height - convertDialog.Height) / 2);
                convertDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                convertDialog.Show();

                foreach (string DDS in Directory.GetFiles(Global.currentPath, "*.dds", SearchOption.TopDirectoryOnly))
                {
                    #region Converting DDS files...
                    //Sets up the BASIC application and executes the converting process.
                    var convertSession = new ProcessStartInfo(Properties.Settings.Default.directXFile, "\"" + DDS + "\" -ft PNG -nogpu -singleproc -f R8G8B8A8_UNORM");
                    convertSession.WorkingDirectory = Global.currentPath;
                    convertSession.WindowStyle = ProcessWindowStyle.Hidden;
                    var Convert = Process.Start(convertSession);
                    Convert.WaitForExit();
                    Convert.Close();
                    #endregion
                }

                convertDialog.Close();

                Global.ddsState = null;
            }
        }

        void Shortcuts_DecompileLUBs_Click(object sender, EventArgs e)
        {
            if (Global.javaCheck == false) MessageBox.Show("Java is required to decompile Lua binaries. Please install Java and restart Sonic '06 Toolkit.", "Java Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                            if (!Directory.Exists(Properties.Settings.Default.unlubPath + Global.sessionID)) Directory.CreateDirectory(Properties.Settings.Default.unlubPath + Global.sessionID);
                            var failsafeBuildSession = new StringBuilder();
                            failsafeBuildSession.Append(Properties.Settings.Default.archivesPath);
                            failsafeBuildSession.Append(Global.sessionID);
                            failsafeBuildSession.Append(@"\");
                            string failsafeCheck = File.ReadAllText(failsafeBuildSession.ToString() + tab_Main.SelectedIndex);
                            #endregion

                            #region Writing decompiler...
                            //Writes the decompiler to the failsafe directory to ensure any LUBs left over from other open archives aren't copied over to the selected archive.
                            if (!Directory.Exists(Properties.Settings.Default.unlubPath + Global.sessionID + @"\" + failsafeCheck)) Directory.CreateDirectory(Properties.Settings.Default.unlubPath + Global.sessionID + @"\" + failsafeCheck);
                            if (!Directory.Exists(Properties.Settings.Default.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\lubs")) Directory.CreateDirectory(Properties.Settings.Default.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\lubs");
                            if (!File.Exists(Properties.Settings.Default.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.jar")) File.WriteAllBytes(Properties.Settings.Default.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.jar", Properties.Resources.unlub);
                            if (!File.Exists(Properties.Settings.Default.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.bat"))
                            {
                                var decompilerWrite = File.Create(Properties.Settings.Default.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.bat");
                                var decompilerText = new UTF8Encoding(true).GetBytes("cd \".\\lubs\"\nfor /r %%i in (*.lub) do java -jar ..\\unlub.jar \"%%~dpni.lub\" > \"%%~dpni.lua\"\nxcopy \".\\*.lua\" \"..\\luas\" /y /i\ndel \".\\*.lua\" /q\n@ECHO OFF\n:delete\ndel /q /f *.lub\n@ECHO OFF\n:rename\ncd \"..\\luas\"\nrename \"*.lua\" \"*.lub\"\nexit");
                                decompilerWrite.Write(decompilerText, 0, decompilerText.Length);
                                decompilerWrite.Close();
                            }
                            #endregion

                            #region Verifying Lua binaries...
                            //Checks the header for each file to ensure that it can be safely decompiled.
                            foreach (string LUB in Directory.GetFiles(Global.currentPath, "*.lub", SearchOption.TopDirectoryOnly))
                            {
                                if (File.Exists(LUB))
                                {
                                    if (File.ReadAllLines(LUB)[0].Contains("LuaP"))
                                    {
                                        File.Copy(LUB, Path.Combine(Properties.Settings.Default.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\lubs\", Path.GetFileName(LUB)), true);
                                    }
                                }
                            }
                            #endregion

                            #region Decompiling Lua binaries...
                            Global.lubState = "decompile";

                            //Sets up the BASIC application and executes the decompiling process.
                            var decompileSession = new ProcessStartInfo(Properties.Settings.Default.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\unlub.bat");
                            decompileSession.WorkingDirectory = Properties.Settings.Default.unlubPath + Global.sessionID + @"\" + failsafeCheck;
                            decompileSession.WindowStyle = ProcessWindowStyle.Hidden;
                            var Decompile = Process.Start(decompileSession);
                            var decompileDialog = new Status();
                            var parentLeft = Left + ((Width - decompileDialog.Width) / 2);
                            var parentTop = Top + ((Height - decompileDialog.Height) / 2);
                            decompileDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                            decompileDialog.Show();
                            Decompile.WaitForExit();
                            Decompile.Close();

                            Global.lubState = null;
                            #endregion

                            #region Moving decompiled Lua binaries...
                            //Copies all LUBs to the final directory, then erases leftovers.
                            foreach (string LUB in Directory.GetFiles(Properties.Settings.Default.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\luas\", "*.lub", SearchOption.TopDirectoryOnly))
                            {
                                if (File.Exists(LUB))
                                {
                                    File.Copy(Path.Combine(Properties.Settings.Default.unlubPath + Global.sessionID + @"\" + failsafeCheck + @"\luas\", Path.GetFileName(LUB)), Path.Combine(Global.currentPath, Path.GetFileName(LUB)), true);
                                    File.Delete(LUB);
                                }
                            }
                            decompileDialog.Close();
                            #endregion
                        }
                        catch
                        {
                            MessageBox.Show("An error occurred when decompiling the Lua binaries.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            escapeStatus();
                        }
                    }
                }
            }
        }

        void Shortcuts_DecodeMSTs_Click(object sender, EventArgs e)
        {
            //Checks if there are any MSTs in the directory.
            if (Directory.GetFiles(Global.currentPath, "*.mst").Length == 0) MessageBox.Show("There are no MSTs to decode in this directory.", "No MSTs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                try
                {
                    //UnicodeEncoding unicode = new UnicodeEncoding(true, false, false);
                    //byte[] buffer = new byte[1024];

                    // arg0 is assumed to be a file to split
                    foreach (string MST in Directory.GetFiles(Global.currentPath, "*.mst", SearchOption.TopDirectoryOnly))
                    {
                        #region Natsumi Decoder (inactive)
                        //List<string> results = new List<string>();          // <- BTW Hyper this array contains ALL the lines read from the file, including any bugged non-unicode lines.

                        //using (FileStream fs = new FileStream(Global.currentPath + selectedMST, FileMode.Open))
                        //{
                        //    byte[] strbuf = new byte[1024];
                        //    int len = 0, pos = 0;

                        //    while ((len = fs.Read(buffer, 0, buffer.Length)) > 0)
                        //    {
                        //        for (int i = 0; i < len; i += 2)
                        //        {

                        //            // check if the next 2 bytes are an end token. Check second byte last because its most likely 0 anyway but we gotta be sure...
                        //            if ((buffer[i + 1] == 0 || buffer[i + 1] == 0xA) && buffer[i] == 0)
                        //            {
                        //                // this is a null terminator or a newline
                        //                results.Add(unicode.GetString(strbuf, 0, pos));
                        //                pos = 0;

                        //            }
                        //            else
                        //            {
                        //                // if the buffer we allocated is too small, reallocate a larger array...
                        //                if (pos >= strbuf.Length)
                        //                {
                        //                    byte[] _sbuf = strbuf;
                        //                    strbuf = new byte[_sbuf.Length << 1];
                        //                    Array.Copy(_sbuf, strbuf, _sbuf.Length);
                        //                }

                        //                // copy next 2 bytes into strbuffer... If file size is not divisible by 2, this is gonna be a real problem here
                        //                strbuf[pos++] = buffer[i];
                        //                strbuf[pos++] = buffer[i + 1];
                        //            }
                        //        }
                        //    }

                        //    // if the last string is not terminated, print it anyway
                        //    if (pos > 0) results.Add(unicode.GetString(strbuf, 0, pos));
                        //}

                        //File.WriteAllLines(Global.currentPath + Path.GetFileNameWithoutExtension(selectedMST) + ".txt", results);
                        #endregion

                        #region HedgeLib Decoder (active)
                        // Read MST
                        var msgs = new Dictionary<string, string>();
                        string mstPath = Path.Combine(Global.currentPath, MST);

                        using (var fs = File.OpenRead(mstPath))
                        {
                            var reader = new BINAReader(fs);
                            var header = reader.ReadHeader();
                            var sig = reader.ReadSignature();

                            if (sig != "WTXT")
                                throw new InvalidSignatureException("WTXT", sig);

                            uint stringTableOff = reader.ReadUInt32();
                            uint msgEntryCount = reader.ReadUInt32();

                            for (uint i = 0; i < msgEntryCount; ++i)
                            {
                                string name = reader.GetString();
                                uint textOffset = reader.ReadUInt32();
                                uint unknown1 = reader.ReadUInt32();
                                long pos = reader.BaseStream.Position;

                                reader.JumpTo(textOffset, false);
                                string text = reader.ReadNullTerminatedStringUTF16();

                                msgs.Add(name, text);
                                reader.JumpTo(pos, true);
                            }
                        }

                        // Generate CSV
                        string csvPath = Path.Combine(Global.currentPath,
                        $"{Path.GetFileNameWithoutExtension(MST)}.csv");

                        var lines = new List<string>();
                        foreach (var msg in msgs)
                        {
                            lines.Add($"{msg.Key},{msg.Value}");
                        }

                        File.WriteAllLines(csvPath, lines, Encoding.Unicode);
                        #endregion
                    }
                }
                catch
                {
                    MessageBox.Show("An error occurred when decoding the MSTs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    escapeStatus();
                }
            }
        }

        void Shortcuts_ConvertSETs_Click(object sender, EventArgs e)
        {
            //Checks if there are any SETs in the directory.
            if (Directory.GetFiles(Global.currentPath, "*.set").Length == 0) MessageBox.Show("There are no SETs to export in this directory.", "No SETs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                try
                {
                    foreach (string SET in Directory.GetFiles(Global.currentPath, "*.set", SearchOption.TopDirectoryOnly))
                    {
                        if (File.Exists(SET))
                        {
                            var readSET = new S06SetData();
                            readSET.Load(SET);
                            readSET.ExportXML(Global.currentPath + Path.GetFileNameWithoutExtension(SET) + ".xml");
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("An error occurred when exporting the SETs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    escapeStatus();
                }
            }
        }

        void Shortcuts_ConvertXNOs_Click(object sender, EventArgs e)
        {
            //Checks if there are any XNOs in the directory.
            if (Directory.GetFiles(Global.currentPath, "*.xno").Length == 0) MessageBox.Show("There are no XNOs to convert in this directory.", "No XNOs available", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
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
                    if (!Directory.Exists(Properties.Settings.Default.unlubPath + Global.sessionID)) Directory.CreateDirectory(Properties.Settings.Default.unlubPath + Global.sessionID);
                    var failsafeBuildSession = new StringBuilder();
                    failsafeBuildSession.Append(Properties.Settings.Default.archivesPath);
                    failsafeBuildSession.Append(Global.sessionID);
                    failsafeBuildSession.Append(@"\");
                    string failsafeCheck = File.ReadAllText(failsafeBuildSession.ToString() + Global.getIndex);
                    #endregion

                    #region Writing converter...
                    //Writes the converter to the failsafe directory to ensure any XNOs left over from other open archives aren't copied over to the selected archive.
                    if (!Directory.Exists(Properties.Settings.Default.xnoPath + Global.sessionID + @"\" + failsafeCheck)) Directory.CreateDirectory(Properties.Settings.Default.xnoPath + Global.sessionID + @"\" + failsafeCheck);
                    if (!Directory.Exists(Properties.Settings.Default.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xnos")) Directory.CreateDirectory(Properties.Settings.Default.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xnos");
                    if (!File.Exists(Properties.Settings.Default.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xno2dae.exe")) File.WriteAllBytes(Properties.Settings.Default.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xno2dae.exe", Properties.Resources.xno2dae);
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
                            checkedBuildSession.Append(Properties.Settings.Default.xnoPath);
                            checkedBuildSession.Append(Global.sessionID);
                            checkedBuildSession.Append(@"\");
                            checkedBuildSession.Append(failsafeCheck);
                            checkedBuildSession.Append(@"\xno2dae.exe");
                            var checkedWrite = File.Create(Properties.Settings.Default.xnoPath + Global.sessionID + @"\" + failsafeCheck + @"\xno2dae.bat");
                            var checkedText = new UTF8Encoding(true).GetBytes("\"" + checkedBuildSession.ToString() + "\" \"" + XNO + "\"");
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
                    }
                    #endregion

                    convertDialog.Close();
                }
                catch
                {
                    MessageBox.Show("An error occurred when converting the XNOs.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    escapeStatus();
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
            if (Global.serverStatus == "offline") MessageBox.Show("Unable to establish a connection to SEGA Carnival.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (Global.serverStatus == "down") MessageBox.Show("The update servers are currently undergoing maintenance. Apologies for the inconvenience.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else Global.updateState = "user"; CheckForUpdates(Global.latestVersion, "https://segacarnival.com/hyper/updates/latest-master.exe", "https://segacarnival.com/hyper/updates/lib.dll", "https://segacarnival.com/hyper/updates/latest_master.txt");
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
                Global.getIndex = tab_Main.SelectedIndex;

                if (tab_Main.SelectedTab.Text != "New Tab")
                {
                    #region Building location data...
                    //Reads the metadata to get the original location of the ARC.
                    var repackBuildSession = new StringBuilder();
                    repackBuildSession.Append(Properties.Settings.Default.archivesPath);
                    repackBuildSession.Append(Global.sessionID);
                    repackBuildSession.Append(@"\");
                    string failsafeCheck = File.ReadAllText(repackBuildSession.ToString() + tab_Main.SelectedIndex);
                    repackBuildSession.Append(@"\");
                    repackBuildSession.Append(failsafeCheck);
                    repackBuildSession.Append(@"\");
                    #endregion

                    if (File.Exists(repackBuildSession.ToString() + "metadata.ini"))
                    {
                        string metadata = File.ReadAllText(repackBuildSession.ToString() + "metadata.ini");
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

        void Tm_tabCheck_Tick(object sender, EventArgs e)
        {
            //Ensures there's at least only one tab left on the control.
            if (tab_Main.TabPages.Count == 1) mainWindow_CloseTab.Enabled = false; else mainWindow_CloseTab.Enabled = true;

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
                            Global.currentPath = currentARC().Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\";
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
                            Global.currentPath = currentARC().Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\";
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
                    lbl_SetDefault.Visible = true;
                }
            }
            else
            {
                if (tab_Main.SelectedTab.Text.Contains(".arc"))
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
                            Global.currentPath = currentARC().Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\";
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
                            Global.currentPath = currentARC().Url.ToString().Replace("file:///", "").Replace("/", @"\") + @"\";
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
                    lbl_SetDefault.Visible = true;
                }
            }

            if (Global.gameChanged == true)
            {
                foreach (TabPage tab in tab_Main.TabPages)
                {
                    if (tab.Text == "New Tab")
                    {
                        tab_Main.TabPages.Remove(tab);
                        newTab();
                    }
                }

                Global.gameChanged = false;
            }

            if (Global.getIndex == -1) Global.getIndex = 0;
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

        void escapeStatus()
        {
            try
            {
                Status statusForm = (Status)Application.OpenForms["Status"];
                statusForm.Close();
            }
            catch { }
        }
        #endregion

        void unpackARC()
        {
            if (Global.arcState == "typical")
            {
                try
                {
                    Global.arcState = "typical";

                    var unpackDialog = new Status();
                    var parentLeft = Left + ((Width - unpackDialog.Width) / 2);
                    var parentTop = Top + ((Height - unpackDialog.Height) / 2);
                    unpackDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                    unpackDialog.Show();

                    if (File.Exists(ofd_OpenFiles.FileName))
                    {
                        byte[] bytes = File.ReadAllBytes(ofd_OpenFiles.FileName).Take(4).ToArray();
                        var hexString = BitConverter.ToString(bytes); hexString = hexString.Replace("-", " ");
                        if (hexString != "55 AA 38 2D")
                        {
                            byte[] angryBytes = File.ReadAllBytes(ofd_OpenFiles.FileName).ToArray();
                            var angryHexString = BitConverter.ToString(angryBytes); angryHexString = angryHexString.Replace("-", " ");
                            if (angryHexString.Contains("4D 49 47 2E 30 30 2E 31 50 53 50"))
                            {
                                MessageBox.Show("This is an Angry Birds ARC file... You need to use GitMO to extract these. Sonic '06 Toolkit will never support this format.", "Stupid Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else { MessageBox.Show("Invalid ARC file detected.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                        else
                        {
                            #region Building unpack data...
                            //Builds the main string which locates the ARC's final unpack directory.
                            string failsafeCheck = Path.GetRandomFileName(); //Unpacked ARCs will have a unique directory to prevent overwriting.
                            var unpackBuildSession = new StringBuilder();
                            unpackBuildSession.Append(Properties.Settings.Default.archivesPath);
                            unpackBuildSession.Append(Global.sessionID);
                            unpackBuildSession.Append(@"\");
                            unpackBuildSession.Append(failsafeCheck);
                            unpackBuildSession.Append(@"\");
                            unpackBuildSession.Append(Path.GetFileNameWithoutExtension(ofd_OpenFiles.FileName));
                            unpackBuildSession.Append(@"\");
                            if (!Directory.Exists(unpackBuildSession.ToString())) Directory.CreateDirectory(unpackBuildSession.ToString());
                            #endregion

                            #region Building ARC data...
                            //Establishes the failsafe directory and copies the ARC prepare for the unpacking process.
                            var arcBuildSession = new StringBuilder();
                            arcBuildSession.Append(Properties.Settings.Default.archivesPath);
                            arcBuildSession.Append(Global.sessionID);
                            arcBuildSession.Append(@"\");
                            arcBuildSession.Append(failsafeCheck);
                            arcBuildSession.Append(@"\");
                            if (!Directory.Exists(arcBuildSession.ToString())) Directory.CreateDirectory(arcBuildSession.ToString());
                            if (File.Exists(ofd_OpenFiles.FileName)) File.Copy(ofd_OpenFiles.FileName, arcBuildSession.ToString() + Path.GetFileName(ofd_OpenFiles.FileName), true);
                            #endregion

                            #region Unpacking ARC...
                            //Sets up the BASIC application and executes the unpacking process.
                            var basicWrite = File.Create(Properties.Settings.Default.toolsPath + "unpack.bat");
                            var basicSession = new UTF8Encoding(true).GetBytes("\"" + Properties.Settings.Default.unpackFile + "\" \"" + arcBuildSession.ToString() + Path.GetFileName(ofd_OpenFiles.FileName) + "\"");
                            basicWrite.Write(basicSession, 0, basicSession.Length);
                            basicWrite.Close();
                            var unpackSession = new ProcessStartInfo(Properties.Settings.Default.toolsPath + "unpack.bat");
                            unpackSession.WorkingDirectory = Properties.Settings.Default.toolsPath;
                            unpackSession.WindowStyle = ProcessWindowStyle.Hidden;
                            var Unpack = Process.Start(unpackSession);
                            Unpack.WaitForExit();
                            Unpack.Close();
                            #endregion

                            #region Writing metadata...
                            //Writes metadata to the unpacked directory to ensure the original path is remembered.
                            var metadataWrite = File.Create(arcBuildSession.ToString() + "metadata.ini");
                            var metadataSession = new UTF8Encoding(true).GetBytes(ofd_OpenFiles.FileName);
                            metadataWrite.Write(metadataSession, 0, metadataSession.Length);
                            metadataWrite.Close();
                            #endregion

                            #region Navigating...
                            //Creates a new tab if the selected one is being used.
                            if (tab_Main.SelectedTab.Text == "New Tab")
                            {
                                navigateToGame = false;
                                resetTab();

                                currentARC().Navigate(unpackBuildSession.ToString());
                                tab_Main.SelectedTab.Text = Path.GetFileName(ofd_OpenFiles.FileName);
                                navigateToGame = true;
                            }
                            else
                            {
                                navigateToGame = false;
                                newTab();

                                currentARC().Navigate(unpackBuildSession.ToString());
                                tab_Main.SelectedTab.Text = Path.GetFileName(ofd_OpenFiles.FileName);
                                navigateToGame = true;
                            }
                            #endregion

                            #region Building location data...
                            //Writes a file to store the failsafe directory to be referenced later.
                            var storageSession = new StringBuilder();
                            storageSession.Append(Properties.Settings.Default.archivesPath);
                            storageSession.Append(Global.sessionID);
                            storageSession.Append(@"\");
                            storageSession.Append(tab_Main.SelectedIndex);
                            var storageWrite = File.Create(storageSession.ToString());
                            var storageText = new UTF8Encoding(true).GetBytes(failsafeCheck);
                            storageWrite.Write(storageText, 0, storageText.Length);
                            storageWrite.Close();
                            #endregion

                            Text = "Sonic '06 Toolkit - '" + ofd_OpenFiles.FileName +"'";

                            Global.arcState = null;
                            unpackDialog.Close();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("An error occurred when unpacking the archive.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    escapeStatus();
                }
            }
            else
            {
                MessageBox.Show("Unpack State set to invalid value: " + Global.arcState + "\nLine information: " + new System.Diagnostics.StackTrace(true).GetFrame(1).GetFileLineNumber(), "Developer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static bool xenia = false;

        void repackARC()
        {
            if (Global.arcState == "save")
            {
                try
                {
                    Global.arcState = "save";

                    var repackDialog = new Status();
                    var parentLeft = Left + ((Width - repackDialog.Width) / 2);
                    var parentTop = Top + ((Height - repackDialog.Height) / 2);
                    repackDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                    repackDialog.Show();

                    #region Building repack data...
                    //Reads the metadata to get the original location of the ARC.
                    var repackBuildSession = new StringBuilder();
                    repackBuildSession.Append(Properties.Settings.Default.archivesPath);
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
                    var basicWrite = File.Create(Properties.Settings.Default.toolsPath + "repack.bat");
                    if (tab_Main.SelectedTab.Text.Contains(".arc"))
                    {
                        var basicSession = new UTF8Encoding(true).GetBytes("\"" + Properties.Settings.Default.repackFile + "\" \"" + repackBuildSession.ToString() + Path.GetFileNameWithoutExtension(metadata) + "\"");
                        basicWrite.Write(basicSession, 0, basicSession.Length);
                        basicWrite.Close();
                    }
                    else { MessageBox.Show("Please use the Repack ARC As option.", "Free Mode", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                    var repackSession = new ProcessStartInfo(Properties.Settings.Default.toolsPath + "repack.bat");
                    repackSession.WorkingDirectory = Properties.Settings.Default.toolsPath;
                    repackSession.WindowStyle = ProcessWindowStyle.Hidden;
                    var Repack = Process.Start(repackSession);
                    Repack.WaitForExit();
                    Repack.Close();

                    string archivePath = repackBuildSession.ToString() + Path.GetFileName(metadata);
                    if (File.Exists(archivePath)) File.Copy(archivePath, metadata, true); //Copies the repacked ARC back to the original location.
                    #endregion

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

                    Global.arcState = null;
                    repackDialog.Close();
                }
                catch
                {
                    MessageBox.Show("An error occurred when repacking the archive.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    escapeStatus();
                }
            }
            else if (Global.arcState == "save-as")
            {
                sfd_SaveFiles.Title = "Repack ARC As...";
                sfd_SaveFiles.Filter = "ARC Files|*.arc";

                if (sfd_SaveFiles.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Global.arcState = "save-as";

                        var repackDialog = new Status();
                        var parentLeft = Left + ((Width - repackDialog.Width) / 2);
                        var parentTop = Top + ((Height - repackDialog.Height) / 2);
                        repackDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                        repackDialog.Show();

                        #region Building repack data...
                        //Reads the metadata to get the original name of the ARC.
                        var repackBuildSession = new StringBuilder();
                        repackBuildSession.Append(Properties.Settings.Default.archivesPath);
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
                        var basicWrite = File.Create(Properties.Settings.Default.toolsPath + "repack.bat");
                        if (tab_Main.SelectedTab.Text.Contains(".arc"))
                        {
                            var basicSession = new UTF8Encoding(true).GetBytes("\"" + Properties.Settings.Default.repackFile + "\" \"" + repackBuildSession.ToString() + Path.GetFileNameWithoutExtension(metadata) + "\"");
                            basicWrite.Write(basicSession, 0, basicSession.Length);
                            basicWrite.Close();
                        }
                        else
                        {
                            if (metadata.EndsWith(@"\"))
                            {
                                var basicSession = new UTF8Encoding(true).GetBytes("\"" + Properties.Settings.Default.arctoolFile + "\" -i \"" + metadata.Remove(metadata.Length - 1) + "\" -c \"" + sfd_SaveFiles.FileName + "\"");
                                basicWrite.Write(basicSession, 0, basicSession.Length);
                                basicWrite.Close();
                            }
                            else
                            {
                                var basicSession = new UTF8Encoding(true).GetBytes("\"" + Properties.Settings.Default.arctoolFile + "\" -i \"" + metadata + "\" -c \"" + sfd_SaveFiles.FileName + "\"");
                                basicWrite.Write(basicSession, 0, basicSession.Length);
                                basicWrite.Close();
                            }
                        }

                        var repackSession = new ProcessStartInfo(Properties.Settings.Default.toolsPath + "repack.bat");
                        repackSession.WorkingDirectory = Properties.Settings.Default.toolsPath;
                        repackSession.WindowStyle = ProcessWindowStyle.Hidden;
                        var Repack = Process.Start(repackSession);
                        Repack.WaitForExit();
                        Repack.Close();

                        if (tab_Main.SelectedTab.Text.Contains(".arc"))
                        {
                            string archivePath = repackBuildSession.ToString() + Path.GetFileName(metadata);
                            if (File.Exists(archivePath)) File.Copy(archivePath, sfd_SaveFiles.FileName, true);
                        }
                        #endregion

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

                        Global.arcState = null;
                        repackDialog.Close();
                    }
                    catch
                    {
                        MessageBox.Show("An error occurred when repacking the archive.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        escapeStatus();
                    }
                }
            }
            else
            {
                MessageBox.Show("Repack State set to invalid value: " + Global.arcState + "\nLine information: " + new System.Diagnostics.StackTrace(true).GetFrame(1).GetFileLineNumber(), "Developer Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void EraseData()
        {
            if (Properties.Settings.Default.archivesPath != "" && Properties.Settings.Default.unlubPath != "" && Properties.Settings.Default.xnoPath != "")
            {
                var archiveData = new DirectoryInfo(Properties.Settings.Default.archivesPath + Global.sessionID);
                var unlubData = new DirectoryInfo(Properties.Settings.Default.unlubPath + Global.sessionID);
                var xnoData = new DirectoryInfo(Properties.Settings.Default.xnoPath + Global.sessionID);

                try
                {
                    if (Directory.Exists(Properties.Settings.Default.archivesPath + Global.sessionID))
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

                    if (Directory.Exists(Properties.Settings.Default.unlubPath + Global.sessionID))
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

                    if (Directory.Exists(Properties.Settings.Default.xnoPath + Global.sessionID))
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
                    var s06toolkitData = new DirectoryInfo(Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\");

                    try
                    {
                        if (Directory.Exists(Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\"))
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
