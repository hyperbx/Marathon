using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.Extensions;
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
            /// A collection of field names that will point to this string.
            /// </summary>
            public List<string> FieldNames { get; set; } = [];

            /// <summary>
            /// The string to be written to the string pool.
            /// </summary>
            public string Data { get; set; } = in_str;
        }

        public List<StringPoolEntry> StringPoolEntries = [];

        public BINAWriter(Stream in_stream, bool in_isBigEndian = true)
            : base(in_stream, StreamOwnership.Retain, in_isBigEndian ? Endianness.Big : Endianness.Little, Encoding.ShiftJIS)
        {
            // Reserve header chunk.
            this.WriteNullBytes(BINAHeader.Size);
        }

        public void CreateStringField(string in_fieldName, string in_str = null, bool in_writeNullPtrOnEmptyString = true, int in_fieldLength = 4)
        {
            if (string.IsNullOrEmpty(in_fieldName))
                return;

            if (string.IsNullOrEmpty(in_str) && in_writeNullPtrOnEmptyString)
            {
                this.WriteNullBytes(in_fieldLength);
                return;
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

            CreateNamedField(in_fieldName, in_fieldLength);

            newEntry.FieldNames.Add(in_fieldName);

            if (isNewEntry)
                StringPoolEntries.Add(newEntry);
        }

        private void WriteStringPool()
        {
            foreach (var entry in StringPoolEntries)
            {
                // Write all offsets that point to this string in the file.
                foreach (var offsetName in entry.FieldNames)
                    WriteNamedField(offsetName, (uint)Position - BINAHeader.Size);

                this.WriteStringNullTerminated(entry.Data);
            }

            this.Align(4);
        }

        private void WriteFooter()
        {
            var offsetTablePos = WriteOffsetTable();

            Header.OffsetTableOffset = (uint)(offsetTablePos - BINAHeader.Size);
            Header.OffsetTableLength = (uint)(Position - offsetTablePos);

            if (Header.HasFooterMagic)
                WriteFooterMagic();

            Header.FileSize = (uint)Position;
        }

        private long WriteOffsetTable()
        {
            var pos = Position;
            var pointerOffset = (long)BINAHeader.Size;

            foreach (var field in Fields)
            {
                var offsetBits = (field.Value - pointerOffset) >> 2;

                if (offsetBits <= 0x3F)
                {
                    WriteBig((byte)((byte)BINAOffsetEncoding.SixBit | offsetBits));
                }
                else if (offsetBits <= 0x3FFF)
                {
                    WriteBig((ushort)(((byte)BINAOffsetEncoding.FourteenBit << 8) | offsetBits));
                }
                else
                {
                    WriteBig((uint)(((byte)BINAOffsetEncoding.ThirtyBit << 24) | offsetBits));
                }

                pointerOffset = field.Value;
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
        /// Alias of <see cref="BinaryObjectWriterEx.WriteNamedField{T}(string, T, bool)"/> that defaults <paramref name="in_removeField"/> to <b>false</b>.
        /// <para>All fields must remain present for the offset table to be written last.</para>
        /// </summary>
        public override void WriteNamedField<T>(string in_name, T in_value, bool in_removeField = false)
        {
            base.WriteNamedField(in_name, in_value, in_removeField);
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
