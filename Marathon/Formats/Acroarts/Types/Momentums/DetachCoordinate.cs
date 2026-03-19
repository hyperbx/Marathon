using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class DetachCoordinate : IMomentumParamSet
    {
        public float UnknownField1 { get; set; }

        public GTCounter GTCounter { get; set; }

        public DetachCoordinate() { }

        public DetachCoordinate(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            UnknownField1 = in_reader.Read<float>();
            GTCounter = in_reader.Read<GTCounter>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(UnknownField1);
            in_writer.Write(GTCounter);
        }

        public uint GetParamCount()
        {
            return 2;
        }
    }
}
