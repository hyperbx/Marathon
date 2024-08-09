namespace Marathon.Formats.Save
{
    public class SonicNextSaveData : FileBase
    {
        public SonicNextSaveData() { }

        public SonicNextSaveData(string file, bool serialise = false)
        {
            switch (Path.GetExtension(file))
            {
                case ".json":
                {
                    Data = JsonDeserialise<FormatData>(file);

                    // Save extension-less JSON (exploiting .NET weirdness, because it doesn't omit all extensions).
                    if (serialise)
                        Save(Path.GetFileNameWithoutExtension(file));

                    break;
                }

                default:
                {
                    Load(file);

                    if (serialise)
                        JsonSerialise(Data);

                    break;
                }
            }
        }

        public override string Extension { get; } = ".bin";

        public override WriteMode WriteMode => WriteMode.Fixed;

        internal static int EpisodeCount { get; } = 32;

        internal static int GlobalFlagCount { get; } = 0x27FF;

        internal static int TrialCount { get; } = 512;

        public class FormatData
        {
            public SonicNextEpisode[] Episodes { get; set; } = new SonicNextEpisode[EpisodeCount];

            public Dictionary<SonicNextFlags, int> GlobalFlags { get; set; } = [];

            public SonicNextTrial[] Trials { get; set; } = new SonicNextTrial[TrialCount];

            public SonicNextOptions Options { get; set; } = new();
        }

        public FormatData Data { get; set; } = new();

        public override void Load(Stream stream)
        {
            BinaryReaderEx reader = new(stream, true);

            // Expected zero.
            reader.ReadSignature(0);

            // Read each episode's lives.
            for (int i = 0; i < EpisodeCount; i++)
            {
                Data.Episodes[i] = new();
                Data.Episodes[i].Lives = reader.ReadInt32();
            }

            // Read each episode's rings.
            for (int i = 0; i < EpisodeCount; i++)
                Data.Episodes[i].Rings = reader.ReadInt32();

            // Read global flags.
            for (int i = 0; i <= GlobalFlagCount; i++)
                Data.GlobalFlags.Add((SonicNextFlags)i, reader.ReadInt32());

            for (int i = 0; i < EpisodeCount; i++)
            {
                Data.Episodes[i].Lua = reader.ReadNullPaddedString(0x100);

                // TODO: unknown data... contains flags!
                reader.JumpAhead(0x80);

                Data.Episodes[i].Objective = reader.ReadNullPaddedString(0x100);
                Data.Episodes[i].Area      = reader.ReadNullPaddedString(0x100);
                Data.Episodes[i].Terrain   = reader.ReadNullPaddedString(0x100);
                Data.Episodes[i].SET       = reader.ReadNullPaddedString(0x100);
                Data.Episodes[i].PATH      = reader.ReadNullPaddedString(0x100);

                // TODO: unknown data...
                reader.JumpAhead(0x500);

                Data.Episodes[i].MST = reader.ReadNullPaddedString(0x100);

                // TODO: unknown data... contains flags!
                reader.JumpAhead(0x24C);

                Data.Episodes[i].Progress = reader.ReadInt32();
                Data.Episodes[i].Year     = reader.ReadInt16();
                Data.Episodes[i].Month    = reader.ReadSByte();
                Data.Episodes[i].Day      = reader.ReadSByte();
                Data.Episodes[i].Hour     = reader.ReadSByte();
                Data.Episodes[i].Minute   = reader.ReadSByte();

                Data.Episodes[i].Location = reader.ReadNullPaddedString(0x42);
            }

            for (int i = 0; i < TrialCount; i++)
            {
                Data.Trials[i] = new();
                Data.Trials[i].ID    = reader.ReadInt32();
                Data.Trials[i].Rank  = (SonicNextRank)reader.ReadInt32();
                Data.Trials[i].Time  = reader.ReadInt32();
                Data.Trials[i].Score = reader.ReadInt32();
                Data.Trials[i].Rings = reader.ReadInt32();
            }

            Data.Options.Subtitles = reader.ReadBoolean(4);
            Data.Options.Music     = reader.ReadSingle();
            Data.Options.Effects   = reader.ReadSingle();
        }

        public override void Save(Stream stream)
        {
            BinaryWriterEx writer = new(stream, true);

            // 32-bit zero signature.
            writer.WriteNulls(4);

            // Write each episode's lives.
            for (int i = 0; i < EpisodeCount; i++)
                writer.Write(Data.Episodes[i].Lives);

            // Write each episode's rings.
            for (int i = 0; i < EpisodeCount; i++)
                writer.Write(Data.Episodes[i].Rings);

            // Write global flags.
            for (int i = 0; i <= GlobalFlagCount; i++)
                writer.Write(Data.GlobalFlags[(SonicNextFlags)i]);

            for (int i = 0; i < EpisodeCount; i++)
            {
                writer.WriteNullPaddedString(Data.Episodes[i].Lua, 0x100);

                // TODO: unknown data... contains flags!
                writer.BaseStream.Position += 0x80;

                writer.WriteNullPaddedString(Data.Episodes[i].Objective, 0x100);
                writer.WriteNullPaddedString(Data.Episodes[i].Area, 0x100);
                writer.WriteNullPaddedString(Data.Episodes[i].Terrain, 0x100);
                writer.WriteNullPaddedString(Data.Episodes[i].SET, 0x100);
                writer.WriteNullPaddedString(Data.Episodes[i].PATH, 0x100);

                // TODO: unknown data...
                writer.BaseStream.Position += 0x500;

                writer.WriteNullPaddedString(Data.Episodes[i].MST, 0x100);

                // TODO: unknown data... contains flags!
                writer.BaseStream.Position += 0x24C;

                writer.Write(Data.Episodes[i].Progress);
                writer.Write(Data.Episodes[i].Year);
                writer.Write(Data.Episodes[i].Month);
                writer.Write(Data.Episodes[i].Day);
                writer.Write(Data.Episodes[i].Hour);
                writer.Write(Data.Episodes[i].Minute);

                writer.WriteNullPaddedString(Data.Episodes[i].Location, 0x42);
            }

            for (int i = 0; i < TrialCount; i++)
            {
                writer.Write(Data.Trials[i].ID);
                writer.Write((int)Data.Trials[i].Rank);
                writer.Write(Data.Trials[i].Time);
                writer.Write(Data.Trials[i].Score);
                writer.Write(Data.Trials[i].Rings);
            }

            writer.WriteBoolean32(Data.Options.Subtitles);
            writer.Write(Data.Options.Music);
            writer.Write(Data.Options.Effects);
        }
    }
}
