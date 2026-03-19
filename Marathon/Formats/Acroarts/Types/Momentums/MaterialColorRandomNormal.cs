using Marathon.IO;
using Marathon.IO.Types;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class MaterialColorRandomNormal : IMomentumParamSet
    {
        public Colour<float, RGBA> Color { get; set; }

        public Colour<float, RGBA> Random { get; set; }

        public uint UnknownField { get; set; }

        public bool SetGeneralColor { get; set; }

        public ColorBlendMode ColorBlendMode { get; set; }

        public MaterialColorRandomNormal() { }

        public MaterialColorRandomNormal(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Color = in_reader.ReadObject<Colour<float, RGBA>>();
            Random = in_reader.ReadObject<Colour<float, RGBA>>();
            UnknownField = in_reader.Read<uint>();
            SetGeneralColor = in_reader.Read<uint>() != 0;
            ColorBlendMode = in_reader.Read<ColorBlendMode>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.WriteObject(Color);
            in_writer.WriteObject(Random);
            in_writer.Write(UnknownField);
            in_writer.Write(SetGeneralColor ? 1 : 0);
            in_writer.Write(ColorBlendMode);
        }

        public uint GetParamCount()
        {
            return 11;
        }
    }
}
