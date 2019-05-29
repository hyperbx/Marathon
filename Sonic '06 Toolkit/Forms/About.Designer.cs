namespace Sonic_06_Toolkit
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.lbl_Title = new System.Windows.Forms.Label();
            this.lbl_Subtitle = new System.Windows.Forms.Label();
            this.lbl_versionNumber = new System.Windows.Forms.Label();
            this.lbl_Description = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.btn_GitHub = new System.Windows.Forms.Button();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.rtb_License = new System.Windows.Forms.RichTextBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.Location = new System.Drawing.Point(12, 8);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(293, 47);
            this.lbl_Title.TabIndex = 0;
            this.lbl_Title.Text = "Sonic \'06 Toolkit";
            this.lbl_Title.Click += new System.EventHandler(this.Lbl_Title_Click);
            // 
            // lbl_Subtitle
            // 
            this.lbl_Subtitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Subtitle.AutoSize = true;
            this.lbl_Subtitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Subtitle.Location = new System.Drawing.Point(358, 9);
            this.lbl_Subtitle.Name = "lbl_Subtitle";
            this.lbl_Subtitle.Size = new System.Drawing.Size(129, 32);
            this.lbl_Subtitle.TabIndex = 1;
            this.lbl_Subtitle.Text = "C# Version";
            // 
            // lbl_versionNumber
            // 
            this.lbl_versionNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_versionNumber.AutoSize = true;
            this.lbl_versionNumber.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_versionNumber.Location = new System.Drawing.Point(444, 38);
            this.lbl_versionNumber.Name = "lbl_versionNumber";
            this.lbl_versionNumber.Size = new System.Drawing.Size(40, 21);
            this.lbl_versionNumber.TabIndex = 2;
            this.lbl_versionNumber.Text = "0.00";
            this.lbl_versionNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_Description
            // 
            this.lbl_Description.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_Description.AutoSize = true;
            this.lbl_Description.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Description.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Description.Location = new System.Drawing.Point(53, 33);
            this.lbl_Description.Name = "lbl_Description";
            this.lbl_Description.Size = new System.Drawing.Size(361, 105);
            this.lbl_Description.TabIndex = 3;
            this.lbl_Description.Text = "Sonic \'06 Toolkit is an application to help make\r\nmodding easier for SONIC THE HE" +
    "DGEHOG (2006)\r\n\r\nThis project is open-source for anyone to step in\r\nand contribu" +
    "te to from GitHub.";
            this.lbl_Description.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Location = new System.Drawing.Point(3, 65);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(488, 259);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tabControl2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(480, 233);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Information";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(3, 3);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(474, 227);
            this.tabControl2.TabIndex = 4;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.btn_GitHub);
            this.tabPage4.Controls.Add(this.lbl_Description);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(466, 201);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "About";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // btn_GitHub
            // 
            this.btn_GitHub.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_GitHub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(218)))), ((int)(((byte)(240)))));
            this.btn_GitHub.FlatAppearance.BorderSize = 0;
            this.btn_GitHub.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_GitHub.Location = new System.Drawing.Point(6, 172);
            this.btn_GitHub.Name = "btn_GitHub";
            this.btn_GitHub.Size = new System.Drawing.Size(454, 23);
            this.btn_GitHub.TabIndex = 5;
            this.btn_GitHub.Text = "GitHub";
            this.btn_GitHub.UseVisualStyleBackColor = false;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.richTextBox1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(466, 201);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "History";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(460, 195);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.rtb_License);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(480, 233);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "License";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // rtb_License
            // 
            this.rtb_License.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rtb_License.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_License.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_License.Location = new System.Drawing.Point(0, 0);
            this.rtb_License.Name = "rtb_License";
            this.rtb_License.ReadOnly = true;
            this.rtb_License.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtb_License.Size = new System.Drawing.Size(480, 233);
            this.rtb_License.TabIndex = 0;
            this.rtb_License.Text = resources.GetString("rtb_License.Text");
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.richTextBox2);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(480, 233);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "Credits";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox2.Location = new System.Drawing.Point(0, 0);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox2.Size = new System.Drawing.Size(480, 233);
            this.richTextBox2.TabIndex = 6;
            this.richTextBox2.Text = resources.GetString("richTextBox2.Text");
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(494, 326);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lbl_versionNumber);
            this.Controls.Add(this.lbl_Subtitle);
            this.Controls.Add(this.lbl_Title);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(510, 365);
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Load += new System.EventHandler(this.About_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Label lbl_Subtitle;
        private System.Windows.Forms.Label lbl_versionNumber;
        private System.Windows.Forms.Label lbl_Description;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox rtb_License;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button btn_GitHub;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.RichTextBox richTextBox2;
    }
}