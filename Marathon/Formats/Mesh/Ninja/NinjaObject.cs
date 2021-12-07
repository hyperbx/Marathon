namespace Marathon.Formats.Mesh.Ninja
{
    /// <summary>
    /// Structure of the main Ninja Object.
    /// </summary>
    public class NinjaObject
    {
        public Vector3 Center { get; set; }

        public float Radius { get; set; }

        public List<NinjaMaterial> Materials { get; set; } = new();

        public List<NinjaMaterialColours> MaterialColours { get; set; } = new();

        public List<NinjaMaterialLogic> MaterialLogics { get; set; } = new();

        public List<NinjaTextureMap> TextureMaps { get; set; } = new();

        public List<NinjaVertexList> VertexLists { get; set; } = new();

        public List<NinjaPrimitiveList> PrimitiveLists { get; set; } = new();

        public uint MaxNodeDepth { get; set; }

        public List<NinjaNode> Nodes { get; set; } = new();

        public uint MatrixIndexCount { get; set; }

        public List<NinjaSubObject> SubObjects { get; set; } = new();

        public uint TextureCount { get; set; }

        /// <summary>
        /// Reads the Ninja Object from a file.
        /// </summary>
        /// <param name="reader">The binary reader for this SegaNN file.</param>
        public void Read(BinaryReaderEx reader)
        {
            // Read the offset to the actual Ninja Object.
            uint dataOffset = reader.ReadUInt32();

            // Jump to the actual Ninja Object.
            reader.JumpTo(dataOffset, true);

            // Read all of the data from the Ninja Object.
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

            // Read Materials.
            reader.JumpTo(MaterialsOffset, true);
            for (int i = 0; i < MaterialsCount; i++)
            {
                reader.JumpTo(MaterialsOffset + (i * 8), true);
                NinjaMaterial Material = new();
                Material.Read(reader);
                Materials.Add(Material);
            }

            // Read Material Colours.
            reader.JumpTo(MaterialsOffset, true);
            for (int i = 0; i < MaterialsCount; i++)
            {
                reader.JumpTo(MaterialsOffset + (i * 8), true);
                NinjaMaterialColours MaterialColours = new();
                MaterialColours.Read(reader);
                this.MaterialColours.Add(MaterialColours);
            }

            // Remove any duplicate Material Colours.
            MaterialColours = MaterialColours.Distinct().ToList();

            // Read Material Logics.
            reader.JumpTo(MaterialsOffset, true);
            for (int i = 0; i < MaterialsCount; i++)
            {
                reader.JumpTo(MaterialsOffset + (i * 8), true);
                NinjaMaterialLogic MaterialLogic = new();
                MaterialLogic.Read(reader);
                MaterialLogics.Add(MaterialLogic);
            }

            // Remove any duplicate Material Logic entries.
            MaterialLogics = MaterialLogics.Distinct().ToList();

            // Read material Texture Map descriptions.
            reader.JumpTo(MaterialsOffset, true);
            for (int i = 0; i < MaterialsCount; i++)
            {
                reader.JumpTo(MaterialsOffset + (i * 8), true);
                NinjaTextureMap TextureMap = new();
                TextureMap.Read(reader);
                TextureMaps.Add(TextureMap);
            }

            // Remove any duplicate material Texture Map lists.
            TextureMaps = TextureMaps.Distinct().ToList();

            // Read Vertex Lists.
            reader.JumpTo(VertexListsOffset, true);
            for (int i = 0; i < VertexListCount; i++)
            {
                reader.JumpTo(VertexListsOffset + (i * 8), true);
                NinjaVertexList VertexList = new();
                VertexList.Read(reader);
                VertexLists.Add(VertexList);
            }

            // Read Primitive Lists.
            reader.JumpTo(PrimitiveListsOffset, true);
            for (int i = 0; i < PrimitiveListCount; i++)
            {
                reader.JumpTo(PrimitiveListsOffset + (i * 8), true);
                NinjaPrimitiveList Primitive = new();
                Primitive.Read(reader);
                PrimitiveLists.Add(Primitive);
            }

            // Read Nodes.
            reader.JumpTo(NodeListsOffset, true);
            for (int i = 0; i < NodeCount; i++)
            {
                NinjaNode Node = new();
                Node.Read(reader);
                Nodes.Add(Node);
            }

            // Read Sub Objects.
            reader.JumpTo(SubObjectsOffset, true);
            for (int i = 0; i < SubObjectCount; i++)
            {
                reader.JumpTo(SubObjectsOffset + (i * 0x14), true);
                NinjaSubObject SubObject = new();
                SubObject.Read(reader);
                SubObjects.Add(SubObject);
            }
        }

        /// <summary>
        /// Write the Ninja Object to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        public void Write(BinaryWriterEx writer)
        {
            // Set up a list of Offsets for earlier points in the chunk.
            Dictionary<string, uint> ObjectOffsets = new();

            // Write NXOB header.
            writer.Write("NXOB");
            writer.Write("SIZE"); // Temporary entry, is filled in later once we know this chunk's size.
            long HeaderSizePosition = writer.BaseStream.Position;
            writer.AddOffset("dataOffset");
            writer.FixPadding(0x10);

            // Vertex Table Size and Offset?
            long VertexSizePosition = writer.BaseStream.Position;
            writer.Write("SIZE"); // Temporary entry, is filled in later once we know this chunk's size.
            writer.AddOffset("VertexTable");
            writer.FixPadding(0x10);

            // Write Nodes.
            ObjectOffsets.Add($"NodesPointer", (uint)writer.BaseStream.Position);
            for (int i = 0; i < Nodes.Count; i++)
                Nodes[i].Write(writer);

            // Write Material Colours.
            for (int i = 0; i < MaterialColours.Count; i++)
                MaterialColours[i].Write(writer, i, ObjectOffsets);

            // Write Material Logics.
            for (int i = 0; i < MaterialLogics.Count; i++)
                MaterialLogics[i].Write(writer, i, ObjectOffsets);

            // Write material Texture Map descriptions.
            for (int i = 0; i < TextureMaps.Count; i++)
                TextureMaps[i].Write(writer, i, ObjectOffsets);

            // Write Materials.
            for (int i = 0; i < Materials.Count; i++)
                Materials[i].Write(writer, i, ObjectOffsets, MaterialColours, MaterialLogics, TextureMaps);

            // Write material list.
            ObjectOffsets.Add($"MaterialsPointer", (uint)writer.BaseStream.Position);
            for (int i = 0; i < Materials.Count; i++)
                Materials[i].WritePointer(writer, i, ObjectOffsets);

            // Write Vertex Lists.
            for (int i = 0; i < VertexLists.Count; i++)
                VertexLists[i].Write(writer, i, ObjectOffsets);

            // Write vertex list.
            ObjectOffsets.Add($"VerticesPointer", (uint)writer.BaseStream.Position);
            for (int i = 0; i < VertexLists.Count; i++)
                VertexLists[i].WritePointer(writer, i, ObjectOffsets);

            // Write Primitive Strip and Index Indices.
            for (int i = 0; i < PrimitiveLists.Count; i++)
            {
                PrimitiveLists[i].WriteStripIndices(writer, i, ObjectOffsets);
                PrimitiveLists[i].WriteIndexIndices(writer, i, ObjectOffsets);
            }

            // Write Primitive Lists.
            for (int i = 0; i < PrimitiveLists.Count; i++)
                PrimitiveLists[i].Write(writer, i, ObjectOffsets);

            // Write Primitive List.
            ObjectOffsets.Add($"PrimitivesPointer", (uint)writer.BaseStream.Position);
            for (int i = 0; i < PrimitiveLists.Count; i++)
                PrimitiveLists[i].WritePointer(writer, i, ObjectOffsets);

            // Write Mesh Sets and Texture Indices.
            for (int i = 0; i < SubObjects.Count; i++)
            {
                SubObjects[i].WriteMeshSets(writer, i, ObjectOffsets);
                SubObjects[i].WriteTextureIndices(writer, i, ObjectOffsets);
            }

            // Write Sub Objects.
            ObjectOffsets.Add($"SubObjectsPointer", (uint)writer.BaseStream.Position);
            for (int i = 0; i < SubObjects.Count; i++)
                SubObjects[i].Write(writer, i, ObjectOffsets);

            // Write main object.
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

            // Write Vertices.
            writer.FillOffset("VertexTable", true, false);
            long VertexTableStart = writer.BaseStream.Position;
            for (int i = 0; i < VertexLists.Count; i++)
                VertexLists[i].WriteVertices(writer, i);

            // Alignment.
            uint VertexTableLength = (uint)(writer.BaseStream.Position - VertexTableStart);
            writer.FixPadding(0x10);

            // Write chunk size and Vertex Table size.
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
