using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.IO;
using Marathon.IO.Types.BINA;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Marathon.Formats.Acroarts.Chunks
{
    public class ResourceChunk : IChunk
    {
        public const string ID = "ABRS"; // "Acroarts Binary ReSource"

        public long Offset { get; set; }

        public List<ResourceChunkParam> Resources { get; set; } = [];

        [JsonIgnore]
        public RelocationTableChunk RelocationTableChunk { get; set; }

        public ResourceChunk() { }

        public ResourceChunk(BINAReader in_reader)
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

            var resourceCount = in_reader.Read<uint>();
            var relocTableOffset = in_reader.Read<uint>();

            for (uint i = 0; i < resourceCount; i++)
            {
                var chunkOffset = in_reader.Read<uint>();
                var virtualResId = in_reader.Read<int>();

                in_reader.ReadAtOffset(Offset + chunkOffset, () =>
                {
                    Resources.Add(new(new ResourcePathChunk(in_reader), virtualResId));
                });
            }

            in_reader.JumpTo(relocTableOffset + Offset);

            RelocationTableChunk = new RelocationTableChunk(in_reader, this);
        }

        public void Write(BinaryObjectWriterEx in_writer, IChunk in_parentChunk = null)
        {
            Offset = in_writer.Position;

            var chunkHeader = new ChunkHeader(in_writer, ID);

            in_writer.Write(AckResource.Version);
            in_writer.Write(Resources.Count);

            var relocTableOffset = in_writer.Reserve<uint>(true);

            if (Resources.Count <= 0)
            {
                // Empty chunk array.
                in_writer.WriteArray([0, -1]);
                in_writer.Align(16);
            }
            else
            {
                var resourceOffsets = new List<long>();

                foreach (var trunk in Resources)
                {
                    resourceOffsets.Add(in_writer.Reserve<uint>());
                    in_writer.Write(trunk.VirtualResID);
                }

                in_writer.Align(16);

                chunkHeader.HeaderSize = (uint)(in_writer.Position - Offset);

                for (int i = 0; i < Resources.Count; i++)
                {
                    in_writer.WriteReserved(resourceOffsets[i], (uint)(in_writer.Position - Offset), false);
                    Resources[i].Data.Write(in_writer, this);
                }
            }

            in_writer.Align(16);
            in_writer.WriteReserved(relocTableOffset, (uint)(in_writer.Position - Offset));

            new RelocationTableChunk().Write(in_writer, this);

            chunkHeader.FinishWrite(in_writer, (uint)(in_writer.Position - Offset - chunkHeader.HeaderSize));

            new EndOfChunk().Write(in_writer);
        }
    }

    public struct ResourceChunkParam(IChunk in_data, int in_virtualResId)
    {
        public IChunk Data = in_data;
        public int VirtualResID = in_virtualResId;
    }
}
