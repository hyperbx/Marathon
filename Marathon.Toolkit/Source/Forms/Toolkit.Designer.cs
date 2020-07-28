using Marathon.Helpers;

namespace Marathon
{
    partial class Toolkit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Toolkit));
            this.MenuStripDark_Main = new Marathon.Components.MenuStripDark();
            this.MenuStripDark_Main_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_File_OpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_File_OpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_File_ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuStripDark_Main_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_Help_About = new System.Windows.Forms.ToolStripMenuItem();
            this.DockPanel_Main = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.Theme_VS2015Dark = new WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme();
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
            this.MenuStripDark_Main_File,
            this.MenuStripDark_Main_Help});
            this.MenuStripDark_Main.Location = new System.Drawing.Point(-5, 0);
            this.MenuStripDark_Main.Name = "MenuStripDark_Main";
            this.MenuStripDark_Main.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.MenuStripDark_Main.Size = new System.Drawing.Size(955, 24);
            this.MenuStripDark_Main.TabIndex = 0;
            // 
            // MenuStripDark_Main_File
            // 
            this.MenuStripDark_Main_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStripDark_Main_File_OpenFile,
            this.MenuStripDark_Main_File_OpenFolder,
            this.MenuStripDark_Main_File_ToolStripSeparator1,
            this.MenuStripDark_Main_File_Exit});
            this.MenuStripDark_Main_File.Name = "MenuStripDark_Main_File";
            this.MenuStripDark_Main_File.Size = new System.Drawing.Size(37, 20);
            this.MenuStripDark_Main_File.Text = "File";
            // 
            // MenuStripDark_Main_File_OpenFile
            // 
            this.MenuStripDark_Main_File_OpenFile.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_File_OpenFile.Name = "MenuStripDark_Main_File_OpenFile";
            this.MenuStripDark_Main_File_OpenFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.MenuStripDark_Main_File_OpenFile.Size = new System.Drawing.Size(223, 22);
            this.MenuStripDark_Main_File_OpenFile.Text = "Open File...";
            this.MenuStripDark_Main_File_OpenFile.Click += new System.EventHandler(this.MenuStripDark_Main_File_OpenFile_Click);
            // 
            // MenuStripDark_Main_File_OpenFolder
            // 
            this.MenuStripDark_Main_File_OpenFolder.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_File_OpenFolder.Name = "MenuStripDark_Main_File_OpenFolder";
            this.MenuStripDark_Main_File_OpenFolder.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.MenuStripDark_Main_File_OpenFolder.Size = new System.Drawing.Size(223, 22);
            this.MenuStripDark_Main_File_OpenFolder.Text = "Open Folder...";
            this.MenuStripDark_Main_File_OpenFolder.Click += new System.EventHandler(this.MenuStripDark_Main_File_OpenFolder_Click);
            // 
            // MenuStripDark_Main_File_ToolStripSeparator1
            // 
            this.MenuStripDark_Main_File_ToolStripSeparator1.Name = "MenuStripDark_Main_File_ToolStripSeparator1";
            this.MenuStripDark_Main_File_ToolStripSeparator1.Size = new System.Drawing.Size(220, 6);
            // 
            // MenuStripDark_Main_File_Exit
            // 
            this.MenuStripDark_Main_File_Exit.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_File_Exit.Name = "MenuStripDark_Main_File_Exit";
            this.MenuStripDark_Main_File_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.MenuStripDark_Main_File_Exit.Size = new System.Drawing.Size(223, 22);
            this.MenuStripDark_Main_File_Exit.Text = "Exit";
            this.MenuStripDark_Main_File_Exit.Click += new System.EventHandler(this.MenuStripDark_Main_File_Exit_Click);
            // 
            // MenuStripDark_Main_Help
            // 
            this.MenuStripDark_Main_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStripDark_Main_Help_About});
            this.MenuStripDark_Main_Help.Name = "MenuStripDark_Main_Help";
            this.MenuStripDark_Main_Help.Size = new System.Drawing.Size(44, 20);
            this.MenuStripDark_Main_Help.Text = "Help";
            // 
            // MenuStripDark_Main_Help_About
            // 
            this.MenuStripDark_Main_Help_About.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_Help_About.Name = "MenuStripDark_Main_Help_About";
            this.MenuStripDark_Main_Help_About.Size = new System.Drawing.Size(107, 22);
            this.MenuStripDark_Main_Help_About.Text = "About";
            this.MenuStripDark_Main_Help_About.Click += new System.EventHandler(this.MenuStripDark_Main_Help_About_Click);
            // 
            // DockPanel_Main
            // 
            this.DockPanel_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DockPanel_Main.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.DockPanel_Main.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.DockPanel_Main.Location = new System.Drawing.Point(0, 24);
            this.DockPanel_Main.Name = "DockPanel_Main";
            this.DockPanel_Main.Padding = new System.Windows.Forms.Padding(6);
            this.DockPanel_Main.ShowAutoHideContentOnHover = false;
            this.DockPanel_Main.Size = new System.Drawing.Size(944, 477);
            this.DockPanel_Main.TabIndex = 2;
            this.DockPanel_Main.Theme = this.Theme_VS2015Dark;
            this.DockPanel_Main.ActiveDocumentChanged += new System.EventHandler(this.DockPanel_Main_ActiveDocumentChanged);
            // 
            // Toolkit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.Controls.Add(this.MenuStripDark_Main);
            this.Controls.Add(this.DockPanel_Main);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MenuStripDark_Main;
            this.MinimumSize = new System.Drawing.Size(512, 512);
            this.Name = "Toolkit";
            this.Text = "Marathon ";
            this.MenuStripDark_Main.ResumeLayout(false);
            this.MenuStripDark_Main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Marathon.Components.MenuStripDark MenuStripDark_Main;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_File;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_Help;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_Help_About;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_File_Exit;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_File_OpenFolder;
        private System.Windows.Forms.ToolStripSeparator MenuStripDark_Main_File_ToolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_File_OpenFile;
        private WeifenLuo.WinFormsUI.Docking.DockPanel DockPanel_Main;
        private WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme Theme_VS2015Dark;
    }
}

