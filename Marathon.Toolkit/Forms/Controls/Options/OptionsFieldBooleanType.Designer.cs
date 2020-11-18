namespace Marathon.Toolkit.Controls
{
    partial class OptionsFieldBooleanType
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
            this.CheckBox_Boolean = new System.Windows.Forms.CheckBox();
            this.Label_Description = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CheckBox_Boolean
            // 
            this.CheckBox_Boolean.AutoSize = true;
            this.CheckBox_Boolean.Location = new System.Drawing.Point(15, 16);
            this.CheckBox_Boolean.Name = "CheckBox_Boolean";
            this.CheckBox_Boolean.Size = new System.Drawing.Size(130, 19);
            this.CheckBox_Boolean.TabIndex = 0;
            this.CheckBox_Boolean.Text = "Nothing to see here";
            this.CheckBox_Boolean.UseVisualStyleBackColor = true;
            this.CheckBox_Boolean.CheckedChanged += new System.EventHandler(this.CheckBox_Boolean_CheckedChanged);
            // 
            // Label_Description
            // 
            this.Label_Description.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Description.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Description.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Label_Description.Location = new System.Drawing.Point(151, 17);
            this.Label_Description.Name = "Label_Description";
            this.Label_Description.Size = new System.Drawing.Size(257, 15);
            this.Label_Description.TabIndex = 3;
            this.Label_Description.Text = "Nothing to see here";
            this.Label_Description.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // OptionsFieldBooleanType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.Controls.Add(this.CheckBox_Boolean);
            this.Controls.Add(this.Label_Description);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Name = "OptionsFieldBooleanType";
            this.Size = new System.Drawing.Size(408, 51);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CheckBox_Boolean;
        private System.Windows.Forms.Label Label_Description;
    }
}
