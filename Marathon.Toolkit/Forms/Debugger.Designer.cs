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
            this.Label_Subtitle = new System.Windows.Forms.Label();
            this.GroupBox_Settings = new System.Windows.Forms.GroupBox();
            this.TreeView_Properties = new System.Windows.Forms.TreeView();
            this.ButtonFlat_DirectX = new Marathon.Toolkit.Components.ButtonFlat();
            this.GroupBox_Settings.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label_Subtitle
            // 
            this.Label_Subtitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Subtitle.AutoSize = true;
            this.Label_Subtitle.BackColor = System.Drawing.Color.Transparent;
            this.Label_Subtitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Subtitle.Location = new System.Drawing.Point(753, 486);
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
            this.GroupBox_Settings.Location = new System.Drawing.Point(12, 9);
            this.GroupBox_Settings.Name = "GroupBox_Settings";
            this.GroupBox_Settings.Size = new System.Drawing.Size(247, 498);
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
            this.TreeView_Properties.Location = new System.Drawing.Point(3, 19);
            this.TreeView_Properties.Name = "TreeView_Properties";
            this.TreeView_Properties.ShowLines = false;
            this.TreeView_Properties.ShowPlusMinus = false;
            this.TreeView_Properties.ShowRootLines = false;
            this.TreeView_Properties.Size = new System.Drawing.Size(241, 473);
            this.TreeView_Properties.TabIndex = 6;
            this.TreeView_Properties.TabStop = false;
            this.TreeView_Properties.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_Properties_AfterSelect);
            this.TreeView_Properties.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeView_Properties_NodeMouseClick);
            // 
            // ButtonFlat_DirectX
            // 
            this.ButtonFlat_DirectX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonFlat_DirectX.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ButtonFlat_DirectX.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_DirectX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_DirectX.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_DirectX.Location = new System.Drawing.Point(271, 17);
            this.ButtonFlat_DirectX.Name = "ButtonFlat_DirectX";
            this.ButtonFlat_DirectX.Size = new System.Drawing.Size(649, 23);
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
            this.ClientSize = new System.Drawing.Size(933, 519);
            this.Controls.Add(this.ButtonFlat_DirectX);
            this.Controls.Add(this.GroupBox_Settings);
            this.Controls.Add(this.Label_Subtitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Debugger";
            this.Text = "Marathon Debugger";
            this.GroupBox_Settings.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Label_Subtitle;
        private System.Windows.Forms.GroupBox GroupBox_Settings;
        private System.Windows.Forms.TreeView TreeView_Properties;
        private Components.ButtonFlat ButtonFlat_DirectX;
    }
}
