using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types;

namespace Marathon.Formats.Mesh.Ninja.Chunks
{
    public class UndefinedChunk : IChunk
    {
        public string ChunkID { get; set; }

        public FourCC Signature { get; set; }

        public byte[] Data { get; set; }

        public UndefinedChunk() { }

        public UndefinedChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);

            ChunkID = Signature.ToString();
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Signature = in_reader.Read<FourCC>();

            var chunkLength = in_reader.Read<uint>();

            Data = in_reader.ReadBytes((int)chunkLength);
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Signature);
            var length = in_writer.Reserve<uint>();
            in_writer.WriteBytes(Data);
            in_writer.WriteReserved(length, (int)(in_writer.Position - (length + 4)));
        }
    }
}
