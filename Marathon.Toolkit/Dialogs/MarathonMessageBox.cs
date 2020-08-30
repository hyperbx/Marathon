using System;
using System.Media;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

// Marathon is licensed under the MIT License:
/*
 * MIT License
 * 
 * Copyright (c) 2020 HyperPolygon64
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

namespace Marathon.Toolkit
{
    public partial class MarathonMessageBoxForm : Form
    {
        public static int TextHeight = 0;

        public MarathonMessageBoxForm(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            InitializeComponent();

            Text = caption;
            RichTextBox_Message.Text = text;

            if (RichTextBox_Message.Text.Length <= 65)
            {
                RichTextBox_Message.Top += 6;
                RichTextBox_Message.Left += 5;
                Width += RichTextBox_Message.Width - 30;
            }
            else
            {
                RichTextBox_Message.Top += 1;
                Width += RichTextBox_Message.Width - 30;
            }

            switch (buttons)
            {
                case MessageBoxButtons.YesNo:
                {
                    Button_Yes.Visible = true;
                    Button_OK.Text = "No";
                    Button_OK.BackColor = Color.Tomato;
                    break;
                }

                case MessageBoxButtons.YesNoCancel:
                {
                    Button_Yes.Visible = true;
                    btn_No.Visible = true;
                    Button_OK.Text = "Cancel";
                    Button_OK.BackColor = Color.Tomato;
                    break;
                }

                case MessageBoxButtons.OKCancel:
                {
                    Button_Yes.Visible = true;
                    Button_Yes.Text = "OK";
                    Button_OK.Text = "Cancel";
                    Button_Yes.BackColor = SystemColors.ControlLightLight;
                    Button_OK.BackColor = Color.Tomato;
                    break;
                }

                case MessageBoxButtons.AbortRetryIgnore:
                {
                    Button_Abort.Visible = true;
                    Button_Yes.Visible = true;
                    Button_Yes.Text = "Retry";
                    Button_OK.Text = "Ignore";
                    Button_OK.BackColor = Color.SkyBlue;
                    break;
                }

                case MessageBoxButtons.RetryCancel:
                {
                    Button_Yes.Visible = true;
                    Button_Yes.Text = "Retry";
                    Button_OK.Text = "Cancel";
                    Button_OK.BackColor = Color.Tomato;
                    break;
                }
            }

            switch (icon)
            {
                case MessageBoxIcon.Error:
                {
                    PictureBox_Icon.BackgroundImage = Properties.Resources.Error.ToBitmap();
                    TopMost = true;
                    SystemSounds.Hand.Play();
                    break;
                }

                case MessageBoxIcon.Information:
                {
                    PictureBox_Icon.BackgroundImage = Extract("shell32.dll", 277, true).ToBitmap();
                    TopMost = false;
                    SystemSounds.Asterisk.Play();
                    break;
                }

                case MessageBoxIcon.Question:
                {
                    PictureBox_Icon.BackgroundImage = Extract("shell32.dll", 154, true).ToBitmap();
                    TopMost = false;
                    SystemSounds.Question.Play();
                    break;
                }

                case MessageBoxIcon.Warning:
                {
                    PictureBox_Icon.BackgroundImage = Extract("shell32.dll", 237, true).ToBitmap();
                    TopMost = true;
                    SystemSounds.Asterisk.Play();
                    break;
                }
            }

            BackColor = PictureBox_Icon.BackColor = RichTextBox_Message.BackColor = Color.FromArgb(45, 45, 48);
            Panel_ButtonBackdrop.BackColor = Color.FromArgb(59, 59, 63);
            RichTextBox_Message.ForeColor = SystemColors.Control;
        }

        private Icon Extract(string file, int number, bool largeIcon)
        {
            ExtractIconEx(file, number, out IntPtr large, out IntPtr small, 1);

            try
            {
                return Icon.FromHandle(largeIcon ? large : small);
            }
            catch
            {
                return null;
            }
        }

        [DllImport("Shell32.dll", EntryPoint = "ExtractIconExW", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern int ExtractIconEx(string sFile, int iIndex, out IntPtr piLargeVersion, out IntPtr piSmallVersion, int amountIcons);

        private void Button_OK_Click(object sender, EventArgs e)
        {
            MessageReceiver.State = Button_OK.Text;
            Close();
        }

        private void Button_Yes_Click(object sender, EventArgs e)
        {
            MessageReceiver.State = Button_Yes.Text;
            Close();
        }

        private void Button_No_Click(object sender, EventArgs e)
        {
            MessageReceiver.State = btn_No.Text;
            Close();
        }

        private void Button_Abort_Click(object sender, EventArgs e)
        {
            MessageReceiver.State = Button_Abort.Text;
            Close();
        }

        private void RichTextBox_Message_ContentsResized(object sender, ContentsResizedEventArgs e) => ((RichTextBox)sender).Height = TextHeight = e.NewRectangle.Height;

        private void MarathonMessageBoxForm_Load(object sender, EventArgs e)
        {
            if (RichTextBox_Message.Text.Length <= 65)
                Height = TextHeight + 140;

            else
                Height = TextHeight + 135;
        }
    }

    public class MessageReceiver
    {
        public static string State;
    }

    public class MarathonMessageBox
    {
        public static DialogResult Show(string text)
        {
            using (MarathonMessageBoxForm messenger = new MarathonMessageBoxForm(text, "Marathon", MessageBoxButtons.OK, MessageBoxIcon.Information))
                messenger.ShowDialog();

            return DialogResult.OK;
        }

        public static DialogResult Show(string text, string caption)
        {
            using (MarathonMessageBoxForm messenger = new MarathonMessageBoxForm(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information))
                messenger.ShowDialog();

            return DialogResult.OK;
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            using (MarathonMessageBoxForm messenger = new MarathonMessageBoxForm(text, caption, buttons, MessageBoxIcon.Information))
                messenger.ShowDialog();

            return MessageReceiver.State switch
            {
                "OK" => DialogResult.OK,
                "Yes" => DialogResult.Yes,
                "No" => DialogResult.No,
                "Abort" => DialogResult.Abort,
                "Retry" => DialogResult.Retry,
                "Ignore" => DialogResult.Ignore,
                "Cancel" => DialogResult.Cancel,
                _ => DialogResult.OK
            };
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            using (MarathonMessageBoxForm messenger = new MarathonMessageBoxForm(text, caption, buttons, icon))
                messenger.ShowDialog();

            return MessageReceiver.State switch
            {
                "OK" => DialogResult.OK,
                "Yes" => DialogResult.Yes,
                "No" => DialogResult.No,
                "Abort" => DialogResult.Abort,
                "Retry" => DialogResult.Retry,
                "Ignore" => DialogResult.Ignore,
                "Cancel" => DialogResult.Cancel,
                _ => DialogResult.OK
            };
        }
    }
}
