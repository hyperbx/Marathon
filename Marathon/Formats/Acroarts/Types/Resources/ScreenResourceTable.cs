using Marathon.Collections;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    public class ScreenResourceTable : IResourceTable
    {
        public IndirectUnmanagedList<uint> Screens { get; set; } = [];

        public IndirectUnmanagedList<ScreenMotionIndex> ScreenMotions { get; set; } = [];

        public ScreenResourceTable() { }

        public ScreenResourceTable(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Screens = new IndirectUnmanagedList<uint>(in_reader);
            ScreenMotions = new IndirectUnmanagedList<ScreenMotionIndex>(in_reader);

            in_reader.JumpAhead(sizeof(uint) * 4);
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer)
        {
            Screens.WriteInfo(in_writer);
            ScreenMotions.WriteInfo(in_writer);

            in_writer.JumpAhead(sizeof(uint) * 4);
        }

        public void WriteArray(BinaryObjectWriterEx in_writer)
        {
            Screens.WriteArray(in_writer);
            ScreenMotions.WriteArray(in_writer);
        }

        public void WriteData(BinaryObjectWriterEx in_writer)
        {
            Screens.WriteData(in_writer);
            ScreenMotions.WriteData(in_writer);
        }
    }

    public struct ScreenMotionIndex
    {
        public uint Index;
        public uint MotionElement;
    }
}
