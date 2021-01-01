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
using Marathon.Helpers;
using Marathon.Components.Helpers;

namespace Marathon.Components
{
    public partial class MarathonProgressBar : UserControl
    {
        private bool AnimationFlag = false;

        /// <summary>
        /// Initialiser for ProgressColour.
        /// </summary>
        private Color _ProgressColour = Color.FromArgb(6, 176, 37);

        /// <summary>
        /// The colour of the progress bar.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The colour of the progress bar.")]
        public Color ProgressColour
        {
            get => _ProgressColour;

            set
            {
                _ProgressColour = value;

                // Refresh the control to update in real-time.
                Refresh();
            }
        }

        /// <summary>
        /// The value of the current progress.
        /// </summary>
        [Category("Behavior"), Browsable(true), Description("The value of the current progress.")]
        public int Progress { get; set; }

        public MarathonProgressBar()
        {
            InitializeComponent();

            if (!DesignHelper.RunningInDesigner())
            {
                // Start animation timer.
                Timer_Animation.Start();
            }
        }

        /// <summary>
        /// Sets the animation flag upon tick.
        /// </summary>
        private void Timer_Animation_Tick(object sender, EventArgs e)
        {
            // Set animation flag.
            AnimationFlag = true;

            // Refresh the control to update in real-time.
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // Calculated progress rectangle.
            RectangleF progress = new RectangleF(0, 0, Progress * (Width / 100), Height);

            // Draw progress rectangle.
            e.Graphics.FillRectangle(new SolidBrush(ProgressColour), progress);

            if (AnimationFlag)
            {
                // X axis.
                int x = 0;

                // Frame timer.
                Timer frames = new Timer
                {
                    Interval = 5
                };

                // Start frame timer.
                frames.Start();

                frames.Tick += delegate
                {
                    // Move X axis.
                    x++;

                    // Draw progress rectangle highlight.
                    e.Graphics.FillRectangle(new SolidBrush(ProgressColour.ChangeBrightness(100)), new RectangleF(x, 0, progress.Width, progress.Height));
                };

                AnimationFlag = false;
            }

            base.OnPaint(e);
        }
    }
}
