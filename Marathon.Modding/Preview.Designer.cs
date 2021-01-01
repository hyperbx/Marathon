namespace Marathon.Modding
{
    partial class Preview
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
            this.PictureBox_Thumbnail = new System.Windows.Forms.PictureBox();
            this.Label_Title = new System.Windows.Forms.Label();
            this.Label_Author = new System.Windows.Forms.Label();
            this.Panel_Blur = new System.Windows.Forms.Panel();
            this.TabControlFlat_Mod = new Marathon.Components.TabControlFlat();
            this.TabPage_Description = new System.Windows.Forms.TabPage();
            this.MarathonRichTextBox_Description = new Marathon.Components.MarathonRichTextBox();
            this.TabPage_Metadata = new System.Windows.Forms.TabPage();
            this.MarathonRichTextBox_Information = new Marathon.Components.MarathonRichTextBox();
            this.TabPage_Configuration = new System.Windows.Forms.TabPage();
            this.MarathonRichTextBox_Technical = new Marathon.Components.MarathonRichTextBox();
            this.ToolTipDark_Title = new Marathon.Components.ToolTipDark();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Thumbnail)).BeginInit();
            this.Panel_Blur.SuspendLayout();
            this.TabControlFlat_Mod.SuspendLayout();
            this.TabPage_Description.SuspendLayout();
            this.TabPage_Metadata.SuspendLayout();
            this.TabPage_Configuration.SuspendLayout();
            this.SuspendLayout();
            // 
            // PictureBox_Thumbnail
            // 
            this.PictureBox_Thumbnail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PictureBox_Thumbnail.BackColor = System.Drawing.Color.Transparent;
            this.PictureBox_Thumbnail.BackgroundImage = global::Marathon.Modding.Properties.Resources.Manager_Full_Colour;
            this.PictureBox_Thumbnail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PictureBox_Thumbnail.Location = new System.Drawing.Point(0, 11);
            this.PictureBox_Thumbnail.Name = "PictureBox_Thumbnail";
            this.PictureBox_Thumbnail.Size = new System.Drawing.Size(369, 208);
            this.PictureBox_Thumbnail.TabIndex = 0;
            this.PictureBox_Thumbnail.TabStop = false;
            // 
            // Label_Title
            // 
            this.Label_Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Title.BackColor = System.Drawing.Color.Transparent;
            this.Label_Title.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.Label_Title.Location = new System.Drawing.Point(0, 234);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(369, 30);
            this.Label_Title.TabIndex = 2;
            this.Label_Title.Text = "Select a mod";
            this.Label_Title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Label_Title.UseMnemonic = false;
            // 
            // Label_Author
            // 
            this.Label_Author.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Author.BackColor = System.Drawing.Color.Transparent;
            this.Label_Author.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.Label_Author.Location = new System.Drawing.Point(0, 264);
            this.Label_Author.Name = "Label_Author";
            this.Label_Author.Size = new System.Drawing.Size(369, 30);
            this.Label_Author.TabIndex = 3;
            this.Label_Author.Text = "to preview";
            this.Label_Author.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Label_Author.UseMnemonic = false;
            // 
            // Panel_Blur
            // 
            this.Panel_Blur.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Blur.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel_Blur.Controls.Add(this.PictureBox_Thumbnail);
            this.Panel_Blur.Controls.Add(this.Label_Author);
            this.Panel_Blur.Controls.Add(this.Label_Title);
            this.Panel_Blur.Location = new System.Drawing.Point(0, 0);
            this.Panel_Blur.Name = "Panel_Blur";
            this.Panel_Blur.Size = new System.Drawing.Size(369, 312);
            this.Panel_Blur.TabIndex = 5;
            // 
            // TabControlFlat_Mod
            // 
            this.TabControlFlat_Mod.ActiveColour = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.TabControlFlat_Mod.AllowDragging = false;
            this.TabControlFlat_Mod.AllowDrop = true;
            this.TabControlFlat_Mod.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControlFlat_Mod.BorderColour = System.Drawing.Color.Transparent;
            this.TabControlFlat_Mod.CloseButtonColour = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(230)))), ((int)(((byte)(245)))));
            this.TabControlFlat_Mod.CloseOnMiddleClick = false;
            this.TabControlFlat_Mod.ClosingMessage = null;
            this.TabControlFlat_Mod.Controls.Add(this.TabPage_Description);
            this.TabControlFlat_Mod.Controls.Add(this.TabPage_Metadata);
            this.TabControlFlat_Mod.Controls.Add(this.TabPage_Configuration);
            this.TabControlFlat_Mod.HeaderColour = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabControlFlat_Mod.HorizontalLineColour = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.TabControlFlat_Mod.Location = new System.Drawing.Point(0, 312);
            this.TabControlFlat_Mod.Name = "TabControlFlat_Mod";
            this.TabControlFlat_Mod.SelectedIndex = 0;
            this.TabControlFlat_Mod.SelectedTextColour = System.Drawing.SystemColors.Control;
            this.TabControlFlat_Mod.ShowCloseButton = false;
            this.TabControlFlat_Mod.ShowClosingMessage = false;
            this.TabControlFlat_Mod.Size = new System.Drawing.Size(369, 120);
            this.TabControlFlat_Mod.TabIndex = 4;
            this.TabControlFlat_Mod.TabPageBackColour = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabControlFlat_Mod.TextColour = System.Drawing.SystemColors.Control;
            // 
            // TabPage_Description
            // 
            this.TabPage_Description.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage_Description.Controls.Add(this.MarathonRichTextBox_Description);
            this.TabPage_Description.Location = new System.Drawing.Point(4, 25);
            this.TabPage_Description.Name = "TabPage_Description";
            this.TabPage_Description.Size = new System.Drawing.Size(361, 91);
            this.TabPage_Description.TabIndex = 0;
            this.TabPage_Description.Text = "Description";
            // 
            // MarathonRichTextBox_Description
            // 
            this.MarathonRichTextBox_Description.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.MarathonRichTextBox_Description.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MarathonRichTextBox_Description.ContentPadding = new System.Windows.Forms.Padding(0);
            this.MarathonRichTextBox_Description.Cursor = System.Windows.Forms.Cursors.Default;
            this.MarathonRichTextBox_Description.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MarathonRichTextBox_Description.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MarathonRichTextBox_Description.ForeColor = System.Drawing.SystemColors.Control;
            this.MarathonRichTextBox_Description.Location = new System.Drawing.Point(0, 0);
            this.MarathonRichTextBox_Description.LockInput = true;
            this.MarathonRichTextBox_Description.Name = "MarathonRichTextBox_Description";
            this.MarathonRichTextBox_Description.ReadOnly = true;
            this.MarathonRichTextBox_Description.Size = new System.Drawing.Size(361, 91);
            this.MarathonRichTextBox_Description.TabIndex = 0;
            this.MarathonRichTextBox_Description.Text = "";
            this.MarathonRichTextBox_Description.Transparent = false;
            this.MarathonRichTextBox_Description.WordWrapToContentPadding = false;
            // 
            // TabPage_Metadata
            // 
            this.TabPage_Metadata.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage_Metadata.Controls.Add(this.MarathonRichTextBox_Information);
            this.TabPage_Metadata.Location = new System.Drawing.Point(4, 25);
            this.TabPage_Metadata.Name = "TabPage_Metadata";
            this.TabPage_Metadata.Size = new System.Drawing.Size(361, 91);
            this.TabPage_Metadata.TabIndex = 1;
            this.TabPage_Metadata.Text = "Metadata";
            // 
            // MarathonRichTextBox_Information
            // 
            this.MarathonRichTextBox_Information.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.MarathonRichTextBox_Information.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MarathonRichTextBox_Information.ContentPadding = new System.Windows.Forms.Padding(0);
            this.MarathonRichTextBox_Information.Cursor = System.Windows.Forms.Cursors.Default;
            this.MarathonRichTextBox_Information.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MarathonRichTextBox_Information.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MarathonRichTextBox_Information.ForeColor = System.Drawing.SystemColors.Control;
            this.MarathonRichTextBox_Information.Location = new System.Drawing.Point(0, 0);
            this.MarathonRichTextBox_Information.LockInput = true;
            this.MarathonRichTextBox_Information.Name = "MarathonRichTextBox_Information";
            this.MarathonRichTextBox_Information.ReadOnly = true;
            this.MarathonRichTextBox_Information.Size = new System.Drawing.Size(361, 91);
            this.MarathonRichTextBox_Information.TabIndex = 1;
            this.MarathonRichTextBox_Information.Text = "";
            this.MarathonRichTextBox_Information.Transparent = false;
            this.MarathonRichTextBox_Information.WordWrapToContentPadding = false;
            // 
            // TabPage_Configuration
            // 
            this.TabPage_Configuration.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.TabPage_Configuration.Controls.Add(this.MarathonRichTextBox_Technical);
            this.TabPage_Configuration.Location = new System.Drawing.Point(4, 25);
            this.TabPage_Configuration.Name = "TabPage_Configuration";
            this.TabPage_Configuration.Size = new System.Drawing.Size(361, 91);
            this.TabPage_Configuration.TabIndex = 2;
            this.TabPage_Configuration.Text = "Configuration";
            // 
            // MarathonRichTextBox_Technical
            // 
            this.MarathonRichTextBox_Technical.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.MarathonRichTextBox_Technical.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MarathonRichTextBox_Technical.ContentPadding = new System.Windows.Forms.Padding(0);
            this.MarathonRichTextBox_Technical.Cursor = System.Windows.Forms.Cursors.Default;
            this.MarathonRichTextBox_Technical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MarathonRichTextBox_Technical.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MarathonRichTextBox_Technical.ForeColor = System.Drawing.SystemColors.Control;
            this.MarathonRichTextBox_Technical.Location = new System.Drawing.Point(0, 0);
            this.MarathonRichTextBox_Technical.LockInput = true;
            this.MarathonRichTextBox_Technical.Name = "MarathonRichTextBox_Technical";
            this.MarathonRichTextBox_Technical.ReadOnly = true;
            this.MarathonRichTextBox_Technical.Size = new System.Drawing.Size(361, 91);
            this.MarathonRichTextBox_Technical.TabIndex = 2;
            this.MarathonRichTextBox_Technical.Text = "";
            this.MarathonRichTextBox_Technical.Transparent = false;
            this.MarathonRichTextBox_Technical.WordWrapToContentPadding = false;
            // 
            // ToolTipDark_Title
            // 
            this.ToolTipDark_Title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.ToolTipDark_Title.ForeColor = System.Drawing.SystemColors.Control;
            this.ToolTipDark_Title.OwnerDraw = true;
            this.ToolTipDark_Title.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // Preview
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.TabControlFlat_Mod);
            this.Controls.Add(this.Panel_Blur);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Name = "Preview";
            this.Size = new System.Drawing.Size(369, 432);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Thumbnail)).EndInit();
            this.Panel_Blur.ResumeLayout(false);
            this.TabControlFlat_Mod.ResumeLayout(false);
            this.TabPage_Description.ResumeLayout(false);
            this.TabPage_Metadata.ResumeLayout(false);
            this.TabPage_Configuration.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBox_Thumbnail;
        private System.Windows.Forms.Label Label_Title;
        private System.Windows.Forms.Label Label_Author;
        private Components.TabControlFlat TabControlFlat_Mod;
        private System.Windows.Forms.TabPage TabPage_Description;
        private System.Windows.Forms.TabPage TabPage_Metadata;
        private Components.MarathonRichTextBox MarathonRichTextBox_Description;
        private Components.MarathonRichTextBox MarathonRichTextBox_Information;
        private System.Windows.Forms.TabPage TabPage_Configuration;
        private Components.MarathonRichTextBox MarathonRichTextBox_Technical;
        private System.Windows.Forms.Panel Panel_Blur;
        private Components.ToolTipDark ToolTipDark_Title;
    }
}
