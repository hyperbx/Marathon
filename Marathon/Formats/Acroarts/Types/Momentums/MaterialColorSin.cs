using Marathon.IO;
using Marathon.IO.Types;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class MaterialColorSin : IMomentumParamSet
    {
        public Colour<float, RGBA> Color { get; set; }

        public uint UnknownField1 { get; set; }

        public uint UnknownField2 { get; set; }

        public uint UnknownField3 { get; set; }

        public uint UnknownField4 { get; set; }

        public uint UnknownField5 { get; set; }

        public uint UnknownField6 { get; set; }

        public GTCounter GTCounter { get; set; }

        public bool SetGeneralColor { get; set; }

        public ColorBlendMode ColorBlendMode { get; set; }

        public MaterialColorSin() { }

        public MaterialColorSin(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Color = in_reader.ReadObject<Colour<float, RGBA>>();
            UnknownField1 = in_reader.Read<uint>();
            UnknownField2 = in_reader.Read<uint>();
            UnknownField3 = in_reader.Read<uint>();
            UnknownField4 = in_reader.Read<uint>();
            UnknownField5 = in_reader.Read<uint>();
            UnknownField6 = in_reader.Read<uint>();
            GTCounter = in_reader.Read<GTCounter>();
            SetGeneralColor = in_reader.Read<uint>() != 0;
            ColorBlendMode = in_reader.Read<ColorBlendMode>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.WriteObject(Color);
            in_writer.Write(UnknownField1);
            in_writer.Write(UnknownField2);
            in_writer.Write(UnknownField3);
            in_writer.Write(UnknownField4);
            in_writer.Write(UnknownField5);
            in_writer.Write(UnknownField6);
            in_writer.Write(GTCounter);
            in_writer.Write(SetGeneralColor ? 1 : 0);
            in_writer.Write(ColorBlendMode);
        }

        public uint GetParamCount()
        {
            return 13;
        }
    }
}
