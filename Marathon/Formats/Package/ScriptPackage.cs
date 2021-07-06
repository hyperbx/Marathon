using System.Collections.Generic;
using System.IO;
using Marathon.IO;

namespace Marathon.Formats.Package
{
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

        public float UnknownSingle1 { get; set; }

        /// <summary>
        /// The range the player must be in for the enemies to find them.
        /// </summary>
        public float FoundRangeIn { get; set; }

        /// <summary>
        /// TODO: needs more research.
        /// </summary>
        public float FoundRangeOut { get; set; }

        public float UnknownSingle4 { get; set; }
        public float UnknownSingle5 { get; set; }
        public float UnknownSingle6 { get; set; }
        public float UnknownSingle7 { get; set; }
        public float UnknownSingle8 { get; set; }

        /// <summary>
        /// The speed the enemies rotate at when searching for the player.
        /// </summary>
        public float RotationSpeed { get; set; }

        /// <summary>
        /// Something to do with rotation speed.
        /// </summary>
        public float UnknownSingle10 { get; set; }

        /// <summary>
        /// Something to do with rotation speed.
        /// </summary>
        public float UnknownSingle11 { get; set; }

        public float UnknownSingle12 { get; set; }
        public float UnknownSingle13 { get; set; }
        public float UnknownSingle14 { get; set; }
        public float UnknownSingle15 { get; set; }

        /// <summary>
        /// Total time taken for the enemies to react to the player in the search range.
        /// </summary>
        public float FoundReactionTime { get; set; }

        public float UnknownSingle17 { get; set; }

        /// <summary>
        /// The size of this struct.
        /// </summary>
        public const uint Size = 0x54;

        public override string ToString() => Name;
    }

    public class ScriptPackage : FileBase
    {
        public ScriptPackage() { }

        public ScriptPackage(string file)
        {
            switch (Path.GetExtension(file))
            {
                case ".json":
                    Parameters = JsonDeserialise<List<ScriptParameter>>(file);
                    break;

                default:
                    Load(file);
                    break;
            }
        }

        public const string Extension = ".bin";

        public List<ScriptParameter> Parameters = new();

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

                scriptParameter.UnknownSingle1    = reader.ReadSingle();
                scriptParameter.FoundRangeIn      = reader.ReadSingle();
                scriptParameter.FoundRangeOut     = reader.ReadSingle();
                scriptParameter.UnknownSingle4    = reader.ReadSingle();
                scriptParameter.UnknownSingle5    = reader.ReadSingle();
                scriptParameter.UnknownSingle6    = reader.ReadSingle();
                scriptParameter.UnknownSingle7    = reader.ReadSingle();
                scriptParameter.UnknownSingle8    = reader.ReadSingle();
                scriptParameter.RotationSpeed     = reader.ReadSingle();
                scriptParameter.UnknownSingle10   = reader.ReadSingle();
                scriptParameter.UnknownSingle11   = reader.ReadSingle();
                scriptParameter.UnknownSingle12   = reader.ReadSingle();
                scriptParameter.UnknownSingle13   = reader.ReadSingle();
                scriptParameter.UnknownSingle14   = reader.ReadSingle();
                scriptParameter.UnknownSingle15   = reader.ReadSingle();
                scriptParameter.FoundReactionTime = reader.ReadSingle();
                scriptParameter.UnknownSingle17   = reader.ReadSingle();

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

                writer.Write(parameter.UnknownSingle1);
                writer.Write(parameter.FoundRangeIn);
                writer.Write(parameter.FoundRangeOut);
                writer.Write(parameter.UnknownSingle4);
                writer.Write(parameter.UnknownSingle5);
                writer.Write(parameter.UnknownSingle6);
                writer.Write(parameter.UnknownSingle7);
                writer.Write(parameter.UnknownSingle8);
                writer.Write(parameter.RotationSpeed);
                writer.Write(parameter.UnknownSingle10);
                writer.Write(parameter.UnknownSingle11);
                writer.Write(parameter.UnknownSingle12);
                writer.Write(parameter.UnknownSingle13);
                writer.Write(parameter.UnknownSingle14);
                writer.Write(parameter.UnknownSingle15);
                writer.Write(parameter.FoundReactionTime);
                writer.Write(parameter.UnknownSingle17);
            }

            writer.WriteNulls(4);
            writer.FinishWrite();
        }
    }
}
