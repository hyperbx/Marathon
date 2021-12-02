namespace Marathon.Formats.Mesh.Ninja
{
    public class NinjaSubObject
    {
        public uint Type { get; set; }

        public List<NinjaMeshSet> MeshSets = new();

        public List<int> TextureIndices = new();

        public void Read(BinaryReaderEx reader)
        {
            Type = reader.ReadUInt32();
            uint MeshSetCount = reader.ReadUInt32();
            uint MeshSetsOffset = reader.ReadUInt32();
            uint TextureIndicesCount = reader.ReadUInt32();
            uint TextureIndicesOffset = reader.ReadUInt32();

            reader.JumpTo(MeshSetsOffset, true);
            for (int i = 0; i < MeshSetCount; i++)
            {
                NinjaMeshSet meshSet = new()
                {
                    Center = reader.ReadVector3(),
                    Radius = reader.ReadSingle(),
                    NodeIndex = reader.ReadInt32(),
                    MatrixIndex = reader.ReadInt32(),
                    MaterialIndex = reader.ReadInt32(),
                    VertexListIndex = reader.ReadInt32(),
                    PrimitiveListIndex = reader.ReadInt32(),
                    ShaderIndex = reader.ReadInt32()
                };
                MeshSets.Add(meshSet);
            }

            reader.JumpTo(TextureIndicesOffset, true);
            for (int i = 0; i < TextureIndicesCount; i++)
                TextureIndices.Add(reader.ReadInt32());
        }

        public void WriteMeshSets(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            ObjectOffsets.Add($"SubObject{index}MeshSets", (uint)writer.BaseStream.Position);
            for (int i = 0; i < MeshSets.Count; i++)
            {
                writer.Write(MeshSets[i].Center);
                writer.Write(MeshSets[i].Radius);
                writer.Write(MeshSets[i].NodeIndex);
                writer.Write(MeshSets[i].MatrixIndex);
                writer.Write(MeshSets[i].MaterialIndex);
                writer.Write(MeshSets[i].VertexListIndex);
                writer.Write(MeshSets[i].PrimitiveListIndex);
                writer.Write(MeshSets[i].ShaderIndex);
            }
        }

        public void WriteTextureIndices(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            ObjectOffsets.Add($"SubObject{index}TextureIndices", (uint)writer.BaseStream.Position);
            for (int i = 0; i < TextureIndices.Count; i++)
                writer.Write(TextureIndices[i]);
        }

        public void Write(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            writer.Write(Type);
            writer.Write(MeshSets.Count);
            writer.AddOffset($"SubObject{index}MeshSets", 0);
            writer.Write(ObjectOffsets[$"SubObject{index}MeshSets"] - writer.Offset);
            writer.Write(TextureIndices.Count);
            writer.AddOffset($"SubObject{index}TextureIndices", 0);
            writer.Write(ObjectOffsets[$"SubObject{index}TextureIndices"] - writer.Offset);
        }
    }

    public class NinjaMeshSet
    {
        public Vector3 Center { get; set; }

        public float Radius { get; set; }

        public int NodeIndex { get; set; }

        public int MatrixIndex { get; set; }

        public int MaterialIndex { get; set; }

        public int VertexListIndex { get; set; }

        public int PrimitiveListIndex { get; set; }

        public int ShaderIndex { get; set; }
    }
}
