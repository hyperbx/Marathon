﻿namespace Marathon.Formats.Event
{
    /// <summary>
    /// File base for the *.epb format.
    /// <para>Used in SONIC THE HEDGEHOG for cutscene properties.</para>
    /// </summary>
    public class EventPlaybook : FileBase
    {
        public EventPlaybook() { }

        public EventPlaybook(string file, bool serialise = false)
        {
            switch (Path.GetExtension(file))
            {
                case ".json":
                {
                    Events = JsonDeserialise<List<Event>>(file);

                    // Save extension-less JSON (exploiting .NET weirdness, because it doesn't omit all extensions).
                    if (serialise)
                        Save(Path.GetFileNameWithoutExtension(file));

                    break;
                }

                default:
                {
                    Load(file);

                    if (serialise)
                        JsonSerialise(Events);

                    break;
                }
            }
        }

        public override string Signature { get; }= ".EPB";

        public override string Extension { get; } = ".epb";

        public List<Event> Events { get; set; } = [];

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            reader.ReadSignature(4, Signature);

            uint UnknownUInt32_1  = reader.ReadUInt32(); // TODO: Figure out what this controls, as nulling it out broke all in game scenes.
            uint EventCount       = reader.ReadUInt32(); // Number of Events in the Playbook.
            uint EventTableOffset = reader.ReadUInt32(); // Offset to this table of Events in the Playbook.

            // Jump to the Event Table's Offset for safety's sake, should always be 0x30 really.
            reader.JumpTo(EventTableOffset, true);

            // Loop through based on EventCount.
            for (int i = 0; i < EventCount; i++)
            {
                Event @event = new();
                uint eventNameOffset    = reader.ReadUInt32();  // Offset to this event's name.
                uint eventFolderOffset  = reader.ReadUInt32();  // Offset to this event's folder.
                @event.EventLength      = reader.ReadUInt32();  // The length of this event in frames.
                @event.Position         = reader.ReadVector3(); // The position this event's objects are placed relative to.
                @event.Rotation         = reader.ReadVector3(); // The rotation of this event's objects.
                uint terrainOffset      = reader.ReadUInt32();  // Offset to this event's terrain.
                uint sceneLuaOffset     = reader.ReadUInt32();  // Offset to this event's scene lua.
                uint sceneBankOffset    = reader.ReadUInt32();  // Offset to this event's scene bank.
                uint particleListOffset = reader.ReadUInt32();  // Offset to this event's particle list.
                uint subtitleMSTOffset  = reader.ReadUInt32();  // Offset to this event's MST file.

                // Store current position to jump back to for the next entry.
                long position = reader.BaseStream.Position;

                // Read all the string values, needs to check if the offset isn't 0 as it reads the .EPB signature otherwise.
                if (eventNameOffset != 0)
                    @event.Name = reader.ReadNullTerminatedString(false, eventNameOffset, true);

                if (eventFolderOffset != 0)
                    @event.Folder = reader.ReadNullTerminatedString(false, eventFolderOffset, true);

                if (terrainOffset != 0)
                    @event.Terrain = reader.ReadNullTerminatedString(false, terrainOffset, true);

                if (sceneLuaOffset != 0)
                    @event.SceneLua = reader.ReadNullTerminatedString(false, sceneLuaOffset, true);

                if (sceneBankOffset != 0)
                    @event.SoundBank = reader.ReadNullTerminatedString(false, sceneBankOffset, true);

                if (particleListOffset != 0)
                    @event.ParticleContainer = reader.ReadNullTerminatedString(false, particleListOffset, true);

                if (subtitleMSTOffset != 0)
                    @event.SubtitleMessageTable = reader.ReadNullTerminatedString(false, subtitleMSTOffset, true);

                // Jump back to the saved position to read the next event.
                reader.JumpTo(position);

                // Save object entry into the Entries list.
                Events.Add(@event);
            }
        }

        public override void Save(Stream stream)
        {
            BINAWriter writer = new(stream);

            writer.WriteSignature(Extension.ToUpper());
            writer.Write(537265920u); // TODO: Unhardcode this once we figure out what it does.
            writer.Write(Events.Count);
            writer.AddOffset("EventTableOffset");

            // Write the events.
            writer.FillOffset("EventTableOffset", true);
            for (int i = 0; i < Events.Count; i++)
            {
                writer.AddString($"Event{i}Name", Events[i].Name);
                writer.AddString($"Event{i}Folder", Events[i].Folder);
                writer.Write(Events[i].EventLength);
                writer.Write(Events[i].Position);
                writer.Write(Events[i].Rotation);
                writer.AddString($"Event{i}Terrain", Events[i].Terrain);
                writer.AddString($"Event{i}SceneLua", Events[i].SceneLua);
                writer.AddString($"Event{i}SceneBank", Events[i].SoundBank);
                writer.AddString($"Event{i}ParticleList", Events[i].ParticleContainer);
                writer.AddString($"Event{i}SubtitleMST", Events[i].SubtitleMessageTable);
            }

            // Write the footer.
            writer.FinishWrite();
        }
    }

    public class Event
    {
        public string Name { get; set; }

        public string Folder { get; set; }

        public string Terrain { get; set; }

        public string SceneLua { get; set; }

        public string SoundBank { get; set; }

        public string ParticleContainer { get; set; }

        public string SubtitleMessageTable { get; set; }

        public uint EventLength { get; set; }

        public Vector3 Position { get; set; }

        public Vector3 Rotation { get; set; }

        public override string ToString() => Name;
    }
}