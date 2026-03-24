using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class ScaleAccel : IMomentumParamSet
    {
        public Vector3 Scale { get; set; }

        public GTCounter GTCounter { get; set; }

        public ScaleAccel() { }

        public ScaleAccel(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Scale = in_reader.Read<Vector3>();
            GTCounter = in_reader.Read<GTCounter>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Scale);
            in_writer.Write(GTCounter);
        }

        public uint GetParamCount()
        {
            return 4;
        }
    }
}
