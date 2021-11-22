namespace Marathon.Formats.Save
{
    public class SonicNextEpisode
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
        /// The path to this mission's Lua script.
        /// </summary>
        public string Lua { get; set; }

        /// <summary>
        /// The name of the MST entry for the loading screen text.
        /// </summary>
        public string Objective { get; set; }

        /// <summary>
        /// This mission's area code defined in game.lub.
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// The path to this mission's terrain data.
        /// </summary>
        public string Terrain { get; set; }

        /// <summary>
        /// The path to this mission's SET data.
        /// </summary>
        public string SET { get; set; }

        /// <summary>
        /// The path to this mission's PATH data.
        /// </summary>
        public string PATH { get; set; }

        /// <summary>
        /// The path to this mission's MST data containing <see cref="String"/>.
        /// </summary>
        public string MST { get; set; }

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
        /// The name of the MST entry for the location text.
        /// </summary>
        public string Location { get; set; }

        public override string ToString() => Lua;
    }
}
