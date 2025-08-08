using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Ninja.Types;
using Marathon.IO;
using System.Collections.Generic;

namespace Marathon.Formats.Ninja.Chunks
{
    public class MorphTargetChunk : IChunk
    {
        public const string ID = "NXMT"; // Ninja directX Morph Target

        public List<MorphTarget> MorphTargets { get; set; } = [];

        public MorphTargetChunk() { }

        public MorphTargetChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var header = in_reader.ReadObject<DataHeader>();

            if (!header.ID.Equals(GetChunkID()))
                throw new InvalidSignatureException(GetChunkID(), header.ID);

            in_reader.JumpTo(InfoChunk.Size + header.DataOffset);

            var morphTargetCount = in_reader.Read<uint>();
            var morphTargetOffset = in_reader.Read<uint>();

            in_reader.JumpTo(InfoChunk.Size + morphTargetOffset);

            for (int i = 0; i < morphTargetCount; i++)
                MorphTargets.Add(new(in_reader));
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var header = new DataHeader(in_writer, GetChunkID(), 0);

            var verticesOffsets = new List<uint>();
            var vertexListOffsets = new List<uint>();
            var morphTargetOffsets = new List<uint>();

            foreach (var morphTarget in MorphTargets)
                verticesOffsets.Add(morphTarget.VertexList.WriteVertices(in_writer, true));

            for (int i = 0; i < MorphTargets.Count; i++)
            {
                vertexListOffsets.Add((uint)(in_writer.Position - InfoChunk.Size));
                MorphTargets[i].VertexList.Write(in_writer, verticesOffsets[i]);
            }

            for (int i = 0; i < MorphTargets.Count; i++)
            {
                morphTargetOffsets.Add((uint)(in_writer.Position - InfoChunk.Size));
                in_writer.Write(2); // TODO: unknown - always 2?
                var vertexListOffset = in_writer.Reserve<uint>();
                in_writer.WriteReserved(vertexListOffset, vertexListOffsets[i], false);
            }

            var morphTargetsPos = (uint)(in_writer.Position - InfoChunk.Size);

            for (int i = 0; i < MorphTargets.Count; i++)
            {
                in_writer.Write(MorphTargets[i].Target);
                var morphTargetOffset = in_writer.Reserve<uint>();
                in_writer.WriteReserved(morphTargetOffset, morphTargetOffsets[i], false);
            }

            var dataOffset = (uint)(in_writer.Position - InfoChunk.Size);

            in_writer.Write(MorphTargets.Count);
            var morphTargetsOffset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(morphTargetsOffset, morphTargetsPos, false);

            in_writer.Align(16);

            header.FinishWrite(in_writer, dataOffset);
        }

        public virtual string GetChunkID()
        {
            return ID;
        }
    }
}
