using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace XISOExtractorGUI
{
    public partial class XisoExtractor
    {
        private Toolkit.EnvironmentX.Main mainForm = null;
        private const uint ReadWriteBuffer = 0x200000;
        private static readonly Encoding Enc = Encoding.GetEncoding(1252);
        internal static bool Abort;
        private static long _baseOffset, _totalSize, _totalProcessed, _multiProcessed;
        internal static long MultiSize;
        private static int _errorlevel;

        internal static event EventHandler<EventArg<string>> Operation;

        internal static event EventHandler<EventArg<string>> Status;

        internal static event EventHandler<EventArg<double>> TotalProgress;

        internal static event EventHandler<EventArg<double>> IsoProgress;

        internal static event EventHandler<EventArg<double, long>> FileProgress;

        private static string GetErrorString(int error)
        {
            switch (error)
            {
                case 0:
                    return "";
                case -1:
                    return "ERROR While verifying Xbox ISO: File to small for the type expected";
                case -2:
                    return "ERROR while verifying Xbox ISO: Not a valid Xbox ISO (could also be unsupported)";
                case 1:
                    return "ERROR while trying to get root sector offset: File to small";
                case 2:
                    return "ERROR while parsing root FS: File to small";
                case 3:
                    return "ERROR while parsing root FS: Invalid TOC Entry Detected";
                case 4:
                    return "ERROR while extracting: File to small";
                case 5:
                    return "ERROR while extracting: Not enough space on harddrive";
                default:
                    return "Unkown Error";
            }
        }

        internal static string GetLastError() { return GetErrorString(_errorlevel); }

        internal bool GetFileListAndSize(XisoOptions opts, out XisoListAndSize retval, out BinaryReader br)
        {
            Abort = false;
            if (opts.GenerateSfv)
                opts.SfvGen = new SfvGenerator(Path.Combine(Path.GetDirectoryName(opts.Source), Path.GetFileNameWithoutExtension(opts.Source)) + ".sfv");
            retval = new XisoListAndSize();
            br = null;
            _errorlevel = 1;
            if (!VerifyXiso(opts.Source))
                return false;
            br = new BinaryReader(File.Open(opts.Source, FileMode.Open, FileAccess.Read, FileShare.Read));
            if (!BinarySeek(ref br, ((_baseOffset + 32) * 2048) + 0x14, 8))
                return false;
            var data = br.ReadBytes(8);
            UpdateOperation("Grabbing Root sector & offset...");
            uint rootsector;
            _errorlevel = int.MaxValue;
            if (!EndianConverter.Little32(ref data, out rootsector))
                return false;
            uint rootsize;
            if (!EndianConverter.Little32(ref data, out rootsize, 4))
                return false;
            UpdateStatus(string.Format("Root sector: {0} (0x{0:X}) Root size: {1} (0x{1:X})", rootsector, rootsize));
            UpdateOperation("Parsing Game Partition FS Table...");
            Parse(ref br, ref retval.List, 0, 0, rootsector);
            _totalProcessed = 0;
            _totalSize = 0;
            var msg = "";
            var newlist = new List<XisoTableData>();
            foreach (var entry in retval.List)
            {
                if (opts.ExcludeSysUpdate && entry.Path.IndexOf("$SystemUpdate", StringComparison.CurrentCultureIgnoreCase) != -1 ||
                   entry.Name.IndexOf("$SystemUpdate", StringComparison.CurrentCultureIgnoreCase) != -1)
                    continue;
                if (entry.IsFile)
                {
                    if (opts.GenerateFileList)
                        msg += string.Format("{0}{1} [Offset: 0x{2:X} Size: {3}]{4}", entry.Path, entry.Name, entry.Offset, Utils.GetSizeReadable(entry.Size), Environment.NewLine);
                    _totalSize += entry.Size;
                    retval.Files++;
                }
                else
                {
                    if (opts.GenerateFileList)
                        msg += string.Format("{0}{1}\\{2}", entry.Path, entry.Name, Environment.NewLine);
                    retval.Folders++;
                }
                newlist.Add(entry);
            }
            retval.List = newlist;
            UpdateStatus(string.Format("Parsing Game Partition FS Table done! Total entries found: {0} Files: {1} Folders: {2} Total File size: {3}", retval.List.Count, retval.Files, retval.Folders,
                                       Utils.GetSizeReadable(_totalSize)));
            if (opts.GenerateFileList)
            {
                msg = string.Format("Total entries: {0}{4}Folders: {1}{4}Files: {2}{4}Total Filesize: {5}{4}{3}", retval.List.Count, retval.Folders, retval.Files, msg, Environment.NewLine,
                                    Utils.GetSizeReadable(_totalSize));
                File.WriteAllText(string.Format("{0}\\{1}.txt", Path.GetDirectoryName(opts.Source), Path.GetFileNameWithoutExtension(opts.Source)), msg);
            }
            if (string.IsNullOrEmpty(opts.Target))
                opts.Target = string.Format("{0}\\{1}", Path.GetDirectoryName(opts.Source), Path.GetFileNameWithoutExtension(opts.Source));
            if (opts.Target.EndsWith("\\", StringComparison.Ordinal))
                opts.Target = opts.Target.Substring(0, opts.Target.Length - 1);
            retval.Size = _totalSize;
            return true;
        }

        internal bool ExtractXiso(XisoOptions opts)
        {
            BinaryReader br = null;
            try
            {
                Abort = false;
                var sw = new Stopwatch();
                sw.Start();
                XisoListAndSize retval;
                UpdateStatus(string.Format("Extracting {0}", opts.Source));
                if (!GetFileListAndSize(opts, out retval, out br))
                    return false;
                _errorlevel = 0;
                if (ExtractXiso(opts, retval, ref br))
                {
                    if (opts.DeleteIsoOnCompletion)
                    {
                        br.Close();
                        File.Delete(opts.Source);
                    }
                    sw.Stop();
                    UpdateStatus(string.Format("Successfully extracted {0} Files in {1} Folders with a total size of {2}", retval.Files, retval.Folders, Utils.GetSizeReadable(_totalSize)));
                    UpdateOperation(string.Format("Completed extraction after {0:F0} Minute(s) and {1} Seconds", sw.Elapsed.TotalMinutes, sw.Elapsed.Seconds));
                    return true;
                }
                sw.Stop();
                if (Abort)
                {
                    UpdateStatus("Aborted by user");
                    UpdateOperation(string.Format("Aborted extraction after {0:F0} Minute(s) and {1} Seconds", sw.Elapsed.TotalMinutes, sw.Elapsed.Seconds));
                }

                else if (_errorlevel != 5)
                {
                    UpdateStatus("Extraction failed!");
                    UpdateOperation(string.Format("Extraction failed after {0:F0} Minute(s) and {1} Seconds", sw.Elapsed.TotalMinutes, sw.Elapsed.Seconds));
                }
                return false;
            }
            finally
            {
                if (br != null)
                    br.Close();
            }
        }

        internal bool ExtractXiso(XisoOptions opts, XisoListAndSize retval)
        {
            var br = new BinaryReader(File.Open(opts.Source, FileMode.Open, FileAccess.Read, FileShare.Read));
            try
            {
                if (opts.GenerateSfv)
                    opts.SfvGen = new SfvGenerator(Path.Combine(Path.GetDirectoryName(opts.Source), Path.GetFileNameWithoutExtension(opts.Source)) + ".sfv");
                var ret = ExtractXiso(opts, retval, ref br);
                if (!ret || !opts.DeleteIsoOnCompletion)
                    return ret;
                br.Close();
                File.Delete(opts.Source);
                return true;
            }
            finally
            {
                br.Close();
            }
        }

        private bool ExtractXiso(XisoOptions opts, XisoListAndSize retval, ref BinaryReader br)
        {
            _errorlevel = 0;
            if (opts.GenerateSfv)
                opts.SfvGen = new SfvGenerator(Path.Combine(Path.GetDirectoryName(opts.Source), Path.GetFileNameWithoutExtension(opts.Source)) + ".sfv");
            var space = GetTotalFreeSpace(opts.Target);
            if (space < 0)
            {
                UpdateStatus(string.Format("WARNING: Unable to get Total Free Space got: {0} We'll be extracting anyways...", space));
                space = retval.Size * 100; // There was an error, try to extract anyways
            }
            if (space > retval.Size)
            {
                _totalSize = retval.Size;
                if (!ExtractFiles(ref br, ref retval.List, opts.Target, opts))
                    return false;
                if (opts.GenerateSfv)
                    opts.SfvGen.Save();
                return true;
            }
            _errorlevel = 5;
            UpdateStatus(string.Format("Extraction failed! (Not enough space on drive) space needed: {0} Space available: {1}", Utils.GetSizeReadable(retval.Size), Utils.GetSizeReadable(space)));
            return false;
        }

        private static long GetTotalFreeSpace(string path)
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.RootDirectory.FullName.Equals(Path.GetPathRoot(path), StringComparison.CurrentCultureIgnoreCase))
                    return drive.TotalFreeSpace;
            }
            return -1;
        }

        internal void UpdateOperation(string operation)
        {
            mainForm.Status = operation;
        }

        internal void UpdateStatus(string status)
        {
            mainForm.Status = status;
        }

        private bool VerifyXiso(string filename)
        {
            UpdateOperation(string.Format("Verifying XISO: {0}", filename));
            var br = new BinaryReader(File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read));
            _baseOffset = 0; //Gamepartition only...
            if (!CheckMediaString(ref br))
            {
                if (_errorlevel != 0)
                    return false;
                _baseOffset = 0x4100; //XGD3
                if (!CheckMediaString(ref br))
                {
                    if (_errorlevel != 0)
                        return false;
                    _baseOffset = 0x1fb20; //XGD2
                    if (!CheckMediaString(ref br))
                    {
                        if (_errorlevel != 0)
                            return false;
                        _baseOffset = 0x30600; //XGD1 (Original Xbox)
                        if (!CheckMediaString(ref br))
                        {
                            br.Close();
                            UpdateStatus("Invalid XISO Image!");
                            _errorlevel = -2;
                            return false;
                        }
                        UpdateStatus("XGD (Original Xbox) Image detected!");
                    }
                    else
                        UpdateStatus("XGD2 Image detected!");
                }
                else
                    UpdateStatus("XGD3 Image detected!");
            }
            else
                UpdateStatus("XGD (Original Xbox) Image detected!");
            br.Close();
            return true;
        }

        private static bool CheckMediaString(ref BinaryReader br)
        {
            _errorlevel = 0;
            if (!BinarySeek(ref br, (_baseOffset + 32) * 2048, 0x14))
            {
                _errorlevel = -1;
                return false;
            }
            var data = br.ReadBytes(0x14);
            return Encoding.ASCII.GetString(data, 0, 0x14).Equals("MICROSOFT*XBOX*MEDIA");
        }

        private static bool BinarySeek(ref BinaryReader br, long offset, long len)
        {
            if (br.BaseStream.Length < offset + len)
            {
                br.Close();
                return false;
            }
            br.BaseStream.Seek(offset, SeekOrigin.Begin);
            return true;
        }

        private static void Parse(ref BinaryReader br, ref List<XisoTableData> list, int offset, int level, uint tocoffset, string dirprefix = "\\")
        {
            if (Abort)
                return;
            _errorlevel = 2;
            if (!BinarySeek(ref br, ((_baseOffset + tocoffset) * 2048) + offset + 4, 4))
                return;
            uint sector;
            EndianConverter.Big32(br.ReadBytes(4), out sector);
            _errorlevel = 3;
            if (sector == uint.MaxValue)
                return;
            _errorlevel = 2;
            if (!BinarySeek(ref br, ((_baseOffset + tocoffset) * 2048) + offset, 2))
                return;
            _errorlevel = int.MaxValue;
            ushort left;
            EndianConverter.Little16(br.ReadBytes(2), out left);
            ushort right;
            _errorlevel = 2;
            if (!BinarySeek(ref br, ((_baseOffset + tocoffset) * 2048) + offset + 2, 2))
                return;
            _errorlevel = int.MaxValue;
            if (!EndianConverter.Little16(br.ReadBytes(2), out right))
                return;
            if (left != 0)
                Parse(ref br, ref list, left * 4, level, tocoffset, dirprefix);
            _errorlevel = 2;
            if (!BinarySeek(ref br, ((_baseOffset + tocoffset) * 2048) + offset + 0xC, 1))
                return;
            if ((br.ReadByte() & 0x10) == 0x10) /* Dircectory found... */
            {
                level++;
                uint tocSector;
                if (!BinarySeek(ref br, ((_baseOffset + tocoffset) * 2048) + offset + 4, 4))
                    return;
                _errorlevel = int.MaxValue;
                if (!EndianConverter.Little32(br.ReadBytes(4), out tocSector))
                    return;
                _errorlevel = 2;
                if (!BinarySeek(ref br, ((_baseOffset + tocoffset) * 2048) + offset + 0xD, 1))
                    return;
                int dirnamelen = br.ReadByte();
                if (!BinarySeek(ref br, ((_baseOffset + tocoffset) * 2048) + offset + 0xE, dirnamelen))
                    return;
                var dirname = Enc.GetString(br.ReadBytes(dirnamelen));
                list.Add(new XisoTableData
                {
                    IsFile = false,
                    Path = dirprefix,
                    Name = dirname
                });
                if (tocSector != 0)
                    Parse(ref br, ref list, 0, level, tocSector, string.Format("{0}{1}\\", dirprefix, dirname));
            }
            else
            {
                if (!BinarySeek(ref br, ((_baseOffset + tocoffset) * 2048) + offset + 0xD, 1))
                    return;
                int filenamelen = br.ReadByte();
                if (!BinarySeek(ref br, ((_baseOffset + tocoffset) * 2048) + offset + 0xE, filenamelen))
                    return;
                var filename = Enc.GetString(br.ReadBytes(filenamelen));
                if (!BinarySeek(ref br, ((_baseOffset + tocoffset) * 2048) + offset + 4, 4))
                    return;
                _errorlevel = int.MaxValue;
                if (!EndianConverter.Little32(br.ReadBytes(4), out sector))
                    return;
                uint size;
                _errorlevel = 2;
                if (!BinarySeek(ref br, ((_baseOffset + tocoffset) * 2048) + offset + 8, 4))
                    return;
                _errorlevel = int.MaxValue;
                if (!EndianConverter.Little32(br.ReadBytes(4), out size))
                    return;
                list.Add(new XisoTableData
                {
                    IsFile = true,
                    Path = dirprefix,
                    Name = filename,
                    Size = size,
                    Offset = (sector + _baseOffset) * 0x800,
                });
            }
            if (right != 0)
                Parse(ref br, ref list, right * 4, level, tocoffset, dirprefix);
        }

        private bool ExtractFiles(ref BinaryReader br, ref List<XisoTableData> list, string target, XisoOptions xisoOpts)
        {
            _totalProcessed = 0;
            UpdateStatus(string.Format("Extracting files to {0}", target));
            UpdateOperation(string.Format("Extracting files to {0}", target));
            if (list.Count == 0)
                return false;
            Directory.CreateDirectory(target);
            foreach (var entry in list)
            {
                if (Abort)
                    return false;
                Directory.CreateDirectory(target + entry.Path);
                if (!entry.IsFile)
                {
                    Directory.CreateDirectory(target + entry.Path + entry.Name);
                    continue;
                }
                UpdateStatus(string.Format("Extracting {0}{1} ({2})", entry.Path, entry.Name, Utils.GetSizeReadable(entry.Size)));
                if (!ExtractFile(ref br, entry.Offset, entry.Size, string.Format("{0}{1}{2}", target, entry.Path, entry.Name), xisoOpts))
                    return false;
            }
            if (xisoOpts.GenerateSfv)
                xisoOpts.SfvGen.Save();
            return true;
        }

        private bool ExtractFile(ref BinaryReader br, long offset, uint size, string target, XisoOptions xisoOpts)
        {
            _errorlevel = 4;
            if (!BinarySeek(ref br, offset, size))
            {
                UpdateStatus("Seek failure");
                return false;
            }
            using (var bw = new BinaryWriter(File.Open(target, FileMode.Create, FileAccess.Write, FileShare.None)))
            {
                _errorlevel = 0;
                uint crc = 0;
                for (uint i = 0; i < size;)
                {
                    if (Abort)
                        return false;
                    var readsize = Utils.GetSmallest(size - i, ReadWriteBuffer);
                    if (!xisoOpts.GenerateSfv)
                        bw.Write(br.ReadBytes((int)readsize));
                    else
                    {
                        var buf = br.ReadBytes((int)readsize);
                        bw.Write(buf);
                        crc = SfvGenerator.Crc.ComputeChecksum(buf, crc);
                    }
                    _totalProcessed += readsize;
                    i += readsize;
                    if (MultiSize <= 0)
                        continue;
                    _multiProcessed += readsize;
                }
                if (xisoOpts.GenerateSfv)
                    xisoOpts.SfvGen.AddFile(target.Replace(xisoOpts.Target, ""), crc);
            }
            return true;
        }
    }

    internal sealed class XisoListAndSize
    {
        public long Files;
        public long Folders;
        public List<XisoTableData> List = new List<XisoTableData>();
        public long Size;
    }

    internal struct XisoTableData
    {
        public bool IsFile;
        public string Name;
        public long Offset;
        public string Path;
        public uint Size;
    }

    public sealed class XisoOptions
    {
        public bool DeleteIsoOnCompletion;
        public bool ExcludeSysUpdate = true;
        public bool GenerateFileList;
        public bool GenerateSfv;
        public SfvGenerator SfvGen;
        public string Source;
        public string Target;
        public bool UseFtp;
    }
}