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
using System.Windows.Forms.Design;
using System.ComponentModel;

namespace Marathon.Toolkit.Controls
{
    [Designer(typeof(RibbonPanelDesigner))]
    public partial class RibbonPanel : UserControl
    {
        /// <summary>
        /// Passthrough control for designer support.
        /// </summary>
        [Category("Appearance"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public FlowLayoutPanel WorkingArea => FlowLayoutPanel_Contents;

        /// <summary>
        /// The name of the category presented by the panel.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The name of the category presented by the panel.")]
        public string Caption { get; set; } = "Placeholder";

        /// <summary>
        /// The colour of the separators.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the separators.")]
        public Color SeparatorColour { get; set; } = Color.FromArgb(65, 65, 65);

        /// <summary>
        /// Displays a separator on the left.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("Displays a separator on the left.")]
        public bool ShowLeftSeparator { get; set; } = false;

        /// <summary>
        /// Displays a separator on the right.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("Displays a separator on the right.")]
        public bool ShowRightSeparator { get; set; } = true;

        /// <summary>
        /// The font used to display text in the control.
        /// </summary>
        [Browsable(false)]
        public override Font Font => new Font("Segoe UI", 8F);

        public RibbonPanel() => InitializeComponent();

        protected override void OnPaint(PaintEventArgs e)
        {
            var drawer = e.Graphics;

            drawer.SmoothingMode = SmoothingMode.HighQuality;
            drawer.PixelOffsetMode = PixelOffsetMode.HighQuality;
            drawer.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            drawer.Clear(BackColor);

            var separatorPen = new Pen(SeparatorColour, 1) { Alignment = PenAlignment.Outset };

            if (ShowLeftSeparator)
            {
                // Draws the left separator.
                drawer.DrawLine(separatorPen, new Point(0, 2), new Point(0, Height - 3));
            }

            if (ShowRightSeparator)
            {
                // Draws the right separator.
                drawer.DrawLine(separatorPen, new Point(Width, 2), new Point(Width, Height - 3));
            }

            // Pre-measured string.
            var stringDimensions = drawer.MeasureString(Caption, Font);

            /* Draws the caption text... Originally, this was just a simple label, but WinForms kept shoving it to the left,
               regardless of alignment. We're doing the most basic of basic maths here to calculate the centre, but also taking away
               a half pixel to account for the separators. */
            drawer.DrawString(Caption, Font, new SolidBrush(SystemColors.ControlDark), (Width / 2 - stringDimensions.Width / 2) - 0.5f, Height - 16);

            // Set interpolation mode.
            drawer.InterpolationMode = InterpolationMode.HighQualityBicubic;
        }
    }

    public class RibbonPanelDesigner : ParentControlDesigner
    {
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            if (Control is RibbonPanel panel)
            {
                // Allows the designer to communicate with the FlowLayoutPanel control.
                EnableDesignMode(panel.WorkingArea, "WorkingArea");
            }
        }
    }
}
