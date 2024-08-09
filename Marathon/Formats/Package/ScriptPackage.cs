namespace Marathon.Formats.Package
{
    /// <summary>
    /// File base for the ScriptParameter.bin format.
    /// <para>Used in SONIC THE HEDGEHOG for defining properties for enemy types.</para>
    /// </summary>
    public class ScriptPackage : FileBase
    {
        public ScriptPackage() { }

        public ScriptPackage(string file, bool serialise = false)
        {
            switch (Path.GetExtension(file))
            {
                case ".json":
                {
                    Parameters = JsonDeserialise<List<ScriptParameter>>(file);

                    // Save extension-less JSON (exploiting .NET weirdness, because it doesn't omit all extensions).
                    if (serialise)
                        Save(Path.GetFileNameWithoutExtension(file));

                    break;
                }

                default:
                {
                    Load(file);

                    if (serialise)
                        JsonSerialise(Parameters);

                    break;
                }
            }
        }

        public override string Extension { get; } = ".bin";

        public List<ScriptParameter> Parameters { get; set; } = [];

        public override void Load(Stream stream)
        {
            BINAReader reader = new(stream);

            var begin = reader.BaseStream.Position;
            var firstStringOffset = reader.ReadInt32() + reader.Offset;
            var count = firstStringOffset / ScriptParameter.Size;
            
            reader.JumpTo(begin);

            for (int i = 0; i < count; i++)
            {
                ScriptParameter scriptParameter = new();

                int nameOffset = reader.ReadInt32();

                scriptParameter.State  = reader.ReadInt32();
                scriptParameter.Health = reader.ReadInt32();
                scriptParameter.Score  = reader.ReadInt32();

                scriptParameter.UnknownSingle_1    = reader.ReadSingle();
                scriptParameter.FoundRangeIn      = reader.ReadSingle();
                scriptParameter.FoundRangeOut     = reader.ReadSingle();
                scriptParameter.UnknownSingle_2    = reader.ReadSingle();
                scriptParameter.UnknownSingle_3    = reader.ReadSingle();
                scriptParameter.MovementSpeed     = reader.ReadSingle();
                scriptParameter.UnknownSingle_4    = reader.ReadSingle();
                scriptParameter.UnknownSingle_5    = reader.ReadSingle();
                scriptParameter.RotationSpeed     = reader.ReadSingle();
                scriptParameter.UnknownSingle_6   = reader.ReadSingle();
                scriptParameter.UnknownSingle_7   = reader.ReadSingle();
                scriptParameter.UnknownSingle_8   = reader.ReadSingle();
                scriptParameter.UnknownSingle_9   = reader.ReadSingle();
                scriptParameter.UnknownSingle_10   = reader.ReadSingle();
                scriptParameter.UnknownSingle_11   = reader.ReadSingle();
                scriptParameter.FoundReactionTime = reader.ReadSingle();
                scriptParameter.UnknownSingle_12   = reader.ReadSingle();

                long pos = reader.BaseStream.Position;

                scriptParameter.Name = reader.ReadNullTerminatedString(false, nameOffset, true);

                reader.JumpTo(pos);

                Parameters.Add(scriptParameter);
            }
        }

        public override void Save(Stream stream)
        {
            BINAWriter writer = new(stream);

            foreach (var parameter in Parameters)
            {
                writer.AddString($"StringOffset{writer.BaseStream.Position}", parameter.Name);

                writer.Write(parameter.State);
                writer.Write(parameter.Health);
                writer.Write(parameter.Score);

                writer.Write(parameter.UnknownSingle_1);
                writer.Write(parameter.FoundRangeIn);
                writer.Write(parameter.FoundRangeOut);
                writer.Write(parameter.UnknownSingle_2);
                writer.Write(parameter.UnknownSingle_3);
                writer.Write(parameter.MovementSpeed);
                writer.Write(parameter.UnknownSingle_4);
                writer.Write(parameter.UnknownSingle_5);
                writer.Write(parameter.RotationSpeed);
                writer.Write(parameter.UnknownSingle_6);
                writer.Write(parameter.UnknownSingle_7);
                writer.Write(parameter.UnknownSingle_8);
                writer.Write(parameter.UnknownSingle_9);
                writer.Write(parameter.UnknownSingle_10);
                writer.Write(parameter.UnknownSingle_11);
                writer.Write(parameter.FoundReactionTime);
                writer.Write(parameter.UnknownSingle_12);
            }

            writer.WriteNulls(4);
            writer.FinishWrite();
        }
    }

    public class ScriptParameter
    {
        /// <summary>
        /// Name of the state.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// <para>0 - Grounded</para>
        /// <para>1 - Flying (?)</para>
        /// <para>2 - Flying (?)</para>
        /// <para>3 - Wall (?)</para>
        /// <para>4 - Wall (?)</para>
        /// <para>5 - Idle Attack</para>
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// Total health points.
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Score given to the player upon dying.
        /// </summary>
        public int Score { get; set; }

        public float UnknownSingle_1 { get; set; }

        /// <summary>
        /// The range the player must be in for the enemies to find them.
        /// </summary>
        public float FoundRangeIn { get; set; }

        /// <summary>
        /// TODO: needs more research.
        /// </summary>
        public float FoundRangeOut { get; set; }

        public float UnknownSingle_2 { get; set; }

        public float UnknownSingle_3 { get; set; }

        /// <summary>
        /// The speed the enemies move at when roam searching for the player.
        /// </summary>
        public float MovementSpeed { get; set; }

        public float UnknownSingle_4 { get; set; }

        public float UnknownSingle_5 { get; set; }

        /// <summary>
        /// The speed the enemies rotate at when searching for the player.
        /// </summary>
        public float RotationSpeed { get; set; }

        /// <summary>
        /// Something to do with rotation speed.
        /// </summary>
        public float UnknownSingle_6 { get; set; }

        /// <summary>
        /// Something to do with rotation speed.
        /// </summary>
        public float UnknownSingle_7 { get; set; }

        public float UnknownSingle_8 { get; set; }

        public float UnknownSingle_9 { get; set; }

        public float UnknownSingle_10 { get; set; }

        public float UnknownSingle_11 { get; set; }

        /// <summary>
        /// Total time taken for the enemies to react to the player in the search range.
        /// </summary>
        public float FoundReactionTime { get; set; }

        public float UnknownSingle_12 { get; set; }

        /// <summary>
        /// The size of this struct.
        /// </summary>
        public const uint Size = 0x54;

        public ScriptParameter() { }

        public override string ToString() => Name;
    }
}
