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

namespace Marathon.Components
{
    public partial class ComboBoxDark : ComboBox
    {
        /// <summary>
        /// The colour of the border for the control.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the border for the control.")]
        public Color BorderColour { get; set; } = Color.FromArgb(67, 67, 70);

        /// <summary>
        /// The colour of the background for the drop down menu.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the background for the drop down menu.")]
        public Color DropDownBackColour { get; set; } = Color.FromArgb(27, 27, 28);

        /// <summary>
        /// The colour of the foreground for the drop down menu.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the foreground for the drop down menu.")]
        public Color DropDownForeColour { get; set; } = SystemColors.Control;

        /// <summary>
        /// The colour of the background for the drop down menu when hovering over it.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the background for the drop down menu when hovering over it.")]
        public Color DropDownHoverColour { get; set; } = Color.FromArgb(63, 63, 70);

        /// <summary>
        /// Tells the painter to draw the background for the drop down menu.
        /// </summary>
        private bool DrawBackground = false;

        public ComboBoxDark()
        {
            InitializeComponent();

            BackColor = Color.FromArgb(51, 51, 55);
            ForeColor = SystemColors.Control;
            FlatStyle = FlatStyle.Popup;
            DrawMode = DrawMode.OwnerDrawVariable;

            // Cheap hack to make the combo box smaller for item drawing later.
            Font = new Font(Font.FontFamily, 6.5f, FontStyle.Regular, GraphicsUnit.Point);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_PAINT = 0xF;
            int buttonWidth = SystemInformation.HorizontalScrollBarArrowWidth;

            base.WndProc(ref m);

            if (m.Msg == WM_PAINT && DropDownStyle != ComboBoxStyle.Simple)
            {
                using (var g = Graphics.FromHwnd(Handle))
                {
                    int dropDownLine = FlatStyle == FlatStyle.Popup ? 1 : 0;

                    // Draw drop down button separator first.
                    g.DrawLine
                    (
                        new Pen(BorderColour),
                        Width - buttonWidth - dropDownLine,
                        0,
                        Width - buttonWidth - dropDownLine,
                        Height
                    );

                    // Draw border on top of separator.
                    g.DrawRectangle(new Pen(BorderColour), 0, 0, Width - 1, Height - 1);
                }
            }
        }

        protected override void OnDropDown(EventArgs e)
        {
            // Enable draw flag.
            DrawBackground = true;

            base.OnDropDown(e);
        }

        protected override void OnDropDownClosed(EventArgs e)
        {
            // Disable draw flag.
            DrawBackground = false;

            base.OnDropDownClosed(e);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            /* Draw back colour when requested.
               I honestly wasn't expecting this to work. */
            if (DrawBackground)
                e.Graphics.FillRectangle(new SolidBrush(DropDownBackColour), e.Bounds);

            // Draw hover colour.
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e.Graphics.FillRectangle(new SolidBrush(DropDownHoverColour), e.Bounds);

            // Draw string anti-aliased.
            if (e.Index != -1)
            {
                TextRenderer.DrawText
                (
                    e.Graphics,
                    Items[e.Index].ToString(),

                    // Scale the font back up to something readable, but keeping the box size the same.
                    new Font(Font.FontFamily, 8f, FontStyle.Regular, GraphicsUnit.Point),

                    new Point(e.Bounds.X, e.Bounds.Y),
                    DropDownForeColour
                );
            }

            // Draw defaults.
            e.DrawFocusRectangle();
        }
    }
}
