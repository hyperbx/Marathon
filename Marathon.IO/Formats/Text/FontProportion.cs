// FontProportion.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 HyperPolygon64
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
using Marathon.IO.Exceptions;
using System;

namespace Marathon.IO.Formats.Text
{
    /// <summary>
    /// File base for the Sonic '06 FTM format.
    /// </summary>
    public class FontProportion : FileBase
    {
        public class Character
        {
            public byte Width;

            public sbyte Margin;

            public Character(byte width) => Width = width;
        }

        public const string Signature = "PRFI", Extension = ".pfi";

        public ushort Padding, Height;

        List<Character> Entries = new List<Character>();

        public override void Load(Stream stream)
        {
            // Header
            ExtendedBinaryReader reader = new ExtendedBinaryReader(stream, true);

            string signature = reader.ReadSignature(4);
            if (signature != Signature)
                throw new InvalidSignatureException(Signature, signature);

            uint entriesOffset = reader.ReadUInt32();

            Padding = reader.ReadUInt16();
            Height = reader.ReadUInt16();

            ushort markerCount = reader.ReadUInt16();
            ushort characterCount = reader.ReadUInt16();

            reader.JumpTo(entriesOffset);

            byte buffer = reader.ReadByte();

            while (buffer != 0x00)
            {
                Entries.Add(new Character(buffer));

                buffer = reader.ReadByte();
            }

            reader.JumpAhead(markerCount - 1);

            for (int i = 0; i < Entries.Count; i++)
            {
                Entries[i].Margin = reader.ReadSByte();
            }
        }
    }
}
