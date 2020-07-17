namespace Marathon.Controls
{
    partial class ListViewExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListViewExplorer));
            this.SplitContainer_TreeView = new System.Windows.Forms.SplitContainer();
            this.TreeView_Explorer = new System.Windows.Forms.TreeView();
            this.ImageList_Keys = new System.Windows.Forms.ImageList(this.components);
            this.ListView_Explorer = new System.Windows.Forms.ListView();
            this.Column_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_DateModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_Size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_HeaderSpace = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_TreeView)).BeginInit();
            this.SplitContainer_TreeView.Panel1.SuspendLayout();
            this.SplitContainer_TreeView.Panel2.SuspendLayout();
            this.SplitContainer_TreeView.SuspendLayout();
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
            this.SplitContainer_TreeView.Location = new System.Drawing.Point(-1, -1);
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
            this.SplitContainer_TreeView.Size = new System.Drawing.Size(802, 452);
            this.SplitContainer_TreeView.SplitterDistance = 183;
            this.SplitContainer_TreeView.TabIndex = 1;
            // 
            // TreeView_Explorer
            // 
            this.TreeView_Explorer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TreeView_Explorer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeView_Explorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView_Explorer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TreeView_Explorer.ForeColor = System.Drawing.SystemColors.Control;
            this.TreeView_Explorer.ImageIndex = 0;
            this.TreeView_Explorer.ImageList = this.ImageList_Keys;
            this.TreeView_Explorer.Location = new System.Drawing.Point(0, 0);
            this.TreeView_Explorer.Name = "TreeView_Explorer";
            this.TreeView_Explorer.SelectedImageIndex = 0;
            this.TreeView_Explorer.Size = new System.Drawing.Size(181, 450);
            this.TreeView_Explorer.TabIndex = 2;
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
            this.Column_DateModified,
            this.Column_Type,
            this.Column_Size,
            this.Column_HeaderSpace});
            this.ListView_Explorer.ForeColor = System.Drawing.SystemColors.Control;
            this.ListView_Explorer.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListView_Explorer.HideSelection = false;
            this.ListView_Explorer.Location = new System.Drawing.Point(-1, 0);
            this.ListView_Explorer.Name = "ListView_Explorer";
            this.ListView_Explorer.OwnerDraw = true;
            this.ListView_Explorer.Size = new System.Drawing.Size(614, 467);
            this.ListView_Explorer.TabIndex = 0;
            this.ListView_Explorer.UseCompatibleStateImageBehavior = false;
            this.ListView_Explorer.View = System.Windows.Forms.View.Details;
            this.ListView_Explorer.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.ListView_Explorer_DrawColumnHeader);
            this.ListView_Explorer.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.ListView_Explorer_DrawItem);
            this.ListView_Explorer.Resize += new System.EventHandler(this.ListView_Explorer_Resize);
            // 
            // Column_Name
            // 
            this.Column_Name.Text = "Name";
            this.Column_Name.Width = 272;
            // 
            // Column_DateModified
            // 
            this.Column_DateModified.Text = "Date modified";
            this.Column_DateModified.Width = 143;
            // 
            // Column_Type
            // 
            this.Column_Type.Text = "Type";
            this.Column_Type.Width = 120;
            // 
            // Column_Size
            // 
            this.Column_Size.Text = "Size";
            this.Column_Size.Width = 79;
            // 
            // Column_HeaderSpace
            // 
            this.Column_HeaderSpace.Text = "";
            this.Column_HeaderSpace.Width = 1000;
            // 
            // ListViewExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.Controls.Add(this.SplitContainer_TreeView);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ListViewExplorer";
            this.Size = new System.Drawing.Size(800, 450);
            this.SplitContainer_TreeView.Panel1.ResumeLayout(false);
            this.SplitContainer_TreeView.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_TreeView)).EndInit();
            this.SplitContainer_TreeView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer SplitContainer_TreeView;
        private System.Windows.Forms.TreeView TreeView_Explorer;
        private System.Windows.Forms.ListView ListView_Explorer;
        private System.Windows.Forms.ColumnHeader Column_Name;
        private System.Windows.Forms.ColumnHeader Column_DateModified;
        private System.Windows.Forms.ColumnHeader Column_Type;
        private System.Windows.Forms.ColumnHeader Column_Size;
        private System.Windows.Forms.ColumnHeader Column_HeaderSpace;
        private System.Windows.Forms.ImageList ImageList_Keys;
    }
}
