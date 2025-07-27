using Marathon.Formats.Parameter;
using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using System.Collections;
using System.Collections.Generic;
using System.IO;

// Format names:        Text Font Picture
// Format references:   Sonicteam::TextFontPicture
// Format designers:    Sonic Team
// Format researchers:  Hyper

namespace Marathon.Formats.Text
{
    /// <summary>
    /// Support for *.pft files; used for defining placeholder images for the <see cref="TextBook"/> format.
    /// </summary>
    public class TextFontPicture : FileBase, IList<TextFontPictureCrop>
    {
        private const string _extension = ".pft"; // "Picture FonT"
        private const string _signature = "FNTP"; // "FoNT Picture"

        public TextFontPicture() { }

        public TextFontPicture(string in_path) : base(in_path) { }

        /// <summary>
        /// The path to the texture these crops pertain to.
        /// </summary>
        public string Texture;

        /// <summary>
        /// The crops for the specified texture.
        /// </summary>
        public List<TextFontPictureCrop> Crops { get; set; } = [];

        public int Count => Crops.Count;

        public bool IsReadOnly => false;

        public TextFontPictureCrop this[int in_index]
        {
            get => Crops[in_index];
            set => Crops[in_index] = value;
        }

        public TextFontPictureCrop this[string in_name]
        {
            get => Crops.Find((x) => x.Name == in_name);
        }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            Endianness = reader.Endianness;

            reader.CheckSignature(_signature);

            var textureNameOffset = reader.Read<uint>();
            var cropCount = reader.Read<uint>();
            var cropTableOffset = reader.Read<uint>();

            reader.ReadAtOffset(BINAHeader.Size + textureNameOffset,
                () => Texture = reader.ReadStringNullTerminated());

            reader.JumpTo(BINAHeader.Size + cropTableOffset);

            for (uint i = 0; i < cropCount; i++)
            {
                var nameOffset = reader.Read<uint>();

                var crop = new TextFontPictureCrop()
                {
                    X = reader.Read<ushort>(),
                    Y = reader.Read<ushort>(),
                    Width = reader.Read<ushort>(),
                    Height = reader.Read<ushort>()
                };

                reader.ReadAtOffset(BINAHeader.Size + nameOffset,
                    () => crop.Name = reader.ReadStringNullTerminated());

                Crops.Add(crop);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream, Endianness);

            writer.WriteSignature(_signature);
            writer.WriteStringOffset(Texture);
            writer.Write(Crops.Count);
            writer.Reserve<uint>("CropTableOffset");
            writer.WriteReserved("CropTableOffset", (uint)(writer.Position - BINAHeader.Size));

            for (int i = 0; i < Crops.Count; i++)
            {
                writer.WriteStringOffset(Crops[i].Name);
                writer.Write(Crops[i].X);
                writer.Write(Crops[i].Y);
                writer.Write(Crops[i].Width);
                writer.Write(Crops[i].Height);
            }

            writer.FinishWrite();
        }

        public int IndexOf(TextFontPictureCrop in_item)
        {
            return Crops.IndexOf(in_item);
        }

        public void Insert(int in_index, TextFontPictureCrop in_item)
        {
            Crops.Insert(in_index, in_item);
        }

        public void RemoveAt(int in_index)
        {
            Crops.RemoveAt(in_index);
        }

        public void Add(TextFontPictureCrop in_item)
        {
            Crops.Add(in_item);
        }

        public void Clear()
        {
            Crops.Clear();
        }

        public bool Contains(TextFontPictureCrop in_item)
        {
            return Crops.Contains(in_item);
        }

        public void CopyTo(TextFontPictureCrop[] in_array, int in_arrayIndex)
        {
            Crops.CopyTo(in_array, in_arrayIndex);
        }

        public bool Remove(TextFontPictureCrop in_item)
        {
            return Crops.Remove(in_item);
        }

        public IEnumerator<TextFontPictureCrop> GetEnumerator()
        {
            return Crops.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return Texture;
        }
    }

    public class TextFontPictureCrop
    {
        /// <summary>
        /// The name of this crop.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The X position of the top-left corner of this crop.
        /// </summary>
        public ushort X { get; set; }

        /// <summary>
        /// The Y position of the top-left corner of this crop.
        /// </summary>
        public ushort Y { get; set; }

        /// <summary>
        /// The width of this crop.
        /// </summary>
        public ushort Width { get; set; }

        /// <summary>
        /// The height of this crop.
        /// </summary>
        public ushort Height { get; set; }

        public TextFontPictureCrop() { }

        public TextFontPictureCrop(string in_name, ushort in_x, ushort in_y, ushort in_width, ushort in_height)
        {
            Name = in_name;
            X = in_x;
            Y = in_y;
            Width = in_width;
            Height = in_height;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}