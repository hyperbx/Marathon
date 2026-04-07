using Amicitia.IO.Binary;
using Assimp;
using Marathon.Extensions;
using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using Marathon.IO.Types.FileSystem;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;

// Format names:        Land Collision
// Format references:   Sonicteam::SoX::Physics::LandCollision
// Format designers:    Sonic Team, Havok
// Format researchers:  Darío, Knuxfan24, Hyper
//
// Format research references:
// - https://github.com/DarioSamo/libgens-sonicglvl/blob/master/src/LibS06/S06Collision.cpp (used with permission)
// - https://github.com/niftools/nifxml/wiki/Havok-MOPP-Data-format

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

            var infoOffset = reader.Read<uint>();
            var hkMoppOffset = reader.Read<uint>();
            var vertexTableOffset = reader.Read<uint>();
            var faceTableOffset = reader.Read<uint>();

            var vertexCount = reader.Read<uint>();

            for (int i = 0; i < vertexCount; i++)
                Vertices.Add(reader.Read<Vector3>());

            var faceCount = reader.Read<uint>();

            for (int i = 0; i < faceCount; i++)
                Faces.Add(reader.ReadObjectEx<CollisionFace>());

            // Unimplemented region: Havok MOPP data.
            //
            // This region starts with a Vector3 to denote the centre position,
            // then a float likely for world size and a uint for the length of
            // the MOPP data.
            //
            // This data is optional and is generated at runtime by the game
            // if it's not present, allowing us to completely skip over it.
            //
            // Unfortunately, that means this file type won't be binary
            // identical with the original files, but they do still work
            // with the game, which is the most important part.
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream, Endianness);

            var infoOffset = writer.Reserve<uint>();

            writer.WriteZero<uint>(); // Havok MOPP data offset. See notes in reader code.
            writer.WriteReserved(infoOffset, (uint)writer.Position - BINAHeader.Size);

            var vertexTableOffset = writer.Reserve<uint>();
            var faceTableOffset = writer.Reserve<uint>();

            writer.WriteReserved(vertexTableOffset, (uint)writer.Position - BINAHeader.Size);
            writer.Write(Vertices.Count);

            for (int i = 0; i < Vertices.Count; i++)
                writer.Write(Vertices[i]);

            writer.WriteReserved(faceTableOffset, (uint)writer.Position - BINAHeader.Size);
            writer.Write(Faces.Count);

            foreach (var face in Faces)
                writer.WriteObjectEx(face);

            writer.FinishWrite();
        }

        public override void Import(string in_path)
        {
            ThrowHelper.ThrowFileNotFoundException(in_path);

            using var ctx = new AssimpContext();

            var scene = ctx.ImportFile(in_path, PostProcessSteps.JoinIdenticalVertices);

            foreach (var mesh in scene.Meshes)
            {
                var flags = ParseTags(mesh.Name);
                var vertexOffset = Vertices.Count;

                Vertices.AddRange(mesh.Vertices);

                foreach (var assimpFace in mesh.Faces)
                {
                    if (assimpFace.IndexCount != 3)
                        throw new InvalidDataException("Invalid vertex format.");

                    var face = new CollisionFace()
                    {
                        A = (ushort)(assimpFace.Indices[0] + vertexOffset),
                        B = (ushort)(assimpFace.Indices[1] + vertexOffset),
                        C = (ushort)(assimpFace.Indices[2] + vertexOffset)
                    };

                    face.SetFlags(flags);

                    Faces.Add(face);
                }
            }
        }

        public override void Export(string in_path = "", bool in_overwrite = true)
        {
            EnsurePath(ref in_path, path => FileSystemHelper.EnsureExtension(path, ".fbx"));

            if (!in_overwrite)
                ThrowHelper.ThrowFileExistsException(in_path);

            using var ctx = new AssimpContext();

            var scene = new Scene()
            {
                RootNode = new(),
                Materials = { new() }
            };

            var groups = Faces.GroupBy(x => x.GetFlags()).ToList();

            foreach (var group in groups)
            {
                var tags = GetTags(group.Key);

                var meshName = tags.Count > 0
                    ? string.Join(' ', tags)
                    : Guid.NewGuid().ToString();

                var mesh = new Assimp.Mesh(meshName, PrimitiveType.Triangle);

                var indices = group
                    .SelectMany(x => new[] { x.A, x.B, x.C })
                    .Distinct().OrderBy(x => x).ToList();

                var indicesMap = indices
                    .Select((oldIndex, newIndex) => new { oldIndex, newIndex })
                    .ToDictionary(x => x.oldIndex, x => x.newIndex);

                foreach (var index in indices)
                    mesh.Vertices.Add(Vertices[index]);

                foreach (var face in group)
                {
                    var assimpFace = new Face()
                    {
                        Indices =
                        {
                            indicesMap[face.A],
                            indicesMap[face.B],
                            indicesMap[face.C]
                        }
                    };

                    mesh.Faces.Add(assimpFace);
                }

                var meshIndex = scene.MeshCount;

                scene.Meshes.Add(mesh);

                var node = new Node(meshName)
                {
                    MeshIndices = { meshIndex }
                };

                scene.RootNode.Children.Add(node);
            }

            ctx.ExportFile(scene, in_path, "fbx");
        }

        public static uint ParseTags(List<string> in_tags)
        {
            var result = 0U;

            foreach (var tag in in_tags)
            {
                if (!tag.StartsWith('@'))
                    continue;

                var value = tag[(tag.LastIndexOf('(') + 1)..].TrimEnd(')');

                if (tag.StartsWith("@ATT"))
                {
                    var attributeValue = 0U;

                    if (uint.TryParse(value.TrimNumberIdentifier(), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var out_uintValue))
                    {
                        attributeValue = out_uintValue;
                    }
                    else if (Enum.TryParse<CollisionAttributes>(value, out var out_enumValue))
                    {
                        attributeValue = (uint)out_enumValue;
                    }

                    result |= attributeValue;
                }
                else if (tag.StartsWith("@MAT"))
                {
                    var materialValue = 0U;

                    if (byte.TryParse(value.TrimNumberIdentifier(), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var out_byteValue))
                    {
                        materialValue = out_byteValue;
                    }
                    else if (Enum.TryParse<CollisionMaterial>(value, out var out_enumValue))
                    {
                        materialValue = (uint)out_enumValue;
                    }

                    result |= materialValue;
                }
            }

            return result;
        }

        public static uint ParseTags(string in_tags)
        {
            return ParseTags([.. in_tags.Split([' '], StringSplitOptions.RemoveEmptyEntries)]);
        }

        public static List<string> GetTags(CollisionAttributes in_attributes, CollisionMaterial in_material)
        {
            var result = new List<string>();
            var mask = 0U;

            foreach (var pair in EnumHelper.ToDictionary<CollisionAttributes, uint>())
            {
                if (pair.Value == 0 || ((uint)in_attributes & pair.Value) != pair.Value)
                    continue;

                result.Add($"@ATT({pair.Key})");

                mask |= pair.Value;
            }

            var unknownAttributes = (uint)in_attributes & ~mask;

            if (unknownAttributes != 0)
                result.Add($"@ATT(0x{unknownAttributes:X8})");

            var materials = EnumHelper.ToDictionary<CollisionMaterial, byte>();

            if (materials.ContainsValue((byte)in_material))
            {
                foreach (var pair in materials)
                {
                    if ((byte)in_material != pair.Value)
                        continue;

                    result.Add($"@MAT({pair.Key})");
                }
            }
            else
            {
                result.Add($"@MAT(0x{(byte)in_material:X2})");
            }

            return result;
        }

        public static List<string> GetTags(CollisionFace in_face)
        {
            return GetTags(in_face.Attributes, in_face.Material);
        }

        public static List<string> GetTags(uint in_flags)
        {
            return GetTags((CollisionAttributes)(in_flags & 0xFFFFFF00), (CollisionMaterial)(in_flags & 0xFF));
        }
    }

    public class CollisionFace : IBinarySerializableEx
    {
        public ushort A { get; set; }

        public ushort B { get; set; }

        public ushort C { get; set; }

        public CollisionAttributes Attributes { get; set; }

        public CollisionMaterial Material { get; set; }

        public CollisionFace() { }

        public CollisionFace(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public CollisionFace
        (
            ushort in_vertexA,
            ushort in_vertexB,
            ushort in_vertexC,
            CollisionAttributes in_attributes = CollisionAttributes.None,
            CollisionMaterial in_material = CollisionMaterial.Stone
        )
        {
            A = in_vertexA;
            B = in_vertexB;
            C = in_vertexC;
            Attributes = in_attributes;
            Material = in_material;
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            A = in_reader.Read<ushort>();
            B = in_reader.Read<ushort>();
            C = in_reader.Read<ushort>();

            in_reader.Align(4);

            SetFlags(in_reader.Read<uint>());
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(A);
            in_writer.Write(B);
            in_writer.Write(C);
            in_writer.Align(4);
            in_writer.Write(GetFlags());
        }

        public uint GetFlags()
        {
            return (uint)Attributes | (uint)Material;
        }

        public void SetFlags(uint in_flags)
        {
            Attributes = (CollisionAttributes)(in_flags & 0xFFFFFF00);
            Material = (CollisionMaterial)(in_flags & 0xFF);
        }

        public List<string> GetTags()
        {
            return LandCollision.GetTags(this);
        }

        public override string ToString()
        {
            return $"<{A}, {B}, {C}> {string.Join(' ', GetTags())}";
        }
    }

    [Flags]
    public enum CollisionAttributes : uint
    {
        None = 0x00000000,
        Wall = 0x00010000,
        NoStand = 0x00040000,
        WaterLow = 0x00080000,
        Death = 0x00100000,
        PlayerOnly = 0x00200000,
        Ceiling = 0x00800000,
        CameraOnly = 0x04000000,
        MoveOnly = 0x10000000,
        HazardEdge = 0x20000000,
        Hazard = 0x28000000,
        WaterHigh = 0x40000000,
        DeathWater = 0x60000000,
        Climbable = 0x80000000
    }

    public enum CollisionMaterial : byte
    {
        Stone = 0,
        Water = 1,
        Wood = 2,
        Metal = 3,
        Grass = 5,
        Sand = 6,
        Snow = 8,
        Dirt = 9,
        Glass = 10,
        MetalEcho = 14
    }
}
