using Marathon.Collections;
using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    public class PrimitiveResourceTable : IResourceTable
    {
        public IndirectUnmanagedList<uint> Textures { get; set; } = [];

        public PrimitiveResourceTable() { }

        public PrimitiveResourceTable(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            Textures = new IndirectUnmanagedList<uint>(in_reader, in_parentChunk.Offset);

            in_reader.JumpAhead(sizeof(uint) * 6);
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            Textures.WriteInfo(in_writer);

            in_writer.JumpAhead(sizeof(uint) * 6);
        }

        public void WriteArray(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            Textures.WriteArray(in_writer, in_parentChunk.Offset);
        }

        public void WriteData(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            Textures.WriteData(in_writer, in_parentChunk.Offset);
        }
    }
}
