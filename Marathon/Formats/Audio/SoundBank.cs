using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using System.Collections.Generic;
using System.IO;

// Format research attribution: Knuxfan24, Hyper

namespace Marathon.Formats.Audio
{
    /// <summary>
    /// Support for *.sbk files; used for defining sounds.
    /// </summary>
    public class SoundBank : FileBase
    {
        private const string _extension = ".sbk"; // "Sound BanK"
        private const string _signature = "SBNK"; // "Sound BaNK"
        private const uint _magic = 0x20060700;

        public SoundBank() { }

        public SoundBank(string in_path) : base(in_path) { }

        /// <summary>
        /// The name of this sound bank.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The sounds in this bank.
        /// </summary>
        public List<SoundBankData> Sounds { get; set; } = [];

        public SoundBankData this[string in_name]
        {
            get => Sounds.Find((x) => x.Name == in_name);
        }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            reader.CheckSignature(_signature);

            var magic = reader.Read<uint>();
            var nameOffset = reader.Read<uint>();
            var soundTableOffset = reader.Read<uint>();
            var soundIndicesOffset = reader.Read<uint>();
            var streamTableOffset = reader.Read<uint>();

            Name = reader.ReadStringFixedLength(0x40);

            var soundCount = reader.Read<uint>();
            var csbCount = reader.Read<uint>();    // The total number of sounds in this bank that use *.csb streams.
            var streamCount = reader.Read<uint>(); // The total number of sounds in this bank that use external streams.

            var currentStream = 0;

            for (int i = 0; i < soundCount; i++)
            {
                var sound = new SoundBankData()
                {
                    Name = reader.ReadStringFixedLength(0x20)
                };

                var isExternalStream = reader.Read<uint>() != 0;
                var cueIndex = reader.Read<uint>();

                sound.Category = reader.Read<uint>();
                sound.UnknownField1 = reader.Read<float>();
                sound.Radius = reader.Read<float>();

                if (isExternalStream)
                {
                    var pos = reader.Position;

                    // Navigate to the correct pointer in the table.
                    reader.JumpTo(BINAHeader.Size + streamTableOffset);
                    reader.JumpAhead(4 * currentStream);

                    var streamNameOffset = reader.ReadUInt32();

                    reader.ReadAtOffset(BINAHeader.Size + streamNameOffset, () => sound.Stream = reader.ReadStringNullTerminated());
                    reader.JumpTo(pos);

                    currentStream++;
                }

                Sounds.Add(sound);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream);

            var csbCount = 0;
            var streamCount = 0;

            for (int i = 0; i < Sounds.Count; i++)
            {
                if (string.IsNullOrEmpty(Sounds[i].Stream))
                {
                    csbCount++;
                }
                else
                {
                    streamCount++;
                }
            }
        
            writer.WriteSignature(_signature);
            writer.Write(_magic);
            writer.CreateNamedField("NameOffset");
            writer.CreateNamedField("SoundTableOffset");
            writer.CreateNamedField("SoundIndicesOffset");
            writer.CreateNamedField("StreamTableOffset");
        
            writer.WriteNamedField("NameOffset", (uint)writer.Position - BINAHeader.Size);
        
            writer.WriteStringFixedLength(Name, 0x40);
            writer.Write(Sounds.Count);
            writer.Write(csbCount);
            writer.Write(streamCount);

            writer.WriteNamedField("SoundTableOffset", (uint)writer.Position - BINAHeader.Size);

            var csbSoundID = 0;
            var streamSoundID = 0;
        
            for (int i = 0; i < Sounds.Count; i++)
            {
                writer.WriteStringFixedLength(Sounds[i].Name, 0x20);

                if (string.IsNullOrEmpty(Sounds[i].Stream))
                {
                    // Write CSB entry.
                    writer.Write(0);
                    writer.Write(csbSoundID);
                    csbSoundID++;
                }
                else
                {
                    // Write external stream entry.
                    writer.Write(1);
                    writer.Write(streamSoundID);
                    streamSoundID++;
                }
        
                writer.Write(Sounds[i].Category);
                writer.Write(Sounds[i].UnknownField1);
                writer.Write(Sounds[i].Radius);
            }

            if (csbCount != 0)
            {
                writer.WriteNamedField("SoundIndicesOffset", (uint)writer.Position - BINAHeader.Size);
        
                for (int i = 0; i < csbCount; i++)
                    writer.Write(i);
            }

            if (streamCount != 0)
            {
                writer.WriteNamedField("StreamTableOffset", (uint)writer.Position - BINAHeader.Size);
        
                for (int i = 0; i < Sounds.Count; i++)
                {
                    if (string.IsNullOrEmpty(Sounds[i].Stream))
                        continue;

                    writer.CreateStringField($"StreamOffset{i}", Sounds[i].Stream);
                }
            }

            writer.FinishWrite();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class SoundBankData
    {
        /// <summary>
        /// The name of this sound.
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
        /// The radius at which this sound can be heard from.
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// The name of the stream this sound uses.
        /// <para>If using a stream from a *.csb file, leave blank.</para>
        /// </summary>
        public string Stream { get; set; }

        public SoundBankData() { }

        public SoundBankData(string in_name, uint in_category, float in_unknownField1, float in_radius, string in_stream)
        {
            Name = in_name;
            Category = in_category;
            UnknownField1 = in_unknownField1;
            Radius = in_radius;
            Stream = in_stream;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
