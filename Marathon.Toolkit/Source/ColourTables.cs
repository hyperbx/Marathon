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

using System.IO;
using System.Linq;
using System.Drawing;
using System.Xml.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Marathon.Components
{
    /// <summary>
    /// ProfessionalColorTable palette used for common controls.
    /// </summary>
    internal class DarkColorTable : ProfessionalColorTable
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
        public override Color MenuItemPressedGradientBegin => Color.FromArgb(27, 27, 28);
        public override Color MenuItemPressedGradientMiddle => MenuItemPressedGradientBegin;
        public override Color MenuItemPressedGradientEnd => MenuItemPressedGradientBegin;
        public override Color CheckBackground => Color.FromArgb(80, 80, 80);
        public override Color CheckPressedBackground => Color.FromArgb(48, 48, 48);
        public override Color CheckSelectedBackground => Color.FromArgb(104, 104, 104);
        public override Color MenuItemSelectedGradientBegin => Color.FromArgb(62, 62, 64);
        public override Color MenuItemSelectedGradientEnd => MenuItemSelectedGradientBegin;
        public override Color ImageMarginGradientBegin => Color.FromArgb(27, 27, 28);
        public override Color ImageMarginGradientMiddle => ImageMarginGradientBegin;
        public override Color ImageMarginGradientEnd => ImageMarginGradientBegin;
        public override Color SeparatorDark => Color.FromArgb(51, 51, 55);
    }

    /// <summary>
    /// ThemeBase class used for WeifenLuo libraries.
    /// </summary>
    public abstract class DockPalette : ThemeBase
    {
        public DockPalette(byte[] resources)
        {
            ColorPalette = new DockPanelColorPalette(new DockPaletteFactory(resources));
            Skin = new DockPanelSkin();
            ToolStripRenderer = new VisualStudioToolStripRenderer(ColorPalette) { UseGlassOnMenuStrip = false };
            Measures.SplitterSize = 6;
            Measures.AutoHideSplitterSize = 3;
            Measures.DockPadding = 6;
            ShowAutoHideContentOnHover = false;
        }

        public override void CleanUp(DockPanel dockPanel)
        {
            PaintingService.CleanUp();
            base.CleanUp(dockPanel);
        }
    }

    /// <summary>
    /// IPaletteFactory palette used for WeifenLuo libraries.
    /// </summary>
    public class DockPaletteFactory : IPaletteFactory
    {
        XDocument _xml = new XDocument();

        public DockPaletteFactory(byte[] file) => _xml = XDocument.Load(new StreamReader(new MemoryStream(file)));

        public void Initialize(DockPanelColorPalette palette)
        {
            palette.AutoHideStripDefault.Background = GetColourFromElement("AutoHideTabBackgroundBegin");
            palette.AutoHideStripDefault.Border = GetColourFromElement("AutoHideTabBorder");
            palette.AutoHideStripDefault.Text = GetColourFromElement("AutoHideTabText");

            palette.AutoHideStripHovered.Background = GetColourFromElement("AutoHideTabMouseOverBackgroundBegin");
            palette.AutoHideStripHovered.Border = GetColourFromElement("AutoHideTabMouseOverBorder");
            palette.AutoHideStripHovered.Text = GetColourFromElement("AutoHideTabMouseOverText");

            palette.CommandBarMenuDefault.Background = GetColourFromElement("CommandShelfHighlightGradientBegin");
            palette.CommandBarMenuDefault.Text = GetColourFromElement("CommandBarTextActive");

            palette.CommandBarMenuPopupDefault.Arrow = GetColourFromElement("CommandBarMenuSubmenuGlyph");
            palette.CommandBarMenuPopupDefault.BackgroundBottom = GetColourFromElement("CommandBarMenuBackgroundGradientEnd");
            palette.CommandBarMenuPopupDefault.BackgroundTop = GetColourFromElement("CommandBarMenuBackgroundGradientBegin");
            palette.CommandBarMenuPopupDefault.Border = GetColourFromElement("CommandBarMenuBorder");
            palette.CommandBarMenuPopupDefault.Checkmark = GetColourFromElement("CommandBarCheckBox");
            palette.CommandBarMenuPopupDefault.CheckmarkBackground = GetColourFromElement("CommandBarSelectedIcon");
            palette.CommandBarMenuPopupDefault.IconBackground = GetColourFromElement("CommandBarMenuIconBackground");
            palette.CommandBarMenuPopupDefault.Separator = GetColourFromElement("CommandBarMenuSeparator");

            palette.CommandBarMenuPopupDisabled.Checkmark = GetColourFromElement("CommandBarCheckBoxDisabled");
            palette.CommandBarMenuPopupDisabled.CheckmarkBackground = GetColourFromElement("CommandBarSelectedIconDisabled");
            palette.CommandBarMenuPopupDisabled.Text = GetColourFromElement("CommandBarTextInactive");

            palette.CommandBarMenuPopupHovered.Arrow = GetColourFromElement("CommandBarMenuMouseOverSubmenuGlyph");
            palette.CommandBarMenuPopupHovered.Checkmark = GetColourFromElement("CommandBarCheckBoxMouseOver");
            palette.CommandBarMenuPopupHovered.CheckmarkBackground = GetColourFromElement("CommandBarHoverOverSelectedIcon");
            palette.CommandBarMenuPopupHovered.ItemBackground = GetColourFromElement("CommandBarMenuItemMouseOver");
            palette.CommandBarMenuPopupHovered.Text = GetColourFromElement("CommandBarMenuItemMouseOver", true);

            palette.CommandBarMenuTopLevelHeaderHovered.Background = GetColourFromElement("CommandBarMouseOverBackgroundBegin");
            palette.CommandBarMenuTopLevelHeaderHovered.Border = GetColourFromElement("CommandBarBorder");
            palette.CommandBarMenuTopLevelHeaderHovered.Text = GetColourFromElement("CommandBarTextHover");

            palette.CommandBarToolbarDefault.Background = GetColourFromElement("CommandBarGradientBegin");
            palette.CommandBarToolbarDefault.Border = GetColourFromElement("CommandBarToolBarBorder");
            palette.CommandBarToolbarDefault.Grip = GetColourFromElement("CommandBarDragHandle");
            palette.CommandBarToolbarDefault.OverflowButtonBackground = GetColourFromElement("CommandBarOptionsBackground");
            palette.CommandBarToolbarDefault.OverflowButtonGlyph = GetColourFromElement("CommandBarOptionsGlyph");
            palette.CommandBarToolbarDefault.Separator = GetColourFromElement("CommandBarToolBarSeparator");
            palette.CommandBarToolbarDefault.SeparatorAccent = GetColourFromElement("CommandBarToolBarSeparatorHighlight");
            palette.CommandBarToolbarDefault.Tray = GetColourFromElement("CommandShelfBackgroundGradientBegin");

            palette.CommandBarToolbarButtonChecked.Background = GetColourFromElement("CommandBarSelected");
            palette.CommandBarToolbarButtonChecked.Border = GetColourFromElement("CommandBarSelectedBorder");
            palette.CommandBarToolbarButtonChecked.Text = GetColourFromElement("CommandBarTextSelected");

            palette.CommandBarToolbarButtonCheckedHovered.Border = GetColourFromElement("CommandBarHoverOverSelectedIconBorder");
            palette.CommandBarToolbarButtonCheckedHovered.Text = GetColourFromElement("CommandBarTextHoverOverSelected");

            palette.CommandBarToolbarButtonDefault.Arrow = GetColourFromElement("DropDownGlyph");

            palette.CommandBarToolbarButtonHovered.Arrow = GetColourFromElement("DropDownMouseOverGlyph");
            palette.CommandBarToolbarButtonHovered.Separator = GetColourFromElement("CommandBarSplitButtonSeparator");

            palette.CommandBarToolbarButtonPressed.Arrow = GetColourFromElement("DropDownMouseDownGlyph");
            palette.CommandBarToolbarButtonPressed.Background = GetColourFromElement("CommandBarMouseDownBackgroundBegin");
            palette.CommandBarToolbarButtonPressed.Text = GetColourFromElement("CommandBarTextMouseDown");

            palette.CommandBarToolbarOverflowHovered.Background = GetColourFromElement("CommandBarOptionsMouseOverBackgroundBegin");
            palette.CommandBarToolbarOverflowHovered.Glyph = GetColourFromElement("CommandBarOptionsMouseOverGlyph");

            palette.CommandBarToolbarOverflowPressed.Background = GetColourFromElement("CommandBarOptionsMouseDownBackgroundBegin");
            palette.CommandBarToolbarOverflowPressed.Glyph = GetColourFromElement("CommandBarOptionsMouseDownGlyph");

            palette.OverflowButtonDefault.Glyph = GetColourFromElement("DocWellOverflowButtonGlyph");

            palette.OverflowButtonHovered.Background = GetColourFromElement("DocWellOverflowButtonMouseOverBackground");
            palette.OverflowButtonHovered.Border = GetColourFromElement("DocWellOverflowButtonMouseOverBorder");
            palette.OverflowButtonHovered.Glyph = GetColourFromElement("DocWellOverflowButtonMouseOverGlyph");

            palette.OverflowButtonPressed.Background = GetColourFromElement("DocWellOverflowButtonMouseDownBackground");
            palette.OverflowButtonPressed.Border = GetColourFromElement("DocWellOverflowButtonMouseDownBorder");
            palette.OverflowButtonPressed.Glyph = GetColourFromElement("DocWellOverflowButtonMouseDownGlyph");

            palette.TabSelectedActive.Background = GetColourFromElement("FileTabSelectedBorder");
            palette.TabSelectedActive.Button = GetColourFromElement("FileTabButtonSelectedActiveGlyph");
            palette.TabSelectedActive.Text = GetColourFromElement("FileTabSelectedText");

            palette.TabSelectedInactive.Background = GetColourFromElement("FileTabInactiveBorder");
            palette.TabSelectedInactive.Button = GetColourFromElement("FileTabButtonSelectedInactiveGlyph");
            palette.TabSelectedInactive.Text = GetColourFromElement("FileTabInactiveText");

            palette.TabUnselected.Text = GetColourFromElement("FileTabText");
            palette.TabUnselected.Background = GetColourFromElement("FileTabBackground");

            palette.TabUnselectedHovered.Background = GetColourFromElement("FileTabHotBorder");
            palette.TabUnselectedHovered.Button = GetColourFromElement("FileTabHotGlyph");
            palette.TabUnselectedHovered.Text = GetColourFromElement("FileTabHotText");

            palette.TabButtonSelectedActiveHovered.Background = GetColourFromElement("FileTabButtonHoverSelectedActive");
            palette.TabButtonSelectedActiveHovered.Border = GetColourFromElement("FileTabButtonHoverSelectedActiveBorder");
            palette.TabButtonSelectedActiveHovered.Glyph = GetColourFromElement("FileTabButtonHoverSelectedActiveGlyph");

            palette.TabButtonSelectedActivePressed.Background = GetColourFromElement("FileTabButtonDownSelectedActive");
            palette.TabButtonSelectedActivePressed.Border = GetColourFromElement("FileTabButtonDownSelectedActiveBorder");
            palette.TabButtonSelectedActivePressed.Glyph = GetColourFromElement("FileTabButtonDownSelectedActiveGlyph");

            palette.TabButtonSelectedInactiveHovered.Background = GetColourFromElement("FileTabButtonHoverSelectedInactive");
            palette.TabButtonSelectedInactiveHovered.Border = GetColourFromElement("FileTabButtonHoverSelectedInactiveBorder");
            palette.TabButtonSelectedInactiveHovered.Glyph = GetColourFromElement("FileTabButtonHoverSelectedInactiveGlyph");

            palette.TabButtonSelectedInactivePressed.Background = GetColourFromElement("FileTabButtonDownSelectedInactive");
            palette.TabButtonSelectedInactivePressed.Border = GetColourFromElement("FileTabButtonDownSelectedInactiveBorder");
            palette.TabButtonSelectedInactivePressed.Glyph = GetColourFromElement("FileTabButtonDownSelectedInactiveGlyph");

            palette.TabButtonUnselectedTabHoveredButtonHovered.Background = GetColourFromElement("FileTabButtonHoverInactive");
            palette.TabButtonUnselectedTabHoveredButtonHovered.Border = GetColourFromElement("FileTabButtonHoverInactiveBorder");
            palette.TabButtonUnselectedTabHoveredButtonHovered.Glyph = GetColourFromElement("FileTabButtonHoverInactiveGlyph");

            palette.TabButtonUnselectedTabHoveredButtonPressed.Background = GetColourFromElement("FileTabButtonDownInactive");
            palette.TabButtonUnselectedTabHoveredButtonPressed.Border = GetColourFromElement("FileTabButtonDownInactiveBorder");
            palette.TabButtonUnselectedTabHoveredButtonPressed.Glyph = GetColourFromElement("FileTabButtonDownInactiveGlyph");

            palette.MainWindowActive.Background = GetColourFromElement("EnvironmentBackground");
            palette.MainWindowStatusBarDefault.Background = GetColourFromElement("StatusBarDefault");
            palette.MainWindowStatusBarDefault.Highlight = GetColourFromElement("StatusBarHighlight");
            palette.MainWindowStatusBarDefault.HighlightText = GetColourFromElement("StatusBarHighlight", true);
            palette.MainWindowStatusBarDefault.ResizeGrip = GetColourFromElement("MainWindowResizeGripTexture1");
            palette.MainWindowStatusBarDefault.ResizeGripAccent = GetColourFromElement("MainWindowResizeGripTexture2");
            palette.MainWindowStatusBarDefault.Text = GetColourFromElement("StatusBarText");

            palette.ToolWindowCaptionActive.Background = GetColourFromElement("TitleBarActiveBorder");
            palette.ToolWindowCaptionActive.Button = GetColourFromElement("ToolWindowButtonActiveGlyph");
            palette.ToolWindowCaptionActive.Grip = GetColourFromElement("TitleBarDragHandleActive");
            palette.ToolWindowCaptionActive.Text = GetColourFromElement("TitleBarActiveText");

            palette.ToolWindowCaptionInactive.Background = GetColourFromElement("TitleBarInactive");
            palette.ToolWindowCaptionInactive.Button = GetColourFromElement("ToolWindowButtonInactiveGlyph");
            palette.ToolWindowCaptionInactive.Grip = GetColourFromElement("TitleBarDragHandle");
            palette.ToolWindowCaptionInactive.Text = GetColourFromElement("TitleBarInactiveText");

            palette.ToolWindowCaptionButtonActiveHovered.Background = GetColourFromElement("ToolWindowButtonHoverActive");
            palette.ToolWindowCaptionButtonActiveHovered.Border = GetColourFromElement("ToolWindowButtonHoverActiveBorder");
            palette.ToolWindowCaptionButtonActiveHovered.Glyph = GetColourFromElement("ToolWindowButtonHoverActiveGlyph");

            palette.ToolWindowCaptionButtonPressed.Background = GetColourFromElement("ToolWindowButtonDown");
            palette.ToolWindowCaptionButtonPressed.Border = GetColourFromElement("ToolWindowButtonDownBorder");
            palette.ToolWindowCaptionButtonPressed.Glyph = GetColourFromElement("ToolWindowButtonDownActiveGlyph");

            palette.ToolWindowCaptionButtonInactiveHovered.Background = GetColourFromElement("ToolWindowButtonHoverInactive");
            palette.ToolWindowCaptionButtonInactiveHovered.Border = GetColourFromElement("ToolWindowButtonHoverInactiveBorder");
            palette.ToolWindowCaptionButtonInactiveHovered.Glyph = GetColourFromElement("ToolWindowButtonHoverInactiveGlyph");

            palette.ToolWindowTabSelectedActive.Background = GetColourFromElement("ToolWindowTabSelectedTab");
            palette.ToolWindowTabSelectedActive.Text = GetColourFromElement("ToolWindowTabSelectedActiveText");

            palette.ToolWindowTabSelectedInactive.Background = palette.ToolWindowTabSelectedActive.Background;
            palette.ToolWindowTabSelectedInactive.Text = GetColourFromElement("ToolWindowTabSelectedText");

            palette.ToolWindowTabUnselected.Text = GetColourFromElement("ToolWindowTabText");
            palette.ToolWindowTabUnselected.Background = GetColourFromElement("ToolWindowTabGradientBegin");

            palette.ToolWindowTabUnselectedHovered.Background = GetColourFromElement("ToolWindowTabMouseOverBackgroundBegin");
            palette.ToolWindowTabUnselectedHovered.Text = GetColourFromElement("ToolWindowTabMouseOverText");

            palette.ToolWindowSeparator = GetColourFromElement("ToolWindowTabSeparator");
            palette.ToolWindowBorder = GetColourFromElement("ToolWindowBorder");

            palette.DockTarget.Background = GetColourFromElement("DockTargetBackground");
            palette.DockTarget.Border = GetColourFromElement("DockTargetBorder");
            palette.DockTarget.ButtonBackground = GetColourFromElement("DockTargetButtonBackgroundBegin");
            palette.DockTarget.ButtonBorder = GetColourFromElement("DockTargetButtonBorder");
            palette.DockTarget.GlyphBackground = GetColourFromElement("DockTargetGlyphBackgroundBegin");
            palette.DockTarget.GlyphArrow = GetColourFromElement("DockTargetGlyphArrow");
            palette.DockTarget.GlyphBorder = GetColourFromElement("DockTargetGlyphBorder");
        }

        private Color GetColourFromElement(string name, bool foreground = false)
        {
            var color = _xml.Root.Element("Theme")
                                 .Elements("Category").FirstOrDefault(item => item.Attribute("Name").Value == "Environment")?
                                 .Elements("Color").FirstOrDefault(item => item.Attribute("Name").Value == name)?
                                 .Element(foreground ? "Foreground" : "Background").Attribute("Source").Value;

            if (color == null) return Color.Transparent;

            return ColorTranslator.FromHtml($"#{color}");
        }
    }
}
