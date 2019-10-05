namespace Sonic_06_Toolkit
{
    partial class Save_Editor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Save_Editor));
            this.group_Search = new System.Windows.Forms.GroupBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lbl_ModsDirectory = new System.Windows.Forms.Label();
            this.lbl_GameDirectory = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.btn_Save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.group_Search.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // group_Search
            // 
            this.group_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.group_Search.Controls.Add(this.label2);
            this.group_Search.Controls.Add(this.label1);
            this.group_Search.Controls.Add(this.numericUpDown2);
            this.group_Search.Controls.Add(this.comboBox2);
            this.group_Search.Controls.Add(this.numericUpDown1);
            this.group_Search.Controls.Add(this.comboBox1);
            this.group_Search.Controls.Add(this.lbl_ModsDirectory);
            this.group_Search.Controls.Add(this.lbl_GameDirectory);
            this.group_Search.Location = new System.Drawing.Point(5, 3);
            this.group_Search.Name = "group_Search";
            this.group_Search.Size = new System.Drawing.Size(270, 129);
            this.group_Search.TabIndex = 54;
            this.group_Search.TabStop = false;
            this.group_Search.Text = "Configuration";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Sonic",
            "Shadow",
            "Silver",
            "Last Episode"});
            this.comboBox2.Location = new System.Drawing.Point(57, 45);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(201, 21);
            this.comboBox2.TabIndex = 46;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Xbox 360",
            "PlayStation 3"});
            this.comboBox1.Location = new System.Drawing.Point(57, 18);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(201, 21);
            this.comboBox1.TabIndex = 45;
            // 
            // lbl_ModsDirectory
            // 
            this.lbl_ModsDirectory.AutoSize = true;
            this.lbl_ModsDirectory.Location = new System.Drawing.Point(7, 21);
            this.lbl_ModsDirectory.Name = "lbl_ModsDirectory";
            this.lbl_ModsDirectory.Size = new System.Drawing.Size(44, 13);
            this.lbl_ModsDirectory.TabIndex = 44;
            this.lbl_ModsDirectory.Text = "System:";
            // 
            // lbl_GameDirectory
            // 
            this.lbl_GameDirectory.AutoSize = true;
            this.lbl_GameDirectory.Location = new System.Drawing.Point(17, 49);
            this.lbl_GameDirectory.Name = "lbl_GameDirectory";
            this.lbl_GameDirectory.Size = new System.Drawing.Size(34, 13);
            this.lbl_GameDirectory.TabIndex = 38;
            this.lbl_GameDirectory.Text = "Story:";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(57, 72);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(201, 20);
            this.numericUpDown1.TabIndex = 55;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(57, 98);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(201, 20);
            this.numericUpDown2.TabIndex = 56;
            // 
            // btn_Save
            // 
            this.btn_Save.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Save.BackColor = System.Drawing.Color.LightGreen;
            this.btn_Save.FlatAppearance.BorderSize = 0;
            this.btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Save.Location = new System.Drawing.Point(5, 136);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(270, 23);
            this.btn_Save.TabIndex = 55;
            this.btn_Save.Text = "Save Changes";
            this.btn_Save.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 57;
            this.label1.Text = "Lives:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 58;
            this.label2.Text = "Money:";
            // 
            // Save_Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 164);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.group_Search);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Save_Editor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Save Editor";
            this.group_Search.ResumeLayout(false);
            this.group_Search.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox group_Search;
        private System.Windows.Forms.Label lbl_ModsDirectory;
        private System.Windows.Forms.Label lbl_GameDirectory;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button btn_Save;
    }
}