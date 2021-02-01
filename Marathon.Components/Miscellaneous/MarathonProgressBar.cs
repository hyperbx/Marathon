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
using Marathon.Helpers;
using Marathon.Components.Helpers;
using Transitions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
        /// The value of the current progress.
        /// </summary>
        [Category("Behavior"), Browsable(true), Description("The value of the current progress.")]
        public int Progress { get; set; }

        public MarathonProgressBar()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            Transition barScroll = new Transition(new TransitionType_EaseInEaseOut(2000));
            barScroll.add(Panel_Progress, "Left", Width);

            Transition barScroll2 = new Transition(new TransitionType_EaseInEaseOut(1));
            barScroll2.add(Panel_Progress, "Left", Width * -1);

            Transition.runLoopingChain(barScroll, barScroll2);

            base.OnCreateControl();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!DesignHelper.RunningInDesigner())
            {
                // Progress * (Width / 100)

            }

            base.OnPaint(e);
        }
    }
}
