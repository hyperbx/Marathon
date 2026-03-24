using Marathon.Formats.Acroarts.Collections;
using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class TranslateGoal : IMomentumParamSet
    {
        public IndirectMomentumParamList<TranslateGoalInfo> Steps { get; set; } = [];

        public GTCounter GTCounter { get; set; }

        public TranslateGoal() { }

        public TranslateGoal(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var infoSetOffset = in_reader.Read<uint>();

            in_reader.ReadAtOffset(in_reader.CalculateOffset(infoSetOffset), () =>
            {
                Steps = new IndirectMomentumParamList<TranslateGoalInfo>(in_reader);
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

    public class TranslateGoalInfo : IMomentumParamSet
    {
        public Vector3 Vector { get; set; }

        public GoalInterpolation GoalInterpolation { get; set; }

        public float TotalTime { get; set; }

        public float Coefficient { get; set; }

        public bool Accel { get; set; }

        public TranslateVectorType VectorType { get; set; }

        public uint UnknownField1 { get; set; }

        public bool UseEndVector { get; set; }

        public int UnknownField2 { get; set; } = -1;

        public TranslateGoalInfo() { }

        public TranslateGoalInfo(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Vector = in_reader.Read<Vector3>();
            GoalInterpolation = in_reader.Read<GoalInterpolation>();
            TotalTime = in_reader.Read<float>();
            Coefficient = in_reader.Read<float>();
            Accel = in_reader.ReadBoolean<uint>();
            VectorType = in_reader.Read<TranslateVectorType>();
            UnknownField1 = in_reader.Read<uint>();
            UseEndVector = in_reader.ReadBoolean<uint>();
            UnknownField2 = in_reader.Read<int>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Vector);
            in_writer.Write(GoalInterpolation);
            in_writer.Write(TotalTime);
            in_writer.Write(Coefficient);
            in_writer.WriteBoolean<uint>(Accel);
            in_writer.Write(VectorType);
            in_writer.Write(UnknownField1);
            in_writer.WriteBoolean<uint>(UseEndVector);
            in_writer.Write(UnknownField2);
        }

        public uint GetParamCount()
        {
            return 11;
        }
    }
}
