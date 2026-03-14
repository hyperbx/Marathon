using Marathon.Collections;
using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    public class ExtraResourceTable : IResourceTable
    {
        public IndirectUnmanagedList<uint> Extras { get; set; } = [];

        public ExtraResourceTable() { }

        public ExtraResourceTable(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            Extras = new IndirectUnmanagedList<uint>(in_reader, in_parentChunk.Offset);

            in_reader.JumpAhead(sizeof(uint) * 6);
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            Extras.WriteInfo(in_writer);

            in_writer.JumpAhead(sizeof(uint) * 6);
        }

        public void WriteArray(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            Extras.WriteArray(in_writer, in_parentChunk.Offset);
        }

        public void WriteData(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            Extras.WriteData(in_writer, in_parentChunk.Offset);
        }
    }
}
