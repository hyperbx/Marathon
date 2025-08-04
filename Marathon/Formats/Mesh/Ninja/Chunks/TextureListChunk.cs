using Amicitia.IO.Binary;
using Marathon.Exceptions;
using Marathon.Formats.Mesh.Ninja.Flags;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types;
using System.Collections.Generic;
using System.IO;

namespace Marathon.Formats.Mesh.Ninja.Chunks
{
    public class TextureListChunk : IChunk
    {
        public const string ID = "NXTL";

        public string ChunkID { get; set; } = ID;

        public List<TextureFile> Textures { get; set; } = [];

        public TextureListChunk() { }

        public TextureListChunk(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            var chunkSignature = in_reader.Read<FourCC>();
            var chunkLength = in_reader.Read<uint>();

            if (!chunkSignature.Equals(ID))
                throw new InvalidSignatureException(ID, chunkSignature);

            var infoOffset = in_reader.Read<uint>();

            in_reader.Seek(InfoChunk.Size + infoOffset, SeekOrigin.Begin);

            var textureFileCount = in_reader.Read<uint>();
            var textureFileOffset = in_reader.Read<uint>();

            in_reader.Seek(InfoChunk.Size + textureFileOffset, SeekOrigin.Begin);

            for (int i = 0; i < textureFileCount; i++)
                Textures.Add(new TextureFile(in_reader));
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.WriteSignature(ID);

            var length = in_writer.Reserve<uint>();
            var infoOffset = in_writer.Reserve<uint>();

            in_writer.Align(16);

            var textureFilePos = (int)(in_writer.Position - InfoChunk.Size);
            var textureFileNameOffsets = new List<uint>();

            foreach (var texture in Textures)
            {
                texture.Write(in_writer, out var out_nameOffset);
                textureFileNameOffsets.Add(out_nameOffset);
            }

            in_writer.WriteReserved(infoOffset, (uint)(in_writer.Position - InfoChunk.Size));
            in_writer.Write(Textures.Count);
            var textureFileOffset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(textureFileOffset, textureFilePos, false);

            for (int i = 0; i < Textures.Count; i++)
            {
                in_writer.WriteReserved(textureFileNameOffsets[i], (uint)(in_writer.Position - InfoChunk.Size), false);
                in_writer.WriteStringNullTerminated(Textures[i].Name);
            }

            in_writer.Align(16);

            in_writer.WriteReserved(length, (int)(in_writer.Position - (length + 4)));
        }
    }

    public class TextureFile
    {
        public TextureFileType Type { get; set; }

        public string Name { get; set; }

        public MinFilter MinFilter { get; set; }

        public MagFilter MagFilter { get; set; }

        public uint GlobalIndex { get; set; }

        public uint Bank { get; set; }

        public TextureFile() { }

        public TextureFile(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Type = in_reader.Read<TextureFileType>();

            var nameOffset = in_reader.Read<uint>();

            in_reader.ReadAtOffset(InfoChunk.Size + nameOffset,
                () => Name = in_reader.ReadStringNullTerminated());

            MinFilter = in_reader.Read<MinFilter>();
            MagFilter = in_reader.Read<MagFilter>();
            GlobalIndex = in_reader.Read<uint>();
            Bank = in_reader.Read<uint>();
        }

        public void Write(BinaryObjectWriterEx in_writer, out uint out_nameOffset)
        {
            in_writer.Write(Type);

            out_nameOffset = in_writer.Reserve<uint>();

            in_writer.Write(MinFilter);
            in_writer.Write(MagFilter);
            in_writer.Write(GlobalIndex);
            in_writer.Write(Bank);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
