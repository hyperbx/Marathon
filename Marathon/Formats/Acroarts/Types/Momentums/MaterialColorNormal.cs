using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class MaterialColorNormal : IMomentumParamSet
    {
        public Colour<float, ARGB> Colour { get; set; }

        public MaterialColorNormal() { }

        public MaterialColorNormal(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Colour = in_reader.ReadObject<Colour<float, ARGB>>();
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            in_writer.WriteObject(Colour);
            in_writer.WriteZero<long>();
        }

        public uint GetParamCount()
        {
            return 6;
        }
    }
}
