using Marathon.Formats.Acroarts.Collections;
using Marathon.IO;
using Marathon.IO.Types;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class ScaleRandomGoal : IMomentumParamSet
    {
        public IndirectMomentumParamList<ScaleRandomGoalInfo> Steps { get; set; } = [];

        public uint UnknownField1 { get; set; }

        public GTCounter GTCounter { get; set; }

        public uint UnknownField2 { get; set; }

        public ScaleRandomGoal() { }

        public ScaleRandomGoal(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var infoSetOffset = in_reader.Read<uint>();

            in_reader.ReadAtOffset(in_reader.CalculateOffset(infoSetOffset), () =>
            {
                Steps = new IndirectMomentumParamList<ScaleRandomGoalInfo>(in_reader);
            });

            UnknownField1 = in_reader.Read<uint>();
            GTCounter = in_reader.Read<GTCounter>();
            UnknownField2 = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var infoSetOffset = in_writer.Reserve<uint>();
            in_writer.Write(UnknownField1);
            in_writer.Write(GTCounter);
            in_writer.Write(UnknownField2);
            in_writer.WriteReserved(infoSetOffset, (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
            Steps.Write(in_writer);
        }

        public uint GetParamCount()
        {
            return 4;
        }
    }

    public class ScaleRandomGoalInfo : IMomentumParamSet
    {
        public Vector3 Scale { get; set; }

        public Vector3 End { get; set; }

        public GoalInterpolation GoalInterpolation { get; set; }

        public Distance<float> TotalTime { get; set; }

        public Distance<float> Coefficient { get; set; }

        public bool Accel { get; set; }

        public uint UnknownField1 { get; set; }

        public int UnknownField2 { get; set; } = -1;

        public ScaleRandomGoalInfo() { }

        public ScaleRandomGoalInfo(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Scale = in_reader.Read<Vector3>();
            End = in_reader.Read<Vector3>();
            GoalInterpolation = in_reader.Read<GoalInterpolation>();
            TotalTime = in_reader.Read<Distance<float>>();
            Coefficient = in_reader.Read<Distance<float>>();
            Accel = in_reader.ReadBoolean<uint>();
            UnknownField1 = in_reader.Read<uint>();
            UnknownField2 = in_reader.Read<int>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Scale);
            in_writer.Write(End);
            in_writer.Write(GoalInterpolation);
            in_writer.Write(TotalTime);
            in_writer.Write(Coefficient);
            in_writer.WriteBoolean<uint>(Accel);
            in_writer.Write(UnknownField1);
            in_writer.Write(UnknownField2);
        }

        public uint GetParamCount()
        {
            return 14;
        }
    }
}
