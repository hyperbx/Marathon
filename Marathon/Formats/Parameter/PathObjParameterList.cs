using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Format names:        Path Obj Parameter List
// Format references:   Sonicteam::PathObjParameter
// Format designers:    Sonic Team
// Format researchers:  Knuxfan24, Hyper

namespace Marathon.Formats.Parameter
{
    /// <summary>
    /// Support for PathObj.bin files; used for defining path-based objects for the common_path_obj actor.
    /// </summary>
    public class PathObjParameterList : FileBase, IList<PathObjParameter>
    {
        private const string _extension = ".bin"; // "BINary"

        public PathObjParameterList() { }

        public PathObjParameterList(string in_path) : base(in_path) { }

        public List<PathObjParameter> Parameters { get; set; } = [];

        public int Count => Parameters.Count;

        public bool IsReadOnly => false;

        public PathObjParameter this[int in_index]
        {
            get => Parameters[in_index];
            set => Parameters[in_index] = value;
        }

        public PathObjParameter this[string in_name]
        {
            get => Parameters.Find((x) => x.Name == in_name);
        }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            Endianness = reader.Endianness;

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
            var writer = new BINAWriter(in_stream, Endianness);

            for (int i = 0; i < Parameters.Count; i++)
            {
                var param = Parameters[i];

                writer.WriteStringOffset(param.Name);
                writer.WriteStringOffset(param.Model);
                writer.WriteStringOffset(param.Animation);
                writer.WriteStringOffset(param.Text);
                writer.WriteStringOffset(param.MaterialAnimation);
            }

            writer.Write(0);
            writer.FinishWrite();
        }

        public int IndexOf(PathObjParameter in_item)
        {
            return Parameters.IndexOf(in_item);
        }

        public void Insert(int in_index, PathObjParameter in_item)
        {
            Parameters.Insert(in_index, in_item);
        }

        public void RemoveAt(int in_index)
        {
            Parameters.RemoveAt(in_index);
        }

        public void Add(PathObjParameter in_item)
        {
            Parameters.Add(in_item);
        }

        public void Clear()
        {
            Parameters.Clear();
        }

        public bool Contains(PathObjParameter in_item)
        {
            return Parameters.Contains(in_item);
        }

        public void CopyTo(PathObjParameter[] in_array, int in_arrayIndex)
        {
            Parameters.CopyTo(in_array, in_arrayIndex);
        }

        public bool Remove(PathObjParameter in_item)
        {
            return Parameters.Remove(in_item);
        }

        public IEnumerator<PathObjParameter> GetEnumerator()
        {
            return Parameters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
