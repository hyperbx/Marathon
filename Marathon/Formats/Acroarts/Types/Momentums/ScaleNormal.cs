using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class ScaleNormal : IMomentumParamSet
    {
        public Vector3 Scale { get; set; }

        public ScaleNormal() { }

        public ScaleNormal(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Scale = in_reader.Read<Vector3>();
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            in_writer.Write(Scale);
        }

        public uint GetParamCount()
        {
            return 3;
        }
    }
}
