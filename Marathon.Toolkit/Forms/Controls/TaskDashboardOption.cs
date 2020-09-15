using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace Marathon.Toolkit.Controls
{
    public partial class TaskDashboardOption : UserControl
    {
        private string _TaskName, _TaskDescription;

        [Description("The name of the task.")]
        public string TaskName
        {
            get => _TaskName;

            set => RadioButton_Task.Text = _TaskName = value;
        }

        [Description("The description given to the task.")]
        public string TaskDescription
        {
            get => _TaskDescription;

            set
            {
                Label_TaskDescription.Text = _TaskDescription = value;
                Width = Label_TaskDescription.Width + 20;
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
