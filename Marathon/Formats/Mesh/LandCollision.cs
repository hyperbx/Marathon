using Amicitia.IO.Binary;
using Assimp;
using Assimp.Configs;
using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using Marathon.IO.Types.FileSystem;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Numerics;

// Format names:        Land Collision
// Format references:   Sonicteam::SoX::Physics::LandCollision
// Format designers:    Sonic Team, Havok
// Format researchers:  Darío, Knuxfan24, Hyper
//
// Format research references:
// - https://github.com/DarioSamo/libgens-sonicglvl/blob/master/src/LibS06/S06Collision.cpp (used with permission)

namespace Marathon.Formats.Mesh
{
    /// <summary>
    /// Support for collision.bin files; used for collision meshes for terrain.
    /// </summary>
    [FileType("Land Collision", "Mesh", @"collision\.bin$", true)]
    public class LandCollision : FileBase
    {
        private const string _extension = ".bin"; // "BINary"

        /// <summary>
        /// The vertices of this collision mesh.
        /// </summary>
        public List<Vector3> Vertices { get; set; } = [];

        /// <summary>
        /// The faces of this collision mesh.
        /// </summary>
        public List<CollisionFace> Faces { get; set; } = [];

        public override string Extension => _extension;

        public LandCollision() { }

        public LandCollision(string in_path) : base(in_path) { }

        public LandCollision(Stream in_stream) : base(in_stream) { }

        public LandCollision(IFile in_file) : base(in_file) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            Endianness = reader.Endianness;

            var unkField1 = reader.Read<uint>(); // TODO: unknown.
            var moppCodeOffset = reader.Read<uint>();
            var vertexTableOffset = reader.Read<uint>();
            var faceTableOffset = reader.Read<uint>();
            var vertexCount = reader.Read<uint>();

            for (int i = 0; i < vertexCount; i++)
                Vertices.Add(reader.Read<Vector3>());

            var faceCount = reader.Read<uint>();

            for (int i = 0; i < faceCount; i++)
            {
                CollisionFace face = new()
                {
                    VertexA = reader.Read<ushort>(),
                    VertexB = reader.Read<ushort>(),
                    VertexC = reader.Read<ushort>()
                };

                reader.Align(4);

                face.Flags = (CollisionFlag)reader.Read<uint>();

                Faces.Add(face);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream, Endianness);

            writer.Reserve<uint>("UnknownField1");

            /* Havok MOPP (memory optimised partial polytope)
               code can only be generated using the SDK, but
               thankfully it's optional in this game, so we
               don't write it here. */
            writer.Write(0);

            writer.WriteReserved("UnknownField1", (uint)writer.Position - BINAHeader.Size);
            writer.Reserve<uint>("VertexTableOffset");
            writer.Reserve<uint>("FaceTableOffset");
            writer.WriteReserved("VertexTableOffset", (uint)writer.Position - BINAHeader.Size);
            writer.Write(Vertices.Count);

            for (int i = 0; i < Vertices.Count; i++)
                writer.Write(Vertices[i]);

            writer.WriteReserved("FaceTableOffset", (uint)writer.Position - BINAHeader.Size);
            writer.Write(Faces.Count);

            for (int i = 0; i < Faces.Count; i++)
            {
                writer.Write(Faces[i].VertexA);
                writer.Write(Faces[i].VertexB);
                writer.Write(Faces[i].VertexC);
                writer.Align(4);
                writer.Write((uint)Faces[i].Flags);
            }

            writer.FinishWrite();
        }

        // FIXME: something is going terribly wrong here!!
        public override void Import(string in_path)
        {
            var ctx = new AssimpContext();
            var cfg = new KeepSceneHierarchyConfig(true);

            ctx.SetConfig(cfg);

            var assimpScene = ctx.ImportFile(in_path, PostProcessSteps.PreTransformVertices);

            foreach (var assimpMesh in assimpScene.Meshes)
            {
                var name = assimpMesh.Name;

                // Assimp suffixes "Mesh" to end of Collada meshes.
                if (name.EndsWith("Mesh"))
                    name = name[..name.LastIndexOf('M')];

                // Assimp prefixes '_' if the name is a number.
                name = name.TrimStart('_');

                // Trim '@' for backwards compatibility.
                var tag = ParseFlags(name.TrimStart('@'));

                foreach (var assimpFace in assimpMesh.Faces)
                {
                    var face = new CollisionFace()
                    {
                        VertexA = (ushort)(assimpFace.Indices[0] + Vertices.Count),
                        VertexB = (ushort)(assimpFace.Indices[1] + Vertices.Count),
                        VertexC = (ushort)(assimpFace.Indices[2] + Vertices.Count),
                        Flags = (CollisionFlag)tag
                    };

                    Faces.Add(face);
                }

                foreach (var assimpVertex in assimpMesh.Vertices)
                    Vertices.Add(new Vector3(assimpVertex.X, assimpVertex.Y, assimpVertex.Z));
            }
        }

        public override void Export(string in_path = "", bool in_overwrite = true)
        {
            EnsurePath(ref in_path, path => FileSystemHelper.EnsureExtension(path, ".obj"));

            if (!in_overwrite)
                ThrowHelper.ThrowFileExistsException(in_path);

            var writer = new StreamWriter(in_path);

            foreach (var vertex in Vertices)
                writer.WriteLine($"v {vertex.X} {vertex.Y} {vertex.Z}");

            var flags = new List<CollisionFlag>();

            foreach (var face in Faces)
            {
                if (flags.Contains(face.Flags))
                    continue;
                
                flags.Add(face.Flags);
            }

            foreach (var flag in flags)
            {
                writer.WriteLine($"\ng {flag.ToString("X").PadLeft(8, '0')}");

                foreach (var face in Faces)
                {
                    if (face.Flags == flag)
                        writer.WriteLine($"f {face.VertexA + 1} {face.VertexB + 1} {face.VertexC + 1}");
                }
            }

            writer.Close();
        }

        private static uint ParseFlags(string in_str)
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

        public override string ToString()
        {
            return $"<{VertexA}, {VertexB}, {VertexC}> @ 0x{Flags.ToString("X").PadLeft(8, '0')}";
        }
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
