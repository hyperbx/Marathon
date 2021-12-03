namespace Marathon.Formats.Mesh.Ninja
{
    public class NinjaNext : FileBase
    {
        public override string Signature { get; } = "NXIF";

        public class FormatData
        {
            public NinjaTextureList TextureList { get; set; }

            public NinjaEffectList EffectList { get; set; }

            public NinjaNodeNameList NodeNameList { get; set; }

            public NinjaObject Object { get; set; }

            public NinjaLight Light { get; set; }

            public NinjaCamera Camera { get; set; }

            public NinjaMotion Motion { get; set; }
        }

        public FormatData Data { get; set; } = new();

        public override void Load(Stream stream)
        {
            BinaryReaderEx reader = new(stream);

            // NXIF Chunk.
            reader.ReadSignature(4, Signature);
            uint chunkSize = reader.ReadUInt32();
            uint dataChunkCount = reader.ReadUInt32();
            uint dataOffset = reader.ReadUInt32();
            uint dataSize = reader.ReadUInt32();
            uint NOF0Offset = reader.ReadUInt32();
            uint NOF0Size = reader.ReadUInt32();
            uint version = reader.ReadUInt32();

            // Set the reader's offset to the value set in the NXIF.
            reader.Offset = dataOffset;

            // Data Chunks
            for (int i = 0; i < dataChunkCount; i++)
            {
                // Read the chunk's ID and size.
                string chunkID = new(reader.ReadChars(0x04));
                chunkSize = reader.ReadUInt32();

                // Calculate where the next chunk begins so we can jump to it.
                long targetPosition = reader.BaseStream.Position + chunkSize;

                // Run the approriate read function for the chunk.
                // If this chunk isn't currently handled, throw an exception.
                switch (chunkID)
                {
                    case "NXTL":
                        Data.TextureList = new();
                        Data.TextureList.Read(reader);
                        break;
                    case "NXEF":
                        Data.EffectList = new();
                        Data.EffectList.Read(reader);
                        break;
                    case "NXNN":
                        Data.NodeNameList = new();
                        Data.NodeNameList.Read(reader);
                        break;
                    case "NXOB":
                        Data.Object = new();
                        Data.Object.Read(reader);
                        break;
                    case "NXLI":
                        Data.Light = new();
                        Data.Light.Read(reader);
                        break;
                    case "NXCA":
                        Data.Camera = new();
                        Data.Camera.Read(reader);
                        break;
                    case "NXMA":
                    case "NXMC":
                    case "NXML":
                    case "NXMM":
                    case "NXMO":
                        Data.Motion = new();
                        Data.Motion.ChunkID = chunkID;
                        Data.Motion.Read(reader);
                        break;
                    default:
                        throw new NotImplementedException();
                }

                // Jump to the position of the next chunk to make sure the reader's in the right place.
                reader.JumpTo(targetPosition);
            }
        }

        public override void Save(Stream stream)
        {
            BinaryWriterEx writer = new(stream);
            writer.Offset = 0x20;

            // Get amount of Data Chunks.
            uint dataChunkCount = 0;
            if (Data.TextureList != null)
                dataChunkCount++;
            if (Data.EffectList != null)
                dataChunkCount++;
            if (Data.NodeNameList != null)
                dataChunkCount++;
            if (Data.Object != null)
                dataChunkCount++;
            if (Data.Light != null)
                dataChunkCount++;
            if (Data.Camera != null)
                dataChunkCount++;
            if (Data.Motion != null)
                dataChunkCount++;

            // NXIF Chunk.
            writer.Write(Signature);
            writer.Write(0x18);
            writer.Write(dataChunkCount);
            writer.Write(0x20);
            long DataSizePosition = writer.BaseStream.Position;
            writer.Write("SIZE"); // Temporary value, filled in once we know how long the data is.
            writer.Write("NOF0"); // Temporary value, filled in once we know where the NOF0 chunk is.
            writer.Write("SIZE"); // Temporary value, filled in once we know how long the NOF0 chunk is.
            writer.Write(0x01);

            // Write the chunks we have loaded.
            if (Data.TextureList != null)
                Data.TextureList.Write(writer);
            if (Data.EffectList != null)
                Data.EffectList.Write(writer);
            if (Data.NodeNameList != null)
                Data.NodeNameList.Write(writer);
            if (Data.Object != null)
                Data.Object.Write(writer);
            if (Data.Light != null)
                Data.Light.Write(writer);
            if (Data.Camera != null)
                Data.Camera.Write(writer);
            if (Data.Motion != null)
                Data.Motion.Write(writer);

            // Calculate and write the position of the NOF0 chunk.
            uint NOF0Offset = (uint)writer.BaseStream.Position;
            writer.BaseStream.Position = DataSizePosition;
            writer.Write(NOF0Offset - 0x20);
            writer.Write(NOF0Offset);
            writer.BaseStream.Position = NOF0Offset;

            // NOF0 Chunk
            writer.Write("NOF0");
            writer.Write("SIZE"); // Temporary value, filled in once we know how long the data is.
            long N0F0SizePosition = writer.BaseStream.Position;

            // Get the stored offsets from the Writer and sort them in order.
            List<uint> Offsets = writer.GetOffsets();
            Offsets.Sort();

            // Write the count of Offsets and finish the header.
            writer.Write(Offsets.Count);
            writer.FixPadding(0x10);

            // Write each offset, taking the writer's offset into account.
            for(int i = 0; i < Offsets.Count; i++)
                writer.Write(Offsets[i] - 0x20);

            // Alignment.
            writer.FixPadding(0x10);

            // Calculate and write the size of the NOF0 chunk.
            long NOF0EndPosition = writer.BaseStream.Position;
            uint NOF0Size = (uint)(NOF0EndPosition - N0F0SizePosition);
            writer.BaseStream.Position = N0F0SizePosition - 0x04;
            writer.Write(NOF0Size);
            writer.BaseStream.Position = DataSizePosition + 0x08;
            writer.Write(NOF0Size + 0x08);
            writer.BaseStream.Position = NOF0EndPosition;

            // NFN0 Chunk
            writer.Write("NFN0");
            writer.Write("SIZE"); // Temporary value, filled in once we know how long the data is.
            long NFN0SizePosition = writer.BaseStream.Position;
            writer.FixPadding(0x10);

            // Get the stream's file name and write it, aligned to 0x10 bytes.
            writer.WriteNullTerminatedString(Path.GetFileName((stream as FileStream).Name));
            writer.FixPadding(0x10);

            // Calculate and write the size of the NFN0 chunk.
            long NFN0EndPosition = writer.BaseStream.Position;
            uint NFN0Size = (uint)(NFN0EndPosition - NFN0SizePosition);
            writer.BaseStream.Position = NFN0SizePosition - 0x04;
            writer.Write(NFN0Size);
            writer.BaseStream.Position = NFN0EndPosition;

            // NEND Chunk
            writer.Write("NEND");
            writer.Write(0x08);
            writer.FixPadding(0x10);
        }
    }
}
