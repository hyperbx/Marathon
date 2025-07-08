using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using System.Collections.Generic;
using System.IO;

// Format research attribution: Knuxfan24, Hyper

namespace Marathon.Formats.Audio
{
    /// <summary>
    /// Support for *.sbk files; used for defining properties for <a href="https://www.criware.com/">CRIWARE</a> cues in cue sheet binaries (*.csb files).
    /// </summary>
    public class SoundBank : FileBase
    {
        private const string _signature = "SBNK"; // "Sound BaNK"

        public SoundBank() { }

        public SoundBank(string in_path) : base(in_path) { }

        /// <summary>
        /// The name of this sound bank.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The sound cues in this bank.
        /// </summary>
        public List<Cue> Cues { get; set; } = [];

        public Cue this[string in_name]
        {
            get => Cues.Find((x) => x.Name == in_name);
        }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            reader.CheckSignature(_signature);

            var unkField1 = reader.Read<uint>();         // TODO: unknown, always seems to be 0x20060700.
            var nameOffset = reader.Read<uint>();        // Pointer to the sound bank name.
            var cueTableOffset = reader.Read<uint>();    // Pointer to the first entry in the sound bank.
            var cueIndicesOffset = reader.Read<uint>();  // Pointer to the index list for non-stream indices (null if none).
            var streamTableOffset = reader.Read<uint>(); // Pointer to the pointer list for external stream paths (null if none).

            Name = reader.ReadStringFixedLength(0x40);

            var cueCount = reader.Read<uint>();       // Total number of cues in this sound bank.
            var csbCueCount = reader.Read<uint>();    // Total number of cues in this sound bank which pull their data from a corresponding *.csb file.
            var streamCueCount = reader.Read<uint>(); // Total number of cues in this sound bank which use external streams.

            var currentStream = 0;

            for (int i = 0; i < cueCount; i++)
            {
                var cue = new Cue()
                {
                    Name = reader.ReadStringFixedLength(0x20)
                };

                var isExternalStream = reader.Read<uint>() != 0;
                var cueIndex = reader.Read<uint>();

                cue.Category = reader.Read<uint>();
                cue.UnknownField1 = reader.Read<float>();
                cue.Radius = reader.Read<float>();

                if (isExternalStream)
                {
                    var pos = reader.Position;

                    // Navigate to the correct pointer in the table.
                    reader.JumpTo(BINAHeader.Size + streamTableOffset);
                    reader.JumpAhead(4 * currentStream);

                    var streamNameOffset = reader.ReadUInt32();

                    reader.ReadAtOffset(BINAHeader.Size + streamNameOffset, () => cue.Stream = reader.ReadStringNullTerminated());
                    reader.JumpTo(pos);

                    currentStream++;
                }

                Cues.Add(cue);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream);

            var csbCueCount = 0;
            var streamCueCount = 0;

            for (int i = 0; i < Cues.Count; i++)
            {
                if (string.IsNullOrEmpty(Cues[i].Stream))
                    csbCueCount++;
                else
                    streamCueCount++;
            }
        
            writer.WriteSignature(_signature);
            writer.Write(0x20060700); // TODO: unknown.
            writer.CreateNamedField("NameOffset");
            writer.CreateNamedField("CueTableOffset");
            writer.CreateNamedField("CueIndicesOffset");
            writer.CreateNamedField("StreamTableOffset");
        
            writer.WriteNamedField("NameOffset", (uint)writer.Position - BINAHeader.Size);
        
            writer.WriteStringFixedLength(Name, 0x40);
            writer.Write(Cues.Count);
            writer.Write(csbCueCount);
            writer.Write(streamCueCount);

            writer.WriteNamedField("CueTableOffset", (uint)writer.Position - BINAHeader.Size);

            var csbCueID = 0;
            var streamCueID = 0;
        
            for (int i = 0; i < Cues.Count; i++)
            {
                writer.WriteStringFixedLength(Cues[i].Name, 0x20);

                if (string.IsNullOrEmpty(Cues[i].Stream))
                {
                    // Write CSB entry.
                    writer.Write(0);
                    writer.Write(csbCueID);
                    csbCueID++;
                }
                else
                {
                    // Write external stream entry.
                    writer.Write(1);
                    writer.Write(streamCueID);
                    streamCueID++;
                }
        
                writer.Write(Cues[i].Category);
                writer.Write(Cues[i].UnknownField1);
                writer.Write(Cues[i].Radius);
            }

            if (csbCueCount != 0)
            {
                writer.WriteNamedField("CueIndicesOffset", (uint)writer.Position - BINAHeader.Size);
        
                for (int i = 0; i < csbCueCount; i++)
                    writer.Write(i);
            }

            if (streamCueCount != 0)
            {
                writer.WriteNamedField("StreamTableOffset", (uint)writer.Position - BINAHeader.Size);
        
                for (int i = 0; i < Cues.Count; i++)
                {
                    if (string.IsNullOrEmpty(Cues[i].Stream))
                        continue;

                    writer.CreateStringField($"StreamOffset{i}", Cues[i].Stream);
                }
            }

            writer.FinishWrite();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Cue
    {
        /// <summary>
        /// The name of this cue in the sound bank.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public uint Category { get; set; }

        /// <summary>
        /// TODO: unknown, possibly a flag or maybe unused?
        /// </summary>
        public float UnknownField1 { get; set; }

        /// <summary>
        /// The distance this sound can be heard from.
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// The name of the XMA this cue uses.
        /// <para>Leave blank if using audio from a *.csb file.</para>
        /// </summary>
        public string Stream { get; set; }

        public Cue() { }

        public Cue(string in_name, uint in_category, float in_unknownField1, float in_radius, string in_stream)
        {
            Name = in_name;
            Category = in_category;
            UnknownField1 = in_unknownField1;
            Radius = in_radius;
            Stream = in_stream;
        }

        public override string ToString() => Name;
    }
}
