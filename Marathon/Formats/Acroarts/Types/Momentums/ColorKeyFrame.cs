using Marathon.IO;
using Marathon.IO.Types;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class ColorKeyFrame : IMomentumParamSet
    {
        public float Frame { get; set; }

        public Color<float, RGBA> Color { get; set; }

        public ColorKeyFrame() { }

        public ColorKeyFrame(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Frame = in_reader.Read<float>();
            Color = in_reader.ReadObject<Color<float, RGBA>>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Frame);
            in_writer.WriteObject(Color);
        }

        public uint GetParamCount()
        {
            return 5;
        }
    }
}
