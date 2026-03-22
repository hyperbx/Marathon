using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class PointLight : IMomentumParamSet
    {
        public uint UnknownField1 { get; set; }

        public float UnknownField2 { get; set; }

        public float UnknownField3 { get; set; }

        public uint UnknownField4 { get; set; }

        public uint UnknownField5 { get; set; }

        public PointLight() { }

        public PointLight(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            UnknownField1 = in_reader.Read<uint>();
            UnknownField2 = in_reader.Read<float>();
            UnknownField3 = in_reader.Read<float>();
            UnknownField4 = in_reader.Read<uint>();
            UnknownField5 = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(UnknownField1);
            in_writer.Write(UnknownField2);
            in_writer.Write(UnknownField3);
            in_writer.Write(UnknownField4);
            in_writer.Write(UnknownField5);
        }

        public uint GetParamCount()
        {
            return 5;
        }
    }
}
