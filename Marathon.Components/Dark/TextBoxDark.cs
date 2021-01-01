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
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Marathon.Components
{
    public partial class TextBoxDark : TextBox
    {
        /// <summary>
        /// Initialiser for Watermark.
        /// </summary>
        private string _Watermark;

        /// <summary>
        /// The watermarked text associated with the control.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The watermarked text associated with the control.")]
        public string Watermark
        {
            get => _Watermark;

            set
            {
                _Watermark = value;

                // Set watermark using cue banner.
                SendMessage(Handle, 0x1501, 1, Watermark);
            }
        }

        /// <summary>
        /// Initialiser for DisabledBackColour.
        /// </summary>
        private Color _DisabledBackColour = Color.FromArgb(24, 24, 24);

        /// <summary>
        /// The colour of the text box when disabled.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the label when disabled.")]
        public Color DisabledBackColour
        {
            get => _DisabledBackColour;

            set
            {
                _DisabledBackColour = value;

                // Refresh the control to update in real-time.
                Refresh();
            }
        }

        /// <summary>
        /// Storage for the original back colour.
        /// </summary>
        private Color _EnabledBackColour;

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        public TextBoxDark()
        {
            InitializeComponent();

            BorderStyle = BorderStyle.FixedSingle;
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ForeColor = SystemColors.Control;
            BackColor = _EnabledBackColour = Color.FromArgb(43, 43, 43);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            if (Enabled)
            {
                // Use enabled colour.
                BackColor = _EnabledBackColour;
            }
            else
            {
                // Use disabled colour.
                BackColor = _DisabledBackColour;
            }

            base.OnEnabledChanged(e);
        }
    }
}
