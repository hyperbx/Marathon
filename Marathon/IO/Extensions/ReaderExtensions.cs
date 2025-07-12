using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Helpers;
using System.Linq;
using System.Text;

namespace Marathon.IO.Extensions
{
    public static class ReaderExtensions
    {
        public static byte[] ReadBytes(this BinaryValueReader in_reader, int in_count)
        {
            return in_reader.ReadArray<byte>(in_count);
        }

        public static bool CheckSignature(byte[] in_expected, byte[] in_received, bool in_isExceptionOnInvalid = true)
        {
            if (in_expected.SequenceEqual(in_received))
                return true;

            if (in_isExceptionOnInvalid)
                throw new InvalidSignatureException(in_expected, in_received);

            return false;
        }

        public static bool CheckSignature(this BinaryValueReader in_reader, byte[] in_expected, bool in_isExceptionOnInvalid = true)
        {
            return CheckSignature(in_expected, in_reader.ReadBytes(in_expected.Length), in_isExceptionOnInvalid);
        }

        public static bool CheckSignature<T>(T in_expected, T in_received, bool in_isExceptionOnInvalid = true) where T : unmanaged
        {
            if (in_expected.Equals(in_received))
                return true;

            if (in_isExceptionOnInvalid)
                throw new InvalidSignatureException(in_expected, in_received);

            return false;
        }

        public static bool CheckSignature<T>(this BinaryValueReader in_reader, T in_expected, bool in_isExceptionOnInvalid = true) where T : unmanaged
        {
            return CheckSignature(in_expected, in_reader.Read<T>(), in_isExceptionOnInvalid);
        }

        public static bool CheckSignature(string in_expected, string in_received, bool in_isExceptionOnInvalid = true)
        {
            if (in_expected.Equals(in_received))
                return true;

            if (in_isExceptionOnInvalid)
                throw new InvalidSignatureException(in_expected, in_received);

            return false;
        }

        public static bool CheckSignature(this BinaryValueReader in_reader, string in_expected, bool in_isExceptionOnInvalid = true)
        {
            var str = in_reader.ReadString(StringBinaryFormat.FixedLength, in_expected.Length);

            return CheckSignature(in_expected, str, in_isExceptionOnInvalid);
        }

        public static Endianness GetEndiannessFromSignature<T>(this BinaryValueReader in_reader, T in_expected, T in_received) where T : unmanaged
        {
            var expectedReversed = BinaryHelper.SwapEndianness(in_expected);

            if (in_received.Equals(expectedReversed))
            {
                return in_reader.Endianness == Endianness.Little
                    ? Endianness.Big
                    : Endianness.Little;
            }

            return in_reader.Endianness == Endianness.Big
                ? Endianness.Little
                : Endianness.Big;
        }

        public static Endianness GetEndiannessFromSignature<T>(this BinaryValueReader in_reader, T in_expected) where T : unmanaged
        {
            return in_reader.GetEndiannessFromSignature(in_reader.Read<T>(), in_expected);
        }

        public static int ReadInt24(this BinaryValueReader in_reader)
        {
            var buf = in_reader.ReadBytes(3);

            return in_reader.Endianness == Endianness.Big ?
                   buf[0] << 16 | buf[1] << 8 | buf[2] :
                   buf[2] << 16 | buf[1] << 8 | buf[0];
        }

        public static uint ReadUInt24(this BinaryValueReader in_reader)
        {
            var buf = in_reader.ReadBytes(3);

            return in_reader.Endianness == Endianness.Big ?
                   ((uint)buf[0] << 16 | (uint)buf[1] << 8 | buf[2]) :
                   ((uint)buf[2] << 16 | (uint)buf[1] << 8 | buf[0]);
        }

        public static string ReadStringNullTerminated(this BinaryValueReader in_reader, Encoding in_encoding = null)
        {
            return in_reader.ReadString(in_encoding ?? in_reader.Encoding, StringBinaryFormat.NullTerminated);
        }

        public static string ReadStringFixedLength(this BinaryValueReader in_reader, int in_length, Encoding in_encoding = null)
        {
            return in_reader.ReadString(in_encoding ?? in_reader.Encoding, StringBinaryFormat.FixedLength, in_length);
        }
    }
}
