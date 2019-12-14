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
            this.group_Encoding = new System.Windows.Forms.GroupBox();
            this.check_PatchXMA = new System.Windows.Forms.CheckBox();
            this.lbl_Encoder = new System.Windows.Forms.Label();
            this.combo_Encoder = new System.Windows.Forms.ComboBox();
            this.pic_Volume = new System.Windows.Forms.PictureBox();
            this.pnl_CheapCoverUp4 = new System.Windows.Forms.Panel();
            this.pnl_CheapCoverUp3 = new System.Windows.Forms.Panel();
            this.pnl_CheapCoverUp2 = new System.Windows.Forms.Panel();
            this.pnl_CheapCoverUp1 = new System.Windows.Forms.Panel();
            this.axWMP_Player = new AxWMPLib.AxWindowsMediaPlayer();
            this.btn_MediaControl = new System.Windows.Forms.Button();
            this.tracker_MediaBar = new System.Windows.Forms.TrackBar();
            this.tracker_Volume = new System.Windows.Forms.TrackBar();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.group_Loop = new System.Windows.Forms.GroupBox();
            this.check_Loop = new System.Windows.Forms.CheckBox();
            this.pnl_LoopOptions = new System.Windows.Forms.Panel();
            this.lbl_Start = new System.Windows.Forms.Label();
            this.nud_End = new System.Windows.Forms.NumericUpDown();
            this.lbl_End = new System.Windows.Forms.Label();
            this.nud_Start = new System.Windows.Forms.NumericUpDown();
            this.tm_NoCheckOnClickTimer = new System.Windows.Forms.Timer(this.components);
            this.tm_MediaPlayer = new System.Windows.Forms.Timer(this.components);
            this.pnl_Backdrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).BeginInit();
            this.group_Encoding.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Volume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axWMP_Player)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tracker_MediaBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tracker_Volume)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.group_Loop.SuspendLayout();
            this.pnl_LoopOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_End)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Start)).BeginInit();
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
            this.btn_Process.Location = new System.Drawing.Point(2, 300);
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
            this.btn_DeselectAll.Location = new System.Drawing.Point(207, 300);
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
            this.btn_SelectAll.Location = new System.Drawing.Point(1, 300);
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
            this.clb_SNDs.Size = new System.Drawing.Size(406, 289);
            this.clb_SNDs.TabIndex = 57;
            this.clb_SNDs.SelectedIndexChanged += new System.EventHandler(this.Clb_SNDs_SelectedIndexChanged);
            // 
            // group_Encoding
            // 
            this.group_Encoding.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.group_Encoding.Controls.Add(this.check_PatchXMA);
            this.group_Encoding.Controls.Add(this.lbl_Encoder);
            this.group_Encoding.Controls.Add(this.combo_Encoder);
            this.group_Encoding.Location = new System.Drawing.Point(2, -2);
            this.group_Encoding.Name = "group_Encoding";
            this.group_Encoding.Size = new System.Drawing.Size(330, 76);
            this.group_Encoding.TabIndex = 61;
            this.group_Encoding.TabStop = false;
            this.group_Encoding.Text = "Encoder";
            // 
            // check_PatchXMA
            // 
            this.check_PatchXMA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.check_PatchXMA.AutoSize = true;
            this.check_PatchXMA.Enabled = false;
            this.check_PatchXMA.Location = new System.Drawing.Point(182, 41);
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
            this.lbl_Encoder.Location = new System.Drawing.Point(11, 22);
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
            this.combo_Encoder.Location = new System.Drawing.Point(13, 38);
            this.combo_Encoder.Name = "combo_Encoder";
            this.combo_Encoder.Size = new System.Drawing.Size(157, 21);
            this.combo_Encoder.TabIndex = 62;
            this.combo_Encoder.SelectedIndexChanged += new System.EventHandler(this.Combo_Encoder_SelectedIndexChanged);
            // 
            // pic_Volume
            // 
            this.pic_Volume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_Volume.BackgroundImage = global::Toolkit.Properties.Resources.audio_volume_mute;
            this.pic_Volume.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pic_Volume.Location = new System.Drawing.Point(338, 3);
            this.pic_Volume.Name = "pic_Volume";
            this.pic_Volume.Size = new System.Drawing.Size(43, 30);
            this.pic_Volume.TabIndex = 72;
            this.pic_Volume.TabStop = false;
            // 
            // pnl_CheapCoverUp4
            // 
            this.pnl_CheapCoverUp4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_CheapCoverUp4.Location = new System.Drawing.Point(370, 37);
            this.pnl_CheapCoverUp4.Name = "pnl_CheapCoverUp4";
            this.pnl_CheapCoverUp4.Size = new System.Drawing.Size(10, 232);
            this.pnl_CheapCoverUp4.TabIndex = 70;
            // 
            // pnl_CheapCoverUp3
            // 
            this.pnl_CheapCoverUp3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_CheapCoverUp3.Location = new System.Drawing.Point(337, 37);
            this.pnl_CheapCoverUp3.Name = "pnl_CheapCoverUp3";
            this.pnl_CheapCoverUp3.Size = new System.Drawing.Size(10, 232);
            this.pnl_CheapCoverUp3.TabIndex = 69;
            // 
            // pnl_CheapCoverUp2
            // 
            this.pnl_CheapCoverUp2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_CheapCoverUp2.Location = new System.Drawing.Point(3, 292);
            this.pnl_CheapCoverUp2.Name = "pnl_CheapCoverUp2";
            this.pnl_CheapCoverUp2.Size = new System.Drawing.Size(334, 10);
            this.pnl_CheapCoverUp2.TabIndex = 70;
            // 
            // pnl_CheapCoverUp1
            // 
            this.pnl_CheapCoverUp1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_CheapCoverUp1.Location = new System.Drawing.Point(3, 259);
            this.pnl_CheapCoverUp1.Name = "pnl_CheapCoverUp1";
            this.pnl_CheapCoverUp1.Size = new System.Drawing.Size(334, 10);
            this.pnl_CheapCoverUp1.TabIndex = 68;
            // 
            // axWMP_Player
            // 
            this.axWMP_Player.Enabled = true;
            this.axWMP_Player.Location = new System.Drawing.Point(0, 0);
            this.axWMP_Player.Name = "axWMP_Player";
            this.axWMP_Player.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWMP_Player.OcxState")));
            this.axWMP_Player.Size = new System.Drawing.Size(39, 45);
            this.axWMP_Player.TabIndex = 67;
            this.axWMP_Player.Visible = false;
            this.axWMP_Player.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.AxWMP_Player_PlayStateChange);
            // 
            // btn_MediaControl
            // 
            this.btn_MediaControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_MediaControl.BackColor = System.Drawing.Color.LightGreen;
            this.btn_MediaControl.Enabled = false;
            this.btn_MediaControl.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btn_MediaControl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_MediaControl.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_MediaControl.Location = new System.Drawing.Point(343, 268);
            this.btn_MediaControl.Name = "btn_MediaControl";
            this.btn_MediaControl.Size = new System.Drawing.Size(33, 26);
            this.btn_MediaControl.TabIndex = 65;
            this.btn_MediaControl.Text = "►";
            this.btn_MediaControl.UseVisualStyleBackColor = false;
            this.btn_MediaControl.Click += new System.EventHandler(this.Btn_MediaControl_Click);
            // 
            // tracker_MediaBar
            // 
            this.tracker_MediaBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tracker_MediaBar.Enabled = false;
            this.tracker_MediaBar.Location = new System.Drawing.Point(-2, 260);
            this.tracker_MediaBar.Maximum = 100;
            this.tracker_MediaBar.Name = "tracker_MediaBar";
            this.tracker_MediaBar.Size = new System.Drawing.Size(345, 45);
            this.tracker_MediaBar.TabIndex = 63;
            this.tracker_MediaBar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tracker_MediaBar.Scroll += new System.EventHandler(this.Tracker_MediaBar_Scroll);
            // 
            // tracker_Volume
            // 
            this.tracker_Volume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tracker_Volume.Location = new System.Drawing.Point(338, 31);
            this.tracker_Volume.Maximum = 100;
            this.tracker_Volume.Name = "tracker_Volume";
            this.tracker_Volume.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tracker_Volume.Size = new System.Drawing.Size(45, 238);
            this.tracker_Volume.TabIndex = 71;
            this.tracker_Volume.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.tracker_Volume.Value = 100;
            this.tracker_Volume.Scroll += new System.EventHandler(this.Tracker_Volume_Scroll);
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
            this.splitContainer1.Panel2.Controls.Add(this.btn_MediaControl);
            this.splitContainer1.Panel2.Controls.Add(this.group_Loop);
            this.splitContainer1.Panel2.Controls.Add(this.pic_Volume);
            this.splitContainer1.Panel2.Controls.Add(this.btn_Process);
            this.splitContainer1.Panel2.Controls.Add(this.pnl_CheapCoverUp4);
            this.splitContainer1.Panel2.Controls.Add(this.group_Encoding);
            this.splitContainer1.Panel2.Controls.Add(this.pnl_CheapCoverUp3);
            this.splitContainer1.Panel2.Controls.Add(this.tracker_Volume);
            this.splitContainer1.Panel2.Controls.Add(this.pnl_CheapCoverUp2);
            this.splitContainer1.Panel2.Controls.Add(this.pnl_CheapCoverUp1);
            this.splitContainer1.Panel2.Controls.Add(this.tracker_MediaBar);
            this.splitContainer1.Size = new System.Drawing.Size(791, 325);
            this.splitContainer1.SplitterDistance = 408;
            this.splitContainer1.TabIndex = 62;
            // 
            // group_Loop
            // 
            this.group_Loop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.group_Loop.Controls.Add(this.check_Loop);
            this.group_Loop.Controls.Add(this.pnl_LoopOptions);
            this.group_Loop.Location = new System.Drawing.Point(2, 77);
            this.group_Loop.Name = "group_Loop";
            this.group_Loop.Size = new System.Drawing.Size(330, 144);
            this.group_Loop.TabIndex = 65;
            this.group_Loop.TabStop = false;
            this.group_Loop.Text = "Loop";
            // 
            // check_Loop
            // 
            this.check_Loop.AutoSize = true;
            this.check_Loop.Enabled = false;
            this.check_Loop.Location = new System.Drawing.Point(14, 23);
            this.check_Loop.Name = "check_Loop";
            this.check_Loop.Size = new System.Drawing.Size(80, 17);
            this.check_Loop.TabIndex = 82;
            this.check_Loop.Text = "Loop Audio";
            this.check_Loop.UseVisualStyleBackColor = true;
            this.check_Loop.CheckedChanged += new System.EventHandler(this.Check_Loop_CheckedChanged);
            // 
            // pnl_LoopOptions
            // 
            this.pnl_LoopOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_LoopOptions.Controls.Add(this.lbl_Start);
            this.pnl_LoopOptions.Controls.Add(this.nud_End);
            this.pnl_LoopOptions.Controls.Add(this.lbl_End);
            this.pnl_LoopOptions.Controls.Add(this.nud_Start);
            this.pnl_LoopOptions.Enabled = false;
            this.pnl_LoopOptions.Location = new System.Drawing.Point(7, 47);
            this.pnl_LoopOptions.Name = "pnl_LoopOptions";
            this.pnl_LoopOptions.Size = new System.Drawing.Size(319, 92);
            this.pnl_LoopOptions.TabIndex = 81;
            // 
            // lbl_Start
            // 
            this.lbl_Start.AutoSize = true;
            this.lbl_Start.Location = new System.Drawing.Point(4, 2);
            this.lbl_Start.Name = "lbl_Start";
            this.lbl_Start.Size = new System.Drawing.Size(67, 13);
            this.lbl_Start.TabIndex = 74;
            this.lbl_Start.Text = "Start Sample";
            // 
            // nud_End
            // 
            this.nud_End.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nud_End.Location = new System.Drawing.Point(6, 61);
            this.nud_End.Maximum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
            this.nud_End.Name = "nud_End";
            this.nud_End.Size = new System.Drawing.Size(305, 20);
            this.nud_End.TabIndex = 78;
            // 
            // lbl_End
            // 
            this.lbl_End.AutoSize = true;
            this.lbl_End.Location = new System.Drawing.Point(4, 45);
            this.lbl_End.Name = "lbl_End";
            this.lbl_End.Size = new System.Drawing.Size(64, 13);
            this.lbl_End.TabIndex = 76;
            this.lbl_End.Text = "End Sample";
            // 
            // nud_Start
            // 
            this.nud_Start.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nud_Start.Location = new System.Drawing.Point(6, 18);
            this.nud_Start.Maximum = new decimal(new int[] {
            1316134911,
            2328,
            0,
            0});
            this.nud_Start.Name = "nud_Start";
            this.nud_Start.Size = new System.Drawing.Size(305, 20);
            this.nud_Start.TabIndex = 77;
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
            this.ClientSize = new System.Drawing.Size(805, 403);
            this.Controls.Add(this.axWMP_Player);
            this.Controls.Add(this.pnl_Backdrop);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(821, 442);
            this.Name = "SonicSoundStudio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sonic Sound Studio";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SonicSoundStudio_FormClosing);
            this.Load += new System.EventHandler(this.SonicSoundStudio_Load);
            this.pnl_Backdrop.ResumeLayout(false);
            this.pnl_Backdrop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).EndInit();
            this.group_Encoding.ResumeLayout(false);
            this.group_Encoding.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Volume)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axWMP_Player)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tracker_MediaBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tracker_Volume)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.group_Loop.ResumeLayout(false);
            this.group_Loop.PerformLayout();
            this.pnl_LoopOptions.ResumeLayout(false);
            this.pnl_LoopOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_End)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_Start)).EndInit();
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
        private System.Windows.Forms.GroupBox group_Encoding;
        private System.Windows.Forms.CheckBox check_PatchXMA;
        private System.Windows.Forms.Label lbl_Encoder;
        private System.Windows.Forms.ComboBox combo_Encoder;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TrackBar tracker_MediaBar;
        private System.Windows.Forms.Button btn_MediaControl;
        private System.Windows.Forms.Timer tm_NoCheckOnClickTimer;
        private AxWMPLib.AxWindowsMediaPlayer axWMP_Player;
        private System.Windows.Forms.Panel pnl_CheapCoverUp2;
        private System.Windows.Forms.Panel pnl_CheapCoverUp1;
        private System.Windows.Forms.Timer tm_MediaPlayer;
        private System.Windows.Forms.Panel pnl_CheapCoverUp4;
        private System.Windows.Forms.Panel pnl_CheapCoverUp3;
        private System.Windows.Forms.TrackBar tracker_Volume;
        private System.Windows.Forms.PictureBox pic_Volume;
        private System.Windows.Forms.Label lbl_End;
        private System.Windows.Forms.Label lbl_Start;
        private System.Windows.Forms.NumericUpDown nud_Start;
        private System.Windows.Forms.NumericUpDown nud_End;
        private System.Windows.Forms.Panel pnl_LoopOptions;
        private System.Windows.Forms.CheckBox check_Loop;
        private System.Windows.Forms.GroupBox group_Loop;
    }
}