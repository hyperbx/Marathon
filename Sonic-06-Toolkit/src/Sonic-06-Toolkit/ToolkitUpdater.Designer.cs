namespace Toolkit.Tools
{
    partial class ToolkitUpdater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolkitUpdater));
            this.pgb_Progress = new System.Windows.Forms.ProgressBar();
            this.lbl_Update = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pgb_Progress
            // 
            this.pgb_Progress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pgb_Progress.Location = new System.Drawing.Point(13, 45);
            this.pgb_Progress.Name = "pgb_Progress";
            this.pgb_Progress.Size = new System.Drawing.Size(433, 50);
            this.pgb_Progress.TabIndex = 5;
            // 
            // lbl_Update
            // 
            this.lbl_Update.AutoSize = true;
            this.lbl_Update.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Update.Location = new System.Drawing.Point(13, 10);
            this.lbl_Update.Name = "lbl_Update";
            this.lbl_Update.Size = new System.Drawing.Size(433, 25);
            this.lbl_Update.TabIndex = 4;
            this.lbl_Update.Text = "Updating Sonic \'06 Toolkit to Version 0.00_00...";
            // 
            // ToolkitUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 106);
            this.Controls.Add(this.pgb_Progress);
            this.Controls.Add(this.lbl_Update);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ToolkitUpdater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sonic \'06 Toolkit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pgb_Progress;
        private System.Windows.Forms.Label lbl_Update;
    }
}