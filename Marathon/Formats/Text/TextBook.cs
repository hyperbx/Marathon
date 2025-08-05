using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

// Format names:        Text Book
// Format references:   Sonicteam::TextBook, Sonicteam::TextCard
// Format designers:    Sonic Team
// Format researchers:  GerbilSoft, Knuxfan24, Hyper

namespace Marathon.Formats.Text
{
    /// <summary>
    /// Support for *.mst files; used for storing wide text with friendly names and variables.
    /// </summary>
    public class TextBook : FileBase, IList<TextCard>
    {
        private const string _extension = ".mst"; // "MeSsage Table" (speculatory)
        private const string _signature = "WTXT"; // "Wide TeXT" (referring to UTF-16)

        public TextBook() { }

        public TextBook(string in_path) : base(in_path) { }

        /// <summary>
        /// The name of this text book.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The cards in this text book.
        /// </summary>
        public List<TextCard> Cards { get; set; } = [];

        public int Count => Cards.Count;

        public bool IsReadOnly => false;

        public TextCard this[int in_index]
        {
            get => Cards[in_index];
            set => Cards[in_index] = value;
        }

        public TextCard this[string in_name]
        {
            get => Cards.Find((x) => x.Name == in_name);
        }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            Endianness = reader.Endianness;

            reader.CheckSignature(_signature);

            var nameOffset = reader.Read<uint>();
            var cardCount = reader.Read<uint>();

            reader.ReadAtOffset(BINAHeader.Size + nameOffset,
                () => Name = reader.ReadStringNullTerminated());

            for (int i = 0; i < cardCount; i++)
            {
                var card = new TextCard();

                var cardNameOffset = reader.Read<uint>();
                var cardTextOffset = reader.Read<uint>();
                var cardVariablesOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + cardNameOffset,
                    () => card.Name = reader.ReadStringNullTerminated());

                reader.ReadAtOffset(BINAHeader.Size + cardTextOffset,
                    () => card.Text = reader.ReadStringNullTerminated(Encoding.BigEndianUnicode));

                if (cardVariablesOffset != 0)
                {
                    reader.ReadAtOffset(BINAHeader.Size + cardVariablesOffset,
                        () => card.Variables = ParseVariables(reader.ReadStringNullTerminated()));
                }

                Cards.Add(card);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream, Endianness);

            writer.WriteSignature(_signature);
            writer.WriteStringOffset(Name);
            writer.Write(Cards.Count);

            for (int i = 0; i < Cards.Count; i++)
            {
                writer.WriteStringOffset(Cards[i].Name);
                writer.Reserve($"CardTextOffset{i}");

                if (Cards[i].Variables == null)
                {
                    writer.Write(0);
                }
                else
                {
                    writer.WriteStringOffset(string.Join(',', Cards[i].Variables));
                }
            }

            for (int i = 0; i < Cards.Count; i++)
            {
                writer.WriteReserved($"CardTextOffset{i}", (uint)(writer.Position - BINAHeader.Size));
                writer.WriteStringNullTerminated(Encoding.BigEndianUnicode, Cards[i].Text);
            }

            writer.FinishWrite();
        }

        public static List<string> ParseVariables(string in_variables)
        {
            var result = new List<string>();
            var start = 0;
            var depth = 0;

            for (int i = 0; i < in_variables.Length; i++)
            {
                var c = in_variables[i];

                if (c == '(')
                {
                    depth++;
                }
                else if (c == ')')
                {
                    depth--;
                }
                else if (c == ',' && depth == 0)
                {
                    result.Add(in_variables.Substring(start, i - start));
                    start = i + 1;
                }
            }

            result.Add(in_variables.Substring(start));

            return result;
        }

        public int IndexOf(TextCard in_item)
        {
            return Cards.IndexOf(in_item);
        }

        public void Insert(int in_index, TextCard in_item)
        {
            Cards.Insert(in_index, in_item);
        }

        public void RemoveAt(int in_index)
        {
            Cards.RemoveAt(in_index);
        }

        public void Add(TextCard in_item)
        {
            Cards.Add(in_item);
        }

        public void Clear()
        {
            Cards.Clear();
        }

        public bool Contains(TextCard in_item)
        {
            return Cards.Contains(in_item);
        }

        public void CopyTo(TextCard[] in_array, int in_arrayIndex)
        {
            Cards.CopyTo(in_array, in_arrayIndex);
        }

        public bool Remove(TextCard in_item)
        {
            return Cards.Remove(in_item);
        }

        public IEnumerator<TextCard> GetEnumerator()
        {
            return Cards.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class TextCard
    {
        /// <summary>
        /// The name of this card.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The text for this card.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The replacement variables for the placeholders in this card.
        /// </summary>
        public List<string> Variables { get; set; }

        public TextCard() { }

        public TextCard(string in_name, string in_text, List<string> in_variables = null)
        {
            Name = in_name;
            Text = in_text;
            Variables = in_variables;
        }

        public List<(string Type, string Value)> MapVariables()
        {
            var result = new List<(string Type, string Value)>();

            if (Variables == null)
                return result;

            foreach (var variable in Variables)
            {
                var open = variable.IndexOf('(');
                var close = variable.IndexOf(')');

                if (open > -1 && close > -1 && close > open)
                {
                    var type = variable[..open];
                    var value = variable.Substring(open + 1, close - open - 1);

                    result.Add((type, value));
                }
                else
                {
                    result.Add((variable, null));
                }
            }

            return result;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
