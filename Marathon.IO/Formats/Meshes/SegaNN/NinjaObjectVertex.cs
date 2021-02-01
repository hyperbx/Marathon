// NinjaObjectVertex.cs is licensed under the MIT License:
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

using System.Collections.Generic;

namespace Marathon.IO.Formats.Meshes.SegaNN
{
    public class NinjaObjectVertex
    {
        public NinjaObjectVertexDescriptionType Type { get; set; }

        /* ---------- NNS_VTXLIST_DX_DESC Values ---------- */

        public NinjaObjectVertexType Description_Format { get; set; }

        public NinjaObjectVertexDirect3DType Description_FVF { get; set; } // TODO: Find out what the FVF part means.

        public uint Description_Stride { get; set; }

        public uint Description_VertexCount { get; set; }

        public uint Description_VertexOffset { get; set; }

        public uint Description_BoneCount { get; set; }

        public uint Description_BoneMatrixOffset { get; set; }

        public uint Description_HDRCommon { get; set; }

        public uint Description_HDRData { get; set; }

        public uint Description_HDRLock { get; set; }

        public List<NinjaObjectVertexXenon> VertexList = new List<NinjaObjectVertexXenon>();

        public List<int> BoneMatrix = new List<int>();

        public NinjaObjectVertex(ExtendedBinaryReader reader)
        {
            Type = (NinjaObjectVertexDescriptionType)reader.ReadUInt32(); // This vertex's type.
            uint VertexDescriptionOffset = reader.ReadUInt32(); // The offset to this vertex's NNS_VTXLIST_DX_DESC chunk..

            long position = reader.BaseStream.Position; // Save position so we can use it to jump back and read the next vertex after we've read all the data for this one.

            // NNS_VTXLIST_DX_DESC chunk
            // Jump to this vertex's description chunk.
            reader.JumpTo(VertexDescriptionOffset, true);

            Description_Format = (NinjaObjectVertexType)reader.ReadUInt32(); // The format of this vertex.
            Description_FVF = (NinjaObjectVertexDirect3DType)reader.ReadUInt32();

            uint VertexDataSize = reader.ReadUInt32(); // How many bytes each bit of data for this vertex takes up.
            uint VertexDataCount = reader.ReadUInt32(); // How many bits of data make up this vertex.
            uint VertexDataOffset = reader.ReadUInt32(); // The offset to this vertex's data.
            uint BoneMatrixCount = reader.ReadUInt32(); // How any nodes makde up this vertex's bone matrix.
            uint BoneMatrixOffset = reader.ReadUInt32(); // The offset to this vertex's bone matrix.

            Description_HDRCommon = reader.ReadUInt32();
            Description_HDRData = reader.ReadUInt32();
            Description_HDRLock = reader.ReadUInt32();

            // NNS_VTXTYPE_XB_??? Chunk.
            if (VertexDataOffset != 0)
            {
                // Jump to this vertex's data.
                reader.JumpTo(VertexDataOffset, true);
                for (int v = 0; v < VertexDataCount; v++)
                {
                    long vertexPosition = reader.BaseStream.Position + VertexDataSize;
                    NinjaObjectVertexXenon _NinjaObjectVertexXB = new NinjaObjectVertexXenon();

                    // Position
                    if (Description_Format.HasFlag(NinjaObjectVertexType.NND_VTXTYPE_XB_POSITION))
                    {
                        _NinjaObjectVertexXB.List_Position = reader.ReadVector3();
                    }

                    // Weight
                    if (Description_Format.HasFlag(NinjaObjectVertexType.NND_VTXTYPE_XB_WEIGHT3))
                    {
                        _NinjaObjectVertexXB.List_Weight = reader.ReadVector3();
                    }

                    // Bone Matrix
                    if (Description_Format.HasFlag(NinjaObjectVertexType.NND_VTXTYPE_XB_MTX_INDEX4))
                    {
                        _NinjaObjectVertexXB.List_BoneMatrix.Add(reader.ReadByte());
                        _NinjaObjectVertexXB.List_BoneMatrix.Add(reader.ReadByte());
                        _NinjaObjectVertexXB.List_BoneMatrix.Add(reader.ReadByte());
                        _NinjaObjectVertexXB.List_BoneMatrix.Add(reader.ReadByte());
                    }

                    // Normals
                    if (Description_Format.HasFlag(NinjaObjectVertexType.NND_VTXTYPE_XB_NORMAL))
                    {
                        _NinjaObjectVertexXB.List_Normals = reader.ReadVector3();
                    }

                    // Vertex Colours
                    if (Description_Format.HasFlag(NinjaObjectVertexType.NND_VTXTYPE_XB_COLOR))
                    {
                        _NinjaObjectVertexXB.List_VertexColour.Add(reader.ReadByte());
                        _NinjaObjectVertexXB.List_VertexColour.Add(reader.ReadByte());
                        _NinjaObjectVertexXB.List_VertexColour.Add(reader.ReadByte());
                        _NinjaObjectVertexXB.List_VertexColour.Add(reader.ReadByte());
                    }

                    // Texture Coordinates
                    for (int t = 0; t < ((uint)Description_Format / (uint)NinjaObjectVertexType.NND_VTXTYPE_XB_TEXCOORD); t++)
                    {
                        _NinjaObjectVertexXB.List_TextureCoordinates.Add(reader.ReadVector2());
                    }

                    // Tangent
                    if (Description_Format.HasFlag(NinjaObjectVertexType.NND_VTXTYPE_XB_TANGENT))
                    {
                        _NinjaObjectVertexXB.List_Tangent = reader.ReadVector3();
                    }

                    // Binormal
                    if (Description_Format.HasFlag(NinjaObjectVertexType.NND_VTXTYPE_XB_BINORMAL))
                    {
                        _NinjaObjectVertexXB.List_Binormal = reader.ReadVector3();
                    }

                    // Save Vertex Entry.
                    VertexList.Add(_NinjaObjectVertexXB);
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
                {
                    BoneMatrix.Add(reader.ReadInt32());
                }
            }

            // Jump back to the saved position to read the next node.
            reader.JumpTo(position);
        }
    }

    public class NinjaObjectVertexXenon
    {
        /* ---------- NNS_VTXTYPE_XB_??? Values ---------- */

        public Vector3 List_Position { get; set; }

        public Vector3 List_Weight { get; set; }

        public List<byte> List_BoneMatrix = new List<byte>(); // Just based on what I'm seeing from the MaxScript and LibS06.

        public Vector3 List_Normals { get; set; }

        public List<byte> List_VertexColour = new List<byte>();

        public List<Vector2> List_TextureCoordinates = new List<Vector2>();

        public Vector3 List_Tangent { get; set; }

        public Vector3 List_Binormal { get; set; }
    }
}
