using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Ninja.Types;
using Marathon.IO;
using Marathon.IO.Extensions;
using System.Collections;
using System.Collections.Generic;

namespace Marathon.Formats.Ninja.Chunks
{
    public class TextureListChunk : IChunk, IList<TextureFile>
    {
        public const string ID = "NXTL"; // Ninja directX Texture List

        public List<TextureFile> Textures { get; set; } = [];

        public int Count => Textures.Count;

        public bool IsReadOnly => false;

        public TextureFile this[int in_index]
        {
            get => Textures[in_index];
            set => Textures[in_index] = value;
        }

        public TextureFile this[string in_name]
        {
            get => Textures.Find(x => x.Name == in_name);
        }

        public TextureListChunk() { }

        public TextureListChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var header = in_reader.ReadObject<DataHeader>();

            if (!header.ID.Equals(GetChunkID()))
                throw new InvalidSignatureException(GetChunkID(), header.ID);

            in_reader.JumpTo(InfoChunk.Size + header.DataOffset);

            var textureFileCount = in_reader.Read<uint>();
            var textureFileOffset = in_reader.Read<uint>();

            in_reader.JumpTo(InfoChunk.Size + textureFileOffset);

            for (int i = 0; i < textureFileCount; i++)
                Textures.Add(new(in_reader));
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var header = new DataHeader(in_writer, GetChunkID(), 0);

            var textureFilePos = (int)(in_writer.Position - InfoChunk.Size);
            var textureFileNameOffsets = new List<uint>();

            foreach (var texture in Textures)
            {
                texture.Write(in_writer, out var out_nameOffset);
                textureFileNameOffsets.Add(out_nameOffset);
            }

            var dataPos = (uint)(in_writer.Position - InfoChunk.Size);

            in_writer.Write(Textures.Count);
            var textureFileOffset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(textureFileOffset, textureFilePos, false);

            for (int i = 0; i < Textures.Count; i++)
            {
                in_writer.WriteReserved(textureFileNameOffsets[i], (uint)(in_writer.Position - InfoChunk.Size), false);
                in_writer.WriteStringNullTerminated(Textures[i].Name);
            }

            in_writer.Align(16);

            header.FinishWrite(in_writer, dataPos);
        }

        public int IndexOf(TextureFile in_item)
        {
            return Textures.IndexOf(in_item);
        }

        public void Insert(int in_index, TextureFile in_item)
        {
            Textures.Insert(in_index, in_item);
        }

        public void RemoveAt(int in_index)
        {
            Textures.RemoveAt(in_index);
        }

        public void Add(TextureFile in_item)
        {
            Textures.Add(in_item);
        }

        public void Clear()
        {
            Textures.Clear();
        }

        public bool Contains(TextureFile in_item)
        {
            return Textures.Contains(in_item);
        }

        public void CopyTo(TextureFile[] in_array, int in_arrayIndex)
        {
            Textures.CopyTo(in_array, in_arrayIndex);
        }

        public bool Remove(TextureFile in_item)
        {
            return Textures.Remove(in_item);
        }

        public IEnumerator<TextureFile> GetEnumerator()
        {
            return Textures.GetEnumerator();
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
