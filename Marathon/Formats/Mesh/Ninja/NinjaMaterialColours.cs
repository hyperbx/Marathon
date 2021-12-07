namespace Marathon.Formats.Mesh.Ninja
{
    /// <summary>
    /// Structure of a Ninja Object Material's Material Colours.
    /// This is kept seperate from Materials as multiple materials can use the same colours.
    /// </summary>
    public class NinjaMaterialColours
    {
        public Vector4 Diffuse { get; set; }

        public Vector4 Ambient { get; set; }

        public Vector4 Specular { get; set; }

        public Vector4 Emissive { get; set; }

        public float Power { get; set; }

        public uint Reserved0 { get; set; }

        public uint Reserved1 { get; set; }

        public uint Reserved2 { get; set; }

        /// <summary>
        /// This offset is stored by us purely for the writing process.
        /// </summary>
        public uint Offset { get; set; }

        /// <summary>
        /// Reads a material's colours from a file.
        /// </summary>
        /// <param name="reader">The binary reader for this SegaNN file.</param>
        public void Read(BinaryReaderEx reader)
        {
            // Skip over the material's Type.
            reader.JumpAhead(4);
            
            // Jump to the material's main data.
            reader.JumpTo(reader.ReadUInt32(), true);

            // Skip over the material's Flag and User Defined data.
            reader.JumpAhead(8);

            // Save the offset for the writing process then jump to it.
            Offset = reader.ReadUInt32();
            reader.JumpTo(Offset, true);

            // Save the colour data for this material.
            Diffuse = reader.ReadVector4();
            Ambient = reader.ReadVector4();
            Specular = reader.ReadVector4();
            Emissive = reader.ReadVector4();
            Power = reader.ReadSingle();
            Reserved0 = reader.ReadUInt32();
            Reserved1 = reader.ReadUInt32();
            Reserved2 = reader.ReadUInt32();
        }

        /// <summary>
        /// Writes this material colour entry to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        /// <param name="index">The number of this material colour entry in a linear list.</param>
        /// <param name="ObjectOffsets">The list of offsets this Object chunk uses.</param>
        public void Write(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            // Add an entry for this material colour entry into the offset list so we know where it is.
            ObjectOffsets.Add($"ColourOffset{index}", (uint)writer.BaseStream.Position);

            // Write the material colour data.
            writer.Write(Diffuse);
            writer.Write(Ambient);
            writer.Write(Specular);
            writer.Write(Emissive);
            writer.Write(Power);
            writer.Write(Reserved0);
            writer.Write(Reserved1);
            writer.Write(Reserved2);
        }

        public override bool Equals(object obj)
        {
            if (obj is NinjaMaterialColours)
                return Equals((NinjaMaterialColours)obj);

            return false;
        }

        public bool Equals(NinjaMaterialColours obj)
        {
            if (obj == null)
                return false;

            if (!EqualityComparer<uint>.Default.Equals(Offset, obj.Offset))
                return false;

            return true;
        }

        public override int GetHashCode() => EqualityComparer<uint>.Default.GetHashCode(Offset);
    }
}
