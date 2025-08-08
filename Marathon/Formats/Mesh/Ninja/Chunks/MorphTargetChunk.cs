using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Mesh.Ninja.Types;
using Marathon.IO;
using System.Collections.Generic;

namespace Marathon.Formats.Mesh.Ninja.Chunks
{
    public class MorphTargetChunk : IChunk
    {
        public const string ID = "NXMT";

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
                MorphTargets.Add(new MorphTarget(in_reader));
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

            var chunkEnd = (uint)in_writer.Position;
            var chunkSize = chunkEnd - header.GetChunkStart();

            header.FinishWrite(in_writer, chunkSize, dataOffset);
        }

        public virtual string GetChunkID()
        {
            return ID;
        }
    }

    public class MorphTarget
    {
        public uint Target { get; set; }

        public VertexList VertexList { get; set; }

        public MorphTarget() { }

        public MorphTarget(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Target = in_reader.Read<uint>();
            var morphTargetOffset = in_reader.Read<uint>();

            var pos = in_reader.Position;

            in_reader.JumpTo(InfoChunk.Size + morphTargetOffset);

            var unkField = in_reader.Read<uint>(); // TODO: unknown - always 2?
            var vertexListOffset = in_reader.Read<uint>();

            in_reader.JumpTo(InfoChunk.Size + vertexListOffset);
            
            VertexList = new VertexList(in_reader, true);

            in_reader.JumpTo(pos);
        }
    }
}
