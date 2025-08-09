using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types;
using System.Collections;
using System.Collections.Generic;

namespace Marathon.Formats.Ninja.Chunks
{
    public class OffsetChunk : IChunk, IList<uint>
    {
        public const string ID = "NOF0"; // "Ninja OFfset"

        public List<uint> Offsets { get; set; } = [];

        public int Count => Offsets.Count;

        public bool IsReadOnly => false;

        public uint this[int in_index]
        {
            get => Offsets[in_index];
            set => Offsets[in_index] = value;
        }

        public OffsetChunk() { }

        public OffsetChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public OffsetChunk(List<uint> in_offsets)
        {
            Offsets = in_offsets;
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var chunkSignature = in_reader.Read<FourCC>();
            var chunkLength = in_reader.Read<uint>();

            if (!chunkSignature.Equals(ID))
                throw new InvalidSignatureException(ID, chunkSignature);

            var offsetCount = in_reader.Read<int>();

            in_reader.Align(16);

            Offsets.AddRange(in_reader.ReadArray<uint>(offsetCount));
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.WriteSignature(ID);
            var length = in_writer.Reserve<uint>();
            in_writer.Write(Offsets.Count);
            in_writer.Align(16);
            in_writer.WriteCollection(Offsets);
            in_writer.Align(16);
            in_writer.WriteReserved(length, (int)(in_writer.Position - (length + 4)));
        }

        public int IndexOf(uint in_item)
        {
            return Offsets.IndexOf(in_item);
        }

        public void Insert(int in_index, uint in_item)
        {
            Offsets.Insert(in_index, in_item);
        }

        public void RemoveAt(int in_index)
        {
            Offsets.RemoveAt(in_index);
        }

        public void Add(uint in_item)
        {
            Offsets.Add(in_item);
        }

        public void Clear()
        {
            Offsets.Clear();
        }

        public bool Contains(uint in_item)
        {
            return Offsets.Contains(in_item);
        }

        public void CopyTo(uint[] in_array, int in_arrayIndex)
        {
            Offsets.CopyTo(in_array, in_arrayIndex);
        }

        public bool Remove(uint in_item)
        {
            return Offsets.Remove(in_item);
        }

        public IEnumerator<uint> GetEnumerator()
        {
            return Offsets.GetEnumerator();
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
