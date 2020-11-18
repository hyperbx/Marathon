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
            this.Label_Description = new System.Windows.Forms.Label();
            this.ButtonFlat_Browse = new Marathon.Toolkit.Components.ButtonFlat();
            this.TextBoxDark_String = new Marathon.Toolkit.Components.TextBoxDark();
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
            // ButtonFlat_Browse
            // 
            this.ButtonFlat_Browse.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ButtonFlat_Browse.FlatAppearance.BorderSize = 0;
            this.ButtonFlat_Browse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonFlat_Browse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ButtonFlat_Browse.Location = new System.Drawing.Point(376, 22);
            this.ButtonFlat_Browse.Name = "ButtonFlat_Browse";
            this.ButtonFlat_Browse.Size = new System.Drawing.Size(25, 23);
            this.ButtonFlat_Browse.TabIndex = 3;
            this.ButtonFlat_Browse.Text = "...";
            this.ButtonFlat_Browse.UseVisualStyleBackColor = false;
            this.ButtonFlat_Browse.Visible = false;
            this.ButtonFlat_Browse.Click += new System.EventHandler(this.ButtonFlat_Browse_Click);
            // 
            // TextBoxDark_String
            // 
            this.TextBoxDark_String.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.TextBoxDark_String.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TextBoxDark_String.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxDark_String.ForeColor = System.Drawing.SystemColors.Control;
            this.TextBoxDark_String.Location = new System.Drawing.Point(7, 22);
            this.TextBoxDark_String.Name = "TextBoxDark_String";
            this.TextBoxDark_String.Size = new System.Drawing.Size(363, 23);
            this.TextBoxDark_String.TabIndex = 4;
            this.TextBoxDark_String.TextChanged += new System.EventHandler(this.TextBoxDark_String_TextChanged);
            // 
            // OptionsFieldStringType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.Controls.Add(this.ButtonFlat_Browse);
            this.Controls.Add(this.Label_Title);
            this.Controls.Add(this.Label_Description);
            this.Controls.Add(this.TextBoxDark_String);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Name = "OptionsFieldStringType";
            this.Size = new System.Drawing.Size(408, 51);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label_Title;
        private System.Windows.Forms.Label Label_Description;
        private Components.ButtonFlat ButtonFlat_Browse;
        private Components.TextBoxDark TextBoxDark_String;
    }
}
