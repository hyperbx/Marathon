using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Ninja.Types;
using Marathon.IO;
using System.Collections;
using System.Collections.Generic;

namespace Marathon.Formats.Ninja.Chunks
{
    public class MorphTargetChunk : IChunk, IList<MorphTarget>
    {
        public const string ID = "NXMT"; // "Ninja directX Morph Target"

        public List<MorphTarget> MorphTargets { get; set; } = [];

        public int Count => MorphTargets.Count;

        public bool IsReadOnly => false;

        public MorphTarget this[int in_index]
        {
            get => MorphTargets[in_index];
            set => MorphTargets[in_index] = value;
        }

        public MorphTargetChunk() { }

        public MorphTargetChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var header = in_reader.ReadObject<ChunkHeader>();

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
            var header = new ChunkHeader(in_writer, GetChunkID(), 0);

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

        public int IndexOf(MorphTarget in_item)
        {
            return MorphTargets.IndexOf(in_item);
        }

        public void Insert(int in_index, MorphTarget in_item)
        {
            MorphTargets.Insert(in_index, in_item);
        }

        public void RemoveAt(int in_index)
        {
            MorphTargets.RemoveAt(in_index);
        }

        public void Add(MorphTarget in_item)
        {
            MorphTargets.Add(in_item);
        }

        public void Clear()
        {
            MorphTargets.Clear();
        }

        public bool Contains(MorphTarget in_item)
        {
            return MorphTargets.Contains(in_item);
        }

        public void CopyTo(MorphTarget[] in_array, int in_arrayIndex)
        {
            MorphTargets.CopyTo(in_array, in_arrayIndex);
        }

        public bool Remove(MorphTarget in_item)
        {
            return MorphTargets.Remove(in_item);
        }

        public IEnumerator<MorphTarget> GetEnumerator()
        {
            return MorphTargets.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual string GetChunkID()
        {
            return ID;
        }
    }
}
