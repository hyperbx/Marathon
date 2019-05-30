namespace Sonic_06_Toolkit
{
    partial class MST_Studio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MST_Studio));
            this.pnl_Backdrop = new System.Windows.Forms.Panel();
            this.pic_Logo = new System.Windows.Forms.PictureBox();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.btn_Convert = new System.Windows.Forms.Button();
            this.btn_DeselectAll = new System.Windows.Forms.Button();
            this.btn_SelectAll = new System.Windows.Forms.Button();
            this.clb_MSTs = new System.Windows.Forms.CheckedListBox();
            this.split_MSTStudio = new System.Windows.Forms.SplitContainer();
            this.clb_XNOs_XNM = new System.Windows.Forms.CheckedListBox();
            this.clb_XNMs = new System.Windows.Forms.CheckedListBox();
            this.lbl_Mode = new System.Windows.Forms.Label();
            this.combo_Mode = new System.Windows.Forms.ComboBox();
            this.pnl_Backdrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.split_MSTStudio)).BeginInit();
            this.split_MSTStudio.Panel1.SuspendLayout();
            this.split_MSTStudio.Panel2.SuspendLayout();
            this.split_MSTStudio.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Backdrop
            // 
            this.pnl_Backdrop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_Backdrop.BackColor = System.Drawing.Color.DarkOrchid;
            this.pnl_Backdrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Backdrop.Controls.Add(this.pic_Logo);
            this.pnl_Backdrop.Controls.Add(this.lbl_Title);
            this.pnl_Backdrop.Location = new System.Drawing.Point(-3, -2);
            this.pnl_Backdrop.Name = "pnl_Backdrop";
            this.pnl_Backdrop.Size = new System.Drawing.Size(439, 69);
            this.pnl_Backdrop.TabIndex = 30;
            // 
            // pic_Logo
            // 
            this.pic_Logo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_Logo.BackColor = System.Drawing.Color.Transparent;
            this.pic_Logo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_Logo.BackgroundImage")));
            this.pic_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic_Logo.Location = new System.Drawing.Point(362, 1);
            this.pic_Logo.Name = "pic_Logo";
            this.pic_Logo.Size = new System.Drawing.Size(67, 67);
            this.pic_Logo.TabIndex = 11;
            this.pic_Logo.TabStop = false;
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_Title.Location = new System.Drawing.Point(14, 10);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(212, 47);
            this.lbl_Title.TabIndex = 37;
            this.lbl_Title.Text = "MST Studio";
            // 
            // btn_Convert
            // 
            this.btn_Convert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Convert.BackColor = System.Drawing.Color.DarkOrchid;
            this.btn_Convert.Enabled = false;
            this.btn_Convert.FlatAppearance.BorderSize = 0;
            this.btn_Convert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Convert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Convert.Location = new System.Drawing.Point(350, 387);
            this.btn_Convert.Name = "btn_Convert";
            this.btn_Convert.Size = new System.Drawing.Size(75, 23);
            this.btn_Convert.TabIndex = 34;
            this.btn_Convert.Text = "Export";
            this.btn_Convert.UseVisualStyleBackColor = false;
            this.btn_Convert.Click += new System.EventHandler(this.Btn_Decode_Click);
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
            this.btn_DeselectAll.TabIndex = 33;
            this.btn_DeselectAll.Text = "Deselect All";
            this.btn_DeselectAll.UseVisualStyleBackColor = false;
            this.btn_DeselectAll.Click += new System.EventHandler(this.Btn_DeselectAll_Click);
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
            this.btn_SelectAll.TabIndex = 32;
            this.btn_SelectAll.Text = "Select All";
            this.btn_SelectAll.UseVisualStyleBackColor = false;
            this.btn_SelectAll.Click += new System.EventHandler(this.Btn_SelectAll_Click);
            // 
            // clb_MSTs
            // 
            this.clb_MSTs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clb_MSTs.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.clb_MSTs.CheckOnClick = true;
            this.clb_MSTs.FormattingEnabled = true;
            this.clb_MSTs.Location = new System.Drawing.Point(8, 76);
            this.clb_MSTs.Name = "clb_MSTs";
            this.clb_MSTs.Size = new System.Drawing.Size(417, 304);
            this.clb_MSTs.TabIndex = 31;
            this.clb_MSTs.SelectedIndexChanged += new System.EventHandler(this.Clb_MSTs_SelectedIndexChanged);
            // 
            // split_MSTStudio
            // 
            this.split_MSTStudio.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.split_MSTStudio.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.split_MSTStudio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.split_MSTStudio.Location = new System.Drawing.Point(8, 76);
            this.split_MSTStudio.Name = "split_MSTStudio";
            // 
            // split_MSTStudio.Panel1
            // 
            this.split_MSTStudio.Panel1.Controls.Add(this.clb_XNOs_XNM);
            // 
            // split_MSTStudio.Panel2
            // 
            this.split_MSTStudio.Panel2.Controls.Add(this.clb_XNMs);
            this.split_MSTStudio.Size = new System.Drawing.Size(417, 304);
            this.split_MSTStudio.SplitterDistance = 207;
            this.split_MSTStudio.TabIndex = 36;
            // 
            // clb_XNOs_XNM
            // 
            this.clb_XNOs_XNM.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clb_XNOs_XNM.CheckOnClick = true;
            this.clb_XNOs_XNM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clb_XNOs_XNM.FormattingEnabled = true;
            this.clb_XNOs_XNM.Location = new System.Drawing.Point(0, 0);
            this.clb_XNOs_XNM.Name = "clb_XNOs_XNM";
            this.clb_XNOs_XNM.Size = new System.Drawing.Size(205, 302);
            this.clb_XNOs_XNM.TabIndex = 0;
            // 
            // clb_XNMs
            // 
            this.clb_XNMs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.clb_XNMs.CheckOnClick = true;
            this.clb_XNMs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clb_XNMs.FormattingEnabled = true;
            this.clb_XNMs.Location = new System.Drawing.Point(0, 0);
            this.clb_XNMs.Name = "clb_XNMs";
            this.clb_XNMs.Size = new System.Drawing.Size(204, 302);
            this.clb_XNMs.TabIndex = 0;
            // 
            // lbl_Mode
            // 
            this.lbl_Mode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Mode.AutoSize = true;
            this.lbl_Mode.Location = new System.Drawing.Point(184, 392);
            this.lbl_Mode.Name = "lbl_Mode";
            this.lbl_Mode.Size = new System.Drawing.Size(37, 13);
            this.lbl_Mode.TabIndex = 38;
            this.lbl_Mode.Text = "Mode:";
            // 
            // combo_Mode
            // 
            this.combo_Mode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.combo_Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_Mode.FormattingEnabled = true;
            this.combo_Mode.Items.AddRange(new object[] {
            "Export",
            "Import"});
            this.combo_Mode.Location = new System.Drawing.Point(223, 388);
            this.combo_Mode.Name = "combo_Mode";
            this.combo_Mode.Size = new System.Drawing.Size(121, 21);
            this.combo_Mode.TabIndex = 37;
            this.combo_Mode.SelectedIndexChanged += new System.EventHandler(this.Combo_Mode_SelectedIndexChanged);
            // 
            // MST_Studio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(434, 419);
            this.Controls.Add(this.lbl_Mode);
            this.Controls.Add(this.combo_Mode);
            this.Controls.Add(this.pnl_Backdrop);
            this.Controls.Add(this.btn_Convert);
            this.Controls.Add(this.btn_DeselectAll);
            this.Controls.Add(this.btn_SelectAll);
            this.Controls.Add(this.clb_MSTs);
            this.Controls.Add(this.split_MSTStudio);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(429, 458);
            this.Name = "MST_Studio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MST Studio";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MST_Studio_FormClosing);
            this.Load += new System.EventHandler(this.MST_Studio_Load);
            this.pnl_Backdrop.ResumeLayout(false);
            this.pnl_Backdrop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).EndInit();
            this.split_MSTStudio.Panel1.ResumeLayout(false);
            this.split_MSTStudio.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.split_MSTStudio)).EndInit();
            this.split_MSTStudio.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnl_Backdrop;
        internal System.Windows.Forms.PictureBox pic_Logo;
        private System.Windows.Forms.Button btn_Convert;
        private System.Windows.Forms.Button btn_DeselectAll;
        private System.Windows.Forms.Button btn_SelectAll;
        private System.Windows.Forms.CheckedListBox clb_MSTs;
        private System.Windows.Forms.SplitContainer split_MSTStudio;
        private System.Windows.Forms.CheckedListBox clb_XNOs_XNM;
        private System.Windows.Forms.CheckedListBox clb_XNMs;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Label lbl_Mode;
        private System.Windows.Forms.ComboBox combo_Mode;
    }
}