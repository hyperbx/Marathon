using Marathon.IO;
using Marathon.IO.Types;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class MaterialColorSin : IMomentumParamSet
    {
        public float Frequency { get; set; }

        public Distance<float> Red { get; set; }

        public Distance<float> Green { get; set; }

        public Distance<float> Blue { get; set; }

        public Distance<float> Alpha { get; set; }

        public float Phase { get; set; }

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
            Frequency = in_reader.Read<float>();
            Red = in_reader.Read<Distance<float>>();
            Green = in_reader.Read<Distance<float>>();
            Blue = in_reader.Read<Distance<float>>();
            Alpha = in_reader.Read<Distance<float>>();
            Phase = in_reader.Read<float>();
            GTCounter = in_reader.Read<GTCounter>();
            SetGeneralColor = in_reader.ReadBoolean<uint>();
            ColorBlendMode = in_reader.Read<ColorBlendMode>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Frequency);
            in_writer.Write(Red);
            in_writer.Write(Green);
            in_writer.Write(Blue);
            in_writer.Write(Alpha);
            in_writer.Write(Phase);
            in_writer.Write(GTCounter);
            in_writer.WriteBoolean<uint>(SetGeneralColor);
            in_writer.Write(ColorBlendMode);
        }

        public uint GetParamCount()
        {
            return 13;
        }
    }
}
