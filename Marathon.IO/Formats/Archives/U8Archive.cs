// U8Archive.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 Radfordhound
 * Copyright (c) 2020 HyperBE32
 * Copyright (c) 2020 GerbilSoft
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

using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using Marathon.IO.Helpers;
using Marathon.IO.Exceptions;

namespace Marathon.IO.Formats.Archives
{
    /// <summary>
    /// <para>File base for the U8 format.</para>
    /// <para>Used in more than a few games licensed by Nintendo for storing game data.</para>
    /// <para>Also supported is Sonic Team's modified variant used in SONIC THE HEDGEHOG to support Zlib compression.</para>
    /// </summary>
    public class U8Archive : Archive
    {
        /// <summary>
        /// Sets the compression level for repacking.
        /// </summary>
        public CompressionLevel CompressionLevel { get; set; } = CompressionLevel.Optimal;

        /// <summary>
        /// Use the Zlib-compressed variant of U8 (used for SONIC THE HEDGEHOG).
        /// </summary>
        public static bool Zlib { get; set; } = true;

        /// <summary>
        /// Endianness toggle (big-endian for most use cases; Sonic Heroes PS2 uses little-endian).
        /// </summary>
        public static bool BigEndian { get; set; } = true;

        /// <summary>
        /// Determines what type the node is.
        /// </summary>
        public enum U8DataType
        {
            File,
            Directory
        }

        /// <summary>
        /// Struct representing U8 entry nodes.
        /// </summary>
        public struct U8DataEntry
        {
            /// <summary>
            /// Various properties of this entry.
            /// </summary>
            /// 
            /// <remarks>
            /// First byte is the U8 data type (0 == File, 1 == Directory).
            /// 
            /// The remaining 24 bits are the offset to this entry's name relative
            /// to the beginning of the string table. Use TypeMask and NameOffsetMask
            /// to get the respective values.
            /// </remarks>
            public uint Flags;

            /// <summary>
            /// Mask used for extracting the data type - see <see cref="U8DataType"/> for the types.
            /// </summary>
            public const uint TypeMask = 0xFF000000;

            /// <summary>
            /// Mask used for extracting the offset to the name of this node.
            /// </summary>
            public const uint NameOffsetMask = 0x00FFFFFF;

            /// <summary>
            /// Data pertaining to this entry.
            /// </summary>
            /// 
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
            /// 
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
            /// 
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

            /// <summary>
            /// The size of this struct.
            /// </summary>
            /// 
            /// <remarks>
            /// Zlib-compressed U8 has an extra UInt32, hence the extra four bytes.
            /// </remarks>
            public static uint SizeOf = (uint)(Zlib ? 16 : 12);

            /// <summary>
            /// This node's data type.
            /// </summary>
            public U8DataType Type
                => (U8DataType)((Flags & TypeMask) >> 24);

            /// <summary>
            /// The offset to the name of this node.
            /// </summary>
            public uint NameOffset
                => Flags & NameOffsetMask;

            public U8DataEntry(ExtendedBinaryReader reader)
            {
                Flags            = reader.ReadUInt32();
                Data             = reader.ReadUInt32();
                Size             = reader.ReadUInt32();
                UncompressedSize = Zlib ? reader.ReadUInt32() : 0; // This only exists for Zlib-compressed U8.
            }
        }

        public class U8ArchiveFile : ArchiveFile
        {
            public U8ArchiveFile() { }

            public U8ArchiveFile(ArchiveFile file, bool keepOffset = true) : base(file, keepOffset) { }

            public override byte[] Decompress(Stream stream, ArchiveFile file)
            {
                // Create ExtendedBinaryReader.
                ExtendedBinaryReader reader = new ExtendedBinaryReader(stream, BigEndian);

                // Jump to the file's data.
                reader.JumpTo(file.Offset);

                // Decompress if compressed.
                if (Zlib && file.Length != 0)
                {
                    // Read the file's Zlib-compressed data.
                    byte[] compressedData = reader.ReadBytes((int)file.Length);

                    // Decompress the file's Zlib-compressed data.
                    using (var compressedStream = new MemoryStream(compressedData))
                    {
                        using (var zipStream = new ZlibStream(compressedStream, CompressionMode.Decompress))
                        {
                            using (var resultStream = new MemoryStream())
                            {
                                // Copy decompressed data to result.
                                zipStream.CopyTo(resultStream);

                                // Return decompressed data.
                                return resultStream.ToArray();
                            }
                        }
                    }
                }

                // File is not compressed or is Nintendo U8.
                else if (file.UncompressedSize != 0)
                {
                    // Read the file's uncompressed data.
                    return Zlib ? reader.ReadBytes((int)file.UncompressedSize) : reader.ReadBytes((int)file.Length);
                }

                // File is already uncompressed.
                else
                    return file.Data;
            }
        }

        public const uint Signature = 0x55AA382D;

        public const string Extension = ".arc";

        public U8Archive() { }

        public U8Archive(string file, bool zlib = true, bool bigEndian = true, ArchiveStreamMode archiveMode = ArchiveStreamMode.CopyToMemory) : base(file, archiveMode)
        {
            Zlib = zlib;
            BigEndian = bigEndian;
        }

        /// <summary>
        /// Reloads the archive with the same properties.
        /// </summary>
        public override Archive Reload()
            => new U8Archive(Location, Zlib, BigEndian, ArchiveStreamMode);

        public override void Load(Stream stream)
        {
            // Create ExtendedBinaryReader.
            var reader = new ExtendedBinaryReader(stream, BigEndian);

            // Read archive signature.
            uint signature = reader.ReadUInt32();
            if (signature != Signature)
                throw new InvalidSignatureException(Signature.ToString(), signature.ToString());

            // Read the rest of the standard U8 header.
            uint entriesOffset = reader.ReadUInt32(); // Offset to where the table starts.
            uint entriesLength = reader.ReadUInt32(); // Length of the table.
            uint dataOffset    = reader.ReadUInt32(); // Offset to where the data starts.

            // Read U8 root entry.
            reader.JumpTo(entriesOffset);
            var u8RootEntry = new U8DataEntry(reader);

            // Compute string table offset.
            uint strTableOffset = entriesOffset + (u8RootEntry.Size * U8DataEntry.SizeOf);

            // Create U8 entries array and copy root entry into it.
            var u8Entries = new U8DataEntry[u8RootEntry.Size];
            u8Entries[0]  = u8RootEntry;

            // Read U8 child entries.
            for (uint i = 1; i < u8RootEntry.Size; ++i)
                u8Entries[i] = new U8DataEntry(reader);

            // Recursively parse U8 entries, converting them into ArchiveData entries.
            ParseEntries(0, new ArchiveDirectory() { Data = Data }, true);

            uint ParseEntries(uint u8EntryIndex, ArchiveDirectory entries, bool isRoot = false)
            {
                ref U8DataEntry u8Entry = ref u8Entries[u8EntryIndex];

                // Read name.
                reader.JumpTo(strTableOffset + u8Entry.NameOffset);
                string name = reader.ReadNullTerminatedString();

                // Recursively parse Directory entries.
                if (u8Entry.Type == U8DataType.Directory)
                {
                    // Create ArchiveDirectory node.
                    var dirEntry = new ArchiveDirectory()
                    {
                        Name = name,
                        Parent = entries,
                        IsRoot = isRoot
                    };

                    // Add the ArchiveDirectory to the current entries.
                    entries.Data.Add(dirEntry);

                    // Set entries to dirEntry.Data.
                    entries = dirEntry;

                    // Recursively parse the directory's child entries.
                    uint u8ChildIndex = ++u8EntryIndex;
                    while (u8ChildIndex < u8Entry.Size)
                        u8ChildIndex = ParseEntries(u8ChildIndex, entries);

                    // Return the index of the next entry.
                    return u8Entry.Size;
                }

                // Parse File entries.
                else if (u8Entry.Type == U8DataType.File)
                {
                    // Create U8ArchiveFile node.
                    var fileEntry = new U8ArchiveFile()
                    {
                        Name = name,
                        Parent = entries,
                        Length = u8Entry.Size,
                        UncompressedSize = u8Entry.UncompressedSize,
                        Offset = u8Entry.Data
                    };

                    // Load the data into memory if requested.
                    fileEntry.Data = ArchiveStreamMode == ArchiveStreamMode.CopyToMemory ? fileEntry.Decompress(stream, fileEntry) : new byte[] { 0x00 };

                    // Add the U8ArchiveFile to the current entries.
                    entries.Data.Add(fileEntry);

                    // Return the index of the next entry.
                    return ++u8EntryIndex;
                }

                // Throw if we encounter an entry of an unknown type.
                else
                    throw new NotSupportedException($"Encountered an U8 entry of unsupported type ({(uint)u8Entry.Type}).");
            }
        }

        public override void Save(Stream stream)
        {
            // TODO: Sort data entries before writing!! This is very important actually
            // since iirc the game relies on sorting to find U8 entries more quickly.
            // Not doing this may result in game crashes in some cases!
            // Search for "sort" here for more info: http://wiki.tockdom.com/wiki/U8_(File_Format)

            // Create ExtendedBinaryWriter.
            var writer = new ExtendedBinaryWriter(stream, BigEndian);

            // Write archive signature.
            writer.Write(Signature);

            // Store the offset locations for later when we fill out the entries.
            writer.AddOffset("EntriesOffset");
            writer.AddOffset("EntriesLength");
            writer.AddOffset("DataOffset");

            /* Write unknown values.

               (We have to set at least one of these to something non-zero for compatibillity
               with HedgeArcPack, which unfortunately has no other real way of telling if a
               given archive is a standard U8 archive, or a Zlib U8 archive.)

               (There's nothing special about these constants; they can be anything as long as at
               least one of them is non-zero. I just picked these because arctool uses them too lol.)

               TODO: Figure out what these values are.
               The third uint here seems like it's in little endian?? */
            if (Zlib)
            {
                writer.Write(0xE4f91200U);
                writer.Write(0x00000402U);
                writer.WriteNulls(8);
            }
            else
            {
                writer.WriteNulls(16);
            }

            // Fill in the offset for where the table starts.
            writer.FillInOffset("EntriesOffset");

            uint globalEntryIndex = 0, strTableLen = 0;
            bool hasData = false;

            // Write entries recursively.
            foreach (var dataEntry in Data)
                WriteEntries(dataEntry);

            // Write entry names recursively.
            foreach (var dataEntry in Data)
                WriteEntryNames(dataEntry);

            // Fill-in EntriesLength.
            writer.FillInOffset("EntriesLength", (uint)stream.Position - 0x20);

            // Align the file to an offset divisible by 32.
            if (hasData) StreamHelper.AlignTo32Bytes(stream);

            // Fill-in DataOffset.
            writer.FillInOffset("DataOffset");

            if (hasData)
            {
                globalEntryIndex = 0;

                // Write entry data recursively.
                foreach (var dataEntry in Data)
                    WriteEntryData(dataEntry);
            }

            void WriteEntries(ArchiveData dataEntry, uint parentIndex = 0)
            {
                // Get U8 entry type.
                var u8EntryType = dataEntry.IsDirectory ? U8DataType.Directory : U8DataType.File;

                // Write U8 entry flags.
                writer.Write(((uint)u8EntryType << 24) | strTableLen);

                // Increase string table length.
                strTableLen += (uint)Encoding.UTF8.GetByteCount(dataEntry.Name) + 1;

                // Write directory entries.
                if (dataEntry.IsDirectory)
                {
                    var dirEntry = (ArchiveDirectory)dataEntry;

                    if (dirEntry.IsRoot)
                    {
                        // Root node has no parent.
                        writer.WriteNulls(4);

                        // Increase global entry index.
                        ++globalEntryIndex;

                        // Write the number of nodes in the archive.
                        writer.Write((uint)ArchiveData.GetTotalCount(Data));
                    }
                    else
                    {
                        // Write parent index.
                        writer.Write(parentIndex);

                        // Set parent index to the index of the current directory entry.
                        parentIndex = globalEntryIndex;

                        // Increase global entry index.
                        ++globalEntryIndex;

                        // Write next directory index.
                        writer.Write(globalEntryIndex + (uint)dirEntry.TotalContentsCount);
                    }

                    // TODO: Figure out what this is; it's only present in '06 archives.
                    if (Zlib)
                        writer.WriteNulls(4);

                    // Write directory contents recursively.
                    foreach (var childEntry in dirEntry.Data)
                        WriteEntries(childEntry, parentIndex);
                }

                // Write file entries.
                else
                {
                    var fileEntry = (ArchiveFile)dataEntry;

                    /* Store the locations of the data offset and file compressed
                       size for later when we write the file's data. */
                    writer.AddOffset($"Data_{globalEntryIndex}");

                    // Write compressed and uncompressed sizes for decompression.
                    if (Zlib)
                    {
                        // Write the file's compressed size.
                        writer.AddOffset($"CompressedSize_{globalEntryIndex}");

                        // Write the file's uncompressed size.
                        writer.Write(fileEntry.UncompressedSize);
                    }

                    // Nintendo U8 doesn't store compressed size.
                    else
                    {
                        // Write the file's uncompressed size.
                        writer.Write(fileEntry.Length);
                    }

                    // Increase global entry index.
                    ++globalEntryIndex;

                    // Indicate that this archive does, in fact, contain actual data.
                    hasData = true;
                }
            }

            void WriteEntryNames(ArchiveData dataEntry)
            {
                // Write entry name.
                writer.WriteNullTerminatedString(dataEntry.Name);

                // Recurse through directory entry contents.
                if (dataEntry.IsDirectory)
                {
                    var dirEntry = (ArchiveDirectory)dataEntry;
                    foreach (var childEntry in dirEntry.Data)
                        WriteEntryNames(childEntry);
                }
            }

            void WriteEntryData(ArchiveData dataEntry)
            {
                // Recurse through directory entry contents.
                if (dataEntry.IsDirectory)
                {
                    var dirEntry = (ArchiveDirectory)dataEntry;

                    // Increase global entry index.
                    ++globalEntryIndex;

                    // Write entry data recursively.
                    foreach (var childEntry in dirEntry.Data)
                        WriteEntryData(childEntry);
                }

                // Write file entry data.
                else
                {
                    var fileEntry = (ArchiveFile)dataEntry;

                    // Temporary storage for the file's data.
                    byte[] data;

                    // Compress the file's data.
                    if (Zlib)
                    {
                        using (var compressedStream = new MemoryStream())
                        {
                            using (var zlibStream = new ZlibStream(compressedStream, CompressionLevel))
                            {
                                using (var zipStream = new BufferedStream(zlibStream))
                                {
                                    // Compress data using ZlibStream so we have the Zlib header and Adler32 checksum.
                                    zipStream.Write(fileEntry.Data, 0, fileEntry.Data.Length);
                                }
                            }

                            // Store the compressed data so we can access its properties.
                            data = compressedStream.ToArray();
                        }
                    }

                    // Nintendo U8 doesn't use compression.
                    else
                    {
                        data = fileEntry.Data;
                    }

                    // Align the file to an offset divisible by 32.
                    StreamHelper.AlignTo32Bytes(stream);

                    // Fill-in file data offset and compressed size.
                    writer.FillInOffset($"Data_{globalEntryIndex}");

                    // Nintendo U8 doesn't store compressed size.
                    if (Zlib)
                        writer.FillInOffset($"CompressedSize_{globalEntryIndex}", (uint)data.Length);

                    // Write the file's data.
                    writer.Write(data);

                    // Increase global entry index.
                    ++globalEntryIndex;
                }
            }
        }
    }
}