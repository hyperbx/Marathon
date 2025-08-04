using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types;

namespace Marathon.Formats.Mesh.Ninja.Chunks
{
    public class EndChunk : IChunk
    {
        public const string ID = "NEND";

        public string ChunkID { get; set; } = ID;

        public EndChunk() { }

        public EndChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var chunkSignature = in_reader.Read<FourCC>();
            var chunkLength = in_reader.Read<uint>();

            if (!chunkSignature.Equals(ID))
                throw new InvalidSignatureException(ID, chunkSignature);

            in_reader.Align(16);
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.WriteSignature(ID);
            var length = in_writer.Reserve<uint>();
            in_writer.Align(16);
            in_writer.WriteReserved(length, (int)(in_writer.Position - (length + 4)));
        }
    }
}
