using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class BlurBelt : IMomentumParamSet
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

        public uint UnknownField10 { get; set; }

        public uint UnknownField11 { get; set; }

        public float UnknownField12 { get; set; }

        public float UnknownField13 { get; set; }

        public float UnknownField14 { get; set; }

        public uint UnknownField15 { get; set; }

        public uint UnknownField16 { get; set; }

        public float UnknownField17 { get; set; }

        public uint UnknownField18 { get; set; }

        public uint UnknownField19 { get; set; }

        public uint UnknownField20 { get; set; }

        public int UnknownField21 { get; set; }

        public uint UnknownField22 { get; set; }

        public float UnknownField23 { get; set; }

        public float UnknownField24 { get; set; }

        public float UnknownField25 { get; set; }

        public float UnknownField26 { get; set; }

        public uint UnknownField27 { get; set; }

        public float UnknownField28 { get; set; }

        public uint UnknownField29 { get; set; }

        public uint UnknownField30 { get; set; }

        public uint UnknownField31 { get; set; }

        public uint UnknownField32 { get; set; }

        public float UnknownField33 { get; set; }

        public uint UnknownField34 { get; set; }

        public float UnknownField35 { get; set; }

        public uint UnknownField36 { get; set; }

        public float UnknownField37 { get; set; }

        public BlurBelt() { }

        public BlurBelt(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
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
            UnknownField10 = in_reader.Read<uint>();
            UnknownField11 = in_reader.Read<uint>();
            UnknownField12 = in_reader.Read<float>();
            UnknownField13 = in_reader.Read<float>();
            UnknownField14 = in_reader.Read<float>();
            UnknownField15 = in_reader.Read<uint>();
            UnknownField16 = in_reader.Read<uint>();
            UnknownField17 = in_reader.Read<float>();
            UnknownField18 = in_reader.Read<uint>();
            UnknownField19 = in_reader.Read<uint>();
            UnknownField20 = in_reader.Read<uint>();
            UnknownField21 = in_reader.Read<int>();
            UnknownField22 = in_reader.Read<uint>();
            UnknownField23 = in_reader.Read<float>();
            UnknownField24 = in_reader.Read<float>();
            UnknownField25 = in_reader.Read<float>();
            UnknownField26 = in_reader.Read<float>();
            UnknownField27 = in_reader.Read<uint>();
            UnknownField28 = in_reader.Read<float>();
            UnknownField29 = in_reader.Read<uint>();
            UnknownField30 = in_reader.Read<uint>();
            UnknownField31 = in_reader.Read<uint>();
            UnknownField32 = in_reader.Read<uint>();
            UnknownField33 = in_reader.Read<float>();
            UnknownField34 = in_reader.Read<uint>();
            UnknownField35 = in_reader.Read<float>();
            UnknownField36 = in_reader.Read<uint>();
            UnknownField37 = in_reader.Read<float>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
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
            in_writer.Write(UnknownField29);
            in_writer.Write(UnknownField30);
            in_writer.Write(UnknownField31);
            in_writer.Write(UnknownField32);
            in_writer.Write(UnknownField33);
            in_writer.Write(UnknownField34);
            in_writer.Write(UnknownField35);
            in_writer.Write(UnknownField36);
            in_writer.Write(UnknownField37);
        }

        public uint GetParamCount()
        {
            return 37;
        }
    }
}
