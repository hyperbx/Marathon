using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.IO.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Marathon.IO.Types.BINA
{
    public class BINAWriter : BinaryObjectWriterEx
    {
        private const string _footerSignature = "bvh";

        public BINAHeader Header { get; private set; } = new();

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

        public List<StringPoolEntry> StringPoolEntries = [];

        public BINAWriter(Stream in_stream, Endianness in_endianness = Endianness.Big)
            : base(in_stream, StreamOwnership.Retain, in_endianness, EncodingFactory.ShiftJIS)
        {
            // Reserve header chunk.
            this.WriteNullBytes(BINAHeader.Size);
        }

        public long WriteStringOffset(string in_str = null, bool in_writeNullPtrOnEmptyString = true, int in_fieldLength = 4)
        {
            var reserved = Position;

            if (string.IsNullOrEmpty(in_str) && in_writeNullPtrOnEmptyString)
            {
                this.WriteNullBytes(in_fieldLength);
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

        private void WriteStringPool()
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

        private void WriteFooter()
        {
            var offsetTablePos = WriteRelocTable();

            Header.RelocTableOffset = (uint)(offsetTablePos - BINAHeader.Size);
            Header.RelocTableLength = (uint)(Position - offsetTablePos);

            if (Header.HasFooterMagic)
                WriteFooterMagic();

            Header.FileSize = (uint)Position;
        }

        private long WriteRelocTable()
        {
            var pos = Position;
            var lastOffset = (long)BINAHeader.Size;

            foreach (var offset in Offsets)
            {
                var offsetBits = (offset.Value - lastOffset) >> 2;

                if (offsetBits > 0x3FFF)
                {
                    WriteBig((uint)(((byte)BINAOffsetEncoding.ThirtyBit << 24) | offsetBits));
                }
                else if (offsetBits > 0x3F)
                {
                    WriteBig((ushort)(((byte)BINAOffsetEncoding.FourteenBit << 8) | offsetBits));
                }
                else
                {
                    WriteBig((byte)((byte)BINAOffsetEncoding.SixBit | offsetBits));
                }

                lastOffset = offset.Value;
            }

            this.Align(4);

            return pos;
        }

        private void WriteFooterMagic()
        {
            Write(0x10); // TODO: unknown.
            this.WriteNullBytes(4);
            WriteStringNullTerminated(Encoding.UTF8, _footerSignature);
        }

        public void FinishWrite()
        {
            WriteStringPool();
            WriteFooter();

            JumpTo(Header.HeaderOffset);

            Header.Write(this);
        }

        /// <summary>
        /// Alias of <see cref="BinaryObjectWriterEx.WriteReserved{T}(long, T, bool)"/> that defaults <paramref name="in_removeAfterWrite"/> to <b>false</b>.
        /// <para>All fields must remain present for the relocation table to be written last.</para>
        /// </summary>
        public override void WriteReserved<T>(long in_offset, T in_value, bool in_removeAfterWrite = false)
        {
            base.WriteReserved(in_offset, in_value, in_removeAfterWrite);
        }

        /// <summary>
        /// Alias of <see cref="BinaryObjectWriterEx.WriteReserved{T}(string, T, bool)"/> that defaults <paramref name="in_removeAfterWrite"/> to <b>false</b>.
        /// <para>All fields must remain present for the relocation table to be written last.</para>
        /// </summary>
        public override void WriteReserved<T>(string in_name, T in_value, bool in_removeAfterWrite = false)
        {
            base.WriteReserved(in_name, in_value, in_removeAfterWrite);
        }
    }

    public enum BINAOffsetEncoding : byte
    {
        /// <summary>
        /// The offset is stored in the remaining six bits after the type bits.
        /// </summary>
        SixBit = 0x40,

        /// <summary>
        /// The offset is stored in the remaining six bits after the type bits, including an extra byte.
        /// </summary>
        FourteenBit = 0x80,

        /// <summary>
        /// The offset is stored in the remaining six bits after the type bits, including an extra three bytes.
        /// </summary>
        ThirtyBit = 0xC0
    }
}
