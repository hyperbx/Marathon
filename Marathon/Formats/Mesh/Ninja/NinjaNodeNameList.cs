namespace Marathon.Formats.Mesh.Ninja
{
    public class NinjaNodeName
    {
        public uint ID { get; set; }

        public string Name { get; set; }

        public override string ToString() => Name;
    }

    public class NinjaNodeNameList
    {
        public uint Type { get; set; }

        public List<NinjaNodeName> NinjaNodeNames = new();

        public void Read(BinaryReaderEx reader)
        {
            uint dataOffset = reader.ReadUInt32();

            reader.JumpTo(dataOffset, true);

            Type = reader.ReadUInt32();
            uint NodeCount = reader.ReadUInt32();
            uint NodeListOffset = reader.ReadUInt32();

            reader.JumpTo(NodeListOffset, true);

            for (int i = 0; i < NodeCount; i++)
            {
                NinjaNodeName NodeName = new();
                NodeName.ID = reader.ReadUInt32();
                uint NodeName_NameOffset = reader.ReadUInt32();

                long pos = reader.BaseStream.Position;
                reader.JumpTo(NodeName_NameOffset, true);
                NodeName.Name = reader.ReadNullTerminatedString();
                reader.JumpTo(pos);

                NinjaNodeNames.Add(NodeName);
            }
        }

        public void Write(BinaryWriterEx writer)
        {
            // Chunk Header.
            writer.Write("NXNN");
            writer.Write("TEMP");
            long HeaderSizePosition = writer.BaseStream.Position;
            writer.AddOffset("dataOffset");
            writer.FixPadding(0x10);

            // Node Names.
            uint NodeNamesOffset = (uint)writer.BaseStream.Position - writer.Offset;
            for (int i = 0; i < NinjaNodeNames.Count; i++)
            {
                writer.Write(NinjaNodeNames[i].ID);
                writer.AddOffset($"NodeName{i}_NameOffset");
            }

            // Chunk Data.
            writer.FillOffset("dataOffset", true, false);
            writer.Write(Type);
            writer.Write(NinjaNodeNames.Count);
            writer.AddOffset($"NodeName", 0);
            writer.Write(NodeNamesOffset);

            // Chunk String Table.
            for (int i = 0; i < NinjaNodeNames.Count; i++)
            {
                writer.FillOffset($"NodeName{i}_NameOffset", true, false);
                writer.WriteNullTerminatedString(NinjaNodeNames[i].Name);
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
