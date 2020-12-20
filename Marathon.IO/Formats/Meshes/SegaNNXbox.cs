using System;
using System.IO;
using System.Collections.Generic;
using Marathon.IO.Exceptions;

namespace Marathon.IO.Formats.Meshes
{
    // TODO: Maybe try to complete this for once, even if it's just reading? Being able to write back binary identical data will be useful for research.
    // Understand the Footer Nodes.
    // Study en_Kyozoress.xtv for dynamic material animations
    public class SegaNNXbox : FileBase
    {
        // NXTL
        public class NinjaTextureFile
        {
            public string Filename;
            public uint Filter;
        }

        // NXEF
        public class NinjaEffectList
        {
            public uint Type;
            public List<NinjaEffectFile> EffectFileList = new List<NinjaEffectFile>();
            public List<NinjaTechniqueName> EffectTechniqueList = new List<NinjaTechniqueName>();
            public List<short> TechniqueIDList = new List<short>();
        }
        public class NinjaEffectFile
        {
            public uint Type;
            public string Filename;
        }
        public class NinjaTechniqueName
        {
            public uint Type;
            public string Name;
        }

        // NXNN
        public class NinjaNodeNameList
        {
            public uint Type;
            public List<string> NodeNameList = new List<string>();
        }

        // NXOB
        public class NinjaObject
        {
            public Vector3 Center;
            public float Radius;
            public List<NinjaObjectMaterial> MaterialList = new List<NinjaObjectMaterial>();
            public List<NinjaObjectVertex> VertexList = new List<NinjaObjectVertex>();
            public List<NinjaObjectPrimitive> PrimitiveList = new List<NinjaObjectPrimitive>();
            public uint MaxNodeDepth;
            public List<NinjaObjectNode> NodeList = new List<NinjaObjectNode>();
            public uint MatrixPAL; // TODO: Figure out what this stands for. Matrix Something?
            public List<NinjaSubObject> SubObjectList = new List<NinjaSubObject>();
        }
        public class NinjaObjectMaterial
        {
            public uint Type;
            // NNS_MATERIAL_DESC Values.
            public uint Description_Flag;
            public uint Description_UserDefined;
            // NNS_MATERIAL_COLOR Values.
            public Vector4 Colour_Diffuse;
            public Vector4 Colour_Ambient;
            public Vector4 Colour_Specular;
            public Vector4 Colour_Emissive;
            public float Colour_Power;
            // NNS_MATERIAL_LOGIC Values.
            public bool Logic_BlendEnable;
            public uint Logic_SRCBlend; // TODO: Figure out what SRC means.
            public uint Logic_DSTBlend; // TODO: Figure out what DST means.
            public uint Logic_BlendFactor;
            public uint Logic_BlendOP; // TODO: Figure out what the OP part means. Blend Operation?
            public uint Logic_LogicOP; // TODO: Figure out what the OP part means. Logic Operation?
            public bool Logic_AlphaEnable;
            public uint Logic_AlphaFunction;
            public uint Logic_AlphaRef; // TODO: Figure out what the Ref part means. Alpha Reflectivity?
            public bool Logic_ZCompEnable; // TODO: Figure out what the ZComp part means. Z-Compensation?
            public uint Logic_ZFunction;
            public bool Logic_ZUpdateEnable;
            // NNS_MATERIAL_TEXMAP2_DESC Values.
            public uint Texture_Type;
            public uint Texture_ID;
            public Vector2 Texture_Offset;
            public float Texture_Blend;
            public uint Texture_MinFilter; // TODO: Find out what the Min part means. All set to NND_MIN_LINEAR_MIPMAP_NEAREST in the XTO.
            public uint Texture_MagFilter; // TODO: Find out what the Mag part means. All set to NND_MAG_LINEAR in the XTO.
            public float Texture_MipMapBias;
            public uint Texture_MaxMipMapLevel;
        }
        public class NinjaObjectVertex
        {
            public uint Type;
            // NNS_VTXLIST_DX_DESC Values.
            public uint Description_Format;
            public uint Description_FVF; // TODO: Find out what the FVF part means.
            public uint Description_HDRCommon;
            public uint Description_HDRData;
            public uint Description_HDRLock;
            public List<NinjaObjectVertexXB> VertexList = new List<NinjaObjectVertexXB>();
            public List<int> BoneMatrix = new List<int>();
        }
        public class NinjaObjectVertexXB // Temp Name
        {
            // NNS_VTXTYPE_XB_??? Values.
            public Vector3 List_Position;
            public Vector3 List_Weight;
            public List<byte> List_BoneMatrix = new List<byte>(); // Just based on what I'm seeing from the MaxScript and LibS06.
            public Vector3 List_Normals;
            public List<byte> List_VertexColour = new List<byte>();
            public List<Vector2> List_TextureCoordinates = new List<Vector2>();
            public Vector3 List_Tangent;
            public Vector3 List_Binormal;
        }
        public class NinjaObjectPrimitive
        {
            public uint Type;
            // NNS_PRIMTYPE_DX_STRIPLIST Values
            public uint SList_Format;
            public List<ushort> SList_Index = new List<ushort>();
        }
        public class NinjaObjectNode
        {
            public uint Type;
            public ushort Matrix;
            public ushort Parent;
            public ushort Child;
            public ushort Sibling;
            public Vector3 Transform;
            public Vector3 Rotation;
            public Vector3 Scale; // Can only assume that's what SCL stands for.
            public List<float> InvinitMatrix = new List<float>(); // TODO: Find out what Invinit means.
            public Vector3 Center;
            public float Radius;
            public uint UserDefined; // Always 0 in the XTO?
            public Vector3 BoundingBox; // The XTO usually has three RSV (reserved?) values here, but two nodes seem to have Bounding Boxes instead.
        }
        public class NinjaSubObject
        {
            public uint Type;
            public List<NinjaSubObjectMeshSet> MeshSets = new List<NinjaSubObjectMeshSet>();
            public List<int> TextureList = new List<int>();
        }
        public class NinjaSubObjectMeshSet
        {
            // NNS_MESHSET Values.
            public Vector3 Center;
            public float Radius;
            public uint NodeID;
            public uint Matrix;
            public uint MaterialID;
            public uint VertexID;
            public uint PrimitiveID;
            public uint ShaderID;
        }

        public class NinjaMotion
        {
            public uint Type;
            public float StartFrame;
            public float EndFrame;
            public List<NinjaSubMotion> SubMotions = new List<NinjaSubMotion>();
            public float Framerate;
            public uint Reserved0;
            public uint Reserved1;
        }
        public class NinjaSubMotion
        {
            public uint SubMot_Type;
            public uint SubMot_IPType; // TODO: Find out what IP means in this context.
            public uint SubMot_ID;
            public float SubMot_StartFrame;
            public float SubMot_EndFrame;
            public float SubMot_StartKey;
            public float SubMot_EndKey;
            public List<NinjaSubMotionKey> SubMot_KeyList = new List<NinjaSubMotionKey>();
        }
        public class NinjaSubMotionKey
        {
            public float Key_Translation_MFRM;
            public float Key_Translation_MVAL;
            public short Key_Rotation_MFRM;
            public Half Key_Rotation_MVAL;
            public float Key_Diffuse_MFRM;
            public Vector3 Key_Diffuse_MVAL;
        }


        // Declare the Lists, but don't create them, as not every list is used by every SegaNN file.
        public List<NinjaTextureFile> _NinjaTextureFileList;
        public NinjaEffectList        _NinjaEffectList;
        public NinjaNodeNameList      _NinjaNodeNameList;
        public NinjaObject            _NinjaObject;
        public NinjaMotion            _NinjaMotion;

        public override void Load(Stream fileStream)
        {
            ExtendedBinaryReader reader = new ExtendedBinaryReader(fileStream) { Offset = 0x20 };

            // Info Node
            string InfoNodeType  = new string(reader.ReadChars(4)); // The Type of this Info Node.
            if (InfoNodeType != "NXIF")
                throw new InvalidSignatureException("NXIF", InfoNodeType); // Only supporting XN* (for now?).
            uint NodeLength      = reader.ReadUInt32();             // The length of the Info Node.
            uint NodeCount       = reader.ReadUInt32();             // How many other Nodes make up this NN file.
            uint UnknownUInt32_1 = reader.ReadUInt32();             // Seems to always be 0x20 (at least in XN*s)? Offset amount?
            uint UnknownUInt32_2 = reader.ReadUInt32();             // Footer Offset Table Start?
            uint UnknownUInt32_3 = reader.ReadUInt32();             // Footer Offset Table Data Start?
            uint UnknownUInt32_4 = reader.ReadUInt32();             // Footer Offset Table Length?
            uint UnknownUInt32_5 = reader.ReadUInt32();             // Seems to always be 1 (at least in XN*s)?

            // Loop through nodes.
            for (int i = 0; i < NodeCount; i++)
            {
                // Determine type of node to read and its length
                string NextNodeType = new string(reader.ReadChars(4));
                uint NextNodeLength = reader.ReadUInt32();
                reader.JumpBehind(8);

                switch (NextNodeType)
                {
                    case "NXTL":
                        ReadNinjaTextureList(reader);
                        break;
                    case "NXEF":
                        ReadNinjaEffectList(reader);
                        break;
                    case "NXNN":
                        ReadNinjaNodeNameList(reader);
                        break;
                    case "NXOB":
                        ReadNinjaObject(reader);
                        break;
                    case "NXMO":
                    case "NXMA":
                        ReadNinjaMotion(reader);
                        break;
                    default:
                        reader.JumpAhead(8);
                        reader.JumpAhead(NextNodeLength);
                        Console.WriteLine($"Block {NextNodeType} Not Implemented!");
                        break;
                }
            }

            // TODO: Read Footer.
        }

        public void ReadNinjaTextureList(ExtendedBinaryReader reader)
        {
            // Create the _NinjaTextureFileList so we can write to it.
            _NinjaTextureFileList = new List<NinjaTextureFile>();

            string TextureListNodeType = new string(reader.ReadChars(4)); // The type of this Texture List Node.
            uint NodeLength            = reader.ReadUInt32();             // The length of this Node.
            long returnPosition        = reader.BaseStream.Position;      // Save position so we can use it with the length to jump to the next Node afterwards.
            uint NodeOffset            = reader.ReadUInt32();             // Where this Node's Data starts.

            // Jump to the texture node's data.
            reader.JumpTo(NodeOffset, true);

            uint TextureCount      = reader.ReadUInt32(); // The amount of texture data in this Node.
            uint TextureListOffset = reader.ReadUInt32(); // The offset to the texture data in this Node.

            // Jump to the texture list.
            reader.JumpTo(TextureListOffset, true);

            // Loop through based on amount of textures in this Node.
            for (int i = 0; i < TextureCount; i++)
            {
                NinjaTextureFile _NinjaTextureFile = new NinjaTextureFile();
                uint UnknownUInt32_1               = reader.ReadUInt32(); // Unknown, not referenced in the XTO file.
                uint TextureNameOffset             = reader.ReadUInt32(); // The offset to this texture's Filename.
                _NinjaTextureFile.Filter           = reader.ReadUInt32(); // The flags for this texture's Filter Settings.
                uint UnknownUInt32_2               = reader.ReadUInt32(); // Unknown, not referenced in the XTO file.
                uint UnknownUInt32_3               = reader.ReadUInt32(); // Unknown, not referenced in the XTO file.

                // Store current position to jump back to for the next texture.
                long position = reader.BaseStream.Position;

                // Jump to and read this texture's Filename.
                reader.JumpTo(TextureNameOffset, true);
                _NinjaTextureFile.Filename = reader.ReadNullTerminatedString();

                // Jump back to the saved position to read the next texture.
                reader.JumpTo(position);

                // Save texture entry into the _NinjaTextureFileList.
                _NinjaTextureFileList.Add(_NinjaTextureFile);
            }

            // Return to the previously saved position and jump ahead by NodeLength to place us at the next node.
            reader.JumpTo(returnPosition);
            reader.JumpAhead(NodeLength);
        }

        public void ReadNinjaEffectList(ExtendedBinaryReader reader)
        {
            // Create the _NinjaEffectList so we can write to it.
            _NinjaEffectList = new NinjaEffectList();

            string EffectListNodeType = new string(reader.ReadChars(4)); // The Type of this Effect List Node.
            uint NodeLength           = reader.ReadUInt32();             // The length of this Node.
            long returnPosition       = reader.BaseStream.Position;      // Save position so we can use it with the length to jump to the next Node afterwards.
            uint NodeOffset           = reader.ReadUInt32();             // Where this Node's Data starts.

            // Jump to the effect node's data.
            reader.JumpTo(NodeOffset, true);

            _NinjaEffectList.Type     = reader.ReadUInt32(); // The type of effect list this Node is.
            uint EffectFilesCount     = reader.ReadUInt32(); // The amount of effect files in this effect list.
            uint EffectFilesOffset    = reader.ReadUInt32(); // The offset to the effect files in this effect list.
            uint TechniqueNamesCount  = reader.ReadUInt32(); // The amount of technique names in this effect list.
            uint TechniqueNamesOffset = reader.ReadUInt32(); // The offset to the technique names in this effect list.
            uint TechniqueIDsCount    = reader.ReadUInt32(); // The amount of technique ID entries in this effect list.
            uint TechniqueIDsOffset   = reader.ReadUInt32(); // The offset to the technique ID data in this effect list.

            // Effect Files.
            // Jump to the effect files in this effect list.
            reader.JumpTo(EffectFilesOffset, true);

            // Loop through based on amount of effect files in this effect list.
            for (int i = 0; i < EffectFilesCount; i++)
            {
                NinjaEffectFile _NinjaEffectFile = new NinjaEffectFile();
                _NinjaEffectFile.Type     = reader.ReadUInt32(); // The type of effect file this is.
                uint EffectFilenameOffset = reader.ReadUInt32(); // The offset to this effect file's Filename.

                // Store current position to jump back to for the next effect file.
                long position = reader.BaseStream.Position;

                // Jump to and read this effect file's Filename.
                reader.JumpTo(EffectFilenameOffset, true);
                _NinjaEffectFile.Filename = reader.ReadNullTerminatedString();

                // Jump back to the saved position to read the next effect file.
                reader.JumpTo(position);

                // Save effect file entry into EffectFileList in the _NinjaEffectList.
                _NinjaEffectList.EffectFileList.Add(_NinjaEffectFile);
            }

            // Effect Technique
            // Jump to the technique data in this Node.
            reader.JumpTo(TechniqueNamesOffset, true);

            // Loop through based on amount of techniques in this Node.
            for (int i = 0; i < TechniqueNamesCount; i++)
            {
                NinjaTechniqueName _NinjaTechniqueName = new NinjaTechniqueName();
                _NinjaTechniqueName.Type               = reader.ReadUInt32(); // The type of technique this is.
                uint EffectFileID                      = reader.ReadUInt32(); // The ID of this this technique's effect file, always linear.
                uint TechniqueNameOffset               = reader.ReadUInt32(); // The offset to this technique's Filename.

                // Store current position to jump back to for the next technique.
                long currentPos = reader.BaseStream.Position;

                // Jump to and read this technique's Filename.
                reader.JumpTo(TechniqueNameOffset, true);
                _NinjaTechniqueName.Name = reader.ReadNullTerminatedString();

                // Jump back to the saved position to read the next technique.
                reader.JumpTo(currentPos);

                // Save technique entry into EffectTechniqueList in the _NinjaEffectList.
                _NinjaEffectList.EffectTechniqueList.Add(_NinjaTechniqueName);
            }

            // Technique IDs
            // Jump to the technique ID data in this Node.
            reader.JumpTo(TechniqueIDsOffset, true);

            // Loop through based on amount of technique ID entries in this Node and save them into TechniqueIDList in the _NinjaEffectList.
            for (int i = 0; i < TechniqueIDsCount; i++)
                _NinjaEffectList.TechniqueIDList.Add(reader.ReadInt16());

            // Return to the previously saved position and jump ahead by NodeLength to place us at the next node.
            reader.JumpTo(returnPosition);
            reader.JumpAhead(NodeLength);
        }

        public void ReadNinjaNodeNameList(ExtendedBinaryReader reader)
        {
            // Create the _NinjaNodeNameList so we can write to it.
            _NinjaNodeNameList = new NinjaNodeNameList();

            string NodeNameListNodeType = new string(reader.ReadChars(4)); // The Type of this Node Name List Node.
            uint NodeLength             = reader.ReadUInt32();             // Length of this Node.
            long returnPosition         = reader.BaseStream.Position;      // Save position so we can use it with the length to jump to the next Node afterwards.
            uint NodeOffset             = reader.ReadUInt32();             // Where this Node's Data starts.

            // Jump to the name list node's data.
            reader.JumpTo(NodeOffset, true);

            _NinjaNodeNameList.Type   = reader.ReadUInt32(); // The type of node name list this is.
            uint NodeListEntriesCount = reader.ReadUInt32(); // How many name entries exist in this Node.
            uint NodeListOffset       = reader.ReadUInt32(); // The offset to the node list.

            // Jump to the node table in this Node.
            reader.JumpTo(NodeListOffset, true);

            // Loop through based on amount of nodes in this Node.
            for (int i = 0; i < NodeListEntriesCount; i++)
            {
                uint NodeID         = reader.ReadUInt32(); // The ID of the current node, always linear.
                uint NodeTypeOffset = reader.ReadUInt32(); // The offset to this node's name.

                // Store current position to jump back to for the next technique.
                long position = reader.BaseStream.Position;

                // Jump to and read this node's name.
                reader.JumpTo(NodeTypeOffset, true);
                string NodeName = reader.ReadNullTerminatedString();

                // Jump back to the saved position to read the next node.
                reader.JumpTo(position);

                // Save the node name into the NodeNameList in the _NinjaNodeNameList.
                _NinjaNodeNameList.NodeNameList.Add(NodeName);
            }

            // Return to the previously saved position and jump ahead by NodeLength to place us at the next node.
            reader.JumpTo(returnPosition);
            reader.JumpAhead(NodeLength);
        }

        public void ReadNinjaObject(ExtendedBinaryReader reader)
        {
            // Create the _NinjaObject so we can write to it.
            _NinjaObject = new NinjaObject();

            string NodeObjectNodeType = new string(reader.ReadChars(4)); // The Type of this Object Node.
            uint NodeLength           = reader.ReadUInt32();             // Length of this Node.
            long returnPosition       = reader.BaseStream.Position;      // Save position so we can use it with the length to jump to the next Node afterwards.
            uint NodeOffset           = reader.ReadUInt32();             // Where this Node's Data starts.

            // Jump to the object node's data.
            reader.JumpTo(NodeOffset, true);

            // Read object data, counts and pointers.
            _NinjaObject.Center       = reader.ReadVector3(); // The center point of this object.
            _NinjaObject.Radius       = reader.ReadSingle();  // The radius this object takes up?
            uint MaterialCount        = reader.ReadUInt32();  // The amount of materials specified in this object.
            uint MaterialListOffset   = reader.ReadUInt32();  // The offset to this object's material list.
            uint VertexCount          = reader.ReadUInt32();  // The amount of verticies specified in this object.
            uint VertexListOffset     = reader.ReadUInt32();  // The offset to this object's vertex list.
            uint PrimitiveCount       = reader.ReadUInt32();  // The amount of primatives specified in this object.
            uint PrimitiveListOffset  = reader.ReadUInt32();  // The offset to this object's primitive list.
            uint NodeCount            = reader.ReadUInt32();  // The amount of nodes that make up this object.
            _NinjaObject.MaxNodeDepth = reader.ReadUInt32();  // The deepest level that a node is in a chain in this object.
            uint NodeListOffset       = reader.ReadUInt32();  // The offset to this object's node list.
            _NinjaObject.MatrixPAL    = reader.ReadUInt32();  // TODO: Figure out what this stands for and what it does. Matrix Something?
            uint SubObjectCount       = reader.ReadUInt32();  // The amount of sub objects in this object.
            uint SubObjectListOffset  = reader.ReadUInt32();  // The offset to this object's sub object list.
            uint TextureCount         = reader.ReadUInt32();  // The amount of textures in this object (should just match up with the amount of entries in _NinjaTextureFileList?).

            #region NNS_MATERIALPTR
            // Jump to the object's material list.
            reader.JumpTo(MaterialListOffset, true);

            // Loop through based on amount of materials in this object's material list.
            for (int i = 0; i < MaterialCount; i++)
            {
                NinjaObjectMaterial _NinjaObjectMaterial = new NinjaObjectMaterial();
                _NinjaObjectMaterial.Type                = reader.ReadUInt32(); // This material's type.
                uint MaterialDescriptionOffset           = reader.ReadUInt32(); // The offset to this material's NNS_MATERIAL_DESC chunk.
                long position = reader.BaseStream.Position; // Save position so we can use it to jump back and read the next material after we've read all the data for this one.

                // NNS_MATERIAL_DESC chunk
                // Jump to this material's description chunk.
                reader.JumpTo(MaterialDescriptionOffset, true);

                _NinjaObjectMaterial.Description_Flag        = reader.ReadUInt32(); // The flags for this material.
                _NinjaObjectMaterial.Description_UserDefined = reader.ReadUInt32(); // The user defined value for this material.
                uint MaterialColourOffset                    = reader.ReadUInt32(); // The offset to this material's colour data.
                uint MaterialLogicOffset                     = reader.ReadUInt32(); // The offset to this material's logic data.
                uint MaterialTextureDescriptionOffset        = reader.ReadUInt32(); // The offset to this material's texture data.

                // NNS_MATERIAL_COLOR chunk
                if (MaterialColourOffset != 0)
                {
                    // Jump to this material's colour data.
                    reader.JumpTo(MaterialColourOffset, true);

                    _NinjaObjectMaterial.Colour_Diffuse  = reader.ReadVector4(); // The diffuse value for this material.
                    _NinjaObjectMaterial.Colour_Ambient  = reader.ReadVector4(); // The ambient value for this material.
                    _NinjaObjectMaterial.Colour_Specular = reader.ReadVector4(); // The specular value for this material.
                    _NinjaObjectMaterial.Colour_Emissive = reader.ReadVector4(); // The diffuse value for this material.
                    _NinjaObjectMaterial.Colour_Power    = reader.ReadSingle();  // How much the other values are affected? 
                }

                // NNS_MATERIAL_LOGIC chunk.
                if (MaterialLogicOffset != 0)
                {
                    // Jump to this material's logic data.
                    reader.JumpTo(MaterialLogicOffset, true);

                    _NinjaObjectMaterial.Logic_BlendEnable   = reader.ReadBoolean32(); // Whether this material can do blending?
                    _NinjaObjectMaterial.Logic_SRCBlend      = reader.ReadUInt32();
                    _NinjaObjectMaterial.Logic_DSTBlend      = reader.ReadUInt32();
                    _NinjaObjectMaterial.Logic_BlendFactor   = reader.ReadUInt32();    // Something to do with how much this material blends by?
                    _NinjaObjectMaterial.Logic_BlendOP       = reader.ReadUInt32();
                    _NinjaObjectMaterial.Logic_LogicOP       = reader.ReadUInt32();
                    _NinjaObjectMaterial.Logic_AlphaEnable   = reader.ReadBoolean32(); // Whether this material supports alpha tranparency?
                    _NinjaObjectMaterial.Logic_AlphaFunction = reader.ReadUInt32();
                    _NinjaObjectMaterial.Logic_AlphaRef      = reader.ReadUInt32();    // How reflective the alpha channel is maybe?
                    _NinjaObjectMaterial.Logic_ZCompEnable   = reader.ReadBoolean32();
                    _NinjaObjectMaterial.Logic_ZFunction     = reader.ReadUInt32();
                    _NinjaObjectMaterial.Logic_ZUpdateEnable = reader.ReadBoolean32();
                }

                // NNS_MATERIAL_TEXMAP2_DESC chunk.
                if (MaterialTextureDescriptionOffset != 0)
                {
                    // Jump to this material's logic data.
                    reader.JumpTo(MaterialTextureDescriptionOffset, true);

                    _NinjaObjectMaterial.Texture_Type           = reader.ReadUInt32();
                    _NinjaObjectMaterial.Texture_ID             = reader.ReadUInt32();  // Which texture in the _NinjaTextureFileList this material should use.
                    _NinjaObjectMaterial.Texture_Offset         = reader.ReadVector2();
                    _NinjaObjectMaterial.Texture_Blend          = reader.ReadSingle();
                    uint TextureInfoOffset                      = reader.ReadUInt32();  // This is always null in '06's XNOs
                    _NinjaObjectMaterial.Texture_MinFilter      = reader.ReadUInt32();
                    _NinjaObjectMaterial.Texture_MagFilter      = reader.ReadUInt32();
                    _NinjaObjectMaterial.Texture_MipMapBias     = reader.ReadSingle();
                    _NinjaObjectMaterial.Texture_MaxMipMapLevel = reader.ReadUInt32();  // The highest level mipmap this material is allowed to use?
                }

                // Jump back to the saved position to read the next node.
                reader.JumpTo(position);

                // Save this material into the MaterialList in _NinjaObject.
                _NinjaObject.MaterialList.Add(_NinjaObjectMaterial);
            }
            #endregion

            #region NNS_VTXLISTPTR
            // Jump to the object's vertex list.
            reader.JumpTo(VertexListOffset, true);

            // Loop through based on amount of verticies in this object's vertex list.
            for (int i = 0; i < VertexCount; i++)
            {
                NinjaObjectVertex _NinjaObjectVertex = new NinjaObjectVertex();
                _NinjaObjectVertex.Type              = reader.ReadUInt32(); // This vertex's type.
                uint VertexDescriptionOffset         = reader.ReadUInt32(); // The offset to this vertex's NNS_VTXLIST_DX_DESC chunk..

                long position = reader.BaseStream.Position; // Save position so we can use it to jump back and read the next vertex after we've read all the data for this one.

                // NNS_VTXLIST_DX_DESC chunk
                // Jump to this vertex's description chunk.
                reader.JumpTo(VertexDescriptionOffset, true);

                _NinjaObjectVertex.Description_Format    = reader.ReadUInt32(); // The format of this vertex.
                _NinjaObjectVertex.Description_FVF       = reader.ReadUInt32();
                uint VertexDataSize                      = reader.ReadUInt32(); // How many bytes each bit of data for this vertex takes up.
                uint VertexDataCount                     = reader.ReadUInt32(); // How many bits of data make up this vertex.
                uint VertexDataOffset                    = reader.ReadUInt32(); // The offset to this vertex's data.
                uint BoneMatrixCount                     = reader.ReadUInt32(); // How any nodes makde up this vertex's bone matrix.
                uint BoneMatrixOffset                    = reader.ReadUInt32(); // The offset to this vertex's bone matrix.
                _NinjaObjectVertex.Description_HDRCommon = reader.ReadUInt32();
                _NinjaObjectVertex.Description_HDRData   = reader.ReadUInt32();
                _NinjaObjectVertex.Description_HDRLock   = reader.ReadUInt32();

                // NNS_VTXTYPE_XB_??? Chunk.
                if (VertexDataOffset != 0)
                {
                    // Jump to this vertex's data.
                    reader.JumpTo(VertexDataOffset, true);
                    for(int v = 0; v < VertexDataCount; v++)
                    {
                        long vertexPosition = reader.BaseStream.Position + VertexDataSize;
                        NinjaObjectVertexXB _NinjaObjectVertexXB = new NinjaObjectVertexXB();

                        // Position
                        if ((_NinjaObjectVertex.Description_Format & 0x1) == 1){ _NinjaObjectVertexXB.List_Position = reader.ReadVector3(); }     

                        // Weight
                        if ((_NinjaObjectVertex.Description_Format & 0x7000) == 7000) { _NinjaObjectVertexXB.List_Weight = reader.ReadVector3(); }

                        // Bone Matrix
                        if ((_NinjaObjectVertex.Description_Format & 0x400) == 400)
                        {
                            _NinjaObjectVertexXB.List_BoneMatrix.Add(reader.ReadByte());
                            _NinjaObjectVertexXB.List_BoneMatrix.Add(reader.ReadByte());
                            _NinjaObjectVertexXB.List_BoneMatrix.Add(reader.ReadByte());
                            _NinjaObjectVertexXB.List_BoneMatrix.Add(reader.ReadByte());
                        }

                        // Normals.
                        if ((_NinjaObjectVertex.Description_Format & 0x2) == 2) { _NinjaObjectVertexXB.List_Normals = reader.ReadVector3(); } 

                        // Vertex Colours
                        if ((_NinjaObjectVertex.Description_Format & 0x8) == 8)
                        {
                            _NinjaObjectVertexXB.List_VertexColour.Add(reader.ReadByte());
                            _NinjaObjectVertexXB.List_VertexColour.Add(reader.ReadByte());
                            _NinjaObjectVertexXB.List_VertexColour.Add(reader.ReadByte());
                            _NinjaObjectVertexXB.List_VertexColour.Add(reader.ReadByte());
                        }

                        // Texture Coordinates????.
                        for(int t = 0; t < (_NinjaObjectVertex.Description_Format / 0x10000); t++)
                        {
                            _NinjaObjectVertexXB.List_TextureCoordinates.Add(reader.ReadVector2());
                        }

                        // Tangent and Binormals.
                        if ((_NinjaObjectVertex.Description_Format & 0x140) == 140)
                        {
                            _NinjaObjectVertexXB.List_Tangent = reader.ReadVector3();
                            _NinjaObjectVertexXB.List_Binormal = reader.ReadVector3();
                        }

                        // Save Vertex Entry.
                        _NinjaObjectVertex.VertexList.Add(_NinjaObjectVertexXB);
                        reader.JumpTo(vertexPosition);
                    }
                }

                // nnbonematrixlist Chunk.
                if (BoneMatrixOffset != 0)
                {
                    // Jump to this vertex's bone matrix.
                    reader.JumpTo(BoneMatrixOffset, true);

                    // Loop through based on amount of bones in this vertex's matrix and save them into the BoneMatrix in the _NinjaObjectVertex.
                    for (int b = 0; b < BoneMatrixCount; b++)
                        _NinjaObjectVertex.BoneMatrix.Add(reader.ReadInt32());
                }

                // Jump back to the saved position to read the next node.
                reader.JumpTo(position);

                // Save this vertex into the VertexList in _NinjaObject.
                _NinjaObject.VertexList.Add(_NinjaObjectVertex);
            }
            #endregion

            #region NNS_PRIMLISTPTR
            // Jump to the object's primitive list.
            reader.JumpTo(PrimitiveListOffset, true);

            // Loop through based on amount of primitive in this object's primitive list.
            for (int i = 0; i < PrimitiveCount; i++)
            {
                NinjaObjectPrimitive _NinjaObjectPrimitive = new NinjaObjectPrimitive();
                _NinjaObjectPrimitive.Type                 = reader.ReadUInt32(); // This primitive's type.
                uint PrimitiveStripListOffset              = reader.ReadUInt32(); // The offset to this primitive's NNS_PRIMTYPE_DX_STRIPLIST chunk.
                long position = reader.BaseStream.Position; // Save position so we can use it to jump back and read the next primitive after we've read all the data for this one.

                // NNS_PRIMTYPE_DX_STRIPLIST chunk.
                // Jump to this primitive's strip list chunk.
                reader.JumpTo(PrimitiveStripListOffset, true);

                _NinjaObjectPrimitive.SList_Format = reader.ReadUInt32();
                uint IndexNumber                   = reader.ReadUInt32();
                uint StripCount                    = reader.ReadUInt32();
                uint LengthOffset                  = reader.ReadUInt32();
                uint IndexPointer                  = reader.ReadUInt32();
                uint IDXBuffer                     = reader.ReadUInt32(); // Always 0?

                // Jump to this primitive's length offset.
                reader.JumpTo(LengthOffset, true);

                ushort Length = reader.ReadUInt16();

                // Jump to this primitive's index offset.
                reader.JumpTo(IndexPointer, true);

                // TODO: Figure out if I should be using IndexNumber or Length for this, the XTO always has SLIST_INDEX_NUM and the value in SLIST_LENPTR be the same, but other ones don't.
                // Loop through based on amount of entries in this primitive's Strip List and save them into the SList_Index in the _NinjaObjectPrimitive.
                for (int p = 0; p < IndexNumber; p++)
                    _NinjaObjectPrimitive.SList_Index.Add(reader.ReadUInt16());

                // Jump back to the saved position to read the next node.
                reader.JumpTo(position);

                // Save this primitive into the PrimitiveList in _NinjaObject.
                _NinjaObject.PrimitiveList.Add(_NinjaObjectPrimitive);
            }
            #endregion

            #region NNS_NODE
            // Jump to the object's primitive list.
            reader.JumpTo(NodeListOffset, true);

            // Loop through based on amount of primitive in this object's primitive list.
            for (int i = 0; i < NodeCount; i++)
            {
                NinjaObjectNode _NinjaObjectNode = new NinjaObjectNode();
                _NinjaObjectNode.Type            = reader.ReadUInt32();  // This node's type.
                _NinjaObjectNode.Matrix          = reader.ReadUInt16();  // This node's matrix reference, set to FFFF for NIL.
                _NinjaObjectNode.Parent          = reader.ReadUInt16();  // This node's parent reference, set to FFFF for NIL.
                _NinjaObjectNode.Child           = reader.ReadUInt16();  // This node's child reference, set to FFFF for NIL.
                _NinjaObjectNode.Sibling         = reader.ReadUInt16();  // This node's sibling reference, set to FFFF for NIL.
                _NinjaObjectNode.Transform       = reader.ReadVector3(); // This node's transform values.
                _NinjaObjectNode.Rotation        = reader.ReadVector3(); // This node's rotation values.
                _NinjaObjectNode.Scale           = reader.ReadVector3(); // This node's scale values.
                _NinjaObjectNode.InvinitMatrix.Add(reader.ReadSingle());
                _NinjaObjectNode.InvinitMatrix.Add(reader.ReadSingle());
                _NinjaObjectNode.InvinitMatrix.Add(reader.ReadSingle());
                _NinjaObjectNode.InvinitMatrix.Add(reader.ReadSingle());
                _NinjaObjectNode.InvinitMatrix.Add(reader.ReadSingle());
                _NinjaObjectNode.InvinitMatrix.Add(reader.ReadSingle());
                _NinjaObjectNode.InvinitMatrix.Add(reader.ReadSingle());
                _NinjaObjectNode.InvinitMatrix.Add(reader.ReadSingle());
                _NinjaObjectNode.InvinitMatrix.Add(reader.ReadSingle());
                _NinjaObjectNode.InvinitMatrix.Add(reader.ReadSingle());
                _NinjaObjectNode.InvinitMatrix.Add(reader.ReadSingle());
                _NinjaObjectNode.InvinitMatrix.Add(reader.ReadSingle());
                _NinjaObjectNode.InvinitMatrix.Add(reader.ReadSingle());
                _NinjaObjectNode.InvinitMatrix.Add(reader.ReadSingle());
                _NinjaObjectNode.InvinitMatrix.Add(reader.ReadSingle());
                _NinjaObjectNode.InvinitMatrix.Add(reader.ReadSingle());
                _NinjaObjectNode.Center          = reader.ReadVector3(); // The central point of this node.
                _NinjaObjectNode.Radius          = reader.ReadSingle();  // The radius of this node.
                _NinjaObjectNode.UserDefined     = reader.ReadUInt32();  // This node's user defined data.
                _NinjaObjectNode.BoundingBox     = reader.ReadVector3(); // This node's bounding box/reserved data.

                // Save this node into the NodeList in _NinjaObject.
                _NinjaObject.NodeList.Add(_NinjaObjectNode);
            }
            #endregion

            #region NNS_SUBOBJ
            // Jump to the object's sub object list.
            reader.JumpTo(SubObjectListOffset, true);

            // Loop through based on amount of sub objects in this object's sub object list.
            for (int i = 0; i < SubObjectCount; i++)
            {
                NinjaSubObject _NinjaSubObject  = new NinjaSubObject();
                _NinjaSubObject.Type            = reader.ReadUInt32(); // This sub object's type.
                uint MeshSetCount               = reader.ReadUInt32();
                uint MeshSetOffset              = reader.ReadUInt32();
                uint SubObjectTextureCount      = reader.ReadUInt32();
                uint SubObjectTextureListOffset = reader.ReadUInt32();
                long position = reader.BaseStream.Position; // Save position so we can use it to jump back and read the next sub object after we've read all the data for this one.

                // NNS_MESHSET Chunk.
                // Jump to this sub object's mesh set data.
                reader.JumpTo(MeshSetOffset, true);

                for(int m = 0; m < MeshSetCount; m++)
                {
                    NinjaSubObjectMeshSet _NinjaSubObjectMeshSet = new NinjaSubObjectMeshSet
                    {
                        Center      = reader.ReadVector3(),
                        Radius      = reader.ReadSingle(),
                        NodeID      = reader.ReadUInt32(),
                        Matrix      = reader.ReadUInt32(),
                        MaterialID  = reader.ReadUInt32(),
                        VertexID    = reader.ReadUInt32(),
                        PrimitiveID = reader.ReadUInt32(),
                        ShaderID    = reader.ReadUInt32()
                    };
                    _NinjaSubObject.MeshSets.Add(_NinjaSubObjectMeshSet);
                }

                // Jump to this sub object's texture list offset.
                reader.JumpTo(SubObjectTextureListOffset, true);

                // Loop through based on amount of entries in this sub object's Texture List and save them into the TextureList in the _NinjaSubObject.
                for (int t = 0; t < SubObjectTextureCount; t++)
                    _NinjaSubObject.TextureList.Add(reader.ReadInt32());

                // Jump back to the saved position to read the next node.
                reader.JumpTo(position);

                // Save this sub object into the SubObjectList in _NinjaObject.
                _NinjaObject.SubObjectList.Add(_NinjaSubObject);
            }
            #endregion

            // Return to the previously saved position and jump ahead by NodeLength to place us at the next node.
            reader.JumpTo(returnPosition);
            reader.JumpAhead(NodeLength);
        }

        public void ReadNinjaMotion(ExtendedBinaryReader reader)
        {
            // Create the _NinjaMotion so we can write to it.
            _NinjaMotion = new NinjaMotion();

            string MotionNodeType = new string(reader.ReadChars(4)); // The type of this Motion Node.
            uint NodeLength       = reader.ReadUInt32();             // The length of this Node.
            long returnPosition   = reader.BaseStream.Position;      // Save position so we can use it with the length to jump to the next Node afterwards.
            uint NodeOffset       = reader.ReadUInt32();             // Where this Node's Data starts.

            // Jump to the motion node's data.
            reader.JumpTo(NodeOffset, true);

            _NinjaMotion.Type        = reader.ReadUInt32();
            _NinjaMotion.StartFrame  = reader.ReadSingle();
            _NinjaMotion.EndFrame    = reader.ReadSingle();
            uint SubMotionCount      = reader.ReadUInt32();
            uint SubMotionListOffset = reader.ReadUInt32();
            _NinjaMotion.Framerate   = reader.ReadSingle();
            _NinjaMotion.Reserved0   = reader.ReadUInt32();
            _NinjaMotion.Reserved1   = reader.ReadUInt32();

            // Sub Motions
            reader.JumpTo(SubMotionListOffset, true);
            for(int i = 0; i < SubMotionCount; i++)
            {
                NinjaSubMotion _NinjaSubMotion    = new NinjaSubMotion();
                _NinjaSubMotion.SubMot_Type       = reader.ReadUInt32();  // This value is needed to figure out what the data should be read as. Is all in S06XnFile.h in LibS06
                _NinjaSubMotion.SubMot_IPType     = reader.ReadUInt32();
                _NinjaSubMotion.SubMot_ID         = reader.ReadUInt32();
                _NinjaSubMotion.SubMot_StartFrame = reader.ReadSingle();
                _NinjaSubMotion.SubMot_EndFrame   = reader.ReadSingle();
                _NinjaSubMotion.SubMot_StartKey   = reader.ReadSingle();
                _NinjaSubMotion.SubMot_EndKey     = reader.ReadSingle();
                uint KeyFrameCount                = reader.ReadUInt32();
                uint KeySize                      = reader.ReadUInt32();
                uint KeyOffset                    = reader.ReadUInt32();

                // Store current position to jump back to for the next sub motion.
                long position = reader.BaseStream.Position;

                // Keys
                reader.JumpTo(KeyOffset, true);
                for(int k = 0; k < KeyFrameCount; k++)
                {
                    NinjaSubMotionKey _NinjaSubMotionKey = new NinjaSubMotionKey();
                    switch (_NinjaSubMotion.SubMot_Type)
                    {
                        case 257:
                        case 513:
                        case 1025:
                        case 16777217:
                            _NinjaSubMotionKey.Key_Translation_MFRM = reader.ReadSingle();
                            _NinjaSubMotionKey.Key_Translation_MVAL = reader.ReadSingle();
                            _NinjaSubMotion.SubMot_KeyList.Add(_NinjaSubMotionKey);
                            break;
                        case 2066:
                        case 4114:
                        case 8210:
                            _NinjaSubMotionKey.Key_Rotation_MFRM = reader.ReadInt16();
                            _NinjaSubMotionKey.Key_Rotation_MVAL = reader.ReadHalf();
                            _NinjaSubMotion.SubMot_KeyList.Add(_NinjaSubMotionKey);
                            break;
                        case 3585:
                            _NinjaSubMotionKey.Key_Diffuse_MFRM = reader.ReadSingle();
                            _NinjaSubMotionKey.Key_Diffuse_MVAL = reader.ReadVector3();
                            _NinjaSubMotion.SubMot_KeyList.Add(_NinjaSubMotionKey);
                            break;
                        default:
                            Console.WriteLine($"NinjaSubMotion Type of 0x{_NinjaSubMotion.SubMot_Type.ToString("X").PadLeft(8, '0')} ({_NinjaSubMotion.SubMot_Type}) not currently supported!");
                            break;
                    }
                }

                // Jump back to the saved position to read the next sub motion.
                reader.JumpTo(position);

                _NinjaMotion.SubMotions.Add(_NinjaSubMotion);
            }

            // Return to the previously saved position and jump ahead by NodeLength to place us at the next node.
            reader.JumpTo(returnPosition);
            reader.JumpAhead(NodeLength);
        }
    }
}
