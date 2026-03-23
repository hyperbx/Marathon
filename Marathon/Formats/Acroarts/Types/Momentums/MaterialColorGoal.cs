using Marathon.Formats.Acroarts.Collections;
using Marathon.IO;
using Marathon.IO.Types;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class MaterialColorGoal : IMomentumParamSet
    {
        public IndirectMomentumParamList<MaterialColorGoalInfo>[] Channels { get; set; } = new IndirectMomentumParamList<MaterialColorGoalInfo>[4];

        public GTCounter GTCounter { get; set; }

        public bool SetGeneralColor { get; set; }

        public ColorBlendMode ColorBlendMode { get; set; }

        public IndirectMomentumParamList<MaterialColorGoalInfo> this[ColorChannel in_channel] => Channels[(int)in_channel];

        public MaterialColorGoal()
        {
            for (int i = 0; i < Channels.Length; i++)
                Channels[i] = [];
        }

        public MaterialColorGoal(BinaryObjectReaderEx in_reader)
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
                    Channels[i] = new IndirectMomentumParamList<MaterialColorGoalInfo>(in_reader);
                });
            }

            GTCounter = in_reader.Read<GTCounter>();
            SetGeneralColor = in_reader.ReadBoolean<uint>();
            ColorBlendMode = in_reader.Read<ColorBlendMode>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var offsets = new long[Channels.Length];

            for (int i = 0; i < offsets.Length; i++)
                offsets[i] = in_writer.Reserve<uint>();

            in_writer.Write(GTCounter);
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
            return 7;
        }
    }

    public class MaterialColorGoalInfo : IMomentumParamSet
    {
        public float Color { get; set; }
        
        public GoalInterpolation GoalInterpolation { get; set; }

        public float TotalTime { get; set; }

        public float Coefficient { get; set; }

        public bool Accel { get; set; }

        public bool UseEndColor { get; set; }

        public int UnknownField { get; set; } = -1;

        public MaterialColorGoalInfo() { }

        public MaterialColorGoalInfo(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Color = in_reader.Read<float>();
            GoalInterpolation = in_reader.Read<GoalInterpolation>();
            TotalTime = in_reader.Read<float>();
            Coefficient = in_reader.Read<float>();
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
            return 7;
        }
    }
}
