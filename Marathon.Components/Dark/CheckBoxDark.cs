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
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using Marathon.Components.Helpers;

namespace Marathon.Components
{
    /// <summary>
    /// Determines how the check box is drawn.
    /// </summary>
    public enum CheckBoxStyle
    {
        /// <summary>
        /// Fills the check box with a square.
        /// </summary>
        Square,

        /// <summary>
        /// Fits a check in the bounds of the check box.
        /// </summary>
        Check
    }

    [DefaultEvent("CheckedChanged"), Designer(typeof(DynamicSizeControlDesigner))]
    public partial class CheckBoxDark : UserControl
    {
        // Mouse flags.
        private bool MouseDownFlag = false,
                     MouseHoverFlag = false;

        // Check box bounds.
        Rectangle CheckBoxBounds = new Rectangle(2, 4, 13, 13);

        /// <summary>
        /// Initialiser for Label.
        /// </summary>
        private string _Label;

        /// <summary>
        /// The text associated with the control.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The text associated with the control.")]
        public string Label
        {
            get => _Label;

            set
            {
                _Label = value;

                // Set new width based on new string.
                Width = CheckBoxBounds.Width + TextRenderer.MeasureText(value, Font).Width + 5;

                // Refresh the control to update in real-time.
                Refresh();
            }
        }

        /// <summary>
        /// Initailiser for Enabled.
        /// </summary>
        private bool _Enabled = true;

        /// <summary>
        /// Indicates whether the control is enabled.
        /// </summary>
        [Category("Behavior"), Browsable(true), Description("Indicates whether the control is enabled.")]
        new public bool Enabled
        {
            get => _Enabled;

            set
            {
                _Enabled = value;

                // Refresh the control to update in real-time.
                Refresh();
            }
        }

        /// <summary>
        /// Initialiser for Checked.
        /// </summary>
        private bool _Checked = false;

        /// <summary>
        /// Indicates whether the component is in the checked state.
        /// </summary>
        [Category("Behavior"), Browsable(true), Description("Indicates whether the component is in the checked state.")]
        public bool Checked
        {
            get => _Checked;

            set
            {
                _Checked = value;

                // Invoke checked changed event.
                CheckedChanged?.Invoke(this, new EventArgs());

                // Refresh the control to update in real-time.
                Refresh();
            }
        }

        /// <summary>
        /// Initialiser for DisabledForeColour.
        /// </summary>
        private Color _DisabledForeColour = SystemColors.GrayText;

        /// <summary>
        /// The colour of the label when disabled.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the label when disabled.")]
        public Color DisabledForeColour
        {
            get => _DisabledForeColour;

            set
            {
                _DisabledForeColour = value;

                // Refresh the control to update in real-time.
                Refresh();
            }
        }

        /// <summary>
        /// Initialiser for CheckBoxBackColour.
        /// </summary>
        private Color _CheckBoxBackColour;

        /// <summary>
        /// The colour of the check box.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the check box.")]
        public Color CheckBoxBackColour
        {
            get => _CheckBoxBackColour;

            set
            {
                _CheckBoxBackColour = value;

                // Refresh the control to update in real-time.
                Refresh();
            }
        }

        /// <summary>
        /// Initialiser for CheckBoxOutlineColour.
        /// </summary>
        private Color _CheckBoxOutlineColour = Color.FromArgb(51, 51, 51);

        /// <summary>
        /// The colour of the outline of the check box.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the outline of the check box.")]
        public Color CheckBoxOutlineColour
        {
            get => _CheckBoxOutlineColour;

            set
            {
                _CheckBoxOutlineColour = value;

                // Refresh the control to update in real-time.
                Refresh();
            }
        }

        /// <summary>
        /// Initialiser for CheckBoxCheckedColour.
        /// </summary>
        private Color _CheckBoxCheckedColour = Color.FromArgb(226, 226, 226);

        /// <summary>
        /// The colour of the rectangle used for the checked state.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the rectangle used for the checked state.")]
        public Color CheckBoxCheckedColour
        {
            get => _CheckBoxCheckedColour;

            set
            {
                _CheckBoxCheckedColour = value;

                // Refresh the control to update in real-time.
                Refresh();
            }
        }

        /// <summary>
        /// Initialiser for CheckBoxIndeterminateColour.
        /// </summary>
        private Color _CheckBoxIndeterminateColour = Color.FromArgb(107, 107, 107);

        /// <summary>
        /// The colour of the rectangle used for the indeterminate state.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the rectangle used for the indeterminate state.")]
        public Color CheckBoxIndeterminateColour
        {
            get => _CheckBoxIndeterminateColour;

            set
            {
                _CheckBoxIndeterminateColour = value;

                // Refresh the control to update in real-time.
                Refresh();
            }
        }

        /// <summary>
        /// Initialiser for RightAlign.
        /// </summary>
        private bool _RightAlign = false;

        /// <summary>
        /// Determines if the check box is drawn on the right of the control.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("Determines if the check box is drawn on the right of the control.")]
        public bool RightAlign
        {
            get => _RightAlign;

            set
            {
                _RightAlign = value;

                // Refresh the control to update in real-time.
                Refresh();
            }
        }

        /// <summary>
        /// Causes the check box to automatically change state when clicked.
        /// </summary>
        [Category("Behavior"), Browsable(true), Description("Causes the check box to automatically change state when clicked.")]
        public bool AutoCheck { get; set; } = true;

        /// <summary>
        /// Indicates whether the CheckBox will allow three check states rather than two.
        /// </summary>
        [Category("Behavior"), Browsable(true), Description("Indicates whether the CheckBox will allow three check states rather than two.")]
        public bool ThreeState { get; set; } = false;

        /// <summary>
        /// Determines if the checked state is indeterminate.
        /// </summary>
        [Category("Behavior"), Browsable(true), Description("Determines if the checked state is indeterminate.")]
        public bool Indeterminate { get; set; } = false;

        /// <summary>
        /// Determines how the check box is drawn.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("Determines how the check box is drawn.")]
        public CheckBoxStyle CheckStyle { get; set; } = CheckBoxStyle.Check;

        /// <summary>
        /// Event handler for when the check state has been modified.
        /// </summary>
        public event EventHandler CheckedChanged;

        public CheckBoxDark()
        {
            InitializeComponent();

            // Set colours.
            BackColor = CheckBoxBackColour = Color.FromArgb(25, 25, 25);
            ForeColor = SystemColors.Control;

            // Set properties.
            Label = Name;
        }

        private void DrawCheckBoxRectangle(Graphics g, Rectangle checkBox)
        {
            // Fill outline rectangle.
            g.FillRectangle
            (
                new SolidBrush
                (
                    // Check mouse hover flag.
                    MouseHoverFlag ?

                    // Check mouse down flag.
                    MouseDownFlag ?

                    // Use default outline colour if mouse down.
                    CheckBoxOutlineColour :

                    // Use slightly brighter alternative if mouse hover.
                    CheckBoxOutlineColour.ChangeBrightness(50) :

                    // Use default outline colour.
                    CheckBoxOutlineColour
                ),

                checkBox
            );

            // Inflate rectangle for inner rectangle.
            checkBox.Inflate(-1, -1);

            // Fill with back colour to create outline.
            g.FillRectangle(new SolidBrush(CheckBoxBackColour), checkBox);

            // Inflate rectangle again for the checked states.
            checkBox.Inflate(-1, -1);

            // Fill checked colour.
            if (Checked)
            {
                if (Indeterminate)
                {
                    // Draw indeterminate colour.
                    DrawCheckedShape(g, CheckBoxIndeterminateColour, checkBox);
                }
                else
                {
                    // Draw checked colour.
                    DrawCheckedShape(g, CheckBoxCheckedColour, checkBox);
                }
            }
        }

        private void DrawCheckedShape(Graphics g, Color colour, Rectangle checkBox)
        {
            // Draw a square if indeterminate.
            CheckBoxStyle checkStyle = Indeterminate ? CheckBoxStyle.Square : CheckStyle;

            switch (checkStyle)
            {
                case CheckBoxStyle.Square:
                {
                    // Draw rectangle.
                    g.FillRectangle(new SolidBrush(colour), checkBox);

                    break;
                }

                case CheckBoxStyle.Check:
                {
                    // Stored for easier reference.
                    Color shadowColour = colour.ChangeBrightness(35);

                    // Set anti-alias quality.
                    g.SmoothingMode = SmoothingMode.HighQuality;

                    // Draw check mark shadow.
                    g.DrawLine(new Pen(shadowColour, 1), checkBox.X, checkBox.Y + 5, checkBox.X + 3, checkBox.Y + 7);
                    g.DrawLine(new Pen(shadowColour, 1), checkBox.X + 3, checkBox.Y + 7, checkBox.X + 8, checkBox.Y + 2);

                    // Draw check mark.
                    g.DrawLine(new Pen(colour, 1), checkBox.X, checkBox.Y + 4, checkBox.X + 2, checkBox.Y + 6);
                    g.DrawLine(new Pen(colour, 1), checkBox.X + 3, checkBox.Y + 6, checkBox.X + 8, checkBox.Y + 1);

                    break;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Set check box location based on alignment.
            CheckBoxBounds.X = RightAlign ? e.ClipRectangle.Width - (CheckBoxBounds.Width + 2) : 2;

            // Offset for the height of the text.
            int heightOffset = e.ClipRectangle.Y + (CheckBoxBounds.Height / 4);

            // Draw the text separately to use the proper colours.
            TextRenderer.DrawText
            (
                e.Graphics,
                Label,
                Font,

                RightAlign ?

                // Draw text on the right.
                new Point
                (
                    e.ClipRectangle.Width - (CheckBoxBounds.Width + 5) - TextRenderer.MeasureText(Label, Font).Width,
                    heightOffset
                ) :

                // Draw text on the left.
                new Point
                (
                    e.ClipRectangle.X + CheckBoxBounds.Width + 5,
                    heightOffset
                ),

                // Set colour based on enabled state.
                Enabled ? ForeColor : DisabledForeColour,

                // Set text anchor.
                TextFormatFlags.Left
            );

            // Draw rectangle.
            DrawCheckBoxRectangle(e.Graphics, CheckBoxBounds);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            // Don't do anything.
            if (!Enabled)
                return;

            // Set the mouse down flag.
            MouseDownFlag = true;

            // Refresh the control to update in real-time.
            Refresh();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            // Don't do anything.
            if (!Enabled)
                return;

            // Disable the mouse down flag.
            MouseDownFlag = false;

            if (Checked)
            {
                // Don't check the check box.
                if (!AutoCheck)
                    return;

                // Set indeterminate flag based on ThreeState.
                if (ThreeState & !Indeterminate)
                {
                    // Make indeterminate.
                    Checked = Indeterminate = true;
                }
                else
                {
                    // Uncheck and make determinate.
                    Checked = Indeterminate = false;
                }
            }
            else
            {
                // Check the check box.
                Checked = true;
            }
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);

            // Don't do anything.
            if (!Enabled)
                return;

            // Set the mouse hover flag.
            MouseHoverFlag = true;

            // Refresh the control to update in real-time.
            Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            // Don't do anything.
            if (!Enabled)
                return;

            // Disable the mouse hover flag.
            MouseHoverFlag = false;

            // Refresh the control to update in real-time.
            Refresh();
        }
    }
}