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

namespace Marathon.IO.Formats.SonicNext
{
    /// <summary>
    /// File base for the Sonic '06 ARC format.
    /// </summary>
    public class CompressedU8Archive : FileBase
    {
        /* NOTES: We always skip the first iteration of the for loops in some cases
                  to avoid applying properties to the root node, which doesn't need them. */

        public class NodeEntry
        {
            public string Name;      // Name of the file
            public byte[] Data;      // Decompressed data of the file
            public bool Directory;   // Directory flag
            public NodeEntry Parent; // Parent node
        }

        public const uint Signature = 0x55AA382D;
        public const string Extension = ".arc";

        List<NodeEntry> Entries = new List<NodeEntry>();

        public override void Load(Stream stream)
        {
            ExtendedBinaryReader reader = new ExtendedBinaryReader(stream, true);

            // Read archive signature.
            uint signature = reader.ReadUInt32();
            if (signature != Signature) throw new InvalidSignatureException(Signature.ToString(), signature.ToString());

            uint entriesOffset = reader.ReadUInt32(); // Offset to where the table starts.
            uint entriesLength = reader.ReadUInt32(); // Length of the table.
            uint dataOffset    = reader.ReadUInt32(); // Offset to where the data starts.

            // Gets the string table position.
            reader.JumpAhead(24);
            uint nodeCount = reader.ReadUInt32();
            uint stringTableOffset = (nodeCount * 16) + 32;
            reader.JumpBehind(12);

            for (int i = 0; i < nodeCount; i++)
            {
                // Create data entry...
                NodeEntry dataEntry = new NodeEntry();

                // Sets up properties for the current entry.
                dataEntry.Directory   = reader.ReadBoolean(); // File = 0 | Directory = 1
                uint fileNameOffset   = reader.ReadUInt24();  // Offset to the name in the string table
                uint fileDataOffset   = reader.ReadUInt32();  // File = Data Offset | Directory = Parent Index
                uint compressedSize   = reader.ReadUInt32();  // File = Compressed Size | Directory = First node outside directory
                uint decompressedSize = reader.ReadUInt32();  // Decompressed size of the file

                // Stores the current position so we can return to the table later.
                long position = reader.BaseStream.Position;

                if (fileNameOffset != 0)
                {
                    // Reads the name of the entry.
                    reader.JumpTo(stringTableOffset + fileNameOffset);
                    dataEntry.Name = reader.ReadNullTerminatedString();
                }

                if (dataEntry.Directory)
                {
                    // Set parent property.
                    if (i != 0) dataEntry.Parent = Entries[(int)fileDataOffset];

                    // Add resulting directory to archive nodes.
                    Entries.Add(dataEntry);
                }
                else
                {
                    // Jump to the file data.
                    reader.JumpTo(fileDataOffset);

                    // Store compressed data for streams.
                    byte[] compressedData = reader.ReadBytes((int)compressedSize);

                    // Decompress zlib data.
                    using (MemoryStream compressedStream = new MemoryStream(compressedData))
                        using (ZlibStream zipStream = new ZlibStream(compressedStream, CompressionMode.Decompress))
                            using (MemoryStream resultStream = new MemoryStream())
                            {
                                // Copy decompressed data to result.
                                zipStream.CopyTo(resultStream);

                                // Add resulting file to archive nodes.
                                Entries.Add(new NodeEntry() { Name = dataEntry.Name, Data = resultStream.ToArray() });
                            }
                }

                // Jump back to the table.
                reader.JumpTo(position);
            }
        }

        public override void Save(Stream stream)
        {
            ExtendedBinaryWriter writer = new ExtendedBinaryWriter(stream, true);

            // Write archive signature.
            writer.Write(Signature);

            // Stores the offset locations for later when we fill out the entries.
            writer.AddOffset("EntriesOffset");
            writer.AddOffset("EntriesLength");
            writer.AddOffset("DataOffset");

            // TODO: Unknown - count in little-endian?
            writer.WriteNulls(16);

            // Fills in the offset for where the table starts.
            writer.FillInOffset("EntriesOffset");

            // Stores the current position so we know where the entry table is.
            long entryTablePosition = stream.Position;

            for (int i = 0; i < Entries.Count; i++)
            {
                // Writes the directory flag.
                writer.WriteByType(typeof(bool), Entries[i].Directory);

                // Stores the offset locations for later when we fill out the string table.
                writer.AddOffset($"StringTableOffset_{i}", 3);

                // Write parent entry index if it's a directory.
                if (Entries[i].Directory && i != 0)
                    writer.Write((uint)(Entries[i].Parent == null ? Entries.Count : Entries.IndexOf(Entries[i].Parent)));

                // Stores the offset location for later when we write the data if it's a file.
                else
                    writer.AddOffset($"EntryDataOffset_{i}");

                // Stores the offset location for later when we compress the data.
                writer.AddOffset($"CompressedSize_{i}");

                // Writes the decompressed size.
                writer.Write((uint)(Entries[i].Data == null ? 0 : Entries[i].Data.Count()));
            }

            // Stores the current position so we know where the string table is.
            long stringTableOffset = stream.Position;

            for (int i = 0; i < Entries.Count; i++)
            {
                if (i == 0) continue;

                // Stores the current position so we know where the last string was written.
                long LastStringPosition = stream.Position;

                // Jump to the respective offset in the table.
                writer.JumpToOffset($"StringTableOffset_{i}", true);

                // Writes the offset of the last written string.
                writer.WriteUInt24((uint)(LastStringPosition - stringTableOffset) + 1);

                // Jumps back to the position of the last written string.
                stream.Position = LastStringPosition;

                // Writes the name of the file or directory.
                if (Entries[i].Name != null)
                    writer.WriteNullTerminatedString(Entries[i].Name);
            }

            // Fills in the offset for where the entries end.
            writer.FillInOffset("EntriesLength", (uint)(stream.Position - entryTablePosition), false, true);

            for (int i = 0; i < Entries.Count; i++)
            {
                if (Entries[i].Directory)
                {
                    // Fills in the offset for the last child node index.
                    // TODO: Actually get this crap working...
                    writer.FillInOffset($"CompressedSize_{i}", 0, false, true);
                }
                else
                {
                    // Stores the data in a new array to determine its contents.
                    byte[] inputData = Entries[i].Data ?? new byte[0];

                    using (MemoryStream compressedStream = new MemoryStream())
                    {
                        // Compress data using ZlibStream so we have the zlib header and Adler32 checksum.
                        using (BufferedStream zipStream = new BufferedStream(new ZlibStream(compressedStream, CompressionLevel.Optimal)))
                            zipStream.Write(inputData, 0, inputData.Length);

                        // Stores the compressed data so we can access its properties.
                        byte[] compressedData = compressedStream.ToArray();

                        // Aligns the stream data to 32 bytes...
                        StreamHelpers.AlignTo32Bytes(stream);

                        // Fills in the offset for where the data starts and removes it after the first entry.
                        writer.FillInOffset("DataOffset", false, true, false);

                        // Fills in the offset for where the current entry's data starts.
                        writer.FillInOffset($"EntryDataOffset_{i}");

                        // Write compressed data...
                        writer.Write(compressedData);

                        // Fills in the offset for the compressed size.
                        writer.FillInOffset($"CompressedSize_{i}", (uint)compressedData.Length, false, true);
                    }
                }
            }
        }

        //public void Extract(string destination)
        //{
        //    string directory = string.Empty;

        //    foreach (DataEntry entry in Entries)
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
