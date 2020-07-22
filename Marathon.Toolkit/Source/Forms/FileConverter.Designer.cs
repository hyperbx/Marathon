namespace Marathon
{
    partial class FileConverter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileConverter));
            this.MenuStripDark_Main = new Marathon.Components.MenuStripDark();
            this.MenuStripDark_Main_Tools = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.ButtonFlat_Convert = new Marathon.Components.ButtonFlat();
            this.ListView_Conversion = new System.Windows.Forms.ListView();
            this.Column_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Column_Blank = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MenuStripDark_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStripDark_Main
            // 
            this.MenuStripDark_Main.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MenuStripDark_Main.AutoSize = false;
            this.MenuStripDark_Main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.MenuStripDark_Main.Dock = System.Windows.Forms.DockStyle.None;
            this.MenuStripDark_Main.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStripDark_Main_Tools,
            this.MenuStripDark_Main_Help});
            this.MenuStripDark_Main.Location = new System.Drawing.Point(-4, 0);
            this.MenuStripDark_Main.Name = "MenuStripDark_Main";
            this.MenuStripDark_Main.Size = new System.Drawing.Size(325, 24);
            this.MenuStripDark_Main.TabIndex = 0;
            this.MenuStripDark_Main.Text = "menuStripDark1";
            // 
            // MenuStripDark_Main_Tools
            // 
            this.MenuStripDark_Main_Tools.Name = "MenuStripDark_Main_Tools";
            this.MenuStripDark_Main_Tools.Size = new System.Drawing.Size(46, 20);
            this.MenuStripDark_Main_Tools.Text = "Tools";
            // 
            // MenuStripDark_Main_Help
            // 
            this.MenuStripDark_Main_Help.Name = "MenuStripDark_Main_Help";
            this.MenuStripDark_Main_Help.Size = new System.Drawing.Size(44, 20);
            this.MenuStripDark_Main_Help.Text = "Help";
            // 
            // ButtonFlat_Convert
            // 
            this.ButtonFlat_Convert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonFlat_Convert.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ButtonFlat_Convert.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_Convert.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_Convert.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_Convert.Location = new System.Drawing.Point(12, 436);
            this.ButtonFlat_Convert.Name = "ButtonFlat_Convert";
            this.ButtonFlat_Convert.Size = new System.Drawing.Size(296, 23);
            this.ButtonFlat_Convert.TabIndex = 1;
            this.ButtonFlat_Convert.Text = "Convert";
            this.ButtonFlat_Convert.UseVisualStyleBackColor = false;
            this.ButtonFlat_Convert.Visible = false;
            // 
            // ListView_Conversion
            // 
            this.ListView_Conversion.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListView_Conversion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ListView_Conversion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ListView_Conversion.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Column_Name,
            this.Column_Blank});
            this.ListView_Conversion.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ListView_Conversion.HideSelection = false;
            this.ListView_Conversion.Location = new System.Drawing.Point(12, 35);
            this.ListView_Conversion.Name = "ListView_Conversion";
            this.ListView_Conversion.Size = new System.Drawing.Size(296, 390);
            this.ListView_Conversion.TabIndex = 2;
            this.ListView_Conversion.UseCompatibleStateImageBehavior = false;
            this.ListView_Conversion.View = System.Windows.Forms.View.Details;
            this.ListView_Conversion.Visible = false;
            // 
            // Column_Name
            // 
            this.Column_Name.Text = "Name";
            this.Column_Name.Width = 296;
            // 
            // Column_Blank
            // 
            this.Column_Blank.Text = "";
            this.Column_Blank.Width = 1000;
            // 
            // FileConverter
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(320, 471);
            this.Controls.Add(this.ListView_Conversion);
            this.Controls.Add(this.ButtonFlat_Convert);
            this.Controls.Add(this.MenuStripDark_Main);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)));
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStripDark_Main;
            this.MinimumSize = new System.Drawing.Size(252, 382);
            this.Name = "FileConverter";
            this.Text = "File Converter";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FileConverter_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FileConverter_DragEnter);
            this.MenuStripDark_Main.ResumeLayout(false);
            this.MenuStripDark_Main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Components.MenuStripDark MenuStripDark_Main;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_Tools;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_Help;
        private Components.ButtonFlat ButtonFlat_Convert;
        private System.Windows.Forms.ListView ListView_Conversion;
        private System.Windows.Forms.ColumnHeader Column_Name;
        private System.Windows.Forms.ColumnHeader Column_Blank;
    }
}