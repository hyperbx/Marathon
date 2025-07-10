using Amicitia.IO.Binary;
using Amicitia.IO.Streams;
using Marathon.IO;
using Marathon.IO.Extensions;
using System.Collections.Generic;
using System.IO;

// Format research attribution: Hyper

namespace Marathon.Formats.Save
{
    /// <summary>
    /// Support for SonicNextSaveData.bin; used for storing player progress and game configuration.
    /// </summary>
    public class SonicNextSaveData : FileBase
    {
        private const int _episodeCount = 32;
        private const int _globalFlagCount = 0x27FF;
        private const int _trialCount = 512;

        public SonicNextSaveData() { }

        public SonicNextSaveData(string in_path) : base(in_path) { }

        public override WriteMode WriteMode => WriteMode.Fixed;

        public SonicNextEpisode[] Episodes { get; set; } = new SonicNextEpisode[_episodeCount];

        public Dictionary<SonicNextFlags, int> GlobalFlags { get; set; } = [];

        public SonicNextTrial[] Trials { get; set; } = new SonicNextTrial[_trialCount];

        public SonicNextOptions Options { get; set; } = new();

        public override void Read(Stream in_stream)
        {
            var reader = new BinaryObjectReaderEx(in_stream, StreamOwnership.Retain, Endianness.Big);

            // Expected zero.
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
                GlobalFlags.Add((SonicNextFlags)i, reader.Read<int>());

            for (int i = 0; i < _episodeCount; i++)
            {
                Episodes[i].Lua = reader.ReadString(StringBinaryFormat.FixedLength, 0x100);

                // TODO: unknown, contains flags!
                reader.JumpAhead(0x80);

                Episodes[i].Objective  = reader.ReadStringFixedLength(0x100);
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
                    Rank = reader.Read<SonicNextRank>(),
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
                writer.Write(GlobalFlags[(SonicNextFlags)i]);
        
            for (int i = 0; i < _episodeCount; i++)
            {
                writer.WriteStringFixedLength(Episodes[i].Lua, 0x100);
        
                // TODO: unknown, contains flags!
                writer.JumpAhead(0x80);
        
                writer.WriteStringFixedLength(Episodes[i].Objective, 0x100);
                writer.WriteStringFixedLength(Episodes[i].Area, 0x100);
                writer.WriteStringFixedLength(Episodes[i].Terrain, 0x100);
                writer.WriteStringFixedLength(Episodes[i].StageSet, 0x100);
                writer.WriteStringFixedLength(Episodes[i].SplinePath, 0x100);
        
                // TODO: unknown.
                writer.JumpAhead(0x500);
        
                writer.WriteStringFixedLength(Episodes[i].TextBook, 0x100);
        
                // TODO: unknown, contains flags!
                writer.JumpAhead(0x24C);
        
                writer.Write(Episodes[i].Progress);
                writer.Write(Episodes[i].Year);
                writer.Write(Episodes[i].Month);
                writer.Write(Episodes[i].Day);
                writer.Write(Episodes[i].Hour);
                writer.Write(Episodes[i].Minute);
        
                writer.WriteStringFixedLength(Episodes[i].Location, 0x42);
            }
        
            for (int i = 0; i < _trialCount; i++)
            {
                writer.Write(Trials[i].ID);
                writer.Write(Trials[i].Rank);
                writer.Write(Trials[i].Time);
                writer.Write(Trials[i].Score);
                writer.Write(Trials[i].Rings);
            }
        
            writer.Write(Options.Subtitles ? 1 : 0);
            writer.Write(Options.Music);
            writer.Write(Options.Effects);
        }
    }
}
