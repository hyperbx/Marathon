using Marathon.Collections;
using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    public class ScreenResourceTable : IResourceTable
    {
        public IndirectUnmanagedList<uint> Screens { get; set; } = [];

        public IndirectUnmanagedList<ScreenMotionIndex> ScreenMotions { get; set; } = [];

        public ScreenResourceTable() { }

        public ScreenResourceTable(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            Screens = new IndirectUnmanagedList<uint>(in_reader, in_parentChunk.Offset);
            ScreenMotions = new IndirectUnmanagedList<ScreenMotionIndex>(in_reader, in_parentChunk.Offset);

            in_reader.JumpAhead(sizeof(uint) * 4);
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            Screens.WriteInfo(in_writer);
            ScreenMotions.WriteInfo(in_writer);

            in_writer.JumpAhead(sizeof(uint) * 4);
        }

        public void WriteArray(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            Screens.WriteArray(in_writer, in_parentChunk.Offset);
            ScreenMotions.WriteArray(in_writer, in_parentChunk.Offset);
        }

        public void WriteData(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            Screens.WriteData(in_writer, in_parentChunk.Offset);
            ScreenMotions.WriteData(in_writer, in_parentChunk.Offset);
        }
    }

    public struct ScreenMotionIndex
    {
        public uint Index;
        public uint MotionElement;
    }
}
