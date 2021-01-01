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
using BrightIdeasSoftware;
using Marathon.Components.Helpers;

namespace Marathon.Components
{
    public partial class ObjectListViewDark : ObjectListView
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
        /// The colour of the items when selected.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the items when selected.")]
        public Color SelectedItemColour { get; set; } = Color.FromArgb(98, 98, 98);

        public ObjectListViewDark()
        {
            InitializeComponent();

            // Set colours.
            BackColor = Color.FromArgb(32, 32, 32);
            ForeColor = SystemColors.Control;

            // Set properties.
            AllowColumnReorder = false;
            BorderStyle = BorderStyle.None;
            FullRowSelect = true;
            HeaderMinimumHeight = 31;
            OwnerDrawnHeader = true;
            RenderNonEditableCheckboxesAsDisabled = true;
            RowHeight = 20;
            ShowGroups = false;
            ShowItemToolTips = true;
            Sorting = SortOrder.Ascending;
            UseFilterIndicator = true;
        }

        private void DrawSelectionRectangle
        (
            Graphics g,
            Rectangle bounds,
            int index,
            bool focused = false,
            bool subItem = false,
            ListViewItem parentItem = null,
            int subItemIndex = 0
        )
        {
            // Set clipping region.
            g.SetClip(bounds);

            // Draw full border.
            g.FillRectangle(new SolidBrush(SelectedItemColour.ChangeBrightness(-59)), bounds);

            switch (subItem)
            {
                case true:
                {
                    // Current sub item is the last index.
                    if (parentItem != null && subItemIndex == parentItem.SubItems.Count - 1)
                    {
                        // Crush width and height to reveal right border.
                        bounds = new Rectangle
                        (
                            bounds.X - 1,
                            index == 0 ? bounds.Y + 1 : bounds.Y,
                            bounds.Width,
                            index == 0 ? bounds.Height - 2 : bounds.Height - 1
                        );
                    }
                    else
                    {
                        goto default;
                    }

                    break;
                }

                default:
                {
                    // Crush width and height to reveal borders around whole item.
                    bounds = new Rectangle(subItem ? bounds.X : bounds.X + 1, bounds.Y, bounds.Width, bounds.Height - 1);

                    if (index == 0)
                    {
                        // Boundaries for first index.
                        bounds = new Rectangle(bounds.X, bounds.Y + 1, bounds.Width, bounds.Height - 1);
                    }

                    break;
                }
            }

            if (focused)
            {
                // Fill focus box.
                g.FillRectangle(new SolidBrush(SelectedItemColour.ChangeBrightness(21)), bounds);
            }
            else
            {
                // Fill selection box.
                g.FillRectangle(new SolidBrush(SelectedItemColour), bounds);
            }

            // Reset clipping region.
            g.ResetClip();
        }

        private void DrawFocusRectangle
        (
            Graphics g,
            Rectangle bounds,
            bool subItem = false,
            ListViewItem parentItem = null,
            int subItemIndex = 0
        )
        {
            // Set clipping region.
            g.SetClip(bounds);

            // Draw full border.
            g.FillRectangle(new SolidBrush(SelectedItemColour), bounds);

            // Deflate bounds to keep borders drawn.
            bounds.Inflate(-1, -1);

            switch (subItem)
            {
                case true:
                {
                    // Current sub item is the last index.
                    if (parentItem != null && subItemIndex == parentItem.SubItems.Count - 1)
                    {
                        // Draw the right border to close the focus rectangle.
                        g.FillRectangle(new SolidBrush(BackColor), new Rectangle(bounds.X - 1, bounds.Y, bounds.Width + 1, bounds.Height));
                    }
                    else
                    {
                        // Fill focus box but overspill to hide the subitem borders.
                        g.FillRectangle(new SolidBrush(BackColor), bounds.InflateRectangle(1, 0));
                    }

                    break;
                }

                default:
                {
                    // Fill focus box.
                    g.FillRectangle(new SolidBrush(BackColor), bounds);

                    break;
                }
            }

            // Reset clipping region.
            g.ResetClip();
        }

        private void DrawFieldText
        (
            Graphics g,
            Rectangle bounds,
            string text,
            HorizontalAlignment textAlign = HorizontalAlignment.Left,
            int verticalOffset = -1
        )
        {
            // -1 is just an unset flag - if it's set to that, default to measured height.
            if (verticalOffset == -1)
            {
                // I'm way too picky with pixels.
                verticalOffset = (RowHeight / 2) - (TextRenderer.MeasureText(text, Font).Height / 2) - 1;
            }

            // Draws the text.
            switch (textAlign)
            {
                case HorizontalAlignment.Left:
                {
                    // Draws the column text on the left.
                    TextRenderer.DrawText
                    (
                        g,
                        text,
                        Font,
                        new Point(bounds.X + 4, bounds.Y + verticalOffset),
                        ForeColor
                    );

                    break;
                }

                case HorizontalAlignment.Right:
                {
                    // Draws the column text on the right.
                    TextRenderer.DrawText
                    (
                        g,
                        text,
                        Font,
                        new Point(bounds.Right - TextRenderer.MeasureText(text, Font).Width - 4, bounds.Y + verticalOffset),
                        ForeColor
                    );

                    break;
                }

                case HorizontalAlignment.Center:
                {
                    // Draws the column text in the centre.
                    TextRenderer.DrawText
                    (
                        g,
                        text,
                        Font,
                        new Point(bounds.X + bounds.Width / 2 - TextRenderer.MeasureText(text, Font).Width / 2, bounds.Y + verticalOffset),
                        ForeColor
                    );

                    break;
                }
            }
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            if (e.Item.Selected)
            {
                // Draw selection rectangle.
                DrawSelectionRectangle(e.Graphics, e.Bounds, e.ItemIndex, e.Item.Focused);
            }
            else
            {
                if ((e.State & ListViewItemStates.Focused) != 0)
                {
                    // Draw outline.
                    DrawFocusRectangle(e.Graphics, e.Bounds);
                }
                else
                {
                    // Fill item.
                    e.Graphics.FillRectangle(new SolidBrush(BackColor), e.Bounds);
                }
            }

            // Draws the item text.
            DrawFieldText(e.Graphics, e.Bounds, e.Item.Text, e.Item.ListView.Columns[0].TextAlign);
        }

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                // Draw default background.
                e.DrawBackground();

                if (e.Item.Selected)
                {
                    // Draw selection rectangle.
                    DrawSelectionRectangle(e.Graphics, e.Bounds, e.ItemIndex, e.Item.Focused, true, e.Item, e.ColumnIndex);
                }
                else
                {
                    if ((e.ItemState & ListViewItemStates.Focused) != 0)
                    {
                        // Draw outline.
                        DrawFocusRectangle(e.Graphics, e.Bounds, true, e.Item, e.ColumnIndex);
                    }
                }

                // Draws the item text.
                DrawFieldText(e.Graphics, e.Bounds, e.SubItem.Text, e.Header.TextAlign);
            }
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            // Solid brush hot colour.
            SolidBrush headerStyle = e.ColumnIndex == HeaderControl.ColumnIndexUnderCursor ?

                                     e.State == ListViewItemStates.Selected ?
                                     new SolidBrush(ColumnBackColour.ChangeBrightness(99)) : // Mouse down colour.
                                     new SolidBrush(ColumnBackColour.ChangeBrightness(35)) : // Mouse hover colour.

                                     // Default back colour.
                                     new SolidBrush(ColumnBackColour);

            // Draw default header.
            {
                // Draws the column background.
                e.Graphics.FillRectangle(headerStyle, e.Bounds);

                // Draws the whitespace area.
                e.Graphics.FillRectangle(new SolidBrush(ColumnBackColour), e.Bounds.X, e.Bounds.Y + 25, e.Bounds.Width, 6);
            }

            // Draw separators for columns.
            e.Graphics.DrawLine(new Pen(ColumnSeparatorColour), e.Bounds.Right - 1, e.Bounds.Y, e.Bounds.Right - 1, 24);

            if (LastSortColumn != null && e.ColumnIndex == LastSortColumn.Index)
            {
                // Last sorted column boundaries.
                Rectangle bounds = ListViewHelper.GetColumnRectangle(this, LastSortColumn);

                // Set anti-alias quality.
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                switch (LastSortOrder)
                {
                    case SortOrder.Ascending:
                    {
                        // Draw up arrow.
                        e.Graphics.DrawLine(new Pen(ColumnSeparatorColour), bounds.X + (bounds.Width / 2), bounds.Y, bounds.X + (bounds.Width / 2) + 3, 3);
                        e.Graphics.DrawLine(new Pen(ColumnSeparatorColour), bounds.X + (bounds.Width / 2), bounds.Y, bounds.X + (bounds.Width / 2) - 3, 3);

                        break;
                    }

                    case SortOrder.Descending:
                    {
                        // Draw down arrow.
                        e.Graphics.DrawLine(new Pen(ColumnSeparatorColour), bounds.X + (bounds.Width / 2), bounds.Y + 3, bounds.X + (bounds.Width / 2) - 3, 0);
                        e.Graphics.DrawLine(new Pen(ColumnSeparatorColour), bounds.X + (bounds.Width / 2), bounds.Y + 3, bounds.X + (bounds.Width / 2) + 3, 0);

                        break;
                    }
                }
            }

            // Draws the column text.
            DrawFieldText(e.Graphics, e.Bounds, Columns[e.ColumnIndex].Text, e.Header.TextAlign, 5);
        }
    }
}
