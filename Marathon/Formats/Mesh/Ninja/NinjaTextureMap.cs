using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathon.Formats.Mesh.Ninja
{
    public class NinjaTextureMap
    {
        public List<NinjaMaterialTextureMapDescription> NinjaTextureMapDescriptions = new();

        public uint Offset { get; set; }

        public void Read(BinaryReaderEx reader)
        {
            NinjaNext_MaterialType Type = (NinjaNext_MaterialType)reader.ReadUInt32();
            reader.JumpTo(reader.ReadUInt32(), true);
            reader.JumpAhead(0x10);
            Offset = reader.ReadUInt32();
            reader.JumpTo(Offset, true);

            int textureCount = 0;
            if (Type.HasFlag(NinjaNext_MaterialType.NND_MATTYPE_TEXTURE))
                textureCount = 1;
            if (Type.HasFlag(NinjaNext_MaterialType.NND_MATTYPE_TEXTURE2))
                textureCount = 2;
            if (Type.HasFlag(NinjaNext_MaterialType.NND_MATTYPE_TEXTURE3))
                textureCount = 3;
            if (Type.HasFlag(NinjaNext_MaterialType.NND_MATTYPE_TEXTURE4))
                textureCount = 4;

            for (int t = 0; t < textureCount; t++)
            {
                NinjaMaterialTextureMapDescription TexMapDesc = new()
                {
                    Type = reader.ReadUInt32(),
                    Index = reader.ReadInt32(),
                    Offset = reader.ReadVector2(),
                    Blend = reader.ReadSingle(),
                    TextureInfo = reader.ReadUInt32(),
                    MinFilter = (NinjaNext_MinFilter)reader.ReadUInt16(),
                    MagFilter = (NinjaNext_MagFilter)reader.ReadUInt16(),
                    MipMapBias = reader.ReadSingle(),
                    MaxMipLevel = reader.ReadUInt32(),
                    Reserved0 = reader.ReadUInt32(),
                    Reserved1 = reader.ReadUInt32(),
                    Reserved2 = reader.ReadUInt32()
                };
                NinjaTextureMapDescriptions.Add(TexMapDesc);
            }
        }

        public void Write(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            ObjectOffsets.Add($"TexDescOffset{index}", (uint)writer.BaseStream.Position);
            for (int i = 0; i < NinjaTextureMapDescriptions.Count; i++)
            {
                writer.Write(NinjaTextureMapDescriptions[i].Type);
                writer.Write(NinjaTextureMapDescriptions[i].Index);
                writer.Write(NinjaTextureMapDescriptions[i].Offset);
                writer.Write(NinjaTextureMapDescriptions[i].Blend);
                writer.Write(NinjaTextureMapDescriptions[i].TextureInfo);
                writer.Write((ushort)NinjaTextureMapDescriptions[i].MinFilter);
                writer.Write((ushort)NinjaTextureMapDescriptions[i].MagFilter);
                writer.Write(NinjaTextureMapDescriptions[i].MipMapBias);
                writer.Write(NinjaTextureMapDescriptions[i].MaxMipLevel);
                writer.Write(NinjaTextureMapDescriptions[i].Reserved0);
                writer.Write(NinjaTextureMapDescriptions[i].Reserved1);
                writer.Write(NinjaTextureMapDescriptions[i].Reserved2);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is NinjaTextureMap)
                return Equals((NinjaTextureMap)obj);
            return false;
        }
        public bool Equals(NinjaTextureMap obj)
        {
            if (obj == null) return false;
            if (!EqualityComparer<uint>.Default.Equals(Offset, obj.Offset)) return false;
            return true;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            hash ^= EqualityComparer<uint>.Default.GetHashCode(Offset);
            return hash;
        }
    }

    public class NinjaMaterialTextureMapDescription
    {
        public uint Type { get; set; }

        public int Index { get; set; }

        public Vector2 Offset { get; set; }

        public float Blend { get; set; }

        public uint TextureInfo { get; set; }

        public NinjaNext_MinFilter MinFilter { get; set; }

        public NinjaNext_MagFilter MagFilter { get; set; }

        public float MipMapBias { get; set; }

        public uint MaxMipLevel { get; set; }

        public uint Reserved0 { get; set; }

        public uint Reserved1 { get; set; }

        public uint Reserved2 { get; set; }
    }
}
