// Collision.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 Knuxfan24
 * Copyright (c) 2020 HyperBE32
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

        public class Face
        {
            public ushort Vertex1, Vertex2, Vertex3;
            public uint Flags;
        }

        public const string Extension = ".bin";

        public List<Vector3> Vertices = new List<Vector3>();
        public List<Face> Faces = new List<Face>();

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
                Face face = new Face
                {
                    Vertex1 = reader.ReadUInt16(),
                    Vertex2 = reader.ReadUInt16(),
                    Vertex3 = reader.ReadUInt16()
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
                writer.Write(Faces[i].Vertex1);
                writer.Write(Faces[i].Vertex2);
                writer.Write(Faces[i].Vertex3);
                writer.WriteNulls(2);           // Could probably use writer.FixPadding(); here?
                writer.Write(Faces[i].Flags);
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

                foreach (Face face in Faces)
                {
                    obj.WriteLine($"g {Path.GetFileNameWithoutExtension(filepath)}@{face.Flags.ToString("x")}");
                    obj.WriteLine($"f {face.Vertex1 + 1} {face.Vertex2 + 1} {face.Vertex3 + 1}");
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
                    Face face = new Face
                    {
                        Vertex1 = (ushort)(assimpFace.Indices[0] + Vertices.Count),
                        Vertex2 = (ushort)(assimpFace.Indices[1] + Vertices.Count),
                        Vertex3 = (ushort)(assimpFace.Indices[2] + Vertices.Count),
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
