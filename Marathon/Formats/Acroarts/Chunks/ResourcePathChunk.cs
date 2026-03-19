using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types;
using System.IO.Enumeration;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class ResourcePathChunk : IBinarySerializableEx
    {
        public string Path { get; set; }

        public ResourcePathChunk() { }

        public ResourcePathChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public ResourcePathChunk(string in_path)
        {
            Path = in_path;
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var chunkHeader = in_reader.ReadObject<ChunkHeader>();

            Path = in_reader.ReadStringFixedLength((int)chunkHeader.Length);
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var chunkHeader = new ChunkHeader(in_writer, GetResourceIDFromPath(Path));
            var position = in_writer.Position;

            in_writer.WriteStringNullTerminated(Path);
            in_writer.Align(16);

            chunkHeader.FinishWrite(in_writer, (uint)(in_writer.Position - position), ChunkHeader.DefaultHeaderSize, 0x40, 0);
        }

        public static FourCC GetResourceIDFromPath(string in_path)
        {
            var id = "    ";
            var extension = System.IO.Path.GetExtension(in_path).ToLower();

            switch (extension)
            {
                case ".dds":
                    id = "DDS ";
                    break;

                case ".xncp":
                    id = "FAPC";
                    break;

                case var _ when FileSystemName.MatchesSimpleExpression(".xn*", extension):
                    id = "NXIF";
                    break;
            };

            return new FourCC(id);
        }
    }
}
