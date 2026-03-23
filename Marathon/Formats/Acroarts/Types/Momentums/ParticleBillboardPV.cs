using Marathon.Formats.Acroarts.Collections;
using Marathon.IO;
using Marathon.IO.Types;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class ParticleBillboardPV : IMomentumParamSet
    {
        public ParticleEmitterType EmitterType { get; set; }

        public uint UnknownField1 { get; set; }

        public Vector3 Position { get; set; }

        public bool IsCameraLocal { get; set; }

        public float Radius { get; set; }

        public Vector3 Offset { get; set; }

        public Distance<float> Width { get; set; }

        public Distance<float> Height { get; set; }

        public uint UnknownField2 { get; set; }

        public bool UseSplineMotion { get; set; }

        public IndirectMomentumParamList<SplineKeyFrame<Vector2>> SplineKeyFrames { get; set; } = [];

        public bool Loop { get; set; }

        public bool UseMaterialColor { get; set; }

        public int BlendMode { get; set; }

        public uint ParticleCount { get; set; }

        public float Rate { get; set; }

        public uint UnknownField3 { get; set; }

        public uint UnknownField4 { get; set; }

        public float Angle { get; set; }

        public Distance<float> AngleBetween { get; set; }

        public bool Billboard { get; set; }

        public uint UnknownField5 { get; set; }

        public Distance<float> VelocityX { get; set; }

        public Distance<float> VelocityY { get; set; }

        public Distance<float> VelocityZ { get; set; }

        public float UnknownField12 { get; set; }

        public float UnknownField13 { get; set; }

        public Distance<float> AccelX { get; set; }

        public Distance<float> AccelY { get; set; }

        public Distance<float> AccelZ { get; set; }

        public uint UnknownField14 { get; set; }

        public float Parameter { get; set; }

        public uint UnknownField15 { get; set; }

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

        public bool UnknownField16 { get; set; }

        public bool UseRandomPattern { get; set; }

        public ParticleBillboardPV() { }

        public ParticleBillboardPV(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            EmitterType = in_reader.Read<ParticleEmitterType>();
            UnknownField1 = in_reader.Read<uint>();
            Position = in_reader.Read<Vector3>();
            IsCameraLocal = in_reader.ReadBoolean<uint>();
            Radius = in_reader.Read<float>();
            Offset = in_reader.Read<Vector3>();
            Width = in_reader.Read<Distance<float>>();
            Height = in_reader.Read<Distance<float>>();
            UnknownField2 = in_reader.Read<uint>();
            UseSplineMotion = in_reader.ReadBoolean<uint>();

            var splineKeyFramesOffset = in_reader.Read<uint>();

            in_reader.ReadAtOffset(in_reader.CalculateOffset(splineKeyFramesOffset), () =>
            {
                SplineKeyFrames.Read(in_reader);
            });

            Loop = in_reader.ReadBoolean<uint>();
            UseMaterialColor = in_reader.ReadBoolean<uint>();
            BlendMode = in_reader.Read<int>();
            ParticleCount = in_reader.Read<uint>();
            Rate = in_reader.Read<float>();
            UnknownField3 = in_reader.Read<uint>();
            UnknownField4 = in_reader.Read<uint>();
            Angle = in_reader.Read<float>();
            AngleBetween = in_reader.Read<Distance<float>>();
            Billboard = in_reader.ReadBoolean<uint>();
            UnknownField5 = in_reader.Read<uint>();
            VelocityX = in_reader.Read<Distance<float>>();
            VelocityY = in_reader.Read<Distance<float>>();
            VelocityZ = in_reader.Read<Distance<float>>();
            UnknownField12 = in_reader.Read<float>();
            UnknownField13 = in_reader.Read<float>();
            AccelX = in_reader.Read<Distance<float>>();
            AccelY = in_reader.Read<Distance<float>>();
            AccelZ = in_reader.Read<Distance<float>>();
            UnknownField14 = in_reader.Read<uint>();
            Parameter = in_reader.Read<float>();
            UnknownField15 = in_reader.Read<uint>();

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
            UnknownField16 = in_reader.ReadBoolean<uint>();
            UseRandomPattern = in_reader.ReadBoolean<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(EmitterType);
            in_writer.Write(UnknownField1);
            in_writer.Write(Position);
            in_writer.WriteBoolean<uint>(IsCameraLocal);
            in_writer.Write(Radius);
            in_writer.Write(Offset);
            in_writer.Write(Width);
            in_writer.Write(Height);
            in_writer.Write(UnknownField2);
            in_writer.WriteBoolean<uint>(UseSplineMotion);
            var splineKeyFramesOffset = in_writer.Reserve<uint>();
            in_writer.WriteBoolean<uint>(Loop);
            in_writer.WriteBoolean<uint>(UseMaterialColor);
            in_writer.Write(BlendMode);
            in_writer.Write(ParticleCount);
            in_writer.Write(Rate);
            in_writer.Write(UnknownField3);
            in_writer.Write(UnknownField4);
            in_writer.Write(Angle);
            in_writer.Write(AngleBetween);
            in_writer.WriteBoolean<uint>(Billboard);
            in_writer.Write(UnknownField5);
            in_writer.Write(VelocityX);
            in_writer.Write(VelocityY);
            in_writer.Write(VelocityZ);
            in_writer.Write(UnknownField12);
            in_writer.Write(UnknownField13);
            in_writer.Write(AccelX);
            in_writer.Write(AccelY);
            in_writer.Write(AccelZ);
            in_writer.Write(UnknownField14);
            in_writer.Write(Parameter);
            in_writer.Write(UnknownField15);
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
            in_writer.WriteBoolean<uint>(UnknownField16);
            in_writer.WriteBoolean<uint>(UseRandomPattern);

            in_writer.WriteReserved(splineKeyFramesOffset, (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
            SplineKeyFrames.Write(in_writer);

            in_writer.WriteReserved(colorKeyFramesOffset, (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
            ColorKeyFrames.Write(in_writer);
        }

        public float GetRandomDistanceBetween(Distance<float> in_distance)
        {
            return Random.GetRandomF(in_distance.Max - in_distance.Min) + in_distance.Min;
        }

        public uint GetParamCount()
        {
            return 59;
        }
    }
}
