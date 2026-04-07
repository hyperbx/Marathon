using Marathon.Formats.Ninja.Chunks;
using Marathon.Formats.Ninja.Flags;
using Marathon.IO;
using System.Collections.Generic;

namespace Marathon.Formats.Ninja.Types
{
    public class SubObject
    {
        private uint _meshSetOffset;
        private uint _textureIndexesOffset;

        public const int InfoSize = 0x14;

        public SubObjectType Type { get; set; }

        public List<MeshSet> MeshSets { get; set; } = [];

        public List<int> TextureIndexes { get; set; } = [];

        public SubObject() { }

        public SubObject(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Type = in_reader.Read<SubObjectType>();

            var meshSetCount = in_reader.Read<uint>();
            var meshSetOffset = in_reader.Read<uint>();
            var textureIndexesCount = in_reader.Read<uint>();
            var textureIndexesOffset = in_reader.Read<uint>();

            in_reader.JumpTo(InfoChunk.Size + meshSetOffset);

            for (int i = 0; i < meshSetCount; i++)
                MeshSets.Add(new(in_reader));

            in_reader.JumpTo(InfoChunk.Size + textureIndexesOffset);

            for (int i = 0; i < textureIndexesCount; i++)
                TextureIndexes.Add(in_reader.Read<int>());
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Type);
            in_writer.Write(MeshSets.Count);

            var meshSetOffset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(meshSetOffset, _meshSetOffset - InfoChunk.Size, false);

            in_writer.Write(TextureIndexes.Count);

            if (TextureIndexes.Count <= 0)
            {
                in_writer.Write(0);
            }
            else
            {
                var textureIndexesOffset = in_writer.Reserve<uint>();
                in_writer.WriteReserved(textureIndexesOffset, _textureIndexesOffset - InfoChunk.Size, false);
            }
        }

        public void WriteMeshSets(BinaryObjectWriterEx in_writer)
        {
            _meshSetOffset = (uint)in_writer.Position;

            foreach (var meshSet in MeshSets)
                meshSet.Write(in_writer);
        }

        public void WriteTextureIndexes(BinaryObjectWriterEx in_writer)
        {
            _textureIndexesOffset = (uint)in_writer.Position;

            foreach (var index in TextureIndexes)
                in_writer.Write(index);
        }

        public List<TextureFile> GetTextures(TextureListChunk in_textureListChunk)
        {
            var result = new List<TextureFile>();

            foreach (var index in TextureIndexes)
                result.Add(in_textureListChunk.Textures[index]);

            return result;
        }
    }
}
