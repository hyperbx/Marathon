namespace Marathon.Toolkit.Forms
{
    partial class ArchiveExplorer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArchiveExplorer));
            this.KryptonTreeView_Explorer = new ComponentFactory.Krypton.Toolkit.KryptonTreeView();
            this.ImageList_Keys = new System.Windows.Forms.ImageList(this.components);
            this.Label_DirectoryEmpty = new System.Windows.Forms.Label();
            this.ListViewDark_Explorer = new Marathon.Toolkit.Components.ListViewDark();
            this.Column_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_Size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_Space = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.KryptonRibbonTab_Home = new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab();
            this.KryptonRibbonGroup_Clipboard = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
            this.KryptonRibbonGroupTriple_Clipboard_Copy_Paste = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.KryptonRibbonGroupButton_Clipboard_Copy = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.KryptonRibbonGroupButton_Clipboard_Paste = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.KryptonRibbonGroupTriple_Clipboard_Cut_CopyPath = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.KryptonRibbonGroupButton_Clipboard_Cut = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.KryptonRibbonGroupButton_Clipboard_CopyPath = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.KryptonRibbonGroup_Organise = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
            this.KryptonRibbonGroupTriple_Organise_Delete_Rename = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.KryptonRibbonGroupButton_Organise_Delete = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.KryptonRibbonGroupButton_Organise_Rename = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.KryptonRibbonGroup_New = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
            this.KryptonRibbonGroupTriple_New_NewFolder = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.KryptonRibbonGroupButton_New_NewFolder = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.KryptonRibbonGroupTriple_New_NewItem = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.KryptonRibbonGroupButton_New_NewItem = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.KryptonRibbonGroup_Select = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup();
            this.KryptonRibbonGroupTriple_Select_All_None_Invert = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple();
            this.KryptonRibbonGroupButton_Select_SelectAll = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.KryptonRibbonGroupButton_Select_SelectNone = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.KryptonRibbonGroupButton_Select_InvertSelection = new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton();
            this.KryptonRibbonQATButton_Save = new ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton();
            this.KryptonRibbonQATButton_Extract = new ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton();
            this.KryptonSplitContainer_Explorer = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.KryptonContextMenuItem_File_Save = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.KryptonContextMenuItem_File_SaveAs = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            this.KryptonContextMenuSeparator_File_1 = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuSeparator();
            this.KryptonContextMenuItem_File_Extract = new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.KryptonRibbon_MarathonForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KryptonSplitContainer_Explorer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KryptonSplitContainer_Explorer.Panel1)).BeginInit();
            this.KryptonSplitContainer_Explorer.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KryptonSplitContainer_Explorer.Panel2)).BeginInit();
            this.KryptonSplitContainer_Explorer.Panel2.SuspendLayout();
            this.KryptonSplitContainer_Explorer.SuspendLayout();
            this.SuspendLayout();
            // 
            // KryptonRibbon_MarathonForm
            // 
            this.KryptonRibbon_MarathonForm.QATButtons.AddRange(new System.ComponentModel.Component[] {
            this.KryptonRibbonQATButton_Save,
            this.KryptonRibbonQATButton_Extract});
            this.KryptonRibbon_MarathonForm.RibbonAppButton.AppButtonMenuItems.AddRange(new ComponentFactory.Krypton.Toolkit.KryptonContextMenuItemBase[] {
            this.KryptonContextMenuItem_File_Save,
            this.KryptonContextMenuItem_File_SaveAs,
            this.KryptonContextMenuSeparator_File_1,
            this.KryptonContextMenuItem_File_Extract});
            this.KryptonRibbon_MarathonForm.RibbonAppButton.AppButtonShowRecentDocs = false;
            this.KryptonRibbon_MarathonForm.RibbonTabs.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonTab[] {
            this.KryptonRibbonTab_Home});
            this.KryptonRibbon_MarathonForm.SelectedTab = this.KryptonRibbonTab_Home;
            this.KryptonRibbon_MarathonForm.Size = new System.Drawing.Size(969, 115);
            // 
            // KryptonTreeView_Explorer
            // 
            this.KryptonTreeView_Explorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.KryptonTreeView_Explorer.ImageIndex = 0;
            this.KryptonTreeView_Explorer.ImageList = this.ImageList_Keys;
            this.KryptonTreeView_Explorer.LabelEdit = true;
            this.KryptonTreeView_Explorer.Location = new System.Drawing.Point(0, 0);
            this.KryptonTreeView_Explorer.Name = "KryptonTreeView_Explorer";
            this.KryptonTreeView_Explorer.SelectedImageIndex = 0;
            this.KryptonTreeView_Explorer.Size = new System.Drawing.Size(182, 573);
            this.KryptonTreeView_Explorer.StateCheckedNormal.Node.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.KryptonTreeView_Explorer.StateCheckedNormal.Node.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.KryptonTreeView_Explorer.StateCheckedNormal.Node.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.KryptonTreeView_Explorer.StateCheckedNormal.Node.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.KryptonTreeView_Explorer.StateCheckedNormal.Node.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.KryptonTreeView_Explorer.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.KryptonTreeView_Explorer.StateCommon.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.KryptonTreeView_Explorer.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.KryptonTreeView_Explorer.StateCommon.Node.Content.LongText.Color1 = System.Drawing.SystemColors.Control;
            this.KryptonTreeView_Explorer.StateCommon.Node.Content.LongText.Color2 = System.Drawing.SystemColors.Control;
            this.KryptonTreeView_Explorer.StateCommon.Node.Content.ShortText.Color1 = System.Drawing.SystemColors.Control;
            this.KryptonTreeView_Explorer.StateCommon.Node.Content.ShortText.Color2 = System.Drawing.SystemColors.Control;
            this.KryptonTreeView_Explorer.TabIndex = 3;
            this.KryptonTreeView_Explorer.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.KryptonTreeView_Explorer_AfterLabelEdit);
            this.KryptonTreeView_Explorer.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.KryptonTreeView_Explorer_BeforeLabelEdit);
            this.KryptonTreeView_Explorer.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.KryptonTreeView_Explorer_NodeMouseClick);
            this.KryptonTreeView_Explorer.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.KryptonTreeView_Explorer_NodeMouseDoubleClick);
            this.KryptonTreeView_Explorer.NodeMouseHover += new System.Windows.Forms.TreeNodeMouseHoverEventHandler(this.KryptonTreeView_Explorer_NodeMouseHover);
            // 
            // ImageList_Keys
            // 
            this.ImageList_Keys.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList_Keys.ImageStream")));
            this.ImageList_Keys.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList_Keys.Images.SetKeyName(0, "Folder");
            this.ImageList_Keys.Images.SetKeyName(1, "File");
            this.ImageList_Keys.Images.SetKeyName(2, "PendingFile");
            // 
            // Label_DirectoryEmpty
            // 
            this.Label_DirectoryEmpty.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Label_DirectoryEmpty.AutoSize = true;
            this.Label_DirectoryEmpty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.Label_DirectoryEmpty.ForeColor = System.Drawing.SystemColors.Control;
            this.Label_DirectoryEmpty.Location = new System.Drawing.Point(344, 41);
            this.Label_DirectoryEmpty.Name = "Label_DirectoryEmpty";
            this.Label_DirectoryEmpty.Size = new System.Drawing.Size(113, 15);
            this.Label_DirectoryEmpty.TabIndex = 1;
            this.Label_DirectoryEmpty.Text = "This folder is empty.";
            this.Label_DirectoryEmpty.Visible = false;
            this.Label_DirectoryEmpty.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListViewDark_Explorer_MouseDown);
            // 
            // ListViewDark_Explorer
            // 
            this.ListViewDark_Explorer.AllowDrop = true;
            this.ListViewDark_Explorer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListViewDark_Explorer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ListViewDark_Explorer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListViewDark_Explorer.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Column_Name,
            this.Column_Type,
            this.Column_Size,
            this.Column_Space});
            this.ListViewDark_Explorer.ForeColor = System.Drawing.SystemColors.Control;
            this.ListViewDark_Explorer.FullRowSelect = true;
            this.ListViewDark_Explorer.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListViewDark_Explorer.HideSelection = false;
            this.ListViewDark_Explorer.LabelEdit = true;
            this.ListViewDark_Explorer.Location = new System.Drawing.Point(-1, 0);
            this.ListViewDark_Explorer.Name = "ListViewDark_Explorer";
            this.ListViewDark_Explorer.OwnerDraw = true;
            this.ListViewDark_Explorer.ShowItemToolTips = true;
            this.ListViewDark_Explorer.Size = new System.Drawing.Size(784, 590);
            this.ListViewDark_Explorer.SmallImageList = this.ImageList_Keys;
            this.ListViewDark_Explorer.TabIndex = 0;
            this.ListViewDark_Explorer.UseCompatibleStateImageBehavior = false;
            this.ListViewDark_Explorer.View = System.Windows.Forms.View.Details;
            this.ListViewDark_Explorer.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.ListViewDark_Explorer_AfterLabelEdit);
            this.ListViewDark_Explorer.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.ListViewDark_Explorer_ItemDrag);
            this.ListViewDark_Explorer.ItemMouseHover += new System.Windows.Forms.ListViewItemMouseHoverEventHandler(this.ListViewDark_Explorer_ItemMouseHover);
            this.ListViewDark_Explorer.SelectedIndexChanged += new System.EventHandler(this.ListViewDark_Explorer_SelectedIndexChanged);
            this.ListViewDark_Explorer.DragDrop += new System.Windows.Forms.DragEventHandler(this.ListViewDark_Explorer_DragDrop);
            this.ListViewDark_Explorer.DragEnter += new System.Windows.Forms.DragEventHandler(this.ListViewDark_Explorer_DragEnter);
            this.ListViewDark_Explorer.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListViewDark_Explorer_MouseDoubleClick);
            this.ListViewDark_Explorer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListViewDark_Explorer_MouseDown);
            // 
            // Column_Name
            // 
            this.Column_Name.Text = "Name";
            this.Column_Name.Width = 300;
            // 
            // Column_Type
            // 
            this.Column_Type.Text = "Type";
            this.Column_Type.Width = 150;
            // 
            // Column_Size
            // 
            this.Column_Size.Text = "Size";
            this.Column_Size.Width = 150;
            // 
            // Column_Space
            // 
            this.Column_Space.Text = "";
            this.Column_Space.Width = 484;
            // 
            // KryptonRibbonTab_Home
            // 
            this.KryptonRibbonTab_Home.Groups.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup[] {
            this.KryptonRibbonGroup_Clipboard,
            this.KryptonRibbonGroup_Organise,
            this.KryptonRibbonGroup_New,
            this.KryptonRibbonGroup_Select});
            this.KryptonRibbonTab_Home.KeyTip = "H";
            this.KryptonRibbonTab_Home.Text = "Home";
            // 
            // KryptonRibbonGroup_Clipboard
            // 
            this.KryptonRibbonGroup_Clipboard.AllowCollapsed = false;
            this.KryptonRibbonGroup_Clipboard.DialogBoxLauncher = false;
            this.KryptonRibbonGroup_Clipboard.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.KryptonRibbonGroupTriple_Clipboard_Copy_Paste,
            this.KryptonRibbonGroupTriple_Clipboard_Cut_CopyPath});
            this.KryptonRibbonGroup_Clipboard.TextLine1 = "Clipboard";
            // 
            // KryptonRibbonGroupTriple_Clipboard_Copy_Paste
            // 
            this.KryptonRibbonGroupTriple_Clipboard_Copy_Paste.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.KryptonRibbonGroupButton_Clipboard_Copy,
            this.KryptonRibbonGroupButton_Clipboard_Paste});
            // 
            // KryptonRibbonGroupButton_Clipboard_Copy
            // 
            this.KryptonRibbonGroupButton_Clipboard_Copy.Enabled = false;
            this.KryptonRibbonGroupButton_Clipboard_Copy.ImageLarge = global::Marathon.Toolkit.Properties.Resources.Task_Ribbon_Copy;
            this.KryptonRibbonGroupButton_Clipboard_Copy.KeyTip = "C";
            this.KryptonRibbonGroupButton_Clipboard_Copy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.KryptonRibbonGroupButton_Clipboard_Copy.TextLine1 = "Copy";
            this.KryptonRibbonGroupButton_Clipboard_Copy.Click += new System.EventHandler(this.KryptonRibbonGroup_Clipboard_Click_Group);
            // 
            // KryptonRibbonGroupButton_Clipboard_Paste
            // 
            this.KryptonRibbonGroupButton_Clipboard_Paste.Enabled = false;
            this.KryptonRibbonGroupButton_Clipboard_Paste.ImageLarge = global::Marathon.Toolkit.Properties.Resources.Task_Ribbon_Paste;
            this.KryptonRibbonGroupButton_Clipboard_Paste.KeyTip = "V";
            this.KryptonRibbonGroupButton_Clipboard_Paste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.KryptonRibbonGroupButton_Clipboard_Paste.TextLine1 = "Paste";
            this.KryptonRibbonGroupButton_Clipboard_Paste.Click += new System.EventHandler(this.KryptonRibbonGroup_Clipboard_Click_Group);
            // 
            // KryptonRibbonGroupTriple_Clipboard_Cut_CopyPath
            // 
            this.KryptonRibbonGroupTriple_Clipboard_Cut_CopyPath.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.KryptonRibbonGroupButton_Clipboard_Cut,
            this.KryptonRibbonGroupButton_Clipboard_CopyPath});
            this.KryptonRibbonGroupTriple_Clipboard_Cut_CopyPath.MaximumSize = ComponentFactory.Krypton.Ribbon.GroupItemSize.Medium;
            // 
            // KryptonRibbonGroupButton_Clipboard_Cut
            // 
            this.KryptonRibbonGroupButton_Clipboard_Cut.Enabled = false;
            this.KryptonRibbonGroupButton_Clipboard_Cut.ImageSmall = global::Marathon.Toolkit.Properties.Resources.Task_Ribbon_Cut;
            this.KryptonRibbonGroupButton_Clipboard_Cut.KeyTip = "X";
            this.KryptonRibbonGroupButton_Clipboard_Cut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.KryptonRibbonGroupButton_Clipboard_Cut.TextLine1 = "Cut";
            this.KryptonRibbonGroupButton_Clipboard_Cut.Click += new System.EventHandler(this.KryptonRibbonGroup_Clipboard_Click_Group);
            // 
            // KryptonRibbonGroupButton_Clipboard_CopyPath
            // 
            this.KryptonRibbonGroupButton_Clipboard_CopyPath.ImageSmall = global::Marathon.Toolkit.Properties.Resources.Task_Ribbon_CopyPath;
            this.KryptonRibbonGroupButton_Clipboard_CopyPath.KeyTip = "D";
            this.KryptonRibbonGroupButton_Clipboard_CopyPath.TextLine1 = "Copy path";
            this.KryptonRibbonGroupButton_Clipboard_CopyPath.Click += new System.EventHandler(this.KryptonRibbonGroup_Clipboard_Click_Group);
            // 
            // KryptonRibbonGroup_Organise
            // 
            this.KryptonRibbonGroup_Organise.AllowCollapsed = false;
            this.KryptonRibbonGroup_Organise.DialogBoxLauncher = false;
            this.KryptonRibbonGroup_Organise.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.KryptonRibbonGroupTriple_Organise_Delete_Rename});
            this.KryptonRibbonGroup_Organise.TextLine1 = "Organise";
            // 
            // KryptonRibbonGroupTriple_Organise_Delete_Rename
            // 
            this.KryptonRibbonGroupTriple_Organise_Delete_Rename.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.KryptonRibbonGroupButton_Organise_Delete,
            this.KryptonRibbonGroupButton_Organise_Rename});
            // 
            // KryptonRibbonGroupButton_Organise_Delete
            // 
            this.KryptonRibbonGroupButton_Organise_Delete.Enabled = false;
            this.KryptonRibbonGroupButton_Organise_Delete.ImageLarge = global::Marathon.Toolkit.Properties.Resources.Task_Ribbon_Delete;
            this.KryptonRibbonGroupButton_Organise_Delete.KeyTip = "DEL";
            this.KryptonRibbonGroupButton_Organise_Delete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.KryptonRibbonGroupButton_Organise_Delete.TextLine1 = "Delete";
            this.KryptonRibbonGroupButton_Organise_Delete.Click += new System.EventHandler(this.KryptonRibbonGroupButton_Organise_Click_Group);
            // 
            // KryptonRibbonGroupButton_Organise_Rename
            // 
            this.KryptonRibbonGroupButton_Organise_Rename.Enabled = false;
            this.KryptonRibbonGroupButton_Organise_Rename.ImageLarge = global::Marathon.Toolkit.Properties.Resources.Task_Ribbon_Rename;
            this.KryptonRibbonGroupButton_Organise_Rename.KeyTip = "F2";
            this.KryptonRibbonGroupButton_Organise_Rename.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.KryptonRibbonGroupButton_Organise_Rename.TextLine1 = "Rename";
            this.KryptonRibbonGroupButton_Organise_Rename.Click += new System.EventHandler(this.KryptonRibbonGroupButton_Organise_Click_Group);
            // 
            // KryptonRibbonGroup_New
            // 
            this.KryptonRibbonGroup_New.AllowCollapsed = false;
            this.KryptonRibbonGroup_New.DialogBoxLauncher = false;
            this.KryptonRibbonGroup_New.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.KryptonRibbonGroupTriple_New_NewFolder,
            this.KryptonRibbonGroupTriple_New_NewItem});
            this.KryptonRibbonGroup_New.TextLine1 = "New";
            // 
            // KryptonRibbonGroupTriple_New_NewFolder
            // 
            this.KryptonRibbonGroupTriple_New_NewFolder.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.KryptonRibbonGroupButton_New_NewFolder});
            // 
            // KryptonRibbonGroupButton_New_NewFolder
            // 
            this.KryptonRibbonGroupButton_New_NewFolder.ImageLarge = global::Marathon.Toolkit.Properties.Resources.Task_Ribbon_Folder;
            this.KryptonRibbonGroupButton_New_NewFolder.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.KryptonRibbonGroupButton_New_NewFolder.TextLine1 = "New";
            this.KryptonRibbonGroupButton_New_NewFolder.TextLine2 = "folder";
            this.KryptonRibbonGroupButton_New_NewFolder.Click += new System.EventHandler(this.KryptonRibbonGroupButton_New_NewFolder_Click);
            // 
            // KryptonRibbonGroupTriple_New_NewItem
            // 
            this.KryptonRibbonGroupTriple_New_NewItem.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.KryptonRibbonGroupButton_New_NewItem});
            this.KryptonRibbonGroupTriple_New_NewItem.MaximumSize = ComponentFactory.Krypton.Ribbon.GroupItemSize.Medium;
            // 
            // KryptonRibbonGroupButton_New_NewItem
            // 
            this.KryptonRibbonGroupButton_New_NewItem.ImageSmall = global::Marathon.Toolkit.Properties.Resources.Task_Ribbon_NewItem;
            this.KryptonRibbonGroupButton_New_NewItem.KeyTip = "N";
            this.KryptonRibbonGroupButton_New_NewItem.TextLine1 = "New item";
            // 
            // KryptonRibbonGroup_Select
            // 
            this.KryptonRibbonGroup_Select.AllowCollapsed = false;
            this.KryptonRibbonGroup_Select.DialogBoxLauncher = false;
            this.KryptonRibbonGroup_Select.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupContainer[] {
            this.KryptonRibbonGroupTriple_Select_All_None_Invert});
            this.KryptonRibbonGroup_Select.TextLine1 = "Select";
            // 
            // KryptonRibbonGroupTriple_Select_All_None_Invert
            // 
            this.KryptonRibbonGroupTriple_Select_All_None_Invert.Items.AddRange(new ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupItem[] {
            this.KryptonRibbonGroupButton_Select_SelectAll,
            this.KryptonRibbonGroupButton_Select_SelectNone,
            this.KryptonRibbonGroupButton_Select_InvertSelection});
            this.KryptonRibbonGroupTriple_Select_All_None_Invert.MaximumSize = ComponentFactory.Krypton.Ribbon.GroupItemSize.Medium;
            // 
            // KryptonRibbonGroupButton_Select_SelectAll
            // 
            this.KryptonRibbonGroupButton_Select_SelectAll.ImageSmall = global::Marathon.Toolkit.Properties.Resources.Task_Ribbon_SelectAll;
            this.KryptonRibbonGroupButton_Select_SelectAll.KeyTip = "A";
            this.KryptonRibbonGroupButton_Select_SelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.KryptonRibbonGroupButton_Select_SelectAll.TextLine1 = "Select all";
            this.KryptonRibbonGroupButton_Select_SelectAll.Click += new System.EventHandler(this.KryptonRibbonGroup_Select_Click_Group);
            // 
            // KryptonRibbonGroupButton_Select_SelectNone
            // 
            this.KryptonRibbonGroupButton_Select_SelectNone.ImageSmall = global::Marathon.Toolkit.Properties.Resources.Task_Ribbon_SelectNone;
            this.KryptonRibbonGroupButton_Select_SelectNone.KeyTip = "ESC";
            this.KryptonRibbonGroupButton_Select_SelectNone.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.A)));
            this.KryptonRibbonGroupButton_Select_SelectNone.TextLine1 = "Select none";
            this.KryptonRibbonGroupButton_Select_SelectNone.Click += new System.EventHandler(this.KryptonRibbonGroup_Select_Click_Group);
            // 
            // KryptonRibbonGroupButton_Select_InvertSelection
            // 
            this.KryptonRibbonGroupButton_Select_InvertSelection.ImageSmall = global::Marathon.Toolkit.Properties.Resources.Task_Ribbon_InvertSelection;
            this.KryptonRibbonGroupButton_Select_InvertSelection.KeyTip = "I";
            this.KryptonRibbonGroupButton_Select_InvertSelection.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.KryptonRibbonGroupButton_Select_InvertSelection.TextLine1 = "Invert selection";
            this.KryptonRibbonGroupButton_Select_InvertSelection.Click += new System.EventHandler(this.KryptonRibbonGroup_Select_Click_Group);
            // 
            // KryptonRibbonQATButton_Save
            // 
            this.KryptonRibbonQATButton_Save.Image = global::Marathon.Toolkit.Properties.Resources.Task_QAT_Save;
            this.KryptonRibbonQATButton_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.KryptonRibbonQATButton_Save.Text = "Save";
            this.KryptonRibbonQATButton_Save.Click += new System.EventHandler(this.RibbonAppButton_AppButtonMenuItems_Click_Group);
            // 
            // KryptonRibbonQATButton_Extract
            // 
            this.KryptonRibbonQATButton_Extract.Image = global::Marathon.Toolkit.Properties.Resources.Task_QAT_Export;
            this.KryptonRibbonQATButton_Extract.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.O)));
            this.KryptonRibbonQATButton_Extract.Text = "Extract";
            this.KryptonRibbonQATButton_Extract.Click += new System.EventHandler(this.RibbonAppButton_AppButtonMenuItems_Click_Group);
            // 
            // KryptonSplitContainer_Explorer
            // 
            this.KryptonSplitContainer_Explorer.ContainerBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.KryptonSplitContainer_Explorer.Cursor = System.Windows.Forms.Cursors.Default;
            this.KryptonSplitContainer_Explorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.KryptonSplitContainer_Explorer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.KryptonSplitContainer_Explorer.Location = new System.Drawing.Point(0, 0);
            this.KryptonSplitContainer_Explorer.Name = "KryptonSplitContainer_Explorer";
            // 
            // KryptonSplitContainer_Explorer.Panel1
            // 
            this.KryptonSplitContainer_Explorer.Panel1.Controls.Add(this.KryptonTreeView_Explorer);
            // 
            // KryptonSplitContainer_Explorer.Panel2
            // 
            this.KryptonSplitContainer_Explorer.Panel2.Controls.Add(this.Label_DirectoryEmpty);
            this.KryptonSplitContainer_Explorer.Panel2.Controls.Add(this.ListViewDark_Explorer);
            this.KryptonSplitContainer_Explorer.Size = new System.Drawing.Size(969, 573);
            this.KryptonSplitContainer_Explorer.SplitterDistance = 182;
            this.KryptonSplitContainer_Explorer.SplitterWidth = 4;
            this.KryptonSplitContainer_Explorer.TabIndex = 3;
            // 
            // KryptonContextMenuItem_File_Save
            // 
            this.KryptonContextMenuItem_File_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.KryptonContextMenuItem_File_Save.Text = "&Save";
            this.KryptonContextMenuItem_File_Save.Click += new System.EventHandler(this.RibbonAppButton_AppButtonMenuItems_Click_Group);
            // 
            // KryptonContextMenuItem_File_SaveAs
            // 
            this.KryptonContextMenuItem_File_SaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.KryptonContextMenuItem_File_SaveAs.Text = "Save &As";
            this.KryptonContextMenuItem_File_SaveAs.Click += new System.EventHandler(this.RibbonAppButton_AppButtonMenuItems_Click_Group);
            // 
            // KryptonContextMenuItem_File_Extract
            // 
            this.KryptonContextMenuItem_File_Extract.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.O)));
            this.KryptonContextMenuItem_File_Extract.Text = "&Extract";
            this.KryptonContextMenuItem_File_Extract.Click += new System.EventHandler(this.RibbonAppButton_AppButtonMenuItems_Click_Group);
            // 
            // ArchiveExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(969, 573);
            this.Controls.Add(this.KryptonSplitContainer_Explorer);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ArchiveExplorer";
            this.Text = "Archive Explorer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ArchiveExplorer_FormClosing);
            this.Controls.SetChildIndex(this.KryptonSplitContainer_Explorer, 0);
            this.Controls.SetChildIndex(this.KryptonRibbon_MarathonForm, 0);
            ((System.ComponentModel.ISupportInitialize)(this.KryptonRibbon_MarathonForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KryptonSplitContainer_Explorer.Panel1)).EndInit();
            this.KryptonSplitContainer_Explorer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.KryptonSplitContainer_Explorer.Panel2)).EndInit();
            this.KryptonSplitContainer_Explorer.Panel2.ResumeLayout(false);
            this.KryptonSplitContainer_Explorer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KryptonSplitContainer_Explorer)).EndInit();
            this.KryptonSplitContainer_Explorer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ImageList ImageList_Keys;
        private Components.ListViewDark ListViewDark_Explorer;
        private System.Windows.Forms.ColumnHeader Column_Name;
        private System.Windows.Forms.ColumnHeader Column_Type;
        private System.Windows.Forms.ColumnHeader Column_Size;
        private System.Windows.Forms.ColumnHeader Column_Space;
        private System.Windows.Forms.Label Label_DirectoryEmpty;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton KryptonRibbonQATButton_Save;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonQATButton KryptonRibbonQATButton_Extract;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonTab KryptonRibbonTab_Home;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup KryptonRibbonGroup_Clipboard;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple KryptonRibbonGroupTriple_Clipboard_Copy_Paste;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton KryptonRibbonGroupButton_Clipboard_Copy;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton KryptonRibbonGroupButton_Clipboard_Paste;
        private ComponentFactory.Krypton.Toolkit.KryptonTreeView KryptonTreeView_Explorer;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer KryptonSplitContainer_Explorer;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup KryptonRibbonGroup_Organise;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple KryptonRibbonGroupTriple_Organise_Delete_Rename;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton KryptonRibbonGroupButton_Organise_Delete;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton KryptonRibbonGroupButton_Organise_Rename;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup KryptonRibbonGroup_Select;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple KryptonRibbonGroupTriple_Select_All_None_Invert;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton KryptonRibbonGroupButton_Select_SelectAll;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton KryptonRibbonGroupButton_Select_SelectNone;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton KryptonRibbonGroupButton_Select_InvertSelection;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroup KryptonRibbonGroup_New;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple KryptonRibbonGroupTriple_New_NewFolder;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton KryptonRibbonGroupButton_New_NewFolder;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple KryptonRibbonGroupTriple_New_NewItem;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton KryptonRibbonGroupButton_New_NewItem;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupTriple KryptonRibbonGroupTriple_Clipboard_Cut_CopyPath;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton KryptonRibbonGroupButton_Clipboard_Cut;
        private ComponentFactory.Krypton.Ribbon.KryptonRibbonGroupButton KryptonRibbonGroupButton_Clipboard_CopyPath;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem KryptonContextMenuItem_File_Save;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem KryptonContextMenuItem_File_Extract;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuSeparator KryptonContextMenuSeparator_File_1;
        private ComponentFactory.Krypton.Toolkit.KryptonContextMenuItem KryptonContextMenuItem_File_SaveAs;
    }
}
