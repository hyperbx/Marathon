using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class MaterialColorGoal : IMomentumParamSet
    {
        public uint UnknownField1 { get; set; }

        public uint UnknownField2 { get; set; }

        public uint UnknownField3 { get; set; }

        public uint UnknownField4 { get; set; }

        public uint UnknownField5 { get; set; }

        public uint UnknownField6 { get; set; }

        public uint UnknownField7 { get; set; }

        public MaterialColorGoal() { }

        public MaterialColorGoal(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            UnknownField1 = in_reader.Read<uint>();
            UnknownField2 = in_reader.Read<uint>();
            UnknownField3 = in_reader.Read<uint>();
            UnknownField4 = in_reader.Read<uint>();
            UnknownField5 = in_reader.Read<uint>();
            UnknownField6 = in_reader.Read<uint>();
            UnknownField7 = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            in_writer.Write(UnknownField1);
            in_writer.Write(UnknownField2);
            in_writer.Write(UnknownField3);
            in_writer.Write(UnknownField4);
            in_writer.Write(UnknownField5);
            in_writer.Write(UnknownField6);
            in_writer.Write(UnknownField7);
        }

        public uint GetParamCount()
        {
            return 7;
        }
    }
}
