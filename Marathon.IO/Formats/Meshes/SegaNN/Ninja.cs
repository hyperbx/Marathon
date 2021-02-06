// Ninja.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2021 Knuxfan24
 * Copyright (c) 2021 HyperBE32
 * Copyright (c) 2021 Radfordhound
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.IO;
using System.Collections.Generic;
using Marathon.IO.Exceptions;

namespace Marathon.IO.Formats.Meshes.SegaNN
{
    public class Ninja : FileBase
    {
        // TODO: Make this stuff more modular, probably shouldn't be storing linear indicies and stuff like that when the intent is to be able to import stuff.
        public List<NinjaTexture> Textures;
        public NinjaEffect Effects;
        public NinjaNodeList Nodes;
        public NinjaObject Object;
        public NinjaMotion Motion;

        public override void Load(Stream fileStream)
        {
            ExtendedBinaryReader reader = new ExtendedBinaryReader(fileStream) { Offset = 0x20 };

            // Use info node as file magic.
            string InfoNodeType = new string(reader.ReadChars(4)); // The Type of this Info Node.
            if (InfoNodeType != "NXIF")
                throw new InvalidSignatureException("NXIF", InfoNodeType); // Only supporting XN* (for now?).

            uint NodeLength        = reader.ReadUInt32(); // The length of the Info Node.
            uint DataChunkCount    = reader.ReadUInt32(); // How many other Nodes make up this NN file.
            uint DataChunkOffset   = reader.ReadUInt32(); // Seems to always be 0x20 (at least in XN*s)? Offset amount?
            uint DataSize          = reader.ReadUInt32(); // Footer Offset Table Start?
            uint OffsetTableOffset = reader.ReadUInt32(); // Footer Offset Table Data Start?
            uint OffsetTableSize   = reader.ReadUInt32(); // Footer Offset Table Length?
            uint Version           = reader.ReadUInt32(); // Seems to always be 1 (at least in XN*s)?

            // Loop through nodes.
            for (int i = 0; i < DataChunkCount; i++)
            {
                // Determine type of node to read and its length.
                string NextNodeType = new string(reader.ReadChars(4));
                uint NextNodeLength = reader.ReadUInt32();
                reader.JumpBehind(8);

                switch (NextNodeType)
                {
                    case "NXTL":
                        ReadNinjaTextures(reader);
                        break;

                    case "NXEF":
                    case "NXNN":
                    case "NXOB":
                        ReadNinjaNodeGeneric(reader);
                        break;

                    case "NXMO":
                    case "NXMA":
                        ReadNinjaMotion(reader);
                        break;

                    default:
                        reader.JumpAhead(8);
                        reader.JumpAhead(NextNodeLength);
                        Console.WriteLine($"Node {NextNodeType} not implemented!");
                        break;
                }
            }

            // TODO: Read Footer.
        }

        public void ReadNinjaTextures(ExtendedBinaryReader reader)
        {
            // Create the _NinjaTextureFileList so we can write to it.
            Textures = new List<NinjaTexture>();

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
                // Save texture entry into the _NinjaTextureFileList.
                Textures.Add(new NinjaTexture(reader));
            }

            // Return to the previously saved position and jump ahead by NodeLength to place us at the next node.
            reader.JumpTo(returnPosition);
            reader.JumpAhead(NodeLength);
        }

        public void ReadNinjaNodeGeneric(ExtendedBinaryReader reader)
        {
            string NodeType = new string(reader.ReadChars(4)); // The Type of this Node Name List Node.
            uint NodeLength = reader.ReadUInt32();             // Length of this Node.

            long returnPosition = reader.BaseStream.Position; // Save position so we can use it with the length to jump to the next Node afterwards.

            uint NodeOffset = reader.ReadUInt32(); // Where this Node's Data starts.

            // Jump to the name list node's data.
            reader.JumpTo(NodeOffset, true);

            // Set lists based on input node.
            switch (NodeType)
            {
                case "NXEF":
                    Effects = new NinjaEffect(reader);
                    break;

                case "NXNN":
                    Nodes = new NinjaNodeList(reader);
                    break;

                case "NXOB":
                    Object = new NinjaObject(reader);
                    break;
            }

            // Return to the previously saved position and jump ahead by NodeLength to place us at the next node.
            reader.JumpTo(returnPosition);
            reader.JumpAhead(NodeLength);
        }

        public void ReadNinjaMotion(ExtendedBinaryReader reader)
        {
            // TODO: holy shit rewrite this lol

            // Create the _NinjaMotion so we can write to it.
            Motion = new NinjaMotion();

            string MotionNodeType = new string(reader.ReadChars(4)); // The type of this Motion Node.
            uint NodeLength       = reader.ReadUInt32();                   // The length of this Node.
            long returnPosition   = reader.BaseStream.Position;        // Save position so we can use it with the length to jump to the next Node afterwards.
            uint NodeOffset       = reader.ReadUInt32();                   // Where this Node's Data starts.

            // Jump to the motion node's data.
            reader.JumpTo(NodeOffset, true);

            Motion.Type              = reader.ReadUInt32();
            Motion.StartFrame        = reader.ReadSingle();
            Motion.EndFrame          = reader.ReadSingle();
            uint SubMotionCount      = reader.ReadUInt32();
            uint SubMotionListOffset = reader.ReadUInt32();
            Motion.Framerate         = reader.ReadSingle();
            Motion.Reserved0         = reader.ReadUInt32();
            Motion.Reserved1         = reader.ReadUInt32();

            // Sub Motions
            reader.JumpTo(SubMotionListOffset, true);

            for (int i = 0; i < SubMotionCount; i++)
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
                for (int k = 0; k < KeyFrameCount; k++)
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

                Motion.SubMotions.Add(_NinjaSubMotion);
            }

            // Return to the previously saved position and jump ahead by NodeLength to place us at the next node.
            reader.JumpTo(returnPosition);
            reader.JumpAhead(NodeLength);
        }

        public unsafe override void Save(Stream fileStream)
        {
            // TODO: everything.
            // Main difficulty (to me) will be sizes and offsets (especially the NOF0 chunk), as XNO uses offsets to an EARLIER part of the file quite a lot.
            ExtendedBinaryWriter writer = new ExtendedBinaryWriter(fileStream) { Offset = 0x20 };

            // NXIF Chunk
            writer.Write("NXIF");
            writer.Write(0x18);

            // Calculate how many nodes this Ninja file will have.
            int NodeCount = 0;
            if (Textures != null) { NodeCount++; }
            if (Effects  != null) { NodeCount++; }
            if (Nodes    != null) { NodeCount++; }
            if (Object   != null) { NodeCount++; }
            if (Motion   != null) { NodeCount++; }

            writer.Write(NodeCount);
            writer.Write(0x20);
            long NXIFSizePosition = writer.BaseStream.Position;
            writer.Write("SIZE"); // The complete size of all of the "data chunks" in the file combined, including padding. TODO: Figure out how to write this
            writer.AddOffset("NOF0Offset");
            long NOF0SizePosition = writer.BaseStream.Position;
            writer.Write("SIZE"); //Size of the NOF0 chunk, including its header and, optionally, the padding at the end of the chunk. TODO: Figure out how to write this
            writer.Write(1);

            // NXTL Chunk
            if (Textures != null)
            {
                // Chunk Header
                writer.Write("NXTL");
                long NXTLSizePosition = writer.BaseStream.Position;
                writer.Write("SIZE"); // Placeholder value for the size, calculated after the data is written.
                writer.AddOffset("NXTLDataOffset");
                writer.Write(0);

                // Data
                writer.FillInOffset("NXTLDataOffset", true);
                writer.Write(Textures.Count);
                writer.AddOffset("TexturesOffset");

                // Texture Data
                writer.FillInOffset("TexturesOffset", true, false);
                for (int i = 0; i < Textures.Count; i++)
                {
                    writer.Write((uint)Textures[i].Type);
                    writer.AddOffset($"Texture{i}NameOffset");
                    writer.Write((ushort)Textures[i].MinFilter);
                    writer.Write((ushort)Textures[i].MagFilter);
                    writer.Write(Textures[i].GlobalIndex);
                    writer.Write(Textures[i].Bank);
                }

                // Texture Names
                for (int i = 0; i < Textures.Count; i++)
                {
                    writer.FillInOffset($"Texture{i}NameOffset", true, false);
                    writer.WriteNullTerminatedString(Textures[i].Name);
                }
                writer.FixPadding(0x10);

                // Calculate chunk size.
                long Position = writer.BaseStream.Position;
                writer.BaseStream.Position = NXTLSizePosition;
                writer.Write((uint)(Position - NXTLSizePosition - 4));
                writer.BaseStream.Position = Position;
            }

            // NXEF Chunk
            if (Effects != null)
            {
                // Chunk Header
                writer.Write("NXEF");
                long NXEFSizePosition = writer.BaseStream.Position;
                writer.Write("SIZE"); // Placeholder value for the size, calculated after the data is written.
                writer.AddOffset("NXEFDataOffset");
                writer.Write(0);

                // Data
                writer.FillInOffset("NXEFDataOffset", true);
                writer.Write(Effects.Type);
                writer.Write(Effects.EffectFiles.Count);
                writer.AddOffset("EffectFilesOffset");
                writer.Write(Effects.EffectTechniques.Count);
                writer.AddOffset("EffectTechniquesOffset");
                writer.Write(Effects.TechniqueIndices.Count);
                writer.AddOffset("TechniqueIndicesOffset");

                // Effect Files
                writer.FillInOffset("EffectFilesOffset", true, false);
                for (int i = 0; i < Effects.EffectFiles.Count; i++)
                {
                    writer.Write(Effects.EffectFiles[i].Type);
                    writer.AddOffset($"EffectFile{i}NameOffset");
                }

                // Effect Techniques
                writer.FillInOffset("EffectTechniquesOffset", true, false);
                for (int i = 0; i < Effects.EffectTechniques.Count; i++)
                {
                    writer.Write(Effects.EffectTechniques[i].Type);
                    writer.Write(Effects.EffectTechniques[i].Index); // Can we just write i here?
                    writer.AddOffset($"EffectTechniques{i}NameOffset");
                }

                // Technique Indicies
                writer.FillInOffset("TechniqueIndicesOffset", true, false);
                for (int i = 0; i < Effects.TechniqueIndices.Count; i++)
                {
                    writer.Write(Effects.TechniqueIndices[i]);
                }

                // Effect File Names
                for (int i = 0; i < Effects.EffectFiles.Count; i++)
                {
                    writer.FillInOffset($"EffectFile{i}NameOffset", true, false);
                    writer.WriteNullTerminatedString(Effects.EffectFiles[i].Name);
                }

                // Effect Technique Names
                for (int i = 0; i < Effects.EffectTechniques.Count; i++)
                {
                    writer.FillInOffset($"EffectTechniques{i}NameOffset", true, false);
                    writer.WriteNullTerminatedString(Effects.EffectTechniques[i].Name);
                }
                writer.FixPadding(0x10);

                // Calculate chunk size.
                long Position = writer.BaseStream.Position;
                writer.BaseStream.Position = NXEFSizePosition;
                writer.Write((uint)(Position - NXEFSizePosition - 4));
                writer.BaseStream.Position = Position;
            }

            // NXNN Chunk
            if (Nodes != null)
            {
                // Chunk Header
                writer.Write("NXNN");
                long NXNNSizePosition = writer.BaseStream.Position;
                writer.Write("SIZE"); // Placeholder value for the size, calculated after the data is written.
                writer.AddOffset("NXNNDataOffset");
                writer.Write(0);

                // Data
                writer.FillInOffset("NXNNDataOffset", true);
                writer.Write(Nodes.Type);
                writer.Write(Nodes.Names.Count);
                writer.AddOffset("EntriesOffset");

                // Nodes
                writer.FillInOffset("EntriesOffset", true, false);
                for (int i = 0; i < Nodes.Names.Count; i++)
                {
                    writer.Write(i);
                    writer.AddOffset($"Node{i}NameOffset");
                }

                for (int i = 0; i < Nodes.Names.Count; i++)
                {
                    writer.FillInOffset($"Node{i}NameOffset", true, false);
                    writer.WriteNullTerminatedString(Nodes.Names[i]);
                }
                writer.FixPadding(0x10);

                // Calculate chunk size.
                long Position = writer.BaseStream.Position;
                writer.BaseStream.Position = NXNNSizePosition;
                writer.Write((uint)(Position - NXNNSizePosition - 4));
                writer.BaseStream.Position = Position;
            }

            // NXOB Chunk (This shit doesn't work)
            if (Object != null)
            {
                // Chunk Header
                writer.Write("NXOB");
                long NXOBSizePosition = writer.BaseStream.Position;
                writer.Write("SIZE"); // Placeholder value for the size, calculated after the data is written.
                writer.AddOffset("NXOBDataOffset");
                writer.Write(0);

                // Node List
                foreach (NinjaObjectNode node in Object.Nodes)
                {
                    writer.Write((uint)node.Type);
                    writer.Write(node.Matrix);
                    writer.Write(node.Parent);
                    writer.Write(node.Child);
                    writer.Write(node.Sibling);
                    writer.Write(node.Transform);
                    writer.Write(node.Rotation);
                    writer.Write(node.Scale);
                    foreach (float value in node.InvinitMatrix)
                    {
                        writer.Write(value);
                    }
                    writer.Write(node.Center);
                    writer.Write(node.Radius);
                    writer.Write(node.UserDefined);
                    writer.Write(node.BoundingBox);
                }

                // Material Colours (will write more data than the original XNOs as some are able to reuse Material Colours.)
                for (int i = 0; i < Object.Materials.Count; i++)
                {
                    writer.Write(Object.Materials[i].Colour.Diffuse);
                    writer.Write(Object.Materials[i].Colour.Ambient);
                    writer.Write(Object.Materials[i].Colour.Specular);
                    writer.Write(Object.Materials[i].Colour.Emissive);
                    writer.Write(Object.Materials[i].Colour.Power);
                    writer.FixPadding(0x10);
                }

                // Material Logic (will write more data than the original XNOs as some are able to reuse Material Logics.)
                for (int i = 0; i < Object.Materials.Count; i++)
                {
                    writer.WriteBoolean32(Object.Materials[i].Logic.BlendEnable);
                    writer.Write(Object.Materials[i].Logic.SRCBlend);
                    writer.Write(Object.Materials[i].Logic.DSTBlend);
                    writer.Write(Object.Materials[i].Logic.BlendFactor);
                    writer.Write((uint)Object.Materials[i].Logic.BlendOP);
                    writer.Write((uint)Object.Materials[i].Logic.LogicOP);
                    writer.WriteBoolean32(Object.Materials[i].Logic.AlphaEnable);
                    writer.Write((uint)Object.Materials[i].Logic.AlphaFunction);
                    writer.Write(Object.Materials[i].Logic.AlphaRef);
                    writer.WriteBoolean32(Object.Materials[i].Logic.ZCompEnable);
                    writer.Write((uint)Object.Materials[i].Logic.ZFunction);
                    writer.WriteBoolean32(Object.Materials[i].Logic.ZUpdateEnable);
                    writer.FixPadding(0x10);
                }

                // Material Texture Maps.
                for (int i = 0; i < Object.Materials.Count; i++)
                {
                    foreach (NinjaObjectMaterialTexture texture in Object.Materials[i].Textures)
                    {
                        writer.Write(texture.Type);
                        writer.Write(texture.ID);
                        writer.Write(texture.Offset);
                        writer.Write(texture.Blend);
                        writer.Write(texture.InfoOffset);
                        writer.Write((ushort)texture.MinFilter);
                        writer.Write((ushort)texture.MagFilter);
                        writer.Write(texture.MipMapBias);
                        writer.Write(texture.MaxMipMapLevel);
                        writer.FixPadding(0x10);
                    }
                }

                // Material Descriptions
                foreach (NinjaObjectMaterial material in Object.Materials)
                {
                    writer.Write(material.Description_Flag);
                    writer.Write(material.Description_UserDefined);
                    writer.Write("OFST"); // Colour Offset
                    writer.Write("OFST"); // Logic Offset
                    writer.Write("OFST"); // Texture Offset
                    writer.FixPadding(0x10);
                }

                // Material List Pointers
                foreach (NinjaObjectMaterial material in Object.Materials)
                {
                    writer.Write((uint)material.Type);
                    writer.Write("OFST"); // Material Offset
                }

                // Vertex Descriptions
                foreach (NinjaObjectVertex vertexDesc in Object.Vertices)
                {
                    foreach(int BoneMatrix in vertexDesc.BoneMatrix)
                    {
                        writer.Write(BoneMatrix);
                    }
                    writer.Write((uint)vertexDesc.Description_Format);
                    writer.Write((uint)vertexDesc.Description_FVF);
                    writer.Write(vertexDesc.Description_Stride);
                    writer.Write(vertexDesc.VertexList.Count);
                    writer.Write("OFST"); // Vertex List Offset
                    writer.Write(vertexDesc.BoneMatrix.Count);
                    writer.Write("OFST"); // Bone Matrix Offset
                    writer.Write(vertexDesc.Description_HDRCommon);
                    writer.Write(vertexDesc.Description_HDRData);
                    writer.Write(vertexDesc.Description_HDRLock);
                    writer.FixPadding(0x10);
                }

                // Vertex List Pointer
                foreach (NinjaObjectVertex vertexDesc in Object.Vertices)
                {
                    writer.Write((uint)vertexDesc.Type);
                    writer.Write("OFST"); // Vertex Description Offset
                }

                // Primitive Lengths and Indices
                foreach (NinjaPrimitive prim in Object.Primitives)
                {
                    writer.Write((ushort)prim.SList_Indices.Count);
                    writer.FixPadding(0x4);
                    foreach(ushort index in prim.SList_Indices)
                    {
                        writer.Write(index);
                    }
                    writer.FixPadding(0x4);
                }


            }






            /*if (Object != null)
            {
                // Chunk Header
                writer.Write("NXOB");
                long NXOBSizePosition = writer.BaseStream.Position;
                writer.Write("SIZE"); // Placeholder value for the size, calculated after the data is written.
                writer.AddOffset("NXOBDataOffset");
                writer.Write(0);

                // Data
                writer.FillInOffset("NXOBDataOffset", true);
                writer.Write(Object.Center);
                writer.Write(Object.Radius);
                writer.Write(Object.Materials.Count);
                writer.AddOffset("ObjectMaterialsOffset");
                writer.Write(Object.Vertices.Count);
                writer.AddOffset("ObjectVerticesOffset");
                writer.Write(Object.Primitives.Count);
                writer.AddOffset("ObjectPrimitivesOffset");
                writer.Write(Object.Nodes.Count);
                writer.Write(Object.MaxNodeDepth);
                writer.AddOffset("ObjectNodesOffset");
                writer.Write(Object.MatrixPal);
                writer.Write(Object.SubObjects.Count);
                writer.AddOffset("SubObjectsOffset");
                writer.Write(Object.TextureCount);
                writer.FixPadding(0x10);

                #region Materials
                writer.FillInOffset("ObjectMaterialsOffset", true, false);
                for (int i = 0; i < Object.Materials.Count; i++)
                {
                    writer.Write((uint)Object.Materials[i].Type);
                    writer.AddOffset($"Material{i}DescriptionOffset");
                }

                for (int i = 0; i < Object.Materials.Count; i++)
                {
                    writer.FillInOffset($"Material{i}DescriptionOffset", true, false);
                    writer.Write(Object.Materials[i].Description_Flag);
                    writer.Write(Object.Materials[i].Description_UserDefined);
                    writer.AddOffset($"Material{i}ColourOffset");
                    writer.AddOffset($"Material{i}LogicOffset");
                    writer.AddOffset($"Material{i}TextureDescriptionOffset");
                    writer.FixPadding(0x10);
                }

                for (int i = 0; i < Object.Materials.Count; i++)
                {
                    writer.FillInOffset($"Material{i}ColourOffset", true, false);
                    writer.Write(Object.Materials[i].Colour_Diffuse);
                    writer.Write(Object.Materials[i].Colour_Ambient);
                    writer.Write(Object.Materials[i].Colour_Specular);
                    writer.Write(Object.Materials[i].Colour_Emissive);
                    writer.Write(Object.Materials[i].Colour_Power);
                    writer.FixPadding(0x10);
                }

                for (int i = 0; i < Object.Materials.Count; i++)
                {
                    writer.FillInOffset($"Material{i}LogicOffset", true, false);
                    writer.Write(Convert.ToUInt32(Object.Materials[i].Logic_BlendEnable));
                    writer.Write(Object.Materials[i].Logic_SRCBlend);
                    writer.Write(Object.Materials[i].Logic_DSTBlend);
                    writer.Write(Object.Materials[i].Logic_BlendFactor);
                    writer.Write((uint)Object.Materials[i].Logic_BlendOP);
                    writer.Write((uint)Object.Materials[i].Logic_LogicOP);
                    writer.Write(Convert.ToUInt32(Object.Materials[i].Logic_AlphaEnable));
                    writer.Write((uint)Object.Materials[i].Logic_AlphaFunction);
                    writer.Write(Object.Materials[i].Logic_AlphaRef);
                    writer.Write(Convert.ToUInt32(Object.Materials[i].Logic_ZCompEnable));
                    writer.Write((uint)Object.Materials[i].Logic_ZFunction);
                    writer.Write(Convert.ToUInt32(Object.Materials[i].Logic_ZUpdateEnable));
                    writer.FixPadding(0x10);
                }

                for (int i = 0; i < Object.Materials.Count; i++)
                {
                    writer.FillInOffset($"Material{i}TextureDescriptionOffset", true, false);
                    writer.Write(Object.Materials[i].Texture_Type);
                    writer.Write(Object.Materials[i].Texture_ID);
                    writer.Write(Object.Materials[i].Texture_Offset);
                    writer.Write(Object.Materials[i].Texture_Blend);
                    writer.Write(Object.Materials[i].Texture_InfoOffset);
                    writer.Write(Object.Materials[i].Texture_MinFilter);
                    writer.Write(Object.Materials[i].Texture_MagFilter);
                    writer.Write(Object.Materials[i].Texture_MipMapBias);
                    writer.Write(Object.Materials[i].Texture_MaxMipMapLevel);
                    writer.FixPadding(0x10);
                }
                #endregion

                #region Verticies
                writer.FillInOffset("ObjectVerticesOffset", true, false);
                for (int i = 0; i < Object.Vertices.Count; i++)
                {
                    writer.Write((uint)Object.Vertices[i].Type);
                    writer.AddOffset($"Vertices{i}Offset");
                }

                for (int i = 0; i < Object.Vertices.Count; i++)
                {
                    writer.FillInOffset($"Vertices{i}Offset", true, false);
                    writer.Write((uint)Object.Vertices[i].Description_Format);
                    writer.Write((uint)Object.Vertices[i].Description_FVF);
                    writer.Write(Object.Vertices[i].Description_Stride); // TODO: Should probably calculate this from the Format?
                    writer.Write(Object.Vertices[i].VertexList.Count);
                    writer.AddOffset($"Vertices{i}VertexList");
                    writer.Write(Object.Vertices[i].BoneMatrix.Count);
                    writer.AddOffset($"Vertices{i}BoneMatrix");
                    writer.Write(Object.Vertices[i].Description_HDRCommon);
                    writer.Write(Object.Vertices[i].Description_HDRData);
                    writer.Write(Object.Vertices[i].Description_HDRLock);
                    writer.FixPadding(0x10);
                }

                for (int i = 0; i < Object.Vertices.Count; i++)
                {
                    writer.FillInOffset($"Vertices{i}VertexList", true, false);
                    for (int v = 0; v < Object.Vertices[i].VertexList.Count; v++)
                    {
                        if (Object.Vertices[i].Description_Format.HasFlag(NinjaObjectVertexType.NND_VTXTYPE_XB_POSITION)) { writer.Write(Object.Vertices[i].VertexList[v].List_Position); }
                        if (Object.Vertices[i].Description_Format.HasFlag(NinjaObjectVertexType.NND_VTXTYPE_XB_WEIGHT3)) { writer.Write(Object.Vertices[i].VertexList[v].List_Weight); }
                        if (Object.Vertices[i].Description_Format.HasFlag(NinjaObjectVertexType.NND_VTXTYPE_XB_MTX_INDEX4))
                        {
                            writer.Write(Object.Vertices[i].VertexList[v].List_BoneMatrix[0]);
                            writer.Write(Object.Vertices[i].VertexList[v].List_BoneMatrix[1]);
                            writer.Write(Object.Vertices[i].VertexList[v].List_BoneMatrix[2]);
                            writer.Write(Object.Vertices[i].VertexList[v].List_BoneMatrix[3]);
                        }
                        if (Object.Vertices[i].Description_Format.HasFlag(NinjaObjectVertexType.NND_VTXTYPE_XB_NORMAL)) { writer.Write(Object.Vertices[i].VertexList[v].List_Normals); }
                        if (Object.Vertices[i].Description_Format.HasFlag(NinjaObjectVertexType.NND_VTXTYPE_XB_COLOR))
                        {
                            writer.Write(Object.Vertices[i].VertexList[v].List_VertexColour[0]);
                            writer.Write(Object.Vertices[i].VertexList[v].List_VertexColour[1]);
                            writer.Write(Object.Vertices[i].VertexList[v].List_VertexColour[2]);
                            writer.Write(Object.Vertices[i].VertexList[v].List_VertexColour[3]);
                        }
                        for (int t = 0; t < ((uint)Object.Vertices[i].Description_Format / (uint)NinjaObjectVertexType.NND_VTXTYPE_XB_TEXCOORD); t++)
                        {
                            writer.Write(Object.Vertices[i].VertexList[v].List_TextureCoordinates[t]);
                        }
                        if (Object.Vertices[i].Description_Format.HasFlag(NinjaObjectVertexType.NND_VTXTYPE_XB_TANGENT)) { writer.Write(Object.Vertices[i].VertexList[v].List_Tangent); }
                        if (Object.Vertices[i].Description_Format.HasFlag(NinjaObjectVertexType.NND_VTXTYPE_XB_BINORMAL)) { writer.Write(Object.Vertices[i].VertexList[v].List_Binormal); }
                    }
                    writer.FixPadding(0x10);
                }

                for (int i = 0; i < Object.Vertices.Count; i++)
                {
                    writer.FillInOffset($"Vertices{i}BoneMatrix", true, false);
                    for (int v = 0; v < Object.Vertices[i].BoneMatrix.Count; v++)
                    {
                        writer.Write(Object.Vertices[i].BoneMatrix[v]);
                    }
                    writer.FixPadding(0x10);
                }
                #endregion

                #region Primitives
                writer.FillInOffset("ObjectPrimitivesOffset", true, false);
                for (int i = 0; i < Object.Primitives.Count; i++)
                {
                    writer.Write((uint)Object.Primitives[i].Type);
                    writer.AddOffset($"Primitives{i}Offset");
                }

                for (int i = 0; i < Object.Primitives.Count; i++)
                {
                    writer.FillInOffset($"Primitives{i}Offset", true, false);
                    writer.Write(Object.Primitives[i].Format);
                    writer.Write(Object.Primitives[i].SList_Indices.Count);
                    writer.Write(1); // This is probably wrong.
                    writer.AddOffset($"Primitives{i}LengthOffset");
                    writer.AddOffset($"Primitives{i}IndexOffset");
                    writer.Write(0); // This is probably wrong.
                    writer.FixPadding(0x10);
                }

                for (int i = 0; i < Object.Primitives.Count; i++)
                {
                    writer.FillInOffset($"Primitives{i}LengthOffset", true, false);
                    writer.Write((ushort)Object.Primitives[i].SList_Indices.Count);
                    writer.FixPadding(0x10);
                }

                for (int i = 0; i < Object.Primitives.Count; i++)
                {
                    writer.FillInOffset($"Primitives{i}IndexOffset", true, false);
                    foreach (ushort face in Object.Primitives[i].SList_Indices)
                    {
                        writer.Write(face);
                    }
                    writer.FixPadding(0x10);
                }
                #endregion

                #region Nodes
                writer.FillInOffset("ObjectNodesOffset", true, false);
                for (int i = 0; i < Object.Nodes.Count; i++)
                {
                    writer.Write((uint)Object.Nodes[i].Type);
                    writer.Write(Object.Nodes[i].Matrix);
                    writer.Write(Object.Nodes[i].Parent);
                    writer.Write(Object.Nodes[i].Child);
                    writer.Write(Object.Nodes[i].Sibling);
                    writer.Write(Object.Nodes[i].Transform);
                    writer.Write(Object.Nodes[i].Rotation);
                    writer.Write(Object.Nodes[i].Scale);
                    foreach (float value in Object.Nodes[i].InvinitMatrix)
                    {
                        writer.Write(value);
                    }
                    writer.Write(Object.Nodes[i].Center);
                    writer.Write(Object.Nodes[i].Radius);
                    writer.Write(Object.Nodes[i].UserDefined);
                    writer.Write(Object.Nodes[i].BoundingBox);
                    writer.FixPadding(0x10);
                }
                #endregion

                #region SubObjects
                writer.FillInOffset("SubObjectsOffset", true, false);
                for (int i = 0; i < Object.SubObjects.Count; i++)
                {
                    writer.Write((uint)Object.SubObjects[i].Type);
                    writer.Write(Object.SubObjects[i].MeshSets.Count);
                    writer.AddOffset($"SubObjects{i}MeshSetOffset");
                    writer.Write(Object.SubObjects[i].TextureList.Count);
                    writer.AddOffset($"SubObjects{i}TextureListOffset");
                }

                for (int i = 0; i < Object.SubObjects.Count; i++)
                {
                    writer.FillInOffset($"SubObjects{i}MeshSetOffset", true, false);
                    for(int s = 0; s < Object.SubObjects[i].MeshSets.Count; s++)
                    {
                        writer.Write(Object.SubObjects[i].MeshSets[s].Center);
                        writer.Write(Object.SubObjects[i].MeshSets[s].Radius);
                        writer.Write(Object.SubObjects[i].MeshSets[s].NodeIndex);
                        writer.Write(Object.SubObjects[i].MeshSets[s].Matrix);
                        writer.Write(Object.SubObjects[i].MeshSets[s].MaterialIndex);
                        writer.Write(Object.SubObjects[i].MeshSets[s].VertexIndex);
                        writer.Write(Object.SubObjects[i].MeshSets[s].PrimitiveIndex);
                        writer.Write(Object.SubObjects[i].MeshSets[s].ShaderIndex);
                    }
                    writer.FixPadding(0x10);
                }

                for (int i = 0; i < Object.SubObjects.Count; i++)
                {
                    writer.FillInOffset($"SubObjects{i}TextureListOffset", true, false);
                    for (int s = 0; s < Object.SubObjects[i].TextureList.Count; s++)
                    {
                        writer.Write(Object.SubObjects[i].TextureList[s]);
                    }
                    writer.FixPadding(0x10);
                }

                writer.FixPadding(0x10);
                #endregion

                // Calculate chunk size.
                long Position = writer.BaseStream.Position;
                writer.BaseStream.Position = NXOBSizePosition;
                writer.Write((uint)(Position - NXOBSizePosition - 4));
                writer.BaseStream.Position = Position;
            }*/

            // NOF0 Chunk
            // Calculate data chunk sizes.
            long N0FOPosition = writer.BaseStream.Position;
            writer.BaseStream.Position = NXIFSizePosition;
            writer.Write((uint)(N0FOPosition - NXIFSizePosition - 0x10));
            writer.BaseStream.Position = N0FOPosition;

            writer.FillInOffset("NOF0Offset");
            List<uint> Offsets = writer.GetOffsets();
            writer.Write("NOF0");
            long NOF0SizePosition2 = writer.BaseStream.Position;
            writer.Write("SIZE"); // Placeholder value for the size, calculated after the data is written.
            writer.Write(Offsets.Count); // The number of offsets listed in the array contained within this chunk.
            writer.Write(0);
            foreach(uint Offset in Offsets)
            {
                writer.Write(Offset - 0x20);
            }
            writer.FixPadding(0x10);

            // Calculate chunk size.
            N0FOPosition = writer.BaseStream.Position;
            writer.BaseStream.Position = NOF0SizePosition2;
            writer.Write((uint)(N0FOPosition - NOF0SizePosition2 - 4));
            writer.BaseStream.Position = NOF0SizePosition;
            writer.Write((uint)(N0FOPosition - NOF0SizePosition2 + 4));
            writer.BaseStream.Position = N0FOPosition;

            // NFN0 Chunk
            writer.Write("NFN0");
            long NFN0SizePosition = writer.BaseStream.Position;
            writer.Write("SIZE"); // Placeholder value for the size, calculated after the data is written.
            writer.Write(0);
            writer.Write(0);
            writer.WriteNullTerminatedString("en_Kyozoress.xno");
            writer.FixPadding(0x10);

            // Calculate chunk size.
            long NFN0Position = writer.BaseStream.Position;
            writer.BaseStream.Position = NFN0SizePosition;
            writer.Write((uint)(NFN0Position - NFN0SizePosition - 4));
            writer.BaseStream.Position = NFN0Position;

            // NEND Chunk
            writer.Write("NEND");
            writer.Write(8);
            writer.Write(0);
            writer.Write(0);
        }

        public void ReadNOF0(string file)
        {
            ExtendedBinaryReader reader = new ExtendedBinaryReader(File.OpenRead(file)) { Offset = 0x20 };

            // Use info node as file magic.
            string InfoNodeType = new string(reader.ReadChars(4)); // The Type of this Info Node.
            if (InfoNodeType != "NXIF")
                throw new InvalidSignatureException("NXIF", InfoNodeType); // Only supporting XN* (for now?).

            uint NodeLength = reader.ReadUInt32(); // The length of the Info Node.
            uint DataChunkCount = reader.ReadUInt32(); // How many other Nodes make up this NN file.
            uint DataChunkOffset = reader.ReadUInt32(); // Seems to always be 0x20 (at least in XN*s)? Offset amount?
            uint DataSize = reader.ReadUInt32(); // Footer Offset Table Start?
            uint OffsetTableOffset = reader.ReadUInt32(); // Footer Offset Table Data Start?
            uint OffsetTableSize = reader.ReadUInt32(); // Footer Offset Table Length?
            uint Version = reader.ReadUInt32(); // Seems to always be 1 (at least in XN*s)?

            reader.JumpTo(OffsetTableOffset);
            reader.JumpAhead(8);
            uint count = reader.ReadUInt32();
            reader.JumpAhead(4);
            for(int i = 0; i < count; i++)
            {
                long pos = reader.BaseStream.Position;
                reader.JumpTo(reader.ReadUInt32(), true);
                Console.WriteLine((reader.ReadUInt32() + 0x20).ToString("X"));
                reader.JumpTo(pos + 0x4);
            }
        }
    }
}
