namespace Marathon.Toolkit.Controls
{
    partial class RibbonButton
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
            this.PictureBox_Image.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PictureBox_Image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.PictureBox_Image.Location = new System.Drawing.Point(23, 6);
            this.PictureBox_Image.Name = "PictureBox_Image";
            this.PictureBox_Image.Size = new System.Drawing.Size(32, 32);
            this.PictureBox_Image.TabIndex = 1;
            this.PictureBox_Image.TabStop = false;
            this.PictureBox_Image.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Controls_MouseDown_Group);
            this.PictureBox_Image.MouseEnter += new System.EventHandler(this.Controls_MouseEnter_Group);
            this.PictureBox_Image.MouseLeave += new System.EventHandler(this.Controls_MouseLeave_Group);
            this.PictureBox_Image.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Controls_MouseUp_Group);
            // 
            // LabelDark_Text
            // 
            this.LabelDark_Text.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LabelDark_Text.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelDark_Text.ForeColor = System.Drawing.Color.White;
            this.LabelDark_Text.Location = new System.Drawing.Point(0, 40);
            this.LabelDark_Text.Name = "LabelDark_Text";
            this.LabelDark_Text.Size = new System.Drawing.Size(78, 34);
            this.LabelDark_Text.TabIndex = 0;
            this.LabelDark_Text.Text = "Placeholder";
            this.LabelDark_Text.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.LabelDark_Text.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Controls_MouseDown_Group);
            this.LabelDark_Text.MouseEnter += new System.EventHandler(this.Controls_MouseEnter_Group);
            this.LabelDark_Text.MouseLeave += new System.EventHandler(this.Controls_MouseLeave_Group);
            this.LabelDark_Text.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Controls_MouseUp_Group);
            // 
            // RibbonButton
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.Controls.Add(this.PictureBox_Image);
            this.Controls.Add(this.LabelDark_Text);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "RibbonButton";
            this.Size = new System.Drawing.Size(78, 74);
            this.EnabledChanged += new System.EventHandler(this.RibbonButton_EnabledChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Controls_MouseDown_Group);
            this.MouseEnter += new System.EventHandler(this.Controls_MouseEnter_Group);
            this.MouseLeave += new System.EventHandler(this.Controls_MouseLeave_Group);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Controls_MouseUp_Group);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Image)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Marathon.Toolkit.Components.LabelDark LabelDark_Text;
        private System.Windows.Forms.PictureBox PictureBox_Image;
    }
}
