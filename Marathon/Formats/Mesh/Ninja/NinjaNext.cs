using Assimp;
using Assimp.Configs;
using NvTriStripDotNet;

namespace Marathon.Formats.Mesh.Ninja
{
    public class NinjaNext : FileBase
    {
        public NinjaNext() { }

        public NinjaNext(string file, bool serialise = false)
        {
            Load(file);

            if (serialise)
                JsonSerialise(Data);
        }



        public override string Signature { get; } = "NXIF";

        public class FormatData
        {
            public NinjaTextureList TextureList { get; set; }

            public NinjaEffectList EffectList { get; set; }

            public NinjaNodeNameList NodeNameList { get; set; }

            public NinjaObject Object { get; set; }

            public NinjaLight Light { get; set; }

            public NinjaCamera Camera { get; set; }

            public NinjaMotion Motion { get; set; }
        }

        public FormatData Data { get; set; } = new();

        public override void Load(Stream stream)
        {
            BinaryReaderEx reader = new(stream);

            // NXIF Chunk.
            reader.ReadSignature(4, Signature);
            uint chunkSize = reader.ReadUInt32();
            uint dataChunkCount = reader.ReadUInt32();
            uint dataOffset = reader.ReadUInt32();
            uint dataSize = reader.ReadUInt32();
            uint NOF0Offset = reader.ReadUInt32();
            uint NOF0Size = reader.ReadUInt32();
            uint version = reader.ReadUInt32();

            // Set the reader's offset to the value set in the NXIF.
            reader.Offset = dataOffset;

            // Data Chunks
            for (int i = 0; i < dataChunkCount; i++)
            {
                // Read the chunk's ID and size.
                string chunkID = new(reader.ReadChars(0x04));
                chunkSize = reader.ReadUInt32();

                // Calculate where the next chunk begins so we can jump to it.
                long targetPosition = reader.BaseStream.Position + chunkSize;

                // Run the approriate read function for the chunk.
                // If this chunk isn't currently handled, throw an exception.
                switch (chunkID)
                {
                    case "NXTL":
                        Data.TextureList = new();
                        Data.TextureList.Read(reader);
                        break;
                    case "NXEF":
                        Data.EffectList = new();
                        Data.EffectList.Read(reader);
                        break;
                    case "NXNN":
                        Data.NodeNameList = new();
                        Data.NodeNameList.Read(reader);
                        break;
                    case "NXOB":
                        Data.Object = new();
                        Data.Object.Read(reader);
                        break;
                    case "NXLI":
                        Data.Light = new();
                        Data.Light.Read(reader);
                        break;
                    case "NXCA":
                        Data.Camera = new();
                        Data.Camera.Read(reader);
                        break;
                    case "NXMA":
                    case "NXMC":
                    case "NXML":
                    case "NXMM":
                    case "NXMO":
                        Data.Motion = new();
                        Data.Motion.ChunkID = chunkID;
                        Data.Motion.Read(reader);
                        break;
                    default:
                        throw new NotImplementedException();
                }

                // Jump to the position of the next chunk to make sure the reader's in the right place.
                reader.JumpTo(targetPosition);
            }

            // If this SegaNN file has both an Object Chunk and a Node Name List then fill in the Nodes with their names.
            if (Data.Object != null && Data.NodeNameList != null)
            {
                for (int i = 0; i < Data.Object.Nodes.Count; i++)
                {
                    Data.Object.Nodes[i].Name = Data.NodeNameList.NinjaNodeNames[i];
                }
            }
        }

        public override void Save(Stream stream)
        {
            BinaryWriterEx writer = new(stream);
            writer.Offset = 0x20;

            // Get amount of Data Chunks.
            uint dataChunkCount = 0;
            if (Data.TextureList != null)
                dataChunkCount++;
            if (Data.EffectList != null)
                dataChunkCount++;
            if (Data.NodeNameList != null)
                dataChunkCount++;
            if (Data.Object != null)
                dataChunkCount++;
            if (Data.Light != null)
                dataChunkCount++;
            if (Data.Camera != null)
                dataChunkCount++;
            if (Data.Motion != null)
                dataChunkCount++;

            // NXIF Chunk.
            writer.Write(Signature);
            writer.Write(0x18);
            writer.Write(dataChunkCount);
            writer.Write(0x20);
            long DataSizePosition = writer.BaseStream.Position;
            writer.Write("SIZE"); // Temporary value, filled in once we know how long the data is.
            writer.Write("NOF0"); // Temporary value, filled in once we know where the NOF0 chunk is.
            writer.Write("SIZE"); // Temporary value, filled in once we know how long the NOF0 chunk is.
            writer.Write(0x01);

            // Write the chunks we have loaded.
            if (Data.TextureList != null)
                Data.TextureList.Write(writer);
            if (Data.EffectList != null)
                Data.EffectList.Write(writer);
            if (Data.NodeNameList != null)
                Data.NodeNameList.Write(writer);
            if (Data.Object != null)
                Data.Object.Write(writer);
            if (Data.Light != null)
                Data.Light.Write(writer);
            if (Data.Camera != null)
                Data.Camera.Write(writer);
            if (Data.Motion != null)
                Data.Motion.Write(writer);

            // Calculate and write the position of the NOF0 chunk.
            uint NOF0Offset = (uint)writer.BaseStream.Position;
            writer.BaseStream.Position = DataSizePosition;
            writer.Write(NOF0Offset - 0x20);
            writer.Write(NOF0Offset);
            writer.BaseStream.Position = NOF0Offset;

            // NOF0 Chunk
            writer.Write("NOF0");
            writer.Write("SIZE"); // Temporary value, filled in once we know how long the data is.
            long N0F0SizePosition = writer.BaseStream.Position;

            // Get the stored offsets from the Writer and sort them in order.
            List<uint> Offsets = writer.GetOffsets();
            Offsets.Sort();

            // Write the count of Offsets and finish the header.
            writer.Write(Offsets.Count);
            writer.FixPadding(0x10);

            // Write each offset, taking the writer's offset into account.
            for(int i = 0; i < Offsets.Count; i++)
                writer.Write(Offsets[i] - 0x20);

            // Alignment.
            writer.FixPadding(0x10);

            // Calculate and write the size of the NOF0 chunk.
            long NOF0EndPosition = writer.BaseStream.Position;
            uint NOF0Size = (uint)(NOF0EndPosition - N0F0SizePosition);
            writer.BaseStream.Position = N0F0SizePosition - 0x04;
            writer.Write(NOF0Size);
            writer.BaseStream.Position = DataSizePosition + 0x08;
            writer.Write(NOF0Size + 0x08);
            writer.BaseStream.Position = NOF0EndPosition;

            // NFN0 Chunk
            writer.Write("NFN0");
            writer.Write("SIZE"); // Temporary value, filled in once we know how long the data is.
            long NFN0SizePosition = writer.BaseStream.Position;
            writer.FixPadding(0x10);

            // Get the stream's file name and write it, aligned to 0x10 bytes.
            writer.WriteNullTerminatedString(Path.GetFileName((stream as FileStream).Name));
            writer.FixPadding(0x10);

            // Calculate and write the size of the NFN0 chunk.
            long NFN0EndPosition = writer.BaseStream.Position;
            uint NFN0Size = (uint)(NFN0EndPosition - NFN0SizePosition);
            writer.BaseStream.Position = NFN0SizePosition - 0x04;
            writer.Write(NFN0Size);
            writer.BaseStream.Position = NFN0EndPosition;

            // NEND Chunk
            writer.Write("NEND");
            writer.Write(0x08);
            writer.FixPadding(0x10);
        }

        public class AssimpData
        {
            public string NodeName;

            public Assimp.Matrix4x4 Transform;

            public short ParentBoneID = -1;
            public short ChildBoneID = -1;
            public short SiblingBoneID = -1;

            public uint Type = 0;

            public override string ToString() => NodeName;
        }

        public void ImportAssimp(string filePath)
        {
            List<AssimpData> Nodes = new();

            // Setup AssimpNet Scene.
            Console.WriteLine($"Loading {filePath} into Assimp.");
            AssimpContext assimpImporter = new();
            KeepSceneHierarchyConfig config = new(true);
            assimpImporter.SetConfig(config);
            Scene assimpModel = assimpImporter.ImportFile(filePath, PostProcessSteps.GenerateBoundingBoxes);

            // Get Nodes (doesn't do the mesh nodes which is a bit of a problem).
            void Traverse(Node node, string caller = null)
            {
                if (node.HasMeshes == false && !node.Name.Contains("$Assimp") && node.Name != "RootNode")
                {
                    AssimpData data = new()
                    {
                        NodeName = node.Name,
                        Transform = node.Transform,
                    };

                    for (int i = 0; i < Nodes.Count; i++)
                        if (Nodes[i].NodeName == caller)
                            data.ParentBoneID = (short)i;

                    if (node.HasChildren)
                        data.ChildBoneID = (short)(Nodes.Count + 1);

                    Nodes.Add(data);
                }

                for (int i = 0; i < node.Children.Count; i++)
                    Traverse(node.Children[i], node.Name);
            }

            Traverse(assimpModel.RootNode);

            for (int i = 0; i < assimpModel.Meshes.Count; i++)
            {
                AssimpData data = new();
                data.NodeName = assimpModel.Meshes[i].Name;
                data.ParentBoneID = 0;
                data.Type = 1;
                data.Transform = new(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
                Nodes.Add(data);
            }

            // Figure out Sibling Indices.
            for (int i = Nodes.Count - 1; i >= 0; i--)
            {
                var parent = Nodes[i].ParentBoneID;
                for (int b = i - 1; b >= 0; b--)
                {
                    if (Nodes[b].ParentBoneID == parent)
                    {
                        Nodes[i].SiblingBoneID = (short)b;
                        break;
                    }
                }

            }

            // Texture List.
            Console.WriteLine("Importing textures.");
            Data.TextureList = new();
            for (int i = 0; i < assimpModel.Materials.Count; i++)
            {
                NinjaTextureFile texture = new();
                texture.MinFilter = NinjaNext_MinFilter.NND_MIN_LINEAR_MIPMAP_NEAREST;
                texture.MagFilter = NinjaNext_MagFilter.NND_MAG_LINEAR;

                if (assimpModel.Materials[i].TextureAmbient.FilePath != null)
                    texture.FileName = Path.GetFileName(assimpModel.Materials[i].TextureAmbient.FilePath);

                if (assimpModel.Materials[i].TextureDiffuse.FilePath != null)
                    texture.FileName = Path.GetFileName(assimpModel.Materials[i].TextureDiffuse.FilePath);

                if (assimpModel.Materials[i].TextureDisplacement.FilePath != null)
                    texture.FileName = Path.GetFileName(assimpModel.Materials[i].TextureDisplacement.FilePath);

                if (assimpModel.Materials[i].TextureEmissive.FilePath != null)
                    texture.FileName = Path.GetFileName(assimpModel.Materials[i].TextureEmissive.FilePath);

                if (assimpModel.Materials[i].TextureHeight.FilePath != null)
                    texture.FileName = Path.GetFileName(assimpModel.Materials[i].TextureHeight.FilePath);

                if (assimpModel.Materials[i].TextureLightMap.FilePath != null)
                    texture.FileName = Path.GetFileName(assimpModel.Materials[i].TextureLightMap.FilePath);

                if (assimpModel.Materials[i].TextureNormal.FilePath != null)
                    texture.FileName = Path.GetFileName(assimpModel.Materials[i].TextureNormal.FilePath);

                if (assimpModel.Materials[i].TextureOpacity.FilePath != null)
                    texture.FileName = Path.GetFileName(assimpModel.Materials[i].TextureOpacity.FilePath);

                if (assimpModel.Materials[i].TextureReflection.FilePath != null)
                    texture.FileName = Path.GetFileName(assimpModel.Materials[i].TextureReflection.FilePath);

                if (assimpModel.Materials[i].TextureSpecular.FilePath != null)
                    texture.FileName = Path.GetFileName(assimpModel.Materials[i].TextureSpecular.FilePath);

                if (texture.FileName != null)
                    Data.TextureList.NinjaTextureFiles.Add(texture);
            }

            // Effect List.
            Console.WriteLine("Creating effect node.");
            Data.EffectList = new();
            for (int i = 0; i < assimpModel.Materials.Count; i++)
            {
                NinjaEffectFile effectFile = new();
                effectFile.FileName = "Billboard03.fx";

                NinjaTechniqueName techniqueName = new();
                techniqueName.Name = "Billboard03";

                Data.EffectList.NinjaEffectFiles.Add(effectFile);
                Data.EffectList.NinjaTechniqueNames.Add(techniqueName);
                Data.EffectList.NinjaTechniqueIndices.Add((short)i);
            }

            // Node Name List.
            Console.WriteLine("Creating node name list.");
            Data.NodeNameList = new();
            for(int i = 0; i < Nodes.Count; i++)
                Data.NodeNameList.NinjaNodeNames.Add(Nodes[i].NodeName);

            // Object.
            Console.WriteLine("Creating object.");
            Data.Object = new();

            Data.Object.Center = new Vector3((assimpModel.Meshes[0].BoundingBox.Min.X + assimpModel.Meshes[0].BoundingBox.Max.X) / 2f, (assimpModel.Meshes[0].BoundingBox.Min.Y + assimpModel.Meshes[0].BoundingBox.Max.Y) / 2f, (assimpModel.Meshes[0].BoundingBox.Min.Z + assimpModel.Meshes[0].BoundingBox.Max.Z) / 2f);
            float maxSize = assimpModel.Meshes[0].BoundingBox.Max.X - assimpModel.Meshes[0].BoundingBox.Min.X;
            float tempSize = assimpModel.Meshes[0].BoundingBox.Max.Y - assimpModel.Meshes[0].BoundingBox.Min.Y;
            if (tempSize > maxSize)
            {
                maxSize = tempSize;
            }
            tempSize = assimpModel.Meshes[0].BoundingBox.Max.Z - assimpModel.Meshes[0].BoundingBox.Min.Z;
            if (tempSize > maxSize)
            {
                maxSize = tempSize;
            }
            Data.Object.Radius = maxSize;

            // Materials
            for (int i = 0; i < assimpModel.Materials.Count; i++)
            {
                NinjaMaterial material = new();
                material.Type = NinjaNext_MaterialType.NND_MATTYPE_TEXTURE | NinjaNext_MaterialType.NND_MATTYPE_TEXMATTYPE2;
                material.Flag = (NinjaNext_MaterialType)0x01000030;
                material.MaterialColourOffset = (uint)i;
                material.MaterialLogicOffset = (uint)i;
                material.MaterialTexMapDescriptionOffset = (uint)i;

                NinjaMaterialColours colours = new();
                colours.Diffuse = new(assimpModel.Materials[i].ColorDiffuse.R, assimpModel.Materials[i].ColorDiffuse.G, assimpModel.Materials[i].ColorDiffuse.B, assimpModel.Materials[i].ColorDiffuse.A);
                colours.Ambient = new(assimpModel.Materials[i].ColorAmbient.R, assimpModel.Materials[i].ColorAmbient.G, assimpModel.Materials[i].ColorAmbient.B, assimpModel.Materials[i].ColorAmbient.A);
                colours.Specular = new(assimpModel.Materials[i].ColorSpecular.R, assimpModel.Materials[i].ColorSpecular.G, assimpModel.Materials[i].ColorSpecular.B, assimpModel.Materials[i].ColorSpecular.A);
                colours.Emissive = new(assimpModel.Materials[i].ColorEmissive.R, assimpModel.Materials[i].ColorEmissive.G, assimpModel.Materials[i].ColorEmissive.B, assimpModel.Materials[i].ColorEmissive.A);
                colours.Power = 8f;
                colours.Offset = (uint)i;

                NinjaMaterialLogic logic = new();
                logic.Blend = true;
                logic.SRCBlend = NinjaNext_BlendMode.NNE_BLENDMODE_SRCALPHA;
                logic.DSTBlend = NinjaNext_BlendMode.NNE_BLENDMODE_INVSRCALPHA;
                logic.BlendOperation = NinjaNext_BlendOperation.NNE_BLENDOP_ADD;
                logic.LogicOperation = NinjaNext_LogicOperation.NNE_LOGICOP_NONE;
                logic.Alpha = true;
                logic.AlphaFunction = NinjaNext_CMPFunction.NNE_CMPFUNC_GREATER;
                logic.ZComparison = true;
                logic.ZComparisonFunction = NinjaNext_CMPFunction.NNE_CMPFUNC_LESSEQUAL;
                logic.ZUpdate = true;
                logic.Offset = (uint)i;

                NinjaTextureMap texture = new();
                texture.Offset = (uint)i;
                NinjaMaterialTextureMapDescription description = new NinjaMaterialTextureMapDescription();
                description.Blend = 1f;
                description.Index = i;
                description.MinFilter = NinjaNext_MinFilter.NND_MIN_LINEAR_MIPMAP_NEAREST;
                description.MagFilter = NinjaNext_MagFilter.NND_MAG_LINEAR;
                description.Type = 0x400c0101;
                texture.NinjaTextureMapDescriptions.Add(description);

                Data.Object.Materials.Add(material);
                Data.Object.MaterialColours.Add(colours);
                Data.Object.MaterialLogics.Add(logic);
                Data.Object.TextureMaps.Add(texture);
            }
            Data.Object.TextureCount = (uint)assimpModel.Materials.Count;

            // Vertex Lists
            Console.WriteLine("Creating verticies.");
            for (int i = 0; i < assimpModel.Meshes.Count; i++)
            {
                NinjaVertexList vertexList = new();
                vertexList.Type = NinjaNext_VertexType.NND_VTXTYPE_DX_VERTEXDESC;
                vertexList.Format = NinjaNext_XboxVertexType.NND_VTXTYPE_XB_PW4INCT;
                vertexList.FlexibleVertexFormat = NinjaNext_FlexibleVertexFormat.NND_D3DFVF_XYZB3 | NinjaNext_FlexibleVertexFormat.NND_D3DFVF_NORMAL | NinjaNext_FlexibleVertexFormat.NND_D3DFVF_DIFFUSE | NinjaNext_FlexibleVertexFormat.NND_D3DFVF_TEX1 | NinjaNext_FlexibleVertexFormat.NND_D3DFVF_LASTBETA_UBYTE4;
            
                for (int v = 0; v < assimpModel.Meshes[i].Vertices.Count; v++)
                {
                    NinjaVertex vertex = new();
                    vertex.Position = new(assimpModel.Meshes[i].Vertices[v].X, assimpModel.Meshes[i].Vertices[v].Y, assimpModel.Meshes[i].Vertices[v].Z);
                    vertex.Normals = new(assimpModel.Meshes[i].Normals[v].X, assimpModel.Meshes[i].Normals[v].Y, assimpModel.Meshes[i].Normals[v].Z);

                    if (assimpModel.Meshes[i].TextureCoordinateChannelCount != 0)
                    {
                        vertex.TextureCoordinates = new();
                        vertex.TextureCoordinates.Add(new(assimpModel.Meshes[i].TextureCoordinateChannels[0][v].X, -assimpModel.Meshes[i].TextureCoordinateChannels[0][v].Y));
                    }

                    vertex.VertexColours = new byte[4];
                    if (assimpModel.Meshes[i].VertexColorChannelCount != 0)
                    {
                        vertex.VertexColours[0] = (byte)(assimpModel.Meshes[i].VertexColorChannels[0][v].B * 255);
                        vertex.VertexColours[1] = (byte)(assimpModel.Meshes[i].VertexColorChannels[0][v].G * 255);
                        vertex.VertexColours[2] = (byte)(assimpModel.Meshes[i].VertexColorChannels[0][v].R * 255);
                        vertex.VertexColours[3] = (byte)(assimpModel.Meshes[i].VertexColorChannels[0][v].A * 255);
                    }
                    else
                    {
                        vertex.VertexColours[0] = 255;
                        vertex.VertexColours[1] = 255;
                        vertex.VertexColours[2] = 255;
                        vertex.VertexColours[3] = 255;
                    }

                    for (int i1 = 0; i1 < assimpModel.Meshes[i].Bones.Count; i1++)
                    {
                        Bone bone = assimpModel.Meshes[i].Bones[i1];
                        for (int b = 0; b < bone.VertexWeights.Count; b++)
                        {
                            if (bone.VertexWeights[b].VertexID == v)
                            {
                                if (vertex.MatrixIndices == null)
                                {
                                    vertex.MatrixIndices = new byte[4];
                                    vertex.MatrixIndices[0] = 0xff;
                                    vertex.MatrixIndices[1] = 0xff;
                                    vertex.MatrixIndices[2] = 0xff;
                                    vertex.MatrixIndices[3] = 0xff;
                                }
                                if (vertex.Weight == null)
                                    vertex.Weight = new();

                                if (vertex.MatrixIndices[0] == 0xff)
                                {
                                    vertex.MatrixIndices[0] = (byte)bone.VertexWeights[b].VertexID;
                                    var currentWeight = vertex.Weight.GetValueOrDefault();
                                    vertex.Weight = new(bone.VertexWeights[b].Weight, currentWeight.Y, currentWeight.Z);
                                }
                                else if (vertex.MatrixIndices[1] == 0xff)
                                {
                                    vertex.MatrixIndices[1] = (byte)bone.VertexWeights[b].VertexID;
                                    var currentWeight = vertex.Weight.GetValueOrDefault();
                                    vertex.Weight = new(currentWeight.X, bone.VertexWeights[b].Weight, currentWeight.Z);
                                }
                                else if (vertex.MatrixIndices[2] == 0xff)
                                {
                                    vertex.MatrixIndices[2] = (byte)bone.VertexWeights[b].VertexID;
                                    var currentWeight = vertex.Weight.GetValueOrDefault();
                                    vertex.Weight = new(currentWeight.X, currentWeight.Y, bone.VertexWeights[b].Weight);
                                }
                                else if (vertex.MatrixIndices[3] == 0xff)
                                    vertex.MatrixIndices[3] = (byte)bone.VertexWeights[b].VertexID;

                                if (!vertexList.BoneMatrixIndices.Contains(i1))
                                    vertexList.BoneMatrixIndices.Add(i1);
                            }
                        }
                    }

                    if (vertex.MatrixIndices != null)
                    {
                        if (vertex.MatrixIndices[0] == 0xff)
                            vertex.MatrixIndices[0] = 0;
                        if (vertex.MatrixIndices[1] == 0xff)
                            vertex.MatrixIndices[1] = 0;
                        if (vertex.MatrixIndices[2] == 0xff)
                            vertex.MatrixIndices[2] = 0;
                        if (vertex.MatrixIndices[3] == 0xff)
                            vertex.MatrixIndices[3] = 0;
                    }

                    vertexList.Vertices.Add(vertex);
                }

                Data.Object.VertexLists.Add(vertexList);
            }

            // Primitives
            Console.WriteLine("Creating triangle strip primitives.");
            for (int i = 0; i < assimpModel.Meshes.Count; i++)
            {
                // Get Faces.
                List<ushort> Faces = new List<ushort>();
                foreach (var Face in assimpModel.Meshes[i].Faces)
                {
                    Faces.Add((ushort)Face.Indices[0]);
                    Faces.Add((ushort)Face.Indices[1]);
                    Faces.Add((ushort)Face.Indices[2]);
                }

                NvStripifier nvStripifier = new NvStripifier();
                nvStripifier.GenerateStrips(Faces.ToArray(), out PrimitiveGroup[] stripList);

                NinjaPrimitiveList primitiveList = new();
                primitiveList.Type = NinjaNext_PrimitiveType.NND_PRIMTYPE_DX_STRIPLIST;
                primitiveList.Format = 0x00004810;
                primitiveList.StripIndices.Add((ushort)stripList[0].IndexCount);
                foreach(ushort strip in stripList[0].Indices)
                {
                    primitiveList.IndexIndices.Add(strip);
                }

                Data.Object.PrimitiveLists.Add(primitiveList);
            }

            // Nodes.
            Console.WriteLine("Creating nodes.");
            for (int i = 0; i < Nodes.Count; i++)
            {
                NinjaNode node = new();
                node.Name = Nodes[i].NodeName;
                node.MatrixIndex = (short)i;
                node.ParentIndex = Nodes[i].ParentBoneID;
                node.ChildIndex = Nodes[i].ChildBoneID;
                node.SiblingIndex = Nodes[i].SiblingBoneID;
                node.Scaling = new(1, 1, 1);
                if (Nodes[i].Type == 0)
                    node.Type = NinjaNext_NodeType.NND_NODETYPE_UNIT_TRANSLATION | NinjaNext_NodeType.NND_NODETYPE_UNIT_ROTATION | NinjaNext_NodeType.NND_NODETYPE_UNIT_SCALING | NinjaNext_NodeType.NND_NODETYPE_UNIT_INIT_MATRIX | NinjaNext_NodeType.NND_NODETYPE_UNIT33_INIT_MATRIX | NinjaNext_NodeType.NND_NODETYPE_ORTHO33_INIT_MATRIX | NinjaNext_NodeType.NND_NODETYPE_ROTATE_TYPE_XZY;
                else
                    node.Type = NinjaNext_NodeType.NND_NODETYPE_UNIT_TRANSLATION | NinjaNext_NodeType.NND_NODETYPE_UNIT_ROTATION | NinjaNext_NodeType.NND_NODETYPE_UNIT_SCALING | NinjaNext_NodeType.NND_NODETYPE_UNIT_INIT_MATRIX | NinjaNext_NodeType.NND_NODETYPE_UNIT33_INIT_MATRIX | NinjaNext_NodeType.NND_NODETYPE_ORTHO33_INIT_MATRIX | NinjaNext_NodeType.NND_NODETYPE_ROTATE_TYPE_XZY | NinjaNext_NodeType.NND_NODETYPE_BBOX_DATA;

                node.InvInitMatrix = new float[16];
                node.InvInitMatrix[0] = Nodes[i].Transform.A1;
                node.InvInitMatrix[1] = Nodes[i].Transform.A2;
                node.InvInitMatrix[2] = Nodes[i].Transform.A3;
                node.InvInitMatrix[3] = Nodes[i].Transform.A4;
                node.InvInitMatrix[4] = Nodes[i].Transform.B1;
                node.InvInitMatrix[5] = Nodes[i].Transform.B2;
                node.InvInitMatrix[6] = Nodes[i].Transform.B3;
                node.InvInitMatrix[7] = Nodes[i].Transform.B4;
                node.InvInitMatrix[8] = Nodes[i].Transform.C1;
                node.InvInitMatrix[9] = Nodes[i].Transform.C2;
                node.InvInitMatrix[10] = Nodes[i].Transform.C3;
                node.InvInitMatrix[11] = Nodes[i].Transform.C4;
                node.InvInitMatrix[12] = Nodes[i].Transform.D1;
                node.InvInitMatrix[13] = Nodes[i].Transform.D2;
                node.InvInitMatrix[14] = Nodes[i].Transform.D3;
                node.InvInitMatrix[15] = Nodes[i].Transform.D4;

                Data.Object.Nodes.Add(node);
            }

            // Sub Objects
            Console.WriteLine("Creating subobjects.");
            NinjaSubObject subObject = new();
            subObject.Type = 513;

            for (int i = 0; i < assimpModel.Meshes.Count; i++)
            {
                NinjaMeshSet meshSet = new();
                meshSet.MaterialIndex = i;
                meshSet.PrimitiveListIndex = i;
                meshSet.ShaderIndex = i;
                meshSet.VertexListIndex = i;
                subObject.MeshSets.Add(meshSet);
                subObject.TextureIndices.Add(i);
            }

            Data.Object.SubObjects.Add(subObject);
        }

        public void ImportAssimpAnim(string filePath, NinjaNext model)
        {
            // Setup AssimpNet Scene.
            Console.WriteLine($"Loading {filePath} into Assimp.");
            AssimpContext assimpImporter = new();
            KeepSceneHierarchyConfig config = new(true);
            assimpImporter.SetConfig(config);
            Scene assimpModel = assimpImporter.ImportFile(filePath, PostProcessSteps.GenerateSmoothNormals | PostProcessSteps.CalculateTangentSpace);

            Data.Motion = new();
            Data.Motion.ChunkID = "NXMO";
            Data.Motion.Type = NinjaNext_MotionType.NND_MOTIONTYPE_NODE | NinjaNext_MotionType.NND_MOTIONTYPE_REPEAT | NinjaNext_MotionType.NND_MOTIONTYPE_VERSION2;
            Data.Motion.StartFrame = 0;
            Data.Motion.EndFrame = (float)assimpModel.Animations[0].DurationInTicks;
            Data.Motion.Framerate = (float)assimpModel.Animations[0].TicksPerSecond;

            foreach (Animation animation in assimpModel.Animations)
            {
                foreach (NodeAnimationChannel nodeChannel in animation.NodeAnimationChannels)
                {
                    if (model.Data.NodeNameList != null)
                    {
                        for (int i = 0; i < model.Data.NodeNameList.NinjaNodeNames.Count; i++)
                        {
                            if (model.Data.NodeNameList.NinjaNodeNames[i] == nodeChannel.NodeName)
                            {
                                if (nodeChannel.HasPositionKeys)
                                {
                                    NinjaSubMotion positionSubMotion = new();
                                    positionSubMotion.Type = NinjaNext_SubMotionType.NND_SMOTTYPE_FRAME_FLOAT | NinjaNext_SubMotionType.NND_SMOTTYPE_TRANSLATION_MASK;
                                    positionSubMotion.InterpolationType = NinjaNext_SubMotionInterpolationType.NND_SMOTIPTYPE_LINEAR | NinjaNext_SubMotionInterpolationType.NND_SMOTIPTYPE_REPEAT;
                                    positionSubMotion.EndFrame = (float)assimpModel.Animations[0].DurationInTicks;
                                    positionSubMotion.EndKeyframe = (float)assimpModel.Animations[0].DurationInTicks;
                                    positionSubMotion.NodeIndex = i;

                                    for (int a = 0; a < nodeChannel.PositionKeys.Count; a++)
                                    {
                                        NinjaKeyframe.NNS_MOTION_KEY_VECTOR position = new();
                                        position.Frame = (float)nodeChannel.PositionKeys[a].Time;
                                        position.Value = new(nodeChannel.PositionKeys[a].Value.X, nodeChannel.PositionKeys[a].Value.Y, nodeChannel.PositionKeys[a].Value.Z);
                                        positionSubMotion.Keyframes.Add(position);
                                    }

                                    Data.Motion.SubMotions.Add(positionSubMotion);
                                }

                                if (nodeChannel.HasRotationKeys)
                                {
                                    NinjaSubMotion rotationSubMotion = new();
                                    rotationSubMotion.Type = NinjaNext_SubMotionType.NND_SMOTTYPE_FRAME_SINT16 | NinjaNext_SubMotionType.NND_SMOTTYPE_ANGLE_ANGLE16 | NinjaNext_SubMotionType.NND_SMOTTYPE_ROTATION_XYZ;
                                    rotationSubMotion.InterpolationType = NinjaNext_SubMotionInterpolationType.NND_SMOTIPTYPE_LINEAR | NinjaNext_SubMotionInterpolationType.NND_SMOTIPTYPE_REPEAT;
                                    rotationSubMotion.EndFrame = (float)assimpModel.Animations[0].DurationInTicks;
                                    rotationSubMotion.EndKeyframe = (float)assimpModel.Animations[0].DurationInTicks;
                                    rotationSubMotion.NodeIndex = i;

                                    for (int a = 0; a < nodeChannel.RotationKeys.Count; a++)
                                    {
                                        // TODO: Assimp uses Quats, so I THINK I need to do this conversion to Euler?
                                        float sinr_cosp = 2 * (nodeChannel.RotationKeys[a].Value.W * nodeChannel.RotationKeys[a].Value.X + nodeChannel.RotationKeys[a].Value.Y * nodeChannel.RotationKeys[a].Value.Z);
                                        float cosr_cosp = 1 - 2 * (nodeChannel.RotationKeys[a].Value.X * nodeChannel.RotationKeys[a].Value.X + nodeChannel.RotationKeys[a].Value.Y * nodeChannel.RotationKeys[a].Value.Y);
                                        double xAngle = Math.Atan2(sinr_cosp, cosr_cosp);

                                        double yAngle;
                                        float sinp = 2 * (nodeChannel.RotationKeys[a].Value.W * nodeChannel.RotationKeys[a].Value.Y - nodeChannel.RotationKeys[a].Value.Z * nodeChannel.RotationKeys[a].Value.X);
                                        if (Math.Abs(sinp) >= 1)
                                            yAngle = Math.PI / 2 * Math.Sign(sinp); // use 90 degrees if out of range
                                        else
                                            yAngle = Math.Asin(sinp);

                                        float siny_cosp = 2 * (nodeChannel.RotationKeys[a].Value.W * nodeChannel.RotationKeys[a].Value.Z + nodeChannel.RotationKeys[a].Value.X * nodeChannel.RotationKeys[a].Value.Y);
                                        float cosy_cosp = 1 - 2 * (nodeChannel.RotationKeys[a].Value.Y * nodeChannel.RotationKeys[a].Value.Y + nodeChannel.RotationKeys[a].Value.Z * nodeChannel.RotationKeys[a].Value.Z);
                                        double zAngle =  Math.Atan2(siny_cosp, cosy_cosp);

                                        NinjaKeyframe.NNS_MOTION_KEY_ROTATE_A16 rotation = new();
                                        rotation.Frame = (short)nodeChannel.RotationKeys[a].Time;
                                        rotation.Value1 = (short)(xAngle * 65536 / 360);
                                        rotation.Value2 = (short)(yAngle * 65536 / 360);
                                        rotation.Value3 = (short)(zAngle * 65536 / 360);
                                        //rotation.Value1 = (short)(nodeChannel.RotationKeys[a].Value.X * 65536 / 360);
                                        //rotation.Value2 = (short)(nodeChannel.RotationKeys[a].Value.Y * 65536 / 360);
                                        //rotation.Value3 = (short)(nodeChannel.RotationKeys[a].Value.Z * 65536 / 360);
                                        rotationSubMotion.Keyframes.Add(rotation);
                                    }

                                    Data.Motion.SubMotions.Add(rotationSubMotion);
                                }

                                //if (nodeChannel.HasScalingKeys)
                                //{
                                //    NinjaSubMotion scaleSubMotion = new();
                                //    scaleSubMotion.Type = NinjaNext_SubMotionType.NND_SMOTTYPE_FRAME_FLOAT | NinjaNext_SubMotionType.NND_SMOTTYPE_SCALING_MASK;
                                //    scaleSubMotion.InterpolationType = NinjaNext_SubMotionInterpolationType.NND_SMOTIPTYPE_LINEAR | NinjaNext_SubMotionInterpolationType.NND_SMOTIPTYPE_REPEAT;
                                //    scaleSubMotion.EndFrame = (float)assimpModel.Animations[0].DurationInTicks;
                                //    scaleSubMotion.EndKeyframe = (float)assimpModel.Animations[0].DurationInTicks;
                                //    scaleSubMotion.ID = i;

                                //    for (int a = 0; a < nodeChannel.ScalingKeys.Count; a++)
                                //    {
                                //        NinjaKeyframe.NNS_MOTION_KEY_VECTOR scale = new();
                                //        scale.Frame = (float)nodeChannel.PositionKeys[a].Time;
                                //        scale.Value = new(nodeChannel.PositionKeys[a].Value.X, nodeChannel.PositionKeys[a].Value.Y, nodeChannel.PositionKeys[a].Value.Z);
                                //        scaleSubMotion.Keyframes.Add(scale);
                                //    }

                                //    Data.Motion.SubMotions.Add(scaleSubMotion);
                                //}
                            }
                        }
                    }
                }
            }
        }
    }
}
