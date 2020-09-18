using Marathon.Toolkit.Helpers;

namespace Marathon.Toolkit.Forms
{
    partial class Workspace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Workspace));
            this.MenuStripDark_Main = new Marathon.Toolkit.Components.MenuStripDark();
            this.MenuStripDark_Main_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_View = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_View_FileConverter = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_View_Output = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_Window = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_Window_Windows = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_Help_ReportABug = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_Help_ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuStripDark_Main_Help_About = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_Debug = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_File_New = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_File_New_Archive = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_File_Open = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_File_Open_File = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_File_Open_Folder = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_File_ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuStripDark_Main_File_Options = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStripDark_Main_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
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
            this.MenuStripDark_Main_View,
            this.MenuStripDark_Main_Window,
            this.MenuStripDark_Main_Help,
            this.MenuStripDark_Main_Debug});
            this.MenuStripDark_Main.Location = new System.Drawing.Point(-5, 0);
            this.MenuStripDark_Main.Name = "MenuStripDark_Main";
            this.MenuStripDark_Main.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.MenuStripDark_Main.Size = new System.Drawing.Size(947, 24);
            this.MenuStripDark_Main.TabIndex = 0;
            // 
            // MenuStripDark_Main_File
            // 
            this.MenuStripDark_Main_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStripDark_Main_File_New,
            this.MenuStripDark_Main_File_Open,
            this.MenuStripDark_Main_File_ToolStripSeparator1,
            this.MenuStripDark_Main_File_Options,
            this.MenuStripDark_Main_File_Exit});
            this.MenuStripDark_Main_File.Name = "MenuStripDark_Main_File";
            this.MenuStripDark_Main_File.Size = new System.Drawing.Size(37, 20);
            this.MenuStripDark_Main_File.Text = "&File";
            // 
            // MenuStripDark_Main_View
            // 
            this.MenuStripDark_Main_View.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStripDark_Main_View_FileConverter,
            this.MenuStripDark_Main_View_Output});
            this.MenuStripDark_Main_View.Name = "MenuStripDark_Main_View";
            this.MenuStripDark_Main_View.Size = new System.Drawing.Size(44, 20);
            this.MenuStripDark_Main_View.Text = "&View";
            // 
            // MenuStripDark_Main_View_FileConverter
            // 
            this.MenuStripDark_Main_View_FileConverter.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_View_FileConverter.Image = global::Marathon.Toolkit.Properties.Resources.Placeholder;
            this.MenuStripDark_Main_View_FileConverter.Name = "MenuStripDark_Main_View_FileConverter";
            this.MenuStripDark_Main_View_FileConverter.Size = new System.Drawing.Size(147, 22);
            this.MenuStripDark_Main_View_FileConverter.Text = "&File Converter";
            this.MenuStripDark_Main_View_FileConverter.Click += new System.EventHandler(this.MenuStripDark_Main_View_FileConverter_Click);
            // 
            // MenuStripDark_Main_View_Output
            // 
            this.MenuStripDark_Main_View_Output.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_View_Output.Image = global::Marathon.Toolkit.Properties.Resources.Task_Output;
            this.MenuStripDark_Main_View_Output.Name = "MenuStripDark_Main_View_Output";
            this.MenuStripDark_Main_View_Output.Size = new System.Drawing.Size(147, 22);
            this.MenuStripDark_Main_View_Output.Text = "&Output";
            this.MenuStripDark_Main_View_Output.Click += new System.EventHandler(this.MenuStripDark_Main_View_Output_Click);
            // 
            // MenuStripDark_Main_Window
            // 
            this.MenuStripDark_Main_Window.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStripDark_Main_Window_Windows});
            this.MenuStripDark_Main_Window.Name = "MenuStripDark_Main_Window";
            this.MenuStripDark_Main_Window.Size = new System.Drawing.Size(63, 20);
            this.MenuStripDark_Main_Window.Text = "&Window";
            // 
            // MenuStripDark_Main_Window_Windows
            // 
            this.MenuStripDark_Main_Window_Windows.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_Window_Windows.Image = global::Marathon.Toolkit.Properties.Resources.Task_OpenDialog;
            this.MenuStripDark_Main_Window_Windows.Name = "MenuStripDark_Main_Window_Windows";
            this.MenuStripDark_Main_Window_Windows.Size = new System.Drawing.Size(123, 22);
            this.MenuStripDark_Main_Window_Windows.Text = "&Windows";
            this.MenuStripDark_Main_Window_Windows.Click += new System.EventHandler(this.MenuStripDark_Main_Window_Windows_Click);
            // 
            // MenuStripDark_Main_Help
            // 
            this.MenuStripDark_Main_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStripDark_Main_Help_ReportABug,
            this.MenuStripDark_Main_Help_ToolStripSeparator1,
            this.MenuStripDark_Main_Help_About});
            this.MenuStripDark_Main_Help.Name = "MenuStripDark_Main_Help";
            this.MenuStripDark_Main_Help.Size = new System.Drawing.Size(44, 20);
            this.MenuStripDark_Main_Help.Text = "&Help";
            // 
            // MenuStripDark_Main_Help_ReportABug
            // 
            this.MenuStripDark_Main_Help_ReportABug.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_Help_ReportABug.Image = global::Marathon.Toolkit.Properties.Resources.Task_Bug;
            this.MenuStripDark_Main_Help_ReportABug.Name = "MenuStripDark_Main_Help_ReportABug";
            this.MenuStripDark_Main_Help_ReportABug.Size = new System.Drawing.Size(151, 22);
            this.MenuStripDark_Main_Help_ReportABug.Text = "Report a bug...";
            this.MenuStripDark_Main_Help_ReportABug.Click += new System.EventHandler(this.MenuStripDark_Main_Help_ReportABug_Click);
            // 
            // MenuStripDark_Main_Help_ToolStripSeparator1
            // 
            this.MenuStripDark_Main_Help_ToolStripSeparator1.Name = "MenuStripDark_Main_Help_ToolStripSeparator1";
            this.MenuStripDark_Main_Help_ToolStripSeparator1.Size = new System.Drawing.Size(148, 6);
            // 
            // MenuStripDark_Main_Help_About
            // 
            this.MenuStripDark_Main_Help_About.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_Help_About.Image = global::Marathon.Toolkit.Properties.Resources.Task_Information;
            this.MenuStripDark_Main_Help_About.Name = "MenuStripDark_Main_Help_About";
            this.MenuStripDark_Main_Help_About.Size = new System.Drawing.Size(151, 22);
            this.MenuStripDark_Main_Help_About.Text = "&About";
            this.MenuStripDark_Main_Help_About.Click += new System.EventHandler(this.MenuStripDark_Main_Help_About_Click);
            // 
            // MenuStripDark_Main_Debug
            // 
            this.MenuStripDark_Main_Debug.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.MenuStripDark_Main_Debug.Name = "MenuStripDark_Main_Debug";
            this.MenuStripDark_Main_Debug.Size = new System.Drawing.Size(54, 20);
            this.MenuStripDark_Main_Debug.Text = "Debug";
            this.MenuStripDark_Main_Debug.Visible = false;
            // 
            // MenuStripDark_Main_File_New
            // 
            this.MenuStripDark_Main_File_New.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStripDark_Main_File_New_Archive});
            this.MenuStripDark_Main_File_New.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_File_New.Image = global::Marathon.Toolkit.Properties.Resources.Task_New;
            this.MenuStripDark_Main_File_New.Name = "MenuStripDark_Main_File_New";
            this.MenuStripDark_Main_File_New.Size = new System.Drawing.Size(180, 22);
            this.MenuStripDark_Main_File_New.Text = "&New";
            // 
            // MenuStripDark_Main_File_New_Archive
            // 
            this.MenuStripDark_Main_File_New_Archive.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_File_New_Archive.Image = global::Marathon.Toolkit.Properties.Resources.Task_Archive;
            this.MenuStripDark_Main_File_New_Archive.Name = "MenuStripDark_Main_File_New_Archive";
            this.MenuStripDark_Main_File_New_Archive.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.MenuStripDark_Main_File_New_Archive.Size = new System.Drawing.Size(166, 22);
            this.MenuStripDark_Main_File_New_Archive.Text = "&Archive...";
            // 
            // MenuStripDark_Main_File_Open
            // 
            this.MenuStripDark_Main_File_Open.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuStripDark_Main_File_Open_File,
            this.MenuStripDark_Main_File_Open_Folder});
            this.MenuStripDark_Main_File_Open.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_File_Open.Image = global::Marathon.Toolkit.Properties.Resources.Task_Open;
            this.MenuStripDark_Main_File_Open.Name = "MenuStripDark_Main_File_Open";
            this.MenuStripDark_Main_File_Open.Size = new System.Drawing.Size(180, 22);
            this.MenuStripDark_Main_File_Open.Text = "&Open";
            // 
            // MenuStripDark_Main_File_Open_File
            // 
            this.MenuStripDark_Main_File_Open_File.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_File_Open_File.Image = global::Marathon.Toolkit.Properties.Resources.Task_OpenFile;
            this.MenuStripDark_Main_File_Open_File.Name = "MenuStripDark_Main_File_Open_File";
            this.MenuStripDark_Main_File_Open_File.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.MenuStripDark_Main_File_Open_File.Size = new System.Drawing.Size(191, 22);
            this.MenuStripDark_Main_File_Open_File.Text = "&File...";
            this.MenuStripDark_Main_File_Open_File.Click += new System.EventHandler(this.MenuStripDark_Main_Open_File_Click);
            // 
            // MenuStripDark_Main_File_Open_Folder
            // 
            this.MenuStripDark_Main_File_Open_Folder.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_File_Open_Folder.Image = global::Marathon.Toolkit.Properties.Resources.Task_OpenFolder;
            this.MenuStripDark_Main_File_Open_Folder.Name = "MenuStripDark_Main_File_Open_Folder";
            this.MenuStripDark_Main_File_Open_Folder.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.MenuStripDark_Main_File_Open_Folder.Size = new System.Drawing.Size(191, 22);
            this.MenuStripDark_Main_File_Open_Folder.Text = "Fol&der...";
            this.MenuStripDark_Main_File_Open_Folder.Click += new System.EventHandler(this.MenuStripDark_Main_Open_Folder_Click);
            // 
            // MenuStripDark_Main_File_ToolStripSeparator1
            // 
            this.MenuStripDark_Main_File_ToolStripSeparator1.Name = "MenuStripDark_Main_File_ToolStripSeparator1";
            this.MenuStripDark_Main_File_ToolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // MenuStripDark_Main_File_Options
            // 
            this.MenuStripDark_Main_File_Options.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_File_Options.Image = global::Marathon.Toolkit.Properties.Resources.Task_Settings;
            this.MenuStripDark_Main_File_Options.Name = "MenuStripDark_Main_File_Options";
            this.MenuStripDark_Main_File_Options.Size = new System.Drawing.Size(180, 22);
            this.MenuStripDark_Main_File_Options.Text = "&Options...";
            this.MenuStripDark_Main_File_Options.Click += new System.EventHandler(this.MenuStripDark_Main_File_Options_Click);
            // 
            // MenuStripDark_Main_File_Exit
            // 
            this.MenuStripDark_Main_File_Exit.ForeColor = System.Drawing.SystemColors.Control;
            this.MenuStripDark_Main_File_Exit.Image = global::Marathon.Toolkit.Properties.Resources.Task_Exit;
            this.MenuStripDark_Main_File_Exit.Name = "MenuStripDark_Main_File_Exit";
            this.MenuStripDark_Main_File_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.MenuStripDark_Main_File_Exit.Size = new System.Drawing.Size(180, 22);
            this.MenuStripDark_Main_File_Exit.Text = "&Exit";
            this.MenuStripDark_Main_File_Exit.Click += new System.EventHandler(this.MenuStripDark_Main_File_Exit_Click);
            // 
            // DockPanel_Main
            // 
            this.DockPanel_Main.AllowDrop = true;
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
            // 
            // Workspace
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
            this.Name = "Workspace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Marathon Toolkit";
            this.MenuStripDark_Main.ResumeLayout(false);
            this.MenuStripDark_Main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Marathon.Toolkit.Components.MenuStripDark MenuStripDark_Main;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_File;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_Help;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_Help_About;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_File_Exit;
        private System.Windows.Forms.ToolStripSeparator MenuStripDark_Main_File_ToolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_File_Open;
        private WeifenLuo.WinFormsUI.Docking.DockPanel DockPanel_Main;
        private WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme Theme_VS2015Dark;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_File_New;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_File_New_Archive;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_File_Open_File;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_File_Open_Folder;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_Debug;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_Window;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_Window_Windows;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_File_Options;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_View;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_View_Output;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_View_FileConverter;
        private System.Windows.Forms.ToolStripMenuItem MenuStripDark_Main_Help_ReportABug;
        private System.Windows.Forms.ToolStripSeparator MenuStripDark_Main_Help_ToolStripSeparator1;
    }
}

