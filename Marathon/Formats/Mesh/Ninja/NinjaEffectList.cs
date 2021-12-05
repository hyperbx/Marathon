namespace Marathon.Formats.Mesh.Ninja
{
    /// <summary>
    /// Structure of a Ninja Effect File entry.
    /// </summary>
    public class NinjaEffectFile
    {
        public uint Type { get; set; }

        public string FileName { get; set; }

        public override string ToString() => FileName;
    }

    /// <summary>
    /// Structure of a Ninja Technique Name entry.
    /// </summary>
    public class NinjaTechniqueName
    {
        public uint Type { get; set; }

        public string Name { get; set; }

        public override string ToString() => Name;
    }

    /// <summary>
    /// Structure of the main Ninja Effect List.
    /// </summary>
    public class NinjaEffectList
    {
        public uint Type { get; set; }

        public List<NinjaEffectFile> NinjaEffectFiles = new();

        public List<NinjaTechniqueName> NinjaTechniqueNames = new();

        public List<short> NinjaTechniqueIndices = new();

        /// <summary>
        /// Reads the Ninja Effect List from a file.
        /// </summary>
        /// <param name="reader">The binary reader for this SegaNN file.</param>
        public void Read(BinaryReaderEx reader)
        {
            // Read the offset to the actual Ninja Effect List.
            uint dataOffset = reader.ReadUInt32();

            // Jump to the actual Ninja Effect List.
            reader.JumpTo(dataOffset, true);

            // Read all of the data from the Ninja Effect List.
            Type = reader.ReadUInt32();
            uint EffectFilesCount = reader.ReadUInt32();
            uint EffectFilesOffset = reader.ReadUInt32();
            uint TechniqueNamesCount = reader.ReadUInt32();
            uint TechniqueNamesOffset = reader.ReadUInt32();
            uint TechniqueIndicesCount = reader.ReadUInt32();
            uint TechniqueIndicesOffset = reader.ReadUInt32();

            // Jump to and read each Effect File.
            reader.JumpTo(EffectFilesOffset, true);
            for (int i = 0; i < EffectFilesCount; i++)
            {
                // Set up a new Effect File.
                NinjaEffectFile EffectFile = new();

                // Read this Effect File's type.
                EffectFile.Type = reader.ReadUInt32();

                // Read the offset to this Effect File's name.
                uint EffectFile_NameOffset = reader.ReadUInt32();

                // Store our current position.
                long pos = reader.BaseStream.Position;

                // Jump to this Effect File's Name Offset, read it and jump back.
                reader.JumpTo(EffectFile_NameOffset, true);
                EffectFile.FileName = reader.ReadNullTerminatedString();
                reader.JumpTo(pos);

                // Save this Effect File.
                NinjaEffectFiles.Add(EffectFile);
            }

            // Jump to and read each Technique Name.
            reader.JumpTo(TechniqueNamesOffset, true);
            for (int i = 0; i < TechniqueNamesCount; i++)
            {
                // Set up a new Technique Name.
                NinjaTechniqueName TechniqueName = new();

                // Read this Effect Technique's Type and Effect File Index.
                TechniqueName.Type = reader.ReadUInt32();
                uint EffectFileIndex = reader.ReadUInt32();

                // Read the offset to this Technique Name's... Well... Name.
                uint EffectFile_NameOffset = reader.ReadUInt32();

                // Store our current position.
                long pos = reader.BaseStream.Position;

                // Jump to this Technique Name's Name Offset, read it and jump back.
                reader.JumpTo(EffectFile_NameOffset, true);
                TechniqueName.Name = reader.ReadNullTerminatedString();
                reader.JumpTo(pos);

                // Save this Technique Name.
                NinjaTechniqueNames.Add(TechniqueName);
            }

            // Jump to and read each Technique Index.
            reader.JumpTo(TechniqueIndicesOffset, true);
            for (int i = 0; i < TechniqueIndicesCount; i++)
                NinjaTechniqueIndices.Add(reader.ReadInt16());
        }

        /// <summary>
        /// Write the Ninja Effect List to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        public void Write(BinaryWriterEx writer)
        {
            // Chunk Header.
            writer.Write("NXEF");
            writer.Write("SIZE"); // Temporary entry, is filled in later once we know this chunk's size.
            long HeaderSizePosition = writer.BaseStream.Position;
            writer.AddOffset("dataOffset");
            writer.FixPadding(0x10);

            // Effect Files.
            uint EffectFilesOffset = (uint)writer.BaseStream.Position - writer.Offset;
            for (int i = 0; i < NinjaEffectFiles.Count; i++)
            {
                writer.Write(NinjaEffectFiles[i].Type);
                writer.AddOffset($"EffectFile{i}_NameOffset");
            }

            // Technique Names.
            uint TechniqueNamesOffset = (uint)writer.BaseStream.Position - writer.Offset;
            for (int i = 0; i < NinjaTechniqueNames.Count; i++)
            {
                writer.Write(NinjaTechniqueNames[i].Type);
                writer.Write(i);
                writer.AddOffset($"TechniqueName{i}_NameOffset");
            }

            // Technique Indices.
            uint TechniqueIndicesOffset = (uint)writer.BaseStream.Position - writer.Offset;
            for (int i = 0; i < NinjaTechniqueIndices.Count; i++)
                writer.Write(NinjaTechniqueIndices[i]);

            writer.FixPadding();

            // Chunk Data.
            writer.FillOffset("dataOffset", true, false);
            writer.Write(Type);
            writer.Write(NinjaEffectFiles.Count);
            writer.AddOffset($"EffectFiles", 0);
            writer.Write(EffectFilesOffset);
            writer.Write(NinjaTechniqueNames.Count);
            writer.AddOffset($"TechniqueNames", 0);
            writer.Write(TechniqueNamesOffset);
            writer.Write(NinjaTechniqueIndices.Count);
            writer.AddOffset($"TechniqueIndices", 0);
            writer.Write(TechniqueIndicesOffset);

            // Chunk String Table.
            for (int i = 0; i < NinjaEffectFiles.Count; i++)
            {
                writer.FillOffset($"EffectFile{i}_NameOffset", true, false);
                writer.WriteNullTerminatedString(NinjaEffectFiles[i].FileName);
            }
            for (int i = 0; i < NinjaTechniqueNames.Count; i++)
            {
                writer.FillOffset($"TechniqueName{i}_NameOffset", true, false);
                writer.WriteNullTerminatedString(NinjaTechniqueNames[i].Name);
            }

            // Alignment.
            writer.FixPadding(0x10);

            // Calculate and fill in chunk size.
            long ChunkEndPosition = writer.BaseStream.Position;
            uint ChunkSize = (uint)(ChunkEndPosition - HeaderSizePosition);
            writer.BaseStream.Position = HeaderSizePosition - 0x04;
            writer.Write(ChunkSize);
            writer.BaseStream.Position = ChunkEndPosition;
        }
    }
}
