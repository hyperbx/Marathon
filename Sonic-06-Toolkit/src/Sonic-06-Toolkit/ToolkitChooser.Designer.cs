namespace Toolkit.Text
{
    partial class Chooser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Chooser));
            this.lbl_Description = new System.Windows.Forms.Label();
            this.pnl_ButtonBackdrop = new System.Windows.Forms.Panel();
            this.btn_Choice2 = new System.Windows.Forms.Button();
            this.btn_Choice1 = new System.Windows.Forms.Button();
            this.btn_Abort = new System.Windows.Forms.Button();
            this.pic_Icon = new System.Windows.Forms.PictureBox();
            this.rtb_Message = new System.Windows.Forms.RichTextBox();
            this.pnl_ButtonBackdrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Icon)).BeginInit();
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
            // pnl_ButtonBackdrop
            // 
            this.pnl_ButtonBackdrop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_ButtonBackdrop.BackColor = System.Drawing.SystemColors.Control;
            this.pnl_ButtonBackdrop.Controls.Add(this.btn_Choice2);
            this.pnl_ButtonBackdrop.Controls.Add(this.btn_Choice1);
            this.pnl_ButtonBackdrop.Controls.Add(this.btn_Abort);
            this.pnl_ButtonBackdrop.Location = new System.Drawing.Point(-1, 77);
            this.pnl_ButtonBackdrop.Name = "pnl_ButtonBackdrop";
            this.pnl_ButtonBackdrop.Size = new System.Drawing.Size(268, 58);
            this.pnl_ButtonBackdrop.TabIndex = 1;
            // 
            // btn_Choice2
            // 
            this.btn_Choice2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Choice2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Choice2.Location = new System.Drawing.Point(179, 10);
            this.btn_Choice2.Name = "btn_Choice2";
            this.btn_Choice2.Size = new System.Drawing.Size(74, 23);
            this.btn_Choice2.TabIndex = 4;
            this.btn_Choice2.Text = "Choice";
            this.btn_Choice2.UseVisualStyleBackColor = true;
            this.btn_Choice2.Click += new System.EventHandler(this.Btn_OK_Click);
            // 
            // btn_Choice1
            // 
            this.btn_Choice1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Choice1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Choice1.Location = new System.Drawing.Point(95, 10);
            this.btn_Choice1.Name = "btn_Choice1";
            this.btn_Choice1.Size = new System.Drawing.Size(75, 23);
            this.btn_Choice1.TabIndex = 5;
            this.btn_Choice1.Text = "Choice";
            this.btn_Choice1.UseVisualStyleBackColor = true;
            this.btn_Choice1.Click += new System.EventHandler(this.Btn_Yes_Click);
            // 
            // btn_Abort
            // 
            this.btn_Abort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Abort.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Abort.Location = new System.Drawing.Point(11, 10);
            this.btn_Abort.Name = "btn_Abort";
            this.btn_Abort.Size = new System.Drawing.Size(75, 23);
            this.btn_Abort.TabIndex = 6;
            this.btn_Abort.Text = "Abort";
            this.btn_Abort.UseVisualStyleBackColor = true;
            this.btn_Abort.Click += new System.EventHandler(this.Btn_Abort_Click);
            // 
            // pic_Icon
            // 
            this.pic_Icon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pic_Icon.Location = new System.Drawing.Point(16, 16);
            this.pic_Icon.Name = "pic_Icon";
            this.pic_Icon.Size = new System.Drawing.Size(45, 45);
            this.pic_Icon.TabIndex = 2;
            this.pic_Icon.TabStop = false;
            // 
            // rtb_Message
            // 
            this.rtb_Message.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_Message.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.rtb_Message.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb_Message.Cursor = System.Windows.Forms.Cursors.Default;
            this.rtb_Message.DetectUrls = false;
            this.rtb_Message.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb_Message.Location = new System.Drawing.Point(64, 25);
            this.rtb_Message.Name = "rtb_Message";
            this.rtb_Message.ReadOnly = true;
            this.rtb_Message.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.rtb_Message.Size = new System.Drawing.Size(185, 13);
            this.rtb_Message.TabIndex = 3;
            this.rtb_Message.Text = "None";
            this.rtb_Message.ContentsResized += new System.Windows.Forms.ContentsResizedEventHandler(this.Rtb_Message_ContentsResized);
            // 
            // Chooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(264, 119);
            this.ControlBox = false;
            this.Controls.Add(this.pic_Icon);
            this.Controls.Add(this.pnl_ButtonBackdrop);
            this.Controls.Add(this.rtb_Message);
            this.Controls.Add(this.lbl_Description);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Chooser";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Unify Messenger";
            this.Load += new System.EventHandler(this.UnifyMessages_Load);
            this.pnl_ButtonBackdrop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Icon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Description;
        private System.Windows.Forms.Panel pnl_ButtonBackdrop;
        private System.Windows.Forms.PictureBox pic_Icon;
        private System.Windows.Forms.RichTextBox rtb_Message;
        private System.Windows.Forms.Button btn_Choice2;
        private System.Windows.Forms.Button btn_Choice1;
        private System.Windows.Forms.Button btn_Abort;
    }
}