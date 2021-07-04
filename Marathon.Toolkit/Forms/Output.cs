// Marathon is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2021 HyperBE32
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
using System.Windows.Forms;
using Marathon.Components;
using Marathon.Helpers;

namespace Marathon.Toolkit.Forms
{
    public partial class Output : MarathonDockContent
    {
        private readonly string _DefaultOutput = "Marathon Toolkit" + $"{Program.GetExtendedInformation()} ({Program.Architecture()})\n\n";

        public Output()
        {
            InitializeComponent();

            // Set last word wrap setting.
            ButtonDark_ToggleWordWrap.Checked =
            MarathonRichTextBox_Console.WordWrapToContentPadding =
            Marathon.Properties.Settings.Default.Output_ToggleWordWrap;

            InitialiseOutput();
        }

        /// <summary>
        /// Resets the output window and initialises it.
        /// </summary>
        private void InitialiseOutput()
        {
            // Reset output.
            MarathonRichTextBox_Console.Text = string.Empty;

            // Set output info.
            MarathonRichTextBox_Console.Text += _DefaultOutput;

            // Set console output to the RichTextBox control.
            Console.SetOut(new ConsoleWriter(MarathonRichTextBox_Console));
        }

        /// <summary>
        /// Perform tasks upon clicking a ListViewItem.
        /// </summary>
        private void RichTextBoxLocked_Console_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                {
                    ContextMenuStripDark menu = new ContextMenuStripDark();

                    menu.Items.Add
                    (
                        new ToolStripMenuItem
                        (
                            "Copy to Clipboard",
                            Cache.LoadBitmapResource(nameof(Marathon.Properties.Resources.Placeholder)),
                            
                            delegate
                            {
                                Clipboard.SetText(MarathonRichTextBox_Console.Text);
                            }
                        )
                    );

                    menu.Show(Cursor.Position);

                    break;
                }
            }
        }

        /// <summary>
        /// Clear the output window.
        /// </summary>
        private void ButtonFlat_ClearAll_Click(object sender, EventArgs e)
            => InitialiseOutput();

        /// <summary>
        /// Toggles word wrap for the output window.
        /// </summary>
        private void ButtonFlat_ToggleWordWrap_Click(object sender, EventArgs e)
        {
            // Set properties and settings to the opposite of the current.
            ButtonDark_ToggleWordWrap.Checked =
            MarathonRichTextBox_Console.WordWrapToContentPadding =
            Marathon.Properties.Settings.Default.Output_ToggleWordWrap =
            !MarathonRichTextBox_Console.WordWrapToContentPadding;
        }

        /// <summary>
        /// Keeps the output box scrolled to the end always.
        /// </summary>
        private void MarathonRichTextBox_Console_TextChanged(object sender, EventArgs e)
        {
            // Move caret to the end.
            MarathonRichTextBox_Console.SelectionStart = MarathonRichTextBox_Console.Text.Length;

            // Scroll to the end.
            MarathonRichTextBox_Console.ScrollToCaret();
        }
    }
}
