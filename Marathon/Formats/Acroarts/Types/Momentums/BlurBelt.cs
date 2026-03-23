using Marathon.IO;
using Marathon.IO.Types;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class BlurBelt : IMomentumParamSet
    {
        public uint UnknownField1 { get; set; }

        public float Start { get; set; }

        public float End { get; set; }

        public float UnknownField2 { get; set; }

        public int KeyFrameCount { get; set; }

        public int VertexCount { get; set; }

        public uint UnknownField3 { get; set; }

        public uint UnknownField4 { get; set; }

        public uint BlendMode { get; set; }

        public uint NodeIndexStart { get; set; }

        public Vector3 PositionStart { get; set; }

        public Color<float, RGBA> ColorStart { get; set; }

        public int FalloffTypeStart { get; set; }

        public float FalloffPowerStart { get; set; }

        public uint UnknownField5 { get; set; }

        public int NodeIndexEnd { get; set; }

        public Vector3 PositionEnd { get; set; }

        public Color<float, RGBA> ColorEnd { get; set; }

        public int FalloffTypeEnd { get; set; }

        public float FalloffPowerEnd { get; set; }

        public uint UnknownField6 { get; set; }

        public ColorApplyMode ColorApplyMode { get; set; }

        public float ApproachSpeed { get; set; }

        public Distance<float> BufferWidth { get; set; }

        public Distance<float> BufferHeight { get; set; }

        public BlurBelt() { }

        public BlurBelt(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            UnknownField1 = in_reader.Read<uint>();
            Start = in_reader.Read<float>();
            End = in_reader.Read<float>();
            UnknownField2 = in_reader.Read<float>();
            KeyFrameCount = in_reader.Read<int>();
            VertexCount = in_reader.Read<int>();
            UnknownField3 = in_reader.Read<uint>();
            UnknownField4 = in_reader.Read<uint>();
            BlendMode = in_reader.Read<uint>();
            NodeIndexStart = in_reader.Read<uint>();
            PositionStart = in_reader.Read<Vector3>();
            ColorStart = in_reader.Read<Color<float, RGBA>>();
            FalloffTypeStart = in_reader.Read<int>();
            FalloffPowerStart = in_reader.Read<float>();
            UnknownField5 = in_reader.Read<uint>();
            NodeIndexEnd = in_reader.Read<int>();
            PositionEnd = in_reader.Read<Vector3>();
            ColorEnd = in_reader.Read<Color<float, RGBA>>();
            FalloffTypeEnd = in_reader.Read<int>();
            FalloffPowerEnd = in_reader.Read<float>();
            UnknownField6 = in_reader.Read<uint>();
            ColorApplyMode = in_reader.Read<ColorApplyMode>();
            ApproachSpeed = in_reader.Read<float>();
            BufferWidth = in_reader.Read<Distance<float>>();
            BufferHeight = in_reader.Read<Distance<float>>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(UnknownField1);
            in_writer.Write(Start);
            in_writer.Write(End);
            in_writer.Write(UnknownField2);
            in_writer.Write(KeyFrameCount);
            in_writer.Write(VertexCount);
            in_writer.Write(UnknownField3);
            in_writer.Write(UnknownField4);
            in_writer.Write(BlendMode);
            in_writer.Write(NodeIndexStart);
            in_writer.Write(PositionStart);
            in_writer.Write(ColorStart);
            in_writer.Write(FalloffTypeStart);
            in_writer.Write(FalloffPowerStart);
            in_writer.Write(UnknownField5);
            in_writer.Write(NodeIndexEnd);
            in_writer.Write(PositionEnd);
            in_writer.Write(ColorEnd);
            in_writer.Write(FalloffTypeEnd);
            in_writer.Write(FalloffPowerEnd);
            in_writer.Write(UnknownField6);
            in_writer.Write(ColorApplyMode);
            in_writer.Write(ApproachSpeed);
            in_writer.Write(BufferWidth);
            in_writer.Write(BufferHeight);
        }

        public uint GetParamCount()
        {
            return 37;
        }
    }
}
