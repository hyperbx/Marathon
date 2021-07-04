namespace Marathon.Components
{
    partial class MarathonMessageBoxForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarathonMessageBoxForm));
            this.Panel_ButtonBackdrop = new System.Windows.Forms.Panel();
            this.PictureBox_Icon = new System.Windows.Forms.PictureBox();
            this.MarathonRichTextBox_Body = new Marathon.Components.MarathonRichTextBox();
            this.ButtonDark_2 = new Marathon.Components.ButtonDark();
            this.ButtonDark_1 = new Marathon.Components.ButtonDark();
            this.ButtonDark_3 = new Marathon.Components.ButtonDark();
            this.Panel_ButtonBackdrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Icon)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel_ButtonBackdrop
            // 
            this.Panel_ButtonBackdrop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Panel_ButtonBackdrop.Controls.Add(this.ButtonDark_2);
            this.Panel_ButtonBackdrop.Controls.Add(this.ButtonDark_1);
            this.Panel_ButtonBackdrop.Controls.Add(this.ButtonDark_3);
            this.Panel_ButtonBackdrop.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel_ButtonBackdrop.Location = new System.Drawing.Point(0, 77);
            this.Panel_ButtonBackdrop.Name = "Panel_ButtonBackdrop";
            this.Panel_ButtonBackdrop.Size = new System.Drawing.Size(264, 42);
            this.Panel_ButtonBackdrop.TabIndex = 0;
            // 
            // PictureBox_Icon
            // 
            this.PictureBox_Icon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("PictureBox_Icon.BackgroundImage")));
            this.PictureBox_Icon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PictureBox_Icon.Location = new System.Drawing.Point(15, 16);
            this.PictureBox_Icon.Name = "PictureBox_Icon";
            this.PictureBox_Icon.Size = new System.Drawing.Size(45, 45);
            this.PictureBox_Icon.TabIndex = 2;
            this.PictureBox_Icon.TabStop = false;
            // 
            // MarathonRichTextBox_Body
            // 
            this.MarathonRichTextBox_Body.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MarathonRichTextBox_Body.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.MarathonRichTextBox_Body.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MarathonRichTextBox_Body.ContentPadding = new System.Windows.Forms.Padding(1, 32, 4, 0);
            this.MarathonRichTextBox_Body.Cursor = System.Windows.Forms.Cursors.Default;
            this.MarathonRichTextBox_Body.ForeColor = System.Drawing.SystemColors.Control;
            this.MarathonRichTextBox_Body.Location = new System.Drawing.Point(61, 0);
            this.MarathonRichTextBox_Body.LockInput = true;
            this.MarathonRichTextBox_Body.Name = "MarathonRichTextBox_Body";
            this.MarathonRichTextBox_Body.ReadOnly = true;
            this.MarathonRichTextBox_Body.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.MarathonRichTextBox_Body.Size = new System.Drawing.Size(203, 77);
            this.MarathonRichTextBox_Body.TabIndex = 3;
            this.MarathonRichTextBox_Body.Text = "Placeholder";
            this.MarathonRichTextBox_Body.Transparent = false;
            this.MarathonRichTextBox_Body.WordWrapToContentPadding = true;
            this.MarathonRichTextBox_Body.Zoom = false;
            this.MarathonRichTextBox_Body.ContentsResized += new System.Windows.Forms.ContentsResizedEventHandler(this.MarathonRichTextBox_Body_ContentsResized);
            // 
            // ButtonDark_2
            // 
            this.ButtonDark_2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonDark_2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ButtonDark_2.Checked = false;
            this.ButtonDark_2.FlatAppearance.BorderSize = 0;
            this.ButtonDark_2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(94)))), ((int)(((byte)(94)))));
            this.ButtonDark_2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.ButtonDark_2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDark_2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonDark_2.ForeColor = System.Drawing.SystemColors.Control;
            this.ButtonDark_2.FormBackColour = System.Drawing.Color.Empty;
            this.ButtonDark_2.Location = new System.Drawing.Point(94, 10);
            this.ButtonDark_2.Name = "ButtonDark_2";
            this.ButtonDark_2.Size = new System.Drawing.Size(75, 23);
            this.ButtonDark_2.TabIndex = 0;
            this.ButtonDark_2.Text = "Placeholder";
            this.ButtonDark_2.UseVisualStyleBackColor = false;
            this.ButtonDark_2.Visible = false;
            this.ButtonDark_2.Click += new System.EventHandler(this.Button_Click_Group);
            // 
            // ButtonDark_1
            // 
            this.ButtonDark_1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonDark_1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ButtonDark_1.Checked = false;
            this.ButtonDark_1.FlatAppearance.BorderSize = 0;
            this.ButtonDark_1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(94)))), ((int)(((byte)(94)))));
            this.ButtonDark_1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.ButtonDark_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDark_1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonDark_1.ForeColor = System.Drawing.SystemColors.Control;
            this.ButtonDark_1.FormBackColour = System.Drawing.Color.Empty;
            this.ButtonDark_1.Location = new System.Drawing.Point(10, 10);
            this.ButtonDark_1.Name = "ButtonDark_1";
            this.ButtonDark_1.Size = new System.Drawing.Size(75, 23);
            this.ButtonDark_1.TabIndex = 2;
            this.ButtonDark_1.Text = "Placeholder";
            this.ButtonDark_1.UseVisualStyleBackColor = false;
            this.ButtonDark_1.Visible = false;
            this.ButtonDark_1.Click += new System.EventHandler(this.Button_Click_Group);
            // 
            // ButtonDark_3
            // 
            this.ButtonDark_3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonDark_3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ButtonDark_3.Checked = false;
            this.ButtonDark_3.FlatAppearance.BorderSize = 0;
            this.ButtonDark_3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(94)))), ((int)(((byte)(94)))));
            this.ButtonDark_3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(71)))), ((int)(((byte)(71)))));
            this.ButtonDark_3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDark_3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonDark_3.ForeColor = System.Drawing.SystemColors.Control;
            this.ButtonDark_3.FormBackColour = System.Drawing.Color.Empty;
            this.ButtonDark_3.Location = new System.Drawing.Point(178, 10);
            this.ButtonDark_3.Name = "ButtonDark_3";
            this.ButtonDark_3.Size = new System.Drawing.Size(75, 23);
            this.ButtonDark_3.TabIndex = 1;
            this.ButtonDark_3.Text = "Placeholder";
            this.ButtonDark_3.UseVisualStyleBackColor = false;
            this.ButtonDark_3.Visible = false;
            this.ButtonDark_3.Click += new System.EventHandler(this.Button_Click_Group);
            // 
            // MarathonMessageBoxForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(264, 119);
            this.ControlBox = false;
            this.Controls.Add(this.PictureBox_Icon);
            this.Controls.Add(this.MarathonRichTextBox_Body);
            this.Controls.Add(this.Panel_ButtonBackdrop);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MarathonMessageBoxForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Marathon";
            this.Panel_ButtonBackdrop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Icon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_ButtonBackdrop;
        private System.Windows.Forms.PictureBox PictureBox_Icon;
        private ButtonDark ButtonDark_1;
        private ButtonDark ButtonDark_2;
        private ButtonDark ButtonDark_3;
        private MarathonRichTextBox MarathonRichTextBox_Body;
    }
}
