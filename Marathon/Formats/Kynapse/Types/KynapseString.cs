using Amicitia.IO.Binary;
using Marathon.IO;
using Marathon.IO.Extensions;
using System;

namespace Marathon.Formats.Kynapse.Types
{
    public class KynapseString
    {
        public static string Read(BinaryObjectReaderEx in_reader, int in_fixedLength = -1)
        {
            var length = in_reader.Read<int>();

            if (in_fixedLength > 0)
                length = Math.Min(length, in_fixedLength);

            return in_reader.ReadStringFixedLength(length);
        }

        public static void Write(BinaryObjectWriterEx in_writer, string in_value, int in_fixedLength = -1)
        {
            if (string.IsNullOrEmpty(in_value))
            {
                in_writer.Write(0);
                return;
            }

            var str = in_value;

            if (str.Length > in_fixedLength && in_fixedLength > 0)
                str = str[..in_fixedLength];

            in_writer.WriteString(StringBinaryFormat.PrefixedLength32, str);
        }
    }
}
