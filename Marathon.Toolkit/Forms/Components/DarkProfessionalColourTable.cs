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

using System.Drawing;
using System.Windows.Forms;

namespace Marathon.Toolkit.Helpers
{
    /// <summary>
    /// ProfessionalColorTable palette used for common controls.
    /// </summary>
    internal class DarkProfessionalColourTable : ProfessionalColorTable
    {
        public override Color MenuItemBorder => MenuItemSelected;
        public override Color ButtonSelectedBorder => MenuItemBorder;
        public override Color ToolStripDropDownBackground => Color.FromArgb(27, 27, 28);
        public override Color MenuItemSelected => Color.FromArgb(51, 51, 52);
        public override Color ButtonCheckedHighlight => MenuItemSelected;
        public override Color ButtonPressedHighlight => MenuItemSelected;
        public override Color ButtonSelectedGradientBegin => Color.FromArgb(24, 24, 24);
        public override Color ButtonSelectedGradientMiddle => ButtonSelectedGradientBegin;
        public override Color ButtonSelectedGradientEnd => ButtonSelectedGradientBegin;
        public override Color MenuItemPressedGradientBegin => ToolStripDropDownBackground;
        public override Color MenuItemPressedGradientMiddle => MenuItemPressedGradientBegin;
        public override Color MenuItemPressedGradientEnd => MenuItemPressedGradientBegin;
        public override Color CheckBackground => Color.FromArgb(80, 80, 80);
        public override Color CheckPressedBackground => Color.FromArgb(48, 48, 48);
        public override Color CheckSelectedBackground => Color.FromArgb(104, 104, 104);
        public override Color MenuItemSelectedGradientBegin => Color.FromArgb(62, 62, 64);
        public override Color MenuItemSelectedGradientEnd => MenuItemSelectedGradientBegin;
        public override Color ImageMarginGradientBegin => ToolStripDropDownBackground;
        public override Color ImageMarginGradientMiddle => ImageMarginGradientBegin;
        public override Color ImageMarginGradientEnd => ImageMarginGradientBegin;
        public override Color SeparatorDark => Color.FromArgb(51, 51, 55);
        public override Color MenuBorder => SeparatorDark;
        public override Color ToolStripBorder => Color.FromArgb(45, 45, 48);
    }

    /// <summary>
    /// ProfessionalRenderer for context menus and strips.
    /// </summary>
    public class DarkToolStripProfessionalRenderer : ToolStripProfessionalRenderer
    {
        public DarkToolStripProfessionalRenderer() : base(new DarkProfessionalColourTable())
        {
            // Fixes an erroneous grey line appearing on the end of ToolStrip controls.
            RoundedEdges = false;
        }

        /// <summary>
        /// Changes the colour of the arrow for sub-menus upon hovering.
        /// </summary>
        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            if (e.Item.Selected)
                e.ArrowColor = Color.FromArgb(0, 122, 204);
            else
                e.ArrowColor = Color.FromArgb(153, 153, 153);

            base.OnRenderArrow(e);
        }
    }
}
