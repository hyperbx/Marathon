using Marathon.IO;
using Marathon.IO.Extensions;
using System;
using System.IO;

namespace Marathon.Formats.Acroarts.Types
{
    public class FixedString
    {
        public static string Read(BinaryObjectReaderEx in_reader)
        {
            var pos = in_reader.Position;
            var result = in_reader.ReadStringFixedLength(0x80);

            if (result.Length > 0x0F)
            {
                in_reader.Seek(pos, SeekOrigin.Begin);
                result = in_reader.ReadStringFixedLength(0x100);
            }

            return result;
        }

        public static void Write(BinaryObjectWriterEx in_writer, string in_value, Action in_preWriteStringAction = null)
        {
            in_writer.WriteZero<long>();
            in_preWriteStringAction?.Invoke();
            in_writer.WriteStringFixedLength(in_value, GetFixedLength(in_value));
        }

        public static void Write(BinaryObjectWriterEx in_writer, string in_value, long in_reservedOffset)
        {
            Write(in_writer, in_value, () =>
            {
                in_writer.WriteReserved(in_reservedOffset, (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative));
            });
        }

        public static int GetFixedLength(string in_value)
        {
            return in_value.Length > 0x0F ? 0x100 : 0x80;
        }
    }
}
