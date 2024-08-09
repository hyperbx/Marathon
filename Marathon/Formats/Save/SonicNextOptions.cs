namespace Marathon.Formats.Save
{
    public class SonicNextOptions
    {
        /// <summary>
        /// Determines whether or not subtitles are enabled.
        /// </summary>
        public bool Subtitles { get; set; }

        /// <summary>
        /// The volume of the music.
        /// </summary>
        public float Music { get; set; }

        /// <summary>
        /// The volume of the sound effects.
        /// </summary>
        public float Effects { get; set; }

        public SonicNextOptions() { }

        public SonicNextOptions(bool in_isSubtitles, float in_music, float in_effects)
        {
            Subtitles = in_isSubtitles;
            Music = in_music;
            Effects = in_effects;
        }
    }
}
