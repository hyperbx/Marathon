namespace Sonic_06_Toolkit
{
    partial class Processing_ARC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Processing_ARC));
            this.pnl_windowCheck = new System.Windows.Forms.Panel();
            this.lbl_repackState = new System.Windows.Forms.Label();
            this.pic_Logo = new System.Windows.Forms.PictureBox();
            this.pnl_windowCheck.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_windowCheck
            // 
            this.pnl_windowCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.pnl_windowCheck.Controls.Add(this.pic_Logo);
            this.pnl_windowCheck.Controls.Add(this.lbl_repackState);
            this.pnl_windowCheck.Location = new System.Drawing.Point(0, 0);
            this.pnl_windowCheck.Name = "pnl_windowCheck";
            this.pnl_windowCheck.Size = new System.Drawing.Size(277, 97);
            this.pnl_windowCheck.TabIndex = 2;
            // 
            // lbl_repackState
            // 
            this.lbl_repackState.AutoSize = true;
            this.lbl_repackState.Location = new System.Drawing.Point(108, 43);
            this.lbl_repackState.Name = "lbl_repackState";
            this.lbl_repackState.Size = new System.Drawing.Size(158, 13);
            this.lbl_repackState.TabIndex = 5;
            this.lbl_repackState.Text = "Processing ARCs. Please wait...";
            // 
            // pic_Logo
            // 
            this.pic_Logo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_Logo.BackgroundImage")));
            this.pic_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic_Logo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_Logo.Location = new System.Drawing.Point(10, 8);
            this.pic_Logo.Name = "pic_Logo";
            this.pic_Logo.Size = new System.Drawing.Size(86, 83);
            this.pic_Logo.TabIndex = 6;
            this.pic_Logo.TabStop = false;
            // 
            // Processing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.ClientSize = new System.Drawing.Size(277, 97);
            this.ControlBox = false;
            this.Controls.Add(this.pnl_windowCheck);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Processing";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Processing ARCs...";
            this.pnl_windowCheck.ResumeLayout(false);
            this.pnl_windowCheck.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel pnl_windowCheck;
        internal System.Windows.Forms.PictureBox pic_Logo;
        internal System.Windows.Forms.Label lbl_repackState;
    }
}