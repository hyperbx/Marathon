namespace Sonic_06_Toolkit
{
    partial class ADX_Studio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ADX_Studio));
            this.pnl_Backdrop = new System.Windows.Forms.Panel();
            this.pic_Logo = new System.Windows.Forms.PictureBox();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.btn_Convert = new System.Windows.Forms.Button();
            this.btn_DeselectAll = new System.Windows.Forms.Button();
            this.btn_SelectAll = new System.Windows.Forms.Button();
            this.clb_ADX = new System.Windows.Forms.CheckedListBox();
            this.mstrip_Options = new System.Windows.Forms.MenuStrip();
            this.main_Options = new System.Windows.Forms.ToolStripMenuItem();
            this.options_Modes = new System.Windows.Forms.ToolStripMenuItem();
            this.modes_ADXtoWAV = new System.Windows.Forms.ToolStripMenuItem();
            this.modes_WAVtoADX = new System.Windows.Forms.ToolStripMenuItem();
            this.options_Volume = new System.Windows.Forms.ToolStripMenuItem();
            this.volume_5 = new System.Windows.Forms.ToolStripMenuItem();
            this.volume_4 = new System.Windows.Forms.ToolStripMenuItem();
            this.volume_3 = new System.Windows.Forms.ToolStripMenuItem();
            this.volume_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.volume_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.volume_0 = new System.Windows.Forms.ToolStripMenuItem();
            this.options_Looping = new System.Windows.Forms.ToolStripMenuItem();
            this.looping_Ignore = new System.Windows.Forms.ToolStripMenuItem();
            this.looping_Remove = new System.Windows.Forms.ToolStripMenuItem();
            this.options_DownmixToMono = new System.Windows.Forms.ToolStripMenuItem();
            this.pnl_Backdrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).BeginInit();
            this.mstrip_Options.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Backdrop
            // 
            this.pnl_Backdrop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_Backdrop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.pnl_Backdrop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_Backdrop.BackgroundImage")));
            this.pnl_Backdrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Backdrop.Controls.Add(this.pic_Logo);
            this.pnl_Backdrop.Controls.Add(this.lbl_Title);
            this.pnl_Backdrop.Location = new System.Drawing.Point(-3, -2);
            this.pnl_Backdrop.Name = "pnl_Backdrop";
            this.pnl_Backdrop.Size = new System.Drawing.Size(440, 69);
            this.pnl_Backdrop.TabIndex = 36;
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
            this.lbl_Title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Title.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_Title.Location = new System.Drawing.Point(14, 10);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(210, 47);
            this.lbl_Title.TabIndex = 1;
            this.lbl_Title.Text = "ADX Studio";
            // 
            // btn_Convert
            // 
            this.btn_Convert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Convert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.btn_Convert.Enabled = false;
            this.btn_Convert.FlatAppearance.BorderSize = 0;
            this.btn_Convert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Convert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Convert.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Convert.Location = new System.Drawing.Point(350, 387);
            this.btn_Convert.Name = "btn_Convert";
            this.btn_Convert.Size = new System.Drawing.Size(75, 23);
            this.btn_Convert.TabIndex = 40;
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
            // clb_ADX
            // 
            this.clb_ADX.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clb_ADX.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.clb_ADX.CheckOnClick = true;
            this.clb_ADX.FormattingEnabled = true;
            this.clb_ADX.Location = new System.Drawing.Point(8, 76);
            this.clb_ADX.Name = "clb_ADX";
            this.clb_ADX.Size = new System.Drawing.Size(417, 304);
            this.clb_ADX.TabIndex = 37;
            this.clb_ADX.SelectedIndexChanged += new System.EventHandler(this.Clb_ADX_SelectedIndexChanged);
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
            this.mstrip_Options.TabIndex = 43;
            this.mstrip_Options.Text = "menuStrip1";
            // 
            // main_Options
            // 
            this.main_Options.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.options_Modes,
            this.options_Volume,
            this.options_Looping,
            this.options_DownmixToMono});
            this.main_Options.Name = "main_Options";
            this.main_Options.Size = new System.Drawing.Size(61, 20);
            this.main_Options.Text = "Options";
            // 
            // options_Modes
            // 
            this.options_Modes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modes_ADXtoWAV,
            this.modes_WAVtoADX});
            this.options_Modes.Name = "options_Modes";
            this.options_Modes.Size = new System.Drawing.Size(174, 22);
            this.options_Modes.Text = "Modes";
            // 
            // modes_ADXtoWAV
            // 
            this.modes_ADXtoWAV.CheckOnClick = true;
            this.modes_ADXtoWAV.Name = "modes_ADXtoWAV";
            this.modes_ADXtoWAV.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.modes_ADXtoWAV.Size = new System.Drawing.Size(180, 22);
            this.modes_ADXtoWAV.Text = "ADX to WAV";
            this.modes_ADXtoWAV.CheckedChanged += new System.EventHandler(this.Modes_ADXtoWAV_CheckedChanged);
            // 
            // modes_WAVtoADX
            // 
            this.modes_WAVtoADX.CheckOnClick = true;
            this.modes_WAVtoADX.Name = "modes_WAVtoADX";
            this.modes_WAVtoADX.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.W)));
            this.modes_WAVtoADX.Size = new System.Drawing.Size(180, 22);
            this.modes_WAVtoADX.Text = "WAV to ADX";
            this.modes_WAVtoADX.CheckedChanged += new System.EventHandler(this.Modes_WAVtoADX_CheckedChanged);
            // 
            // options_Volume
            // 
            this.options_Volume.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.volume_5,
            this.volume_4,
            this.volume_3,
            this.volume_2,
            this.volume_1,
            this.volume_0});
            this.options_Volume.Name = "options_Volume";
            this.options_Volume.Size = new System.Drawing.Size(174, 22);
            this.options_Volume.Text = "Volume";
            this.options_Volume.Visible = false;
            // 
            // volume_5
            // 
            this.volume_5.CheckOnClick = true;
            this.volume_5.Name = "volume_5";
            this.volume_5.Size = new System.Drawing.Size(80, 22);
            this.volume_5.Text = "5";
            this.volume_5.CheckedChanged += new System.EventHandler(this.Volume_5_CheckedChanged);
            // 
            // volume_4
            // 
            this.volume_4.CheckOnClick = true;
            this.volume_4.Name = "volume_4";
            this.volume_4.Size = new System.Drawing.Size(80, 22);
            this.volume_4.Text = "4";
            this.volume_4.CheckedChanged += new System.EventHandler(this.Volume_4_CheckedChanged);
            // 
            // volume_3
            // 
            this.volume_3.CheckOnClick = true;
            this.volume_3.Name = "volume_3";
            this.volume_3.Size = new System.Drawing.Size(80, 22);
            this.volume_3.Text = "3";
            this.volume_3.CheckedChanged += new System.EventHandler(this.Volume_3_CheckedChanged);
            // 
            // volume_2
            // 
            this.volume_2.CheckOnClick = true;
            this.volume_2.Name = "volume_2";
            this.volume_2.Size = new System.Drawing.Size(80, 22);
            this.volume_2.Text = "2";
            this.volume_2.CheckedChanged += new System.EventHandler(this.Volume_2_CheckedChanged);
            // 
            // volume_1
            // 
            this.volume_1.CheckOnClick = true;
            this.volume_1.Name = "volume_1";
            this.volume_1.Size = new System.Drawing.Size(80, 22);
            this.volume_1.Text = "1";
            this.volume_1.CheckedChanged += new System.EventHandler(this.Volume_1_CheckedChanged);
            // 
            // volume_0
            // 
            this.volume_0.CheckOnClick = true;
            this.volume_0.Name = "volume_0";
            this.volume_0.Size = new System.Drawing.Size(80, 22);
            this.volume_0.Text = "0";
            this.volume_0.CheckedChanged += new System.EventHandler(this.Volume_0_CheckedChanged);
            // 
            // options_Looping
            // 
            this.options_Looping.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.looping_Ignore,
            this.looping_Remove});
            this.options_Looping.Name = "options_Looping";
            this.options_Looping.Size = new System.Drawing.Size(174, 22);
            this.options_Looping.Text = "Looping";
            this.options_Looping.Visible = false;
            // 
            // looping_Ignore
            // 
            this.looping_Ignore.CheckOnClick = true;
            this.looping_Ignore.Name = "looping_Ignore";
            this.looping_Ignore.Size = new System.Drawing.Size(230, 22);
            this.looping_Ignore.Text = "Ignore All Loop Markers";
            this.looping_Ignore.CheckedChanged += new System.EventHandler(this.Looping_Ignore_CheckedChanged);
            // 
            // looping_Remove
            // 
            this.looping_Remove.CheckOnClick = true;
            this.looping_Remove.Name = "looping_Remove";
            this.looping_Remove.Size = new System.Drawing.Size(230, 22);
            this.looping_Remove.Text = "Remove End of Loop Position";
            this.looping_Remove.CheckedChanged += new System.EventHandler(this.Looping_Remove_CheckedChanged);
            // 
            // options_DownmixToMono
            // 
            this.options_DownmixToMono.CheckOnClick = true;
            this.options_DownmixToMono.Name = "options_DownmixToMono";
            this.options_DownmixToMono.Size = new System.Drawing.Size(174, 22);
            this.options_DownmixToMono.Text = "Downmix to Mono";
            this.options_DownmixToMono.Visible = false;
            this.options_DownmixToMono.CheckedChanged += new System.EventHandler(this.Options_DownmixToStereo_CheckedChanged);
            // 
            // ADX_Studio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(434, 419);
            this.Controls.Add(this.pnl_Backdrop);
            this.Controls.Add(this.btn_Convert);
            this.Controls.Add(this.btn_DeselectAll);
            this.Controls.Add(this.btn_SelectAll);
            this.Controls.Add(this.mstrip_Options);
            this.Controls.Add(this.clb_ADX);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(450, 458);
            this.Name = "ADX_Studio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ADX Studio";
            this.Load += new System.EventHandler(this.ADX_Studio_Load);
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
        private System.Windows.Forms.CheckedListBox clb_ADX;
        private System.Windows.Forms.MenuStrip mstrip_Options;
        private System.Windows.Forms.ToolStripMenuItem main_Options;
        private System.Windows.Forms.ToolStripMenuItem options_Modes;
        private System.Windows.Forms.ToolStripMenuItem modes_ADXtoWAV;
        private System.Windows.Forms.ToolStripMenuItem modes_WAVtoADX;
        private System.Windows.Forms.ToolStripMenuItem options_Volume;
        private System.Windows.Forms.ToolStripMenuItem volume_5;
        private System.Windows.Forms.ToolStripMenuItem volume_4;
        private System.Windows.Forms.ToolStripMenuItem volume_3;
        private System.Windows.Forms.ToolStripMenuItem volume_2;
        private System.Windows.Forms.ToolStripMenuItem volume_1;
        private System.Windows.Forms.ToolStripMenuItem volume_0;
        private System.Windows.Forms.ToolStripMenuItem options_Looping;
        private System.Windows.Forms.ToolStripMenuItem looping_Ignore;
        private System.Windows.Forms.ToolStripMenuItem looping_Remove;
        private System.Windows.Forms.ToolStripMenuItem options_DownmixToMono;
    }
}