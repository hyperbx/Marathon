namespace Toolkit
{
    partial class ToolkitAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolkitAbout));
            this.lbl_Title = new System.Windows.Forms.Label();
            this.lbl_VersionNumber = new System.Windows.Forms.Label();
            this.lbl_Credits = new System.Windows.Forms.Label();
            this.pic_Logo = new System.Windows.Forms.PictureBox();
            this.link_Hyper = new System.Windows.Forms.LinkLabel();
            this.link_GerbilSoft = new System.Windows.Forms.LinkLabel();
            this.link_Sable = new System.Windows.Forms.LinkLabel();
            this.link_ShadowLAG = new System.Windows.Forms.LinkLabel();
            this.link_Radfordhound = new System.Windows.Forms.LinkLabel();
            this.link_Skyth = new System.Windows.Forms.LinkLabel();
            this.link_DarioSamo = new System.Windows.Forms.LinkLabel();
            this.link_xorloser = new System.Windows.Forms.LinkLabel();
            this.link_SEGACarnival = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.Location = new System.Drawing.Point(238, 9);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(312, 50);
            this.lbl_Title.TabIndex = 30;
            this.lbl_Title.Text = "Sonic \'06 Toolkit";
            // 
            // lbl_VersionNumber
            // 
            this.lbl_VersionNumber.AutoSize = true;
            this.lbl_VersionNumber.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_VersionNumber.Location = new System.Drawing.Point(243, 55);
            this.lbl_VersionNumber.Name = "lbl_VersionNumber";
            this.lbl_VersionNumber.Size = new System.Drawing.Size(107, 25);
            this.lbl_VersionNumber.TabIndex = 31;
            this.lbl_VersionNumber.Text = "Version 3.0";
            // 
            // lbl_Credits
            // 
            this.lbl_Credits.AutoSize = true;
            this.lbl_Credits.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Credits.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lbl_Credits.Location = new System.Drawing.Point(246, 91);
            this.lbl_Credits.Name = "lbl_Credits";
            this.lbl_Credits.Size = new System.Drawing.Size(381, 187);
            this.lbl_Credits.TabIndex = 33;
            this.lbl_Credits.Text = resources.GetString("lbl_Credits.Text");
            // 
            // pic_Logo
            // 
            this.pic_Logo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pic_Logo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pic_Logo.BackgroundImage = global::Toolkit.Properties.Resources.logo_main;
            this.pic_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic_Logo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_Logo.Location = new System.Drawing.Point(-1, -1);
            this.pic_Logo.Name = "pic_Logo";
            this.pic_Logo.Size = new System.Drawing.Size(230, 297);
            this.pic_Logo.TabIndex = 34;
            this.pic_Logo.TabStop = false;
            // 
            // link_Hyper
            // 
            this.link_Hyper.AutoSize = true;
            this.link_Hyper.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.link_Hyper.LinkColor = System.Drawing.SystemColors.ControlText;
            this.link_Hyper.Location = new System.Drawing.Point(246, 108);
            this.link_Hyper.Name = "link_Hyper";
            this.link_Hyper.Size = new System.Drawing.Size(43, 17);
            this.link_Hyper.TabIndex = 35;
            this.link_Hyper.TabStop = true;
            this.link_Hyper.Text = "Hyper";
            this.link_Hyper.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_Hyper_LinkClicked);
            // 
            // link_GerbilSoft
            // 
            this.link_GerbilSoft.AutoSize = true;
            this.link_GerbilSoft.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.link_GerbilSoft.LinkColor = System.Drawing.SystemColors.ControlText;
            this.link_GerbilSoft.Location = new System.Drawing.Point(246, 125);
            this.link_GerbilSoft.Name = "link_GerbilSoft";
            this.link_GerbilSoft.Size = new System.Drawing.Size(66, 17);
            this.link_GerbilSoft.TabIndex = 36;
            this.link_GerbilSoft.TabStop = true;
            this.link_GerbilSoft.Text = "GerbilSoft";
            this.link_GerbilSoft.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_GerbilSoft_LinkClicked);
            // 
            // link_Sable
            // 
            this.link_Sable.AutoSize = true;
            this.link_Sable.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.link_Sable.LinkColor = System.Drawing.SystemColors.ControlText;
            this.link_Sable.Location = new System.Drawing.Point(246, 142);
            this.link_Sable.Name = "link_Sable";
            this.link_Sable.Size = new System.Drawing.Size(40, 17);
            this.link_Sable.TabIndex = 37;
            this.link_Sable.TabStop = true;
            this.link_Sable.Text = "Sable";
            this.link_Sable.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_Sable_LinkClicked);
            // 
            // link_ShadowLAG
            // 
            this.link_ShadowLAG.AutoSize = true;
            this.link_ShadowLAG.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.link_ShadowLAG.LinkColor = System.Drawing.SystemColors.ControlText;
            this.link_ShadowLAG.Location = new System.Drawing.Point(246, 176);
            this.link_ShadowLAG.Name = "link_ShadowLAG";
            this.link_ShadowLAG.Size = new System.Drawing.Size(81, 17);
            this.link_ShadowLAG.TabIndex = 38;
            this.link_ShadowLAG.TabStop = true;
            this.link_ShadowLAG.Text = "Shadow LAG";
            this.link_ShadowLAG.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_ShadowLAG_LinkClicked);
            // 
            // link_Radfordhound
            // 
            this.link_Radfordhound.AutoSize = true;
            this.link_Radfordhound.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.link_Radfordhound.LinkColor = System.Drawing.SystemColors.ControlText;
            this.link_Radfordhound.Location = new System.Drawing.Point(246, 159);
            this.link_Radfordhound.Name = "link_Radfordhound";
            this.link_Radfordhound.Size = new System.Drawing.Size(93, 17);
            this.link_Radfordhound.TabIndex = 39;
            this.link_Radfordhound.TabStop = true;
            this.link_Radfordhound.Text = "Radfordhound";
            this.link_Radfordhound.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_Radfordhound_LinkClicked);
            // 
            // link_Skyth
            // 
            this.link_Skyth.AutoSize = true;
            this.link_Skyth.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.link_Skyth.LinkColor = System.Drawing.SystemColors.ControlText;
            this.link_Skyth.Location = new System.Drawing.Point(246, 193);
            this.link_Skyth.Name = "link_Skyth";
            this.link_Skyth.Size = new System.Drawing.Size(38, 17);
            this.link_Skyth.TabIndex = 40;
            this.link_Skyth.TabStop = true;
            this.link_Skyth.Text = "Skyth";
            this.link_Skyth.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_Skyth_LinkClicked);
            // 
            // link_DarioSamo
            // 
            this.link_DarioSamo.AutoSize = true;
            this.link_DarioSamo.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.link_DarioSamo.LinkColor = System.Drawing.SystemColors.ControlText;
            this.link_DarioSamo.Location = new System.Drawing.Point(246, 210);
            this.link_DarioSamo.Name = "link_DarioSamo";
            this.link_DarioSamo.Size = new System.Drawing.Size(73, 17);
            this.link_DarioSamo.TabIndex = 41;
            this.link_DarioSamo.TabStop = true;
            this.link_DarioSamo.Text = "DarioSamo";
            this.link_DarioSamo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_DarioSamo_LinkClicked);
            // 
            // link_xorloser
            // 
            this.link_xorloser.AutoSize = true;
            this.link_xorloser.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.link_xorloser.LinkColor = System.Drawing.SystemColors.ControlText;
            this.link_xorloser.Location = new System.Drawing.Point(246, 227);
            this.link_xorloser.Name = "link_xorloser";
            this.link_xorloser.Size = new System.Drawing.Size(56, 17);
            this.link_xorloser.TabIndex = 42;
            this.link_xorloser.TabStop = true;
            this.link_xorloser.Text = "xorloser";
            this.link_xorloser.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_xorloser_LinkClicked);
            // 
            // link_SEGACarnival
            // 
            this.link_SEGACarnival.AutoSize = true;
            this.link_SEGACarnival.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.link_SEGACarnival.LinkColor = System.Drawing.SystemColors.ControlText;
            this.link_SEGACarnival.Location = new System.Drawing.Point(310, 261);
            this.link_SEGACarnival.Name = "link_SEGACarnival";
            this.link_SEGACarnival.Size = new System.Drawing.Size(89, 17);
            this.link_SEGACarnival.TabIndex = 43;
            this.link_SEGACarnival.TabStop = true;
            this.link_SEGACarnival.Text = "SEGA Carnival";
            this.link_SEGACarnival.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_SEGACarnival_LinkClicked);
            // 
            // ToolkitAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(646, 294);
            this.Controls.Add(this.link_SEGACarnival);
            this.Controls.Add(this.link_xorloser);
            this.Controls.Add(this.link_DarioSamo);
            this.Controls.Add(this.link_Skyth);
            this.Controls.Add(this.link_Radfordhound);
            this.Controls.Add(this.link_ShadowLAG);
            this.Controls.Add(this.link_Sable);
            this.Controls.Add(this.link_GerbilSoft);
            this.Controls.Add(this.link_Hyper);
            this.Controls.Add(this.pic_Logo);
            this.Controls.Add(this.lbl_VersionNumber);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.lbl_Credits);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ToolkitAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sonic \'06 Toolkit";
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Label lbl_VersionNumber;
        private System.Windows.Forms.Label lbl_Credits;
        private System.Windows.Forms.PictureBox pic_Logo;
        private System.Windows.Forms.LinkLabel link_Hyper;
        private System.Windows.Forms.LinkLabel link_GerbilSoft;
        private System.Windows.Forms.LinkLabel link_Sable;
        private System.Windows.Forms.LinkLabel link_ShadowLAG;
        private System.Windows.Forms.LinkLabel link_Radfordhound;
        private System.Windows.Forms.LinkLabel link_Skyth;
        private System.Windows.Forms.LinkLabel link_DarioSamo;
        private System.Windows.Forms.LinkLabel link_xorloser;
        private System.Windows.Forms.LinkLabel link_SEGACarnival;
    }
}