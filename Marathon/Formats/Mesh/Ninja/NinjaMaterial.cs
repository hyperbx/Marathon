namespace Marathon.Formats.Mesh.Ninja
{
    /// <summary>
    /// Structure of a Ninja Object Material.
    /// </summary>
    public class NinjaMaterial
    {
        public MaterialType Type { get; set; }

        public MaterialType Flag { get; set; }

        public int UserDefined { get; set; }

        public uint Reserved0 { get; set; }

        public uint Reserved1 { get; set; }

        public uint Reserved2 { get; set; }
        
        /* The below offsets are stored by us purely for the writing process. */

        public uint MaterialColourOffset { get; set; }
        
        public uint MaterialLogicOffset { get; set; }
        
        public uint MaterialTexMapDescriptionOffset { get; set; }

        /// <summary>
        /// Reads the base material data from a file.
        /// </summary>
        /// <param name="reader">The binary reader for this SegaNN file.</param>
        public void Read(BinaryReaderEx reader)
        {
            // Read this material's type and jump to the main data offset.
            Type = (MaterialType)reader.ReadUInt32();
            reader.JumpTo(reader.ReadUInt32(), true);

            /* Read this material's flag, user defined and reserved data, as well as the
               offsets for the approriate Colour, Logic and Texture Map descriptions. */
            Flag = (MaterialType)reader.ReadUInt32();
            UserDefined = reader.ReadInt32();
            MaterialColourOffset = reader.ReadUInt32();
            MaterialLogicOffset = reader.ReadUInt32();
            MaterialTexMapDescriptionOffset = reader.ReadUInt32();
            Reserved0 = reader.ReadUInt32();
            Reserved1 = reader.ReadUInt32();
            Reserved2 = reader.ReadUInt32();
        }

        /// <summary>
        /// Write this material to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        /// <param name="index">The number of this material in a linear list.</param>
        /// <param name="ObjectOffsets">The list of offsets this Object chunk uses.</param>
        /// <param name="MaterialColours">The list of Material Colours this object chunk has.</param>
        /// <param name="MaterialLogics">The list of Material Logic Definitions this object chunk has.</param>
        /// <param name="TextureMaps">The list of Material Texture Map Descriptors this object chunk has.</param>
        public void Write
        (
            BinaryWriterEx writer,
            int index,
            Dictionary<string, uint> ObjectOffsets,
            List<NinjaMaterialColours> MaterialColours,
            List<NinjaMaterialLogic> MaterialLogics,
            List<NinjaTextureMap> TextureMaps
        )
        {
            // Add an entry for this material into the offset list so we know where it is.
            ObjectOffsets.Add($"Material{index}", (uint)writer.BaseStream.Position);

            // Write this material's flag and user defined data.
            writer.Write((uint)Flag);
            writer.Write(UserDefined);

            // Add an offset to fill in later with the NOF0 chunk.
            writer.AddOffset($"Material{index}ColourOffset", 0);

            // Loop through the material colours, if we find one with an offset matching ours, then write its position value.
            for (int i = 0; i < MaterialColours.Count; i++)
            {
                if (MaterialColourOffset == MaterialColours[i].Offset)
                    writer.Write(ObjectOffsets[$"ColourOffset{i}"] - writer.Offset);
            }

            // Add an offset to fill in later with the NOF0 chunk.
            writer.AddOffset($"Material{index}LogicOffset", 0);

            // Loop through the material logic definitions, if we find one with an offset matching ours, then write its position value.
            for (int i = 0; i < MaterialLogics.Count; i++)
            {
                if (MaterialLogicOffset == MaterialLogics[i].Offset)
                    writer.Write(ObjectOffsets[$"LogicOffset{i}"] - writer.Offset);
            }

            // Make sure we actually have textures, if not, write a 0 instead of an offset.
            if (MaterialTexMapDescriptionOffset != 0)
            {
                // Add an offset to fill in later with the NOF0 chunk.
                writer.AddOffset($"Material{index}TexDescOffset", 0);

                // Loop through the texture map Descriptors, if we find one with an offset matching ours, then write its position value.
                for (int i = 0; i < TextureMaps.Count; i++)
                {
                    if (MaterialTexMapDescriptionOffset == TextureMaps[i].Offset)
                        writer.Write(ObjectOffsets[$"TexDescOffset{i}"] - writer.Offset);
                }
            }
            else
            {
                writer.Write(0);
            }
            
            // Write the reserved data.
            writer.Write(Reserved0);
            writer.Write(Reserved1);
            writer.Write(Reserved2);
        }

        /// <summary>
        /// Writes the type and pointer for this material to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        /// <param name="index">The number of this material in a linear list.</param>
        /// <param name="ObjectOffsets">The list of offsets this Object chunk uses.</param>
        public void WritePointer(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            // Write this material's type.
            writer.Write((uint)Type);

            // Add an offset to the BinaryWriter so we can fill it in in the NOF0 chunk.
            writer.AddOffset($"Material{index}", 0);

            // Write the value in ObjectOffsets of the main data for this material.
            writer.Write(ObjectOffsets[$"Material{index}"] - writer.Offset);
        }
    }
}
