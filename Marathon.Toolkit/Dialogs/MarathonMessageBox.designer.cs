namespace Marathon.Toolkit
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
            this.lbl_Description = new System.Windows.Forms.Label();
            this.Panel_ButtonBackdrop = new System.Windows.Forms.Panel();
            this.Button_Abort = new System.Windows.Forms.Button();
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Yes = new System.Windows.Forms.Button();
            this.btn_No = new System.Windows.Forms.Button();
            this.PictureBox_Icon = new System.Windows.Forms.PictureBox();
            this.RichTextBox_Message = new System.Windows.Forms.RichTextBox();
            this.Panel_ButtonBackdrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Icon)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Description
            // 
            this.lbl_Description.AutoSize = true;
            this.lbl_Description.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Description.Location = new System.Drawing.Point(62, 25);
            this.lbl_Description.Name = "lbl_Description";
            this.lbl_Description.Size = new System.Drawing.Size(35, 13);
            this.lbl_Description.TabIndex = 0;
            this.lbl_Description.Text = "None";
            this.lbl_Description.Visible = false;
            // 
            // Panel_ButtonBackdrop
            // 
            this.Panel_ButtonBackdrop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_ButtonBackdrop.BackColor = System.Drawing.SystemColors.Control;
            this.Panel_ButtonBackdrop.Controls.Add(this.Button_Abort);
            this.Panel_ButtonBackdrop.Controls.Add(this.Button_OK);
            this.Panel_ButtonBackdrop.Controls.Add(this.Button_Yes);
            this.Panel_ButtonBackdrop.Controls.Add(this.btn_No);
            this.Panel_ButtonBackdrop.Location = new System.Drawing.Point(-1, 77);
            this.Panel_ButtonBackdrop.Name = "Panel_ButtonBackdrop";
            this.Panel_ButtonBackdrop.Size = new System.Drawing.Size(268, 58);
            this.Panel_ButtonBackdrop.TabIndex = 1;
            // 
            // Button_Abort
            // 
            this.Button_Abort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Abort.BackColor = System.Drawing.Color.Tomato;
            this.Button_Abort.FlatAppearance.BorderSize = 0;
            this.Button_Abort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Abort.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Abort.Location = new System.Drawing.Point(11, 10);
            this.Button_Abort.Name = "Button_Abort";
            this.Button_Abort.Size = new System.Drawing.Size(75, 23);
            this.Button_Abort.TabIndex = 2;
            this.Button_Abort.Text = "Abort";
            this.Button_Abort.UseVisualStyleBackColor = false;
            this.Button_Abort.Visible = false;
            this.Button_Abort.Click += new System.EventHandler(this.Button_Abort_Click);
            // 
            // Button_OK
            // 
            this.Button_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_OK.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Button_OK.FlatAppearance.BorderSize = 0;
            this.Button_OK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_OK.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_OK.Location = new System.Drawing.Point(179, 10);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 0;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = false;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Button_Yes
            // 
            this.Button_Yes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Yes.BackColor = System.Drawing.Color.LightGreen;
            this.Button_Yes.FlatAppearance.BorderSize = 0;
            this.Button_Yes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Yes.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Button_Yes.Location = new System.Drawing.Point(95, 10);
            this.Button_Yes.Name = "Button_Yes";
            this.Button_Yes.Size = new System.Drawing.Size(75, 23);
            this.Button_Yes.TabIndex = 1;
            this.Button_Yes.Text = "Yes";
            this.Button_Yes.UseVisualStyleBackColor = false;
            this.Button_Yes.Visible = false;
            this.Button_Yes.Click += new System.EventHandler(this.Button_Yes_Click);
            // 
            // btn_No
            // 
            this.btn_No.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_No.BackColor = System.Drawing.Color.Tomato;
            this.btn_No.FlatAppearance.BorderSize = 0;
            this.btn_No.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_No.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_No.Location = new System.Drawing.Point(95, 10);
            this.btn_No.Name = "btn_No";
            this.btn_No.Size = new System.Drawing.Size(75, 23);
            this.btn_No.TabIndex = 3;
            this.btn_No.Text = "No";
            this.btn_No.UseVisualStyleBackColor = false;
            this.btn_No.Visible = false;
            this.btn_No.Click += new System.EventHandler(this.Button_No_Click);
            // 
            // PictureBox_Icon
            // 
            this.PictureBox_Icon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.PictureBox_Icon.Location = new System.Drawing.Point(16, 16);
            this.PictureBox_Icon.Name = "PictureBox_Icon";
            this.PictureBox_Icon.Size = new System.Drawing.Size(45, 45);
            this.PictureBox_Icon.TabIndex = 2;
            this.PictureBox_Icon.TabStop = false;
            // 
            // RichTextBox_Message
            // 
            this.RichTextBox_Message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RichTextBox_Message.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.RichTextBox_Message.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichTextBox_Message.Cursor = System.Windows.Forms.Cursors.Default;
            this.RichTextBox_Message.DetectUrls = false;
            this.RichTextBox_Message.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RichTextBox_Message.Location = new System.Drawing.Point(64, 25);
            this.RichTextBox_Message.Name = "RichTextBox_Message";
            this.RichTextBox_Message.ReadOnly = true;
            this.RichTextBox_Message.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.RichTextBox_Message.Size = new System.Drawing.Size(185, 13);
            this.RichTextBox_Message.TabIndex = 3;
            this.RichTextBox_Message.Text = "None";
            this.RichTextBox_Message.ContentsResized += new System.Windows.Forms.ContentsResizedEventHandler(this.RichTextBox_Message_ContentsResized);
            // 
            // MarathonMessageBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(264, 119);
            this.ControlBox = false;
            this.Controls.Add(this.PictureBox_Icon);
            this.Controls.Add(this.Panel_ButtonBackdrop);
            this.Controls.Add(this.RichTextBox_Message);
            this.Controls.Add(this.lbl_Description);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MarathonMessageBoxForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Marathon";
            this.Load += new System.EventHandler(this.MarathonMessageBoxForm_Load);
            this.Panel_ButtonBackdrop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Description;
        private System.Windows.Forms.Panel Panel_ButtonBackdrop;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.PictureBox PictureBox_Icon;
        private System.Windows.Forms.Button Button_Yes;
        private System.Windows.Forms.Button Button_Abort;
        private System.Windows.Forms.Button btn_No;
        private System.Windows.Forms.RichTextBox RichTextBox_Message;
    }
}