using Marathon.Collections;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    public class LightResourceTable : IResourceTable
    {
        public IndirectUnmanagedList<uint> Lights { get; set; } = [];

        public IndirectUnmanagedList<uint> LightMotions { get; set; } = [];

        public LightResourceTable() { }

        public LightResourceTable(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Lights = new IndirectUnmanagedList<uint>(in_reader);
            LightMotions = new IndirectUnmanagedList<uint>(in_reader);

            in_reader.JumpAhead(sizeof(uint) * 4);
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer)
        {
            Lights.WriteInfo(in_writer);
            LightMotions.WriteInfo(in_writer);

            in_writer.JumpAhead(sizeof(uint) * 4);
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
    }
}
