// BINA.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2018 Radfordhound
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
using System.Text;
using Marathon.IO.Headers;
using System.Collections.Generic;

namespace Marathon.IO.Helpers
{
    public static class BINA
    {
        public static Encoding Encoding => Encoding.GetEncoding("shift-jis");

        public enum OffsetTypes
        {
            SixBit = 0x40,
            FourteenBit = 0x80,
            ThirtyBit = 0xC0
        }
    }

    public class BINAReader : ExtendedBinaryReader
    {
        public BINAReader(Stream input, uint offset = 0) : base(input, BINA.Encoding) => Offset = offset;

        public BINAReader(Stream input, Encoding encoding, uint offset = 0) : base(input, encoding) => Offset = offset;

        public BINAHeader ReadHeader() => new BINAv1Header(this);

        public List<uint> ReadFooter(uint finalTableLength)
        {
            uint lastOffsetPos = Offset;
            uint footerEnd = (uint)BaseStream.Position + finalTableLength;
            var offsets = new List<uint>();

            while (BaseStream.Position < BaseStream.Length && BaseStream.Position < footerEnd)
            {
                byte b = ReadByte();
                byte type = (byte)(b & 0xC0); // 0xC0 = 1100 0000. We're getting the first 2 bits.
                byte d = (byte)(b & 0x3F);    // 0x3F = 0011 1111. We're getting the last 6 bits.

                if (type == (byte)BINA.OffsetTypes.SixBit)
                {
                    d <<= 2;
                    offsets.Add(d + lastOffsetPos);
                }
                else if (type == (byte)BINA.OffsetTypes.FourteenBit)
                {
                    byte b2 = ReadByte();
                    ushort d2 = (ushort)(((d << 8) | b2) << 2);

                    offsets.Add(d2 + lastOffsetPos);
                }
                else if (type == (byte)BINA.OffsetTypes.ThirtyBit)
                {
                    var bytes = ReadBytes(3);
                    uint d2 = (uint)(((d << 24) | (bytes[0] << 16) | (bytes[1] << 8) | bytes[2]) << 2);

                    offsets.Add(d2 + lastOffsetPos);
                }
                else break;

                lastOffsetPos = offsets[offsets.Count - 1];
            }

            return offsets;
        }
    }

    public class BINAWriter : ExtendedBinaryWriter
    {
        protected class StringTableEntry
        {
            public List<string> OffsetNames = new List<string>();
            public string Data;

            public StringTableEntry() { }

            public StringTableEntry(string data) => Data = data;
        }

        protected List<StringTableEntry> _StringTableEntries = new List<StringTableEntry>();

        public BINAWriter(Stream output, uint offset = 0, bool isBigEndian = false) : base(output, BINA.Encoding, isBigEndian)
            => Offset = offset;

        public BINAWriter(Stream output, BINAHeader header) : base(output, BINA.Encoding, header.IsBigEndian)
            => header.PrepareWrite(this);

        public BINAWriter(Stream output, Encoding encoding, BINAHeader header) : base(output, encoding, header.IsBigEndian)
            => header.PrepareWrite(this);

        public void FinishWrite(BINAHeader header)
        {
            WriteStringTable();
            WriteFooter(header);

            BaseStream.Position = 0;
            header.FinishWrite(this);
        }

        public uint WriteStringTable()
        {
            FixPadding();
            uint stringTablePos = (uint)BaseStream.Position;

            foreach (var tableEntry in _StringTableEntries)
            {
                // Fill-in all the offsets that point to this string in the file.
                foreach (string offsetName in tableEntry.OffsetNames)
                    FillInOffset(offsetName, (uint)BaseStream.Position, true);

                // Write the string...
                WriteNullTerminatedString(tableEntry.Data);
            }

            FixPadding();
            return stringTablePos;
        }

        public uint WriteFooter(BINAHeader header)
        {
            uint footerStartPos = WriteFooter();

            // Update header values and write footer magic...
            header.FinalTableLength = (uint)BaseStream.Position - footerStartPos;

            if (header is BINAv1Header h1)
            {
                h1.FinalTableOffset = (footerStartPos - Offset);
                if (h1.IsFooterMagicPresent)
                    h1.WriteFooterMagic(this);
            }

            header.FileSize = (uint)BaseStream.Position;

            return footerStartPos;
        }

        public uint WriteFooter()
        {
            bool isBigEndian = IsBigEndian;
            uint footerStartPos = (uint)BaseStream.Position;
            uint lastOffsetPos = Offset;
            IsBigEndian = true;

            // Writes the offset table...
            foreach (var offset in _OffsetDictionary)
            {
                uint d = (offset.Value - lastOffsetPos) >> 2;

                if (d <= 0x3F)
                    Write((byte)(((byte)BINA.OffsetTypes.SixBit) | d));

                else if (d <= 0x3FFF)
                    Write((ushort)((((byte)BINA.OffsetTypes.FourteenBit) << 8) | d));

                else
                    Write((uint)((((byte)BINA.OffsetTypes.ThirtyBit) << 24) | d));

                lastOffsetPos = offset.Value;
            }

            FixPadding(4);
            IsBigEndian = isBigEndian;
            return footerStartPos;
        }

        public void AddString(string offsetName, string str, uint offsetLength = 4)
        {
            if (string.IsNullOrEmpty(offsetName)) return;

            if (string.IsNullOrEmpty(str))
            {
                WriteNulls(offsetLength);
                return;
            }

            StringTableEntry tableEntry = new StringTableEntry(str);
            bool newEntry = true;

            // Makes sure there aren't any existing entries of this string.
            foreach (StringTableEntry strEntry in _StringTableEntries)
            {
                if (strEntry.Data == str)
                {
                    tableEntry = strEntry;
                    newEntry = false;
                    break;
                }
            }

            // Adds an offset to the string to be written into the string table later.
            AddOffset(offsetName, offsetLength);
            tableEntry.OffsetNames.Add(offsetName);

            if (newEntry)
                _StringTableEntries.Add(tableEntry);
        }

        public override void FillInOffset(string name, bool additive = false, bool removeOffset = false)
            => base.FillInOffset(name, additive, removeOffset);

        public override void FillInOffset(string name, uint value, bool additive = false, bool removeOffset = false)
            => base.FillInOffset(name, value, additive, removeOffset);
    }
}
