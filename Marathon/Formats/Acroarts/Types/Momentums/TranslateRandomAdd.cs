using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class TranslateRandomAdd : IMomentumParamSet
    {
        public Vector3 Position { get; set; }

        public Vector3 Add { get; set; }

        public uint UnknownField1 { get; set; }

        public uint UnknownField2 { get; set; }

        public uint UnknownField3 { get; set; }

        public uint UnknownField4 { get; set; }

        public TranslateRandomAdd() { }

        public TranslateRandomAdd(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Position = in_reader.Read<Vector3>();
            Add = in_reader.Read<Vector3>();
            UnknownField1 = in_reader.Read<uint>();
            UnknownField2 = in_reader.Read<uint>();
            UnknownField3 = in_reader.Read<uint>();
            UnknownField4 = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            in_writer.Write(Position);
            in_writer.Write(Add);
            in_writer.Write(UnknownField1);
            in_writer.Write(UnknownField2);
            in_writer.Write(UnknownField3);
            in_writer.Write(UnknownField4);
        }

        public uint GetParamCount()
        {
            return 10;
        }
    }
}
