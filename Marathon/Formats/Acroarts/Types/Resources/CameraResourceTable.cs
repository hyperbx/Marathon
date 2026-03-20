using Marathon.Formats.Acroarts.Collections;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    public class CameraResourceTable : IResourceTable
    {
        public IndirectUnmanagedList<uint> Cameras { get; set; } = [];

        public IndirectUnmanagedList<uint> CameraMotions { get; set; } = [];

        public CameraResourceTable() { }

        public CameraResourceTable(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Cameras = new IndirectUnmanagedList<uint>(in_reader);
            CameraMotions = new IndirectUnmanagedList<uint>(in_reader);

            in_reader.JumpAhead(sizeof(uint) * 4);
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer)
        {
            Cameras.WriteInfo(in_writer);
            CameraMotions.WriteInfo(in_writer);

            in_writer.JumpAhead(sizeof(uint) * 4);
        }

        public void WriteArray(BinaryObjectWriterEx in_writer)
        {
            Cameras.WriteArray(in_writer);
            CameraMotions.WriteArray(in_writer);
        }

        public void WriteData(BinaryObjectWriterEx in_writer)
        {
            Cameras.WriteData(in_writer);
            CameraMotions.WriteData(in_writer);
        }
    }
}
