// Marathon is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2020 HyperBE32
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Marathon.Helpers
{
    public class ConsoleWriter : TextWriter
    {
        private Control Writer;

        private StringBuilder Content = new StringBuilder();

        public ConsoleWriter(ListBox _ListBox)
            => Writer = _ListBox;

        public ConsoleWriter(TextBox _TextBox)
            => Writer = _TextBox;

        public ConsoleWriter(RichTextBox _RichTextBox)
            => Writer = _RichTextBox;

        public override Encoding Encoding
            => Encoding.UTF8;

        public static void Write(object value, bool timestamp = false, string source = "")
        {
            Console.Write
            (
                (timestamp ? $"[{DateTime.Now:hh:mm:ss tt}] " : string.Empty) +
                (string.IsNullOrEmpty(source) ? string.Empty : $"[{source}] ") + value
            );
        }

        public static void WriteLine(object value, bool timestamp = true, string source = "Marathon")
        {
            Console.Write
            (
                (timestamp ? $"[{DateTime.Now:hh:mm:ss tt}] " : string.Empty) +
                (string.IsNullOrEmpty(source) ? string.Empty : $"[{source}] ") + value + '\n'
            );
        }

        /// <summary>
        /// Writes a string to the control - used when calling Console.WriteLine().
        /// </summary>
        /// <param name="value">String to write.</param>
        public override void Write(string value)
            => InvokeWriter(value);

        /// <summary>
        /// Writes a character to the control - used when calling Console.Write().
        /// </summary>
        /// <param name="value">Character to write.</param>
        public override void Write(char value)
        {
            Content.Append(value);

            InvokeWriter(Content);

            Content = new StringBuilder();
        }

        /// <summary>
        /// Invokes the writer control to write the character or string.
        /// </summary>
        /// <param name="value">Character or string to write.</param>
        private void InvokeWriter(object value)
        {
            if (TypeHelper.IsObjectOfType(Writer, typeof(ListBox)))
            {
                var listBox = (ListBox)Writer;

                listBox.Items.Add(value);

                listBox.SelectedIndex = ((ListBox)Writer).Items.Count - 1;
                listBox.SelectedIndex = -1;
            }
            else
            {
                Writer.Text += value;
            }
        }
    }
}