namespace Marathon.Formats.Mesh.Ninja
{
    /// <summary>
    /// Structure of a Ninja Object Primitive List.
    /// </summary>
    public class NinjaPrimitiveList
    {
        public NinjaNext_PrimitiveType Type { get; set; }

        public uint Format { get; set; }

        public List<ushort> StripIndices = new();

        public List<ushort> IndexIndices = new();

        public uint IndexBuffer { get; set; }

        public uint Reserved0 { get; set; }

        public uint Reserved1 { get; set; }

        /// <summary>
        /// Reads the primitive list data from a file.
        /// </summary>
        /// <param name="reader">The binary reader for this SegaNN file.</param>
        public void Read(BinaryReaderEx reader)
        {
            // Read this primitive list's type and jump to the main data offset.
            Type = (NinjaNext_PrimitiveType)reader.ReadUInt32();
            reader.JumpTo(reader.ReadUInt32(), true);

            // Read the data associated with this primitive list.
            Format = reader.ReadUInt32();
            uint IndexCount = reader.ReadUInt32();
            uint StripCount = reader.ReadUInt32();
            uint StripIndicesOffset = reader.ReadUInt32();
            uint IndexIndicesOffset = reader.ReadUInt32();
            IndexBuffer = reader.ReadUInt32();
            Reserved0 = reader.ReadUInt32();
            Reserved1 = reader.ReadUInt32();

            // Jump to and read the Strip Indices for this primitive list.
            reader.JumpTo(StripIndicesOffset, true);
            for (int i = 0; i < StripCount; i++)
                StripIndices.Add(reader.ReadUInt16());

            // Jump to and read the Index Indices for this primitive list.
            reader.JumpTo(IndexIndicesOffset, true);
            for (int i = 0; i < IndexCount; i++)
                IndexIndices.Add(reader.ReadUInt16());
        }

        /// <summary>
        /// Write this primitive list to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        /// <param name="index">The number of this primitive list in a linear list.</param>
        /// <param name="ObjectOffsets">The list of offsets this Object chunk uses.</param>
        public void Write(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            // Add an entry for this primitive list into ObjectOffsets so we know where it is.
            ObjectOffsets.Add($"Primitive{index}", (uint)writer.BaseStream.Position);

            // Write the data for this primitive list.
            writer.Write(Format);
            writer.Write(IndexIndices.Count);
            writer.Write(StripIndices.Count);

            // Add an offset to the BinaryWriter so we can fill it in in the NOF0 chunk.
            writer.AddOffset($"Primitive{index}StripIndices", 0);

            // Write the value in ObjectOffsets of the strip indices of this primitive list.
            writer.Write(ObjectOffsets[$"Primitive{index}StripIndices"] - writer.Offset);

            // Add an offset to the BinaryWriter so we can fill it in in the NOF0 chunk.
            writer.AddOffset($"Primitive{index}IndexIndices", 0);

            // Write the value in ObjectOffsets of the index indices of this primitive list.
            writer.Write(ObjectOffsets[$"Primitive{index}IndexIndices"] - writer.Offset);
            writer.Write(IndexBuffer);
            writer.Write(Reserved0);
            writer.Write(Reserved1);
        }

        /// <summary>
        /// Writes this primitive list's Strip Indices to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        /// <param name="index">The number of this primitive list in a linear list.</param>
        /// <param name="ObjectOffsets">The list of offsets this Object chunk uses.</param>
        public void WriteStripIndices(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            // Add an entry for this primitive list's strip indices into ObjectOffsets so we know where it is.
            ObjectOffsets.Add($"Primitive{index}StripIndices", (uint)writer.BaseStream.Position);

            // Write this primitive list's strip indices.
            for (int i = 0; i < StripIndices.Count; i++)
                writer.Write(StripIndices[i]);

            // Ensure we're still aligned to four bytes.
            writer.FixPadding();
        }

        /// <summary>
        /// Writes this primitive list's Index Indices to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        /// <param name="index">The number of this primitive list in a linear list.</param>
        /// <param name="ObjectOffsets">The list of offsets this Object chunk uses.</param>
        public void WriteIndexIndices(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            // Add an entry for this primitive list's index indices into ObjectOffsets so we know where it is.
            ObjectOffsets.Add($"Primitive{index}IndexIndices", (uint)writer.BaseStream.Position);

            // Write this primitive list's index indices.
            for (int i = 0; i < IndexIndices.Count; i++)
                writer.Write(IndexIndices[i]);

            // Ensure we're still aligned to four bytes.
            writer.FixPadding();
        }

        /// <summary>
        /// Writes the type and pointer for this primitive list to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        /// <param name="index">The number of this primitive list in a linear list.</param>
        /// <param name="ObjectOffsets">The list of offsets this Object chunk uses.</param>
        public void WritePointer(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            // Write this primitive list's type.
            writer.Write((uint)Type);

            // Add an offset to the BinaryWriter so we can fill it in in the NOF0 chunk.
            writer.AddOffset($"Primitive{index}", 0);

            // Write the value in ObjectOffsets of the main data for this primitive list.
            writer.Write(ObjectOffsets[$"Primitive{index}"] - writer.Offset);
        }
    }
}
