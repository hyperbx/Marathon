using Marathon.IO;
using Marathon.IO.Extensions;
using Marathon.IO.Types.BINA;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

// Format names:        Text Book
// Format references:   Sonicteam::TextBook
// Format designers:    Sonic Team
// Format researchers:  GerbilSoft, Knuxfan24, Hyper

namespace Marathon.Formats.Text
{
    /// <summary>
    /// Support for *.mst files; used for storing wide text with friendly names and variables.
    /// </summary>
    public class TextBook : FileBase, IList<TextBookMessage>
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
        /// The messages in this text book.
        /// </summary>
        public List<TextBookMessage> Messages { get; set; } = [];

        public int Count => Messages.Count;

        public bool IsReadOnly => false;

        public TextBookMessage this[int in_index]
        {
            get => Messages[in_index];
            set => Messages[in_index] = value;
        }

        public TextBookMessage this[string in_name]
        {
            get => Messages.Find((x) => x.Name == in_name);
        }

        public override void Read(Stream in_stream)
        {
            var reader = new BINAReader(in_stream);

            Endianness = reader.Endianness;

            reader.CheckSignature(_signature);

            var nameOffset = reader.Read<uint>();
            var messageCount = reader.Read<uint>();

            reader.ReadAtOffset(BINAHeader.Size + nameOffset,
                () => Name = reader.ReadStringNullTerminated());

            for (int i = 0; i < messageCount; i++)
            {
                var message = new TextBookMessage();

                var msgNameOffset = reader.Read<uint>();
                var msgTextOffset = reader.Read<uint>();
                var msgVariablesOffset = reader.Read<uint>();

                reader.ReadAtOffset(BINAHeader.Size + msgNameOffset,
                    () => message.Name = reader.ReadStringNullTerminated());

                reader.ReadAtOffset(BINAHeader.Size + msgTextOffset,
                    () => message.Text = reader.ReadStringNullTerminated(Encoding.BigEndianUnicode));

                if (msgVariablesOffset != 0)
                {
                    reader.ReadAtOffset(BINAHeader.Size + msgVariablesOffset,
                        () => message.Variables = reader.ReadStringNullTerminated().Split(','));
                }

                Messages.Add(message);
            }
        }

        public override void Write(Stream in_stream)
        {
            var writer = new BINAWriter(in_stream, Endianness);

            writer.WriteSignature(_signature);
            writer.WriteStringOffset(Name);
            writer.Write(Messages.Count);

            for (int i = 0; i < Messages.Count; i++)
            {
                writer.WriteStringOffset(Messages[i].Name);
                writer.Reserve($"MessageTextOffset{i}");

                if (Messages[i].Variables == null)
                {
                    writer.Write(0);
                }
                else
                {
                    writer.WriteStringOffset(string.Join(',', Messages[i].Variables));
                }
            }

            for (int i = 0; i < Messages.Count; i++)
            {
                writer.WriteReserved($"MessageTextOffset{i}", (uint)(writer.Position - BINAHeader.Size));
                writer.WriteStringNullTerminated(Encoding.BigEndianUnicode, Messages[i].Text);
            }

            writer.FinishWrite();
        }

        public int IndexOf(TextBookMessage in_item)
        {
            return Messages.IndexOf(in_item);
        }

        public void Insert(int in_index, TextBookMessage in_item)
        {
            Messages.Insert(in_index, in_item);
        }

        public void RemoveAt(int in_index)
        {
            Messages.RemoveAt(in_index);
        }

        public void Add(TextBookMessage in_item)
        {
            Messages.Add(in_item);
        }

        public void Clear()
        {
            Messages.Clear();
        }

        public bool Contains(TextBookMessage in_item)
        {
            return Messages.Contains(in_item);
        }

        public void CopyTo(TextBookMessage[] in_array, int in_arrayIndex)
        {
            Messages.CopyTo(in_array, in_arrayIndex);
        }

        public bool Remove(TextBookMessage in_item)
        {
            return Messages.Remove(in_item);
        }

        public IEnumerator<TextBookMessage> GetEnumerator()
        {
            return Messages.GetEnumerator();
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

    public class TextBookMessage
    {
        /// <summary>
        /// The name of this message.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The text for this message.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The replacement variables for the placeholders in this message.
        /// </summary>
        public string[] Variables { get; set; }

        public TextBookMessage() { }

        public TextBookMessage(string in_name, string in_text, string[] in_variables = null)
        {
            Name = in_name;
            Text = in_text;
            Variables = in_variables;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
