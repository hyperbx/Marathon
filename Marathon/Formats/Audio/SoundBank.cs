using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections;
using System.Collections.Generic;
using System.IO;

// Format names:        Sound Bank
// Format references:   Sonicteam::SoX::Audio::SoundBank, Sonicteam::SoundBankDataBin
// Format designers:    Sonic Team
// Format researchers:  Knuxfan24, Hyper

namespace Marathon.Formats.Audio
{
    /// <summary>
    /// Support for *.sbk files; used for defining sounds.
    /// </summary>
    public class SoundBank : FileBase, IList<SoundBankData>
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

        public int Count => Sounds.Count;

        public bool IsReadOnly => false;

        public SoundBankData this[int in_index]
        {
            get => Sounds[in_index];
            set => Sounds[in_index] = value;
        }

        public SoundBankData this[string in_name]
        {
            get => Sounds.Find(x => x.Name == in_name);
        }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            Endianness = reader.Endianness;

            reader.CheckSignature(_signature);

            var magic = reader.Read<uint>();
            var nameOffset = reader.Read<uint>();
            var soundTableOffset = reader.Read<uint>();
            var soundIndicesOffset = reader.Read<uint>();
            var streamTableOffset = reader.Read<uint>();

            Name = reader.ReadStringFixedLength(0x40);

            var soundCount = reader.Read<uint>();
            var csbCount = reader.Read<uint>();
            var streamCount = reader.Read<uint>();

            var currentStream = 0;

            for (int i = 0; i < soundCount; i++)
            {
                var sound = new SoundBankData()
                {
                    Name = reader.ReadStringFixedLength(0x20)
                };

                sound.StreamType = reader.Read<StreamType>();

                var soundIndex = reader.Read<uint>();

                sound.UnknownField1 = reader.Read<uint>();
                sound.UnknownField2 = reader.Read<float>();
                sound.Radius = reader.Read<float>();

                if (sound.StreamType == StreamType.External && streamTableOffset != 0)
                {
                    var pos = reader.Position;

                    // Navigate to the correct pointer in the table.
                    reader.JumpTo(BINAHeader.Size + streamTableOffset);
                    reader.JumpAhead(4 * currentStream);

                    var streamNameOffset = reader.Read<uint>();

                    reader.ReadAtOffset(BINAHeader.Size + streamNameOffset,
                        () => sound.Stream = reader.ReadStringNullTerminated());

                    reader.JumpTo(pos);

                    currentStream++;
                }

                Sounds.Add(sound);
            }

            if (soundIndicesOffset != 0)
            {
                reader.JumpTo(BINAHeader.Size + soundIndicesOffset);

                for (int i = 0; i < Sounds.Count; i++)
                {
                    if (Sounds[i].StreamType != StreamType.CueSheet)
                        continue;

                    Sounds[i].CueIndex = reader.Read<int>();
                }
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream, Endianness);

            var csbCount = 0;
            var streamCount = 0;
            var hasIndexTable = false;
            var hasStreamTable = false;

            foreach (var sound in Sounds)
            {
                if (sound.StreamType == StreamType.CueSheet)
                {
                    csbCount++;

                    if (sound.CueIndex != -1)
                        hasIndexTable = true;
                }
                else if (sound.StreamType == StreamType.External)
                {
                    streamCount++;

                    if (!string.IsNullOrEmpty(sound.Stream))
                        hasStreamTable = true;
                }
            }

            writer.WriteSignature(_signature);
            writer.Write(_magic);
            writer.Reserve<uint>("NameOffset");
            writer.Reserve<uint>("SoundTableOffset");

            if (csbCount == 0 || !hasIndexTable)
            {
                writer.Write(0);
            }
            else
            {
                writer.Reserve<uint>("SoundIndicesOffset");
            }

            if (streamCount == 0 || !hasStreamTable)
            {
                writer.Write(0);
            }
            else
            {
                writer.Reserve<uint>("StreamTableOffset");
            }

            writer.WriteReserved("NameOffset", (uint)writer.Position - BINAHeader.Size);
            writer.WriteStringFixedLength(Name, 0x40);
            writer.Write(Sounds.Count);
            writer.Write(csbCount);
            writer.Write(streamCount);
            writer.WriteReserved("SoundTableOffset", (uint)writer.Position - BINAHeader.Size);

            var csbSoundID = 0;
            var streamSoundID = 0;
        
            for (int i = 0; i < Sounds.Count; i++)
            {
                var sound = Sounds[i];

                writer.WriteStringFixedLength(sound.Name, 0x20);
                writer.Write(sound.StreamType);

                if (sound.StreamType == StreamType.CueSheet)
                {
                    writer.Write(csbSoundID);
                    csbSoundID++;
                }
                else if (sound.StreamType == StreamType.External)
                {
                    writer.Write(streamSoundID);
                    streamSoundID++;
                }
                else
                {
                    writer.Write(0);
                }

                writer.Write(sound.UnknownField1);
                writer.Write(sound.UnknownField2);
                writer.Write(sound.Radius);
            }

            if (csbCount != 0 && hasIndexTable)
            {
                writer.WriteReserved("SoundIndicesOffset", (uint)writer.Position - BINAHeader.Size);

                for (int i = 0; i < Sounds.Count; i++)
                {
                    if (Sounds[i].StreamType != StreamType.CueSheet)
                        continue;

                    writer.Write(Sounds[i].CueIndex);
                }
            }

            if (streamCount != 0 && hasStreamTable)
            {
                writer.WriteReserved("StreamTableOffset", (uint)writer.Position - BINAHeader.Size);

                for (int i = 0; i < Sounds.Count; i++)
                {
                    if (Sounds[i].StreamType != StreamType.External)
                        continue;

                    writer.WriteStringOffset(Sounds[i].Stream);
                }
            }

            writer.FinishWrite();
        }

        public int IndexOf(SoundBankData in_item)
        {
            return Sounds.IndexOf(in_item);
        }

        public void Insert(int in_index, SoundBankData in_item)
        {
            Sounds.Insert(in_index, in_item);
        }

        public void RemoveAt(int in_index)
        {
            Sounds.RemoveAt(in_index);
        }

        public void Add(SoundBankData in_item)
        {
            Sounds.Add(in_item);
        }

        public void Clear()
        {
            Sounds.Clear();
        }

        public bool Contains(SoundBankData in_item)
        {
            return Sounds.Contains(in_item);
        }

        public void CopyTo(SoundBankData[] in_array, int in_arrayIndex)
        {
            Sounds.CopyTo(in_array, in_arrayIndex);
        }

        public bool Remove(SoundBankData in_item)
        {
            return Sounds.Remove(in_item);
        }

        public IEnumerator<SoundBankData> GetEnumerator()
        {
            return Sounds.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
        /// The type of audio stream used by this sound.
        /// </summary>
        public StreamType StreamType { get; set; }

        /// <summary>
        /// TODO: unknown.
        /// </summary>
        public uint UnknownField1 { get; set; }

        /// <summary>
        /// TODO: unknown, possibly a flag or maybe unused?
        /// </summary>
        public float UnknownField2 { get; set; }

        /// <summary>
        /// The radius at which this sound can be heard from.
        /// </summary>
        public float Radius { get; set; }

        /// <summary>
        /// The index of the cue in the *.csb file.
        /// </summary>
        public int CueIndex { get; set; } = -1;

        /// <summary>
        /// The name of the stream this sound uses.
        /// <para>If using a stream from a *.csb file, leave blank.</para>
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Stream { get; set; }

        public SoundBankData() { }

        public SoundBankData(string in_name, uint in_unkField1, float in_unkField2, float in_radius, int in_cueIndex, string in_stream)
        {
            Name = in_name;
            UnknownField1 = in_unkField1;
            UnknownField2 = in_unkField2;
            Radius = in_radius;
            CueIndex = in_cueIndex;
            Stream = in_stream;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StreamType : int
    {
        Undefined = -1,

        /// <summary>
        /// The audio data is stored in a *.csb file.
        /// </summary>
        CueSheet,

        /// <summary>
        /// The audio data is stored in an external audio file (e.g. *.xma, *.at3).
        /// </summary>
        External
    }
}
