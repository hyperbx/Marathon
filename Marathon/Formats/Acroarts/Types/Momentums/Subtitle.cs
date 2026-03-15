using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using Marathon.IO.Extensions;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class Subtitle : IMomentumParamSet
    {
        public string TextBookName { get; set; } = "subtitle";

        public string MessageName { get; set; }

        public uint Frame { get; set; }

        public uint UnknownField { get; set; }

        public Subtitle() { }

        public Subtitle(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            var textBookNameOffset = in_reader.Read<uint>();
            var messageNameOffset = in_reader.Read<uint>();

            Frame = in_reader.Read<uint>();
            UnknownField = in_reader.Read<uint>();

            in_reader.ReadAtOffset(in_parentChunk.Offset + textBookNameOffset,
                () => TextBookName = in_reader.ReadStringFixedLength(0x80));

            in_reader.ReadAtOffset(in_parentChunk.Offset + messageNameOffset,
                () => MessageName = in_reader.ReadStringFixedLength(0x80));
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            var textBookNameOffset = in_writer.Reserve<uint>();
            var messageNameOffset = in_writer.Reserve<uint>();

            in_writer.Write(Frame);
            in_writer.Write(UnknownField);

            in_writer.WriteZero<long>();
            in_writer.WriteReserved(textBookNameOffset, (uint)(in_writer.Position - in_parentChunk.Offset), false);
            in_writer.WriteStringFixedLength(TextBookName, 0x80);

            in_writer.WriteZero<long>();
            in_writer.WriteReserved(messageNameOffset, (uint)(in_writer.Position - in_parentChunk.Offset), false);
            in_writer.WriteStringFixedLength(MessageName, 0x80);
        }

        public uint GetParamCount()
        {
            return 4;
        }
    }
}
