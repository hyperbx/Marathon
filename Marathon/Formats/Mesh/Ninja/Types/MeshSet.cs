using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Mesh.Ninja.Types
{
    public class MeshSet
    {
        public Vector3 Centre { get; set; }

        public float Radius { get; set; }

        public int NodeIndex { get; set; }

        public int MatrixIndex { get; set; }

        public int MaterialIndex { get; set; }

        public int VertexListIndex { get; set; }

        public int PrimitiveListIndex { get; set; }

        public int ShaderIndex { get; set; }

        public MeshSet() { }

        public MeshSet(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Centre = in_reader.Read<Vector3>();
            Radius = in_reader.Read<float>();
            NodeIndex = in_reader.Read<int>();
            MatrixIndex = in_reader.Read<int>();
            MaterialIndex = in_reader.Read<int>();
            VertexListIndex = in_reader.Read<int>();
            PrimitiveListIndex = in_reader.Read<int>();
            ShaderIndex = in_reader.Read<int>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Centre);
            in_writer.Write(Radius);
            in_writer.Write(NodeIndex);
            in_writer.Write(MatrixIndex);
            in_writer.Write(MaterialIndex);
            in_writer.Write(VertexListIndex);
            in_writer.Write(PrimitiveListIndex);
            in_writer.Write(ShaderIndex);
        }
    }
}
