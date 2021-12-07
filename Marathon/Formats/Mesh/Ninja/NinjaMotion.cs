namespace Marathon.Formats.Mesh.Ninja
{
    /// <summary>
    /// Structure of the main Ninja Motion data.
    /// </summary>
    public class NinjaMotion
    {
        // NinjaMotion is used by a lot of things, so we store the Chunk ID so we know what to write back.
        public string ChunkID { get; set; }

        public NinjaNext_MotionType Type { get; set; }

        public float StartFrame { get; set; }

        public float EndFrame { get; set; }

        public List<NinjaSubMotion> SubMotions = new();

        public float Framerate { get; set; }

        public uint Reserved0 { get; set; }

        public uint Reserved1 { get; set; }

        /// <summary>
        /// Reads the Ninja Motion data from a file.
        /// </summary>
        /// <param name="reader">The binary reader for this SegaNN file.</param>
        public void Read(BinaryReaderEx reader)
        {
            // Read the offset to the actual Ninja Motion data.
            uint dataOffset = reader.ReadUInt32();

            // Jump to the actual Ninja Motion data.
            reader.JumpTo(dataOffset, true);

            // Read all of the data from the Ninja Motion data.
            Type = (NinjaNext_MotionType)reader.ReadUInt32();
            StartFrame = reader.ReadSingle();
            EndFrame = reader.ReadSingle();
            uint SubMotionCount = reader.ReadUInt32();
            uint SubMotionsOffset = reader.ReadUInt32();
            Framerate = reader.ReadSingle();
            Reserved0 = reader.ReadUInt32();
            Reserved1 = reader.ReadUInt32();

            // Jump to the offset for this motion data's sub motions.
            reader.JumpTo(SubMotionsOffset, true);

            // Loop through and read all the sub motions.
            for (int i = 0; i < SubMotionCount; i++)
            {
                NinjaSubMotion subMotion = new();
                subMotion.Read(reader);
                SubMotions.Add(subMotion);
            }
        }

        /// <summary>
        /// Write the Ninja Motion data to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        public void Write(BinaryWriterEx writer)
        {
            // Set up a list of Offsets for earlier points in the chunk.
            Dictionary<string, uint> MotionOffsets = new();

            // Chunk Header.
            writer.Write(ChunkID);
            writer.Write("SIZE"); // Temporary entry, is filled in later once we know this chunk's size.
            long HeaderSizePosition = writer.BaseStream.Position;
            writer.AddOffset("dataOffset");
            writer.FixPadding(0x10);

            // Keyframes.
            for (int i = 0; i < SubMotions.Count; i++)
            {
                // Add an offset to our list so we know where this sub motion's keyframes are.
                MotionOffsets.Add($"SubMotion{i}KeyframesOffset", (uint)writer.BaseStream.Position);

                // Loop through this sub motions keyframes and write different data depending on the Type flag.
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
                        writer.Write((SubMotions[i].Keyframes[k] as NinjaKeyframe.NNS_MOTION_KEY_ROTATE_A16).Value1);
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
                        // If none of those flags are found, error out.
                        throw new NotImplementedException();
                    }
                }
            }

            // Sub Motions.
            // Add an offset to our list so we know where the sub motions are.
            MotionOffsets.Add($"SubMotionTable", (uint)writer.BaseStream.Position);
            for (int i = 0; i < SubMotions.Count; i++)
            {
                // Write most of the data for this sub motion.
                writer.Write((uint)SubMotions[i].Type);
                writer.Write((uint)SubMotions[i].InterpolationType);
                writer.Write(SubMotions[i].NodeIndex);
                writer.Write(SubMotions[i].StartFrame);
                writer.Write(SubMotions[i].EndFrame);
                writer.Write(SubMotions[i].StartKeyframe);
                writer.Write(SubMotions[i].EndKeyframe);
                writer.Write(SubMotions[i].Keyframes.Count);

                // Figure out the size value to write based on the flags.
                // Error out if none of them are found.
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

                // Add an offset to the BinaryWriter so we can fill it in in the NOF0 chunk.
                writer.AddOffset($"SubMotion{i}KeyframesOffset", 0);

                // Write the previously saved position for this sub motion's keyframes.
                writer.Write(MotionOffsets[$"SubMotion{i}KeyframesOffset"] - writer.Offset);
            }

            // Chunk Data.
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
