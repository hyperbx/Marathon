using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using Marathon.IO.Extensions;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class Subtitle : IMomentumParamSet
    {
        public string TextBookName { get; set; } = "subtitle";

        public string TextCardName { get; set; }

        public uint UnknownField1 { get; set; }

        public uint UnknownField2 { get; set; }

        public Subtitle() { }

        public Subtitle(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            var textBookNameOffset = in_reader.Read<uint>();
            var textCardNameOffset = in_reader.Read<uint>();

            UnknownField1 = in_reader.Read<uint>();
            UnknownField2 = in_reader.Read<uint>();

            in_reader.ReadAtOffset(in_parentChunk.Offset + textBookNameOffset,
                () => TextBookName = in_reader.ReadStringFixedLength(0x80));

            in_reader.ReadAtOffset(in_parentChunk.Offset + textCardNameOffset,
                () => TextCardName = in_reader.ReadStringFixedLength(0x80));
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            var textBookNameOffset = in_writer.Reserve<uint>();
            var textCardNameOffset = in_writer.Reserve<uint>();

            in_writer.Write(UnknownField1);
            in_writer.Write(UnknownField2);

            in_writer.WriteZero<long>();
            in_writer.WriteReserved(textBookNameOffset, (uint)(in_writer.Position - in_parentChunk.Offset), false);
            in_writer.WriteStringFixedLength(TextBookName, 0x80);

            in_writer.WriteZero<long>();
            in_writer.WriteReserved(textCardNameOffset, (uint)(in_writer.Position - in_parentChunk.Offset), false);
            in_writer.WriteStringFixedLength(TextCardName, 0x80);
        }

        public uint GetParamCount()
        {
            return 4;
        }
    }
}
