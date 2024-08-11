namespace Marathon.Formats.Event
{
    /// <summary>
    /// File base for the *.tev format.
    /// <para>Used in SONIC THE HEDGEHOG for defining timed events for objects.</para>
    /// </summary>
    public class TimeEvent : FileBase
    {
        public TimeEvent() { }

        public TimeEvent(string file, bool serialise = false)
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

        public override string Signature { get; } = ".TEV";

        public override string Extension { get; } = ".tev";

        public class FormatData
        {
            public string Animation { get; set; }

            public List<AnimationTimer> Events { get; set; } = [];

            public override string ToString() => Animation;
        }

        public FormatData Data { get; set; } = new();

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            reader.ReadSignature(4, Signature);

            reader.JumpAhead(4); // Always null in official files.
            uint XNMOffset = reader.ReadUInt32();

            long position = reader.BaseStream.Position; // Save position so we can jump back.

            Data.Animation = reader.ReadNullTerminatedString(false, XNMOffset, true); // Read XNM associated with this time event file.
            reader.JumpTo(position);                                                  // Return to previous position.

            uint EntryCount       = reader.ReadUInt32();
            uint EventTableOffset = reader.ReadUInt32(); // Offset to the table of entries in this time event file.

            // Jump to the Event Table's Offset for safety's sake, should always be ox20 really.
            reader.JumpTo(EventTableOffset, true);

            // Loop through entries
            for (int i = 0; i < EntryCount; i++)
            {
                AnimationTimer entry = new();

                reader.JumpAhead(4); // Always null in official files.

                uint TargetNodeOffset   = reader.ReadUInt32();
                uint ParticleBankOffset = reader.ReadUInt32();
                uint ParticleOffset     = reader.ReadUInt32();

                entry.StartTime = reader.ReadSingle();
                entry.EndTime = reader.ReadSingle();

                reader.JumpAhead(1); // Always null in official files. Padding?

                entry.UnknownBoolean = reader.ReadBoolean();

                reader.JumpAhead(2); // Always null in official files. Padding?

                entry.ParticlePosition  = reader.ReadVector3();
                entry.UnknownVector3  = reader.ReadVector3();

                // Store current position to jump back to for the next entry.
                position = reader.BaseStream.Position;

                reader.JumpTo(TargetNodeOffset, true);
                entry.TargetNode = reader.ReadNullTerminatedString();

                reader.JumpTo(ParticleBankOffset, true);
                entry.ParticleContainer = reader.ReadNullTerminatedString();

                reader.JumpTo(ParticleOffset, true);
                entry.ParticleName = reader.ReadNullTerminatedString();

                // Jump back to the saved position to read the next entry..
                reader.JumpTo(position);

                // Save event entry into the Entries list.
                Data.Events.Add(entry);
            }
        }

        public override void Save(Stream stream)
        {
            BINAWriter writer = new(stream);

            writer.WriteSignature(Signature);
            writer.WriteNulls(4);
            writer.AddString("XNMOffset", Data.Animation);
            writer.Write(Data.Events.Count);
            writer.AddOffset("EntriesOffset");

            // Write the event entries.
            writer.FillOffset("EntriesOffset", true);
            for (int i = 0; i < Data.Events.Count; i++)
            {
                writer.WriteNulls(4);
                writer.AddString($"TargetNode{i}", Data.Events[i].TargetNode);
                writer.AddString($"ParticleBank{i}", Data.Events[i].ParticleContainer);
                writer.AddString($"Particle{i}", Data.Events[i].ParticleName);
                writer.Write(Data.Events[i].StartTime);
                writer.Write(Data.Events[i].EndTime);
                writer.WriteNulls(1);
                writer.Write(Data.Events[i].UnknownBoolean);
                writer.WriteNulls(2);
                writer.Write(Data.Events[i].ParticlePosition);
                writer.Write(Data.Events[i].UnknownVector3);
            }

            // Write the footer.
            writer.FinishWrite();
        }
    }

    public class AnimationTimer
    {
        public string TargetNode { get; set; }

        public string ParticleContainer { get; set; }

        public string ParticleName { get; set; }

        public float StartTime { get; set; }

        public float EndTime { get; set; }

        public bool UnknownBoolean { get; set; }

        public Vector3 ParticlePosition { get; set; }

        public Vector3 UnknownVector3 { get; set; }

        public override string ToString() => ParticleName;
    }
}