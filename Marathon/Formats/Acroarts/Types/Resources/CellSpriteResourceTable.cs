using Marathon.Formats.Acroarts.Collections;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    public class CellSpriteResourceTable : IResourceTable
    {
        public IndirectUnmanagedList<CellSpriteIndex> CellSprites { get; set; } = [];

        public IndirectUnmanagedList<uint> Textures { get; set; } = [];

        public CellSpriteResourceTable() { }

        public CellSpriteResourceTable(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            CellSprites = new IndirectUnmanagedList<CellSpriteIndex>(in_reader);
            Textures = new IndirectUnmanagedList<uint>(in_reader);

            in_reader.JumpAhead(sizeof(uint) * 4);
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer)
        {
            CellSprites.WriteInfo(in_writer);
            Textures.WriteInfo(in_writer);

            in_writer.JumpAhead(sizeof(uint) * 4);
        }

        public void WriteArray(BinaryObjectWriterEx in_writer)
        {
            CellSprites.WriteArray(in_writer);
            Textures.WriteArray(in_writer);
        }

        public void WriteData(BinaryObjectWriterEx in_writer)
        {
            CellSprites.WriteData(in_writer);
            Textures.WriteData(in_writer);
        }
    }

    public struct CellSpriteIndex
    {
        public uint Index;
        public uint DefaultAction;
    }
}
