namespace Marathon
{
    partial class MarathonErrorHandler
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarathonErrorHandler));
            this.Label_Title = new System.Windows.Forms.Label();
            this.RichTextBox_Error = new System.Windows.Forms.RichTextBox();
            this.Panel_RichTextBox_Container = new System.Windows.Forms.Panel();
            this.PictureBox_Logo = new System.Windows.Forms.PictureBox();
            this.ButtonFlat_GitHub = new Marathon.Components.ButtonFlat();
            this.ButtonFlat_OK = new Marathon.Components.ButtonFlat();
            this.Panel_RichTextBox_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // Label_Title
            // 
            this.Label_Title.AutoSize = true;
            this.Label_Title.Font = new System.Drawing.Font("Segoe UI Semilight", 14.8F);
            this.Label_Title.Location = new System.Drawing.Point(9, 9);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(343, 28);
            this.Label_Title.TabIndex = 0;
            this.Label_Title.Text = "An unhandled exception has occurred...";
            // 
            // RichTextBox_Error
            // 
            this.RichTextBox_Error.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.RichTextBox_Error.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichTextBox_Error.Cursor = System.Windows.Forms.Cursors.Default;
            this.RichTextBox_Error.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RichTextBox_Error.ForeColor = System.Drawing.SystemColors.Control;
            this.RichTextBox_Error.Location = new System.Drawing.Point(0, 0);
            this.RichTextBox_Error.Name = "RichTextBox_Error";
            this.RichTextBox_Error.ReadOnly = true;
            this.RichTextBox_Error.Size = new System.Drawing.Size(918, 404);
            this.RichTextBox_Error.TabIndex = 1;
            this.RichTextBox_Error.Text = "";
            // 
            // Panel_RichTextBox_Container
            // 
            this.Panel_RichTextBox_Container.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_RichTextBox_Container.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_RichTextBox_Container.Controls.Add(this.RichTextBox_Error);
            this.Panel_RichTextBox_Container.Location = new System.Drawing.Point(12, 49);
            this.Panel_RichTextBox_Container.Name = "Panel_RichTextBox_Container";
            this.Panel_RichTextBox_Container.Size = new System.Drawing.Size(920, 406);
            this.Panel_RichTextBox_Container.TabIndex = 3;
            // 
            // PictureBox_Logo
            // 
            this.PictureBox_Logo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBox_Logo.BackgroundImage = global::Marathon.Properties.Resources.Main_LogoSmall;
            this.PictureBox_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PictureBox_Logo.Location = new System.Drawing.Point(881, 0);
            this.PictureBox_Logo.Name = "PictureBox_Logo";
            this.PictureBox_Logo.Size = new System.Drawing.Size(51, 50);
            this.PictureBox_Logo.TabIndex = 4;
            this.PictureBox_Logo.TabStop = false;
            // 
            // ButtonFlat_GitHub
            // 
            this.ButtonFlat_GitHub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonFlat_GitHub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(218)))), ((int)(((byte)(241)))));
            this.ButtonFlat_GitHub.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_GitHub.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_GitHub.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_GitHub.Location = new System.Drawing.Point(773, 466);
            this.ButtonFlat_GitHub.Name = "ButtonFlat_GitHub";
            this.ButtonFlat_GitHub.Size = new System.Drawing.Size(75, 23);
            this.ButtonFlat_GitHub.TabIndex = 6;
            this.ButtonFlat_GitHub.Text = "Report";
            this.ButtonFlat_GitHub.UseVisualStyleBackColor = false;
            this.ButtonFlat_GitHub.Click += new System.EventHandler(this.ButtonFlat_GitHub_Click);
            // 
            // ButtonFlat_OK
            // 
            this.ButtonFlat_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonFlat_OK.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ButtonFlat_OK.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_OK.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_OK.Location = new System.Drawing.Point(857, 466);
            this.ButtonFlat_OK.Name = "ButtonFlat_OK";
            this.ButtonFlat_OK.Size = new System.Drawing.Size(75, 23);
            this.ButtonFlat_OK.TabIndex = 5;
            this.ButtonFlat_OK.Text = "Close";
            this.ButtonFlat_OK.UseVisualStyleBackColor = false;
            this.ButtonFlat_OK.Click += new System.EventHandler(this.ButtonFlat_Close_Click);
            // 
            // MarathonErrorHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.Controls.Add(this.ButtonFlat_GitHub);
            this.Controls.Add(this.ButtonFlat_OK);
            this.Controls.Add(this.Panel_RichTextBox_Container);
            this.Controls.Add(this.Label_Title);
            this.Controls.Add(this.PictureBox_Logo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(450, 450);
            this.Name = "MarathonErrorHandler";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Marathon Error Handler";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MarathonErrorHandler_FormClosing);
            this.Panel_RichTextBox_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_Title;
        private System.Windows.Forms.RichTextBox RichTextBox_Error;
        private System.Windows.Forms.Panel Panel_RichTextBox_Container;
        private System.Windows.Forms.PictureBox PictureBox_Logo;
        private Components.ButtonFlat ButtonFlat_OK;
        private Components.ButtonFlat ButtonFlat_GitHub;
    }
}
