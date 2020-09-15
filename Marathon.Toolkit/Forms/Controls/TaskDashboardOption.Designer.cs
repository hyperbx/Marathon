namespace Marathon.Toolkit.Controls
{
    partial class TaskDashboardOption
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.RadioButton_Task = new System.Windows.Forms.RadioButton();
            this.Label_TaskDescription = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // RadioButton_Task
            // 
            this.RadioButton_Task.AutoSize = true;
            this.RadioButton_Task.Location = new System.Drawing.Point(4, 3);
            this.RadioButton_Task.Name = "RadioButton_Task";
            this.RadioButton_Task.Size = new System.Drawing.Size(129, 19);
            this.RadioButton_Task.TabIndex = 0;
            this.RadioButton_Task.TabStop = true;
            this.RadioButton_Task.Text = "Nothing to see here";
            this.RadioButton_Task.UseVisualStyleBackColor = true;
            this.RadioButton_Task.CheckedChanged += new System.EventHandler(this.RadioButton_Task_CheckedChanged);
            // 
            // Label_TaskDescription
            // 
            this.Label_TaskDescription.AutoSize = true;
            this.Label_TaskDescription.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_TaskDescription.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Label_TaskDescription.Location = new System.Drawing.Point(20, 23);
            this.Label_TaskDescription.Name = "Label_TaskDescription";
            this.Label_TaskDescription.Size = new System.Drawing.Size(322, 15);
            this.Label_TaskDescription.TabIndex = 1;
            this.Label_TaskDescription.Text = "This is placeholder text intended to describe the current task.";
            this.Label_TaskDescription.Click += new System.EventHandler(this.QuickDashboardTask_Click_Group);
            // 
            // TaskDashboardOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.Controls.Add(this.Label_TaskDescription);
            this.Controls.Add(this.RadioButton_Task);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Name = "TaskDashboardOption";
            this.Size = new System.Drawing.Size(350, 44);
            this.Click += new System.EventHandler(this.QuickDashboardTask_Click_Group);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton RadioButton_Task;
        private System.Windows.Forms.Label Label_TaskDescription;
    }
}
