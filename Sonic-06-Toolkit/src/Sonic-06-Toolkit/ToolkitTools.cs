using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using Toolkit.Text;
using Ookii.Dialogs;
using HedgeLib.Sets;
using Microsoft.Win32;
using VGAudio.Formats;
using SonicAudioLib.IO;
using System.Diagnostics;
using Toolkit.EnvironmentX;
using System.Windows.Forms;
using System.Threading.Tasks;
using VGAudio.Containers.Adx;
using VGAudio.Containers.Wave;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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

namespace Toolkit.Tools
{
    class Updater
    {
        public static void CheckForUpdates(string currentVersion, string newVersionDownloadLink, string versionInfoLink, bool userAccess) {
            try {
                string latestVersion;
                string changeLogs;

                try {
                    latestVersion = new TimedWebClient { Timeout = 100000 }.DownloadString(versionInfoLink);
                } catch { return; }

                try {
                    changeLogs = new TimedWebClient { Timeout = 100000 }.DownloadString("https://segacarnival.com/hyper/updates/changelogs.txt");
                } catch { changeLogs = "► Allan please add details"; }

                if (latestVersion.Contains("Version")) {
                    if (latestVersion != currentVersion) {
                        DialogResult confirmUpdate = MessageBox.Show($"Sonic '06 Toolkit - {latestVersion} is now available!\n\nChangelogs:\n{changeLogs}\n\nDo you wish to download it?", "New update available!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        switch (confirmUpdate)
                        {
                            case DialogResult.Yes:
                                var exists = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetEntryAssembly().Location)).Count() > 1;
                                if (exists) { MessageBox.Show("Please close any other instances of Sonic '06 Toolkit and try again.", "Stupid Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                else
                                    try {
                                        if (File.Exists(Application.ExecutablePath))
                                            new ToolkitUpdater(latestVersion, newVersionDownloadLink).ShowDialog();
                                        else MessageBox.Show("Sonic '06 Toolkit doesn't exist..?", "Stupid Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    } catch {
                                        MessageBox.Show("An error occurred when updating Sonic '06 Toolkit.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                break;
                        }
                    } else if (userAccess) MessageBox.Show("There are currently no updates available.", "Sonic '06 Toolkit", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else {
                    Main.serverStatus = "down";
                    if (!Properties.Settings.Default.env_updaterDisabled) MessageBox.Show("The update servers are currently undergoing maintenance. Apologies for the inconvenience.", "Server Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch {
                Main.serverStatus = "offline";
            }
        }
    }

    public class TimedWebClient : WebClient
    {
        public int Timeout { get; set; }

        public TimedWebClient() {
            Timeout = 100000;
        }

        protected override WebRequest GetWebRequest(Uri address) {
            var objWebRequest = base.GetWebRequest(address);
            objWebRequest.Timeout = Timeout;
            return objWebRequest;
        }
    }

    class Batch
    {
        private static Main mainForm = new Main(Array.Empty<string>(), Program.sessionID);

        public static void DecodeADX(string location, SearchOption searchMethod, bool showFullPath) {
            foreach (string ADX in Directory.GetFiles(location, "*.adx", searchMethod))
                if (File.Exists(ADX) && Verification.VerifyMagicNumberCommon(ADX)) {
                    mainForm.Status = StatusMessages.cmn_Converting(ADX, "WAV", showFullPath);
                    byte[] adxFile = File.ReadAllBytes(ADX);
                    AudioData audio = new AdxReader().Read(adxFile);
                    byte[] wavFile = new WaveWriter().GetFile(audio);
                    File.WriteAllBytes(Path.Combine(location, $"{Path.GetFileNameWithoutExtension(ADX)}.wav"), wavFile);
                } else { mainForm.Status = StatusMessages.ex_InvalidFile(Path.GetFileName(ADX), "ADX", showFullPath); }
        }

        public static async void DecodeAT3(string location, SearchOption searchMethod, bool showFullPath) {
            foreach (string AT3 in Directory.GetFiles(location, "*.at3", searchMethod))
                if (File.Exists(AT3) && Verification.VerifyMagicNumberCommon(AT3)) {
                    mainForm.Status = StatusMessages.cmn_Converting(AT3, "WAV", showFullPath);
                    var process = await ProcessAsyncHelper.ExecuteShellCommand(Paths.AT3Tool,
                                        $"-d \"{AT3}\" \"{Path.Combine(location, Path.GetFileNameWithoutExtension(AT3))}.wav\"",
                                        Application.StartupPath,
                                        100000);
                    if (process.ExitCode != 0)
                        mainForm.Status = StatusMessages.cmn_ConvertFailed(AT3, "WAV", showFullPath);
                } else { mainForm.Status = StatusMessages.ex_InvalidFile(Path.GetFileName(AT3), "AT3", showFullPath); }
        }

        public static async void DecodeBIN(string location, SearchOption searchMethod, bool showFullPath) {
            foreach (string BIN in Directory.GetFiles(location, "*.bin", searchMethod))
                if (File.Exists(BIN) && Verification.VerifyMagicNumberExtended(BIN)) {
                    mainForm.Status = StatusMessages.cmn_Exporting(BIN, showFullPath);
                    var process = await ProcessAsyncHelper.ExecuteShellCommand(Paths.BINDecoder,
                                        $"\"{BIN}\"",
                                        Path.GetDirectoryName(BIN),
                                        100000);
                    if (process.ExitCode != 0)
                        mainForm.Status = StatusMessages.cmn_ExportFailed(BIN, showFullPath);
                } else { mainForm.Status = StatusMessages.ex_InvalidFile(Path.GetFileName(BIN), "BIN", showFullPath); }
        }

        public static void UnpackCSB(bool WAV, string location, SearchOption searchMethod, bool showFullPath) {
            foreach (string CSB in Directory.GetFiles(location, "*.csb", searchMethod))
                if (File.Exists(CSB) && Verification.VerifyMagicNumberCommon(CSB)) {
                    try {
                        mainForm.Status = StatusMessages.cmn_Unpacking(CSB, showFullPath);
                        var extractor = new DataExtractor {
                            MaxThreads = 1,
                            BufferSize = 4096,
                            EnableThreading = false
                        };
                        Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(CSB), Path.GetFileNameWithoutExtension(CSB)));
                        CSBTools.ExtractCSBNodes(extractor, CSB, Path.Combine(Path.GetDirectoryName(CSB), Path.GetFileNameWithoutExtension(CSB)));
                        extractor.Run();
                        mainForm.Status = StatusMessages.cmn_Unpacked(CSB, showFullPath);
                    } catch { mainForm.Status = StatusMessages.cmn_UnpackFailed(CSB, showFullPath); }

                    if (WAV) {
                         if (Directory.Exists(Path.Combine(Path.GetDirectoryName(CSB), Path.GetFileNameWithoutExtension(CSB))))
                            foreach (string ADX in Directory.GetFiles(Path.Combine(Path.GetDirectoryName(CSB), Path.GetFileNameWithoutExtension(CSB)), "*.adx", SearchOption.AllDirectories))
                                try {
                                    mainForm.Status = StatusMessages.cmn_Converting(ADX, "WAV", showFullPath);
                                    byte[] adxFile = File.ReadAllBytes(ADX);
                                    AudioData audio = new AdxReader().Read(adxFile);
                                    byte[] wavFile = new WaveWriter().GetFile(audio);
                                    File.WriteAllBytes(Path.Combine(Path.GetDirectoryName(ADX), $"{Path.GetFileNameWithoutExtension(ADX)}.wav"), wavFile);
                                    File.Delete(ADX);
                                } catch { mainForm.Status = StatusMessages.cmn_ConvertFailed(ADX, "WAV", showFullPath); }
                    }
                } else { mainForm.Status = StatusMessages.ex_InvalidFile(Path.GetFileName(CSB), "CSB", showFullPath); }
        }

        public static async void DecodeDDS(string location, SearchOption searchMethod, bool showFullPath) {
            foreach (string DDS in Directory.GetFiles(location, "*.dds", searchMethod))
                if (File.Exists(DDS) && Verification.VerifyMagicNumberCommon(DDS)) {
                    mainForm.Status = StatusMessages.cmn_Converting(DDS, "PNG", showFullPath);
                    var convert = await ProcessAsyncHelper.ExecuteShellCommand(Paths.DDSTool,
                                        $"-ft png -srgb \"{DDS}\" \"{Path.GetFileNameWithoutExtension(DDS)}.png\"",
                                        Path.GetDirectoryName(DDS),
                                        100000);
                    if (convert.Completed)
                        if (convert.ExitCode != 0)
                            mainForm.Status = StatusMessages.cmn_ConvertFailed(DDS, "PNG", showFullPath);
                } else { mainForm.Status = StatusMessages.ex_InvalidFile(Path.GetFileName(DDS), "DDS", showFullPath); }
        }

        public static async void DecompileLUB(string location, SearchOption searchMethod, string filter, bool showFullPath) {
            if (Verification.VerifyApplicationIntegrity(Paths.LuaDecompiler)) {
                foreach (string LUB in Directory.GetFiles(location, filter, searchMethod))
                    if (File.Exists(LUB) && Verification.VerifyMagicNumberExtended(LUB)) {
                        if (Path.GetFileName(LUB) == "standard.lub") break;

                        mainForm.Status = StatusMessages.lua_Decompiling(LUB, showFullPath);
                        var decompile = await ProcessAsyncHelper.ExecuteShellCommand("java.exe",
                                                $"-jar \"{Paths.LuaDecompiler}\" \"{LUB}\"",
                                                location,
                                                100000);
                        if (decompile.Completed)
                            if (decompile.ExitCode == 0)
                                File.WriteAllText(LUB, decompile.Output);
                            else if (decompile.ExitCode == -1)
                                mainForm.Status = StatusMessages.lua_DecompileFailed(LUB, showFullPath);
                    } else { mainForm.Status = StatusMessages.ex_InvalidFile(Path.GetFileName(LUB), "LUB", showFullPath); }
            }
        }

        public static void DecodeSET(string location, SearchOption searchMethod, bool showFullPath) {
            foreach (string SET in Directory.GetFiles(location, "*.set", searchMethod))
                if (File.Exists(SET) && Verification.VerifyMagicNumberExtended(SET)) {
                    mainForm.Status = StatusMessages.cmn_Exporting(SET, showFullPath);
                    var readSET = new S06SetData();
                    readSET.Load(SET);
                    readSET.ExportXML(Path.Combine(Path.GetDirectoryName(SET), $"{Path.GetFileNameWithoutExtension(SET)}.xml"));
                } else { mainForm.Status = StatusMessages.ex_InvalidFile(Path.GetFileName(SET), "SET", showFullPath); }
        }

        public static async void DecodeMST(string location, SearchOption searchMethod, bool showFullPath) {
            foreach (string MST in Directory.GetFiles(location, "*.mst", searchMethod))
                if (File.Exists(MST) && Verification.VerifyMagicNumberExtended(MST)) {
                    mainForm.Status = StatusMessages.cmn_Exporting(MST, showFullPath);
                    var process = await ProcessAsyncHelper.ExecuteShellCommand(Paths.MSTTool,
                                        $"\"{MST}\"",
                                        Application.StartupPath,
                                        100000);
                    if (process.Completed)
                        if (process.ExitCode != 0)
                            mainForm.Status = StatusMessages.cmn_ExportFailed(MST, showFullPath);
                } else { mainForm.Status = StatusMessages.ex_InvalidFile(Path.GetFileName(MST), "MST", showFullPath); }
        }

        public static async void DecryptXEX(string location, SearchOption searchMethod, bool showFullPath) {
            foreach (string XEX in Directory.GetFiles(location, "*.xex", searchMethod))
                if (File.Exists(XEX) && Verification.VerifyMagicNumberCommon(XEX)) {
                    mainForm.Status = StatusMessages.xex_Decrypting(XEX, showFullPath);
                    var process = await ProcessAsyncHelper.ExecuteShellCommand(Paths.XexTool,
                                        $"-e u \"{XEX}\"",
                                        location,
                                        100000);
                    if (process.Completed)
                        if (process.ExitCode != 0)
                            mainForm.Status = StatusMessages.xex_DecryptFailed(XEX, showFullPath);
                } else { mainForm.Status = StatusMessages.ex_InvalidFile(Path.GetFileName(XEX), "XEX", showFullPath); }
        }

        public static async void DecodeXMA(string location, SearchOption searchMethod, bool showFullPath) {
            foreach (string XMA in Directory.GetFiles(location, "*.xma", searchMethod))
                if (File.Exists(XMA) && Verification.VerifyMagicNumberCommon(XMA)) {
                    mainForm.Status = StatusMessages.cmn_Converting(XMA, "WAV", showFullPath);
                    try {
                        byte[] xmaBytes = File.ReadAllBytes(XMA).ToArray();
                        string hexString = BitConverter.ToString(xmaBytes).Replace("-", "");
                        if (hexString.Contains(Properties.Resources.xma_Patch)) {
                            FileInfo fi = new FileInfo(XMA);
                            FileStream fs = fi.Open(FileMode.Open);
                            long bytesToDelete = 56;
                            fs.SetLength(Math.Max(0, fi.Length - bytesToDelete));
                            fs.Close();
                        }
                    } catch { mainForm.Status = StatusMessages.xma_DecodeFooterError(XMA, showFullPath); return; }
                    try {
                        if (File.Exists($"{Path.Combine(Path.GetDirectoryName(XMA), Path.GetFileNameWithoutExtension(XMA))}.wav"))
                            File.Delete($"{Path.Combine(Path.GetDirectoryName(XMA), Path.GetFileNameWithoutExtension(XMA))}.wav");
                    } catch { }
                    var process = await ProcessAsyncHelper.ExecuteShellCommand(Paths.XMADecoder,
                                        $"\"{XMA}\"",
                                        location,
                                        100000);
                    if (process.ExitCode != 0)
                        mainForm.Status = StatusMessages.cmn_ConvertFailed(XMA, "WAV", showFullPath);
                } else { mainForm.Status = StatusMessages.ex_InvalidFile(Path.GetFileName(XMA), "XMA", showFullPath); }
        }

        public static async void DecodeXNO(string location, SearchOption searchMethod, bool showFullPath) {
            foreach (string XNO in Directory.GetFiles(location, "*.xno", searchMethod))
                if (File.Exists(XNO) && Verification.VerifyMagicNumberCommon(XNO)) {
                    mainForm.Status = StatusMessages.cmn_Converting(XNO, "DAE", showFullPath);
                    var process = await ProcessAsyncHelper.ExecuteShellCommand(Paths.XNODecoder,
                                        $"\"{XNO}\"",
                                        location,
                                        100000);
                    if (process.Completed)
                        if (process.ExitCode != 0)
                            mainForm.Status = StatusMessages.cmn_ConvertFailed(XNO, "DAE", showFullPath);
                } else { mainForm.Status = StatusMessages.ex_InvalidFile(Path.GetFileName(XNO), "XNO", showFullPath); }
        }
    }

    class Browsers
    {
        public static string CommonBrowser(bool folder, string title, string filter) {
            if (!folder) {
                OpenFileDialog fileBrowser = new OpenFileDialog {
                    Title = title,
                    Filter = filter,
                    RestoreDirectory = true,
                };
                if (fileBrowser.ShowDialog() == DialogResult.OK) return fileBrowser.FileName;
                else return string.Empty;
            } else {
                VistaFolderBrowserDialog folderBrowser = new VistaFolderBrowserDialog {
                    Description = title,
                    UseDescriptionForTitle = true,
                };
                if (folderBrowser.ShowDialog() == DialogResult.OK) return folderBrowser.SelectedPath;
                else return string.Empty;
            }
        }

        public static string SaveFile(string title, string filter)  {
            SaveFileDialog saveFiles = new SaveFileDialog {
                Title = title,
                Filter = filter,
                RestoreDirectory = true,
            };
            if (saveFiles.ShowDialog() == DialogResult.OK) return saveFiles.FileName;
            else return string.Empty;
        }
    }

    class ByteArray
    {
        public static byte[] StringToByteArray(string hex) {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static byte[] StringToByteArrayExtended(string hex) {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static bool ByteArrayToFile(string fileName, byte[] byteArray) {
            using (var fs = new FileStream(fileName, FileMode.Append, FileAccess.Write)) {
                fs.Write(byteArray, 0, byteArray.Length);
                return true;
            }
        }
    }

    class Verification
    {
        public static bool VerifyMagicNumberCommon(string path) {
            try {
                string hexString = BitConverter.ToString(File.ReadAllBytes(path).Take(4).ToArray()).Replace("-", " ");

                if (Path.GetExtension(path).ToLower() == ".arc") {
                    if (hexString == "55 AA 38 2D") return true;
                    else return false;
                } else if (Path.GetExtension(path).ToLower() == ".dds") {
                    if (hexString == "44 44 53 20") return true;
                    else return false;
                } else if (Path.GetExtension(path).ToLower() == ".xno" || Path.GetExtension(path).ToLower() == ".xnm") {
                    if (hexString == "4E 58 49 46") return true;
                    else return false;
                } else if (Path.GetExtension(path).ToLower() == ".adx") {
                    if (hexString == "80 00 00 24") return true;
                    else return false;
                } else if (Path.GetExtension(path).ToLower() == ".csb") {
                    if (hexString == "40 55 54 46") return true;
                    else return false;
                } else if (Path.GetExtension(path).ToLower() == ".at3" || Path.GetExtension(path).ToLower() == ".wav" || Path.GetExtension(path).ToLower() == ".xma") {
                    if (hexString == "52 49 46 46") return true;
                    else return false;
                } else if (Path.GetExtension(path).ToLower() == ".xex") {
                    if (hexString == "58 45 58 32") return true;
                    else return false;
                } else if (Path.GetExtension(path).ToLower() == ".png") {
                    if (hexString == "89 50 4E 47") return true;
                    else return false;
                } else if (Path.GetExtension(path).ToLower() == ".obj") {
                    if (hexString == "6D 74 6C 6C") return true;
                    else return false;
                }
                return false;
            } catch { return false; }
        }

        public static bool VerifyMagicNumberExtended(string path) {
            try {
                string hexString = BitConverter.ToString(File.ReadAllBytes(path).Take(50).ToArray()).Replace("-", " ");

                if (Path.GetExtension(path).ToLower() == ".bin" || Path.GetExtension(path).ToLower() == ".set" || Path.GetExtension(path).ToLower() == ".mst") {
                    if (hexString.Contains("31 42 42 49 4E 41")) return true;
                    else return false;
                } else if (Path.GetExtension(path).ToLower() == ".lub") {
                    if (hexString.Contains("1B 4C 75 61 50")) return true;
                    else return false;
                }
                return false;
            } catch { return false; }
        }

        public static bool VerifyCriWareSoundBank(string path) {
            List<string> sounds = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".adx") || s.EndsWith(".wav")).ToList();
            if (sounds.Count > 0) return true;
            else return false;
        }

        public static bool VerifyApplicationIntegrity(string path) {
            if (Path.GetFileName(path) == "luac50.exe")
                if (File.Exists(Path.Combine(Application.StartupPath, "Lua50.dll"))) return true;
                else return false;
            else if (Path.GetFileName(path) == "unlub.jar")
                if (JavaCheck()) return true;
                else return false;
            else if (Path.GetFileName(path) == "s06collision.exe")
                if (PythonCheck()) return true;
                else return false;
            return false;
        }

        public static bool VerifyXML(string path, string type) {
            try {
                string hexString = BitConverter.ToString(File.ReadAllBytes(path).Take(100).ToArray()).Replace("-", " ");

                if (Path.GetExtension(path).ToLower() == ".xml") {
                    if (type == "SET")
                        if (hexString.Contains("3C 53 65 74 44 61 74 61 3E")) return true;
                        else return false;
                    else if (type == "MST")
                        if (hexString.Contains("3C 6D 73 74 30 36")) return true;
                        else return false;
                    else return false;
                }
                return false;
            } catch { return false; }
        }

        public static bool JavaCheck() {
            try {
                var javaArg = new ProcessStartInfo("java", "-version");
                javaArg.WindowStyle = ProcessWindowStyle.Hidden;
                javaArg.RedirectStandardOutput = true;
                javaArg.RedirectStandardError = true;
                javaArg.UseShellExecute = false;
                javaArg.CreateNoWindow = true;
                var javaProcess = new Process();
                javaProcess.StartInfo = javaArg;
                javaProcess.Start();
                return true;
            } catch { return false; }
        }

        public static bool PythonCheck() {
            try {
                Process pythonArg = new Process();
                pythonArg.StartInfo.UseShellExecute = false;
                pythonArg.StartInfo.RedirectStandardOutput = true;
                pythonArg.StartInfo.CreateNoWindow = true;
                pythonArg.StartInfo.FileName = "python";
                pythonArg.StartInfo.Arguments = "--version";
                pythonArg.Start();
                return true;
            } catch { return false; }
        }
    }

    class Windows
    {
        public static void SetAssociation(string extension) {
            // Delete the key instead of trying to change it
            var CurrentUser = Registry.CurrentUser.OpenSubKey($"Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts\\{extension}", true);
            CurrentUser.DeleteSubKey("UserChoice", false);
            CurrentUser.Close();

            // Tell explorer the file association has been changed
            SHChangeNotify(0x08000000, 0x0000, IntPtr.Zero, IntPtr.Zero);
        }
        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);
    }

    public static class ProcessAsyncHelper
    {
        public static async Task<ProcessResult> ExecuteShellCommand(string command, string arguments, string working, int timeout) {
            var result = new ProcessResult();

            using (var process = new Process()) {
                // If you run bash-script on Linux it is possible that ExitCode can be 255.
                // To fix it you can try to add '#!/bin/bash' header to the script.

                process.StartInfo.FileName = command;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.WorkingDirectory = working;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = true;

                var outputBuilder = new StringBuilder();
                var outputCloseEvent = new TaskCompletionSource<bool>();

                process.OutputDataReceived += (s, e) => {
                    // The output stream has been closed i.e. the process has terminated
                    if (e.Data == null) {
                        outputCloseEvent.SetResult(true);
                    } else {
                        outputBuilder.AppendLine(e.Data);
                    }
                };

                var errorBuilder = new StringBuilder();
                var errorCloseEvent = new TaskCompletionSource<bool>();

                process.ErrorDataReceived += (s, e) => {
                    // The error stream has been closed i.e. the process has terminated
                    if (e.Data == null) {
                        errorCloseEvent.SetResult(true);
                    } else {
                        errorBuilder.AppendLine(e.Data);
                    }
                };

                bool isStarted;

                try { isStarted = process.Start(); }
                catch (Exception error) {
                    // Usually it occurs when an executable file is not found or is not executable

                    result.Completed = true;
                    result.ExitCode = -1;
                    result.Error = error.Message;

                    isStarted = false;
                }

                if (isStarted) {
                    // Reads the output stream first and then waits because deadlocks are possible
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    // Creates task to wait for process exit using timeout
                    var waitForExit = WaitForExitAsync(process, timeout);

                    // Create task to wait for process exit and closing all output streams
                    var processTask = Task.WhenAll(waitForExit, outputCloseEvent.Task, errorCloseEvent.Task);

                    // Waits process completion and then checks it was not completed by timeout
                    if (await Task.WhenAny(Task.Delay(timeout), processTask) == processTask && waitForExit.Result) {
                        result.Completed = true;
                        result.ExitCode = process.ExitCode;

                        // Adds process output if it was completed with error
                        result.Output = outputBuilder.ToString();
                        result.Error = errorBuilder.ToString();
                    } else {
                        try {
                            // Kill hung process
                            process.Kill();
                        }
                        catch { }
                    }
                }
            }

            return result;
        }

        private static Task<bool> WaitForExitAsync(Process process, int timeout) { return Task.Run(() => process.WaitForExit(timeout)); }

        public struct ProcessResult {
            public bool Completed;
            public int? ExitCode;
            public string Output;
            public string Error;
        }
    }
}
