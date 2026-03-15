using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class DirectionalLight : IMomentumParamSet
    {
        public uint UnknownField { get; set; }

        public DirectionalLight() { }

        public DirectionalLight(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            UnknownField = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            in_writer.Write(UnknownField);
        }

        public uint GetParamCount()
        {
            return 1;
        }
    }
}
