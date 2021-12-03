using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathon.Formats.Mesh.Ninja
{
    public class NinjaMotion
    {
        public string ChunkID { get; set; }

        public NinjaNext_MotionType Type { get; set; }

        public float StartFrame { get; set; }

        public float EndFrame { get; set; }

        public List<NinjaSubMotion> SubMotions = new();

        public float Framerate { get; set; }

        public uint Reserved0 { get; set; }

        public uint Reserved1 { get; set; }

        public void Read(BinaryReaderEx reader)
        {
            uint dataOffset = reader.ReadUInt32();

            reader.JumpTo(dataOffset, true);

            Type = (NinjaNext_MotionType)reader.ReadUInt32();
            StartFrame = reader.ReadSingle();
            EndFrame = reader.ReadSingle();
            uint SubMotionCount = reader.ReadUInt32();
            uint SubMotionsOffset = reader.ReadUInt32();
            Framerate = reader.ReadSingle();
            Reserved0 = reader.ReadUInt32();
            Reserved1 = reader.ReadUInt32();

            reader.JumpTo(SubMotionsOffset, true);
            for (int i = 0; i < SubMotionCount; i++)
            {
                NinjaSubMotion subMotion = new();
                subMotion.Read(reader);
                SubMotions.Add(subMotion);
            }
        }

        public void Write(BinaryWriterEx writer)
        {
            Dictionary<string, uint> MotionOffsets = new();

            // Chunk Header.
            writer.Write(ChunkID);
            writer.Write("SIZE");
            long HeaderSizePosition = writer.BaseStream.Position;
            writer.AddOffset("dataOffset");
            writer.FixPadding(0x10);

            for (int i = 0; i < SubMotions.Count; i++)
            {
                MotionOffsets.Add($"SubMotion{i}KeyframesOffset", (uint)writer.BaseStream.Position);
                for (int k = 0; k < SubMotions[i].Keyframes.Count; k++)
                {
                    if (SubMotions[i].Type.HasFlag(NinjaNext_SubMotionType.NND_SMOTTYPE_TRANSLATION_MASK) || SubMotions[i].Type.HasFlag(NinjaNext_SubMotionType.NND_SMOTTYPE_SCALING_MASK) ||
                        SubMotions[i].Type.HasFlag(NinjaNext_SubMotionType.NND_SMOTTYPE_AMBIENT_MASK) || SubMotions[i].Type.HasFlag(NinjaNext_SubMotionType.NND_SMOTTYPE_DIFFUSE_MASK) ||
                        SubMotions[i].Type.HasFlag(NinjaNext_SubMotionType.NND_SMOTTYPE_SPECULAR_MASK) || SubMotions[i].Type.HasFlag(NinjaNext_SubMotionType.NND_SMOTTYPE_LIGHT_COLOR_MASK))
                    {
                        writer.Write((SubMotions[i].Keyframes[k] as NinjaKeyframe.NNS_MOTION_KEY_VECTOR).Frame);
                        writer.Write((SubMotions[i].Keyframes[k] as NinjaKeyframe.NNS_MOTION_KEY_VECTOR).Value);
                    }
                    else if (SubMotions[i].Type.HasFlag(NinjaNext_SubMotionType.NND_SMOTTYPE_ROTATION_XYZ))
                    {
                        writer.Write((SubMotions[i].Keyframes[k] as NinjaKeyframe.NNS_MOTION_KEY_ROTATE_A16).Frame);
                        writer.Write((SubMotions[i].Keyframes[k] as NinjaKeyframe.NNS_MOTION_KEY_ROTATE_A16).Value);
                        writer.Write((SubMotions[i].Keyframes[k] as NinjaKeyframe.NNS_MOTION_KEY_ROTATE_A16).Value2);
                        writer.Write((SubMotions[i].Keyframes[k] as NinjaKeyframe.NNS_MOTION_KEY_ROTATE_A16).Value3);
                    }
                    else if (SubMotions[i].Type.HasFlag(NinjaNext_SubMotionType.NND_SMOTTYPE_FRAME_FLOAT))
                    {
                        writer.Write((SubMotions[i].Keyframes[k] as NinjaKeyframe.NNS_MOTION_KEY_FLOAT).Frame);
                        writer.Write((SubMotions[i].Keyframes[k] as NinjaKeyframe.NNS_MOTION_KEY_FLOAT).Value);
                    }
                    else if (SubMotions[i].Type.HasFlag(NinjaNext_SubMotionType.NND_SMOTTYPE_FRAME_SINT16))
                    {
                        writer.Write((SubMotions[i].Keyframes[k] as NinjaKeyframe.NNS_MOTION_KEY_SINT16).Frame);
                        writer.Write((SubMotions[i].Keyframes[k] as NinjaKeyframe.NNS_MOTION_KEY_SINT16).Value);
                    }
                    else
                    {
                        throw new NotImplementedException();
                    }
                }
            }

            MotionOffsets.Add($"SubMotionTable", (uint)writer.BaseStream.Position);
            for (int i = 0; i < SubMotions.Count; i++)
            {
                writer.Write((uint)SubMotions[i].Type);
                writer.Write((uint)SubMotions[i].InterpolationType);
                writer.Write(SubMotions[i].ID);
                writer.Write(SubMotions[i].StartFrame);
                writer.Write(SubMotions[i].EndFrame);
                writer.Write(SubMotions[i].StartKeyframe);
                writer.Write(SubMotions[i].EndKeyframe);
                writer.Write(SubMotions[i].Keyframes.Count);
                if (SubMotions[i].Type.HasFlag(NinjaNext_SubMotionType.NND_SMOTTYPE_TRANSLATION_MASK) || SubMotions[i].Type.HasFlag(NinjaNext_SubMotionType.NND_SMOTTYPE_SCALING_MASK) ||
                    SubMotions[i].Type.HasFlag(NinjaNext_SubMotionType.NND_SMOTTYPE_AMBIENT_MASK) || SubMotions[i].Type.HasFlag(NinjaNext_SubMotionType.NND_SMOTTYPE_DIFFUSE_MASK) ||
                    SubMotions[i].Type.HasFlag(NinjaNext_SubMotionType.NND_SMOTTYPE_SPECULAR_MASK) || SubMotions[i].Type.HasFlag(NinjaNext_SubMotionType.NND_SMOTTYPE_LIGHT_COLOR_MASK))
                    writer.Write(16);
                else if (SubMotions[i].Type.HasFlag(NinjaNext_SubMotionType.NND_SMOTTYPE_ROTATION_XYZ))
                    writer.Write(8);
                else if (SubMotions[i].Type.HasFlag(NinjaNext_SubMotionType.NND_SMOTTYPE_FRAME_FLOAT))
                    writer.Write(8);
                else if (SubMotions[i].Type.HasFlag(NinjaNext_SubMotionType.NND_SMOTTYPE_FRAME_SINT16))
                    writer.Write(4);
                else
                    throw new NotImplementedException();
                writer.AddOffset($"SubMotion{i}KeyframesOffset", 0);
                writer.Write(MotionOffsets[$"SubMotion{i}KeyframesOffset"] - writer.Offset);
            }

            writer.FillOffset("dataOffset", true);
            writer.Write((uint)Type);
            writer.Write(StartFrame);
            writer.Write(EndFrame);
            writer.Write(SubMotions.Count);
            writer.AddOffset($"SubMotionTable", 0);
            writer.Write(MotionOffsets[$"SubMotionTable"] - writer.Offset);
            writer.Write(Framerate);
            writer.Write(Reserved0);
            writer.Write(Reserved1);

            // Alignment.
            writer.FixPadding(0x10);

            // Chunk Size.
            long ChunkEndPosition = writer.BaseStream.Position;
            uint ChunkSize = (uint)(ChunkEndPosition - HeaderSizePosition);
            writer.BaseStream.Position = HeaderSizePosition - 0x04;
            writer.Write(ChunkSize);
            writer.BaseStream.Position = ChunkEndPosition;
        }
    }
}
