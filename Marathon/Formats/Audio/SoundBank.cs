﻿namespace Marathon.Formats.Audio
{
    /// <summary>
    /// File base for the *.sbk format.
    /// <para>Used in SONIC THE HEDGEHOG for defining properties for <a href="https://www.criware.com/">CriWare</a> sound effects in <see cref="CueSheetBinary"/> files.</para>
    /// </summary>
    public class SoundBank : FileBase
    {
        public SoundBank() { }

        public SoundBank(string file, bool serialise = false)
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

        public override string Signature { get; } = "SBNK";

        public override string Extension { get; } = ".sbk";

        public class FormatData
        {
            public string Name { get; set; }

            public List<Cue> Cues = [];

            public override string ToString() => Name;
        }

        public FormatData Data { get; set; } = new();

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            reader.ReadSignature(4, Signature);

            uint UnknownUInt32_1   = reader.ReadUInt32(); // These four bytes seems to always be { 20, 06, 07, 00 } in official files.
            uint bankNameOffset    = reader.ReadUInt32(); // Offset to the Sound Bank name (seemingly always { 00, 00, 00, 18 } in official files).
            uint cueNameOffset     = reader.ReadUInt32(); // Offset of the first entry in the Sound Bank (seemingly always { 00, 00, 00, 64 } in official files).
            uint cueIndiciesOffset = reader.ReadUInt32(); // Offset to the number list for non-stream indices (set to { 00, 00, 00, 00 } if the file doesn't have any).
            uint streamOffset      = reader.ReadUInt32(); // Offset to the table for XMA names (set to { 00, 00, 00, 00 } if the file doesn't have any).

            Data.Name           = reader.ReadNullPaddedString(0x40); // Sound Bank's name.
            uint cueCount       = reader.ReadUInt32();               // Total Number of Cues in this Sound Bank.
            uint csbCueCount    = reader.ReadUInt32();               // Amount of Cues in this Sound Bank which pull their data from a corresponding CSB file.
            uint streamCueCount = reader.ReadUInt32();               // Amount of Cues in this Sound Bank which use XMA files.

            int streams = 0; // Keep track of which stream we're on so we know where to jump to in the XMA string table.

            for (int i = 0; i < cueCount; i++)
            {
                Cue cue = new() { Name = reader.ReadNullPaddedString(0x20) };

                uint cueType  = reader.ReadUInt32();
                uint cueIndex = reader.ReadUInt32();

                cue.Category      = reader.ReadUInt32();
                cue.UnknownSingle = reader.ReadSingle();
                cue.Radius        = reader.ReadSingle();

                if (cueType == 1)
                {
                    // Store current position for later so we can jump back.
                    long pos = reader.BaseStream.Position;

                    reader.JumpTo(streamOffset, true);
                    reader.JumpAhead(4 * streams); // Jump ahead to the right offset for our Cue's XMA.

                    cue.Stream = reader.ReadNullTerminatedString(false, reader.ReadUInt32(), true); // Read the XMA's name for this Cue.

                    reader.JumpTo(pos); // Jump back to where we were.

                    streams++;
                }

                // Save Cue to list.
                Data.Cues.Add(cue);
            }
        }

        public override void Save(Stream stream)
        {
            BINAWriter writer = new(stream);

            // Determine amount of Cues that use a CSB and amount that use an XMA.
            int csbCueCount    = 0;
            int streamCueCount = 0;

            for (int i = 0; i < Data.Cues.Count; i++)
            {
                if (string.IsNullOrEmpty(Data.Cues[i].Stream))
                    csbCueCount++;
                else
                    streamCueCount++;
            }

            writer.WriteSignature(Signature);

            writer.Write(537265920); // Hardcoded as all official files seem to have this number in this position.

            writer.AddOffset("banksOffset");
            writer.AddOffset("cueNamesOffset");
            writer.AddOffset("cueIndicesOffset");
            writer.AddOffset("streamsOffset");

            writer.FillOffset("banksOffset", true);

            writer.WriteNullPaddedString(Data.Name, 0x40);
            writer.Write(Data.Cues.Count);
            writer.Write(csbCueCount);
            writer.Write(streamCueCount);

            // Cue Information
            writer.FillOffset("cueNamesOffset", true);

            // Keep track of the entry types and IDs.
            int csbCueID    = 0;
            int streamCueID = 0;

            for (int i = 0; i < Data.Cues.Count; i++)
            {
                writer.WriteNullPaddedString(Data.Cues[i].Name, 0x20);

                // Write a CSB-based entry.
                if (string.IsNullOrEmpty(Data.Cues[i].Stream))
                {
                    writer.Write(0);
                    writer.Write(csbCueID);
                    csbCueID++;
                }

                // Write an XMA-based entry.
                else
                {
                    writer.Write(1);
                    writer.Write(streamCueID);
                    streamCueID++;
                }

                writer.Write(Data.Cues[i].Category);
                writer.Write(Data.Cues[i].UnknownSingle);
                writer.Write(Data.Cues[i].Radius);
            }

            // CSB Cue ID List (if any are present).
            if (csbCueCount != 0)
            {
                writer.FillOffset("cueIndicesOffset", true);

                for (int i = 0; i < csbCueCount; i++)
                    writer.Write(i);
            }

            // Stream Names (if any are present).
            if (streamCueCount != 0)
            {
                writer.FillOffset("streamsOffset", true);

                for (int i = 0; i < Data.Cues.Count; i++)
                {
                    if (Data.Cues[i].Stream != null)
                        writer.AddString($"streamOffset{i}", $"{Data.Cues[i].Stream}");
                }
            }

            // Write the footer.
            writer.FinishWrite();
        }
    }

    public class Cue
    {
        /// <summary>
        /// Name of this Cue in the Sound Bank.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Uncertain what exactly this affects.
        /// </summary>
        public uint Category { get; set; }

        /// <summary>
        /// Unknown - possibly a flag, possibly unused?
        /// </summary>
        public float UnknownSingle { get; set; }

        /// <summary>
        /// The distance this sound can be heard from.
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// XMA this Cue uses, if null, assume it uses a CSB instead.
        /// </summary>
        public string Stream { get; set; }

        public Cue() { }

        public Cue(string in_name, uint in_category, float in_unknownSingle, float in_radius, string in_stream)
        {
            Name = in_name;
            Category = in_category;
            UnknownSingle = in_unknownSingle;
            Radius = in_radius;
            Stream = in_stream;
        }

        public override string ToString() => Name;
    }
}
