namespace Toolkit.Tools
{
    partial class ExecutableModification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExecutableModification));
            this.pnl_Backdrop = new System.Windows.Forms.Panel();
            this.pic_Logo = new System.Windows.Forms.PictureBox();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.mstrip_Options = new System.Windows.Forms.MenuStrip();
            this.main_Options = new System.Windows.Forms.ToolStripMenuItem();
            this.options_Encryption = new System.Windows.Forms.ToolStripMenuItem();
            this.encryption_Encrypt = new System.Windows.Forms.ToolStripMenuItem();
            this.encryption_Decrypt = new System.Windows.Forms.ToolStripMenuItem();
            this.options_Compression = new System.Windows.Forms.ToolStripMenuItem();
            this.compression_Compress = new System.Windows.Forms.ToolStripMenuItem();
            this.compression_Decompress = new System.Windows.Forms.ToolStripMenuItem();
            this.options_System = new System.Windows.Forms.ToolStripMenuItem();
            this.system_Retail = new System.Windows.Forms.ToolStripMenuItem();
            this.system_Developer = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Process = new System.Windows.Forms.Button();
            this.btn_DeselectAll = new System.Windows.Forms.Button();
            this.btn_SelectAll = new System.Windows.Forms.Button();
            this.clb_XEXs = new System.Windows.Forms.CheckedListBox();
            this.tm_ProcessCheck = new System.Windows.Forms.Timer(this.components);
            this.pnl_Backdrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).BeginInit();
            this.mstrip_Options.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Backdrop
            // 
            this.pnl_Backdrop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_Backdrop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.pnl_Backdrop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_Backdrop.BackgroundImage")));
            this.pnl_Backdrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Backdrop.Controls.Add(this.pic_Logo);
            this.pnl_Backdrop.Controls.Add(this.lbl_Title);
            this.pnl_Backdrop.Location = new System.Drawing.Point(-3, -1);
            this.pnl_Backdrop.Name = "pnl_Backdrop";
            this.pnl_Backdrop.Size = new System.Drawing.Size(522, 69);
            this.pnl_Backdrop.TabIndex = 63;
            // 
            // pic_Logo
            // 
            this.pic_Logo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_Logo.BackColor = System.Drawing.Color.Transparent;
            this.pic_Logo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_Logo.BackgroundImage")));
            this.pic_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic_Logo.Location = new System.Drawing.Point(444, 1);
            this.pic_Logo.Name = "pic_Logo";
            this.pic_Logo.Size = new System.Drawing.Size(67, 67);
            this.pic_Logo.TabIndex = 11;
            this.pic_Logo.TabStop = false;
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Title.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_Title.Location = new System.Drawing.Point(13, 9);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(420, 47);
            this.lbl_Title.TabIndex = 1;
            this.lbl_Title.Text = "Executable Modification";
            // 
            // mstrip_Options
            // 
            this.mstrip_Options.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mstrip_Options.BackColor = System.Drawing.SystemColors.Control;
            this.mstrip_Options.Dock = System.Windows.Forms.DockStyle.None;
            this.mstrip_Options.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.main_Options});
            this.mstrip_Options.Location = new System.Drawing.Point(361, 387);
            this.mstrip_Options.Name = "mstrip_Options";
            this.mstrip_Options.Size = new System.Drawing.Size(69, 24);
            this.mstrip_Options.TabIndex = 68;
            this.mstrip_Options.Text = "menuStrip1";
            // 
            // main_Options
            // 
            this.main_Options.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.options_Encryption,
            this.options_Compression,
            this.options_System});
            this.main_Options.Name = "main_Options";
            this.main_Options.Size = new System.Drawing.Size(61, 20);
            this.main_Options.Text = "Options";
            // 
            // options_Encryption
            // 
            this.options_Encryption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.encryption_Encrypt,
            this.encryption_Decrypt});
            this.options_Encryption.Name = "options_Encryption";
            this.options_Encryption.Size = new System.Drawing.Size(144, 22);
            this.options_Encryption.Text = "Encryption";
            // 
            // encryption_Encrypt
            // 
            this.encryption_Encrypt.CheckOnClick = true;
            this.encryption_Encrypt.Name = "encryption_Encrypt";
            this.encryption_Encrypt.Size = new System.Drawing.Size(115, 22);
            this.encryption_Encrypt.Text = "Encrypt";
            this.encryption_Encrypt.CheckedChanged += new System.EventHandler(this.Encryption_Encrypt_CheckedChanged);
            // 
            // encryption_Decrypt
            // 
            this.encryption_Decrypt.CheckOnClick = true;
            this.encryption_Decrypt.Name = "encryption_Decrypt";
            this.encryption_Decrypt.Size = new System.Drawing.Size(115, 22);
            this.encryption_Decrypt.Text = "Decrypt";
            this.encryption_Decrypt.CheckedChanged += new System.EventHandler(this.Encryption_Decrypt_CheckedChanged);
            // 
            // options_Compression
            // 
            this.options_Compression.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compression_Compress,
            this.compression_Decompress});
            this.options_Compression.Name = "options_Compression";
            this.options_Compression.Size = new System.Drawing.Size(144, 22);
            this.options_Compression.Text = "Compression";
            // 
            // compression_Compress
            // 
            this.compression_Compress.CheckOnClick = true;
            this.compression_Compress.Name = "compression_Compress";
            this.compression_Compress.Size = new System.Drawing.Size(139, 22);
            this.compression_Compress.Text = "Compress";
            this.compression_Compress.CheckedChanged += new System.EventHandler(this.Compression_Compress_CheckedChanged);
            // 
            // compression_Decompress
            // 
            this.compression_Decompress.CheckOnClick = true;
            this.compression_Decompress.Name = "compression_Decompress";
            this.compression_Decompress.Size = new System.Drawing.Size(139, 22);
            this.compression_Decompress.Text = "Decompress";
            this.compression_Decompress.CheckedChanged += new System.EventHandler(this.Compression_Decompress_CheckedChanged);
            // 
            // options_System
            // 
            this.options_System.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.system_Retail,
            this.system_Developer});
            this.options_System.Name = "options_System";
            this.options_System.Size = new System.Drawing.Size(144, 22);
            this.options_System.Text = "System";
            // 
            // system_Retail
            // 
            this.system_Retail.CheckOnClick = true;
            this.system_Retail.Name = "system_Retail";
            this.system_Retail.Size = new System.Drawing.Size(127, 22);
            this.system_Retail.Text = "Retail";
            this.system_Retail.CheckedChanged += new System.EventHandler(this.System_Retail_CheckedChanged);
            // 
            // system_Developer
            // 
            this.system_Developer.CheckOnClick = true;
            this.system_Developer.Name = "system_Developer";
            this.system_Developer.Size = new System.Drawing.Size(127, 22);
            this.system_Developer.Text = "Developer";
            this.system_Developer.CheckedChanged += new System.EventHandler(this.System_Developer_CheckedChanged);
            // 
            // btn_Process
            // 
            this.btn_Process.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Process.BackColor = System.Drawing.Color.ForestGreen;
            this.btn_Process.Enabled = false;
            this.btn_Process.FlatAppearance.BorderSize = 0;
            this.btn_Process.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Process.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Process.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Process.Location = new System.Drawing.Point(433, 388);
            this.btn_Process.Name = "btn_Process";
            this.btn_Process.Size = new System.Drawing.Size(75, 23);
            this.btn_Process.TabIndex = 67;
            this.btn_Process.Text = "Process";
            this.btn_Process.UseVisualStyleBackColor = false;
            this.btn_Process.Click += new System.EventHandler(this.Btn_Process_Click);
            // 
            // btn_DeselectAll
            // 
            this.btn_DeselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_DeselectAll.BackColor = System.Drawing.Color.Tomato;
            this.btn_DeselectAll.FlatAppearance.BorderSize = 0;
            this.btn_DeselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DeselectAll.Location = new System.Drawing.Point(91, 388);
            this.btn_DeselectAll.Name = "btn_DeselectAll";
            this.btn_DeselectAll.Size = new System.Drawing.Size(75, 23);
            this.btn_DeselectAll.TabIndex = 66;
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
            this.btn_SelectAll.Location = new System.Drawing.Point(9, 388);
            this.btn_SelectAll.Name = "btn_SelectAll";
            this.btn_SelectAll.Size = new System.Drawing.Size(75, 23);
            this.btn_SelectAll.TabIndex = 65;
            this.btn_SelectAll.Text = "Select All";
            this.btn_SelectAll.UseVisualStyleBackColor = false;
            this.btn_SelectAll.Click += new System.EventHandler(this.Btn_SelectAll_Click);
            // 
            // clb_XEXs
            // 
            this.clb_XEXs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clb_XEXs.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.clb_XEXs.CheckOnClick = true;
            this.clb_XEXs.FormattingEnabled = true;
            this.clb_XEXs.Location = new System.Drawing.Point(9, 77);
            this.clb_XEXs.Name = "clb_XEXs";
            this.clb_XEXs.Size = new System.Drawing.Size(499, 304);
            this.clb_XEXs.TabIndex = 64;
            this.clb_XEXs.SelectedIndexChanged += new System.EventHandler(this.Clb_XEXs_SelectedIndexChanged);
            // 
            // tm_ProcessCheck
            // 
            this.tm_ProcessCheck.Interval = 10;
            this.tm_ProcessCheck.Tick += new System.EventHandler(this.Tm_ProcessCheck_Tick);
            // 
            // ExecutableModification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 419);
            this.Controls.Add(this.pnl_Backdrop);
            this.Controls.Add(this.mstrip_Options);
            this.Controls.Add(this.btn_Process);
            this.Controls.Add(this.btn_DeselectAll);
            this.Controls.Add(this.btn_SelectAll);
            this.Controls.Add(this.clb_XEXs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(532, 458);
            this.Name = "ExecutableModification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Executable Modification";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExecutableModification_FormClosing);
            this.Load += new System.EventHandler(this.ExecutableModification_Load);
            this.pnl_Backdrop.ResumeLayout(false);
            this.pnl_Backdrop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).EndInit();
            this.mstrip_Options.ResumeLayout(false);
            this.mstrip_Options.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Backdrop;
        internal System.Windows.Forms.PictureBox pic_Logo;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.MenuStrip mstrip_Options;
        private System.Windows.Forms.ToolStripMenuItem main_Options;
        private System.Windows.Forms.ToolStripMenuItem options_Encryption;
        private System.Windows.Forms.ToolStripMenuItem encryption_Encrypt;
        private System.Windows.Forms.ToolStripMenuItem encryption_Decrypt;
        private System.Windows.Forms.Button btn_Process;
        private System.Windows.Forms.Button btn_DeselectAll;
        private System.Windows.Forms.Button btn_SelectAll;
        private System.Windows.Forms.CheckedListBox clb_XEXs;
        private System.Windows.Forms.ToolStripMenuItem options_Compression;
        private System.Windows.Forms.ToolStripMenuItem compression_Compress;
        private System.Windows.Forms.ToolStripMenuItem compression_Decompress;
        private System.Windows.Forms.ToolStripMenuItem options_System;
        private System.Windows.Forms.ToolStripMenuItem system_Retail;
        private System.Windows.Forms.ToolStripMenuItem system_Developer;
        private System.Windows.Forms.Timer tm_ProcessCheck;
    }
}