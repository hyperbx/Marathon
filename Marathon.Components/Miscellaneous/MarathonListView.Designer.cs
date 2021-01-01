namespace Marathon.Components
{
    partial class MarathonListView
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
            this.Panel_Whitespace = new System.Windows.Forms.Panel();
            this.ObjectListViewDark_ObjectListView = new Marathon.Components.ObjectListViewDark();
            ((System.ComponentModel.ISupportInitialize)(this.ObjectListViewDark_ObjectListView)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel_Whitespace
            // 
            this.Panel_Whitespace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Whitespace.Location = new System.Drawing.Point(0, 0);
            this.Panel_Whitespace.Name = "Panel_Whitespace";
            this.Panel_Whitespace.Size = new System.Drawing.Size(150, 24);
            this.Panel_Whitespace.TabIndex = 1;
            // 
            // ObjectListViewDark_ObjectListView
            // 
            this.ObjectListViewDark_ObjectListView.AllowColumnReorder = true;
            this.ObjectListViewDark_ObjectListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ObjectListViewDark_ObjectListView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ObjectListViewDark_ObjectListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ObjectListViewDark_ObjectListView.ColumnBackColour = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ObjectListViewDark_ObjectListView.ColumnSeparatorColour = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(99)))), ((int)(((byte)(99)))));
            this.ObjectListViewDark_ObjectListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.ObjectListViewDark_ObjectListView.ForeColor = System.Drawing.SystemColors.Control;
            this.ObjectListViewDark_ObjectListView.FullRowSelect = true;
            this.ObjectListViewDark_ObjectListView.HeaderMinimumHeight = 31;
            this.ObjectListViewDark_ObjectListView.HideSelection = false;
            this.ObjectListViewDark_ObjectListView.Location = new System.Drawing.Point(0, 0);
            this.ObjectListViewDark_ObjectListView.Name = "ObjectListViewDark_ObjectListView";
            this.ObjectListViewDark_ObjectListView.OwnerDrawnHeader = true;
            this.ObjectListViewDark_ObjectListView.RenderNonEditableCheckboxesAsDisabled = true;
            this.ObjectListViewDark_ObjectListView.RowHeight = 20;
            this.ObjectListViewDark_ObjectListView.SelectedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(119)))), ((int)(((byte)(119)))));
            this.ObjectListViewDark_ObjectListView.SelectedItemColour = System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
            this.ObjectListViewDark_ObjectListView.ShowGroups = false;
            this.ObjectListViewDark_ObjectListView.ShowItemToolTips = true;
            this.ObjectListViewDark_ObjectListView.Size = new System.Drawing.Size(75, 150);
            this.ObjectListViewDark_ObjectListView.TabIndex = 0;
            this.ObjectListViewDark_ObjectListView.UseCompatibleStateImageBehavior = false;
            this.ObjectListViewDark_ObjectListView.UseFilterIndicator = true;
            this.ObjectListViewDark_ObjectListView.View = System.Windows.Forms.View.Details;
            this.ObjectListViewDark_ObjectListView.ColumnWidthChanged += new System.Windows.Forms.ColumnWidthChangedEventHandler(this.ListView_ColumnWidthChanged);
            this.ObjectListViewDark_ObjectListView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.ListView_ColumnWidthChanging);
            // 
            // MarathonListView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.ObjectListViewDark_ObjectListView);
            this.Controls.Add(this.Panel_Whitespace);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Name = "MarathonListView";
            ((System.ComponentModel.ISupportInitialize)(this.ObjectListViewDark_ObjectListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel Panel_Whitespace;
        private ObjectListViewDark ObjectListViewDark_ObjectListView;
    }
}
