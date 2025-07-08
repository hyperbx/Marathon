using Amicitia.IO;
using Amicitia.IO.Binary;
using System.IO;

namespace Marathon.IO.Extensions
{
    public static class WriterExtensions
    {
        public static void WriteSignature(this BinaryObjectWriter in_writer, string in_signature)
        {
            in_writer.WriteStringFixedLength(in_writer.Encoding, in_signature, in_signature.Length);
        }

        public static void WriteNullBytes(this BinaryObjectWriter in_writer, int in_count)
        {
            in_writer.WriteBytes(new byte[in_count]);
        }

        public static void WriteInt24(this BinaryObjectWriter in_writer, int in_value)
        {
            WriteUInt24(in_writer, (uint)in_value);
        }

        public static void WriteUInt24(this BinaryObjectWriter in_writer, uint in_value)
        {
            var buf = new byte[3];

            if (in_writer.Endianness == Endianness.Big)
            {
                buf[0] = (byte)(in_value >> 16);
                buf[1] = (byte)(in_value >> 8);
                buf[2] = (byte)(in_value);
            }
            else
            {
                buf[0] = (byte)(in_value);
                buf[1] = (byte)(in_value >> 8);
                buf[2] = (byte)(in_value >> 16);
            }

            in_writer.WriteBytes(buf);
        }

        public static void WriteStringNullTerminated(this BinaryObjectWriter in_writer, string in_str)
        {
            in_writer.WriteStringNullTerminated(in_writer.Encoding, in_str);
        }

        public static void WriteStringFixedLength(this BinaryObjectWriter in_writer, string in_str, int in_length)
        {
            in_writer.WriteStringFixedLength(in_writer.Encoding, in_str, in_length);
        }

        public static void Align(this BinaryObjectWriter in_writer, int in_alignment)
        {
            in_writer.WriteNullBytes(AlignmentHelper.GetAlignedDifference(in_writer.Position, in_alignment));
        }
    }
}
