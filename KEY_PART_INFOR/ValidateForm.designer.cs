namespace HfutIe.Online
{
    partial class ValidateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ValidateForm));
            this.form_panel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.confrim_btn = new DevExpress.XtraEditors.SimpleButton();
            this.center_panel = new System.Windows.Forms.Panel();
            this.password_lbl = new System.Windows.Forms.Label();
            this.passwordTip_lbl = new System.Windows.Forms.Label();
            this.tip_lbl = new System.Windows.Forms.Label();
            this.userTip_lbl = new System.Windows.Forms.Label();
            this.name_lbl = new System.Windows.Forms.Label();
            this.Password_txted = new DevExpress.XtraEditors.TextEdit();
            this.UserName_txted = new DevExpress.XtraEditors.TextEdit();
            this.top_panel = new System.Windows.Forms.Panel();
            this.Exit_pic_Box = new System.Windows.Forms.PictureBox();
            this.title_lbl = new System.Windows.Forms.Label();
            this.form_panel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.center_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Password_txted.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserName_txted.Properties)).BeginInit();
            this.top_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Exit_pic_Box)).BeginInit();
            this.SuspendLayout();
            // 
            // form_panel
            // 
            this.form_panel.Controls.Add(this.panel1);
            this.form_panel.Controls.Add(this.center_panel);
            this.form_panel.Controls.Add(this.top_panel);
            this.form_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.form_panel.Location = new System.Drawing.Point(0, 0);
            this.form_panel.Name = "form_panel";
            this.form_panel.Size = new System.Drawing.Size(327, 193);
            this.form_panel.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.simpleButton2);
            this.panel1.Controls.Add(this.confrim_btn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 133);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(327, 60);
            this.panel1.TabIndex = 3;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("华文细黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.simpleButton2.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton2.Image")));
            this.simpleButton2.Location = new System.Drawing.Point(45, 15);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "取   消";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // confrim_btn
            // 
            this.confrim_btn.Appearance.Font = new System.Drawing.Font("华文细黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.confrim_btn.Appearance.Options.UseFont = true;
            this.confrim_btn.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.confrim_btn.Image = ((System.Drawing.Image)(resources.GetObject("confrim_btn.Image")));
            this.confrim_btn.Location = new System.Drawing.Point(197, 15);
            this.confrim_btn.Name = "confrim_btn";
            this.confrim_btn.Size = new System.Drawing.Size(75, 23);
            this.confrim_btn.TabIndex = 2;
            this.confrim_btn.Text = "确   认";
            this.confrim_btn.Click += new System.EventHandler(this.confrim_btn_Click);
            // 
            // center_panel
            // 
            this.center_panel.Controls.Add(this.password_lbl);
            this.center_panel.Controls.Add(this.passwordTip_lbl);
            this.center_panel.Controls.Add(this.tip_lbl);
            this.center_panel.Controls.Add(this.userTip_lbl);
            this.center_panel.Controls.Add(this.name_lbl);
            this.center_panel.Controls.Add(this.Password_txted);
            this.center_panel.Controls.Add(this.UserName_txted);
            this.center_panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.center_panel.Location = new System.Drawing.Point(0, 26);
            this.center_panel.Name = "center_panel";
            this.center_panel.Size = new System.Drawing.Size(327, 107);
            this.center_panel.TabIndex = 1;
            // 
            // password_lbl
            // 
            this.password_lbl.AutoSize = true;
            this.password_lbl.Font = new System.Drawing.Font("华文细黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.password_lbl.Location = new System.Drawing.Point(27, 60);
            this.password_lbl.Name = "password_lbl";
            this.password_lbl.Size = new System.Drawing.Size(56, 17);
            this.password_lbl.TabIndex = 1;
            this.password_lbl.Text = "密    码";
            // 
            // passwordTip_lbl
            // 
            this.passwordTip_lbl.AutoSize = true;
            this.passwordTip_lbl.BackColor = System.Drawing.SystemColors.Control;
            this.passwordTip_lbl.Font = new System.Drawing.Font("华文细黑", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passwordTip_lbl.ForeColor = System.Drawing.Color.Red;
            this.passwordTip_lbl.Location = new System.Drawing.Point(252, 61);
            this.passwordTip_lbl.Name = "passwordTip_lbl";
            this.passwordTip_lbl.Size = new System.Drawing.Size(0, 12);
            this.passwordTip_lbl.TabIndex = 1;
            // 
            // tip_lbl
            // 
            this.tip_lbl.AutoSize = true;
            this.tip_lbl.Font = new System.Drawing.Font("华文细黑", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tip_lbl.ForeColor = System.Drawing.Color.Red;
            this.tip_lbl.Location = new System.Drawing.Point(94, 88);
            this.tip_lbl.Name = "tip_lbl";
            this.tip_lbl.Size = new System.Drawing.Size(0, 12);
            this.tip_lbl.TabIndex = 1;
            // 
            // userTip_lbl
            // 
            this.userTip_lbl.AutoSize = true;
            this.userTip_lbl.Font = new System.Drawing.Font("华文细黑", 8.999999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userTip_lbl.ForeColor = System.Drawing.Color.Red;
            this.userTip_lbl.Location = new System.Drawing.Point(252, 23);
            this.userTip_lbl.Name = "userTip_lbl";
            this.userTip_lbl.Size = new System.Drawing.Size(0, 12);
            this.userTip_lbl.TabIndex = 1;
            // 
            // name_lbl
            // 
            this.name_lbl.AutoSize = true;
            this.name_lbl.Font = new System.Drawing.Font("华文细黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.name_lbl.Location = new System.Drawing.Point(27, 21);
            this.name_lbl.Name = "name_lbl";
            this.name_lbl.Size = new System.Drawing.Size(56, 17);
            this.name_lbl.TabIndex = 1;
            this.name_lbl.Text = "用户名";
            // 
            // Password_txted
            // 
            this.Password_txted.Location = new System.Drawing.Point(99, 56);
            this.Password_txted.Name = "Password_txted";
            this.Password_txted.Properties.Appearance.Font = new System.Drawing.Font("华文细黑", 12F);
            this.Password_txted.Properties.Appearance.Options.UseFont = true;
            this.Password_txted.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.Password_txted.Properties.UseSystemPasswordChar = true;
            this.Password_txted.Size = new System.Drawing.Size(146, 26);
            this.Password_txted.TabIndex = 0;
            this.Password_txted.EditValueChanged += new System.EventHandler(this.Password_txted_EditValueChanged);
            // 
            // UserName_txted
            // 
            this.UserName_txted.Location = new System.Drawing.Point(99, 17);
            this.UserName_txted.Name = "UserName_txted";
            this.UserName_txted.Properties.Appearance.Font = new System.Drawing.Font("华文细黑", 12F);
            this.UserName_txted.Properties.Appearance.Options.UseFont = true;
            this.UserName_txted.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.UserName_txted.Size = new System.Drawing.Size(146, 26);
            this.UserName_txted.TabIndex = 0;
            this.UserName_txted.EditValueChanged += new System.EventHandler(this.UserName_txted_EditValueChanged);
            // 
            // top_panel
            // 
            this.top_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.top_panel.Controls.Add(this.Exit_pic_Box);
            this.top_panel.Controls.Add(this.title_lbl);
            this.top_panel.Dock = System.Windows.Forms.DockStyle.Top;
            this.top_panel.Font = new System.Drawing.Font("华文细黑", 14.25F);
            this.top_panel.ForeColor = System.Drawing.Color.White;
            this.top_panel.Location = new System.Drawing.Point(0, 0);
            this.top_panel.Name = "top_panel";
            this.top_panel.Size = new System.Drawing.Size(327, 26);
            this.top_panel.TabIndex = 0;
            // 
            // Exit_pic_Box
            // 
            this.Exit_pic_Box.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.Exit_pic_Box.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Exit_pic_Box.Image = ((System.Drawing.Image)(resources.GetObject("Exit_pic_Box.Image")));
            this.Exit_pic_Box.Location = new System.Drawing.Point(300, 1);
            this.Exit_pic_Box.Name = "Exit_pic_Box";
            this.Exit_pic_Box.Size = new System.Drawing.Size(25, 25);
            this.Exit_pic_Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Exit_pic_Box.TabIndex = 69;
            this.Exit_pic_Box.TabStop = false;
            this.Exit_pic_Box.Click += new System.EventHandler(this.Exit_pic_Box_Click);
            // 
            // title_lbl
            // 
            this.title_lbl.AutoSize = true;
            this.title_lbl.Location = new System.Drawing.Point(101, 2);
            this.title_lbl.Name = "title_lbl";
            this.title_lbl.Size = new System.Drawing.Size(124, 21);
            this.title_lbl.TabIndex = 0;
            this.title_lbl.Text = "操作权限验证";
            // 
            // ValidateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 193);
            this.Controls.Add(this.form_panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ValidateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ValidateForm";
            this.TopMost = true;
            this.form_panel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.center_panel.ResumeLayout(false);
            this.center_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Password_txted.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserName_txted.Properties)).EndInit();
            this.top_panel.ResumeLayout(false);
            this.top_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Exit_pic_Box)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel form_panel;
        private System.Windows.Forms.Panel center_panel;
        private System.Windows.Forms.Panel top_panel;
        private System.Windows.Forms.Label title_lbl;
        private System.Windows.Forms.Label password_lbl;
        private System.Windows.Forms.Label name_lbl;
        private DevExpress.XtraEditors.TextEdit Password_txted;
        private DevExpress.XtraEditors.TextEdit UserName_txted;
        private DevExpress.XtraEditors.SimpleButton confrim_btn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox Exit_pic_Box;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private System.Windows.Forms.Label passwordTip_lbl;
        private System.Windows.Forms.Label userTip_lbl;
        private System.Windows.Forms.Label tip_lbl;
    }
}