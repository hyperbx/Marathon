namespace Marathon.Toolkit.Forms
{
    partial class ErrorHandler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ErrorHandler));
            this.Label_Title = new System.Windows.Forms.Label();
            this.Panel_RichTextBox_Container = new System.Windows.Forms.Panel();
            this.MarathonRichTextBox_Error = new Marathon.Components.MarathonRichTextBox();
            this.PictureBox_Logo = new System.Windows.Forms.PictureBox();
            this.ButtonDark_GitHub = new Marathon.Components.ButtonDark();
            this.ButtonDark_Close = new Marathon.Components.ButtonDark();
            this.ButtonDark_Copy = new Marathon.Components.ButtonDark();
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
            this.Label_Title.Size = new System.Drawing.Size(542, 28);
            this.Label_Title.TabIndex = 0;
            this.Label_Title.Text = "A fatal error has occurred and the application has been halted...";
            // 
            // Panel_RichTextBox_Container
            // 
            this.Panel_RichTextBox_Container.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_RichTextBox_Container.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_RichTextBox_Container.Controls.Add(this.MarathonRichTextBox_Error);
            this.Panel_RichTextBox_Container.Location = new System.Drawing.Point(12, 49);
            this.Panel_RichTextBox_Container.Name = "Panel_RichTextBox_Container";
            this.Panel_RichTextBox_Container.Size = new System.Drawing.Size(920, 406);
            this.Panel_RichTextBox_Container.TabIndex = 3;
            // 
            // MarathonRichTextBox_Error
            // 
            this.MarathonRichTextBox_Error.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.MarathonRichTextBox_Error.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MarathonRichTextBox_Error.ContentPadding = new System.Windows.Forms.Padding(0);
            this.MarathonRichTextBox_Error.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.MarathonRichTextBox_Error.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MarathonRichTextBox_Error.ForeColor = System.Drawing.SystemColors.Window;
            this.MarathonRichTextBox_Error.Location = new System.Drawing.Point(0, 0);
            this.MarathonRichTextBox_Error.LockInput = false;
            this.MarathonRichTextBox_Error.Name = "MarathonRichTextBox_Error";
            this.MarathonRichTextBox_Error.ReadOnly = true;
            this.MarathonRichTextBox_Error.Size = new System.Drawing.Size(918, 404);
            this.MarathonRichTextBox_Error.TabIndex = 2;
            this.MarathonRichTextBox_Error.TabStop = false;
            this.MarathonRichTextBox_Error.Text = "";
            this.MarathonRichTextBox_Error.Transparent = false;
            this.MarathonRichTextBox_Error.WordWrap = false;
            this.MarathonRichTextBox_Error.WordWrapToContentPadding = false;
            // 
            // PictureBox_Logo
            // 
            this.PictureBox_Logo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBox_Logo.BackgroundImage = global::Marathon.Toolkit.Properties.Resources.Toolkit_Small_Colour;
            this.PictureBox_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PictureBox_Logo.Location = new System.Drawing.Point(881, 0);
            this.PictureBox_Logo.Name = "PictureBox_Logo";
            this.PictureBox_Logo.Size = new System.Drawing.Size(51, 50);
            this.PictureBox_Logo.TabIndex = 4;
            this.PictureBox_Logo.TabStop = false;
            // 
            // ButtonDark_GitHub
            // 
            this.ButtonDark_GitHub.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonDark_GitHub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ButtonDark_GitHub.Checked = false;
            this.ButtonDark_GitHub.FlatAppearance.BorderSize = 0;
            this.ButtonDark_GitHub.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDark_GitHub.ForeColor = System.Drawing.SystemColors.Control;
            this.ButtonDark_GitHub.FormBackColour = System.Drawing.Color.Empty;
            this.ButtonDark_GitHub.Location = new System.Drawing.Point(674, 466);
            this.ButtonDark_GitHub.Name = "ButtonDark_GitHub";
            this.ButtonDark_GitHub.Size = new System.Drawing.Size(124, 23);
            this.ButtonDark_GitHub.TabIndex = 6;
            this.ButtonDark_GitHub.Text = "Report to GitHub";
            this.ButtonDark_GitHub.UseVisualStyleBackColor = false;
            this.ButtonDark_GitHub.Click += new System.EventHandler(this.ButtonDark_GitHub_Click);
            // 
            // ButtonDark_Close
            // 
            this.ButtonDark_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonDark_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ButtonDark_Close.Checked = false;
            this.ButtonDark_Close.FlatAppearance.BorderSize = 0;
            this.ButtonDark_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDark_Close.ForeColor = System.Drawing.SystemColors.Control;
            this.ButtonDark_Close.FormBackColour = System.Drawing.Color.Empty;
            this.ButtonDark_Close.Location = new System.Drawing.Point(808, 466);
            this.ButtonDark_Close.Name = "ButtonDark_Close";
            this.ButtonDark_Close.Size = new System.Drawing.Size(124, 23);
            this.ButtonDark_Close.TabIndex = 5;
            this.ButtonDark_Close.Text = "Ignore and Close";
            this.ButtonDark_Close.UseVisualStyleBackColor = false;
            this.ButtonDark_Close.Click += new System.EventHandler(this.ButtonDark_Close_Click);
            // 
            // ButtonDark_Copy
            // 
            this.ButtonDark_Copy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonDark_Copy.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ButtonDark_Copy.Checked = false;
            this.ButtonDark_Copy.FlatAppearance.BorderSize = 0;
            this.ButtonDark_Copy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDark_Copy.ForeColor = System.Drawing.SystemColors.Control;
            this.ButtonDark_Copy.FormBackColour = System.Drawing.Color.Empty;
            this.ButtonDark_Copy.Location = new System.Drawing.Point(540, 466);
            this.ButtonDark_Copy.Name = "ButtonDark_Copy";
            this.ButtonDark_Copy.Size = new System.Drawing.Size(124, 23);
            this.ButtonDark_Copy.TabIndex = 7;
            this.ButtonDark_Copy.Text = "Copy to Clipboard";
            this.ButtonDark_Copy.UseVisualStyleBackColor = false;
            this.ButtonDark_Copy.Click += new System.EventHandler(this.ButtonDark_Copy_Click);
            // 
            // ErrorHandler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.Controls.Add(this.ButtonDark_Copy);
            this.Controls.Add(this.ButtonDark_GitHub);
            this.Controls.Add(this.ButtonDark_Close);
            this.Controls.Add(this.Panel_RichTextBox_Container);
            this.Controls.Add(this.Label_Title);
            this.Controls.Add(this.PictureBox_Logo);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(635, 540);
            this.Name = "ErrorHandler";
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
        private System.Windows.Forms.Panel Panel_RichTextBox_Container;
        private System.Windows.Forms.PictureBox PictureBox_Logo;
        private Components.ButtonDark ButtonDark_Close;
        private Components.ButtonDark ButtonDark_GitHub;
        private Components.ButtonDark ButtonDark_Copy;
        private Components.MarathonRichTextBox MarathonRichTextBox_Error;
    }
}
