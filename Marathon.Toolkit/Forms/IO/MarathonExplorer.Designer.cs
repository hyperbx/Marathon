namespace Marathon.Toolkit.Forms
{
    partial class MarathonExplorer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarathonExplorer));
            this.WebBrowser_Explorer = new System.Windows.Forms.WebBrowser();
            this.KryptonTreeView_Explorer = new ComponentFactory.Krypton.Toolkit.KryptonTreeView();
            this.ImageList_Keys = new System.Windows.Forms.ImageList(this.components);
            this.SplitContainer_WebBrowser = new System.Windows.Forms.SplitContainer();
            this.ButtonDark_DirectoryTree = new Marathon.Components.ButtonDark();
            this.ButtonDark_Clipboard = new Marathon.Components.ButtonDark();
            this.ButtonDark_Up = new Marathon.Components.ButtonDark();
            this.ButtonDark_Forward = new Marathon.Components.ButtonDark();
            this.ButtonDark_Back = new Marathon.Components.ButtonDark();
            this.TextBox_Address = new System.Windows.Forms.TextBox();
            this.KryptonSplitContainer_Explorer = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.ToolTip_Information = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.KryptonRibbon_MarathonDockContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_WebBrowser)).BeginInit();
            this.SplitContainer_WebBrowser.Panel1.SuspendLayout();
            this.SplitContainer_WebBrowser.Panel2.SuspendLayout();
            this.SplitContainer_WebBrowser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KryptonSplitContainer_Explorer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KryptonSplitContainer_Explorer.Panel1)).BeginInit();
            this.KryptonSplitContainer_Explorer.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.KryptonSplitContainer_Explorer.Panel2)).BeginInit();
            this.KryptonSplitContainer_Explorer.Panel2.SuspendLayout();
            this.KryptonSplitContainer_Explorer.SuspendLayout();
            this.SuspendLayout();
            // 
            // KryptonRibbon_MarathonDockContent
            // 
            this.KryptonRibbon_MarathonDockContent.RibbonAppButton.AppButtonShowRecentDocs = false;
            // 
            // WebBrowser_Explorer
            // 
            this.WebBrowser_Explorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebBrowser_Explorer.Location = new System.Drawing.Point(0, 0);
            this.WebBrowser_Explorer.MinimumSize = new System.Drawing.Size(23, 23);
            this.WebBrowser_Explorer.Name = "WebBrowser_Explorer";
            this.WebBrowser_Explorer.Size = new System.Drawing.Size(1078, 655);
            this.WebBrowser_Explorer.TabIndex = 0;
            this.WebBrowser_Explorer.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.WebBrowser_Explorer_Navigated);
            // 
            // KryptonTreeView_Explorer
            // 
            this.KryptonTreeView_Explorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.KryptonTreeView_Explorer.ImageIndex = 0;
            this.KryptonTreeView_Explorer.ImageList = this.ImageList_Keys;
            this.KryptonTreeView_Explorer.Location = new System.Drawing.Point(0, 0);
            this.KryptonTreeView_Explorer.Name = "KryptonTreeView_Explorer";
            this.KryptonTreeView_Explorer.SelectedImageIndex = 0;
            this.KryptonTreeView_Explorer.Size = new System.Drawing.Size(182, 655);
            this.KryptonTreeView_Explorer.StateCheckedNormal.Node.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.KryptonTreeView_Explorer.StateCheckedNormal.Node.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.KryptonTreeView_Explorer.StateCheckedNormal.Node.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.KryptonTreeView_Explorer.StateCheckedNormal.Node.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.KryptonTreeView_Explorer.StateCheckedNormal.Node.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.KryptonTreeView_Explorer.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.KryptonTreeView_Explorer.StateCommon.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            this.KryptonTreeView_Explorer.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.KryptonTreeView_Explorer.StateCommon.Node.Content.LongText.Color1 = System.Drawing.SystemColors.Control;
            this.KryptonTreeView_Explorer.StateCommon.Node.Content.LongText.Color2 = System.Drawing.SystemColors.Control;
            this.KryptonTreeView_Explorer.StateCommon.Node.Content.ShortText.Color1 = System.Drawing.SystemColors.Control;
            this.KryptonTreeView_Explorer.StateCommon.Node.Content.ShortText.Color2 = System.Drawing.SystemColors.Control;
            this.KryptonTreeView_Explorer.TabIndex = 4;
            this.KryptonTreeView_Explorer.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.KryptonTreeView_Explorer_AfterExpand);
            this.KryptonTreeView_Explorer.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.KryptonTreeView_Explorer_NodeMouseDoubleClick);
            this.KryptonTreeView_Explorer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.KryptonTreeView_Explorer_MouseDown);
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
            this.SplitContainer_WebBrowser.Panel1.Controls.Add(this.ButtonDark_DirectoryTree);
            this.SplitContainer_WebBrowser.Panel1.Controls.Add(this.ButtonDark_Clipboard);
            this.SplitContainer_WebBrowser.Panel1.Controls.Add(this.ButtonDark_Up);
            this.SplitContainer_WebBrowser.Panel1.Controls.Add(this.ButtonDark_Forward);
            this.SplitContainer_WebBrowser.Panel1.Controls.Add(this.ButtonDark_Back);
            this.SplitContainer_WebBrowser.Panel1.Controls.Add(this.TextBox_Address);
            this.SplitContainer_WebBrowser.Panel1MinSize = 24;
            // 
            // SplitContainer_WebBrowser.Panel2
            // 
            this.SplitContainer_WebBrowser.Panel2.Controls.Add(this.KryptonSplitContainer_Explorer);
            this.SplitContainer_WebBrowser.Size = new System.Drawing.Size(1264, 681);
            this.SplitContainer_WebBrowser.SplitterDistance = 25;
            this.SplitContainer_WebBrowser.SplitterWidth = 1;
            this.SplitContainer_WebBrowser.TabIndex = 0;
            // 
            // ButtonDark_DirectoryTree
            // 
            this.ButtonDark_DirectoryTree.BackColor = System.Drawing.Color.Transparent;
            this.ButtonDark_DirectoryTree.BackgroundImage = global::Marathon.Toolkit.Properties.Resources.MarathonExplorer_DirectoryTree_Enabled;
            this.ButtonDark_DirectoryTree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ButtonDark_DirectoryTree.Checked = false;
            this.ButtonDark_DirectoryTree.FlatAppearance.BorderSize = 0;
            this.ButtonDark_DirectoryTree.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ButtonDark_DirectoryTree.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.ButtonDark_DirectoryTree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDark_DirectoryTree.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonDark_DirectoryTree.FormBackColour = System.Drawing.Color.Empty;
            this.ButtonDark_DirectoryTree.Location = new System.Drawing.Point(55, 1);
            this.ButtonDark_DirectoryTree.Name = "ButtonDark_DirectoryTree";
            this.ButtonDark_DirectoryTree.Size = new System.Drawing.Size(24, 24);
            this.ButtonDark_DirectoryTree.TabIndex = 13;
            this.ToolTip_Information.SetToolTip(this.ButtonDark_DirectoryTree, "Hide directory tree");
            this.ButtonDark_DirectoryTree.UseVisualStyleBackColor = false;
            this.ButtonDark_DirectoryTree.Click += new System.EventHandler(this.ButtonDark_Navigation_Click_Group);
            // 
            // ButtonDark_Clipboard
            // 
            this.ButtonDark_Clipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonDark_Clipboard.BackColor = System.Drawing.Color.Transparent;
            this.ButtonDark_Clipboard.BackgroundImage = global::Marathon.Toolkit.Properties.Resources.MarathonExplorer_Clipboard_Enabled;
            this.ButtonDark_Clipboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ButtonDark_Clipboard.Checked = false;
            this.ButtonDark_Clipboard.FlatAppearance.BorderSize = 0;
            this.ButtonDark_Clipboard.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ButtonDark_Clipboard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.ButtonDark_Clipboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDark_Clipboard.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonDark_Clipboard.FormBackColour = System.Drawing.Color.Empty;
            this.ButtonDark_Clipboard.Location = new System.Drawing.Point(1239, 1);
            this.ButtonDark_Clipboard.Name = "ButtonDark_Clipboard";
            this.ButtonDark_Clipboard.Size = new System.Drawing.Size(24, 24);
            this.ButtonDark_Clipboard.TabIndex = 11;
            this.ToolTip_Information.SetToolTip(this.ButtonDark_Clipboard, "Copy current address to clipboard");
            this.ButtonDark_Clipboard.UseVisualStyleBackColor = false;
            this.ButtonDark_Clipboard.Click += new System.EventHandler(this.ButtonDark_Navigation_Click_Group);
            // 
            // ButtonDark_Up
            // 
            this.ButtonDark_Up.BackColor = System.Drawing.Color.Transparent;
            this.ButtonDark_Up.BackgroundImage = global::Marathon.Toolkit.Properties.Resources.MarathonExplorer_Up_Enabled;
            this.ButtonDark_Up.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonDark_Up.Checked = false;
            this.ButtonDark_Up.FlatAppearance.BorderSize = 0;
            this.ButtonDark_Up.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ButtonDark_Up.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.ButtonDark_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDark_Up.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonDark_Up.FormBackColour = System.Drawing.Color.Empty;
            this.ButtonDark_Up.Location = new System.Drawing.Point(82, 1);
            this.ButtonDark_Up.Name = "ButtonDark_Up";
            this.ButtonDark_Up.Size = new System.Drawing.Size(24, 24);
            this.ButtonDark_Up.TabIndex = 10;
            this.ToolTip_Information.SetToolTip(this.ButtonDark_Up, "Up");
            this.ButtonDark_Up.UseVisualStyleBackColor = false;
            this.ButtonDark_Up.Click += new System.EventHandler(this.ButtonDark_Navigation_Click_Group);
            // 
            // ButtonDark_Forward
            // 
            this.ButtonDark_Forward.BackColor = System.Drawing.Color.Transparent;
            this.ButtonDark_Forward.BackgroundImage = global::Marathon.Toolkit.Properties.Resources.MarathonExplorer_Forward_Disabled;
            this.ButtonDark_Forward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonDark_Forward.Checked = false;
            this.ButtonDark_Forward.Enabled = false;
            this.ButtonDark_Forward.FlatAppearance.BorderSize = 0;
            this.ButtonDark_Forward.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ButtonDark_Forward.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.ButtonDark_Forward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDark_Forward.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonDark_Forward.FormBackColour = System.Drawing.Color.Empty;
            this.ButtonDark_Forward.Location = new System.Drawing.Point(28, 1);
            this.ButtonDark_Forward.Name = "ButtonDark_Forward";
            this.ButtonDark_Forward.Size = new System.Drawing.Size(24, 24);
            this.ButtonDark_Forward.TabIndex = 9;
            this.ToolTip_Information.SetToolTip(this.ButtonDark_Forward, "Forward");
            this.ButtonDark_Forward.UseVisualStyleBackColor = false;
            this.ButtonDark_Forward.Click += new System.EventHandler(this.ButtonDark_Navigation_Click_Group);
            // 
            // ButtonDark_Back
            // 
            this.ButtonDark_Back.BackColor = System.Drawing.Color.Transparent;
            this.ButtonDark_Back.BackgroundImage = global::Marathon.Toolkit.Properties.Resources.MarathonExplorer_Back_Disabled;
            this.ButtonDark_Back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonDark_Back.Checked = false;
            this.ButtonDark_Back.Enabled = false;
            this.ButtonDark_Back.FlatAppearance.BorderSize = 0;
            this.ButtonDark_Back.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ButtonDark_Back.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.ButtonDark_Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDark_Back.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonDark_Back.FormBackColour = System.Drawing.Color.Empty;
            this.ButtonDark_Back.Location = new System.Drawing.Point(1, 1);
            this.ButtonDark_Back.Name = "ButtonDark_Back";
            this.ButtonDark_Back.Size = new System.Drawing.Size(24, 24);
            this.ButtonDark_Back.TabIndex = 8;
            this.ToolTip_Information.SetToolTip(this.ButtonDark_Back, "Back");
            this.ButtonDark_Back.UseVisualStyleBackColor = false;
            this.ButtonDark_Back.Click += new System.EventHandler(this.ButtonDark_Navigation_Click_Group);
            // 
            // TextBox_Address
            // 
            this.TextBox_Address.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox_Address.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.TextBox_Address.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox_Address.Font = new System.Drawing.Font("Segoe UI", 9.1F);
            this.TextBox_Address.ForeColor = System.Drawing.SystemColors.Control;
            this.TextBox_Address.Location = new System.Drawing.Point(107, 1);
            this.TextBox_Address.Name = "TextBox_Address";
            this.TextBox_Address.Size = new System.Drawing.Size(1131, 24);
            this.TextBox_Address.TabIndex = 12;
            this.TextBox_Address.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_Address_KeyDown);
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
            this.KryptonSplitContainer_Explorer.Panel2.Controls.Add(this.WebBrowser_Explorer);
            this.KryptonSplitContainer_Explorer.Size = new System.Drawing.Size(1264, 655);
            this.KryptonSplitContainer_Explorer.SplitterDistance = 182;
            this.KryptonSplitContainer_Explorer.SplitterWidth = 4;
            this.KryptonSplitContainer_Explorer.TabIndex = 1;
            // 
            // ToolTip_Information
            // 
            this.ToolTip_Information.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ToolTip_Information.ForeColor = System.Drawing.SystemColors.Control;
            // 
            // MarathonExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.SplitContainer_WebBrowser);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MarathonExplorer";
            this.Text = "Marathon Explorer";
            this.UseRibbon = false;
            this.Controls.SetChildIndex(this.SplitContainer_WebBrowser, 0);
            this.Controls.SetChildIndex(this.KryptonRibbon_MarathonDockContent, 0);
            ((System.ComponentModel.ISupportInitialize)(this.KryptonRibbon_MarathonDockContent)).EndInit();
            this.SplitContainer_WebBrowser.Panel1.ResumeLayout(false);
            this.SplitContainer_WebBrowser.Panel1.PerformLayout();
            this.SplitContainer_WebBrowser.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer_WebBrowser)).EndInit();
            this.SplitContainer_WebBrowser.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.KryptonSplitContainer_Explorer.Panel1)).EndInit();
            this.KryptonSplitContainer_Explorer.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.KryptonSplitContainer_Explorer.Panel2)).EndInit();
            this.KryptonSplitContainer_Explorer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.KryptonSplitContainer_Explorer)).EndInit();
            this.KryptonSplitContainer_Explorer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser WebBrowser_Explorer;
        private System.Windows.Forms.SplitContainer SplitContainer_WebBrowser;
        private Marathon.Components.ButtonDark ButtonDark_Up;
        private Marathon.Components.ButtonDark ButtonDark_Forward;
        private Marathon.Components.ButtonDark ButtonDark_Back;
        private Marathon.Components.ButtonDark ButtonDark_Clipboard;
        private System.Windows.Forms.ToolTip ToolTip_Information;
        private System.Windows.Forms.TextBox TextBox_Address;
        private Components.ButtonDark ButtonDark_DirectoryTree;
        private System.Windows.Forms.ImageList ImageList_Keys;
        private ComponentFactory.Krypton.Toolkit.KryptonTreeView KryptonTreeView_Explorer;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer KryptonSplitContainer_Explorer;
    }
}
