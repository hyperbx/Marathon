namespace Toolkit
{
    partial class TextureConverter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextureConverter));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.mstrip_Options = new System.Windows.Forms.MenuStrip();
            this.main_Options = new System.Windows.Forms.ToolStripMenuItem();
            this.options_Modes = new System.Windows.Forms.ToolStripMenuItem();
            this.modes_DDStoPNG = new System.Windows.Forms.ToolStripMenuItem();
            this.modes_PNGtoDDS = new System.Windows.Forms.ToolStripMenuItem();
            this.options_Compression = new System.Windows.Forms.ToolStripMenuItem();
            this.compression_DXT1 = new System.Windows.Forms.ToolStripMenuItem();
            this.compression_DXT3 = new System.Windows.Forms.ToolStripMenuItem();
            this.compression_DXT5 = new System.Windows.Forms.ToolStripMenuItem();
            this.compression_A8R8G8B8 = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_DeselectAll = new System.Windows.Forms.Button();
            this.btn_SelectAll = new System.Windows.Forms.Button();
            this.clb_IMGs = new System.Windows.Forms.CheckedListBox();
            this.btn_Process = new System.Windows.Forms.Button();
            this.group_Preview = new System.Windows.Forms.GroupBox();
            this.pic_Preview = new System.Windows.Forms.PictureBox();
            this.tm_NoCheckOnClickTimer = new System.Windows.Forms.Timer(this.components);
            this.pic_Logo = new System.Windows.Forms.PictureBox();
            this.pnl_Backdrop = new System.Windows.Forms.Panel();
            this.lbl_Title = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.mstrip_Options.SuspendLayout();
            this.group_Preview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Preview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).BeginInit();
            this.pnl_Backdrop.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(7, 71);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.mstrip_Options);
            this.splitContainer1.Panel1.Controls.Add(this.btn_DeselectAll);
            this.splitContainer1.Panel1.Controls.Add(this.btn_SelectAll);
            this.splitContainer1.Panel1.Controls.Add(this.clb_IMGs);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btn_Process);
            this.splitContainer1.Panel2.Controls.Add(this.group_Preview);
            this.splitContainer1.Size = new System.Drawing.Size(791, 341);
            this.splitContainer1.SplitterDistance = 408;
            this.splitContainer1.TabIndex = 64;
            // 
            // mstrip_Options
            // 
            this.mstrip_Options.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mstrip_Options.BackColor = System.Drawing.SystemColors.Control;
            this.mstrip_Options.Dock = System.Windows.Forms.DockStyle.None;
            this.mstrip_Options.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.main_Options});
            this.mstrip_Options.Location = new System.Drawing.Point(339, 316);
            this.mstrip_Options.Name = "mstrip_Options";
            this.mstrip_Options.Size = new System.Drawing.Size(69, 24);
            this.mstrip_Options.TabIndex = 65;
            this.mstrip_Options.Text = "menuStrip1";
            // 
            // main_Options
            // 
            this.main_Options.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.options_Modes,
            this.options_Compression});
            this.main_Options.Name = "main_Options";
            this.main_Options.Size = new System.Drawing.Size(61, 20);
            this.main_Options.Text = "Options";
            // 
            // options_Modes
            // 
            this.options_Modes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modes_DDStoPNG,
            this.modes_PNGtoDDS});
            this.options_Modes.Name = "options_Modes";
            this.options_Modes.Size = new System.Drawing.Size(144, 22);
            this.options_Modes.Text = "Modes";
            // 
            // modes_DDStoPNG
            // 
            this.modes_DDStoPNG.CheckOnClick = true;
            this.modes_DDStoPNG.Name = "modes_DDStoPNG";
            this.modes_DDStoPNG.Size = new System.Drawing.Size(137, 22);
            this.modes_DDStoPNG.Text = "DDS to PNG";
            this.modes_DDStoPNG.CheckedChanged += new System.EventHandler(this.Modes_DDStoPNG_CheckedChanged);
            // 
            // modes_PNGtoDDS
            // 
            this.modes_PNGtoDDS.CheckOnClick = true;
            this.modes_PNGtoDDS.Name = "modes_PNGtoDDS";
            this.modes_PNGtoDDS.Size = new System.Drawing.Size(137, 22);
            this.modes_PNGtoDDS.Text = "PNG to DDS";
            this.modes_PNGtoDDS.CheckedChanged += new System.EventHandler(this.Modes_PNGtoDDS_CheckedChanged);
            // 
            // options_Compression
            // 
            this.options_Compression.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compression_DXT1,
            this.compression_DXT3,
            this.compression_DXT5,
            this.compression_A8R8G8B8});
            this.options_Compression.Name = "options_Compression";
            this.options_Compression.Size = new System.Drawing.Size(144, 22);
            this.options_Compression.Text = "Compression";
            this.options_Compression.Visible = false;
            // 
            // compression_DXT1
            // 
            this.compression_DXT1.CheckOnClick = true;
            this.compression_DXT1.Name = "compression_DXT1";
            this.compression_DXT1.Size = new System.Drawing.Size(240, 22);
            this.compression_DXT1.Text = "DXT1 (Opaque/1-bit Alpha)";
            this.compression_DXT1.CheckedChanged += new System.EventHandler(this.Compression_DXT1_CheckedChanged);
            // 
            // compression_DXT3
            // 
            this.compression_DXT3.CheckOnClick = true;
            this.compression_DXT3.Name = "compression_DXT3";
            this.compression_DXT3.Size = new System.Drawing.Size(240, 22);
            this.compression_DXT3.Text = "DXT3 (Explicit/4-bit Alpha)";
            this.compression_DXT3.CheckedChanged += new System.EventHandler(this.Compression_DXT3_CheckedChanged);
            // 
            // compression_DXT5
            // 
            this.compression_DXT5.CheckOnClick = true;
            this.compression_DXT5.Name = "compression_DXT5";
            this.compression_DXT5.Size = new System.Drawing.Size(240, 22);
            this.compression_DXT5.Text = "DXT5 (Interpolated/8-bit Alpha)";
            this.compression_DXT5.CheckedChanged += new System.EventHandler(this.Compression_DXT5_CheckedChanged);
            // 
            // compression_A8R8G8B8
            // 
            this.compression_A8R8G8B8.CheckOnClick = true;
            this.compression_A8R8G8B8.Name = "compression_A8R8G8B8";
            this.compression_A8R8G8B8.Size = new System.Drawing.Size(240, 22);
            this.compression_A8R8G8B8.Text = "A8R8G8B8";
            this.compression_A8R8G8B8.CheckedChanged += new System.EventHandler(this.Compression_A8R8G8B8_CheckedChanged);
            // 
            // btn_DeselectAll
            // 
            this.btn_DeselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_DeselectAll.BackColor = System.Drawing.Color.Tomato;
            this.btn_DeselectAll.FlatAppearance.BorderSize = 0;
            this.btn_DeselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DeselectAll.Location = new System.Drawing.Point(82, 316);
            this.btn_DeselectAll.Name = "btn_DeselectAll";
            this.btn_DeselectAll.Size = new System.Drawing.Size(75, 23);
            this.btn_DeselectAll.TabIndex = 64;
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
            this.btn_SelectAll.Location = new System.Drawing.Point(0, 316);
            this.btn_SelectAll.Name = "btn_SelectAll";
            this.btn_SelectAll.Size = new System.Drawing.Size(75, 23);
            this.btn_SelectAll.TabIndex = 63;
            this.btn_SelectAll.Text = "Select All";
            this.btn_SelectAll.UseVisualStyleBackColor = false;
            this.btn_SelectAll.Click += new System.EventHandler(this.Btn_SelectAll_Click);
            // 
            // clb_IMGs
            // 
            this.clb_IMGs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clb_IMGs.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.clb_IMGs.FormattingEnabled = true;
            this.clb_IMGs.Location = new System.Drawing.Point(0, 4);
            this.clb_IMGs.Name = "clb_IMGs";
            this.clb_IMGs.Size = new System.Drawing.Size(406, 304);
            this.clb_IMGs.TabIndex = 57;
            this.clb_IMGs.SelectedIndexChanged += new System.EventHandler(this.Clb_IMGs_SelectedIndexChanged);
            // 
            // btn_Process
            // 
            this.btn_Process.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Process.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_Process.Enabled = false;
            this.btn_Process.FlatAppearance.BorderSize = 0;
            this.btn_Process.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Process.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Process.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Process.Location = new System.Drawing.Point(2, 316);
            this.btn_Process.Name = "btn_Process";
            this.btn_Process.Size = new System.Drawing.Size(377, 23);
            this.btn_Process.TabIndex = 60;
            this.btn_Process.Text = "Convert";
            this.btn_Process.UseVisualStyleBackColor = false;
            this.btn_Process.Click += new System.EventHandler(this.Btn_Process_Click);
            // 
            // group_Preview
            // 
            this.group_Preview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.group_Preview.Controls.Add(this.pic_Preview);
            this.group_Preview.Location = new System.Drawing.Point(2, -2);
            this.group_Preview.Name = "group_Preview";
            this.group_Preview.Size = new System.Drawing.Size(377, 311);
            this.group_Preview.TabIndex = 61;
            this.group_Preview.TabStop = false;
            this.group_Preview.Text = "Preview";
            // 
            // pic_Preview
            // 
            this.pic_Preview.BackgroundImage = global::Toolkit.Properties.Resources.logo_exception;
            this.pic_Preview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic_Preview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic_Preview.Location = new System.Drawing.Point(3, 16);
            this.pic_Preview.Name = "pic_Preview";
            this.pic_Preview.Size = new System.Drawing.Size(371, 292);
            this.pic_Preview.TabIndex = 0;
            this.pic_Preview.TabStop = false;
            // 
            // tm_NoCheckOnClickTimer
            // 
            this.tm_NoCheckOnClickTimer.Interval = 10;
            this.tm_NoCheckOnClickTimer.Tick += new System.EventHandler(this.Tm_NoCheckOnClickTimer_Tick);
            // 
            // pic_Logo
            // 
            this.pic_Logo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_Logo.BackColor = System.Drawing.Color.Transparent;
            this.pic_Logo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_Logo.BackgroundImage")));
            this.pic_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic_Logo.Location = new System.Drawing.Point(731, 1);
            this.pic_Logo.Name = "pic_Logo";
            this.pic_Logo.Size = new System.Drawing.Size(67, 67);
            this.pic_Logo.TabIndex = 11;
            this.pic_Logo.TabStop = false;
            // 
            // pnl_Backdrop
            // 
            this.pnl_Backdrop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_Backdrop.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.pnl_Backdrop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_Backdrop.BackgroundImage")));
            this.pnl_Backdrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Backdrop.Controls.Add(this.pic_Logo);
            this.pnl_Backdrop.Controls.Add(this.lbl_Title);
            this.pnl_Backdrop.Location = new System.Drawing.Point(-1, -1);
            this.pnl_Backdrop.Name = "pnl_Backdrop";
            this.pnl_Backdrop.Size = new System.Drawing.Size(808, 69);
            this.pnl_Backdrop.TabIndex = 63;
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Title.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_Title.Location = new System.Drawing.Point(14, 10);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(318, 47);
            this.lbl_Title.TabIndex = 1;
            this.lbl_Title.Text = "Texture Converter";
            // 
            // TextureConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 419);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pnl_Backdrop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(821, 458);
            this.Name = "TextureConverter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Texture Converter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TextureConverter_FormClosing);
            this.Load += new System.EventHandler(this.TextureConverter_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.mstrip_Options.ResumeLayout(false);
            this.mstrip_Options.PerformLayout();
            this.group_Preview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Preview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).EndInit();
            this.pnl_Backdrop.ResumeLayout(false);
            this.pnl_Backdrop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckedListBox clb_IMGs;
        private System.Windows.Forms.Button btn_Process;
        private System.Windows.Forms.GroupBox group_Preview;
        private System.Windows.Forms.Timer tm_NoCheckOnClickTimer;
        internal System.Windows.Forms.PictureBox pic_Logo;
        private System.Windows.Forms.Panel pnl_Backdrop;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.PictureBox pic_Preview;
        private System.Windows.Forms.Button btn_DeselectAll;
        private System.Windows.Forms.Button btn_SelectAll;
        private System.Windows.Forms.MenuStrip mstrip_Options;
        private System.Windows.Forms.ToolStripMenuItem main_Options;
        private System.Windows.Forms.ToolStripMenuItem options_Modes;
        private System.Windows.Forms.ToolStripMenuItem modes_DDStoPNG;
        private System.Windows.Forms.ToolStripMenuItem modes_PNGtoDDS;
        private System.Windows.Forms.ToolStripMenuItem options_Compression;
        private System.Windows.Forms.ToolStripMenuItem compression_DXT1;
        private System.Windows.Forms.ToolStripMenuItem compression_DXT3;
        private System.Windows.Forms.ToolStripMenuItem compression_DXT5;
        private System.Windows.Forms.ToolStripMenuItem compression_A8R8G8B8;
    }
}