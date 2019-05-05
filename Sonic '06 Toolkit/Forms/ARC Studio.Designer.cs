namespace Sonic_06_Toolkit
{
    partial class ARC_Studio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ARC_Studio));
            this.lbl_Title = new System.Windows.Forms.Label();
            this.pnl_Backdrop = new System.Windows.Forms.Panel();
            this.pic_Logo = new System.Windows.Forms.PictureBox();
            this.group_RootPath = new System.Windows.Forms.GroupBox();
            this.btn_BrowseARC1 = new System.Windows.Forms.Button();
            this.text_ARC1 = new System.Windows.Forms.TextBox();
            this.group_ToolsPath = new System.Windows.Forms.GroupBox();
            this.btn_BrowseARC2 = new System.Windows.Forms.Button();
            this.text_ARC2 = new System.Windows.Forms.TextBox();
            this.group_ArchivesPath = new System.Windows.Forms.GroupBox();
            this.btn_BrowseOutput = new System.Windows.Forms.Button();
            this.text_Output = new System.Windows.Forms.TextBox();
            this.btn_Merge = new System.Windows.Forms.Button();
            this.ofd_OpenARC = new System.Windows.Forms.OpenFileDialog();
            this.sfd_Output = new System.Windows.Forms.SaveFileDialog();
            this.pnl_Backdrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).BeginInit();
            this.group_RootPath.SuspendLayout();
            this.group_ToolsPath.SuspendLayout();
            this.group_ArchivesPath.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_Title.Location = new System.Drawing.Point(14, 10);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(207, 47);
            this.lbl_Title.TabIndex = 1;
            this.lbl_Title.Text = "ARC Studio";
            // 
            // pnl_Backdrop
            // 
            this.pnl_Backdrop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_Backdrop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.pnl_Backdrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Backdrop.Controls.Add(this.pic_Logo);
            this.pnl_Backdrop.Controls.Add(this.lbl_Title);
            this.pnl_Backdrop.Location = new System.Drawing.Point(-3, -2);
            this.pnl_Backdrop.Name = "pnl_Backdrop";
            this.pnl_Backdrop.Size = new System.Drawing.Size(536, 69);
            this.pnl_Backdrop.TabIndex = 18;
            // 
            // pic_Logo
            // 
            this.pic_Logo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_Logo.BackColor = System.Drawing.Color.Transparent;
            this.pic_Logo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_Logo.BackgroundImage")));
            this.pic_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic_Logo.Location = new System.Drawing.Point(459, 1);
            this.pic_Logo.Name = "pic_Logo";
            this.pic_Logo.Size = new System.Drawing.Size(67, 67);
            this.pic_Logo.TabIndex = 11;
            this.pic_Logo.TabStop = false;
            // 
            // group_RootPath
            // 
            this.group_RootPath.Controls.Add(this.btn_BrowseARC1);
            this.group_RootPath.Controls.Add(this.text_ARC1);
            this.group_RootPath.Location = new System.Drawing.Point(9, 71);
            this.group_RootPath.Name = "group_RootPath";
            this.group_RootPath.Size = new System.Drawing.Size(513, 71);
            this.group_RootPath.TabIndex = 22;
            this.group_RootPath.TabStop = false;
            this.group_RootPath.Text = "ARC #1";
            // 
            // btn_BrowseARC1
            // 
            this.btn_BrowseARC1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(171)))), ((int)(((byte)(83)))));
            this.btn_BrowseARC1.FlatAppearance.BorderSize = 0;
            this.btn_BrowseARC1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_BrowseARC1.Location = new System.Drawing.Point(471, 30);
            this.btn_BrowseARC1.Name = "btn_BrowseARC1";
            this.btn_BrowseARC1.Size = new System.Drawing.Size(25, 20);
            this.btn_BrowseARC1.TabIndex = 19;
            this.btn_BrowseARC1.Text = "...";
            this.btn_BrowseARC1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_BrowseARC1.UseVisualStyleBackColor = false;
            this.btn_BrowseARC1.Click += new System.EventHandler(this.Btn_BrowseARC1_Click);
            // 
            // text_ARC1
            // 
            this.text_ARC1.Location = new System.Drawing.Point(17, 30);
            this.text_ARC1.Name = "text_ARC1";
            this.text_ARC1.Size = new System.Drawing.Size(449, 20);
            this.text_ARC1.TabIndex = 0;
            // 
            // group_ToolsPath
            // 
            this.group_ToolsPath.Controls.Add(this.btn_BrowseARC2);
            this.group_ToolsPath.Controls.Add(this.text_ARC2);
            this.group_ToolsPath.Location = new System.Drawing.Point(9, 145);
            this.group_ToolsPath.Name = "group_ToolsPath";
            this.group_ToolsPath.Size = new System.Drawing.Size(513, 71);
            this.group_ToolsPath.TabIndex = 23;
            this.group_ToolsPath.TabStop = false;
            this.group_ToolsPath.Text = "ARC #2";
            // 
            // btn_BrowseARC2
            // 
            this.btn_BrowseARC2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(171)))), ((int)(((byte)(83)))));
            this.btn_BrowseARC2.FlatAppearance.BorderSize = 0;
            this.btn_BrowseARC2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_BrowseARC2.Location = new System.Drawing.Point(471, 30);
            this.btn_BrowseARC2.Name = "btn_BrowseARC2";
            this.btn_BrowseARC2.Size = new System.Drawing.Size(25, 20);
            this.btn_BrowseARC2.TabIndex = 19;
            this.btn_BrowseARC2.Text = "...";
            this.btn_BrowseARC2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_BrowseARC2.UseVisualStyleBackColor = false;
            this.btn_BrowseARC2.Click += new System.EventHandler(this.Btn_BrowseARC2_Click);
            // 
            // text_ARC2
            // 
            this.text_ARC2.Location = new System.Drawing.Point(17, 30);
            this.text_ARC2.Name = "text_ARC2";
            this.text_ARC2.Size = new System.Drawing.Size(449, 20);
            this.text_ARC2.TabIndex = 0;
            // 
            // group_ArchivesPath
            // 
            this.group_ArchivesPath.Controls.Add(this.btn_BrowseOutput);
            this.group_ArchivesPath.Controls.Add(this.text_Output);
            this.group_ArchivesPath.Location = new System.Drawing.Point(9, 220);
            this.group_ArchivesPath.Name = "group_ArchivesPath";
            this.group_ArchivesPath.Size = new System.Drawing.Size(513, 71);
            this.group_ArchivesPath.TabIndex = 24;
            this.group_ArchivesPath.TabStop = false;
            this.group_ArchivesPath.Text = "Output";
            // 
            // btn_BrowseOutput
            // 
            this.btn_BrowseOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(171)))), ((int)(((byte)(83)))));
            this.btn_BrowseOutput.FlatAppearance.BorderSize = 0;
            this.btn_BrowseOutput.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_BrowseOutput.Location = new System.Drawing.Point(471, 30);
            this.btn_BrowseOutput.Name = "btn_BrowseOutput";
            this.btn_BrowseOutput.Size = new System.Drawing.Size(25, 20);
            this.btn_BrowseOutput.TabIndex = 19;
            this.btn_BrowseOutput.Text = "...";
            this.btn_BrowseOutput.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_BrowseOutput.UseVisualStyleBackColor = false;
            this.btn_BrowseOutput.Click += new System.EventHandler(this.Btn_BrowseOutput_Click);
            // 
            // text_Output
            // 
            this.text_Output.Location = new System.Drawing.Point(17, 30);
            this.text_Output.Name = "text_Output";
            this.text_Output.Size = new System.Drawing.Size(449, 20);
            this.text_Output.TabIndex = 0;
            // 
            // btn_Merge
            // 
            this.btn_Merge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(91)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))));
            this.btn_Merge.FlatAppearance.BorderSize = 0;
            this.btn_Merge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Merge.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Merge.Location = new System.Drawing.Point(447, 297);
            this.btn_Merge.Name = "btn_Merge";
            this.btn_Merge.Size = new System.Drawing.Size(75, 23);
            this.btn_Merge.TabIndex = 26;
            this.btn_Merge.Text = "Merge";
            this.btn_Merge.UseVisualStyleBackColor = false;
            this.btn_Merge.Click += new System.EventHandler(this.Btn_Merge_Click);
            // 
            // ofd_OpenARC
            // 
            this.ofd_OpenARC.FileName = "openFileDialog1";
            // 
            // sfd_Output
            // 
            this.sfd_Output.Filter = "ARC Files|*.arc";
            this.sfd_Output.RestoreDirectory = true;
            this.sfd_Output.Title = "Output...";
            // 
            // ARC_Studio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(531, 330);
            this.Controls.Add(this.btn_Merge);
            this.Controls.Add(this.group_RootPath);
            this.Controls.Add(this.group_ToolsPath);
            this.Controls.Add(this.group_ArchivesPath);
            this.Controls.Add(this.pnl_Backdrop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ARC_Studio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ARC Studio";
            this.pnl_Backdrop.ResumeLayout(false);
            this.pnl_Backdrop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).EndInit();
            this.group_RootPath.ResumeLayout(false);
            this.group_RootPath.PerformLayout();
            this.group_ToolsPath.ResumeLayout(false);
            this.group_ToolsPath.PerformLayout();
            this.group_ArchivesPath.ResumeLayout(false);
            this.group_ArchivesPath.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.PictureBox pic_Logo;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Panel pnl_Backdrop;
        private System.Windows.Forms.GroupBox group_RootPath;
        internal System.Windows.Forms.Button btn_BrowseARC1;
        private System.Windows.Forms.TextBox text_ARC1;
        private System.Windows.Forms.GroupBox group_ToolsPath;
        internal System.Windows.Forms.Button btn_BrowseARC2;
        private System.Windows.Forms.TextBox text_ARC2;
        private System.Windows.Forms.GroupBox group_ArchivesPath;
        internal System.Windows.Forms.Button btn_BrowseOutput;
        private System.Windows.Forms.TextBox text_Output;
        private System.Windows.Forms.Button btn_Merge;
        private System.Windows.Forms.OpenFileDialog ofd_OpenARC;
        private System.Windows.Forms.SaveFileDialog sfd_Output;
    }
}