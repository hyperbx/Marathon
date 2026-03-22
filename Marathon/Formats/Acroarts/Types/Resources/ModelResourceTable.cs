using Marathon.Formats.Acroarts.Collections;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    public class ModelResourceTable : IResourceTable
    {
        public IndirectUnmanagedList<uint> Models { get; set; } = [];

        public IndirectUnmanagedList<uint> Materials { get; set; } = [];

        public IndirectUnmanagedList<uint> Motions { get; set; } = [];

        public IndirectUnmanagedList<uint> Textures { get; set; } = [];

        public ModelResourceTable() { }

        public ModelResourceTable(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Models = new IndirectUnmanagedList<uint>(in_reader);
            Materials = new IndirectUnmanagedList<uint>(in_reader);
            Motions = new IndirectUnmanagedList<uint>(in_reader);
            Textures = new IndirectUnmanagedList<uint>(in_reader);
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
    }
}
