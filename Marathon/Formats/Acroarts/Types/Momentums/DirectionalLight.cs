using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class DirectionalLight : IMomentumParamSet
    {
        public uint UnknownField { get; set; }

        public DirectionalLight() { }

        public DirectionalLight(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            UnknownField = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(UnknownField);
        }

        public uint GetParamCount()
        {
            return 1;
        }
    }
}
