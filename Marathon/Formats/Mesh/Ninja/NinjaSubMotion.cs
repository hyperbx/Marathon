using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathon.Formats.Mesh.Ninja
{
    public class NinjaSubMotion
    {
        public NNE_SMOTTYPE Type { get; set; }

        public NNE_SMOTIPTYPE InterpolationType { get; set; }

        public int ID { get; set; }

        public float StartFrame { get; set; }

        public float EndFrame { get; set; }

        public float StartKeyframe { get; set; }

        public float EndKeyframe { get; set; }

        public List<object> Keyframes = new();

        public void Read(BinaryReaderEx reader)
        {
            Type = (NNE_SMOTTYPE)reader.ReadUInt32();
            InterpolationType = (NNE_SMOTIPTYPE)reader.ReadUInt32();
            ID = reader.ReadInt32();
            StartFrame = reader.ReadSingle();
            EndFrame = reader.ReadSingle();
            StartKeyframe = reader.ReadSingle();
            EndKeyframe = reader.ReadSingle();
            uint KeyFrameCount = reader.ReadUInt32();
            uint KeyFrameSize = reader.ReadUInt32();
            uint KeyFrameOffset = reader.ReadUInt32();

            long pos = reader.BaseStream.Position;

            reader.JumpTo(KeyFrameOffset, true);
            for (int i = 0; i < KeyFrameCount; i++)
            {
                if (Type.HasFlag(NNE_SMOTTYPE.NND_SMOTTYPE_TRANSLATION_MASK) || Type.HasFlag(NNE_SMOTTYPE.NND_SMOTTYPE_SCALING_MASK) || Type.HasFlag(NNE_SMOTTYPE.NND_SMOTTYPE_AMBIENT_MASK) ||
                    Type.HasFlag(NNE_SMOTTYPE.NND_SMOTTYPE_DIFFUSE_MASK) || Type.HasFlag(NNE_SMOTTYPE.NND_SMOTTYPE_SPECULAR_MASK) || Type.HasFlag(NNE_SMOTTYPE.NND_SMOTTYPE_LIGHT_COLOR_MASK))
                {
                    NinjaKeyframe.NNS_MOTION_KEY_VECTOR Keyframe = new();
                    Keyframe.Read(reader);
                    Keyframes.Add(Keyframe);
                }
                else if (Type.HasFlag(NNE_SMOTTYPE.NND_SMOTTYPE_ROTATION_XYZ))
                {
                    NinjaKeyframe.NNS_MOTION_KEY_ROTATE_A16 Keyframe = new();
                    Keyframe.Read(reader);
                    Keyframes.Add(Keyframe);
                }
                // Generic Handling, these could go tits up.
                else if(Type.HasFlag(NNE_SMOTTYPE.NND_SMOTTYPE_FRAME_FLOAT) && KeyFrameSize == 8)
                {
                    NinjaKeyframe.NNS_MOTION_KEY_FLOAT Keyframe = new();
                    Keyframe.Read(reader);
                    Keyframes.Add(Keyframe);
                }
                else if(Type.HasFlag(NNE_SMOTTYPE.NND_SMOTTYPE_FRAME_SINT16) && KeyFrameSize == 4)
                {
                    NinjaKeyframe.NNS_MOTION_KEY_SINT16 Keyframe = new();
                    Keyframe.Read(reader);
                    Keyframes.Add(Keyframe);
                }
                // All else has failed, give up.
                else
                {
                    throw new NotImplementedException();
                }
            }

            reader.JumpTo(pos);
        }
    }
}
