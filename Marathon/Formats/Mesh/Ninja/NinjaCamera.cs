namespace Marathon.Formats.Mesh.Ninja
{
    /// <summary>
    /// Structure of the main Ninja Camera.
    /// TODO: This is all wild guessing which I'm 100% sure is all sorts of bollocks.
    /// </summary>
    public class NinjaCamera
    {
        public NinjaNext_CameraType Type { get; set; }

        public uint UnknownUInt32_1 { get; set; } // Always 0.

        public uint UnknownUInt32_2 { get; set; } // Flags maybe?

        public Vector3 UnknownVector3_1 { get; set; }

        public Vector3 UnknownVector3_2 { get; set; }

        public float UnknownFloat_1 { get; set; } // Seems like it ends up as NaN a lot? Maybe a coicidence that it's occasionally sensible numbers???

        public float UnknownFloat_2 { get; set; } // Seems like it ends up as NaN a lot? Maybe a coicidence that it's occasionally sensible numbers???

        public float UnknownFloat_3 { get; set; } // Could be either a float or uint maybe?

        /// <summary>
        /// Reads the Ninja Camera from a file.
        /// </summary>
        /// <param name="reader">The binary reader for this SegaNN file.</param>
        public void Read(BinaryReaderEx reader)
        {
            // Read the offset to the actual Ninja Camera.
            uint dataOffset = reader.ReadUInt32();

            // Jump to the actual Ninja Camera.
            reader.JumpTo(dataOffset, true);

            // Read the Type(?) of this Camera and the offset to the camera data.
            Type = (NinjaNext_CameraType)reader.ReadUInt32();
            uint CameraOffset = reader.ReadUInt32();

            // Jump to and read the data for this Camera.
            reader.JumpTo(CameraOffset, true);
            UnknownUInt32_1 = reader.ReadUInt32();
            UnknownUInt32_2 = reader.ReadUInt32();
            UnknownVector3_1 = reader.ReadVector3();
            UnknownVector3_2 = reader.ReadVector3();
            UnknownFloat_1 = reader.ReadSingle();
            UnknownFloat_2 = reader.ReadSingle();
            UnknownFloat_3 = reader.ReadSingle();
        }

        /// <summary>
        /// Write the Ninja Camera to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        public void Write(BinaryWriterEx writer)
        {
            // Chunk Header.
            writer.Write("NXCA");
            writer.Write("SIZE");
            long HeaderSizePosition = writer.BaseStream.Position;
            writer.AddOffset("dataOffset");
            writer.FixPadding(0x10);

            // Camera Data.
            uint CameraPosition = (uint)writer.BaseStream.Position;
            writer.Write(UnknownUInt32_1);
            writer.Write(UnknownUInt32_2);
            writer.Write(UnknownVector3_1);
            writer.Write(UnknownVector3_2);
            writer.Write(UnknownFloat_1);
            writer.Write(UnknownFloat_2);
            writer.Write(UnknownFloat_3);

            // Chunk Data.
            writer.FillOffset("dataOffset", true);
            writer.Write((uint)Type);
            writer.AddOffset($"CameraData", 0);
            writer.Write(CameraPosition - 0x20);

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
