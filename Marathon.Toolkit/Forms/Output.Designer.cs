namespace Marathon.Toolkit.Forms
{
    partial class Output
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
            this.MarathonRichTextBox_Console = new Marathon.Components.MarathonRichTextBox();
            this.ButtonDark_ClearAll = new Marathon.Components.ButtonDark();
            this.Line_Separator_2 = new Marathon.Components.Line();
            this.ButtonDark_ToggleWordWrap = new Marathon.Components.ButtonDark();
            this.ToolTipDark_StripButtons = new Marathon.Components.ToolTipDark();
            ((System.ComponentModel.ISupportInitialize)(this.KryptonRibbon_MarathonDockContent)).BeginInit();
            this.SuspendLayout();
            // 
            // KryptonRibbon_MarathonDockContent
            // 
            this.KryptonRibbon_MarathonDockContent.RibbonAppButton.AppButtonShowRecentDocs = false;
            // 
            // MarathonRichTextBox_Console
            // 
            this.MarathonRichTextBox_Console.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MarathonRichTextBox_Console.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.MarathonRichTextBox_Console.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MarathonRichTextBox_Console.ContentPadding = new System.Windows.Forms.Padding(13, 3, 0, 0);
            this.MarathonRichTextBox_Console.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MarathonRichTextBox_Console.Font = new System.Drawing.Font("Consolas", 9F);
            this.MarathonRichTextBox_Console.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(241)))), ((int)(((byte)(241)))));
            this.MarathonRichTextBox_Console.Location = new System.Drawing.Point(0, 26);
            this.MarathonRichTextBox_Console.LockInput = false;
            this.MarathonRichTextBox_Console.Name = "MarathonRichTextBox_Console";
            this.MarathonRichTextBox_Console.ReadOnly = true;
            this.MarathonRichTextBox_Console.Size = new System.Drawing.Size(1264, 655);
            this.MarathonRichTextBox_Console.TabIndex = 0;
            this.MarathonRichTextBox_Console.TabStop = false;
            this.MarathonRichTextBox_Console.Text = "";
            this.MarathonRichTextBox_Console.Transparent = false;
            this.MarathonRichTextBox_Console.WordWrap = false;
            this.MarathonRichTextBox_Console.WordWrapToContentPadding = false;
            this.MarathonRichTextBox_Console.TextChanged += new System.EventHandler(this.MarathonRichTextBox_Console_TextChanged);
            this.MarathonRichTextBox_Console.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RichTextBoxLocked_Console_MouseDown);
            // 
            // ButtonDark_ClearAll
            // 
            this.ButtonDark_ClearAll.BackColor = System.Drawing.Color.Transparent;
            this.ButtonDark_ClearAll.BackgroundImage = global::Marathon.Toolkit.Properties.Resources.Task_Strip_ClearWindowContent;
            this.ButtonDark_ClearAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonDark_ClearAll.Checked = false;
            this.ButtonDark_ClearAll.FlatAppearance.BorderSize = 0;
            this.ButtonDark_ClearAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.ButtonDark_ClearAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(65)))));
            this.ButtonDark_ClearAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDark_ClearAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonDark_ClearAll.FormBackColour = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ButtonDark_ClearAll.Location = new System.Drawing.Point(4, 3);
            this.ButtonDark_ClearAll.Name = "ButtonDark_ClearAll";
            this.ButtonDark_ClearAll.Size = new System.Drawing.Size(20, 20);
            this.ButtonDark_ClearAll.TabIndex = 1;
            this.ToolTipDark_StripButtons.SetToolTip(this.ButtonDark_ClearAll, "Clear All");
            this.ButtonDark_ClearAll.UseVisualStyleBackColor = false;
            this.ButtonDark_ClearAll.Click += new System.EventHandler(this.ButtonFlat_ClearAll_Click);
            // 
            // Line_Separator_2
            // 
            this.Line_Separator_2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.Line_Separator_2.DropShadow = true;
            this.Line_Separator_2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Line_Separator_2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.Line_Separator_2.LineAlignment = Marathon.Components.Line.Alignment.Vertical;
            this.Line_Separator_2.LineWidth = 1;
            this.Line_Separator_2.Location = new System.Drawing.Point(26, 4);
            this.Line_Separator_2.Name = "Line_Separator_2";
            this.Line_Separator_2.Size = new System.Drawing.Size(5, 18);
            this.Line_Separator_2.TabIndex = 7;
            // 
            // ButtonDark_ToggleWordWrap
            // 
            this.ButtonDark_ToggleWordWrap.BackColor = System.Drawing.Color.Transparent;
            this.ButtonDark_ToggleWordWrap.BackgroundImage = global::Marathon.Toolkit.Properties.Resources.Task_Strip_WordWrap;
            this.ButtonDark_ToggleWordWrap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ButtonDark_ToggleWordWrap.Checked = false;
            this.ButtonDark_ToggleWordWrap.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.ButtonDark_ToggleWordWrap.FlatAppearance.BorderSize = 0;
            this.ButtonDark_ToggleWordWrap.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.ButtonDark_ToggleWordWrap.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(65)))));
            this.ButtonDark_ToggleWordWrap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDark_ToggleWordWrap.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonDark_ToggleWordWrap.FormBackColour = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ButtonDark_ToggleWordWrap.Location = new System.Drawing.Point(35, 3);
            this.ButtonDark_ToggleWordWrap.Name = "ButtonDark_ToggleWordWrap";
            this.ButtonDark_ToggleWordWrap.Size = new System.Drawing.Size(20, 20);
            this.ButtonDark_ToggleWordWrap.TabIndex = 2;
            this.ToolTipDark_StripButtons.SetToolTip(this.ButtonDark_ToggleWordWrap, "Toggle Word Wrap");
            this.ButtonDark_ToggleWordWrap.UseVisualStyleBackColor = false;
            this.ButtonDark_ToggleWordWrap.Click += new System.EventHandler(this.ButtonFlat_ToggleWordWrap_Click);
            // 
            // ToolTipDark_StripButtons
            // 
            this.ToolTipDark_StripButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ToolTipDark_StripButtons.ForeColor = System.Drawing.SystemColors.Control;
            this.ToolTipDark_StripButtons.OwnerDraw = true;
            this.ToolTipDark_StripButtons.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // Output
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.BruteforceFlickerPrevention = true;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.ButtonDark_ToggleWordWrap);
            this.Controls.Add(this.Line_Separator_2);
            this.Controls.Add(this.ButtonDark_ClearAll);
            this.Controls.Add(this.MarathonRichTextBox_Console);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.InDesignHelperMode = false;
            this.Name = "Output";
            this.Text = "Output";
            this.ToolWindow = true;
            this.UseRibbon = false;
            this.Controls.SetChildIndex(this.MarathonRichTextBox_Console, 0);
            this.Controls.SetChildIndex(this.ButtonDark_ClearAll, 0);
            this.Controls.SetChildIndex(this.Line_Separator_2, 0);
            this.Controls.SetChildIndex(this.ButtonDark_ToggleWordWrap, 0);
            this.Controls.SetChildIndex(this.KryptonRibbon_MarathonDockContent, 0);
            ((System.ComponentModel.ISupportInitialize)(this.KryptonRibbon_MarathonDockContent)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Components.MarathonRichTextBox MarathonRichTextBox_Console;
        private Components.ButtonDark ButtonDark_ClearAll;
        private Components.Line Line_Separator_2;
        private Components.ButtonDark ButtonDark_ToggleWordWrap;
        private Components.ToolTipDark ToolTipDark_StripButtons;
    }
}
