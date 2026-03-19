using Marathon.IO;
using Marathon.IO.Extensions;
using System;
using System.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class MomentumString
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
            if (string.IsNullOrEmpty(in_value))
            {
                in_preWriteStringAction?.Invoke();
                return;
            }

            in_writer.WriteZero<long>();
            in_preWriteStringAction?.Invoke();
            in_writer.WriteStringFixedLength(in_value, GetFixedLength(in_value));
        }

        public static void Write(BinaryObjectWriterEx in_writer, string in_value, long in_reservedOffset)
        {
            Write(in_writer, in_value, () =>
            {
                var offset = 0U;

                if (!string.IsNullOrEmpty(in_value))
                    offset = (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative);

                in_writer.WriteReserved(in_reservedOffset, offset, offset == 0);
            });
        }

        public static int GetFixedLength(string in_value)
        {
            return in_value.Length > 0x0F ? 0x100 : 0x80;
        }
    }
}
