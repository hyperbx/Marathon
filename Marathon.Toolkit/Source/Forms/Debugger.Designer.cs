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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Debugger));
            this.Label_Title = new System.Windows.Forms.Label();
            this.Label_Subtitle = new System.Windows.Forms.Label();
            this.GroupBox_Properties = new System.Windows.Forms.GroupBox();
            this.TreeView_Properties = new System.Windows.Forms.TreeView();
            this.ButtonFlat_SaveSettings = new Marathon.Toolkit.Components.ButtonFlat();
            this.ButtonFlat_LoadSettings = new Marathon.Toolkit.Components.ButtonFlat();
            this.GroupBox_Properties.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label_Title
            // 
            this.Label_Title.AutoSize = true;
            this.Label_Title.BackColor = System.Drawing.Color.Transparent;
            this.Label_Title.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Title.Location = new System.Drawing.Point(12, 7);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(319, 45);
            this.Label_Title.TabIndex = 3;
            this.Label_Title.Text = "Marathon Debugger";
            // 
            // Label_Subtitle
            // 
            this.Label_Subtitle.AutoSize = true;
            this.Label_Subtitle.BackColor = System.Drawing.Color.Transparent;
            this.Label_Subtitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Subtitle.Location = new System.Drawing.Point(17, 51);
            this.Label_Subtitle.Name = "Label_Subtitle";
            this.Label_Subtitle.Size = new System.Drawing.Size(168, 21);
            this.Label_Subtitle.TabIndex = 4;
            this.Label_Subtitle.Text = "Nothing to see here...";
            // 
            // GroupBox_Properties
            // 
            this.GroupBox_Properties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GroupBox_Properties.Controls.Add(this.ButtonFlat_SaveSettings);
            this.GroupBox_Properties.Controls.Add(this.ButtonFlat_LoadSettings);
            this.GroupBox_Properties.Controls.Add(this.TreeView_Properties);
            this.GroupBox_Properties.ForeColor = System.Drawing.SystemColors.Control;
            this.GroupBox_Properties.Location = new System.Drawing.Point(23, 87);
            this.GroupBox_Properties.Name = "GroupBox_Properties";
            this.GroupBox_Properties.Size = new System.Drawing.Size(247, 411);
            this.GroupBox_Properties.TabIndex = 5;
            this.GroupBox_Properties.TabStop = false;
            this.GroupBox_Properties.Text = "Properties";
            // 
            // TreeView_Properties
            // 
            this.TreeView_Properties.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TreeView_Properties.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.TreeView_Properties.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TreeView_Properties.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.TreeView_Properties.ForeColor = System.Drawing.SystemColors.Control;
            this.TreeView_Properties.ItemHeight = 22;
            this.TreeView_Properties.Location = new System.Drawing.Point(3, 19);
            this.TreeView_Properties.Name = "TreeView_Properties";
            this.TreeView_Properties.ShowLines = false;
            this.TreeView_Properties.ShowPlusMinus = false;
            this.TreeView_Properties.ShowRootLines = false;
            this.TreeView_Properties.Size = new System.Drawing.Size(241, 358);
            this.TreeView_Properties.TabIndex = 6;
            this.TreeView_Properties.TabStop = false;
            this.TreeView_Properties.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_Properties_AfterSelect);
            this.TreeView_Properties.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_Properties_NodeMouseClick);
            // 
            // ButtonFlat_SaveSettings
            // 
            this.ButtonFlat_SaveSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonFlat_SaveSettings.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ButtonFlat_SaveSettings.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_SaveSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_SaveSettings.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_SaveSettings.Location = new System.Drawing.Point(125, 383);
            this.ButtonFlat_SaveSettings.Name = "ButtonFlat_SaveSettings";
            this.ButtonFlat_SaveSettings.Size = new System.Drawing.Size(118, 23);
            this.ButtonFlat_SaveSettings.TabIndex = 7;
            this.ButtonFlat_SaveSettings.Text = "Save";
            this.ButtonFlat_SaveSettings.UseVisualStyleBackColor = false;
            this.ButtonFlat_SaveSettings.Click += new System.EventHandler(this.ButtonFlat_SaveSettings_Click);
            // 
            // ButtonFlat_LoadSettings
            // 
            this.ButtonFlat_LoadSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ButtonFlat_LoadSettings.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ButtonFlat_LoadSettings.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_LoadSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_LoadSettings.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_LoadSettings.Location = new System.Drawing.Point(4, 383);
            this.ButtonFlat_LoadSettings.Name = "ButtonFlat_LoadSettings";
            this.ButtonFlat_LoadSettings.Size = new System.Drawing.Size(118, 23);
            this.ButtonFlat_LoadSettings.TabIndex = 6;
            this.ButtonFlat_LoadSettings.Text = "Load";
            this.ButtonFlat_LoadSettings.UseVisualStyleBackColor = false;
            this.ButtonFlat_LoadSettings.Click += new System.EventHandler(this.ButtonFlat_LoadSettings_Click);
            // 
            // Debugger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(933, 519);
            this.Controls.Add(this.GroupBox_Properties);
            this.Controls.Add(this.Label_Subtitle);
            this.Controls.Add(this.Label_Title);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Debugger";
            this.Text = "Marathon Debugger";
            this.GroupBox_Properties.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Label_Title;
        private System.Windows.Forms.Label Label_Subtitle;
        private System.Windows.Forms.GroupBox GroupBox_Properties;
        private System.Windows.Forms.TreeView TreeView_Properties;
        private Components.ButtonFlat ButtonFlat_LoadSettings;
        private Components.ButtonFlat ButtonFlat_SaveSettings;
    }
}