using Amicitia.IO.Binary;
using Marathon.IO;
using Marathon.IO.Types;

namespace Marathon.Formats.Ninja.Chunks
{
    public class ChunkHeader : IBinarySerializable
    {
        private long _lengthOffset;
        private long _chunkStart;
        private long _dataOffset;
        private long _versionOffset;

        public const int Size = 0x10;

        public FourCC ID { get; set; }

        public uint Length { get; set; }

        public uint DataOffset { get; set; }

        public int Version { get; set; }

        public ChunkHeader() { }

        public ChunkHeader(FourCC in_id, uint in_length, uint in_dataOffset, int in_version = 0)
        {
            ID = in_id;
            Length = in_length;
            DataOffset = in_dataOffset;
            Version = in_version;
        }

        public ChunkHeader(string in_id, uint in_length, uint in_dataOffset, int in_version = 0)
            : this(new FourCC(in_id), in_length, in_dataOffset, in_version) { }

        public ChunkHeader(BinaryObjectWriterEx in_writer, FourCC in_id, int in_version = 0)
        {
            ID = in_id;
            Version = in_version;

            Reserve(in_writer);
        }

        public ChunkHeader(BinaryObjectWriterEx in_writer, string in_id, int in_version = 0)
            : this(in_writer, new FourCC(in_id), in_version) { }

        public void Read(BinaryObjectReader in_reader)
        {
            ID = in_reader.ReadObject<FourCC>();
            Length = in_reader.Read<uint>();
            DataOffset = in_reader.Read<uint>();
            Version = in_reader.Read<int>();
        }

        public void Write(BinaryObjectWriter in_writer)
        {
            in_writer.WriteObject(ID);
            in_writer.Write(Length);
            in_writer.Write(DataOffset);
            in_writer.Write(Version);
        }

        public void Reserve(BinaryObjectWriterEx in_writer)
        {
            in_writer.WriteObject(ID);
            _lengthOffset = in_writer.Reserve<uint>(true);
            _chunkStart = (uint)in_writer.Position;
            _dataOffset = in_writer.Reserve<uint>(true);
            _versionOffset = in_writer.Reserve<uint>(true);
        }

        public void FinishWrite(BinaryObjectWriterEx in_writer)
        {
            in_writer.WriteReserved(_lengthOffset, Length);
            in_writer.WriteReserved(_dataOffset, DataOffset);
            in_writer.WriteReserved(_versionOffset, Version);
        }

        public void FinishWrite(BinaryObjectWriterEx in_writer, uint in_dataOffset, int in_version = 0)
        {
            Length = (uint)(in_writer.Position - _chunkStart);
            DataOffset = in_dataOffset;
            Version = in_version;

            FinishWrite(in_writer);
        }

        public long GetChunkStart()
        {
            return _chunkStart;
        }
    }
}
