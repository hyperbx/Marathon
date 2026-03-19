using Marathon.Collections;
using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    public class ExtraResourceTable : IResourceTable
    {
        public IndirectUnmanagedList<uint> Extras { get; set; } = [];

        public ExtraResourceTable() { }

        public ExtraResourceTable(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Extras = new IndirectUnmanagedList<uint>(in_reader);

            in_reader.JumpAhead(sizeof(uint) * 6);
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer)
        {
            Extras.WriteInfo(in_writer);

            in_writer.JumpAhead(sizeof(uint) * 6);
        }

        public void WriteArray(BinaryObjectWriterEx in_writer)
        {
            Extras.WriteArray(in_writer);
        }

        public void WriteData(BinaryObjectWriterEx in_writer)
        {
            Extras.WriteData(in_writer);
        }
    }
}
