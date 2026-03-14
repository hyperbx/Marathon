using Marathon.Collections;
using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    public class LightResourceTable : IResourceTable
    {
        public IndirectUnmanagedList<uint> Lights { get; set; } = [];

        public IndirectUnmanagedList<uint> LightMotions { get; set; } = [];

        public LightResourceTable() { }

        public LightResourceTable(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            Lights = new IndirectUnmanagedList<uint>(in_reader, in_parentChunk.Offset);
            LightMotions = new IndirectUnmanagedList<uint>(in_reader, in_parentChunk.Offset);

            in_reader.JumpAhead(sizeof(uint) * 4);
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            Lights.WriteInfo(in_writer);
            LightMotions.WriteInfo(in_writer);

            in_writer.JumpAhead(sizeof(uint) * 4);
        }

        public void WriteArray(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            Lights.WriteArray(in_writer, in_parentChunk.Offset);
            LightMotions.WriteArray(in_writer, in_parentChunk.Offset);
        }

        public void WriteData(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            Lights.WriteData(in_writer, in_parentChunk.Offset);
            LightMotions.WriteData(in_writer, in_parentChunk.Offset);
        }
    }
}
