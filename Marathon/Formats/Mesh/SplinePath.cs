using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

// Format research attribution: Knuxfan24, Aruki

namespace Marathon.Formats.Mesh
{
    /// <summary>
    /// Support for *.path files; used for spline data.
    /// </summary>
    public class SplinePath : FileBase
    {
        public SplinePath() { }

        public SplinePath(string in_path) : base(in_path) { }

        /// <summary>
        /// The defined paths in this file.
        /// </summary>
        public List<SplinePathData> Paths { get; set; } = [];

        public SplinePathData this[string in_name]
        {
            get => Paths.Find((x) => x.Name == in_name);
        }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            var pathTableOffset = reader.Read<uint>();
            var pathCount = reader.Read<uint>();
            var nodeTableOffset = reader.Read<uint>();
            var nodeCount = reader.Read<uint>();

            for (int i = 0; i < pathCount; i++)
            {
                var path = new SplinePathData();

                var pathOffset = reader.Read<uint>();
                var splineCount = reader.Read<uint>();

                path.UnknownField1 = reader.Read<float>();

                var pos = reader.Position;

                reader.JumpTo(BINAHeader.Size + pathOffset);

                var splineOffset = reader.Read<uint>();
                var vertexCount = reader.Read<uint>(); 

                path.UnknownField2 = reader.Read<uint>();

                reader.JumpTo(BINAHeader.Size + splineOffset);

                for (int j = 0; j < splineCount; j++)
                {
                    var spline = new SplineRoot();

                    for (int k = 0; k < vertexCount; k++)
                    {
                        var point = new SplineVertex()
                        {
                            Flags = reader.Read<uint>(),
                            Position = reader.Read<Vector3>(),
                            InPosition = reader.Read<Vector3>(),
                            OutPosition = reader.Read<Vector3>()
                        };

                        spline.Vertices.Add(point);
                    }

                    path.Splines.Add(spline);
                }

                Paths.Add(path);

                reader.JumpTo(pos);
            }

            reader.JumpTo(BINAHeader.Size + nodeTableOffset);

            for (int i = 0; i < nodeCount; i++)
            {
                /* TODO: unknown - usually the same as the
                   path's number sequentially, but not always. */
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
            var writer = new BINAWriter(in_stream);

            writer.CreateNamedField("PathTableOffset");
            writer.Write(Paths.Count);
            writer.CreateNamedField("NodeTableOffset");
            writer.Write(Paths.Count); // Always seems to be the same as the path count.
            writer.WriteNamedField("PathTableOffset", (uint)writer.Position - BINAHeader.Size);

            for (int i = 0; i < Paths.Count; i++)
            {
                writer.CreateNamedField($"Path{i}Offset");
                writer.Write(Paths[i].Splines.Count);
                writer.Write(Paths[i].UnknownField1);
            }

            for (int i = 0; i < Paths.Count; i++)
            {
                writer.WriteNamedField($"Path{i}Offset", (uint)writer.Position - BINAHeader.Size);
                writer.CreateNamedField($"Path{i}SplineOffset");
                writer.Write(Paths[i].Splines[0].Vertices.Count);
                writer.Write(Paths[i].UnknownField2);
            }

            for (int i = 0; i < Paths.Count; i++)
            {
                writer.WriteNamedField($"Path{i}SplineOffset", (uint)writer.Position - BINAHeader.Size);

                for (int j = 0; j < Paths[i].Splines.Count; j++)
                {
                    for (int k = 0; k < Paths[i].Splines[j].Vertices.Count; k++)
                    {
                        writer.Write(Paths[i].Splines[j].Vertices[k].Flags);
                        writer.Write(Paths[i].Splines[j].Vertices[k].Position);
                        writer.Write(Paths[i].Splines[j].Vertices[k].InPosition);
                        writer.Write(Paths[i].Splines[j].Vertices[k].OutPosition);
                    }
                }
            }

            writer.WriteNamedField("NodeTableOffset", (uint)writer.Position - BINAHeader.Size);

            for (int i = 0; i < Paths.Count; i++)
            {
                writer.Write(Paths[i].NodeIndex);
                writer.Write(Paths[i].Position);
                writer.Write(Paths[i].Rotation);
                writer.CreateStringField($"Path{i}Name", Paths[i].Name);
            }

            writer.FinishWrite();
        }
    }

    public class SplinePathData
    {
        public float UnknownField1 { get; set; }

        public uint UnknownField2 { get; set; }

        public uint NodeIndex { get; set; }

        public List<SplineRoot> Splines { get; set; } = [];

        public Vector3 Position { get; set; }

        public Quaternion Rotation { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class SplineRoot
    {
        public List<SplineVertex> Vertices { get; set; } = [];

        public SplineRoot() { }

        public SplineRoot(List<SplineVertex> in_vertices)
        {
            Vertices = in_vertices;
        }
    }

    public class SplineVertex
    {
        public uint Flags { get; set; }

        public Vector3 Position { get; set; }

        public Vector3 InPosition { get; set; }

        public Vector3 OutPosition { get; set; }

        public SplineVertex() { }

        public SplineVertex(uint in_flags, Vector3 in_position, Vector3 in_inPosition, Vector3 in_outPosition)
        {
            Flags = in_flags;
            Position = in_position;
            InPosition = in_inPosition;
            OutPosition = in_outPosition;
        }
    }
}