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
        public int Lives { get; set; } = 5;

        /// <summary>
        /// The number of total rings pertaining to this episode.
        /// </summary>
        public int Rings { get; set; }

        /// <summary>
        /// The location of this episode's mission script.
        /// </summary>
        public string Script { get; set; }

        /// <summary>
        /// This mission's temporary flags.
        /// </summary>
        public int[] TemporaryFlags { get; set; } = new int[32];

        /// <summary>
        /// The name of the text book card for the mission text (loaded from <see cref="TextBook"/>).
        /// </summary>
        public string Mission { get; set; }

        /// <summary>
        /// This mission's area code defined in the main game script (<b>game.lub</b>).
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
        /// The location of this mission's text data.
        /// </summary>
        public string TextBook { get; set; }

        /// <summary>
        /// The percent of story completion for this episode.
        /// </summary>
        public int Progress { get; set; }

        /// <summary>
        /// The year this episode was saved during.
        /// </summary>
        public short Year { get; set; }

        /// <summary>
        /// The month this episode was saved during.
        /// </summary>
        public sbyte Month { get; set; }

        /// <summary>
        /// The day this episode was saved during.
        /// </summary>
        public sbyte Day { get; set; }

        /// <summary>
        /// The hour this episode was saved during.
        /// </summary>
        public sbyte Hour { get; set; }

        /// <summary>
        /// The minute this episode was saved during.
        /// </summary>
        public sbyte Minute { get; set; }

        /// <summary>
        /// The name of the text book card for this area's name (loaded from <b>msg_mainmenu</b>).
        /// </summary>
        public string Location { get; set; }

        public override string ToString()
        {
            return $"Progress: {Progress}% | Lives: {Lives} | Rings: {Rings} | Last Save Time: {Year}/{Month}/{Day} {Hour}:{Minute}";
        }
    }
}
