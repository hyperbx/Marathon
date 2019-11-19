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
            this.file_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.file_OpenARC = new System.Windows.Forms.ToolStripMenuItem();
            this.file_OpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.file_ExtractISO = new System.Windows.Forms.ToolStripMenuItem();
            this.file_Separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.file_Repack = new System.Windows.Forms.ToolStripMenuItem();
            this.file_RepackAs = new System.Windows.Forms.ToolStripMenuItem();
            this.file_RepackAll = new System.Windows.Forms.ToolStripMenuItem();
            this.file_Separator3 = new System.Windows.Forms.ToolStripSeparator();
            this.file_Preferences = new System.Windows.Forms.ToolStripMenuItem();
            this.preferences_Paths = new System.Windows.Forms.ToolStripMenuItem();
            this.paths_ClearGameDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.preferences_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.preferences_AssociateARCs = new System.Windows.Forms.ToolStripMenuItem();
            this.preferences_DisableSoftwareUpdater = new System.Windows.Forms.ToolStripMenuItem();
            this.preferences_DisableGameDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.preferences_Separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.preferences_Advanced = new System.Windows.Forms.ToolStripMenuItem();
            this.advanced_ResetToolkit = new System.Windows.Forms.ToolStripMenuItem();
            this.advanced_ResetLog = new System.Windows.Forms.ToolStripMenuItem();
            this.file_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.main_SDK = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_ArchiveMerger = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_CollisionGenerator = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_ExecutableDecryptor = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_LuaCompilation = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_ModelAnimationExporter = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_PlacementConverter = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_SonicSoundStudio = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_TextureRasteriser = new System.Windows.Forms.ToolStripMenuItem();
            this.sdk_TextEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.main_Shortcuts = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_UnpackARC = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_UnpackARCtoDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_UnpackARCforModManager = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_UnpackARCtoToolkit = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeBIN = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeBINinDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeBINinSubdirectories = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecryptXEX = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecryptXEXinDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecryptXEXinSubdirectories = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecompileLUB = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecompileLUBinDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecompileMissionLUBinDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecompileLUBinSubdirectories = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecompileMissionLUBinSubdirectories = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeXNO = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeXNOinDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeXNOinSubdirectories = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeSET = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeSETinDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeSETinSubdirectories = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeADX = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeADXinDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeADXinSubdirectories = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeAT3 = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeAT3inDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeAT3inSubdirectories = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_UnpackCSB = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_UnpackCSBinDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_UnpackCSBinDirectoryToADX = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_UnpackCSBinDirectoryToWAV = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_UnpackCSBinSubdirectories = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_UnpackCSBinSubdirectoriesToADX = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_UnpackCSBinSubdirectoriesToWAV = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeXMA = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeXMAinDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeXMAinSubdirectories = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_ConvertDDS = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_ConvertDDSinDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_ConvertDDSinSubdirectories = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeMST = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeMSTinDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcuts_DecodeMSTinSubdirectories = new System.Windows.Forms.ToolStripMenuItem();
            this.main_Window = new System.Windows.Forms.ToolStripMenuItem();
            this.window_NewTab = new System.Windows.Forms.ToolStripMenuItem();
            this.window_NewWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.window_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.window_CloseTab = new System.Windows.Forms.ToolStripMenuItem();
            this.window_CloseAllTabs = new System.Windows.Forms.ToolStripMenuItem();
            this.main_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.help_Documentation = new System.Windows.Forms.ToolStripMenuItem();
            this.help_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.help_CheckForUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.help_ReportBug = new System.Windows.Forms.ToolStripMenuItem();
            this.help_GitHub = new System.Windows.Forms.ToolStripMenuItem();
            this.help_About = new System.Windows.Forms.ToolStripMenuItem();
            this.status_Main = new System.Windows.Forms.StatusStrip();
            this.pic_Logo = new System.Windows.Forms.PictureBox();
            this.cms_Folder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.folder_OpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.folder_CopyToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_SetDefault = new System.Windows.Forms.Label();
            this.tm_CheapFix = new System.Windows.Forms.Timer(this.components);
            this.tm_ResetStatus = new System.Windows.Forms.Timer(this.components);
            this.bw_Worker = new System.ComponentModel.BackgroundWorker();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.unifytb_Main = new UnifyTabControl.UnifyTabControl();
            this.pnl_MenuStrip.SuspendLayout();
            this.mstrip_Main.SuspendLayout();
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
            this.btn_NewTab.Location = new System.Drawing.Point(368, 0);
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
            this.btn_SessionID.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_SessionID.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_SessionID.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_SessionID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SessionID.ForeColor = System.Drawing.SystemColors.GrayText;
            this.btn_SessionID.Location = new System.Drawing.Point(751, -1);
            this.btn_SessionID.Name = "btn_SessionID";
            this.btn_SessionID.Size = new System.Drawing.Size(49, 26);
            this.btn_SessionID.TabIndex = 17;
            this.btn_SessionID.Text = "2006";
            this.btn_SessionID.UseVisualStyleBackColor = false;
            this.btn_SessionID.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_SessionID_MouseUp);
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
            this.mstrip_Main.Location = new System.Drawing.Point(105, 0);
            this.mstrip_Main.Name = "mstrip_Main";
            this.mstrip_Main.Size = new System.Drawing.Size(381, 24);
            this.mstrip_Main.TabIndex = 30;
            this.mstrip_Main.Text = "menuStrip1";
            // 
            // main_File
            // 
            this.main_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.file_NewARC,
            this.file_Separator1,
            this.file_OpenARC,
            this.file_OpenFolder,
            this.file_ExtractISO,
            this.file_Separator2,
            this.file_Repack,
            this.file_RepackAs,
            this.file_RepackAll,
            this.file_Separator3,
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
            // file_Separator1
            // 
            this.file_Separator1.Name = "file_Separator1";
            this.file_Separator1.Size = new System.Drawing.Size(211, 6);
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
            // file_Separator2
            // 
            this.file_Separator2.Name = "file_Separator2";
            this.file_Separator2.Size = new System.Drawing.Size(211, 6);
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
            // file_RepackAll
            // 
            this.file_RepackAll.Enabled = false;
            this.file_RepackAll.Image = ((System.Drawing.Image)(resources.GetObject("file_RepackAll.Image")));
            this.file_RepackAll.Name = "file_RepackAll";
            this.file_RepackAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.file_RepackAll.Size = new System.Drawing.Size(214, 22);
            this.file_RepackAll.Text = "Repack All";
            this.file_RepackAll.Click += new System.EventHandler(this.File_RepackAll_Click);
            // 
            // file_Separator3
            // 
            this.file_Separator3.Name = "file_Separator3";
            this.file_Separator3.Size = new System.Drawing.Size(211, 6);
            // 
            // file_Preferences
            // 
            this.file_Preferences.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preferences_Paths,
            this.preferences_Separator1,
            this.preferences_AssociateARCs,
            this.preferences_DisableSoftwareUpdater,
            this.preferences_DisableGameDirectory,
            this.preferences_Separator2,
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
            // preferences_Separator1
            // 
            this.preferences_Separator1.Name = "preferences_Separator1";
            this.preferences_Separator1.Size = new System.Drawing.Size(266, 6);
            // 
            // preferences_AssociateARCs
            // 
            this.preferences_AssociateARCs.CheckOnClick = true;
            this.preferences_AssociateARCs.Image = ((System.Drawing.Image)(resources.GetObject("preferences_AssociateARCs.Image")));
            this.preferences_AssociateARCs.Name = "preferences_AssociateARCs";
            this.preferences_AssociateARCs.Size = new System.Drawing.Size(269, 22);
            this.preferences_AssociateARCs.Text = "Associate ARCs with Sonic \'06 Toolkit";
            this.preferences_AssociateARCs.CheckedChanged += new System.EventHandler(this.Preferences_AssociateARCs_CheckedChanged);
            // 
            // preferences_DisableSoftwareUpdater
            // 
            this.preferences_DisableSoftwareUpdater.CheckOnClick = true;
            this.preferences_DisableSoftwareUpdater.Image = ((System.Drawing.Image)(resources.GetObject("preferences_DisableSoftwareUpdater.Image")));
            this.preferences_DisableSoftwareUpdater.Name = "preferences_DisableSoftwareUpdater";
            this.preferences_DisableSoftwareUpdater.Size = new System.Drawing.Size(269, 22);
            this.preferences_DisableSoftwareUpdater.Text = "Disable software updater";
            this.preferences_DisableSoftwareUpdater.CheckedChanged += new System.EventHandler(this.Preferences_DisableSoftwareUpdater_CheckedChanged);
            // 
            // preferences_DisableGameDirectory
            // 
            this.preferences_DisableGameDirectory.CheckOnClick = true;
            this.preferences_DisableGameDirectory.Image = ((System.Drawing.Image)(resources.GetObject("preferences_DisableGameDirectory.Image")));
            this.preferences_DisableGameDirectory.Name = "preferences_DisableGameDirectory";
            this.preferences_DisableGameDirectory.Size = new System.Drawing.Size(269, 22);
            this.preferences_DisableGameDirectory.Text = "Disable game directory";
            this.preferences_DisableGameDirectory.CheckedChanged += new System.EventHandler(this.Preferences_DisableGameDirectory_CheckedChanged);
            // 
            // preferences_Separator2
            // 
            this.preferences_Separator2.Name = "preferences_Separator2";
            this.preferences_Separator2.Size = new System.Drawing.Size(266, 6);
            // 
            // preferences_Advanced
            // 
            this.preferences_Advanced.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.advanced_ResetToolkit,
            this.advanced_ResetLog});
            this.preferences_Advanced.Image = ((System.Drawing.Image)(resources.GetObject("preferences_Advanced.Image")));
            this.preferences_Advanced.Name = "preferences_Advanced";
            this.preferences_Advanced.Size = new System.Drawing.Size(269, 22);
            this.preferences_Advanced.Text = "Advanced";
            // 
            // advanced_ResetToolkit
            // 
            this.advanced_ResetToolkit.Image = ((System.Drawing.Image)(resources.GetObject("advanced_ResetToolkit.Image")));
            this.advanced_ResetToolkit.Name = "advanced_ResetToolkit";
            this.advanced_ResetToolkit.Size = new System.Drawing.Size(190, 22);
            this.advanced_ResetToolkit.Text = "Reset Sonic \'06 Toolkit";
            this.advanced_ResetToolkit.Click += new System.EventHandler(this.Advanced_ResetToolkit_Click);
            // 
            // advanced_ResetLog
            // 
            this.advanced_ResetLog.Image = ((System.Drawing.Image)(resources.GetObject("advanced_ResetLog.Image")));
            this.advanced_ResetLog.Name = "advanced_ResetLog";
            this.advanced_ResetLog.Size = new System.Drawing.Size(190, 22);
            this.advanced_ResetLog.Text = "Reset Session Log";
            this.advanced_ResetLog.Click += new System.EventHandler(this.Advanced_ResetLog_Click);
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
            this.sdk_CollisionGenerator,
            this.sdk_ExecutableDecryptor,
            this.sdk_LuaCompilation,
            this.sdk_ModelAnimationExporter,
            this.sdk_PlacementConverter,
            this.sdk_SonicSoundStudio,
            this.sdk_TextureRasteriser,
            this.sdk_TextEditor});
            this.main_SDK.Name = "main_SDK";
            this.main_SDK.Size = new System.Drawing.Size(40, 20);
            this.main_SDK.Text = "SDK";
            // 
            // sdk_ArchiveMerger
            // 
            this.sdk_ArchiveMerger.Image = ((System.Drawing.Image)(resources.GetObject("sdk_ArchiveMerger.Image")));
            this.sdk_ArchiveMerger.Name = "sdk_ArchiveMerger";
            this.sdk_ArchiveMerger.Size = new System.Drawing.Size(301, 22);
            this.sdk_ArchiveMerger.Text = "Archive Merger (ARC)...";
            this.sdk_ArchiveMerger.Click += new System.EventHandler(this.Sdk_ArchiveMerger_Click);
            // 
            // sdk_CollisionGenerator
            // 
            this.sdk_CollisionGenerator.Enabled = false;
            this.sdk_CollisionGenerator.Image = ((System.Drawing.Image)(resources.GetObject("sdk_CollisionGenerator.Image")));
            this.sdk_CollisionGenerator.Name = "sdk_CollisionGenerator";
            this.sdk_CollisionGenerator.Size = new System.Drawing.Size(301, 22);
            this.sdk_CollisionGenerator.Text = "Collision Generator (BIN)...";
            this.sdk_CollisionGenerator.Click += new System.EventHandler(this.Sdk_CollisionGenerator_Click);
            // 
            // sdk_ExecutableDecryptor
            // 
            this.sdk_ExecutableDecryptor.Enabled = false;
            this.sdk_ExecutableDecryptor.Image = ((System.Drawing.Image)(resources.GetObject("sdk_ExecutableDecryptor.Image")));
            this.sdk_ExecutableDecryptor.Name = "sdk_ExecutableDecryptor";
            this.sdk_ExecutableDecryptor.Size = new System.Drawing.Size(301, 22);
            this.sdk_ExecutableDecryptor.Text = "Executable Modification (XEX)...";
            this.sdk_ExecutableDecryptor.Click += new System.EventHandler(this.Sdk_ExecutableDecryptor_Click);
            // 
            // sdk_LuaCompilation
            // 
            this.sdk_LuaCompilation.Enabled = false;
            this.sdk_LuaCompilation.Image = ((System.Drawing.Image)(resources.GetObject("sdk_LuaCompilation.Image")));
            this.sdk_LuaCompilation.Name = "sdk_LuaCompilation";
            this.sdk_LuaCompilation.Size = new System.Drawing.Size(301, 22);
            this.sdk_LuaCompilation.Text = "Lua Compilation (LUB)...";
            this.sdk_LuaCompilation.Click += new System.EventHandler(this.Sdk_LuaCompilation_Click);
            // 
            // sdk_ModelAnimationExporter
            // 
            this.sdk_ModelAnimationExporter.Enabled = false;
            this.sdk_ModelAnimationExporter.Image = ((System.Drawing.Image)(resources.GetObject("sdk_ModelAnimationExporter.Image")));
            this.sdk_ModelAnimationExporter.Name = "sdk_ModelAnimationExporter";
            this.sdk_ModelAnimationExporter.Size = new System.Drawing.Size(301, 22);
            this.sdk_ModelAnimationExporter.Text = "Model/Animation Exporter (XNO/XNM)...";
            this.sdk_ModelAnimationExporter.Click += new System.EventHandler(this.Sdk_ModelAnimationExporter_Click);
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
            // sdk_SonicSoundStudio
            // 
            this.sdk_SonicSoundStudio.Enabled = false;
            this.sdk_SonicSoundStudio.Image = ((System.Drawing.Image)(resources.GetObject("sdk_SonicSoundStudio.Image")));
            this.sdk_SonicSoundStudio.Name = "sdk_SonicSoundStudio";
            this.sdk_SonicSoundStudio.Size = new System.Drawing.Size(301, 22);
            this.sdk_SonicSoundStudio.Text = "Sonic Sound Studio (ADX/AT3/CSB/XMA)...";
            this.sdk_SonicSoundStudio.Click += new System.EventHandler(this.Sdk_SonicSoundStudio_Click);
            // 
            // sdk_TextureRasteriser
            // 
            this.sdk_TextureRasteriser.Enabled = false;
            this.sdk_TextureRasteriser.Image = ((System.Drawing.Image)(resources.GetObject("sdk_TextureRasteriser.Image")));
            this.sdk_TextureRasteriser.Name = "sdk_TextureRasteriser";
            this.sdk_TextureRasteriser.Size = new System.Drawing.Size(301, 22);
            this.sdk_TextureRasteriser.Text = "Texture Converter (DDS)...";
            this.sdk_TextureRasteriser.Click += new System.EventHandler(this.Sdk_TextureRasteriser_Click);
            // 
            // sdk_TextEditor
            // 
            this.sdk_TextEditor.Enabled = false;
            this.sdk_TextEditor.Image = ((System.Drawing.Image)(resources.GetObject("sdk_TextEditor.Image")));
            this.sdk_TextEditor.Name = "sdk_TextEditor";
            this.sdk_TextEditor.Size = new System.Drawing.Size(301, 22);
            this.sdk_TextEditor.Text = "Text Encoding (MST)...";
            this.sdk_TextEditor.Click += new System.EventHandler(this.Sdk_TextEditor_Click);
            // 
            // main_Shortcuts
            // 
            this.main_Shortcuts.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcuts_UnpackARC,
            this.shortcuts_DecodeBIN,
            this.shortcuts_DecryptXEX,
            this.shortcuts_DecompileLUB,
            this.shortcuts_DecodeXNO,
            this.shortcuts_DecodeSET,
            this.shortcuts_DecodeADX,
            this.shortcuts_DecodeAT3,
            this.shortcuts_UnpackCSB,
            this.shortcuts_DecodeXMA,
            this.shortcuts_ConvertDDS,
            this.shortcuts_DecodeMST});
            this.main_Shortcuts.Name = "main_Shortcuts";
            this.main_Shortcuts.Size = new System.Drawing.Size(69, 20);
            this.main_Shortcuts.Text = "Shortcuts";
            // 
            // shortcuts_UnpackARC
            // 
            this.shortcuts_UnpackARC.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcuts_UnpackARCtoDirectory,
            this.shortcuts_UnpackARCtoToolkit});
            this.shortcuts_UnpackARC.Enabled = false;
            this.shortcuts_UnpackARC.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_UnpackARC.Image")));
            this.shortcuts_UnpackARC.Name = "shortcuts_UnpackARC";
            this.shortcuts_UnpackARC.Size = new System.Drawing.Size(227, 22);
            this.shortcuts_UnpackARC.Text = "U8 Archive (ARC)...";
            // 
            // shortcuts_UnpackARCtoDirectory
            // 
            this.shortcuts_UnpackARCtoDirectory.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcuts_UnpackARCforModManager});
            this.shortcuts_UnpackARCtoDirectory.Name = "shortcuts_UnpackARCtoDirectory";
            this.shortcuts_UnpackARCtoDirectory.Size = new System.Drawing.Size(253, 22);
            this.shortcuts_UnpackARCtoDirectory.Text = "Unpack all in the current directory";
            this.shortcuts_UnpackARCtoDirectory.Click += new System.EventHandler(this.Shortcuts_UnpackARCtoDirectory_Click);
            // 
            // shortcuts_UnpackARCforModManager
            // 
            this.shortcuts_UnpackARCforModManager.Name = "shortcuts_UnpackARCforModManager";
            this.shortcuts_UnpackARCforModManager.Size = new System.Drawing.Size(254, 22);
            this.shortcuts_UnpackARCforModManager.Text = "Unpack for Mod Manager support";
            this.shortcuts_UnpackARCforModManager.Click += new System.EventHandler(this.Shortcuts_UnpackARCforModManager_Click);
            // 
            // shortcuts_UnpackARCtoToolkit
            // 
            this.shortcuts_UnpackARCtoToolkit.Name = "shortcuts_UnpackARCtoToolkit";
            this.shortcuts_UnpackARCtoToolkit.Size = new System.Drawing.Size(253, 22);
            this.shortcuts_UnpackARCtoToolkit.Text = "Unpack all to Sonic \'06 Toolkit";
            this.shortcuts_UnpackARCtoToolkit.Click += new System.EventHandler(this.Shortcuts_UnpackARCtoToolkit_Click);
            // 
            // shortcuts_DecodeBIN
            // 
            this.shortcuts_DecodeBIN.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcuts_DecodeBINinDirectory,
            this.shortcuts_DecodeBINinSubdirectories});
            this.shortcuts_DecodeBIN.Enabled = false;
            this.shortcuts_DecodeBIN.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_DecodeBIN.Image")));
            this.shortcuts_DecodeBIN.Name = "shortcuts_DecodeBIN";
            this.shortcuts_DecodeBIN.Size = new System.Drawing.Size(227, 22);
            this.shortcuts_DecodeBIN.Text = "Collision (BIN)...";
            // 
            // shortcuts_DecodeBINinDirectory
            // 
            this.shortcuts_DecodeBINinDirectory.Name = "shortcuts_DecodeBINinDirectory";
            this.shortcuts_DecodeBINinDirectory.Size = new System.Drawing.Size(267, 22);
            this.shortcuts_DecodeBINinDirectory.Text = "Decode all in the current directory";
            this.shortcuts_DecodeBINinDirectory.Click += new System.EventHandler(this.Shortcuts_DecodeBINinDirectory_Click);
            // 
            // shortcuts_DecodeBINinSubdirectories
            // 
            this.shortcuts_DecodeBINinSubdirectories.Name = "shortcuts_DecodeBINinSubdirectories";
            this.shortcuts_DecodeBINinSubdirectories.Size = new System.Drawing.Size(267, 22);
            this.shortcuts_DecodeBINinSubdirectories.Text = "Decode all in the local subdirectories";
            this.shortcuts_DecodeBINinSubdirectories.Click += new System.EventHandler(this.Shortcuts_DecodeBINinSubdirectories_Click);
            // 
            // shortcuts_DecryptXEX
            // 
            this.shortcuts_DecryptXEX.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcuts_DecryptXEXinDirectory,
            this.shortcuts_DecryptXEXinSubdirectories});
            this.shortcuts_DecryptXEX.Enabled = false;
            this.shortcuts_DecryptXEX.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_DecryptXEX.Image")));
            this.shortcuts_DecryptXEX.Name = "shortcuts_DecryptXEX";
            this.shortcuts_DecryptXEX.Size = new System.Drawing.Size(227, 22);
            this.shortcuts_DecryptXEX.Text = "Xbox Executable (XEX)...";
            // 
            // shortcuts_DecryptXEXinDirectory
            // 
            this.shortcuts_DecryptXEXinDirectory.Name = "shortcuts_DecryptXEXinDirectory";
            this.shortcuts_DecryptXEXinDirectory.Size = new System.Drawing.Size(268, 22);
            this.shortcuts_DecryptXEXinDirectory.Text = "Decrypt all in the current directory";
            this.shortcuts_DecryptXEXinDirectory.Click += new System.EventHandler(this.Shortcuts_DecryptXEXinDirectory_Click);
            // 
            // shortcuts_DecryptXEXinSubdirectories
            // 
            this.shortcuts_DecryptXEXinSubdirectories.Name = "shortcuts_DecryptXEXinSubdirectories";
            this.shortcuts_DecryptXEXinSubdirectories.Size = new System.Drawing.Size(268, 22);
            this.shortcuts_DecryptXEXinSubdirectories.Text = "Decrypt all in the local subdirectories";
            this.shortcuts_DecryptXEXinSubdirectories.Click += new System.EventHandler(this.Shortcuts_DecryptXEXinSubdirectories_Click);
            // 
            // shortcuts_DecompileLUB
            // 
            this.shortcuts_DecompileLUB.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcuts_DecompileLUBinDirectory,
            this.shortcuts_DecompileLUBinSubdirectories});
            this.shortcuts_DecompileLUB.Enabled = false;
            this.shortcuts_DecompileLUB.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_DecompileLUB.Image")));
            this.shortcuts_DecompileLUB.Name = "shortcuts_DecompileLUB";
            this.shortcuts_DecompileLUB.Size = new System.Drawing.Size(227, 22);
            this.shortcuts_DecompileLUB.Text = "Lua Binary (LUB)...";
            // 
            // shortcuts_DecompileLUBinDirectory
            // 
            this.shortcuts_DecompileLUBinDirectory.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcuts_DecompileMissionLUBinDirectory});
            this.shortcuts_DecompileLUBinDirectory.Name = "shortcuts_DecompileLUBinDirectory";
            this.shortcuts_DecompileLUBinDirectory.Size = new System.Drawing.Size(284, 22);
            this.shortcuts_DecompileLUBinDirectory.Text = "Decompile all in the current directory";
            this.shortcuts_DecompileLUBinDirectory.Click += new System.EventHandler(this.Shortcuts_DecompileLUBinDirectory_Click);
            // 
            // shortcuts_DecompileMissionLUBinDirectory
            // 
            this.shortcuts_DecompileMissionLUBinDirectory.Name = "shortcuts_DecompileMissionLUBinDirectory";
            this.shortcuts_DecompileMissionLUBinDirectory.Size = new System.Drawing.Size(216, 22);
            this.shortcuts_DecompileMissionLUBinDirectory.Text = "Mission Lua binaries only...";
            this.shortcuts_DecompileMissionLUBinDirectory.Click += new System.EventHandler(this.Shortcuts_DecompileMissionLUBinDirectory_Click);
            // 
            // shortcuts_DecompileLUBinSubdirectories
            // 
            this.shortcuts_DecompileLUBinSubdirectories.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcuts_DecompileMissionLUBinSubdirectories});
            this.shortcuts_DecompileLUBinSubdirectories.Name = "shortcuts_DecompileLUBinSubdirectories";
            this.shortcuts_DecompileLUBinSubdirectories.Size = new System.Drawing.Size(284, 22);
            this.shortcuts_DecompileLUBinSubdirectories.Text = "Decompile all in the local subdirectories";
            this.shortcuts_DecompileLUBinSubdirectories.Click += new System.EventHandler(this.Shortcuts_DecompileLUBinSubdirectories_Click);
            // 
            // shortcuts_DecompileMissionLUBinSubdirectories
            // 
            this.shortcuts_DecompileMissionLUBinSubdirectories.Name = "shortcuts_DecompileMissionLUBinSubdirectories";
            this.shortcuts_DecompileMissionLUBinSubdirectories.Size = new System.Drawing.Size(216, 22);
            this.shortcuts_DecompileMissionLUBinSubdirectories.Text = "Mission Lua binaries only...";
            this.shortcuts_DecompileMissionLUBinSubdirectories.Click += new System.EventHandler(this.shortcuts_DecompileMissionLUBinSubdirectories_Click);
            // 
            // shortcuts_DecodeXNO
            // 
            this.shortcuts_DecodeXNO.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcuts_DecodeXNOinDirectory,
            this.shortcuts_DecodeXNOinSubdirectories});
            this.shortcuts_DecodeXNO.Enabled = false;
            this.shortcuts_DecodeXNO.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_DecodeXNO.Image")));
            this.shortcuts_DecodeXNO.Name = "shortcuts_DecodeXNO";
            this.shortcuts_DecodeXNO.Size = new System.Drawing.Size(227, 22);
            this.shortcuts_DecodeXNO.Text = "SEGA NN Object (XNO)...";
            // 
            // shortcuts_DecodeXNOinDirectory
            // 
            this.shortcuts_DecodeXNOinDirectory.Name = "shortcuts_DecodeXNOinDirectory";
            this.shortcuts_DecodeXNOinDirectory.Size = new System.Drawing.Size(267, 22);
            this.shortcuts_DecodeXNOinDirectory.Text = "Decode all in the current directory";
            this.shortcuts_DecodeXNOinDirectory.Click += new System.EventHandler(this.Shortcuts_DecodeXNOinDirectory_Click);
            // 
            // shortcuts_DecodeXNOinSubdirectories
            // 
            this.shortcuts_DecodeXNOinSubdirectories.Name = "shortcuts_DecodeXNOinSubdirectories";
            this.shortcuts_DecodeXNOinSubdirectories.Size = new System.Drawing.Size(267, 22);
            this.shortcuts_DecodeXNOinSubdirectories.Text = "Decode all in the local subdirectories";
            this.shortcuts_DecodeXNOinSubdirectories.Click += new System.EventHandler(this.shortcuts_DecodeXNOinSubdirectories_Click);
            // 
            // shortcuts_DecodeSET
            // 
            this.shortcuts_DecodeSET.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcuts_DecodeSETinDirectory,
            this.shortcuts_DecodeSETinSubdirectories});
            this.shortcuts_DecodeSET.Enabled = false;
            this.shortcuts_DecodeSET.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_DecodeSET.Image")));
            this.shortcuts_DecodeSET.Name = "shortcuts_DecodeSET";
            this.shortcuts_DecodeSET.Size = new System.Drawing.Size(227, 22);
            this.shortcuts_DecodeSET.Text = "Placement (SET)...";
            // 
            // shortcuts_DecodeSETinDirectory
            // 
            this.shortcuts_DecodeSETinDirectory.Name = "shortcuts_DecodeSETinDirectory";
            this.shortcuts_DecodeSETinDirectory.Size = new System.Drawing.Size(267, 22);
            this.shortcuts_DecodeSETinDirectory.Text = "Decode all in the current directory";
            this.shortcuts_DecodeSETinDirectory.Click += new System.EventHandler(this.Shortcuts_DecodeSETinDirectory_Click);
            // 
            // shortcuts_DecodeSETinSubdirectories
            // 
            this.shortcuts_DecodeSETinSubdirectories.Name = "shortcuts_DecodeSETinSubdirectories";
            this.shortcuts_DecodeSETinSubdirectories.Size = new System.Drawing.Size(267, 22);
            this.shortcuts_DecodeSETinSubdirectories.Text = "Decode all in the local subdirectories";
            this.shortcuts_DecodeSETinSubdirectories.Click += new System.EventHandler(this.Shortcuts_DecodeSETinSubdirectories_Click);
            // 
            // shortcuts_DecodeADX
            // 
            this.shortcuts_DecodeADX.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcuts_DecodeADXinDirectory,
            this.shortcuts_DecodeADXinSubdirectories});
            this.shortcuts_DecodeADX.Enabled = false;
            this.shortcuts_DecodeADX.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_DecodeADX.Image")));
            this.shortcuts_DecodeADX.Name = "shortcuts_DecodeADX";
            this.shortcuts_DecodeADX.Size = new System.Drawing.Size(227, 22);
            this.shortcuts_DecodeADX.Text = "CriWare ADX (ADX)...";
            // 
            // shortcuts_DecodeADXinDirectory
            // 
            this.shortcuts_DecodeADXinDirectory.Name = "shortcuts_DecodeADXinDirectory";
            this.shortcuts_DecodeADXinDirectory.Size = new System.Drawing.Size(267, 22);
            this.shortcuts_DecodeADXinDirectory.Text = "Decode all in the current directory";
            this.shortcuts_DecodeADXinDirectory.Click += new System.EventHandler(this.Shortcuts_DecodeADXinDirectory_Click);
            // 
            // shortcuts_DecodeADXinSubdirectories
            // 
            this.shortcuts_DecodeADXinSubdirectories.Name = "shortcuts_DecodeADXinSubdirectories";
            this.shortcuts_DecodeADXinSubdirectories.Size = new System.Drawing.Size(267, 22);
            this.shortcuts_DecodeADXinSubdirectories.Text = "Decode all in the local subdirectories";
            this.shortcuts_DecodeADXinSubdirectories.Click += new System.EventHandler(this.Shortcuts_DecodeADXinSubdirectories_Click);
            // 
            // shortcuts_DecodeAT3
            // 
            this.shortcuts_DecodeAT3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcuts_DecodeAT3inDirectory,
            this.shortcuts_DecodeAT3inSubdirectories});
            this.shortcuts_DecodeAT3.Enabled = false;
            this.shortcuts_DecodeAT3.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_DecodeAT3.Image")));
            this.shortcuts_DecodeAT3.Name = "shortcuts_DecodeAT3";
            this.shortcuts_DecodeAT3.Size = new System.Drawing.Size(227, 22);
            this.shortcuts_DecodeAT3.Text = "ATRAC3plus (AT3)...";
            // 
            // shortcuts_DecodeAT3inDirectory
            // 
            this.shortcuts_DecodeAT3inDirectory.Name = "shortcuts_DecodeAT3inDirectory";
            this.shortcuts_DecodeAT3inDirectory.Size = new System.Drawing.Size(267, 22);
            this.shortcuts_DecodeAT3inDirectory.Text = "Decode all in the current directory";
            this.shortcuts_DecodeAT3inDirectory.Click += new System.EventHandler(this.Shortcuts_DecodeAT3inDirectory_Click);
            // 
            // shortcuts_DecodeAT3inSubdirectories
            // 
            this.shortcuts_DecodeAT3inSubdirectories.Name = "shortcuts_DecodeAT3inSubdirectories";
            this.shortcuts_DecodeAT3inSubdirectories.Size = new System.Drawing.Size(267, 22);
            this.shortcuts_DecodeAT3inSubdirectories.Text = "Decode all in the local subdirectories";
            this.shortcuts_DecodeAT3inSubdirectories.Click += new System.EventHandler(this.Shortcuts_DecodeAT3inSubdirectories_Click);
            // 
            // shortcuts_UnpackCSB
            // 
            this.shortcuts_UnpackCSB.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcuts_UnpackCSBinDirectory,
            this.shortcuts_UnpackCSBinSubdirectories});
            this.shortcuts_UnpackCSB.Enabled = false;
            this.shortcuts_UnpackCSB.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_UnpackCSB.Image")));
            this.shortcuts_UnpackCSB.Name = "shortcuts_UnpackCSB";
            this.shortcuts_UnpackCSB.Size = new System.Drawing.Size(227, 22);
            this.shortcuts_UnpackCSB.Text = "CriWare Sound Bank (CSB)...";
            // 
            // shortcuts_UnpackCSBinDirectory
            // 
            this.shortcuts_UnpackCSBinDirectory.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcuts_UnpackCSBinDirectoryToADX,
            this.shortcuts_UnpackCSBinDirectoryToWAV});
            this.shortcuts_UnpackCSBinDirectory.Name = "shortcuts_UnpackCSBinDirectory";
            this.shortcuts_UnpackCSBinDirectory.Size = new System.Drawing.Size(267, 22);
            this.shortcuts_UnpackCSBinDirectory.Text = "Unpack all in the current directory";
            this.shortcuts_UnpackCSBinDirectory.Click += new System.EventHandler(this.Shortcuts_UnpackCSBinDirectory_Click);
            // 
            // shortcuts_UnpackCSBinDirectoryToADX
            // 
            this.shortcuts_UnpackCSBinDirectoryToADX.Name = "shortcuts_UnpackCSBinDirectoryToADX";
            this.shortcuts_UnpackCSBinDirectoryToADX.Size = new System.Drawing.Size(156, 22);
            this.shortcuts_UnpackCSBinDirectoryToADX.Text = "Unpack to ADX";
            this.shortcuts_UnpackCSBinDirectoryToADX.Click += new System.EventHandler(this.Shortcuts_UnpackCSBinDirectory_Click);
            // 
            // shortcuts_UnpackCSBinDirectoryToWAV
            // 
            this.shortcuts_UnpackCSBinDirectoryToWAV.Name = "shortcuts_UnpackCSBinDirectoryToWAV";
            this.shortcuts_UnpackCSBinDirectoryToWAV.Size = new System.Drawing.Size(156, 22);
            this.shortcuts_UnpackCSBinDirectoryToWAV.Text = "Unpack to WAV";
            this.shortcuts_UnpackCSBinDirectoryToWAV.Click += new System.EventHandler(this.Shortcuts_UnpackCSBinDirectoryToWAV_Click);
            // 
            // shortcuts_UnpackCSBinSubdirectories
            // 
            this.shortcuts_UnpackCSBinSubdirectories.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcuts_UnpackCSBinSubdirectoriesToADX,
            this.shortcuts_UnpackCSBinSubdirectoriesToWAV});
            this.shortcuts_UnpackCSBinSubdirectories.Name = "shortcuts_UnpackCSBinSubdirectories";
            this.shortcuts_UnpackCSBinSubdirectories.Size = new System.Drawing.Size(267, 22);
            this.shortcuts_UnpackCSBinSubdirectories.Text = "Unpack all in the local subdirectories";
            this.shortcuts_UnpackCSBinSubdirectories.Click += new System.EventHandler(this.Shortcuts_UnpackCSBinSubdirectories_Click);
            // 
            // shortcuts_UnpackCSBinSubdirectoriesToADX
            // 
            this.shortcuts_UnpackCSBinSubdirectoriesToADX.Name = "shortcuts_UnpackCSBinSubdirectoriesToADX";
            this.shortcuts_UnpackCSBinSubdirectoriesToADX.Size = new System.Drawing.Size(156, 22);
            this.shortcuts_UnpackCSBinSubdirectoriesToADX.Text = "Unpack to ADX";
            this.shortcuts_UnpackCSBinSubdirectoriesToADX.Click += new System.EventHandler(this.Shortcuts_UnpackCSBinDirectory_Click);
            // 
            // shortcuts_UnpackCSBinSubdirectoriesToWAV
            // 
            this.shortcuts_UnpackCSBinSubdirectoriesToWAV.Name = "shortcuts_UnpackCSBinSubdirectoriesToWAV";
            this.shortcuts_UnpackCSBinSubdirectoriesToWAV.Size = new System.Drawing.Size(156, 22);
            this.shortcuts_UnpackCSBinSubdirectoriesToWAV.Text = "Unpack to WAV";
            this.shortcuts_UnpackCSBinSubdirectoriesToWAV.Click += new System.EventHandler(this.Shortcuts_UnpackCSBinSubdirectoriesToWAV_Click);
            // 
            // shortcuts_DecodeXMA
            // 
            this.shortcuts_DecodeXMA.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcuts_DecodeXMAinDirectory,
            this.shortcuts_DecodeXMAinSubdirectories});
            this.shortcuts_DecodeXMA.Enabled = false;
            this.shortcuts_DecodeXMA.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_DecodeXMA.Image")));
            this.shortcuts_DecodeXMA.Name = "shortcuts_DecodeXMA";
            this.shortcuts_DecodeXMA.Size = new System.Drawing.Size(227, 22);
            this.shortcuts_DecodeXMA.Text = "Xbox Media Audio (XMA)...";
            // 
            // shortcuts_DecodeXMAinDirectory
            // 
            this.shortcuts_DecodeXMAinDirectory.Name = "shortcuts_DecodeXMAinDirectory";
            this.shortcuts_DecodeXMAinDirectory.Size = new System.Drawing.Size(267, 22);
            this.shortcuts_DecodeXMAinDirectory.Text = "Decode all in the current directory";
            this.shortcuts_DecodeXMAinDirectory.Click += new System.EventHandler(this.Shortcuts_DecodeXMAinDirectory_Click);
            // 
            // shortcuts_DecodeXMAinSubdirectories
            // 
            this.shortcuts_DecodeXMAinSubdirectories.Name = "shortcuts_DecodeXMAinSubdirectories";
            this.shortcuts_DecodeXMAinSubdirectories.Size = new System.Drawing.Size(267, 22);
            this.shortcuts_DecodeXMAinSubdirectories.Text = "Decode all in the local subdirectories";
            this.shortcuts_DecodeXMAinSubdirectories.Click += new System.EventHandler(this.Shortcuts_DecodeXMAinSubdirectories_Click);
            // 
            // shortcuts_ConvertDDS
            // 
            this.shortcuts_ConvertDDS.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcuts_ConvertDDSinDirectory,
            this.shortcuts_ConvertDDSinSubdirectories});
            this.shortcuts_ConvertDDS.Enabled = false;
            this.shortcuts_ConvertDDS.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_ConvertDDS.Image")));
            this.shortcuts_ConvertDDS.Name = "shortcuts_ConvertDDS";
            this.shortcuts_ConvertDDS.Size = new System.Drawing.Size(227, 22);
            this.shortcuts_ConvertDDS.Text = "DirectDraw Surface (DDS)...";
            // 
            // shortcuts_ConvertDDSinDirectory
            // 
            this.shortcuts_ConvertDDSinDirectory.Name = "shortcuts_ConvertDDSinDirectory";
            this.shortcuts_ConvertDDSinDirectory.Size = new System.Drawing.Size(269, 22);
            this.shortcuts_ConvertDDSinDirectory.Text = "Convert all in the current directory";
            this.shortcuts_ConvertDDSinDirectory.Click += new System.EventHandler(this.Shortcuts_ConvertDDSinDirectory_Click);
            // 
            // shortcuts_ConvertDDSinSubdirectories
            // 
            this.shortcuts_ConvertDDSinSubdirectories.Name = "shortcuts_ConvertDDSinSubdirectories";
            this.shortcuts_ConvertDDSinSubdirectories.Size = new System.Drawing.Size(269, 22);
            this.shortcuts_ConvertDDSinSubdirectories.Text = "Convert all in the local subdirectories";
            this.shortcuts_ConvertDDSinSubdirectories.Click += new System.EventHandler(this.Shortcuts_ConvertDDSinSubdirectories_Click);
            // 
            // shortcuts_DecodeMST
            // 
            this.shortcuts_DecodeMST.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shortcuts_DecodeMSTinDirectory,
            this.shortcuts_DecodeMSTinSubdirectories});
            this.shortcuts_DecodeMST.Enabled = false;
            this.shortcuts_DecodeMST.Image = ((System.Drawing.Image)(resources.GetObject("shortcuts_DecodeMST.Image")));
            this.shortcuts_DecodeMST.Name = "shortcuts_DecodeMST";
            this.shortcuts_DecodeMST.Size = new System.Drawing.Size(227, 22);
            this.shortcuts_DecodeMST.Text = "UTF-16 Encoded Text (MST)...";
            // 
            // shortcuts_DecodeMSTinDirectory
            // 
            this.shortcuts_DecodeMSTinDirectory.Name = "shortcuts_DecodeMSTinDirectory";
            this.shortcuts_DecodeMSTinDirectory.Size = new System.Drawing.Size(267, 22);
            this.shortcuts_DecodeMSTinDirectory.Text = "Decode all in the current directory";
            this.shortcuts_DecodeMSTinDirectory.Click += new System.EventHandler(this.Shortcuts_DecodeMSTinDirectory_Click);
            // 
            // shortcuts_DecodeMSTinSubdirectories
            // 
            this.shortcuts_DecodeMSTinSubdirectories.Name = "shortcuts_DecodeMSTinSubdirectories";
            this.shortcuts_DecodeMSTinSubdirectories.Size = new System.Drawing.Size(267, 22);
            this.shortcuts_DecodeMSTinSubdirectories.Text = "Decode all in the local subdirectories";
            this.shortcuts_DecodeMSTinSubdirectories.Click += new System.EventHandler(this.Shortcuts_DecodeMSTinSubdirectories_Click);
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
            this.help_Documentation,
            this.help_Separator1,
            this.help_CheckForUpdates,
            this.help_ReportBug,
            this.help_GitHub,
            this.help_About});
            this.main_Help.Name = "main_Help";
            this.main_Help.Size = new System.Drawing.Size(44, 20);
            this.main_Help.Text = "Help";
            // 
            // help_Documentation
            // 
            this.help_Documentation.Image = ((System.Drawing.Image)(resources.GetObject("help_Documentation.Image")));
            this.help_Documentation.Name = "help_Documentation";
            this.help_Documentation.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.help_Documentation.Size = new System.Drawing.Size(203, 22);
            this.help_Documentation.Text = "Documentation";
            this.help_Documentation.Click += new System.EventHandler(this.Help_Documentation_Click);
            // 
            // help_Separator1
            // 
            this.help_Separator1.Name = "help_Separator1";
            this.help_Separator1.Size = new System.Drawing.Size(200, 6);
            // 
            // help_CheckForUpdates
            // 
            this.help_CheckForUpdates.Image = ((System.Drawing.Image)(resources.GetObject("help_CheckForUpdates.Image")));
            this.help_CheckForUpdates.Name = "help_CheckForUpdates";
            this.help_CheckForUpdates.Size = new System.Drawing.Size(203, 22);
            this.help_CheckForUpdates.Text = "Check for updates...";
            this.help_CheckForUpdates.Click += new System.EventHandler(this.Help_CheckForUpdates_Click);
            // 
            // help_ReportBug
            // 
            this.help_ReportBug.Image = ((System.Drawing.Image)(resources.GetObject("help_ReportBug.Image")));
            this.help_ReportBug.Name = "help_ReportBug";
            this.help_ReportBug.Size = new System.Drawing.Size(203, 22);
            this.help_ReportBug.Text = "Report a bug...";
            this.help_ReportBug.Click += new System.EventHandler(this.Help_ReportBug_Click);
            // 
            // help_GitHub
            // 
            this.help_GitHub.Image = ((System.Drawing.Image)(resources.GetObject("help_GitHub.Image")));
            this.help_GitHub.Name = "help_GitHub";
            this.help_GitHub.Size = new System.Drawing.Size(203, 22);
            this.help_GitHub.Text = "GitHub...";
            this.help_GitHub.Click += new System.EventHandler(this.Help_GitHub_Click);
            // 
            // help_About
            // 
            this.help_About.Image = ((System.Drawing.Image)(resources.GetObject("help_About.Image")));
            this.help_About.Name = "help_About";
            this.help_About.Size = new System.Drawing.Size(203, 22);
            this.help_About.Text = "About";
            this.help_About.Click += new System.EventHandler(this.Help_About_Click);
            // 
            // status_Main
            // 
            this.status_Main.Location = new System.Drawing.Point(0, 428);
            this.status_Main.Name = "status_Main";
            this.status_Main.Size = new System.Drawing.Size(800, 22);
            this.status_Main.TabIndex = 26;
            this.status_Main.Text = "statusStrip1";
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
            // lbl_Status
            // 
            this.lbl_Status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_Status.AutoSize = true;
            this.lbl_Status.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Status.Location = new System.Drawing.Point(2, 432);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(42, 15);
            this.lbl_Status.TabIndex = 30;
            this.lbl_Status.Text = "Ready.";
            this.lbl_Status.Click += new System.EventHandler(this.Lbl_Status_Click);
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
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbl_Status);
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
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.pnl_MenuStrip.ResumeLayout(false);
            this.pnl_MenuStrip.PerformLayout();
            this.mstrip_Main.ResumeLayout(false);
            this.mstrip_Main.PerformLayout();
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
        private System.Windows.Forms.MenuStrip mstrip_Main;
        private System.Windows.Forms.ToolStripMenuItem main_File;
        private System.Windows.Forms.ToolStripMenuItem file_NewARC;
        private System.Windows.Forms.ToolStripSeparator file_Separator1;
        private System.Windows.Forms.ToolStripMenuItem file_OpenARC;
        private System.Windows.Forms.ToolStripMenuItem file_ExtractISO;
        private System.Windows.Forms.ToolStripMenuItem file_OpenFolder;
        private System.Windows.Forms.ToolStripSeparator file_Separator2;
        private System.Windows.Forms.ToolStripMenuItem file_Repack;
        private System.Windows.Forms.ToolStripMenuItem file_RepackAs;
        private System.Windows.Forms.ToolStripSeparator file_Separator3;
        private System.Windows.Forms.ToolStripMenuItem file_Preferences;
        private System.Windows.Forms.ToolStripMenuItem preferences_AssociateARCs;
        private System.Windows.Forms.ToolStripMenuItem preferences_DisableSoftwareUpdater;
        private System.Windows.Forms.ToolStripMenuItem preferences_DisableGameDirectory;
        private System.Windows.Forms.ToolStripSeparator preferences_Separator2;
        private System.Windows.Forms.ToolStripMenuItem preferences_Advanced;
        private System.Windows.Forms.ToolStripMenuItem advanced_ResetToolkit;
        private System.Windows.Forms.ToolStripMenuItem file_Exit;
        private System.Windows.Forms.ToolStripMenuItem main_SDK;
        private System.Windows.Forms.ToolStripMenuItem main_Shortcuts;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeAT3;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeBIN;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_UnpackCSB;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_UnpackCSBinDirectory;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_UnpackCSBinSubdirectories;
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
        private System.Windows.Forms.ToolStripMenuItem help_Documentation;
        private System.Windows.Forms.ToolStripSeparator help_Separator1;
        private System.Windows.Forms.ToolStripMenuItem help_CheckForUpdates;
        private System.Windows.Forms.ToolStripMenuItem help_ReportBug;
        private System.Windows.Forms.ToolStripMenuItem help_About;
        private System.Windows.Forms.ToolStripMenuItem preferences_Paths;
        private System.Windows.Forms.ToolStripMenuItem paths_ClearGameDirectory;
        private System.Windows.Forms.ToolStripSeparator preferences_Separator1;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeADX;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_UnpackARC;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_UnpackARCtoToolkit;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_UnpackARCtoDirectory;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecompileLUBinDirectory;
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
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecompileMissionLUBinDirectory;
        private System.Windows.Forms.Label lbl_Status;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeADXinDirectory;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeADXinSubdirectories;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_ConvertDDSinDirectory;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeSETinDirectory;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeAT3inDirectory;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeAT3inSubdirectories;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeBINinDirectory;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeBINinSubdirectories;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeMSTinDirectory;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecryptXEXinDirectory;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeXMAinDirectory;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeXMAinSubdirectories;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeXNOinDirectory;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_ConvertDDSinSubdirectories;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecompileLUBinSubdirectories;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecompileMissionLUBinSubdirectories;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeMSTinSubdirectories;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeSETinSubdirectories;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecryptXEXinSubdirectories;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_DecodeXNOinSubdirectories;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_UnpackCSBinDirectoryToADX;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_UnpackCSBinDirectoryToWAV;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_UnpackCSBinSubdirectoriesToADX;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_UnpackCSBinSubdirectoriesToWAV;
        private System.Windows.Forms.ToolStripMenuItem help_GitHub;
        private System.Windows.Forms.ToolStripMenuItem advanced_ResetLog;
        private System.Windows.Forms.ToolStripMenuItem shortcuts_UnpackARCforModManager;
        private System.Windows.Forms.ToolStripMenuItem file_RepackAll;
    }
}

