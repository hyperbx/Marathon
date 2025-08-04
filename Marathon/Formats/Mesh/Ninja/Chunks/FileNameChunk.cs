using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types;

namespace Marathon.Formats.Mesh.Ninja.Chunks
{
    public class FileNameChunk : IChunk
    {
        public const string ID = "NFN0";

        public string ChunkID { get; set; } = ID;

        public string Name { get; set; }

        public FileNameChunk() { }

        public FileNameChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public FileNameChunk(string in_name)
        {
            Name = in_name;
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var chunkSignature = in_reader.Read<FourCC>();
            var chunkLength = in_reader.Read<uint>();

            if (!chunkSignature.Equals(ID))
                throw new InvalidSignatureException(ID, chunkSignature);

            in_reader.Align(16);

            Name = in_reader.ReadStringNullTerminated();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.WriteSignature(ID);
            var length = in_writer.Reserve<uint>();
            in_writer.Align(16);
            in_writer.WriteStringNullTerminated(Name);
            in_writer.Align(16);
            in_writer.WriteReserved(length, (int)(in_writer.Position - (length + 4)));
        }
    }
}
