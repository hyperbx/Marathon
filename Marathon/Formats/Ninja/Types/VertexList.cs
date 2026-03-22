using Marathon.Formats.Ninja.Chunks;
using Marathon.Formats.Ninja.Flags;
using Marathon.IO;
using Marathon.IO.Extensions;
using System.Collections.Generic;

namespace Marathon.Formats.Ninja.Types
{
    public class VertexList
    {
        private long _dataOffset;
        private long _verticesOffset;

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

        public VertexList(BinaryObjectReaderEx in_reader, bool in_isMorphTarget = false)
        {
            Read(in_reader, in_isMorphTarget);
        }

        public void Read(BinaryObjectReaderEx in_reader, bool in_isMorphTarget = false)
        {
            if (!in_isMorphTarget)
            {
                Type = in_reader.Read<VertexType>();

                var dataOffset = in_reader.Read<uint>();

                in_reader.JumpTo(InfoChunk.Size + dataOffset);
            }

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

        public void Write(BinaryObjectWriterEx in_writer, uint in_verticesOffset = 0)
        {
            var boneMatrixIndicesPos = (uint)(in_writer.Position - InfoChunk.Size);

            foreach (var index in BoneMatrixIndices)
                in_writer.Write(index);

            _dataOffset = in_writer.Position - InfoChunk.Size;

            in_writer.Write(GetVertexFormat());
            in_writer.Write(FlexibleFormat);
            in_writer.Write(GetVertexSize());
            in_writer.Write(Vertices.Count);
            _verticesOffset = in_writer.Reserve<uint>();

            if (in_verticesOffset != 0)
                in_writer.WriteReserved(_verticesOffset, in_verticesOffset, false);

            in_writer.Write(BoneMatrixIndices.Count);

            if (BoneMatrixIndices.Count <= 0)
            {
                in_writer.Write(0);
            }
            else
            {
                var boneMatrixIndicesOffset = in_writer.Reserve<uint>();
                in_writer.WriteReserved(boneMatrixIndicesOffset, boneMatrixIndicesPos, false);
            }

            in_writer.Write(HDRCommon);
            in_writer.Write(HDRData);
            in_writer.Write(HDRLock);
            in_writer.WriteZero<byte>(8);
        }

        public void WritePointer(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Type);
            var offset = in_writer.Reserve<uint>();
            in_writer.WriteReserved(offset, (uint)_dataOffset, false);
        }
        
        public uint WriteVertices(BinaryObjectWriterEx in_writer, bool in_isMorphTarget = false)
        {
            var verticesOffset = (uint)(in_writer.Position - InfoChunk.Size);

            if (!in_isMorphTarget)
                in_writer.WriteReserved(_verticesOffset, verticesOffset, false);

            foreach (var vertex in Vertices)
            {
                if (vertex.Position != null)
                    in_writer.Write(vertex.Position.Value);

                if (vertex.Weight != null)
                    in_writer.Write(vertex.Weight.Value);

                if (vertex.MatrixIndices != null)
                    in_writer.WriteBytes(vertex.MatrixIndices);

                if (vertex.Normal != null)
                    in_writer.Write(vertex.Normal.Value);

                if (vertex.VertexColorA != null)
                    in_writer.Write(vertex.VertexColorA.Value);

                if (vertex.VertexColorB != null)
                    in_writer.Write(vertex.VertexColorB.Value);

                if (vertex.TextureCoordinates != null)
                {
                    foreach (var coord in vertex.TextureCoordinates)
                        in_writer.Write(coord);
                }

                if (vertex.Tangent != null)
                    in_writer.Write(vertex.Tangent.Value);

                if (vertex.Binormal != null)
                    in_writer.Write(vertex.Binormal.Value);
            }

            return verticesOffset;
        }

        public VertexFormat GetVertexFormat()
        {
            VertexFormat result = 0;

            if (Vertices.Count <= 0)
                return result;

            if (Vertices[0].Position != null)
                result |= VertexFormat.NND_VTXTYPE_XB_POSITION;

            if (Vertices[0].Weight != null)
                result |= VertexFormat.NND_VTXTYPE_XB_WEIGHT3;

            if (Vertices[0].MatrixIndices != null)
                result |= VertexFormat.NND_VTXTYPE_XB_MTX_INDEX4;

            if (Vertices[0].Normal != null)
                result |= VertexFormat.NND_VTXTYPE_XB_NORMAL;

            if (Vertices[0].VertexColorA != null)
                result |= VertexFormat.NND_VTXTYPE_XB_COLOR;

            if (Vertices[0].VertexColorB != null)
                result |= VertexFormat.NND_VTXTYPE_XB_COLOR2;

            if (Vertices[0].TextureCoordinates != null)
            {
                var uvCount = Vertices[0].TextureCoordinates.Count;

                if (uvCount == 1)
                {
                    result |= VertexFormat.NND_VTXTYPE_XB_SINGLETEXCOORD;
                }
                else if (uvCount == 2)
                {
                    result |= VertexFormat.NND_VTXTYPE_XB_DOUBLETEXCOORD;
                }
                else if (uvCount == 3)
                {
                    result |= VertexFormat.NND_VTXTYPE_XB_SINGLETEXCOORD | VertexFormat.NND_VTXTYPE_XB_DOUBLETEXCOORD;
                }
            }

            if (Vertices[0].Tangent != null)
                result |= VertexFormat.NND_VTXTYPE_XB_TANGENT;

            if (Vertices[0].Binormal != null)
                result |= VertexFormat.NND_VTXTYPE_XB_BINORMAL;

            return result;
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
