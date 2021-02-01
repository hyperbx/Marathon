namespace Marathon.Toolkit.Forms
{
    partial class Debugger
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
            this.TreeView_Properties = new System.Windows.Forms.TreeView();
            this.ButtonDark_MarathonMessageBox_Sample = new Marathon.Components.ButtonDark();
            this.GroupContainer_Settings = new Marathon.Components.GroupContainer();
            this.MarathonListView_Sample = new Marathon.Components.MarathonListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.CheckBoxDark_Sample = new Marathon.Components.CheckBoxDark();
            this.GroupContainer_Samples = new Marathon.Components.GroupContainer();
            this.marathonProgressBar1 = new Marathon.Components.MarathonProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.KryptonRibbon_MarathonDockContent)).BeginInit();
            this.GroupContainer_Settings.WorkingArea.SuspendLayout();
            this.GroupContainer_Settings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MarathonListView_Sample.ListView)).BeginInit();
            this.GroupContainer_Samples.WorkingArea.SuspendLayout();
            this.GroupContainer_Samples.SuspendLayout();
            this.SuspendLayout();
            // 
            // KryptonRibbon_MarathonDockContent
            // 
            this.KryptonRibbon_MarathonDockContent.RibbonAppButton.AppButtonShowRecentDocs = false;
            // 
            // TreeView_Properties
            // 
            this.TreeView_Properties.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.TreeView_Properties.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeView_Properties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeView_Properties.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.TreeView_Properties.ForeColor = System.Drawing.SystemColors.Control;
            this.TreeView_Properties.ItemHeight = 22;
            this.TreeView_Properties.Location = new System.Drawing.Point(0, 0);
            this.TreeView_Properties.Name = "TreeView_Properties";
            this.TreeView_Properties.ShowLines = false;
            this.TreeView_Properties.ShowPlusMinus = false;
            this.TreeView_Properties.ShowRootLines = false;
            this.TreeView_Properties.Size = new System.Drawing.Size(257, 631);
            this.TreeView_Properties.TabIndex = 6;
            this.TreeView_Properties.TabStop = false;
            this.TreeView_Properties.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_Properties_AfterSelect);
            this.TreeView_Properties.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_Properties_NodeMouseClick);
            // 
            // ButtonDark_MarathonMessageBox_Sample
            // 
            this.ButtonDark_MarathonMessageBox_Sample.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonDark_MarathonMessageBox_Sample.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.ButtonDark_MarathonMessageBox_Sample.Checked = false;
            this.ButtonDark_MarathonMessageBox_Sample.FlatAppearance.BorderSize = 0;
            this.ButtonDark_MarathonMessageBox_Sample.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDark_MarathonMessageBox_Sample.ForeColor = System.Drawing.SystemColors.Control;
            this.ButtonDark_MarathonMessageBox_Sample.FormBackColour = System.Drawing.Color.Empty;
            this.ButtonDark_MarathonMessageBox_Sample.Location = new System.Drawing.Point(5, 5);
            this.ButtonDark_MarathonMessageBox_Sample.Name = "ButtonDark_MarathonMessageBox_Sample";
            this.ButtonDark_MarathonMessageBox_Sample.Size = new System.Drawing.Size(807, 23);
            this.ButtonDark_MarathonMessageBox_Sample.TabIndex = 6;
            this.ButtonDark_MarathonMessageBox_Sample.Text = "MarathonMessageBox Sample";
            this.ButtonDark_MarathonMessageBox_Sample.UseVisualStyleBackColor = false;
            this.ButtonDark_MarathonMessageBox_Sample.Click += new System.EventHandler(this.ButtonDark_MarathonMessageBox_Sample_Click);
            // 
            // GroupContainer_Settings
            // 
            this.GroupContainer_Settings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GroupContainer_Settings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupContainer_Settings.BorderColour = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.GroupContainer_Settings.HeaderText = "Settings";
            this.GroupContainer_Settings.Location = new System.Drawing.Point(12, 12);
            this.GroupContainer_Settings.Name = "GroupContainer_Settings";
            this.GroupContainer_Settings.Size = new System.Drawing.Size(259, 657);
            this.GroupContainer_Settings.TabIndex = 7;
            // 
            // GroupContainer_Settings.WorkingArea
            // 
            this.GroupContainer_Settings.WorkingArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupContainer_Settings.WorkingArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.GroupContainer_Settings.WorkingArea.Controls.Add(this.TreeView_Properties);
            this.GroupContainer_Settings.WorkingArea.Location = new System.Drawing.Point(1, 25);
            this.GroupContainer_Settings.WorkingArea.Name = "WorkingArea";
            this.GroupContainer_Settings.WorkingArea.Size = new System.Drawing.Size(257, 631);
            this.GroupContainer_Settings.WorkingArea.TabIndex = 0;
            // 
            // MarathonListView_Sample
            // 
            this.MarathonListView_Sample.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MarathonListView_Sample.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.MarathonListView_Sample.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MarathonListView_Sample.ForeColor = System.Drawing.SystemColors.Control;
            // 
            // 
            // 
            this.MarathonListView_Sample.ListView.AllColumns.Add(this.olvColumn1);
            this.MarathonListView_Sample.ListView.AllColumns.Add(this.olvColumn2);
            this.MarathonListView_Sample.ListView.AllColumns.Add(this.olvColumn3);
            this.MarathonListView_Sample.ListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MarathonListView_Sample.ListView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.MarathonListView_Sample.ListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MarathonListView_Sample.ListView.ColumnBackColour = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.MarathonListView_Sample.ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3});
            this.MarathonListView_Sample.ListView.ColumnSeparatorColour = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.MarathonListView_Sample.ListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.MarathonListView_Sample.ListView.ForeColor = System.Drawing.SystemColors.Control;
            this.MarathonListView_Sample.ListView.FullRowSelect = true;
            this.MarathonListView_Sample.ListView.HeaderMinimumHeight = 31;
            this.MarathonListView_Sample.ListView.HideSelection = false;
            this.MarathonListView_Sample.ListView.Location = new System.Drawing.Point(0, 0);
            this.MarathonListView_Sample.ListView.Name = "ObjectListViewDark_ObjectListView";
            this.MarathonListView_Sample.ListView.OwnerDrawnHeader = true;
            this.MarathonListView_Sample.ListView.RenderNonEditableCheckboxesAsDisabled = true;
            this.MarathonListView_Sample.ListView.RowHeight = 20;
            this.MarathonListView_Sample.ListView.SelectedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(119)))));
            this.MarathonListView_Sample.ListView.SelectedItemColour = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.MarathonListView_Sample.ListView.ShowGroups = false;
            this.MarathonListView_Sample.ListView.ShowItemToolTips = true;
            this.MarathonListView_Sample.ListView.Size = new System.Drawing.Size(400, 934);
            this.MarathonListView_Sample.ListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.MarathonListView_Sample.ListView.TabIndex = 0;
            this.MarathonListView_Sample.ListView.UseCompatibleStateImageBehavior = false;
            this.MarathonListView_Sample.ListView.UseFilterIndicator = true;
            this.MarathonListView_Sample.ListView.View = System.Windows.Forms.View.Details;
            this.MarathonListView_Sample.Location = new System.Drawing.Point(0, 90);
            this.MarathonListView_Sample.Name = "MarathonListView_Sample";
            this.MarathonListView_Sample.Size = new System.Drawing.Size(968, 542);
            this.MarathonListView_Sample.TabIndex = 8;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.Text = "Name";
            this.olvColumn1.Width = 200;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Type";
            this.olvColumn2.Text = "Type";
            this.olvColumn2.Width = 100;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Size";
            this.olvColumn3.Text = "Size";
            this.olvColumn3.Width = 100;
            // 
            // CheckBoxDark_Sample
            // 
            this.CheckBoxDark_Sample.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBoxDark_Sample.AutoCheck = true;
            this.CheckBoxDark_Sample.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.CheckBoxDark_Sample.CheckBoxBackColour = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.CheckBoxDark_Sample.CheckBoxCheckedColour = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.CheckBoxDark_Sample.CheckBoxIndeterminateColour = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(107)))), ((int)(((byte)(107)))));
            this.CheckBoxDark_Sample.CheckBoxOutlineColour = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.CheckBoxDark_Sample.Checked = false;
            this.CheckBoxDark_Sample.CheckStyle = Marathon.Components.CheckBoxStyle.Check;
            this.CheckBoxDark_Sample.DisabledForeColour = System.Drawing.SystemColors.GrayText;
            this.CheckBoxDark_Sample.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckBoxDark_Sample.ForeColor = System.Drawing.SystemColors.Control;
            this.CheckBoxDark_Sample.Indeterminate = false;
            this.CheckBoxDark_Sample.Label = "CheckBoxDark Sample";
            this.CheckBoxDark_Sample.Location = new System.Drawing.Point(818, 6);
            this.CheckBoxDark_Sample.Name = "CheckBoxDark_Sample";
            this.CheckBoxDark_Sample.RightAlign = false;
            this.CheckBoxDark_Sample.Size = new System.Drawing.Size(144, 22);
            this.CheckBoxDark_Sample.TabIndex = 9;
            this.CheckBoxDark_Sample.ThreeState = true;
            // 
            // GroupContainer_Samples
            // 
            this.GroupContainer_Samples.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupContainer_Samples.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.GroupContainer_Samples.BorderColour = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.GroupContainer_Samples.HeaderText = "Samples";
            this.GroupContainer_Samples.Location = new System.Drawing.Point(282, 12);
            this.GroupContainer_Samples.Name = "GroupContainer_Samples";
            this.GroupContainer_Samples.Size = new System.Drawing.Size(970, 657);
            this.GroupContainer_Samples.TabIndex = 10;
            // 
            // GroupContainer_Samples.WorkingArea
            // 
            this.GroupContainer_Samples.WorkingArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupContainer_Samples.WorkingArea.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.GroupContainer_Samples.WorkingArea.Controls.Add(this.marathonProgressBar1);
            this.GroupContainer_Samples.WorkingArea.Controls.Add(this.MarathonListView_Sample);
            this.GroupContainer_Samples.WorkingArea.Controls.Add(this.CheckBoxDark_Sample);
            this.GroupContainer_Samples.WorkingArea.Controls.Add(this.ButtonDark_MarathonMessageBox_Sample);
            this.GroupContainer_Samples.WorkingArea.Location = new System.Drawing.Point(1, 25);
            this.GroupContainer_Samples.WorkingArea.Name = "WorkingArea";
            this.GroupContainer_Samples.WorkingArea.Size = new System.Drawing.Size(968, 631);
            this.GroupContainer_Samples.WorkingArea.TabIndex = 0;
            // 
            // marathonProgressBar1
            // 
            this.marathonProgressBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.marathonProgressBar1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.marathonProgressBar1.ForeColor = System.Drawing.SystemColors.Control;
            this.marathonProgressBar1.Location = new System.Drawing.Point(5, 34);
            this.marathonProgressBar1.Name = "marathonProgressBar1";
            this.marathonProgressBar1.Progress = 25;
            this.marathonProgressBar1.ProgressColour = System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(176)))), ((int)(((byte)(37)))));
            this.marathonProgressBar1.Size = new System.Drawing.Size(807, 50);
            this.marathonProgressBar1.TabIndex = 10;
            // 
            // Debugger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.GroupContainer_Samples);
            this.Controls.Add(this.GroupContainer_Settings);
            this.Name = "Debugger";
            this.Text = "Marathon Debugger";
            this.UseRibbon = false;
            this.Controls.SetChildIndex(this.GroupContainer_Settings, 0);
            this.Controls.SetChildIndex(this.KryptonRibbon_MarathonDockContent, 0);
            this.Controls.SetChildIndex(this.GroupContainer_Samples, 0);
            ((System.ComponentModel.ISupportInitialize)(this.KryptonRibbon_MarathonDockContent)).EndInit();
            this.GroupContainer_Settings.WorkingArea.ResumeLayout(false);
            this.GroupContainer_Settings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MarathonListView_Sample.ListView)).EndInit();
            this.GroupContainer_Samples.WorkingArea.ResumeLayout(false);
            this.GroupContainer_Samples.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TreeView TreeView_Properties;
        private Components.ButtonDark ButtonDark_MarathonMessageBox_Sample;
        private Components.GroupContainer GroupContainer_Settings;
        private Components.MarathonListView MarathonListView_Sample;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private Components.CheckBoxDark CheckBoxDark_Sample;
        private Components.GroupContainer GroupContainer_Samples;
        private Components.MarathonProgressBar marathonProgressBar1;
    }
}
