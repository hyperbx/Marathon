namespace Marathon.Toolkit.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.Label_Title = new System.Windows.Forms.Label();
            this.Label_Version = new System.Windows.Forms.Label();
            this.PictureBox_Logo = new System.Windows.Forms.PictureBox();
            this.Label_License = new System.Windows.Forms.Label();
            this.TreeView_Contributors = new System.Windows.Forms.TreeView();
            this.Panel_TreeView_Contributors_Border = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Logo)).BeginInit();
            this.Panel_TreeView_Contributors_Border.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label_Title
            // 
            this.Label_Title.AutoSize = true;
            this.Label_Title.BackColor = System.Drawing.Color.Transparent;
            this.Label_Title.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Title.Location = new System.Drawing.Point(12, 7);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(272, 45);
            this.Label_Title.TabIndex = 1;
            this.Label_Title.Text = "Marathon Toolkit";
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
            this.PictureBox_Logo.BackgroundImage = Resources.LoadBitmapResource("Main_Logo_Corner_Transparent");
            this.PictureBox_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PictureBox_Logo.Location = new System.Drawing.Point(233, 14);
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
            this.Label_License.Location = new System.Drawing.Point(16, 223);
            this.Label_License.Name = "Label_License";
            this.Label_License.Size = new System.Drawing.Size(203, 19);
            this.Label_License.TabIndex = 4;
            this.Label_License.Text = "Licensed under the MIT License";
            this.Label_License.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Label_License_MouseDoubleClick);
            // 
            // TreeView_Contributors
            // 
            this.TreeView_Contributors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeView_Contributors.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TreeView_Contributors.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeView_Contributors.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.TreeView_Contributors.ForeColor = System.Drawing.SystemColors.Control;
            this.TreeView_Contributors.ItemHeight = 22;
            this.TreeView_Contributors.Location = new System.Drawing.Point(0, 0);
            this.TreeView_Contributors.Name = "TreeView_Contributors";
            this.TreeView_Contributors.ShowLines = false;
            this.TreeView_Contributors.ShowPlusMinus = false;
            this.TreeView_Contributors.ShowRootLines = false;
            this.TreeView_Contributors.Size = new System.Drawing.Size(219, 96);
            this.TreeView_Contributors.TabIndex = 5;
            this.TreeView_Contributors.TabStop = false;
            this.TreeView_Contributors.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_Contributors_AfterSelect);
            this.TreeView_Contributors.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_Contributors_NodeMouseClick);
            // 
            // Panel_TreeView_Contributors_Border
            // 
            this.Panel_TreeView_Contributors_Border.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_TreeView_Contributors_Border.Controls.Add(this.TreeView_Contributors);
            this.Panel_TreeView_Contributors_Border.Location = new System.Drawing.Point(17, 100);
            this.Panel_TreeView_Contributors_Border.Name = "Panel_TreeView_Contributors_Border";
            this.Panel_TreeView_Contributors_Border.Size = new System.Drawing.Size(202, 96);
            this.Panel_TreeView_Contributors_Border.TabIndex = 6;
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(536, 256);
            this.Controls.Add(this.Label_License);
            this.Controls.Add(this.Label_Version);
            this.Controls.Add(this.Label_Title);
            this.Controls.Add(this.PictureBox_Logo);
            this.Controls.Add(this.Panel_TreeView_Contributors_Border);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(552, 295);
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Logo)).EndInit();
            this.Panel_TreeView_Contributors_Border.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Label_Title;
        private System.Windows.Forms.Label Label_Version;
        private System.Windows.Forms.PictureBox PictureBox_Logo;
        private System.Windows.Forms.Label Label_License;
        private System.Windows.Forms.TreeView TreeView_Contributors;
        private System.Windows.Forms.Panel Panel_TreeView_Contributors_Border;
    }
}
