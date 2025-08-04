using Marathon.Formats.Mesh.Ninja.Chunks;
using Marathon.Formats.Mesh.Ninja.Flags;
using Marathon.IO;
using Marathon.IO.Extensions;
using System.Collections.Generic;

namespace Marathon.Formats.Mesh.Ninja.Types
{
    public class VertexList
    {
        private uint _infoOffset;
        private uint _verticesOffset;

        public const int InfoSize = 8;

        public VertexType Type { get; set; }

        public VertexFormat Format { get; set; }

        public FlexibleVertexFormat FlexibleFormat { get; set; }

        public List<Vertex> Vertices { get; set; } = [];

        public List<int> BoneMatrixIndices { get; set; } = [];

        public uint HDRCommon { get; set; }

        public uint HDRData { get; set; }

        public uint HDRLock { get; set; }

        public VertexList() { }

        public VertexList(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Type = in_reader.Read<VertexType>();

            var infoOffset = in_reader.Read<uint>();

            in_reader.JumpTo(InfoChunk.Size + infoOffset);

            Format = in_reader.Read<VertexFormat>();
            FlexibleFormat = in_reader.Read<FlexibleVertexFormat>();

            var vertexSize = in_reader.Read<uint>();
            var vertexCount = in_reader.Read<uint>();
            var vertexListOffset = in_reader.Read<uint>();
            var boneCount = in_reader.Read<uint>();
            var boneMatrixIndicesOffset = in_reader.Read<uint>();

            HDRCommon = in_reader.Read<uint>();
            HDRData = in_reader.Read<uint>();
            HDRLock = in_reader.Read<uint>();

            in_reader.JumpTo(InfoChunk.Size + vertexListOffset);

            for (int i = 0; i < vertexCount; i++)
                Vertices.Add(new Vertex(in_reader, this));

            in_reader.JumpTo(InfoChunk.Size + boneMatrixIndicesOffset);

            for (int i = 0; i < boneCount; i++)
                BoneMatrixIndices.Add(in_reader.Read<int>());
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            var boneMatrixIndicesPos = in_writer.Position;

            foreach (var index in BoneMatrixIndices)
                in_writer.Write(index);

            _infoOffset = (uint)in_writer.Position;

            in_writer.Write(Format);
            in_writer.Write(FlexibleFormat);
            in_writer.Write(GetVertexSize());
            in_writer.Write(Vertices.Count);
            _verticesOffset = in_writer.Reserve<uint>();
            in_writer.Write(BoneMatrixIndices.Count);

            if (BoneMatrixIndices.Count <= 0)
            {
                in_writer.Write(0);
            }
            else
            {
                var boneMatrixIndicesOffset = in_writer.Reserve<uint>();

                in_writer.WriteReserved(boneMatrixIndicesOffset, (uint)(boneMatrixIndicesPos - InfoChunk.Size), false);
            }

            in_writer.Write(HDRCommon);
            in_writer.Write(HDRData);
            in_writer.Write(HDRLock);
            in_writer.WriteNullBytes(8);
        }

        public void WritePointer(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Type);
            var offset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(offset, _infoOffset - InfoChunk.Size, false);
        }
        
        public void WriteVertices(BinaryObjectWriterEx in_writer)
        {
            in_writer.WriteReserved(_verticesOffset, (uint)(in_writer.Position - InfoChunk.Size), false);

            foreach (var vertex in Vertices)
            {
                if (vertex.Position != null)
                    in_writer.Write(vertex.Position.Value);

                if (vertex.Weight != null)
                    in_writer.Write(vertex.Weight.Value);

                if (vertex.MatrixIndices != null)
                    in_writer.WriteBytes(vertex.MatrixIndices);

                if (vertex.Normals != null)
                    in_writer.Write(vertex.Normals.Value);

                if (vertex.VertexColoursA != null)
                    in_writer.WriteBytes(vertex.VertexColoursA);

                if (vertex.VertexColoursB != null)
                    in_writer.WriteBytes(vertex.VertexColoursB);

                if (vertex.TextureCoordinates != null)
                {
                    foreach (var coord in vertex.TextureCoordinates)
                        in_writer.Write(coord);
                }

                if (vertex.Tangent != null)
                    in_writer.Write(vertex.Tangent.Value);

                if (vertex.Binormals != null)
                    in_writer.Write(vertex.Binormals.Value);
            }
        }

        public int GetVertexSize()
        {
            var result = 0;

            if (Format.HasFlag(VertexFormat.NND_VTXTYPE_XB_POSITION))
                result += 12;

            if (Format.HasFlag(VertexFormat.NND_VTXTYPE_XB_WEIGHT3))
                result += 12;

            if (Format.HasFlag(VertexFormat.NND_VTXTYPE_XB_MTX_INDEX4))
                result += 4;

            if (Format.HasFlag(VertexFormat.NND_VTXTYPE_XB_NORMAL))
                result += 12;

            if (Format.HasFlag(VertexFormat.NND_VTXTYPE_XB_COLOR))
                result += 4;

            if (Format.HasFlag(VertexFormat.NND_VTXTYPE_XB_COLOR2))
                result += 4;

            if (Vertices[0].TextureCoordinates != null)
                result += 8 * Vertices[0].TextureCoordinates.Count;

            if (Format.HasFlag(VertexFormat.NND_VTXTYPE_XB_TANGENT))
                result += 12;

            if (Format.HasFlag(VertexFormat.NND_VTXTYPE_XB_BINORMAL))
                result += 12;

            return result;
        }
    }
}
