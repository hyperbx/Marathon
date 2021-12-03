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

            public short Value { get; set; } // Might be a half? The XTM has 0.000000F as the value for these?

            public void Read(BinaryReaderEx reader)
            {
                Frame = reader.ReadInt16();
                Value = reader.ReadInt16();
            }
        }
    }
}
