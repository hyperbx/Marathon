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
            this.ButtonFlat_TreeView_ShowDirectoryTree = new Marathon.Components.ButtonFlat();
            this.ButtonFlat_TreeView_Clipboard = new Marathon.Components.ButtonFlat();
            this.ButtonFlat_TreeView_Up = new Marathon.Components.ButtonFlat();
            this.ButtonFlat_TreeView_Forward = new Marathon.Components.ButtonFlat();
            this.ButtonFlat_TreeView_Back = new Marathon.Components.ButtonFlat();
            this.TextBox_TreeView_Address = new System.Windows.Forms.TextBox();
            this.TreeView_Explorer = new System.Windows.Forms.TreeView();
            this.ImageList_Keys = new System.Windows.Forms.ImageList(this.components);
            this.SplitContainer_WebBrowser = new System.Windows.Forms.SplitContainer();
            this.ButtonFlat_WebBrowser_ShowDirectoryTree = new Marathon.Components.ButtonFlat();
            this.TextBox_WebBrowser_Address = new System.Windows.Forms.TextBox();
            this.ButtonFlat_WebBrowser_Clipboard = new Marathon.Components.ButtonFlat();
            this.ButtonFlat_WebBrowser_Up = new Marathon.Components.ButtonFlat();
            this.ButtonFlat_WebBrowser_Forward = new Marathon.Components.ButtonFlat();
            this.ButtonFlat_WebBrowser_Back = new Marathon.Components.ButtonFlat();
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
            this.WebBrowser_Explorer.Size = new System.Drawing.Size(533, 379);
            this.WebBrowser_Explorer.TabIndex = 0;
            this.WebBrowser_Explorer.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.WebBrowser_Explorer_Navigated);
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
            this.SplitContainer_TreeView.Panel1.Controls.Add(this.ButtonFlat_TreeView_ShowDirectoryTree);
            this.SplitContainer_TreeView.Panel1.Controls.Add(this.ButtonFlat_TreeView_Clipboard);
            this.SplitContainer_TreeView.Panel1.Controls.Add(this.ButtonFlat_TreeView_Up);
            this.SplitContainer_TreeView.Panel1.Controls.Add(this.ButtonFlat_TreeView_Forward);
            this.SplitContainer_TreeView.Panel1.Controls.Add(this.ButtonFlat_TreeView_Back);
            this.SplitContainer_TreeView.Panel1.Controls.Add(this.TextBox_TreeView_Address);
            this.SplitContainer_TreeView.Panel1.Controls.Add(this.TreeView_Explorer);
            this.SplitContainer_TreeView.Panel1MinSize = 109;
            // 
            // SplitContainer_TreeView.Panel2
            // 
            this.SplitContainer_TreeView.Panel2.Controls.Add(this.SplitContainer_WebBrowser);
            this.SplitContainer_TreeView.Size = new System.Drawing.Size(722, 407);
            this.SplitContainer_TreeView.SplitterDistance = 183;
            this.SplitContainer_TreeView.TabIndex = 1;
            // 
            // ButtonFlat_TreeView_ShowDirectoryTree
            // 
            this.ButtonFlat_TreeView_ShowDirectoryTree.BackColor = System.Drawing.Color.Transparent;
            this.ButtonFlat_TreeView_ShowDirectoryTree.BackgroundImage = global::Marathon.Properties.Resources.WebBrowserExplorer_DirectoryTree_Enabled;
            this.ButtonFlat_TreeView_ShowDirectoryTree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ButtonFlat_TreeView_ShowDirectoryTree.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_TreeView_ShowDirectoryTree.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ButtonFlat_TreeView_ShowDirectoryTree.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.ButtonFlat_TreeView_ShowDirectoryTree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_TreeView_ShowDirectoryTree.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_TreeView_ShowDirectoryTree.Location = new System.Drawing.Point(55, 1);
            this.ButtonFlat_TreeView_ShowDirectoryTree.Name = "ButtonFlat_TreeView_ShowDirectoryTree";
            this.ButtonFlat_TreeView_ShowDirectoryTree.Size = new System.Drawing.Size(24, 24);
            this.ButtonFlat_TreeView_ShowDirectoryTree.TabIndex = 9;
            this.ButtonFlat_TreeView_ShowDirectoryTree.UseVisualStyleBackColor = false;
            this.ButtonFlat_TreeView_ShowDirectoryTree.Click += new System.EventHandler(this.ButtonFlat_ShowDirectoryTree_Click);
            // 
            // ButtonFlat_TreeView_Clipboard
            // 
            this.ButtonFlat_TreeView_Clipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonFlat_TreeView_Clipboard.BackColor = System.Drawing.Color.Transparent;
            this.ButtonFlat_TreeView_Clipboard.BackgroundImage = global::Marathon.Properties.Resources.WebBrowserExplorer_Clipboard_Enabled;
            this.ButtonFlat_TreeView_Clipboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ButtonFlat_TreeView_Clipboard.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_TreeView_Clipboard.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ButtonFlat_TreeView_Clipboard.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.ButtonFlat_TreeView_Clipboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_TreeView_Clipboard.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_TreeView_Clipboard.Location = new System.Drawing.Point(129, 1);
            this.ButtonFlat_TreeView_Clipboard.Name = "ButtonFlat_TreeView_Clipboard";
            this.ButtonFlat_TreeView_Clipboard.Size = new System.Drawing.Size(24, 24);
            this.ButtonFlat_TreeView_Clipboard.TabIndex = 8;
            this.ToolTip_Information.SetToolTip(this.ButtonFlat_TreeView_Clipboard, "Copy current address to clipboard");
            this.ButtonFlat_TreeView_Clipboard.UseVisualStyleBackColor = false;
            this.ButtonFlat_TreeView_Clipboard.Click += new System.EventHandler(this.ButtonFlat_Navigation_Click);
            // 
            // ButtonFlat_TreeView_Up
            // 
            this.ButtonFlat_TreeView_Up.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonFlat_TreeView_Up.BackColor = System.Drawing.Color.Transparent;
            this.ButtonFlat_TreeView_Up.BackgroundImage = global::Marathon.Properties.Resources.WebBrowserExplorer_Up_Enabled;
            this.ButtonFlat_TreeView_Up.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonFlat_TreeView_Up.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_TreeView_Up.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ButtonFlat_TreeView_Up.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.ButtonFlat_TreeView_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_TreeView_Up.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_TreeView_Up.Location = new System.Drawing.Point(156, 1);
            this.ButtonFlat_TreeView_Up.Name = "ButtonFlat_TreeView_Up";
            this.ButtonFlat_TreeView_Up.Size = new System.Drawing.Size(24, 24);
            this.ButtonFlat_TreeView_Up.TabIndex = 7;
            this.ToolTip_Information.SetToolTip(this.ButtonFlat_TreeView_Up, "Up");
            this.ButtonFlat_TreeView_Up.UseVisualStyleBackColor = false;
            this.ButtonFlat_TreeView_Up.Click += new System.EventHandler(this.ButtonFlat_Navigation_Click);
            // 
            // ButtonFlat_TreeView_Forward
            // 
            this.ButtonFlat_TreeView_Forward.BackColor = System.Drawing.Color.Transparent;
            this.ButtonFlat_TreeView_Forward.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonFlat_TreeView_Forward.BackgroundImage")));
            this.ButtonFlat_TreeView_Forward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonFlat_TreeView_Forward.Enabled = false;
            this.ButtonFlat_TreeView_Forward.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_TreeView_Forward.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ButtonFlat_TreeView_Forward.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.ButtonFlat_TreeView_Forward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_TreeView_Forward.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_TreeView_Forward.Location = new System.Drawing.Point(28, 1);
            this.ButtonFlat_TreeView_Forward.Name = "ButtonFlat_TreeView_Forward";
            this.ButtonFlat_TreeView_Forward.Size = new System.Drawing.Size(24, 24);
            this.ButtonFlat_TreeView_Forward.TabIndex = 5;
            this.ToolTip_Information.SetToolTip(this.ButtonFlat_TreeView_Forward, "Forward");
            this.ButtonFlat_TreeView_Forward.UseVisualStyleBackColor = false;
            this.ButtonFlat_TreeView_Forward.Click += new System.EventHandler(this.ButtonFlat_Navigation_Click);
            // 
            // ButtonFlat_TreeView_Back
            // 
            this.ButtonFlat_TreeView_Back.BackColor = System.Drawing.Color.Transparent;
            this.ButtonFlat_TreeView_Back.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonFlat_TreeView_Back.BackgroundImage")));
            this.ButtonFlat_TreeView_Back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonFlat_TreeView_Back.Enabled = false;
            this.ButtonFlat_TreeView_Back.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_TreeView_Back.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ButtonFlat_TreeView_Back.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.ButtonFlat_TreeView_Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_TreeView_Back.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_TreeView_Back.Location = new System.Drawing.Point(1, 1);
            this.ButtonFlat_TreeView_Back.Name = "ButtonFlat_TreeView_Back";
            this.ButtonFlat_TreeView_Back.Size = new System.Drawing.Size(24, 24);
            this.ButtonFlat_TreeView_Back.TabIndex = 4;
            this.ToolTip_Information.SetToolTip(this.ButtonFlat_TreeView_Back, "Back");
            this.ButtonFlat_TreeView_Back.UseVisualStyleBackColor = false;
            this.ButtonFlat_TreeView_Back.Click += new System.EventHandler(this.ButtonFlat_Navigation_Click);
            // 
            // TextBox_TreeView_Address
            // 
            this.TextBox_TreeView_Address.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox_TreeView_Address.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.TextBox_TreeView_Address.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox_TreeView_Address.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBox_TreeView_Address.ForeColor = System.Drawing.SystemColors.Control;
            this.TextBox_TreeView_Address.Location = new System.Drawing.Point(-1, 26);
            this.TextBox_TreeView_Address.Name = "TextBox_TreeView_Address";
            this.TextBox_TreeView_Address.Size = new System.Drawing.Size(183, 23);
            this.TextBox_TreeView_Address.TabIndex = 3;
            this.TextBox_TreeView_Address.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_Address_KeyDown);
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
            this.TreeView_Explorer.Location = new System.Drawing.Point(-1, 53);
            this.TreeView_Explorer.Name = "TreeView_Explorer";
            this.TreeView_Explorer.SelectedImageIndex = 0;
            this.TreeView_Explorer.Size = new System.Drawing.Size(183, 353);
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
            this.SplitContainer_WebBrowser.Panel1.Controls.Add(this.ButtonFlat_WebBrowser_ShowDirectoryTree);
            this.SplitContainer_WebBrowser.Panel1.Controls.Add(this.TextBox_WebBrowser_Address);
            this.SplitContainer_WebBrowser.Panel1.Controls.Add(this.ButtonFlat_WebBrowser_Clipboard);
            this.SplitContainer_WebBrowser.Panel1.Controls.Add(this.ButtonFlat_WebBrowser_Up);
            this.SplitContainer_WebBrowser.Panel1.Controls.Add(this.ButtonFlat_WebBrowser_Forward);
            this.SplitContainer_WebBrowser.Panel1.Controls.Add(this.ButtonFlat_WebBrowser_Back);
            this.SplitContainer_WebBrowser.Panel1MinSize = 24;
            // 
            // SplitContainer_WebBrowser.Panel2
            // 
            this.SplitContainer_WebBrowser.Panel2.Controls.Add(this.WebBrowser_Explorer);
            this.SplitContainer_WebBrowser.Size = new System.Drawing.Size(533, 405);
            this.SplitContainer_WebBrowser.SplitterDistance = 25;
            this.SplitContainer_WebBrowser.SplitterWidth = 1;
            this.SplitContainer_WebBrowser.TabIndex = 0;
            // 
            // ButtonFlat_WebBrowser_ShowDirectoryTree
            // 
            this.ButtonFlat_WebBrowser_ShowDirectoryTree.BackColor = System.Drawing.Color.Transparent;
            this.ButtonFlat_WebBrowser_ShowDirectoryTree.BackgroundImage = global::Marathon.Properties.Resources.WebBrowserExplorer_DirectoryTree_Enabled;
            this.ButtonFlat_WebBrowser_ShowDirectoryTree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ButtonFlat_WebBrowser_ShowDirectoryTree.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_WebBrowser_ShowDirectoryTree.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ButtonFlat_WebBrowser_ShowDirectoryTree.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.ButtonFlat_WebBrowser_ShowDirectoryTree.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_WebBrowser_ShowDirectoryTree.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_WebBrowser_ShowDirectoryTree.Location = new System.Drawing.Point(55, 1);
            this.ButtonFlat_WebBrowser_ShowDirectoryTree.Name = "ButtonFlat_WebBrowser_ShowDirectoryTree";
            this.ButtonFlat_WebBrowser_ShowDirectoryTree.Size = new System.Drawing.Size(24, 24);
            this.ButtonFlat_WebBrowser_ShowDirectoryTree.TabIndex = 13;
            this.ButtonFlat_WebBrowser_ShowDirectoryTree.UseVisualStyleBackColor = false;
            this.ButtonFlat_WebBrowser_ShowDirectoryTree.Click += new System.EventHandler(this.ButtonFlat_ShowDirectoryTree_Click);
            // 
            // TextBox_WebBrowser_Address
            // 
            this.TextBox_WebBrowser_Address.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox_WebBrowser_Address.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.TextBox_WebBrowser_Address.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox_WebBrowser_Address.Font = new System.Drawing.Font("Segoe UI", 9.1F);
            this.TextBox_WebBrowser_Address.ForeColor = System.Drawing.SystemColors.Control;
            this.TextBox_WebBrowser_Address.Location = new System.Drawing.Point(82, 1);
            this.TextBox_WebBrowser_Address.Name = "TextBox_WebBrowser_Address";
            this.TextBox_WebBrowser_Address.Size = new System.Drawing.Size(396, 24);
            this.TextBox_WebBrowser_Address.TabIndex = 12;
            this.TextBox_WebBrowser_Address.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_Address_KeyDown);
            // 
            // ButtonFlat_WebBrowser_Clipboard
            // 
            this.ButtonFlat_WebBrowser_Clipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonFlat_WebBrowser_Clipboard.BackColor = System.Drawing.Color.Transparent;
            this.ButtonFlat_WebBrowser_Clipboard.BackgroundImage = global::Marathon.Properties.Resources.WebBrowserExplorer_Clipboard_Enabled;
            this.ButtonFlat_WebBrowser_Clipboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ButtonFlat_WebBrowser_Clipboard.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_WebBrowser_Clipboard.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ButtonFlat_WebBrowser_Clipboard.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.ButtonFlat_WebBrowser_Clipboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_WebBrowser_Clipboard.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_WebBrowser_Clipboard.Location = new System.Drawing.Point(481, 1);
            this.ButtonFlat_WebBrowser_Clipboard.Name = "ButtonFlat_WebBrowser_Clipboard";
            this.ButtonFlat_WebBrowser_Clipboard.Size = new System.Drawing.Size(24, 24);
            this.ButtonFlat_WebBrowser_Clipboard.TabIndex = 11;
            this.ToolTip_Information.SetToolTip(this.ButtonFlat_WebBrowser_Clipboard, "Copy current address to clipboard");
            this.ButtonFlat_WebBrowser_Clipboard.UseVisualStyleBackColor = false;
            this.ButtonFlat_WebBrowser_Clipboard.Click += new System.EventHandler(this.ButtonFlat_Navigation_Click);
            // 
            // ButtonFlat_WebBrowser_Up
            // 
            this.ButtonFlat_WebBrowser_Up.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonFlat_WebBrowser_Up.BackColor = System.Drawing.Color.Transparent;
            this.ButtonFlat_WebBrowser_Up.BackgroundImage = global::Marathon.Properties.Resources.WebBrowserExplorer_Up_Enabled;
            this.ButtonFlat_WebBrowser_Up.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonFlat_WebBrowser_Up.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_WebBrowser_Up.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ButtonFlat_WebBrowser_Up.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.ButtonFlat_WebBrowser_Up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_WebBrowser_Up.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_WebBrowser_Up.Location = new System.Drawing.Point(508, 1);
            this.ButtonFlat_WebBrowser_Up.Name = "ButtonFlat_WebBrowser_Up";
            this.ButtonFlat_WebBrowser_Up.Size = new System.Drawing.Size(24, 24);
            this.ButtonFlat_WebBrowser_Up.TabIndex = 10;
            this.ToolTip_Information.SetToolTip(this.ButtonFlat_WebBrowser_Up, "Up");
            this.ButtonFlat_WebBrowser_Up.UseVisualStyleBackColor = false;
            this.ButtonFlat_WebBrowser_Up.Click += new System.EventHandler(this.ButtonFlat_Navigation_Click);
            // 
            // ButtonFlat_WebBrowser_Forward
            // 
            this.ButtonFlat_WebBrowser_Forward.BackColor = System.Drawing.Color.Transparent;
            this.ButtonFlat_WebBrowser_Forward.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonFlat_WebBrowser_Forward.BackgroundImage")));
            this.ButtonFlat_WebBrowser_Forward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonFlat_WebBrowser_Forward.Enabled = false;
            this.ButtonFlat_WebBrowser_Forward.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_WebBrowser_Forward.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ButtonFlat_WebBrowser_Forward.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.ButtonFlat_WebBrowser_Forward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_WebBrowser_Forward.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_WebBrowser_Forward.Location = new System.Drawing.Point(28, 1);
            this.ButtonFlat_WebBrowser_Forward.Name = "ButtonFlat_WebBrowser_Forward";
            this.ButtonFlat_WebBrowser_Forward.Size = new System.Drawing.Size(24, 24);
            this.ButtonFlat_WebBrowser_Forward.TabIndex = 9;
            this.ToolTip_Information.SetToolTip(this.ButtonFlat_WebBrowser_Forward, "Forward");
            this.ButtonFlat_WebBrowser_Forward.UseVisualStyleBackColor = false;
            this.ButtonFlat_WebBrowser_Forward.Click += new System.EventHandler(this.ButtonFlat_Navigation_Click);
            // 
            // ButtonFlat_WebBrowser_Back
            // 
            this.ButtonFlat_WebBrowser_Back.BackColor = System.Drawing.Color.Transparent;
            this.ButtonFlat_WebBrowser_Back.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ButtonFlat_WebBrowser_Back.BackgroundImage")));
            this.ButtonFlat_WebBrowser_Back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonFlat_WebBrowser_Back.Enabled = false;
            this.ButtonFlat_WebBrowser_Back.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_WebBrowser_Back.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ButtonFlat_WebBrowser_Back.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDark;
            this.ButtonFlat_WebBrowser_Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_WebBrowser_Back.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_WebBrowser_Back.Location = new System.Drawing.Point(1, 1);
            this.ButtonFlat_WebBrowser_Back.Name = "ButtonFlat_WebBrowser_Back";
            this.ButtonFlat_WebBrowser_Back.Size = new System.Drawing.Size(24, 24);
            this.ButtonFlat_WebBrowser_Back.TabIndex = 8;
            this.ToolTip_Information.SetToolTip(this.ButtonFlat_WebBrowser_Back, "Back");
            this.ButtonFlat_WebBrowser_Back.UseVisualStyleBackColor = false;
            this.ButtonFlat_WebBrowser_Back.Click += new System.EventHandler(this.ButtonFlat_Navigation_Click);
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
            this.Controls.Add(this.SplitContainer_TreeView);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "WebBrowserExplorer";
            this.Size = new System.Drawing.Size(720, 405);
            this.Load += new System.EventHandler(this.WebBrowserExplorer_Load);
            this.SplitContainer_TreeView.Panel1.ResumeLayout(false);
            this.SplitContainer_TreeView.Panel1.PerformLayout();
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
        private System.Windows.Forms.TextBox TextBox_TreeView_Address;
        private System.Windows.Forms.TreeView TreeView_Explorer;
        private Marathon.Components.ButtonFlat ButtonFlat_TreeView_Up;
        private Marathon.Components.ButtonFlat ButtonFlat_TreeView_Forward;
        private Marathon.Components.ButtonFlat ButtonFlat_TreeView_Back;
        private System.Windows.Forms.SplitContainer SplitContainer_WebBrowser;
        private Marathon.Components.ButtonFlat ButtonFlat_WebBrowser_Up;
        private Marathon.Components.ButtonFlat ButtonFlat_WebBrowser_Forward;
        private Marathon.Components.ButtonFlat ButtonFlat_WebBrowser_Back;
        private Marathon.Components.ButtonFlat ButtonFlat_TreeView_Clipboard;
        private Marathon.Components.ButtonFlat ButtonFlat_WebBrowser_Clipboard;
        private System.Windows.Forms.ToolTip ToolTip_Information;
        private System.Windows.Forms.TextBox TextBox_WebBrowser_Address;
        private Components.ButtonFlat ButtonFlat_TreeView_ShowDirectoryTree;
        private Components.ButtonFlat ButtonFlat_WebBrowser_ShowDirectoryTree;
        private System.Windows.Forms.ImageList ImageList_Keys;
    }
}
