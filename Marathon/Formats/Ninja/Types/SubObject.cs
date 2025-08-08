using Marathon.Formats.Ninja.Chunks;
using Marathon.IO;
using System.Collections.Generic;

namespace Marathon.Formats.Ninja.Types
{
    public class SubObject
    {
        private uint _meshSetOffset;
        private uint _textureIndicesOffset;

        public const int InfoSize = 0x14;

        public uint Type { get; set; }

        public List<MeshSet> MeshSets { get; set; } = [];

        public List<int> TextureIndices { get; set; } = [];

        public SubObject() { }

        public SubObject(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Type = in_reader.Read<uint>();

            var meshSetCount = in_reader.Read<uint>();
            var meshSetOffset = in_reader.Read<uint>();
            var textureIndicesCount = in_reader.Read<uint>();
            var textureIndicesOffset = in_reader.Read<uint>();

            in_reader.JumpTo(InfoChunk.Size + meshSetOffset);

            for (int i = 0; i < meshSetCount; i++)
                MeshSets.Add(new MeshSet(in_reader));

            in_reader.JumpTo(InfoChunk.Size + textureIndicesOffset);

            for (int i = 0; i < textureIndicesCount; i++)
                TextureIndices.Add(in_reader.Read<int>());
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Type);
            in_writer.Write(MeshSets.Count);

            var meshSetOffset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(meshSetOffset, _meshSetOffset - InfoChunk.Size, false);

            in_writer.Write(TextureIndices.Count);

            if (TextureIndices.Count <= 0)
            {
                in_writer.Write(0);
            }
            else
            {
                var textureIndicesOffset = in_writer.Reserve<uint>();
                in_writer.WriteReserved(textureIndicesOffset, _textureIndicesOffset - InfoChunk.Size, false);
            }
        }

        public void WriteMeshSets(BinaryObjectWriterEx in_writer)
        {
            _meshSetOffset = (uint)in_writer.Position;

            foreach (var meshSet in MeshSets)
                meshSet.Write(in_writer);
        }

        public void WriteTextureIndices(BinaryObjectWriterEx in_writer)
        {
            _textureIndicesOffset = (uint)in_writer.Position;

            foreach (var index in TextureIndices)
                in_writer.Write(index);
        }
    }
}
