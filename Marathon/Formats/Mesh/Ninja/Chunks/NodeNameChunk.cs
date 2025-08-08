using Marathon.Exceptions;
using Marathon.Formats.Mesh.Ninja.Flags;
using Marathon.Formats.Mesh.Ninja.Types;
using Marathon.IO;
using Marathon.IO.Extensions;
using System.Collections.Generic;

namespace Marathon.Formats.Mesh.Ninja.Chunks
{
    public class NodeNameChunk : IChunk
    {
        public const string ID = "NXNN"; // Ninja directX Node Name

        public NodeNameSortType Type { get; set; }

        public List<string> Names { get; set; } = [];

        public NodeNameChunk() { }

        public NodeNameChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var header = in_reader.ReadObject<DataHeader>();

            if (!header.ID.Equals(GetChunkID()))
                throw new InvalidSignatureException(GetChunkID(), header.ID);

            in_reader.JumpTo(InfoChunk.Size + header.DataOffset);

            Type = in_reader.Read<NodeNameSortType>();

            var nodeCount = in_reader.Read<uint>();
            var nodeListOffset = in_reader.Read<uint>();

            in_reader.JumpTo(InfoChunk.Size + nodeListOffset);

            for (int i = 0; i < nodeCount; i++)
            {
                var nodeID = in_reader.Read<uint>();
                var nodeNameOffset = in_reader.Read<uint>();

                in_reader.ReadAtOffset(InfoChunk.Size + nodeNameOffset,
                    () => Names.Add(in_reader.ReadStringNullTerminated()));
            }
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var header = new DataHeader(in_writer, GetChunkID(), 0);

            var nodeListPos = (uint)in_writer.Position;
            var nodeNameOffsets = new List<uint>();

            for (int i = 0; i < Names.Count; i++)
            {
                in_writer.Write(i);
                nodeNameOffsets.Add(in_writer.Reserve<uint>());
            }

            var dataPos = (uint)(in_writer.Position - InfoChunk.Size);

            in_writer.Write(Type);
            in_writer.Write(Names.Count);
            var nodeListOffset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(nodeListOffset, nodeListPos - InfoChunk.Size, false);

            for (int i = 0; i < Names.Count; i++)
            {
                in_writer.WriteReserved(nodeNameOffsets[i], (uint)(in_writer.Position - InfoChunk.Size), false);
                in_writer.WriteStringNullTerminated(Names[i]);
            }

            in_writer.Align(16);

            header.FinishWrite(in_writer, dataPos);
        }

        public virtual string GetChunkID()
        {
            return ID;
        }
    }
}
