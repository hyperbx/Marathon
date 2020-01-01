using System;
using System.IO;
using System.Linq;
using Toolkit.Text;
using Toolkit.Tools;
using System.Diagnostics;
using System.Windows.Forms;
using System.Security.Principal;

// Sonic '06 Toolkit is licensed under the MIT License:
/*
 * MIT License

 * Copyright (c) 2020 Gabriel (HyperPolygon64)

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
    static class Program
    {
        public static string applicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static int sessionID = new Random().Next(1, 99999);

        [STAThread]

        static void Main(string[] args) {
            Directory.CreateDirectory($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\");

            if (!File.Exists(Path.Combine(Application.StartupPath, "Lua50.dll")))
                File.WriteAllBytes(Path.Combine(Application.StartupPath, "Lua50.dll"), Properties.Resources.lua50);

            if (!File.Exists(Path.Combine(Application.StartupPath, "HedgeLib.dll")))
                File.WriteAllBytes(Path.Combine(Application.StartupPath, "HedgeLib.dll"), Properties.Resources.HedgeLib);

            if (!File.Exists(Path.Combine(Application.StartupPath, "Ookii.Dialogs.dll")))
                File.WriteAllBytes(Path.Combine(Application.StartupPath, "Ookii.Dialogs.dll"), Properties.Resources.Ookii_Dialogs);

            if (!File.Exists(Path.Combine(Application.StartupPath, "SonicAudioLib.dll")))
                File.WriteAllBytes(Path.Combine(Application.StartupPath, "SonicAudioLib.dll"), Properties.Resources.SonicAudioLib);

            if (!File.Exists(Path.Combine(Application.StartupPath, "TinyXML2.dll")))
                File.WriteAllBytes(Path.Combine(Application.StartupPath, "TinyXML2.dll"), Properties.Resources.tinyxml2);

            if (!File.Exists($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\arctool.exe"))
                File.WriteAllBytes($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\arctool.exe", Properties.Resources.arctool);

            if (!File.Exists($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\unpack.exe"))
                File.WriteAllBytes($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\unpack.exe", Properties.Resources.unpack);

            if (!File.Exists($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\repack.exe"))
                File.WriteAllBytes($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\repack.exe", Properties.Resources.repack);

            if (!File.Exists($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\luac50.exe"))
                File.WriteAllBytes($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\luac50.exe", Properties.Resources.luac50);

            if (!File.Exists($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\unlub.jar"))
                File.WriteAllBytes($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\unlub.jar", Properties.Resources.unlub);

            if (!File.Exists($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\xextool.exe"))
                File.WriteAllBytes($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\xextool.exe", Properties.Resources.xextool);

            if (!File.Exists($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\texconv.exe"))
                File.WriteAllBytes($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\texconv.exe", Properties.Resources.texconv);

            if (!File.Exists($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\PS3_at3tool.exe"))
                File.WriteAllBytes($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\PS3_at3tool.exe", Properties.Resources.PS3_at3tool);

            if (!File.Exists($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\xmaencode2008.exe"))
                File.WriteAllBytes($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\xmaencode2008.exe", Properties.Resources.xmaencode2008);

            if (!File.Exists($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\towav.exe"))
                File.WriteAllBytes($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\towav.exe", Properties.Resources.towav);

            if (!File.Exists($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\mst06.exe"))
                File.WriteAllBytes($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\mst06.exe", Properties.Resources.mst06);

            if (!File.Exists($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\xno2dae.exe"))
                File.WriteAllBytes($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\xno2dae.exe", Properties.Resources.xno2dae);

            if (!File.Exists($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\s06col.exe"))
                File.WriteAllBytes($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\s06col.exe", Properties.Resources.s06col);

            if (!File.Exists($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\s06collision.exe"))
                File.WriteAllBytes($"{applicationData}\\Hyper_Development_Team\\Sonic '06 Toolkit\\Tools\\s06collision.exe", Properties.Resources.s06collision);

            Verification.VerifyDependencies(Paths.Tools);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main(args, sessionID));
        }

        public static bool RunningAsAdmin() { return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator); }

        public static bool CheckProcessStateByName(string name, bool withoutExtension) { 
            if (withoutExtension) return Process.GetProcessesByName(Path.GetFileNameWithoutExtension(name)).Count() > 0;
            else return Process.GetProcessesByName(Path.GetFileName(name)).Count() > 0;
        }

        public static void ExecuteAsAdmin(string fileName, string arguments) {
            Process proc = new Process();
            proc.StartInfo.FileName = fileName;
            proc.StartInfo.Arguments = arguments;
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.Verb = "runas";
            proc.Start();
        }

        public static void Restart() {
            Process proc = new Process();
            proc.StartInfo.FileName = Application.ExecutablePath;
            proc.Start();
            Application.Exit();
        }
    }
}
