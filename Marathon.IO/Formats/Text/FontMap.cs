// FontMap.cs is licensed under the MIT License:
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

namespace Marathon.IO.Formats.Text
{
    /// <summary>
    /// <para>File base for the FTM format.</para>
    /// <para>Used in SONIC THE HEDGEHOG for <a href="https://home.unicode.org">Unicode</a> and <a href="https://en.wikipedia.org/wiki/Shift_JIS">Shift JIS</a> font maps.</para>
    /// </summary>
    public class FontMap : FileBase
    {
        public class Character
        {
            public byte Unicode,
                        Row;

            public short UnknownUInt16_1;

            public Character(byte unicode, byte row)
            {
                Unicode = unicode;
                Row = row;
            }
        }

        public const string Signature = "FNTM", Extension = ".ftm";

        public string Texture;

        public List<Character> Entries = new List<Character>();

        public override void Load(Stream stream)
        {
            // Header
            BINAReader reader = new BINAReader(stream);
            reader.ReadHeader();

            string signature = reader.ReadSignature(4);
            if (signature != Signature)
                throw new InvalidSignatureException(Signature, signature);

            // TODO: Unknown.
            uint UnknownUInt32_1 = reader.ReadUInt32();
            uint UnknownUInt32_2 = reader.ReadUInt32();

            byte columnCount = reader.ReadByte();
            byte rowCount = reader.ReadByte();

            ushort UnknownUInt16_1 = reader.ReadUInt16();

            uint texturePos = reader.ReadUInt32();
            uint unicodeEntries = reader.ReadUInt32();

            // Texture
            reader.JumpTo(texturePos, true);
            Texture = reader.ReadNullTerminatedString();

            reader.JumpTo(unicodeEntries, true);

            // Unicode Characters
            while (reader.BaseStream.Position != texturePos + 0x20)
            {
                byte unicodeCharacter = reader.ReadByte();
                byte rowNumber = reader.ReadByte();

                // TODO: Unknown - possibly a flag?
                ushort UnknownUInt16_2 = reader.ReadUInt16();

                Entries.Add(new Character((byte)(unicodeCharacter - 0x20), rowNumber));
            }
        }
    }
}
