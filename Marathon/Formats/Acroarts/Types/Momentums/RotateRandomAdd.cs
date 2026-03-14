using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class RotateRandomAdd : IMomentumParamSet
    {
        public Vector3 Rotation { get; set; }

        public Vector3 Add { get; set; }

        public uint UnknownField1 { get; set; }

        public uint UnknownField2 { get; set; }

        public RotateRandomAdd() { }

        public RotateRandomAdd(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Rotation = in_reader.Read<Vector3>();
            Add = in_reader.Read<Vector3>();
            UnknownField1 = in_reader.Read<uint>();
            UnknownField2 = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            in_writer.Write(Rotation);
            in_writer.Write(Add);
            in_writer.Write(UnknownField1);
            in_writer.Write(UnknownField2);
        }

        public uint GetParamCount()
        {
            return 8;
        }
    }
}
