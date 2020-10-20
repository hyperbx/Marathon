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
using System.Windows.Forms;

namespace Marathon.Toolkit.Components
{
    public partial class LabelDark : Label
    {
        /// <summary>
        /// The format of the label text.
        /// </summary>
        private readonly StringFormat CentreStringFormat = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        public LabelDark() => InitializeComponent();

        protected override void OnPaint(PaintEventArgs e)
        {
            var drawer = e.Graphics;

            drawer.SmoothingMode = SmoothingMode.HighQuality;
            drawer.PixelOffsetMode = PixelOffsetMode.HighQuality;
            drawer.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            // Draw the label with the corrected colours.
            drawer.DrawString(Text,
                              Font,
                              new SolidBrush(Enabled ? ForeColor : Color.FromArgb(204, 204, 204)),
                              ClientRectangle,
                              ContentAlignmentToStringFormat(TextAlign));
        }

        /// <summary>
        /// Converts a ContentAlignment enumerator to the appropriate StringFormat.
        /// </summary>
        private StringFormat ContentAlignmentToStringFormat(ContentAlignment textAlign)
        {
            StringFormat format = new StringFormat();

            switch (textAlign)
            {
                case ContentAlignment.TopCenter:
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    break;
                }

                case ContentAlignment.TopLeft:
                {
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Near;
                    break;
                }

                case ContentAlignment.TopRight:
                {
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Far;
                    break;
                }

                case ContentAlignment.MiddleCenter:
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    break;
                }

                case ContentAlignment.MiddleLeft:
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Near;
                    break;
                }

                case ContentAlignment.MiddleRight:
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Far;
                    break;
                }

                case ContentAlignment.BottomCenter:
                {
                    format.Alignment = StringAlignment.Far;
                    format.LineAlignment = StringAlignment.Center;
                    break;
                }

                case ContentAlignment.BottomLeft:
                {
                    format.Alignment = StringAlignment.Far;
                    format.LineAlignment = StringAlignment.Near;
                    break;
                }

                case ContentAlignment.BottomRight:
                {
                    format.Alignment = StringAlignment.Far;
                    format.LineAlignment = StringAlignment.Far;
                    break;
                }
            }

            return format;
        }
    }
}
