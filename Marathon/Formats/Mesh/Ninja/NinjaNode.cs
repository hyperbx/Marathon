namespace Marathon.Formats.Mesh.Ninja
{
    public class NinjaNode
    {
        public uint Type { get; set; }

        public short MatrixIndex { get; set; }

        public short ParentIndex { get; set; }

        public short ChildIndex { get; set; }

        public short SiblingIndex { get; set; }

        public Vector3 Translation { get; set; }

        public Vector3 Rotation { get; set; }

        public Vector3 Scaling { get; set; }

        public float[] InvInitMatrix { get; set; }

        public Vector3 Center { get; set; }

        public float Radius { get; set; }

        public uint UserDefined { get; set; }

        public Vector3 BoundingBox { get; set; }

        public void Read(BinaryReaderEx reader)
        {
            Type = reader.ReadUInt32();
            MatrixIndex = reader.ReadInt16();
            ParentIndex = reader.ReadInt16();
            ChildIndex = reader.ReadInt16();
            SiblingIndex = reader.ReadInt16();
            Translation = reader.ReadVector3();
            Rotation = reader.ReadVector3();
            Scaling = reader.ReadVector3();

            InvInitMatrix = new float[16];
            for(int i = 0; i < InvInitMatrix.Length; i++)
                InvInitMatrix[i] = reader.ReadSingle();

            Center = reader.ReadVector3();
            Radius = reader.ReadSingle();
            UserDefined = reader.ReadUInt32();
            BoundingBox = reader.ReadVector3();
        }

        public void Write(BinaryWriterEx writer)
        {
            writer.Write(Type);
            writer.Write(MatrixIndex);
            writer.Write(ParentIndex);
            writer.Write(ChildIndex);
            writer.Write(SiblingIndex);
            writer.Write(Translation);
            writer.Write(Rotation);
            writer.Write(Scaling);
            for (int i = 0; i != InvInitMatrix.Length; i++)
                writer.Write(InvInitMatrix[i]);
            writer.Write(Center);
            writer.Write(Radius);
            writer.Write(UserDefined);
            writer.Write(BoundingBox);
        }
    }
}
