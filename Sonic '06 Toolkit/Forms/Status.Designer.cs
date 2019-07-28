namespace Sonic_06_Toolkit
{
    partial class Status
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Status));
            this.pic_Logo = new System.Windows.Forms.PictureBox();
            this.pnl_windowCheck = new System.Windows.Forms.Panel();
            this.NOW_LOADING = new System.Windows.Forms.PictureBox();
            this.lbl_unpackState = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).BeginInit();
            this.pnl_windowCheck.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NOW_LOADING)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_Logo
            // 
            this.pic_Logo.BackgroundImage = global::Sonic_06_Toolkit.Properties.Resources.logo_main;
            this.pic_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic_Logo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_Logo.Location = new System.Drawing.Point(9, 8);
            this.pic_Logo.Name = "pic_Logo";
            this.pic_Logo.Size = new System.Drawing.Size(86, 83);
            this.pic_Logo.TabIndex = 7;
            this.pic_Logo.TabStop = false;
            // 
            // pnl_windowCheck
            // 
            this.pnl_windowCheck.AutoSize = true;
            this.pnl_windowCheck.BackColor = System.Drawing.Color.Honeydew;
            this.pnl_windowCheck.Controls.Add(this.NOW_LOADING);
            this.pnl_windowCheck.Controls.Add(this.pic_Logo);
            this.pnl_windowCheck.Controls.Add(this.lbl_unpackState);
            this.pnl_windowCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_windowCheck.Location = new System.Drawing.Point(0, 0);
            this.pnl_windowCheck.Name = "pnl_windowCheck";
            this.pnl_windowCheck.Size = new System.Drawing.Size(259, 97);
            this.pnl_windowCheck.TabIndex = 8;
            // 
            // NOW_LOADING
            // 
            this.NOW_LOADING.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("NOW_LOADING.BackgroundImage")));
            this.NOW_LOADING.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.NOW_LOADING.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NOW_LOADING.Location = new System.Drawing.Point(0, 0);
            this.NOW_LOADING.Name = "NOW_LOADING";
            this.NOW_LOADING.Size = new System.Drawing.Size(259, 97);
            this.NOW_LOADING.TabIndex = 8;
            this.NOW_LOADING.TabStop = false;
            this.NOW_LOADING.Visible = false;
            // 
            // lbl_unpackState
            // 
            this.lbl_unpackState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_unpackState.AutoSize = true;
            this.lbl_unpackState.Location = new System.Drawing.Point(105, 46);
            this.lbl_unpackState.Name = "lbl_unpackState";
            this.lbl_unpackState.Size = new System.Drawing.Size(33, 13);
            this.lbl_unpackState.TabIndex = 6;
            this.lbl_unpackState.Text = "None";
            // 
            // Status
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(259, 97);
            this.ControlBox = false;
            this.Controls.Add(this.pnl_windowCheck);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Status";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Status";
            this.Load += new System.EventHandler(this.Status_Notifier_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).EndInit();
            this.pnl_windowCheck.ResumeLayout(false);
            this.pnl_windowCheck.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NOW_LOADING)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.PictureBox pic_Logo;
        internal System.Windows.Forms.Panel pnl_windowCheck;
        internal System.Windows.Forms.Label lbl_unpackState;
        private System.Windows.Forms.PictureBox NOW_LOADING;
    }
}