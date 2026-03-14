using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using Marathon.IO.Extensions;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class ScaleRandomAdd : IMomentumParamSet
    {
        public Vector3 Scale { get; set; }

        public Vector3 Add { get; set; }
        
        public uint UnknownField { get; set; }

        public ScaleRandomAdd() { }

        public ScaleRandomAdd(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Scale = in_reader.Read<Vector3>();
            Add = in_reader.Read<Vector3>();
            UnknownField = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            in_writer.Write(Scale);
            in_writer.Write(Add);
            in_writer.Write(UnknownField);
            in_writer.WriteZero<long>();
        }

        public uint GetParamCount()
        {
            return 9;
        }
    }
}
