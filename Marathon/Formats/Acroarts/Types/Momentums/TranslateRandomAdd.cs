using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class TranslateRandomAdd : IMomentumParamSet
    {
        public Vector3 Position { get; set; }

        public Vector3 Add { get; set; }

        public uint UnknownField { get; set; }

        public GTCounter GTCounter { get; set; }

        public TranslateVectorType VectorType { get; set; }

        public uint Parameter { get; set; }

        public TranslateRandomAdd() { }

        public TranslateRandomAdd(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Position = in_reader.Read<Vector3>();
            Add = in_reader.Read<Vector3>();
            UnknownField = in_reader.Read<uint>();
            GTCounter = in_reader.Read<GTCounter>();
            VectorType = in_reader.Read<TranslateVectorType>();
            Parameter = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            in_writer.Write(Position);
            in_writer.Write(Add);
            in_writer.Write(UnknownField);
            in_writer.Write(GTCounter);
            in_writer.Write(VectorType);
            in_writer.Write(Parameter);
        }

        public uint GetParamCount()
        {
            return 10;
        }
    }
}
