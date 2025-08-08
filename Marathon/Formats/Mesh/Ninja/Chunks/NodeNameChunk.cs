using Marathon.Exceptions;
using Marathon.Formats.Mesh.Ninja.Flags;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types;
using System.Collections.Generic;

namespace Marathon.Formats.Mesh.Ninja.Chunks
{
    public class NodeNameChunk : IChunk
    {
        public const string ID = "NXNN";

        public NodeNameSortType Type { get; set; }

        public List<string> Names { get; set; } = [];

        public NodeNameChunk() { }

        public NodeNameChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var chunkSignature = in_reader.Read<FourCC>();
            var chunkLength = in_reader.Read<uint>();

            if (!chunkSignature.Equals(ID))
                throw new InvalidSignatureException(ID, chunkSignature);

            var infoOffset = in_reader.Read<uint>();

            in_reader.JumpTo(InfoChunk.Size + infoOffset);

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
            in_writer.WriteSignature(ID);

            var length = in_writer.Reserve<uint>();
            var infoOffset = in_writer.Reserve<uint>();

            in_writer.Align(16);

            var nodeListPos = (uint)in_writer.Position;
            var nodeNameOffsets = new List<uint>();

            for (int i = 0; i < Names.Count; i++)
            {
                in_writer.Write(i);
                nodeNameOffsets.Add(in_writer.Reserve<uint>());
            }

            in_writer.WriteReserved(infoOffset, (uint)(in_writer.Position - InfoChunk.Size));
            
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

            in_writer.WriteReserved(length, (int)(in_writer.Position - (length + 4)));
        }

        public virtual string GetChunkID()
        {
            return ID;
        }
    }
}
