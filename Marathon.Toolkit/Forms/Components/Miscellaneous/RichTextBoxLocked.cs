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
using System.Runtime.InteropServices;

namespace Marathon.Toolkit.Components
{
    public partial class RichTextBoxLocked : RichTextBox
    {
        [DllImport("user32.dll")]
        private static extern int HideCaret(IntPtr hwnd);

        public RichTextBoxLocked()
        {
            InitializeComponent();

            ReadOnly = true;
            TabStop = false;

            HideCaret(Handle);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            HideCaret(Handle);

            base.OnGotFocus(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            HideCaret(Handle);

            base.OnEnter(e);
        }

        protected override void OnResize(EventArgs e)
        {
            HideCaret(Handle);

            base.OnResize(e);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            HideCaret(Handle);

            base.OnMouseUp(mevent);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            HideCaret(Handle);

            base.OnMouseDown(e);
        }
    }
}