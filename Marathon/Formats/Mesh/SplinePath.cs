using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using Marathon.IO.Types.FileSystem;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

// Format names:        Spline Path
// Format references:   Sonicteam::SplinePath
// Format designers:    Sonic Team
// Format researchers:  Aruki, Knuxfan24, Hyper
//
// Format research references:
// - https://github.com/Knuxfan24/Sonic-06-Stage-Editing-Archive/tree/cleanup/Binaries/PATH%20Maxscripts

namespace Marathon.Formats.Mesh
{
    /// <summary>
    /// Support for *.path files; used for spline data.
    /// </summary>
    [FileType("Spline Path", "Mesh", _extension)]
    public class SplinePath : FileBase
    {
        private const string _extension = ".path"; // "PATH"

        /// <summary>
        /// The paths in this file.
        /// </summary>
        public List<SplinePathInfo> Paths { get; set; } = [];

        public override string Extension => _extension;

        public SplinePathInfo this[int in_index]
        {
            get => Paths[in_index];
            set => Paths[in_index] = value;
        }

        public SplinePathInfo this[string in_name]
        {
            get => Paths.Find(x => x.Name == in_name);
        }

        public SplinePath() { }

        public SplinePath(string in_path) : base(in_path) { }

        public SplinePath(Stream in_stream) : base(in_stream) { }

        public SplinePath(IFile in_file) : base(in_file) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            Endianness = reader.Endianness;

            var pathTableOffset = reader.Read<uint>();
            var pathCount = reader.Read<uint>();
            var nodeTableOffset = reader.Read<uint>();
            var nodeCount = reader.Read<uint>();

            reader.JumpTo(BINAHeader.Size + pathTableOffset);

            for (int i = 0; i < pathCount; i++)
            {
                var path = new SplinePathInfo();
                var pathOffset = reader.Read<uint>();
                var splineCount = reader.Read<uint>();
                path.UnknownField = reader.Read<float>();

                reader.ReadAtOffset(BINAHeader.Size + pathOffset, () =>
                {
                    for (int j = 0; j < splineCount; j++)
                    {
                        var spline = new SplinePathRoot();
                        var splineOffset = reader.Read<uint>();
                        var vertexCount = reader.Read<uint>();
                        spline.UnknownField = reader.Read<uint>();

                        reader.ReadAtOffset(BINAHeader.Size + splineOffset, () =>
                        {
                            for (int k = 0; k < vertexCount; k++)
                            {
                                var vertex = new SplinePathVertex()
                                {
                                    Flags = reader.Read<uint>(),
                                    Position = reader.Read<Vector3>(),
                                    In = reader.Read<Vector3>(),
                                    Out = reader.Read<Vector3>()
                                };

                                spline.Vertices.Add(vertex);
                            }
                        });

                        path.Splines.Add(spline);
                    }
                });

                Paths.Add(path);
            }

            reader.JumpTo(BINAHeader.Size + nodeTableOffset);

            for (int i = 0; i < nodeCount; i++)
            {
                Paths[i].NodeIndex = reader.Read<uint>();
                Paths[i].Position = reader.Read<Vector3>();
                Paths[i].Rotation = reader.Read<Quaternion>();

                var nameOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + nameOffset,
                    () => Paths[i].Name = reader.ReadStringNullTerminated());
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream, Endianness);

            writer.Reserve<uint>("PathTableOffset");
            writer.Write(Paths.Count);
            writer.Reserve<uint>("NodeTableOffset");
            writer.Write(Paths.Count);
            writer.WriteReserved("PathTableOffset", (uint)writer.Position - BINAHeader.Size);

            for (int i = 0; i < Paths.Count; i++)
            {
                writer.Reserve<uint>($"Path{i}Offset");
                writer.Write(Paths[i].Splines.Count);
                writer.Write(Paths[i].UnknownField);
            }

            for (int i = 0; i < Paths.Count; i++)
            {
                writer.WriteReserved($"Path{i}Offset", (uint)writer.Position - BINAHeader.Size);

                for (int j = 0; j < Paths[i].Splines.Count; j++)
                {
                    writer.Reserve<uint>($"Path{i}Spline{j}Offset");
                    writer.Write(Paths[i].Splines[j].Vertices.Count);
                    writer.Write(Paths[i].Splines[j].UnknownField);
                }
            }

            for (int i = 0; i < Paths.Count; i++)
            {
                for (int j = 0; j < Paths[i].Splines.Count; j++)
                {
                    writer.WriteReserved($"Path{i}Spline{j}Offset", (uint)writer.Position - BINAHeader.Size);

                    for (int k = 0; k < Paths[i].Splines[j].Vertices.Count; k++)
                    {
                        writer.Write(Paths[i].Splines[j].Vertices[k].Flags);
                        writer.Write(Paths[i].Splines[j].Vertices[k].Position);
                        writer.Write(Paths[i].Splines[j].Vertices[k].In);
                        writer.Write(Paths[i].Splines[j].Vertices[k].Out);
                    }
                }
            }

            writer.WriteReserved("NodeTableOffset", (uint)writer.Position - BINAHeader.Size);

            for (int i = 0; i < Paths.Count; i++)
            {
                writer.Write(Paths[i].NodeIndex);
                writer.Write(Paths[i].Position);
                writer.Write(Paths[i].Rotation);
                writer.WriteStringOffset(Paths[i].Name);
            }

            writer.FinishWrite();
        }
    }

    public class SplinePathInfo
    {
        public string Name { get; set; }

        public uint NodeIndex { get; set; }

        public float UnknownField { get; set; }

        public Vector3 Position { get; set; }

        public Quaternion Rotation { get; set; }

        public List<SplinePathRoot> Splines { get; set; } = [];

        public override string ToString()
        {
            return Name;
        }
    }

    public class SplinePathRoot
    {
        public uint UnknownField { get; set; }

        public List<SplinePathVertex> Vertices { get; set; } = [];
    }

    public class SplinePathVertex
    {
        public uint Flags { get; set; }

        public Vector3 Position { get; set; }

        public Vector3 In { get; set; }

        public Vector3 Out { get; set; }

        public SplinePathVertex() { }

        public SplinePathVertex(uint in_flags, Vector3 in_position, Vector3 in_in, Vector3 in_out)
        {
            Flags = in_flags;
            Position = in_position;
            In = in_in;
            Out = in_out;
        }
    }
}