namespace Marathon.Formats.Mesh.Ninja
{
    /// <summary>
    /// Structure of a Ninja Textue File entry.
    /// </summary>
    public class NinjaTextureFile
    {
        public uint Type { get; set; }

        public string FileName { get; set; }

        public NinjaNext_MinFilter MinFilter { get; set; }

        public NinjaNext_MagFilter MagFilter { get; set; }

        public uint GlobalIndex { get; set; }

        public uint Bank { get; set; }

        public override string ToString() => FileName;
    }

    /// <summary>
    /// Structure of the main Ninja Texture List.
    /// </summary>
    public class NinjaTextureList
    {
        public List<NinjaTextureFile> NinjaTextureFiles = new();

        /// <summary>
        /// Reads the Ninja Texture List from a file.
        /// </summary>
        /// <param name="reader">The binary reader for this SegaNN file.</param>
        public void Read(BinaryReaderEx reader)
        {
            // Read the offset to the main data of this Texture List chunk.
            uint dataOffset = reader.ReadUInt32();

            // Jump to the main data of this Texture List chunk.
            reader.JumpTo(dataOffset, true);

            // Read the amount of texture files in this Texture List chunk and the offset to the table of them.
            uint texFileCount = reader.ReadUInt32();
            uint texFilesOffset = reader.ReadUInt32();

            // Jump to the first texture file in this Texture List chunk.
            reader.JumpTo(texFilesOffset, true);

            // Loop through based on the count of texture files in this chunk.
            for(int i = 0; i < texFileCount; i++)
            {
                // Setup a new texture file entry.
                NinjaTextureFile TextureFile = new();

                // Read the values and the offset to the filename for this texture.
                TextureFile.Type = reader.ReadUInt32();
                uint TextureFile_NameOffset = reader.ReadUInt32();
                TextureFile.MinFilter = (NinjaNext_MinFilter)reader.ReadUInt16();
                TextureFile.MagFilter = (NinjaNext_MagFilter)reader.ReadUInt16();
                TextureFile.GlobalIndex = reader.ReadUInt32();
                TextureFile.Bank = reader.ReadUInt32();

                // Save our current position so we can jump back after reading.
                long pos = reader.BaseStream.Position;

                // Jump to the NameOffset, read the name of this texture file then jump back.
                reader.JumpTo(TextureFile_NameOffset, true);
                TextureFile.FileName = reader.ReadNullTerminatedString();
                reader.JumpTo(pos);

                // Save this texture.
                NinjaTextureFiles.Add(TextureFile);
            }
        }

        /// <summary>
        /// Write the Ninja Texture List to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        public void Write(BinaryWriterEx writer)
        {
            // Chunk Header.
            writer.Write("NXTL");
            writer.Write("SIZE"); // Temporary entry, is filled in later once we know this chunk's size.
            long HeaderSizePosition = writer.BaseStream.Position;
            writer.AddOffset("dataOffset");
            writer.FixPadding(0x10);
            
            // Texture Files.
            uint texFilesOffset = (uint)writer.BaseStream.Position - writer.Offset;
            for (int i = 0; i < NinjaTextureFiles.Count; i++)
            {
                writer.Write(NinjaTextureFiles[i].Type);
                writer.AddOffset($"TextureFile{i}_NameOffset");
                writer.Write((ushort)NinjaTextureFiles[i].MinFilter);
                writer.Write((ushort)NinjaTextureFiles[i].MagFilter);
                writer.Write(NinjaTextureFiles[i].GlobalIndex);
                writer.Write(NinjaTextureFiles[i].Bank);
            }

            // Chunk Data.
            writer.FillOffset("dataOffset", true, false);
            writer.Write(NinjaTextureFiles.Count);
            writer.AddOffset($"TextureFiles", 0);
            writer.Write(texFilesOffset);

            // Chunk String Table.
            for (int i = 0; i < NinjaTextureFiles.Count; i++)
            {
                writer.FillOffset($"TextureFile{i}_NameOffset", true, false);
                writer.WriteNullTerminatedString(NinjaTextureFiles[i].FileName);
            }

            // Alignment.
            writer.FixPadding(0x10);

            // Chunk Size.
            long ChunkEndPosition = writer.BaseStream.Position;
            uint ChunkSize = (uint)(ChunkEndPosition - HeaderSizePosition);
            writer.BaseStream.Position = HeaderSizePosition - 0x04;
            writer.Write(ChunkSize);
            writer.BaseStream.Position = ChunkEndPosition;
        }
    }
}
