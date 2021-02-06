// NinjaObject.cs is licensed under the MIT License:
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
    public class NinjaObject
    {
        public Vector3 Center { get; set; }

        public float Radius { get; set; }

        public uint MaterialCount { get; set; }

        public uint MaterialOffset { get; set; }

        public List<NinjaObjectMaterial> Materials = new List<NinjaObjectMaterial>();

        public uint VertexCount { get; set; }

        public uint VertexOffset { get; set; }

        public List<NinjaObjectVertex> Vertices = new List<NinjaObjectVertex>();

        public uint PrimitiveCount { get; set; }

        public uint PrimitiveOffset { get; set; }

        public List<NinjaPrimitive> Primitives = new List<NinjaPrimitive>();

        public uint NodeCount { get; set; }

        public uint MaxNodeDepth { get; set; }

        public uint NodeListOffset { get; set; }

        public List<NinjaObjectNode> Nodes = new List<NinjaObjectNode>();

        public uint MatrixPal; // TODO: Figure out what this stands for. Matrix Something?

        public uint SubObjectCount { get; set; }

        public uint SubObjectOffset { get; set; }

        public List<NinjaSubObject> SubObjects = new List<NinjaSubObject>();

        public uint TextureCount { get; set; }

        public NinjaObject(ExtendedBinaryReader reader)
        {
            // Read object data, counts and pointers.
            Center = reader.ReadVector3(); // The center point of this object.
            Radius = reader.ReadSingle();  // The radius this object takes up?
            MaterialCount = reader.ReadUInt32();  // The amount of materials specified in this object.
            MaterialOffset = reader.ReadUInt32();  // The offset to this object's material list.
            VertexCount = reader.ReadUInt32();  // The amount of verticies specified in this object.
            VertexOffset = reader.ReadUInt32();  // The offset to this object's vertex list.
            PrimitiveCount = reader.ReadUInt32();  // The amount of primatives specified in this object.
            PrimitiveOffset = reader.ReadUInt32();  // The offset to this object's primitive list.
            NodeCount = reader.ReadUInt32();  // The amount of nodes that make up this object.
            MaxNodeDepth = reader.ReadUInt32();  // The deepest level that a node is in a chain in this object.
            NodeListOffset = reader.ReadUInt32();  // The offset to this object's node list.
            MatrixPal = reader.ReadUInt32();  // TODO: Figure out what this stands for and what it does. Matrix Something?
            SubObjectCount = reader.ReadUInt32();  // The amount of sub objects in this object.
            SubObjectOffset = reader.ReadUInt32();  // The offset to this object's sub object list.
            TextureCount = reader.ReadUInt32();  // The amount of textures in this object (should just match up with the amount of entries in _NinjaTextureFileList?).

            #region NNS_MATERIALPTR
            // Jump to the object's material list.
            reader.JumpTo(MaterialOffset, true);

            // Loop through based on amount of materials in this object's material list.
            for (int i = 0; i < MaterialCount; i++)
            {
                // Save this material into the material list.
                Materials.Add(new NinjaObjectMaterial(reader));
            }
            #endregion

            #region NNS_VTXLISTPTR
            // Jump to the object's vertex list.
            reader.JumpTo(VertexOffset, true);

            // Loop through based on amount of verticies in this object's vertex list.
            for (int i = 0; i < VertexCount; i++)
            {
                // Save this vertex into the vertex list.
                Vertices.Add(new NinjaObjectVertex(reader));
            }
            #endregion

            #region NNS_PRIMLISTPTR
            // Jump to the object's primitive list.
            reader.JumpTo(PrimitiveOffset, true);

            // Loop through based on amount of primitive in this object's primitive list.
            for (int i = 0; i < PrimitiveCount; i++)
            {
                // Save this primitive into the primitive list.
                Primitives.Add(new NinjaPrimitive(reader));
            }
            #endregion

            #region NNS_NODE
            // Jump to the object's primitive list.
            reader.JumpTo(NodeListOffset, true);

            // Loop through based on amount of primitive in this object's primitive list.
            for (int i = 0; i < NodeCount; i++)
            {
                // Save this node into the NodeList.
                Nodes.Add(new NinjaObjectNode(reader));
            }
            #endregion

            #region NNS_SUBOBJ
            // Jump to the object's sub object list.
            reader.JumpTo(SubObjectOffset, true);

            // Loop through based on amount of sub objects in this object's sub object list.
            for (int i = 0; i < SubObjectCount; i++)
            {
                // Save this sub object into the SubObjectList in 
                SubObjects.Add(new NinjaSubObject(reader));
            }
            #endregion
        }
    }

    public class NinjaObjectNode
    {
        public NinjaNodeType Type { get; set; }

        public ushort Matrix { get; set; }

        public ushort Parent { get; set; }

        public ushort Child { get; set; }

        public ushort Sibling { get; set; }

        public Vector3 Transform { get; set; }

        public Vector3 Rotation { get; set; }

        public Vector3 Scale { get; set; } // Can only assume that's what SCL stands for.

        public List<float> InvinitMatrix = new List<float>(); // TODO: Find out what Invinit means.

        public Vector3 Center { get; set; }

        public float Radius { get; set; }

        public uint UserDefined { get; set; } // Always 0 in the XTO?

        public Vector3 BoundingBox { get; set; } // The XTO usually has three RSV (reserved?) values here, but two nodes seem to have Bounding Boxes instead.

        public NinjaObjectNode(ExtendedBinaryReader reader)
        {
            Type = (NinjaNodeType)reader.ReadUInt32();  // This node's type.
            Matrix = reader.ReadUInt16();  // This node's matrix reference, set to FFFF for NIL.
            Parent = reader.ReadUInt16();  // This node's parent reference, set to FFFF for NIL.
            Child = reader.ReadUInt16();  // This node's child reference, set to FFFF for NIL.
            Sibling = reader.ReadUInt16();  // This node's sibling reference, set to FFFF for NIL.
            Transform = reader.ReadVector3(); // This node's transform values.
            Rotation = reader.ReadVector3(); // This node's rotation values.
            Scale = reader.ReadVector3(); // This node's scale values.

            // Read matrices.
            for (int m = 0; m < 16; m++)
            {
                // This node's thing..?
                InvinitMatrix.Add(reader.ReadSingle());
            }

            Center = reader.ReadVector3(); // The central point of this node.
            Radius = reader.ReadSingle();  // The radius of this node.
            UserDefined = reader.ReadUInt32();  // This node's user defined data.
            BoundingBox = reader.ReadVector3(); // This node's bounding box/reserved data.
        }
    }

    public class NinjaSubObject
    {
        public NinjaSubObjectType Type { get; set; }

        public List<NinjaSubObjectMeshSet> MeshSets = new List<NinjaSubObjectMeshSet>();

        public List<int> TextureList = new List<int>();

        public NinjaSubObject(ExtendedBinaryReader reader)
        {
            Type = (NinjaSubObjectType)reader.ReadUInt32(); // This sub object's type.
            uint MeshSetCount = reader.ReadUInt32();
            uint MeshSetOffset = reader.ReadUInt32();
            uint SubObjectTextureCount = reader.ReadUInt32();
            uint SubObjectTextureListOffset = reader.ReadUInt32();
            long position = reader.BaseStream.Position; // Save position so we can use it to jump back and read the next sub object after we've read all the data for this one.

            // NNS_MESHSET Chunk.
            // Jump to this sub object's mesh set data.
            reader.JumpTo(MeshSetOffset, true);

            for (int m = 0; m < MeshSetCount; m++)
            {
                NinjaSubObjectMeshSet _NinjaSubObjectMeshSet = new NinjaSubObjectMeshSet
                {
                    Center = reader.ReadVector3(),
                    Radius = reader.ReadSingle(),
                    NodeIndex = reader.ReadUInt32(),
                    Matrix = reader.ReadUInt32(),
                    MaterialIndex = reader.ReadUInt32(),
                    VertexIndex = reader.ReadUInt32(),
                    PrimitiveIndex = reader.ReadUInt32(),
                    ShaderIndex = reader.ReadUInt32()
                };

                MeshSets.Add(_NinjaSubObjectMeshSet);
            }

            // Jump to this sub object's texture list offset.
            reader.JumpTo(SubObjectTextureListOffset, true);

            // Loop through based on amount of entries in this sub object's Texture List and save them into the TextureList in the _NinjaSubObject.
            for (int t = 0; t < SubObjectTextureCount; t++)
            {
                TextureList.Add(reader.ReadInt32());
            }

            // Jump back to the saved position to read the next node.
            reader.JumpTo(position);
        }
    }

    public class NinjaSubObjectMeshSet
    {
        /* ---------- NNS_MESHSET Values ---------- */

        public Vector3 Center { get; set; }

        public float Radius { get; set; }

        public uint NodeIndex { get; set; }

        public uint Matrix { get; set; }

        public uint MaterialIndex { get; set; }

        public uint VertexIndex { get; set; }

        public uint PrimitiveIndex { get; set; }

        public uint ShaderIndex { get; set; }
    }
}
