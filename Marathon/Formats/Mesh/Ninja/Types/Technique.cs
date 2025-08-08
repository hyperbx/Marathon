using Marathon.Formats.Mesh.Ninja.Chunks;
using Marathon.IO;
using Marathon.IO.Extensions;

namespace Marathon.Formats.Mesh.Ninja.Types
{
    public class Technique
    {
        public const int Size = 8;

        public uint Type { get; set; }

        public uint EffectIndex { get; set; }

        public string Name { get; set; }

        public Technique() { }

        public Technique(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Type = in_reader.Read<uint>();
            EffectIndex = in_reader.Read<uint>();

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

        public override string ToString()
        {
            return Name;
        }
    }
}
