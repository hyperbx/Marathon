using System;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using HedgeLib.Sets;
using System.Diagnostics;
using System.Windows.Forms;

namespace Sonic_06_Toolkit.Tools
{
    class ADX
    {
        static ProcessStartInfo adxSession;

        public static void ConvertToWAV(string args, string selectedADX)
        {
            if (Global.adxState == "launch-adx")
            {
                adxSession = new ProcessStartInfo(Properties.Settings.Default.adx2wavFile, $"\"{args}\" \"{Path.GetDirectoryName(args)}\\{Path.GetFileNameWithoutExtension(args)}.wav\"")
                {
                    WorkingDirectory = Path.GetDirectoryName(args),
                    WindowStyle = ProcessWindowStyle.Hidden
                };
            }
            else if (Global.adxState == "adx")
            {
                adxSession = new ProcessStartInfo(Properties.Settings.Default.adx2wavFile, $"\"{Path.Combine(Global.currentPath, selectedADX)}\" \"{Path.GetDirectoryName(Path.Combine(Global.currentPath, selectedADX))}\\{Path.GetFileNameWithoutExtension(Path.Combine(Global.currentPath, selectedADX))}.wav\"")
                {
                    WorkingDirectory = Global.currentPath,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
            }

            Begin();
        }

        public static void ConvertToADX(string selectedWAV)
        {
            if (Global.adxState == "wav")
            {
                adxSession = new ProcessStartInfo(Properties.Settings.Default.criconverterFile, $"\"{Path.Combine(Global.currentPath, selectedWAV)}\" \"{Path.GetDirectoryName(Path.Combine(Global.currentPath, selectedWAV))}\\{Path.GetFileNameWithoutExtension(Path.Combine(Global.currentPath, selectedWAV))}.adx\" -codec=adx -volume=" + ADX_Studio.vol + " -downmix=" + ADX_Studio.downmix + ADX_Studio.ignoreLoop + ADX_Studio.removeLoop)
                {
                    WorkingDirectory = Global.currentPath,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
            }

            Begin();
        }

        static void Begin()
        {
            if (Debugger.unsafeState == true) { MessageBox.Show("CriWare tools are missing. Please restart Sonic '06 Toolkit and try again.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                if (File.Exists(Properties.Settings.Default.adx2wavFile) || File.Exists(Properties.Settings.Default.criconverterFile))
                {
                    var Convert = Process.Start(adxSession);
                    var convertDialog = new Status();
                    var parentLeft = Main.FormLeft + ((Main.FormWidth - convertDialog.Width) / 2);
                    var parentTop = Main.FormTop + ((Main.FormHeight - convertDialog.Height) / 2);
                    if (Global.adxState == "launch-adx") convertDialog.StartPosition = FormStartPosition.CenterScreen;
                    else convertDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                    convertDialog.Show();
                    Convert.WaitForExit();
                    Convert.Close();
                    convertDialog.Close();

                    Global.adxState = null;
                }
                else { MessageBox.Show("CriWare tools are missing. Please restart Sonic '06 Toolkit and try again.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }

    class ARC
    {
        static ProcessStartInfo arcSession;
        public static string getLocation;
        public static string failsafeCheck; //Unpacked ARCs will have a unique directory to prevent overwriting.

        public static void Unpack(string args, string filename)
        {
            if (Global.arcState == "launch-typical")
            {
                if (!Properties.Settings.Default.unpackAndLaunch)
                {
                    //Sets up the BASIC application and executes the unpacking process.
                    var basicWrite = File.Create($"{Properties.Settings.Default.toolsPath}unpack.bat");
                    var basicSession = new UTF8Encoding(true).GetBytes($"\"{Properties.Settings.Default.unpackFile}\" \"{args}\"");
                    basicWrite.Write(basicSession, 0, basicSession.Length);
                    basicWrite.Close();

                    arcSession = new ProcessStartInfo($"{Properties.Settings.Default.toolsPath}unpack.bat")
                    {
                        WorkingDirectory = Properties.Settings.Default.toolsPath,
                        WindowStyle = ProcessWindowStyle.Hidden
                    };

                    Begin();
                }
                else { Extract(args, filename); }
            }
            else if (Global.arcState == "typical")
            {
                Extract(args, filename);
            }
        }

        static void Extract(string args, string filename)
        {
            byte[] bytes;

            if (Global.arcState == "typical") bytes = File.ReadAllBytes(filename).Take(4).ToArray();
            else bytes = File.ReadAllBytes(args).Take(4).ToArray();

            var hexString = BitConverter.ToString(bytes); hexString = hexString.Replace("-", " ");

            if (hexString != "55 AA 38 2D") MessageBox.Show("Invalid ARC file detected.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                string unpackBuildSession;
                byte[] basicSession;
                byte[] metadataSession;
                failsafeCheck = Path.GetRandomFileName();

                //Builds the main string which locates the ARC's final unpack directory.
                if (Global.arcState == "typical") { unpackBuildSession = $"{Properties.Settings.Default.archivesPath}{Global.sessionID}\\{failsafeCheck}\\{Path.GetFileNameWithoutExtension(filename)}\\"; }
                else { unpackBuildSession = $"{Properties.Settings.Default.archivesPath}{Global.sessionID}\\{failsafeCheck}\\{Path.GetFileNameWithoutExtension(args)}\\"; }

                if (!Directory.Exists(unpackBuildSession))
                {
                    Directory.CreateDirectory(unpackBuildSession);
                    getLocation = unpackBuildSession;
                }

                //Establishes the failsafe directory and copies the ARC prepare for the unpacking process.
                string arcBuildSession = $"{Properties.Settings.Default.archivesPath}{Global.sessionID}\\{failsafeCheck}\\";

                if (!Directory.Exists(arcBuildSession)) Directory.CreateDirectory(arcBuildSession);

                if (Global.arcState == "typical")
                {
                    if (File.Exists(filename)) if (Global.arcState == "typical") File.Copy(filename, $"{arcBuildSession}{Path.GetFileName(filename)}", true);
                }
                else
                {
                    if (File.Exists(args)) File.Copy(args, $"{arcBuildSession}{Path.GetFileName(args)}", true);
                }

                //Sets up the BASIC application and executes the unpacking process.
                var basicWrite = File.Create($"{Properties.Settings.Default.toolsPath}unpack.bat");

                if (Global.arcState == "typical") basicSession = new UTF8Encoding(true).GetBytes($"\"{Properties.Settings.Default.unpackFile}\" \"{arcBuildSession}{Path.GetFileName(filename)}\"");
                else basicSession = new UTF8Encoding(true).GetBytes($"\"{Properties.Settings.Default.unpackFile}\" \"{arcBuildSession}{Path.GetFileName(args)}\"");

                basicWrite.Write(basicSession, 0, basicSession.Length);
                basicWrite.Close();

                arcSession = new ProcessStartInfo($"{Properties.Settings.Default.toolsPath}unpack.bat")
                {
                    WorkingDirectory = Properties.Settings.Default.toolsPath,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                //Writes metadata to the unpacked directory to ensure the original path is remembered.
                var metadataWrite = File.Create($"{arcBuildSession}metadata.ini");

                if (Global.arcState == "typical") metadataSession = new UTF8Encoding(true).GetBytes(filename);
                else metadataSession = new UTF8Encoding(true).GetBytes(args);

                metadataWrite.Write(metadataSession, 0, metadataSession.Length);
                metadataWrite.Close();

                Begin();
            }
        }

        public static void Repack(string tabText, string repackBuildSession, string metadata)
        {
            //Sets up the BASIC application and executes the repacking process.
            var basicWrite = File.Create(Properties.Settings.Default.toolsPath + "repack.bat");
            if (tabText.Contains(".arc"))
            {
                var basicSession = new UTF8Encoding(true).GetBytes($"\"{Properties.Settings.Default.repackFile}\" \"{repackBuildSession}{Path.GetFileNameWithoutExtension(metadata)}\"");
                basicWrite.Write(basicSession, 0, basicSession.Length);
                basicWrite.Close();
            }
            else { MessageBox.Show("Please use the Repack ARC As option.", "Free Mode", MessageBoxButtons.OK, MessageBoxIcon.Information); }

            arcSession = new ProcessStartInfo($"{Properties.Settings.Default.toolsPath}repack.bat")
            {
                WorkingDirectory = Properties.Settings.Default.toolsPath,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Begin();
        }

        public static void RepackAs(string tabText, string repackBuildSession, string metadata, string destination)
        {
            //Sets up the BASIC application and executes the repacking process.
            var basicWrite = File.Create(Properties.Settings.Default.toolsPath + "repack.bat");
            if (tabText.Contains(".arc"))
            {
                var basicSession = new UTF8Encoding(true).GetBytes($"\"{Properties.Settings.Default.repackFile}\" \"{repackBuildSession}{Path.GetFileNameWithoutExtension(metadata)}\"");
                basicWrite.Write(basicSession, 0, basicSession.Length);
                basicWrite.Close();
            }
            else
            {
                if (metadata.EndsWith(@"\"))
                {
                    var basicSession = new UTF8Encoding(true).GetBytes($"\"{Properties.Settings.Default.arctoolFile}\" -i \"{metadata.Remove(metadata.Length - 1)}\" -c \"{destination}\"");
                    basicWrite.Write(basicSession, 0, basicSession.Length);
                    basicWrite.Close();
                }
                else
                {
                    var basicSession = new UTF8Encoding(true).GetBytes($"\"{Properties.Settings.Default.arctoolFile}\" -i \"{metadata}\" -c \"{destination}\"");
                    basicWrite.Write(basicSession, 0, basicSession.Length);
                    basicWrite.Close();
                }
            }

            arcSession = new ProcessStartInfo($"{Properties.Settings.Default.toolsPath}repack.bat")
            {
                WorkingDirectory = Properties.Settings.Default.toolsPath,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Begin();

            if (tabText.Contains(".arc"))
            {
                string archivePath = $"{repackBuildSession}{Path.GetFileName(metadata)}";
                if (File.Exists(archivePath)) File.Copy(archivePath, destination, true);
            }
        }

        static void Begin()
        {
                var ARC = Process.Start(arcSession);
                var unpackDialog = new Status();
                var parentLeft = Main.FormLeft + ((Main.FormWidth - unpackDialog.Width) / 2);
                var parentTop = Main.FormTop + ((Main.FormHeight - unpackDialog.Height) / 2);
                if (Global.arcState == "launch-typical") unpackDialog.StartPosition = FormStartPosition.CenterScreen;
                else unpackDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                unpackDialog.Show();
                ARC.WaitForExit();
                ARC.Close();
                unpackDialog.Close();

                Global.arcState = null;
        }
    }

    class AT3
    {
        static ProcessStartInfo at3Session;

        public static void ConvertToWAV(string args, string selectedAT3)
        {
            if (Global.at3State == "launch-at3")
            {
                at3Session = new ProcessStartInfo(Properties.Settings.Default.at3File, $"-d \"{args}\" \"{Path.GetDirectoryName(args)}\\{Path.GetFileNameWithoutExtension(args)}.wav\"")
                {
                    WorkingDirectory = Path.GetDirectoryName(args),
                    WindowStyle = ProcessWindowStyle.Hidden
                };
            }
            else if (Global.at3State == "at3")
            {
                at3Session = new ProcessStartInfo(Properties.Settings.Default.at3File, $"-d \"{Path.Combine(Global.currentPath, selectedAT3)}\" \"{Path.GetDirectoryName(Path.Combine(Global.currentPath, selectedAT3))}\\{Path.GetFileNameWithoutExtension(Path.Combine(Global.currentPath, selectedAT3))}.wav\"")
                {
                    WorkingDirectory = Global.currentPath,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
            }

            Begin();
        }

        public static void ConvertToAT3(string selectedWAV)
        {
            if (Global.at3State == "wav")
            {
                at3Session = new ProcessStartInfo(Properties.Settings.Default.at3File, $"-e {AT3_Studio.wholeLoop}{AT3_Studio.beginLoop}{AT3_Studio.startLoop}{AT3_Studio.endLoop}\"{Path.Combine(Global.currentPath, selectedWAV)}\" \"{Path.GetDirectoryName(Path.Combine(Global.currentPath, selectedWAV))}\\{Path.GetFileNameWithoutExtension(Path.Combine(Global.currentPath, selectedWAV))}.at3\"")
                {
                    WorkingDirectory = Global.currentPath,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
            }

            Begin();
        }

        static void Begin()
        {
            if (Debugger.unsafeState == true) { MessageBox.Show("SONY tools are missing. Please restart Sonic '06 Toolkit and try again.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                if (File.Exists(Properties.Settings.Default.at3File))
                {
                    var Convert = Process.Start(at3Session);
                    var convertDialog = new Status();
                    var parentLeft = Main.FormLeft + ((Main.FormWidth - convertDialog.Width) / 2);
                    var parentTop = Main.FormTop + ((Main.FormHeight - convertDialog.Height) / 2);
                    if (Global.at3State == "launch-at3") convertDialog.StartPosition = FormStartPosition.CenterScreen;
                    else convertDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                    convertDialog.Show();
                    Convert.WaitForExit();
                    Convert.Close();
                    convertDialog.Close();

                    Global.at3State = null;
                }
                else { MessageBox.Show("SONY tools are missing. Please restart Sonic '06 Toolkit and try again.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }

    class CSB
    {
        static ProcessStartInfo csbSession;

        public static void Packer(string args, string selectedCSB)
        {
            if (Global.csbState == "launch-unpack")
            {
                csbSession = new ProcessStartInfo(Properties.Settings.Default.csbFile, $"\"{args}\"")
                {
                    WorkingDirectory = Path.GetDirectoryName(args),
                    WindowStyle = ProcessWindowStyle.Hidden
                };
            }
            else if (Global.csbState == "unpack" || Global.csbState == "repack")
            {
                csbSession = new ProcessStartInfo(Properties.Settings.Default.csbFile, $"\"{Path.Combine(Global.currentPath, selectedCSB)}\"")
                {
                    WorkingDirectory = Global.currentPath,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
            }

            Begin();
        }

        static void Begin()
        {
            if (Debugger.unsafeState == true) { MessageBox.Show("SonicAudioTools are missing. Please restart Sonic '06 Toolkit and try again.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                if (File.Exists(Properties.Settings.Default.csbFile))
                {
                    var Unpack = Process.Start(csbSession);
                    var unpackDialog = new Status();
                    var parentLeft = Main.FormLeft + ((Main.FormWidth - unpackDialog.Width) / 2);
                    var parentTop = Main.FormTop + ((Main.FormHeight - unpackDialog.Height) / 2);
                    if (Global.csbState == "launch-unpack") unpackDialog.StartPosition = FormStartPosition.CenterScreen;
                    else unpackDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                    unpackDialog.Show();
                    Unpack.WaitForExit();
                    Unpack.Close();
                    unpackDialog.Close();

                    Global.csbState = null;
                }
                else { MessageBox.Show("SonicAudioTools are missing. Please restart Sonic '06 Toolkit and try again.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }

    class DDS
    {
        static ProcessStartInfo ddsSession;

        public static void Convert(string args, string selectedDDS)
        {
            if (Global.ddsState == "launch-dds")
            {
                //Sets up the BASIC application and executes the converting process.
                ddsSession = new ProcessStartInfo(Properties.Settings.Default.directXFile, $"\"{args}\" -ft PNG{DDS_Studio.useGPU} -singleproc{DDS_Studio.forceDirectX10} -f R8G8B8A8_UNORM")
                {
                    WorkingDirectory = Global.currentPath,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
            }
            else if (Global.ddsState == "dds")
            {
                //Sets up the BASIC application and executes the converting process.
                ddsSession = new ProcessStartInfo(Properties.Settings.Default.directXFile, $"\"{Path.Combine(Global.currentPath, selectedDDS)}\" -ft PNG{DDS_Studio.useGPU} -singleproc{DDS_Studio.forceDirectX10} -f R8G8B8A8_UNORM")
                {
                    WorkingDirectory = Global.currentPath,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
            }

            Begin();
        }

        static void Begin()
        {
            if (Debugger.unsafeState == true) { MessageBox.Show("DirectX files are missing. Please restart Sonic '06 Toolkit and try again.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                if (File.Exists(Properties.Settings.Default.directXFile))
                {
                    var Convert = Process.Start(ddsSession);
                    var convertDialog = new Status();
                    var parentLeft = Main.FormLeft + ((Main.FormWidth - convertDialog.Width) / 2);
                    var parentTop = Main.FormTop + ((Main.FormHeight - convertDialog.Height) / 2);
                    if (Global.ddsState == "launch-dds") convertDialog.StartPosition = FormStartPosition.CenterScreen;
                    else convertDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                    convertDialog.Show();
                    Convert.WaitForExit();
                    Convert.Close();
                    convertDialog.Close();

                    Global.ddsState = null;
                }
                else { MessageBox.Show("DirectX files are missing. Please restart Sonic '06 Toolkit and try again.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }

    class LUB
    {
        static ProcessStartInfo lubSession;
        static string failsafeCheck;

        public static void WriteDecompiler()
        {
            //Gets the failsafe directory.
            if (!Directory.Exists($"{Properties.Settings.Default.unlubPath}{Global.sessionID}")) Directory.CreateDirectory($"{Properties.Settings.Default.unlubPath}{Global.sessionID}");
            if (Global.lubState == "decompile" || Global.lubState == "decompile-all") failsafeCheck = File.ReadAllText($"{Properties.Settings.Default.archivesPath}{Global.sessionID}\\{Global.getIndex}");
            else failsafeCheck = Path.GetRandomFileName();

            //Writes the decompiler to the failsafe directory to ensure any LUBs left over from other open archives aren't copied over to the selected archive.
            if (!Directory.Exists($"{Properties.Settings.Default.unlubPath}{Global.sessionID}\\{failsafeCheck}")) Directory.CreateDirectory($"{Properties.Settings.Default.unlubPath}{Global.sessionID}\\{failsafeCheck}");
            if (!Directory.Exists($"{Properties.Settings.Default.unlubPath}{Global.sessionID}\\{failsafeCheck}\\lubs")) Directory.CreateDirectory($"{Properties.Settings.Default.unlubPath}{Global.sessionID}\\{failsafeCheck}\\lubs");
            if (!File.Exists($"{Properties.Settings.Default.unlubPath}{Global.sessionID}\\{failsafeCheck}\\unlub.jar")) File.WriteAllBytes($"{Properties.Settings.Default.unlubPath}{Global.sessionID}\\{failsafeCheck}\\unlub.jar", Properties.Resources.unlub);
            if (!File.Exists($"{Properties.Settings.Default.unlubPath}{Global.sessionID}\\{failsafeCheck}\\unlub.bat"))
            {
                var decompilerWrite = File.Create($"{Properties.Settings.Default.unlubPath}{Global.sessionID}\\{failsafeCheck}\\unlub.bat");
                var decompilerText = new UTF8Encoding(true).GetBytes("cd \".\\lubs\"\nfor /r %%i in (*.lub) do java -jar ..\\unlub.jar \"%%~dpni.lub\" > \"%%~dpni.lua\"\nxcopy \".\\*.lua\" \"..\\luas\" /y /i\ndel \".\\*.lua\" /q\n@ECHO OFF\n:delete\ndel /q /f *.lub\n@ECHO OFF\n:rename\ncd \"..\\luas\"\nrename \"*.lua\" \"*.lub\"\nexit");
                decompilerWrite.Write(decompilerText, 0, decompilerText.Length);
                decompilerWrite.Close();
            }
        }

        public static void Decompile(string args, string LUB)
        {
            WriteDecompiler();

            //Checks if the first file to be processed is blacklisted. If so, abort the operation to ensure the file doesn't get corrupted.
            if (Global.lubState == "launch-decompile")
            {
                if (Path.GetFileName(args) == "standard.lub")
                {
                    MessageBox.Show("File: standard.lub\n\nThis file cannot be decompiled; attempts to do so will render the file unusable.", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else if (Global.lubState == "decompile")
            {
                if (Path.GetFileName(LUB) == "standard.lub")
                {
                    MessageBox.Show("File: standard.lub\n\nThis file cannot be decompiled; attempts to do so will render the file unusable.", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else if (Global.lubState == "decompile-all")
            {
                if (File.Exists($"{Global.currentPath}standard.lub"))
                {
                    MessageBox.Show("File: standard.lub\n\nThis file cannot be decompiled; attempts to do so will render the file unusable.", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (Global.lubState == "launch-decompile")
            {
                //Checks if any of the blacklisted files are present. If so, warn the user about modifying the files.
                if (Path.GetFileName(args) == "render_shadowmap.lub") MessageBox.Show("File: render_shadowmap.lub\n\nEditing this file may render this archive unusable. Please edit this at your own risk!", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (Path.GetFileName(args) == "game.lub") MessageBox.Show("File: game.lub\n\nEditing this file may render it unusable. Please edit this at your own risk!", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (Path.GetFileName(args) == "object.lub") MessageBox.Show("File: object.lub\n\nEditing this file may render it unusable. Please edit this at your own risk!", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //Checks the header for each file to ensure that it can be safely decompiled.
                if (File.Exists(args))
                {
                    if (File.ReadAllLines(args)[0].Contains("LuaP"))
                    {
                        File.Copy(args, Path.Combine($"{Properties.Settings.Default.unlubPath}{Global.sessionID}\\{failsafeCheck}\\lubs\\", Path.GetFileName(args)), true);
                    }
                    else { return; }
                }
            }
            else if (Global.lubState == "decompile")
            {
                //Checks if any of the blacklisted files are present. If so, warn the user about modifying the files.
                if (Path.GetFileName(LUB) == "render_shadowmap.lub") MessageBox.Show("File: render_shadowmap.lub\n\nEditing this file may render this archive unusable. Please edit this at your own risk!", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (Path.GetFileName(LUB) == "game.lub") MessageBox.Show("File: game.lub\n\nEditing this file may render it unusable. Please edit this at your own risk!", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (Path.GetFileName(LUB) == "object.lub") MessageBox.Show("File: object.lub\n\nEditing this file may render it unusable. Please edit this at your own risk!", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                File.Copy(Path.Combine(Global.currentPath, Path.GetFileName(LUB)), Path.Combine($"{Properties.Settings.Default.unlubPath}{Global.sessionID}\\{failsafeCheck}\\lubs\\", Path.GetFileName(LUB)), true);
            }
            else if (Global.lubState == "decompile-all")
            {
                //Checks if any of the blacklisted files are present. If so, warn the user about modifying the files.
                if (File.Exists($"{Global.currentPath}render_shadowmap.lub")) MessageBox.Show("File: render_shadowmap.lub\n\nEditing this file may render this archive unusable. Please edit this at your own risk!", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (File.Exists($"{Global.currentPath}game.lub")) MessageBox.Show("File: game.lub\n\nEditing this file may render it unusable. Please edit this at your own risk!", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (File.Exists($"{Global.currentPath}object.lub")) MessageBox.Show("File: object.lub\n\nEditing this file may render it unusable. Please edit this at your own risk!", "Blacklisted file detected!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //Checks the header for each file to ensure that it can be safely decompiled.
                foreach (string listLUBs in Directory.GetFiles(Tools.Global.currentPath, "*.lub", SearchOption.TopDirectoryOnly))
                {
                    if (File.Exists(listLUBs))
                    {
                        if (File.ReadAllLines(listLUBs)[0].Contains("LuaP"))
                        {
                            File.Copy(listLUBs, Path.Combine($"{Properties.Settings.Default.unlubPath}{Global.sessionID}\\{failsafeCheck}\\lubs\\", Path.GetFileName(listLUBs)), true);
                        }
                    }
                }
            }

            lubSession = new ProcessStartInfo($"{Properties.Settings.Default.unlubPath}{Global.sessionID}\\{failsafeCheck}\\unlub.bat")
            {
                WorkingDirectory = $"{Properties.Settings.Default.unlubPath}{Global.sessionID}\\{failsafeCheck}",
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Begin(args);
        }

        static void Begin(string args)
        {
            if (Debugger.unsafeState == true) { MessageBox.Show("unlub files are missing. Please restart LUB Studio and try again.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                if (File.Exists($"{Properties.Settings.Default.unlubPath}{Global.sessionID}\\{failsafeCheck}\\unlub.jar"))
                {
                    var Decompile = Process.Start(lubSession);
                    var decompileDialog = new Status();
                    var parentLeft = Main.FormLeft + ((Main.FormWidth - decompileDialog.Width) / 2);
                    var parentTop = Main.FormTop + ((Main.FormHeight - decompileDialog.Height) / 2);
                    if (Global.lubState == "launch-decompile") decompileDialog.StartPosition = FormStartPosition.CenterScreen;
                    else decompileDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                    decompileDialog.Show();
                    Decompile.WaitForExit();
                    Decompile.Close();

                    if (Global.lubState == "launch-decompile")
                    {
                        //Copies all LUBs to the final directory, then erases leftovers.
                        foreach (string LUB in Directory.GetFiles($"{Properties.Settings.Default.unlubPath}{Global.sessionID}\\{failsafeCheck}\\luas\\", "*.lub", SearchOption.TopDirectoryOnly))
                        {
                            if (File.Exists(LUB))
                            {
                                File.Copy(Path.Combine($"{Properties.Settings.Default.unlubPath}{Global.sessionID}\\{failsafeCheck}\\luas\\", Path.GetFileName(LUB)), args, true);
                                File.Delete(LUB);
                            }
                        }
                    }
                    else if (Global.lubState == "decompile")
                    {
                        //Copies all LUBs to the final directory, then erases leftovers.
                        foreach (string LUB in Directory.GetFiles($"{Properties.Settings.Default.unlubPath}{Global.sessionID}\\{failsafeCheck}\\luas\\", "*.lub", SearchOption.TopDirectoryOnly))
                        {
                            if (File.Exists(LUB))
                            {
                                File.Copy(Path.Combine($"{Properties.Settings.Default.unlubPath}{Global.sessionID}\\{failsafeCheck}\\luas\\", Path.GetFileName(LUB)), Path.Combine(Global.currentPath, Path.GetFileName(LUB)), true);
                                File.Delete(LUB);
                            }
                        }
                    }
                    else if (Global.lubState == "decompile-all")
                    {
                        //Copies all LUBs to the final directory, then erases leftovers.
                        foreach (string LUB in Directory.GetFiles($"{Properties.Settings.Default.unlubPath}{Global.sessionID}\\{failsafeCheck}\\luas\\", "*.lub", SearchOption.TopDirectoryOnly))
                        {
                            if (File.Exists(LUB))
                            {
                                File.Copy(Path.Combine($"{Properties.Settings.Default.unlubPath}{Global.sessionID}\\{failsafeCheck}\\luas\\", Path.GetFileName(LUB)), Path.Combine(Global.currentPath, Path.GetFileName(LUB)), true);
                                File.Delete(LUB);
                            }
                        }
                    }

                    decompileDialog.Close();

                    Global.lubState = null;
                }
                else { MessageBox.Show("unlub files are missing. Please restart LUB Studio and try again.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }

    class MST
    {
        static ProcessStartInfo mstSession;

        public static void Export(string args, string selectedMST)
        {
            if (Global.mstState == "launch-mst")
            {
                //Sets up the BASIC application and executes the converting process.
                mstSession = new ProcessStartInfo(Properties.Settings.Default.mstFile, $"\"{args}\"")
                {
                    WorkingDirectory = Path.GetDirectoryName(args),
                    WindowStyle = ProcessWindowStyle.Hidden
                };
            }
            else if (Global.mstState == "mst")
            {
                //Sets up the BASIC application and executes the converting process.
                mstSession = new ProcessStartInfo(Properties.Settings.Default.mstFile, $"\"{Path.Combine(Global.currentPath, selectedMST)}\"")
                {
                    WorkingDirectory = Global.currentPath,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
            }

            Begin(string.Empty);
        }

        public static void Import(string args, string selectedXML)
        {
            if (Global.mstState == "launch-xml")
            {
                //Sets up the BASIC application and executes the converting process.
                mstSession = new ProcessStartInfo(Properties.Settings.Default.mstFile, $"\"{args}\"")
                {
                    WorkingDirectory = Path.GetDirectoryName(args),
                    WindowStyle = ProcessWindowStyle.Hidden
                };
            }
            else if (Global.mstState == "xml")
            {
                //Sets up the BASIC application and executes the converting process.
                mstSession = new ProcessStartInfo(Properties.Settings.Default.mstFile, $"\"{Path.Combine(Global.currentPath, selectedXML)}\"")
                {
                    WorkingDirectory = Global.currentPath,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
            }

            Begin(selectedXML);
        }

        static void Begin(string selectedXML)
        {
            if (Debugger.unsafeState == true) { MessageBox.Show("mst06 files are missing. Please restart Sonic '06 Toolkit and try again.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                if (File.Exists(Properties.Settings.Default.mstFile))
                {
                    var Decode = Process.Start(mstSession);
                    Decode.WaitForExit();
                    Decode.Close();

                    Global.mstState = null;
                }
                else { MessageBox.Show("mst06 files are missing. Please restart Sonic '06 Toolkit and try again.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }

    class PNG
    {
        static ProcessStartInfo pngSession;

        public static void Convert(string args, string selectedPNG)
        {
            if (Global.ddsState == "launch-png")
            {
                //Sets up the BASIC application and executes the converting process.
                pngSession = new ProcessStartInfo(Properties.Settings.Default.directXFile, $"\"{args}\" -ft DDS{DDS_Studio.useGPU} -singleproc{DDS_Studio.forceDirectX10} -f R8G8B8A8_UNORM")
                {
                    WorkingDirectory = Global.currentPath,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
            }
            else if (Global.ddsState == "png")
            {
                //Sets up the BASIC application and executes the converting process.
                pngSession = new ProcessStartInfo(Properties.Settings.Default.directXFile, $"\"{Path.Combine(Global.currentPath, selectedPNG)}\" -ft DDS{DDS_Studio.useGPU} -singleproc{DDS_Studio.forceDirectX10} -f R8G8B8A8_UNORM")
                {
                    WorkingDirectory = Global.currentPath,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
            }

            Begin();
        }

        static void Begin()
        {
            if (Debugger.unsafeState == true) { MessageBox.Show("DirectX files are missing. Please restart Sonic '06 Toolkit and try again.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                if (File.Exists(Properties.Settings.Default.directXFile))
                {
                    var Convert = Process.Start(pngSession);
                    var convertDialog = new Status();
                    var parentLeft = Main.FormLeft + ((Main.FormWidth - convertDialog.Width) / 2);
                    var parentTop = Main.FormTop + ((Main.FormHeight - convertDialog.Height) / 2);
                    if (Global.ddsState == "launch-png") convertDialog.StartPosition = FormStartPosition.CenterScreen;
                    else convertDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                    convertDialog.Show();
                    Convert.WaitForExit();
                    Convert.Close();
                    convertDialog.Close();

                    Global.ddsState = null;
                }
                else { MessageBox.Show("DirectX files are missing. Please restart Sonic '06 Toolkit and try again.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }

    class SET
    {
        public static void Export(string args, string SET)
        {
            if (Global.setState == "launch-export")
            {
                if (File.Exists(args))
                {
                    var readSET = new S06SetData();
                    readSET.Load(args);
                    readSET.ExportXML($"{Path.GetFileNameWithoutExtension(args)}.xml");
                }
            }
            else if (Global.setState == "export")
            {
                if (File.Exists($"{Global.currentPath}{Path.GetFileName(SET)}"))
                {
                    var readSET = new S06SetData();
                    readSET.Load($"{Global.currentPath}{Path.GetFileName(SET)}");
                    readSET.ExportXML($"{Global.currentPath}{Path.GetFileNameWithoutExtension(SET)}.xml");
                }
            }
        }

        public static void Import(string XML)
        {
            if (Properties.Settings.Default.backupSET == true) if (File.Exists($"{Global.currentPath}{Path.GetFileNameWithoutExtension(XML)}.set")) File.Copy($"{Global.currentPath}{Path.GetFileNameWithoutExtension(XML)}.set", $"{Global.currentPath}{Path.GetFileNameWithoutExtension(XML)}.set.bak", true);

            if (File.Exists($"{Global.currentPath}{Path.GetFileNameWithoutExtension(XML)}.set")) File.Delete($"{Global.currentPath}{Path.GetFileNameWithoutExtension(XML)}.set");

            var readXML = new S06SetData();
            readXML.ImportXML($"{Global.currentPath}{XML}");
            readXML.Save($"{Global.currentPath}{Path.GetFileNameWithoutExtension(XML)}.set");

            if (Properties.Settings.Default.deleteXML == true) if (File.Exists($"{Global.currentPath}{XML}")) File.Delete($"{Global.currentPath}{XML}");
        }
    }

    class XNO
    {
        static ProcessStartInfo xnoSession;
        static string failsafeCheck;

        static void WriteConverter()
        {
            #region Getting current ARC failsafe...
            //Gets the failsafe directory.
            if (!Directory.Exists($"{Properties.Settings.Default.unlubPath}{Global.sessionID}")) Directory.CreateDirectory($"{Properties.Settings.Default.unlubPath}{Global.sessionID}");
            if (Global.xnoState == "xno") failsafeCheck = File.ReadAllText($"{Properties.Settings.Default.archivesPath}{Global.sessionID}\\{Global.getIndex}");
            else failsafeCheck = Path.GetRandomFileName();
            #endregion

            #region Writing converter...
            //Writes the decompiler to the failsafe directory to ensure any XNOs left over from other open archives aren't copied over to the selected archive.
            if (!Directory.Exists($"{Properties.Settings.Default.xnoPath}{Global.sessionID}\\{failsafeCheck}")) Directory.CreateDirectory($"{Properties.Settings.Default.xnoPath}{Global.sessionID}\\{failsafeCheck}");
            if (!File.Exists($"{Properties.Settings.Default.xnoPath}{Global.sessionID}\\{failsafeCheck}\\xno2dae.exe")) File.WriteAllBytes($"{Properties.Settings.Default.xnoPath}{Global.sessionID}\\{failsafeCheck}\\xno2dae.exe", Properties.Resources.xno2dae);
            #endregion
        }

        public static void Convert(string args, string selectedXNO)
        {
            WriteConverter();

            string checkedBuildSession = $"{Properties.Settings.Default.xnoPath}{Global.sessionID}\\{failsafeCheck}\\xno2dae.exe";
            var checkedWrite = File.Create($"{Properties.Settings.Default.xnoPath}{Global.sessionID}\\{failsafeCheck}\\xno2dae.bat");

            if (Global.xnoState == "xno")
            {
                var checkedText = new UTF8Encoding(true).GetBytes($"\"{checkedBuildSession}\" \"{selectedXNO}\"");
                checkedWrite.Write(checkedText, 0, checkedText.Length);
                checkedWrite.Close();
            }
            else
            {
                var checkedText = new UTF8Encoding(true).GetBytes($"\"{checkedBuildSession}\" \"{args}\"");
                checkedWrite.Write(checkedText, 0, checkedText.Length);
                checkedWrite.Close();
            }

            //Sets up the BASIC application and executes the conversion process.
            xnoSession = new ProcessStartInfo($"{Properties.Settings.Default.xnoPath}{Global.sessionID}\\{failsafeCheck}\\xno2dae.bat")
            {
                WorkingDirectory = Global.currentPath,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Begin();
        }

        public static void Animate(string selectedXNO, string selectedXNM)
        {
            WriteConverter();

            string checkedBuildSession = $"{Properties.Settings.Default.xnoPath}{Global.sessionID}\\{failsafeCheck}\\xno2dae.exe";
            var checkedWrite = File.Create($"{Properties.Settings.Default.xnoPath}{Global.sessionID}\\{failsafeCheck}\\xno2dae.bat");

            var checkedText = new UTF8Encoding(true).GetBytes($"\"{checkedBuildSession}\" \"{selectedXNO}\" \"{selectedXNM}\"");
            checkedWrite.Write(checkedText, 0, checkedText.Length);
            checkedWrite.Close();

            //Sets up the BASIC application and executes the conversion process.
            xnoSession = new ProcessStartInfo($"{Properties.Settings.Default.xnoPath}{Global.sessionID}\\{failsafeCheck}\\xno2dae.bat")
            {
                WorkingDirectory = Global.currentPath,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Begin();
        }

        static void Begin()
        {
            if (Debugger.unsafeState == true) { MessageBox.Show("xno2dae files are missing. Please restart Sonic '06 Toolkit and try again.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else
            {
                if (File.Exists(Properties.Settings.Default.xnoFile) || Debugger.unsafeState == false)
                {
                    var Convert = Process.Start(xnoSession);
                    var convertDialog = new Status();
                    var parentLeft = Main.FormLeft + ((Main.FormWidth - convertDialog.Width) / 2);
                    var parentTop = Main.FormTop + ((Main.FormHeight - convertDialog.Height) / 2);
                    if (Global.xnoState == "launch-xno") convertDialog.StartPosition = FormStartPosition.CenterScreen;
                    else convertDialog.Location = new System.Drawing.Point(parentLeft, parentTop);
                    convertDialog.Show();
                    Convert.WaitForExit();
                    Convert.Close();
                    convertDialog.Close();

                    Global.xnoState = null;
                }
                else { MessageBox.Show("xno2dae files are missing. Please restart Sonic '06 Toolkit and try again.", "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }

    class Notification
    {
        public static void Dispose()
        {
            Status statusForm = Application.OpenForms["Status"] != null ? (Status)Application.OpenForms["Status"] : null;

            if (statusForm != null)
            {
                try
                {
                    statusForm = (Status)Application.OpenForms["Status"];
                    statusForm.Close();
                }
                catch { }
            }
        }
    }

    public class TimedWebClient : WebClient
    {
        public int Timeout { get; set; }

        public TimedWebClient()
        {
            Timeout = 100000;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var objWebRequest = base.GetWebRequest(address);
            objWebRequest.Timeout = Timeout;
            return objWebRequest;
        }
    }

    public class Global
    {
        public static string versionNumber = "1.95";
        public static string latestVersion = "Version " + versionNumber;
        public static string serverStatus;
        public static string currentPath;
        public static string updateState;
        public static string exisoState;
        public static string arcState;
        public static string adxState;
        public static string at3State;
        public static string csbState;
        public static string ddsState;
        public static string lubState;
        public static string setState;
        public static string mstState;
        public static string xnoState;

        public static string applicationData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        public static int sessionID;
        public static int getIndex;

        public static bool javaCheck = true;
        public static bool gameChanged = false;
    }
}
