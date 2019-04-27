namespace Sonic_06_Toolkit
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.mstrip_Main = new System.Windows.Forms.MenuStrip();
            this.menu_File = new System.Windows.Forms.ToolStripMenuItem();
            this.file_OpenARC = new System.Windows.Forms.ToolStripMenuItem();
            this.file_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.file_RepackARC = new System.Windows.Forms.ToolStripMenuItem();
            this.file_RepackARCAs = new System.Windows.Forms.ToolStripMenuItem();
            this.file_Separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.file_Preferences = new System.Windows.Forms.ToolStripMenuItem();
            this.preferences_Themes = new System.Windows.Forms.ToolStripMenuItem();
            this.themes_Compact = new System.Windows.Forms.ToolStripMenuItem();
            this.themes_Original = new System.Windows.Forms.ToolStripMenuItem();
            this.preferences_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.preferences_ShowSessionID = new System.Windows.Forms.ToolStripMenuItem();
            this.file_CloseARC = new System.Windows.Forms.ToolStripMenuItem();
            this.file_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_SDK = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_LUBStudio = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_DecompileLUBs = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.sdk_XNOStudio = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_ConvertXNOs = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_Separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mSTStudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Tabs = new System.Windows.Forms.ToolStripMenuItem();
            this.tabs_NewTab = new System.Windows.Forms.ToolStripMenuItem();
            this.tabs_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tabs_CloseTab = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.help_Documentation = new System.Windows.Forms.ToolStripMenuItem();
            this.help_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.help_About = new System.Windows.Forms.ToolStripMenuItem();
            this.ofd_OpenARC = new System.Windows.Forms.OpenFileDialog();
            this.btn_SessionID = new System.Windows.Forms.Button();
            this.pnl_Backdrop = new System.Windows.Forms.Panel();
            this.btn_NewTab = new System.Windows.Forms.Button();
            this.btn_Forward = new System.Windows.Forms.Button();
            this.btn_Back = new System.Windows.Forms.Button();
            this.btn_Repack = new System.Windows.Forms.Button();
            this.tab_Main = new System.Windows.Forms.TabControl();
            this.tm_tabCheck = new System.Windows.Forms.Timer(this.components);
            this.btn_OpenFolder = new System.Windows.Forms.Button();
            this.sfd_RepackARCAs = new System.Windows.Forms.SaveFileDialog();
            this.mstrip_Main.SuspendLayout();
            this.pnl_Backdrop.SuspendLayout();
            this.SuspendLayout();
            // 
            // mstrip_Main
            // 
            this.mstrip_Main.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.mstrip_Main.Dock = System.Windows.Forms.DockStyle.None;
            this.mstrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_File,
            this.menu_SDK,
            this.menu_Tabs,
            this.menu_Help});
            this.mstrip_Main.Location = new System.Drawing.Point(104, 0);
            this.mstrip_Main.Name = "mstrip_Main";
            this.mstrip_Main.Size = new System.Drawing.Size(172, 24);
            this.mstrip_Main.TabIndex = 0;
            this.mstrip_Main.Text = "menuStrip1";
            // 
            // menu_File
            // 
            this.menu_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.file_OpenARC,
            this.file_Separator1,
            this.file_RepackARC,
            this.file_RepackARCAs,
            this.file_Separator2,
            this.file_Preferences,
            this.file_CloseARC,
            this.file_Exit});
            this.menu_File.Name = "menu_File";
            this.menu_File.Size = new System.Drawing.Size(37, 20);
            this.menu_File.Text = "File";
            // 
            // file_OpenARC
            // 
            this.file_OpenARC.Image = ((System.Drawing.Image)(resources.GetObject("file_OpenARC.Image")));
            this.file_OpenARC.Name = "file_OpenARC";
            this.file_OpenARC.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.file_OpenARC.Size = new System.Drawing.Size(226, 22);
            this.file_OpenARC.Text = "Open ARC";
            this.file_OpenARC.Click += new System.EventHandler(this.File_OpenARC_Click);
            // 
            // file_Separator1
            // 
            this.file_Separator1.Name = "file_Separator1";
            this.file_Separator1.Size = new System.Drawing.Size(223, 6);
            // 
            // file_RepackARC
            // 
            this.file_RepackARC.Enabled = false;
            this.file_RepackARC.Image = ((System.Drawing.Image)(resources.GetObject("file_RepackARC.Image")));
            this.file_RepackARC.Name = "file_RepackARC";
            this.file_RepackARC.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.file_RepackARC.Size = new System.Drawing.Size(226, 22);
            this.file_RepackARC.Text = "Repack ARC";
            this.file_RepackARC.Click += new System.EventHandler(this.File_RepackARC_Click);
            // 
            // file_RepackARCAs
            // 
            this.file_RepackARCAs.Enabled = false;
            this.file_RepackARCAs.Image = ((System.Drawing.Image)(resources.GetObject("file_RepackARCAs.Image")));
            this.file_RepackARCAs.Name = "file_RepackARCAs";
            this.file_RepackARCAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.file_RepackARCAs.Size = new System.Drawing.Size(226, 22);
            this.file_RepackARCAs.Text = "Repack ARC As...";
            this.file_RepackARCAs.Click += new System.EventHandler(this.File_RepackARCAs_Click);
            // 
            // file_Separator2
            // 
            this.file_Separator2.Name = "file_Separator2";
            this.file_Separator2.Size = new System.Drawing.Size(223, 6);
            // 
            // file_Preferences
            // 
            this.file_Preferences.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferences_Themes,
            this.preferences_Separator1,
            this.preferences_ShowSessionID});
            this.file_Preferences.Image = ((System.Drawing.Image)(resources.GetObject("file_Preferences.Image")));
            this.file_Preferences.Name = "file_Preferences";
            this.file_Preferences.Size = new System.Drawing.Size(226, 22);
            this.file_Preferences.Text = "Preferences";
            // 
            // preferences_Themes
            // 
            this.preferences_Themes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.themes_Compact,
            this.themes_Original});
            this.preferences_Themes.Name = "preferences_Themes";
            this.preferences_Themes.Size = new System.Drawing.Size(159, 22);
            this.preferences_Themes.Text = "Themes";
            // 
            // themes_Compact
            // 
            this.themes_Compact.Checked = true;
            this.themes_Compact.CheckOnClick = true;
            this.themes_Compact.CheckState = System.Windows.Forms.CheckState.Checked;
            this.themes_Compact.Name = "themes_Compact";
            this.themes_Compact.Size = new System.Drawing.Size(123, 22);
            this.themes_Compact.Text = "Compact";
            this.themes_Compact.CheckedChanged += new System.EventHandler(this.Themes_Compact_CheckedChanged);
            // 
            // themes_Original
            // 
            this.themes_Original.CheckOnClick = true;
            this.themes_Original.Name = "themes_Original";
            this.themes_Original.Size = new System.Drawing.Size(123, 22);
            this.themes_Original.Text = "Original";
            this.themes_Original.CheckedChanged += new System.EventHandler(this.Themes_Original_CheckedChanged);
            // 
            // preferences_Separator1
            // 
            this.preferences_Separator1.Name = "preferences_Separator1";
            this.preferences_Separator1.Size = new System.Drawing.Size(156, 6);
            // 
            // preferences_ShowSessionID
            // 
            this.preferences_ShowSessionID.Checked = true;
            this.preferences_ShowSessionID.CheckOnClick = true;
            this.preferences_ShowSessionID.CheckState = System.Windows.Forms.CheckState.Checked;
            this.preferences_ShowSessionID.Name = "preferences_ShowSessionID";
            this.preferences_ShowSessionID.Size = new System.Drawing.Size(159, 22);
            this.preferences_ShowSessionID.Text = "Show Session ID";
            this.preferences_ShowSessionID.CheckedChanged += new System.EventHandler(this.Preferences_ShowSessionID_CheckedChanged);
            // 
            // file_CloseARC
            // 
            this.file_CloseARC.Enabled = false;
            this.file_CloseARC.Image = ((System.Drawing.Image)(resources.GetObject("file_CloseARC.Image")));
            this.file_CloseARC.Name = "file_CloseARC";
            this.file_CloseARC.Size = new System.Drawing.Size(226, 22);
            this.file_CloseARC.Text = "Close ARC";
            this.file_CloseARC.Click += new System.EventHandler(this.File_CloseARC_Click);
            // 
            // file_Exit
            // 
            this.file_Exit.Image = ((System.Drawing.Image)(resources.GetObject("file_Exit.Image")));
            this.file_Exit.Name = "file_Exit";
            this.file_Exit.Size = new System.Drawing.Size(226, 22);
            this.file_Exit.Text = "Exit";
            this.file_Exit.Click += new System.EventHandler(this.File_Exit_Click);
            // 
            // menu_SDK
            // 
            this.menu_SDK.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sdk_LUBStudio,
            this.sdk_DecompileLUBs,
            this.sdk_Separator1,
            this.sdk_XNOStudio,
            this.sdk_ConvertXNOs,
            this.sdk_Separator2,
            this.mSTStudioToolStripMenuItem});
            this.menu_SDK.Name = "menu_SDK";
            this.menu_SDK.Size = new System.Drawing.Size(40, 20);
            this.menu_SDK.Text = "SDK";
            // 
            // sdk_LUBStudio
            // 
            this.sdk_LUBStudio.Enabled = false;
            this.sdk_LUBStudio.Name = "sdk_LUBStudio";
            this.sdk_LUBStudio.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.D)));
            this.sdk_LUBStudio.Size = new System.Drawing.Size(334, 22);
            this.sdk_LUBStudio.Text = "LUB Studio...";
            this.sdk_LUBStudio.Click += new System.EventHandler(this.Sdk_LUBStudio_Click);
            // 
            // sdk_DecompileLUBs
            // 
            this.sdk_DecompileLUBs.Enabled = false;
            this.sdk_DecompileLUBs.Name = "sdk_DecompileLUBs";
            this.sdk_DecompileLUBs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D)));
            this.sdk_DecompileLUBs.Size = new System.Drawing.Size(334, 22);
            this.sdk_DecompileLUBs.Text = "Decompile all LUBs in this directory";
            this.sdk_DecompileLUBs.Click += new System.EventHandler(this.Sdk_DecompileLUBs_Click);
            // 
            // sdk_Separator1
            // 
            this.sdk_Separator1.Name = "sdk_Separator1";
            this.sdk_Separator1.Size = new System.Drawing.Size(331, 6);
            // 
            // sdk_XNOStudio
            // 
            this.sdk_XNOStudio.Enabled = false;
            this.sdk_XNOStudio.Name = "sdk_XNOStudio";
            this.sdk_XNOStudio.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.X)));
            this.sdk_XNOStudio.Size = new System.Drawing.Size(334, 22);
            this.sdk_XNOStudio.Text = "XNO Studio...";
            this.sdk_XNOStudio.Click += new System.EventHandler(this.Sdk_XNOStudio_Click);
            // 
            // sdk_ConvertXNOs
            // 
            this.sdk_ConvertXNOs.Enabled = false;
            this.sdk_ConvertXNOs.Name = "sdk_ConvertXNOs";
            this.sdk_ConvertXNOs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.X)));
            this.sdk_ConvertXNOs.Size = new System.Drawing.Size(334, 22);
            this.sdk_ConvertXNOs.Text = "Convert all XNOs in this directory";
            this.sdk_ConvertXNOs.Click += new System.EventHandler(this.Sdk_ConvertXNOs_Click);
            // 
            // sdk_Separator2
            // 
            this.sdk_Separator2.Name = "sdk_Separator2";
            this.sdk_Separator2.Size = new System.Drawing.Size(331, 6);
            // 
            // mSTStudioToolStripMenuItem
            // 
            this.mSTStudioToolStripMenuItem.Enabled = false;
            this.mSTStudioToolStripMenuItem.Name = "mSTStudioToolStripMenuItem";
            this.mSTStudioToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.M)));
            this.mSTStudioToolStripMenuItem.Size = new System.Drawing.Size(334, 22);
            this.mSTStudioToolStripMenuItem.Text = "MST Studio...";
            // 
            // menu_Tabs
            // 
            this.menu_Tabs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabs_NewTab,
            this.tabs_Separator1,
            this.tabs_CloseTab});
            this.menu_Tabs.Name = "menu_Tabs";
            this.menu_Tabs.Size = new System.Drawing.Size(43, 20);
            this.menu_Tabs.Text = "Tabs";
            // 
            // tabs_NewTab
            // 
            this.tabs_NewTab.Image = ((System.Drawing.Image)(resources.GetObject("tabs_NewTab.Image")));
            this.tabs_NewTab.Name = "tabs_NewTab";
            this.tabs_NewTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.tabs_NewTab.Size = new System.Drawing.Size(170, 22);
            this.tabs_NewTab.Text = "New Tab";
            this.tabs_NewTab.Click += new System.EventHandler(this.Tabs_NewTab_Click);
            // 
            // tabs_Separator1
            // 
            this.tabs_Separator1.Name = "tabs_Separator1";
            this.tabs_Separator1.Size = new System.Drawing.Size(167, 6);
            // 
            // tabs_CloseTab
            // 
            this.tabs_CloseTab.Enabled = false;
            this.tabs_CloseTab.Image = ((System.Drawing.Image)(resources.GetObject("tabs_CloseTab.Image")));
            this.tabs_CloseTab.Name = "tabs_CloseTab";
            this.tabs_CloseTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.tabs_CloseTab.Size = new System.Drawing.Size(170, 22);
            this.tabs_CloseTab.Text = "Close Tab";
            this.tabs_CloseTab.Click += new System.EventHandler(this.Tabs_CloseTab_Click);
            // 
            // menu_Help
            // 
            this.menu_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.help_Documentation,
            this.help_Separator1,
            this.help_About});
            this.menu_Help.Name = "menu_Help";
            this.menu_Help.Size = new System.Drawing.Size(44, 20);
            this.menu_Help.Text = "Help";
            // 
            // help_Documentation
            // 
            this.help_Documentation.Image = ((System.Drawing.Image)(resources.GetObject("help_Documentation.Image")));
            this.help_Documentation.Name = "help_Documentation";
            this.help_Documentation.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.help_Documentation.Size = new System.Drawing.Size(176, 22);
            this.help_Documentation.Text = "Documentation";
            this.help_Documentation.Click += new System.EventHandler(this.Help_Documentation_Click);
            // 
            // help_Separator1
            // 
            this.help_Separator1.Name = "help_Separator1";
            this.help_Separator1.Size = new System.Drawing.Size(173, 6);
            // 
            // help_About
            // 
            this.help_About.Image = ((System.Drawing.Image)(resources.GetObject("help_About.Image")));
            this.help_About.Name = "help_About";
            this.help_About.Size = new System.Drawing.Size(176, 22);
            this.help_About.Text = "About";
            this.help_About.Click += new System.EventHandler(this.Help_About_Click);
            // 
            // ofd_OpenARC
            // 
            this.ofd_OpenARC.DefaultExt = "arc";
            this.ofd_OpenARC.Filter = "ARC Files|*.arc";
            this.ofd_OpenARC.Title = "Please select an ARC file...";
            // 
            // btn_SessionID
            // 
            this.btn_SessionID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_SessionID.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_SessionID.Enabled = false;
            this.btn_SessionID.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btn_SessionID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SessionID.Location = new System.Drawing.Point(751, -1);
            this.btn_SessionID.Name = "btn_SessionID";
            this.btn_SessionID.Size = new System.Drawing.Size(49, 26);
            this.btn_SessionID.TabIndex = 1;
            this.btn_SessionID.Text = "2006";
            this.btn_SessionID.UseVisualStyleBackColor = false;
            // 
            // pnl_Backdrop
            // 
            this.pnl_Backdrop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_Backdrop.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnl_Backdrop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl_Backdrop.Controls.Add(this.mstrip_Main);
            this.pnl_Backdrop.Location = new System.Drawing.Point(-2, -2);
            this.pnl_Backdrop.Name = "pnl_Backdrop";
            this.pnl_Backdrop.Size = new System.Drawing.Size(805, 28);
            this.pnl_Backdrop.TabIndex = 3;
            // 
            // btn_NewTab
            // 
            this.btn_NewTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(161)))), ((int)(((byte)(226)))));
            this.btn_NewTab.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btn_NewTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_NewTab.Location = new System.Drawing.Point(276, -1);
            this.btn_NewTab.Name = "btn_NewTab";
            this.btn_NewTab.Size = new System.Drawing.Size(23, 26);
            this.btn_NewTab.TabIndex = 1;
            this.btn_NewTab.Text = "+";
            this.btn_NewTab.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btn_NewTab.UseVisualStyleBackColor = false;
            this.btn_NewTab.Click += new System.EventHandler(this.Btn_NewTab_Click);
            // 
            // btn_Forward
            // 
            this.btn_Forward.BackColor = System.Drawing.Color.SkyBlue;
            this.btn_Forward.Enabled = false;
            this.btn_Forward.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btn_Forward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Forward.Location = new System.Drawing.Point(53, -1);
            this.btn_Forward.Name = "btn_Forward";
            this.btn_Forward.Size = new System.Drawing.Size(55, 26);
            this.btn_Forward.TabIndex = 5;
            this.btn_Forward.Text = "Forward";
            this.btn_Forward.UseVisualStyleBackColor = false;
            this.btn_Forward.Click += new System.EventHandler(this.Btn_Forward_Click);
            // 
            // btn_Back
            // 
            this.btn_Back.BackColor = System.Drawing.Color.Tomato;
            this.btn_Back.Enabled = false;
            this.btn_Back.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btn_Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Back.Location = new System.Drawing.Point(-1, -1);
            this.btn_Back.Name = "btn_Back";
            this.btn_Back.Size = new System.Drawing.Size(55, 26);
            this.btn_Back.TabIndex = 6;
            this.btn_Back.Text = "Back";
            this.btn_Back.UseVisualStyleBackColor = false;
            this.btn_Back.Click += new System.EventHandler(this.Btn_Back_Click);
            // 
            // btn_Repack
            // 
            this.btn_Repack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Repack.BackColor = System.Drawing.Color.LightGreen;
            this.btn_Repack.Enabled = false;
            this.btn_Repack.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btn_Repack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Repack.Location = new System.Drawing.Point(690, -1);
            this.btn_Repack.Name = "btn_Repack";
            this.btn_Repack.Size = new System.Drawing.Size(62, 26);
            this.btn_Repack.TabIndex = 7;
            this.btn_Repack.Text = "Repack";
            this.btn_Repack.UseVisualStyleBackColor = false;
            this.btn_Repack.Click += new System.EventHandler(this.Btn_Repack_Click);
            // 
            // tab_Main
            // 
            this.tab_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tab_Main.Location = new System.Drawing.Point(1, 26);
            this.tab_Main.Name = "tab_Main";
            this.tab_Main.SelectedIndex = 0;
            this.tab_Main.Size = new System.Drawing.Size(800, 424);
            this.tab_Main.TabIndex = 8;
            this.tab_Main.SelectedIndexChanged += new System.EventHandler(this.Tab_Main_SelectedIndexChanged);
            // 
            // tm_tabCheck
            // 
            this.tm_tabCheck.Tick += new System.EventHandler(this.Tm_tabCheck_Tick);
            // 
            // btn_OpenFolder
            // 
            this.btn_OpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_OpenFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(171)))), ((int)(((byte)(83)))));
            this.btn_OpenFolder.Enabled = false;
            this.btn_OpenFolder.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btn_OpenFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OpenFolder.Location = new System.Drawing.Point(613, -1);
            this.btn_OpenFolder.Name = "btn_OpenFolder";
            this.btn_OpenFolder.Size = new System.Drawing.Size(78, 26);
            this.btn_OpenFolder.TabIndex = 9;
            this.btn_OpenFolder.Text = "Open Folder";
            this.btn_OpenFolder.UseVisualStyleBackColor = false;
            this.btn_OpenFolder.Click += new System.EventHandler(this.Btn_OpenFolder_Click);
            // 
            // sfd_RepackARCAs
            // 
            this.sfd_RepackARCAs.Filter = "ARC Files|*.arc";
            this.sfd_RepackARCAs.RestoreDirectory = true;
            this.sfd_RepackARCAs.Title = "Repack ARC As...";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_OpenFolder);
            this.Controls.Add(this.tab_Main);
            this.Controls.Add(this.btn_Repack);
            this.Controls.Add(this.btn_NewTab);
            this.Controls.Add(this.btn_Back);
            this.Controls.Add(this.btn_Forward);
            this.Controls.Add(this.btn_SessionID);
            this.Controls.Add(this.pnl_Backdrop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mstrip_Main;
            this.MinimumSize = new System.Drawing.Size(500, 245);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sonic \'06 Toolkit";
            this.Load += new System.EventHandler(this.Main_Load);
            this.mstrip_Main.ResumeLayout(false);
            this.mstrip_Main.PerformLayout();
            this.pnl_Backdrop.ResumeLayout(false);
            this.pnl_Backdrop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip mstrip_Main;
        private System.Windows.Forms.ToolStripMenuItem menu_File;
        private System.Windows.Forms.ToolStripMenuItem menu_SDK;
        private System.Windows.Forms.ToolStripMenuItem menu_Tabs;
        private System.Windows.Forms.ToolStripMenuItem menu_Help;
        private System.Windows.Forms.ToolStripMenuItem file_OpenARC;
        private System.Windows.Forms.ToolStripSeparator file_Separator1;
        private System.Windows.Forms.ToolStripMenuItem file_Exit;
        private System.Windows.Forms.ToolStripMenuItem tabs_NewTab;
        private System.Windows.Forms.ToolStripSeparator tabs_Separator1;
        private System.Windows.Forms.ToolStripMenuItem tabs_CloseTab;
        private System.Windows.Forms.ToolStripMenuItem help_About;
        private System.Windows.Forms.ToolStripMenuItem sdk_LUBStudio;
        private System.Windows.Forms.ToolStripMenuItem sdk_DecompileLUBs;
        private System.Windows.Forms.ToolStripSeparator sdk_Separator1;
        private System.Windows.Forms.ToolStripMenuItem sdk_XNOStudio;
        private System.Windows.Forms.ToolStripMenuItem sdk_ConvertXNOs;
        private System.Windows.Forms.OpenFileDialog ofd_OpenARC;
        private System.Windows.Forms.Button btn_SessionID;
        private System.Windows.Forms.Panel pnl_Backdrop;
        private System.Windows.Forms.Button btn_Forward;
        private System.Windows.Forms.Button btn_Back;
        private System.Windows.Forms.Button btn_Repack;
        private System.Windows.Forms.Timer tm_tabCheck;
        private System.Windows.Forms.ToolStripMenuItem file_RepackARC;
        private System.Windows.Forms.ToolStripSeparator file_Separator2;
        private System.Windows.Forms.Button btn_OpenFolder;
        private System.Windows.Forms.TabControl tab_Main;
        private System.Windows.Forms.ToolStripSeparator sdk_Separator2;
        private System.Windows.Forms.ToolStripMenuItem mSTStudioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem file_CloseARC;
        private System.Windows.Forms.ToolStripMenuItem file_RepackARCAs;
        private System.Windows.Forms.SaveFileDialog sfd_RepackARCAs;
        private System.Windows.Forms.ToolStripMenuItem file_Preferences;
        private System.Windows.Forms.ToolStripMenuItem preferences_ShowSessionID;
        private System.Windows.Forms.ToolStripMenuItem help_Documentation;
        private System.Windows.Forms.ToolStripSeparator help_Separator1;
        private System.Windows.Forms.Button btn_NewTab;
        private System.Windows.Forms.ToolStripMenuItem preferences_Themes;
        private System.Windows.Forms.ToolStripMenuItem themes_Compact;
        private System.Windows.Forms.ToolStripMenuItem themes_Original;
        private System.Windows.Forms.ToolStripSeparator preferences_Separator1;
    }
}

