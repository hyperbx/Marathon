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
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Marathon.Toolkit.Components
{
    public partial class TabControlFlat : TabControl
    {
        /// <summary>
        /// The colour of the selected page.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the selected tab.")]
        public Color ActiveColour { get; set; } = Color.FromArgb(0, 122, 204);

        /// <summary>
        /// The colour of the background for the tabs.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the background for the tabs.")]
        public Color TabPageBackColour { get; set; } = Color.FromArgb(24, 24, 24);

        /// <summary>
        /// The colour of the border for the control.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the border for the control.")]
        public Color BorderColour { get; set; } = Color.FromArgb(100, 100, 100);

        /// <summary>
        /// The colour of the close button.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the close button.")]
        public Color CloseButtonColour { get; set; } = Color.FromArgb(208, 230, 245);

        /// <summary>
        /// The colour of the header.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the header.")]
        public Color HeaderColour { get; set; } = Color.FromArgb(45, 45, 48);

        /// <summary>
        /// The colour of the horizontal line under the tabs.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the horizontal line under the tabs.")]
        public Color HorizontalLineColour { get; set; } = Color.FromArgb(0, 122, 204);

        /// <summary>
        /// The colour of the tab text.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the tab text.")]
        public Color TextColour { get; set; } = SystemColors.Control;

        /// <summary>
        /// The colour of the selected tab text.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the selected tab text.")]
        public Color SelectedTextColour { get; set; } = SystemColors.Control;

        /// <summary>
        /// Determines whether the tabs can be dragged.
        /// </summary>
        [Category("Behavior"), Browsable(true), Description("Determines whether the tabs can be dragged.")]
        public bool AllowDragging { get; set; } = true;

        /// <summary>
        /// Allows tabs to be closed with middle-click.
        /// </summary>
        [Category("Behavior"), Browsable(true), Description("Allows tabs to be closed with middle-click.")]
        public bool CloseOnMiddleClick { get; set; } = false;

        /// <summary>
        /// Displays a close button on the tabs.
        /// </summary>
        [Category("Behavior"), Browsable(true), Description("Displays a close button on the tabs.")]
        public bool ShowCloseButton { get; set; } = false;

        /// <summary>
        /// Displays a message upon closing the tab.
        /// </summary>
        [Category("Behavior"), Browsable(true), Description("Displays a message upon closing the tab.")]
        public bool ShowClosingMessage { get; set; } = false;

        /// <summary>
        /// The message displayed upon closing the tab.
        /// </summary>
        [Category("Behavior"), Browsable(true), Description("The message displayed upon closing the tab.")]
        public string ClosingMessage { get; set; }

        /// <summary>
        /// The format of the tab text.
        /// </summary>
        private readonly StringFormat CentreStringFormat = new StringFormat
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Center
        };

        /// <summary>
        /// The last tab interacted with by the user.
        /// </summary>
        private TabPage LastInteractedTab;

        /// <summary>
        /// Image used for the close button.
        /// </summary>
        private Bitmap CloseButton = Resources.LoadBitmapResource(nameof(Properties.Resources.TabControlFlat_Close));

        public TabControlFlat()
        {
            // Set the component style so we can do OwnerDraw tasks properly.
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);

            // Forced on so we can drag tabs.
            AllowDrop = true;
        }

        /// <summary>
        /// Drags the selected tab.
        /// </summary>
        protected override void OnDragOver(DragEventArgs e)
        {
            if (AllowDragging && !Program.RunningInDesigner())
            {
                var draggedTab = (TabPage)e.Data.GetData(typeof(TabPage));
                var pointedTab = GetPointedTab();

                if (ReferenceEquals(draggedTab, LastInteractedTab) && pointedTab != null)
                {
                    e.Effect = DragDropEffects.Move;

                    if (!ReferenceEquals(pointedTab, draggedTab))
                    {
                        SwapTabIndex(draggedTab, pointedTab);
                    }
                }
            }
        }

        /// <summary>
        /// Handles tab selection and closure.
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!Program.RunningInDesigner())
            {
                if (AllowDragging)
                {
                    // Update last interacted tab.
                    LastInteractedTab = GetPointedTab();
                }

                var cursor = e.Location;

                // Close button was clicked.
                if (ShowCloseButton)
                {
                    for (int i = 0; i < TabCount; i++)
                    {
                        var closeButton = GetTabRect(i);
                        closeButton.Offset(closeButton.Width - 15, 2);
                        closeButton.Size = CloseButton.Size;

                        // Iterate again if the close button wasn't clicked.
                        if (!closeButton.Contains(cursor))
                            continue;

                        if (ShowClosingMessage)
                        {
                            if (MarathonMessageBox.Show(ClosingMessage, string.Empty, MessageBoxButtons.YesNo) ==
                                DialogResult.Yes)
                            {
                                TabPages.RemoveAt(i);
                            }
                        }
                        else
                        {
                            TabPages.RemoveAt(i);
                        }
                    }
                }

                // Middle mouse button was clicked above the tab.
                else if (CloseOnMiddleClick)
                {
                    if (e.Button == MouseButtons.Middle)
                    {
                        TabPages.Remove(GetPointedTab());
                    }
                }
            }
        }

        /// <summary>
        /// Holds the selected page until it's abandoned.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (AllowDragging && !Program.RunningInDesigner())
            {
                if (e.Button == MouseButtons.Left && LastInteractedTab != null)
                {
                    // Start drag and drop operation.
                    DoDragDrop(LastInteractedTab, DragDropEffects.Move);
                }
            }
        }

        /// <summary>
        /// Abandons the selected tab.
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (AllowDragging && !Program.RunningInDesigner())
            {
                // Reset last interacted tab.
                LastInteractedTab = null;
            }
        }

        /// <summary>
        /// Draws the TabControl.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            var drawer = e.Graphics;

            drawer.SmoothingMode = SmoothingMode.HighQuality;
            drawer.PixelOffsetMode = PixelOffsetMode.HighQuality;
            drawer.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            drawer.Clear(HeaderColour);

            if (!Program.RunningInDesigner())
            {
                // Check if null so we don't crash with nothing present.
                if (SelectedTab != null)
                {
                    SelectedTab.BackColor = TabPageBackColour;
                    SelectedTab.BorderStyle = BorderStyle.None;
                }
            }

            // The height input by the user.
            int commonHeight = ItemSize.Height;

            for (int i = 0; i <= TabCount - 1; i++)
            {
                var header = new Rectangle(new Point(GetTabRect(i).Location.X + 3, GetTabRect(i).Location.Y), new Size(GetTabRect(i).Width, commonHeight));
                var headerSize = new Rectangle(header.Location, new Size(header.Width, header.Height));

                // Current iteration is the selected tab.
                if (i == SelectedIndex)
                {
                    // Draws the back of the header.
                    drawer.FillRectangle(new SolidBrush(HeaderColour), headerSize);

                    // Draws the selected colour.
                    drawer.FillRectangle(new SolidBrush(ActiveColour), new Rectangle(header.X - 5, header.Y - 3, header.Width, header.Height));

                    // Draws the tab text.
                    DrawTabText(TabPages[i].Text, SelectedTextColour);

                    // Draws the close button if requested.
                    if (ShowCloseButton)
                    {
                        /* The WinForms designer really doesn't like Marathon's bitmap cache,
                           so we'll just draw a simple ASCII character here instead as a placeholder. */
                        if (Program.RunningInDesigner())
                        {
                            e.Graphics.DrawString("x", Font, new SolidBrush(CloseButtonColour), headerSize.Right - 17, commonHeight - 21);
                        }

                        // Otherwise, we can just draw the bitmap.
                        else
                        {
                            // Draw the image unscaled so we don't get blurriness.
                            e.Graphics.DrawImageUnscaledAndClipped(CloseButton, new Rectangle(new Point(headerSize.Right - 20, commonHeight - 15), CloseButton.Size));
                        }
                    }
                }

                // Otherwise, just draw the string alone.
                else
                {
                    // Draws the header when it's not selected.
                    DrawTabText(TabPages[i].Text, TextColour);
                }

                // Draws the tab text with common arguments.
                void DrawTabText(string text, Color colour)
                {
                    // Shift the text to the centre if the close button isn't displayed.
                    float textX = ShowCloseButton ? header.X : header.Left + drawer.MeasureString(text, Font).Width / 10;

                    drawer.DrawString(text,
                                      Font,
                                      new SolidBrush(colour),
                                      new RectangleF(textX, headerSize.Y - 2, headerSize.Width, headerSize.Height),
                                      CentreStringFormat);
                }
            }

            // Draws the horizontal line.
            drawer.DrawLine(new Pen(HorizontalLineColour, 5), new Point(0, commonHeight), new Point(Width, commonHeight));

            // Draws the background of the control.
            drawer.FillRectangle(new SolidBrush(TabPageBackColour), new Rectangle(0, commonHeight, Width, Height));

            // Draws the border of the control.
            drawer.DrawRectangle(new Pen(BorderColour, 2), new Rectangle(0, 0, Width, Height));

            // Set interpolation mode.
            drawer.InterpolationMode = InterpolationMode.HighQualityBicubic;
        }

        /// <summary>
        /// Gets the tab under the cursor.
        /// </summary>
        private TabPage GetPointedTab()
        {
            for (int i = 0; i <= TabPages.Count - 1; i++)
            {
                if (GetTabRect(i).Contains(PointToClient(Cursor.Position)))
                {
                    return TabPages[i];
                }
            }

            return null;
        }

        /// <summary>
        /// Swaps two tabs.
        /// </summary>
        private void SwapTabIndex(TabPage Source, TabPage Destination)
        {
            var sourceIndex = TabPages.IndexOf(Source);
            var destinationIndex = TabPages.IndexOf(Destination);

            TabPages[destinationIndex] = Source;
            TabPages[sourceIndex] = Destination;

            if (SelectedIndex == sourceIndex)
            {
                SelectedIndex = destinationIndex;
            }
            else if (SelectedIndex == destinationIndex)
            {
                SelectedIndex = sourceIndex;
            }

            Refresh();
        }
    }
}
