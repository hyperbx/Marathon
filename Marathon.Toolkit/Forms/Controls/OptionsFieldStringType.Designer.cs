namespace Marathon.Toolkit.Controls
{
    partial class OptionsFieldStringType
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
            this.Label_Title = new System.Windows.Forms.Label();
            this.TextBox_String = new System.Windows.Forms.TextBox();
            this.Label_Description = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Label_Title
            // 
            this.Label_Title.AutoSize = true;
            this.Label_Title.Location = new System.Drawing.Point(4, 3);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Size = new System.Drawing.Size(111, 15);
            this.Label_Title.TabIndex = 0;
            this.Label_Title.Text = "Nothing to see here";
            // 
            // TextBox_String
            // 
            this.TextBox_String.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBox_String.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.TextBox_String.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBox_String.ForeColor = System.Drawing.SystemColors.Control;
            this.TextBox_String.Location = new System.Drawing.Point(7, 22);
            this.TextBox_String.Name = "TextBox_String";
            this.TextBox_String.Size = new System.Drawing.Size(394, 23);
            this.TextBox_String.TabIndex = 1;
            this.TextBox_String.TextChanged += new System.EventHandler(this.TextBox_String_TextChanged);
            // 
            // Label_Description
            // 
            this.Label_Description.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Label_Description.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label_Description.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.Label_Description.Location = new System.Drawing.Point(121, 3);
            this.Label_Description.Name = "Label_Description";
            this.Label_Description.Size = new System.Drawing.Size(280, 15);
            this.Label_Description.TabIndex = 2;
            this.Label_Description.Text = "Nothing to see here";
            this.Label_Description.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // OptionsFieldStringType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.Controls.Add(this.Label_Description);
            this.Controls.Add(this.TextBox_String);
            this.Controls.Add(this.Label_Title);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Name = "OptionsFieldStringType";
            this.Size = new System.Drawing.Size(408, 51);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_Title;
        private System.Windows.Forms.TextBox TextBox_String;
        private System.Windows.Forms.Label Label_Description;
    }
}
