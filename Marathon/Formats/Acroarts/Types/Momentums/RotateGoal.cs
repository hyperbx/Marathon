using Marathon.Formats.Acroarts.Collections;
using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class RotateGoal : IMomentumParamSet
    {
        public IndirectMomentumParamList<RotateGoalInfo> Steps { get; set; } = [];

        public GTCounter GTCounter { get; set; }

        public RotateGoal() { }

        public RotateGoal(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var infoSetOffset = in_reader.Read<uint>();

            in_reader.ReadAtOffset(in_reader.CalculateOffset(infoSetOffset), () =>
            {
                Steps = new IndirectMomentumParamList<RotateGoalInfo>(in_reader);
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

    public class RotateGoalInfo : IMomentumParamSet
    {
        public Vector3 Rotation { get; set; }

        public GoalInterpolation GoalInterpolation { get; set; }

        public float TotalTime { get; set; }

        public float Coefficient { get; set; }

        public bool Accel { get; set; }

        public uint UnknownField1 { get; set; }

        public int UnknownField2 { get; set; }

        public RotateGoalInfo() { }

        public RotateGoalInfo(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Rotation = in_reader.Read<Vector3>();
            GoalInterpolation = in_reader.Read<GoalInterpolation>();
            TotalTime = in_reader.Read<float>();
            Coefficient = in_reader.Read<float>();
            Accel = in_reader.Read<uint>() != 0;
            UnknownField1 = in_reader.Read<uint>();
            UnknownField2 = in_reader.Read<int>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Rotation);
            in_writer.Write(GoalInterpolation);
            in_writer.Write(TotalTime);
            in_writer.Write(Coefficient);
            in_writer.Write(Accel ? 1 : 0);
            in_writer.Write(UnknownField1);
            in_writer.Write(UnknownField2);
        }

        public uint GetParamCount()
        {
            return 9;
        }
    }
}
