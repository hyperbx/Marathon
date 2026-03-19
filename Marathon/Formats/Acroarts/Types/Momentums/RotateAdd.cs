using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class RotateAdd : IMomentumParamSet
    {
        public Vector4 Rotation { get; set; }

        public RotateAdd() { }

        public RotateAdd(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Rotation = in_reader.Read<Vector4>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Rotation);
        }

        public uint GetParamCount()
        {
            return 4;
        }
    }
}
