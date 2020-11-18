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
            this.ButtonFlat_DirectoryTree = new Marathon.Toolkit.Components.ButtonFlat();
            this.ButtonFlat_Clipboard = new Marathon.Toolkit.Components.ButtonFlat();
            this.ButtonFlat_Up = new Marathon.Toolkit.Components.ButtonFlat();
            this.ButtonFlat_Forward = new Marathon.Toolkit.Components.ButtonFlat();
            this.ButtonFlat_Back = new Marathon.Toolkit.Components.ButtonFlat();
            this.TextBox_Address = new System.Windows.Forms.TextBox();
            this.KryptonSplitContainer_Explorer = new ComponentFactory.Krypton.Toolkit.KryptonSplitContainer();
            this.ToolTip_Information = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.KryptonRibbon_MarathonForm)).BeginInit();
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
            // KryptonRibbon_MarathonForm
            // 
            this.KryptonRibbon_MarathonForm.RibbonAppButton.AppButtonShowRecentDocs = false;
            this.KryptonRibbon_MarathonForm.Size = new System.Drawing.Size(969, 142);
            // 
            // WebBrowser_Explorer
            // 
            this.WebBrowser_Explorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebBrowser_Explorer.Location = new System.Drawing.Point(0, 0);
            this.WebBrowser_Explorer.MinimumSize = new System.Drawing.Size(23, 23);
            this.WebBrowser_Explorer.Name = "WebBrowser_Explorer";
            this.WebBrowser_Explorer.Size = new System.Drawing.Size(783, 547);
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
            this.KryptonTreeView_Explorer.Size = new System.Drawing.Size(182, 547);
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
            this.SplitContainer_WebBrowser.Panel2.Controls.Add(this.KryptonSplitContainer_Explorer);
            this.SplitContainer_WebBrowser.Size = new System.Drawing.Size(969, 573);
            this.SplitContainer_WebBrowser.SplitterDistance = 25;
            this.SplitContainer_WebBrowser.SplitterWidth = 1;
            this.SplitContainer_WebBrowser.TabIndex = 0;
            // 
            // ButtonFlat_DirectoryTree
            // 
            this.ButtonFlat_DirectoryTree.BackColor = System.Drawing.Color.Transparent;
            this.ButtonFlat_DirectoryTree.BackgroundImage = global::Marathon.Toolkit.Properties.Resources.MarathonExplorer_DirectoryTree_Enabled;
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
            this.ButtonFlat_DirectoryTree.Click += new System.EventHandler(this.ButtonFlat_Navigation_Click_Group);
            // 
            // ButtonFlat_Clipboard
            // 
            this.ButtonFlat_Clipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonFlat_Clipboard.BackColor = System.Drawing.Color.Transparent;
            this.ButtonFlat_Clipboard.BackgroundImage = global::Marathon.Toolkit.Properties.Resources.MarathonExplorer_Clipboard_Enabled;
            this.ButtonFlat_Clipboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ButtonFlat_Clipboard.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_Clipboard.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ButtonFlat_Clipboard.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.ButtonFlat_Clipboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_Clipboard.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_Clipboard.Location = new System.Drawing.Point(944, 1);
            this.ButtonFlat_Clipboard.Name = "ButtonFlat_Clipboard";
            this.ButtonFlat_Clipboard.Size = new System.Drawing.Size(24, 24);
            this.ButtonFlat_Clipboard.TabIndex = 11;
            this.ToolTip_Information.SetToolTip(this.ButtonFlat_Clipboard, "Copy current address to clipboard");
            this.ButtonFlat_Clipboard.UseVisualStyleBackColor = false;
            this.ButtonFlat_Clipboard.Click += new System.EventHandler(this.ButtonFlat_Navigation_Click_Group);
            // 
            // ButtonFlat_Up
            // 
            this.ButtonFlat_Up.BackColor = System.Drawing.Color.Transparent;
            this.ButtonFlat_Up.BackgroundImage = global::Marathon.Toolkit.Properties.Resources.MarathonExplorer_Up_Enabled;
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
            this.ButtonFlat_Up.Click += new System.EventHandler(this.ButtonFlat_Navigation_Click_Group);
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
            this.ButtonFlat_Forward.Click += new System.EventHandler(this.ButtonFlat_Navigation_Click_Group);
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
            this.ButtonFlat_Back.Click += new System.EventHandler(this.ButtonFlat_Navigation_Click_Group);
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
            this.TextBox_Address.Size = new System.Drawing.Size(836, 24);
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
            this.KryptonSplitContainer_Explorer.Size = new System.Drawing.Size(969, 547);
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
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(969, 573);
            this.Controls.Add(this.SplitContainer_WebBrowser);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MarathonExplorer";
            this.Text = "Marathon Explorer";
            this.UseRibbon = false;
            this.Controls.SetChildIndex(this.SplitContainer_WebBrowser, 0);
            this.Controls.SetChildIndex(this.KryptonRibbon_MarathonForm, 0);
            ((System.ComponentModel.ISupportInitialize)(this.KryptonRibbon_MarathonForm)).EndInit();
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
        private Marathon.Toolkit.Components.ButtonFlat ButtonFlat_Up;
        private Marathon.Toolkit.Components.ButtonFlat ButtonFlat_Forward;
        private Marathon.Toolkit.Components.ButtonFlat ButtonFlat_Back;
        private Marathon.Toolkit.Components.ButtonFlat ButtonFlat_Clipboard;
        private System.Windows.Forms.ToolTip ToolTip_Information;
        private System.Windows.Forms.TextBox TextBox_Address;
        private Components.ButtonFlat ButtonFlat_DirectoryTree;
        private System.Windows.Forms.ImageList ImageList_Keys;
        private ComponentFactory.Krypton.Toolkit.KryptonTreeView KryptonTreeView_Explorer;
        private ComponentFactory.Krypton.Toolkit.KryptonSplitContainer KryptonSplitContainer_Explorer;
    }
}
