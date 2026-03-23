using Marathon.IO;
using Marathon.IO.Types;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class MaterialColorRandomNormal : IMomentumParamSet
    {
        public Color<float, RGBA> Color { get; set; }

        public Color<float, RGBA> Random { get; set; }

        public GTCounter GTCounter { get; set; }

        public bool SetGeneralColor { get; set; }

        public ColorBlendMode ColorBlendMode { get; set; }

        public MaterialColorRandomNormal() { }

        public MaterialColorRandomNormal(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Color = in_reader.ReadObject<Color<float, RGBA>>();
            Random = in_reader.ReadObject<Color<float, RGBA>>();
            GTCounter = in_reader.Read<GTCounter>();
            SetGeneralColor = in_reader.ReadBoolean<uint>();
            ColorBlendMode = in_reader.Read<ColorBlendMode>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.WriteObject(Color);
            in_writer.WriteObject(Random);
            in_writer.Write(GTCounter);
            in_writer.WriteBoolean<uint>(SetGeneralColor);
            in_writer.Write(ColorBlendMode);
        }

        public uint GetParamCount()
        {
            return 11;
        }
    }
}
