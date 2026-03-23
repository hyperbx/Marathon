using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    public interface IResourceTable
    {
        void Read(BinaryObjectReaderEx in_reader);

        void WriteInfo(BinaryObjectWriterEx in_writer);

        void WriteArray(BinaryObjectWriterEx in_writer);

        void WriteData(BinaryObjectWriterEx in_writer);

        int[] GetIndexes();
    }
}
