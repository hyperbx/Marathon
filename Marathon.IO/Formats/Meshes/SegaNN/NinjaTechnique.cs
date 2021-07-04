// NinjaTechnique.cs is licensed under the MIT License:
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

namespace Marathon.IO.Formats.Meshes.SegaNN
{
    public class NinjaTechnique
    {
        public string Name { get; set; }

        public uint Type { get; set; }

        public uint Index { get; set; }

        public uint NameOffset { get; set; }

        public NinjaTechnique(ExtendedBinaryReader reader)
        {
            Type = reader.ReadUInt32();
            Index = reader.ReadUInt32();
            NameOffset = reader.ReadUInt32();

            // Store current position to jump back to.
            long position = reader.BaseStream.Position;

            // Jump to and read this technique's name.
            reader.JumpTo(NameOffset, true);
            Name = reader.ReadNullTerminatedString();

            // Jump back to the saved position.
            reader.JumpTo(position);
        }
    }
}
