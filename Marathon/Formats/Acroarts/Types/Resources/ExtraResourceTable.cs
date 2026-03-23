using Marathon.Formats.Acroarts.Collections;
using Marathon.IO;
using System.Collections.Generic;
using System.Linq;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    public class ExtraResourceTable : IResourceTable
    {
        public IndirectUnmanagedList<int> Extras { get; set; } = [];

        public ExtraResourceTable() { }

        public ExtraResourceTable(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Extras = new IndirectUnmanagedList<int>(in_reader);

            in_reader.JumpAhead(sizeof(int) * 6);
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer)
        {
            Extras.WriteInfo(in_writer);

            in_writer.JumpAhead(sizeof(int) * 6);
        }

        public void WriteArray(BinaryObjectWriterEx in_writer)
        {
            Extras.WriteArray(in_writer);
        }

        public void WriteData(BinaryObjectWriterEx in_writer)
        {
            Extras.WriteData(in_writer);
        }

        public int[] GetIndexes()
        {
            var result = new List<int>();

            result.AddRange(Extras);
            result.Sort();

            return [.. result.Distinct()];
        }
    }
}
