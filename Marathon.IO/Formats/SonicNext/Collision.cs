using System;
using System.IO;
using Marathon.IO.Headers;
using Marathon.IO.Helpers;
using System.Collections.Generic;

namespace Marathon.IO.Formats.SonicNext
{
    /// <summary>
    /// File base for the Sonic '06 BIN format.
    /// </summary>
    public class Collision : FileBase
    {
        public class S06CollisionFace
        {
            public ushort Vertex1, Vertex2, Vertex3;
            public uint Flags;
        }

        public const string Extension = ".bin";

        public List<Vector3> Vertices = new List<Vector3>();
        public List<S06CollisionFace> Faces = new List<S06CollisionFace>();

        public override void Load(Stream fileStream)
        {
            // BINA Header
            BINAReader reader = new BINAReader(fileStream);
            reader.ReadHeader();

            uint unknownOffset1 = reader.ReadUInt32();
            uint moppCodeOffset = reader.ReadUInt32(); // Seems to not be needed by the game?
            uint vertexCountOffset = reader.ReadUInt32();
            uint faceCountOffset = reader.ReadUInt32();
            uint vertexCount = reader.ReadUInt32();

            // Vertexes
            for (int i = 0; i < vertexCount; i++)
            {
                Vertices.Add(reader.ReadVector3());
            }

            // Faces
            uint faceCount = reader.ReadUInt32();
            for (int i = 0; i < faceCount; i++)
            {
                S06CollisionFace face = new S06CollisionFace();
                face.Vertex1 = reader.ReadUInt16();
                face.Vertex2 = reader.ReadUInt16();
                face.Vertex3 = reader.ReadUInt16();
                reader.JumpAhead(2);
                face.Flags = reader.ReadUInt32();
                Faces.Add(face);
            }
        }

        public override void Save(Stream fileStream)
        {
            BINAv1Header Header = new BINAv1Header();
            BINAWriter writer = new BINAWriter(fileStream, Header);

            // Write offsets.
            writer.AddOffset("unknownOffset1");
            writer.Write(0); //moppCodeOffset
            writer.FillInOffset("unknownOffset1", true);
            writer.AddOffset("vertexCountOffset");
            writer.AddOffset("faceCountOffset");

            // Write vertex data.
            writer.FillInOffset("vertexCountOffset", true);
            writer.Write(Vertices.Count);
            for (int i = 0; i < Vertices.Count; i++)
            {
                writer.Write(Vertices[i]);
            }

            // Write face data.
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

            // Write the footer.
            writer.FinishWrite(Header);
        }

        // These two functions could do with being rewritten with a proper model handling approach.
        public void ExportOBJ(string filepath)
        {
            using (StreamWriter obj = new StreamWriter(filepath))
            {
                foreach (Vector3 vertex in Vertices)
                {
                    obj.WriteLine($"v {vertex.X} {vertex.Y} {vertex.Z}");
                }

                foreach (S06CollisionFace face in Faces)
                {
                    obj.WriteLine($"g {Path.GetFileNameWithoutExtension(filepath)}_{face.Flags}");
                    obj.WriteLine($"f {face.Vertex1 + 1} {face.Vertex2 + 1} {face.Vertex3 + 1}");
                }
            }
        }

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
                    S06CollisionFace face = new S06CollisionFace();
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
