using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathon.Formats.Mesh.Ninja
{
    public class NinjaKeyframe
    {
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

        public class NNS_MOTION_KEY_SINT16
        {
            public short Frame { get; set; }

            public short Value { get; set; } // Uses BAM.

            public void Read(BinaryReaderEx reader)
            {
                Frame = reader.ReadInt16();
                Value = reader.ReadInt16();
            }
        }

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
