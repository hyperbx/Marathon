// SaveData.cs is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 HyperPolygon64
 * Copyright (c) 2020 Knuxfan24
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System.IO;
using System.Collections.Generic;

namespace Marathon.IO.Formats.Miscellaneous
{
    // Big-endian Save Data Research
    /*
     * (0x04) - Life Count for Sonic (Int32)
     * (0x08) - Life Count for Shadow (Int32)
     * (0x0C) - Life Count for Silver (Int32)
     * (0x10) - Life Count for Last Episode (Int32)
     * (0x14 - 0x80) - Life Counts for DLC Slots (Int32)
     * (0x84) - Ring Count for Sonic (Int32)
     * (0x88) - Ring Count for Shadow (Int32)
     * (0x8C) - Ring Count for Silver (Int32)
     * (0x90) - Ring Count for Last Episode (Int32)
     * (0x94 - 0x100) - Ring Counts for DLC Slots (Int32)
     * (0x5EC4) - Sonic Light Dash (Boolean)
     * (0x5EC8) - Sonic Anti-Gravity (Boolean)
     * (0x5ECC) - Sonic Bound Bracelet (Boolean)
     * (0x5ED0) - Sonic Unused Bracelet (Boolean)
     * (0x5ED4) - Sonic Green Gem (Boolean)
     * (0x5ED8) - Sonic Red Gem (Boolean)
     * (0x5EDC) - Sonic Blue Gem (Boolean)
     * (0x5EE0) - Sonic White Gem (Boolean)
     * (0x5EE4) - Sonic Sky Gem (Boolean)
     * (0x5EE8) - Sonic Yellow Gem (Boolean)
     * (0x5EEC) - Sonic Purple Gem (Boolean)
     * (0x5EF0) - Sonic Rainbow Gem (Boolean)
     * (0xAFD0) - Progress (Int32)
     * (0xAFD4) - Year (Int16)
     * (0xAFD6) - Month (Int8)
     * (0xAFD7) - Day (Int8)
     * (0xAFD8) - Hour (Int8)
     * (0xAFD9) - Minute (Int8)
     *
     * Town Missions seem to start at 28404, 5 sets of bytes.
     * First set unknown, starts as FF FF FF FF? Stats aren't displayed when this is the case?
     * Second Set Rank, from 00 (S) to 04 (D)
     * Third Set controls saves time as a int in milliseconds
     * Fourth Set is score
     * Fifth Set is rings
     * 
     * (0x2AC04) - Subtitles (Boolean)
     * (0x2AC08) - Music Volume (Single)
     * (0x2AC0C) - Effect Volume (Single)
     */

    /* TODO: the save data contains a crap ton of flags;
             these aren't read as we can't easily identify the Boolean values,
             so a lot of research will need to be done. */

    /// <summary>
    /// Fixed file base for the Sonic '06 SonicNextSaveData.bin format.
    /// </summary>
    public class SaveData : FixedFileBase
    {
        /// <summary>
        /// Information stored for each episode.
        /// </summary>
        public class Episode
        {
            public long Offset;     // Offset of the current episode.

            public int Lives,       // Total lives.
                       Rings,       // Total Rings.
                       Progress;    // Progress percentage.

            public short Year;      // Year of last save.

            public sbyte Month,     // Month of last save.
                         Day,       // Day of last save.
                         Hour,      // Hour of last save.
                         Minute;    // Minute of last save.

            public string Location; // Name of the current location string.

            // Information for the currently saved location.
            public List<Stage> Information = new List<Stage>();
        }

        /// <summary>
        /// Information stored for each episode's location data.
        /// </summary>
        public class Stage
        {
            public long Offset; // Offset of the current stage.

            public string Name; // Path or name for the current stage.
        }

        /// <summary>
        /// Information stored for each mission.
        /// </summary>
        public class Mission
        {
            public int UnknownInt32_1, // TODO: Unknown - possibly a flag?
                       Rank,           // Best rank (0 - S | 1 - A | 2 - B | 3 - C | 4 - D).
                       Time,           // Best time.
                       Score,          // Best score.
                       Rings;          // Best Rings.
        }

        /// <summary>
        /// Boolean array to toggle character upgrades.
        /// </summary>
        public class Upgrade
        {
                        // Sonic's upgrades.
            public bool equip_lightdash,
                        equip_sliding,
                        equip_boundjump,
                        equip_homingsmash,
                        equip_gem_green,
                        equip_gem_red,
                        equip_gem_blue,
                        equip_gem_white,
                        equip_gem_sky,
                        equip_gem_yellow,
                        equip_gem_purple,
                        equip_gem_super,
                        
                        // Shadow's upgrades.
                        equip_shadow_lightdash,
                        equip_shadow_boost_lv1,
                        equip_shadow_boost_lv2,
                        equip_shadow_boost_lv3,
                        
                        // Silver's upgrades.
                        equip_silver_holdsmash,
                        equip_silver_catch_all,
                        equip_silver_teleport,
                        equip_silver_psychoshock,
                        equip_silver_speedup;
        }

        /// <summary>
        /// Information stored for the system settings.
        /// </summary>
        public class System
        {
            public bool Subtitles; // Subtitles toggle.

            public float Music,    // Music volume.
                         Effects;  // Sound effect volume.
        }

        public const string Extension = ".bin";

        public List<Episode> Episodes = new List<Episode>();
        public List<Mission> Missions = new List<Mission>();
        public Upgrade Upgrades = new Upgrade();
        public System Settings = new System();

        public override void Load(Stream stream)
        {
            ExtendedBinaryReader reader = new ExtendedBinaryReader(stream, true);

            Episode sonic  = new Episode(),
                    shadow = new Episode(),
                    silver = new Episode(),
                    last   = new Episode();

            reader.ReadUInt32(); // Skip what appears to be padding.

            // Define life count.
            sonic.Lives  = reader.ReadInt32();
            shadow.Lives = reader.ReadInt32();
            silver.Lives = reader.ReadInt32();
            last.Lives   = reader.ReadInt32();

            reader.JumpTo(0x84); // Jump to Ring table.

            // Define Ring count.
            sonic.Rings  = reader.ReadInt32();
            shadow.Rings = reader.ReadInt32();
            silver.Rings = reader.ReadInt32();
            last.Rings   = reader.ReadInt32();

            reader.JumpTo(0x5EC4); // Jump to upgrade table.

            // Sonic's upgrades.
            Upgrades.equip_lightdash   = reader.ReadBoolean32();
            Upgrades.equip_sliding     = reader.ReadBoolean32();
            Upgrades.equip_boundjump   = reader.ReadBoolean32();
            Upgrades.equip_homingsmash = reader.ReadBoolean32();
            Upgrades.equip_gem_green   = reader.ReadBoolean32();
            Upgrades.equip_gem_red     = reader.ReadBoolean32();
            Upgrades.equip_gem_blue    = reader.ReadBoolean32();
            Upgrades.equip_gem_white   = reader.ReadBoolean32();
            Upgrades.equip_gem_sky     = reader.ReadBoolean32();
            Upgrades.equip_gem_yellow  = reader.ReadBoolean32();
            Upgrades.equip_gem_purple  = reader.ReadBoolean32();
            Upgrades.equip_gem_super   = reader.ReadBoolean32();

            // Shadow's upgrades.
            Upgrades.equip_shadow_lightdash = reader.ReadBoolean32();
            Upgrades.equip_shadow_boost_lv1 = reader.ReadBoolean32();
            Upgrades.equip_shadow_boost_lv2 = reader.ReadBoolean32();
            Upgrades.equip_shadow_boost_lv3 = reader.ReadBoolean32();

            // Silver's upgrades.
            Upgrades.equip_silver_holdsmash   = reader.ReadBoolean32();
            Upgrades.equip_silver_catch_all   = reader.ReadBoolean32();
            Upgrades.equip_silver_teleport    = reader.ReadBoolean32();
            Upgrades.equip_silver_psychoshock = reader.ReadBoolean32();
            Upgrades.equip_silver_speedup     = reader.ReadBoolean32();

            void ReadEpisodeInformation(Episode episode, long endOffset)
            {
                while (reader.BaseStream.Position != endOffset)
                {
                    long position = reader.BaseStream.Position;
                    string information = reader.ReadNullTerminatedString();

                    if (!string.IsNullOrEmpty(information) && information != "\u0001")
                    {
                        episode.Information.Add(new Stage()
                        {
                            Name = information,
                            Offset = position
                        });
                    }
                }

                episode.Offset = reader.BaseStream.Position;

                episode.Progress = reader.ReadInt32();
                episode.Year     = reader.ReadInt16();
                episode.Month    = reader.ReadSByte();
                episode.Day      = reader.ReadSByte();
                episode.Hour     = reader.ReadSByte();
                episode.Minute   = reader.ReadSByte();
                episode.Location = reader.ReadNullTerminatedString();
            }

            reader.JumpTo(0xA104); // Jump to episode information.

            // Read Sonic's Episode information.
            ReadEpisodeInformation(sonic, 0xAFD0);

            // Read Shadow's Episode information.
            ReadEpisodeInformation(shadow, 0xBEE8);

            // Read Silver's Episode information.
            ReadEpisodeInformation(silver, 0xCE00);

            // Read Last Episode information.
            ReadEpisodeInformation(last, 0xDD18);

            reader.JumpTo(0x28404); // Jump to mission information.

            // Read all mission information.
            while (reader.BaseStream.Position != 0x2AC04)
            {
                Mission mission = new Mission()
                {
                    UnknownInt32_1 = reader.ReadInt32(),
                    Rank  = reader.ReadInt32(),
                    Time  = reader.ReadInt32(),
                    Score = reader.ReadInt32(),
                    Rings = reader.ReadInt32()
                };

                Missions.Add(mission);
            }

            // Read system settings.
            Settings.Subtitles = reader.ReadBoolean32();
            Settings.Music     = reader.ReadSingle();
            Settings.Effects   = reader.ReadSingle();

            // Add all episodes.
            Episodes.AddRange(new[] { sonic, shadow, silver, last });
        }

        public override void Save(Stream stream)
        {
            ExtendedBinaryWriter writer = new ExtendedBinaryWriter(stream, true);

            writer.BaseStream.Position = 0x04;

            foreach (Episode episode in Episodes)
                writer.WriteByType<int>(episode.Lives);

            writer.BaseStream.Position = 0x84;

            foreach (Episode episode in Episodes)
                writer.WriteByType<int>(episode.Rings);

            writer.BaseStream.Position = 0x5EC4;

            // Sonic's upgrades.
            writer.WriteBoolean32(Upgrades.equip_lightdash);
            writer.WriteBoolean32(Upgrades.equip_sliding);
            writer.WriteBoolean32(Upgrades.equip_boundjump);
            writer.WriteBoolean32(Upgrades.equip_homingsmash);
            writer.WriteBoolean32(Upgrades.equip_gem_green);
            writer.WriteBoolean32(Upgrades.equip_gem_red);
            writer.WriteBoolean32(Upgrades.equip_gem_blue);
            writer.WriteBoolean32(Upgrades.equip_gem_white);
            writer.WriteBoolean32(Upgrades.equip_gem_sky);
            writer.WriteBoolean32(Upgrades.equip_gem_yellow);
            writer.WriteBoolean32(Upgrades.equip_gem_purple);
            writer.WriteBoolean32(Upgrades.equip_gem_super);

            // Shadow's upgrades.
            writer.WriteBoolean32(Upgrades.equip_shadow_lightdash);
            writer.WriteBoolean32(Upgrades.equip_shadow_boost_lv1);
            writer.WriteBoolean32(Upgrades.equip_shadow_boost_lv2);
            writer.WriteBoolean32(Upgrades.equip_shadow_boost_lv3);

            // Silver's upgrades.
            writer.WriteBoolean32(Upgrades.equip_silver_holdsmash);
            writer.WriteBoolean32(Upgrades.equip_silver_catch_all);
            writer.WriteBoolean32(Upgrades.equip_silver_teleport);
            writer.WriteBoolean32(Upgrades.equip_silver_psychoshock);
            writer.WriteBoolean32(Upgrades.equip_silver_speedup);

            writer.BaseStream.Position = 0xA104;

            foreach (Episode episode in Episodes)
            {
                foreach (Stage info in episode.Information)
                {
                    writer.BaseStream.Position = info.Offset;
                    writer.WriteNullTerminatedString(info.Name);
                }

                writer.BaseStream.Position = episode.Offset;

                writer.WriteByType<int>(episode.Progress);
                writer.WriteByType<short>(episode.Year);
                writer.WriteByType<sbyte>(episode.Month);
                writer.WriteByType<sbyte>(episode.Day);
                writer.WriteByType<sbyte>(episode.Hour);
                writer.WriteByType<sbyte>(episode.Minute);
                writer.WriteNullTerminatedString(episode.Location);
            }

            writer.BaseStream.Position = 0x28404;

            foreach (Mission mission in Missions)
            {
                writer.WriteByType<int>(mission.UnknownInt32_1);
                writer.WriteByType<int>(mission.Rank);
                writer.WriteByType<int>(mission.Time);
                writer.WriteByType<int>(mission.Score);
                writer.WriteByType<int>(mission.Rings);
            }

            writer.WriteBoolean32(Settings.Subtitles);
            writer.WriteByType<float>(Settings.Music);
            writer.WriteByType<float>(Settings.Effects);
        }
    }
}
