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
using System.Windows.Forms;

namespace Marathon.Toolkit.Controls
{
    public partial class TaskDashboardOption : UserControl
    {
        private string _TaskName, _TaskDescription;

        /// <summary>
        /// The name of the task.
        /// </summary>
        public string TaskName
        {
            get => _TaskName;

            set => RadioButton_Task.Text = _TaskName = value;
        }

        /// <summary>
        /// The description given to the task.
        /// </summary>
        public string TaskDescription
        {
            get => _TaskDescription;

            set
            {
                Label_Description.Text = _TaskDescription = value;
                Width = Label_Description.Width + 20;
            }
        }

        public TaskDashboardOption() => InitializeComponent();

        /// <summary>
        /// Custom event handler for external use.
        /// </summary>
        public event EventHandler Activated;

        /// <summary>
        /// Invokes the custom event handler when the task is selected.
        /// </summary>
        private void RadioButton_Task_CheckedChanged(object sender, EventArgs e) => Activated?.Invoke(sender, e);

        /// <summary>
        /// Sets the task checked state if its inner region is clicked.
        /// </summary>
        private void QuickDashboardTask_Click_Group(object sender, EventArgs e) => RadioButton_Task.Checked = true;
    }
}
