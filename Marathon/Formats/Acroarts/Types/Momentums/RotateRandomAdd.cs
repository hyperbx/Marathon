using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class RotateRandomAdd : IMomentumParamSet
    {
        public Vector3 Rotation { get; set; }

        public Vector3 Add { get; set; }

        public uint UnknownField { get; set; }

        public GTCounter GTCounter { get; set; }

        public RotateRandomAdd() { }

        public RotateRandomAdd(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Rotation = in_reader.Read<Vector3>();
            Add = in_reader.Read<Vector3>();
            UnknownField = in_reader.Read<uint>();
            GTCounter = in_reader.Read<GTCounter>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Rotation);
            in_writer.Write(Add);
            in_writer.Write(UnknownField);
            in_writer.Write(GTCounter);
        }

        public uint GetParamCount()
        {
            return 8;
        }
    }
}
