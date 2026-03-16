using Marathon.Formats.Acroarts.Chunks;
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

        public TranslateNormal(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Position = in_reader.Read<Vector3>();
            VectorType = in_reader.Read<TranslateVectorType>();
            Parameter = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
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
