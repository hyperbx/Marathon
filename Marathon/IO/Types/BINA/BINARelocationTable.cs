using Amicitia.IO.Binary;
using Marathon.IO.Extensions;

namespace Marathon.IO.Types.BINA
{
    public class BINARelocationTable(long in_offset) : RelocationTable<uint>(in_offset)
    {
        public override long Read(BinaryObjectReaderEx in_reader, long in_length)
        {
            var lastOffset = Offset;
            var end = in_reader.Position + in_length;

            Clear();

            while (in_reader.Position < in_reader.Length && in_reader.Position < end)
            {
                var b = in_reader.ReadByte();
                var type = (byte)(b & 0xC0);
                var offset = (byte)(b & 0x3F);

                if (type == (byte)BINAOffsetEncoding.SixBit)
                {
                    offset <<= 2;

                    Add((uint)(offset + lastOffset));
                }
                else if (type == (byte)BINAOffsetEncoding.FourteenBit)
                {
                    var remainder = in_reader.ReadByte();
                    var decoded = (ushort)(((offset << 8) | remainder) << 2);

                    Add((uint)(decoded + lastOffset));
                }
                else if (type == (byte)BINAOffsetEncoding.ThirtyBit)
                {
                    var remainder = in_reader.ReadBytes(3);
                    var decoded = (uint)(((offset << 24) | (remainder[0] << 16) | (remainder[1] << 8) | remainder[2]) << 2);

                    Add((uint)(decoded + lastOffset));
                }
                else
                {
                    break;
                }

                lastOffset = this[Count - 1];
            }

            return lastOffset;
        }

        public override long Write(BinaryObjectWriterEx in_writer)
        {
            var pos = in_writer.Position;
            var lastOffset = BINAHeader.Size + Offset;

            foreach (var offset in this)
            {
                var offsetBits = (offset - lastOffset) >> 2;

                if (offsetBits > 0x3FFF)
                {
                    in_writer.WriteBig((uint)(((byte)BINAOffsetEncoding.ThirtyBit << 24) | offsetBits));
                }
                else if (offsetBits > 0x3F)
                {
                    in_writer.WriteBig((ushort)(((byte)BINAOffsetEncoding.FourteenBit << 8) | offsetBits));
                }
                else
                {
                    in_writer.WriteBig((byte)((byte)BINAOffsetEncoding.SixBit | offsetBits));
                }

                lastOffset = offset;
            }

            in_writer.Align(4);

            Length = in_writer.Position - pos;

            return pos;
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
