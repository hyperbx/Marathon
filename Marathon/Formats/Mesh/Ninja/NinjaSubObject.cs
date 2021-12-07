namespace Marathon.Formats.Mesh.Ninja
{
    /// <summary>
    /// Structure of the a Ninja Sub Object.
    /// </summary>
    public class NinjaSubObject
    {
        public uint Type { get; set; }

        public List<NinjaMeshSet> MeshSets { get; set; } = new();

        public List<int> TextureIndices { get; set; } = new();

        /// <summary>
        /// Reads the sub object from a file.
        /// </summary>
        /// <param name="reader">The binary reader for this SegaNN file.</param>
        public void Read(BinaryReaderEx reader)
        {
            Type = reader.ReadUInt32();
            uint meshSetCount = reader.ReadUInt32();
            uint meshSetsOffset = reader.ReadUInt32();
            uint textureIndicesCount = reader.ReadUInt32();
            uint textureIndicesOffset = reader.ReadUInt32();

            // Jump to and save all the mesh sets for this sub object.
            reader.JumpTo(meshSetsOffset, true);
            for (int i = 0; i < meshSetCount; i++)
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

            // Jump to and save all the texture indices for this sub object.
            reader.JumpTo(textureIndicesOffset, true);
            for (int i = 0; i < textureIndicesCount; i++)
                TextureIndices.Add(reader.ReadInt32());
        }

        /// <summary>
        /// Writes this sub object's Mesh Sets to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        /// <param name="index">The number of this sub object in a linear list.</param>
        /// <param name="ObjectOffsets">The list of offsets this Object chunk uses.</param>
        public void WriteMeshSets(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            // Add an entry for this sub object's Mesh Sets into the offset list so we know where it is.
            ObjectOffsets.Add($"SubObject{index}MeshSets", (uint)writer.BaseStream.Position);

            // Loop through and write each Mesh Set.
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

        /// <summary>
        /// Writes this sub object's texture indices list to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        /// <param name="index">The number of this sub object in a linear list.</param>
        /// <param name="ObjectOffsets">The list of offsets this Object chunk uses.</param>
        public void WriteTextureIndices(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            // Add an entry for this sub object's texture indices list into the offset list so we know where it is.
            ObjectOffsets.Add($"SubObject{index}TextureIndices", (uint)writer.BaseStream.Position);

            // Loop through and write each texture index.
            for (int i = 0; i < TextureIndices.Count; i++)
                writer.Write(TextureIndices[i]);
        }

        /// <summary>
        /// Write this sub object to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        /// <param name="index">The number of this sub object in a linear list.</param>
        /// <param name="ObjectOffsets">The list of offsets this Object chunk uses.</param>
        public void Write(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            // Write this sub object's type and count of Mesh Sets.
            writer.Write(Type);
            writer.Write(MeshSets.Count);

            // Add an offset to fill in later with the NOF0 chunk.
            writer.AddOffset($"SubObject{index}MeshSets", 0);

            // Write the previously saved position for this sub object's Mesh Sets.
            writer.Write(ObjectOffsets[$"SubObject{index}MeshSets"] - writer.Offset);

            // Write this sub object's Texture Indices count.
            writer.Write(TextureIndices.Count);

            // Add an offset to fill in later with the NOF0 chunk.
            writer.AddOffset($"SubObject{index}TextureIndices", 0);

            // Write the previously saved position for this sub object's Texture Indices.
            writer.Write(ObjectOffsets[$"SubObject{index}TextureIndices"] - writer.Offset);
        }
    }

    /// <summary>
    /// Structure of a Ninja Object Mesh Set.
    /// </summary>
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
