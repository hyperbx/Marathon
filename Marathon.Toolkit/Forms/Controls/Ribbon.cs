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
using System.Windows.Forms.Design;
using System.ComponentModel;
using Marathon.Toolkit.Components;

namespace Marathon.Toolkit.Controls
{
    [Designer(typeof(RibbonDesigner))]
    public partial class Ribbon : UserControl
    {
        /// <summary>
        /// Passthrough control for designer support.
        /// </summary>
        [Category("Appearance"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TabControlFlat WorkingArea => TabControlFlat_Ribbon;

        /// <summary>
        /// Initialiser for RibbonVisible.
        /// </summary>
        private bool _RibbonVisible = true;

        /// <summary>
        /// Displays the ribbon.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("Displays the ribbon.")]
        public bool RibbonVisible
        {
            get => _RibbonVisible;

            set
            {
                _RibbonVisible = value;

                if (!Program.RunningInDesigner())
                {
                    // Change button graphic state.
                    ButtonFlat_Visibility.BackgroundImage = value ?
                                                            Resources.LoadBitmapResource(nameof(Properties.Resources.Ribbon_ExpandUp)) :
                                                            Resources.LoadBitmapResource(nameof(Properties.Resources.Ribbon_ExpandDown));

                    // Change size to hide the tabs.
                    Size = value ? SizeCache : Size = new Size(SizeCache.Width, 22);
                }
            }
        }

        /// <summary>
        /// The font used to display text in the control.
        /// </summary>
        [Browsable(false)]
        public override Font Font => new Font("Segoe UI", 8F);

        /// <summary>
        /// Cached size for ribbon visibility.
        /// </summary>
        private Size SizeCache;

        public Ribbon()
        {
            InitializeComponent();

            Dock = DockStyle.Top;

            SizeCache = Size;
        }

        protected override void OnResize(EventArgs e)
        {
            // FUCKING WINFORMS!
            ButtonFlat_Visibility.Left = Width - ButtonFlat_Visibility.Width;

            base.OnResize(e);
        }

        private void TabControlFlat_Ribbon_ControlAdded(object sender, ControlEventArgs e)
        {
            // Removes all padding.
            e.Control.Padding = e.Control.Margin = Padding.Empty;

            // Sets up new tabs with the ribbon colour.
            e.Control.BackColor = TabControlFlat_Ribbon.TabPageBackColour;
        }

        private void ButtonFlat_Visibility_Click(object sender, EventArgs e)
        {
            // Flip the visibility Boolean.
            RibbonVisible = !RibbonVisible;
        }

        private void TabControlFlat_Ribbon_MouseClick(object sender, MouseEventArgs e)
        {
            // Expand the ribbon if a tab is selected.
            if (e.Button == MouseButtons.Left && !RibbonVisible)
                RibbonVisible = true;
        }
    }

    public class RibbonDesigner : ParentControlDesigner
    {
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            if (Control is Ribbon ribbon)
            {
                // Allows the designer to communicate with the TabControlFlat control.
                EnableDesignMode(ribbon.WorkingArea, "WorkingArea");
            }
        }
    }
}
