using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Mesh.Ninja.Types;
using Marathon.IO;
using Marathon.IO.Extensions;

namespace Marathon.Formats.Mesh.Ninja.Chunks
{
    public class EndChunk : IChunk
    {
        public const string ID = "NEND"; // Ninja END

        public EndChunk() { }

        public EndChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var header = in_reader.ReadObject<DataHeader>();

            if (!header.ID.Equals(GetChunkID()))
                throw new InvalidSignatureException(GetChunkID(), header.ID);

            in_reader.Align(16);
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            new DataHeader(in_writer, GetChunkID(), 0).FinishWrite(in_writer, 0);
        }

        public virtual string GetChunkID()
        {
            return ID;
        }
    }
}
