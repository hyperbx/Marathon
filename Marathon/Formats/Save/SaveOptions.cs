// Format names:        Save Data
// Format references:   Sonicteam::SaveDataTask
// Format designers:    Sonic Team
// Format researchers:  Hyper

namespace Marathon.Formats.Save
{
    public class SaveOptions
    {
        /// <summary>
        /// Determines whether subtitles are enabled.
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

        public SaveOptions() { }

        public SaveOptions(bool in_isSubtitles, float in_music, float in_effects)
        {
            Subtitles = in_isSubtitles;
            Music = in_music;
            Effects = in_effects;
        }
    }
}
