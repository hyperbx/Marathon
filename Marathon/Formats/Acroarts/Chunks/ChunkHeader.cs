using Amicitia.IO;
using Amicitia.IO.Binary;
using Marathon.IO;
using Marathon.IO.Types;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class ChunkHeader : IBinarySerializable
    {
        public const uint DefaultHeaderSize = 16;

        private long _lengthOffset;
        private long _chunkStart;
        private long _headerSizeOffset;
        private long _systemFlagsOffset;
        private long _userFlagsOffset;

        public FourCC ID { get; set; }

        public uint Length { get; set; }

        public uint HeaderSize { get; set; } = DefaultHeaderSize;

        public ushort SystemFlags { get; set; }

        public ushort UserFlags { get; set; }

        public ChunkHeader() { }

        public ChunkHeader(FourCC in_id, uint in_length, uint in_headerSize, ushort in_systemFlags, ushort in_userFlags)
        {
            ID = in_id;
            Length = in_length;
            HeaderSize = in_headerSize;
            SystemFlags = in_systemFlags;
            UserFlags = in_userFlags;
        }

        public ChunkHeader(string in_id, uint in_length, uint in_headerSize, ushort in_systemFlags, ushort in_userFlags)
            : this(new FourCC(in_id), in_length, in_headerSize, in_systemFlags, in_userFlags) { }

        public ChunkHeader(BinaryObjectWriterEx in_writer, FourCC in_id)
        {
            ID = in_id;

            Reserve(in_writer);
        }

        public ChunkHeader(BinaryObjectWriterEx in_writer, string in_id)
            : this(in_writer, new FourCC(in_id)) { }

        public void Read(BinaryObjectReader in_reader)
        {
            ID = in_reader.ReadObject<FourCC>();
            Length = in_reader.Read<uint>();
            HeaderSize = in_reader.Read<uint>();
            SystemFlags = in_reader.Read<ushort>();
            UserFlags = in_reader.Read<ushort>();
        }

        public void Write(BinaryObjectWriter in_writer)
        {
            in_writer.WriteObject(ID);
            in_writer.Write(Length);
            in_writer.Write(HeaderSize);
            in_writer.Write(SystemFlags);
            in_writer.Write(UserFlags);
        }

        public void Reserve(BinaryObjectWriterEx in_writer)
        {
            in_writer.WriteObject(ID);
            _lengthOffset = in_writer.Reserve<uint>(true);
            _headerSizeOffset = in_writer.Reserve<uint>(true);
            _systemFlagsOffset = in_writer.Reserve<ushort>(true);
            _userFlagsOffset = in_writer.Reserve<ushort>(true);
            _chunkStart = (uint)in_writer.Position;
        }

        public void FinishWrite(BinaryObjectWriterEx in_writer)
        {
            in_writer.WriteReserved(_lengthOffset, (uint)AlignmentHelper.Align(in_writer.Position - _chunkStart, 16));
            in_writer.WriteReserved(_headerSizeOffset, HeaderSize);
            in_writer.WriteReserved(_systemFlagsOffset, SystemFlags);
            in_writer.WriteReserved(_userFlagsOffset, UserFlags);
        }

        public void FinishWrite(BinaryObjectWriterEx in_writer, uint in_length, uint in_headerSize, ushort in_systemFlags, ushort in_userFlags)
        {
            in_writer.WriteReserved(_lengthOffset, in_length);
            in_writer.WriteReserved(_headerSizeOffset, in_headerSize);
            in_writer.WriteReserved(_systemFlagsOffset, in_systemFlags);
            in_writer.WriteReserved(_userFlagsOffset, in_userFlags);
        }

        public void FinishWrite(BinaryObjectWriterEx in_writer, uint in_length, uint in_headerSize)
        {
            FinishWrite(in_writer, in_length, in_headerSize, SystemFlags, UserFlags);
        }

        public void FinishWrite(BinaryObjectWriterEx in_writer, uint in_length)
        {
            FinishWrite(in_writer, in_length, HeaderSize, SystemFlags, UserFlags);
        }

        public long GetChunkStart()
        {
            return _chunkStart;
        }
    }
}
