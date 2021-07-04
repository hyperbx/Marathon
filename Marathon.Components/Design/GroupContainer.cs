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

using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System;

namespace Marathon.Components
{
    [Designer(typeof(GroupContainerDesigner))]
    public partial class GroupContainer : UserControl
    {
        /// <summary>
        /// Initialiser for HeaderText.
        /// </summary>
        private string _HeaderText = "Placeholder";

        /// <summary>
        /// The text associated with the control.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The text associated with the control.")]
        public string HeaderText
        {
            get => _HeaderText;

            set
            {
                // Update label text.
                Label_Header.Text = _HeaderText = value;
            }
        }

        /// <summary>
        /// Initialiser for BorderColour.
        /// </summary>
        private Color _BorderColour = Color.FromArgb(51, 51, 51);

        /// <summary>
        /// The colour of the border for the control.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the border for the control.")]
        public Color BorderColour
        {
            get => _BorderColour;

            set
            {
                _BorderColour = value;

                // Refresh the control to update in real-time.
                Refresh();
            }
        }

        /// <summary>
        /// Working area for designer support.
        /// </summary>
        [Category("Appearance"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Panel WorkingArea
            => Panel_Container;

        public GroupContainer()
            => InitializeComponent();

        protected override void OnPaint(PaintEventArgs e)
        {
            // Draw a rectangle around the control using the requested border colour.
            e.Graphics.DrawRectangle(new Pen(BorderColour, 1), 0, 0, Width - 1, Height - 1);

            base.OnPaint(e);
        }

        protected override void OnResize(EventArgs e)
        {
            // Refresh the control to update in real-time.
            Refresh();

            base.OnResize(e);
        }
    }

    public class GroupContainerDesigner : ParentControlDesigner
    {
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            EnableDesignMode(((GroupContainer)Control).WorkingArea, "WorkingArea");
        }
    }
}
