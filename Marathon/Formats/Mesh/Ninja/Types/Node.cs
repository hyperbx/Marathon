using Marathon.Formats.Mesh.Ninja.Flags;
using Marathon.Formats.Script.Lua.Types;
using Marathon.IO;
using System.Numerics;

namespace Marathon.Formats.Mesh.Ninja.Types
{
    public class Node
    {
        public NodeType Type { get; set; }

        public short MatrixIndex { get; set; } = -1;

        public short ParentIndex { get; set; } = -1;

        public short ChildIndex { get; set; } = -1;

        public short SiblingIndex { get; set; } = -1;

        public Vector3 Translation { get; set; }

        public Vector3 Rotation { get; set; }

        public Vector3 Scale { get; set; }

        public Matrix4x4 InvInitMatrix { get; set; }

        public Vector3 Center { get; set; }

        public float Radius { get; set; }

        public uint UserData { get; set; }

        public Vector3 BoundingBox { get; set; }

        public Node() { }

        public Node(BinaryObjectReaderEx in_reader)
        {
            Read(in_reader);
        }

        public void Read(BinaryObjectReaderEx in_reader)
        {
            Type = in_reader.Read<NodeType>();
            MatrixIndex = in_reader.Read<short>();
            ParentIndex = in_reader.Read<short>();
            ChildIndex = in_reader.Read<short>();
            SiblingIndex = in_reader.Read<short>();
            Translation = in_reader.Read<Vector3>();
            Rotation = in_reader.Read<Vector3>();
            Scale = in_reader.Read<Vector3>();
            InvInitMatrix = in_reader.Read<Matrix4x4>();
            Center = in_reader.Read<Vector3>();
            Radius = in_reader.Read<float>();
            UserData = in_reader.Read<uint>();
            BoundingBox = in_reader.Read<Vector3>();
        }

        public void Write(BinaryObjectWriterEx in_writer)
        {
            in_writer.Write(Type);
            in_writer.Write(MatrixIndex);
            in_writer.Write(ParentIndex);
            in_writer.Write(ChildIndex);
            in_writer.Write(SiblingIndex);
            in_writer.Write(Translation);
            in_writer.Write(Rotation);
            in_writer.Write(Scale);
            in_writer.Write(InvInitMatrix);
            in_writer.Write(Center);
            in_writer.Write(Radius);
            in_writer.Write(UserData);
            in_writer.Write(BoundingBox);
        }
    }
}
