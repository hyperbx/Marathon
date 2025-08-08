using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Ninja.Types;
using Marathon.IO;
using Marathon.IO.Extensions;

namespace Marathon.Formats.Ninja.Chunks
{
    public class FileNameChunk : IChunk
    {
        public const string ID = "NFN0"; // Ninja File Name

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
            var header = in_reader.ReadObject<DataHeader>();

            if (!header.ID.Equals(GetChunkID()))
                throw new InvalidSignatureException(GetChunkID(), header.ID);

            Name = in_reader.ReadStringNullTerminated();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var header = new DataHeader(in_writer, GetChunkID(), 0);

            in_writer.WriteStringNullTerminated(Name);
            in_writer.Align(16);

            header.FinishWrite(in_writer, 0);
        }

        public virtual string GetChunkID()
        {
            return ID;
        }
    }
}
