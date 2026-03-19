using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class SoundPlay : IMomentumParamSet
    {
        public string SoundBankName { get; set; } = "event";

        public string SoundName { get; set; }

        public SoundPlay() { }

        public SoundPlay(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var soundBankNameOffset = in_reader.Read<uint>();
            var soundNameOffset = in_reader.Read<uint>();

            in_reader.ReadAtOffset(in_reader.CalculateOffset(soundBankNameOffset),
                () => SoundBankName = FixedString.Read(in_reader));

            in_reader.ReadAtOffset(in_reader.CalculateOffset(soundNameOffset),
                () => SoundName = FixedString.Read(in_reader));
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var soundBankNameOffset = in_writer.Reserve<uint>();
            var soundNameOffset = in_writer.Reserve<uint>();

            FixedString.Write(in_writer, SoundBankName, soundBankNameOffset);
            FixedString.Write(in_writer, SoundName, soundNameOffset);
        }

        public uint GetParamCount()
        {
            return 2;
        }
    }
}
