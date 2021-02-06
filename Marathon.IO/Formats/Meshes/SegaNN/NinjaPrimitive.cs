// NinjaPrimitive.cs is licensed under the MIT License:
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
    public class NinjaPrimitive
    {
        public NinjaPrimitiveType Type { get; set; }

        /* ---------- NNS_PRIMTYPE_DX_STRIPLIST Values ---------- */

        public uint Format { get; set; } // TODO: set up primitive member enum class.

        public List<ushort> SList_Indices = new List<ushort>();

        public NinjaPrimitive(ExtendedBinaryReader reader)
        {
            Type = (NinjaPrimitiveType)reader.ReadUInt32(); // This primitive's type.
            uint PrimitiveStripListOffset = reader.ReadUInt32(); // The offset to this primitive's NNS_PRIMTYPE_DX_STRIPLIST chunk.

            long position = reader.BaseStream.Position; // Save position so we can use it to jump back and read the next primitive after we've read all the data for this one.

            // NNS_PRIMTYPE_DX_STRIPLIST chunk.
            // Jump to this primitive's strip list chunk.
            reader.JumpTo(PrimitiveStripListOffset, true);

            Format            = reader.ReadUInt32();
            uint IndexNumber  = reader.ReadUInt32();
            uint StripCount   = reader.ReadUInt32();
            if (StripCount != 1)
            {
                System.Console.WriteLine($"StripCount in a Primitive in this XNO was {StripCount} rather than 1!");
            }
            uint LengthOffset = reader.ReadUInt32();
            uint IndexPointer = reader.ReadUInt32();
            uint IndexBuffer  = reader.ReadUInt32(); // Always 0?

            // Jump to this primitive's length offset.
            reader.JumpTo(LengthOffset, true);

            ushort Length = reader.ReadUInt16();

            // Jump to this primitive's index offset.
            reader.JumpTo(IndexPointer, true);

            /* TODO: Figure out if I should be using IndexNumber or Length for this, the XTO always has SLIST_INDEX_NUM and the value in SLIST_LENPTR be the same, but other ones don't.
                     Loop through based on amount of entries in this primitive's Strip List and save them into the SList_Index in the _NinjaObjectPrimitive. */
            for (int p = 0; p < IndexNumber; p++)
            {
                SList_Indices.Add(reader.ReadUInt16());
            }

            // Jump back to the saved position to read the next node.
            reader.JumpTo(position);
        }
    }
}
