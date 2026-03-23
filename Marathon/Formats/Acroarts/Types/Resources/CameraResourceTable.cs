using Marathon.Formats.Acroarts.Collections;
using Marathon.IO;
using System.Collections.Generic;
using System.Linq;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    public class CameraResourceTable : IResourceTable
    {
        public IndirectUnmanagedList<int> Cameras { get; set; } = [];

        public IndirectUnmanagedList<int> CameraMotions { get; set; } = [];

        public CameraResourceTable() { }

        public CameraResourceTable(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Cameras = new IndirectUnmanagedList<int>(in_reader);
            CameraMotions = new IndirectUnmanagedList<int>(in_reader);

            in_reader.JumpAhead(sizeof(int) * 4);
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer)
        {
            Cameras.WriteInfo(in_writer);
            CameraMotions.WriteInfo(in_writer);

            in_writer.JumpAhead(sizeof(int) * 4);
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

        public int[] GetIndexes()
        {
            var result = new List<int>();

            result.AddRange(Cameras);
            result.AddRange(CameraMotions);
            result.Sort();

            return [.. result.Distinct()];
        }
    }
}
