namespace Toolkit
{
    partial class SectionButton
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
            this.SuspendLayout();
            // 
            // SectionButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(45)))));
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SectionButton";
            this.Size = new System.Drawing.Size(281, 35);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SectionButton_MouseDown);
            this.MouseEnter += new System.EventHandler(this.SectionButton_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.SectionButton_MouseLeave);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SectionButton_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
