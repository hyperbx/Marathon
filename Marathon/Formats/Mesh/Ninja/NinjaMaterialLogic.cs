namespace Marathon.Formats.Mesh.Ninja
{
    /// <summary>
    /// Structure of a Ninja Object Material's Material Logic.
    /// This is kept seperate from Materials as multiple materials can use the same logic.
    /// </summary>
    public class NinjaMaterialLogic
    {
        public bool Blend { get; set; }

        public BlendMode SRCBlend { get; set; }

        public BlendMode DSTBlend { get; set; }

        public uint BlendFactor { get; set; }

        public BlendOperation BlendOperation { get; set; }

        public LogicOperation LogicOperation { get; set; }

        public bool Alpha { get; set; }

        public CMPFunction AlphaFunction { get; set; }

        public uint AlphaRef { get; set; }

        public bool ZComparison { get; set; }

        public CMPFunction ZComparisonFunction { get; set; }

        public bool ZUpdate { get; set; }

        public uint Reserved0 { get; set; }

        public uint Reserved1 { get; set; }

        public uint Reserved2 { get; set; }

        public uint Reserved3 { get; set; }

        /// <summary>
        /// This offset is stored by us purely for the writing process.
        /// </summary>
        public uint Offset { get; set; }

        /// <summary>
        /// Reads a material's logic from a file.
        /// </summary>
        /// <param name="reader">The binary reader for this SegaNN file.</param>
        public void Read(BinaryReaderEx reader)
        {
            // Skip over the material's Type.
            reader.JumpAhead(4);

            // Jump to the material's main data.
            reader.JumpTo(reader.ReadUInt32(), true);

            // Skip over the material's Flag, User Defined data and Colour offset.
            reader.JumpAhead(0xC);

            // Save the offset for the writing process then jump to it.
            Offset = reader.ReadUInt32();
            reader.JumpTo(Offset, true);

            // Save the logic data for this material.
            Blend = reader.ReadBoolean(4);
            SRCBlend = (BlendMode)reader.ReadUInt32();
            DSTBlend = (BlendMode)reader.ReadUInt32();
            BlendFactor = reader.ReadUInt32();
            BlendOperation = (BlendOperation)reader.ReadUInt32();
            LogicOperation = (LogicOperation)reader.ReadUInt32();
            Alpha = reader.ReadBoolean(4);
            AlphaFunction = (CMPFunction)reader.ReadUInt32();
            AlphaRef = reader.ReadUInt32();
            ZComparison = reader.ReadBoolean(4);
            ZComparisonFunction = (CMPFunction)reader.ReadUInt32();
            ZUpdate = reader.ReadBoolean(4);
            Reserved0 = reader.ReadUInt32();
            Reserved1 = reader.ReadUInt32();
            Reserved2 = reader.ReadUInt32();
            Reserved3 = reader.ReadUInt32();
        }

        /// <summary>
        /// Writes this material logic entry to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        /// <param name="index">The number of this material logic entry in a linear list.</param>
        /// <param name="ObjectOffsets">The list of offsets this Object chunk uses.</param>
        public void Write(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            // Add an entry for this material logic entry into the offset list so we know where it is.
            ObjectOffsets.Add($"LogicOffset{index}", (uint)writer.BaseStream.Position);

            // Write the material logic data.
            writer.Write(Blend);
            writer.FixPadding();
            writer.Write((uint)SRCBlend);
            writer.Write((uint)DSTBlend);
            writer.Write(BlendFactor);
            writer.Write((uint)BlendOperation);
            writer.Write((uint)LogicOperation);
            writer.Write(Alpha);
            writer.FixPadding();
            writer.Write((uint)AlphaFunction);
            writer.Write(AlphaRef);
            writer.Write(ZComparison);
            writer.FixPadding();
            writer.Write((uint)ZComparisonFunction);
            writer.Write(ZUpdate);
            writer.FixPadding();
            writer.Write(Reserved0);
            writer.Write(Reserved1);
            writer.Write(Reserved2);
            writer.Write(Reserved3);
        }

        public override bool Equals(object obj)
        {
            if (obj is NinjaMaterialLogic)
                return Equals((NinjaMaterialLogic)obj);

            return false;
        }

        public bool Equals(NinjaMaterialLogic obj)
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
