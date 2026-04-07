using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.Exceptions;
using Marathon.Helpers;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.FileSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

// Format names:        Kynogon Spatial Graph
// Format references:   Kaim::CSpatialGraph
// Format designers:    Kynogon
// Format researchers:  Hyper
//
// Format research references:
// - Sacred 2: Fallen Angel for Kynapse symbols.

namespace Marathon.Formats.Kynapse
{
    /// <summary>
    /// Support for *.pdl files; used for Kynapse spatial graphs.
    /// </summary>
    [FileType("Kynogon Spatial Graph", "Kynapse", _extension)]
    public class KynogonSpatialGraph : FileBase
    {
        private const string _extension = ".pdl"; // "sPatial Data List" (speculatory; from Fable II, may not be correct)
        private const string _signature = "Kynogon Spatial graph";
        private const int _version = 3;

        public float CoverageDistance { get; set; }

        public List<SpatialGraphVertex> Vertices { get; set; } = [];

        public List<uint> VertexAstarPredecessors { get; set; } = [];

        public List<uint> VertexFirstInEdges { get; set; } = [];

        public List<uint> VertexFirstOutEdges { get; set; } = [];

        public List<uint> Edges { get; set; } = [];

        public List<uint> StartVertices { get; set; } = [];

        public List<uint> EndVertices { get; set; } = [];

        public List<uint> NextInEdges { get; set; } = [];

        public List<uint> NextOutEdges { get; set; } = [];

        public List<float> EdgeLengths { get; set; } = [];

        public List<float> EdgeCosts { get; set; } = [];

        public List<uint> EdgePathObjects { get; set; } = [];

        public override string Extension => _extension;

        public KynogonSpatialGraph() { }

        public KynogonSpatialGraph(string in_path) : base(in_path) { }

        public KynogonSpatialGraph(Stream in_stream) : base(in_stream) { }

        public KynogonSpatialGraph(IFile in_file) : base(in_file) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BinaryObjectReaderEx(in_stream, StreamOwnership.Retain, Endianness.Little);

            reader.CheckSignature(_signature);

            var version = reader.Read<int>();

            if (version != _version)
                throw new InvalidSignatureException(_version, version);

            reader.JumpAhead(0x0B); // Padding.

            var optionalDataFlags = reader.Read<SpatialGraphOptionalDataFlags>();

            CoverageDistance = reader.Read<float>();

            var lastVertexId = reader.Read<uint>();
            var vertexSize = reader.Read<uint>();
            var vertexCount = reader.Read<uint>();

            for (int i = 0; i < vertexCount; i++)
            {
                Vertices.Add(reader.ReadObjectEx<SpatialGraphVertex>());
                reader.JumpAhead(sizeof(uint));
            }

            if (optionalDataFlags.HasFlag(SpatialGraphOptionalDataFlags.HasVertexAstarPredecessorsOrEdgeCosts))
            {
                for (int i = 0; i < vertexSize; i++)
                    VertexAstarPredecessors.Add(reader.Read<uint>());

                for (int i = 0; i < vertexCount; i++)
                    VertexAstarPredecessors[i] = 0;
            }

            if (optionalDataFlags.HasFlag(SpatialGraphOptionalDataFlags.HasInEdges))
            {
                for (int i = 0; i < vertexSize; i++)
                    VertexFirstInEdges.Add(reader.Read<uint>());
            }

            if (optionalDataFlags.HasFlag(SpatialGraphOptionalDataFlags.HasOutEdges))
            {
                for (int i = 0; i < vertexSize; i++)
                    VertexFirstOutEdges.Add(reader.Read<uint>());
            }

            var lastEdgeId = reader.Read<uint>();
            var edgeSize = reader.Read<uint>();
            var edgeCount = reader.Read<uint>();

            for (int i = 0; i < edgeSize; i++)
            {
                Edges.Add(reader.Read<uint>());
                reader.JumpAhead(sizeof(uint));
            }

            for (int i = 0; i < edgeCount; i++)
                StartVertices.Add(reader.Read<uint>());

            for (int i = 0; i < edgeCount; i++)
                EndVertices.Add(reader.Read<uint>());

            if (optionalDataFlags.HasFlag(SpatialGraphOptionalDataFlags.HasInEdges))
            {
                for (int i = 0; i < edgeCount; i++)
                    NextInEdges.Add(reader.Read<uint>());
            }

            if (optionalDataFlags.HasFlag(SpatialGraphOptionalDataFlags.HasOutEdges))
            {
                for (int i = 0; i < edgeCount; i++)
                    NextOutEdges.Add(reader.Read<uint>());
            }

            if (optionalDataFlags.HasFlag(SpatialGraphOptionalDataFlags.HasEdgeLengths))
            {
                for (int i = 0; i < edgeCount; i++)
                    EdgeLengths.Add(reader.Read<float>());
            }

            if (optionalDataFlags.HasFlag(SpatialGraphOptionalDataFlags.HasVertexAstarPredecessorsOrEdgeCosts))
            {
                for (int i = 0; i < edgeCount; i++)
                    EdgeCosts.Add(reader.Read<float>());
            }

            if (optionalDataFlags.HasFlag(SpatialGraphOptionalDataFlags.HasEdgePathObjects))
            {
                for (int i = 0; i < edgeCount; i++)
                    EdgePathObjects.Add(reader.Read<uint>());

                for (int i = 0; i < edgeCount; i++)
                    EdgePathObjects[i] = 0;
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BinaryObjectWriterEx(in_stream, StreamOwnership.Retain, Endianness.Little);

            writer.WriteSignature(_signature);
            writer.Write(_version);

            var padding = new byte[0x0B];
            Array.Fill<byte>(padding, 0xAA);

            writer.WriteArray(padding);

            // These flags are supplied to the Kynapse writer by the
            // programmer and aren't deterministic based on which fields
            // are filled out in the binary format. All spatial graphs in
            // Sonic '06 use all available optional fields.
            var optionalDataFlags = SpatialGraphOptionalDataFlags.HasAll;

            writer.Write(optionalDataFlags);
            writer.Write(CoverageDistance);

            writer.Write(Vertices.Count > 0 ? Vertices.Last().ID + 1 : 0);
            writer.Write(Vertices.Count);
            writer.Write(Vertices.Count);

            foreach (var vertex in Vertices)
            {
                writer.WriteObjectEx(vertex);
                writer.WriteZero<uint>();
            }

            if (optionalDataFlags.HasFlag(SpatialGraphOptionalDataFlags.HasVertexAstarPredecessorsOrEdgeCosts))
            {
                foreach (var vertexAstarPredecessor in VertexAstarPredecessors)
                    writer.Write(vertexAstarPredecessor);
            }

            if (optionalDataFlags.HasFlag(SpatialGraphOptionalDataFlags.HasInEdges))
            {
                foreach (var vertexFirstInEdge in VertexFirstInEdges)
                    writer.Write(vertexFirstInEdge);
            }

            if (optionalDataFlags.HasFlag(SpatialGraphOptionalDataFlags.HasOutEdges))
            {
                foreach (var vertexFirstOutEdge in VertexFirstOutEdges)
                    writer.Write(vertexFirstOutEdge);
            }

            writer.Write(Edges.Count > 0 ? Edges.Last() + 1 : 0);
            writer.Write(Edges.Count);
            writer.Write(Edges.Count);

            foreach (var edge in Edges)
            {
                writer.Write(edge);
                writer.WriteZero<uint>();
            }

            foreach (var startVertex in StartVertices)
                writer.Write(startVertex);

            foreach (var endVertex in EndVertices)
                writer.Write(endVertex);

            if (optionalDataFlags.HasFlag(SpatialGraphOptionalDataFlags.HasInEdges))
            {
                foreach (var nextInEdge in NextInEdges)
                    writer.Write(nextInEdge);
            }

            if (optionalDataFlags.HasFlag(SpatialGraphOptionalDataFlags.HasOutEdges))
            {
                foreach (var nextOutEdge in NextOutEdges)
                    writer.Write(nextOutEdge);
            }

            if (optionalDataFlags.HasFlag(SpatialGraphOptionalDataFlags.HasEdgeLengths))
            {
                foreach (var edgeLength in EdgeLengths)
                    writer.Write(edgeLength);
            }

            if (optionalDataFlags.HasFlag(SpatialGraphOptionalDataFlags.HasVertexAstarPredecessorsOrEdgeCosts))
            {
                foreach (var edgeCost in EdgeCosts)
                    writer.Write(edgeCost);
            }

            if (optionalDataFlags.HasFlag(SpatialGraphOptionalDataFlags.HasEdgePathObjects))
            {
                foreach (var edgePathObject in EdgePathObjects)
                    writer.Write(edgePathObject);
            }
        }

        public override void Import(string in_path)
        {
            throw new NotImplementedException();
        }

        public override void Export(string in_path = "", bool in_overwrite = true)
        {
            EnsurePath(ref in_path, path => FileSystemHelper.EnsureExtension(path, ".obj"));

            if (!in_overwrite)
                ThrowHelper.ThrowFileExistsException(in_path);

            using var writer = new StreamWriter(in_path);

            writer.WriteLine("# Kynogon Spatial Graph");
            writer.WriteLine($"# Coverage Distance: {CoverageDistance}");
            writer.WriteLine();

            foreach (var vertex in Vertices)
                writer.WriteLine($"v {vertex.Position.X} {vertex.Position.Y} {vertex.Position.Z}");

            if (StartVertices.Count <= 0 && EndVertices.Count <= 0)
                return;

            if (StartVertices.Count != EndVertices.Count)
                throw new InvalidDataException("Mismatching start and end vertex count.");

            writer.WriteLine();
            writer.WriteLine($"g {FileSystemHelper.TruncateAllExtensions(Path.GetFileName(in_path))}");

            for (int i = 0; i < StartVertices.Count; i++)
                writer.WriteLine($"l {StartVertices[i] + 1} {EndVertices[i] + 1}");
        }
    }

    public class SpatialGraphVertex : IBinarySerializableEx
    {
        public int ID { get; set; }

        public Vector3 Position { get; set; }

        public SpatialGraphVertex() { }

        public SpatialGraphVertex(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public SpatialGraphVertex(int in_id, Vector3 in_position)
        {
            ID = in_id;
            Position = in_position;
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            ID = in_reader.Read<int>();
            Position = in_reader.Read<Vector3>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(ID);
            in_writer.Write(Position);
        }

        public override string ToString()
        {
            return Position.ToString();
        }
    }

    [Flags]
    public enum SpatialGraphOptionalDataFlags : uint
    {
        None = 0,
        HasVertexAstarPredecessorsOrEdgeCosts = 1,
        HasEdgePathObjects = 2,
        HasEdgeLengths = 4,
        HasOutEdges = 8,
        HasInEdges = 16,
        HasAll = HasVertexAstarPredecessorsOrEdgeCosts | HasEdgePathObjects | HasEdgeLengths | HasOutEdges | HasInEdges
    }
}
