using System;
using System.IO;
using System.Linq;
using NAudio.Wave;
using SonicAudioLib.IO;
using SonicAudioLib.CriMw;
using System.Globalization;
using SonicAudioLib.Archives;
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

// SonicAudioLib is licensed under the MIT License:
/*
 * MIT License

 * Copyright (c) 2019 Skyth

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
    class CSBTools
    {
        public static void ExtractCSBNodes(DataExtractor extractor, string inputDirectory, string outputDirectory) {
            CriCpkArchive cpkArchive = null;
            string cpkPath = $"{outputDirectory}.cpk";
            bool found = File.Exists(cpkPath);

            //This should fix "File not found" error in case-sensitive file systems.
            //Add new extensions when necessary.
            foreach (string extension in new string[] { "cpk", "CPK" }) {
                if (found) break;
                cpkPath = $"{outputDirectory}.{extension}";
                found = File.Exists(cpkPath);
            }

            using (CriTableReader reader = CriTableReader.Create(inputDirectory)) {
                while (reader.Read()) {
                    if (reader.GetString("name") == "SOUND_ELEMENT") {
                        long tablePosition = reader.GetPosition("utf");
                        using (CriTableReader sdlReader = CriTableReader.Create(reader.GetSubStream("utf"))) {
                            while (sdlReader.Read()) {
                                if (sdlReader.GetByte("fmt") != 0)
                                    throw new Exception("The given CSB file contains an audio file which is not an ADX. Only CSB files with ADXs are supported.");

                                bool streaming = sdlReader.GetBoolean("stmflg");
                                if (streaming && !found)
                                    throw new Exception("Cannot find the external .CPK file for this .CSB file. Please ensure that the external .CPK file is stored in the directory where the .CPK file is.");
                                else if (streaming && found && cpkArchive == null) {
                                    cpkArchive = new CriCpkArchive();
                                    cpkArchive.Load(cpkPath, 4096);
                                }

                                string sdlName = sdlReader.GetString("name");
                                DirectoryInfo destinationPath = new DirectoryInfo(Path.Combine(outputDirectory, sdlName));
                                destinationPath.Create();

                                CriAaxArchive aaxArchive = new CriAaxArchive();

                                if (streaming) {
                                    CriCpkEntry cpkEntry = cpkArchive.GetByPath(sdlName);

                                    if (cpkEntry != null) {
                                        using (Stream cpkSource = File.OpenRead(cpkPath))
                                        using (Stream aaxSource = cpkEntry.Open(cpkSource)) {
                                            aaxArchive.Read(aaxSource);
                                            foreach (CriAaxEntry entry in aaxArchive) {
                                                extractor.Add(cpkPath,
                                                    Path.Combine(destinationPath.FullName,
                                                    entry.Flag == CriAaxEntryFlag.Intro ? "Intro.adx" : "Loop.adx"),
                                                    cpkEntry.Position + entry.Position, entry.Length);
                                            }
                                        }
                                    }
                                } else {
                                    long aaxPosition = sdlReader.GetPosition("data");
                                    using (Stream aaxSource = sdlReader.GetSubStream("data")) {
                                        aaxArchive.Read(aaxSource);
                                        foreach (CriAaxEntry entry in aaxArchive) {
                                            extractor.Add(inputDirectory,
                                                Path.Combine(destinationPath.FullName,
                                                entry.Flag == CriAaxEntryFlag.Intro ? "Intro.adx" : "Loop.adx"),
                                                tablePosition + aaxPosition + entry.Position, entry.Length);
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
            }
        }

        public static void WriteCSB(string inputDirectory) {
            string baseDirectory = Path.GetDirectoryName(inputDirectory);
            string csbPath = $"{inputDirectory}.csb";

            foreach (string extension in new string[] { "csb", "CSB" }) {
                if (File.Exists(csbPath)) break;
                csbPath = $"{inputDirectory}.{extension}";
            }

            if (!File.Exists(csbPath))
                throw new Exception("Cannot find the .CSB file for this directory. Please ensure that the .CSB file is stored in the directory where this directory is.");

            CriCpkArchive cpkArchive = new CriCpkArchive();

            CriTable csbFile = new CriTable();
            csbFile.Load(csbPath, 4096);

            CriRow soundElementRow = csbFile.Rows.First(row => (string)row["name"] == "SOUND_ELEMENT");

            CriTable soundElementTable = new CriTable();
            soundElementTable.Load((byte[])soundElementRow["utf"]);

            List<FileInfo> junks = new List<FileInfo>();

            foreach (CriRow sdlRow in soundElementTable.Rows) {
                string sdlName = (string)sdlRow["name"];

                DirectoryInfo sdlDirectory = new DirectoryInfo(Path.Combine(inputDirectory, sdlName));

                if (!sdlDirectory.Exists)
                    throw new Exception($"Cannot find sound element directory for replacement.\nPath attempt: {sdlDirectory.FullName}");

                bool streaming = (byte)sdlRow["stmflg"] != 0;
                uint sampleRate = (uint)sdlRow["sfreq"];
                byte numberChannels = (byte)sdlRow["nch"];

                CriAaxArchive aaxArchive = new CriAaxArchive();
                foreach (FileInfo file in sdlDirectory.GetFiles("*.adx")) {
                    CriAaxEntry entry = new CriAaxEntry();
                    if (file.Name.ToLower(CultureInfo.GetCultureInfo("en-US")) == "intro.adx") {
                        entry.Flag = CriAaxEntryFlag.Intro;
                        entry.FilePath = file;
                        aaxArchive.Add(entry);

                        ReadADX(file, out sampleRate, out numberChannels);
                    } else if (file.Name.ToLower(CultureInfo.GetCultureInfo("en-US")) == "loop.adx") {
                        entry.Flag = CriAaxEntryFlag.Loop;
                        entry.FilePath = file;
                        aaxArchive.Add(entry);

                        ReadADX(file, out sampleRate, out numberChannels);
                    }
                }

                if (streaming) {
                    CriCpkEntry entry = new CriCpkEntry();
                    entry.Name = Path.GetFileName(sdlName);
                    entry.DirectoryName = Path.GetDirectoryName(sdlName);
                    entry.Id = (uint)cpkArchive.Count;
                    entry.FilePath = new FileInfo(Path.GetTempFileName());
                    junks.Add(entry.FilePath);

                    cpkArchive.Add(entry);
                    aaxArchive.Save(entry.FilePath.FullName, 4096);
                } else {
                    sdlRow["data"] = aaxArchive.Save();
                }

                sdlRow["sfreq"] = sampleRate;
                sdlRow["nch"] = numberChannels;
            }

            soundElementTable.WriterSettings = CriTableWriterSettings.AdxSettings;
            soundElementRow["utf"] = soundElementTable.Save();

            csbFile.WriterSettings = CriTableWriterSettings.AdxSettings;
            csbFile.Save(csbPath, 4096);

            if (cpkArchive.Count > 0) {
                string cpkPath = $"{inputDirectory}.cpk";
                foreach (string extension in new string[] { "cpk", "CPK" }) {
                    if (File.Exists($"{inputDirectory}.{extension}")) {
                        cpkPath = $"{inputDirectory}.{extension}";
                        break;
                    }
                }

                cpkArchive.Save(cpkPath, 4096);
            }

            foreach (FileInfo junk in junks)
                junk.Delete();
        }

        public static void ReadADX(FileInfo fileInfo, out uint sampleRate, out byte numberChannels) {
            using (Stream source = fileInfo.OpenRead()) {
                source.Seek(7, SeekOrigin.Begin);
                numberChannels = DataStream.ReadByte(source);
                sampleRate = DataStream.ReadUInt32BE(source);
            }
        }
    }

    class MP3
    {
        public static string CreateTemporaryWAV(string input) {
            string tempPath = Path.GetTempPath();
            string tempFile = Path.GetRandomFileName();
            using (Mp3FileReader mp3 = new Mp3FileReader(input))
                using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
                    WaveFileWriter.CreateWaveFile(Path.Combine(tempPath, $"{tempFile}.wav"), pcm);
            return Path.Combine(tempPath, $"{tempFile}.wav");
        }
    }
}
