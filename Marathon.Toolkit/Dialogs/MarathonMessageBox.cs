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

        public MarathonMessageBoxForm(string text,
                                      string caption = "Marathon",
                                      MessageBoxButtons buttons = MessageBoxButtons.OK,
                                      MessageBoxIcon icon = MessageBoxIcon.Information)
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
                case MessageBoxButtons.OK:
                {
                    Button_3.Text = "OK";
                    Button_3.Visible = true;
                    Button_3.Tag = MessageState.OK;

                    break;
                }

                case MessageBoxButtons.OKCancel:
                {
                    Button_2.Text = "OK";
                    Button_2.Visible = true;
                    Button_2.Tag = MessageState.OK;

                    Button_3.Text = "Cancel";
                    Button_3.Visible = true;
                    Button_3.Tag = MessageState.Cancel;

                    break;
                }

                case MessageBoxButtons.YesNo:
                {
                    Button_2.Text = "Yes";
                    Button_2.Visible = true;
                    Button_2.Tag = MessageState.Yes;

                    Button_3.Text = "No";
                    Button_3.Visible = true;
                    Button_3.Tag = MessageState.No;

                    break;
                }

                case MessageBoxButtons.YesNoCancel:
                {
                    Button_1.Text = "Yes";
                    Button_1.Visible = true;
                    Button_1.Tag = MessageState.Yes;

                    Button_2.Text = "No";
                    Button_2.Visible = true;
                    Button_2.Tag = MessageState.No;

                    Button_3.Text = "Cancel";
                    Button_3.Visible = true;
                    Button_3.Tag = MessageState.Cancel;

                    break;
                }

                case MessageBoxButtons.RetryCancel:
                {
                    Button_2.Text = "Retry";
                    Button_2.Visible = true;
                    Button_2.Tag = MessageState.Retry;

                    Button_3.Text = "Cancel";
                    Button_3.Visible = true;
                    Button_3.Tag = MessageState.Cancel;

                    break;
                }

                case MessageBoxButtons.AbortRetryIgnore:
                {
                    Button_1.Text = "Abort";
                    Button_1.Visible = true;
                    Button_1.Tag = MessageState.Abort;

                    Button_2.Text = "Retry";
                    Button_2.Visible = true;
                    Button_2.Tag = MessageState.Retry;

                    Button_3.Text = "Ignore";
                    Button_3.Visible = true;
                    Button_3.Tag = MessageState.Ignore;

                    break;
                }

                default:
                {
                    goto case MessageBoxButtons.OK;
                }
            }

            // Extracts an icon from a DLL.
            Icon Extract(string file, int number, bool largeIcon)
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

            switch (icon)
            {
                case MessageBoxIcon.Error:
                {
                    PictureBox_Icon.BackgroundImage = Properties.Resources.Error.ToBitmap();
                    SystemSounds.Hand.Play();
                    TopMost = true;

                    break;
                }

                case MessageBoxIcon.Information:
                {
                    PictureBox_Icon.BackgroundImage = Extract("shell32.dll", 277, true).ToBitmap();
                    SystemSounds.Asterisk.Play();
                    TopMost = false;

                    break;
                }

                case MessageBoxIcon.Question:
                {
                    PictureBox_Icon.BackgroundImage = Extract("shell32.dll", 154, true).ToBitmap();
                    SystemSounds.Question.Play();
                    TopMost = false;

                    break;
                }

                case MessageBoxIcon.Warning:
                {
                    PictureBox_Icon.BackgroundImage = Extract("shell32.dll", 237, true).ToBitmap();
                    SystemSounds.Asterisk.Play();
                    TopMost = true;

                    break;
                }

                default:
                {
                    goto case MessageBoxIcon.Information;
                }
            }
        }

        [DllImport("Shell32.dll", EntryPoint = "ExtractIconExW", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern int ExtractIconEx(string sFile, int iIndex, out IntPtr piLargeVersion, out IntPtr piSmallVersion, int amountIcons);

        /// <summary>
        /// Sends the message to the receiver upon clicking an option.
        /// </summary>
        private void Button_Click_Group(object sender, EventArgs e)
        {
            MessageReceiver.State = (MessageState)((Button)sender).Tag;

            Close();
        }

        /// <summary>
        /// Sets the new height of the RichTextBox control.
        /// </summary>
        private void RichTextBox_Message_ContentsResized(object sender, ContentsResizedEventArgs e) => RichTextBox_Message.Height = TextHeight = e.NewRectangle.Height;

        /// <summary>
        /// Resizes the form to fit the text upon loading.
        /// </summary>
        private void MarathonMessageBoxForm_Load(object sender, EventArgs e)
        {
            if (RichTextBox_Message.Text.Length <= 65)
                Height = TextHeight + 140;

            else
                Height = TextHeight + 135;
        }
    }

    /// <summary>
    /// Message states as enumerators.
    /// </summary>
    public enum MessageState
    {
        Yes,
        No,
        Cancel,
        Abort,
        Retry,
        Ignore,
        OK
    }

    /// <summary>
    /// A class that receives the message from the selected choice.
    /// </summary>
    public class MessageReceiver
    {
        public static MessageState State;
    }

    /// <summary>
    /// A custom message box class with the intent of mimicking the standard WINAPI message box with a dark theme.
    /// </summary>
    public class MarathonMessageBox
    {
        /// <summary>
        /// Displays the message box with the specified text.
        /// </summary>
        /// <param name="text">Text to display.</param>
        public static DialogResult Show(string text)
        {
            using (MarathonMessageBoxForm messenger = new MarathonMessageBoxForm(text))
                messenger.ShowDialog();

            return DialogResult.OK;
        }

        /// <summary>
        /// Displays the message box with the specified text and caption.
        /// </summary>
        /// <param name="text">Text to display.</param>
        /// <param name="caption">Caption to display on the title bar.</param>
        public static DialogResult Show(string text, string caption)
        {
            using (MarathonMessageBoxForm messenger = new MarathonMessageBoxForm(text, caption))
                messenger.ShowDialog();

            return DialogResult.OK;
        }

        /// <summary>
        /// Displays the message box with the specified text, caption and buttons.
        /// </summary>
        /// <param name="text">Text to display.</param>
        /// <param name="caption">Caption to display on the title bar.</param>
        /// <param name="buttons">Buttons to display.</param>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        {
            using (MarathonMessageBoxForm messenger = new MarathonMessageBoxForm(text, caption, buttons))
                messenger.ShowDialog();

            return MessageReceiver.State switch
            {
                MessageState.OK     => DialogResult.OK,
                MessageState.Yes    => DialogResult.Yes,
                MessageState.No     => DialogResult.No,
                MessageState.Abort  => DialogResult.Abort,
                MessageState.Retry  => DialogResult.Retry,
                MessageState.Ignore => DialogResult.Ignore,
                MessageState.Cancel => DialogResult.Cancel,
                _                   => DialogResult.OK
            };
        }

        /// <summary>
        /// Displays the message box with the specified text, caption, buttons and icon.
        /// </summary>
        /// <param name="text">Text to display.</param>
        /// <param name="caption">Caption to display on the title bar.</param>
        /// <param name="buttons">Buttons to display.</param>
        /// <param name="icon">Icon to display.</param>
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            using (MarathonMessageBoxForm messenger = new MarathonMessageBoxForm(text, caption, buttons, icon))
                messenger.ShowDialog();

            return MessageReceiver.State switch
            {
                MessageState.OK     => DialogResult.OK,
                MessageState.Yes    => DialogResult.Yes,
                MessageState.No     => DialogResult.No,
                MessageState.Abort  => DialogResult.Abort,
                MessageState.Retry  => DialogResult.Retry,
                MessageState.Ignore => DialogResult.Ignore,
                MessageState.Cancel => DialogResult.Cancel,
                _                   => DialogResult.OK
            };
        }
    }
}
