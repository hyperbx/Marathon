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
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using Marathon.Components.Helpers;

namespace Marathon.Components
{
	public partial class ButtonDark : Button
	{
		/// <summary>
		/// Initialiser for FormBackColour.
		/// </summary>
		private Color _FormBackColour;

		/// <summary>
		/// The colour of the form behind the button (used when transparency is enabled for the hover and pressed colours).
		/// </summary>
		[
			Category("Appearance"),
			Browsable(true),
			Description("The colour of the form behind the button (used when transparency is enabled for the hover and pressed colours).")
		]
		public Color FormBackColour
		{
			get => _FormBackColour;

            set
            {
				_FormBackColour = value;

				// Only change this if transparent.
				if (BackColor == Color.Transparent)
					FlatAppearance.MouseOverBackColor = FormBackColour.ChangeBrightness(17);
            }
		}

		/// <summary>
		/// Initialiser for Checked.
		/// </summary>
		private bool _Checked = false;

		/// <summary>
		/// Cached colour for mouse down.
		/// </summary>
		private Color _CheckedColourCache;

		/// <summary>
		/// Cached proportions of the button.
		/// </summary>
		private Rectangle _CheckedRectangleCache;

		/// <summary>
		/// Check state for the button.
		/// </summary>
		[Category("Behavior"), Browsable(true), Description("Check state for the button.")]
		public bool Checked
        {
			get => _Checked;

            set
			{
				// Checked...
				if (_Checked = value)
                {
					// Set new mouse down colour.
					FlatAppearance.MouseDownBackColor = FormBackColour;

					// Draw a border around the button.
					FlatAppearance.BorderSize = 1;

					// Increase size for border.
					Size = new Size(Width + 2, Height + 2);

					// Shift the button to the top left.
					Location = new Point(Location.X - 1, Location.Y - 1);
				}

				// Unchecked...
				else
                {
					// Restore original mouse down colour.
					FlatAppearance.MouseDownBackColor = _CheckedColourCache;

					// Remove the border.
					FlatAppearance.BorderSize = 0;

					// Perform only if the rectangle cache is populated.
					if (_CheckedRectangleCache != new Rectangle())
					{
						// Restore original size.
						Size = _CheckedRectangleCache.Size;

						// Restore original location.
						Location = _CheckedRectangleCache.Location;
					}
				}
            }
        }

		public ButtonDark()
		{
			InitializeComponent();

			FlatStyle = FlatStyle.Flat;
			FlatAppearance.BorderSize = 0;
			FlatAppearance.MouseOverBackColor = Color.FromArgb(71, 71, 71);
			FlatAppearance.MouseDownBackColor = Color.FromArgb(94, 94, 94);

			ForeColor = SystemColors.Control;
			BackColor = Color.FromArgb(32, 32, 32);
		}

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

			// Cache the original mouse down colour.
			_CheckedColourCache = FlatAppearance.MouseDownBackColor;

			// Cache the original button proportions.
			_CheckedRectangleCache = new Rectangle(Location, Size);
		}
    }
}
