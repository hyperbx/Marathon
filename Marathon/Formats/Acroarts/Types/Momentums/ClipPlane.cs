using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class ClipPlane : IMomentumParamSet
    {
        public float Near { get; set; }

        public float Far { get; set; }

        public ClipPlane() { }

        public ClipPlane(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Near = in_reader.Read<float>();
            Far = in_reader.Read<float>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Near);
            in_writer.Write(Far);
        }

        public uint GetParamCount()
        {
            return 2;
        }
    }
}
