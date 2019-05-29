namespace Sonic_06_Toolkit
{
    partial class Loop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Loop));
            this.lbl_Title = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pic_Logo = new System.Windows.Forms.PictureBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Confirm = new System.Windows.Forms.Button();
            this.group_Start = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.track_Start = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.track_End = new System.Windows.Forms.TrackBar();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).BeginInit();
            this.group_Start.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track_Start)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track_End)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Title.Location = new System.Drawing.Point(12, 10);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(102, 47);
            this.lbl_Title.TabIndex = 1;
            this.lbl_Title.Text = "Loop";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pic_Logo);
            this.panel1.Controls.Add(this.lbl_Title);
            this.panel1.Location = new System.Drawing.Point(-1, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(468, 69);
            this.panel1.TabIndex = 8;
            // 
            // pic_Logo
            // 
            this.pic_Logo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_Logo.BackColor = System.Drawing.Color.Transparent;
            this.pic_Logo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_Logo.BackgroundImage")));
            this.pic_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic_Logo.Location = new System.Drawing.Point(391, 1);
            this.pic_Logo.Name = "pic_Logo";
            this.pic_Logo.Size = new System.Drawing.Size(67, 67);
            this.pic_Logo.TabIndex = 11;
            this.pic_Logo.TabStop = false;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.Tomato;
            this.btn_Cancel.FlatAppearance.BorderSize = 0;
            this.btn_Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cancel.Location = new System.Drawing.Point(380, 273);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 27;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.Btn_Cancel_Click);
            // 
            // btn_Confirm
            // 
            this.btn_Confirm.BackColor = System.Drawing.Color.LightGreen;
            this.btn_Confirm.FlatAppearance.BorderSize = 0;
            this.btn_Confirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Confirm.Location = new System.Drawing.Point(299, 273);
            this.btn_Confirm.Name = "btn_Confirm";
            this.btn_Confirm.Size = new System.Drawing.Size(75, 23);
            this.btn_Confirm.TabIndex = 26;
            this.btn_Confirm.Text = "Confirm";
            this.btn_Confirm.UseVisualStyleBackColor = false;
            this.btn_Confirm.Click += new System.EventHandler(this.Btn_Confirm_Click);
            // 
            // group_Start
            // 
            this.group_Start.Controls.Add(this.label2);
            this.group_Start.Controls.Add(this.label1);
            this.group_Start.Controls.Add(this.track_Start);
            this.group_Start.Location = new System.Drawing.Point(13, 77);
            this.group_Start.Name = "group_Start";
            this.group_Start.Size = new System.Drawing.Size(442, 89);
            this.group_Start.TabIndex = 34;
            this.group_Start.TabStop = false;
            this.group_Start.Text = "Start";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(395, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "48000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(13, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "0";
            // 
            // track_Start
            // 
            this.track_Start.LargeChange = 1000;
            this.track_Start.Location = new System.Drawing.Point(4, 23);
            this.track_Start.Maximum = 48000;
            this.track_Start.Name = "track_Start";
            this.track_Start.Size = new System.Drawing.Size(434, 45);
            this.track_Start.SmallChange = 100;
            this.track_Start.TabIndex = 31;
            this.track_Start.TickFrequency = 1000;
            this.track_Start.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.track_End);
            this.groupBox1.Location = new System.Drawing.Point(13, 176);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 89);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "End";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(395, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "48000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(13, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "0";
            // 
            // track_End
            // 
            this.track_End.LargeChange = 1000;
            this.track_End.Location = new System.Drawing.Point(4, 23);
            this.track_End.Maximum = 48000;
            this.track_End.Name = "track_End";
            this.track_End.Size = new System.Drawing.Size(434, 45);
            this.track_End.SmallChange = 100;
            this.track_End.TabIndex = 31;
            this.track_End.TickFrequency = 1000;
            this.track_End.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // Loop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 307);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.group_Start);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Confirm);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Loop";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Loop";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).EndInit();
            this.group_Start.ResumeLayout(false);
            this.group_Start.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track_Start)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.track_End)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.PictureBox pic_Logo;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Confirm;
        private System.Windows.Forms.GroupBox group_Start;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar track_Start;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar track_End;
    }
}