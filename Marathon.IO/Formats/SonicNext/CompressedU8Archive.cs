// CompressedU8Archive.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 Radfordhound
 * Copyright (c) 2020 GerbilSoft
 * Copyright (c) 2020 HyperPolygon64
 * Copyright (c) 2020 Knuxfan24
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System.IO;
using System.Linq;
using Marathon.IO.Helpers;
using System.IO.Compression;
using Marathon.IO.Exceptions;
using System.Collections.Generic;
using System;
using System.Text;

namespace Marathon.IO.Formats.SonicNext
{
    public abstract class U8DataEntry
    {
        /// <summary>
        /// Name of the file or directory this U8DataEntry represents.
        /// </summary>
        public string Name;

        /// <summary>
        /// Whether this U8DataEntry represents a directory or a file.
        /// </summary>
        public abstract bool IsDirectory { get; }

        public U8DataEntry() { }
        public U8DataEntry(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Recursively computes the total number of DataEntries contained within the given list.
        /// </summary>
        /// <param name="dataEntries">The list of data entries to recurse through.</param>
        /// <returns>The total, recursive number of DataEntries contained within the given list.</returns>
        public static int GetTotalCount(List<U8DataEntry> dataEntries)
        {
            int totalCount = dataEntries.Count;
            foreach (var dataEntry in dataEntries)
            {
                if (dataEntry is U8DirectoryEntry childDirEntry)
                {
                    totalCount += childDirEntry.TotalContentsCount;
                }
            }

            return totalCount;
        }
    }

    public class U8FileEntry : U8DataEntry
    {
        /// <summary>
        /// Decompressed data of the file.
        /// </summary>
        public byte[] Data;

        public override bool IsDirectory => false;

        public U8FileEntry() { }
        public U8FileEntry(string name, byte[] data) : base(name)
        {
            Data = data;
        }
    }

    public class U8DirectoryEntry : U8DataEntry
    {
        /// <summary>
        /// The directories/files inside the directory.
        /// </summary>
        public List<U8DataEntry> Contents = new List<U8DataEntry>();

        /// <summary>
        /// The total, recursive number of DataEntries contained within this directory.
        /// </summary>
        public int TotalContentsCount => GetTotalCount(Contents);

        public override bool IsDirectory => true;

        public U8DirectoryEntry() { }
        public U8DirectoryEntry(string name) : base(name) { }
    }

    /// <summary>
    /// File base for the Sonic '06 ARC format.
    /// </summary>
    public class CompressedU8Archive : FileBase
    {
        public const uint Signature = 0x55AA382D;
        public const string Extension = ".arc";

        public readonly List<U8DataEntry> Entries = new List<U8DataEntry>();

        enum U8DataEntryType
        {
            File = 0,
            Directory = 1
        }

        struct U8DataEntryZlib
        {
            /// <summary>
            /// Various properties of this entry.
            /// </summary>
            /// <remarks>
            /// First byte is the U8 data type (0 == File, 1 == Directory).
            /// 
            /// The remaining 24 bits are the offset to this entry's name relative
            /// to the beginning of the string table. Use TypeMask and NameOffsetMask
            /// to get the respective values.
            /// </remarks>
            public uint Flags;

            /// <summary>
            /// Data pertaining to this entry.
            /// </summary>
            /// <remarks>
            /// For files, this is an offset to the file's data.
            /// 
            /// For directories, this is the index of the parent directory.
            /// The root entry just sets this to 0.
            /// </remarks>
            public uint Data;

            /// <summary>
            /// Size of the data this entry contains.
            /// </summary>
            /// <remarks>
            /// For files, this is the compressed size of the file's data.
            /// 
            /// For directories, this is the index of the next entry that
            /// isn't a child of this one. For the root entry and the final directory
            /// entry in the archive, this happens to be the total entry count
            /// for the entire archive.
            /// </remarks>
            public uint Size;

            /// <summary>
            /// The uncompressed size of the file represented by this
            /// entry, if this entry represents a file.
            /// </summary>
            /// <remarks>
            /// For files, this is the uncompressed size of the file's data.
            /// 
            /// For directories, I'm honestly not sure what this is; sometimes
            /// it's set to 0, sometimes it's set to the ASCII value of "none",
            /// sometimes it's other ASCII text, sometimes it's just a random number??
            /// 
            /// '06 doesn't seem to care if you just set this to 0, so it might just be unused.
            /// </remarks>
            public uint UncompressedSize;

            public const uint TypeMask = 0xFF000000;
            public const uint NameOffsetMask = 0x00FFFFFF;
            public const uint SizeOf = 16;

            public U8DataEntryType Type => (U8DataEntryType)((Flags & TypeMask) >> 24);
            public uint NameOffset => (Flags & NameOffsetMask);

            public U8DataEntryZlib(ExtendedBinaryReader reader)
            {
                Flags = reader.ReadUInt32();
                Data = reader.ReadUInt32();
                Size = reader.ReadUInt32();
                UncompressedSize = reader.ReadUInt32();
            }
        }

        public override void Load(Stream stream)
        {
            // Create ExtendedBinaryReader.
            var reader = new ExtendedBinaryReader(stream, true);

            // Read archive signature.
            uint signature = reader.ReadUInt32();
            if (signature != Signature)
            {
                throw new InvalidSignatureException(
                    Signature.ToString(), signature.ToString());
            }

            // Read the rest of the standard U8 header.
            uint entriesOffset = reader.ReadUInt32(); // Offset to where the table starts.
            uint entriesLength = reader.ReadUInt32(); // Length of the table.
            uint dataOffset    = reader.ReadUInt32(); // Offset to where the data starts.

            // Read U8 root entry.
            reader.JumpTo(entriesOffset);
            var u8RootEntry = new U8DataEntryZlib(reader);

            // Compute string table offset.
            uint strTableOffset = (entriesOffset + (u8RootEntry.Size *
                U8DataEntryZlib.SizeOf));

            // Create U8 entries array and copy root entry into it.
            var u8Entries = new U8DataEntryZlib[u8RootEntry.Size];
            u8Entries[0] = u8RootEntry;

            // Read U8 child entries.
            for (uint i = 1; i < u8RootEntry.Size; ++i)
            {
                u8Entries[i] = new U8DataEntryZlib(reader);
            }

            // Recursively parse U8 entries, converting them into DataEntries.
            ParseEntries(0, Entries, true);

            uint ParseEntries(uint u8EntryIndex, List<U8DataEntry> entries, bool isRoot = false)
            {
                ref U8DataEntryZlib u8Entry = ref u8Entries[u8EntryIndex];

                // Read name.
                reader.JumpTo(strTableOffset + u8Entry.NameOffset);
                string name = reader.ReadNullTerminatedString();

                // Recursively parse Directory entries.
                if (u8Entry.Type == U8DataEntryType.Directory)
                {
                    // If we're not dealing with the root entry, create a directory entry.
                    if (!isRoot)
                    {
                        // Create U8DirectoryEntry and add it to entries.
                        var dirEntry = new U8DirectoryEntry(name);
                        entries.Add(dirEntry);

                        // Set entries to dirEntry.Contents.
                        entries = dirEntry.Contents;
                    }

                    // Recursively parse the directory's child entries.
                    uint u8ChildIndex = (u8EntryIndex + 1);
                    while (u8ChildIndex < u8Entry.Size)
                    {
                        u8ChildIndex = ParseEntries(u8ChildIndex, entries);
                    }

                    // Return the index of the next entry.
                    return u8Entry.Size;
                }

                // Parse File entries.
                else if (u8Entry.Type == U8DataEntryType.File)
                {
                    // Jump to the file's data.
                    reader.JumpTo(u8Entry.Data);

                    // Read the file's zlib-compressed data.
                    byte[] compressedData = reader.ReadBytes((int)u8Entry.Size);

                    // Decompress the file's zlib-compressed data.
                    using (var compressedStream = new MemoryStream(compressedData))
                    using (var zipStream = new ZlibStream(compressedStream, CompressionMode.Decompress))
                    using (var resultStream = new MemoryStream())
                    {
                        // Copy decompressed data to result.
                        zipStream.CopyTo(resultStream);

                        // Create U8FileEntry from resulting uncompressed data and add it to entries.
                        entries.Add(new U8FileEntry(name, resultStream.ToArray()));
                    }

                    // Return the index of the next entry.
                    return u8EntryIndex + 1;
                }

                // Throw if we encounter an entry of an unknown type
                else throw new NotSupportedException(
                    $"Encountered an U8 entry of unsupported type ({(uint)u8Entry.Type}).");
            }
        }

        public override void Save(Stream stream)
        {
            // TODO: Sort data entries before writing!! This is very important actually
            // since iirc the game relies on sorting to find U8 entries more quickly.
            // Not doing this may result in game crashes in some cases!
            // Search for "sort" here for more info: http://wiki.tockdom.com/wiki/U8_(File_Format)

            // Create ExtendedBinaryWriter.
            var writer = new ExtendedBinaryWriter(stream, true);

            // Write archive signature.
            writer.Write(Signature);

            // Store the offset locations for later when we fill out the entries.
            writer.AddOffset("EntriesOffset");
            writer.AddOffset("EntriesLength");
            writer.AddOffset("DataOffset");

            // Write unknown values.

            // (We have to set at least one of these to something non-zero for compatibillity
            // with HedgeArcPack, which unfortunately has no other real way of telling if a
            // given archive is a standard U8 archive, or a Zlib U8 archive.)

            // (There's nothing special about these constants; they can be anything as long as at
            // least one of them is non-zero. I just picked these because arctool uses them too lol.)

            // TODO: Figure out what these values are.
            // The third uint here seems like it's in little endian??
            writer.Write(0xE4f91200U);
            writer.Write(0x00000402U);
            writer.WriteNulls(8);

            // Fill in the offset for where the table starts.
            writer.FillInOffset("EntriesOffset");

            // Write root entry.
            writer.Write((uint)U8DataEntryType.Directory << 24);
            writer.WriteNulls(4);
            writer.Write((uint)U8DataEntry.GetTotalCount(Entries) + 1);
            writer.WriteNulls(4); // TODO: Figure out what this is.

            // Write entries recursively.
            uint globalEntryIndex = 1, strTableLen = 1;
            bool hasData = false;

            foreach (var dataEntry in Entries)
            {
                WriteEntries(dataEntry);
            }

            // Write root entry name (always just an empty string).
            writer.Write((byte)0);

            // Write entry names recursively.
            foreach (var dataEntry in Entries)
            {
                WriteEntryNames(dataEntry);
            }

            // Fill-in EntriesLength.
            writer.FillInOffset("EntriesLength", (uint)stream.Position - 0x20);

            // Align the file to an offset divisible by 32.
            if (hasData) StreamHelpers.AlignTo32Bytes(stream);

            // Fill-in DataOffset.
            writer.FillInOffset("DataOffset");

            // Write entry data recursively.
            if (hasData)
            {
                globalEntryIndex = 1;
                foreach (var dataEntry in Entries)
                {
                    WriteEntryData(dataEntry);
                }
            }

            void WriteEntries(U8DataEntry dataEntry, uint parentIndex = 0)
            {
                // Get U8 entry type.
                var u8EntryType = (dataEntry.IsDirectory) ?
                    U8DataEntryType.Directory : U8DataEntryType.File;

                // Write U8 entry flags.
                writer.Write(((uint)u8EntryType << 24) | strTableLen);

                // Increase string table length.
                strTableLen += ((uint)Encoding.UTF8.GetByteCount(dataEntry.Name) + 1);

                // Write directory entries.
                if (dataEntry.IsDirectory)
                {
                    var dirEntry = (U8DirectoryEntry)dataEntry;

                    // Write parent index.
                    writer.Write(parentIndex);

                    // Set parent index to the index of the current directory entry.
                    parentIndex = globalEntryIndex;

                    // Increase global entry index.
                    ++globalEntryIndex;

                    // Write next directory index.
                    writer.Write(globalEntryIndex + (uint)dirEntry.TotalContentsCount);

                    // TODO: Figure out what this is.
                    writer.WriteNulls(4);

                    // Write directory contents recursively.
                    foreach (var childEntry in dirEntry.Contents)
                    {
                        WriteEntries(childEntry, parentIndex);
                    }
                }

                // Write file entries.
                else
                {
                    var fileEntry = (U8FileEntry)dataEntry;

                    // Store the locations of the data offset and file compressed
                    // size for later when we write the file's data.
                    writer.AddOffset($"Data_{globalEntryIndex}");
                    writer.AddOffset($"CompressedSize_{globalEntryIndex}");

                    // Write the file's uncompressed size.
                    writer.Write((uint)fileEntry.Data.Length);

                    // Increase global entry index.
                    ++globalEntryIndex;

                    // Indicate that this archive does, in fact, contain actual data.
                    hasData = true;
                }
            }

            void WriteEntryNames(U8DataEntry dataEntry)
            {
                // Write entry name.
                writer.WriteNullTerminatedString(dataEntry.Name);

                // Recurse through directory entry contents.
                if (dataEntry.IsDirectory)
                {
                    var dirEntry = (U8DirectoryEntry)dataEntry;
                    foreach (var childEntry in dirEntry.Contents)
                    {
                        WriteEntryNames(childEntry);
                    }
                }
            }

            void WriteEntryData(U8DataEntry dataEntry)
            {
                // Recurse through directory entry contents.
                if (dataEntry.IsDirectory)
                {
                    var dirEntry = (U8DirectoryEntry)dataEntry;

                    // Increase global entry index.
                    ++globalEntryIndex;

                    // Write entry data recursively.
                    foreach (var childEntry in dirEntry.Contents)
                    {
                        WriteEntryData(childEntry);
                    }
                }

                // Write file entry data.
                else
                {
                    var fileEntry = (U8FileEntry)dataEntry;

                    // Compress the file's data.
                    byte[] compressedData;

                    using (var compressedStream = new MemoryStream())
                    {
                        using (var zlibStream = new ZlibStream(compressedStream, CompressionLevel.Optimal))
                        using (var zipStream = new BufferedStream(zlibStream))
                        {
                            // Compress data using ZlibStream so we have the zlib header and Adler32 checksum.
                            zipStream.Write(fileEntry.Data, 0, fileEntry.Data.Length);
                        }

                        // Store the compressed data so we can access its properties.
                        compressedData = compressedStream.ToArray();
                    }

                    // Align the file to an offset divisible by 32.
                    StreamHelpers.AlignTo32Bytes(stream);

                    // Fill-in file data offset and compressed size.
                    writer.FillInOffset($"Data_{globalEntryIndex}");
                    writer.FillInOffset($"CompressedSize_{globalEntryIndex}",
                        (uint)compressedData.Length);

                    // Write the file's compressed data.
                    writer.Write(compressedData);

                    // Increase global entry index.
                    ++globalEntryIndex;
                }
            }
        }

        //public void Extract(string destination)
        //{
        //    string directory = string.Empty;

        //    foreach (U8DataEntry entry in Entries)
        //    {
        //        if (entry.IsDirectory)
        //        {
        //            directory = string.Empty;

        //            List<string> paths = new List<string> { entry.Name };

        //            uint ParentDir = entry.Parent;

        //            while (ParentDir != 0)
        //            {
        //                paths.Add(Entries[(int)ParentDir].Name);
        //                ParentDir = Entries[(int)ParentDir].Parent;
        //            }

        //            paths.Reverse();

        //            foreach (string path in paths)
        //                directory += $"{path}\\".Equals("\\") ? string.Empty : $"{path}\\";

        //            Directory.CreateDirectory(Path.Combine(destination, directory));
        //        }
        //        else
        //            File.WriteAllBytes(Path.Combine(destination, directory, entry.Name), entry.Data);
        //    }
        //}
    }
}
