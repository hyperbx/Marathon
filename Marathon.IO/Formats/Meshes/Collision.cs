// Collision.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2021 Knuxfan24
 * Copyright (c) 2021 HyperBE32
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
using Marathon.IO.Headers;
using Assimp;
using Assimp.Configs;

namespace Marathon.IO.Formats.Meshes
{
    /// <summary>
    /// <para>File base for a BIN format.</para>
    /// <para>Used in SONIC THE HEDGEHOG for collision meshes.</para>
    /// </summary>
    public class Collision : FileBase
    {
        public Collision() { }

        public Collision(string file)
        {
            switch (Path.GetExtension(file))
            {
                case ".fbx":
                case ".obj":
                    ImportAssimp(file);
                    break;

                default:
                    Load(file);
                    break;
            }
        }

        /// <summary>
        /// Collision flags as enumerators.
        /// TODO: needs more research before use.
        /// </summary>
        [Flags]
        public enum CollisionFlag : uint
        {
            /* Sound Flags */
            Concrete  = 0,
            Concrete4 = 4,
            Concrete7 = 7,
            ConcreteB = 0xB,
            ConcreteC = 0xC,
            ConcreteD = 0xD,
            ConcreteF = 0xF,

            Pool      = 1,
            Wood      = 2,
            Metal     = 3,
            Grass     = 5,
            Sand      = 6,
            Snow      = 8,
            Dirt      = 9,
            Glass     = 0xA,
            MetalEcho = 0xE,

            /* Collision Types */
            Wall    = 0x10000,
            NoStand = 0x40000,

            Water         = 0x80000,
            Water40000000 = 0x40000000,

            Death            = 0x100000,
            PlayerOnly       = 0x200000,
            CameraOnly       = 0x4000000,
            Tentative        = 0x10000000,
            HazardousCorners = 0x20000000,
            HazardousTerrain = 0x28000000,
            HazardousWater   = 0x60000000,
            Climbable        = 0x80000000
        }

        /// <summary>
        /// Standard face type, but with collision flags.
        /// </summary>
        public class CollisionFace : Face
        {
            public uint Flags;
        }

        public const string Extension = ".bin";

        public List<Vector3> Vertices = new List<Vector3>();
        public List<CollisionFace> Faces = new List<CollisionFace>();

        public override void Load(Stream fileStream)
        {
            BINAReader reader = new BINAReader(fileStream);
            reader.ReadHeader();

            uint unknownUInt32_1   = reader.ReadUInt32(); // TODO: Unknown.
            uint moppCodeOffset    = reader.ReadUInt32(); // TODO: Unknown - seems unnecessary?
            uint vertexCountOffset = reader.ReadUInt32();
            uint faceCountOffset   = reader.ReadUInt32();
            uint vertexCount       = reader.ReadUInt32();

            for (int i = 0; i < vertexCount; i++)
                Vertices.Add(reader.ReadVector3());

            uint faceCount = reader.ReadUInt32();

            for (int i = 0; i < faceCount; i++)
            {
                CollisionFace face = new CollisionFace
                {
                    VertexA = reader.ReadUInt16(),
                    VertexB = reader.ReadUInt16(),
                    VertexC = reader.ReadUInt16()
                };

                reader.JumpAhead(2); // Could probably use reader.FixPadding(); here?

                face.Flags = reader.ReadUInt32(); // Could probably do with finding a way to read each Nibble (half byte) individually.

                Faces.Add(face);
            }
        }

        public override void Save(Stream fileStream)
        {
            BINAv1Header header = new BINAv1Header();
            BINAWriter writer = new BINAWriter(fileStream, header);

            writer.AddOffset("unknownUInt32_1");

            writer.WriteNulls(4); // Ignore MOPP code offset.

            writer.FillInOffset("unknownUInt32_1", true);

            writer.AddOffset("vertexCountOffset");
            writer.AddOffset("faceCountOffset");

            writer.FillInOffset("vertexCountOffset", true);
            writer.Write(Vertices.Count);

            for (int i = 0; i < Vertices.Count; i++)
                writer.Write(Vertices[i]);

            writer.FillInOffset("faceCountOffset", true);
            writer.Write(Faces.Count);

            for (int i = 0; i < Faces.Count; i++)
            {
                writer.Write(Faces[i].VertexA);
                writer.Write(Faces[i].VertexB);
                writer.Write(Faces[i].VertexC);
                writer.WriteNulls(2);           // Could probably use writer.FixPadding(); here?
                writer.Write((uint)Faces[i].Flags);
            }

            writer.FinishWrite(header);
        }

        /// <summary>
        /// Exports the vertices and faces to the OBJ format.
        /// This could do with being rewritten with a proper model handling approach.
        /// </summary>
        public void ExportOBJ(string filepath)
        {
            using (StreamWriter obj = new StreamWriter(filepath))
            {
                foreach (Vector3 vertex in Vertices)
                    obj.WriteLine($"v {vertex.X} {vertex.Y} {vertex.Z}");

                foreach (CollisionFace face in Faces)
                {
                    obj.WriteLine($"g {Path.GetFileNameWithoutExtension(filepath)}@{face.Flags.ToString("x")}");
                    obj.WriteLine($"f {face.VertexA + 1} {face.VertexB + 1} {face.VertexC + 1}");
                }
            }
        }

        /// <summary>
        /// Imports data from an Assimp compatible format
        /// </summary>
        /// <param name="filepath"></param>
        public void ImportAssimp(string filepath)
        {
            // Setup Assimp Scene.
            AssimpContext assimpImporter = new AssimpContext();
            KeepSceneHierarchyConfig config = new KeepSceneHierarchyConfig(true);
            assimpImporter.SetConfig(config);
            Scene assimpModel = assimpImporter.ImportFile(filepath, PostProcessSteps.PreTransformVertices);

            foreach(Mesh assimpMesh in assimpModel.Meshes)
            {
                // Mesh Tag
                uint meshNameTag = 0u;
                if (assimpMesh.Name.Contains("@")) { meshNameTag = (uint)Convert.ToInt32(assimpMesh.Name.Substring(assimpMesh.Name.LastIndexOf('@') + 1), 16); }

                // Faces.
                foreach (Assimp.Face assimpFace in assimpMesh.Faces)
                {
                    CollisionFace face = new CollisionFace
                    {
                        VertexA = (ushort)(assimpFace.Indices[0] + Vertices.Count),
                        VertexB = (ushort)(assimpFace.Indices[1] + Vertices.Count),
                        VertexC = (ushort)(assimpFace.Indices[2] + Vertices.Count),
                        Flags   = meshNameTag
                    };
                    Faces.Add(face);
                }

                // Verticies.
                foreach(Vector3D assimpVertex in assimpMesh.Vertices)
                {
                    Vertices.Add(new Vector3(assimpVertex.X, assimpVertex.Y, assimpVertex.Z));
                }
            }
        }
    }
}