namespace Marathon.Toolkit.Forms
{
    partial class TextEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextEditor));
            this.TalkWindow_Body = new Marathon.Components.TalkWindow();
            this.kryptonDataGridView1 = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
            this.Column_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column_Body = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.KryptonRibbon_MarathonDockContent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // KryptonRibbon_MarathonDockContent
            // 
            this.KryptonRibbon_MarathonDockContent.RibbonAppButton.AppButtonShowRecentDocs = false;
            // 
            // TalkWindow_Body
            // 
            this.TalkWindow_Body.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.TalkWindow_Body.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TalkWindow_Body.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TalkWindow_Body.BackgroundImage")));
            this.TalkWindow_Body.Location = new System.Drawing.Point(194, 516);
            this.TalkWindow_Body.Name = "TalkWindow_Body";
            this.TalkWindow_Body.Size = new System.Drawing.Size(877, 158);
            this.TalkWindow_Body.TabIndex = 1;
            // 
            // kryptonDataGridView1
            // 
            this.kryptonDataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonDataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.kryptonDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kryptonDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column_Name,
            this.Column_Body});
            this.kryptonDataGridView1.Location = new System.Drawing.Point(12, 12);
            this.kryptonDataGridView1.Name = "kryptonDataGridView1";
            this.kryptonDataGridView1.Size = new System.Drawing.Size(1240, 498);
            this.kryptonDataGridView1.StateCommon.Background.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.kryptonDataGridView1.StateCommon.Background.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.kryptonDataGridView1.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
            this.kryptonDataGridView1.TabIndex = 2;
            // 
            // Column_Name
            // 
            this.Column_Name.HeaderText = "Name";
            this.Column_Name.Name = "Column_Name";
            // 
            // Column_Body
            // 
            this.Column_Body.HeaderText = "Body";
            this.Column_Body.Name = "Column_Body";
            // 
            // TextEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.kryptonDataGridView1);
            this.Controls.Add(this.TalkWindow_Body);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.InDesignHelperMode = false;
            this.Name = "TextEditor";
            this.Text = "Text Editor";
            this.Controls.SetChildIndex(this.KryptonRibbon_MarathonDockContent, 0);
            this.Controls.SetChildIndex(this.TalkWindow_Body, 0);
            this.Controls.SetChildIndex(this.kryptonDataGridView1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.KryptonRibbon_MarathonDockContent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Components.TalkWindow TalkWindow_Body;
        private ComponentFactory.Krypton.Toolkit.KryptonDataGridView kryptonDataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column_Body;
    }
}