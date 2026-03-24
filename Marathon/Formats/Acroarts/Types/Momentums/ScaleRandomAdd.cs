using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class ScaleRandomAdd : IMomentumParamSet
    {
        public Vector3 Scale { get; set; }

        public Vector3 Add { get; set; }

        public uint UnknownField { get; set; }

        public GTCounter GTCounter { get; set; }

        public bool UseUniformScale { get; set; }

        public ScaleRandomAdd() { }

        public ScaleRandomAdd(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Scale = in_reader.Read<Vector3>();
            Add = in_reader.Read<Vector3>();
            UnknownField = in_reader.Read<uint>();
            GTCounter = in_reader.Read<GTCounter>();
            UseUniformScale = in_reader.ReadBoolean<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Scale);
            in_writer.Write(Add);
            in_writer.Write(UnknownField);
            in_writer.Write(GTCounter);
            in_writer.WriteBoolean<uint>(UseUniformScale);
        }

        public uint GetParamCount()
        {
            return 9;
        }
    }
}
