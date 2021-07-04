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

        public List<ushort> Indices = new List<ushort>();

        public List<ushort> Strips = new List<ushort>();

        public NinjaPrimitive(ExtendedBinaryReader reader)
        {
            Type = (NinjaPrimitiveType)reader.ReadUInt32();      // This primitive's type.
            uint PrimitiveStripListOffset = reader.ReadUInt32(); // The offset to this primitive's NNS_PRIMTYPE_DX_STRIPLIST chunk.

            long position = reader.BaseStream.Position; // Save position so we can use it to jump back and read the next primitive after we've read all the data for this one.

            // NNS_PRIMTYPE_DX_STRIPLIST chunk.
            // Jump to this primitive's strip list chunk.
            reader.JumpTo(PrimitiveStripListOffset, true);

            Format                 = reader.ReadUInt32();
            uint IndexNumber       = reader.ReadUInt32();
            uint StripCount        = reader.ReadUInt32();
            uint StripLengthOffset = reader.ReadUInt32();
            uint IndexPointer      = reader.ReadUInt32();
            uint IndexBuffer       = reader.ReadUInt32(); // Always 0?

            // Jump to this primitive's length offset.
            reader.JumpTo(StripLengthOffset, true);

            // Read strip lengths.
            for (int i = 0; i < StripCount; i++)
            {
                Strips.Add(reader.ReadUInt16());
            }

            // Jump to this primitive's index offset.
            reader.JumpTo(IndexPointer, true);

            // Read strip indices.
            for (int i = 0; i < IndexNumber; i++)
            {
                Indices.Add(reader.ReadUInt16());
            }

            // Jump back to the saved position to read the next node.
            reader.JumpTo(position);
        }

        /// <summary>
        /// Returns a nested list of triangle strips.
        /// </summary>
        public List<List<int>> GetCoupledStrips()
        {
            List<List<int>> coupledStrips = new List<List<int>>();
            int lastIndex = 0;

            // Iterate through strip lengths.
            foreach (int stripLength in Strips)
            {
                List<int> strip = new List<int>();

                // Gather strip indices to create a singular strip.
                for (int i = 0; i < stripLength; i++)
                {
                    strip.Add(Indices[lastIndex]);

                    // Increment last index for relative calculation.
                    lastIndex++;
                }

                // Add strip to coupled list.
                coupledStrips.Add(strip);
            }

            return coupledStrips;
        }
    }
}