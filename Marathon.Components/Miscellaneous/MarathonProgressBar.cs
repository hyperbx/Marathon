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
using Marathon.Helpers;
using WinFormAnimation;
using System;

namespace Marathon.Components
{
    public partial class MarathonProgressBar : UserControl
    {
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
                _ProgressColour = Panel_Progress.BackColor = value;

                // Refresh the control to update in real-time.
                Refresh();
            }
        }

        /// <summary>
        /// Use marquee appearance for uncertain progress.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("Use marquee appearance for uncertain progress.")]
        public bool Marquee { get; set; } = false;

        /// <summary>
        /// The speed of the marquee animation in milliseconds.
        /// </summary>
        [Category("Behavior"), Browsable(true), Description("The speed of the marquee animation in milliseconds.")]
        public ulong MarqueeSpeed { get; set; } = 2000;


        /// <summary>
        /// The width of the marquee bar.
        /// </summary>
        [Category("Appearance"), Browsable(true), Description("The width of the marquee bar.")]
        public int MarqueeWidth { get; set; } = 128;

        /// <summary>
        /// The value of the current progress.
        /// </summary>
        [Category("Behavior"), Browsable(true), Description("The value of the current progress.")]
        public int Progress { get; set; } = 0;

        public MarathonProgressBar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates the marquee animator and plays.
        /// </summary>
        private void StartMarquee()
        {
            new Animator
            (
                new Path
                (
                    Width * -1,
                    Width,
                    MarqueeSpeed
                ),

                FPSLimiterKnownValues.LimitSixty
            )
            {
                Repeat = true
            }
            .Play(Panel_Progress, "Left");
        }

        protected override void OnCreateControl()
        {
            if (!DesignHelper.RunningInDesigner())
            {
                // Progress * (Width / 100)

                if (Marquee)
                {
                    // Bar has a reason to be displayed.
                    Panel_Progress.Visible = true;

                    // Set marquee bar width.
                    Panel_Progress.Width = MarqueeWidth;

                    // Create marquee animator.
                    StartMarquee();
                }
            }

            base.OnCreateControl();
        }

        protected override void OnResize(EventArgs e)
        {
            // Update marquee animator.
            StartMarquee();

            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }
    }
}
