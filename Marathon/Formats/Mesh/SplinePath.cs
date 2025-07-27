using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

// Format names:        Spline Path
// Format references:   Sonicteam::SplinePath
// Format designers:    Sonic Team
// Format researchers:  Aruki, Knuxfan24, Hyper

namespace Marathon.Formats.Mesh
{
    /// <summary>
    /// Support for *.path files; used for spline data.
    /// </summary>
    public class SplinePath : FileBase, IList<SplinePathData>
    {
        private const string _extension = ".path"; // "PATH"

        public SplinePath() { }

        public SplinePath(string in_path) : base(in_path) { }

        /// <summary>
        /// The defined paths in this file.
        /// </summary>
        public List<SplinePathData> Paths { get; set; } = [];

        public int Count => Paths.Count;

        public bool IsReadOnly => false;

        public SplinePathData this[int in_index]
        {
            get => Paths[in_index];
            set => Paths[in_index] = value;
        }

        public SplinePathData this[string in_name]
        {
            get => Paths.Find((x) => x.Name == in_name);
        }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            Endianness = reader.Endianness;

            var pathTableOffset = reader.Read<uint>();
            var pathCount = reader.Read<uint>();
            var nodeTableOffset = reader.Read<uint>();
            var nodeCount = reader.Read<uint>();

            for (int i = 0; i < pathCount; i++)
            {
                var path = new SplinePathData();
                var pathOffset = reader.Read<uint>();
                var splineCount = reader.Read<uint>();

                path.UnknownField = reader.Read<float>();

                var pos = reader.Position;

                reader.JumpTo(BINAHeader.Size + pathOffset);

                for (int j = 0; j < splineCount; j++)
                {
                    var spline = new SplineRoot();
                    var splineOffset = reader.Read<uint>();
                    var vertexCount = reader.Read<uint>();

                    spline.UnknownField = reader.Read<uint>();

                    var splinePos = reader.Position;

                    reader.JumpTo(BINAHeader.Size + splineOffset);

                    for (int k = 0; k < vertexCount; k++)
                    {
                        var vertex = new SplineVertex()
                        {
                            Flags = reader.Read<uint>(),
                            Position = reader.Read<Vector3>(),
                            InPosition = reader.Read<Vector3>(),
                            OutPosition = reader.Read<Vector3>()
                        };

                        spline.Vertices.Add(vertex);
                    }

                    reader.JumpTo(splinePos);

                    path.Splines.Add(spline);
                }

                reader.JumpTo(pos);

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
                        writer.Write(Paths[i].Splines[j].Vertices[k].InPosition);
                        writer.Write(Paths[i].Splines[j].Vertices[k].OutPosition);
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

        public int IndexOf(SplinePathData in_item)
        {
            return Paths.IndexOf(in_item);
        }

        public void Insert(int in_index, SplinePathData in_item)
        {
            Paths.Insert(in_index, in_item);
        }

        public void RemoveAt(int in_index)
        {
            Paths.RemoveAt(in_index);
        }

        public void Add(SplinePathData in_item)
        {
            Paths.Add(in_item);
        }

        public void Clear()
        {
            Paths.Clear();
        }

        public bool Contains(SplinePathData in_item)
        {
            return Paths.Contains(in_item);
        }

        public void CopyTo(SplinePathData[] in_array, int in_arrayIndex)
        {
            Paths.CopyTo(in_array, in_arrayIndex);
        }

        public bool Remove(SplinePathData in_item)
        {
            return Paths.Remove(in_item);
        }

        public IEnumerator<SplinePathData> GetEnumerator()
        {
            return Paths.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class SplinePathData
    {
        public float UnknownField { get; set; }

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
        public uint UnknownField { get; set; }

        public List<SplineVertex> Vertices { get; set; } = [];

        public SplineRoot() { }

        public SplineRoot(uint in_unkField, List<SplineVertex> in_vertices)
        {
            UnknownField = in_unkField;
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