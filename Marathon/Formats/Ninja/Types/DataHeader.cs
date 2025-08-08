using Amicitia.IO.Binary;
using Marathon.IO;
using Marathon.IO.Types;

namespace Marathon.Formats.Ninja.Types
{
    public class DataHeader : IBinarySerializable
    {
        private uint _lengthOffset;
        private uint _chunkStart;
        private uint _dataOffset;
        private uint _versionOffset;

        public const int Size = 0x10;

        public FourCC ID { get; set; }

        public uint Length { get; set; }

        public uint DataOffset { get; set; }

        public int Version { get; set; }

        public DataHeader() { }

        public DataHeader(FourCC in_id, uint in_length, uint in_dataOffset, int in_version = 0)
        {
            ID = in_id;
            Length = in_length;
            DataOffset = in_dataOffset;
            Version = in_version;
        }

        public DataHeader(string in_id, uint in_length, uint in_dataOffset, int in_version = 0)
            : this(new FourCC(in_id), in_length, in_dataOffset, in_version) { }

        public DataHeader(BinaryObjectWriterEx in_writer, FourCC in_id, int in_version = 0)
        {
            ID = in_id;
            Version = in_version;

            Reserve(in_writer);
        }

        public DataHeader(BinaryObjectWriterEx in_writer, string in_id, int in_version = 0)
            : this(in_writer, new FourCC(in_id), in_version) { }

        public void Read(BinaryObjectReader in_reader)
        {
            ID = in_reader.Read<FourCC>();
            Length = in_reader.Read<uint>();
            DataOffset = in_reader.Read<uint>();
            Version = in_reader.Read<int>();
        }

        public void Write(BinaryObjectWriter in_writer)
        {
            in_writer.Write(ID);
            in_writer.Write(Length);
            in_writer.Write(DataOffset);
            in_writer.Write(Version);
        }

        public void Reserve(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(ID);
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

        public uint GetChunkStart()
        {
            return _chunkStart;
        }
    }
}
