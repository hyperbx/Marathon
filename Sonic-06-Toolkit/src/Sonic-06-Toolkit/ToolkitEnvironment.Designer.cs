namespace Toolkit.EnvironmentX
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
            this.btn_OpenFolder = new System.Windows.Forms.Button();
            this.btn_Repack = new System.Windows.Forms.Button();
            this.btn_NewTab = new System.Windows.Forms.Button();
            this.btn_OpenFolderOptions = new System.Windows.Forms.Button();
            this.btn_Back = new System.Windows.Forms.Button();
            this.btn_Forward = new System.Windows.Forms.Button();
            this.btn_SessionID = new System.Windows.Forms.Button();
            this.pnl_MenuStrip = new System.Windows.Forms.Panel();
            this.mstrip_Main = new System.Windows.Forms.MenuStrip();
            this.main_File = new System.Windows.Forms.ToolStripMenuItem();
            this.file_NewARC = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.file_OpenARC = new System.Windows.Forms.ToolStripMenuItem();
            this.file_OpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.file_ExtractISO = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.file_Repack = new System.Windows.Forms.ToolStripMenuItem();
            this.file_RepackAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.file_Preferences = new System.Windows.Forms.ToolStripMenuItem();
            this.preferences_Paths = new System.Windows.Forms.ToolStripMenuItem();
            this.paths_ClearGameDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.preferences_AssociateARCs = new System.Windows.Forms.ToolStripMenuItem();
            this.preferences_DisableSoftwareUpdater = new System.Windows.Forms.ToolStripMenuItem();
            this.preferences_DisableGameDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.preferences_Advanced = new System.Windows.Forms.ToolStripMenuItem();
            this.advanced_DebugMode = new System.Windows.Forms.ToolStripMenuItem();
            this.advanced_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.advanced_Reset = new System.Windows.Forms.ToolStripMenuItem();
            this.file_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.main_SDK = new System.Windows.Forms.ToolStripMenuItem();
            this.main_Shortcuts = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeAT3 = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeBIN = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_UnpackCSB = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_UnpackCSBtoAIF = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_ExtractCSBsToWAV = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_ConvertDDS = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecompileLUB = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeMST = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeSET = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeXMA = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeXNO = new System.Windows.Forms.ToolStripMenuItem();
            this.main_Window = new System.Windows.Forms.ToolStripMenuItem();
            this.window_NewTab = new System.Windows.Forms.ToolStripMenuItem();
            this.window_NewWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.window_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.window_CloseTab = new System.Windows.Forms.ToolStripMenuItem();
            this.window_CloseAllTabs = new System.Windows.Forms.ToolStripMenuItem();
            this.main_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.mainHelp_Documentation = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.mainHelp_CheckForUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.mainHelp_ReportBug = new System.Windows.Forms.ToolStripMenuItem();
            this.mainHelp_About = new System.Windows.Forms.ToolStripMenuItem();
            this.status_Main = new System.Windows.Forms.StatusStrip();
            this.lbl_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.pic_Logo = new System.Windows.Forms.PictureBox();
            this.cms_Folder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.folder_OpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.folder_CopyToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_SetDefault = new System.Windows.Forms.Label();
            this.tm_CheapFix = new System.Windows.Forms.Timer(this.components);
            this.tm_ResetStatus = new System.Windows.Forms.Timer(this.components);
            this.bw_Worker = new System.ComponentModel.BackgroundWorker();
            this.unifytb_Main = new UnifyTabControl.UnifyTabControl();
            this.shortcuts_DecryptXEX = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeADX = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_UnpackARC = new System.Windows.Forms.ToolStripMenuItem();
            this.shorcuts_UnpackARCtoToolkit = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_UnpackARCtoDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecompileLUBinDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecompileLUBinArchive = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_ArchiveMerger = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_TextureRasteriser = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_ModelAnimationExporter = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_SonicSoundStudio = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_CollisionGenerator = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_TextEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_LuaCompilation = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_PlacementConverter = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_ExecutableDecryptor = new System.Windows.Forms.ToolStripMenuItem();
            this.pnl_MenuStrip.SuspendLayout();
            this.mstrip_Main.SuspendLayout();
            this.status_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).BeginInit();
            this.cms_Folder.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_OpenFolder
            // 
            this.btn_OpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_OpenFolder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(171)))), ((int)(((byte)(83)))));
            this.btn_OpenFolder.Enabled = false;
            this.btn_OpenFolder.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btn_OpenFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OpenFolder.Location = new System.Drawing.Point(593, -1);
            this.btn_OpenFolder.Name = "btn_OpenFolder";
            this.btn_OpenFolder.Size = new System.Drawing.Size(78, 26);
            this.btn_OpenFolder.TabIndex = 22;
            this.btn_OpenFolder.Text = "Open Folder";
            this.btn_OpenFolder.UseVisualStyleBackColor = false;
            this.btn_OpenFolder.Click += new System.EventHandler(this.Btn_OpenFolder_Click);
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
            this.btn_Repack.TabIndex = 21;
            this.btn_Repack.Text = "Repack";
            this.btn_Repack.UseVisualStyleBackColor = false;
            this.btn_Repack.Click += new System.EventHandler(this.Btn_Repack_Click);
            // 
            // btn_NewTab
            // 
            this.btn_NewTab.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_NewTab.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btn_NewTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_NewTab.Location = new System.Drawing.Point(365, 0);
            this.btn_NewTab.Name = "btn_NewTab";
            this.btn_NewTab.Size = new System.Drawing.Size(23, 25);
            this.btn_NewTab.TabIndex = 16;
            this.btn_NewTab.Text = "+";
            this.btn_NewTab.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btn_NewTab.UseVisualStyleBackColor = false;
            this.btn_NewTab.Click += new System.EventHandler(this.Window_NewTab_Click);
            // 
            // btn_OpenFolderOptions
            // 
            this.btn_OpenFolderOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_OpenFolderOptions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(171)))), ((int)(((byte)(83)))));
            this.btn_OpenFolderOptions.Enabled = false;
            this.btn_OpenFolderOptions.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.btn_OpenFolderOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OpenFolderOptions.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OpenFolderOptions.Location = new System.Drawing.Point(670, -1);
            this.btn_OpenFolderOptions.Name = "btn_OpenFolderOptions";
            this.btn_OpenFolderOptions.Size = new System.Drawing.Size(20, 26);
            this.btn_OpenFolderOptions.TabIndex = 25;
            this.btn_OpenFolderOptions.Text = "▼";
            this.btn_OpenFolderOptions.UseVisualStyleBackColor = false;
            this.btn_OpenFolderOptions.Click += new System.EventHandler(this.Btn_OpenFolderOptions_Click);
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
            this.btn_Back.TabIndex = 20;
            this.btn_Back.Text = "Back";
            this.btn_Back.UseVisualStyleBackColor = false;
            this.btn_Back.Click += new System.EventHandler(this.Btn_Back_Click);
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
            this.btn_Forward.TabIndex = 19;
            this.btn_Forward.Text = "Forward";
            this.btn_Forward.UseVisualStyleBackColor = false;
            this.btn_Forward.Click += new System.EventHandler(this.Btn_Forward_Click);
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
            this.btn_SessionID.TabIndex = 17;
            this.btn_SessionID.Text = "2006";
            this.btn_SessionID.UseVisualStyleBackColor = false;
            // 
            // pnl_MenuStrip
            // 
            this.pnl_MenuStrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_MenuStrip.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pnl_MenuStrip.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl_MenuStrip.Controls.Add(this.mstrip_Main);
            this.pnl_MenuStrip.Location = new System.Drawing.Point(-2, -2);
            this.pnl_MenuStrip.Name = "pnl_MenuStrip";
            this.pnl_MenuStrip.Size = new System.Drawing.Size(805, 28);
            this.pnl_MenuStrip.TabIndex = 18;
            // 
            // mstrip_Main
            // 
            this.mstrip_Main.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.mstrip_Main.Dock = System.Windows.Forms.DockStyle.None;
            this.mstrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.main_File,
            this.main_SDK,
            this.main_Shortcuts,
            this.main_Window,
            this.main_Help});
            this.mstrip_Main.Location = new System.Drawing.Point(104, 0);
            this.mstrip_Main.Name = "mstrip_Main";
            this.mstrip_Main.Size = new System.Drawing.Size(381, 24);
            this.mstrip_Main.TabIndex = 30;
            this.mstrip_Main.Text = "menuStrip1";
            // 
            // main_File
            // 
            this.main_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.file_NewARC,
            this.toolStripSeparator7,
            this.file_OpenARC,
            this.file_OpenFolder,
            this.file_ExtractISO,
            this.toolStripSeparator2,
            this.file_Repack,
            this.file_RepackAs,
            this.toolStripSeparator3,
            this.file_Preferences,
            this.file_Exit});
            this.main_File.Name = "main_File";
            this.main_File.Size = new System.Drawing.Size(37, 20);
            this.main_File.Text = "File";
            // 
            // file_NewARC
            // 
            this.file_NewARC.Image = ((System.Drawing.Image)(resources.GetObject("file_NewARC.Image")));
            this.file_NewARC.Name = "file_NewARC";
            this.file_NewARC.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.file_NewARC.Size = new System.Drawing.Size(214, 22);
            this.file_NewARC.Text = "New ARC...";
            this.file_NewARC.Click += new System.EventHandler(this.File_NewARC_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(211, 6);
            // 
            // file_OpenARC
            // 
            this.file_OpenARC.Image = ((System.Drawing.Image)(resources.GetObject("file_OpenARC.Image")));
            this.file_OpenARC.Name = "file_OpenARC";
            this.file_OpenARC.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.file_OpenARC.Size = new System.Drawing.Size(214, 22);
            this.file_OpenARC.Text = "Open ARC...";
            this.file_OpenARC.Click += new System.EventHandler(this.File_OpenARC_Click);
            // 
            // file_OpenFolder
            // 
            this.file_OpenFolder.Image = ((System.Drawing.Image)(resources.GetObject("file_OpenFolder.Image")));
            this.file_OpenFolder.Name = "file_OpenFolder";
            this.file_OpenFolder.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.O)));
            this.file_OpenFolder.Size = new System.Drawing.Size(214, 22);
            this.file_OpenFolder.Text = "Open Folder...";
            this.file_OpenFolder.Click += new System.EventHandler(this.File_OpenFolder_Click);
            // 
            // file_ExtractISO
            // 
            this.file_ExtractISO.Image = ((System.Drawing.Image)(resources.GetObject("file_ExtractISO.Image")));
            this.file_ExtractISO.Name = "file_ExtractISO";
            this.file_ExtractISO.Size = new System.Drawing.Size(214, 22);
            this.file_ExtractISO.Text = "Extract Xbox 360 ISO...";
            this.file_ExtractISO.Click += new System.EventHandler(this.File_ExtractISO_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(211, 6);
            // 
            // file_Repack
            // 
            this.file_Repack.Enabled = false;
            this.file_Repack.Image = ((System.Drawing.Image)(resources.GetObject("file_Repack.Image")));
            this.file_Repack.Name = "file_Repack";
            this.file_Repack.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.file_Repack.Size = new System.Drawing.Size(214, 22);
            this.file_Repack.Text = "Repack";
            this.file_Repack.Click += new System.EventHandler(this.Btn_Repack_Click);
            // 
            // file_RepackAs
            // 
            this.file_RepackAs.Enabled = false;
            this.file_RepackAs.Image = ((System.Drawing.Image)(resources.GetObject("file_RepackAs.Image")));
            this.file_RepackAs.Name = "file_RepackAs";
            this.file_RepackAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.file_RepackAs.Size = new System.Drawing.Size(214, 22);
            this.file_RepackAs.Text = "Repack As...";
            this.file_RepackAs.Click += new System.EventHandler(this.File_RepackAs_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(211, 6);
            // 
            // file_Preferences
            // 
            this.file_Preferences.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferences_Paths,
            this.toolStripSeparator1,
            this.preferences_AssociateARCs,
            this.preferences_DisableSoftwareUpdater,
            this.preferences_DisableGameDirectory,
            this.toolStripSeparator8,
            this.preferences_Advanced});
            this.file_Preferences.Image = ((System.Drawing.Image)(resources.GetObject("file_Preferences.Image")));
            this.file_Preferences.Name = "file_Preferences";
            this.file_Preferences.Size = new System.Drawing.Size(214, 22);
            this.file_Preferences.Text = "Preferences";
            // 
            // preferences_Paths
            // 
            this.preferences_Paths.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.paths_ClearGameDirectory});
            this.preferences_Paths.Image = ((System.Drawing.Image)(resources.GetObject("preferences_Paths.Image")));
            this.preferences_Paths.Name = "preferences_Paths";
            this.preferences_Paths.Size = new System.Drawing.Size(269, 22);
            this.preferences_Paths.Text = "Paths";
            // 
            // paths_ClearGameDirectory
            // 
            this.paths_ClearGameDirectory.Image = ((System.Drawing.Image)(resources.GetObject("paths_ClearGameDirectory.Image")));
            this.paths_ClearGameDirectory.Name = "paths_ClearGameDirectory";
            this.paths_ClearGameDirectory.Size = new System.Drawing.Size(184, 22);
            this.paths_ClearGameDirectory.Text = "Clear game directory";
            this.paths_ClearGameDirectory.Click += new System.EventHandler(this.Paths_ClearGameDirectory_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(266, 6);
            // 
            // preferences_AssociateARCs
            // 
            this.preferences_AssociateARCs.CheckOnClick = true;
            this.preferences_AssociateARCs.Image = ((System.Drawing.Image)(resources.GetObject("preferences_AssociateARCs.Image")));
            this.preferences_AssociateARCs.Name = "preferences_AssociateARCs";
            this.preferences_AssociateARCs.Size = new System.Drawing.Size(269, 22);
            this.preferences_AssociateARCs.Text = "Associate ARCs with Sonic \'06 Toolkit";
            // 
            // preferences_DisableSoftwareUpdater
            // 
            this.preferences_DisableSoftwareUpdater.CheckOnClick = true;
            this.preferences_DisableSoftwareUpdater.Image = ((System.Drawing.Image)(resources.GetObject("preferences_DisableSoftwareUpdater.Image")));
            this.preferences_DisableSoftwareUpdater.Name = "preferences_DisableSoftwareUpdater";
            this.preferences_DisableSoftwareUpdater.Size = new System.Drawing.Size(269, 22);
            this.preferences_DisableSoftwareUpdater.Text = "Disable software updater";
            // 
            // preferences_DisableGameDirectory
            // 
            this.preferences_DisableGameDirectory.CheckOnClick = true;
            this.preferences_DisableGameDirectory.Image = ((System.Drawing.Image)(resources.GetObject("preferences_DisableGameDirectory.Image")));
            this.preferences_DisableGameDirectory.Name = "preferences_DisableGameDirectory";
            this.preferences_DisableGameDirectory.Size = new System.Drawing.Size(269, 22);
            this.preferences_DisableGameDirectory.Text = "Disable game directory";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(266, 6);
            // 
            // preferences_Advanced
            // 
            this.preferences_Advanced.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.advanced_DebugMode,
            this.advanced_Separator1,
            this.advanced_Reset});
            this.preferences_Advanced.Image = ((System.Drawing.Image)(resources.GetObject("preferences_Advanced.Image")));
            this.preferences_Advanced.Name = "preferences_Advanced";
            this.preferences_Advanced.Size = new System.Drawing.Size(269, 22);
            this.preferences_Advanced.Text = "Advanced";
            // 
            // advanced_DebugMode
            // 
            this.advanced_DebugMode.CheckOnClick = true;
            this.advanced_DebugMode.Image = ((System.Drawing.Image)(resources.GetObject("advanced_DebugMode.Image")));
            this.advanced_DebugMode.Name = "advanced_DebugMode";
            this.advanced_DebugMode.Size = new System.Drawing.Size(190, 22);
            this.advanced_DebugMode.Text = "Debug Mode";
            this.advanced_DebugMode.Visible = false;
            // 
            // advanced_Separator1
            // 
            this.advanced_Separator1.Name = "advanced_Separator1";
            this.advanced_Separator1.Size = new System.Drawing.Size(187, 6);
            // 
            // advanced_Reset
            // 
            this.advanced_Reset.Image = ((System.Drawing.Image)(resources.GetObject("advanced_Reset.Image")));
            this.advanced_Reset.Name = "advanced_Reset";
            this.advanced_Reset.Size = new System.Drawing.Size(190, 22);
            this.advanced_Reset.Text = "Reset Sonic \'06 Toolkit";
            // 
            // file_Exit
            // 
            this.file_Exit.Image = ((System.Drawing.Image)(resources.GetObject("file_Exit.Image")));
            this.file_Exit.Name = "file_Exit";
            this.file_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.file_Exit.Size = new System.Drawing.Size(214, 22);
            this.file_Exit.Text = "Exit";
            this.file_Exit.Click += new System.EventHandler(this.File_Exit_Click);
            // 
            // main_SDK
            // 
            this.main_SDK.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sdk_ArchiveMerger,
            this.sdk_TextureRasteriser,
            this.sdk_ModelAnimationExporter,
            this.sdk_SonicSoundStudio,
            this.sdk_CollisionGenerator,
            this.sdk_TextEditor,
            this.sdk_LuaCompilation,
            this.sdk_PlacementConverter,
            this.sdk_ExecutableDecryptor});
            this.main_SDK.Name = "main_SDK";
            this.main_SDK.Size = new System.Drawing.Size(40, 20);
            this.main_SDK.Text = "SDK";
            // 
            // main_Shortcuts
            // 
            this.main_Shortcuts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcuts_DecodeADX,
            this.shortcuts_UnpackARC,
            this.shortcuts_DecodeAT3,
            this.shortcuts_DecodeBIN,
            this.shortcuts_UnpackCSB,
            this.shortcuts_ConvertDDS,
            this.shortcuts_DecompileLUB,
            this.shortcuts_DecodeMST,
            this.shortcuts_DecodeSET,
            this.shortcuts_DecryptXEX,
            this.shortcuts_DecodeXMA,
            this.shortcuts_DecodeXNO});
            this.main_Shortcuts.Name = "main_Shortcuts";
            this.main_Shortcuts.Size = new System.Drawing.Size(69, 20);
            this.main_Shortcuts.Text = "Shortcuts";
            // 
            // shortcuts_DecodeAT3
            // 
            this.shortcuts_DecodeAT3.Enabled = false;
            this.shortcuts_DecodeAT3.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_DecodeAT3.Image")));
            this.shortcuts_DecodeAT3.Name = "shortcuts_DecodeAT3";
            this.shortcuts_DecodeAT3.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.A)));
            this.shortcuts_DecodeAT3.Size = new System.Drawing.Size(301, 22);
            this.shortcuts_DecodeAT3.Text = "Decode all AT3 files here...";
            // 
            // shortcuts_DecodeBIN
            // 
            this.shortcuts_DecodeBIN.Enabled = false;
            this.shortcuts_DecodeBIN.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_DecodeBIN.Image")));
            this.shortcuts_DecodeBIN.Name = "shortcuts_DecodeBIN";
            this.shortcuts_DecodeBIN.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.B)));
            this.shortcuts_DecodeBIN.Size = new System.Drawing.Size(301, 22);
            this.shortcuts_DecodeBIN.Text = "Decode all BIN files here...";
            // 
            // shortcuts_UnpackCSB
            // 
            this.shortcuts_UnpackCSB.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcuts_UnpackCSBtoAIF,
            this.shortcuts_ExtractCSBsToWAV});
            this.shortcuts_UnpackCSB.Enabled = false;
            this.shortcuts_UnpackCSB.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_UnpackCSB.Image")));
            this.shortcuts_UnpackCSB.Name = "shortcuts_UnpackCSB";
            this.shortcuts_UnpackCSB.Size = new System.Drawing.Size(301, 22);
            this.shortcuts_UnpackCSB.Text = "Unpack all CSB files here...";
            // 
            // shortcuts_UnpackCSBtoAIF
            // 
            this.shortcuts_UnpackCSBtoAIF.Name = "shortcuts_UnpackCSBtoAIF";
            this.shortcuts_UnpackCSBtoAIF.Size = new System.Drawing.Size(256, 22);
            this.shortcuts_UnpackCSBtoAIF.Text = "Unpack all to AIF (sound editing)";
            // 
            // shortcuts_ExtractCSBsToWAV
            // 
            this.shortcuts_ExtractCSBsToWAV.Name = "shortcuts_ExtractCSBsToWAV";
            this.shortcuts_ExtractCSBsToWAV.Size = new System.Drawing.Size(256, 22);
            this.shortcuts_ExtractCSBsToWAV.Text = "Unpack all to WAV (sound ripping)";
            // 
            // shortcuts_ConvertDDS
            // 
            this.shortcuts_ConvertDDS.Enabled = false;
            this.shortcuts_ConvertDDS.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_ConvertDDS.Image")));
            this.shortcuts_ConvertDDS.Name = "shortcuts_ConvertDDS";
            this.shortcuts_ConvertDDS.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.D)));
            this.shortcuts_ConvertDDS.Size = new System.Drawing.Size(301, 22);
            this.shortcuts_ConvertDDS.Text = "Rasterise all DDS files here...";
            // 
            // shortcuts_DecompileLUB
            // 
            this.shortcuts_DecompileLUB.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcuts_DecompileLUBinDirectory,
            this.shortcuts_DecompileLUBinArchive});
            this.shortcuts_DecompileLUB.Enabled = false;
            this.shortcuts_DecompileLUB.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_DecompileLUB.Image")));
            this.shortcuts_DecompileLUB.Name = "shortcuts_DecompileLUB";
            this.shortcuts_DecompileLUB.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.L)));
            this.shortcuts_DecompileLUB.Size = new System.Drawing.Size(301, 22);
            this.shortcuts_DecompileLUB.Text = "Decompile all LUB files here...";
            // 
            // shortcuts_DecodeMST
            // 
            this.shortcuts_DecodeMST.Enabled = false;
            this.shortcuts_DecodeMST.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_DecodeMST.Image")));
            this.shortcuts_DecodeMST.Name = "shortcuts_DecodeMST";
            this.shortcuts_DecodeMST.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.M)));
            this.shortcuts_DecodeMST.Size = new System.Drawing.Size(301, 22);
            this.shortcuts_DecodeMST.Text = "Decode all MST files here...";
            // 
            // shortcuts_DecodeSET
            // 
            this.shortcuts_DecodeSET.Enabled = false;
            this.shortcuts_DecodeSET.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_DecodeSET.Image")));
            this.shortcuts_DecodeSET.Name = "shortcuts_DecodeSET";
            this.shortcuts_DecodeSET.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.shortcuts_DecodeSET.Size = new System.Drawing.Size(301, 22);
            this.shortcuts_DecodeSET.Text = "Decode all SET files here...";
            this.shortcuts_DecodeSET.Click += new System.EventHandler(this.Shortcuts_DecodeSET_Click);
            // 
            // shortcuts_DecodeXMA
            // 
            this.shortcuts_DecodeXMA.Enabled = false;
            this.shortcuts_DecodeXMA.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_DecodeXMA.Image")));
            this.shortcuts_DecodeXMA.Name = "shortcuts_DecodeXMA";
            this.shortcuts_DecodeXMA.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.W)));
            this.shortcuts_DecodeXMA.Size = new System.Drawing.Size(301, 22);
            this.shortcuts_DecodeXMA.Text = "Decode all XMA files here...";
            // 
            // shortcuts_DecodeXNO
            // 
            this.shortcuts_DecodeXNO.Enabled = false;
            this.shortcuts_DecodeXNO.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_DecodeXNO.Image")));
            this.shortcuts_DecodeXNO.Name = "shortcuts_DecodeXNO";
            this.shortcuts_DecodeXNO.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.X)));
            this.shortcuts_DecodeXNO.Size = new System.Drawing.Size(301, 22);
            this.shortcuts_DecodeXNO.Text = "Decode all XNO files here...";
            // 
            // main_Window
            // 
            this.main_Window.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.window_NewTab,
            this.window_NewWindow,
            this.window_Separator1,
            this.window_CloseTab,
            this.window_CloseAllTabs});
            this.main_Window.Name = "main_Window";
            this.main_Window.Size = new System.Drawing.Size(63, 20);
            this.main_Window.Text = "Window";
            // 
            // window_NewTab
            // 
            this.window_NewTab.Image = ((System.Drawing.Image)(resources.GetObject("window_NewTab.Image")));
            this.window_NewTab.Name = "window_NewTab";
            this.window_NewTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.window_NewTab.Size = new System.Drawing.Size(223, 22);
            this.window_NewTab.Text = "New Tab";
            this.window_NewTab.Click += new System.EventHandler(this.Window_NewTab_Click);
            // 
            // window_NewWindow
            // 
            this.window_NewWindow.Image = ((System.Drawing.Image)(resources.GetObject("window_NewWindow.Image")));
            this.window_NewWindow.Name = "window_NewWindow";
            this.window_NewWindow.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.window_NewWindow.Size = new System.Drawing.Size(223, 22);
            this.window_NewWindow.Text = "New Window";
            this.window_NewWindow.Click += new System.EventHandler(this.Window_NewWindow_Click);
            // 
            // window_Separator1
            // 
            this.window_Separator1.Name = "window_Separator1";
            this.window_Separator1.Size = new System.Drawing.Size(220, 6);
            // 
            // window_CloseTab
            // 
            this.window_CloseTab.Image = ((System.Drawing.Image)(resources.GetObject("window_CloseTab.Image")));
            this.window_CloseTab.Name = "window_CloseTab";
            this.window_CloseTab.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.window_CloseTab.Size = new System.Drawing.Size(223, 22);
            this.window_CloseTab.Text = "Close Tab";
            this.window_CloseTab.Click += new System.EventHandler(this.Window_CloseTab_Click);
            // 
            // window_CloseAllTabs
            // 
            this.window_CloseAllTabs.Image = ((System.Drawing.Image)(resources.GetObject("window_CloseAllTabs.Image")));
            this.window_CloseAllTabs.Name = "window_CloseAllTabs";
            this.window_CloseAllTabs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.W)));
            this.window_CloseAllTabs.Size = new System.Drawing.Size(223, 22);
            this.window_CloseAllTabs.Text = "Close All Tabs";
            this.window_CloseAllTabs.Click += new System.EventHandler(this.Window_CloseAllTabs_Click);
            // 
            // main_Help
            // 
            this.main_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainHelp_Documentation,
            this.toolStripSeparator11,
            this.mainHelp_CheckForUpdates,
            this.mainHelp_ReportBug,
            this.mainHelp_About});
            this.main_Help.Name = "main_Help";
            this.main_Help.Size = new System.Drawing.Size(44, 20);
            this.main_Help.Text = "Help";
            // 
            // mainHelp_Documentation
            // 
            this.mainHelp_Documentation.Image = ((System.Drawing.Image)(resources.GetObject("mainHelp_Documentation.Image")));
            this.mainHelp_Documentation.Name = "mainHelp_Documentation";
            this.mainHelp_Documentation.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.mainHelp_Documentation.Size = new System.Drawing.Size(203, 22);
            this.mainHelp_Documentation.Text = "Documentation";
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(200, 6);
            // 
            // mainHelp_CheckForUpdates
            // 
            this.mainHelp_CheckForUpdates.Image = ((System.Drawing.Image)(resources.GetObject("mainHelp_CheckForUpdates.Image")));
            this.mainHelp_CheckForUpdates.Name = "mainHelp_CheckForUpdates";
            this.mainHelp_CheckForUpdates.Size = new System.Drawing.Size(203, 22);
            this.mainHelp_CheckForUpdates.Text = "Check for updates...";
            // 
            // mainHelp_ReportBug
            // 
            this.mainHelp_ReportBug.Image = ((System.Drawing.Image)(resources.GetObject("mainHelp_ReportBug.Image")));
            this.mainHelp_ReportBug.Name = "mainHelp_ReportBug";
            this.mainHelp_ReportBug.Size = new System.Drawing.Size(203, 22);
            this.mainHelp_ReportBug.Text = "Report a bug...";
            // 
            // mainHelp_About
            // 
            this.mainHelp_About.Image = ((System.Drawing.Image)(resources.GetObject("mainHelp_About.Image")));
            this.mainHelp_About.Name = "mainHelp_About";
            this.mainHelp_About.Size = new System.Drawing.Size(203, 22);
            this.mainHelp_About.Text = "About";
            // 
            // status_Main
            // 
            this.status_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_Status});
            this.status_Main.Location = new System.Drawing.Point(0, 428);
            this.status_Main.Name = "status_Main";
            this.status_Main.Size = new System.Drawing.Size(800, 22);
            this.status_Main.TabIndex = 26;
            this.status_Main.Text = "statusStrip1";
            // 
            // lbl_Status
            // 
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(42, 17);
            this.lbl_Status.Text = "Ready.";
            // 
            // pic_Logo
            // 
            this.pic_Logo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_Logo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pic_Logo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_Logo.BackgroundImage")));
            this.pic_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic_Logo.Location = new System.Drawing.Point(467, 166);
            this.pic_Logo.Name = "pic_Logo";
            this.pic_Logo.Size = new System.Drawing.Size(334, 263);
            this.pic_Logo.TabIndex = 28;
            this.pic_Logo.TabStop = false;
            // 
            // cms_Folder
            // 
            this.cms_Folder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.folder_OpenFolder,
            this.folder_CopyToClipboard});
            this.cms_Folder.Name = "cms_Repack";
            this.cms_Folder.ShowItemToolTips = false;
            this.cms_Folder.Size = new System.Drawing.Size(172, 48);
            this.cms_Folder.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.Cms_Folder_Closed);
            // 
            // folder_OpenFolder
            // 
            this.folder_OpenFolder.BackColor = System.Drawing.SystemColors.Control;
            this.folder_OpenFolder.Image = ((System.Drawing.Image)(resources.GetObject("folder_OpenFolder.Image")));
            this.folder_OpenFolder.Name = "folder_OpenFolder";
            this.folder_OpenFolder.Size = new System.Drawing.Size(171, 22);
            this.folder_OpenFolder.Text = "Open Folder";
            this.folder_OpenFolder.Click += new System.EventHandler(this.Btn_OpenFolder_Click);
            // 
            // folder_CopyToClipboard
            // 
            this.folder_CopyToClipboard.Image = ((System.Drawing.Image)(resources.GetObject("folder_CopyToClipboard.Image")));
            this.folder_CopyToClipboard.Name = "folder_CopyToClipboard";
            this.folder_CopyToClipboard.Size = new System.Drawing.Size(171, 22);
            this.folder_CopyToClipboard.Text = "Copy to Clipboard";
            this.folder_CopyToClipboard.Click += new System.EventHandler(this.Folder_CopyToClipboard_Click);
            // 
            // lbl_SetDefault
            // 
            this.lbl_SetDefault.AutoSize = true;
            this.lbl_SetDefault.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_SetDefault.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbl_SetDefault.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lbl_SetDefault.Location = new System.Drawing.Point(7, 51);
            this.lbl_SetDefault.Name = "lbl_SetDefault";
            this.lbl_SetDefault.Size = new System.Drawing.Size(219, 15);
            this.lbl_SetDefault.TabIndex = 29;
            this.lbl_SetDefault.Text = "Click here to select your game directory...";
            this.lbl_SetDefault.Click += new System.EventHandler(this.Lbl_SetDefault_Click);
            // 
            // tm_CheapFix
            // 
            this.tm_CheapFix.Interval = 10;
            this.tm_CheapFix.Tick += new System.EventHandler(this.Tm_CheapFix_Tick);
            // 
            // tm_ResetStatus
            // 
            this.tm_ResetStatus.Interval = 5000;
            this.tm_ResetStatus.Tick += new System.EventHandler(this.Tm_ResetStatus_Tick);
            // 
            // unifytb_Main
            // 
            this.unifytb_Main.ActiveColor = System.Drawing.SystemColors.ControlLightLight;
            this.unifytb_Main.AllowDrop = true;
            this.unifytb_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.unifytb_Main.BackTabColor = System.Drawing.SystemColors.ControlLightLight;
            this.unifytb_Main.BorderColor = System.Drawing.Color.Transparent;
            this.unifytb_Main.ClosingButtonColor = System.Drawing.SystemColors.GrayText;
            this.unifytb_Main.ClosingMessage = null;
            this.unifytb_Main.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unifytb_Main.HeaderColor = System.Drawing.SystemColors.Control;
            this.unifytb_Main.HorizontalLineColor = System.Drawing.SystemColors.ControlLightLight;
            this.unifytb_Main.ItemSize = new System.Drawing.Size(240, 16);
            this.unifytb_Main.Location = new System.Drawing.Point(0, 25);
            this.unifytb_Main.Name = "unifytb_Main";
            this.unifytb_Main.SelectedIndex = 0;
            this.unifytb_Main.SelectedTextColor = System.Drawing.SystemColors.ControlText;
            this.unifytb_Main.ShowClosingButton = false;
            this.unifytb_Main.ShowClosingMessage = false;
            this.unifytb_Main.Size = new System.Drawing.Size(800, 404);
            this.unifytb_Main.TabIndex = 27;
            this.unifytb_Main.TextColor = System.Drawing.SystemColors.ControlText;
            this.unifytb_Main.SelectedIndexChanged += new System.EventHandler(this.Unifytb_Main_SelectedIndexChanged);
            this.unifytb_Main.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Unifytb_Main_MouseClick);
            // 
            // shortcuts_DecryptXEX
            // 
            this.shortcuts_DecryptXEX.Enabled = false;
            this.shortcuts_DecryptXEX.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_DecryptXEX.Image")));
            this.shortcuts_DecryptXEX.Name = "shortcuts_DecryptXEX";
            this.shortcuts_DecryptXEX.Size = new System.Drawing.Size(301, 22);
            this.shortcuts_DecryptXEX.Text = "Decrypt all XEX files here...";
            // 
            // shortcuts_DecodeADX
            // 
            this.shortcuts_DecodeADX.Enabled = false;
            this.shortcuts_DecodeADX.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_DecodeADX.Image")));
            this.shortcuts_DecodeADX.Name = "shortcuts_DecodeADX";
            this.shortcuts_DecodeADX.Size = new System.Drawing.Size(301, 22);
            this.shortcuts_DecodeADX.Text = "Decode all ADX files here...";
            // 
            // shortcuts_UnpackARC
            // 
            this.shortcuts_UnpackARC.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shorcuts_UnpackARCtoToolkit,
            this.shortcuts_UnpackARCtoDirectory});
            this.shortcuts_UnpackARC.Enabled = false;
            this.shortcuts_UnpackARC.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_UnpackARC.Image")));
            this.shortcuts_UnpackARC.Name = "shortcuts_UnpackARC";
            this.shortcuts_UnpackARC.Size = new System.Drawing.Size(301, 22);
            this.shortcuts_UnpackARC.Text = "Unpack all ARC files here...";
            // 
            // shorcuts_UnpackARCtoToolkit
            // 
            this.shorcuts_UnpackARCtoToolkit.Name = "shorcuts_UnpackARCtoToolkit";
            this.shorcuts_UnpackARCtoToolkit.Size = new System.Drawing.Size(238, 22);
            this.shorcuts_UnpackARCtoToolkit.Text = "Unpack to Sonic \'06 Toolkit";
            // 
            // shortcuts_UnpackARCtoDirectory
            // 
            this.shortcuts_UnpackARCtoDirectory.Name = "shortcuts_UnpackARCtoDirectory";
            this.shortcuts_UnpackARCtoDirectory.Size = new System.Drawing.Size(238, 22);
            this.shortcuts_UnpackARCtoDirectory.Text = "Unpack in the current directory";
            // 
            // shortcuts_DecompileLUBinDirectory
            // 
            this.shortcuts_DecompileLUBinDirectory.Name = "shortcuts_DecompileLUBinDirectory";
            this.shortcuts_DecompileLUBinDirectory.Size = new System.Drawing.Size(270, 22);
            this.shortcuts_DecompileLUBinDirectory.Text = "Decompile all in the current directory";
            // 
            // shortcuts_DecompileLUBinArchive
            // 
            this.shortcuts_DecompileLUBinArchive.Name = "shortcuts_DecompileLUBinArchive";
            this.shortcuts_DecompileLUBinArchive.Size = new System.Drawing.Size(270, 22);
            this.shortcuts_DecompileLUBinArchive.Text = "Decompile all in the entire archive";
            // 
            // sdk_ArchiveMerger
            // 
            this.sdk_ArchiveMerger.Enabled = false;
            this.sdk_ArchiveMerger.Image = ((System.Drawing.Image)(resources.GetObject("sdk_ArchiveMerger.Image")));
            this.sdk_ArchiveMerger.Name = "sdk_ArchiveMerger";
            this.sdk_ArchiveMerger.Size = new System.Drawing.Size(301, 22);
            this.sdk_ArchiveMerger.Text = "Archive Merger (ARC)...";
            // 
            // sdk_TextureRasteriser
            // 
            this.sdk_TextureRasteriser.Enabled = false;
            this.sdk_TextureRasteriser.Image = ((System.Drawing.Image)(resources.GetObject("sdk_TextureRasteriser.Image")));
            this.sdk_TextureRasteriser.Name = "sdk_TextureRasteriser";
            this.sdk_TextureRasteriser.Size = new System.Drawing.Size(301, 22);
            this.sdk_TextureRasteriser.Text = "Texture Rasteriser (DDS)...";
            // 
            // sdk_ModelAnimationExporter
            // 
            this.sdk_ModelAnimationExporter.Enabled = false;
            this.sdk_ModelAnimationExporter.Image = ((System.Drawing.Image)(resources.GetObject("sdk_ModelAnimationExporter.Image")));
            this.sdk_ModelAnimationExporter.Name = "sdk_ModelAnimationExporter";
            this.sdk_ModelAnimationExporter.Size = new System.Drawing.Size(301, 22);
            this.sdk_ModelAnimationExporter.Text = "Model/Animation Exporter (XNO/XNM)...";
            // 
            // sdk_SonicSoundStudio
            // 
            this.sdk_SonicSoundStudio.Enabled = false;
            this.sdk_SonicSoundStudio.Image = ((System.Drawing.Image)(resources.GetObject("sdk_SonicSoundStudio.Image")));
            this.sdk_SonicSoundStudio.Name = "sdk_SonicSoundStudio";
            this.sdk_SonicSoundStudio.Size = new System.Drawing.Size(301, 22);
            this.sdk_SonicSoundStudio.Text = "Sonic Sound Studio (ADX/AT3/CSB/XMA)...";
            // 
            // sdk_CollisionGenerator
            // 
            this.sdk_CollisionGenerator.Enabled = false;
            this.sdk_CollisionGenerator.Image = ((System.Drawing.Image)(resources.GetObject("sdk_CollisionGenerator.Image")));
            this.sdk_CollisionGenerator.Name = "sdk_CollisionGenerator";
            this.sdk_CollisionGenerator.Size = new System.Drawing.Size(301, 22);
            this.sdk_CollisionGenerator.Text = "Collision Generator (BIN)...";
            // 
            // sdk_TextEditor
            // 
            this.sdk_TextEditor.Enabled = false;
            this.sdk_TextEditor.Image = ((System.Drawing.Image)(resources.GetObject("sdk_TextEditor.Image")));
            this.sdk_TextEditor.Name = "sdk_TextEditor";
            this.sdk_TextEditor.Size = new System.Drawing.Size(301, 22);
            this.sdk_TextEditor.Text = "Text Editor (MST)...";
            // 
            // sdk_LuaCompilation
            // 
            this.sdk_LuaCompilation.Enabled = false;
            this.sdk_LuaCompilation.Image = ((System.Drawing.Image)(resources.GetObject("sdk_LuaCompilation.Image")));
            this.sdk_LuaCompilation.Name = "sdk_LuaCompilation";
            this.sdk_LuaCompilation.Size = new System.Drawing.Size(301, 22);
            this.sdk_LuaCompilation.Text = "Lua Compilation (LUB)...";
            // 
            // sdk_PlacementConverter
            // 
            this.sdk_PlacementConverter.Enabled = false;
            this.sdk_PlacementConverter.Image = ((System.Drawing.Image)(resources.GetObject("sdk_PlacementConverter.Image")));
            this.sdk_PlacementConverter.Name = "sdk_PlacementConverter";
            this.sdk_PlacementConverter.Size = new System.Drawing.Size(301, 22);
            this.sdk_PlacementConverter.Text = "Placement Converter (SET)...";
            this.sdk_PlacementConverter.Click += new System.EventHandler(this.Sdk_PlacementConverter_Click);
            // 
            // sdk_ExecutableDecryptor
            // 
            this.sdk_ExecutableDecryptor.Enabled = false;
            this.sdk_ExecutableDecryptor.Image = ((System.Drawing.Image)(resources.GetObject("sdk_ExecutableDecryptor.Image")));
            this.sdk_ExecutableDecryptor.Name = "sdk_ExecutableDecryptor";
            this.sdk_ExecutableDecryptor.Size = new System.Drawing.Size(301, 22);
            this.sdk_ExecutableDecryptor.Text = "Executable Decryptor (XEX)...";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbl_SetDefault);
            this.Controls.Add(this.pic_Logo);
            this.Controls.Add(this.unifytb_Main);
            this.Controls.Add(this.status_Main);
            this.Controls.Add(this.btn_OpenFolder);
            this.Controls.Add(this.btn_Repack);
            this.Controls.Add(this.btn_NewTab);
            this.Controls.Add(this.btn_OpenFolderOptions);
            this.Controls.Add(this.btn_Back);
            this.Controls.Add(this.btn_Forward);
            this.Controls.Add(this.btn_SessionID);
            this.Controls.Add(this.pnl_MenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(609, 489);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sonic \'06 Toolkit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.pnl_MenuStrip.ResumeLayout(false);
            this.pnl_MenuStrip.PerformLayout();
            this.mstrip_Main.ResumeLayout(false);
            this.mstrip_Main.PerformLayout();
            this.status_Main.ResumeLayout(false);
            this.status_Main.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).EndInit();
            this.cms_Folder.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_OpenFolder;
        private System.Windows.Forms.Button btn_Repack;
        private System.Windows.Forms.Button btn_NewTab;
        private System.Windows.Forms.Button btn_OpenFolderOptions;
        private System.Windows.Forms.Button btn_Back;
        private System.Windows.Forms.Button btn_Forward;
        public System.Windows.Forms.Button btn_SessionID;
        private System.Windows.Forms.Panel pnl_MenuStrip;
        private System.Windows.Forms.StatusStrip status_Main;
        private UnifyTabControl.UnifyTabControl unifytb_Main;
        private System.Windows.Forms.PictureBox pic_Logo;
        private System.Windows.Forms.ContextMenuStrip cms_Folder;
        private System.Windows.Forms.ToolStripMenuItem folder_OpenFolder;
        private System.Windows.Forms.ToolStripMenuItem folder_CopyToClipboard;
        private System.Windows.Forms.Label lbl_SetDefault;
        private System.Windows.Forms.Timer tm_CheapFix;
        private System.Windows.Forms.Timer tm_ResetStatus;
        private System.ComponentModel.BackgroundWorker bw_Worker;
        private System.Windows.Forms.ToolStripStatusLabel lbl_Status;
        private System.Windows.Forms.MenuStrip mstrip_Main;
        private System.Windows.Forms.ToolStripMenuItem main_File;
        private System.Windows.Forms.ToolStripMenuItem file_NewARC;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem file_OpenARC;
        private System.Windows.Forms.ToolStripMenuItem file_ExtractISO;
        private System.Windows.Forms.ToolStripMenuItem file_OpenFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem file_Repack;
        private System.Windows.Forms.ToolStripMenuItem file_RepackAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem file_Preferences;
        private System.Windows.Forms.ToolStripMenuItem preferences_AssociateARCs;
        private System.Windows.Forms.ToolStripMenuItem preferences_DisableSoftwareUpdater;
        private System.Windows.Forms.ToolStripMenuItem preferences_DisableGameDirectory;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem preferences_Advanced;
        public System.Windows.Forms.ToolStripMenuItem advanced_DebugMode;
        public System.Windows.Forms.ToolStripSeparator advanced_Separator1;
        private System.Windows.Forms.ToolStripMenuItem advanced_Reset;
        private System.Windows.Forms.ToolStripMenuItem file_Exit;
        private System.Windows.Forms.ToolStripMenuItem main_SDK;
        private System.Windows.Forms.ToolStripMenuItem main_Shortcuts;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeAT3;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeBIN;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_UnpackCSB;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_UnpackCSBtoAIF;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_ExtractCSBsToWAV;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_ConvertDDS;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecompileLUB;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeMST;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeSET;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeXMA;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeXNO;
        private System.Windows.Forms.ToolStripMenuItem main_Window;
        private System.Windows.Forms.ToolStripMenuItem window_NewTab;
        private System.Windows.Forms.ToolStripMenuItem window_NewWindow;
        private System.Windows.Forms.ToolStripSeparator window_Separator1;
        private System.Windows.Forms.ToolStripMenuItem window_CloseTab;
        private System.Windows.Forms.ToolStripMenuItem window_CloseAllTabs;
        private System.Windows.Forms.ToolStripMenuItem main_Help;
        private System.Windows.Forms.ToolStripMenuItem mainHelp_Documentation;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem mainHelp_CheckForUpdates;
        private System.Windows.Forms.ToolStripMenuItem mainHelp_ReportBug;
        private System.Windows.Forms.ToolStripMenuItem mainHelp_About;
        private System.Windows.Forms.ToolStripMenuItem preferences_Paths;
        private System.Windows.Forms.ToolStripMenuItem paths_ClearGameDirectory;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeADX;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_UnpackARC;
        private System.Windows.Forms.ToolStripMenuItem shorcuts_UnpackARCtoToolkit;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_UnpackARCtoDirectory;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecompileLUBinDirectory;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecompileLUBinArchive;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecryptXEX;
        private System.Windows.Forms.ToolStripMenuItem sdk_ArchiveMerger;
        private System.Windows.Forms.ToolStripMenuItem sdk_TextureRasteriser;
        private System.Windows.Forms.ToolStripMenuItem sdk_ModelAnimationExporter;
        private System.Windows.Forms.ToolStripMenuItem sdk_SonicSoundStudio;
        private System.Windows.Forms.ToolStripMenuItem sdk_CollisionGenerator;
        private System.Windows.Forms.ToolStripMenuItem sdk_TextEditor;
        private System.Windows.Forms.ToolStripMenuItem sdk_LuaCompilation;
        private System.Windows.Forms.ToolStripMenuItem sdk_PlacementConverter;
        private System.Windows.Forms.ToolStripMenuItem sdk_ExecutableDecryptor;
    }
}

