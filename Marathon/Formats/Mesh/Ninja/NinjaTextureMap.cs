namespace Marathon.Formats.Mesh.Ninja
{
    /// <summary>
    /// Structure of a Ninja Object Material's Texture Map List.
    /// This is kept seperate from Materials as multiple materials can use the same texture map list.
    /// </summary>
    public class NinjaTextureMap
    {
        public List<NinjaMaterialTextureMapDescription> NinjaTextureMapDescriptions = new();

        // This offset is stored by us purely for the writing process.
        public uint Offset { get; set; }

        /// <summary>
        /// Reads a material's texture map list from a file.
        /// </summary>
        /// <param name="reader">The binary reader for this SegaNN file.</param>
        public void Read(BinaryReaderEx reader)
        {
            // Read the material's Type, as it tells us the amount of textures.
            NinjaNext_MaterialType Type = (NinjaNext_MaterialType)reader.ReadUInt32();

            // Jump to the material's main data.
            reader.JumpTo(reader.ReadUInt32(), true);

            // Skip over the material's Flag, User Definied data, Colour offset and Logic offset.
            reader.JumpAhead(0x10);

            // Save the offset for the writing process then jump to it.
            Offset = reader.ReadUInt32();
            reader.JumpTo(Offset, true);

            // Figure out how many textures this texture map list has in it based on the material's Type.
            int textureCount = 0;
            if (Type.HasFlag(NinjaNext_MaterialType.NND_MATTYPE_TEXTURE))
                textureCount = 1;
            if (Type.HasFlag(NinjaNext_MaterialType.NND_MATTYPE_TEXTURE2))
                textureCount = 2;
            if (Type.HasFlag(NinjaNext_MaterialType.NND_MATTYPE_TEXTURE3))
                textureCount = 3;
            if (Type.HasFlag(NinjaNext_MaterialType.NND_MATTYPE_TEXTURE4))
                textureCount = 4;

            // Loop through and save each texture map description entry.
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

        /// <summary>
        /// Writes this material texture map list entry to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        /// <param name="index">The number of this material texture map list in a linear list.</param>
        /// <param name="ObjectOffsets">The list of offsets this Object chunk uses.</param>
        public void Write(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            // Add an entry for this material texture map list into ObjectOffsets so we know where it is.
            ObjectOffsets.Add($"TexDescOffset{index}", (uint)writer.BaseStream.Position);

            // Loop through the texture map descriptions in this texture map list and write them.
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

        // Overrides to make it possible to check if two Material Colour entries are the same (by checking their offset).
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

    /// <summary>
    /// Structure of a Ninja Object Texture Map Description.
    /// </summary>
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
