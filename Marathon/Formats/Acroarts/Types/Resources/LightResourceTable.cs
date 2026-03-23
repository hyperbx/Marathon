using Marathon.Formats.Acroarts.Collections;
using Marathon.IO;
using System.Collections.Generic;
using System.Linq;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    public class LightResourceTable : IResourceTable
    {
        public IndirectUnmanagedList<int> Lights { get; set; } = [];

        public IndirectUnmanagedList<int> LightMotions { get; set; } = [];

        public LightResourceTable() { }

        public LightResourceTable(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Lights = new IndirectUnmanagedList<int>(in_reader);
            LightMotions = new IndirectUnmanagedList<int>(in_reader);

            in_reader.JumpAhead(sizeof(int) * 4);
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer)
        {
            Lights.WriteInfo(in_writer);
            LightMotions.WriteInfo(in_writer);

            in_writer.JumpAhead(sizeof(int) * 4);
        }

        public void WriteArray(BinaryObjectWriterEx in_writer)
        {
            Lights.WriteArray(in_writer);
            LightMotions.WriteArray(in_writer);
        }

        public void WriteData(BinaryObjectWriterEx in_writer)
        {
            Lights.WriteData(in_writer);
            LightMotions.WriteData(in_writer);
        }

        public int[] GetIndexes()
        {
            var result = new List<int>();

            result.AddRange(Lights);
            result.AddRange(LightMotions);
            result.Sort();

            return [.. result.Distinct()];
        }
    }
}
