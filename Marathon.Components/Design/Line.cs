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

using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using Marathon.Components.Helpers;

namespace Marathon.Components
{
    public partial class Line : UserControl
    {
        /// <summary>
        /// Initialiser for LineWidth.
        /// </summary>
        private int _LineWidth = 1;

        /// <summary>
        /// The width of the line drawn.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The width of the line drawn.")]
        public int LineWidth
        {
            get => _LineWidth;

            set
            {
                _LineWidth = value;

                // Refresh the control to update in real-time.
                Refresh();
            }
        }

        /// <summary>
        /// Initialiser for DropShadow.
        /// </summary>
        private bool _DropShadow = true;

        /// <summary>
        /// Draw a drop shadow for the line.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("Draw a drop shadow for the line.")]
        public bool DropShadow
        {
            get => _DropShadow;

            set
            {
                _DropShadow = value;

                // Refresh the control to update in real-time.
                Refresh();
            }
        }

        /// <summary>
        /// Initialiser for LineAlignment.
        /// </summary>
        private Alignment _LineAlignment = Alignment.Horizontal;

        /// <summary>
        /// The direction the line is facing.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The direction the line is facing.")]
        public Alignment LineAlignment
        {
            get => _LineAlignment;

            set
            {
                _LineAlignment = value;

                // Refresh the control to update in real-time.
                Refresh();
            }
        }

        /// <summary>
        /// Enumerator for line alignment.
        /// </summary>
        public enum Alignment
        {
            Vertical,
            Horizontal
        }

        public Line()
            => InitializeComponent();

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            Color shadow = Color.FromArgb(34, 34, 34).ChangeBrightness(36);

            // Draw vertical line.
            if (LineAlignment == Alignment.Vertical)
            {
                // Line.
                g.DrawLine(new Pen(ForeColor, LineWidth), Width / 2, 0, Width / 2, Height);

                // Drop shadow.
                if (DropShadow)
                    g.DrawLine(new Pen(shadow, LineWidth), Width / 2 + 1, 0, Width / 2 + 1, Height);
            }

            // Draw horizontal line.
            else if (LineAlignment == Alignment.Horizontal)
            {
                // Line.
                g.DrawLine(new Pen(ForeColor, LineWidth), 0, Height / 2, Width, Height / 2);

                // Drop shadow.
                if (DropShadow)
                    g.DrawLine(new Pen(shadow, LineWidth), 0, Height / 2 + 1, Width, Height / 2 + 1);
            }

            base.OnPaint(e);
        }
    }
}
