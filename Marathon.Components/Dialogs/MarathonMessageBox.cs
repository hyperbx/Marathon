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
using System.Media;
using System.Drawing;
using System.Windows.Forms;

namespace Marathon.Components
{
    public partial class MarathonMessageBoxForm : Form
    {
        private int BodyHeight;

        public MarathonMessageBoxForm
        (
            string text,
            string caption = "Marathon",
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.Information
        )
        {
            InitializeComponent();

            // Set body and caption.
            MarathonRichTextBox_Body.Text = text;
            Text = caption;

            if (text.Length <= 50)
            {
                // Use body height and only account for the button panel.
                Height = BodyHeight + Panel_ButtonBackdrop.Height;
            }
            else
            {
                // Stored for easier reference.
                Padding bodyPadding = MarathonRichTextBox_Body.ContentPadding;

                // Set width to locked safe width.
                Width = 400;

                // Use body height and account for content padding.
                Height = BodyHeight + 145 - (MarathonRichTextBox_Body.ContentPadding.Top / 4);

                // Decrease top padding to align the taller body with the icon.
                MarathonRichTextBox_Body.ContentPadding = new Padding
                (
                    bodyPadding.Left,

                    // Calculate top padding based off body length.
                    bodyPadding.Top - (text.Length > 130 ? 10 : text.Length > 65 ? 5 : 0),

                    bodyPadding.Right,
                    bodyPadding.Bottom
                );
            }

            switch (buttons)
            {
                case MessageBoxButtons.OK:
                {
                    ButtonDark_3.Text = "OK";
                    ButtonDark_3.Visible = true;
                    ButtonDark_3.Tag = MessageState.OK;

                    AcceptButton = ButtonDark_3;

                    break;
                }

                case MessageBoxButtons.OKCancel:
                {
                    ButtonDark_2.Text = "OK";
                    ButtonDark_2.Visible = true;
                    ButtonDark_2.Tag = MessageState.OK;

                    ButtonDark_3.Text = "Cancel";
                    ButtonDark_3.Visible = true;
                    ButtonDark_3.Tag = MessageState.Cancel;

                    AcceptButton = ButtonDark_2;
                    CancelButton = ButtonDark_3;

                    break;
                }

                case MessageBoxButtons.YesNo:
                {
                    ButtonDark_2.Text = "Yes";
                    ButtonDark_2.Visible = true;
                    ButtonDark_2.Tag = MessageState.Yes;

                    ButtonDark_3.Text = "No";
                    ButtonDark_3.Visible = true;
                    ButtonDark_3.Tag = MessageState.No;

                    AcceptButton = ButtonDark_2;
                    CancelButton = ButtonDark_3;

                    break;
                }

                case MessageBoxButtons.YesNoCancel:
                {
                    ButtonDark_1.Text = "Yes";
                    ButtonDark_1.Visible = true;
                    ButtonDark_1.Tag = MessageState.Yes;

                    ButtonDark_2.Text = "No";
                    ButtonDark_2.Visible = true;
                    ButtonDark_2.Tag = MessageState.No;

                    ButtonDark_3.Text = "Cancel";
                    ButtonDark_3.Visible = true;
                    ButtonDark_3.Tag = MessageState.Cancel;

                    AcceptButton = ButtonDark_1;
                    CancelButton = ButtonDark_3;

                    break;
                }

                case MessageBoxButtons.RetryCancel:
                {
                    ButtonDark_2.Text = "Retry";
                    ButtonDark_2.Visible = true;
                    ButtonDark_2.Tag = MessageState.Retry;

                    ButtonDark_3.Text = "Cancel";
                    ButtonDark_3.Visible = true;
                    ButtonDark_3.Tag = MessageState.Cancel;

                    AcceptButton = ButtonDark_2;
                    CancelButton = ButtonDark_3;

                    break;
                }

                case MessageBoxButtons.AbortRetryIgnore:
                {
                    ButtonDark_1.Text = "Abort";
                    ButtonDark_1.Visible = true;
                    ButtonDark_1.Tag = MessageState.Abort;

                    ButtonDark_2.Text = "Retry";
                    ButtonDark_2.Visible = true;
                    ButtonDark_2.Tag = MessageState.Retry;

                    ButtonDark_3.Text = "Ignore";
                    ButtonDark_3.Visible = true;
                    ButtonDark_3.Tag = MessageState.Ignore;

                    AcceptButton = ButtonDark_2;
                    CancelButton = ButtonDark_3;

                    break;
                }

                default:
                {
                    goto case MessageBoxButtons.OK;
                }
            }

            switch (icon)
            {
                case MessageBoxIcon.Error:
                {
                    PictureBox_Icon.BackgroundImage = Cache.LoadBitmapResource(nameof(Properties.Resources.Feedback_Error));
                    SystemSounds.Hand.Play();
                    TopMost = true;

                    break;
                }

                case MessageBoxIcon.Information:
                {
                    PictureBox_Icon.BackgroundImage = Cache.LoadBitmapResource(nameof(Properties.Resources.Feedback_Information));
                    SystemSounds.Asterisk.Play();
                    TopMost = false;

                    break;
                }

                case MessageBoxIcon.Question:
                {
                    PictureBox_Icon.BackgroundImage = Cache.LoadBitmapResource(nameof(Properties.Resources.Feedback_Question));
                    SystemSounds.Question.Play();
                    TopMost = false;

                    break;
                }

                case MessageBoxIcon.Warning:
                {
                    PictureBox_Icon.BackgroundImage = Cache.LoadBitmapResource(nameof(Properties.Resources.Feedback_Warning));
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

        /// <summary>
        /// Sends the message to the receiver upon clicking an option.
        /// </summary>
        private void Button_Click_Group(object sender, EventArgs e)
        {
            MarathonMessageBox.State = (MessageState)((Button)sender).Tag;

            Close();
        }

        /// <summary>
        /// Get the height of the body.
        /// </summary>
        private void MarathonRichTextBox_Body_ContentsResized(object sender, ContentsResizedEventArgs e)
            => BodyHeight = e.NewRectangle.Height;
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
    /// A custom message box class with the intent of mimicking the standard WINAPI message box with a dark theme.
    /// </summary>
    public class MarathonMessageBox
    {
        /// <summary>
        /// Message receiver for the final message box state.
        /// </summary>
        public static MessageState State;

        /// <summary>
        /// Displays the message box with the specified text, caption, buttons and icon.
        /// </summary>
        /// <param name="text">Text to display.</param>
        /// <param name="caption">Caption to display on the title bar.</param>
        /// <param name="buttons">Buttons to display.</param>
        /// <param name="icon">Icon to display.</param>
        public static DialogResult Show
        (
            string text,
            string caption = "Marathon",
            MessageBoxButtons buttons = MessageBoxButtons.OK,
            MessageBoxIcon icon = MessageBoxIcon.Information
        )
        {
            using
            (
                MarathonMessageBoxForm messenger = new MarathonMessageBoxForm
                (
                    text,
                    string.IsNullOrEmpty(caption) ? "Marathon" : caption,
                    buttons,
                    icon
                )
            )
            {
                messenger.ShowDialog();
            }

            return State switch
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
