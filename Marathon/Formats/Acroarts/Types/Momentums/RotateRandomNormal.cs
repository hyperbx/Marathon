using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class RotateRandomNormal : IMomentumParamSet
    {
        public Vector3 Rotation { get; set; }

        public Vector3 Random { get; set; }

        public uint UnknownField { get; set; }

        public RotateRandomNormal() { }

        public RotateRandomNormal(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Rotation = in_reader.Read<Vector3>();
            Random = in_reader.Read<Vector3>();
            UnknownField = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            in_writer.Write(Rotation);
            in_writer.Write(Random);
            in_writer.Write(UnknownField);
        }

        public uint GetParamCount()
        {
            return 7;
        }
    }
}
