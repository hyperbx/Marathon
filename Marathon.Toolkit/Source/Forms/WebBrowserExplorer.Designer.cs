namespace Marathon.Controls
{
    partial class WebBrowserExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebBrowserExplorer));
            this.WebBrowser_Explorer = new System.Windows.Forms.WebBrowser();
            this.SplitContainer_TreeView = new System.Windows.Forms.SplitContainer();
            this.TreeView_Explorer = new System.Windows.Forms.TreeView();
            this.ImageList_Keys = new System.Windows.Forms.ImageList(this.components);
            this.SplitContainer_WebBrowser = new System.Windows.Forms.SplitContainer();
            this.ButtonFlat_DirectoryTree = new Marathon.Components.ButtonFlat();
            this.ButtonFlat_Clipboard = new Marathon.Components.ButtonFlat();
            this.ButtonFlat_Up = new Marathon.Components.ButtonFlat();
            this.ButtonFlat_Forward = new Marathon.Components.ButtonFlat();
            this.ButtonFlat_Back = new Marathon.Components.ButtonFlat();
            this.TextBox_Address = new System.Windows.Forms.TextBox();
            this.ToolTip_Information = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_TreeView)).BeginInit();
            this.SplitContainer_TreeView.Panel1.SuspendLayout();
            this.SplitContainer_TreeView.Panel2.SuspendLayout();
            this.SplitContainer_TreeView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_WebBrowser)).BeginInit();
            this.SplitContainer_WebBrowser.Panel1.SuspendLayout();
            this.SplitContainer_WebBrowser.Panel2.SuspendLayout();
            this.SplitContainer_WebBrowser.SuspendLayout();
            this.SuspendLayout();
            // 
            // WebBrowser_Explorer
            // 
            this.WebBrowser_Explorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebBrowser_Explorer.Location = new System.Drawing.Point(0, 0);
            this.WebBrowser_Explorer.MinimumSize = new System.Drawing.Size(23, 23);
            this.WebBrowser_Explorer.Name = "WebBrowser_Explorer";
            this.WebBrowser_Explorer.Size = new System.Drawing.Size(517, 340);
            this.WebBrowser_Explorer.TabIndex = 0;
            this.WebBrowser_Explorer.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.WebBrowser_Explorer_Navigated);
            // 
            // SplitContainer_TreeView
            // 
            this.SplitContainer_TreeView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.SplitContainer_TreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer_TreeView.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SplitContainer_TreeView.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer_TreeView.Name = "SplitContainer_TreeView";
            // 
            // SplitContainer_TreeView.Panel1
            // 
            this.SplitContainer_TreeView.Panel1.Controls.Add(this.TreeView_Explorer);
            this.SplitContainer_TreeView.Panel1MinSize = 109;
            // 
            // SplitContainer_TreeView.Panel2
            // 
            this.SplitContainer_TreeView.Panel2.Controls.Add(this.WebBrowser_Explorer);
            this.SplitContainer_TreeView.Size = new System.Drawing.Size(704, 340);
            this.SplitContainer_TreeView.SplitterDistance = 183;
            this.SplitContainer_TreeView.TabIndex = 1;
            // 
            // TreeView_Explorer
            // 
            this.TreeView_Explorer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeView_Explorer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TreeView_Explorer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TreeView_Explorer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TreeView_Explorer.ForeColor = System.Drawing.SystemColors.Control;
            this.TreeView_Explorer.ImageIndex = 0;
            this.TreeView_Explorer.ImageList = this.ImageList_Keys;
            this.TreeView_Explorer.Location = new System.Drawing.Point(-1, -1);
            this.TreeView_Explorer.Name = "TreeView_Explorer";
            this.TreeView_Explorer.SelectedImageIndex = 0;
            this.TreeView_Explorer.Size = new System.Drawing.Size(184, 342);
            this.TreeView_Explorer.TabIndex = 2;
            this.TreeView_Explorer.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_Explorer_AfterExpand);
            this.TreeView_Explorer.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_Explorer_NodeMouseDoubleClick);
            this.TreeView_Explorer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeView_Explorer_MouseDown);
            // 
            // ImageList_Keys
            // 
            this.ImageList_Keys.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList_Keys.ImageStream")));
            this.ImageList_Keys.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList_Keys.Images.SetKeyName(0, "Folder");
            // 
            // SplitContainer_WebBrowser
            // 
            this.SplitContainer_WebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer_WebBrowser.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SplitContainer_WebBrowser.IsSplitterFixed = true;
            this.SplitContainer_WebBrowser.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer_WebBrowser.Name = "SplitContainer_WebBrowser";
            this.SplitContainer_WebBrowser.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainer_WebBrowser.Panel1
            // 
            this.SplitContainer_WebBrowser.Panel1.Controls.Add(this.ButtonFlat_DirectoryTree);
            this.SplitContainer_WebBrowser.Panel1.Controls.Add(this.ButtonFlat_Clipboard);
            this.SplitContainer_WebBrowser.Panel1.Controls.Add(this.ButtonFlat_Up);
            this.SplitContainer_WebBrowser.Panel1.Controls.Add(this.ButtonFlat_Forward);
            this.SplitContainer_WebBrowser.Panel1.Controls.Add(this.ButtonFlat_Back);
            this.SplitContainer_WebBrowser.Panel1.Controls.Add(this.TextBox_Address);
            this.SplitContainer_WebBrowser.Panel1MinSize = 24;
            // 
            // SplitContainer_WebBrowser.Panel2
            // 
            this.SplitContainer_WebBrowser.Panel2.Controls.Add(this.SplitContainer_TreeView);
            this.SplitContainer_WebBrowser.Size = new System.Drawing.Size(704, 366);
            this.SplitContainer_WebBrowser.SplitterDistance = 25;
            this.SplitContainer_WebBrowser.SplitterWidth = 1;
            this.SplitContainer_WebBrowser.TabIndex = 0;
            // 
            // ButtonFlat_DirectoryTree
            // 
            this.ButtonFlat_DirectoryTree.BackColor = System.Drawing.Color.Transparent;
            this.ButtonFlat_DirectoryTree.BackgroundImage = global::Marathon.Properties.Resources.WebBrowserExplorer_DirectoryTree_Enabled;
            this.ButtonFlat_DirectoryTree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ButtonFlat_DirectoryTree.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_DirectoryTree.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ButtonFlat_DirectoryTree.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.ButtonFlat_DirectoryTree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_DirectoryTree.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_DirectoryTree.Location = new System.Drawing.Point(55, 1);
            this.ButtonFlat_DirectoryTree.Name = "ButtonFlat_DirectoryTree";
            this.ButtonFlat_DirectoryTree.Size = new System.Drawing.Size(24, 24);
            this.ButtonFlat_DirectoryTree.TabIndex = 13;
            this.ToolTip_Information.SetToolTip(this.ButtonFlat_DirectoryTree, "Hide directory tree");
            this.ButtonFlat_DirectoryTree.UseVisualStyleBackColor = false;
            this.ButtonFlat_DirectoryTree.Click += new System.EventHandler(this.ButtonFlat_Navigation_Click);
            // 
            // ButtonFlat_Clipboard
            // 
            this.ButtonFlat_Clipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonFlat_Clipboard.BackColor = System.Drawing.Color.Transparent;
            this.ButtonFlat_Clipboard.BackgroundImage = global::Marathon.Properties.Resources.WebBrowserExplorer_Clipboard_Enabled;
            this.ButtonFlat_Clipboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ButtonFlat_Clipboard.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_Clipboard.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ButtonFlat_Clipboard.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.ButtonFlat_Clipboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_Clipboard.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_Clipboard.Location = new System.Drawing.Point(679, 1);
            this.ButtonFlat_Clipboard.Name = "ButtonFlat_Clipboard";
            this.ButtonFlat_Clipboard.Size = new System.Drawing.Size(24, 24);
            this.ButtonFlat_Clipboard.TabIndex = 11;
            this.ToolTip_Information.SetToolTip(this.ButtonFlat_Clipboard, "Copy current address to clipboard");
            this.ButtonFlat_Clipboard.UseVisualStyleBackColor = false;
            this.ButtonFlat_Clipboard.Click += new System.EventHandler(this.ButtonFlat_Navigation_Click);
            // 
            // ButtonFlat_Up
            // 
            this.ButtonFlat_Up.BackColor = System.Drawing.Color.Transparent;
            this.ButtonFlat_Up.BackgroundImage = global::Marathon.Properties.Resources.WebBrowserExplorer_Up_Enabled;
            this.ButtonFlat_Up.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonFlat_Up.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_Up.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ButtonFlat_Up.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.ButtonFlat_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_Up.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_Up.Location = new System.Drawing.Point(82, 1);
            this.ButtonFlat_Up.Name = "ButtonFlat_Up";
            this.ButtonFlat_Up.Size = new System.Drawing.Size(24, 24);
            this.ButtonFlat_Up.TabIndex = 10;
            this.ToolTip_Information.SetToolTip(this.ButtonFlat_Up, "Up");
            this.ButtonFlat_Up.UseVisualStyleBackColor = false;
            this.ButtonFlat_Up.Click += new System.EventHandler(this.ButtonFlat_Navigation_Click);
            // 
            // ButtonFlat_Forward
            // 
            this.ButtonFlat_Forward.BackColor = System.Drawing.Color.Transparent;
            this.ButtonFlat_Forward.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonFlat_Forward.BackgroundImage")));
            this.ButtonFlat_Forward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonFlat_Forward.Enabled = false;
            this.ButtonFlat_Forward.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_Forward.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ButtonFlat_Forward.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.ButtonFlat_Forward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_Forward.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_Forward.Location = new System.Drawing.Point(28, 1);
            this.ButtonFlat_Forward.Name = "ButtonFlat_Forward";
            this.ButtonFlat_Forward.Size = new System.Drawing.Size(24, 24);
            this.ButtonFlat_Forward.TabIndex = 9;
            this.ToolTip_Information.SetToolTip(this.ButtonFlat_Forward, "Forward");
            this.ButtonFlat_Forward.UseVisualStyleBackColor = false;
            this.ButtonFlat_Forward.Click += new System.EventHandler(this.ButtonFlat_Navigation_Click);
            // 
            // ButtonFlat_Back
            // 
            this.ButtonFlat_Back.BackColor = System.Drawing.Color.Transparent;
            this.ButtonFlat_Back.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonFlat_Back.BackgroundImage")));
            this.ButtonFlat_Back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonFlat_Back.Enabled = false;
            this.ButtonFlat_Back.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_Back.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ButtonFlat_Back.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.ButtonFlat_Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_Back.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_Back.Location = new System.Drawing.Point(1, 1);
            this.ButtonFlat_Back.Name = "ButtonFlat_Back";
            this.ButtonFlat_Back.Size = new System.Drawing.Size(24, 24);
            this.ButtonFlat_Back.TabIndex = 8;
            this.ToolTip_Information.SetToolTip(this.ButtonFlat_Back, "Back");
            this.ButtonFlat_Back.UseVisualStyleBackColor = false;
            this.ButtonFlat_Back.Click += new System.EventHandler(this.ButtonFlat_Navigation_Click);
            // 
            // TextBox_Address
            // 
            this.TextBox_Address.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox_Address.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.TextBox_Address.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox_Address.Font = new System.Drawing.Font("Segoe UI", 9.1F);
            this.TextBox_Address.ForeColor = System.Drawing.SystemColors.Control;
            this.TextBox_Address.Location = new System.Drawing.Point(109, 1);
            this.TextBox_Address.Name = "TextBox_Address";
            this.TextBox_Address.Size = new System.Drawing.Size(567, 24);
            this.TextBox_Address.TabIndex = 12;
            this.TextBox_Address.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_Address_KeyDown);
            // 
            // ToolTip_Information
            // 
            this.ToolTip_Information.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ToolTip_Information.ForeColor = System.Drawing.SystemColors.Control;
            // 
            // WebBrowserExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(704, 366);
            this.Controls.Add(this.SplitContainer_WebBrowser);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "WebBrowserExplorer";
            this.Text = "Explorer ";
            this.Load += new System.EventHandler(this.WebBrowserExplorer_Load);
            this.SplitContainer_TreeView.Panel1.ResumeLayout(false);
            this.SplitContainer_TreeView.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_TreeView)).EndInit();
            this.SplitContainer_TreeView.ResumeLayout(false);
            this.SplitContainer_WebBrowser.Panel1.ResumeLayout(false);
            this.SplitContainer_WebBrowser.Panel1.PerformLayout();
            this.SplitContainer_WebBrowser.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_WebBrowser)).EndInit();
            this.SplitContainer_WebBrowser.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser WebBrowser_Explorer;
        private System.Windows.Forms.SplitContainer SplitContainer_TreeView;
        private System.Windows.Forms.TreeView TreeView_Explorer;
        private System.Windows.Forms.SplitContainer SplitContainer_WebBrowser;
        private Marathon.Components.ButtonFlat ButtonFlat_Up;
        private Marathon.Components.ButtonFlat ButtonFlat_Forward;
        private Marathon.Components.ButtonFlat ButtonFlat_Back;
        private Marathon.Components.ButtonFlat ButtonFlat_Clipboard;
        private System.Windows.Forms.ToolTip ToolTip_Information;
        private System.Windows.Forms.TextBox TextBox_Address;
        private Components.ButtonFlat ButtonFlat_DirectoryTree;
        private System.Windows.Forms.ImageList ImageList_Keys;
    }
}
