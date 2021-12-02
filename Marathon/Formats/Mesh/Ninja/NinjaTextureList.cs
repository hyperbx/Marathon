namespace Marathon.Formats.Mesh.Ninja
{
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

    public class NinjaTextureList
    {
        public List<NinjaTextureFile> NinjaTextureFiles = new();

        public void Read(BinaryReaderEx reader)
        {
            uint dataOffset = reader.ReadUInt32();

            reader.JumpTo(dataOffset, true);

            uint texFileCount = reader.ReadUInt32();
            uint texFilesOffset = reader.ReadUInt32();

            reader.JumpTo(texFilesOffset, true);

            for(int i = 0; i < texFileCount; i++)
            {
                NinjaTextureFile TextureFile = new();
                TextureFile.Type = reader.ReadUInt32();
                uint TextureFile_NameOffset = reader.ReadUInt32();
                TextureFile.MinFilter = (NinjaNext_MinFilter)reader.ReadUInt16();
                TextureFile.MagFilter = (NinjaNext_MagFilter)reader.ReadUInt16();
                TextureFile.GlobalIndex = reader.ReadUInt32();
                TextureFile.Bank = reader.ReadUInt32();

                long pos = reader.BaseStream.Position;
                reader.JumpTo(TextureFile_NameOffset, true);
                TextureFile.FileName = reader.ReadNullTerminatedString();
                reader.JumpTo(pos);

                NinjaTextureFiles.Add(TextureFile);
            }
        }

        public void Write(BinaryWriterEx writer)
        {
            // Chunk Header.
            writer.Write("NXTL");
            writer.Write("TEMP");
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
