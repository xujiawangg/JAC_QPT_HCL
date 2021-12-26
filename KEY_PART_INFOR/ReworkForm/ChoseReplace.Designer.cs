namespace HfutIe
{
    partial class ChoseReplace
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChoseReplace));
            this.yes_btn = new System.Windows.Forms.Button();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.barcode_cobox = new System.Windows.Forms.ComboBox();
            this.panel16 = new System.Windows.Forms.Panel();
            this.main_station_lbl = new System.Windows.Forms.Label();
            this.Exit_pic_Box = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Exit_pic_Box)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // yes_btn
            // 
            this.yes_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.yes_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.yes_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.yes_btn.Font = new System.Drawing.Font("华文细黑", 12F, System.Drawing.FontStyle.Bold);
            this.yes_btn.ForeColor = System.Drawing.Color.White;
            this.yes_btn.Location = new System.Drawing.Point(66, 92);
            this.yes_btn.Name = "yes_btn";
            this.yes_btn.Size = new System.Drawing.Size(99, 34);
            this.yes_btn.TabIndex = 0;
            this.yes_btn.Text = "确 定";
            this.yes_btn.UseVisualStyleBackColor = false;
            this.yes_btn.Click += new System.EventHandler(this.yes_btn_Click);
            // 
            // cancel_btn
            // 
            this.cancel_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.cancel_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.cancel_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancel_btn.Font = new System.Drawing.Font("华文细黑", 12F, System.Drawing.FontStyle.Bold);
            this.cancel_btn.ForeColor = System.Drawing.Color.White;
            this.cancel_btn.Location = new System.Drawing.Point(229, 92);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(99, 34);
            this.cancel_btn.TabIndex = 1;
            this.cancel_btn.Text = "取 消";
            this.cancel_btn.UseVisualStyleBackColor = false;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("华文细黑", 14F);
            this.label1.Location = new System.Drawing.Point(18, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "安全件条码：";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.label1_MouseDown);
            this.label1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.label1_MouseUp);
            // 
            // barcode_cobox
            // 
            this.barcode_cobox.BackColor = System.Drawing.SystemColors.Control;
            this.barcode_cobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.barcode_cobox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.barcode_cobox.Font = new System.Drawing.Font("华文细黑", 12F);
            this.barcode_cobox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.barcode_cobox.FormattingEnabled = true;
            this.barcode_cobox.Location = new System.Drawing.Point(135, 26);
            this.barcode_cobox.Name = "barcode_cobox";
            this.barcode_cobox.Size = new System.Drawing.Size(234, 25);
            this.barcode_cobox.TabIndex = 3;
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(62)))), ((int)(((byte)(76)))));
            this.panel16.Controls.Add(this.main_station_lbl);
            this.panel16.Controls.Add(this.Exit_pic_Box);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel16.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel16.Location = new System.Drawing.Point(0, 0);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(403, 43);
            this.panel16.TabIndex = 66;
            // 
            // main_station_lbl
            // 
            this.main_station_lbl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.main_station_lbl.AutoSize = true;
            this.main_station_lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(62)))), ((int)(((byte)(76)))));
            this.main_station_lbl.Font = new System.Drawing.Font("华文细黑", 20F);
            this.main_station_lbl.ForeColor = System.Drawing.Color.White;
            this.main_station_lbl.Location = new System.Drawing.Point(118, 4);
            this.main_station_lbl.Name = "main_station_lbl";
            this.main_station_lbl.Size = new System.Drawing.Size(148, 30);
            this.main_station_lbl.TabIndex = 67;
            this.main_station_lbl.Text = "替换件选择";
            this.main_station_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Exit_pic_Box
            // 
            this.Exit_pic_Box.BackColor = System.Drawing.Color.Transparent;
            this.Exit_pic_Box.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Exit_pic_Box.Image = ((System.Drawing.Image)(resources.GetObject("Exit_pic_Box.Image")));
            this.Exit_pic_Box.Location = new System.Drawing.Point(360, 4);
            this.Exit_pic_Box.Name = "Exit_pic_Box";
            this.Exit_pic_Box.Size = new System.Drawing.Size(36, 36);
            this.Exit_pic_Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Exit_pic_Box.TabIndex = 65;
            this.Exit_pic_Box.TabStop = false;
            this.Exit_pic_Box.Click += new System.EventHandler(this.Exit_pic_Box_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.yes_btn);
            this.panel1.Controls.Add(this.barcode_cobox);
            this.panel1.Controls.Add(this.cancel_btn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(403, 147);
            this.panel1.TabIndex = 67;
            // 
            // ChoseReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(231)))), ((int)(((byte)(243)))));
            this.ClientSize = new System.Drawing.Size(403, 190);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel16);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChoseReplace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "选择替换安全件条码";
            this.Load += new System.EventHandler(this.ChoseReplace_Load);
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Exit_pic_Box)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button yes_btn;
        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox barcode_cobox;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Label main_station_lbl;
        private System.Windows.Forms.PictureBox Exit_pic_Box;
        private System.Windows.Forms.Panel panel1;
    }
}