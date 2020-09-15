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
            this.ListView_Explorer = new Marathon.Toolkit.Components.ListViewDark();
            this.Column_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_Size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_Space = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MenuStripDark_Main = new Marathon.Toolkit.Components.MenuStripDark();
            this.MenuStripDark_Main_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_File_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_File_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_File_ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuStripDark_Main_File_Close = new System.Windows.Forms.ToolStripMenuItem();
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
            this.SplitContainer_TreeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.SplitContainer_TreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.SplitContainer_TreeView.Panel2.Controls.Add(this.ListView_Explorer);
            this.SplitContainer_TreeView.Size = new System.Drawing.Size(786, 389);
            this.SplitContainer_TreeView.SplitterDistance = 184;
            this.SplitContainer_TreeView.TabIndex = 1;
            // 
            // TreeView_Explorer
            // 
            this.TreeView_Explorer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeView_Explorer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TreeView_Explorer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeView_Explorer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TreeView_Explorer.ForeColor = System.Drawing.SystemColors.Control;
            this.TreeView_Explorer.ImageIndex = 0;
            this.TreeView_Explorer.ImageList = this.ImageList_Keys;
            this.TreeView_Explorer.LabelEdit = true;
            this.TreeView_Explorer.Location = new System.Drawing.Point(0, 0);
            this.TreeView_Explorer.Name = "TreeView_Explorer";
            this.TreeView_Explorer.SelectedImageIndex = 0;
            this.TreeView_Explorer.ShowRootLines = false;
            this.TreeView_Explorer.Size = new System.Drawing.Size(182, 387);
            this.TreeView_Explorer.TabIndex = 2;
            this.TreeView_Explorer.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeView_Explorer_AfterLabelEdit);
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
            // ListView_Explorer
            // 
            this.ListView_Explorer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_Explorer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ListView_Explorer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListView_Explorer.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Column_Name,
            this.Column_Type,
            this.Column_Size,
            this.Column_Space});
            this.ListView_Explorer.ForeColor = System.Drawing.SystemColors.Control;
            this.ListView_Explorer.FullRowSelect = true;
            this.ListView_Explorer.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListView_Explorer.HideSelection = false;
            this.ListView_Explorer.Location = new System.Drawing.Point(-1, 0);
            this.ListView_Explorer.Name = "ListView_Explorer";
            this.ListView_Explorer.OwnerDraw = true;
            this.ListView_Explorer.Size = new System.Drawing.Size(597, 404);
            this.ListView_Explorer.SmallImageList = this.ImageList_Keys;
            this.ListView_Explorer.TabIndex = 0;
            this.ListView_Explorer.UseCompatibleStateImageBehavior = false;
            this.ListView_Explorer.View = System.Windows.Forms.View.Details;
            this.ListView_Explorer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListView_Explorer_MouseDown);
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
            this.Column_Space.Width = 297;
            // 
            // MenuStripDark_Main
            // 
            this.MenuStripDark_Main.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MenuStripDark_Main.AutoSize = false;
            this.MenuStripDark_Main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.MenuStripDark_Main.Dock = System.Windows.Forms.DockStyle.None;
            this.MenuStripDark_Main.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStripDark_Main_File});
            this.MenuStripDark_Main.Location = new System.Drawing.Point(-4, 0);
            this.MenuStripDark_Main.Name = "MenuStripDark_Main";
            this.MenuStripDark_Main.Size = new System.Drawing.Size(790, 24);
            this.MenuStripDark_Main.TabIndex = 2;
            // 
            // MenuStripDark_Main_File
            // 
            this.MenuStripDark_Main_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStripDark_Main_File_Save,
            this.MenuStripDark_Main_File_SaveAs,
            this.MenuStripDark_Main_File_ToolStripSeparator2,
            this.MenuStripDark_Main_File_Close});
            this.MenuStripDark_Main_File.Name = "MenuStripDark_Main_File";
            this.MenuStripDark_Main_File.Size = new System.Drawing.Size(37, 20);
            this.MenuStripDark_Main_File.Text = "File";
            // 
            // MenuStripDark_Main_File_Save
            // 
            this.MenuStripDark_Main_File_Save.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_File_Save.Name = "MenuStripDark_Main_File_Save";
            this.MenuStripDark_Main_File_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.MenuStripDark_Main_File_Save.Size = new System.Drawing.Size(186, 22);
            this.MenuStripDark_Main_File_Save.Text = "Save";
            // 
            // MenuStripDark_Main_File_SaveAs
            // 
            this.MenuStripDark_Main_File_SaveAs.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_File_SaveAs.Name = "MenuStripDark_Main_File_SaveAs";
            this.MenuStripDark_Main_File_SaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.MenuStripDark_Main_File_SaveAs.Size = new System.Drawing.Size(186, 22);
            this.MenuStripDark_Main_File_SaveAs.Text = "Save As...";
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
            this.MenuStripDark_Main_File_Close.Click += new System.EventHandler(this.MenuStripDark_Main_File_Close_Click);
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
            this.SplitContainer_TreeView.Panel1.ResumeLayout(false);
            this.SplitContainer_TreeView.Panel2.ResumeLayout(false);
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
        private Components.ListViewDark ListView_Explorer;
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
    }
}
