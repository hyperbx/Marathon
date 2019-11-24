namespace Toolkit.Logs
{
    partial class ToolkitSessionLog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolkitSessionLog));
            this.list_Logs = new System.Windows.Forms.ListBox();
            this.lbl_RefreshText = new System.Windows.Forms.Label();
            this.nud_RefreshTimer = new System.Windows.Forms.NumericUpDown();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.tm_RefreshLogs = new System.Windows.Forms.Timer(this.components);
            this.btn_TimerEnabled = new System.Windows.Forms.Button();
            this.mstrip_Options = new System.Windows.Forms.MenuStrip();
            this.main_Options = new System.Windows.Forms.ToolStripMenuItem();
            this.options_ListPriority = new System.Windows.Forms.ToolStripMenuItem();
            this.priority_TopToBottom = new System.Windows.Forms.ToolStripMenuItem();
            this.priority_BottomToTop = new System.Windows.Forms.ToolStripMenuItem();
            this.options_Timestamps = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Refresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nud_RefreshTimer)).BeginInit();
            this.mstrip_Options.SuspendLayout();
            this.SuspendLayout();
            // 
            // list_Logs
            // 
            this.list_Logs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_Logs.FormattingEnabled = true;
            this.list_Logs.Location = new System.Drawing.Point(-1, -1);
            this.list_Logs.Name = "list_Logs";
            this.list_Logs.Size = new System.Drawing.Size(609, 329);
            this.list_Logs.TabIndex = 0;
            this.list_Logs.SelectedIndexChanged += new System.EventHandler(this.List_Logs_SelectedIndexChanged);
            // 
            // lbl_RefreshText
            // 
            this.lbl_RefreshText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_RefreshText.AutoSize = true;
            this.lbl_RefreshText.Location = new System.Drawing.Point(81, 335);
            this.lbl_RefreshText.Name = "lbl_RefreshText";
            this.lbl_RefreshText.Size = new System.Drawing.Size(182, 13);
            this.lbl_RefreshText.TabIndex = 1;
            this.lbl_RefreshText.Text = "Refresh every:                      seconds";
            // 
            // nud_RefreshTimer
            // 
            this.nud_RefreshTimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nud_RefreshTimer.Location = new System.Drawing.Point(158, 331);
            this.nud_RefreshTimer.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nud_RefreshTimer.Name = "nud_RefreshTimer";
            this.nud_RefreshTimer.Size = new System.Drawing.Size(55, 20);
            this.nud_RefreshTimer.TabIndex = 2;
            this.nud_RefreshTimer.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_RefreshTimer.ValueChanged += new System.EventHandler(this.Nud_RefreshTimer_ValueChanged);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Clear.BackColor = System.Drawing.Color.Tomato;
            this.btn_Clear.FlatAppearance.BorderSize = 0;
            this.btn_Clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Clear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Clear.Location = new System.Drawing.Point(530, 330);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(75, 23);
            this.btn_Clear.TabIndex = 62;
            this.btn_Clear.Text = "Clear";
            this.btn_Clear.UseVisualStyleBackColor = false;
            this.btn_Clear.Click += new System.EventHandler(this.Btn_Clear_Click);
            // 
            // tm_RefreshLogs
            // 
            this.tm_RefreshLogs.Tick += new System.EventHandler(this.Tm_RefreshLogs_Tick);
            // 
            // btn_TimerEnabled
            // 
            this.btn_TimerEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_TimerEnabled.BackColor = System.Drawing.Color.SkyBlue;
            this.btn_TimerEnabled.FlatAppearance.BorderSize = 0;
            this.btn_TimerEnabled.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_TimerEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_TimerEnabled.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_TimerEnabled.Location = new System.Drawing.Point(453, 330);
            this.btn_TimerEnabled.Name = "btn_TimerEnabled";
            this.btn_TimerEnabled.Size = new System.Drawing.Size(75, 23);
            this.btn_TimerEnabled.TabIndex = 63;
            this.btn_TimerEnabled.Text = "Pause";
            this.btn_TimerEnabled.UseVisualStyleBackColor = false;
            this.btn_TimerEnabled.Click += new System.EventHandler(this.Btn_TimerEnabled_Click);
            // 
            // mstrip_Options
            // 
            this.mstrip_Options.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mstrip_Options.BackColor = System.Drawing.SystemColors.Control;
            this.mstrip_Options.Dock = System.Windows.Forms.DockStyle.None;
            this.mstrip_Options.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.main_Options});
            this.mstrip_Options.Location = new System.Drawing.Point(381, 330);
            this.mstrip_Options.Name = "mstrip_Options";
            this.mstrip_Options.Size = new System.Drawing.Size(69, 24);
            this.mstrip_Options.TabIndex = 65;
            this.mstrip_Options.Text = "menuStrip1";
            // 
            // main_Options
            // 
            this.main_Options.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.options_ListPriority,
            this.options_Timestamps});
            this.main_Options.Name = "main_Options";
            this.main_Options.Size = new System.Drawing.Size(61, 20);
            this.main_Options.Text = "Options";
            // 
            // options_ListPriority
            // 
            this.options_ListPriority.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.priority_TopToBottom,
            this.priority_BottomToTop});
            this.options_ListPriority.Name = "options_ListPriority";
            this.options_ListPriority.Size = new System.Drawing.Size(176, 22);
            this.options_ListPriority.Text = "List Priority";
            // 
            // priority_TopToBottom
            // 
            this.priority_TopToBottom.CheckOnClick = true;
            this.priority_TopToBottom.Name = "priority_TopToBottom";
            this.priority_TopToBottom.Size = new System.Drawing.Size(150, 22);
            this.priority_TopToBottom.Text = "Top to Bottom";
            this.priority_TopToBottom.CheckedChanged += new System.EventHandler(this.Priority_TopToBottom_CheckedChanged);
            // 
            // priority_BottomToTop
            // 
            this.priority_BottomToTop.CheckOnClick = true;
            this.priority_BottomToTop.Name = "priority_BottomToTop";
            this.priority_BottomToTop.Size = new System.Drawing.Size(150, 22);
            this.priority_BottomToTop.Text = "Bottom to Top";
            this.priority_BottomToTop.CheckedChanged += new System.EventHandler(this.Priority_BottomToTop_CheckedChanged);
            // 
            // options_Timestamps
            // 
            this.options_Timestamps.CheckOnClick = true;
            this.options_Timestamps.Name = "options_Timestamps";
            this.options_Timestamps.Size = new System.Drawing.Size(176, 22);
            this.options_Timestamps.Text = "Enable Timestamps";
            this.options_Timestamps.CheckedChanged += new System.EventHandler(this.Options_Timestamps_CheckedChanged);
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Refresh.BackColor = System.Drawing.Color.LightGreen;
            this.btn_Refresh.FlatAppearance.BorderSize = 0;
            this.btn_Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Refresh.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Refresh.Location = new System.Drawing.Point(2, 330);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(75, 23);
            this.btn_Refresh.TabIndex = 66;
            this.btn_Refresh.Text = "Refresh";
            this.btn_Refresh.UseVisualStyleBackColor = false;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // ToolkitSessionLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 355);
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.mstrip_Options);
            this.Controls.Add(this.btn_TimerEnabled);
            this.Controls.Add(this.btn_Clear);
            this.Controls.Add(this.nud_RefreshTimer);
            this.Controls.Add(this.lbl_RefreshText);
            this.Controls.Add(this.list_Logs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(623, 394);
            this.Name = "ToolkitSessionLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Session Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ToolkitSessionLog_FormClosing);
            this.Move += new System.EventHandler(this.ToolkitSessionLog_Move);
            ((System.ComponentModel.ISupportInitialize)(this.nud_RefreshTimer)).EndInit();
            this.mstrip_Options.ResumeLayout(false);
            this.mstrip_Options.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox list_Logs;
        private System.Windows.Forms.Label lbl_RefreshText;
        private System.Windows.Forms.NumericUpDown nud_RefreshTimer;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Timer tm_RefreshLogs;
        private System.Windows.Forms.Button btn_TimerEnabled;
        private System.Windows.Forms.MenuStrip mstrip_Options;
        private System.Windows.Forms.ToolStripMenuItem main_Options;
        private System.Windows.Forms.ToolStripMenuItem options_ListPriority;
        private System.Windows.Forms.ToolStripMenuItem priority_TopToBottom;
        private System.Windows.Forms.ToolStripMenuItem priority_BottomToTop;
        private System.Windows.Forms.ToolStripMenuItem options_Timestamps;
        private System.Windows.Forms.Button btn_Refresh;
    }
}