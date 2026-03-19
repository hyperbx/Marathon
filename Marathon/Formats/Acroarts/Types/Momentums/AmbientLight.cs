using Marathon.IO;
using Marathon.IO.Types;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class AmbientLight : IMomentumParamSet
    {
        public Colour<float, RGBA> Color { get; set; }

        public AmbientLight() { }

        public AmbientLight(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Color = in_reader.ReadObject<Colour<float, RGBA>>();
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
