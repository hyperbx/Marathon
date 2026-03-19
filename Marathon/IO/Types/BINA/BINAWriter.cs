using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.IO.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Marathon.IO.Types.BINA
{
    public class BINAWriter : BinaryObjectWriterEx
    {
        private const string _footerSignature = "bvh";

        public BINAHeader Header { get; private set; } = new();

        public List<StringPoolEntry> StringPoolEntries { get; set; } = [];

        public BINAWriter(Stream in_stream, Endianness in_endianness = Endianness.Big)
            : base(in_stream, StreamOwnership.Retain, in_endianness, EncodingFactory.ShiftJIS)
        {
            // Reserve header chunk.
            this.WriteZero<byte>(BINAHeader.Size);
        }

        public BINAWriter(Stream in_stream, long in_offset = 0, Endianness in_endianness = Endianness.Big)
            : base(in_stream, StreamOwnership.Retain, in_endianness, EncodingFactory.ShiftJIS)
        {
            Header.Offset = in_offset;

            JumpTo(Header.Offset);

            // Reserve header chunk.
            this.WriteZero<byte>(BINAHeader.Size);
        }

        public long WriteStringOffset(string in_str = null, bool in_writeNullPtrOnEmptyString = true, int in_fieldLength = 4)
        {
            var reserved = Position;

            if (string.IsNullOrEmpty(in_str) && in_writeNullPtrOnEmptyString)
            {
                this.WriteZero<byte>(in_fieldLength);
                return reserved;
            }

            var newEntry = new StringPoolEntry(in_str);
            var isNewEntry = true;

            foreach (var entry in StringPoolEntries)
            {
                if (entry.Data == in_str)
                {
                    newEntry = entry;
                    isNewEntry = false;
                    break;
                }
            }

            reserved = Reserve(in_fieldLength);

            newEntry.Offsets.Add(reserved);

            if (isNewEntry)
                StringPoolEntries.Add(newEntry);

            return reserved;
        }

        public void WriteStringPool()
        {
            foreach (var entry in StringPoolEntries)
            {
                // Write all offsets that point to this string in the file.
                foreach (var offset in entry.Offsets)
                    WriteReserved(offset, (uint)Position - BINAHeader.Size);

                this.WriteStringNullTerminated(entry.Data);
            }

            this.Align(4);
        }

        public void WriteFooter()
        {
            var relocTablePos = WriteRelocationTable();

            Header.RelocTableOffset = (uint)(relocTablePos - BINAHeader.Size - Header.Offset);
            Header.RelocTableLength = (uint)(Position - relocTablePos);

            if (Header.HasFooterMagic)
                WriteFooterMagic();

            Header.Length = (uint)(Position - Header.Offset);
        }

        public long WriteRelocationTable()
        {
            var relocTable = new BINARelocationTable(Header.Offset);

            relocTable.AddOffsets(Offsets.Values);

            return relocTable.Write(this);
        }

        public void WriteFooterMagic()
        {
            Write(0x10); // TODO: unknown.
            this.WriteZero<int>();
            WriteStringNullTerminated(Encoding.UTF8, _footerSignature);
        }

        public void FinishWrite()
        {
            WriteStringPool();
            WriteFooter();

            JumpTo(Header.Offset);

            Header.Write(this);
        }
    }

    public class StringPoolEntry(string in_str)
    {
        /// <summary>
        /// A collection of offsets to this string.
        /// </summary>
        public List<long> Offsets { get; set; } = [];

        /// <summary>
        /// The string to be written to the string pool.
        /// </summary>
        public string Data { get; set; } = in_str;
    }
}
