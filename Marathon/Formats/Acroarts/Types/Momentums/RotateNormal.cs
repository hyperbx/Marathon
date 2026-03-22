using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class RotateNormal : IMomentumParamSet
    {
        public Vector3 Rotation { get; set; }

        public RotateNormal() { }

        public RotateNormal(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Rotation = in_reader.Read<Vector3>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Rotation);
        }

        public uint GetParamCount()
        {
            return 3;
        }
    }
}
