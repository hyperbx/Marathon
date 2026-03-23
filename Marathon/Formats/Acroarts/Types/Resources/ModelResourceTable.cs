using Marathon.Formats.Acroarts.Collections;
using Marathon.IO;
using System.Collections.Generic;
using System.Linq;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    public class ModelResourceTable : IResourceTable
    {
        public IndirectUnmanagedList<int> Models { get; set; } = [];

        public IndirectUnmanagedList<int> Materials { get; set; } = [];

        public IndirectUnmanagedList<int> Motions { get; set; } = [];

        public IndirectUnmanagedList<int> Textures { get; set; } = [];

        public ModelResourceTable() { }

        public ModelResourceTable(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Models = new IndirectUnmanagedList<int>(in_reader);
            Materials = new IndirectUnmanagedList<int>(in_reader);
            Motions = new IndirectUnmanagedList<int>(in_reader);
            Textures = new IndirectUnmanagedList<int>(in_reader);
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer)
        {
            Models.WriteInfo(in_writer);
            Materials.WriteInfo(in_writer);
            Motions.WriteInfo(in_writer);
            Textures.WriteInfo(in_writer);
        }

        public void WriteArray(BinaryObjectWriterEx in_writer)
        {
            Models.WriteArray(in_writer);
            Materials.WriteArray(in_writer);
            Motions.WriteArray(in_writer);
            Textures.WriteArray(in_writer);
        }

        public void WriteData(BinaryObjectWriterEx in_writer)
        {
            Models.WriteData(in_writer);
            Materials.WriteData(in_writer);
            Motions.WriteData(in_writer);
            Textures.WriteData(in_writer);
        }

        public int[] GetIndexes()
        {
            var result = new List<int>();

            result.AddRange(Models);
            result.AddRange(Materials);
            result.AddRange(Motions);
            result.AddRange(Textures);
            result.Sort();

            return [.. result.Distinct()];
        }
    }
}
