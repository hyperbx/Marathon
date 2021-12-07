namespace Marathon.Formats.Mesh.Ninja
{
    /// <summary>
    /// Structure of the main Ninja Light.
    /// TODO (Knuxfan24): This is all wild guessing which I'm 100% sure is all sorts of bollocks.
    /// </summary>
    public class NinjaLight
    {
        public LightType Type { get; set; }

        public uint UnknownUInt32_1 { get; set; }

        public Vector3 UnknownVector3_1 { get; set; }

        public Vector3 UnknownVector3_2 { get; set; }

        public Vector3 UnknownVector3_3 { get; set; }

        public float UnknownFloat_1 { get; set; }

        /// <summary>
        /// Reads the Ninja Light from a file.
        /// </summary>
        /// <param name="reader">The binary reader for this SegaNN file.</param>
        public void Read(BinaryReaderEx reader)
        {
            // Read the offset to the actual Ninja Light.
            uint dataOffset = reader.ReadUInt32();

            // Jump to the actual Ninja Light.
            reader.JumpTo(dataOffset, true);

            // Read the Type of this Light and the offset to the Light data.
            Type = (LightType)reader.ReadUInt32();
            uint LightOffset = reader.ReadUInt32();

            // Jump to and read the data for this Light.
            reader.JumpTo(LightOffset, true);
            UnknownUInt32_1 = reader.ReadUInt32();
            UnknownVector3_1 = reader.ReadVector3();
            UnknownVector3_2 = reader.ReadVector3();
            UnknownVector3_3 = reader.ReadVector3();
            UnknownFloat_1 = reader.ReadSingle();
        }

        /// <summary>
        /// Write the Ninja Light to a file.
        /// </summary>
        /// <param name="writer">The binary writer for this SegaNN file.</param>
        public void Write(BinaryWriterEx writer)
        {
            // Write NXLI header.
            writer.Write("NXLI");
            writer.Write("SIZE"); // Temporary entry, is filled in later once we know this chunk's size.
            long HeaderSizePosition = writer.BaseStream.Position;
            writer.AddOffset("dataOffset");
            writer.FixPadding(0x10);

            // Write light data.
            uint LightPosition = (uint)writer.BaseStream.Position;
            writer.Write(UnknownUInt32_1);
            writer.Write(UnknownVector3_1);
            writer.Write(UnknownVector3_2);
            writer.Write(UnknownVector3_3);
            writer.Write(UnknownFloat_1);

            // Write chunk data.
            writer.FillOffset("dataOffset", true);
            writer.Write((uint)Type);
            writer.AddOffset($"LightData", 0);
            writer.Write(LightPosition - 0x20);

            // Alignment.
            writer.FixPadding(0x10);

            // Write chunk size.
            long ChunkEndPosition = writer.BaseStream.Position;
            uint ChunkSize = (uint)(ChunkEndPosition - HeaderSizePosition);
            writer.BaseStream.Position = HeaderSizePosition - 4;
            writer.Write(ChunkSize);
            writer.BaseStream.Position = ChunkEndPosition;
        }
    }
}
