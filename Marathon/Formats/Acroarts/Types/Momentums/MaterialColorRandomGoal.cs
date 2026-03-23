using Marathon.Formats.Acroarts.Collections;
using Marathon.IO;
using Marathon.IO.Types;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class MaterialColorRandomGoal : IMomentumParamSet
    {
        public IndirectMomentumParamList<MaterialColorRandomGoalInfo>[] Channels { get; set; } = new IndirectMomentumParamList<MaterialColorRandomGoalInfo>[4];

        public GTCounter GTCounter { get; set; }

        public uint UnknownField { get; set; }

        public bool SetGeneralColor { get; set; }

        public ColorBlendMode ColorBlendMode { get; set; }

        public IndirectMomentumParamList<MaterialColorRandomGoalInfo> this[ColorChannel in_channel] => Channels[(int)in_channel];

        public MaterialColorRandomGoal()
        {
            for (int i = 0; i < Channels.Length; i++)
                Channels[i] = [];
        }

        public MaterialColorRandomGoal(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            for (int i = 0; i < Channels.Length; i++)
            {
                var offset = in_reader.Read<uint>();

                in_reader.ReadAtOffset(in_reader.CalculateOffset(offset), () =>
                {
                    Channels[i] = new IndirectMomentumParamList<MaterialColorRandomGoalInfo>(in_reader);
                });
            }

            GTCounter = in_reader.Read<GTCounter>();
            UnknownField = in_reader.Read<uint>();
            SetGeneralColor = in_reader.ReadBoolean<uint>();
            ColorBlendMode = in_reader.Read<ColorBlendMode>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var offsets = new long[Channels.Length];

            for (int i = 0; i < offsets.Length; i++)
                offsets[i] = in_writer.Reserve<uint>();

            in_writer.Write(GTCounter);
            in_writer.Write(UnknownField);
            in_writer.WriteBoolean<uint>(SetGeneralColor);
            in_writer.Write(ColorBlendMode);

            for (int i = 0; i < Channels.Length; i++)
            {
                in_writer.WriteReserved(offsets[i], (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
                Channels[i].Write(in_writer);
            }
        }

        public uint GetParamCount()
        {
            return 8;
        }
    }

    public class MaterialColorRandomGoalInfo : IMomentumParamSet
    {
        public Distance<float> Color { get; set; }

        public GoalInterpolation GoalInterpolation { get; set; }

        public Distance<float> TotalTime { get; set; }

        public Distance<float> Coefficient { get; set; }

        public bool Accel { get; set; }

        public bool UseEndColor { get; set; }

        public int UnknownField { get; set; }

        public MaterialColorRandomGoalInfo() { }

        public MaterialColorRandomGoalInfo(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Color = in_reader.Read<Distance<float>>();
            GoalInterpolation = in_reader.Read<GoalInterpolation>();
            TotalTime = in_reader.Read<Distance<float>>();
            Coefficient = in_reader.Read<Distance<float>>();
            Accel = in_reader.ReadBoolean<uint>();
            UseEndColor = in_reader.ReadBoolean<uint>();
            UnknownField = in_reader.Read<int>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Color);
            in_writer.Write(GoalInterpolation);
            in_writer.Write(TotalTime);
            in_writer.Write(Coefficient);
            in_writer.WriteBoolean<uint>(Accel);
            in_writer.WriteBoolean<uint>(UseEndColor);
            in_writer.Write(UnknownField);
        }

        public uint GetParamCount()
        {
            return 10;
        }
    }
}
