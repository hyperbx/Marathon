using System.IO;
using System.Numerics;
using System.Collections.Generic;
using Marathon.IO;

namespace Marathon.Formats.Event
{
    public class TimedEvent
    {
        public string TargetNode { get; set; }

        public string ParticleBank { get; set; }

        public string Particle { get; set; }

        public float UnknownSingle_1 { get; set; }

        public float UnknownSingle_2 { get; set; }

        public bool UnknownBoolean_1 { get; set; }

        public Vector3 ParticlePosition { get; set; }

        public Vector3 UnknownVector3_1 { get; set; }

        public override string ToString() => Particle;
    }

    public class TimeEvent : FileBase
    {
        public TimeEvent() { }

        public TimeEvent(string file)
        {
            switch (Path.GetExtension(file))
            {
                case ".json":
                    Data = JsonDeserialise<FormatData>(file);
                    break;

                default:
                    Load(file);
                    break;
            }
        }

        public class FormatData
        {
            public string Animation;

            public List<TimedEvent> Events = new();

            public override string ToString() => Animation;
        }

        public FormatData Data = new();

        public const string Signature = ".TEV",
                            Extension = ".tev";

        public override void Load(Stream fileStream)
        {
            BINAReader reader = new(fileStream);

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
                TimedEvent entry = new();

                reader.JumpAhead(4); // Always null in official files.

                uint TargetNodeOffset   = reader.ReadUInt32();
                uint ParticleBankOffset = reader.ReadUInt32();
                uint ParticleOffset     = reader.ReadUInt32();
                entry.UnknownSingle_1   = reader.ReadSingle();
                entry.UnknownSingle_2   = reader.ReadSingle();

                reader.JumpAhead(1); // Always null in official files. Padding?

                entry.UnknownBoolean_1 = reader.ReadBoolean();

                reader.JumpAhead(2); // Always null in official files. Padding?

                entry.ParticlePosition  = reader.ReadVector3();
                entry.UnknownVector3_1  = reader.ReadVector3();

                // Store current position to jump back to for the next entry.
                position = reader.BaseStream.Position;

                reader.JumpTo(TargetNodeOffset, true);
                entry.TargetNode = reader.ReadNullTerminatedString();

                reader.JumpTo(ParticleBankOffset, true);
                entry.ParticleBank = reader.ReadNullTerminatedString();

                reader.JumpTo(ParticleOffset, true);
                entry.Particle = reader.ReadNullTerminatedString();

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
            writer.FillInOffset("EntriesOffset", true);
            for (int i = 0; i < Data.Events.Count; i++)
            {
                writer.WriteNulls(4);
                writer.AddString($"TargetNode{i}", Data.Events[i].TargetNode);
                writer.AddString($"ParticleBank{i}", Data.Events[i].ParticleBank);
                writer.AddString($"Particle{i}", Data.Events[i].Particle);
                writer.Write(Data.Events[i].UnknownSingle_1);
                writer.Write(Data.Events[i].UnknownSingle_2);
                writer.WriteNulls(1);
                writer.Write(Data.Events[i].UnknownBoolean_1);
                writer.WriteNulls(2);
                writer.Write(Data.Events[i].ParticlePosition);
                writer.Write(Data.Events[i].UnknownVector3_1);
            }

            // Write the footer.
            writer.FinishWrite();
        }
    }
}