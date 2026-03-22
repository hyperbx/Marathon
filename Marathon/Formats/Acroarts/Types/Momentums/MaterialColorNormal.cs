using Marathon.IO;
using Marathon.IO.Types;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class MaterialColorNormal : IMomentumParamSet
    {
        public Color<float, ARGB> Color { get; set; }

        public bool SetGeneralColor { get; set; }

        public ColorBlendMode ColorBlendMode { get; set; }

        public MaterialColorNormal() { }

        public MaterialColorNormal(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Color = in_reader.ReadObject<Color<float, ARGB>>();
            SetGeneralColor = in_reader.Read<uint>() != 0;
            ColorBlendMode = in_reader.Read<ColorBlendMode>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.WriteObject(Color);
            in_writer.Write(SetGeneralColor ? 1 : 0);
            in_writer.Write(ColorBlendMode);
        }

        public uint GetParamCount()
        {
            return 6;
        }
    }
}
