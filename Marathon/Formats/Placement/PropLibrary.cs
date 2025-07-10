using Marathon.Extensions;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using System.Collections.Generic;
using System.IO;

// Format research attribution: Knuxfan24, Hyper

namespace Marathon.Formats.Placement
{
    /// <summary>
    /// Support for *.prop files; used for defining actors.
    /// </summary>
    public class PropLibrary : FileBase
    {
        public PropLibrary() { }

        public PropLibrary(string in_path) : base(in_path) { }

        /// <summary>
        /// The name of this library.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The actors in this library.
        /// </summary>
        public List<Actor> Actors { get; set; } = [];

        public Actor this[string in_name]
        {
            get => Actors.Find((x) => x.Name == in_name);
        }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

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
                var unkField1 = reader.Read<uint>();
                var unkField2 = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + actorNameOffset, () => actor.Name = reader.ReadStringNullTerminated());

                var pos = reader.Position;

                for (int j = 0; j < parameterCount; j++)
                {
                    reader.JumpTo(BINAHeader.Size + parameterOffset + (j * 0x18));

                    var param = new ActorParameter()
                    {
                        Name = reader.ReadStringFixedLength(0x10),
                        Type = (StageSetDataType)reader.Read<uint>()
                    };

                    actor.Parameters.Add(param);
                }

                reader.JumpTo(pos);

                Actors.Add(actor);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream);

            writer.WriteNullBytes(12);
            writer.WriteStringFixedLength(Name.Truncate(0x20), 0x20);
            writer.Write(Actors.Count);
            writer.CreateNamedField("ActorTableOffset");
            writer.WriteNamedField("ActorTableOffset", (uint)writer.Position - BINAHeader.Size);

            for (int i = 0; i < Actors.Count; i++)
            {
                writer.CreateStringField($"Actor{i}Name", Actors[i].Name);
                writer.Write(Actors[i].Parameters.Count);

                if (Actors[i].Parameters.Count == 0)
                {
                    writer.Write(0);
                }
                else
                {
                    writer.CreateNamedField($"Actor{i}ParameterOffset");
                }

                writer.WriteNullBytes(8);
            }

            for (int i = 0; i < Actors.Count; i++)
            {
                if (Actors[i].Parameters.Count == 0)
                    continue;

                writer.WriteNamedField($"Actor{i}ParameterOffset", (uint)writer.Position - BINAHeader.Size);

                for (int j = 0; j < Actors[i].Parameters.Count; j++)
                {
                    writer.WriteStringFixedLength(Actors[i].Parameters[j].Name.Truncate(0x10), 0x10);
                    writer.Write((uint)Actors[i].Parameters[j].Type);
                    writer.Write(j);
                }
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
        /// The parameters this actor accepts.
        /// </summary>
        public List<ActorParameter> Parameters { get; set; } = [];

        public Actor() { }

        public Actor(string in_name, List<ActorParameter> in_parameters)
        {
            Name = in_name;
            Parameters = in_parameters;
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
