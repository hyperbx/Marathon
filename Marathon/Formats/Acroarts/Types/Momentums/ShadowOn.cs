using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class ShadowOn : IMomentumParamSet
    {
        public bool On { get; set; }

        public ShadowOn() { }

        public ShadowOn(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            On = in_reader.Read<uint>() != 0;
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(On ? 1 : 0);
        }

        public uint GetParamCount()
        {
            return 1;
        }
    }
}
