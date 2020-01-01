using System;
using System.IO;
using System.Text;
using Toolkit.Text;
using System.Collections.Generic;

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

// XISOExtractorGUI is licensed under The Unlicense:
/*
 * The Unlicense

 * This is free and unencumbered software released into the public domain.

 * Anyone is free to copy, modify, publish, use, compile, sell, or
 * distribute this software, either in source code form or as a compiled
 * binary, for any purpose, commercial or non-commercial, and by any
 * means.

 * In jurisdictions that recognize copyright laws, the author or authors
 * of this software dedicate any and all copyright interest in the
 * software to the public domain. We make this dedication for the benefit
 * of the public at large and to the detriment of our heirs and
 * successors. We intend this dedication to be an overt act of
 * relinquishment in perpetuity of all present and future rights to this
 * software under copyright law.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
 * OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
 * ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.

 * For more information, please refer to <http://unlicense.org>
 */

namespace Toolkit.Tools
{
    public partial class XisoExtractor
    {
        private const uint ReadWriteBuffer = 0x200000;
        private static readonly Encoding Enc = Encoding.GetEncoding(1252);
        internal static bool Abort;
        private static long _baseOffset, _totalSize, _totalProcessed, _multiProcessed;
        internal static long MultiSize;
        public static int _errorlevel;

        private static string GetErrorString(int error, string filename) {
            switch (error) {
                case 0:
                    return StatusMessages.iso_Extracted(filename, false);
                case -1:
                    return StatusMessages.iso_FileTooSmall(filename, false);
                case -2:
                    return StatusMessages.iso_Invalid(filename, false);
                case 1:
                    return StatusMessages.iso_RootSectorOffsetFailure(filename, false);
                case 2:
                    return StatusMessages.iso_ParseFSError(filename, false);
                case 3:
                    return StatusMessages.iso_InvalidTOC(filename, false);
                case 4:
                    return StatusMessages.iso_FileTooSmall(filename, false);
                case 5:
                    return StatusMessages.ex_OutOfSpace;
                default:
                    return StatusMessages.ex_UnknownISOExtractError;
            }
        }

        internal static string GetLastError(string filename) { return GetErrorString(_errorlevel, filename); }

        internal static bool GetFileListAndSize(XisoOptions opts, out XisoListAndSize retval, out BinaryReader br)
        {
            Abort = false;
            retval = new XisoListAndSize();
            br = null;
            _errorlevel = 1;
            if (!VerifyXiso(opts.Source))
                return false;
            br = new BinaryReader(File.Open(opts.Source, FileMode.Open, FileAccess.Read, FileShare.Read));
            if (!BinarySeek(ref br, ((_baseOffset + 32) * 2048) + 0x14, 8))
                return false;
            var data = br.ReadBytes(8);
            uint rootsector;
            _errorlevel = int.MaxValue;
            if (!EndianConverter.Little32(ref data, out rootsector))
                return false;
            uint rootsize;
            if (!EndianConverter.Little32(ref data, out rootsize, 4))
                return false;
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
                        msg += string.Format("{0}{1} [Offset: 0x{2:X} Size: {3}]{4}", entry.Path, entry.Name, entry.Offset, Utilities.GetSizeReadable(entry.Size), Environment.NewLine);
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
            if (opts.GenerateFileList)
            {
                msg = string.Format("Total entries: {0}{4}Folders: {1}{4}Files: {2}{4}Total Filesize: {5}{4}{3}", retval.List.Count, retval.Folders, retval.Files, msg, Environment.NewLine,
                                    Utilities.GetSizeReadable(_totalSize));
                File.WriteAllText(string.Format("{0}\\{1}.txt", Path.GetDirectoryName(opts.Source), Path.GetFileNameWithoutExtension(opts.Source)), msg);
            }
            if (string.IsNullOrEmpty(opts.Target))
                opts.Target = string.Format("{0}\\{1}", Path.GetDirectoryName(opts.Source), Path.GetFileNameWithoutExtension(opts.Source));
            if (opts.Target.EndsWith("\\", StringComparison.Ordinal))
                opts.Target = opts.Target.Substring(0, opts.Target.Length - 1);
            retval.Size = _totalSize;
            return true;
        }

        internal static bool ExtractXiso(XisoOptions opts) {
            BinaryReader br = null;

            try {
                if (!GetFileListAndSize(opts, out XisoListAndSize retval, out br))
                    return false;
                if (ExtractXiso(opts, retval, ref br)) {
                    if (opts.DeleteIsoOnCompletion) {
                        br.Close();
                        File.Delete(opts.Source);
                    }
                    return true;
                }
                return false;
            } finally {
                if (br != null)
                    br.Close();
            }
        }

        internal bool ExtractXiso(XisoOptions opts, XisoListAndSize retval) {
            var br = new BinaryReader(File.Open(opts.Source, FileMode.Open, FileAccess.Read, FileShare.Read));

            try {
                var ret = ExtractXiso(opts, retval, ref br);
                if (!ret || !opts.DeleteIsoOnCompletion)
                    return ret;
                br.Close();
                File.Delete(opts.Source);
                return true;
            } finally {
                br.Close();
            }
        }

        private static bool ExtractXiso(XisoOptions opts, XisoListAndSize retval, ref BinaryReader br) {
            var space = GetTotalFreeSpace(opts.Target);
            if (space < 0)
                space = retval.Size * 100;
            if (space > retval.Size) {
                _totalSize = retval.Size;
                if (!ExtractFiles(ref br, ref retval.List, opts.Target, opts))
                    return false;
                return true;
            }
            return false;
        }

        private static long GetTotalFreeSpace(string path) {
            foreach (var drive in DriveInfo.GetDrives())
                if (drive.IsReady && drive.RootDirectory.FullName.Equals(Path.GetPathRoot(path), StringComparison.CurrentCultureIgnoreCase))
                    return drive.TotalFreeSpace;
            return -1;
        }

        private static bool VerifyXiso(string filename) {
            var br = new BinaryReader(File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read));
            _baseOffset = 0;
            if (!CheckMediaString(ref br)) {
                if (_errorlevel != 0)
                    return false;
                _baseOffset = 0x4100; //XGD3
                if (!CheckMediaString(ref br)) {
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
                            _errorlevel = -2;
                            return false;
                        }
                    }
                }
            }
            br.Close();
            return true;
        }

        private static bool CheckMediaString(ref BinaryReader br) {
            _errorlevel = 0;
            if (!BinarySeek(ref br, (_baseOffset + 32) * 2048, 0x14)) {
                _errorlevel = -1;
                return false;
            }
            var data = br.ReadBytes(0x14);
            return Encoding.ASCII.GetString(data, 0, 0x14).Equals("MICROSOFT*XBOX*MEDIA");
        }

        private static bool BinarySeek(ref BinaryReader br, long offset, long len) {
            if (br.BaseStream.Length < offset + len) {
                br.Close();
                return false;
            }
            br.BaseStream.Seek(offset, SeekOrigin.Begin);
            return true;
        }

        private static void Parse(ref BinaryReader br, ref List<XisoTableData> list, int offset, int level, uint tocoffset, string dirprefix = "\\") {
            if (Abort)
                return;
            _errorlevel = 2;
            if (!BinarySeek(ref br, ((_baseOffset + tocoffset) * 2048) + offset + 4, 4))
                return;
            EndianConverter.Big32(br.ReadBytes(4), out uint sector);
            _errorlevel = 3;
            if (sector == uint.MaxValue)
                return;
            _errorlevel = 2;
            if (!BinarySeek(ref br, ((_baseOffset + tocoffset) * 2048) + offset, 2))
                return;
            _errorlevel = int.MaxValue;
            EndianConverter.Little16(br.ReadBytes(2), out ushort left);
            _errorlevel = 2;
            if (!BinarySeek(ref br, ((_baseOffset + tocoffset) * 2048) + offset + 2, 2))
                return;
            _errorlevel = int.MaxValue;
            if (!EndianConverter.Little16(br.ReadBytes(2), out ushort right))
                return;
            if (left != 0)
                Parse(ref br, ref list, left * 4, level, tocoffset, dirprefix);
            _errorlevel = 2;
            if (!BinarySeek(ref br, ((_baseOffset + tocoffset) * 2048) + offset + 0xC, 1))
                return;
            if ((br.ReadByte() & 0x10) == 0x10) { // Directory found...
                level++;
                if (!BinarySeek(ref br, ((_baseOffset + tocoffset) * 2048) + offset + 4, 4))
                    return;
                _errorlevel = int.MaxValue;
                if (!EndianConverter.Little32(br.ReadBytes(4), out uint tocSector))
                    return;
                _errorlevel = 2;
                if (!BinarySeek(ref br, ((_baseOffset + tocoffset) * 2048) + offset + 0xD, 1))
                    return;
                int dirnamelen = br.ReadByte();
                if (!BinarySeek(ref br, ((_baseOffset + tocoffset) * 2048) + offset + 0xE, dirnamelen))
                    return;
                var dirname = Enc.GetString(br.ReadBytes(dirnamelen));
                list.Add(new XisoTableData {
                    IsFile = false,
                    Path = dirprefix,
                    Name = dirname
                });
                if (tocSector != 0)
                    Parse(ref br, ref list, 0, level, tocSector, string.Format("{0}{1}\\", dirprefix, dirname));
            } else {
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
                _errorlevel = 2;
                if (!BinarySeek(ref br, ((_baseOffset + tocoffset) * 2048) + offset + 8, 4))
                    return;
                _errorlevel = int.MaxValue;
                if (!EndianConverter.Little32(br.ReadBytes(4), out uint size))
                    return;
                list.Add(new XisoTableData {
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

        private static bool ExtractFiles(ref BinaryReader br, ref List<XisoTableData> list, string target, XisoOptions xisoOpts) {
            _totalProcessed = 0;
            if (list.Count == 0)
                return false;
            Directory.CreateDirectory(target);
            foreach (var entry in list) {
                if (Abort)
                    return false;
                Directory.CreateDirectory(target + entry.Path);
                if (!entry.IsFile) {
                    Directory.CreateDirectory(target + entry.Path + entry.Name);
                    continue;
                }
                if (!ExtractFile(ref br, entry.Offset, entry.Size, string.Format("{0}{1}{2}", target, entry.Path, entry.Name), xisoOpts))
                    return false;
            }
            return true;
        }

        private static bool ExtractFile(ref BinaryReader br, long offset, uint size, string target, XisoOptions xisoOpts) {
            _errorlevel = 4;
            if (!BinarySeek(ref br, offset, size))
                return false;
            using (var bw = new BinaryWriter(File.Open(target, FileMode.Create, FileAccess.Write, FileShare.None))) {
                _errorlevel = 0;
                for (uint i = 0; i < size;) {
                    if (Abort)
                        return false;
                    var readsize = Utilities.GetSmallest(size - i, ReadWriteBuffer);
                    bw.Write(br.ReadBytes((int)readsize));
                    _totalProcessed += readsize;
                    i += readsize;
                    if (MultiSize <= 0)
                        continue;
                    _multiProcessed += readsize;
                }
            }
            return true;
        }
    }

    internal sealed class XisoListAndSize {
        public long Files;
        public long Folders;
        public List<XisoTableData> List = new List<XisoTableData>();
        public long Size;
    }

    internal struct XisoTableData {
        public bool IsFile;
        public string Name;
        public long Offset;
        public string Path;
        public uint Size;
    }

    public sealed class XisoOptions {
        public bool DeleteIsoOnCompletion;
        public bool ExcludeSysUpdate = true;
        public bool GenerateFileList;
        public string Source;
        public string Target;
    }
}