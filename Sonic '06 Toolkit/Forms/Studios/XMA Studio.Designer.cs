namespace Sonic_06_Toolkit
{
    partial class XMA_Studio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XMA_Studio));
            this.looping_SetLoop = new System.Windows.Forms.ToolStripMenuItem();
            this.looping_Whole = new System.Windows.Forms.ToolStripMenuItem();
            this.options_Looping = new System.Windows.Forms.ToolStripMenuItem();
            this.modes_WAVtoXMA = new System.Windows.Forms.ToolStripMenuItem();
            this.modes_XMAtoWAV = new System.Windows.Forms.ToolStripMenuItem();
            this.options_Modes = new System.Windows.Forms.ToolStripMenuItem();
            this.main_Options = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Convert = new System.Windows.Forms.Button();
            this.btn_DeselectAll = new System.Windows.Forms.Button();
            this.btn_SelectAll = new System.Windows.Forms.Button();
            this.clb_XMA = new System.Windows.Forms.CheckedListBox();
            this.mstrip_Options = new System.Windows.Forms.MenuStrip();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.pnl_Backdrop = new System.Windows.Forms.Panel();
            this.options_PatchXMA = new System.Windows.Forms.ToolStripMenuItem();
            this.pic_Logo = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mstrip_Options.SuspendLayout();
            this.pnl_Backdrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // looping_SetLoop
            // 
            this.looping_SetLoop.Name = "looping_SetLoop";
            this.looping_SetLoop.Size = new System.Drawing.Size(172, 22);
            this.looping_SetLoop.Text = "Set loop position...";
            this.looping_SetLoop.Visible = false;
            // 
            // looping_Whole
            // 
            this.looping_Whole.CheckOnClick = true;
            this.looping_Whole.Name = "looping_Whole";
            this.looping_Whole.Size = new System.Drawing.Size(180, 22);
            this.looping_Whole.Text = "Loop entire track";
            this.looping_Whole.CheckedChanged += new System.EventHandler(this.Looping_Whole_CheckedChanged);
            // 
            // options_Looping
            // 
            this.options_Looping.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.looping_Whole,
            this.looping_SetLoop});
            this.options_Looping.Name = "options_Looping";
            this.options_Looping.Size = new System.Drawing.Size(180, 22);
            this.options_Looping.Text = "Looping";
            this.options_Looping.Visible = false;
            // 
            // modes_WAVtoXMA
            // 
            this.modes_WAVtoXMA.CheckOnClick = true;
            this.modes_WAVtoXMA.Name = "modes_WAVtoXMA";
            this.modes_WAVtoXMA.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.W)));
            this.modes_WAVtoXMA.Size = new System.Drawing.Size(183, 22);
            this.modes_WAVtoXMA.Text = "WAV to XMA";
            this.modes_WAVtoXMA.CheckedChanged += new System.EventHandler(this.Modes_WAVtoXMA_CheckedChanged);
            // 
            // modes_XMAtoWAV
            // 
            this.modes_XMAtoWAV.CheckOnClick = true;
            this.modes_XMAtoWAV.Name = "modes_XMAtoWAV";
            this.modes_XMAtoWAV.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.modes_XMAtoWAV.Size = new System.Drawing.Size(183, 22);
            this.modes_XMAtoWAV.Text = "XMA to WAV";
            this.modes_XMAtoWAV.CheckedChanged += new System.EventHandler(this.Modes_XMAtoWAV_CheckedChanged);
            // 
            // options_Modes
            // 
            this.options_Modes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modes_XMAtoWAV,
            this.modes_WAVtoXMA});
            this.options_Modes.Name = "options_Modes";
            this.options_Modes.Size = new System.Drawing.Size(133, 22);
            this.options_Modes.Text = "Modes";
            // 
            // main_Options
            // 
            this.main_Options.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.options_Modes,
            this.options_Looping,
            this.options_PatchXMA});
            this.main_Options.Name = "main_Options";
            this.main_Options.Size = new System.Drawing.Size(61, 20);
            this.main_Options.Text = "Options";
            // 
            // btn_Convert
            // 
            this.btn_Convert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Convert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(121)))), ((int)(((byte)(59)))));
            this.btn_Convert.Enabled = false;
            this.btn_Convert.FlatAppearance.BorderSize = 0;
            this.btn_Convert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Convert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Convert.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Convert.Location = new System.Drawing.Point(350, 387);
            this.btn_Convert.Name = "btn_Convert";
            this.btn_Convert.Size = new System.Drawing.Size(75, 23);
            this.btn_Convert.TabIndex = 55;
            this.btn_Convert.Text = "Encode";
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
            this.btn_DeselectAll.TabIndex = 54;
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
            this.btn_SelectAll.TabIndex = 53;
            this.btn_SelectAll.Text = "Select All";
            this.btn_SelectAll.UseVisualStyleBackColor = false;
            this.btn_SelectAll.Click += new System.EventHandler(this.Btn_SelectAll_Click);
            // 
            // clb_XMA
            // 
            this.clb_XMA.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clb_XMA.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.clb_XMA.CheckOnClick = true;
            this.clb_XMA.FormattingEnabled = true;
            this.clb_XMA.Location = new System.Drawing.Point(8, 76);
            this.clb_XMA.Name = "clb_XMA";
            this.clb_XMA.Size = new System.Drawing.Size(417, 304);
            this.clb_XMA.TabIndex = 52;
            this.clb_XMA.SelectedIndexChanged += new System.EventHandler(this.Clb_XMA_SelectedIndexChanged);
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
            this.mstrip_Options.TabIndex = 56;
            this.mstrip_Options.Text = "menuStrip1";
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_Title.Location = new System.Drawing.Point(14, 3);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(219, 47);
            this.lbl_Title.TabIndex = 1;
            this.lbl_Title.Text = "XMA Studio";
            // 
            // pnl_Backdrop
            // 
            this.pnl_Backdrop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_Backdrop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(121)))), ((int)(((byte)(59)))));
            this.pnl_Backdrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Backdrop.Controls.Add(this.label1);
            this.pnl_Backdrop.Controls.Add(this.pic_Logo);
            this.pnl_Backdrop.Controls.Add(this.lbl_Title);
            this.pnl_Backdrop.Location = new System.Drawing.Point(-3, -2);
            this.pnl_Backdrop.Name = "pnl_Backdrop";
            this.pnl_Backdrop.Size = new System.Drawing.Size(440, 69);
            this.pnl_Backdrop.TabIndex = 51;
            // 
            // options_PatchXMA
            // 
            this.options_PatchXMA.CheckOnClick = true;
            this.options_PatchXMA.Name = "options_PatchXMA";
            this.options_PatchXMA.Size = new System.Drawing.Size(180, 22);
            this.options_PatchXMA.Text = "Patch XMA";
            this.options_PatchXMA.Visible = false;
            this.options_PatchXMA.CheckedChanged += new System.EventHandler(this.Options_PatchXMA_CheckedChanged);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(19, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "experimental";
            // 
            // XMA_Studio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 419);
            this.Controls.Add(this.btn_Convert);
            this.Controls.Add(this.btn_DeselectAll);
            this.Controls.Add(this.btn_SelectAll);
            this.Controls.Add(this.clb_XMA);
            this.Controls.Add(this.mstrip_Options);
            this.Controls.Add(this.pnl_Backdrop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(450, 458);
            this.Name = "XMA_Studio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "XMA Studio (experimental)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.XMA_Studio_FormClosing);
            this.Load += new System.EventHandler(this.XMA_Studio_Load);
            this.mstrip_Options.ResumeLayout(false);
            this.mstrip_Options.PerformLayout();
            this.pnl_Backdrop.ResumeLayout(false);
            this.pnl_Backdrop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem looping_SetLoop;
        private System.Windows.Forms.ToolStripMenuItem looping_Whole;
        private System.Windows.Forms.ToolStripMenuItem options_Looping;
        private System.Windows.Forms.ToolStripMenuItem modes_WAVtoXMA;
        private System.Windows.Forms.ToolStripMenuItem modes_XMAtoWAV;
        private System.Windows.Forms.ToolStripMenuItem options_Modes;
        private System.Windows.Forms.ToolStripMenuItem main_Options;
        private System.Windows.Forms.Button btn_Convert;
        private System.Windows.Forms.Button btn_DeselectAll;
        private System.Windows.Forms.Button btn_SelectAll;
        private System.Windows.Forms.CheckedListBox clb_XMA;
        private System.Windows.Forms.MenuStrip mstrip_Options;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Panel pnl_Backdrop;
        internal System.Windows.Forms.PictureBox pic_Logo;
        private System.Windows.Forms.ToolStripMenuItem options_PatchXMA;
        private System.Windows.Forms.Label label1;
    }
}