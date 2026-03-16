using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using Marathon.IO.Types;
using System.Numerics;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class BillboardTail : IMomentumParamSet
    {
        public uint UnknownField1 { get; set; }

        public float UnknownField2 { get; set; }

        public float UnknownField3 { get; set; }

        public float UnknownField4 { get; set; }

        public uint UnknownField5 { get; set; }

        public uint UnknownField6 { get; set; }

        public uint UnknownField7 { get; set; }

        public uint UnknownField8 { get; set; }

        public uint BlendMode { get; set; }

        public int NodeCount { get; set; }

        public float UnknownField9 { get; set; }

        public float UnknownField10 { get; set; }

        public float UnknownField11 { get; set; }

        public float UnknownField12 { get; set; }

        public Vector3 Position { get; set; }

        public Colour<float, RGBA> Color { get; set; } 

        public ColorApplyMode ColorApplyMode { get; set; }

        public float UnknownField13 { get; set; }

        public uint UnknownField14 { get; set; }

        public float UnknownField15 { get; set; }

        public uint UnknownField16 { get; set; }

        public float UnknownField17 { get; set; }

        public uint UnknownField18 { get; set; }

        public BillboardTail() { }

        public BillboardTail(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            UnknownField1 = in_reader.Read<uint>();
            UnknownField2 = in_reader.Read<float>();
            UnknownField3 = in_reader.Read<float>();
            UnknownField4 = in_reader.Read<float>();
            UnknownField5 = in_reader.Read<uint>();
            UnknownField6 = in_reader.Read<uint>();
            UnknownField7 = in_reader.Read<uint>();
            UnknownField8 = in_reader.Read<uint>();
            BlendMode = in_reader.Read<uint>();
            NodeCount = in_reader.Read<int>();
            UnknownField9 = in_reader.Read<float>();
            UnknownField10 = in_reader.Read<float>();
            UnknownField11 = in_reader.Read<float>();
            UnknownField12 = in_reader.Read<float>();
            Position = in_reader.Read<Vector3>();
            Color = in_reader.ReadObject<Colour<float, RGBA>>();
            ColorApplyMode = in_reader.Read<ColorApplyMode>();
            UnknownField13 = in_reader.Read<float>();
            UnknownField14 = in_reader.Read<uint>();
            UnknownField15 = in_reader.Read<float>();
            UnknownField16 = in_reader.Read<uint>();
            UnknownField17 = in_reader.Read<float>();
            UnknownField18 = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            in_writer.Write(UnknownField1);
            in_writer.Write(UnknownField2);
            in_writer.Write(UnknownField3);
            in_writer.Write(UnknownField4);
            in_writer.Write(UnknownField5);
            in_writer.Write(UnknownField6);
            in_writer.Write(UnknownField7);
            in_writer.Write(UnknownField8);
            in_writer.Write(BlendMode);
            in_writer.Write(NodeCount);
            in_writer.Write(UnknownField9);
            in_writer.Write(UnknownField10);
            in_writer.Write(UnknownField11);
            in_writer.Write(UnknownField12);
            in_writer.Write(Position);
            in_writer.WriteObject(Color);
            in_writer.Write(ColorApplyMode);
            in_writer.Write(UnknownField13);
            in_writer.Write(UnknownField14);
            in_writer.Write(UnknownField15);
            in_writer.Write(UnknownField16);
            in_writer.Write(UnknownField17);
            in_writer.Write(UnknownField18);
        }

        public uint GetParamCount()
        {
            return 28;
        }
    }
}
