namespace Marathon.Toolkit.Forms
{
    partial class Windows
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Windows));
            this.Panel_ListViewDark_Windows_Border = new System.Windows.Forms.Panel();
            this.ListViewDark_Windows = new Marathon.Toolkit.Components.ListViewDark();
            this.Column_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_Space = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Panel_ListViewDark_Windows_Border.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_ListViewDark_Windows_Border
            // 
            this.Panel_ListViewDark_Windows_Border.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_ListViewDark_Windows_Border.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_ListViewDark_Windows_Border.Controls.Add(this.ListViewDark_Windows);
            this.Panel_ListViewDark_Windows_Border.Location = new System.Drawing.Point(12, 12);
            this.Panel_ListViewDark_Windows_Border.Name = "Panel_ListViewDark_Windows_Border";
            this.Panel_ListViewDark_Windows_Border.Size = new System.Drawing.Size(354, 370);
            this.Panel_ListViewDark_Windows_Border.TabIndex = 2;
            // 
            // ListViewDark_Windows
            // 
            this.ListViewDark_Windows.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListViewDark_Windows.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ListViewDark_Windows.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListViewDark_Windows.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Column_Name,
            this.Column_Space});
            this.ListViewDark_Windows.ForeColor = System.Drawing.SystemColors.Control;
            this.ListViewDark_Windows.FullRowSelect = true;
            this.ListViewDark_Windows.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListViewDark_Windows.HideSelection = false;
            this.ListViewDark_Windows.Location = new System.Drawing.Point(-1, 0);
            this.ListViewDark_Windows.Name = "ListViewDark_Windows";
            this.ListViewDark_Windows.OwnerDraw = true;
            this.ListViewDark_Windows.Size = new System.Drawing.Size(353, 385);
            this.ListViewDark_Windows.TabIndex = 0;
            this.ListViewDark_Windows.UseCompatibleStateImageBehavior = false;
            this.ListViewDark_Windows.View = System.Windows.Forms.View.Details;
            this.ListViewDark_Windows.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ListViewDark_Windows_MouseDown);
            // 
            // Column_Name
            // 
            this.Column_Name.Text = "Name";
            this.Column_Name.Width = 9999;
            // 
            // Column_Space
            // 
            this.Column_Space.Text = "";
            this.Column_Space.Width = 25;
            // 
            // Windows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.ClientSize = new System.Drawing.Size(378, 394);
            this.Controls.Add(this.Panel_ListViewDark_Windows_Border);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(394, 433);
            this.Name = "Windows";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Windows";
            this.Panel_ListViewDark_Windows_Border.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Components.ListViewDark ListViewDark_Windows;
        private System.Windows.Forms.ColumnHeader Column_Name;
        private System.Windows.Forms.ColumnHeader Column_Space;
        private System.Windows.Forms.Panel Panel_ListViewDark_Windows_Border;
    }
}
