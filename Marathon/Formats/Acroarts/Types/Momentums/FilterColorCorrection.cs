using Marathon.IO;
using Marathon.IO.Types;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class FilterColorCorrection : IMomentumParamSet
    {
        public Color<float, RGBA> Color { get; set; }

        public FilterColorCorrection() { }

        public FilterColorCorrection(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Color = in_reader.ReadObject<Color<float, RGBA>>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.WriteObject(Color);
        }

        public uint GetParamCount()
        {
            return 4;
        }
    }
}
