using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class TranslateRandomNormal : IMomentumParamSet
    {
        public Vector3 Vector { get; set; }

        public Vector3 Random { get; set; }

        public GTCounter GTCounter { get; set; }

        public TranslateVectorType VectorType { get; set; }

        public uint Parameter { get; set; }

        public TranslateRandomNormal() { }

        public TranslateRandomNormal(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Vector = in_reader.Read<Vector3>();
            Random = in_reader.Read<Vector3>();
            GTCounter = in_reader.Read<GTCounter>();
            VectorType = in_reader.Read<TranslateVectorType>();
            Parameter = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Vector);
            in_writer.Write(Random);
            in_writer.Write(GTCounter);
            in_writer.Write(VectorType);
            in_writer.Write(Parameter);
        }

        public uint GetParamCount()
        {
            return 9;
        }
    }
}
