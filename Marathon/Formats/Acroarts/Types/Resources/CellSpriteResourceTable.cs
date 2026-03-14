using Marathon.Collections;
using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    public class CellSpriteResourceTable : IResourceTable
    {
        public IndirectUnmanagedList<CellSpriteIndex> CellSprites { get; set; } = [];

        public IndirectUnmanagedList<uint> Textures { get; set; } = [];

        public CellSpriteResourceTable() { }

        public CellSpriteResourceTable(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            CellSprites = new IndirectUnmanagedList<CellSpriteIndex>(in_reader, in_parentChunk.Offset);
            Textures = new IndirectUnmanagedList<uint>(in_reader, in_parentChunk.Offset);

            in_reader.JumpAhead(sizeof(uint) * 4);
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            CellSprites.WriteInfo(in_writer);
            Textures.WriteInfo(in_writer);

            in_writer.JumpAhead(sizeof(uint) * 4);
        }

        public void WriteArray(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            CellSprites.WriteArray(in_writer, in_parentChunk.Offset);
            Textures.WriteArray(in_writer, in_parentChunk.Offset);
        }

        public void WriteData(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            CellSprites.WriteData(in_writer, in_parentChunk.Offset);
            Textures.WriteData(in_writer, in_parentChunk.Offset);
        }
    }

    public struct CellSpriteIndex
    {
        public uint Index;
        public uint DefaultAction;
    }
}
