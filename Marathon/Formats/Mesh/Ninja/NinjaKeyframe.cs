namespace Marathon.Formats.Mesh.Ninja
{
    /// <summary>
    /// Different structures for different keyframe types.
    /// </summary>
    public class NinjaKeyframe
    {
        /// <summary>
        /// Keyframe that consists of a single floating point value.
        /// </summary>
        public class NNS_MOTION_KEY_FLOAT
        {
            public float Frame { get; set; }

            public float Value { get; set; }

            public void Read(BinaryReaderEx reader)
            {
                Frame = reader.ReadSingle();
                Value = reader.ReadSingle();
            }
        }

        /// <summary>
        /// Keyframe that consists of a single short, usually using the Binary Angle Measurement System.
        /// </summary>
        public class NNS_MOTION_KEY_SINT16
        {
            public short Frame { get; set; }

            public short Value { get; set; }

            public void Read(BinaryReaderEx reader)
            {
                Frame = reader.ReadInt16();
                Value = reader.ReadInt16();
            }
        }

        /// <summary>
        /// Keyframe that consists of a Vector3.
        /// </summary>
        public class NNS_MOTION_KEY_VECTOR
        {
            public float Frame { get; set; }

            public Vector3 Value { get; set; }

            public void Read(BinaryReaderEx reader)
            {
                Frame = reader.ReadSingle();
                Value = reader.ReadVector3();
            }
        }

        /// <summary>
        /// Keyframe that consists of three shorts, usually using the Binary Angle Measurement System.
        /// </summary>
        public class NNS_MOTION_KEY_ROTATE_A16
        {
            public short Frame { get; set; }

            public short Value1 { get; set; } 

            public short Value2 { get; set; }

            public short Value3 { get; set; }

            public void Read(BinaryReaderEx reader)
            {
                Frame = reader.ReadInt16();
                Value1 = reader.ReadInt16();
                Value2 = reader.ReadInt16();
                Value3 = reader.ReadInt16();
            }
        }
    }
}
