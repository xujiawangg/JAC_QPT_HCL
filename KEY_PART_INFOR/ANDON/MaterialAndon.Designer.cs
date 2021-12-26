namespace HfutIe
{
    partial class MaterialAndon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterialAndon));
            this.yes_btn = new System.Windows.Forms.Button();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.panel16 = new System.Windows.Forms.Panel();
            this.part_lbl = new System.Windows.Forms.Label();
            this.main_station_lbl = new System.Windows.Forms.Label();
            this.Exit_pic_Box = new System.Windows.Forms.PictureBox();
            this.panel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Exit_pic_Box)).BeginInit();
            this.SuspendLayout();
            // 
            // yes_btn
            // 
            this.yes_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.yes_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.yes_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.yes_btn.Font = new System.Drawing.Font("华文细黑", 12F, System.Drawing.FontStyle.Bold);
            this.yes_btn.ForeColor = System.Drawing.Color.White;
            this.yes_btn.Location = new System.Drawing.Point(54, 123);
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
            this.cancel_btn.Location = new System.Drawing.Point(249, 122);
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
            this.label1.Location = new System.Drawing.Point(56, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "配送数量：";
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.SystemColors.Control;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox1.Font = new System.Drawing.Font("华文细黑", 12F);
            this.comboBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(173, 66);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(155, 25);
            this.comboBox1.TabIndex = 3;
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(62)))), ((int)(((byte)(76)))));
            this.panel16.Controls.Add(this.part_lbl);
            this.panel16.Controls.Add(this.main_station_lbl);
            this.panel16.Controls.Add(this.Exit_pic_Box);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel16.Font = new System.Drawing.Font("华文细黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel16.Location = new System.Drawing.Point(0, 0);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(403, 43);
            this.panel16.TabIndex = 67;
            // 
            // part_lbl
            // 
            this.part_lbl.AutoSize = true;
            this.part_lbl.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.part_lbl.ForeColor = System.Drawing.Color.White;
            this.part_lbl.Location = new System.Drawing.Point(1, 20);
            this.part_lbl.Name = "part_lbl";
            this.part_lbl.Size = new System.Drawing.Size(0, 20);
            this.part_lbl.TabIndex = 68;
            // 
            // main_station_lbl
            // 
            this.main_station_lbl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.main_station_lbl.AutoSize = true;
            this.main_station_lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(62)))), ((int)(((byte)(76)))));
            this.main_station_lbl.Font = new System.Drawing.Font("华文细黑", 12F);
            this.main_station_lbl.ForeColor = System.Drawing.Color.White;
            this.main_station_lbl.Location = new System.Drawing.Point(2, 2);
            this.main_station_lbl.Name = "main_station_lbl";
            this.main_station_lbl.Size = new System.Drawing.Size(104, 17);
            this.main_station_lbl.TabIndex = 67;
            this.main_station_lbl.Text = "配送物料数量";
            this.main_station_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Exit_pic_Box
            // 
            this.Exit_pic_Box.BackColor = System.Drawing.Color.Transparent;
            this.Exit_pic_Box.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Exit_pic_Box.Image = ((System.Drawing.Image)(resources.GetObject("Exit_pic_Box.Image")));
            this.Exit_pic_Box.Location = new System.Drawing.Point(363, 3);
            this.Exit_pic_Box.Name = "Exit_pic_Box";
            this.Exit_pic_Box.Size = new System.Drawing.Size(36, 36);
            this.Exit_pic_Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Exit_pic_Box.TabIndex = 65;
            this.Exit_pic_Box.TabStop = false;
            this.Exit_pic_Box.Click += new System.EventHandler(this.Exit_pic_Box_Click);
            // 
            // MaterialAndon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(403, 190);
            this.Controls.Add(this.panel16);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.yes_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MaterialAndon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "编辑配送物料数量";
            this.Load += new System.EventHandler(this.MaterialAndon_Load);
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Exit_pic_Box)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button yes_btn;
        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Label main_station_lbl;
        private System.Windows.Forms.PictureBox Exit_pic_Box;
        private System.Windows.Forms.Label part_lbl;
    }
}