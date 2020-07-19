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
        public class DataEntry
        {
            public string Name;
            public byte[] Data;
            public bool IsDirectory;
            public uint ParentDirectory, CompressedSize, DecompressedSize;
        }

        public const uint Signature = 0x55AA382D;
        public const string Extension = ".arc";

        public List<DataEntry> Entries = new List<DataEntry>();

        public override void Load(Stream stream)
        {
            ExtendedBinaryReader reader = new ExtendedBinaryReader(stream, true);

            // Read archive signature.
            uint signature = reader.ReadUInt32();
            if (signature != Signature) throw new InvalidSignatureException(Signature.ToString(), signature.ToString());

            uint EntriesOffset = reader.ReadUInt32(); // Offset to where the table starts.
            uint EntriesLength = reader.ReadUInt32(); // Length of the table.
            uint DataOffset = reader.ReadUInt32();    // Offset to where the data starts.

            // TODO: Unknown - count in little-endian?
            uint UnknownUInt32_1 = reader.ReadUInt32();
            uint UnknownUInt32_2 = reader.ReadUInt32();
            uint UnknownUInt32_3 = reader.ReadUInt32();
            uint UnknownUInt32_4 = reader.ReadUInt32();

            // Gets the string table position.
            reader.JumpAhead(8);
            uint nodeCount = reader.ReadUInt32();
            uint stringTableOffset = (nodeCount * 16) + 32;
            reader.JumpBehind(12);

            for (int i = 0; i < nodeCount; i++)
            {
                DataEntry entry = new DataEntry();

                // Sets up properties for the current entry.
                entry.IsDirectory      = reader.ReadBoolean();
                uint FileNameOffset    = reader.ReadUInt24();
                uint FileDataOffset    = reader.ReadUInt32();
                entry.CompressedSize   = reader.ReadUInt32();
                entry.DecompressedSize = reader.ReadUInt32();

                // Stores the current position so we can return to the table later.
                long position = reader.BaseStream.Position;

                // Reads the name of the entry.
                reader.JumpTo(stringTableOffset + FileNameOffset);
                entry.Name = reader.ReadNullTerminatedString();

                if (!entry.IsDirectory)
                {
                    reader.JumpTo(FileDataOffset);

                    byte[] compressedData = reader.ReadBytes((int)entry.CompressedSize);

                    // Decompress zlib data.
                    using (var compressedStream = new MemoryStream(compressedData))
                        using (var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
                            using (var resultStream = new MemoryStream())
                            {
                                zipStream.CopyTo(resultStream);
                                entry.Data = resultStream.ToArray();
                            }

                    Entries.Add(entry);
                }
                else
                {
                    // Sets the parent directory ID.
                    entry.ParentDirectory = FileDataOffset;

                    Entries.Add(entry);
                }

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

            for (int i = 0; i < Entries.Count; i++)
            {
                // Writes the directory flag.
                writer.WriteByType(typeof(bool), Entries[i].IsDirectory);

                // Stores the offset locations for later when we fill out the string table.
                writer.AddOffset($"StringTableOffset_{i}", 3);

                if (Entries[i].IsDirectory)
                    writer.Write(Entries[i].ParentDirectory); // Write directory parent ID.
                else
                    writer.AddOffset($"EntryDataOffset_{i}"); // Stores the offset location for later when we write the data.

                // Writes the compressed and decompressed sizes.
                writer.Write(Entries[i].CompressedSize);
                writer.Write(Entries[i].DecompressedSize);
            }

            // Fills in the offset for where the entries end.
            writer.FillInOffset("EntriesLength");

            // Stores the current position so we know where the string table is.
            long StringTableOffset = stream.Position;

            for (int i = 0; i < Entries.Count; i++)
            {
                // Stores the current position so we know where the last string was written.
                long LastStringPosition = stream.Position;

                // Jump to the respective offset in the table.
                writer.JumpToOffset($"StringTableOffset_{i}", true);

                // Writes the offset of the last written string.
                writer.WriteUInt24((uint)(LastStringPosition - StringTableOffset));

                // Jumps back to the position of the last written string.
                stream.Position = LastStringPosition;

                // Writes the name of the file or directory.
                writer.WriteNullTerminatedString(Entries[i].Name);
            }

            // TODO: Align data to 32 bytes.
            // This doesn't appear to be necessary as the game reads it perfectly fine, but for accuracy's sake, it'd be ideal.

            // Fills in the offset for where the data starts.
            writer.FillInOffset("DataOffset");

            for (int i = 0; i < Entries.Count; i++)
            {
                if (!Entries[i].IsDirectory)
                {
                    byte[] inputData = Entries[i].Data == null ? new byte[0] : Entries[i].Data;

                    writer.FillInOffset($"EntryDataOffset_{i}");

                    using (var compressedStream = new MemoryStream())
                    {
                        // Compress file data using ZlibStream so we have the zlib header and Adler32 checksum.
                        using (var zipStream = new BufferedStream(new ZlibStream(compressedStream, CompressionLevel.Optimal)))
                            zipStream.Write(inputData, 0, inputData.Length);

                        // Write compressed data...
                        writer.Write(compressedStream.ToArray());
                    }
                }
            }
        }

        public void Extract(string destination)
        {
            string directory = string.Empty;

            foreach (DataEntry entry in Entries)
            {
                if (entry.IsDirectory)
                {
                    directory = string.Empty;
                    List<string> paths = new List<string>();
                    paths.Add(entry.Name);
                    uint ParentDir = entry.ParentDirectory;

                    while (ParentDir != 0)
                    {
                        paths.Add(Entries[(int)ParentDir].Name);
                        ParentDir = Entries[(int)ParentDir].ParentDirectory;
                    }

                    paths.Reverse();

                    foreach (string path in paths)
                        directory += $"{path}\\".Equals("\\") ? string.Empty : $"{path}\\";

                    Directory.CreateDirectory(Path.Combine(destination, directory));
                }
                else
                    File.WriteAllBytes(Path.Combine(destination, directory, entry.Name), entry.Data);
            }
        }
    }
}
