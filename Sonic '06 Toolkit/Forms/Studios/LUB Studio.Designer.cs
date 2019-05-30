namespace Sonic_06_Toolkit
{
    partial class LUB_Studio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LUB_Studio));
            this.lbl_Title = new System.Windows.Forms.Label();
            this.pnl_Backdrop = new System.Windows.Forms.Panel();
            this.pic_Logo = new System.Windows.Forms.PictureBox();
            this.clb_LUBs = new System.Windows.Forms.CheckedListBox();
            this.btn_SelectAll = new System.Windows.Forms.Button();
            this.btn_DeselectAll = new System.Windows.Forms.Button();
            this.btn_Decompile = new System.Windows.Forms.Button();
            this.pnl_Backdrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_Title.Location = new System.Drawing.Point(14, 10);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(201, 47);
            this.lbl_Title.TabIndex = 1;
            this.lbl_Title.Text = "LUB Studio";
            // 
            // pnl_Backdrop
            // 
            this.pnl_Backdrop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_Backdrop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(124)))));
            this.pnl_Backdrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Backdrop.Controls.Add(this.pic_Logo);
            this.pnl_Backdrop.Controls.Add(this.lbl_Title);
            this.pnl_Backdrop.Location = new System.Drawing.Point(-3, -2);
            this.pnl_Backdrop.Name = "pnl_Backdrop";
            this.pnl_Backdrop.Size = new System.Drawing.Size(418, 69);
            this.pnl_Backdrop.TabIndex = 6;
            // 
            // pic_Logo
            // 
            this.pic_Logo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_Logo.BackColor = System.Drawing.Color.Transparent;
            this.pic_Logo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_Logo.BackgroundImage")));
            this.pic_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic_Logo.Location = new System.Drawing.Point(341, 1);
            this.pic_Logo.Name = "pic_Logo";
            this.pic_Logo.Size = new System.Drawing.Size(67, 67);
            this.pic_Logo.TabIndex = 11;
            this.pic_Logo.TabStop = false;
            // 
            // clb_LUBs
            // 
            this.clb_LUBs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clb_LUBs.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.clb_LUBs.CheckOnClick = true;
            this.clb_LUBs.FormattingEnabled = true;
            this.clb_LUBs.Location = new System.Drawing.Point(8, 76);
            this.clb_LUBs.Name = "clb_LUBs";
            this.clb_LUBs.Size = new System.Drawing.Size(396, 304);
            this.clb_LUBs.TabIndex = 7;
            this.clb_LUBs.SelectedIndexChanged += new System.EventHandler(this.Clb_LUBs_SelectedIndexChanged);
            // 
            // btn_SelectAll
            // 
            this.btn_SelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_SelectAll.BackColor = System.Drawing.Color.SkyBlue;
            this.btn_SelectAll.FlatAppearance.BorderSize = 0;
            this.btn_SelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SelectAll.Location = new System.Drawing.Point(8, 387);
            this.btn_SelectAll.Name = "btn_SelectAll";
            this.btn_SelectAll.Size = new System.Drawing.Size(75, 23);
            this.btn_SelectAll.TabIndex = 8;
            this.btn_SelectAll.Text = "Select All";
            this.btn_SelectAll.UseVisualStyleBackColor = false;
            this.btn_SelectAll.Click += new System.EventHandler(this.Btn_SelectAll_Click);
            // 
            // btn_DeselectAll
            // 
            this.btn_DeselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_DeselectAll.BackColor = System.Drawing.Color.Tomato;
            this.btn_DeselectAll.FlatAppearance.BorderSize = 0;
            this.btn_DeselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DeselectAll.Location = new System.Drawing.Point(90, 387);
            this.btn_DeselectAll.Name = "btn_DeselectAll";
            this.btn_DeselectAll.Size = new System.Drawing.Size(75, 23);
            this.btn_DeselectAll.TabIndex = 9;
            this.btn_DeselectAll.Text = "Deselect All";
            this.btn_DeselectAll.UseVisualStyleBackColor = false;
            this.btn_DeselectAll.Click += new System.EventHandler(this.Btn_DeselectAll_Click);
            // 
            // btn_Decompile
            // 
            this.btn_Decompile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Decompile.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btn_Decompile.Enabled = false;
            this.btn_Decompile.FlatAppearance.BorderSize = 0;
            this.btn_Decompile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Decompile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Decompile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Decompile.Location = new System.Drawing.Point(329, 387);
            this.btn_Decompile.Name = "btn_Decompile";
            this.btn_Decompile.Size = new System.Drawing.Size(75, 23);
            this.btn_Decompile.TabIndex = 10;
            this.btn_Decompile.Text = "Decompile";
            this.btn_Decompile.UseVisualStyleBackColor = false;
            this.btn_Decompile.Click += new System.EventHandler(this.Btn_Decompile_Click);
            // 
            // LUB_Studio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(413, 419);
            this.Controls.Add(this.btn_Decompile);
            this.Controls.Add(this.btn_DeselectAll);
            this.Controls.Add(this.btn_SelectAll);
            this.Controls.Add(this.clb_LUBs);
            this.Controls.Add(this.pnl_Backdrop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(429, 458);
            this.Name = "LUB_Studio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LUB Studio";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LUB_Studio_FormClosing);
            this.Load += new System.EventHandler(this.LUB_Studio_Load);
            this.pnl_Backdrop.ResumeLayout(false);
            this.pnl_Backdrop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Panel pnl_Backdrop;
        private System.Windows.Forms.CheckedListBox clb_LUBs;
        private System.Windows.Forms.Button btn_SelectAll;
        private System.Windows.Forms.Button btn_DeselectAll;
        private System.Windows.Forms.Button btn_Decompile;
        internal System.Windows.Forms.PictureBox pic_Logo;
    }
}