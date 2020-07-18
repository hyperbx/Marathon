namespace Marathon
{
    partial class About
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("HyperPolygon64");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Radfordhound");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Knuxfan24");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("GerbilSoft");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.Label_Title = new System.Windows.Forms.Label();
            this.Label_Version = new System.Windows.Forms.Label();
            this.PictureBox_Logo = new System.Windows.Forms.PictureBox();
            this.Label_License = new System.Windows.Forms.Label();
            this.TreeView_Contributors = new System.Windows.Forms.TreeView();
            this.RichTextBox_Contributor = new System.Windows.Forms.RichTextBox();
            this.Panel_RichTextBox_Container = new System.Windows.Forms.Panel();
            this.GroupBox_Contributors = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Logo)).BeginInit();
            this.Panel_RichTextBox_Container.SuspendLayout();
            this.GroupBox_Contributors.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label_Title
            // 
            this.Label_Title.AutoSize = true;
            this.Label_Title.BackColor = System.Drawing.Color.Transparent;
            this.Label_Title.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Title.Location = new System.Drawing.Point(12, 7);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(165, 45);
            this.Label_Title.TabIndex = 1;
            this.Label_Title.Text = "Marathon";
            // 
            // Label_Version
            // 
            this.Label_Version.AutoSize = true;
            this.Label_Version.BackColor = System.Drawing.Color.Transparent;
            this.Label_Version.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Version.Location = new System.Drawing.Point(18, 51);
            this.Label_Version.Name = "Label_Version";
            this.Label_Version.Size = new System.Drawing.Size(96, 21);
            this.Label_Version.TabIndex = 2;
            this.Label_Version.Text = "Placeholder";
            // 
            // PictureBox_Logo
            // 
            this.PictureBox_Logo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBox_Logo.BackgroundImage = global::Marathon.Properties.Resources.Main_LogoCornerTransparent;
            this.PictureBox_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PictureBox_Logo.Location = new System.Drawing.Point(233, 57);
            this.PictureBox_Logo.Name = "PictureBox_Logo";
            this.PictureBox_Logo.Size = new System.Drawing.Size(304, 243);
            this.PictureBox_Logo.TabIndex = 3;
            this.PictureBox_Logo.TabStop = false;
            // 
            // Label_License
            // 
            this.Label_License.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_License.AutoSize = true;
            this.Label_License.BackColor = System.Drawing.Color.Transparent;
            this.Label_License.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.Label_License.Location = new System.Drawing.Point(16, 266);
            this.Label_License.Name = "Label_License";
            this.Label_License.Size = new System.Drawing.Size(203, 19);
            this.Label_License.TabIndex = 4;
            this.Label_License.Text = "Licensed under the MIT License";
            // 
            // TreeView_Contributors
            // 
            this.TreeView_Contributors.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TreeView_Contributors.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeView_Contributors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView_Contributors.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.TreeView_Contributors.ForeColor = System.Drawing.SystemColors.Control;
            this.TreeView_Contributors.ItemHeight = 22;
            this.TreeView_Contributors.Location = new System.Drawing.Point(3, 19);
            this.TreeView_Contributors.Name = "TreeView_Contributors";
            treeNode1.Name = "Node_HyperPolygon64";
            treeNode1.Text = "HyperPolygon64";
            treeNode2.Name = "Node_Radfordhound";
            treeNode2.Text = "Radfordhound";
            treeNode3.Name = "Node_Knuxfan24";
            treeNode3.Text = "Knuxfan24";
            treeNode4.Name = "Node_GerbilSoft";
            treeNode4.Text = "GerbilSoft";
            this.TreeView_Contributors.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            this.TreeView_Contributors.ShowLines = false;
            this.TreeView_Contributors.ShowPlusMinus = false;
            this.TreeView_Contributors.ShowRootLines = false;
            this.TreeView_Contributors.Size = new System.Drawing.Size(188, 154);
            this.TreeView_Contributors.TabIndex = 5;
            this.TreeView_Contributors.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_Contributors_NodeMouseDoubleClick);
            // 
            // RichTextBox_Contributor
            // 
            this.RichTextBox_Contributor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RichTextBox_Contributor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.RichTextBox_Contributor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichTextBox_Contributor.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.RichTextBox_Contributor.ForeColor = System.Drawing.SystemColors.Control;
            this.RichTextBox_Contributor.Location = new System.Drawing.Point(2, 0);
            this.RichTextBox_Contributor.Name = "RichTextBox_Contributor";
            this.RichTextBox_Contributor.ReadOnly = true;
            this.RichTextBox_Contributor.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.RichTextBox_Contributor.ShortcutsEnabled = false;
            this.RichTextBox_Contributor.Size = new System.Drawing.Size(302, 299);
            this.RichTextBox_Contributor.TabIndex = 6;
            this.RichTextBox_Contributor.Text = "";
            // 
            // Panel_RichTextBox_Container
            // 
            this.Panel_RichTextBox_Container.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_RichTextBox_Container.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_RichTextBox_Container.Controls.Add(this.RichTextBox_Contributor);
            this.Panel_RichTextBox_Container.Location = new System.Drawing.Point(233, -1);
            this.Panel_RichTextBox_Container.Name = "Panel_RichTextBox_Container";
            this.Panel_RichTextBox_Container.Size = new System.Drawing.Size(304, 301);
            this.Panel_RichTextBox_Container.TabIndex = 7;
            this.Panel_RichTextBox_Container.Visible = false;
            // 
            // GroupBox_Contributors
            // 
            this.GroupBox_Contributors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GroupBox_Contributors.Controls.Add(this.TreeView_Contributors);
            this.GroupBox_Contributors.ForeColor = System.Drawing.SystemColors.Control;
            this.GroupBox_Contributors.Location = new System.Drawing.Point(20, 82);
            this.GroupBox_Contributors.Name = "GroupBox_Contributors";
            this.GroupBox_Contributors.Size = new System.Drawing.Size(194, 176);
            this.GroupBox_Contributors.TabIndex = 7;
            this.GroupBox_Contributors.TabStop = false;
            this.GroupBox_Contributors.Text = "Contributors";
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(536, 299);
            this.Controls.Add(this.GroupBox_Contributors);
            this.Controls.Add(this.Panel_RichTextBox_Container);
            this.Controls.Add(this.Label_License);
            this.Controls.Add(this.Label_Version);
            this.Controls.Add(this.Label_Title);
            this.Controls.Add(this.PictureBox_Logo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(552, 295);
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Logo)).EndInit();
            this.Panel_RichTextBox_Container.ResumeLayout(false);
            this.GroupBox_Contributors.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Label_Title;
        private System.Windows.Forms.Label Label_Version;
        private System.Windows.Forms.PictureBox PictureBox_Logo;
        private System.Windows.Forms.Label Label_License;
        private System.Windows.Forms.TreeView TreeView_Contributors;
        private System.Windows.Forms.RichTextBox RichTextBox_Contributor;
        private System.Windows.Forms.Panel Panel_RichTextBox_Container;
        private System.Windows.Forms.GroupBox GroupBox_Contributors;
    }
}