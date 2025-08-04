using Amicitia.IO.Binary;
using Marathon.Formats.Mesh.Ninja.Chunks;
using Marathon.Formats.Mesh.Ninja.Flags;
using Marathon.IO;
using System.Collections.Generic;

namespace Marathon.Formats.Mesh.Ninja.Types
{
    public class PrimitiveList
    {
        private uint _infoOffset;
        private uint _stripIndicesOffset;
        private uint _indexIndicesOffset;

        public const int InfoSize = 8;

        public PrimitiveType Type { get; set; }

        public uint Format { get; set; }

        public List<ushort> StripIndices { get; set; } = [];

        public List<ushort> IndexIndices { get; set; } = [];

        public uint IndexBuffer { get; set; }

        public uint[] Reserved { get; set; } = new uint[2];

        public PrimitiveList() { }

        public PrimitiveList(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Type = in_reader.Read<PrimitiveType>();

            var infoOffset = in_reader.Read<uint>();

            in_reader.JumpTo(InfoChunk.Size + infoOffset);

            Format = in_reader.Read<uint>();

            var indexCount = in_reader.Read<uint>();
            var stripCount = in_reader.Read<uint>();
            var stripIndicesOffset = in_reader.Read<uint>();
            var indexIndicesOffset = in_reader.Read<uint>();

            IndexBuffer = in_reader.Read<uint>();
            Reserved = in_reader.ReadArray<uint>(2);

            in_reader.JumpTo(InfoChunk.Size + stripIndicesOffset);

            for (int i = 0; i < stripCount; i++)
                StripIndices.Add(in_reader.Read<ushort>());

            in_reader.JumpTo(InfoChunk.Size + indexIndicesOffset);

            for (int i = 0; i < indexCount; i++)
                IndexIndices.Add(in_reader.Read<ushort>());
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer)
        {
            _infoOffset = (uint)in_writer.Position;

            in_writer.Write(Format);
            in_writer.Write(IndexIndices.Count);
            in_writer.Write(StripIndices.Count);

            var stripIndicesOffset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(stripIndicesOffset, _stripIndicesOffset - InfoChunk.Size, false);

            var indexIndicesOffset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(indexIndicesOffset, _indexIndicesOffset - InfoChunk.Size, false);

            in_writer.Write(IndexBuffer);
            in_writer.WriteArray(Reserved);
        }

        public void WritePointer(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Type);
            var offset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(offset, _infoOffset - InfoChunk.Size, false);
        }

        public void WriteStripIndices(BinaryObjectWriterEx in_writer)
        {
            _stripIndicesOffset = (uint)in_writer.Position;

            foreach (var index in StripIndices)
                in_writer.Write(index);

            in_writer.Align(4);
        }

        public void WriteIndexIndices(BinaryObjectWriterEx in_writer)
        {
            _indexIndicesOffset = (uint)in_writer.Position;

            foreach (var index in IndexIndices)
                in_writer.Write(index);

            in_writer.Align(4);
        }
    }
}
