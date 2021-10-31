using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Marathon.IO;

namespace Marathon.Formats.Mesh
{
    /// <summary>
    /// File base for the *.path format.
    /// <para>Used in SONIC THE HEDGEHOG for splines.</para>
    /// </summary>
    public class PathSpline : FileBase
    {
        public PathSpline() { }

        public PathSpline(string file) : base(file) { }

        public override string Extension { get; } = ".path";

        public List<PathData> Paths { get; set; } = new();

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            uint PathTableOffset = reader.ReadUInt32(); // Where the table of paths is.
            uint PathTableCount  = reader.ReadUInt32();  // How many entries are in the table of paths.
            uint NodeTableOffset = reader.ReadUInt32(); // Where the table of nodes is.
            uint NodeTableCount  = reader.ReadUInt32();  // How many enteries are in the table of nodes, seems to always be the same as PathTableCount?

            // Jump to the Path Table's Offset for safety's sake, should always be ox30 really.
            reader.JumpTo(PathTableOffset, true);

            // Loop through based on PathTableCount.
            for (int i = 0; i < PathTableCount; i++)
            {
                PathData path = new();

                uint PathOffset  = reader.ReadUInt32(); // Offset to the path data.
                uint SplineCount = reader.ReadUInt32(); // How many splines make up this path.
                path.Flag1       = reader.ReadUInt32(); // Read this path's flag.

                // Store position in file.
                long position = reader.BaseStream.Position;

                // Jump to position of this path's data.
                reader.JumpTo(PathOffset, true);

                uint SplineOffset = reader.ReadUInt32(); // Offset to the spines of this path.
                uint VertexCount  = reader.ReadUInt32(); // How many verticies make up this path's splines.
                path.Flag2        = reader.ReadUInt32(); // Read this path's second flag.

                // Jump to the position of this path's splines.
                reader.JumpTo(SplineOffset, true);

                // Read this path's splines.
                for (int s = 0; s < SplineCount; s++)
                {
                    Spline spline = new();
                    for (int v = 0; v < VertexCount; v++)
                    {
                        Spline.SplineData vertex = new()
                        {
                            Flag = reader.ReadUInt32(),
                            Position = reader.ReadVector3(),
                            InPosition = reader.ReadVector3(),
                            OutPosition = reader.ReadVector3()
                        };

                        spline.Vertices.Add(vertex);
                    }

                    path.Splines.Add(spline);
                }

                // Save path entry and return to the previously stored position.
                Paths.Add(path);
                reader.JumpTo(position);
            }

            // Jump to the Node Table's offset.
            reader.JumpTo(NodeTableOffset, true);

            // Loop through based on NodeTableCount.
            for (int i = 0; i < NodeTableCount; i++)
            {
                Paths[i].NodeNumber = reader.ReadUInt32();    // Unknown, usually the same as the path's number sequentially, but not always.
                Paths[i].Position   = reader.ReadVector3();
                Paths[i].Rotation   = reader.ReadQuaternion();

                // Offset to the path's name.
                uint NameOffset = reader.ReadUInt32();

                // Store position in file.
                long position = reader.BaseStream.Position;

                // Read the path's name.
                Paths[i].Name = reader.ReadNullTerminatedString(false, NameOffset, true);

                // Jump back to the saved position to read the next node.
                reader.JumpTo(position);
            }
        }

        public override void Save(Stream stream)
        {
            BINAWriter writer = new(stream);

            writer.AddOffset("PathTableOffset");
            writer.Write(Paths.Count);

            writer.AddOffset("NodeTableOffset");
            writer.Write(Paths.Count); // Node Table Count seems to always be the same as PathTableCount?

            // Path Table
            writer.FillOffset("PathTableOffset", true);
            for (int i = 0; i < Paths.Count; i++)
            {
                writer.AddOffset($"Path{i}Offset");
                writer.Write(Paths[i].Splines.Count);
                writer.Write(Paths[i].Flag1);
            }

            for (int i = 0; i < Paths.Count; i++)
            {
                writer.FillOffset($"Path{i}Offset", true);
                writer.AddOffset($"Path{i}SplineOffset");
                writer.Write(Paths[i].Splines[0].Vertices.Count);
                writer.Write(Paths[i].Flag2);
            }

            for (int i = 0; i < Paths.Count; i++)
            {
                writer.FillOffset($"Path{i}SplineOffset", true);
                for (int s = 0; s < Paths[i].Splines.Count; s++)
                {
                    for (int v = 0; v < Paths[i].Splines[s].Vertices.Count; v++)
                    {
                        writer.Write(Paths[i].Splines[s].Vertices[v].Flag);
                        writer.Write(Paths[i].Splines[s].Vertices[v].Position);
                        writer.Write(Paths[i].Splines[s].Vertices[v].InPosition);
                        writer.Write(Paths[i].Splines[s].Vertices[v].OutPosition);
                    }
                }
            }

            // Node Table
            writer.FillOffset("NodeTableOffset", true);
            for (int i = 0; i < Paths.Count; i++)
            {
                writer.Write(Paths[i].NodeNumber);
                writer.Write(Paths[i].Position);
                writer.Write(Paths[i].Rotation);
                writer.AddString($"Path{i}Name", Paths[i].Name);
            }

            writer.FinishWrite();
        }
    }

    public class PathData
    {
        public uint Flag1 { get; set; }

        public uint Flag2 { get; set; }

        public uint NodeNumber { get; set; }

        public List<Spline> Splines { get; set; } = new();

        public Vector3 Position { get; set; }

        public Quaternion Rotation { get; set; }

        public string Name { get; set; }

        public override string ToString() => Name;
    }

    public class Spline
    {
        public List<SplineData> Vertices { get; set; } = new();

        public class SplineData
        {
            public uint Flag { get; set; }

            public Vector3 Position { get; set; }

            public Vector3 InPosition { get; set; }

            public Vector3 OutPosition { get; set; }
        }
    }
}