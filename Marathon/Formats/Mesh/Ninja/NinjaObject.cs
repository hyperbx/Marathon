namespace Marathon.Formats.Mesh.Ninja
{
    public class NinjaObject
    {
        public Vector3 Center { get; set; }

        public float Radius { get; set; }

        public List<NinjaMaterial> Materials = new();

        public List<NinjaMaterialColours> MaterialColours = new();
        public List<NinjaMaterialLogic> MaterialLogics = new();
        public List<NinjaTextureMap> TextureMaps = new();

        public List<NinjaVertexList> VertexLists = new();

        public List<NinjaPrimitiveList> PrimitiveLists = new();

        public uint MaxNodeDepth { get; set; }

        public List<NinjaNode> Nodes = new();

        public uint MatrixIndexCount { get; set; }

        public List<NinjaSubObject> SubObjects = new();

        public uint TextureCount { get; set; }

        public void Read(BinaryReaderEx reader)
        {
            uint dataOffset = reader.ReadUInt32();

            reader.JumpTo(dataOffset, true);

            Center = reader.ReadVector3();
            Radius = reader.ReadSingle();
            uint MaterialsCount = reader.ReadUInt32();
            uint MaterialsOffset = reader.ReadUInt32();
            uint VertexListCount = reader.ReadUInt32();
            uint VertexListsOffset = reader.ReadUInt32();
            uint PrimitiveListCount = reader.ReadUInt32();
            uint PrimitiveListsOffset = reader.ReadUInt32();
            uint NodeCount = reader.ReadUInt32();
            MaxNodeDepth = reader.ReadUInt32();
            uint NodeListsOffset = reader.ReadUInt32();
            MatrixIndexCount = reader.ReadUInt32();
            uint SubObjectCount = reader.ReadUInt32();
            uint SubObjectsOffset = reader.ReadUInt32();
            TextureCount = reader.ReadUInt32();

            // Materials
            reader.JumpTo(MaterialsOffset, true);
            for (int i = 0; i < MaterialsCount; i++)
            {
                reader.JumpTo(MaterialsOffset + (i * 0x08), true);
                NinjaMaterial Material = new();
                Material.Read(reader);
                Materials.Add(Material);
            }

            // Material Colours Test
            reader.JumpTo(MaterialsOffset, true);
            for (int i = 0; i < MaterialsCount; i++)
            {
                reader.JumpTo(MaterialsOffset + (i * 0x08), true);
                NinjaMaterialColours test = new();
                test.Read(reader);
                MaterialColours.Add(test);
            }
            MaterialColours = MaterialColours.Distinct().ToList();

            // Material Logics Test
            reader.JumpTo(MaterialsOffset, true);
            for (int i = 0; i < MaterialsCount; i++)
            {
                reader.JumpTo(MaterialsOffset + (i * 0x08), true);
                NinjaMaterialLogic test = new();
                test.Read(reader);
                MaterialLogics.Add(test);
            }
            MaterialLogics = MaterialLogics.Distinct().ToList();

            // Material Texture Map Descriptions Test
            reader.JumpTo(MaterialsOffset, true);
            for (int i = 0; i < MaterialsCount; i++)
            {
                reader.JumpTo(MaterialsOffset + (i * 0x08), true);
                NinjaTextureMap test = new();
                test.Read(reader);
                TextureMaps.Add(test);
            }
            TextureMaps = TextureMaps.Distinct().ToList();

            // Vertex Lists
            reader.JumpTo(VertexListsOffset, true);
            for (int i = 0; i < VertexListCount; i++)
            {
                reader.JumpTo(VertexListsOffset + (i * 0x08), true);
                NinjaVertexList VertexList = new();
                VertexList.Read(reader);
                VertexLists.Add(VertexList);
            }

            // Primitive Lists
            reader.JumpTo(PrimitiveListsOffset, true);
            for (int i = 0; i < PrimitiveListCount; i++)
            {
                reader.JumpTo(PrimitiveListsOffset + (i * 0x08), true);
                NinjaPrimitiveList Primitive = new();
                Primitive.Read(reader);
                PrimitiveLists.Add(Primitive);
            }

            // Nodes
            reader.JumpTo(NodeListsOffset, true);
            for (int i = 0; i < NodeCount; i++)
            {
                NinjaNode Node = new();
                Node.Read(reader);
                Nodes.Add(Node);
            }

            // Sub Objects
            reader.JumpTo(SubObjectsOffset, true);
            for (int i = 0; i < SubObjectCount; i++)
            {
                reader.JumpTo(SubObjectsOffset + (i * 0x14), true);
                NinjaSubObject SubObject = new();
                SubObject.Read(reader);
                SubObjects.Add(SubObject);
            }
        }
    
        public void Write(BinaryWriterEx writer)
        {
            Dictionary<string, uint> ObjectOffsets = new();

            // Chunk Header.
            writer.Write("NXOB");
            writer.Write("TEMP");
            long HeaderSizePosition = writer.BaseStream.Position;
            writer.AddOffset("dataOffset");
            writer.FixPadding(0x10);

            // Vertex Table Size and Offset?
            long VertexSizePosition = writer.BaseStream.Position;
            writer.Write("TEMP");
            writer.AddOffset("VertexTable");
            writer.FixPadding(0x10);

            // Nodes
            ObjectOffsets.Add($"NodesPointer", (uint)writer.BaseStream.Position);
            for (int i = 0; i < Nodes.Count; i++)
                Nodes[i].Write(writer);

            // Material Colours
            for (int i = 0; i < MaterialColours.Count; i++)
                MaterialColours[i].Write(writer, i, ObjectOffsets);

            // Material Logics
            for (int i = 0; i < MaterialLogics.Count; i++)
                MaterialLogics[i].Write(writer, i, ObjectOffsets);

            // Material Texture Map Descriptions
            for (int i = 0; i < TextureMaps.Count; i++)
                TextureMaps[i].Write(writer, i, ObjectOffsets);

            // Materials
            for (int i = 0; i < Materials.Count; i++)
                Materials[i].Write(writer, i, ObjectOffsets, MaterialColours, MaterialLogics, TextureMaps);

            // Material List
            ObjectOffsets.Add($"MaterialsPointer", (uint)writer.BaseStream.Position);
            for (int i = 0; i < Materials.Count; i++)
                Materials[i].WritePointer(writer, i, ObjectOffsets);

            // Vertex Lists
            for (int i = 0; i < VertexLists.Count; i++)
                VertexLists[i].Write(writer, i, ObjectOffsets);

            // Vertex List
            ObjectOffsets.Add($"VerticesPointer", (uint)writer.BaseStream.Position);
            for (int i = 0; i < VertexLists.Count; i++)
                VertexLists[i].WritePointer(writer, i, ObjectOffsets);

            // Primitive Strip Indices
            for (int i = 0; i < PrimitiveLists.Count; i++)
            {
                PrimitiveLists[i].WriteStripIndices(writer, i, ObjectOffsets);
                PrimitiveLists[i].WriteIndexIndices(writer, i, ObjectOffsets);
            }    

            // Primitive Lists
            for (int i = 0; i < PrimitiveLists.Count; i++)
                PrimitiveLists[i].Write(writer, i, ObjectOffsets);

            // Primitive List
            ObjectOffsets.Add($"PrimitivesPointer", (uint)writer.BaseStream.Position);
            for (int i = 0; i < PrimitiveLists.Count; i++)
                PrimitiveLists[i].WritePointer(writer, i, ObjectOffsets);

            // Mesh Sets and Texture Indices
            for (int i = 0; i < SubObjects.Count; i++)
            {
                SubObjects[i].WriteMeshSets(writer, i, ObjectOffsets);
                SubObjects[i].WriteTextureIndices(writer, i, ObjectOffsets);
            }

            ObjectOffsets.Add($"SubObjectsPointer", (uint)writer.BaseStream.Position);
            // Sub Objects
            for (int i = 0; i < SubObjects.Count; i++)
                SubObjects[i].Write(writer, i, ObjectOffsets);

            // Main Object
            writer.FillOffset("dataOffset", true);
            writer.Write(Center);
            writer.Write(Radius);
            writer.Write(Materials.Count);
            writer.AddOffset($"MaterialsPointer", 0);
            writer.Write(ObjectOffsets[$"MaterialsPointer"] - writer.Offset);
            writer.Write(VertexLists.Count);
            writer.AddOffset($"VerticesPointer", 0);
            writer.Write(ObjectOffsets[$"VerticesPointer"] - writer.Offset);
            writer.Write(PrimitiveLists.Count);
            writer.AddOffset($"PrimitivesPointer", 0);
            writer.Write(ObjectOffsets[$"PrimitivesPointer"] - writer.Offset);
            writer.Write(Nodes.Count);
            writer.Write(MaxNodeDepth);
            writer.AddOffset($"NodesPointer", 0);
            writer.Write(ObjectOffsets[$"NodesPointer"] - writer.Offset);
            writer.Write(MatrixIndexCount);
            writer.Write(SubObjects.Count);
            writer.AddOffset($"SubObjectsPointer", 0);
            writer.Write(ObjectOffsets[$"SubObjectsPointer"] - writer.Offset);
            writer.Write(TextureCount);

            // Vertices
            writer.FillOffset("VertexTable", true, false);
            long VertexTableStart = writer.BaseStream.Position;
            for (int i = 0; i < VertexLists.Count; i++)
                VertexLists[i].WriteVertexList(writer, i);

            // Alignment.
            uint VertexTableLength = (uint)(writer.BaseStream.Position - VertexTableStart);
            writer.FixPadding(0x10);

            // Chunk Size and Vertex Table Size.
            long ChunkEndPosition = writer.BaseStream.Position;
            uint ChunkSize = (uint)(ChunkEndPosition - HeaderSizePosition);
            writer.BaseStream.Position = HeaderSizePosition - 0x04;
            writer.Write(ChunkSize);

            writer.BaseStream.Position = VertexSizePosition;
            writer.Write(VertexTableLength);
            writer.BaseStream.Position = ChunkEndPosition;
        }
    }
}
