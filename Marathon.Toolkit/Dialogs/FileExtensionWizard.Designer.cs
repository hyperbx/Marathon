namespace Marathon.Toolkit.Forms
{
    partial class FileExtensionWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileExtensionWizard));
            this.Label_Title = new System.Windows.Forms.Label();
            this.PictureBox_Logo = new System.Windows.Forms.PictureBox();
            this.Label_Subtitle = new System.Windows.Forms.Label();
            this.FlowLayoutPanel_Tasks = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // Label_Title
            // 
            this.Label_Title.AutoSize = true;
            this.Label_Title.Font = new System.Drawing.Font("Segoe UI Semilight", 14.8F);
            this.Label_Title.Location = new System.Drawing.Point(9, 9);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(577, 28);
            this.Label_Title.TabIndex = 0;
            this.Label_Title.Text = "The selected file uses a common extension supported by Marathon.";
            // 
            // PictureBox_Logo
            // 
            this.PictureBox_Logo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBox_Logo.BackgroundImage = global::Marathon.Toolkit.Properties.Resources.Main_Logo_Small_Dark;
            this.PictureBox_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PictureBox_Logo.Location = new System.Drawing.Point(679, 0);
            this.PictureBox_Logo.Name = "PictureBox_Logo";
            this.PictureBox_Logo.Size = new System.Drawing.Size(51, 50);
            this.PictureBox_Logo.TabIndex = 5;
            this.PictureBox_Logo.TabStop = false;
            // 
            // Label_Subtitle
            // 
            this.Label_Subtitle.AutoSize = true;
            this.Label_Subtitle.Font = new System.Drawing.Font("Segoe UI Semilight", 14.8F);
            this.Label_Subtitle.Location = new System.Drawing.Point(46, 71);
            this.Label_Subtitle.Name = "Label_Subtitle";
            this.Label_Subtitle.Size = new System.Drawing.Size(324, 28);
            this.Label_Subtitle.TabIndex = 7;
            this.Label_Subtitle.Text = "What task would you like to perform?";
            // 
            // FlowLayoutPanel_Tasks
            // 
            this.FlowLayoutPanel_Tasks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FlowLayoutPanel_Tasks.AutoScroll = true;
            this.FlowLayoutPanel_Tasks.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FlowLayoutPanel_Tasks.Location = new System.Drawing.Point(12, 130);
            this.FlowLayoutPanel_Tasks.Name = "FlowLayoutPanel_Tasks";
            this.FlowLayoutPanel_Tasks.Padding = new System.Windows.Forms.Padding(65, 0, 0, 0);
            this.FlowLayoutPanel_Tasks.Size = new System.Drawing.Size(713, 177);
            this.FlowLayoutPanel_Tasks.TabIndex = 8;
            // 
            // FileExtensionWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.ClientSize = new System.Drawing.Size(737, 319);
            this.Controls.Add(this.FlowLayoutPanel_Tasks);
            this.Controls.Add(this.Label_Subtitle);
            this.Controls.Add(this.PictureBox_Logo);
            this.Controls.Add(this.Label_Title);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Float;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(665, 358);
            this.Name = "FileExtensionWizard";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "File Extension Wizard";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_Title;
        private System.Windows.Forms.PictureBox PictureBox_Logo;
        private System.Windows.Forms.Label Label_Subtitle;
        private System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel_Tasks;
    }
}