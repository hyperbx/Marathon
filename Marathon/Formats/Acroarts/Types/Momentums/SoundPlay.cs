using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;
using Marathon.IO.Extensions;

namespace Marathon.Formats.Acroarts.Types.Momentums
{
    public class SoundPlay : IMomentumParamSet
    {
        public string SoundBankName { get; set; } = "event";

        public string SoundName { get; set; }

        public SoundPlay() { }

        public SoundPlay(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            var soundBankNameOffset = in_reader.Read<uint>();
            var soundNameOffset = in_reader.Read<uint>();

            in_reader.ReadAtOffset(in_parentChunk.Offset + soundBankNameOffset,
                () => SoundBankName = in_reader.ReadStringFixedLength(0x80));

            in_reader.ReadAtOffset(in_parentChunk.Offset + soundNameOffset,
                () => SoundName = in_reader.ReadStringFixedLength(0x80));
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            var soundBankNameOffset = in_writer.Reserve<uint>();
            var soundNameOffset = in_writer.Reserve<uint>();

            in_writer.WriteZero<long>();
            in_writer.WriteReserved(soundBankNameOffset, (uint)(in_writer.Position - in_parentChunk.Offset), false);
            in_writer.WriteStringFixedLength(SoundBankName, 0x80);

            in_writer.WriteZero<long>();
            in_writer.WriteReserved(soundNameOffset, (uint)(in_writer.Position - in_parentChunk.Offset), false);
            in_writer.WriteStringFixedLength(SoundName, 0x80);
        }

        public uint GetParamCount()
        {
            return 2;
        }
    }
}
