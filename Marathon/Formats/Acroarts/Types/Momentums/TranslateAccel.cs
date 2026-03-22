using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class TranslateAccel : IMomentumParamSet
    {
        public Vector3 Vector { get; set; }

        public Vector3 BaseVector { get; set; }

        public TranslateAccel() { }

        public TranslateAccel(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Vector = in_reader.Read<Vector3>();
            BaseVector = in_reader.Read<Vector3>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Vector);
            in_writer.Write(BaseVector);
        }

        public uint GetParamCount()
        {
            return 6;
        }
    }
}
