using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Security.Principal;

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
    static class Program
    {
        [STAThread]

        public static void Main(string[] args)
        {
            WritePrerequisites();

            var generateSessionID = new Random();
            Tools.Global.sessionID = generateSessionID.Next(1, 99999); //Generates a random number between 1 to 99999 for a unique Session ID.

            if (Properties.Settings.Default.skipWorkaround == true)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main(args));
            }
            else
            {
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Main(args));
                }
                catch
                {
                    WritePrerequisites();
                }
            }
        }

        public static bool RunningAsAdmin()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static void WritePrerequisites()
        {
            if (Properties.Settings.Default.rootPath == string.Empty) Properties.Settings.Default.rootPath = Tools.Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\";
            if (Properties.Settings.Default.toolsPath == string.Empty) Properties.Settings.Default.toolsPath = Tools.Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Tools\";
            if (Properties.Settings.Default.archivesPath == string.Empty) Properties.Settings.Default.archivesPath = Tools.Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Archives\";
            if (Properties.Settings.Default.unlubPath == string.Empty) Properties.Settings.Default.unlubPath = Tools.Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Tools\unlub\";
            if (Properties.Settings.Default.xnoPath == string.Empty) Properties.Settings.Default.xnoPath = Tools.Global.applicationData + @"\Hyper_Development_Team\Sonic '06 Toolkit\Tools\xno2dae\";
            Properties.Settings.Default.Save();

            if (!Directory.Exists(Properties.Settings.Default.rootPath)) Directory.CreateDirectory(Properties.Settings.Default.rootPath);
            if (!Directory.Exists(Properties.Settings.Default.toolsPath)) Directory.CreateDirectory(Properties.Settings.Default.toolsPath);
            if (!Directory.Exists(Properties.Settings.Default.toolsPath + @"Arctool\")) Directory.CreateDirectory(Properties.Settings.Default.toolsPath + @"Arctool\");
            if (!Directory.Exists(Properties.Settings.Default.toolsPath + @"Arctool\arctool\")) Directory.CreateDirectory(Properties.Settings.Default.toolsPath + @"Arctool\arctool\");
            if (!Directory.Exists(Properties.Settings.Default.toolsPath + @"GerbilSoft\")) Directory.CreateDirectory(Properties.Settings.Default.toolsPath + @"GerbilSoft\");
            if (!Directory.Exists(Properties.Settings.Default.toolsPath + @"CsbEditor\")) Directory.CreateDirectory(Properties.Settings.Default.toolsPath + @"CsbEditor\");
            if (!Directory.Exists(Properties.Settings.Default.toolsPath + @"CriWare\")) Directory.CreateDirectory(Properties.Settings.Default.toolsPath + @"CriWare\");
            if (!Directory.Exists(Properties.Settings.Default.toolsPath + @"Microsoft\")) Directory.CreateDirectory(Properties.Settings.Default.toolsPath + @"Microsoft\");
            if (!Directory.Exists(Properties.Settings.Default.toolsPath + @"exiso\")) Directory.CreateDirectory(Properties.Settings.Default.toolsPath + @"exiso\");
            if (!Directory.Exists(Properties.Settings.Default.toolsPath + @"SONY\")) Directory.CreateDirectory(Properties.Settings.Default.toolsPath + @"SONY\");
            if (!Directory.Exists(Properties.Settings.Default.archivesPath)) Directory.CreateDirectory(Properties.Settings.Default.archivesPath);
            if (!Directory.Exists(Properties.Settings.Default.unlubPath)) Directory.CreateDirectory(Properties.Settings.Default.unlubPath);
            if (!Directory.Exists(Properties.Settings.Default.xnoPath)) Directory.CreateDirectory(Properties.Settings.Default.xnoPath);
            if (!Directory.Exists(Properties.Settings.Default.toolsPath + @"LibS06\")) Directory.CreateDirectory(Properties.Settings.Default.toolsPath + @"LibS06\");

            if (Properties.Settings.Default.unpackFile == string.Empty) Properties.Settings.Default.unpackFile = Properties.Settings.Default.toolsPath + @"unpack.exe";
            if (Properties.Settings.Default.repackFile == string.Empty) Properties.Settings.Default.repackFile = Properties.Settings.Default.toolsPath + @"repack.exe";
            if (Properties.Settings.Default.arctoolFile == string.Empty) Properties.Settings.Default.arctoolFile = Properties.Settings.Default.toolsPath + @"Arctool\arctool.exe";
            if (Properties.Settings.Default.mstFile == string.Empty) Properties.Settings.Default.mstFile = Properties.Settings.Default.toolsPath + @"GerbilSoft\mst06.exe";
            if (Properties.Settings.Default.csbFile == string.Empty) Properties.Settings.Default.csbFile = Properties.Settings.Default.toolsPath + @"CsbEditor\CsbEditor.exe";
            if (Properties.Settings.Default.adx2wavFile == string.Empty) Properties.Settings.Default.adx2wavFile = Properties.Settings.Default.toolsPath + @"CriWare\ADX2WAV.exe";
            if (Properties.Settings.Default.criconverterFile == string.Empty) Properties.Settings.Default.criconverterFile = Properties.Settings.Default.toolsPath + @"CriWare\criatomencd.exe";
            if (Properties.Settings.Default.directXFile == string.Empty) Properties.Settings.Default.directXFile = Properties.Settings.Default.toolsPath + @"Microsoft\texconv.exe";
            if (Properties.Settings.Default.exisoFile == string.Empty) Properties.Settings.Default.exisoFile = Properties.Settings.Default.toolsPath + @"exiso\exiso.exe";
            if (Properties.Settings.Default.at3File == string.Empty) Properties.Settings.Default.at3File = Properties.Settings.Default.toolsPath + @"SONY\at3tool.exe";
            if (Properties.Settings.Default.xmaencodeFile == string.Empty) Properties.Settings.Default.xmaencodeFile = Properties.Settings.Default.toolsPath + @"Microsoft\xmaencode2008.exe";
            if (Properties.Settings.Default.towavFile == string.Empty) Properties.Settings.Default.towavFile = Properties.Settings.Default.toolsPath + @"Microsoft\towav.exe";
            if (Properties.Settings.Default.aax2adxFile == string.Empty) Properties.Settings.Default.aax2adxFile = Properties.Settings.Default.toolsPath + @"CriWare\AAX2WAV.exe";
            if (Properties.Settings.Default.csbextractFile == string.Empty) Properties.Settings.Default.csbextractFile = Properties.Settings.Default.toolsPath + @"CriWare\csb_extract.exe";
            if (Properties.Settings.Default.collisionFile == string.Empty) Properties.Settings.Default.collisionFile = Properties.Settings.Default.toolsPath + @"LibS06\s06col.exe";
            if (Properties.Settings.Default.colExportFile == string.Empty) Properties.Settings.Default.colExportFile = Properties.Settings.Default.toolsPath + @"LibS06\s06collision.py";
            Properties.Settings.Default.Save();

            if (!File.Exists(Path.Combine(Application.StartupPath, "HedgeLib.dll"))) File.WriteAllBytes(Path.Combine(Application.StartupPath, "HedgeLib.dll"), Properties.Resources.HedgeLib);
            if (!File.Exists(Path.Combine(Application.StartupPath, "Ookii.Dialogs.dll"))) File.WriteAllBytes(Path.Combine(Application.StartupPath, "Ookii.Dialogs.dll"), Properties.Resources.Ookii_Dialogs);
            if (!File.Exists(Properties.Settings.Default.unpackFile)) File.WriteAllBytes(Properties.Settings.Default.unpackFile, Properties.Resources.unpack);
            if (!File.Exists(Properties.Settings.Default.repackFile)) File.WriteAllBytes(Properties.Settings.Default.repackFile, Properties.Resources.repack);
            if (!File.Exists(Properties.Settings.Default.arctoolFile)) File.WriteAllBytes(Properties.Settings.Default.arctoolFile, Properties.Resources.arctool);
            if (!File.Exists(Properties.Settings.Default.csbFile)) File.WriteAllBytes(Properties.Settings.Default.csbFile, Properties.Resources.CsbEditor);
            if (!File.Exists(Properties.Settings.Default.toolsPath + @"Arctool\arctool\arcc.php")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"Arctool\arctool\arcc.php", Properties.Resources.arcphp);
            if (!File.Exists(Properties.Settings.Default.toolsPath + @"Arctool\arctool\arctool.php")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"Arctool\arctool\arctool.php", Properties.Resources.arctoolphp);
            if (!File.Exists(Properties.Settings.Default.toolsPath + @"CsbEditor\SonicAudioLib.dll")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"CsbEditor\SonicAudioLib.dll", Properties.Resources.SonicAudioLib);
            if (!File.Exists(Properties.Settings.Default.toolsPath + @"CsbEditor\CsbEditor.exe.config")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"CsbEditor\CsbEditor.exe.config", Properties.Resources.CsbEditorConfig);
            if (!File.Exists(Properties.Settings.Default.toolsPath + @"GerbilSoft\mst06.exe")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"GerbilSoft\mst06.exe", Properties.Resources.mst06);
            if (!File.Exists(Properties.Settings.Default.toolsPath + @"GerbilSoft\tinyxml2.dll")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"GerbilSoft\tinyxml2.dll", Properties.Resources.tinyxml2);
            if (!File.Exists(Properties.Settings.Default.adx2wavFile)) File.WriteAllBytes(Properties.Settings.Default.adx2wavFile, Properties.Resources.ADX2WAV);
            if (!File.Exists(Properties.Settings.Default.criconverterFile)) File.WriteAllBytes(Properties.Settings.Default.criconverterFile, Properties.Resources.criatomencd);
            if (!File.Exists(Properties.Settings.Default.aax2adxFile)) File.WriteAllBytes(Properties.Settings.Default.aax2adxFile, Properties.Resources.AAX2ADX);
            if (!File.Exists(Properties.Settings.Default.csbextractFile)) File.WriteAllBytes(Properties.Settings.Default.csbextractFile, Properties.Resources.csb_extract);
            if (!File.Exists(Properties.Settings.Default.toolsPath + @"CriWare\AsyncAudioEncoder.dll")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"CriWare\AsyncAudioEncoder.dll", Properties.Resources.AsyncAudioEncoder);
            if (!File.Exists(Properties.Settings.Default.toolsPath + @"CriWare\AudioStream.dll")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"CriWare\AudioStream.dll", Properties.Resources.AudioStream);
            if (!File.Exists(Properties.Settings.Default.toolsPath + @"CriWare\criatomencd.exe.config")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"CriWare\criatomencd.exe.config", Properties.Resources.criatomencdConfig);
            if (!File.Exists(Properties.Settings.Default.toolsPath + @"CriWare\CriAtomEncoderComponent.dll")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"CriWare\CriAtomEncoderComponent.dll", Properties.Resources.CriAtomEncoderComponent);
            if (!File.Exists(Properties.Settings.Default.toolsPath + @"CriWare\CriSamplingRateConverter.dll")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"CriWare\CriSamplingRateConverter.dll", Properties.Resources.CriSamplingRateConverter);
            if (!File.Exists(Properties.Settings.Default.toolsPath + @"CriWare\vsthost.dll")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"CriWare\vsthost.dll", Properties.Resources.vsthost);
            if (!File.Exists(Properties.Settings.Default.toolsPath + @"exiso\exiso.exe")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"exiso\exiso.exe", Properties.Resources.exiso);
            if (!File.Exists(Properties.Settings.Default.toolsPath + @"SONY\at3tool.exe")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"SONY\at3tool.exe", Properties.Resources.at3tool);
            if (!File.Exists(Properties.Settings.Default.toolsPath + @"Microsoft\texconv.exe")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"Microsoft\texconv.exe", Properties.Resources.texconv);
            if (!File.Exists(Properties.Settings.Default.toolsPath + @"Microsoft\xmaencode2008.exe")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"Microsoft\xmaencode2008.exe", Properties.Resources.xmaencode2008);
            if (!File.Exists(Properties.Settings.Default.toolsPath + @"Microsoft\towav.exe")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"Microsoft\towav.exe", Properties.Resources.towav);
            if (!File.Exists(Properties.Settings.Default.collisionFile)) File.WriteAllBytes(Properties.Settings.Default.collisionFile, Properties.Resources.s06col);
            if (!File.Exists(Properties.Settings.Default.colExportFile)) File.WriteAllBytes(Properties.Settings.Default.colExportFile, Properties.Resources.s06collision);
            if (!File.Exists(Properties.Settings.Default.toolsPath + @"unlub\unlub.jar")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"unlub\unlub.jar", Properties.Resources.unlub);
            if (!File.Exists(Properties.Settings.Default.toolsPath + @"unlub\lua50.dll")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"unlub\lua50.dll", Properties.Resources.lua50);
            if (!File.Exists(Properties.Settings.Default.toolsPath + @"unlub\luac50.exe")) File.WriteAllBytes(Properties.Settings.Default.toolsPath + @"unlub\luac50.exe", Properties.Resources.luac50);

            Application.Exit();
        }
    }
}
