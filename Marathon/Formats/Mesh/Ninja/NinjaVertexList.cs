namespace Marathon.Formats.Mesh.Ninja
{
    /// <summary>
    /// Structure of the main Ninja Vertex List.
    /// </summary>
    public class NinjaVertexList
    {
        public VertexType Type { get; set; }

        public XboxVertexType Format { get; set; }

        public FlexibleVertexFormat FlexibleVertexFormat { get; set; }

        public List<NinjaVertex> Vertices { get; set; } = new();

        public List<int> BoneMatrixIndices { get; set; } = new();

        public uint HDRCommon { get; set; }

        public uint HDRData { get; set; }

        public uint HDRLock { get; set; }

        /// <summary>
        /// Reads the Ninja Vertex List from a file.
        /// </summary>
        /// <param name="reader">The binary reader for this SegaNN file.</param>
        public void Read(BinaryReaderEx reader)
        {
            // Read this Vertex List's type and jump to the main data offset.
            Type = (VertexType)reader.ReadUInt32();
            reader.JumpTo(reader.ReadUInt32(), true);

            // Read the data associated with this Vertex List.
            Format = (XboxVertexType)reader.ReadUInt32();
            FlexibleVertexFormat = (FlexibleVertexFormat)reader.ReadUInt32();
            uint DataSize = reader.ReadUInt32();
            uint VertexCount = reader.ReadUInt32();
            uint VertexListOffset = reader.ReadUInt32();
            uint BoneCount = reader.ReadUInt32();
            uint BoneMatrixIndicesOffset = reader.ReadUInt32();
            HDRCommon = reader.ReadUInt32();
            HDRData = reader.ReadUInt32();
            HDRLock = reader.ReadUInt32();

            // Jump to and read all the vertices in this list.
            reader.JumpTo(VertexListOffset, true);
            for (int i = 0; i < VertexCount; i++)
            {
                NinjaVertex Vertex = new();

                // Read different elements depending on the Format flag.
                {
                    if (Format.HasFlag(XboxVertexType.NND_VTXTYPE_XB_POSITION))
                        Vertex.Position = reader.ReadVector3();

                    if (Format.HasFlag(XboxVertexType.NND_VTXTYPE_XB_WEIGHT3))
                        Vertex.Weight = reader.ReadVector3();

                    if (Format.HasFlag(XboxVertexType.NND_VTXTYPE_XB_MTX_INDEX4))
                        Vertex.MatrixIndices = reader.ReadBytes(4);

                    if (Format.HasFlag(XboxVertexType.NND_VTXTYPE_XB_NORMAL))
                        Vertex.Normals = reader.ReadVector3();

                    if (Format.HasFlag(XboxVertexType.NND_VTXTYPE_XB_COLOR))
                        Vertex.VertexColours = reader.ReadBytes(4);

                    if (Format.HasFlag(XboxVertexType.NND_VTXTYPE_XB_COLOR2))
                        Vertex.VertexColours2 = reader.ReadBytes(4);

                    // Knuxfan24: I don't know why this works, but it does.
                    for (int t = 0; t < ((uint)Format / (uint)XboxVertexType.NND_VTXTYPE_XB_TEXCOORD); t++)
                    {
                        // Actually create the Texture Coordinates list if it doesn't exist already.
                        if (Vertex.TextureCoordinates == null)
                            Vertex.TextureCoordinates = new();

                        Vertex.TextureCoordinates.Add(reader.ReadVector2());
                    }

                    if (Format.HasFlag(XboxVertexType.NND_VTXTYPE_XB_TANGENT))
                        Vertex.Tangent = reader.ReadVector3();

                    if (Format.HasFlag(XboxVertexType.NND_VTXTYPE_XB_BINORMAL))
                        Vertex.Binormals = reader.ReadVector3();
                }

                // Save this vertex.
                Vertices.Add(Vertex);
            }

            // Jump to and read the Bone Matrix Indices for this Vertex List.
            reader.JumpTo(BoneMatrixIndicesOffset, true);
            for (int i = 0; i < BoneCount; i++)
                BoneMatrixIndices.Add(reader.ReadInt32());
        }

        /// <summary>
        /// Writes this vertex list to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        /// <param name="index">The number of this vertex list in a linear list.</param>
        /// <param name="ObjectOffsets">The list of offsets this Object chunk uses.</param>
        public void Write(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            // Calculate the Data Size based on the Format flag.
            uint dataSize = 0;
            {
                if (Format.HasFlag(XboxVertexType.NND_VTXTYPE_XB_POSITION))
                    dataSize += 12;

                if (Format.HasFlag(XboxVertexType.NND_VTXTYPE_XB_WEIGHT3))
                    dataSize += 12;

                if (Format.HasFlag(XboxVertexType.NND_VTXTYPE_XB_MTX_INDEX4))
                    dataSize += 4;

                if (Format.HasFlag(XboxVertexType.NND_VTXTYPE_XB_NORMAL))
                    dataSize += 12;

                if (Format.HasFlag(XboxVertexType.NND_VTXTYPE_XB_COLOR))
                    dataSize += 4;

                if (Format.HasFlag(XboxVertexType.NND_VTXTYPE_XB_COLOR2))
                    dataSize += 4;

                if (Vertices[0].TextureCoordinates != null)
                    dataSize += (uint)(8 * Vertices[0].TextureCoordinates.Count);

                if (Format.HasFlag(XboxVertexType.NND_VTXTYPE_XB_TANGENT))
                    dataSize += 12;

                if (Format.HasFlag(XboxVertexType.NND_VTXTYPE_XB_BINORMAL))
                    dataSize += 12;
            }

            // Add an entry for this vertex list's bone matrix indices into ObjectOffsets so we know where it is.
            ObjectOffsets.Add($"VertexList{index}BoneMatrixIndicesOffset", (uint)writer.BaseStream.Position);

            // Write this vertex list's Bone Matrix Indices.
            for (int i = 0; i < BoneMatrixIndices.Count; i++)
                writer.Write(BoneMatrixIndices[i]);

            // Add an entry for this vertex list into ObjectOffsets so we know where it is.
            ObjectOffsets.Add($"VertexList{index}", (uint)writer.BaseStream.Position);

            // Write the data for this vertex list.
            writer.Write((uint)Format);
            writer.Write((uint)FlexibleVertexFormat);
            writer.Write(dataSize);
            writer.Write(Vertices.Count);
            writer.AddOffset($"VertexList{index}Vertices");
            writer.Write(BoneMatrixIndices.Count);

            // Only handle a Bone Matrix Indices offset if we have any, if not, write a 0 and don't add an offset for the NOF0 chunl.
            if (BoneMatrixIndices.Count != 0)
            {
                writer.AddOffset($"VertexList{index}BoneMatrixIndicesOffset", 0);
                writer.Write(ObjectOffsets[$"VertexList{index}BoneMatrixIndicesOffset"] - writer.Offset);
            }
            else
            {
                writer.Write(0);
            }

            writer.Write(HDRCommon);
            writer.Write(HDRData);
            writer.Write(HDRLock);
            writer.WriteNulls(8);
        }

        /// <summary>
        /// Writes the vertices for this vertex list to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        /// <param name="index">The number of this vertex list in a linear list.</param>
        public void WriteVertices(BinaryWriterEx writer, int index)
        {
            // Set this as the location of this vertex table.
            writer.FillOffset($"VertexList{index}Vertices", true, false);

            // Loop through and write the values in each vertex that isn't null.
            for (int i = 0; i < Vertices.Count; i++)
            {
                if (Vertices[i].Position != null)
                    writer.Write((Vector3)Vertices[i].Position);

                if (Vertices[i].Weight != null)
                    writer.Write((Vector3)Vertices[i].Weight);

                if (Vertices[i].MatrixIndices != null)
                    writer.Write(Vertices[i].MatrixIndices);

                if (Vertices[i].Normals != null)
                    writer.Write((Vector3)Vertices[i].Normals);

                if (Vertices[i].VertexColours != null)
                    writer.Write(Vertices[i].VertexColours);

                if (Vertices[i].VertexColours2 != null)
                    writer.Write(Vertices[i].VertexColours2);

                if (Vertices[i].TextureCoordinates != null)
                {
                    foreach (Vector2 Coordinate in Vertices[i].TextureCoordinates)
                        writer.Write(Coordinate);
                }

                if (Vertices[i].Tangent != null)
                    writer.Write((Vector3)Vertices[i].Tangent);

                if (Vertices[i].Binormals != null)
                    writer.Write((Vector3)Vertices[i].Binormals);
            }
        }

        /// <summary>
        /// Writes the type and pointer for this vertex list to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        /// <param name="index">The number of this vertex list in a linear list.</param>
        /// <param name="ObjectOffsets">The list of offsets this Object chunk uses.</param>
        public void WritePointer(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            // Write this vertex list's type.
            writer.Write((uint)Type);

            // Add an offset to fill in later with the NOF0 chunk.
            writer.AddOffset($"VertexList{index}", 0);

            // Write the value in the offset list of the main data for this vertex list.
            writer.Write(ObjectOffsets[$"VertexList{index}"] - writer.Offset);
        }
    }

    /// <summary>
    /// Structure of a Ninja Vertex.
    /// </summary>
    public class NinjaVertex
    {
        public Vector3? Position { get; set; }

        public Vector3? Weight { get; set; }

        public byte[] MatrixIndices { get; set; }

        public Vector3? Normals { get; set; }

        public byte[] VertexColours { get; set; }

        /// <summary>
        /// TODO (Knuxfan24): what the hell is this? Some files do have this, has a flag of 0x10.
        /// </summary>
        public byte[] VertexColours2 { get; set; }

        public List<Vector2> TextureCoordinates { get; set; }

        public Vector3? Tangent { get; set; }

        public Vector3? Binormals { get; set; }
    }
}
