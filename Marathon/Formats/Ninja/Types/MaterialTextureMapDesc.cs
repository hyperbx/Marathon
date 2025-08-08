using Marathon.Formats.Ninja.Flags;
using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Ninja.Types
{
    public class MaterialTextureMapDesc
    {
        public uint Type { get; set; }

        public int Index { get; set; }

        public Vector2 Offset { get; set; }

        public float Blend { get; set; }

        public uint TextureInfo { get; set; }

        public MinFilter MinFilter { get; set; }

        public MagFilter MagFilter { get; set; }

        public float MipMapBias { get; set; }

        public uint MaxMipLevel { get; set; }

        public uint[] Reserved { get; set; } = new uint[3];

        public MaterialTextureMapDesc() { }

        public MaterialTextureMapDesc(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Type = in_reader.Read<uint>();
            Index = in_reader.Read<int>();
            Offset = in_reader.Read<Vector2>();
            Blend = in_reader.Read<float>();
            TextureInfo = in_reader.Read<uint>();
            MinFilter = in_reader.Read<MinFilter>();
            MagFilter = in_reader.Read<MagFilter>();
            MipMapBias = in_reader.Read<float>();
            MaxMipLevel = in_reader.Read<uint>();
            Reserved = in_reader.ReadArray<uint>(3);
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Type);
            in_writer.Write(Index);
            in_writer.Write(Offset);
            in_writer.Write(Blend);
            in_writer.Write(TextureInfo);
            in_writer.Write(MinFilter);
            in_writer.Write(MagFilter);
            in_writer.Write(MipMapBias);
            in_writer.Write(MaxMipLevel);
            in_writer.WriteArray(Reserved);
        }

        public override bool Equals(object in_obj)
        {
            if (in_obj is not MaterialTextureMapDesc out_textureMapDesc)
                return false;

            return out_textureMapDesc.Type == Type &&
                   out_textureMapDesc.Index == Index &&
                   out_textureMapDesc.Offset == Offset &&
                   out_textureMapDesc.Blend == Blend &&
                   out_textureMapDesc.TextureInfo == TextureInfo &&
                   out_textureMapDesc.MinFilter == MinFilter &&
                   out_textureMapDesc.MagFilter == MagFilter &&
                   out_textureMapDesc.MipMapBias == MipMapBias &&
                   out_textureMapDesc.MaxMipLevel == MaxMipLevel;
        }
    }
}
