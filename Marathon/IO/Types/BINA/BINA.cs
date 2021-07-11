using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Marathon.IO
{
    /// <summary>
    /// BINA specification definitions.
    /// </summary>
    public static class BINA
    {
        public static Encoding Encoding
        {
            get
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                return Encoding.GetEncoding("shift-jis");
            }
        }

        public enum OffsetTypes
        {
            SixBit = 0x40,
            FourteenBit = 0x80,
            ThirtyBit = 0xC0
        }
    }

    /// <summary>
    /// BinaryReader extension for reading BINA formats.
    /// </summary>
    public class BINAReader : BinaryReaderEx
    {
        public BINAReader(Stream input, uint offset = 0) : base(input, BINA.Encoding)
        {
            Offset = offset;

            ReadHeader();
        }

        public BINAReader(Stream input, Encoding encoding, uint offset = 0) : base(input, encoding)
        {
            Offset = offset;

            ReadHeader();
        }

        public BINAHeader ReadHeader() => new(this);

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

                lastOffsetPos = offsets[^1];
            }

            return offsets;
        }
    }

    /// <summary>
    /// BinaryWriter extension for writing BINA formats.
    /// </summary>
    public class BINAWriter : BinaryWriterEx
    {
        public static BINAHeader Header = new();

        protected class StringTableEntry
        {
            public List<string> OffsetNames = new();
            public string Data;

            public StringTableEntry() { }

            public StringTableEntry(string data)
                => Data = data;
        }

        protected List<StringTableEntry> StringTableEntries = new();

        public BINAWriter(Stream output) : base(output, Header.IsBigEndian) => Header.PrepareWrite(this);

        public BINAWriter(Stream output, Encoding encoding) : base(output, encoding, Header.IsBigEndian) => Header.PrepareWrite(this);

        public BINAWriter(Stream output, uint offset = 0, bool isBigEndian = false) : base(output, isBigEndian) => Offset = offset;

        public void FinishWrite()
        {
            WriteStringTable();
            WriteFooter();

            BaseStream.Position = 0;
            Header.FinishWrite(this);
        }

        public uint WriteStringTable()
        {
            FixPadding();

            uint stringTablePos = (uint)BaseStream.Position;

            foreach (var tableEntry in StringTableEntries)
            {
                // Fill-in all the offsets that point to this string in the file.
                foreach (string offsetName in tableEntry.OffsetNames)
                    FillOffset(offsetName, (uint)BaseStream.Position, true);

                // Write the string...
                WriteNullTerminatedString(tableEntry.Data);
            }

            FixPadding();

            return stringTablePos;
        }

        public uint WriteFooter()
        {
            uint footerStartPos = GetFooterStartPosition();

            // Update header values and write footer magic...
            Header.FinalTableLength = (uint)BaseStream.Position - footerStartPos;

            Header.FinalTableOffset = (footerStartPos - Offset);
            if (Header.IsFooterMagicPresent)
                Header.WriteFooterMagic(this);

            Header.FileSize = (uint)BaseStream.Position;

            return footerStartPos;
        }

        public uint GetFooterStartPosition()
        {
            bool isBigEndian = IsBigEndian;
            uint footerStartPos = (uint)BaseStream.Position;
            uint lastOffsetPos = Offset;

            IsBigEndian = true;

            // Writes the offset table...
            foreach (var offset in Offsets)
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

        public void AddString(string offsetName, string str, bool writeNulls = true, uint offsetLength = 4)
        {
            if (string.IsNullOrEmpty(offsetName))
                return;

            if (string.IsNullOrEmpty(str) && writeNulls)
            {
                WriteNulls(offsetLength);

                return;
            }

            StringTableEntry tableEntry = new(str);
            bool newEntry = true;

            // Makes sure there aren't any existing entries of this string.
            foreach (StringTableEntry strEntry in StringTableEntries)
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
                StringTableEntries.Add(tableEntry);
        }

        public override void FillOffset(string name, bool additive = false, bool removeOffset = false, bool throwOnMissingOffset = true)
            => base.FillOffset(name, additive, removeOffset, throwOnMissingOffset);

        public override void FillOffset(string name, uint value, bool additive = false, bool removeOffset = false, bool throwOnMissingOffset = true)
            => base.FillOffset(name, value, additive, removeOffset, throwOnMissingOffset);
    }
}
