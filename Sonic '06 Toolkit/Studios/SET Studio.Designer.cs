namespace Sonic_06_Toolkit
{
    partial class SET_Studio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SET_Studio));
            this.pnl_Backdrop = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pic_Logo = new System.Windows.Forms.PictureBox();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.btn_Convert = new System.Windows.Forms.Button();
            this.btn_DeselectAll = new System.Windows.Forms.Button();
            this.btn_SelectAll = new System.Windows.Forms.Button();
            this.clb_SETs = new System.Windows.Forms.CheckedListBox();
            this.mstrip_Options = new System.Windows.Forms.MenuStrip();
            this.main_Options = new System.Windows.Forms.ToolStripMenuItem();
            this.options_Modes = new System.Windows.Forms.ToolStripMenuItem();
            this.modes_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.modes_Import = new System.Windows.Forms.ToolStripMenuItem();
            this.options_CreateBackupSET = new System.Windows.Forms.ToolStripMenuItem();
            this.options_DeleteXML = new System.Windows.Forms.ToolStripMenuItem();
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
            this.pnl_Backdrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Backdrop.Controls.Add(this.label1);
            this.pnl_Backdrop.Controls.Add(this.pic_Logo);
            this.pnl_Backdrop.Controls.Add(this.lbl_Title);
            this.pnl_Backdrop.Location = new System.Drawing.Point(-3, -2);
            this.pnl_Backdrop.Name = "pnl_Backdrop";
            this.pnl_Backdrop.Size = new System.Drawing.Size(440, 69);
            this.pnl_Backdrop.TabIndex = 36;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(19, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "experimental";
            // 
            // pic_Logo
            // 
            this.pic_Logo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_Logo.BackColor = System.Drawing.Color.Transparent;
            this.pic_Logo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_Logo.BackgroundImage")));
            this.pic_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic_Logo.Location = new System.Drawing.Point(363, 1);
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
            this.lbl_Title.Location = new System.Drawing.Point(14, 3);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(197, 47);
            this.lbl_Title.TabIndex = 1;
            this.lbl_Title.Text = "SET Studio";
            // 
            // btn_Convert
            // 
            this.btn_Convert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Convert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.btn_Convert.Enabled = false;
            this.btn_Convert.FlatAppearance.BorderSize = 0;
            this.btn_Convert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Convert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Convert.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Convert.Location = new System.Drawing.Point(350, 387);
            this.btn_Convert.Name = "btn_Convert";
            this.btn_Convert.Size = new System.Drawing.Size(75, 23);
            this.btn_Convert.TabIndex = 40;
            this.btn_Convert.Text = "Export";
            this.btn_Convert.UseVisualStyleBackColor = false;
            this.btn_Convert.Click += new System.EventHandler(this.Btn_Convert_Click);
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
            this.btn_DeselectAll.TabIndex = 39;
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
            this.btn_SelectAll.TabIndex = 38;
            this.btn_SelectAll.Text = "Select All";
            this.btn_SelectAll.UseVisualStyleBackColor = false;
            this.btn_SelectAll.Click += new System.EventHandler(this.Btn_SelectAll_Click);
            // 
            // clb_SETs
            // 
            this.clb_SETs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clb_SETs.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.clb_SETs.CheckOnClick = true;
            this.clb_SETs.FormattingEnabled = true;
            this.clb_SETs.Location = new System.Drawing.Point(8, 76);
            this.clb_SETs.Name = "clb_SETs";
            this.clb_SETs.Size = new System.Drawing.Size(417, 304);
            this.clb_SETs.TabIndex = 37;
            this.clb_SETs.SelectedIndexChanged += new System.EventHandler(this.Clb_SETs_SelectedIndexChanged);
            // 
            // mstrip_Options
            // 
            this.mstrip_Options.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mstrip_Options.BackColor = System.Drawing.SystemColors.Control;
            this.mstrip_Options.Dock = System.Windows.Forms.DockStyle.None;
            this.mstrip_Options.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.main_Options});
            this.mstrip_Options.Location = new System.Drawing.Point(278, 386);
            this.mstrip_Options.Name = "mstrip_Options";
            this.mstrip_Options.Size = new System.Drawing.Size(69, 24);
            this.mstrip_Options.TabIndex = 44;
            this.mstrip_Options.Text = "menuStrip1";
            // 
            // main_Options
            // 
            this.main_Options.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.options_Modes,
            this.options_CreateBackupSET,
            this.options_DeleteXML});
            this.main_Options.Name = "main_Options";
            this.main_Options.Size = new System.Drawing.Size(61, 20);
            this.main_Options.Text = "Options";
            // 
            // options_Modes
            // 
            this.options_Modes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modes_Export,
            this.modes_Import});
            this.options_Modes.Name = "options_Modes";
            this.options_Modes.Size = new System.Drawing.Size(190, 22);
            this.options_Modes.Text = "Modes";
            // 
            // modes_Export
            // 
            this.modes_Export.CheckOnClick = true;
            this.modes_Export.Name = "modes_Export";
            this.modes_Export.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
            this.modes_Export.Size = new System.Drawing.Size(143, 22);
            this.modes_Export.Text = "Export";
            this.modes_Export.CheckedChanged += new System.EventHandler(this.Modes_Export_CheckedChanged);
            // 
            // modes_Import
            // 
            this.modes_Import.CheckOnClick = true;
            this.modes_Import.Name = "modes_Import";
            this.modes_Import.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.I)));
            this.modes_Import.Size = new System.Drawing.Size(143, 22);
            this.modes_Import.Text = "Import";
            this.modes_Import.CheckedChanged += new System.EventHandler(this.Modes_Import_CheckedChanged);
            // 
            // options_CreateBackupSET
            // 
            this.options_CreateBackupSET.CheckOnClick = true;
            this.options_CreateBackupSET.Name = "options_CreateBackupSET";
            this.options_CreateBackupSET.Size = new System.Drawing.Size(190, 22);
            this.options_CreateBackupSET.Text = "Create a backup SET";
            this.options_CreateBackupSET.Visible = false;
            this.options_CreateBackupSET.CheckedChanged += new System.EventHandler(this.Options_CreateBackupSET_CheckedChanged);
            // 
            // options_DeleteXML
            // 
            this.options_DeleteXML.CheckOnClick = true;
            this.options_DeleteXML.Name = "options_DeleteXML";
            this.options_DeleteXML.Size = new System.Drawing.Size(190, 22);
            this.options_DeleteXML.Text = "Delete XML on Import";
            this.options_DeleteXML.Visible = false;
            this.options_DeleteXML.CheckedChanged += new System.EventHandler(this.Options_DeleteXML_CheckedChanged);
            // 
            // SET_Studio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 419);
            this.Controls.Add(this.mstrip_Options);
            this.Controls.Add(this.pnl_Backdrop);
            this.Controls.Add(this.btn_Convert);
            this.Controls.Add(this.btn_DeselectAll);
            this.Controls.Add(this.btn_SelectAll);
            this.Controls.Add(this.clb_SETs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "SET_Studio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SET Studio (experimental)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SET_Studio_FormClosing);
            this.Load += new System.EventHandler(this.SET_Studio_Load);
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
        private System.Windows.Forms.Button btn_Convert;
        private System.Windows.Forms.Button btn_DeselectAll;
        private System.Windows.Forms.Button btn_SelectAll;
        private System.Windows.Forms.CheckedListBox clb_SETs;
        private System.Windows.Forms.MenuStrip mstrip_Options;
        private System.Windows.Forms.ToolStripMenuItem main_Options;
        private System.Windows.Forms.ToolStripMenuItem options_Modes;
        private System.Windows.Forms.ToolStripMenuItem modes_Export;
        private System.Windows.Forms.ToolStripMenuItem modes_Import;
        private System.Windows.Forms.ToolStripMenuItem options_CreateBackupSET;
        private System.Windows.Forms.ToolStripMenuItem options_DeleteXML;
        private System.Windows.Forms.Label label1;
    }
}