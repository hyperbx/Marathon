// Format names:        Save Data
// Format references:   Sonicteam::SaveDataTask
// Format designers:    Sonic Team
// Format researchers:  Hyper

namespace Marathon.Formats.Save
{
    public class SaveEpisode
    {
        /// <summary>
        /// The number of total lives pertaining to this episode.
        /// </summary>
        public int Lives { get; set; }

        /// <summary>
        /// The number of total rings pertaining to this episode.
        /// </summary>
        public int Rings { get; set; }

        /// <summary>
        /// The location of this mission's Lua script.
        /// </summary>
        public string Lua { get; set; }

        /// <summary>
        /// The name of the text book message for the loading screen text.
        /// </summary>
        public string Objective { get; set; }

        /// <summary>
        /// This mission's area code defined in the main game script ("game.lub").
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// The location of this mission's terrain data.
        /// </summary>
        public string Terrain { get; set; }

        /// <summary>
        /// The location of this mission's stage set data.
        /// </summary>
        public string StageSet { get; set; }

        /// <summary>
        /// The location of this mission's spline data.
        /// </summary>
        public string SplinePath { get; set; }

        /// <summary>
        /// The location of this mission's message data.
        /// </summary>
        public string TextBook { get; set; }

        /// <summary>
        /// The percent of story completion for this episode.
        /// </summary>
        public int Progress { get; set; }

        /// <summary>
        /// The year this save data was created on.
        /// </summary>
        public short Year { get; set; }

        /// <summary>
        /// The month of the year this save data was created on.
        /// </summary>
        public sbyte Month { get; set; }

        /// <summary>
        /// The day of the month this save data was created on.
        /// </summary>
        public sbyte Day { get; set; }

        /// <summary>
        /// The hour of the day this save data was created on.
        /// </summary>
        public sbyte Hour { get; set; }

        /// <summary>
        /// The minute of the hour this save data was created on.
        /// </summary>
        public sbyte Minute { get; set; }

        /// <summary>
        /// The name of the text book message for the location text.
        /// </summary>
        public string Location { get; set; }

        public override string ToString()
        {
            return Lua;
        }
    }
}
