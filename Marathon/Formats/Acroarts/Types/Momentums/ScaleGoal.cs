using Marathon.Formats.Acroarts.Collections;
using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class ScaleGoal : IMomentumParamSet
    {
        public IndirectMomentumParamList<ScaleGoalInfo> Steps { get; set; } = [];

        public GTCounter GTCounter { get; set; }

        public ScaleGoal() { }

        public ScaleGoal(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var infoSetOffset = in_reader.Read<uint>();

            in_reader.ReadAtOffset(in_reader.CalculateOffset(infoSetOffset), () =>
            {
                Steps = new IndirectMomentumParamList<ScaleGoalInfo>(in_reader);
            });

            GTCounter = in_reader.Read<GTCounter>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var infoSetOffset = in_writer.Reserve<uint>();
            in_writer.Write(GTCounter);
            in_writer.WriteReserved(infoSetOffset, (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
            Steps.Write(in_writer);
        }

        public uint GetParamCount()
        {
            return 2;
        }
    }

    public class ScaleGoalInfo : IMomentumParamSet
    {
        public Vector3 UnknownField1 { get; set; }

        public uint UnknownField2 { get; set; }

        public Vector3 UnknownField3 { get; set; }

        public uint UnknownField4 { get; set; }

        public int UnknownField5 { get; set; }

        public ScaleGoalInfo() { }

        public ScaleGoalInfo(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            UnknownField1 = in_reader.Read<Vector3>();
            UnknownField2 = in_reader.Read<uint>();
            UnknownField3 = in_reader.Read<Vector3>();
            UnknownField4 = in_reader.Read<uint>();
            UnknownField5 = in_reader.Read<int>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(UnknownField1);
            in_writer.Write(UnknownField2);
            in_writer.Write(UnknownField3);
            in_writer.Write(UnknownField4);
            in_writer.Write(UnknownField5);
        }

        public uint GetParamCount()
        {
            return 9;
        }
    }
}
