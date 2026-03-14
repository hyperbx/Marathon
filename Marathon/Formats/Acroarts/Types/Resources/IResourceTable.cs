using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types.Resources
{
    public interface IResourceTable
    {
        void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk);

        void WriteInfo(BinaryObjectWriterEx in_writer, IChunk in_parentChunk);

        void WriteArray(BinaryObjectWriterEx in_writer, IChunk in_parentChunk);

        void WriteData(BinaryObjectWriterEx in_writer, IChunk in_parentChunk);
    }
}
