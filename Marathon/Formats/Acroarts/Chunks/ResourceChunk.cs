using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class ResourceChunk : List<ResourceChunkParam>, IBinarySerializableEx
    {
        public const string ID = "ABRS"; // "Acroarts Binary ReSource"

        [JsonIgnore]
        public RelocationTableChunk RelocationTableChunk { get; set; }

        public ResourceChunk() { }

        public ResourceChunk(BinaryObjectReaderEx in_reader)
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

            var resourceCount = in_reader.Read<uint>();
            var relocTableOffset = in_reader.Read<uint>();

            for (uint i = 0; i < resourceCount; i++)
            {
                var chunkOffset = in_reader.Read<uint>();
                var virtualResId = in_reader.Read<int>();

                in_reader.ReadAtOffset(in_reader.CalculateOffset(chunkOffset), () =>
                {
                    Add(new(new ResourcePathChunk(in_reader), virtualResId));
                });
            }

            in_reader.JumpTo(in_reader.CalculateOffset(relocTableOffset));

            RelocationTableChunk = new RelocationTableChunk(in_reader);

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

            if (Count <= 0)
            {
                // Empty chunk array.
                in_writer.WriteArray([0, -1]);
                in_writer.Align(16);
            }
            else
            {
                var resourceOffsets = new List<long>();

                foreach (var trunk in this)
                {
                    resourceOffsets.Add(in_writer.Reserve<uint>());
                    in_writer.Write(trunk.ID);
                }

                in_writer.Align(16);

                chunkHeader.HeaderSize = (uint)(in_writer.Position - in_writer.OffsetOrigin);

                for (int i = 0; i < Count; i++)
                {
                    in_writer.WriteReserved(resourceOffsets[i], (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative), false);
                    this[i].Resource.Write(in_writer);
                }
            }

            in_writer.Align(16);
            in_writer.WriteReserved(relocTableOffset, (uint)in_writer.CalculateOffset(in_writer.Position, OffsetType.Relative));

            new RelocationTableChunk().Write(in_writer);

            chunkHeader.FinishWrite(in_writer, (uint)(in_writer.Position - in_writer.OffsetOrigin - chunkHeader.HeaderSize));

            new EndOfChunk().Write(in_writer);

            in_writer.PopOffsetOrigin();
        }
    }

    public struct ResourceChunkParam(ResourcePathChunk in_resource, int in_id)
    {
        public ResourcePathChunk Resource = in_resource;
        public int ID = in_id;
    }
}
