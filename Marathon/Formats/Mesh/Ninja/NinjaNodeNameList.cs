namespace Marathon.Formats.Mesh.Ninja
{
    /// <summary>
    /// Structure of the main Ninja Node Name List.
    /// </summary>
    public class NinjaNodeNameList
    {
        public NodeNameSortType Type { get; set; }

        public List<string> NinjaNodeNames { get; set; } = new();

        /// <summary>
        /// Reads the Ninja Node Name List from a file.
        /// </summary>
        /// <param name="reader">The binary reader for this SegaNN file.</param>
        public void Read(BinaryReaderEx reader)
        {
            // Read the offset to the main data of this Node Name List chunk.
            uint dataOffset = reader.ReadUInt32();

            // Jump to the main data of this Node Name List chunk.
            reader.JumpTo(dataOffset, true);

            // Read the type of this Node Name List chunk, the amount of node names and the offset to the table of them.
            Type = (NodeNameSortType)reader.ReadUInt32();
            uint NodeCount = reader.ReadUInt32();
            uint NodeListOffset = reader.ReadUInt32();

            // Jump to the first node name in this Node Name List chunk.
            reader.JumpTo(NodeListOffset, true);

            // Loop through based on the count of node names in this chunk.
            for (int i = 0; i < NodeCount; i++)
            {
                // Read this Node's index (always linear) and the offset to this node's actual name.
                uint NodeID = reader.ReadUInt32();
                uint NodeName_NameOffset = reader.ReadUInt32();

                // Save our current position so we can jump back after reading.
                long pos = reader.BaseStream.Position;

                // Jump to the NameOffset, store the name of this node then jump back.
                reader.JumpTo(NodeName_NameOffset, true);
                NinjaNodeNames.Add(reader.ReadNullTerminatedString());
                reader.JumpTo(pos);
            }
        }

        /// <summary>
        /// Write the Ninja Node Name List to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        public void Write(BinaryWriterEx writer)
        {
            // Write NXNN header.
            writer.Write("NXNN");
            writer.Write("SIZE"); // Temporary entry, is filled in later once we know this chunk's size.
            long HeaderSizePosition = writer.BaseStream.Position;
            writer.AddOffset("dataOffset");
            writer.FixPadding(0x10);

            // Write Node Names.
            uint NodeNamesOffset = (uint)writer.BaseStream.Position - writer.Offset;
            for (int i = 0; i < NinjaNodeNames.Count; i++)
            {
                writer.Write(i);
                writer.AddOffset($"NodeName{i}_NameOffset");
            }

            // Write chunk data.
            writer.FillOffset("dataOffset", true, false);
            writer.Write((uint)Type);
            writer.Write(NinjaNodeNames.Count);
            writer.AddOffset($"NodeName", 0);
            writer.Write(NodeNamesOffset);

            // Write chunk string table.
            for (int i = 0; i < NinjaNodeNames.Count; i++)
            {
                writer.FillOffset($"NodeName{i}_NameOffset", true, false);
                writer.WriteNullTerminatedString(NinjaNodeNames[i]);
            }

            // Alignment.
            writer.FixPadding(0x10);

            // Write chunk size.
            long ChunkEndPosition = writer.BaseStream.Position;
            uint ChunkSize = (uint)(ChunkEndPosition - HeaderSizePosition);
            writer.BaseStream.Position = HeaderSizePosition - 4;
            writer.Write(ChunkSize);
            writer.BaseStream.Position = ChunkEndPosition;
        }
    }
}
