namespace Marathon.Formats.Mesh.Ninja
{
    /// <summary>
    /// Structure of a Ninja Object Node entry.
    /// </summary>
    public class NinjaNode
    {
        // Not actually part of the Node, used purely so we don't keep having to go back and forth between the Node Name List and the Nodes themselves.
        public string Name { get; set; }

        public NinjaNext_NodeType Type { get; set; }

        public short MatrixIndex { get; set; } = -1;

        public short ParentIndex { get; set; } = -1;

        public short ChildIndex { get; set; } = -1;

        public short SiblingIndex { get; set; } = -1;

        public Vector3 Translation { get; set; }

        public Vector3 Rotation { get; set; }

        public Vector3 Scaling { get; set; }

        public float[] InvInitMatrix { get; set; }

        public Vector3 Center { get; set; }

        public float Radius { get; set; }

        public uint UserDefined { get; set; }

        public Vector3 BoundingBox { get; set; }

        public override string ToString() => Name;

        /// <summary>
        /// Reads a Ninja Object Node from a file.
        /// </summary>
        /// <param name="reader">The binary reader for this SegaNN file.</param>
        public void Read(BinaryReaderEx reader)
        {
            Type = (NinjaNext_NodeType)reader.ReadUInt32();
            MatrixIndex = reader.ReadInt16();
            ParentIndex = reader.ReadInt16();
            ChildIndex = reader.ReadInt16();
            SiblingIndex = reader.ReadInt16();
            Translation = reader.ReadVector3();
            Rotation = reader.ReadVector3();
            Scaling = reader.ReadVector3();

            // Set up and read all the entries for the InvInitMatrix.
            InvInitMatrix = new float[16];
            for(int i = 0; i < InvInitMatrix.Length; i++)
                InvInitMatrix[i] = reader.ReadSingle();

            Center = reader.ReadVector3();
            Radius = reader.ReadSingle();
            UserDefined = reader.ReadUInt32();
            BoundingBox = reader.ReadVector3();
        }

        /// <summary>
        /// Writes this Ninja Object Node to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        public void Write(BinaryWriterEx writer)
        {
            writer.Write((uint)Type);
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
