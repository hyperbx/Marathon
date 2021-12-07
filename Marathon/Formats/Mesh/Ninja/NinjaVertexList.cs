namespace Marathon.Formats.Mesh.Ninja
{
    public class NinjaVertex
    {
        public Vector3? Position;

        public Vector3? Weight;

        public byte[] MatrixIndices;

        public Vector3? Normals;

        public byte[] VertexColours;

        public byte[] VertexColours2; //wtf

        public List<Vector2> TextureCoordinates;

        public Vector3? Tangent;

        public Vector3? Binormals;
    }

    public class NinjaVertexList
    {
        public NinjaNext_VertexType Type { get; set; }

        public NinjaNext_XboxVertexType Format { get; set; }

        public NinjaNext_FlexibleVertexFormat FlexibleVertexFormat { get; set; }

        public List<NinjaVertex> Vertices = new();

        public List<int> BoneMatrixIndices = new();

        public uint HDRCommon { get; set; }

        public uint HDRData { get; set; }

        public uint HDRLock { get; set; }

        public void Read(BinaryReaderEx reader)
        {
            Type = (NinjaNext_VertexType)reader.ReadUInt32();
            reader.JumpTo(reader.ReadUInt32(), true);

            Format = (NinjaNext_XboxVertexType)reader.ReadUInt32();
            FlexibleVertexFormat = (NinjaNext_FlexibleVertexFormat)reader.ReadUInt32();
            uint DataSize = reader.ReadUInt32();
            uint VertexCount = reader.ReadUInt32();
            uint VertexListOffset = reader.ReadUInt32();
            uint BoneCount = reader.ReadUInt32();
            uint BoneMatrixIndicesOffset = reader.ReadUInt32();
            HDRCommon = reader.ReadUInt32();
            HDRData = reader.ReadUInt32();
            HDRLock = reader.ReadUInt32();

            reader.JumpTo(VertexListOffset, true);
            for (int i = 0; i < VertexCount; i++)
            {
                NinjaVertex Vertex = new();

                if (Format.HasFlag(NinjaNext_XboxVertexType.NND_VTXTYPE_XB_POSITION))
                    Vertex.Position = reader.ReadVector3();
                if (Format.HasFlag(NinjaNext_XboxVertexType.NND_VTXTYPE_XB_WEIGHT3))
                    Vertex.Weight = reader.ReadVector3();
                if (Format.HasFlag(NinjaNext_XboxVertexType.NND_VTXTYPE_XB_MTX_INDEX4))
                    Vertex.MatrixIndices = reader.ReadBytes(0x04);
                if (Format.HasFlag(NinjaNext_XboxVertexType.NND_VTXTYPE_XB_NORMAL))
                    Vertex.Normals = reader.ReadVector3();
                if (Format.HasFlag(NinjaNext_XboxVertexType.NND_VTXTYPE_XB_COLOR))
                    Vertex.VertexColours = reader.ReadBytes(0x04);
                if (Format.HasFlag(NinjaNext_XboxVertexType.NND_VTXTYPE_XB_COLOR2))
                    Vertex.VertexColours2 = reader.ReadBytes(0x04);

                for (int t = 0; t < ((uint)Format / (uint)NinjaNext_XboxVertexType.NND_VTXTYPE_XB_TEXCOORD); t++)
                {
                    if (Vertex.TextureCoordinates == null)
                        Vertex.TextureCoordinates = new();

                    Vertex.TextureCoordinates.Add(reader.ReadVector2());
                }

                if (Format.HasFlag(NinjaNext_XboxVertexType.NND_VTXTYPE_XB_TANGENT))
                    Vertex.Tangent = reader.ReadVector3();
                if (Format.HasFlag(NinjaNext_XboxVertexType.NND_VTXTYPE_XB_BINORMAL))
                    Vertex.Binormals = reader.ReadVector3();

                Vertices.Add(Vertex);
            }

            reader.JumpTo(BoneMatrixIndicesOffset, true);
            for (int i = 0; i < BoneCount; i++)
                BoneMatrixIndices.Add(reader.ReadInt32());
        }
    
        public void Write(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            uint DataSize = 0;

            if (Format.HasFlag(NinjaNext_XboxVertexType.NND_VTXTYPE_XB_POSITION))
                DataSize += 12;
            if (Format.HasFlag(NinjaNext_XboxVertexType.NND_VTXTYPE_XB_WEIGHT3))
                DataSize += 12;
            if (Format.HasFlag(NinjaNext_XboxVertexType.NND_VTXTYPE_XB_MTX_INDEX4))
                DataSize += 4;
            if (Format.HasFlag(NinjaNext_XboxVertexType.NND_VTXTYPE_XB_NORMAL))
                DataSize += 12;
            if (Format.HasFlag(NinjaNext_XboxVertexType.NND_VTXTYPE_XB_COLOR))
                DataSize += 4;
            if (Format.HasFlag(NinjaNext_XboxVertexType.NND_VTXTYPE_XB_COLOR2))
                DataSize += 4;
            if (Vertices[0].TextureCoordinates != null)
                DataSize += (uint)(8 * Vertices[0].TextureCoordinates.Count);
            if (Format.HasFlag(NinjaNext_XboxVertexType.NND_VTXTYPE_XB_TANGENT))
                DataSize += 12;
            if (Format.HasFlag(NinjaNext_XboxVertexType.NND_VTXTYPE_XB_BINORMAL))
                DataSize += 12;

            ObjectOffsets.Add($"VertexList{index}BoneMatrixIndicesOffset", (uint)writer.BaseStream.Position);
            for (int i = 0; i < BoneMatrixIndices.Count; i++)
                writer.Write(BoneMatrixIndices[i]);

            ObjectOffsets.Add($"VertexList{index}", (uint)writer.BaseStream.Position);
            writer.Write((uint)Format);
            writer.Write((uint)FlexibleVertexFormat);
            writer.Write(DataSize);
            writer.Write(Vertices.Count);
            writer.AddOffset($"VertexList{index}Vertices");
            writer.Write(BoneMatrixIndices.Count);
            if (BoneMatrixIndices.Count != 0)
            {
                writer.AddOffset($"VertexList{index}BoneMatrixIndicesOffset", 0);
                writer.Write(ObjectOffsets[$"VertexList{index}BoneMatrixIndicesOffset"] - writer.Offset);
            }
            else
                writer.Write(0);
            writer.Write(HDRCommon);
            writer.Write(HDRData);
            writer.Write(HDRLock);
            writer.WriteNulls(0x8);
        }

        public void WritePointer(BinaryWriterEx writer, int index, Dictionary<string, uint> ObjectOffsets)
        {
            writer.Write((uint)Type);
            writer.AddOffset($"VertexList{index}", 0);
            writer.Write(ObjectOffsets[$"VertexList{index}"] - writer.Offset);
        }

        public void WriteVertexList(BinaryWriterEx writer, int index)
        {
            writer.FillOffset($"VertexList{index}Vertices", true, false);
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
                    foreach (Vector2 coord in Vertices[i].TextureCoordinates)
                        writer.Write(coord);
                }
                if (Vertices[i].Tangent != null)
                    writer.Write((Vector3)Vertices[i].Tangent);
                if (Vertices[i].Binormals != null)
                    writer.Write((Vector3)Vertices[i].Binormals);
            }
        }
    }
}
