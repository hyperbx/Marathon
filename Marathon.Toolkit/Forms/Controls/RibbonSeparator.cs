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
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Windows.Forms;

namespace Marathon.Toolkit.Controls
{
    public partial class RibbonSeparator : UserControl
    {
        /// <summary>
        /// The colour of the separator.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the separator.")]
        public Color SeparatorColour { get; set; } = Color.FromArgb(92, 92, 92);

        public RibbonSeparator()
        {
            InitializeComponent();

            // Remove default margin.
            Margin = Padding.Empty;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var drawer = e.Graphics;

            drawer.SmoothingMode = SmoothingMode.HighQuality;
            drawer.PixelOffsetMode = PixelOffsetMode.HighQuality;
            drawer.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            drawer.Clear(BackColor);

            var separatorPen = new Pen(SeparatorColour, 1) { Alignment = PenAlignment.Outset };

            // Draws the separator.
            drawer.DrawLine(separatorPen, new Point(Width / 2, 9), new Point(Width / 2, Height));

            // Set interpolation mode.
            drawer.InterpolationMode = InterpolationMode.HighQualityBicubic;
        }
    }
}
