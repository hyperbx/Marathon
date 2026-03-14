using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class BillboardTail : IMomentumParamSet
    {
        public uint UnknownField1 { get; set; }

        public float UnknownField2 { get; set; }

        public float UnknownField3 { get; set; }

        public float UnknownField4 { get; set; }

        public uint UnknownField5 { get; set; }

        public uint UnknownField6 { get; set; }

        public uint UnknownField7 { get; set; }

        public uint UnknownField8 { get; set; }

        public uint UnknownField9 { get; set; }

        public int UnknownField10 { get; set; }

        public float UnknownField11 { get; set; }

        public float UnknownField12 { get; set; }

        public float UnknownField13 { get; set; }

        public float UnknownField14 { get; set; }

        public float UnknownField15 { get; set; }

        public uint UnknownField16 { get; set; }

        public uint UnknownField17 { get; set; }

        public float UnknownField18 { get; set; }

        public float UnknownField19 { get; set; }

        public float UnknownField20 { get; set; }

        public float UnknownField21 { get; set; }

        public uint UnknownField22 { get; set; }

        public float UnknownField23 { get; set; }

        public uint UnknownField24 { get; set; }

        public float UnknownField25 { get; set; }

        public uint UnknownField26 { get; set; }

        public float UnknownField27 { get; set; }

        public uint UnknownField28 { get; set; }

        public BillboardTail() { }

        public BillboardTail(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            UnknownField1 = in_reader.Read<uint>();
            UnknownField2 = in_reader.Read<float>();
            UnknownField3 = in_reader.Read<float>();
            UnknownField4 = in_reader.Read<float>();
            UnknownField5 = in_reader.Read<uint>();
            UnknownField6 = in_reader.Read<uint>();
            UnknownField7 = in_reader.Read<uint>();
            UnknownField8 = in_reader.Read<uint>();
            UnknownField9 = in_reader.Read<uint>();
            UnknownField10 = in_reader.Read<int>();
            UnknownField11 = in_reader.Read<float>();
            UnknownField12 = in_reader.Read<float>();
            UnknownField13 = in_reader.Read<float>();
            UnknownField14 = in_reader.Read<float>();
            UnknownField15 = in_reader.Read<float>();
            UnknownField16 = in_reader.Read<uint>();
            UnknownField17 = in_reader.Read<uint>();
            UnknownField18 = in_reader.Read<float>();
            UnknownField19 = in_reader.Read<float>();
            UnknownField20 = in_reader.Read<float>();
            UnknownField21 = in_reader.Read<float>();
            UnknownField22 = in_reader.Read<uint>();
            UnknownField23 = in_reader.Read<float>();
            UnknownField24 = in_reader.Read<uint>();
            UnknownField25 = in_reader.Read<float>();
            UnknownField26 = in_reader.Read<uint>();
            UnknownField27 = in_reader.Read<float>();
            UnknownField28 = in_reader.Read<uint>();
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
            in_writer.Write(UnknownField8);
            in_writer.Write(UnknownField9);
            in_writer.Write(UnknownField10);
            in_writer.Write(UnknownField11);
            in_writer.Write(UnknownField12);
            in_writer.Write(UnknownField13);
            in_writer.Write(UnknownField14);
            in_writer.Write(UnknownField15);
            in_writer.Write(UnknownField16);
            in_writer.Write(UnknownField17);
            in_writer.Write(UnknownField18);
            in_writer.Write(UnknownField19);
            in_writer.Write(UnknownField20);
            in_writer.Write(UnknownField21);
            in_writer.Write(UnknownField22);
            in_writer.Write(UnknownField23);
            in_writer.Write(UnknownField24);
            in_writer.Write(UnknownField25);
            in_writer.Write(UnknownField26);
            in_writer.Write(UnknownField27);
            in_writer.Write(UnknownField28);
        }

        public uint GetParamCount()
        {
            return 28;
        }
    }
}
