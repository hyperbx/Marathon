// NinjaNodeList.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2021 Knuxfan24
 * Copyright (c) 2021 HyperBE32
 * Copyright (c) 2021 Radfordhound
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

using System.Collections.Generic;

namespace Marathon.IO.Formats.Meshes.SegaNN
{
    public class NinjaNodeList
    {
        public uint Type { get; set; }

        public uint Count { get; set; }

        public uint EntriesOffset { get; set; }

        public List<string> Names = new List<string>();

        public NinjaNodeList(ExtendedBinaryReader reader)
        {
            Type = reader.ReadUInt32();
            Count = reader.ReadUInt32();
            EntriesOffset = reader.ReadUInt32();

            // Jump to the node table in this Node.
            reader.JumpTo(EntriesOffset, true);

            // Loop through based on amount of nodes in this Node.
            for (int i = 0; i < Count; i++)
            {
                uint NodeID = reader.ReadUInt32(); // The ID of the current node, always linear.
                uint NodeTypeOffset = reader.ReadUInt32(); // The offset to this node's name.

                // Store current position to jump back to for the next technique.
                long position = reader.BaseStream.Position;

                // Jump to and read this node's name.
                reader.JumpTo(NodeTypeOffset, true);
                string NodeName = reader.ReadNullTerminatedString();

                // Jump back to the saved position to read the next node.
                reader.JumpTo(position);

                // Save the node name into the NodeNameList in the _NinjaNodeNameList.
                Names.Add(NodeName);
            }
        }
    }
}
