using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class ScaleAdd : IMomentumParamSet
    {
        public Vector4 Scale { get; set; }

        public ScaleAdd() { }

        public ScaleAdd(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Scale = in_reader.Read<Vector4>();
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            in_writer.Write(Scale);
        }

        public uint GetParamCount()
        {
            return 4;
        }
    }
}
