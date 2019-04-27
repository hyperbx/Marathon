namespace Sonic_06_Toolkit
{
    partial class Paths
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Paths));
            this.lbl_Title = new System.Windows.Forms.Label();
            this.pnl_Backdrop = new System.Windows.Forms.Panel();
            this.pic_Logo = new System.Windows.Forms.PictureBox();
            this.group_RootPath = new System.Windows.Forms.GroupBox();
            this.btn_BrowseRoot = new System.Windows.Forms.Button();
            this.text_RootPath = new System.Windows.Forms.TextBox();
            this.btn_BrowseTools = new System.Windows.Forms.Button();
            this.text_ToolsPath = new System.Windows.Forms.TextBox();
            this.group_ToolsPath = new System.Windows.Forms.GroupBox();
            this.btn_BrowseArchives = new System.Windows.Forms.Button();
            this.text_ArchivesPath = new System.Windows.Forms.TextBox();
            this.group_ArchivesPath = new System.Windows.Forms.GroupBox();
            this.btn_Restore = new System.Windows.Forms.Button();
            this.btn_AppPath = new System.Windows.Forms.Button();
            this.btn_Confirm = new System.Windows.Forms.Button();
            this.fbd_Browse = new System.Windows.Forms.FolderBrowserDialog();
            this.btn_Cancel = new System.Windows.Forms.Button();
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
            this.lbl_Title.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Title.Location = new System.Drawing.Point(14, 10);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(110, 47);
            this.lbl_Title.TabIndex = 1;
            this.lbl_Title.Text = "Paths";
            // 
            // pnl_Backdrop
            // 
            this.pnl_Backdrop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_Backdrop.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnl_Backdrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Backdrop.Controls.Add(this.pic_Logo);
            this.pnl_Backdrop.Controls.Add(this.lbl_Title);
            this.pnl_Backdrop.Location = new System.Drawing.Point(-3, -2);
            this.pnl_Backdrop.Name = "pnl_Backdrop";
            this.pnl_Backdrop.Size = new System.Drawing.Size(536, 69);
            this.pnl_Backdrop.TabIndex = 17;
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
            this.group_RootPath.Controls.Add(this.btn_BrowseRoot);
            this.group_RootPath.Controls.Add(this.text_RootPath);
            this.group_RootPath.Location = new System.Drawing.Point(9, 71);
            this.group_RootPath.Name = "group_RootPath";
            this.group_RootPath.Size = new System.Drawing.Size(513, 71);
            this.group_RootPath.TabIndex = 18;
            this.group_RootPath.TabStop = false;
            this.group_RootPath.Text = "Root Path";
            // 
            // btn_BrowseRoot
            // 
            this.btn_BrowseRoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(171)))), ((int)(((byte)(83)))));
            this.btn_BrowseRoot.FlatAppearance.BorderSize = 0;
            this.btn_BrowseRoot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_BrowseRoot.Location = new System.Drawing.Point(471, 30);
            this.btn_BrowseRoot.Name = "btn_BrowseRoot";
            this.btn_BrowseRoot.Size = new System.Drawing.Size(25, 20);
            this.btn_BrowseRoot.TabIndex = 19;
            this.btn_BrowseRoot.Text = "...";
            this.btn_BrowseRoot.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_BrowseRoot.UseVisualStyleBackColor = false;
            this.btn_BrowseRoot.Click += new System.EventHandler(this.Btn_BrowseRoot_Click);
            // 
            // text_RootPath
            // 
            this.text_RootPath.Location = new System.Drawing.Point(17, 30);
            this.text_RootPath.Name = "text_RootPath";
            this.text_RootPath.Size = new System.Drawing.Size(449, 20);
            this.text_RootPath.TabIndex = 0;
            // 
            // btn_BrowseTools
            // 
            this.btn_BrowseTools.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(171)))), ((int)(((byte)(83)))));
            this.btn_BrowseTools.FlatAppearance.BorderSize = 0;
            this.btn_BrowseTools.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_BrowseTools.Location = new System.Drawing.Point(471, 30);
            this.btn_BrowseTools.Name = "btn_BrowseTools";
            this.btn_BrowseTools.Size = new System.Drawing.Size(25, 20);
            this.btn_BrowseTools.TabIndex = 19;
            this.btn_BrowseTools.Text = "...";
            this.btn_BrowseTools.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_BrowseTools.UseVisualStyleBackColor = false;
            this.btn_BrowseTools.Click += new System.EventHandler(this.Btn_BrowseTools_Click);
            // 
            // text_ToolsPath
            // 
            this.text_ToolsPath.Location = new System.Drawing.Point(17, 30);
            this.text_ToolsPath.Name = "text_ToolsPath";
            this.text_ToolsPath.Size = new System.Drawing.Size(449, 20);
            this.text_ToolsPath.TabIndex = 0;
            // 
            // group_ToolsPath
            // 
            this.group_ToolsPath.Controls.Add(this.btn_BrowseTools);
            this.group_ToolsPath.Controls.Add(this.text_ToolsPath);
            this.group_ToolsPath.Location = new System.Drawing.Point(9, 145);
            this.group_ToolsPath.Name = "group_ToolsPath";
            this.group_ToolsPath.Size = new System.Drawing.Size(513, 71);
            this.group_ToolsPath.TabIndex = 20;
            this.group_ToolsPath.TabStop = false;
            this.group_ToolsPath.Text = "Tools Path";
            // 
            // btn_BrowseArchives
            // 
            this.btn_BrowseArchives.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(171)))), ((int)(((byte)(83)))));
            this.btn_BrowseArchives.FlatAppearance.BorderSize = 0;
            this.btn_BrowseArchives.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_BrowseArchives.Location = new System.Drawing.Point(471, 30);
            this.btn_BrowseArchives.Name = "btn_BrowseArchives";
            this.btn_BrowseArchives.Size = new System.Drawing.Size(25, 20);
            this.btn_BrowseArchives.TabIndex = 19;
            this.btn_BrowseArchives.Text = "...";
            this.btn_BrowseArchives.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_BrowseArchives.UseVisualStyleBackColor = false;
            this.btn_BrowseArchives.Click += new System.EventHandler(this.Btn_BrowseArchives_Click);
            // 
            // text_ArchivesPath
            // 
            this.text_ArchivesPath.Location = new System.Drawing.Point(17, 30);
            this.text_ArchivesPath.Name = "text_ArchivesPath";
            this.text_ArchivesPath.Size = new System.Drawing.Size(449, 20);
            this.text_ArchivesPath.TabIndex = 0;
            // 
            // group_ArchivesPath
            // 
            this.group_ArchivesPath.Controls.Add(this.btn_BrowseArchives);
            this.group_ArchivesPath.Controls.Add(this.text_ArchivesPath);
            this.group_ArchivesPath.Location = new System.Drawing.Point(9, 220);
            this.group_ArchivesPath.Name = "group_ArchivesPath";
            this.group_ArchivesPath.Size = new System.Drawing.Size(513, 71);
            this.group_ArchivesPath.TabIndex = 21;
            this.group_ArchivesPath.TabStop = false;
            this.group_ArchivesPath.Text = "Archives Path";
            // 
            // btn_Restore
            // 
            this.btn_Restore.BackColor = System.Drawing.Color.Tomato;
            this.btn_Restore.FlatAppearance.BorderSize = 0;
            this.btn_Restore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Restore.Location = new System.Drawing.Point(9, 297);
            this.btn_Restore.Name = "btn_Restore";
            this.btn_Restore.Size = new System.Drawing.Size(103, 23);
            this.btn_Restore.TabIndex = 22;
            this.btn_Restore.Text = "Restore Defaults";
            this.btn_Restore.UseVisualStyleBackColor = false;
            this.btn_Restore.Click += new System.EventHandler(this.Btn_Restore_Click);
            // 
            // btn_AppPath
            // 
            this.btn_AppPath.BackColor = System.Drawing.Color.Tomato;
            this.btn_AppPath.FlatAppearance.BorderSize = 0;
            this.btn_AppPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AppPath.Location = new System.Drawing.Point(118, 297);
            this.btn_AppPath.Name = "btn_AppPath";
            this.btn_AppPath.Size = new System.Drawing.Size(125, 23);
            this.btn_AppPath.TabIndex = 23;
            this.btn_AppPath.Text = "Set to Application Path";
            this.btn_AppPath.UseVisualStyleBackColor = false;
            this.btn_AppPath.Click += new System.EventHandler(this.Btn_AppPath_Click);
            // 
            // btn_Confirm
            // 
            this.btn_Confirm.BackColor = System.Drawing.Color.LightGreen;
            this.btn_Confirm.FlatAppearance.BorderSize = 0;
            this.btn_Confirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Confirm.Location = new System.Drawing.Point(366, 297);
            this.btn_Confirm.Name = "btn_Confirm";
            this.btn_Confirm.Size = new System.Drawing.Size(75, 23);
            this.btn_Confirm.TabIndex = 24;
            this.btn_Confirm.Text = "Confirm";
            this.btn_Confirm.UseVisualStyleBackColor = false;
            this.btn_Confirm.Click += new System.EventHandler(this.Btn_Confirm_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.Tomato;
            this.btn_Cancel.FlatAppearance.BorderSize = 0;
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancel.Location = new System.Drawing.Point(447, 297);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 25;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // Paths
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(531, 330);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Confirm);
            this.Controls.Add(this.btn_AppPath);
            this.Controls.Add(this.btn_Restore);
            this.Controls.Add(this.group_ArchivesPath);
            this.Controls.Add(this.group_ToolsPath);
            this.Controls.Add(this.group_RootPath);
            this.Controls.Add(this.pnl_Backdrop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Paths";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Paths";
            this.Load += new System.EventHandler(this.Paths_Load);
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
        internal System.Windows.Forms.Button btn_BrowseRoot;
        private System.Windows.Forms.TextBox text_RootPath;
        internal System.Windows.Forms.Button btn_BrowseTools;
        private System.Windows.Forms.TextBox text_ToolsPath;
        private System.Windows.Forms.GroupBox group_ToolsPath;
        internal System.Windows.Forms.Button btn_BrowseArchives;
        private System.Windows.Forms.TextBox text_ArchivesPath;
        private System.Windows.Forms.GroupBox group_ArchivesPath;
        private System.Windows.Forms.Button btn_Restore;
        private System.Windows.Forms.Button btn_AppPath;
        private System.Windows.Forms.Button btn_Confirm;
        private System.Windows.Forms.FolderBrowserDialog fbd_Browse;
        internal System.Windows.Forms.Button btn_Cancel;
    }
}