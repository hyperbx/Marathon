// PathPackage.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
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
using System.Collections.Generic;
using Marathon.IO.Headers;

namespace Marathon.IO.Formats.Miscellaneous
{
    /// <summary>
    /// <para>File base for the PathObj.bin format.</para>
    /// <para>Used in SONIC THE HEDGEHOG for defining spline guided objects.</para>
    /// </summary>
    public class PathPackage : FileBase
    {
        public class ObjectEntry
        {
            public string Name;
            public List<string> Files = new List<string>();
        }

        public const string Extension = ".bin";

        public List<ObjectEntry> Entries = new List<ObjectEntry>();

        public override void Load(Stream stream)
        {
            BINAReader reader = new BINAReader(stream);
            reader.ReadHeader();

            // Store first offset after the header.
            long startPosition = reader.BaseStream.Position;

            // Store the offset to the next string entry (repurposed as offset table length).
            uint offsetTableLength = reader.ReadUInt32();

            // Jump back to the first offset so we can iterate through the loop.
            reader.JumpTo(startPosition);

            // Create new node.
            ObjectEntry @object = new ObjectEntry();

            // Read stream until we've reached the end of the offset table.
            while (reader.BaseStream.Position != offsetTableLength - 4 + 0x20)
            {
                // Store the offset to the next string entry.
                uint stringOffset = reader.ReadUInt32();

                // Store current position to jump back to for the next entry.
                long position = reader.BaseStream.Position;

                // If the offset is null, save the current entry into the list and make a new one.
                if (stringOffset == 0)
                {
                    // Add current entry.
                    Entries.Add(@object);

                    // Create new node for the next entry.
                    @object = new ObjectEntry();
                }
                else
                {
                    reader.JumpTo(stringOffset, true);

                    // If the object name is null, read the current string as the name.
                    if (@object.Name == null)
                        @object.Name = reader.ReadNullTerminatedString();

                    // Otherwise, read it as the next file entry.
                    else
                        @object.Files.Add(reader.ReadNullTerminatedString());
                }

                // Jump back to the start and continue reading from the last string offset.
                reader.JumpTo(position);
            }
        }

        public override void Save(Stream stream)
        {
            BINAv1Header header = new BINAv1Header();
            BINAWriter writer = new BINAWriter(stream, header);

            for (int i = 0; i < Entries.Count; i++)
            {
                // Write name of entry.
                writer.AddString($"entryName_{i}", Entries[i].Name);

                // Write files for the current entry.
                for (int o = 0; o < Entries[i].Files.Count; o++)
                    writer.AddString($"entryName_{i}_File_{o}", Entries[i].Files[o]);

                // Write nulls to define the next node.
                writer.WriteNulls(4);
            }

            // Write extra set of nulls as padding.
            writer.WriteNulls(4);

            writer.FinishWrite(header);
        }
    }
}
