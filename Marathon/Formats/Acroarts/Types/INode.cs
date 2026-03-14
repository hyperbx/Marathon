using Marathon.Formats.Acroarts.Chunks;
using Marathon.IO;

namespace Marathon.Formats.Acroarts.Types
{
    public interface INode
    {
        void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null);

        void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null);
    }
}
