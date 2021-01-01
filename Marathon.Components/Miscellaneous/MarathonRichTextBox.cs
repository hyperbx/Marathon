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
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Marathon.Components.Helpers;

namespace Marathon.Components
{
    public partial class MarathonRichTextBox : RichTextBox
    {
        /// <summary>
        /// Lock input to this text box.
        /// </summary>
        [Category("Behavior"), Browsable(true), Description("Lock input to this text box.")]
        public bool LockInput { get; set; } = false;

        /// <summary>
        /// Allows the contents to be zoomed.
        /// </summary>
        [Category("Behavior"), Browsable(true), Description("Allows the contents to be zoomed.")]
        public bool Zoom { get; set; } = true;

        /// <summary>
        /// Make this control transparent.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("Make this control transparent.")]
        public bool Transparent { get; set; } = false;

        /// <summary>
        /// Initialiser for WordWrapToContentPadding.
        /// </summary>
        private bool _WordWrapToContentPadding = false;

        /// <summary>
        /// Indicates if lines are automatically word-wrapped for multiline edit controls.
        /// </summary>
        [Category("Behavior"), Browsable(true), Description("Indicates if lines are automatically word-wrapped for multiline edit controls.")]
        public bool WordWrapToContentPadding
        {
            get => _WordWrapToContentPadding;

            set
            {
                _WordWrapToContentPadding = WordWrap = value;

                RefreshPadding();
            }
        }

        /// <summary>
        /// Initialiser for ContentPadding.
        /// </summary>
        public Padding _ContentPadding = new Padding(0);

        /// <summary>
        /// Specifies the interior spacing of a control.
        /// </summary>
        [Category("Layout"), Browsable(true), Description("Specifies the interior spacing of a control.")]
        public Padding ContentPadding
        {
            get => _ContentPadding;

            set
            {
                _ContentPadding = value;

                RefreshPadding();
            }
        }

        [DllImport("user32.dll")]
        private static extern int HideCaret(IntPtr hwnd);

        public MarathonRichTextBox()
        {
            InitializeComponent();

            if (LockInput)
            {
                ReadOnly = true;
                TabStop = false;

                HideCaret(Handle);
            }
        }

        protected override void OnCreateControl()
        {
            // Set up padding.
            RefreshPadding();

            base.OnCreateControl();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;

                // Make transparent.
                if (Transparent)
                    createParams.ExStyle |= 0x20;

                return createParams;
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if ((ModifierKeys & Keys.Control) != 0)
            {
                // Enable/disable zoom.
                ((HandledMouseEventArgs)e).Handled = !Zoom;
            }
            else
            {
                base.OnMouseWheel(e);
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            if (LockInput)
                HideCaret(Handle);

            base.OnGotFocus(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            if (LockInput)
                HideCaret(Handle);

            base.OnEnter(e);
        }

        protected override void OnResize(EventArgs e)
        {
            if (LockInput)
                HideCaret(Handle);

            base.OnResize(e);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (LockInput)
                HideCaret(Handle);

            base.OnMouseUp(mevent);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (LockInput)
                HideCaret(Handle);

            base.OnMouseDown(e);
        }

        /// <summary>
        /// Refreshes the padding.
        /// </summary>
        private void RefreshPadding()
            => this.SetInnerMargins(ContentPadding.Left, ContentPadding.Top, ContentPadding.Right, ContentPadding.Bottom);
    }
}