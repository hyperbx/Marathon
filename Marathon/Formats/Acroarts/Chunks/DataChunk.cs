using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class DataChunk : List<TrunkChunkParam>, IChunk
    {
        public const string ID = "ABDA"; // "Acroarts Binary DAta"

        public long Offset { get; set; }

        [JsonIgnore]
        public RelocationTableChunk RelocationTableChunk { get; set; }

        public DataChunk() { }

        public DataChunk(BINAReader in_reader)
        {
            Read(in_reader, null);
        }

        public void Read(BinaryObjectReaderEx in_reader, IChunk in_parentChunk = null)
        {
            Offset = in_reader.Position;

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

                in_reader.ReadAtOffset(Offset + chunkOffset, () =>
                {
                    Add(new(new TrunkChunk(in_reader, this), param));
                });
            }

            in_reader.JumpTo(relocTableOffset + Offset);

            RelocationTableChunk = new RelocationTableChunk(in_reader, this);
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            Offset = in_writer.Position;

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

                chunkHeader.HeaderSize = (uint)(in_writer.Position - Offset);

                for (int i = 0; i < Count; i++)
                {
                    in_writer.WriteReserved(trunkOffsets[i], (uint)(in_writer.Position - Offset), false);
                    this[i].Trunk.Write(in_writer, this);
                }
            }

            in_writer.Align(16);
            in_writer.WriteReserved(relocTableOffset, (uint)(in_writer.Position - Offset));

            new RelocationTableChunk().Write(in_writer, this);

            chunkHeader.FinishWrite(in_writer, (uint)(in_writer.Position - Offset - chunkHeader.HeaderSize));

            new EndOfChunk().Write(in_writer);
        }
    }

    public struct TrunkChunkParam(IChunk in_data, uint in_param)
    {
        public IChunk Trunk = in_data;
        public uint Parameter = in_param;
    }
}
