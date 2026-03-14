using Marathon.Collections;
using Marathon.Formats.Acroarts.Chunks;
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

        public ModelResourceTable(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            Models = new IndirectUnmanagedList<uint>(in_reader, in_parentChunk.Offset);
            Materials = new IndirectUnmanagedList<uint>(in_reader, in_parentChunk.Offset);
            Motions = new IndirectUnmanagedList<uint>(in_reader, in_parentChunk.Offset);
            Textures = new IndirectUnmanagedList<uint>(in_reader, in_parentChunk.Offset);
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            Models.WriteInfo(in_writer);
            Materials.WriteInfo(in_writer);
            Motions.WriteInfo(in_writer);
            Textures.WriteInfo(in_writer);
        }

        public void WriteArray(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            Models.WriteArray(in_writer, in_parentChunk.Offset);
            Materials.WriteArray(in_writer, in_parentChunk.Offset);
            Motions.WriteArray(in_writer, in_parentChunk.Offset);
            Textures.WriteArray(in_writer, in_parentChunk.Offset);
        }

        public void WriteData(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            Models.WriteData(in_writer, in_parentChunk.Offset);
            Materials.WriteData(in_writer, in_parentChunk.Offset);
            Motions.WriteData(in_writer, in_parentChunk.Offset);
            Textures.WriteData(in_writer, in_parentChunk.Offset);
        }
    }
}
