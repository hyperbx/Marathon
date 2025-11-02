using Marathon.Extensions;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

// Format names:        Prop Library
// Format references:   Sonicteam::Prop::Library
// Format designers:    Sonic Team
// Format researchers:  Knuxfan24, Hyper

namespace Marathon.Formats.Placement
{
    /// <summary>
    /// Support for *.prop files; used for defining actors.
    /// </summary>
    public class PropLibrary : FileBase
    {
        private const string _extension = ".prop"; // "PROP"

        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The actors in this library.
        /// </summary>
        public List<Actor> Actors { get; set; } = [];

        public override string Extension => _extension;

        public Actor this[int in_index]
        {
            get => Actors[in_index];
            set => Actors[in_index] = value;
        }

        public Actor this[string in_name]
        {
            get => Actors.Find(x => x.Name == in_name);
        }

        public PropLibrary() { }

        public PropLibrary(string in_path) : base(in_path) { }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            Endianness = reader.Endianness;

            // Always null.
            reader.JumpAhead(12);

            Name = reader.ReadStringFixedLength(0x20);

            var actorCount = reader.Read<uint>();
            var actorTableOffset = reader.Read<uint>();

            for (int i = 0; i < actorCount; i++)
            {
                var actor = new Actor();

                var actorNameOffset = reader.Read<uint>();
                var parameterCount = reader.Read<uint>();
                var parameterOffset = reader.Read<uint>();
                var hasPersistent = reader.Read<uint>() > 0;
                var persistentOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + actorNameOffset, () => actor.Name = reader.ReadStringNullTerminated());

                var pos = reader.Position;

                for (int j = 0; j < parameterCount; j++)
                {
                    reader.JumpTo(BINAHeader.Size + parameterOffset + (j * 0x18));

                    var param = new ActorParameter()
                    {
                        Name = reader.ReadStringFixedLength(0x10),
                        Type = reader.Read<StageSetDataType>()
                    };

                    actor.Parameters.Add(param);
                }

                if (hasPersistent)
                {
                    reader.JumpTo(BINAHeader.Size + persistentOffset);

                    actor.Persistent = reader.ReadStringFixedLength(0x10);
                }

                reader.JumpTo(pos);

                Actors.Add(actor);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream, Endianness);

            writer.WriteNullBytes(12);
            writer.WriteStringFixedLength(Name.Truncate(0x20), 0x20);
            writer.Write(Actors.Count);
            writer.Reserve<uint>("ActorTableOffset");
            writer.WriteReserved("ActorTableOffset", (uint)writer.Position - BINAHeader.Size);

            for (int i = 0; i < Actors.Count; i++)
            {
                var actor = Actors[i];

                writer.WriteStringOffset(actor.Name);
                writer.Write(actor.Parameters.Count);

                if (actor.Parameters.Count == 0)
                {
                    writer.Write(0);
                }
                else
                {
                    writer.Reserve<uint>($"Actor{i}ParameterOffset");
                }

                if (string.IsNullOrEmpty(actor.Persistent))
                {
                    writer.WriteNullBytes(8);
                }
                else
                {
                    writer.Write(1);
                    writer.Reserve<uint>($"Actor{i}PersistentOffset");
                }
            }

            for (int i = 0; i < Actors.Count; i++)
            {
                if (Actors[i].Parameters.Count == 0)
                    continue;

                writer.WriteReserved($"Actor{i}ParameterOffset", (uint)writer.Position - BINAHeader.Size);

                for (int j = 0; j < Actors[i].Parameters.Count; j++)
                {
                    writer.WriteStringFixedLength(Actors[i].Parameters[j].Name.Truncate(0x10), 0x10);
                    writer.Write((uint)Actors[i].Parameters[j].Type);
                    writer.Write(j);
                }
            }

            for (int i = 0; i < Actors.Count; i++)
            {
                var actor = Actors[i];

                if (string.IsNullOrEmpty(actor.Persistent))
                    continue;

                writer.WriteReserved($"Actor{i}PersistentOffset", (uint)writer.Position - BINAHeader.Size);
                writer.WriteStringFixedLength(actor.Persistent.Truncate(0x10), 0x10);
            }

            writer.FinishWrite();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class Actor
    {
        /// <summary>
        /// The name of this actor.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The parameters for this actor.
        /// </summary>
        public List<ActorParameter> Parameters { get; set; } = [];

        /// <summary>
        /// The name of a variable that persists after dying in a stage.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Persistent { get; set; }

        public Actor() { }

        public Actor(string in_name, List<ActorParameter> in_parameters = null, string in_persistent = null)
        {
            Name = in_name;
            Parameters = in_parameters ?? [];
            Persistent = in_persistent;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ActorParameter
    {
        /// <summary>
        /// The name of this parameter.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The data type for this parameter.
        /// </summary>
        public StageSetDataType Type { get; set; }

        public ActorParameter() { }

        public ActorParameter(string in_name)
        {
            Name = in_name;
        }

        public ActorParameter(string in_name, StageSetDataType in_type)
        {
            Name = in_name;
            Type = in_type;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
