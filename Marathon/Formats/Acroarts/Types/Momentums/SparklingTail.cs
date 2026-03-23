using Marathon.Formats.Acroarts.Collections;
using Marathon.IO;
using Marathon.IO.Types;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class SparklingTail : IMomentumParamSet
    {
        public bool UseBezierSpline { get; set; }

        public float UnknownField1 { get; set; }

        public Distance<uint> Bezier { get; set; }

        public int UnknownField2 { get; set; }

        public Vector3 Offset { get; set; }

        public Distance<float> Width { get; set; }

        public Distance<float> Height { get; set; }

        public uint UnknownField3 { get; set; }

        public bool UseSplineMotion { get; set; }

        public IndirectMomentumParamList<SplineKeyFrame<Vector2>> SplineKeyFrames { get; set; } = [];

        public bool Loop { get; set; }

        public int BlendMode { get; set; }

        public int UnknownField4 { get; set; }

        public uint ParticleCount { get; set; }

        public float Rate { get; set; }

        public float UnknownField5 { get; set; }

        public float UnknownField6 { get; set; }

        public float UnknownField7 { get; set; }

        public bool Billboard { get; set; }

        public uint UnknownField8 { get; set; }

        public Distance<float> VelocityX { get; set; }

        public Distance<float> VelocityY { get; set; }

        public Distance<float> VelocityZ { get; set; }

        public uint UnknownField9 { get; set; }

        public uint UnknownField10 { get; set; }

        public Distance<float> AccelX { get; set; }

        public Distance<float> AccelY { get; set; }

        public Distance<float> AccelZ { get; set; }

        public uint UnknownField11 { get; set; }

        public float Parameter { get; set; }

        public IndirectMomentumParamList<ColorKeyFrame> ColorKeyFrames { get; set; } = [];

        public bool UseColorMotion { get; set; }

        public bool UsePattern { get; set; }

        public int TextureCount { get; set; }

        public int Surface { get; set; }

        public AnonymousMomentumParam Anime { get; set; }

        public bool UsePatternMotion { get; set; }

        public int PatternColumns { get; set; }

        public int PatternRows { get; set; }

        public uint PatternIndex { get; set; }

        public uint PatternCount { get; set; }

        public uint UnknownField12 { get; set; }

        public bool UseRandomPattern { get; set; }

        public SparklingTail() { }

        public SparklingTail(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            UseBezierSpline = in_reader.ReadBoolean<uint>();
            UnknownField1 = in_reader.Read<float>();
            Bezier = in_reader.Read<Distance<uint>>();
            UnknownField2 = in_reader.Read<int>();
            Offset = in_reader.Read<Vector3>();
            Width = in_reader.Read<Distance<float>>();
            Height = in_reader.Read<Distance<float>>();
            UnknownField3 = in_reader.Read<uint>();
            UseSplineMotion = in_reader.ReadBoolean<uint>();

            var splineKeyFramesOffset = in_reader.Read<uint>();

            in_reader.ReadAtOffset(in_reader.CalculateOffset(splineKeyFramesOffset), () =>
            {
                SplineKeyFrames.Read(in_reader);
            });

            Loop = in_reader.ReadBoolean<uint>();
            BlendMode = in_reader.Read<int>();
            UnknownField4 = in_reader.Read<int>();
            ParticleCount = in_reader.Read<uint>();
            Rate = in_reader.Read<float>();
            UnknownField5 = in_reader.Read<float>();
            UnknownField6 = in_reader.Read<float>();
            UnknownField7 = in_reader.Read<float>();
            Billboard = in_reader.ReadBoolean<uint>();
            UnknownField8 = in_reader.Read<uint>();
            VelocityX = in_reader.Read<Distance<float>>();
            VelocityY = in_reader.Read<Distance<float>>();
            VelocityZ = in_reader.Read<Distance<float>>();
            UnknownField9 = in_reader.Read<uint>();
            UnknownField10 = in_reader.Read<uint>();
            AccelX = in_reader.Read<Distance<float>>();
            AccelY = in_reader.Read<Distance<float>>();
            AccelZ = in_reader.Read<Distance<float>>();
            UnknownField11 = in_reader.Read<uint>();
            Parameter = in_reader.Read<float>();

            var colorKeyFramesOffset = in_reader.Read<uint>();

            in_reader.ReadAtOffset(in_reader.CalculateOffset(colorKeyFramesOffset), () =>
            {
                ColorKeyFrames.Read(in_reader);
            });

            UseColorMotion = in_reader.ReadBoolean<uint>();
            UsePattern = in_reader.ReadBoolean<uint>();
            TextureCount = in_reader.Read<int>();
            Surface = in_reader.Read<int>();
            Anime = in_reader.ReadObjectEx<AnonymousMomentumParam>();
            UsePatternMotion = in_reader.ReadBoolean<uint>();
            PatternColumns = in_reader.Read<int>();
            PatternRows = in_reader.Read<int>();
            PatternIndex = in_reader.Read<uint>();
            PatternCount = in_reader.Read<uint>();
            UnknownField12 = in_reader.Read<uint>();
            UseRandomPattern = in_reader.ReadBoolean<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.WriteBoolean<uint>(UseBezierSpline);
            in_writer.Write(UnknownField1);
            in_writer.Write(Bezier);
            in_writer.Write(UnknownField2);
            in_writer.Write(Offset);
            in_writer.Write(Width);
            in_writer.Write(Height);
            in_writer.Write(UnknownField3);
            in_writer.WriteBoolean<uint>(UseSplineMotion);
            var splineKeyFramesOffset = in_writer.Reserve<uint>();
            in_writer.WriteBoolean<uint>(Loop);
            in_writer.Write(BlendMode);
            in_writer.Write(UnknownField4);
            in_writer.Write(ParticleCount);
            in_writer.Write(Rate);
            in_writer.Write(UnknownField5);
            in_writer.Write(UnknownField6);
            in_writer.Write(UnknownField7);
            in_writer.WriteBoolean<uint>(Billboard);
            in_writer.Write(UnknownField8);
            in_writer.Write(VelocityX);
            in_writer.Write(VelocityY);
            in_writer.Write(VelocityZ);
            in_writer.Write(UnknownField9);
            in_writer.Write(UnknownField10);
            in_writer.Write(AccelX);
            in_writer.Write(AccelY);
            in_writer.Write(AccelZ);
            in_writer.Write(UnknownField11);
            in_writer.Write(Parameter);
            var colorKeyFramesOffset = in_writer.Reserve<uint>();
            in_writer.WriteBoolean<uint>(UseColorMotion);
            in_writer.WriteBoolean<uint>(UsePattern);
            in_writer.Write(TextureCount);
            in_writer.Write(Surface);
            in_writer.WriteObjectEx(Anime);
            in_writer.WriteBoolean<uint>(UsePatternMotion);
            in_writer.Write(PatternColumns);
            in_writer.Write(PatternRows);
            in_writer.Write(PatternIndex);
            in_writer.Write(PatternCount);
            in_writer.Write(UnknownField12);
            in_writer.WriteBoolean<uint>(UseRandomPattern);

            in_writer.WriteReserved(splineKeyFramesOffset, (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
            SplineKeyFrames.Write(in_writer);

            in_writer.WriteReserved(colorKeyFramesOffset, (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
            ColorKeyFrames.Write(in_writer);
        }

        public uint GetParamCount()
        {
            return 54;
        }
    }
}
