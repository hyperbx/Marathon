using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class TranslateRandomSin : IMomentumParamSet
    {
        public Vector3 Vector { get; set; }

        public uint UnknownField4 { get; set; }

        public uint UnknownField5 { get; set; }

        public uint UnknownField6 { get; set; }

        public float UnknownField7 { get; set; }

        public float UnknownField8 { get; set; }

        public float UnknownField9 { get; set; }

        public float UnknownField10 { get; set; }

        public uint UnknownField11 { get; set; }

        public uint UnknownField12 { get; set; }

        public uint UnknownField13 { get; set; }

        public uint UnknownField14 { get; set; }

        public uint UnknownField15 { get; set; }

        public uint UnknownField16 { get; set; }

        public uint UnknownField17 { get; set; }

        public GTCounter GTCounter { get; set; }

        public TranslateVectorType VectorType { get; set; }

        public int Parameter { get; set; }

        public TranslateRandomSin() { }

        public TranslateRandomSin(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Vector = in_reader.Read<Vector3>();
            UnknownField4 = in_reader.Read<uint>();
            UnknownField5 = in_reader.Read<uint>();
            UnknownField6 = in_reader.Read<uint>();
            UnknownField7 = in_reader.Read<float>();
            UnknownField8 = in_reader.Read<float>();
            UnknownField9 = in_reader.Read<float>();
            UnknownField10 = in_reader.Read<float>();
            UnknownField11 = in_reader.Read<uint>();
            UnknownField12 = in_reader.Read<uint>();
            UnknownField13 = in_reader.Read<uint>();
            UnknownField14 = in_reader.Read<uint>();
            UnknownField15 = in_reader.Read<uint>();
            UnknownField16 = in_reader.Read<uint>();
            UnknownField17 = in_reader.Read<uint>();
            GTCounter = in_reader.Read<GTCounter>();
            VectorType = in_reader.Read<TranslateVectorType>();
            Parameter = in_reader.Read<int>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Vector);
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
            in_writer.Write(GTCounter);
            in_writer.Write(VectorType);
            in_writer.Write(Parameter);
        }

        public uint GetParamCount()
        {
            return 20;
        }
    }
}
