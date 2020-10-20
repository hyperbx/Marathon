namespace Marathon.Toolkit.Controls
{
    partial class RibbonButtonSmall
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
            this.PictureBox_Image = new System.Windows.Forms.PictureBox();
            this.LabelDark_Text = new Marathon.Toolkit.Components.LabelDark();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Image)).BeginInit();
            this.SuspendLayout();
            // 
            // PictureBox_Image
            // 
            this.PictureBox_Image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PictureBox_Image.Location = new System.Drawing.Point(3, 3);
            this.PictureBox_Image.Name = "PictureBox_Image";
            this.PictureBox_Image.Size = new System.Drawing.Size(16, 16);
            this.PictureBox_Image.TabIndex = 2;
            this.PictureBox_Image.TabStop = false;
            this.PictureBox_Image.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Controls_MouseDown_Group);
            this.PictureBox_Image.MouseEnter += new System.EventHandler(this.Controls_MouseEnter_Group);
            this.PictureBox_Image.MouseLeave += new System.EventHandler(this.Controls_MouseLeave_Group);
            this.PictureBox_Image.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Controls_MouseUp_Group);
            // 
            // LabelDark_Text
            // 
            this.LabelDark_Text.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelDark_Text.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelDark_Text.ForeColor = System.Drawing.Color.White;
            this.LabelDark_Text.Location = new System.Drawing.Point(26, 3);
            this.LabelDark_Text.Name = "LabelDark_Text";
            this.LabelDark_Text.Size = new System.Drawing.Size(78, 19);
            this.LabelDark_Text.TabIndex = 3;
            this.LabelDark_Text.Text = "Placeholder";
            this.LabelDark_Text.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Controls_MouseDown_Group);
            this.LabelDark_Text.MouseEnter += new System.EventHandler(this.Controls_MouseEnter_Group);
            this.LabelDark_Text.MouseLeave += new System.EventHandler(this.Controls_MouseLeave_Group);
            this.LabelDark_Text.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Controls_MouseUp_Group);
            // 
            // RibbonButtonSmall
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.LabelDark_Text);
            this.Controls.Add(this.PictureBox_Image);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "RibbonButtonSmall";
            this.Size = new System.Drawing.Size(104, 22);
            this.EnabledChanged += new System.EventHandler(this.RibbonButton_EnabledChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Controls_MouseDown_Group);
            this.MouseEnter += new System.EventHandler(this.Controls_MouseEnter_Group);
            this.MouseLeave += new System.EventHandler(this.Controls_MouseLeave_Group);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Controls_MouseUp_Group);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Image)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBox_Image;
        private Components.LabelDark LabelDark_Text;
    }
}
