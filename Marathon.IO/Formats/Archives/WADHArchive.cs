// WADHArchive.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 HyperBE32
 * Copyright (c) 2020 Radfordhound
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
using Marathon.IO.Exceptions;

namespace Marathon.IO.Formats.Archives
{
    /// <summary>
    /// <para>File base for the WADH format.</para>
    /// <para>Used in a few of Data Design Interactive's games for storing game data.</para>
    /// </summary>
    public class WADHArchive : Archive
    {
        public struct WADHDataEntry
        {
            /// <summary>
            /// Offset to the null-terminated string.
            /// </summary>
            public uint NameOffset;

            /// <summary>
            /// Data pertaining to this entry.
            /// </summary>
            public uint Data;

            /// <summary>
            /// Size of the data this entry contains.
            /// </summary>
            public uint Size;

            /// <summary>
            /// TODO: Unknown.
            /// </summary>
            public bool UnknownBoolean_1;

            /// <summary>
            /// The index of the last node in this directory.
            /// </summary>
            public int LastNodeIndex;

            /// <summary>
            /// TODO: Unknown - never goes above the node count.
            /// </summary>
            public int UnknownInt32_2;

            public const uint SizeOf = 24;

            public WADHDataEntry(ExtendedBinaryReader reader, uint firstFileOffset)
            {
                NameOffset       = reader.ReadUInt32();
                Data             = firstFileOffset + reader.ReadUInt32();
                Size             = reader.ReadUInt32();
                UnknownBoolean_1 = reader.ReadBoolean32();
                LastNodeIndex    = reader.ReadInt32();
                UnknownInt32_2   = reader.ReadInt32();
            }
        }

        public class WADHArchiveFile : ArchiveFile
        {
            public override byte[] Decompress(Stream stream, ArchiveFile file)
            {
                // Create ExtendedBinaryReader.
                ExtendedBinaryReader reader = new ExtendedBinaryReader(stream, false);

                // Jump to the file's data.
                reader.JumpTo(file.Offset);

                // Read the file's uncompressed data.
                return reader.ReadBytes((int)file.Length);
            }
        }

        public const string Signature = "WADH",
                            Extension = ".wad";

        public WADHArchive() { }

        public WADHArchive(string file, bool storeInMemory = true) : base(file, storeInMemory) { }

        public override void Load(Stream stream)
        {
            // Create ExtendedBinaryReader.
            ExtendedBinaryReader reader = new ExtendedBinaryReader(stream);

            // Read archive signature.
            string signature = reader.ReadSignature(4);
            if (signature != Signature)
                throw new InvalidSignatureException(Signature, signature);

            // Read the rest of the WADH header.
            uint firstFileOffset   = reader.ReadUInt32(); // Offset to where the first file's data starts.
            uint nodeCount         = reader.ReadUInt32(); // Number of nodes in the table.
            uint stringTableLength = reader.ReadUInt32(); // Length of the string table.

            // Compute string table offset.
            uint stringTableOffset = nodeCount * WADHDataEntry.SizeOf + 16;

            // Create WADH entries array.
            var wadhEntries = new WADHDataEntry[nodeCount];

            // Read WADH child entries.
            for (uint i = 0; i < nodeCount; ++i)
                wadhEntries[i] = new WADHDataEntry(reader, firstFileOffset);

            // Recursively parse WADH entries, converting them into ArchiveData entries.
            ParseEntries(0, new ArchiveDirectory() { Data = Data });

            int ParseEntries(int wadhEntryIndex, ArchiveDirectory entries)
            {
                ref WADHDataEntry wadhEntry = ref wadhEntries[wadhEntryIndex];

                // Read name.
                reader.JumpTo(stringTableOffset + wadhEntry.NameOffset);
                string name = reader.ReadNullTerminatedString();

                // Recursively parse Directory entries.
                if (wadhEntry.LastNodeIndex != -1)
                {
                    // Create ArchiveDirectory node.
                    var dirEntry = new ArchiveDirectory()
                    {
                        Name = name,
                        Parent = entries
                    };

                    // Add the ArchiveDirectory to the current entries.
                    entries.Data.Add(dirEntry);

                    // Set entries to dirEntry.Data.
                    entries = dirEntry;

                    // Recursively parse the directory's child entries.
                    int wadhChildIndex = ++wadhEntryIndex;
                    while (wadhChildIndex <= wadhEntry.LastNodeIndex)
                        wadhChildIndex = ParseEntries(wadhChildIndex, entries);

                    // Return the index of the next entry.
                    return wadhEntry.LastNodeIndex;
                }

                // Parse File entries.
                else
                {
                    // Create WADHArchiveFile node.
                    var fileEntry = new WADHArchiveFile()
                    {
                        Name = name,
                        Parent = entries,
                        UncompressedSize = wadhEntry.Size,
                        Offset = wadhEntry.Data
                    };

                    // Load the data into memory if requested.
                    fileEntry.Data = StoreInMemory ? fileEntry.Decompress(stream, fileEntry) : new byte[] { 0x00 };

                    // Add the U8ArchiveFile to the current entries.
                    entries.Data.Add(fileEntry);

                    // Return the index of the next entry.
                    return ++wadhEntryIndex;
                }
            }
        }

        public override void Save(Stream stream)
        {
            // Create ExtendedBinaryWriter.
            var writer = new ExtendedBinaryWriter(stream, false);

            // Write archive signature.
            writer.Write(Signature);

            // Store this offset for when we start writing data.
            writer.AddOffset("firstFileOffset");

            // Write the total amount of nodes.
            writer.Write((uint)ArchiveData.GetTotalCount(Data) + 1);

            // Store this offset for when we write the string table.
            writer.AddOffset("stringTableLength");

            // TODO: Finish saving.
        }
    }
}
