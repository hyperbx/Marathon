using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.IO;
using Marathon.IO.Extensions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class DataChunk : List<TrunkChunkParam>, IBinarySerializableEx
    {
        private bool _isChunkAligned = false;

        public const string ID = "ABDA"; // "Acroarts Binary DAta"

        [JsonIgnore]
        public RelocationTableChunk RelocationTableChunk { get; set; }

        public DataChunk() { }

        public DataChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            in_reader.PushOffsetOrigin(in_reader.Position);

            var chunkHeader = in_reader.ReadObject<ChunkHeader>();

            if (!chunkHeader.ID.Equals(ID))
                throw new InvalidSignatureException(ID, chunkHeader.ID);

            var version = in_reader.Read<uint>();

            if (version != AckResource.Version)
                throw new InvalidSignatureException(AckResource.Version, version);

            var trunkCount = in_reader.Read<uint>();
            var relocTableOffset = in_reader.Read<uint>();

            in_reader.JumpAhead(4); // Reserved.

            for (uint i = 0; i < trunkCount; i++)
            {
                var chunkOffset = in_reader.Read<uint>();
                var param = in_reader.Read<uint>();

                in_reader.ReadAtOffset(in_reader.CalculateOffset(chunkOffset), () =>
                {
                    Add(new(new TrunkChunk(in_reader), param));
                });
            }

            in_reader.JumpTo(in_reader.CalculateOffset(relocTableOffset));

            RelocationTableChunk = new RelocationTableChunk(in_reader);
            new EndOfChunk().Read(in_reader);

            _isChunkAligned = in_reader.ReadArray<byte>(0x10).Sum(x => x) == 0;

            in_reader.PopOffsetOrigin();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.PushOffsetOrigin(in_writer.Position);

            var chunkHeader = new ChunkHeader(in_writer, ID)
            {
                HeaderSize = 0x30
            };

            in_writer.Write(AckResource.Version);
            in_writer.Write(Count);

            var relocTableOffset = in_writer.Reserve<uint>(true);

            in_writer.WriteZero<int>(); // Reserved.

            if (Count <= 0)
            {
                // Empty chunk array.
                in_writer.WriteZero<long>();
                in_writer.Align(16);
            }
            else
            {
                var trunkOffsets = new List<long>();

                foreach (var trunk in this)
                {
                    trunkOffsets.Add(in_writer.Reserve<uint>());
                    in_writer.Write(trunk.Parameter);
                }

                in_writer.Align(16);

                chunkHeader.HeaderSize = (uint)(in_writer.Position - in_writer.OffsetOrigin);

                for (int i = 0; i < Count; i++)
                {
                    in_writer.WriteReserved(trunkOffsets[i], (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
                    this[i].Trunk.Write(in_writer);
                }
            }

            in_writer.Align(16);
            in_writer.WriteReserved(relocTableOffset, (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative));

            new RelocationTableChunk().Write(in_writer);

            chunkHeader.FinishWrite(in_writer, (uint)(in_writer.Position - in_writer.OffsetOrigin - chunkHeader.HeaderSize));

            new EndOfChunk().Write(in_writer);

            if (_isChunkAligned)
                in_writer.WriteZero<byte>(0x10);

            in_writer.PopOffsetOrigin();
        }
    }

    public struct TrunkChunkParam(TrunkChunk in_trunk, uint in_parameter)
    {
        public TrunkChunk Trunk = in_trunk;
        public uint Parameter = in_parameter;
    }
}
