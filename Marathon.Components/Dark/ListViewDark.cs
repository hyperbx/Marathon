// Marathon is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2021 HyperBE32
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
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace Marathon.Components
{
    public partial class ListViewDark : ListView
    {
        /// <summary>
        /// The colour of the background for the columns.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the background for the columns.")]
        public Color ColumnBackColour { get; set; } = Color.FromArgb(32, 32, 32);

        /// <summary>
        /// The colour of the separators for the columns.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the separators for the columns.")]
        public Color ColumnSeparatorColour { get; set; } = Color.FromArgb(99, 99, 99);

        /// <summary>
        /// Displays the column text in the centre.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("Displays the column text in the centre.")]
        public bool CentreColumnText { get; set; } = false;

        public ListViewDark()
        {
            InitializeComponent();

            OwnerDraw = true;
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            // Draw the items.
            e.DrawDefault = true;

            base.OnDrawItem(e);
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            // Draws the column background colour.
            e.Graphics.FillRectangle(new SolidBrush(ColumnBackColour), e.Bounds);

            // Draws the column header.
            ColumnHeader column = Columns[e.ColumnIndex];
            e.Graphics.FillRectangle(new SolidBrush(ColumnBackColour), e.Bounds.X, 0, 0, e.Bounds.Height);
            e.Graphics.DrawLine(new Pen(ColumnSeparatorColour), e.Bounds.X, e.Bounds.Y, e.Bounds.Left, e.Bounds.Right);

            // Draws the column text.
            TextRenderer.DrawText
            (
                e.Graphics,
                column.Text,
                Font,

                new Point
                (
                    CentreColumnText ?
                    e.Bounds.X + column.Width / 2 - TextRenderer.MeasureText(column.Text, Font).Width / 2 : // Centre column text.
                    e.Bounds.X + 4, 5                                                                       // Align column text to the left.
                ),

                ForeColor
            );

            base.OnDrawColumnHeader(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (Columns.Count != 0)
                Columns.Cast<ColumnHeader>().Last().Width = Width - Columns.Cast<ColumnHeader>().Sum(x => x.Width);
        }
    }
}
