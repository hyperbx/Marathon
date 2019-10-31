namespace Toolkit.Tools
{
    partial class LuaCompilation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LuaCompilation));
            this.pnl_Backdrop = new System.Windows.Forms.Panel();
            this.lbl_Experimental = new System.Windows.Forms.Label();
            this.pic_Logo = new System.Windows.Forms.PictureBox();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.lbl_Mode = new System.Windows.Forms.Label();
            this.combo_Mode = new System.Windows.Forms.ComboBox();
            this.btn_Process = new System.Windows.Forms.Button();
            this.btn_DeselectAll = new System.Windows.Forms.Button();
            this.btn_SelectAll = new System.Windows.Forms.Button();
            this.clb_LUBs = new System.Windows.Forms.CheckedListBox();
            this.pnl_Backdrop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_Backdrop
            // 
            this.pnl_Backdrop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_Backdrop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(124)))));
            this.pnl_Backdrop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnl_Backdrop.BackgroundImage")));
            this.pnl_Backdrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_Backdrop.Controls.Add(this.lbl_Experimental);
            this.pnl_Backdrop.Controls.Add(this.pic_Logo);
            this.pnl_Backdrop.Controls.Add(this.lbl_Title);
            this.pnl_Backdrop.Location = new System.Drawing.Point(-2, -1);
            this.pnl_Backdrop.Name = "pnl_Backdrop";
            this.pnl_Backdrop.Size = new System.Drawing.Size(439, 69);
            this.pnl_Backdrop.TabIndex = 41;
            // 
            // lbl_Experimental
            // 
            this.lbl_Experimental.AutoSize = true;
            this.lbl_Experimental.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Experimental.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Experimental.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_Experimental.Location = new System.Drawing.Point(18, 44);
            this.lbl_Experimental.Name = "lbl_Experimental";
            this.lbl_Experimental.Size = new System.Drawing.Size(73, 13);
            this.lbl_Experimental.TabIndex = 49;
            this.lbl_Experimental.Text = "experimental";
            // 
            // pic_Logo
            // 
            this.pic_Logo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pic_Logo.BackColor = System.Drawing.Color.Transparent;
            this.pic_Logo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pic_Logo.BackgroundImage")));
            this.pic_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic_Logo.Location = new System.Drawing.Point(362, 0);
            this.pic_Logo.Name = "pic_Logo";
            this.pic_Logo.Size = new System.Drawing.Size(67, 67);
            this.pic_Logo.TabIndex = 11;
            this.pic_Logo.TabStop = false;
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Title.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Title.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_Title.Location = new System.Drawing.Point(12, 8);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(290, 47);
            this.lbl_Title.TabIndex = 48;
            this.lbl_Title.Text = "Lua Compilation";
            // 
            // lbl_Mode
            // 
            this.lbl_Mode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Mode.AutoSize = true;
            this.lbl_Mode.Location = new System.Drawing.Point(185, 393);
            this.lbl_Mode.Name = "lbl_Mode";
            this.lbl_Mode.Size = new System.Drawing.Size(37, 13);
            this.lbl_Mode.TabIndex = 47;
            this.lbl_Mode.Text = "Mode:";
            // 
            // combo_Mode
            // 
            this.combo_Mode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.combo_Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_Mode.FormattingEnabled = true;
            this.combo_Mode.Items.AddRange(new object[] {
            "Decompile",
            "Compile"});
            this.combo_Mode.Location = new System.Drawing.Point(224, 389);
            this.combo_Mode.Name = "combo_Mode";
            this.combo_Mode.Size = new System.Drawing.Size(121, 21);
            this.combo_Mode.TabIndex = 46;
            this.combo_Mode.SelectedIndexChanged += new System.EventHandler(this.Combo_Mode_SelectedIndexChanged);
            // 
            // btn_Process
            // 
            this.btn_Process.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Process.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btn_Process.Enabled = false;
            this.btn_Process.FlatAppearance.BorderSize = 0;
            this.btn_Process.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Process.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Process.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Process.Location = new System.Drawing.Point(351, 388);
            this.btn_Process.Name = "btn_Process";
            this.btn_Process.Size = new System.Drawing.Size(75, 23);
            this.btn_Process.TabIndex = 45;
            this.btn_Process.Text = "Decompile";
            this.btn_Process.UseVisualStyleBackColor = false;
            this.btn_Process.Click += new System.EventHandler(this.Btn_Decompile_Click);
            // 
            // btn_DeselectAll
            // 
            this.btn_DeselectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_DeselectAll.BackColor = System.Drawing.Color.Tomato;
            this.btn_DeselectAll.FlatAppearance.BorderSize = 0;
            this.btn_DeselectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DeselectAll.Location = new System.Drawing.Point(91, 388);
            this.btn_DeselectAll.Name = "btn_DeselectAll";
            this.btn_DeselectAll.Size = new System.Drawing.Size(75, 23);
            this.btn_DeselectAll.TabIndex = 44;
            this.btn_DeselectAll.Text = "Deselect All";
            this.btn_DeselectAll.UseVisualStyleBackColor = false;
            this.btn_DeselectAll.Click += new System.EventHandler(this.Btn_DeselectAll_Click);
            // 
            // btn_SelectAll
            // 
            this.btn_SelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_SelectAll.BackColor = System.Drawing.Color.SkyBlue;
            this.btn_SelectAll.FlatAppearance.BorderSize = 0;
            this.btn_SelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SelectAll.Location = new System.Drawing.Point(9, 388);
            this.btn_SelectAll.Name = "btn_SelectAll";
            this.btn_SelectAll.Size = new System.Drawing.Size(75, 23);
            this.btn_SelectAll.TabIndex = 43;
            this.btn_SelectAll.Text = "Select All";
            this.btn_SelectAll.UseVisualStyleBackColor = false;
            this.btn_SelectAll.Click += new System.EventHandler(this.Btn_SelectAll_Click);
            // 
            // clb_LUBs
            // 
            this.clb_LUBs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clb_LUBs.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.clb_LUBs.CheckOnClick = true;
            this.clb_LUBs.FormattingEnabled = true;
            this.clb_LUBs.Location = new System.Drawing.Point(9, 77);
            this.clb_LUBs.Name = "clb_LUBs";
            this.clb_LUBs.Size = new System.Drawing.Size(417, 304);
            this.clb_LUBs.TabIndex = 42;
            this.clb_LUBs.SelectedIndexChanged += new System.EventHandler(this.Clb_LUBs_SelectedIndexChanged);
            // 
            // LuaCompilation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 419);
            this.Controls.Add(this.pnl_Backdrop);
            this.Controls.Add(this.lbl_Mode);
            this.Controls.Add(this.combo_Mode);
            this.Controls.Add(this.btn_Process);
            this.Controls.Add(this.btn_DeselectAll);
            this.Controls.Add(this.btn_SelectAll);
            this.Controls.Add(this.clb_LUBs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(450, 458);
            this.Name = "LuaCompilation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lua Compilation (experimental)";
            this.Load += new System.EventHandler(this.LuaCompilation_Load);
            this.pnl_Backdrop.ResumeLayout(false);
            this.pnl_Backdrop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnl_Backdrop;
        internal System.Windows.Forms.PictureBox pic_Logo;
        private System.Windows.Forms.Label lbl_Mode;
        private System.Windows.Forms.ComboBox combo_Mode;
        private System.Windows.Forms.Button btn_Process;
        private System.Windows.Forms.Button btn_DeselectAll;
        private System.Windows.Forms.Button btn_SelectAll;
        private System.Windows.Forms.CheckedListBox clb_LUBs;
        private System.Windows.Forms.Label lbl_Experimental;
        private System.Windows.Forms.Label lbl_Title;
    }
}