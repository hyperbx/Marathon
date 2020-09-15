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

using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace Marathon.Toolkit.Components
{
    public partial class ListViewDark : ListView
    {
        public ListViewDark()
        {
            InitializeComponent();

            DrawItem += (drawItemSender, drawItemEventArgs) => drawItemEventArgs.DrawDefault = true;

            DrawColumnHeader += (drawColumnHeaderSender, drawColumnHeaderEventArgs) =>
            {
                // Shorten event arguments for consistency's sake.
                DrawListViewColumnHeaderEventArgs e = drawColumnHeaderEventArgs;

                // Draws the column background colour.
                Color theme = Color.FromArgb(37, 37, 38);
                e.Graphics.FillRectangle(new SolidBrush(theme), e.Bounds);

                // Draws the column header.
                ColumnHeader column = Columns[e.ColumnIndex];
                e.Graphics.FillRectangle(new SolidBrush(theme), e.Bounds.X, 0, 0, e.Bounds.Height);
                e.Graphics.DrawLine(new Pen(Color.FromArgb(63, 63, 70)), e.Bounds.X, e.Bounds.Y, e.Bounds.Left, e.Bounds.Right);

                // Draws the column text.
                TextRenderer.DrawText(e.Graphics, column.Text, Font, new Point(e.Bounds.X + 4, 4), ForeColor);
            };

            Resize += delegate
            {
                if (Columns.Count != 0)
                    Columns.Cast<ColumnHeader>().Last().Width = Width - Columns.Cast<ColumnHeader>().Select(x => x.Width).FirstOrDefault();
            };
        }
    }
}
