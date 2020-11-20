namespace Marathon.Toolkit.Forms
{
    partial class Workspace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Workspace));
            this.KryptonRibbon_Workspace = new ComponentFactory.Krypton.Ribbon.KryptonRibbon();
            this.KryptonContextMenuItem_File_New = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.KryptonContextMenuItem_File_Open = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.KryptonContextMenuItem_File_Open_Items = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems();
            this.KryptonContextMenuItem_File_Open_Items_OpenFile = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.KryptonContextMenuItem_File_Open_Items_OpenFolder = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.KryptonContextMenuSeparator_File_1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuSeparator();
            this.ButtonSpecAppMenu_AboutMarathon = new ComponentFactory.Krypton.Ribbon.ButtonSpecAppMenu();
            this.ButtonSpecAppMenu_MarathonOptions = new ComponentFactory.Krypton.Ribbon.ButtonSpecAppMenu();
            this.ButtonSpecAppMenu_ExitMarathon = new ComponentFactory.Krypton.Ribbon.ButtonSpecAppMenu();
            this.KryptonRibbonTab_Home = new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab();
            this.KryptonRibbonGroup_Home_Organise = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
            this.KryptonRibbonGroupTriple_Home_Organise_BulkRenamer = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.KryptonRibbonGroupButton_Home_Organise_BulkRenamer = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.KryptonRibbonTab_Developer = new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab();
            this.KryptonRibbonGroup_Developer_Tools = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
            this.KryptonRibbonGroupTriple_Developer_New_Debugger = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.KryptonRibbonGroupButton_Developer_Tools_Debugger = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.KryptonRibbonGroupButton_Developer_Tools_ResetSettings = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.DockPanel_Workspace = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.VS2015DarkTheme_DockPanel_Workspace = new WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme();
            this.KryptonManager_Workspace = new ComponentFactory.Krypton.Toolkit.KryptonManager(this.components);
            this.KryptonPalette_Marathon = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
            this.kryptonContextMenuItem2 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.KryptonRibbon_Workspace)).BeginInit();
            this.SuspendLayout();
            // 
            // KryptonRibbon_Workspace
            // 
            this.KryptonRibbon_Workspace.InDesignHelperMode = true;
            this.KryptonRibbon_Workspace.Name = "KryptonRibbon_Workspace";
            this.KryptonRibbon_Workspace.QATLocation = ComponentFactory.Krypton.Ribbon.QATLocation.Hidden;
            this.KryptonRibbon_Workspace.QATUserChange = false;
            this.KryptonRibbon_Workspace.RibbonAppButton.AppButtonMenuItems.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.KryptonContextMenuItem_File_New,
            this.KryptonContextMenuItem_File_Open,
            this.KryptonContextMenuSeparator_File_1});
            this.KryptonRibbon_Workspace.RibbonAppButton.AppButtonSpecs.AddRange(new ComponentFactory.Krypton.Ribbon.ButtonSpecAppMenu[] {
            this.ButtonSpecAppMenu_AboutMarathon,
            this.ButtonSpecAppMenu_MarathonOptions,
            this.ButtonSpecAppMenu_ExitMarathon});
            this.KryptonRibbon_Workspace.RibbonAppButton.AppButtonToolTipBody = "All your \'06 formats are belong to us.";
            this.KryptonRibbon_Workspace.RibbonAppButton.AppButtonToolTipTitle = "Marathon";
            this.KryptonRibbon_Workspace.RibbonTabs.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab[] {
            this.KryptonRibbonTab_Home,
            this.KryptonRibbonTab_Developer});
            this.KryptonRibbon_Workspace.SelectedTab = this.KryptonRibbonTab_Home;
            this.KryptonRibbon_Workspace.Size = new System.Drawing.Size(1264, 115);
            this.KryptonRibbon_Workspace.TabIndex = 5;
            // 
            // KryptonContextMenuItem_File_New
            // 
            this.KryptonContextMenuItem_File_New.LargeKryptonCommandImage = true;
            this.KryptonContextMenuItem_File_New.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.KryptonContextMenuItem_File_New.Text = "&New";
            this.KryptonContextMenuItem_File_New.Click += new System.EventHandler(this.KryptonContextMenuItem_File_New_Click);
            // 
            // KryptonContextMenuItem_File_Open
            // 
            this.KryptonContextMenuItem_File_Open.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.KryptonContextMenuItem_File_Open_Items});
            this.KryptonContextMenuItem_File_Open.LargeKryptonCommandImage = true;
            this.KryptonContextMenuItem_File_Open.SplitSubMenu = true;
            this.KryptonContextMenuItem_File_Open.Text = "&Open";
            this.KryptonContextMenuItem_File_Open.Click += new System.EventHandler(this.KryptonContextMenuItem_File_Open_Items_OpenFile_Click);
            // 
            // KryptonContextMenuItem_File_Open_Items
            // 
            this.KryptonContextMenuItem_File_Open_Items.Items.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.KryptonContextMenuItem_File_Open_Items_OpenFile,
            this.KryptonContextMenuItem_File_Open_Items_OpenFolder});
            // 
            // KryptonContextMenuItem_File_Open_Items_OpenFile
            // 
            this.KryptonContextMenuItem_File_Open_Items_OpenFile.Text = "Open File";
            this.KryptonContextMenuItem_File_Open_Items_OpenFile.Click += new System.EventHandler(this.KryptonContextMenuItem_File_Open_Items_OpenFile_Click);
            // 
            // KryptonContextMenuItem_File_Open_Items_OpenFolder
            // 
            this.KryptonContextMenuItem_File_Open_Items_OpenFolder.Text = "Open Folder";
            this.KryptonContextMenuItem_File_Open_Items_OpenFolder.Click += new System.EventHandler(this.KryptonContextMenuItem_File_Open_Items_OpenFolder_Click);
            // 
            // ButtonSpecAppMenu_AboutMarathon
            // 
            this.ButtonSpecAppMenu_AboutMarathon.Edge = ComponentFactory.Krypton.Toolkit.PaletteRelativeEdgeAlign.Near;
            this.ButtonSpecAppMenu_AboutMarathon.Image = global::Marathon.Toolkit.Properties.Resources.Task_Strip_Information;
            this.ButtonSpecAppMenu_AboutMarathon.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Standalone;
            this.ButtonSpecAppMenu_AboutMarathon.Text = "About Marathon";
            this.ButtonSpecAppMenu_AboutMarathon.UniqueName = "c6f6a7f45c694cc4a173e72b19bd03b0";
            this.ButtonSpecAppMenu_AboutMarathon.Click += new System.EventHandler(this.ButtonSpecAppMenu_AboutMarathon_Click);
            // 
            // ButtonSpecAppMenu_MarathonOptions
            // 
            this.ButtonSpecAppMenu_MarathonOptions.Image = global::Marathon.Toolkit.Properties.Resources.Task_Strip_Settings;
            this.ButtonSpecAppMenu_MarathonOptions.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Standalone;
            this.ButtonSpecAppMenu_MarathonOptions.Text = "Marathon Options";
            this.ButtonSpecAppMenu_MarathonOptions.UniqueName = "1caf061d845e4bc7ac3722e8fcce01d9";
            this.ButtonSpecAppMenu_MarathonOptions.Click += new System.EventHandler(this.ButtonSpecAppMenu_MarathonOptions_Click);
            // 
            // ButtonSpecAppMenu_ExitMarathon
            // 
            this.ButtonSpecAppMenu_ExitMarathon.Image = global::Marathon.Toolkit.Properties.Resources.Task_Strip_Exit;
            this.ButtonSpecAppMenu_ExitMarathon.Style = ComponentFactory.Krypton.Toolkit.PaletteButtonStyle.Standalone;
            this.ButtonSpecAppMenu_ExitMarathon.Text = "Exit Marathon";
            this.ButtonSpecAppMenu_ExitMarathon.UniqueName = "1bdc55c5b4784595b5956e1fb9ad2c74";
            this.ButtonSpecAppMenu_ExitMarathon.Click += new System.EventHandler(this.ButtonSpecAppMenu_ExitMarathon_Click);
            // 
            // KryptonRibbonTab_Home
            // 
            this.KryptonRibbonTab_Home.Groups.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup[] {
            this.KryptonRibbonGroup_Home_Organise});
            this.KryptonRibbonTab_Home.KeyTip = "H";
            this.KryptonRibbonTab_Home.Text = "Home";
            // 
            // KryptonRibbonGroup_Home_Organise
            // 
            this.KryptonRibbonGroup_Home_Organise.DialogBoxLauncher = false;
            this.KryptonRibbonGroup_Home_Organise.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.KryptonRibbonGroupTriple_Home_Organise_BulkRenamer});
            this.KryptonRibbonGroup_Home_Organise.TextLine1 = "Organise";
            // 
            // KryptonRibbonGroupTriple_Home_Organise_BulkRenamer
            // 
            this.KryptonRibbonGroupTriple_Home_Organise_BulkRenamer.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.KryptonRibbonGroupButton_Home_Organise_BulkRenamer});
            // 
            // KryptonRibbonGroupButton_Home_Organise_BulkRenamer
            // 
            this.KryptonRibbonGroupButton_Home_Organise_BulkRenamer.ImageLarge = global::Marathon.Toolkit.Properties.Resources.Task_Ribbon_Rename;
            this.KryptonRibbonGroupButton_Home_Organise_BulkRenamer.KeyTip = "F2";
            this.KryptonRibbonGroupButton_Home_Organise_BulkRenamer.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.KryptonRibbonGroupButton_Home_Organise_BulkRenamer.TextLine1 = "Bulk";
            this.KryptonRibbonGroupButton_Home_Organise_BulkRenamer.TextLine2 = "Renamer";
            this.KryptonRibbonGroupButton_Home_Organise_BulkRenamer.Click += new System.EventHandler(this.KryptonRibbonGroupButton_Home_Organise_BulkRenamer_Click);
            // 
            // KryptonRibbonTab_Developer
            // 
            this.KryptonRibbonTab_Developer.Groups.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup[] {
            this.KryptonRibbonGroup_Developer_Tools});
            this.KryptonRibbonTab_Developer.KeyTip = "L";
            this.KryptonRibbonTab_Developer.Text = "Developer";
            this.KryptonRibbonTab_Developer.Visible = false;
            // 
            // KryptonRibbonGroup_Developer_Tools
            // 
            this.KryptonRibbonGroup_Developer_Tools.DialogBoxLauncher = false;
            this.KryptonRibbonGroup_Developer_Tools.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.KryptonRibbonGroupTriple_Developer_New_Debugger});
            this.KryptonRibbonGroup_Developer_Tools.TextLine1 = "Tools";
            // 
            // KryptonRibbonGroupTriple_Developer_New_Debugger
            // 
            this.KryptonRibbonGroupTriple_Developer_New_Debugger.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.KryptonRibbonGroupButton_Developer_Tools_Debugger,
            this.KryptonRibbonGroupButton_Developer_Tools_ResetSettings});
            // 
            // KryptonRibbonGroupButton_Developer_Tools_Debugger
            // 
            this.KryptonRibbonGroupButton_Developer_Tools_Debugger.ImageLarge = global::Marathon.Toolkit.Properties.Resources.Logo_Small_Dark;
            this.KryptonRibbonGroupButton_Developer_Tools_Debugger.KeyTip = "D";
            this.KryptonRibbonGroupButton_Developer_Tools_Debugger.TextLine1 = "Debugger";
            this.KryptonRibbonGroupButton_Developer_Tools_Debugger.Click += new System.EventHandler(this.KryptonRibbonGroup_Developer_Tools_Click_Group);
            // 
            // KryptonRibbonGroupButton_Developer_Tools_ResetSettings
            // 
            this.KryptonRibbonGroupButton_Developer_Tools_ResetSettings.ImageLarge = global::Marathon.Toolkit.Properties.Resources.Feedback_Warning;
            this.KryptonRibbonGroupButton_Developer_Tools_ResetSettings.TextLine1 = "Reset";
            this.KryptonRibbonGroupButton_Developer_Tools_ResetSettings.TextLine2 = "settings";
            this.KryptonRibbonGroupButton_Developer_Tools_ResetSettings.Click += new System.EventHandler(this.KryptonRibbonGroup_Developer_Tools_Click_Group);
            // 
            // DockPanel_Workspace
            // 
            this.DockPanel_Workspace.AllowDrop = true;
            this.DockPanel_Workspace.DefaultFloatWindowSize = new System.Drawing.Size(1280, 720);
            this.DockPanel_Workspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DockPanel_Workspace.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.DockPanel_Workspace.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.DockPanel_Workspace.Location = new System.Drawing.Point(0, 115);
            this.DockPanel_Workspace.Name = "DockPanel_Workspace";
            this.DockPanel_Workspace.Padding = new System.Windows.Forms.Padding(6);
            this.DockPanel_Workspace.ShowAutoHideContentOnHover = false;
            this.DockPanel_Workspace.Size = new System.Drawing.Size(1264, 566);
            this.DockPanel_Workspace.SupportDeeplyNestedContent = true;
            this.DockPanel_Workspace.TabIndex = 6;
            this.DockPanel_Workspace.Theme = this.VS2015DarkTheme_DockPanel_Workspace;
            this.DockPanel_Workspace.ContentRemoved += new System.EventHandler<WeifenLuo.WinFormsUI.Docking.DockContentEventArgs>(this.DockPanel_Workspace_ContentRemoved);
            // 
            // KryptonManager_Workspace
            // 
            this.KryptonManager_Workspace.GlobalPalette = this.KryptonPalette_Marathon;
            this.KryptonManager_Workspace.GlobalPaletteMode = ComponentFactory.Krypton.Toolkit.PaletteModeManager.Custom;
            // 
            // KryptonPalette_Marathon
            // 
            this.KryptonPalette_Marathon.BasePaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Office365Black;
            this.KryptonPalette_Marathon.ButtonStyles.ButtonCommon.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(108)))), ((int)(((byte)(108)))));
            this.KryptonPalette_Marathon.ButtonStyles.ButtonCommon.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(108)))), ((int)(((byte)(108)))));
            this.KryptonPalette_Marathon.ButtonStyles.ButtonCommon.StatePressed.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.SolidInside;
            this.KryptonPalette_Marathon.ButtonStyles.ButtonCommon.StatePressed.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.KryptonPalette_Marathon.ButtonStyles.ButtonCommon.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.KryptonPalette_Marathon.ButtonStyles.ButtonCommon.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.KryptonPalette_Marathon.ButtonStyles.ButtonCommon.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.KryptonPalette_Marathon.ButtonStyles.ButtonCommon.StateTracking.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.SolidInside;
            this.KryptonPalette_Marathon.ButtonStyles.ButtonCommon.StateTracking.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.KryptonPalette_Marathon.ButtonStyles.ButtonCommon.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.KryptonPalette_Marathon.ContextMenu.StateChecked.ItemImage.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(73)))), ((int)(((byte)(73)))));
            this.KryptonPalette_Marathon.ContextMenu.StateChecked.ItemImage.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(73)))), ((int)(((byte)(73)))));
            this.KryptonPalette_Marathon.ContextMenu.StateChecked.ItemImage.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(119)))));
            this.KryptonPalette_Marathon.ContextMenu.StateChecked.ItemImage.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(119)))));
            this.KryptonPalette_Marathon.ContextMenu.StateChecked.ItemImage.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.KryptonPalette_Marathon.ContextMenu.StateChecked.ItemImage.Border.Rounding = 0;
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.ControlInner.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.ControlInner.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.ControlOuter.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.ControlOuter.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.ControlOuter.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.Heading.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.Heading.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.Heading.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.Heading.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.Heading.Border.DrawBorders = ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom;
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.Heading.Content.LongText.Color1 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.Heading.Content.LongText.Color2 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.Heading.Content.ShortText.Color1 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.Heading.Content.ShortText.Color2 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.ItemHighlight.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(95)))), ((int)(((byte)(95)))));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.ItemHighlight.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(95)))), ((int)(((byte)(95)))));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.ItemHighlight.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.SolidInside;
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.ItemHighlight.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.ItemHighlight.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.ItemHighlight.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.ItemImageColumn.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.ItemImageColumn.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.ItemImageColumn.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.ItemImageColumn.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.ItemImageColumn.Border.DrawBorders = ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right;
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.ItemSplit.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.ItemSplit.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.ItemSplit.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.ItemSplit.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.Separator.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.Separator.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.Separator.Back.ColorStyle = ComponentFactory.Krypton.Toolkit.PaletteColorStyle.SolidTopLine;
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.Separator.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.KryptonPalette_Marathon.ContextMenu.StateCommon.Separator.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.KryptonPalette_Marathon.ContextMenu.StateNormal.ItemShortcutText.ShortText.Color1 = System.Drawing.SystemColors.ControlDark;
            this.KryptonPalette_Marathon.ContextMenu.StateNormal.ItemShortcutText.ShortText.Color2 = System.Drawing.SystemColors.ControlDark;
            this.KryptonPalette_Marathon.ContextMenu.StateNormal.ItemTextAlternate.LongText.Color1 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.ContextMenu.StateNormal.ItemTextAlternate.LongText.Color2 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.ContextMenu.StateNormal.ItemTextAlternate.ShortText.Color1 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.ContextMenu.StateNormal.ItemTextAlternate.ShortText.Color2 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.ContextMenu.StateNormal.ItemTextStandard.LongText.Color1 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.ContextMenu.StateNormal.ItemTextStandard.LongText.Color2 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.ContextMenu.StateNormal.ItemTextStandard.ShortText.Color1 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.ContextMenu.StateNormal.ItemTextStandard.ShortText.Color2 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.ControlStyles.ControlRibbonAppMenu.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.KryptonPalette_Marathon.ControlStyles.ControlRibbonAppMenu.StateCommon.Border.Rounding = 0;
            this.KryptonPalette_Marathon.ControlStyles.ControlToolTip.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.KryptonPalette_Marathon.ControlStyles.ControlToolTip.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.KryptonPalette_Marathon.ControlStyles.ControlToolTip.StateNormal.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(118)))), ((int)(((byte)(118)))));
            this.KryptonPalette_Marathon.ControlStyles.ControlToolTip.StateNormal.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(118)))), ((int)(((byte)(118)))));
            this.KryptonPalette_Marathon.ControlStyles.ControlToolTip.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.KryptonPalette_Marathon.ControlStyles.ControlToolTip.StateNormal.Border.Rounding = 0;
            this.KryptonPalette_Marathon.CustomisedKryptonPaletteFilePath = null;
            this.KryptonPalette_Marathon.Images.ContextMenu.Checked = global::Marathon.Toolkit.Properties.Resources.Task_Checked;
            this.KryptonPalette_Marathon.LabelStyles.LabelKeyTip.StateCommon.LongText.Color1 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.LabelStyles.LabelKeyTip.StateCommon.LongText.Color2 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.LabelStyles.LabelKeyTip.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.LabelStyles.LabelKeyTip.StateCommon.ShortText.Color2 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.LabelStyles.LabelSuperTip.StateCommon.LongText.Color1 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.LabelStyles.LabelSuperTip.StateCommon.LongText.Color2 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.LabelStyles.LabelSuperTip.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.LabelStyles.LabelSuperTip.StateCommon.ShortText.Color2 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.LabelStyles.LabelToolTip.StateCommon.LongText.Color1 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.LabelStyles.LabelToolTip.StateCommon.LongText.Color2 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.LabelStyles.LabelToolTip.StateCommon.ShortText.Color1 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.LabelStyles.LabelToolTip.StateCommon.ShortText.Color2 = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.Ribbon.RibbonAppMenuDocs.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonAppMenuDocs.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonAppMenuDocs.BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonAppMenuDocs.BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonAppMenuDocs.BackColor5 = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonAppMenuDocsEntry.TextColor = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.Ribbon.RibbonAppMenuDocsTitle.TextColor = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.Ribbon.RibbonGeneral.GroupSeparatorDark = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonGeneral.GroupSeparatorLight = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonGeneral.MinimizeBarDarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonGeneral.MinimizeBarLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonGeneral.QATButtonDarkColor = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.Ribbon.RibbonGeneral.QATButtonLightColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonGroupArea.StateCommon.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonGroupArea.StateCommon.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonGroupArea.StateCommon.BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonGroupArea.StateCommon.BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonGroupArea.StateCommon.BackColor5 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonGroupButtonText.StateDisabled.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonGroupButtonText.StateNormal.TextColor = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.Ribbon.RibbonGroupNormalBorder.StateCommon.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonGroupNormalBorder.StateCommon.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonGroupNormalBorder.StateCommon.BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonGroupNormalBorder.StateCommon.BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonGroupNormalBorder.StateCommon.BackColor5 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonGroupNormalBorder.StateTracking.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonGroupNormalBorder.StateTracking.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonGroupNormalBorder.StateTracking.BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonGroupNormalBorder.StateTracking.BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonGroupNormalBorder.StateTracking.BackColor5 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonGroupNormalTitle.StateCommon.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(198)))), ((int)(((byte)(198)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonQATFullbar.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonQATFullbar.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonQATFullbar.BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonQATFullbar.BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonQATFullbar.BackColor5 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonTab.OverrideFocus.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(162)))), ((int)(((byte)(111)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonTab.OverrideFocus.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(162)))), ((int)(((byte)(111)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonTab.OverrideFocus.BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonTab.OverrideFocus.BackColor4 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonTab.OverrideFocus.BackColor5 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonTab.OverrideFocus.TextColor = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.Ribbon.RibbonTab.StateCommon.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonTab.StateCommon.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonTab.StateCommon.BackColor3 = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonTab.StateCommon.TextColor = System.Drawing.Color.White;
            this.KryptonPalette_Marathon.Ribbon.RibbonTab.StateTracking.BackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.KryptonPalette_Marathon.Ribbon.RibbonTab.StateTracking.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))));
            this.KryptonPalette_Marathon.ToolMenuStatus.UseRoundedEdges = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            // 
            // kryptonContextMenuItem2
            // 
            this.kryptonContextMenuItem2.Text = "Menu Item";
            // 
            // Workspace
            // 
            this.AllowFormChrome = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.DockPanel_Workspace);
            this.Controls.Add(this.KryptonRibbon_Workspace);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(960, 540);
            this.Name = "Workspace";
            this.Palette = this.KryptonPalette_Marathon;
            this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
            this.Text = "Marathon Toolkit";
            this.TextExtra = "";
            this.UseDropShadow = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Workspace_FormClosing);
            this.ResizeEnd += new System.EventHandler(this.Workspace_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.KryptonRibbon_Workspace)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ComponentFactory.Krypton.Ribbon.KryptonRibbon KryptonRibbon_Workspace;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem KryptonContextMenuItem_File_New;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem KryptonContextMenuItem_File_Open;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuSeparator KryptonContextMenuSeparator_File_1;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonTab KryptonRibbonTab_Developer;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup KryptonRibbonGroup_Developer_Tools;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple KryptonRibbonGroupTriple_Developer_New_Debugger;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton KryptonRibbonGroupButton_Developer_Tools_Debugger;
        private WeifenLuo.WinFormsUI.Docking.DockPanel DockPanel_Workspace;
        private WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme VS2015DarkTheme_DockPanel_Workspace;
        private ComponentFactory.Krypton.Toolkit.KryptonManager KryptonManager_Workspace;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItems KryptonContextMenuItem_File_Open_Items;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem KryptonContextMenuItem_File_Open_Items_OpenFile;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem KryptonContextMenuItem_File_Open_Items_OpenFolder;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonTab KryptonRibbonTab_Home;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup KryptonRibbonGroup_Home_Organise;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple KryptonRibbonGroupTriple_Home_Organise_BulkRenamer;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton KryptonRibbonGroupButton_Home_Organise_BulkRenamer;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette KryptonPalette_Marathon;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem kryptonContextMenuItem2;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton KryptonRibbonGroupButton_Developer_Tools_ResetSettings;
        private ComponentFactory.Krypton.Ribbon.ButtonSpecAppMenu ButtonSpecAppMenu_ExitMarathon;
        private ComponentFactory.Krypton.Ribbon.ButtonSpecAppMenu ButtonSpecAppMenu_MarathonOptions;
        private ComponentFactory.Krypton.Ribbon.ButtonSpecAppMenu ButtonSpecAppMenu_AboutMarathon;
    }
}

