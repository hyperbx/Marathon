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

            reader.Offset = dataOffset;

            // Data Chunks
            for (int i = 0; i < dataChunkCount; i++)
            {
                string chunkID = new(reader.ReadChars(0x04));
                chunkSize = reader.ReadUInt32();
                long targetPosition = reader.BaseStream.Position + chunkSize;

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
                    default:
                        throw new NotImplementedException();
                }

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

            // NXIF Chunk.
            writer.Write(Signature);
            writer.Write(0x18);
            writer.Write(dataChunkCount);
            writer.Write(0x20);
            long DataSizePosition = writer.BaseStream.Position;
            writer.Write("TEMP");
            writer.Write("TEMP");
            writer.Write("TEMP");
            writer.Write(0x01);

            if (Data.TextureList != null)
                Data.TextureList.Write(writer);
            if (Data.EffectList != null)
                Data.EffectList.Write(writer);
            if (Data.NodeNameList != null)
                Data.NodeNameList.Write(writer);
            if (Data.Object != null)
                Data.Object.Write(writer);

            uint NOF0Offset = (uint)writer.BaseStream.Position;
            writer.BaseStream.Position = DataSizePosition;
            writer.Write(NOF0Offset - 0x20);
            writer.Write(NOF0Offset);
            writer.BaseStream.Position = NOF0Offset;

            // NOF0 Chunk
            writer.Write("NOF0");
            writer.Write("TEMP");
            long N0F0SizePosition = writer.BaseStream.Position;
            List<uint> Offsets = writer.GetOffsets();
            Offsets.Sort();
            writer.Write(Offsets.Count);
            writer.FixPadding(0x10);

            for(int i = 0; i < Offsets.Count; i++)
                writer.Write(Offsets[i] - 0x20);

            // Alignment.
            writer.FixPadding(0x10);

            // NOF0 Size.
            long NOF0EndPosition = writer.BaseStream.Position;
            uint NOF0Size = (uint)(NOF0EndPosition - N0F0SizePosition);
            writer.BaseStream.Position = N0F0SizePosition - 0x04;
            writer.Write(NOF0Size);
            writer.BaseStream.Position = DataSizePosition + 0x08;
            writer.Write(NOF0Size + 0x08);
            writer.BaseStream.Position = NOF0EndPosition;

            // NFN0 Chunk
            writer.Write("NFN0");
            writer.Write("TEMP");
            long NFN0SizePosition = writer.BaseStream.Position;
            writer.FixPadding(0x10);
            writer.WriteNullTerminatedString(Path.GetFileName((stream as FileStream).Name));
            writer.FixPadding(0x10);

            // NFN0 Size
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
