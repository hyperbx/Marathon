using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class FilterColorCorrection : IMomentumParamSet
    {
        public float UnknownField1 { get; set; }

        public float UnknownField2 { get; set; }

        public float UnknownField3 { get; set; }

        public float UnknownField4 { get; set; }

        public FilterColorCorrection() { }

        public FilterColorCorrection(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            UnknownField1 = in_reader.Read<float>();
            UnknownField2 = in_reader.Read<float>();
            UnknownField3 = in_reader.Read<float>();
            UnknownField4 = in_reader.Read<float>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(UnknownField1);
            in_writer.Write(UnknownField2);
            in_writer.Write(UnknownField3);
            in_writer.Write(UnknownField4);
        }

        public uint GetParamCount()
        {
            return 4;
        }
    }
}
