using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class ScaleNormal : IMomentumParamSet
    {
        public Vector3 Scale { get; set; }

        public ScaleNormal() { }

        public ScaleNormal(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Scale = in_reader.Read<Vector3>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Scale);
        }

        public uint GetParamCount()
        {
            return 3;
        }
    }
}
