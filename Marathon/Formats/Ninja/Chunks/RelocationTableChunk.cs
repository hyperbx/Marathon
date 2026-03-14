using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types;
using System.Collections.Generic;

namespace Marathon.Formats.Ninja.Chunks
{
    public class RelocationTableChunk : IChunk
    {
        public const string ID = "NOF0"; // "Ninja OFfset"

        public List<uint> Offsets { get; set; } = [];

        public uint this[int in_index]
        {
            get => Offsets[in_index];
            set => Offsets[in_index] = value;
        }

        public RelocationTableChunk() { }

        public RelocationTableChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public RelocationTableChunk(List<uint> in_offsets)
        {
            Offsets = in_offsets;
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var chunkSignature = in_reader.ReadObject<FourCC>();
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

        public virtual string GetChunkID()
        {
            return ID;
        }
    }
}
