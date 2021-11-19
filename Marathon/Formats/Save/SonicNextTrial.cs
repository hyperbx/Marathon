namespace Marathon.Formats.Save
{
    public class SonicNextTrial
    {
        /// <summary>
        /// The ID for this trial set in the Lua score table.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The rank rewarded for this trial.
        /// </summary>
        public SonicNextRank Rank { get; set; }

        /// <summary>
        /// The time taken to complete this trial.
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// The total score achieved in this trial.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// The total rings collected in this trial.
        /// </summary>
        public int Rings { get; set; }

        public override string ToString() => ID == -1 ? "Incomplete" : ID.ToString();
    }
}
