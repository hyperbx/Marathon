using Assimp;
using Assimp.Configs;
using System.Globalization;

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
                case ".fbx":
                case ".dae":
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
            public List<Vector3> Vertices = [];
            public List<CollisionFace> Faces = [];
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

                face.Flags = (CollisionFlag)reader.ReadUInt32(); // TODO: Experiment with flags to see what they all do.

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
                writer.Write((uint)Data.Faces[i].Flags);
            }

            writer.FinishWrite();
        }

        public void ExportOBJ(string filePath)
        {
            // Create a SteamWriter.
            var obj = new StreamWriter(filePath);

            // Vertices.
            foreach (Vector3 vertex in Data.Vertices)
                obj.WriteLine($"v {vertex.X} {vertex.Y} {vertex.Z}");

            // Faces.
            // Get all used flags.
            var flags = new List<CollisionFlag>();

            foreach (CollisionFace face in Data.Faces)
            {
                if (!flags.Contains(face.Flags))
                    flags.Add(face.Flags);
            }

            // Write Faces.
            foreach (uint flag in flags)
            {
                obj.WriteLine($"\ng {flag.ToString("x").PadLeft(8, '0')}");

                foreach (CollisionFace face in Data.Faces)
                {
                    if ((uint)face.Flags == flag)
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

                // Assimp seems to attach Mesh to end of DAE imports, lets check for that instead.
                // Also seems to have a habit of adding an _ if the name is a number, so factor that in too...
                string name = assimpMesh.Name;
                if (name.EndsWith("Mesh"))
                    name = name.Remove(name.LastIndexOf('M'));
                if (name.StartsWith('_'))
                    name = name.Substring(1);

                // Try read an @ sign for backwards compatibility.
                if (name.Contains('@'))
                {
                    meshNameTag = ParseFlags(name[(name.LastIndexOf('@') + 1)..]);
                }
                else
                {
                    meshNameTag = ParseFlags(name);
                }

                // Faces.
                foreach (Face assimpFace in assimpMesh.Faces)
                {
                    CollisionFace face = new()
                    {
                        VertexA = (ushort)(assimpFace.Indices[0] + Data.Vertices.Count),
                        VertexB = (ushort)(assimpFace.Indices[1] + Data.Vertices.Count),
                        VertexC = (ushort)(assimpFace.Indices[2] + Data.Vertices.Count),
                        Flags = (CollisionFlag)meshNameTag
                    };

                    Data.Faces.Add(face);
                }

                // Vertices.
                foreach (Vector3D assimpVertex in assimpMesh.Vertices)
                    Data.Vertices.Add(new Vector3(assimpVertex.X, assimpVertex.Y, assimpVertex.Z));
            }
        }

        private uint ParseFlags(string in_str)
        {
            if (uint.TryParse(in_str, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var out_flags))
                return out_flags;

            return 0;
        }
    }

    public class CollisionFace
    {
        public ushort VertexA { get; set; }

        public ushort VertexB { get; set; }

        public ushort VertexC { get; set; }

        public CollisionFlag Flags { get; set; }

        public CollisionFace() { }

        public CollisionFace(ushort in_vertexA, ushort in_vertexB, ushort in_vertexC, CollisionFlag in_flags)
        {
            VertexA = in_vertexA;
            VertexB = in_vertexB;
            VertexC = in_vertexC;
            Flags = in_flags;
        }

        public override string ToString() => $"<{VertexA}, {VertexB}, {VertexC}> @{Flags.ToString("X").PadLeft(8, '0')}";
    }

    public enum CollisionFlag : uint
    {
        Concrete      = 0x00000000,
        Water         = 0x00000001,
        Wood          = 0x00000002,
        Metal         = 0x00000003,
        Grass         = 0x00000005,
        Sand          = 0x00000006,
        Snow          = 0x00000008,
        Dirt          = 0x00000009,
        Glass         = 0x0000000A,
        MetalEcho     = 0x0000000E,
        Wall          = 0x00010000,
        NoStand       = 0x00040000,
        WaterSurface  = 0x00080000,
        Death         = 0x00100000,
        PlayerOnly    = 0x00200000,
        CameraOnly    = 0x04000000,
        Tentative     = 0x10000000,
        CornerDamage  = 0x20000000,
        Damage        = 0x28000000,
        WaterSurface2 = 0x40000000,
        DeadlyWater   = 0x60000000,
        Climbable     = 0x80000000
    }
}
