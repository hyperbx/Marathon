using Marathon.Formats.Ninja.Chunks;
using Marathon.Formats.Ninja.Flags;
using Marathon.IO;
using Marathon.IO.Extensions;

namespace Marathon.Formats.Ninja.Types
{
    public class TextureFile
    {
        public TextureFileType Type { get; set; }

        public string Name { get; set; }

        public MinFilter MinFilter { get; set; }

        public MagFilter MagFilter { get; set; }

        public uint GlobalIndex { get; set; }

        public uint Bank { get; set; }

        public TextureFile() { }

        public TextureFile(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Type = in_reader.Read<TextureFileType>();

            var nameOffset = in_reader.Read<uint>();

            in_reader.ReadAtOffset(InfoChunk.Size + nameOffset,
                () => Name = in_reader.ReadStringNullTerminated());

            MinFilter = in_reader.Read<MinFilter>();
            MagFilter = in_reader.Read<MagFilter>();
            GlobalIndex = in_reader.Read<uint>();
            Bank = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer, out uint out_nameOffset)
        {
            in_writer.Write(Type);

            out_nameOffset = in_writer.Reserve<uint>();

            in_writer.Write(MinFilter);
            in_writer.Write(MagFilter);
            in_writer.Write(GlobalIndex);
            in_writer.Write(Bank);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
