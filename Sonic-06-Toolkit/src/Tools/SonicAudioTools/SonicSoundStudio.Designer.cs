namespace Toolkit.Tools
{
    partial class SonicSoundStudio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SonicSoundStudio));
            this.lbl_Title = new System.Windows.Forms.Label();
            this.pnl_Backdrop = new System.Windows.Forms.Panel();
            this.pic_Logo = new System.Windows.Forms.PictureBox();
            this.btn_Process = new System.Windows.Forms.Button();
            this.btn_DeselectAll = new System.Windows.Forms.Button();
            this.btn_SelectAll = new System.Windows.Forms.Button();
            this.clb_SNDs = new System.Windows.Forms.CheckedListBox();
            this.group_Options = new System.Windows.Forms.GroupBox();
            this.pnl_CheapCoverUp2 = new System.Windows.Forms.Panel();
            this.pnl_CheapCoverUp1 = new System.Windows.Forms.Panel();
            this.axWMP_Player = new AxWMPLib.AxWindowsMediaPlayer();
            this.lbl_NowPlaying = new System.Windows.Forms.Label();
            this.btn_MediaControl = new System.Windows.Forms.Button();
            this.check_PatchXMA = new System.Windows.Forms.CheckBox();
            this.lbl_Encoder = new System.Windows.Forms.Label();
            this.combo_Encoder = new System.Windows.Forms.ComboBox();
            this.tracker_MediaBar = new System.Windows.Forms.TrackBar();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tm_NoCheckOnClickTimer = new System.Windows.Forms.Timer(this.components);
            this.tm_MediaPlayer = new System.Windows.Forms.Timer(this.components);
            this.pnl_Backdrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).BeginInit();
            this.group_Options.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWMP_Player)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tracker_MediaBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Title.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_Title.Location = new System.Drawing.Point(14, 10);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(341, 47);
            this.lbl_Title.TabIndex = 1;
            this.lbl_Title.Text = "Sonic Sound Studio";
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
            this.pnl_Backdrop.TabIndex = 51;
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
            // btn_Process
            // 
            this.btn_Process.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Process.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btn_Process.Enabled = false;
            this.btn_Process.FlatAppearance.BorderSize = 0;
            this.btn_Process.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Process.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Process.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Process.Location = new System.Drawing.Point(2, 316);
            this.btn_Process.Name = "btn_Process";
            this.btn_Process.Size = new System.Drawing.Size(377, 23);
            this.btn_Process.TabIndex = 60;
            this.btn_Process.Text = "Encode";
            this.btn_Process.UseVisualStyleBackColor = false;
            this.btn_Process.Click += new System.EventHandler(this.Btn_Process_Click);
            // 
            // btn_DeselectAll
            // 
            this.btn_DeselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_DeselectAll.BackColor = System.Drawing.Color.Tomato;
            this.btn_DeselectAll.FlatAppearance.BorderSize = 0;
            this.btn_DeselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DeselectAll.Location = new System.Drawing.Point(207, 316);
            this.btn_DeselectAll.Name = "btn_DeselectAll";
            this.btn_DeselectAll.Size = new System.Drawing.Size(199, 23);
            this.btn_DeselectAll.TabIndex = 59;
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
            this.btn_SelectAll.Location = new System.Drawing.Point(1, 316);
            this.btn_SelectAll.Name = "btn_SelectAll";
            this.btn_SelectAll.Size = new System.Drawing.Size(199, 23);
            this.btn_SelectAll.TabIndex = 58;
            this.btn_SelectAll.Text = "Select All";
            this.btn_SelectAll.UseVisualStyleBackColor = false;
            this.btn_SelectAll.Click += new System.EventHandler(this.Btn_SelectAll_Click);
            // 
            // clb_SNDs
            // 
            this.clb_SNDs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clb_SNDs.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.clb_SNDs.FormattingEnabled = true;
            this.clb_SNDs.Location = new System.Drawing.Point(0, 4);
            this.clb_SNDs.Name = "clb_SNDs";
            this.clb_SNDs.Size = new System.Drawing.Size(406, 304);
            this.clb_SNDs.TabIndex = 57;
            this.clb_SNDs.SelectedIndexChanged += new System.EventHandler(this.Clb_SNDs_SelectedIndexChanged);
            // 
            // group_Options
            // 
            this.group_Options.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.group_Options.Controls.Add(this.pnl_CheapCoverUp2);
            this.group_Options.Controls.Add(this.pnl_CheapCoverUp1);
            this.group_Options.Controls.Add(this.axWMP_Player);
            this.group_Options.Controls.Add(this.lbl_NowPlaying);
            this.group_Options.Controls.Add(this.btn_MediaControl);
            this.group_Options.Controls.Add(this.check_PatchXMA);
            this.group_Options.Controls.Add(this.lbl_Encoder);
            this.group_Options.Controls.Add(this.combo_Encoder);
            this.group_Options.Controls.Add(this.tracker_MediaBar);
            this.group_Options.Location = new System.Drawing.Point(2, -2);
            this.group_Options.Name = "group_Options";
            this.group_Options.Size = new System.Drawing.Size(377, 311);
            this.group_Options.TabIndex = 61;
            this.group_Options.TabStop = false;
            this.group_Options.Text = "Options";
            // 
            // pnl_CheapCoverUp2
            // 
            this.pnl_CheapCoverUp2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_CheapCoverUp2.Location = new System.Drawing.Point(20, 287);
            this.pnl_CheapCoverUp2.Name = "pnl_CheapCoverUp2";
            this.pnl_CheapCoverUp2.Size = new System.Drawing.Size(306, 10);
            this.pnl_CheapCoverUp2.TabIndex = 70;
            // 
            // pnl_CheapCoverUp1
            // 
            this.pnl_CheapCoverUp1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_CheapCoverUp1.Location = new System.Drawing.Point(20, 254);
            this.pnl_CheapCoverUp1.Name = "pnl_CheapCoverUp1";
            this.pnl_CheapCoverUp1.Size = new System.Drawing.Size(306, 10);
            this.pnl_CheapCoverUp1.TabIndex = 68;
            // 
            // axWMP_Player
            // 
            this.axWMP_Player.Enabled = true;
            this.axWMP_Player.Location = new System.Drawing.Point(20, 186);
            this.axWMP_Player.Name = "axWMP_Player";
            this.axWMP_Player.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWMP_Player.OcxState")));
            this.axWMP_Player.Size = new System.Drawing.Size(307, 45);
            this.axWMP_Player.TabIndex = 67;
            this.axWMP_Player.Visible = false;
            this.axWMP_Player.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.AxWMP_Player_PlayStateChange);
            // 
            // lbl_NowPlaying
            // 
            this.lbl_NowPlaying.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_NowPlaying.AutoSize = true;
            this.lbl_NowPlaying.Location = new System.Drawing.Point(17, 239);
            this.lbl_NowPlaying.Name = "lbl_NowPlaying";
            this.lbl_NowPlaying.Size = new System.Drawing.Size(101, 13);
            this.lbl_NowPlaying.TabIndex = 66;
            this.lbl_NowPlaying.Text = "Now Playing: None.";
            // 
            // btn_MediaControl
            // 
            this.btn_MediaControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_MediaControl.BackColor = System.Drawing.Color.LightGreen;
            this.btn_MediaControl.Enabled = false;
            this.btn_MediaControl.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btn_MediaControl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_MediaControl.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_MediaControl.Location = new System.Drawing.Point(332, 263);
            this.btn_MediaControl.Name = "btn_MediaControl";
            this.btn_MediaControl.Size = new System.Drawing.Size(27, 26);
            this.btn_MediaControl.TabIndex = 65;
            this.btn_MediaControl.Text = "►";
            this.btn_MediaControl.UseVisualStyleBackColor = false;
            this.btn_MediaControl.Click += new System.EventHandler(this.Btn_MediaControl_Click);
            // 
            // check_PatchXMA
            // 
            this.check_PatchXMA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.check_PatchXMA.AutoSize = true;
            this.check_PatchXMA.Enabled = false;
            this.check_PatchXMA.Location = new System.Drawing.Point(226, 46);
            this.check_PatchXMA.Name = "check_PatchXMA";
            this.check_PatchXMA.Size = new System.Drawing.Size(143, 17);
            this.check_PatchXMA.TabIndex = 64;
            this.check_PatchXMA.Text = "Patch Xbox Media Audio";
            this.check_PatchXMA.UseVisualStyleBackColor = true;
            this.check_PatchXMA.CheckedChanged += new System.EventHandler(this.Check_PatchXMA_CheckedChanged);
            // 
            // lbl_Encoder
            // 
            this.lbl_Encoder.AutoSize = true;
            this.lbl_Encoder.Location = new System.Drawing.Point(17, 27);
            this.lbl_Encoder.Name = "lbl_Encoder";
            this.lbl_Encoder.Size = new System.Drawing.Size(38, 13);
            this.lbl_Encoder.TabIndex = 63;
            this.lbl_Encoder.Text = "Codec";
            // 
            // combo_Encoder
            // 
            this.combo_Encoder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.combo_Encoder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_Encoder.FormattingEnabled = true;
            this.combo_Encoder.Items.AddRange(new object[] {
            "ADX (sound editing)",
            "AT3 (PlayStation 3)",
            "CSB (sound importing)",
            "WAV (audio ripping)",
            "XMA (Xbox 360)"});
            this.combo_Encoder.Location = new System.Drawing.Point(19, 43);
            this.combo_Encoder.Name = "combo_Encoder";
            this.combo_Encoder.Size = new System.Drawing.Size(194, 21);
            this.combo_Encoder.TabIndex = 62;
            this.combo_Encoder.SelectedIndexChanged += new System.EventHandler(this.Combo_Encoder_SelectedIndexChanged);
            // 
            // tracker_MediaBar
            // 
            this.tracker_MediaBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tracker_MediaBar.Enabled = false;
            this.tracker_MediaBar.Location = new System.Drawing.Point(13, 255);
            this.tracker_MediaBar.Maximum = 100;
            this.tracker_MediaBar.Name = "tracker_MediaBar";
            this.tracker_MediaBar.Size = new System.Drawing.Size(314, 45);
            this.tracker_MediaBar.TabIndex = 63;
            this.tracker_MediaBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tracker_MediaBar.Scroll += new System.EventHandler(this.Tracker_MediaBar_Scroll);
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
            this.splitContainer1.Panel1.Controls.Add(this.clb_SNDs);
            this.splitContainer1.Panel1.Controls.Add(this.btn_SelectAll);
            this.splitContainer1.Panel1.Controls.Add(this.btn_DeselectAll);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btn_Process);
            this.splitContainer1.Panel2.Controls.Add(this.group_Options);
            this.splitContainer1.Size = new System.Drawing.Size(791, 341);
            this.splitContainer1.SplitterDistance = 408;
            this.splitContainer1.TabIndex = 62;
            // 
            // tm_NoCheckOnClickTimer
            // 
            this.tm_NoCheckOnClickTimer.Interval = 10;
            this.tm_NoCheckOnClickTimer.Tick += new System.EventHandler(this.Tm_NoCheckOnClickTimer_Tick);
            // 
            // tm_MediaPlayer
            // 
            this.tm_MediaPlayer.Interval = 1000;
            this.tm_MediaPlayer.Tick += new System.EventHandler(this.Tm_MediaPlayer_Tick);
            // 
            // SonicSoundStudio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 419);
            this.Controls.Add(this.pnl_Backdrop);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(821, 458);
            this.Name = "SonicSoundStudio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sonic Sound Studio";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SonicSoundStudio_FormClosing);
            this.Load += new System.EventHandler(this.SonicSoundStudio_Load);
            this.pnl_Backdrop.ResumeLayout(false);
            this.pnl_Backdrop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).EndInit();
            this.group_Options.ResumeLayout(false);
            this.group_Options.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWMP_Player)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tracker_MediaBar)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Panel pnl_Backdrop;
        internal System.Windows.Forms.PictureBox pic_Logo;
        private System.Windows.Forms.Button btn_Process;
        private System.Windows.Forms.Button btn_DeselectAll;
        private System.Windows.Forms.Button btn_SelectAll;
        private System.Windows.Forms.CheckedListBox clb_SNDs;
        private System.Windows.Forms.GroupBox group_Options;
        private System.Windows.Forms.CheckBox check_PatchXMA;
        private System.Windows.Forms.Label lbl_Encoder;
        private System.Windows.Forms.ComboBox combo_Encoder;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TrackBar tracker_MediaBar;
        private System.Windows.Forms.Button btn_MediaControl;
        private System.Windows.Forms.Label lbl_NowPlaying;
        private System.Windows.Forms.Timer tm_NoCheckOnClickTimer;
        private AxWMPLib.AxWindowsMediaPlayer axWMP_Player;
        private System.Windows.Forms.Panel pnl_CheapCoverUp2;
        private System.Windows.Forms.Panel pnl_CheapCoverUp1;
        private System.Windows.Forms.Timer tm_MediaPlayer;
    }
}