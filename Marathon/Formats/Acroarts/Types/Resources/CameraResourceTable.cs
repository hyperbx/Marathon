using Marathon.Collections;
using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    public class CameraResourceTable : IResourceTable
    {
        public IndirectUnmanagedList<uint> Cameras { get; set; } = [];

        public IndirectUnmanagedList<uint> CameraMotions { get; set; } = [];

        public CameraResourceTable() { }

        public CameraResourceTable(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            Read(in_reader, in_parentChunk);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk)
        {
            Cameras = new IndirectUnmanagedList<uint>(in_reader, in_parentChunk.Offset);
            CameraMotions = new IndirectUnmanagedList<uint>(in_reader, in_parentChunk.Offset);

            in_reader.JumpAhead(sizeof(uint) * 4);
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            Cameras.WriteInfo(in_writer);
            CameraMotions.WriteInfo(in_writer);

            in_writer.JumpAhead(sizeof(uint) * 4);
        }

        public void WriteArray(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            Cameras.WriteArray(in_writer, in_parentChunk.Offset);
            CameraMotions.WriteArray(in_writer, in_parentChunk.Offset);
        }

        public void WriteData(BinaryObjectWriterEx in_writer, IChunk in_parentChunk)
        {
            Cameras.WriteData(in_writer, in_parentChunk.Offset);
            CameraMotions.WriteData(in_writer, in_parentChunk.Offset);
        }
    }
}
