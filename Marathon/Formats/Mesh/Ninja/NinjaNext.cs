namespace Marathon.Formats.Mesh.Ninja
{
    public class NinjaNext : FileBase
    {
        public NinjaNext() { }

        public NinjaNext(string file, bool serialise = false)
        {
            Load(file);

            if (serialise)
                JsonSerialise(Data);
        }

        public override string Signature { get; } = "NXIF";

        public class FormatData
        {
            public NinjaTextureList? TextureList { get; set; }

            public NinjaEffectList? EffectList { get; set; }

            public NinjaNodeNameList? NodeNameList { get; set; }

            public NinjaObject? Object { get; set; }

            public NinjaLight? Light { get; set; }

            public NinjaCamera? Camera { get; set; }

            public NinjaMotion? Motion { get; set; }
        }

        public FormatData Data { get; set; } = new();

        public override void Load(Stream stream)
        {
            BinaryReaderEx reader = new(stream);

            // Read NXIF chunk.
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

            // Read data chunks.
            for (int i = 0; i < dataChunkCount; i++)
            {
                // Read the chunk's ID and size.
                string chunkID = new(reader.ReadChars(4));
                chunkSize = reader.ReadUInt32();

                // Calculate where the next chunk begins so we can jump to it.
                long targetPosition = reader.BaseStream.Position + chunkSize;

                /* Run the approriate read function for the chunk.
                   If this chunk isn't currently handled, throw an exception. */
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

            // If this SegaNN file has both an Object Chunk and a Node Name List then fill in the Nodes with their names.
            if (Data.Object != null && Data.NodeNameList != null)
            {
                if (Data.Object.Nodes.Count == Data.NodeNameList.NinjaNodeNames.Count)
                {
                    for (int i = 0; i < Data.Object.Nodes.Count; i++)
                        Data.Object.Nodes[i].Name = Data.NodeNameList.NinjaNodeNames[i];
                }
            }
        }

        public override void Save(Stream stream)
        {
            BinaryWriterEx writer = new(stream);
            writer.Offset = 0x20;

            // Get amount of data chunks.
            uint dataChunkCount = 0;
            {
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
            }

            // Write NXIF chunk.
            writer.Write(Signature);
            writer.Write(0x18);
            writer.Write(dataChunkCount);
            writer.Write(0x20);
            long DataSizePosition = writer.BaseStream.Position;
            writer.Write("SIZE"); // Temporary value, filled in once we know how long the data is.
            writer.Write("NOF0"); // Temporary value, filled in once we know where the NOF0 chunk is.
            writer.Write("SIZE"); // Temporary value, filled in once we know how long the NOF0 chunk is.
            writer.Write(1);

            // Write the chunks we have loaded.
            Data.TextureList?.Write(writer);
            Data.EffectList?.Write(writer);
            Data.NodeNameList?.Write(writer);
            Data.Object?.Write(writer);
            Data.Light?.Write(writer);
            Data.Camera?.Write(writer);
            Data.Motion?.Write(writer);

            // Calculate and write the position of the NOF0 chunk.
            uint NOF0Offset = (uint)writer.BaseStream.Position;
            writer.BaseStream.Position = DataSizePosition;
            writer.Write(NOF0Offset - 0x20);
            writer.Write(NOF0Offset);
            writer.BaseStream.Position = NOF0Offset;

            // Write NOF0 chunk.
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
            for (int i = 0; i < Offsets.Count; i++)
                writer.Write(Offsets[i] - 0x20);

            // Alignment.
            writer.FixPadding(0x10);

            // Calculate and write the size of the NOF0 chunk.
            long NOF0EndPosition = writer.BaseStream.Position;
            uint NOF0Size = (uint)(NOF0EndPosition - N0F0SizePosition);
            writer.BaseStream.Position = N0F0SizePosition - 4;
            writer.Write(NOF0Size);
            writer.BaseStream.Position = DataSizePosition + 8;
            writer.Write(NOF0Size + 8);
            writer.BaseStream.Position = NOF0EndPosition;

            // Write NFN0 chunk.
            writer.Write("NFN0");
            writer.Write("SIZE"); // Temporary value, filled in once we know how long the data is.
            long NFN0SizePosition = writer.BaseStream.Position;
            writer.FixPadding(0x10);

            // Get the current file name and write it, aligned to 0x10 bytes.
            writer.WriteNullTerminatedString(Path.GetFileName((stream as FileStream).Name));
            writer.FixPadding(0x10);

            // Calculate and write the size of the NFN0 chunk.
            long NFN0EndPosition = writer.BaseStream.Position;
            uint NFN0Size = (uint)(NFN0EndPosition - NFN0SizePosition);
            writer.BaseStream.Position = NFN0SizePosition - 4;
            writer.Write(NFN0Size);
            writer.BaseStream.Position = NFN0EndPosition;

            // Write NEND chunk.
            writer.Write("NEND");
            writer.Write(8);
            writer.FixPadding(0x10);
        }
    }
}
