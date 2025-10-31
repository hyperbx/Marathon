using Marathon.Formats.Ninja.Chunks;
using Marathon.IO;
using Marathon.IO.Extensions;

namespace Marathon.Formats.Ninja.Types
{
    public class Technique
    {
        public uint Type { get; set; }

        public int EffectIndex { get; set; }

        public string Name { get; set; }

        public Technique() { }

        public Technique(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Type = in_reader.Read<uint>();
            EffectIndex = in_reader.Read<int>();

            var nameOffset = in_reader.Read<uint>();

            in_reader.ReadAtOffset(InfoChunk.Size + nameOffset,
                () => Name = in_reader.ReadStringNullTerminated());
        }

        public long Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Type);
            in_writer.Write(EffectIndex);

            return in_writer.Reserve<uint>();
        }

        public Effect GetEffect(EffectListChunk in_effectListChunk)
        {
            return in_effectListChunk.Effects[EffectIndex];
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
