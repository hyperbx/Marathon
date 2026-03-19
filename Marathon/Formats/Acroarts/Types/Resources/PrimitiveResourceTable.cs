using Marathon.Collections;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    public class PrimitiveResourceTable : IResourceTable
    {
        public IndirectUnmanagedList<uint> Textures { get; set; } = [];

        public PrimitiveResourceTable() { }

        public PrimitiveResourceTable(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Textures = new IndirectUnmanagedList<uint>(in_reader);

            in_reader.JumpAhead(sizeof(uint) * 6);
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer)
        {
            Textures.WriteInfo(in_writer);

            in_writer.JumpAhead(sizeof(uint) * 6);
        }

        public void WriteArray(BinaryObjectWriterEx in_writer)
        {
            Textures.WriteArray(in_writer);
        }

        public void WriteData(BinaryObjectWriterEx in_writer)
        {
            Textures.WriteData(in_writer);
        }
    }
}
