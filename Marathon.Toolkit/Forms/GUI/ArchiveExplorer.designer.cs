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
            this.SplitContainer_TreeView = new System.Windows.Forms.SplitContainer();
            this.TreeView_Explorer = new System.Windows.Forms.TreeView();
            this.ImageList_Keys = new System.Windows.Forms.ImageList(this.components);
            this.Label_DirectoryEmpty = new System.Windows.Forms.Label();
            this.ListViewDark_Explorer = new Marathon.Toolkit.Components.ListViewDark();
            this.Column_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_Size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_Space = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MenuStripDark_Main = new Marathon.Toolkit.Components.MenuStripDark();
            this.MenuStripDark_Main_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_File_Extract = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_File_ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuStripDark_Main_File_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_File_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_File_ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuStripDark_Main_File_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_Tools = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_Tools_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_Tools_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuStripDark_Main_Tools_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_Tools_Rename = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_Selection = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_Selection_SelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_Selection_SelectNone = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_Selection_InvertSelection = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_TreeView)).BeginInit();
            this.SplitContainer_TreeView.Panel1.SuspendLayout();
            this.SplitContainer_TreeView.Panel2.SuspendLayout();
            this.SplitContainer_TreeView.SuspendLayout();
            this.MenuStripDark_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // SplitContainer_TreeView
            // 
            this.SplitContainer_TreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SplitContainer_TreeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.SplitContainer_TreeView.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SplitContainer_TreeView.Location = new System.Drawing.Point(-1, 23);
            this.SplitContainer_TreeView.Name = "SplitContainer_TreeView";
            // 
            // SplitContainer_TreeView.Panel1
            // 
            this.SplitContainer_TreeView.Panel1.Controls.Add(this.TreeView_Explorer);
            this.SplitContainer_TreeView.Panel1MinSize = 109;
            // 
            // SplitContainer_TreeView.Panel2
            // 
            this.SplitContainer_TreeView.Panel2.Controls.Add(this.Label_DirectoryEmpty);
            this.SplitContainer_TreeView.Panel2.Controls.Add(this.ListViewDark_Explorer);
            this.SplitContainer_TreeView.Size = new System.Drawing.Size(786, 389);
            this.SplitContainer_TreeView.SplitterDistance = 183;
            this.SplitContainer_TreeView.TabIndex = 1;
            // 
            // TreeView_Explorer
            // 
            this.TreeView_Explorer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.TreeView_Explorer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeView_Explorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView_Explorer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TreeView_Explorer.ForeColor = System.Drawing.SystemColors.Control;
            this.TreeView_Explorer.ImageIndex = 0;
            this.TreeView_Explorer.ImageList = this.ImageList_Keys;
            this.TreeView_Explorer.LabelEdit = true;
            this.TreeView_Explorer.Location = new System.Drawing.Point(0, 0);
            this.TreeView_Explorer.Name = "TreeView_Explorer";
            this.TreeView_Explorer.SelectedImageIndex = 0;
            this.TreeView_Explorer.ShowNodeToolTips = true;
            this.TreeView_Explorer.ShowRootLines = false;
            this.TreeView_Explorer.Size = new System.Drawing.Size(183, 389);
            this.TreeView_Explorer.TabIndex = 2;
            this.TreeView_Explorer.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeView_Explorer_BeforeLabelEdit);
            this.TreeView_Explorer.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeView_Explorer_AfterLabelEdit);
            this.TreeView_Explorer.NodeMouseHover += new System.Windows.Forms.TreeNodeMouseHoverEventHandler(this.TreeView_Explorer_NodeMouseHover);
            this.TreeView_Explorer.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_Explorer_NodeMouseClick);
            this.TreeView_Explorer.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_Explorer_NodeMouseDoubleClick);
            // 
            // ImageList_Keys
            // 
            this.ImageList_Keys.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList_Keys.ImageStream")));
            this.ImageList_Keys.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList_Keys.Images.SetKeyName(0, "Folder");
            this.ImageList_Keys.Images.SetKeyName(1, "File");
            // 
            // Label_DirectoryEmpty
            // 
            this.Label_DirectoryEmpty.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Label_DirectoryEmpty.AutoSize = true;
            this.Label_DirectoryEmpty.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.Label_DirectoryEmpty.ForeColor = System.Drawing.SystemColors.Control;
            this.Label_DirectoryEmpty.Location = new System.Drawing.Point(245, 37);
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
            this.ListViewDark_Explorer.Location = new System.Drawing.Point(-1, 1);
            this.ListViewDark_Explorer.Name = "ListViewDark_Explorer";
            this.ListViewDark_Explorer.OwnerDraw = true;
            this.ListViewDark_Explorer.ShowItemToolTips = true;
            this.ListViewDark_Explorer.Size = new System.Drawing.Size(599, 406);
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
            this.Column_Space.Width = 299;
            // 
            // MenuStripDark_Main
            // 
            this.MenuStripDark_Main.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MenuStripDark_Main.AutoSize = false;
            this.MenuStripDark_Main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.MenuStripDark_Main.Dock = System.Windows.Forms.DockStyle.None;
            this.MenuStripDark_Main.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStripDark_Main_File,
            this.MenuStripDark_Main_Tools,
            this.MenuStripDark_Main_Selection});
            this.MenuStripDark_Main.Location = new System.Drawing.Point(-4, 0);
            this.MenuStripDark_Main.Name = "MenuStripDark_Main";
            this.MenuStripDark_Main.Size = new System.Drawing.Size(790, 24);
            this.MenuStripDark_Main.TabIndex = 2;
            // 
            // MenuStripDark_Main_File
            // 
            this.MenuStripDark_Main_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStripDark_Main_File_Extract,
            this.MenuStripDark_Main_File_ToolStripSeparator1,
            this.MenuStripDark_Main_File_Save,
            this.MenuStripDark_Main_File_SaveAs,
            this.MenuStripDark_Main_File_ToolStripSeparator2,
            this.MenuStripDark_Main_File_Close});
            this.MenuStripDark_Main_File.Name = "MenuStripDark_Main_File";
            this.MenuStripDark_Main_File.Size = new System.Drawing.Size(37, 20);
            this.MenuStripDark_Main_File.Text = "File";
            // 
            // MenuStripDark_Main_File_Extract
            // 
            this.MenuStripDark_Main_File_Extract.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_File_Extract.Name = "MenuStripDark_Main_File_Extract";
            this.MenuStripDark_Main_File_Extract.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.O)));
            this.MenuStripDark_Main_File_Extract.Size = new System.Drawing.Size(186, 22);
            this.MenuStripDark_Main_File_Extract.Text = "Extract";
            this.MenuStripDark_Main_File_Extract.Click += new System.EventHandler(this.MenuStripDark_Main_File_Click_Group);
            // 
            // MenuStripDark_Main_File_ToolStripSeparator1
            // 
            this.MenuStripDark_Main_File_ToolStripSeparator1.Name = "MenuStripDark_Main_File_ToolStripSeparator1";
            this.MenuStripDark_Main_File_ToolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // MenuStripDark_Main_File_Save
            // 
            this.MenuStripDark_Main_File_Save.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_File_Save.Name = "MenuStripDark_Main_File_Save";
            this.MenuStripDark_Main_File_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.MenuStripDark_Main_File_Save.Size = new System.Drawing.Size(186, 22);
            this.MenuStripDark_Main_File_Save.Text = "Save";
            this.MenuStripDark_Main_File_Save.Click += new System.EventHandler(this.MenuStripDark_Main_File_Click_Group);
            // 
            // MenuStripDark_Main_File_SaveAs
            // 
            this.MenuStripDark_Main_File_SaveAs.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_File_SaveAs.Name = "MenuStripDark_Main_File_SaveAs";
            this.MenuStripDark_Main_File_SaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.MenuStripDark_Main_File_SaveAs.Size = new System.Drawing.Size(186, 22);
            this.MenuStripDark_Main_File_SaveAs.Text = "Save As...";
            this.MenuStripDark_Main_File_SaveAs.Click += new System.EventHandler(this.MenuStripDark_Main_File_Click_Group);
            // 
            // MenuStripDark_Main_File_ToolStripSeparator2
            // 
            this.MenuStripDark_Main_File_ToolStripSeparator2.Name = "MenuStripDark_Main_File_ToolStripSeparator2";
            this.MenuStripDark_Main_File_ToolStripSeparator2.Size = new System.Drawing.Size(183, 6);
            // 
            // MenuStripDark_Main_File_Close
            // 
            this.MenuStripDark_Main_File_Close.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_File_Close.Name = "MenuStripDark_Main_File_Close";
            this.MenuStripDark_Main_File_Close.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.MenuStripDark_Main_File_Close.Size = new System.Drawing.Size(186, 22);
            this.MenuStripDark_Main_File_Close.Text = "Close";
            this.MenuStripDark_Main_File_Close.Click += new System.EventHandler(this.MenuStripDark_Main_File_Click_Group);
            // 
            // MenuStripDark_Main_Tools
            // 
            this.MenuStripDark_Main_Tools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStripDark_Main_Tools_Copy,
            this.MenuStripDark_Main_Tools_Paste,
            this.toolStripSeparator1,
            this.MenuStripDark_Main_Tools_Delete,
            this.MenuStripDark_Main_Tools_Rename});
            this.MenuStripDark_Main_Tools.Name = "MenuStripDark_Main_Tools";
            this.MenuStripDark_Main_Tools.Size = new System.Drawing.Size(46, 20);
            this.MenuStripDark_Main_Tools.Text = "Tools";
            // 
            // MenuStripDark_Main_Tools_Copy
            // 
            this.MenuStripDark_Main_Tools_Copy.Enabled = false;
            this.MenuStripDark_Main_Tools_Copy.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_Tools_Copy.Name = "MenuStripDark_Main_Tools_Copy";
            this.MenuStripDark_Main_Tools_Copy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.MenuStripDark_Main_Tools_Copy.Size = new System.Drawing.Size(144, 22);
            this.MenuStripDark_Main_Tools_Copy.Text = "Copy";
            this.MenuStripDark_Main_Tools_Copy.Click += new System.EventHandler(this.MenuStripDark_Main_Tools_Click_Group);
            // 
            // MenuStripDark_Main_Tools_Paste
            // 
            this.MenuStripDark_Main_Tools_Paste.Enabled = false;
            this.MenuStripDark_Main_Tools_Paste.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_Tools_Paste.Name = "MenuStripDark_Main_Tools_Paste";
            this.MenuStripDark_Main_Tools_Paste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.MenuStripDark_Main_Tools_Paste.Size = new System.Drawing.Size(144, 22);
            this.MenuStripDark_Main_Tools_Paste.Text = "Paste";
            this.MenuStripDark_Main_Tools_Paste.Click += new System.EventHandler(this.MenuStripDark_Main_Tools_Click_Group);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(141, 6);
            // 
            // MenuStripDark_Main_Tools_Delete
            // 
            this.MenuStripDark_Main_Tools_Delete.Enabled = false;
            this.MenuStripDark_Main_Tools_Delete.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_Tools_Delete.Name = "MenuStripDark_Main_Tools_Delete";
            this.MenuStripDark_Main_Tools_Delete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.MenuStripDark_Main_Tools_Delete.Size = new System.Drawing.Size(144, 22);
            this.MenuStripDark_Main_Tools_Delete.Text = "Delete";
            this.MenuStripDark_Main_Tools_Delete.Click += new System.EventHandler(this.MenuStripDark_Main_Tools_Click_Group);
            // 
            // MenuStripDark_Main_Tools_Rename
            // 
            this.MenuStripDark_Main_Tools_Rename.Enabled = false;
            this.MenuStripDark_Main_Tools_Rename.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_Tools_Rename.Name = "MenuStripDark_Main_Tools_Rename";
            this.MenuStripDark_Main_Tools_Rename.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.MenuStripDark_Main_Tools_Rename.Size = new System.Drawing.Size(144, 22);
            this.MenuStripDark_Main_Tools_Rename.Text = "Rename";
            this.MenuStripDark_Main_Tools_Rename.Click += new System.EventHandler(this.MenuStripDark_Main_Tools_Click_Group);
            // 
            // MenuStripDark_Main_Selection
            // 
            this.MenuStripDark_Main_Selection.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStripDark_Main_Selection_SelectAll,
            this.MenuStripDark_Main_Selection_SelectNone,
            this.MenuStripDark_Main_Selection_InvertSelection});
            this.MenuStripDark_Main_Selection.Name = "MenuStripDark_Main_Selection";
            this.MenuStripDark_Main_Selection.Size = new System.Drawing.Size(67, 20);
            this.MenuStripDark_Main_Selection.Text = "Selection";
            // 
            // MenuStripDark_Main_Selection_SelectAll
            // 
            this.MenuStripDark_Main_Selection_SelectAll.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_Selection_SelectAll.Name = "MenuStripDark_Main_Selection_SelectAll";
            this.MenuStripDark_Main_Selection_SelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.MenuStripDark_Main_Selection_SelectAll.Size = new System.Drawing.Size(164, 22);
            this.MenuStripDark_Main_Selection_SelectAll.Text = "Select All";
            this.MenuStripDark_Main_Selection_SelectAll.Click += new System.EventHandler(this.MenuStripDark_Main_Selection_Click_Group);
            // 
            // MenuStripDark_Main_Selection_SelectNone
            // 
            this.MenuStripDark_Main_Selection_SelectNone.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_Selection_SelectNone.Name = "MenuStripDark_Main_Selection_SelectNone";
            this.MenuStripDark_Main_Selection_SelectNone.Size = new System.Drawing.Size(164, 22);
            this.MenuStripDark_Main_Selection_SelectNone.Text = "Select None";
            this.MenuStripDark_Main_Selection_SelectNone.Click += new System.EventHandler(this.MenuStripDark_Main_Selection_Click_Group);
            // 
            // MenuStripDark_Main_Selection_InvertSelection
            // 
            this.MenuStripDark_Main_Selection_InvertSelection.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_Selection_InvertSelection.Name = "MenuStripDark_Main_Selection_InvertSelection";
            this.MenuStripDark_Main_Selection_InvertSelection.Size = new System.Drawing.Size(164, 22);
            this.MenuStripDark_Main_Selection_InvertSelection.Text = "Invert Selection";
            this.MenuStripDark_Main_Selection_InvertSelection.Click += new System.EventHandler(this.MenuStripDark_Main_Selection_Click_Group);
            // 
            // ArchiveExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(784, 411);
            this.Controls.Add(this.MenuStripDark_Main);
            this.Controls.Add(this.SplitContainer_TreeView);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStripDark_Main;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ArchiveExplorer";
            this.Text = "Archive Explorer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ArchiveExplorer_FormClosing);
            this.SplitContainer_TreeView.Panel1.ResumeLayout(false);
            this.SplitContainer_TreeView.Panel2.ResumeLayout(false);
            this.SplitContainer_TreeView.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_TreeView)).EndInit();
            this.SplitContainer_TreeView.ResumeLayout(false);
            this.MenuStripDark_Main.ResumeLayout(false);
            this.MenuStripDark_Main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer SplitContainer_TreeView;
        private System.Windows.Forms.TreeView TreeView_Explorer;
        private System.Windows.Forms.ImageList ImageList_Keys;
        private Components.ListViewDark ListViewDark_Explorer;
        private System.Windows.Forms.ColumnHeader Column_Name;
        private System.Windows.Forms.ColumnHeader Column_Type;
        private System.Windows.Forms.ColumnHeader Column_Size;
        private System.Windows.Forms.ColumnHeader Column_Space;
        private Components.MenuStripDark MenuStripDark_Main;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_File;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_File_Save;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_File_SaveAs;
        private System.Windows.Forms.ToolStripSeparator MenuStripDark_Main_File_ToolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_File_Close;
        private System.Windows.Forms.Label Label_DirectoryEmpty;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_File_Extract;
        private System.Windows.Forms.ToolStripSeparator MenuStripDark_Main_File_ToolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_Tools;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_Tools_Copy;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_Tools_Paste;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_Tools_Rename;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_Tools_Delete;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_Selection;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_Selection_SelectAll;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_Selection_SelectNone;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_Selection_InvertSelection;
    }
}
