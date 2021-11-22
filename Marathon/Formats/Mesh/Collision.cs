using Assimp;
using Assimp.Configs;

namespace Marathon.Formats.Mesh
{
    /// <summary>
    /// File base for the collision.bin format.
    /// <para>Used in SONIC THE HEDGEHOG for collision meshes for terrain.</para>
    /// </summary>
    public class Collision : FileBase
    {
        public Collision() { }

        public Collision(string file, bool serialise = false)
        {
            switch (Path.GetExtension(file))
            {
                case ".obj":
                {
                    ImportAssimp(file);

                    // Save extension-less OBJ (exploiting .NET weirdness, because it doesn't omit all extensions).
                    if (serialise)
                        Save(Path.GetFileNameWithoutExtension(file));

                    break;
                }

                default:
                {
                    Load(file);

                    if (serialise)
                        ExportOBJ($"{Location}.obj");

                    break;
                }
            }
        }

        public override string Extension { get; } = ".bin";

        public class FormatData
        {
            public List<Vector3> Vertices = new();
            public List<CollisionFace> Faces = new();
        }

        public FormatData Data { get; set; } = new();

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            uint unknownUInt32_1   = reader.ReadUInt32(); // TODO: Unknown, is apparently an offset?
            uint moppCodeOffset    = reader.ReadUInt32(); // Offset to this file's Havok MOPP Code, which '06 doesn't seem to use?
            uint vertexTableOffset = reader.ReadUInt32(); // Offset to this file's Vertex Table.
            uint faceTableOffset   = reader.ReadUInt32(); // Offset to this file's Face Table.

            reader.JumpTo(vertexTableOffset, true); // Should already be here but just to be safe.
            uint vertexCount = reader.ReadUInt32(); // Get the amount of Vertices in this file.

            // Read the Vertices.
            for (int i = 0; i < vertexCount; i++)
                Data.Vertices.Add(reader.ReadVector3());

            reader.JumpTo(faceTableOffset, true); // Should already be here but just to be safe.
            uint faceCount = reader.ReadUInt32(); // Get the amount of Faces in this file.

            // Read the Faces.
            for (int i = 0; i < faceCount; i++)
            {
                CollisionFace face = new()
                {
                    VertexA = reader.ReadUInt16(),
                    VertexB = reader.ReadUInt16(),
                    VertexC = reader.ReadUInt16()
                };

                reader.FixPadding();

                face.Flags = reader.ReadUInt32(); // TODO: Experiment with flags to see what they all do.

                Data.Faces.Add(face);
            }
        }

        public override void Save(Stream stream)
        {
            BINAWriter writer = new(stream);

            writer.AddOffset("unknownUInt32_1");

            writer.WriteNulls(4); // Ignore MOPP code offset.

            writer.FillOffset("unknownUInt32_1", true);

            writer.AddOffset("vertexCountOffset");
            writer.AddOffset("faceCountOffset");

            writer.FillOffset("vertexCountOffset", true);
            writer.Write(Data.Vertices.Count);

            for (int i = 0; i < Data.Vertices.Count; i++)
                writer.Write(Data.Vertices[i]);

            writer.FillOffset("faceCountOffset", true);
            writer.Write(Data.Faces.Count);

            for (int i = 0; i < Data.Faces.Count; i++)
            {
                writer.Write(Data.Faces[i].VertexA);
                writer.Write(Data.Faces[i].VertexB);
                writer.Write(Data.Faces[i].VertexC);
                writer.FixPadding();
                writer.Write(Data.Faces[i].Flags);
            }

            writer.FinishWrite();
        }

        public void ExportOBJ(string filePath)
        {
            // Create a SteamWriter.
            StreamWriter obj = new(filePath);

            // Vertices.
            foreach (Vector3 vertex in Data.Vertices)
                obj.WriteLine($"v {vertex.X} {vertex.Y} {vertex.Z}");

            // Faces.
            // Get all used flags.
            List<uint> Flags = new();

            foreach (CollisionFace face in Data.Faces)
            {
                if (!Flags.Contains(face.Flags))
                    Flags.Add(face.Flags);
            }

            // Write Faces.
            foreach (uint Flag in Flags)
            {
                obj.WriteLine($"\ng {Flag.ToString("x").PadLeft(8, '0')}");

                foreach (CollisionFace face in Data.Faces)
                {
                    if (face.Flags == Flag)
                        obj.WriteLine($"f {face.VertexA + 1} {face.VertexB + 1} {face.VertexC + 1}");
                }
            }

            // Close the StreamWriter.
            obj.Close();
        }

        public void ImportAssimp(string filePath)
        {
            // Setup AssimpNet Scene.
            AssimpContext assimpImporter = new();
            KeepSceneHierarchyConfig config = new(true);
            assimpImporter.SetConfig(config);
            Scene assimpModel = assimpImporter.ImportFile(filePath, PostProcessSteps.PreTransformVertices);

            // Loop through all meshes in the imported file.
            foreach (Assimp.Mesh assimpMesh in assimpModel.Meshes)
            {
                // Mesh Tag.
                uint meshNameTag = 0u;

                // Try read an @ sign for backwards compatibility.
                if (assimpMesh.Name.Contains("@"))
                    meshNameTag = (uint)Convert.ToInt32(assimpMesh.Name.Substring(assimpMesh.Name.LastIndexOf('@') + 1), 16);

                // Try read the mesh name as the tag instead, leave it at 0 if it's not valid.
                else
                    try { meshNameTag = (uint)Convert.ToInt32(assimpMesh.Name, 16); } catch { }

                // Faces.
                foreach (Face assimpFace in assimpMesh.Faces)
                {
                    CollisionFace face = new()
                    {
                        VertexA = (ushort)(assimpFace.Indices[0] + Data.Vertices.Count),
                        VertexB = (ushort)(assimpFace.Indices[1] + Data.Vertices.Count),
                        VertexC = (ushort)(assimpFace.Indices[2] + Data.Vertices.Count),
                        Flags = meshNameTag
                    };

                    Data.Faces.Add(face);
                }

                // Vertices.
                foreach (Vector3D assimpVertex in assimpMesh.Vertices)
                    Data.Vertices.Add(new Vector3(assimpVertex.X, assimpVertex.Y, assimpVertex.Z));
            }
        }
    }

    public class CollisionFace
    {
        public ushort VertexA { get; set; }

        public ushort VertexB { get; set; }

        public ushort VertexC { get; set; }

        public uint Flags { get; set; }

        public override string ToString() => $"<{VertexA}, {VertexB}, {VertexC}> @{Flags.ToString("X").PadLeft(8, '0')}";
    }
}
