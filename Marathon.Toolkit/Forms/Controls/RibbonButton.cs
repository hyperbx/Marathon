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
using System.Collections.Generic;
using Marathon.Toolkit.Helpers;

namespace Marathon.Toolkit.Controls
{
    [DefaultEvent("Click")]
    public partial class RibbonButton : UserControl
    {
        /// <summary>
        /// The text used for the button.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The text used for the button.")]
        public string Caption
        {
            get => LabelDark_Text.Text;
            set => LabelDark_Text.Text = value;
        }

        /// <summary>
        /// The image used for the button.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The image used for the button.")]
        public Bitmap ButtonImage
        {
            get => PictureBox_Image.BackgroundImage == null ?
                                                       Resources.LoadBitmapResource(nameof(Properties.Resources.Placeholder)) : // Return the placeholder icon.
                                                       new Bitmap(PictureBox_Image.BackgroundImage);
            
            set => PictureBox_Image.BackgroundImage = value;
        }

        /// <summary>
        /// Cached bitmap for original colours.
        /// </summary>
        private Bitmap BitmapCache;

        /// <summary>
        /// Cached colour palettes.
        /// </summary>
        private Dictionary<string, Color> ColourPalettes = new Dictionary<string, Color>();

        /// <summary>
        /// Default event handler fired upon clicking.
        /// </summary>
        public event EventHandler Click;

        public RibbonButton()
        {
            InitializeComponent();

            // Store the original image in case we change enabled state.
            BitmapCache = ButtonImage;

            // Remove default margin.
            Margin = Padding.Empty;

            // Create colour palettes based on original background colour.
            ColourPalettes.Add("Default", BackColor);
            ColourPalettes.Add("Hover", Color.FromArgb(ColourPalettes["Default"].R + 39, ColourPalettes["Default"].G + 39, ColourPalettes["Default"].B + 39));
            ColourPalettes.Add("Engaged", Color.FromArgb(ColourPalettes["Default"].R + 76, ColourPalettes["Default"].G + 76, ColourPalettes["Default"].B + 76));
        }

        private void RibbonButton_EnabledChanged(object sender, EventArgs e)
        {
            // Change bitmap colour based on enabled state.
            ButtonImage = Enabled ? BitmapCache : BitmapHelper.MakeGreyscale(ButtonImage);
        }

        private void Controls_MouseLeave_Group(object sender, EventArgs e)
        {
            // Reset the brightness of the control.
            BackColor = ColourPalettes["Default"];
        }

        private void Controls_MouseEnter_Group(object sender, EventArgs e)
        {
            // Increase the brightness of the control.
            BackColor = ColourPalettes["Hover"];
        }

        private void Controls_MouseDown_Group(object sender, MouseEventArgs e)
        {
            // Increase the brightness of the control even more.
            if (e.Button == MouseButtons.Left)
                BackColor = ColourPalettes["Engaged"];
        }

        private void Controls_MouseUp_Group(object sender, MouseEventArgs e)
        {
            // Keep the hover colour until MouseLeave is fired.
            if (e.Button == MouseButtons.Left)
            {
                BackColor = ColourPalettes["Hover"];

                // Invoke the default event handler.
                Click?.Invoke(sender, e);
            }
        }
    }
}
