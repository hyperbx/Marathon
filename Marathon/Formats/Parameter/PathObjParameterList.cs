using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using System.Collections.Generic;
using System.IO;

// Format research attribution: Knuxfan24, Hyper

namespace Marathon.Formats.Parameter
{
    /// <summary>
    /// Support for PathObj.bin files; used for defining path-based objects for the common_path_obj actor.
    /// </summary>
    public class PathObjParameterList : FileBase
    {
        private const string _extension = ".bin"; // "BINary"

        public PathObjParameterList() { }

        public PathObjParameterList(string in_path) : base(in_path) { }

        public List<PathObjParameter> Parameters { get; set; } = [];

        public PathObjParameter this[string in_name]
        {
            get => Parameters.Find((x) => x.Name == in_name);
        }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            var stringPoolOffset = reader.Read<uint>();

            reader.JumpBehind(4);

            while (reader.Position < (stringPoolOffset - 4) + BINAHeader.Size)
            {
                var param = new PathObjParameter();
                
                var nameOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + nameOffset,
                    () => param.Name = reader.ReadStringNullTerminated());

                var modelOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + modelOffset,
                    () => param.Model = reader.ReadStringNullTerminated());

                var animationOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + animationOffset,
                    () => param.Animation = reader.ReadStringNullTerminated());

                var textOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + textOffset,
                    () => param.Text = reader.ReadStringNullTerminated());

                var materialAnimationOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + materialAnimationOffset,
                    () => param.MaterialAnimation = reader.ReadStringNullTerminated());

                Parameters.Add(param);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream);

            for (int i = 0; i < Parameters.Count; i++)
            {
                var param = Parameters[i];

                writer.CreateStringField($"Name{i}", param.Name);
                writer.CreateStringField($"Model{i}", param.Model);
                writer.CreateStringField($"Animation{i}", param.Animation);
                writer.CreateStringField($"Text{i}", param.Text);
                writer.CreateStringField($"MaterialAnimation{i}", param.MaterialAnimation);
            }

            writer.Write(0);
            writer.FinishWrite();
        }
    }

    public class PathObjParameter
    {
        /// <summary>
        /// The name of this object.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The model this object should use.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// The animation this object should use.
        /// </summary>
        public string Animation { get; set; }

        /// <summary>
        /// TODO: unknown, source file?
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The material animation this object should use.
        /// </summary>
        public string MaterialAnimation { get; set; }

        public PathObjParameter() { }

        public PathObjParameter(string in_name, string in_model, string in_animation, string in_text, string in_materialAnimation)
        {
            Name = in_name;
            Model = in_model;
            Animation = in_animation;
            Text = in_text;
            MaterialAnimation = in_materialAnimation;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
