namespace Sonic_06_Toolkit
{
    partial class DeepSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeepSearch));
            this.list_Search = new System.Windows.Forms.ListBox();
            this.clb_ARCs = new System.Windows.Forms.CheckedListBox();
            this.group_Search = new System.Windows.Forms.GroupBox();
            this.s06PathButton = new System.Windows.Forms.Button();
            this.lbl_ModsDirectory = new System.Windows.Forms.Label();
            this.s06PathBox = new System.Windows.Forms.TextBox();
            this.lbl_GameDirectory = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Search = new System.Windows.Forms.Button();
            this.btn_DeselectAll = new System.Windows.Forms.Button();
            this.btn_SelectAll = new System.Windows.Forms.Button();
            this.group_Search.SuspendLayout();
            this.SuspendLayout();
            // 
            // list_Search
            // 
            this.list_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list_Search.FormattingEnabled = true;
            this.list_Search.HorizontalScrollbar = true;
            this.list_Search.Location = new System.Drawing.Point(174, 83);
            this.list_Search.Name = "list_Search";
            this.list_Search.Size = new System.Drawing.Size(355, 394);
            this.list_Search.TabIndex = 20;
            // 
            // clb_ARCs
            // 
            this.clb_ARCs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clb_ARCs.CheckOnClick = true;
            this.clb_ARCs.FormattingEnabled = true;
            this.clb_ARCs.Location = new System.Drawing.Point(5, 83);
            this.clb_ARCs.Name = "clb_ARCs";
            this.clb_ARCs.Size = new System.Drawing.Size(165, 394);
            this.clb_ARCs.TabIndex = 0;
            this.clb_ARCs.SelectedIndexChanged += new System.EventHandler(this.Clb_ARCs_SelectedIndexChanged);
            // 
            // group_Search
            // 
            this.group_Search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.group_Search.Controls.Add(this.s06PathButton);
            this.group_Search.Controls.Add(this.lbl_ModsDirectory);
            this.group_Search.Controls.Add(this.s06PathBox);
            this.group_Search.Controls.Add(this.lbl_GameDirectory);
            this.group_Search.Controls.Add(this.searchBox);
            this.group_Search.Location = new System.Drawing.Point(5, 3);
            this.group_Search.Name = "group_Search";
            this.group_Search.Size = new System.Drawing.Size(524, 75);
            this.group_Search.TabIndex = 53;
            this.group_Search.TabStop = false;
            this.group_Search.Text = "Search";
            // 
            // s06PathButton
            // 
            this.s06PathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.s06PathButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(171)))), ((int)(((byte)(83)))));
            this.s06PathButton.FlatAppearance.BorderSize = 0;
            this.s06PathButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.s06PathButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.s06PathButton.Location = new System.Drawing.Point(496, 18);
            this.s06PathButton.Name = "s06PathButton";
            this.s06PathButton.Size = new System.Drawing.Size(22, 20);
            this.s06PathButton.TabIndex = 45;
            this.s06PathButton.Text = "...";
            this.s06PathButton.UseVisualStyleBackColor = false;
            this.s06PathButton.Click += new System.EventHandler(this.S06PathButton_Click);
            // 
            // lbl_ModsDirectory
            // 
            this.lbl_ModsDirectory.AutoSize = true;
            this.lbl_ModsDirectory.Location = new System.Drawing.Point(7, 21);
            this.lbl_ModsDirectory.Name = "lbl_ModsDirectory";
            this.lbl_ModsDirectory.Size = new System.Drawing.Size(83, 13);
            this.lbl_ModsDirectory.TabIndex = 44;
            this.lbl_ModsDirectory.Text = "Game Directory:";
            // 
            // s06PathBox
            // 
            this.s06PathBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.s06PathBox.Location = new System.Drawing.Point(92, 18);
            this.s06PathBox.Name = "s06PathBox";
            this.s06PathBox.Size = new System.Drawing.Size(400, 20);
            this.s06PathBox.TabIndex = 43;
            this.s06PathBox.TextChanged += new System.EventHandler(this.S06PathBox_TextChanged);
            // 
            // lbl_GameDirectory
            // 
            this.lbl_GameDirectory.AutoSize = true;
            this.lbl_GameDirectory.Location = new System.Drawing.Point(15, 47);
            this.lbl_GameDirectory.Name = "lbl_GameDirectory";
            this.lbl_GameDirectory.Size = new System.Drawing.Size(75, 13);
            this.lbl_GameDirectory.TabIndex = 38;
            this.lbl_GameDirectory.Text = "Search Query:";
            // 
            // searchBox
            // 
            this.searchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchBox.Location = new System.Drawing.Point(92, 44);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(426, 20);
            this.searchBox.TabIndex = 37;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancel.BackColor = System.Drawing.Color.Tomato;
            this.btn_Cancel.FlatAppearance.BorderSize = 0;
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancel.Location = new System.Drawing.Point(454, 482);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 57;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // btn_Search
            // 
            this.btn_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Search.BackColor = System.Drawing.Color.LightGreen;
            this.btn_Search.Enabled = false;
            this.btn_Search.FlatAppearance.BorderSize = 0;
            this.btn_Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Search.Location = new System.Drawing.Point(373, 482);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(75, 23);
            this.btn_Search.TabIndex = 56;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = false;
            this.btn_Search.Click += new System.EventHandler(this.Btn_Search_Click);
            // 
            // btn_DeselectAll
            // 
            this.btn_DeselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_DeselectAll.BackColor = System.Drawing.Color.Tomato;
            this.btn_DeselectAll.FlatAppearance.BorderSize = 0;
            this.btn_DeselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DeselectAll.Location = new System.Drawing.Point(90, 482);
            this.btn_DeselectAll.Name = "btn_DeselectAll";
            this.btn_DeselectAll.Size = new System.Drawing.Size(80, 23);
            this.btn_DeselectAll.TabIndex = 59;
            this.btn_DeselectAll.Text = "Deselect All";
            this.btn_DeselectAll.UseVisualStyleBackColor = false;
            this.btn_DeselectAll.Click += new System.EventHandler(this.Btn_DeselectAll_Click);
            // 
            // btn_SelectAll
            // 
            this.btn_SelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_SelectAll.BackColor = System.Drawing.Color.SkyBlue;
            this.btn_SelectAll.FlatAppearance.BorderSize = 0;
            this.btn_SelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SelectAll.Location = new System.Drawing.Point(5, 482);
            this.btn_SelectAll.Name = "btn_SelectAll";
            this.btn_SelectAll.Size = new System.Drawing.Size(80, 23);
            this.btn_SelectAll.TabIndex = 58;
            this.btn_SelectAll.Text = "Select All";
            this.btn_SelectAll.UseVisualStyleBackColor = false;
            this.btn_SelectAll.Click += new System.EventHandler(this.Btn_SelectAll_Click);
            // 
            // DeepSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 511);
            this.Controls.Add(this.list_Search);
            this.Controls.Add(this.clb_ARCs);
            this.Controls.Add(this.btn_DeselectAll);
            this.Controls.Add(this.btn_SelectAll);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.group_Search);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(550, 510);
            this.Name = "DeepSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Deep Search";
            this.Load += new System.EventHandler(this.DeepSearch_Load);
            this.group_Search.ResumeLayout(false);
            this.group_Search.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox list_Search;
        private System.Windows.Forms.CheckedListBox clb_ARCs;
        private System.Windows.Forms.GroupBox group_Search;
        private System.Windows.Forms.Button s06PathButton;
        private System.Windows.Forms.Label lbl_ModsDirectory;
        private System.Windows.Forms.TextBox s06PathBox;
        private System.Windows.Forms.Label lbl_GameDirectory;
        private System.Windows.Forms.TextBox searchBox;
        internal System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Search;
        internal System.Windows.Forms.Button btn_DeselectAll;
        private System.Windows.Forms.Button btn_SelectAll;
    }
}