using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class ScaleAdd : IMomentumParamSet
    {
        public Vector4 Scale { get; set; }

        public ScaleAdd() { }

        public ScaleAdd(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Scale = in_reader.Read<Vector4>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Scale);
        }

        public uint GetParamCount()
        {
            return 4;
        }
    }
}
