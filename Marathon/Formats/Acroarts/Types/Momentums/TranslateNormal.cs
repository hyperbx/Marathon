using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class TranslateNormal : IMomentumParamSet
    {
        public Vector3 Position { get; set; }

        public TranslateVectorType VectorType { get; set; }

        public uint Parameter { get; set; }

        public TranslateNormal() { }

        public TranslateNormal(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Position = in_reader.Read<Vector3>();
            VectorType = in_reader.Read<TranslateVectorType>();
            Parameter = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Position);
            in_writer.Write(VectorType);
            in_writer.Write(Parameter);
        }

        public uint GetParamCount()
        {
            return 5;
        }
    }
}
