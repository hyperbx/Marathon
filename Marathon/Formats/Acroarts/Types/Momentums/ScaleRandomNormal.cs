using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class ScaleRandomNormal : IMomentumParamSet
    {
        public Vector3 Scale { get; set; }

        public Vector3 Random { get; set; }

        public GTCounter GTCounter { get; set; }

        public bool UseUniformScale { get; set; }

        public ScaleRandomNormal() { }

        public ScaleRandomNormal(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Scale = in_reader.Read<Vector3>();
            Random = in_reader.Read<Vector3>();
            GTCounter = in_reader.Read<GTCounter>();
            UseUniformScale = in_reader.ReadBoolean<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Scale);
            in_writer.Write(Random);
            in_writer.Write(GTCounter);
            in_writer.WriteBoolean<uint>(UseUniformScale);
        }

        public uint GetParamCount()
        {
            return 8;
        }
    }
}
