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
            ((System.ComponentModel.ISupportInitialize)(this.nud_RefreshTimer)).BeginInit();
            this.SuspendLayout();
            // 
            // list_Logs
            // 
            this.list_Logs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_Logs.FormattingEnabled = true;
            this.list_Logs.Location = new System.Drawing.Point(0, 0);
            this.list_Logs.Name = "list_Logs";
            this.list_Logs.Size = new System.Drawing.Size(607, 329);
            this.list_Logs.TabIndex = 0;
            this.list_Logs.SelectedIndexChanged += new System.EventHandler(this.List_Logs_SelectedIndexChanged);
            // 
            // lbl_RefreshText
            // 
            this.lbl_RefreshText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_RefreshText.AutoSize = true;
            this.lbl_RefreshText.Location = new System.Drawing.Point(4, 336);
            this.lbl_RefreshText.Name = "lbl_RefreshText";
            this.lbl_RefreshText.Size = new System.Drawing.Size(182, 13);
            this.lbl_RefreshText.TabIndex = 1;
            this.lbl_RefreshText.Text = "Refresh every:                      seconds";
            // 
            // nud_RefreshTimer
            // 
            this.nud_RefreshTimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nud_RefreshTimer.Location = new System.Drawing.Point(81, 332);
            this.nud_RefreshTimer.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nud_RefreshTimer.Minimum = new decimal(new int[] {
            1,
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
            this.nud_RefreshTimer.ValueChanged += new System.EventHandler(this.nud_RefreshTimer_ValueChanged);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Clear.BackColor = System.Drawing.Color.Tomato;
            this.btn_Clear.FlatAppearance.BorderSize = 0;
            this.btn_Clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Clear.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Clear.Location = new System.Drawing.Point(530, 331);
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
            this.btn_TimerEnabled.Location = new System.Drawing.Point(453, 331);
            this.btn_TimerEnabled.Name = "btn_TimerEnabled";
            this.btn_TimerEnabled.Size = new System.Drawing.Size(75, 23);
            this.btn_TimerEnabled.TabIndex = 63;
            this.btn_TimerEnabled.Text = "Pause";
            this.btn_TimerEnabled.UseVisualStyleBackColor = false;
            this.btn_TimerEnabled.Click += new System.EventHandler(this.btn_TimerEnabled_Click);
            // 
            // ToolkitSessionLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 356);
            this.Controls.Add(this.btn_TimerEnabled);
            this.Controls.Add(this.btn_Clear);
            this.Controls.Add(this.nud_RefreshTimer);
            this.Controls.Add(this.lbl_RefreshText);
            this.Controls.Add(this.list_Logs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(623, 395);
            this.Name = "ToolkitSessionLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Session Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ToolkitSessionLog_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nud_RefreshTimer)).EndInit();
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
    }
}