using System;
using System.Media;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

// Sonic '06 Toolkit is licensed under the MIT License:
/*
 * MIT License

 * Copyright (c) 2020 Gabriel (HyperPolygon64)

 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:

 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

namespace Toolkit.Text
{
    internal partial class Chooser : Form
    {
        public static string Accept = string.Empty;
        public static int TextHeight = 0;

        public Chooser()
        {
            InitializeComponent();
        }

        public Chooser(string text, string caption, string choice1, string choice2)
        {
            InitializeComponent();

            Text = caption;
            rtb_Message.Text = text;

            if (rtb_Message.Text.Length <= 65) {
                rtb_Message.Top += 6;
                rtb_Message.Left += 5;
                Width += rtb_Message.Width - 30;
            }
            else {
                rtb_Message.Top += 1;
                Width += rtb_Message.Width - 30;
            }

            btn_Choice1.Text = choice1;
            btn_Choice2.Text = choice2;
            pic_Icon.BackgroundImage = Extract("shell32.dll", 154, true).ToBitmap();
            TopMost = false;
            SystemSounds.Question.Play();
        }

        public static Icon Extract(string file, int number, bool largeIcon) {
            IntPtr large;
            IntPtr small;
            ExtractIconEx(file, number, out large, out small, 1);
            try { return Icon.FromHandle(largeIcon ? large : small); }
            catch { return null; }

        }
        [DllImport("Shell32.dll", EntryPoint = "ExtractIconExW", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]

        private static extern int ExtractIconEx(string sFile, int iIndex, out IntPtr piLargeVersion, out IntPtr piSmallVersion, int amountIcons);

        public static class UserChoice
        {
            public static string Show(string text, string caption, string choice1, string choice2) {
                using (var openMessenger = new Chooser(text, caption, choice1, choice2)) {
                    openMessenger.ShowDialog();
                }

                return Accept;
            }
        }

        private void Btn_OK_Click(object sender, EventArgs e) { Accept = btn_Choice2.Text; Close(); }

        private void Btn_Yes_Click(object sender, EventArgs e) { Accept = btn_Choice1.Text; Close(); }

        private void Btn_Abort_Click(object sender, EventArgs e) { Accept = btn_Abort.Text; Close(); }

        private void Rtb_Message_ContentsResized(object sender, ContentsResizedEventArgs e) {
            var getMessageBoundaries = (RichTextBox)sender;
            getMessageBoundaries.Height = e.NewRectangle.Height;
            TextHeight = e.NewRectangle.Height;
        }

        private void UnifyMessages_Load(object sender, EventArgs e) {
            if (rtb_Message.Text.Length <= 65)
                Height = TextHeight + 140;
            else
                Height = TextHeight + 135;
        }
    }
}
