using Marathon.IO;
using Marathon.IO.Types;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class BillboardTail : IMomentumParamSet
    {
        public bool DisableInitPhase { get; set; }

        public float InitPhaseDuration { get; set; }

        public float Start { get; set; }

        public float End { get; set; }

        public int KeyFrameCount { get; set; }

        public int VertexCount { get; set; }

        public uint UnknownField1 { get; set; }

        public uint UnknownField2 { get; set; }

        public uint BlendMode { get; set; }

        public int NodeCount { get; set; }

        public Distance<float> Slide { get; set; }

        public bool Billboard { get; set; }

        public bool UseSpline { get; set; }

        public Vector3 Position { get; set; }

        public Color<float, RGBA> Color { get; set; } 

        public ColorApplyMode ColorApplyMode { get; set; }

        public float ApproachSpeed { get; set; }

        public Distance<float> BufferWidth { get; set; }

        public Distance<float> BufferHeight { get; set; }

        public uint Count { get; set; }

        public BillboardTail() { }

        public BillboardTail(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            DisableInitPhase = in_reader.ReadBoolean<uint>();
            InitPhaseDuration = in_reader.Read<float>();
            Start = in_reader.Read<float>();
            End = in_reader.Read<float>();
            KeyFrameCount = in_reader.Read<int>();
            VertexCount = in_reader.Read<int>();
            UnknownField1 = in_reader.Read<uint>();
            UnknownField2 = in_reader.Read<uint>();
            BlendMode = in_reader.Read<uint>();
            NodeCount = in_reader.Read<int>();
            Slide = in_reader.Read<Distance<float>>();
            Billboard = in_reader.ReadBoolean<uint>();
            UseSpline = in_reader.ReadBoolean<uint>();
            Position = in_reader.Read<Vector3>();
            Color = in_reader.ReadObject<Color<float, RGBA>>();
            ColorApplyMode = in_reader.Read<ColorApplyMode>();
            ApproachSpeed = in_reader.Read<float>();
            BufferWidth = in_reader.Read<Distance<float>>();
            BufferHeight = in_reader.Read<Distance<float>>();
            Count = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.WriteBoolean<uint>(DisableInitPhase);
            in_writer.Write(InitPhaseDuration);
            in_writer.Write(Start);
            in_writer.Write(End);
            in_writer.Write(KeyFrameCount);
            in_writer.Write(VertexCount);
            in_writer.Write(UnknownField1);
            in_writer.Write(UnknownField2);
            in_writer.Write(BlendMode);
            in_writer.Write(NodeCount);
            in_writer.Write(Slide);
            in_writer.WriteBoolean<uint>(Billboard);
            in_writer.WriteBoolean<uint>(UseSpline);
            in_writer.Write(Position);
            in_writer.WriteObject(Color);
            in_writer.Write(ColorApplyMode);
            in_writer.Write(ApproachSpeed);
            in_writer.Write(BufferWidth);
            in_writer.Write(BufferHeight);
            in_writer.Write(Count);
        }

        public uint GetParamCount()
        {
            return 28;
        }
    }
}
