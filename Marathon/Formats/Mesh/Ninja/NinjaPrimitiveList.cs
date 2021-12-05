namespace Marathon.Formats.Mesh.Ninja
{
    public class NinjaPrimitiveList
    {
        public NinjaNext_PrimitiveType Type { get; set; }

        public uint Format { get; set; }

        public List<ushort> StripIndices = new();

        public List<ushort> IndexIndices = new();

        public uint IndexBuffer { get; set; }

        public uint Reserved0 { get; set; }

        public uint Reserved1 { get; set; }

        public void Read(BinaryReaderEx reader)
        {
            Type = (NinjaNext_PrimitiveType)reader.ReadUInt32();
            reader.JumpTo(reader.ReadUInt32(), true);

            Format = reader.ReadUInt32();
            uint IndexCount = reader.ReadUInt32();
            uint StripCount = reader.ReadUInt32();
            uint StripIndicesOffset = reader.ReadUInt32();
            uint IndexIndicesOffset = reader.ReadUInt32();
            IndexBuffer = reader.ReadUInt32();
            Reserved0 = reader.ReadUInt32();
            Reserved1 = reader.ReadUInt32();

            reader.JumpTo(StripIndicesOffset, true);
            for (int i = 0; i < StripCount; i++)
                StripIndices.Add(reader.ReadUInt16());

            reader.JumpTo(IndexIndicesOffset, true);
            for (int i = 0; i < IndexCount; i++)
                IndexIndices.Add(reader.ReadUInt16());
        }

        public void WriteStripIndices(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            ObjectOffsets.Add($"Primitive{index}StripIndices", (uint)writer.BaseStream.Position);
            for (int i = 0; i < StripIndices.Count; i++)
                writer.Write(StripIndices[i]);

            writer.FixPadding();
        }

        public void WriteIndexIndices(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            ObjectOffsets.Add($"Primitive{index}IndexIndices", (uint)writer.BaseStream.Position);
            for (int i = 0; i < IndexIndices.Count; i++)
                writer.Write(IndexIndices[i]);

            writer.FixPadding();
        }

        public void Write(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            ObjectOffsets.Add($"Primitive{index}", (uint)writer.BaseStream.Position);
            writer.Write(Format);
            writer.Write(IndexIndices.Count);
            writer.Write(StripIndices.Count);
            writer.AddOffset($"Primitive{index}StripIndices", 0);
            writer.Write(ObjectOffsets[$"Primitive{index}StripIndices"] - writer.Offset);
            writer.AddOffset($"Primitive{index}IndexIndices", 0);
            writer.Write(ObjectOffsets[$"Primitive{index}IndexIndices"] - writer.Offset);
            writer.Write(IndexBuffer);
            writer.Write(Reserved0);
            writer.Write(Reserved1);
        }

        public void WritePointer(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            writer.Write((uint)Type);
            writer.AddOffset($"Primitive{index}", 0);
            writer.Write(ObjectOffsets[$"Primitive{index}"] - writer.Offset);
        }
    }
}
