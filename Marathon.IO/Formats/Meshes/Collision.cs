// Collision.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 Knuxfan24
 * Copyright (c) 2020 HyperPolygon64
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
using Marathon.IO.Helpers;

namespace Marathon.IO.Formats.Meshes
{
    /// <summary>
    /// File base for the Sonic '06 BIN format.
    /// </summary>
    public class Collision : FileBase
    {
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

                reader.JumpAhead(2);

                face.Flags = reader.ReadUInt32();

                Faces.Add(face);
            }
        }

        public override void Save(Stream fileStream)
        {
            BINAv1Header header = new BINAv1Header();
            BINAWriter writer = new BINAWriter(fileStream, header);

            writer.AddOffset("unknownUInt32_1");

            writer.Write(0); // Ignore MOPP code offset.

            writer.FillInOffset("unknownUInt32_1", true);

            writer.AddOffset("vertexCountOffset");
            writer.AddOffset("faceCountOffset");

            writer.FillInOffset("vertexCountOffset", true);
            writer.Write(Vertices.Count);

            for (int i = 0; i < Vertices.Count; i++)
                writer.Write(Vertices[i]);

            writer.FillInOffset("faceCountOffset");
            writer.Write(Faces.Count);

            for (int i = 0; i < Faces.Count; i++)
            {
                writer.Write(Faces[i].Vertex1);
                writer.Write(Faces[i].Vertex2);
                writer.Write(Faces[i].Vertex3);
                writer.WriteNulls(2);
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
                    obj.WriteLine($"g {Path.GetFileNameWithoutExtension(filepath)}_{face.Flags}");
                    obj.WriteLine($"f {face.Vertex1 + 1} {face.Vertex2 + 1} {face.Vertex3 + 1}");
                }
            }
        }

        /// <summary>
        /// Imports the vertices and faces from the OBJ format.
        /// This could do with being rewritten with a proper model handling approach.
        /// </summary>
        public void ImportOBJ(string filepath)
        {
            string[] obj = File.ReadAllLines(filepath);
            uint flags = 0;
            foreach (string line in obj)
            {
                if (line.StartsWith("v "))
                {
                    string[] split = line.Split(' ');
                    Vector3 vertex = new Vector3();
                    vertex.X = float.Parse(split[2]);
                    vertex.Y = float.Parse(split[3]);
                    vertex.Z = float.Parse(split[4]);
                    Vertices.Add(vertex);
                }

                if (line.StartsWith("g "))
                {
                    if (line.Contains("@"))
                    {
                        string temp = line.Substring(line.LastIndexOf('@') + 1);
                        flags = (uint)Convert.ToInt32(temp, 16);
                    }
                    else if (line.Contains("_at_"))
                    {
                        string temp = line.Substring(line.LastIndexOf("_at_") + 4);
                        flags = (uint)Convert.ToInt32(temp, 16);
                    }
                    else { flags = 0; }
                }

                if (line.StartsWith("f "))
                {
                    Face face = new Face();
                    string[] split = line.Split(' ');
                    if (split[1].Contains("/")) { face.Vertex1 = (ushort)(float.Parse(split[1].Substring(0, split[1].IndexOf('/'))) - 1f); }
                    if (split[2].Contains("/")) { face.Vertex2 = (ushort)(float.Parse(split[2].Substring(0, split[2].IndexOf('/'))) - 1f); }
                    if (split[3].Contains("/")) { face.Vertex3 = (ushort)(float.Parse(split[3].Substring(0, split[3].IndexOf('/'))) - 1f); }
                    face.Flags = flags;
                    Faces.Add(face);
                }
            }
        }
    }
}
