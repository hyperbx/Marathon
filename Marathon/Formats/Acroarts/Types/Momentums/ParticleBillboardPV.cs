using Marathon.Formats.Acroarts.Collections;
using Marathon.IO;
using Marathon.IO.Types;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class ParticleBillboardPV : IMomentumParamSet
    {
        public ParticleEmitterType EmitterType { get; set; }

        public uint UnknownField2 { get; set; }

        public Vector3 Position { get; set; }

        public bool IsCameraLocal { get; set; }

        public float Radius { get; set; }

        public Vector3 Scale { get; set; }

        public Distance<float> DistanceWidth { get; set; }

        public Distance<float> DistanceHeight { get; set; }

        public uint UnknownField15 { get; set; }

        public bool UseSplineMotion { get; set; }

        public List<ISplineKeyFrame> SplineKeyFrames { get; set; } = [];

        public bool Loop { get; set; }

        public bool UseMaterialColor { get; set; }

        public int BlendMode { get; set; }

        public uint ParticleCount { get; set; }

        public float Rate { get; set; }

        public uint UnknownField23 { get; set; }

        public uint UnknownField24 { get; set; }

        public float Angle { get; set; }

        public Distance<float> DistanceAngle { get; set; }

        public bool Billboard { get; set; }

        public uint UnknownField29 { get; set; }

        public uint UnknownField30 { get; set; }

        public uint UnknownField31 { get; set; }

        public uint UnknownField32 { get; set; }

        public uint UnknownField33 { get; set; }

        public uint UnknownField34 { get; set; }

        public uint UnknownField35 { get; set; }

        public float UnknownField36 { get; set; }

        public float UnknownField37 { get; set; }

        public Distance<float> DistanceX { get; set; }

        public Distance<float> DistanceY { get; set; }

        public Distance<float> DistanceZ { get; set; }

        public uint UnknownField44 { get; set; }

        public float Parameter { get; set; }

        public uint UnknownField46 { get; set; }

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

        public bool UnknownField58 { get; set; }

        public bool UseRandomPattern { get; set; }

        public ParticleBillboardPV() { }

        public ParticleBillboardPV(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            EmitterType = in_reader.Read<ParticleEmitterType>();
            UnknownField2 = in_reader.Read<uint>();
            Position = in_reader.Read<Vector3>();
            IsCameraLocal = in_reader.ReadBoolean<uint>();
            Radius = in_reader.Read<float>();
            Scale = in_reader.Read<Vector3>();
            DistanceWidth = in_reader.Read<Distance<float>>();
            DistanceHeight = in_reader.Read<Distance<float>>();
            UnknownField15 = in_reader.Read<uint>();
            UseSplineMotion = in_reader.ReadBoolean<uint>();

            var splineKeyFramesOffset = in_reader.Read<uint>();

            in_reader.ReadAtOffset(in_reader.CalculateOffset(splineKeyFramesOffset), () =>
            {
                var (count, offset) = IndirectList.PeekInfo(in_reader);

                if (count == 2)
                {
                    var linearSplineKeyFrames = new IndirectMomentumParamList<LinearSplineKeyFrame<Vector2>>();
                    linearSplineKeyFrames.Read(in_reader);

                    SplineKeyFrames.AddRange(linearSplineKeyFrames);
                }
                else
                {
                    var kbSplineKeyFrames = new IndirectMomentumParamList<KBSplineKeyFrame<Vector2>>();
                    kbSplineKeyFrames.Read(in_reader);

                    SplineKeyFrames.AddRange(kbSplineKeyFrames);
                }
            });

            Loop = in_reader.ReadBoolean<uint>();
            UseMaterialColor = in_reader.ReadBoolean<uint>();
            BlendMode = in_reader.Read<int>();
            ParticleCount = in_reader.Read<uint>();
            Rate = in_reader.Read<float>();
            UnknownField23 = in_reader.Read<uint>();
            UnknownField24 = in_reader.Read<uint>();
            Angle = in_reader.Read<float>();
            DistanceAngle = in_reader.Read<Distance<float>>();
            Billboard = in_reader.ReadBoolean<uint>();
            UnknownField29 = in_reader.Read<uint>();
            UnknownField30 = in_reader.Read<uint>();
            UnknownField31 = in_reader.Read<uint>();
            UnknownField32 = in_reader.Read<uint>();
            UnknownField33 = in_reader.Read<uint>();
            UnknownField34 = in_reader.Read<uint>();
            UnknownField35 = in_reader.Read<uint>();
            UnknownField36 = in_reader.Read<float>();
            UnknownField37 = in_reader.Read<float>();
            DistanceX = in_reader.Read<Distance<float>>();
            DistanceY = in_reader.Read<Distance<float>>();
            DistanceZ = in_reader.Read<Distance<float>>();
            UnknownField44 = in_reader.Read<uint>();
            Parameter = in_reader.Read<float>();
            UnknownField46 = in_reader.Read<uint>();

            var colourKeyFramesOffset = in_reader.Read<uint>();

            in_reader.ReadAtOffset(in_reader.CalculateOffset(colourKeyFramesOffset), () =>
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
            UnknownField58 = in_reader.ReadBoolean<uint>();
            UseRandomPattern = in_reader.ReadBoolean<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(EmitterType);
            in_writer.Write(UnknownField2);
            in_writer.Write(Position);
            in_writer.WriteBoolean<uint>(IsCameraLocal);
            in_writer.Write(Radius);
            in_writer.Write(Scale);
            in_writer.Write(DistanceWidth);
            in_writer.Write(DistanceHeight);
            in_writer.Write(UnknownField15);
            in_writer.WriteBoolean<uint>(UseSplineMotion);
            var splineKeyFramesOffset = in_writer.Reserve<uint>();
            in_writer.WriteBoolean<uint>(Loop);
            in_writer.WriteBoolean<uint>(UseMaterialColor);
            in_writer.Write(BlendMode);
            in_writer.Write(ParticleCount);
            in_writer.Write(Rate);
            in_writer.Write(UnknownField23);
            in_writer.Write(UnknownField24);
            in_writer.Write(Angle);
            in_writer.Write(DistanceAngle);
            in_writer.WriteBoolean<uint>(Billboard);
            in_writer.Write(UnknownField29);
            in_writer.Write(UnknownField30);
            in_writer.Write(UnknownField31);
            in_writer.Write(UnknownField32);
            in_writer.Write(UnknownField33);
            in_writer.Write(UnknownField34);
            in_writer.Write(UnknownField35);
            in_writer.Write(UnknownField36);
            in_writer.Write(UnknownField37);
            in_writer.Write(DistanceX);
            in_writer.Write(DistanceY);
            in_writer.Write(DistanceZ);
            in_writer.Write(UnknownField44);
            in_writer.Write(Parameter);
            in_writer.Write(UnknownField46);
            var colourKeyFramesOffset = in_writer.Reserve<uint>();
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
            in_writer.WriteBoolean<uint>(UnknownField58);
            in_writer.WriteBoolean<uint>(UseRandomPattern);

            in_writer.WriteReserved(splineKeyFramesOffset, (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);

            if (SplineKeyFrames.Count == 2)
            {
                var linearSplineKeyFrames = new IndirectMomentumParamList<LinearSplineKeyFrame<Vector2>>();
                linearSplineKeyFrames.AddRange(SplineKeyFrames.Cast<LinearSplineKeyFrame<Vector2>>());
                linearSplineKeyFrames.Write(in_writer);
            }
            else if (SplineKeyFrames.Count > 0)
            {
                var kbSplineKeyFrames = new IndirectMomentumParamList<KBSplineKeyFrame<Vector2>>();
                kbSplineKeyFrames.AddRange(SplineKeyFrames.Cast<KBSplineKeyFrame<Vector2>>());
                kbSplineKeyFrames.Write(in_writer);
            }

            in_writer.WriteReserved(colourKeyFramesOffset, (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
            ColorKeyFrames.Write(in_writer);
        }

        public float GetBetweenValue(Distance<float> in_distance)
        {
            return Random.Get(in_distance.Max - in_distance.Min) + in_distance.Min;
        }

        public uint GetParamCount()
        {
            return 59;
        }
    }
}
