using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class ScaleRandomNormal : IMomentumParamSet
    {
        public Vector3 Scale { get; set; }

        public Vector3 Random { get; set; }

        public uint UnknownField1 { get; set; }

        public uint UnknownField2 { get; set; }

        public ScaleRandomNormal() { }

        public ScaleRandomNormal(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Scale = in_reader.Read<Vector3>();
            Random = in_reader.Read<Vector3>();
            UnknownField1 = in_reader.Read<uint>();
            UnknownField2 = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Scale);
            in_writer.Write(Random);
            in_writer.Write(UnknownField1);
            in_writer.Write(UnknownField2);
        }

        public uint GetParamCount()
        {
            return 8;
        }
    }
}
