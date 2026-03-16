using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class ScaleRandomAdd : IMomentumParamSet
    {
        public Vector3 Scale { get; set; }

        public Vector3 Add { get; set; }

        public uint UnknownField1 { get; set; }

        public GTCounter GTCounter { get; set; }

        public uint UnknownField2 { get; set; }

        public ScaleRandomAdd() { }

        public ScaleRandomAdd(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Scale = in_reader.Read<Vector3>();
            Add = in_reader.Read<Vector3>();
            UnknownField1 = in_reader.Read<uint>();
            GTCounter = in_reader.Read<GTCounter>();
            UnknownField2 = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            in_writer.Write(Scale);
            in_writer.Write(Add);
            in_writer.Write(UnknownField1);
            in_writer.Write(GTCounter);
            in_writer.Write(UnknownField2);
        }

        public uint GetParamCount()
        {
            return 9;
        }
    }
}
