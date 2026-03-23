using Marathon.Formats.Acroarts.Collections;
using Marathon.IO;
using System.Collections.Generic;
using System.Linq;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    public class PrimitiveResourceTable : IResourceTable
    {
        public IndirectUnmanagedList<int> Textures { get; set; } = [];

        public PrimitiveResourceTable() { }

        public PrimitiveResourceTable(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Textures = new IndirectUnmanagedList<int>(in_reader);

            in_reader.JumpAhead(sizeof(int) * 6);
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer)
        {
            Textures.WriteInfo(in_writer);

            in_writer.JumpAhead(sizeof(int) * 6);
        }

        public void WriteArray(BinaryObjectWriterEx in_writer)
        {
            Textures.WriteArray(in_writer);
        }

        public void WriteData(BinaryObjectWriterEx in_writer)
        {
            Textures.WriteData(in_writer);
        }

        public int[] GetIndexes()
        {
            var result = new List<int>();

            result.AddRange(Textures);
            result.Sort();

            return [.. result.Distinct()];
        }
    }
}
