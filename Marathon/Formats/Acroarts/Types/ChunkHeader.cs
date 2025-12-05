using Amicitia.IO.Binary;
using Marathon.IO;
using Marathon.IO.Types;
using System.IO;

namespace Marathon.Formats.Acroarts.Types
{
    public class ChunkHeader : IBinarySerializable
    {
        private const uint _defaultHeaderSize = 16;

        private uint _lengthOffset;
        private uint _chunkStart;
        private uint _headerSize;
        private uint _chunkflags;

        public FourCC ID { get; set; }

        public uint Length { get; set; }

        public uint HeaderSize { get; set; } = _defaultHeaderSize;

        public ChunkHeader() { }

        public ChunkHeader(FourCC in_id, uint in_length, uint in_headerSize,uint in_chankflags)
        {
            ID = in_id;
            Length = in_length;
            HeaderSize = in_headerSize;
            _chunkflags = in_chankflags;
        }

        public ChunkHeader(string in_id, uint in_length, uint in_headerSize, Endianness in_endianness,uint in_chankflags)
            : this(new FourCC(in_id, in_endianness), in_length, in_headerSize, in_chankflags) { }

        public ChunkHeader(BinaryObjectWriterEx in_writer, FourCC in_id)
        {
            ID = in_id;

            Reserve(in_writer);
        }

        public ChunkHeader(BinaryObjectWriterEx in_writer, string in_id)
            : this(in_writer, new FourCC(in_id, in_writer.Endianness)) { }

        public void Read(BinaryObjectReader in_reader)
        {
            ID = in_reader.ReadObject<FourCC>();
            Length = in_reader.Read<uint>();
            HeaderSize = in_reader.Read<uint>();
            _chunkflags = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriter in_writer)
        {
            in_writer.WriteObject(ID);
            in_writer.Write(Length);
            in_writer.Write(HeaderSize);
            in_writer.Write(_chunkflags);
        }

        public void Reserve(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(ID);
            _lengthOffset = in_writer.Reserve<uint>(true);
            _headerSize = in_writer.Reserve<uint>(true);
            _chunkflags = in_writer.Reserve<uint>();
            _chunkStart = (uint)in_writer.Position;
        }

        public void FinishWrite(BinaryObjectWriterEx in_writer)
        {
            in_writer.WriteReserved(_lengthOffset, Length);
            in_writer.WriteReserved(_headerSize, HeaderSize);
        }

        public void FinishWrite(BinaryObjectWriterEx in_writer, uint in_headerSize = _defaultHeaderSize)
        {
            Length = (uint)(in_writer.Position - _chunkStart);
            HeaderSize = in_headerSize;

            FinishWrite(in_writer);
        }

        public uint GetChunkStart()
        {
            return _chunkStart;
        }
    }
}
