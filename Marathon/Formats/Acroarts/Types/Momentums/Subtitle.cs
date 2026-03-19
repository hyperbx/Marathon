using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class Subtitle : IMomentumParamSet
    {
        public string TextBookName { get; set; } = "subtitle";

        public string TextCardName { get; set; }

        public uint UnknownField1 { get; set; }

        public uint UnknownField2 { get; set; }

        public Subtitle() { }

        public Subtitle(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var textBookNameOffset = in_reader.Read<uint>();
            var textCardNameOffset = in_reader.Read<uint>();

            UnknownField1 = in_reader.Read<uint>();
            UnknownField2 = in_reader.Read<uint>();

            if (textBookNameOffset != 0)
            {
                in_reader.ReadAtOffset(in_reader.CalculateOffset(textBookNameOffset),
                    () => TextBookName = MomentumString.Read(in_reader));
            }

            if (textCardNameOffset != 0)
            {
                in_reader.ReadAtOffset(in_reader.CalculateOffset(textCardNameOffset),
                    () => TextCardName = MomentumString.Read(in_reader));
            }
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var textBookNameOffset = in_writer.Reserve<uint>();
            var textCardNameOffset = in_writer.Reserve<uint>();

            in_writer.Write(UnknownField1);
            in_writer.Write(UnknownField2);

            MomentumString.Write(in_writer, TextBookName, textBookNameOffset);
            MomentumString.Write(in_writer, TextCardName, textCardNameOffset);
        }

        public uint GetParamCount()
        {
            return 4;
        }
    }
}
