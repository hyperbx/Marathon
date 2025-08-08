using Marathon.Formats.Ninja.Chunks;
using Marathon.Formats.Ninja.Flags;
using Marathon.IO;

namespace Marathon.Formats.Ninja.Types
{
    public class Material
    {
        private uint _infoOffset;

        public const int InfoSize = 8;

        public MaterialType Type { get; set; }

        public MaterialType Flag { get; set; }

        public int UserData { get; set; }

        public uint[] Reserved { get; set; } = new uint[3];

        public MaterialColour Colour { get; set; }

        public MaterialLogic Logic { get; set; }

        public MaterialTextureMap TextureMap { get; set; }

        public Material() { }

        public Material(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Type = in_reader.Read<MaterialType>();

            var infoOffset = in_reader.Read<uint>();

            in_reader.JumpTo(InfoChunk.Size + infoOffset);

            Flag = in_reader.Read<MaterialType>();
            UserData = in_reader.Read<int>();

            var colourOffset = in_reader.Read<uint>();
            var logicOffset = in_reader.Read<uint>();
            var textureMapOffset = in_reader.Read<uint>();

            Reserved = in_reader.ReadArray<uint>(3);

            in_reader.JumpTo(InfoChunk.Size + colourOffset);

            Colour = new MaterialColour(in_reader);

            in_reader.JumpTo(InfoChunk.Size + logicOffset);

            Logic = new MaterialLogic(in_reader);

            in_reader.JumpTo(InfoChunk.Size + textureMapOffset);

            TextureMap = new MaterialTextureMap(in_reader, GetTextureCount());
        }

        public void WriteInfo(BinaryObjectWriterEx in_writer, uint in_colourOffset, uint in_logicOffset, uint in_textureMapOffset)
        {
            _infoOffset = (uint)in_writer.Position;

            in_writer.Write(Flag);
            in_writer.Write(UserData);

            var colourOffset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(colourOffset, in_colourOffset - InfoChunk.Size, false);

            var logicOffset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(logicOffset, in_logicOffset - InfoChunk.Size, false);

            if (TextureMap.Descriptions.Count <= 0)
            {
                in_writer.Write(0);
            }
            else
            {
                var textureMapOffset = in_writer.Reserve<uint>();
                in_writer.WriteReserved(textureMapOffset, in_textureMapOffset - InfoChunk.Size, false);
            }

            in_writer.WriteArray(Reserved);
        }

        public void WritePointer(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Type);
            var offset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(offset, _infoOffset - InfoChunk.Size, false);
        }

        public int GetTextureCount()
        {
            var result = 0;

            if (Type.HasFlag(MaterialType.NND_MATTYPE_TEXTURE))
                result = 1;

            if (Type.HasFlag(MaterialType.NND_MATTYPE_TEXTURE2))
                result = 2;

            if (Type.HasFlag(MaterialType.NND_MATTYPE_TEXTURE3))
                result = 3;

            if (Type.HasFlag(MaterialType.NND_MATTYPE_TEXTURE4))
                result = 4;

            return result;
        }
    }
}
