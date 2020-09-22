namespace Marathon.Toolkit.Forms
{
    partial class StartPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartPage));
            this.Panel_StartPane = new System.Windows.Forms.Panel();
            this.FlowLayoutPanel_Recent = new System.Windows.Forms.FlowLayoutPanel();
            this.Label_Recent = new System.Windows.Forms.Label();
            this.SplitContainer_Zone1 = new System.Windows.Forms.SplitContainer();
            this.SplitContainer_Zone2 = new System.Windows.Forms.SplitContainer();
            this.Label_Open_Description = new System.Windows.Forms.Label();
            this.Label_Open = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Panel_StartPane.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_Zone1)).BeginInit();
            this.SplitContainer_Zone1.Panel2.SuspendLayout();
            this.SplitContainer_Zone1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_Zone2)).BeginInit();
            this.SplitContainer_Zone2.Panel1.SuspendLayout();
            this.SplitContainer_Zone2.Panel2.SuspendLayout();
            this.SplitContainer_Zone2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_StartPane
            // 
            this.Panel_StartPane.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_StartPane.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.Panel_StartPane.Controls.Add(this.FlowLayoutPanel_Recent);
            this.Panel_StartPane.Controls.Add(this.Label_Recent);
            this.Panel_StartPane.Controls.Add(this.SplitContainer_Zone1);
            this.Panel_StartPane.Location = new System.Drawing.Point(61, 0);
            this.Panel_StartPane.Name = "Panel_StartPane";
            this.Panel_StartPane.Size = new System.Drawing.Size(872, 519);
            this.Panel_StartPane.TabIndex = 0;
            // 
            // FlowLayoutPanel_Recent
            // 
            this.FlowLayoutPanel_Recent.Location = new System.Drawing.Point(37, 107);
            this.FlowLayoutPanel_Recent.Name = "FlowLayoutPanel_Recent";
            this.FlowLayoutPanel_Recent.Size = new System.Drawing.Size(261, 380);
            this.FlowLayoutPanel_Recent.TabIndex = 2;
            // 
            // Label_Recent
            // 
            this.Label_Recent.AutoSize = true;
            this.Label_Recent.Font = new System.Drawing.Font("Segoe UI Light", 29.8F);
            this.Label_Recent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(206)))), ((int)(((byte)(255)))));
            this.Label_Recent.Location = new System.Drawing.Point(28, 35);
            this.Label_Recent.Name = "Label_Recent";
            this.Label_Recent.Size = new System.Drawing.Size(136, 54);
            this.Label_Recent.TabIndex = 0;
            this.Label_Recent.Text = "Recent";
            // 
            // SplitContainer_Zone1
            // 
            this.SplitContainer_Zone1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer_Zone1.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer_Zone1.Name = "SplitContainer_Zone1";
            // 
            // SplitContainer_Zone1.Panel2
            // 
            this.SplitContainer_Zone1.Panel2.Controls.Add(this.SplitContainer_Zone2);
            this.SplitContainer_Zone1.Size = new System.Drawing.Size(872, 519);
            this.SplitContainer_Zone1.SplitterDistance = 333;
            this.SplitContainer_Zone1.TabIndex = 99;
            // 
            // SplitContainer_Zone2
            // 
            this.SplitContainer_Zone2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer_Zone2.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer_Zone2.Name = "SplitContainer_Zone2";
            this.SplitContainer_Zone2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainer_Zone2.Panel1
            // 
            this.SplitContainer_Zone2.Panel1.Controls.Add(this.Label_Open_Description);
            this.SplitContainer_Zone2.Panel1.Controls.Add(this.Label_Open);
            // 
            // SplitContainer_Zone2.Panel2
            // 
            this.SplitContainer_Zone2.Panel2.Controls.Add(this.label1);
            this.SplitContainer_Zone2.Panel2.Controls.Add(this.label2);
            this.SplitContainer_Zone2.Size = new System.Drawing.Size(535, 519);
            this.SplitContainer_Zone2.SplitterDistance = 259;
            this.SplitContainer_Zone2.TabIndex = 99;
            // 
            // Label_Open_Description
            // 
            this.Label_Open_Description.AutoSize = true;
            this.Label_Open_Description.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Label_Open_Description.ForeColor = System.Drawing.SystemColors.Control;
            this.Label_Open_Description.Location = new System.Drawing.Point(25, 85);
            this.Label_Open_Description.Name = "Label_Open_Description";
            this.Label_Open_Description.Size = new System.Drawing.Size(200, 15);
            this.Label_Open_Description.TabIndex = 3;
            this.Label_Open_Description.Text = "Open something on your local drive.";
            // 
            // Label_Open
            // 
            this.Label_Open.AutoSize = true;
            this.Label_Open.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Open.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(206)))), ((int)(((byte)(255)))));
            this.Label_Open.Location = new System.Drawing.Point(21, 43);
            this.Label_Open.Name = "Label_Open";
            this.Label_Open.Size = new System.Drawing.Size(74, 32);
            this.Label_Open.TabIndex = 2;
            this.Label_Open.Text = "Open";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(25, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Open something on your local drive.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(206)))), ((int)(((byte)(255)))));
            this.label2.Location = new System.Drawing.Point(21, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 32);
            this.label2.TabIndex = 4;
            this.label2.Text = "Open";
            // 
            // StartPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(933, 519);
            this.Controls.Add(this.Panel_StartPane);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StartPage";
            this.Text = "Start Page";
            this.Panel_StartPane.ResumeLayout(false);
            this.Panel_StartPane.PerformLayout();
            this.SplitContainer_Zone1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_Zone1)).EndInit();
            this.SplitContainer_Zone1.ResumeLayout(false);
            this.SplitContainer_Zone2.Panel1.ResumeLayout(false);
            this.SplitContainer_Zone2.Panel1.PerformLayout();
            this.SplitContainer_Zone2.Panel2.ResumeLayout(false);
            this.SplitContainer_Zone2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_Zone2)).EndInit();
            this.SplitContainer_Zone2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_StartPane;
        private System.Windows.Forms.Label Label_Recent;
        private System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel_Recent;
        private System.Windows.Forms.SplitContainer SplitContainer_Zone1;
        private System.Windows.Forms.SplitContainer SplitContainer_Zone2;
        private System.Windows.Forms.Label Label_Open_Description;
        private System.Windows.Forms.Label Label_Open;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}