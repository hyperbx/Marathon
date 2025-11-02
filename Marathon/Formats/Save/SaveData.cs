using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.IO;
using Marathon.IO.Extensions;
using System.Collections.Generic;
using System.IO;

// Format names:        Save Data
// Format references:   Sonicteam::SaveDataTask
// Format designers:    Sonic Team
// Format researchers:  Hyper

namespace Marathon.Formats.Save
{
    /// <summary>
    /// Support for SonicNextSaveData.bin; used for storing player progress and game configuration.
    /// </summary>
    public class SaveData : FileBase
    {
        private const string _extension = ".bin"; // "BINary"

        private const int _episodeCount = 32;
        private const int _globalFlagCount = 0x27FF;
        private const int _trialCount = 512;

        public override WriteMode WriteMode => WriteMode.Fixed;

        public SaveEpisode[] Episodes { get; set; } = new SaveEpisode[_episodeCount];

        public Dictionary<SaveFlags, int> GlobalFlags { get; set; } = [];

        public SaveTrial[] Trials { get; set; } = new SaveTrial[_trialCount];

        public SaveOptions Options { get; set; } = new();

        public override string Extension => _extension;

        public SaveData() { }

        public SaveData(string in_path) : base(in_path) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BinaryObjectReaderEx(in_stream, StreamOwnership.Retain, Endianness.Big);

            // Always null.
            reader.CheckSignature(0);

            // Read each episode's lives.
            for (int i = 0; i < _episodeCount; i++)
            {
                Episodes[i] = new()
                {
                    Lives = reader.Read<int>()
                };
            }

            // Read each episode's rings.
            for (int i = 0; i < _episodeCount; i++)
                Episodes[i].Rings = reader.Read<int>();

            for (int i = 0; i <= _globalFlagCount; i++)
                GlobalFlags.Add((SaveFlags)i, reader.Read<int>());

            for (int i = 0; i < _episodeCount; i++)
            {
                Episodes[i].Script = reader.ReadString(StringBinaryFormat.FixedLength, 0x100);
                Episodes[i].TemporaryFlags = reader.ReadArray<int>(32);
                Episodes[i].Mission = reader.ReadStringFixedLength(0x100);
                Episodes[i].Area = reader.ReadStringFixedLength(0x100);
                Episodes[i].Terrain = reader.ReadStringFixedLength(0x100);
                Episodes[i].StageSet = reader.ReadStringFixedLength(0x100);
                Episodes[i].SplinePath = reader.ReadStringFixedLength(0x100);

                // TODO: unknown.
                reader.JumpAhead(0x500);

                Episodes[i].TextBook = reader.ReadStringFixedLength(0x100);

                // TODO: unknown, contains flags!
                reader.JumpAhead(0x24C);

                Episodes[i].Progress = reader.Read<int>();
                Episodes[i].Year = reader.Read<short>();
                Episodes[i].Month = reader.Read<sbyte>();
                Episodes[i].Day = reader.Read<sbyte>();
                Episodes[i].Hour = reader.Read<sbyte>();
                Episodes[i].Minute = reader.Read<sbyte>();
                Episodes[i].Location = reader.ReadStringFixedLength(0x42);
            }

            for (int i = 0; i < _trialCount; i++)
            {
                Trials[i] = new()
                {
                    ID = reader.Read<int>(),
                    Rank = reader.Read<SaveRank>(),
                    Time = reader.Read<int>(),
                    Score = reader.Read<int>(),
                    Rings = reader.Read<int>()
                };
            }

            Options.Subtitles = reader.Read<uint>() != 0;
            Options.Music = reader.Read<float>();
            Options.Effects = reader.Read<float>();
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BinaryObjectWriterEx(in_stream, StreamOwnership.Retain, Endianness.Big);
        
            writer.Write(0);
        
            for (int i = 0; i < _episodeCount; i++)
                writer.Write(Episodes[i].Lives);
        
            for (int i = 0; i < _episodeCount; i++)
                writer.Write(Episodes[i].Rings);
        
            for (int i = 0; i <= _globalFlagCount; i++)
                writer.Write(GlobalFlags[(SaveFlags)i]);
        
            for (int i = 0; i < _episodeCount; i++)
            {
                var episode = Episodes[i];

                writer.WriteStringFixedLength(episode.Script, 0x100);

                for (int j = 0; j < 32; j++)
                    writer.Write(episode.TemporaryFlags[j]);

                writer.WriteStringFixedLength(episode.Mission, 0x100);
                writer.WriteStringFixedLength(episode.Area, 0x100);
                writer.WriteStringFixedLength(episode.Terrain, 0x100);
                writer.WriteStringFixedLength(episode.StageSet, 0x100);
                writer.WriteStringFixedLength(episode.SplinePath, 0x100);
        
                // TODO: unknown.
                writer.JumpAhead(0x500);
        
                writer.WriteStringFixedLength(episode.TextBook, 0x100);
        
                // TODO: unknown, contains flags!
                writer.JumpAhead(0x24C);
        
                writer.Write(episode.Progress);
                writer.Write(episode.Year);
                writer.Write(episode.Month);
                writer.Write(episode.Day);
                writer.Write(episode.Hour);
                writer.Write(episode.Minute);
        
                writer.WriteStringFixedLength(episode.Location, 0x42);
            }
        
            for (int i = 0; i < _trialCount; i++)
            {
                var trial = Trials[i];

                writer.Write(trial.ID);
                writer.Write(trial.Rank);
                writer.Write(trial.Time);
                writer.Write(trial.Score);
                writer.Write(trial.Rings);
            }
        
            writer.Write(Options.Subtitles ? 1 : 0);
            writer.Write(Options.Music);
            writer.Write(Options.Effects);
        }
    }
}
