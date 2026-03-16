using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class ShadowOn : IMomentumParamSet
    {
        public bool On { get; set; }

        public ShadowOn() { }

        public ShadowOn(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            On = in_reader.Read<uint>() != 0;
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            in_writer.Write(On ? 1 : 0);
        }

        public uint GetParamCount()
        {
            return 1;
        }
    }
}
