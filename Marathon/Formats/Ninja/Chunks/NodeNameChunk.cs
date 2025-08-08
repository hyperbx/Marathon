using Marathon.Exceptions;
using Marathon.Formats.Ninja.Flags;
using Marathon.Formats.Ninja.Types;
using Marathon.IO;
using Marathon.IO.Extensions;
using System.Collections;
using System.Collections.Generic;

namespace Marathon.Formats.Ninja.Chunks
{
    public class NodeNameChunk : IChunk, IList<string>
    {
        public const string ID = "NXNN"; // Ninja directX Node Name

        public NodeNameSortType Type { get; set; }

        public List<string> Names { get; set; } = [];

        public int Count => Names.Count;

        public bool IsReadOnly => false;

        public string this[int in_index]
        {
            get => Names[in_index];
            set => Names[in_index] = value;
        }

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

        public int IndexOf(string in_item)
        {
            return Names.IndexOf(in_item);
        }

        public void Insert(int in_index, string in_item)
        {
            Names.Insert(in_index, in_item);
        }

        public void RemoveAt(int in_index)
        {
            Names.RemoveAt(in_index);
        }

        public void Add(string in_item)
        {
            Names.Add(in_item);
        }

        public void Clear()
        {
            Names.Clear();
        }

        public bool Contains(string in_item)
        {
            return Names.Contains(in_item);
        }

        public void CopyTo(string[] in_array, int in_arrayIndex)
        {
            Names.CopyTo(in_array, in_arrayIndex);
        }

        public bool Remove(string in_item)
        {
            return Names.Remove(in_item);
        }

        public IEnumerator<string> GetEnumerator()
        {
            return Names.GetEnumerator();
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
