using Marathon.Formats.Ninja.Flags;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types;
using System.Collections.Generic;
using System.Numerics;

namespace Marathon.Formats.Ninja.Types
{
    public class Vertex
    {
        public Vector3? Position { get; set; }

        public Vector3? Weight { get; set; }

        public byte[]? MatrixIndexes { get; set; }

        public Vector3? Normal { get; set; }

        public Color<byte, BGRA>? VertexColorA { get; set; }

        public Color<byte, BGRA>? VertexColorB { get; set; }

        public List<Vector2> TextureCoordinates { get; set; }

        public Vector3? Tangent { get; set; }

        public Vector3? Binormal { get; set; }

        public Vertex() { }

        public Vertex(BinaryObjectReaderEx in_reader, VertexList in_vertexList)
        {
            Read(in_reader, in_vertexList);
        }

        public void Read(BinaryObjectReaderEx in_reader, VertexList in_vertexList)
        {
            if (in_vertexList.Format.HasFlag(VertexFormat.NND_VTXTYPE_XB_POSITION))
                Position = in_reader.Read<Vector3>();

            if (in_vertexList.Format.HasFlag(VertexFormat.NND_VTXTYPE_XB_WEIGHT3))
                Weight = in_reader.Read<Vector3>();

            if (in_vertexList.Format.HasFlag(VertexFormat.NND_VTXTYPE_XB_MTX_INDEX4))
                MatrixIndexes = in_reader.ReadBytes(4);

            if (in_vertexList.Format.HasFlag(VertexFormat.NND_VTXTYPE_XB_NORMAL))
                Normal = in_reader.Read<Vector3>();

            if (in_vertexList.Format.HasFlag(VertexFormat.NND_VTXTYPE_XB_COLOR))
                VertexColorA = in_reader.ReadObject<Color<byte, BGRA>>();

            if (in_vertexList.Format.HasFlag(VertexFormat.NND_VTXTYPE_XB_COLOR2))
                VertexColorB = in_reader.ReadObject<Color<byte, BGRA>>();

            for (int i = 0; i < (uint)in_vertexList.Format / (uint)VertexFormat.NND_VTXTYPE_XB_SINGLETEXCOORD; i++)
            {
                TextureCoordinates ??= [];
                TextureCoordinates.Add(in_reader.Read<Vector2>());
            }

            if (in_vertexList.Format.HasFlag(VertexFormat.NND_VTXTYPE_XB_TANGENT))
                Tangent = in_reader.Read<Vector3>();

            if (in_vertexList.Format.HasFlag(VertexFormat.NND_VTXTYPE_XB_BINORMAL))
                Binormal = in_reader.Read<Vector3>();
        }
    }
}
