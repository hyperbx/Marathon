using Marathon.Formats.Ninja.Chunks;
using Marathon.IO;
using Marathon.IO.Extensions;

namespace Marathon.Formats.Ninja.Types
{
    public class Effect
    {
        public uint Type { get; set; }

        public string Name { get; set; }

        public Effect() { }

        public Effect(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Type = in_reader.Read<uint>();

            var nameOffset = in_reader.Read<uint>();

            in_reader.ReadAtOffset(InfoChunk.Size + nameOffset,
                () => Name = in_reader.ReadStringNullTerminated());
        }

        public long Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Type);

            return in_writer.Reserve<uint>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
