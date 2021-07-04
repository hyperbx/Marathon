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
            this.ImageList_Keys = new System.Windows.Forms.ImageList(this.components);
            this.GroupContainer_Properties = new Marathon.Components.GroupContainer();
            this.FlowLayoutPanel_Properties = new System.Windows.Forms.FlowLayoutPanel();
            this.CheckBoxDark_CreatePrefix = new Marathon.Components.CheckBoxDark();
            this.CheckBoxDark_CreateSuffix = new Marathon.Components.CheckBoxDark();
            this.CheckBoxDark_AppendSuffixToExtension = new Marathon.Components.CheckBoxDark();
            this.CheckBoxDark_IncludeFiles = new Marathon.Components.CheckBoxDark();
            this.CheckBoxDark_IncludeDirectories = new Marathon.Components.CheckBoxDark();
            this.CheckBoxDark_IncludeSubdirectoryContents = new Marathon.Components.CheckBoxDark();
            this.CheckBoxDark_CaseSensitive = new Marathon.Components.CheckBoxDark();
            this.CheckBoxDark_MakeUppercase = new Marathon.Components.CheckBoxDark();
            this.CheckBoxDark_MakeLowercase = new Marathon.Components.CheckBoxDark();
            this.CheckBoxDark_MakeTitlecase = new Marathon.Components.CheckBoxDark();
            this.GroupContainer_Criteria = new Marathon.Components.GroupContainer();
            this.TextBoxDark_Criteria_1 = new Marathon.Components.TextBoxDark();
            this.TextBoxDark_Criteria_2 = new Marathon.Components.TextBoxDark();
            this.ButtonDark_Rename = new Marathon.Components.ButtonDark();
            this.GroupContainer_Preview = new Marathon.Components.GroupContainer();
            this.MarathonListView_Preview = new Marathon.Components.MarathonListView();
            this.OlvColumn_Original = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.OlvColumn_Renamed = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.GroupContainer_Properties.WorkingArea.SuspendLayout();
            this.GroupContainer_Properties.SuspendLayout();
            this.FlowLayoutPanel_Properties.SuspendLayout();
            this.GroupContainer_Criteria.WorkingArea.SuspendLayout();
            this.GroupContainer_Criteria.SuspendLayout();
            this.GroupContainer_Preview.WorkingArea.SuspendLayout();
            this.GroupContainer_Preview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MarathonListView_Preview.ListView)).BeginInit();
            this.SuspendLayout();
            // 
            // ImageList_Keys
            // 
            this.ImageList_Keys.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList_Keys.ImageStream")));
            this.ImageList_Keys.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList_Keys.Images.SetKeyName(0, "Folder");
            this.ImageList_Keys.Images.SetKeyName(1, "File");
            // 
            // GroupContainer_Properties
            // 
            this.GroupContainer_Properties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupContainer_Properties.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupContainer_Properties.BorderColour = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.GroupContainer_Properties.HeaderText = "Properties";
            this.GroupContainer_Properties.Location = new System.Drawing.Point(12, 133);
            this.GroupContainer_Properties.Name = "GroupContainer_Properties";
            this.GroupContainer_Properties.Size = new System.Drawing.Size(468, 116);
            this.GroupContainer_Properties.TabIndex = 4;
            // 
            // GroupContainer_Properties.WorkingArea
            // 
            this.GroupContainer_Properties.WorkingArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupContainer_Properties.WorkingArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.GroupContainer_Properties.WorkingArea.Controls.Add(this.FlowLayoutPanel_Properties);
            this.GroupContainer_Properties.WorkingArea.Location = new System.Drawing.Point(1, 25);
            this.GroupContainer_Properties.WorkingArea.Name = "WorkingArea";
            this.GroupContainer_Properties.WorkingArea.Size = new System.Drawing.Size(466, 90);
            this.GroupContainer_Properties.WorkingArea.TabIndex = 0;
            // 
            // FlowLayoutPanel_Properties
            // 
            this.FlowLayoutPanel_Properties.Controls.Add(this.CheckBoxDark_CreatePrefix);
            this.FlowLayoutPanel_Properties.Controls.Add(this.CheckBoxDark_CreateSuffix);
            this.FlowLayoutPanel_Properties.Controls.Add(this.CheckBoxDark_AppendSuffixToExtension);
            this.FlowLayoutPanel_Properties.Controls.Add(this.CheckBoxDark_IncludeFiles);
            this.FlowLayoutPanel_Properties.Controls.Add(this.CheckBoxDark_IncludeDirectories);
            this.FlowLayoutPanel_Properties.Controls.Add(this.CheckBoxDark_IncludeSubdirectoryContents);
            this.FlowLayoutPanel_Properties.Controls.Add(this.CheckBoxDark_CaseSensitive);
            this.FlowLayoutPanel_Properties.Controls.Add(this.CheckBoxDark_MakeUppercase);
            this.FlowLayoutPanel_Properties.Controls.Add(this.CheckBoxDark_MakeLowercase);
            this.FlowLayoutPanel_Properties.Controls.Add(this.CheckBoxDark_MakeTitlecase);
            this.FlowLayoutPanel_Properties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlowLayoutPanel_Properties.Location = new System.Drawing.Point(0, 0);
            this.FlowLayoutPanel_Properties.Name = "FlowLayoutPanel_Properties";
            this.FlowLayoutPanel_Properties.Padding = new System.Windows.Forms.Padding(7, 2, 0, 0);
            this.FlowLayoutPanel_Properties.Size = new System.Drawing.Size(466, 90);
            this.FlowLayoutPanel_Properties.TabIndex = 1;
            // 
            // CheckBoxDark_CreatePrefix
            // 
            this.CheckBoxDark_CreatePrefix.AutoCheck = true;
            this.CheckBoxDark_CreatePrefix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.CheckBoxDark_CreatePrefix.CheckBoxBackColour = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.CheckBoxDark_CreatePrefix.CheckBoxCheckedColour = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.CheckBoxDark_CreatePrefix.CheckBoxIndeterminateColour = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(107)))), ((int)(((byte)(107)))));
            this.CheckBoxDark_CreatePrefix.CheckBoxOutlineColour = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.CheckBoxDark_CreatePrefix.Checked = false;
            this.CheckBoxDark_CreatePrefix.CheckStyle = Marathon.Components.CheckBoxStyle.Check;
            this.CheckBoxDark_CreatePrefix.DisabledForeColour = System.Drawing.SystemColors.GrayText;
            this.CheckBoxDark_CreatePrefix.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBoxDark_CreatePrefix.ForeColor = System.Drawing.SystemColors.Control;
            this.CheckBoxDark_CreatePrefix.Indeterminate = false;
            this.CheckBoxDark_CreatePrefix.Label = "Create Prefix";
            this.CheckBoxDark_CreatePrefix.Location = new System.Drawing.Point(10, 5);
            this.CheckBoxDark_CreatePrefix.Name = "CheckBoxDark_CreatePrefix";
            this.CheckBoxDark_CreatePrefix.RightAlign = false;
            this.CheckBoxDark_CreatePrefix.Size = new System.Drawing.Size(92, 22);
            this.CheckBoxDark_CreatePrefix.TabIndex = 0;
            this.CheckBoxDark_CreatePrefix.ThreeState = false;
            this.CheckBoxDark_CreatePrefix.CheckedChanged += new System.EventHandler(this.CheckBoxDark_Properties_CheckedChanged_Group);
            // 
            // CheckBoxDark_CreateSuffix
            // 
            this.CheckBoxDark_CreateSuffix.AutoCheck = true;
            this.CheckBoxDark_CreateSuffix.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.CheckBoxDark_CreateSuffix.CheckBoxBackColour = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.CheckBoxDark_CreateSuffix.CheckBoxCheckedColour = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.CheckBoxDark_CreateSuffix.CheckBoxIndeterminateColour = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(107)))), ((int)(((byte)(107)))));
            this.CheckBoxDark_CreateSuffix.CheckBoxOutlineColour = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.CheckBoxDark_CreateSuffix.Checked = false;
            this.CheckBoxDark_CreateSuffix.CheckStyle = Marathon.Components.CheckBoxStyle.Check;
            this.CheckBoxDark_CreateSuffix.DisabledForeColour = System.Drawing.SystemColors.GrayText;
            this.CheckBoxDark_CreateSuffix.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBoxDark_CreateSuffix.ForeColor = System.Drawing.SystemColors.Control;
            this.CheckBoxDark_CreateSuffix.Indeterminate = false;
            this.CheckBoxDark_CreateSuffix.Label = "Create Suffix";
            this.CheckBoxDark_CreateSuffix.Location = new System.Drawing.Point(108, 5);
            this.CheckBoxDark_CreateSuffix.Name = "CheckBoxDark_CreateSuffix";
            this.CheckBoxDark_CreateSuffix.RightAlign = false;
            this.CheckBoxDark_CreateSuffix.Size = new System.Drawing.Size(92, 22);
            this.CheckBoxDark_CreateSuffix.TabIndex = 1;
            this.CheckBoxDark_CreateSuffix.ThreeState = false;
            this.CheckBoxDark_CreateSuffix.CheckedChanged += new System.EventHandler(this.CheckBoxDark_Properties_CheckedChanged_Group);
            // 
            // CheckBoxDark_AppendSuffixToExtension
            // 
            this.CheckBoxDark_AppendSuffixToExtension.AutoCheck = true;
            this.CheckBoxDark_AppendSuffixToExtension.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.CheckBoxDark_AppendSuffixToExtension.CheckBoxBackColour = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.CheckBoxDark_AppendSuffixToExtension.CheckBoxCheckedColour = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.CheckBoxDark_AppendSuffixToExtension.CheckBoxIndeterminateColour = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(107)))), ((int)(((byte)(107)))));
            this.CheckBoxDark_AppendSuffixToExtension.CheckBoxOutlineColour = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.CheckBoxDark_AppendSuffixToExtension.Checked = false;
            this.CheckBoxDark_AppendSuffixToExtension.CheckStyle = Marathon.Components.CheckBoxStyle.Check;
            this.CheckBoxDark_AppendSuffixToExtension.DisabledForeColour = System.Drawing.SystemColors.GrayText;
            this.CheckBoxDark_AppendSuffixToExtension.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBoxDark_AppendSuffixToExtension.ForeColor = System.Drawing.SystemColors.Control;
            this.CheckBoxDark_AppendSuffixToExtension.Indeterminate = false;
            this.CheckBoxDark_AppendSuffixToExtension.Label = "Append Suffix to Extension";
            this.CheckBoxDark_AppendSuffixToExtension.Location = new System.Drawing.Point(206, 5);
            this.CheckBoxDark_AppendSuffixToExtension.Name = "CheckBoxDark_AppendSuffixToExtension";
            this.CheckBoxDark_AppendSuffixToExtension.RightAlign = false;
            this.CheckBoxDark_AppendSuffixToExtension.Size = new System.Drawing.Size(168, 22);
            this.CheckBoxDark_AppendSuffixToExtension.TabIndex = 2;
            this.CheckBoxDark_AppendSuffixToExtension.ThreeState = false;
            this.CheckBoxDark_AppendSuffixToExtension.Visible = false;
            this.CheckBoxDark_AppendSuffixToExtension.CheckedChanged += new System.EventHandler(this.CheckBoxDark_Properties_CheckedChanged_Group);
            // 
            // CheckBoxDark_IncludeFiles
            // 
            this.CheckBoxDark_IncludeFiles.AutoCheck = true;
            this.CheckBoxDark_IncludeFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.CheckBoxDark_IncludeFiles.CheckBoxBackColour = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.CheckBoxDark_IncludeFiles.CheckBoxCheckedColour = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.CheckBoxDark_IncludeFiles.CheckBoxIndeterminateColour = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(107)))), ((int)(((byte)(107)))));
            this.CheckBoxDark_IncludeFiles.CheckBoxOutlineColour = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.CheckBoxDark_IncludeFiles.Checked = true;
            this.CheckBoxDark_IncludeFiles.CheckStyle = Marathon.Components.CheckBoxStyle.Check;
            this.CheckBoxDark_IncludeFiles.DisabledForeColour = System.Drawing.SystemColors.GrayText;
            this.CheckBoxDark_IncludeFiles.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBoxDark_IncludeFiles.ForeColor = System.Drawing.SystemColors.Control;
            this.CheckBoxDark_IncludeFiles.Indeterminate = false;
            this.CheckBoxDark_IncludeFiles.Label = "Include Files";
            this.CheckBoxDark_IncludeFiles.Location = new System.Drawing.Point(10, 33);
            this.CheckBoxDark_IncludeFiles.Name = "CheckBoxDark_IncludeFiles";
            this.CheckBoxDark_IncludeFiles.RightAlign = false;
            this.CheckBoxDark_IncludeFiles.Size = new System.Drawing.Size(90, 22);
            this.CheckBoxDark_IncludeFiles.TabIndex = 3;
            this.CheckBoxDark_IncludeFiles.ThreeState = false;
            this.CheckBoxDark_IncludeFiles.CheckedChanged += new System.EventHandler(this.CheckBoxDark_Properties_CheckedChanged_Group);
            // 
            // CheckBoxDark_IncludeDirectories
            // 
            this.CheckBoxDark_IncludeDirectories.AutoCheck = true;
            this.CheckBoxDark_IncludeDirectories.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.CheckBoxDark_IncludeDirectories.CheckBoxBackColour = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.CheckBoxDark_IncludeDirectories.CheckBoxCheckedColour = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.CheckBoxDark_IncludeDirectories.CheckBoxIndeterminateColour = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(107)))), ((int)(((byte)(107)))));
            this.CheckBoxDark_IncludeDirectories.CheckBoxOutlineColour = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.CheckBoxDark_IncludeDirectories.Checked = false;
            this.CheckBoxDark_IncludeDirectories.CheckStyle = Marathon.Components.CheckBoxStyle.Check;
            this.CheckBoxDark_IncludeDirectories.DisabledForeColour = System.Drawing.SystemColors.GrayText;
            this.CheckBoxDark_IncludeDirectories.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBoxDark_IncludeDirectories.ForeColor = System.Drawing.SystemColors.Control;
            this.CheckBoxDark_IncludeDirectories.Indeterminate = false;
            this.CheckBoxDark_IncludeDirectories.Label = "Include Directories";
            this.CheckBoxDark_IncludeDirectories.Location = new System.Drawing.Point(106, 33);
            this.CheckBoxDark_IncludeDirectories.Name = "CheckBoxDark_IncludeDirectories";
            this.CheckBoxDark_IncludeDirectories.RightAlign = false;
            this.CheckBoxDark_IncludeDirectories.Size = new System.Drawing.Size(123, 22);
            this.CheckBoxDark_IncludeDirectories.TabIndex = 4;
            this.CheckBoxDark_IncludeDirectories.ThreeState = false;
            this.CheckBoxDark_IncludeDirectories.CheckedChanged += new System.EventHandler(this.CheckBoxDark_Properties_CheckedChanged_Group);
            // 
            // CheckBoxDark_IncludeSubdirectoryContents
            // 
            this.CheckBoxDark_IncludeSubdirectoryContents.AutoCheck = true;
            this.CheckBoxDark_IncludeSubdirectoryContents.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.CheckBoxDark_IncludeSubdirectoryContents.CheckBoxBackColour = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.CheckBoxDark_IncludeSubdirectoryContents.CheckBoxCheckedColour = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.CheckBoxDark_IncludeSubdirectoryContents.CheckBoxIndeterminateColour = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(107)))), ((int)(((byte)(107)))));
            this.CheckBoxDark_IncludeSubdirectoryContents.CheckBoxOutlineColour = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.CheckBoxDark_IncludeSubdirectoryContents.Checked = false;
            this.CheckBoxDark_IncludeSubdirectoryContents.CheckStyle = Marathon.Components.CheckBoxStyle.Check;
            this.CheckBoxDark_IncludeSubdirectoryContents.DisabledForeColour = System.Drawing.SystemColors.GrayText;
            this.CheckBoxDark_IncludeSubdirectoryContents.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBoxDark_IncludeSubdirectoryContents.ForeColor = System.Drawing.SystemColors.Control;
            this.CheckBoxDark_IncludeSubdirectoryContents.Indeterminate = false;
            this.CheckBoxDark_IncludeSubdirectoryContents.Label = "Include Subdirectory Contents";
            this.CheckBoxDark_IncludeSubdirectoryContents.Location = new System.Drawing.Point(235, 33);
            this.CheckBoxDark_IncludeSubdirectoryContents.Name = "CheckBoxDark_IncludeSubdirectoryContents";
            this.CheckBoxDark_IncludeSubdirectoryContents.RightAlign = false;
            this.CheckBoxDark_IncludeSubdirectoryContents.Size = new System.Drawing.Size(185, 22);
            this.CheckBoxDark_IncludeSubdirectoryContents.TabIndex = 5;
            this.CheckBoxDark_IncludeSubdirectoryContents.ThreeState = false;
            this.CheckBoxDark_IncludeSubdirectoryContents.CheckedChanged += new System.EventHandler(this.CheckBoxDark_Properties_CheckedChanged_Group);
            // 
            // CheckBoxDark_CaseSensitive
            // 
            this.CheckBoxDark_CaseSensitive.AutoCheck = true;
            this.CheckBoxDark_CaseSensitive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.CheckBoxDark_CaseSensitive.CheckBoxBackColour = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.CheckBoxDark_CaseSensitive.CheckBoxCheckedColour = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.CheckBoxDark_CaseSensitive.CheckBoxIndeterminateColour = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(107)))), ((int)(((byte)(107)))));
            this.CheckBoxDark_CaseSensitive.CheckBoxOutlineColour = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.CheckBoxDark_CaseSensitive.Checked = false;
            this.CheckBoxDark_CaseSensitive.CheckStyle = Marathon.Components.CheckBoxStyle.Check;
            this.CheckBoxDark_CaseSensitive.DisabledForeColour = System.Drawing.SystemColors.GrayText;
            this.CheckBoxDark_CaseSensitive.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBoxDark_CaseSensitive.ForeColor = System.Drawing.SystemColors.Control;
            this.CheckBoxDark_CaseSensitive.Indeterminate = false;
            this.CheckBoxDark_CaseSensitive.Label = "Case Sensitive";
            this.CheckBoxDark_CaseSensitive.Location = new System.Drawing.Point(10, 61);
            this.CheckBoxDark_CaseSensitive.Name = "CheckBoxDark_CaseSensitive";
            this.CheckBoxDark_CaseSensitive.RightAlign = false;
            this.CheckBoxDark_CaseSensitive.Size = new System.Drawing.Size(99, 22);
            this.CheckBoxDark_CaseSensitive.TabIndex = 6;
            this.CheckBoxDark_CaseSensitive.ThreeState = false;
            this.CheckBoxDark_CaseSensitive.CheckedChanged += new System.EventHandler(this.CheckBoxDark_Properties_CheckedChanged_Group);
            // 
            // CheckBoxDark_MakeUppercase
            // 
            this.CheckBoxDark_MakeUppercase.AutoCheck = true;
            this.CheckBoxDark_MakeUppercase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.CheckBoxDark_MakeUppercase.CheckBoxBackColour = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.CheckBoxDark_MakeUppercase.CheckBoxCheckedColour = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.CheckBoxDark_MakeUppercase.CheckBoxIndeterminateColour = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(107)))), ((int)(((byte)(107)))));
            this.CheckBoxDark_MakeUppercase.CheckBoxOutlineColour = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.CheckBoxDark_MakeUppercase.Checked = false;
            this.CheckBoxDark_MakeUppercase.CheckStyle = Marathon.Components.CheckBoxStyle.Check;
            this.CheckBoxDark_MakeUppercase.DisabledForeColour = System.Drawing.SystemColors.GrayText;
            this.CheckBoxDark_MakeUppercase.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBoxDark_MakeUppercase.ForeColor = System.Drawing.SystemColors.Control;
            this.CheckBoxDark_MakeUppercase.Indeterminate = false;
            this.CheckBoxDark_MakeUppercase.Label = "Make Uppercase";
            this.CheckBoxDark_MakeUppercase.Location = new System.Drawing.Point(115, 61);
            this.CheckBoxDark_MakeUppercase.Name = "CheckBoxDark_MakeUppercase";
            this.CheckBoxDark_MakeUppercase.RightAlign = false;
            this.CheckBoxDark_MakeUppercase.Size = new System.Drawing.Size(112, 22);
            this.CheckBoxDark_MakeUppercase.TabIndex = 7;
            this.CheckBoxDark_MakeUppercase.ThreeState = false;
            this.CheckBoxDark_MakeUppercase.CheckedChanged += new System.EventHandler(this.CheckBoxDark_Properties_CheckedChanged_Group);
            // 
            // CheckBoxDark_MakeLowercase
            // 
            this.CheckBoxDark_MakeLowercase.AutoCheck = true;
            this.CheckBoxDark_MakeLowercase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.CheckBoxDark_MakeLowercase.CheckBoxBackColour = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.CheckBoxDark_MakeLowercase.CheckBoxCheckedColour = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.CheckBoxDark_MakeLowercase.CheckBoxIndeterminateColour = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(107)))), ((int)(((byte)(107)))));
            this.CheckBoxDark_MakeLowercase.CheckBoxOutlineColour = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.CheckBoxDark_MakeLowercase.Checked = false;
            this.CheckBoxDark_MakeLowercase.CheckStyle = Marathon.Components.CheckBoxStyle.Check;
            this.CheckBoxDark_MakeLowercase.DisabledForeColour = System.Drawing.SystemColors.GrayText;
            this.CheckBoxDark_MakeLowercase.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBoxDark_MakeLowercase.ForeColor = System.Drawing.SystemColors.Control;
            this.CheckBoxDark_MakeLowercase.Indeterminate = false;
            this.CheckBoxDark_MakeLowercase.Label = "Make Lowercase";
            this.CheckBoxDark_MakeLowercase.Location = new System.Drawing.Point(233, 61);
            this.CheckBoxDark_MakeLowercase.Name = "CheckBoxDark_MakeLowercase";
            this.CheckBoxDark_MakeLowercase.RightAlign = false;
            this.CheckBoxDark_MakeLowercase.Size = new System.Drawing.Size(112, 22);
            this.CheckBoxDark_MakeLowercase.TabIndex = 8;
            this.CheckBoxDark_MakeLowercase.ThreeState = false;
            this.CheckBoxDark_MakeLowercase.CheckedChanged += new System.EventHandler(this.CheckBoxDark_Properties_CheckedChanged_Group);
            // 
            // CheckBoxDark_MakeTitlecase
            // 
            this.CheckBoxDark_MakeTitlecase.AutoCheck = true;
            this.CheckBoxDark_MakeTitlecase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.CheckBoxDark_MakeTitlecase.CheckBoxBackColour = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.CheckBoxDark_MakeTitlecase.CheckBoxCheckedColour = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.CheckBoxDark_MakeTitlecase.CheckBoxIndeterminateColour = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(107)))), ((int)(((byte)(107)))));
            this.CheckBoxDark_MakeTitlecase.CheckBoxOutlineColour = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.CheckBoxDark_MakeTitlecase.Checked = false;
            this.CheckBoxDark_MakeTitlecase.CheckStyle = Marathon.Components.CheckBoxStyle.Check;
            this.CheckBoxDark_MakeTitlecase.DisabledForeColour = System.Drawing.SystemColors.GrayText;
            this.CheckBoxDark_MakeTitlecase.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBoxDark_MakeTitlecase.ForeColor = System.Drawing.SystemColors.Control;
            this.CheckBoxDark_MakeTitlecase.Indeterminate = false;
            this.CheckBoxDark_MakeTitlecase.Label = "Make Titlecase";
            this.CheckBoxDark_MakeTitlecase.Location = new System.Drawing.Point(351, 61);
            this.CheckBoxDark_MakeTitlecase.Name = "CheckBoxDark_MakeTitlecase";
            this.CheckBoxDark_MakeTitlecase.RightAlign = false;
            this.CheckBoxDark_MakeTitlecase.Size = new System.Drawing.Size(102, 22);
            this.CheckBoxDark_MakeTitlecase.TabIndex = 9;
            this.CheckBoxDark_MakeTitlecase.ThreeState = false;
            this.CheckBoxDark_MakeTitlecase.CheckedChanged += new System.EventHandler(this.CheckBoxDark_Properties_CheckedChanged_Group);
            // 
            // GroupContainer_Criteria
            // 
            this.GroupContainer_Criteria.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupContainer_Criteria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupContainer_Criteria.BorderColour = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.GroupContainer_Criteria.HeaderText = "Criteria";
            this.GroupContainer_Criteria.Location = new System.Drawing.Point(12, 13);
            this.GroupContainer_Criteria.Name = "GroupContainer_Criteria";
            this.GroupContainer_Criteria.Size = new System.Drawing.Size(468, 107);
            this.GroupContainer_Criteria.TabIndex = 3;
            // 
            // GroupContainer_Criteria.WorkingArea
            // 
            this.GroupContainer_Criteria.WorkingArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupContainer_Criteria.WorkingArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.GroupContainer_Criteria.WorkingArea.Controls.Add(this.TextBoxDark_Criteria_1);
            this.GroupContainer_Criteria.WorkingArea.Controls.Add(this.TextBoxDark_Criteria_2);
            this.GroupContainer_Criteria.WorkingArea.Location = new System.Drawing.Point(1, 25);
            this.GroupContainer_Criteria.WorkingArea.Name = "WorkingArea";
            this.GroupContainer_Criteria.WorkingArea.Size = new System.Drawing.Size(466, 81);
            this.GroupContainer_Criteria.WorkingArea.TabIndex = 0;
            // 
            // TextBoxDark_Criteria_1
            // 
            this.TextBoxDark_Criteria_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxDark_Criteria_1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.TextBoxDark_Criteria_1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxDark_Criteria_1.DisabledBackColour = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TextBoxDark_Criteria_1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxDark_Criteria_1.ForeColor = System.Drawing.SystemColors.Control;
            this.TextBoxDark_Criteria_1.Location = new System.Drawing.Point(13, 13);
            this.TextBoxDark_Criteria_1.Name = "TextBoxDark_Criteria_1";
            this.TextBoxDark_Criteria_1.Size = new System.Drawing.Size(440, 23);
            this.TextBoxDark_Criteria_1.TabIndex = 0;
            this.TextBoxDark_Criteria_1.Watermark = "Original name";
            this.TextBoxDark_Criteria_1.TextChanged += new System.EventHandler(this.TextBoxDark_Criteria_TextChanged_Group);
            // 
            // TextBoxDark_Criteria_2
            // 
            this.TextBoxDark_Criteria_2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxDark_Criteria_2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.TextBoxDark_Criteria_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxDark_Criteria_2.DisabledBackColour = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.TextBoxDark_Criteria_2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxDark_Criteria_2.ForeColor = System.Drawing.SystemColors.Control;
            this.TextBoxDark_Criteria_2.Location = new System.Drawing.Point(13, 44);
            this.TextBoxDark_Criteria_2.Name = "TextBoxDark_Criteria_2";
            this.TextBoxDark_Criteria_2.Size = new System.Drawing.Size(440, 23);
            this.TextBoxDark_Criteria_2.TabIndex = 2;
            this.TextBoxDark_Criteria_2.Watermark = "New name";
            this.TextBoxDark_Criteria_2.TextChanged += new System.EventHandler(this.TextBoxDark_Criteria_TextChanged_Group);
            // 
            // ButtonDark_Rename
            // 
            this.ButtonDark_Rename.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonDark_Rename.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ButtonDark_Rename.Checked = false;
            this.ButtonDark_Rename.FlatAppearance.BorderSize = 0;
            this.ButtonDark_Rename.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDark_Rename.ForeColor = System.Drawing.SystemColors.Control;
            this.ButtonDark_Rename.FormBackColour = System.Drawing.Color.Empty;
            this.ButtonDark_Rename.Location = new System.Drawing.Point(12, 539);
            this.ButtonDark_Rename.Name = "ButtonDark_Rename";
            this.ButtonDark_Rename.Size = new System.Drawing.Size(468, 23);
            this.ButtonDark_Rename.TabIndex = 2;
            this.ButtonDark_Rename.Text = "Rename";
            this.ButtonDark_Rename.UseVisualStyleBackColor = false;
            this.ButtonDark_Rename.Click += new System.EventHandler(this.ButtonDark_Rename_Click);
            // 
            // GroupContainer_Preview
            // 
            this.GroupContainer_Preview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupContainer_Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupContainer_Preview.BorderColour = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.GroupContainer_Preview.HeaderText = "Preview";
            this.GroupContainer_Preview.Location = new System.Drawing.Point(12, 262);
            this.GroupContainer_Preview.Name = "GroupContainer_Preview";
            this.GroupContainer_Preview.Size = new System.Drawing.Size(468, 264);
            this.GroupContainer_Preview.TabIndex = 1;
            // 
            // GroupContainer_Preview.WorkingArea
            // 
            this.GroupContainer_Preview.WorkingArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupContainer_Preview.WorkingArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.GroupContainer_Preview.WorkingArea.Controls.Add(this.MarathonListView_Preview);
            this.GroupContainer_Preview.WorkingArea.Location = new System.Drawing.Point(1, 25);
            this.GroupContainer_Preview.WorkingArea.Name = "WorkingArea";
            this.GroupContainer_Preview.WorkingArea.Size = new System.Drawing.Size(466, 238);
            this.GroupContainer_Preview.WorkingArea.TabIndex = 0;
            // 
            // MarathonListView_Preview
            // 
            this.MarathonListView_Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.MarathonListView_Preview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MarathonListView_Preview.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MarathonListView_Preview.ForeColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.MarathonListView_Preview.ListView.AllColumns.Add(this.OlvColumn_Original);
            this.MarathonListView_Preview.ListView.AllColumns.Add(this.OlvColumn_Renamed);
            this.MarathonListView_Preview.ListView.AllowColumnReorder = true;
            this.MarathonListView_Preview.ListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MarathonListView_Preview.ListView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.MarathonListView_Preview.ListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MarathonListView_Preview.ListView.CheckBoxes = true;
            this.MarathonListView_Preview.ListView.ColumnBackColour = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.MarathonListView_Preview.ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.OlvColumn_Original,
            this.OlvColumn_Renamed});
            this.MarathonListView_Preview.ListView.ColumnSeparatorColour = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.MarathonListView_Preview.ListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.MarathonListView_Preview.ListView.ForeColor = System.Drawing.SystemColors.Control;
            this.MarathonListView_Preview.ListView.FullRowSelect = true;
            this.MarathonListView_Preview.ListView.HeaderMinimumHeight = 31;
            this.MarathonListView_Preview.ListView.HideSelection = false;
            this.MarathonListView_Preview.ListView.Location = new System.Drawing.Point(0, 0);
            this.MarathonListView_Preview.ListView.Name = "ObjectListViewDark_ObjectListView";
            this.MarathonListView_Preview.ListView.OwnerDrawnHeader = true;
            this.MarathonListView_Preview.ListView.RenderNonEditableCheckboxesAsDisabled = true;
            this.MarathonListView_Preview.ListView.RowHeight = 20;
            this.MarathonListView_Preview.ListView.SelectedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(119)))));
            this.MarathonListView_Preview.ListView.SelectedItemColour = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.MarathonListView_Preview.ListView.ShowGroups = false;
            this.MarathonListView_Preview.ListView.ShowItemToolTips = true;
            this.MarathonListView_Preview.ListView.Size = new System.Drawing.Size(466, 255);
            this.MarathonListView_Preview.ListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.MarathonListView_Preview.ListView.TabIndex = 0;
            this.MarathonListView_Preview.ListView.UseCompatibleStateImageBehavior = false;
            this.MarathonListView_Preview.ListView.UseFilterIndicator = true;
            this.MarathonListView_Preview.ListView.View = System.Windows.Forms.View.Details;
            this.MarathonListView_Preview.ListView.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.MarathonListView_Preview_ItemCheck);
            this.MarathonListView_Preview.Location = new System.Drawing.Point(0, 0);
            this.MarathonListView_Preview.Name = "MarathonListView_Preview";
            this.MarathonListView_Preview.Size = new System.Drawing.Size(466, 238);
            this.MarathonListView_Preview.TabIndex = 0;
            // 
            // OlvColumn_Original
            // 
            this.OlvColumn_Original.AspectName = "Original";
            this.OlvColumn_Original.Text = "Original";
            this.OlvColumn_Original.Width = 233;
            // 
            // OlvColumn_Renamed
            // 
            this.OlvColumn_Renamed.AspectName = "Renamed";
            this.OlvColumn_Renamed.Text = "Renamed";
            this.OlvColumn_Renamed.Width = 233;
            // 
            // BulkRenamer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(492, 575);
            this.Controls.Add(this.GroupContainer_Properties);
            this.Controls.Add(this.GroupContainer_Criteria);
            this.Controls.Add(this.ButtonDark_Rename);
            this.Controls.Add(this.GroupContainer_Preview);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(508, 614);
            this.Name = "BulkRenamer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bulk Renamer";
            this.Load += new System.EventHandler(this.BulkRenamer_Load);
            this.GroupContainer_Properties.WorkingArea.ResumeLayout(false);
            this.GroupContainer_Properties.ResumeLayout(false);
            this.FlowLayoutPanel_Properties.ResumeLayout(false);
            this.GroupContainer_Criteria.WorkingArea.ResumeLayout(false);
            this.GroupContainer_Criteria.WorkingArea.PerformLayout();
            this.GroupContainer_Criteria.ResumeLayout(false);
            this.GroupContainer_Preview.WorkingArea.ResumeLayout(false);
            this.GroupContainer_Preview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MarathonListView_Preview.ListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Components.TextBoxDark TextBoxDark_Criteria_2;
        private Components.TextBoxDark TextBoxDark_Criteria_1;
        private System.Windows.Forms.ImageList ImageList_Keys;
        private Components.ButtonDark ButtonDark_Rename;
        private Components.GroupContainer GroupContainer_Criteria;
        private Components.GroupContainer GroupContainer_Properties;
        private System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel_Properties;
        private Components.CheckBoxDark CheckBoxDark_CreatePrefix;
        private Components.CheckBoxDark CheckBoxDark_CreateSuffix;
        private Components.CheckBoxDark CheckBoxDark_AppendSuffixToExtension;
        private Components.CheckBoxDark CheckBoxDark_IncludeFiles;
        private Components.CheckBoxDark CheckBoxDark_IncludeDirectories;
        private Components.CheckBoxDark CheckBoxDark_IncludeSubdirectoryContents;
        private Components.CheckBoxDark CheckBoxDark_CaseSensitive;
        private Components.CheckBoxDark CheckBoxDark_MakeUppercase;
        private Components.CheckBoxDark CheckBoxDark_MakeLowercase;
        private Components.CheckBoxDark CheckBoxDark_MakeTitlecase;
        private Components.GroupContainer GroupContainer_Preview;
        private Components.MarathonListView MarathonListView_Preview;
        private BrightIdeasSoftware.OLVColumn OlvColumn_Original;
        private BrightIdeasSoftware.OLVColumn OlvColumn_Renamed;
    }
}