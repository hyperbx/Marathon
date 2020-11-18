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
            this.Label_Subtitle = new System.Windows.Forms.Label();
            this.GroupBox_Settings = new System.Windows.Forms.GroupBox();
            this.TreeView_Properties = new System.Windows.Forms.TreeView();
            this.ButtonFlat_Ribbon = new Marathon.Toolkit.Components.ButtonFlat();
            this.ButtonFlat_DirectX = new Marathon.Toolkit.Components.ButtonFlat();
            ((System.ComponentModel.ISupportInitialize)(this.KryptonRibbon_MarathonForm)).BeginInit();
            this.GroupBox_Settings.SuspendLayout();
            this.SuspendLayout();
            // 
            // KryptonRibbon_MarathonForm
            // 
            this.KryptonRibbon_MarathonForm.RibbonAppButton.AppButtonShowRecentDocs = false;
            this.KryptonRibbon_MarathonForm.Size = new System.Drawing.Size(944, 114);
            // 
            // Label_Subtitle
            // 
            this.Label_Subtitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Subtitle.AutoSize = true;
            this.Label_Subtitle.BackColor = System.Drawing.Color.Transparent;
            this.Label_Subtitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Subtitle.Location = new System.Drawing.Point(764, 468);
            this.Label_Subtitle.Name = "Label_Subtitle";
            this.Label_Subtitle.Size = new System.Drawing.Size(168, 21);
            this.Label_Subtitle.TabIndex = 4;
            this.Label_Subtitle.Text = "Nothing to see here...";
            // 
            // GroupBox_Settings
            // 
            this.GroupBox_Settings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GroupBox_Settings.Controls.Add(this.TreeView_Properties);
            this.GroupBox_Settings.ForeColor = System.Drawing.SystemColors.Control;
            this.GroupBox_Settings.Location = new System.Drawing.Point(12, 6);
            this.GroupBox_Settings.Name = "GroupBox_Settings";
            this.GroupBox_Settings.Size = new System.Drawing.Size(253, 483);
            this.GroupBox_Settings.TabIndex = 5;
            this.GroupBox_Settings.TabStop = false;
            this.GroupBox_Settings.Text = "Settings";
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
            this.TreeView_Properties.Location = new System.Drawing.Point(2, 18);
            this.TreeView_Properties.Name = "TreeView_Properties";
            this.TreeView_Properties.ShowLines = false;
            this.TreeView_Properties.ShowPlusMinus = false;
            this.TreeView_Properties.ShowRootLines = false;
            this.TreeView_Properties.Size = new System.Drawing.Size(249, 461);
            this.TreeView_Properties.TabIndex = 6;
            this.TreeView_Properties.TabStop = false;
            this.TreeView_Properties.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_Properties_AfterSelect);
            this.TreeView_Properties.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_Properties_NodeMouseClick);
            // 
            // ButtonFlat_Ribbon
            // 
            this.ButtonFlat_Ribbon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonFlat_Ribbon.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ButtonFlat_Ribbon.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_Ribbon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_Ribbon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_Ribbon.Location = new System.Drawing.Point(276, 43);
            this.ButtonFlat_Ribbon.Name = "ButtonFlat_Ribbon";
            this.ButtonFlat_Ribbon.Size = new System.Drawing.Size(656, 23);
            this.ButtonFlat_Ribbon.TabIndex = 7;
            this.ButtonFlat_Ribbon.Text = "Ribbon Tests";
            this.ButtonFlat_Ribbon.UseVisualStyleBackColor = false;
            this.ButtonFlat_Ribbon.Click += new System.EventHandler(this.ButtonFlat_Ribbon_Click);
            // 
            // ButtonFlat_DirectX
            // 
            this.ButtonFlat_DirectX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonFlat_DirectX.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ButtonFlat_DirectX.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_DirectX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_DirectX.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_DirectX.Location = new System.Drawing.Point(276, 14);
            this.ButtonFlat_DirectX.Name = "ButtonFlat_DirectX";
            this.ButtonFlat_DirectX.Size = new System.Drawing.Size(656, 23);
            this.ButtonFlat_DirectX.TabIndex = 6;
            this.ButtonFlat_DirectX.Text = "DirectX Tests";
            this.ButtonFlat_DirectX.UseVisualStyleBackColor = false;
            this.ButtonFlat_DirectX.Click += new System.EventHandler(this.ButtonFlat_DirectX_Click);
            // 
            // Debugger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.Controls.Add(this.ButtonFlat_Ribbon);
            this.Controls.Add(this.ButtonFlat_DirectX);
            this.Controls.Add(this.Label_Subtitle);
            this.Controls.Add(this.GroupBox_Settings);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Name = "Debugger";
            this.Text = "Marathon Debugger";
            this.UseRibbon = false;
            this.Controls.SetChildIndex(this.GroupBox_Settings, 0);
            this.Controls.SetChildIndex(this.Label_Subtitle, 0);
            this.Controls.SetChildIndex(this.ButtonFlat_DirectX, 0);
            this.Controls.SetChildIndex(this.ButtonFlat_Ribbon, 0);
            this.Controls.SetChildIndex(this.KryptonRibbon_MarathonForm, 0);
            ((System.ComponentModel.ISupportInitialize)(this.KryptonRibbon_MarathonForm)).EndInit();
            this.GroupBox_Settings.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Label_Subtitle;
        private System.Windows.Forms.GroupBox GroupBox_Settings;
        private System.Windows.Forms.TreeView TreeView_Properties;
        private Components.ButtonFlat ButtonFlat_DirectX;
        private Components.ButtonFlat ButtonFlat_Ribbon;
    }
}
