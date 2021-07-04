namespace Marathon.Components
{
    partial class TalkWindow
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
            this.MarathonRichTextBox_Body = new Marathon.Components.MarathonRichTextBox();
            this.SuspendLayout();
            // 
            // MarathonRichTextBox_Body
            // 
            this.MarathonRichTextBox_Body.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MarathonRichTextBox_Body.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MarathonRichTextBox_Body.Font = new System.Drawing.Font("MS Reference Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MarathonRichTextBox_Body.ForeColor = System.Drawing.Color.White;
            this.MarathonRichTextBox_Body.Location = new System.Drawing.Point(63, 19);
            this.MarathonRichTextBox_Body.LockInput = false;
            this.MarathonRichTextBox_Body.Name = "MarathonRichTextBox_Body";
            this.MarathonRichTextBox_Body.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.MarathonRichTextBox_Body.Size = new System.Drawing.Size(794, 113);
            this.MarathonRichTextBox_Body.TabIndex = 1;
            this.MarathonRichTextBox_Body.Text = "Placeholder";
            this.MarathonRichTextBox_Body.Transparent = true;
            this.MarathonRichTextBox_Body.WordWrapToContentPadding = false;
            // 
            // TalkWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.BackgroundImage = global::Marathon.Components.Properties.Resources.TextEditor_TalkWindow;
            this.Controls.Add(this.MarathonRichTextBox_Body);
            this.DoubleBuffered = true;
            this.Name = "TalkWindow";
            this.Size = new System.Drawing.Size(877, 158);
            this.ResumeLayout(false);

        }

        #endregion
        private Components.MarathonRichTextBox MarathonRichTextBox_Body;
    }
}
