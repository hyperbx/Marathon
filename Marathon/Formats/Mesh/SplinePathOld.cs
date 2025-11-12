using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

// Format names:        Spline Path (Old)
// Format references:   Sonicteam::SplinePath
// Format designers:    Sonic Team
// Format researchers:  Hyper

namespace Marathon.Formats.Mesh
{
    /// <summary>
    /// Support for an early version of the *.path format, stored in *.bin files in the win32 archives; used for spline data.
    /// </summary>
    public class SplinePathOld : FileBase
    {
        private const string _extension = ".bin"; // "BINary"

        public List<SplinePathOldInfo> Paths { get; set; } = [];

        public override string Extension => _extension;

        public SplinePathOldInfo this[int in_index]
        {
            get => Paths[in_index];
            set => Paths[in_index] = value;
        }

        public SplinePathOldInfo this[string in_name]
        {
            get => Paths.Find(x => x.Name == in_name);
        }

        public SplinePathOld() { }

        public SplinePathOld(string in_path) : base(in_path) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            Endianness = reader.Endianness;

            var pathCount = reader.Read<uint>();
            var pathTableOffset = reader.Read<uint>();

            reader.JumpTo(BINAHeader.Size + pathTableOffset);

            for (int i = 0; i < pathCount; i++)
            {
                var reserved = reader.Read<uint>();
                var pathOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + pathOffset, () =>
                {
                    var path = new SplinePathOldInfo();
                    var nameOffset = reader.Read<uint>();

                    reader.ReadAtOffset(BINAHeader.Size + nameOffset,
                        () => path.Name = reader.ReadStringNullTerminated());

                    var splineCount = reader.Read<uint>();
                    path.Length = reader.Read<float>();

                    for (int j = 0; j < splineCount; j++)
                    {
                        var vertexCount = reader.Read<uint>();
                        var splineOffset = reader.Read<uint>();

                        reader.ReadAtOffset(BINAHeader.Size + splineOffset, () =>
                        {
                            var spline = new SplinePathOldRoot()
                            {
                                UnknownField = reader.Read<uint>()
                            };

                            for (int k = 0; k < vertexCount; k++)
                            {
                                var vertex = new SplinePathOldVertex()
                                {
                                    Position = reader.Read<Vector3>(),
                                    UnknownField1 = reader.Read<Vector3>(),
                                    UnknownField2 = reader.Read<Vector3>(),
                                    Length = k == vertexCount - 1 ? 0 : reader.Read<float>()
                                };

                                spline.Vertices.Add(vertex);
                            }

                            path.Splines.Add(spline);
                        });
                    }

                    Paths.Add(path);
                });
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream, Endianness);

            writer.Header.HasSignature = false;

            writer.Write(Paths.Count);
            var pathTableOffset = writer.Reserve<uint>();
            writer.WriteReserved(pathTableOffset, (uint)writer.Position - BINAHeader.Size);

            for (int i = 0; i < Paths.Count; i++)
            {
                writer.Write(0);
                writer.Reserve<uint>($"Path{i}Offset");
            }

            for (int i = 0; i < Paths.Count; i++)
            {
                var path = Paths[i];

                writer.WriteReserved($"Path{i}Offset", (uint)writer.Position - BINAHeader.Size);
                writer.WriteStringOffset(path.Name);
                writer.Write(path.Splines.Count);
                writer.Write(path.Length);

                for (int j = 0; j < path.Splines.Count; j++)
                {
                    var spline = path.Splines[j];

                    writer.Write(spline.Vertices.Count);
                    var splineOffset = writer.Reserve<uint>();
                    writer.WriteReserved(splineOffset, (uint)writer.Position - BINAHeader.Size);
                    writer.Write(spline.UnknownField);

                    for (int k = 0; k < spline.Vertices.Count; k++)
                    {
                        var vertex = spline.Vertices[k];

                        writer.Write(vertex.Position);
                        writer.Write(vertex.UnknownField1);
                        writer.Write(vertex.UnknownField2);

                        if (k < spline.Vertices.Count - 1)
                            writer.Write(vertex.Length);
                    }
                }
            }

            writer.FinishWrite();
        }
    }

    public class SplinePathOldInfo
    {
        public string Name { get; set; }

        public float Length { get; set; }

        public List<SplinePathOldRoot> Splines { get; set; } = [];

        public override string ToString()
        {
            return Name;
        }
    }

    public class SplinePathOldRoot
    {
        public uint UnknownField { get; set; }

        public List<SplinePathOldVertex> Vertices { get; set; } = [];
    }

    public class SplinePathOldVertex
    {
        public Vector3 Position { get; set; }

        public Vector3 UnknownField1 { get; set; }

        public Vector3 UnknownField2 { get; set; }

        public float Length { get; set; }
    }
}