// BoomArchive.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 HyperBE32
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
using Marathon.IO.Exceptions;

namespace Marathon.IO.Formats.Archives
{
    /// <summary>
    /// <para>File base for the WIIU.STREAM format.</para>
    /// <para>Used in Sonic Boom: Rise of Lyric for storing game data.</para>
    /// </summary>
    public class BoomArchive : Archive
    {
        public struct BoomDataEntry
        {
            /// <summary>
            /// Compressed size of the file.
            /// </summary>
            public uint CompressedSize;

            /// <summary>
            /// Decompressed size of the file.
            /// </summary>
            public uint UncompressedSize;

            /// <summary>
            /// Hash used for updates (not required).
            /// </summary>
            public ulong Hash;

            /// <summary>
            /// Name of the file.
            /// </summary>
            public string Name;

            /// <summary>
            /// Data pertaining to this entry.
            /// </summary>
            public long Data;

            public BoomDataEntry(ExtendedBinaryReader reader)
            {
                CompressedSize   = reader.ReadUInt32();
                UncompressedSize = reader.ReadUInt32();
                Hash             = reader.ReadUInt64();
                Name             = reader.ReadNullTerminatedString();
                Data             = reader.BaseStream.Position;
            }
        }

        public const string Signature = "strm",
                            Extension = ".wiiu.stream";

        public BoomArchive() { }

        public BoomArchive(string file, ArchiveStreamMode archiveMode = ArchiveStreamMode.CopyToMemory) : base(file, archiveMode) { }

        public override void Load(Stream stream)
        {
            ExtendedBinaryReader reader = new ExtendedBinaryReader(stream, true);

            // Read file signature.
            string signature = reader.ReadString();
            if (signature != Signature)
                throw new InvalidSignatureException(Signature, signature);

            // Read to end of file.
            while (reader.BaseStream.Position != stream.Length)
            {
                // Create data entry.
                var dataEntry = new BoomDataEntry(reader);

                // Get data bytes.
                byte[] data = ArchiveStreamMode == ArchiveStreamMode.CopyToMemory ? FetchFileData(stream, dataEntry) : new byte[] { 0x00 };

                // Add data entry to entries.
                Data.Add(new ArchiveFile(dataEntry.Name, data));
            }
        }

        public override void Save(Stream stream)
        {
            ExtendedBinaryWriter writer = new ExtendedBinaryWriter(stream, true);

            writer.WriteSignature(Signature);

            foreach (ArchiveFile entry in Data)
            {
                // Compressed Size.
                writer.Write(0); // TODO: Add compression support.

                // Uncompressed Size.
                writer.Write(entry.Data.Length);

                // Hash.
                writer.Write(0xFFFFFFFFFFFFFFFF); // Hash isn't required and computing it is unknown.

                // Name.
                writer.WriteNullTerminatedString(entry.Name);

                // Data.
                writer.Write(entry.Data);
            }
        }

        public byte[] FetchFileData(Stream stream, BoomDataEntry file)
        {
            // Create ExtendedBinaryReader.
            ExtendedBinaryReader reader = new ExtendedBinaryReader(stream, true);

            // Jump to the file's data.
            reader.JumpTo(file.Data);

            // Read uncompressed data.
            if (file.CompressedSize == 0)
                return reader.ReadBytes((int)file.UncompressedSize);

            // Decompress data.
            else
            {
                byte[] data = reader.ReadBytes((int)file.CompressedSize);

                for (int i = 0; i < file.CompressedSize; i++)
                {
                    if ((data[i] & 0x40) != 0)
                    {
                        return new byte[] { 0x00 }; // TODO: Add compression support.
                    }
                }
            }

            // Throw if we encounter an entry of an unknown type.
            throw new NotSupportedException($"Encountered a Boom data entry of unsupported type.");
        }
    }
}
