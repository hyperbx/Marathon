namespace Marathon.Toolkit.Forms
{
    partial class BulkRenamer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BulkRenamer));
            this.GroupBox_Criteria = new System.Windows.Forms.GroupBox();
            this.Label_TextBoxDark_Criteria_2 = new System.Windows.Forms.Label();
            this.TextBoxDark_Criteria_2 = new Marathon.Toolkit.Components.TextBoxDark();
            this.Label_TextBoxDark_Criteria_1 = new System.Windows.Forms.Label();
            this.TextBoxDark_Criteria_1 = new Marathon.Toolkit.Components.TextBoxDark();
            this.GroupBox_Properties = new System.Windows.Forms.GroupBox();
            this.FlowLayoutPanel_Properties = new System.Windows.Forms.FlowLayoutPanel();
            this.CheckBox_CreatePrefix = new System.Windows.Forms.CheckBox();
            this.CheckBox_CreateSuffix = new System.Windows.Forms.CheckBox();
            this.CheckBox_AppendSuffixToExtension = new System.Windows.Forms.CheckBox();
            this.CheckBox_IncludeFiles = new System.Windows.Forms.CheckBox();
            this.CheckBox_IncludeDirectories = new System.Windows.Forms.CheckBox();
            this.CheckBox_IncludeSubdirectoryContents = new System.Windows.Forms.CheckBox();
            this.CheckBox_CaseSensitive = new System.Windows.Forms.CheckBox();
            this.CheckBox_MakeUppercase = new System.Windows.Forms.CheckBox();
            this.CheckBox_MakeLowercase = new System.Windows.Forms.CheckBox();
            this.CheckBox_MakeTitlecase = new System.Windows.Forms.CheckBox();
            this.Panel_ListViewDark_Preview = new System.Windows.Forms.Panel();
            this.ListViewDark_Preview = new Marathon.Toolkit.Components.ListViewDark();
            this.Column_Original = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_Renamed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_Space = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ImageList_Keys = new System.Windows.Forms.ImageList(this.components);
            this.ButtonFlat_Rename = new Marathon.Toolkit.Components.ButtonFlat();
            this.GroupBox_Criteria.SuspendLayout();
            this.GroupBox_Properties.SuspendLayout();
            this.FlowLayoutPanel_Properties.SuspendLayout();
            this.Panel_ListViewDark_Preview.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox_Criteria
            // 
            this.GroupBox_Criteria.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_Criteria.Controls.Add(this.Label_TextBoxDark_Criteria_2);
            this.GroupBox_Criteria.Controls.Add(this.TextBoxDark_Criteria_2);
            this.GroupBox_Criteria.Controls.Add(this.Label_TextBoxDark_Criteria_1);
            this.GroupBox_Criteria.Controls.Add(this.TextBoxDark_Criteria_1);
            this.GroupBox_Criteria.ForeColor = System.Drawing.SystemColors.Control;
            this.GroupBox_Criteria.Location = new System.Drawing.Point(12, 6);
            this.GroupBox_Criteria.Name = "GroupBox_Criteria";
            this.GroupBox_Criteria.Size = new System.Drawing.Size(468, 92);
            this.GroupBox_Criteria.TabIndex = 0;
            this.GroupBox_Criteria.TabStop = false;
            this.GroupBox_Criteria.Text = "Criteria";
            // 
            // Label_TextBoxDark_Criteria_2
            // 
            this.Label_TextBoxDark_Criteria_2.Location = new System.Drawing.Point(7, 57);
            this.Label_TextBoxDark_Criteria_2.Name = "Label_TextBoxDark_Criteria_2";
            this.Label_TextBoxDark_Criteria_2.Size = new System.Drawing.Size(42, 15);
            this.Label_TextBoxDark_Criteria_2.TabIndex = 3;
            this.Label_TextBoxDark_Criteria_2.Text = "New:";
            this.Label_TextBoxDark_Criteria_2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TextBoxDark_Criteria_2
            // 
            this.TextBoxDark_Criteria_2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxDark_Criteria_2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.TextBoxDark_Criteria_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxDark_Criteria_2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxDark_Criteria_2.ForeColor = System.Drawing.SystemColors.Control;
            this.TextBoxDark_Criteria_2.Location = new System.Drawing.Point(52, 53);
            this.TextBoxDark_Criteria_2.Name = "TextBoxDark_Criteria_2";
            this.TextBoxDark_Criteria_2.Size = new System.Drawing.Size(402, 23);
            this.TextBoxDark_Criteria_2.TabIndex = 2;
            this.TextBoxDark_Criteria_2.TextChanged += new System.EventHandler(this.TextBoxDark_Criteria_TextChanged_Group);
            // 
            // Label_TextBoxDark_Criteria_1
            // 
            this.Label_TextBoxDark_Criteria_1.AutoSize = true;
            this.Label_TextBoxDark_Criteria_1.Location = new System.Drawing.Point(20, 26);
            this.Label_TextBoxDark_Criteria_1.Name = "Label_TextBoxDark_Criteria_1";
            this.Label_TextBoxDark_Criteria_1.Size = new System.Drawing.Size(29, 15);
            this.Label_TextBoxDark_Criteria_1.TabIndex = 1;
            this.Label_TextBoxDark_Criteria_1.Text = "Old:";
            this.Label_TextBoxDark_Criteria_1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TextBoxDark_Criteria_1
            // 
            this.TextBoxDark_Criteria_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxDark_Criteria_1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.TextBoxDark_Criteria_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxDark_Criteria_1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxDark_Criteria_1.ForeColor = System.Drawing.SystemColors.Control;
            this.TextBoxDark_Criteria_1.Location = new System.Drawing.Point(52, 22);
            this.TextBoxDark_Criteria_1.Name = "TextBoxDark_Criteria_1";
            this.TextBoxDark_Criteria_1.Size = new System.Drawing.Size(402, 23);
            this.TextBoxDark_Criteria_1.TabIndex = 0;
            this.TextBoxDark_Criteria_1.TextChanged += new System.EventHandler(this.TextBoxDark_Criteria_TextChanged_Group);
            // 
            // GroupBox_Properties
            // 
            this.GroupBox_Properties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox_Properties.Controls.Add(this.FlowLayoutPanel_Properties);
            this.GroupBox_Properties.ForeColor = System.Drawing.SystemColors.Control;
            this.GroupBox_Properties.Location = new System.Drawing.Point(12, 104);
            this.GroupBox_Properties.Name = "GroupBox_Properties";
            this.GroupBox_Properties.Size = new System.Drawing.Size(468, 103);
            this.GroupBox_Properties.TabIndex = 1;
            this.GroupBox_Properties.TabStop = false;
            this.GroupBox_Properties.Text = "Properties";
            // 
            // FlowLayoutPanel_Properties
            // 
            this.FlowLayoutPanel_Properties.Controls.Add(this.CheckBox_CreatePrefix);
            this.FlowLayoutPanel_Properties.Controls.Add(this.CheckBox_CreateSuffix);
            this.FlowLayoutPanel_Properties.Controls.Add(this.CheckBox_AppendSuffixToExtension);
            this.FlowLayoutPanel_Properties.Controls.Add(this.CheckBox_IncludeFiles);
            this.FlowLayoutPanel_Properties.Controls.Add(this.CheckBox_IncludeDirectories);
            this.FlowLayoutPanel_Properties.Controls.Add(this.CheckBox_IncludeSubdirectoryContents);
            this.FlowLayoutPanel_Properties.Controls.Add(this.CheckBox_CaseSensitive);
            this.FlowLayoutPanel_Properties.Controls.Add(this.CheckBox_MakeUppercase);
            this.FlowLayoutPanel_Properties.Controls.Add(this.CheckBox_MakeLowercase);
            this.FlowLayoutPanel_Properties.Controls.Add(this.CheckBox_MakeTitlecase);
            this.FlowLayoutPanel_Properties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlowLayoutPanel_Properties.Location = new System.Drawing.Point(3, 19);
            this.FlowLayoutPanel_Properties.Name = "FlowLayoutPanel_Properties";
            this.FlowLayoutPanel_Properties.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.FlowLayoutPanel_Properties.Size = new System.Drawing.Size(462, 81);
            this.FlowLayoutPanel_Properties.TabIndex = 0;
            // 
            // CheckBox_CreatePrefix
            // 
            this.CheckBox_CreatePrefix.AutoSize = true;
            this.CheckBox_CreatePrefix.Location = new System.Drawing.Point(10, 3);
            this.CheckBox_CreatePrefix.Name = "CheckBox_CreatePrefix";
            this.CheckBox_CreatePrefix.Size = new System.Drawing.Size(93, 19);
            this.CheckBox_CreatePrefix.TabIndex = 0;
            this.CheckBox_CreatePrefix.Text = "Create Prefix";
            this.CheckBox_CreatePrefix.UseVisualStyleBackColor = true;
            this.CheckBox_CreatePrefix.CheckedChanged += new System.EventHandler(this.CheckBox_Properties_CheckedChanged_Group);
            // 
            // CheckBox_CreateSuffix
            // 
            this.CheckBox_CreateSuffix.AutoSize = true;
            this.CheckBox_CreateSuffix.Location = new System.Drawing.Point(109, 3);
            this.CheckBox_CreateSuffix.Name = "CheckBox_CreateSuffix";
            this.CheckBox_CreateSuffix.Size = new System.Drawing.Size(93, 19);
            this.CheckBox_CreateSuffix.TabIndex = 1;
            this.CheckBox_CreateSuffix.Text = "Create Suffix";
            this.CheckBox_CreateSuffix.UseVisualStyleBackColor = true;
            this.CheckBox_CreateSuffix.CheckedChanged += new System.EventHandler(this.CheckBox_Properties_CheckedChanged_Group);
            // 
            // CheckBox_AppendSuffixToExtension
            // 
            this.CheckBox_AppendSuffixToExtension.AutoSize = true;
            this.CheckBox_AppendSuffixToExtension.Location = new System.Drawing.Point(208, 3);
            this.CheckBox_AppendSuffixToExtension.Name = "CheckBox_AppendSuffixToExtension";
            this.CheckBox_AppendSuffixToExtension.Size = new System.Drawing.Size(169, 19);
            this.CheckBox_AppendSuffixToExtension.TabIndex = 9;
            this.CheckBox_AppendSuffixToExtension.Text = "Append Suffix to Extension";
            this.CheckBox_AppendSuffixToExtension.UseVisualStyleBackColor = true;
            this.CheckBox_AppendSuffixToExtension.Visible = false;
            this.CheckBox_AppendSuffixToExtension.CheckedChanged += new System.EventHandler(this.CheckBox_Properties_CheckedChanged_Group);
            // 
            // CheckBox_IncludeFiles
            // 
            this.CheckBox_IncludeFiles.AutoSize = true;
            this.CheckBox_IncludeFiles.Checked = true;
            this.CheckBox_IncludeFiles.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckBox_IncludeFiles.Location = new System.Drawing.Point(10, 28);
            this.CheckBox_IncludeFiles.Name = "CheckBox_IncludeFiles";
            this.CheckBox_IncludeFiles.Size = new System.Drawing.Size(91, 19);
            this.CheckBox_IncludeFiles.TabIndex = 5;
            this.CheckBox_IncludeFiles.Text = "Include Files";
            this.CheckBox_IncludeFiles.UseVisualStyleBackColor = true;
            this.CheckBox_IncludeFiles.CheckedChanged += new System.EventHandler(this.CheckBox_Properties_CheckedChanged_Group);
            // 
            // CheckBox_IncludeDirectories
            // 
            this.CheckBox_IncludeDirectories.AutoSize = true;
            this.CheckBox_IncludeDirectories.Location = new System.Drawing.Point(107, 28);
            this.CheckBox_IncludeDirectories.Name = "CheckBox_IncludeDirectories";
            this.CheckBox_IncludeDirectories.Size = new System.Drawing.Size(124, 19);
            this.CheckBox_IncludeDirectories.TabIndex = 2;
            this.CheckBox_IncludeDirectories.Text = "Include Directories";
            this.CheckBox_IncludeDirectories.UseVisualStyleBackColor = true;
            this.CheckBox_IncludeDirectories.CheckedChanged += new System.EventHandler(this.CheckBox_Properties_CheckedChanged_Group);
            // 
            // CheckBox_IncludeSubdirectoryContents
            // 
            this.CheckBox_IncludeSubdirectoryContents.AutoSize = true;
            this.CheckBox_IncludeSubdirectoryContents.Location = new System.Drawing.Point(237, 28);
            this.CheckBox_IncludeSubdirectoryContents.Name = "CheckBox_IncludeSubdirectoryContents";
            this.CheckBox_IncludeSubdirectoryContents.Size = new System.Drawing.Size(186, 19);
            this.CheckBox_IncludeSubdirectoryContents.TabIndex = 3;
            this.CheckBox_IncludeSubdirectoryContents.Text = "Include Subdirectory Contents";
            this.CheckBox_IncludeSubdirectoryContents.UseVisualStyleBackColor = true;
            this.CheckBox_IncludeSubdirectoryContents.CheckedChanged += new System.EventHandler(this.CheckBox_Properties_CheckedChanged_Group);
            // 
            // CheckBox_CaseSensitive
            // 
            this.CheckBox_CaseSensitive.AutoSize = true;
            this.CheckBox_CaseSensitive.Location = new System.Drawing.Point(10, 53);
            this.CheckBox_CaseSensitive.Name = "CheckBox_CaseSensitive";
            this.CheckBox_CaseSensitive.Size = new System.Drawing.Size(100, 19);
            this.CheckBox_CaseSensitive.TabIndex = 4;
            this.CheckBox_CaseSensitive.Text = "Case Sensitive";
            this.CheckBox_CaseSensitive.UseVisualStyleBackColor = true;
            this.CheckBox_CaseSensitive.CheckedChanged += new System.EventHandler(this.CheckBox_Properties_CheckedChanged_Group);
            // 
            // CheckBox_MakeUppercase
            // 
            this.CheckBox_MakeUppercase.AutoSize = true;
            this.CheckBox_MakeUppercase.Location = new System.Drawing.Point(116, 53);
            this.CheckBox_MakeUppercase.Name = "CheckBox_MakeUppercase";
            this.CheckBox_MakeUppercase.Size = new System.Drawing.Size(113, 19);
            this.CheckBox_MakeUppercase.TabIndex = 6;
            this.CheckBox_MakeUppercase.Text = "Make Uppercase";
            this.CheckBox_MakeUppercase.UseVisualStyleBackColor = true;
            this.CheckBox_MakeUppercase.CheckedChanged += new System.EventHandler(this.CheckBox_Properties_CheckedChanged_Group);
            // 
            // CheckBox_MakeLowercase
            // 
            this.CheckBox_MakeLowercase.AutoSize = true;
            this.CheckBox_MakeLowercase.Location = new System.Drawing.Point(235, 53);
            this.CheckBox_MakeLowercase.Name = "CheckBox_MakeLowercase";
            this.CheckBox_MakeLowercase.Size = new System.Drawing.Size(113, 19);
            this.CheckBox_MakeLowercase.TabIndex = 7;
            this.CheckBox_MakeLowercase.Text = "Make Lowercase";
            this.CheckBox_MakeLowercase.UseVisualStyleBackColor = true;
            this.CheckBox_MakeLowercase.CheckedChanged += new System.EventHandler(this.CheckBox_Properties_CheckedChanged_Group);
            // 
            // CheckBox_MakeTitlecase
            // 
            this.CheckBox_MakeTitlecase.AutoSize = true;
            this.CheckBox_MakeTitlecase.Location = new System.Drawing.Point(354, 53);
            this.CheckBox_MakeTitlecase.Name = "CheckBox_MakeTitlecase";
            this.CheckBox_MakeTitlecase.Size = new System.Drawing.Size(103, 19);
            this.CheckBox_MakeTitlecase.TabIndex = 8;
            this.CheckBox_MakeTitlecase.Text = "Make Titlecase";
            this.CheckBox_MakeTitlecase.UseVisualStyleBackColor = true;
            this.CheckBox_MakeTitlecase.CheckedChanged += new System.EventHandler(this.CheckBox_Properties_CheckedChanged_Group);
            // 
            // Panel_ListViewDark_Preview
            // 
            this.Panel_ListViewDark_Preview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_ListViewDark_Preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_ListViewDark_Preview.Controls.Add(this.ListViewDark_Preview);
            this.Panel_ListViewDark_Preview.Location = new System.Drawing.Point(12, 220);
            this.Panel_ListViewDark_Preview.Name = "Panel_ListViewDark_Preview";
            this.Panel_ListViewDark_Preview.Size = new System.Drawing.Size(468, 300);
            this.Panel_ListViewDark_Preview.TabIndex = 1;
            // 
            // ListViewDark_Preview
            // 
            this.ListViewDark_Preview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListViewDark_Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ListViewDark_Preview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListViewDark_Preview.CheckBoxes = true;
            this.ListViewDark_Preview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Column_Original,
            this.Column_Renamed,
            this.Column_Space});
            this.ListViewDark_Preview.ForeColor = System.Drawing.SystemColors.Control;
            this.ListViewDark_Preview.FullRowSelect = true;
            this.ListViewDark_Preview.HideSelection = false;
            this.ListViewDark_Preview.Location = new System.Drawing.Point(-1, 0);
            this.ListViewDark_Preview.Name = "ListViewDark_Preview";
            this.ListViewDark_Preview.OwnerDraw = true;
            this.ListViewDark_Preview.Size = new System.Drawing.Size(467, 315);
            this.ListViewDark_Preview.SmallImageList = this.ImageList_Keys;
            this.ListViewDark_Preview.TabIndex = 0;
            this.ListViewDark_Preview.UseCompatibleStateImageBehavior = false;
            this.ListViewDark_Preview.View = System.Windows.Forms.View.Details;
            // 
            // Column_Original
            // 
            this.Column_Original.Text = "Original";
            this.Column_Original.Width = 234;
            // 
            // Column_Renamed
            // 
            this.Column_Renamed.Text = "Renamed";
            this.Column_Renamed.Width = 234;
            // 
            // Column_Space
            // 
            this.Column_Space.Text = "";
            this.Column_Space.Width = 233;
            // 
            // ImageList_Keys
            // 
            this.ImageList_Keys.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList_Keys.ImageStream")));
            this.ImageList_Keys.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList_Keys.Images.SetKeyName(0, "Folder");
            this.ImageList_Keys.Images.SetKeyName(1, "File");
            // 
            // ButtonFlat_Rename
            // 
            this.ButtonFlat_Rename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonFlat_Rename.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ButtonFlat_Rename.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_Rename.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_Rename.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_Rename.Location = new System.Drawing.Point(12, 531);
            this.ButtonFlat_Rename.Name = "ButtonFlat_Rename";
            this.ButtonFlat_Rename.Size = new System.Drawing.Size(468, 23);
            this.ButtonFlat_Rename.TabIndex = 2;
            this.ButtonFlat_Rename.Text = "Rename";
            this.ButtonFlat_Rename.UseVisualStyleBackColor = false;
            this.ButtonFlat_Rename.Click += new System.EventHandler(this.ButtonFlat_Rename_Click);
            // 
            // BulkRenamer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.ClientSize = new System.Drawing.Size(492, 566);
            this.Controls.Add(this.ButtonFlat_Rename);
            this.Controls.Add(this.Panel_ListViewDark_Preview);
            this.Controls.Add(this.GroupBox_Properties);
            this.Controls.Add(this.GroupBox_Criteria);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(508, 605);
            this.Name = "BulkRenamer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bulk Renamer";
            this.Load += new System.EventHandler(this.BulkRenamer_Load);
            this.GroupBox_Criteria.ResumeLayout(false);
            this.GroupBox_Criteria.PerformLayout();
            this.GroupBox_Properties.ResumeLayout(false);
            this.FlowLayoutPanel_Properties.ResumeLayout(false);
            this.FlowLayoutPanel_Properties.PerformLayout();
            this.Panel_ListViewDark_Preview.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GroupBox_Criteria;
        private System.Windows.Forms.GroupBox GroupBox_Properties;
        private System.Windows.Forms.Label Label_TextBoxDark_Criteria_2;
        private Components.TextBoxDark TextBoxDark_Criteria_2;
        private System.Windows.Forms.Label Label_TextBoxDark_Criteria_1;
        private Components.TextBoxDark TextBoxDark_Criteria_1;
        private System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel_Properties;
        private System.Windows.Forms.CheckBox CheckBox_CreatePrefix;
        private System.Windows.Forms.CheckBox CheckBox_CreateSuffix;
        private System.Windows.Forms.CheckBox CheckBox_IncludeFiles;
        private System.Windows.Forms.CheckBox CheckBox_IncludeDirectories;
        private System.Windows.Forms.CheckBox CheckBox_IncludeSubdirectoryContents;
        private System.Windows.Forms.CheckBox CheckBox_CaseSensitive;
        private System.Windows.Forms.CheckBox CheckBox_MakeUppercase;
        private System.Windows.Forms.CheckBox CheckBox_MakeLowercase;
        private System.Windows.Forms.CheckBox CheckBox_MakeTitlecase;
        private Components.ListViewDark ListViewDark_Preview;
        private System.Windows.Forms.ColumnHeader Column_Original;
        private System.Windows.Forms.ColumnHeader Column_Renamed;
        private System.Windows.Forms.ColumnHeader Column_Space;
        private System.Windows.Forms.Panel Panel_ListViewDark_Preview;
        private System.Windows.Forms.ImageList ImageList_Keys;
        private Components.ButtonFlat ButtonFlat_Rename;
        private System.Windows.Forms.CheckBox CheckBox_AppendSuffixToExtension;
    }
}