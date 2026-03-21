using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.IO;
using Marathon.IO.Extensions;

namespace Marathon.Formats.Ninja.Chunks
{
    public class EndChunk : IChunk
    {
        public const string ID = "NEND"; // "Ninja END"

        public EndChunk() { }

        public EndChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var header = in_reader.ReadObject<ChunkHeader>();

            if (!header.ID.Equals(GetChunkID()))
                throw new InvalidSignatureException(GetChunkID(), header.ID);

            in_reader.Align(16);
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            new ChunkHeader(in_writer, GetChunkID(), 0).FinishWrite(in_writer, 0);
        }

        public virtual string GetChunkID()
        {
            return ID;
        }
    }
}
