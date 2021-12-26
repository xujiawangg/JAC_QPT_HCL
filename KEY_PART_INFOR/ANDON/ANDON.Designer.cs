namespace HfutIe
{
    partial class ANDON
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ANDON));
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.serverconnection_trm = new System.Windows.Forms.Timer(this.components);
            this.panel16 = new System.Windows.Forms.Panel();
            this.prompt_info_lbl = new System.Windows.Forms.Label();
            this.main_station_lbl = new System.Windows.Forms.Label();
            this.Exit_pic_Box = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Exit_pic_Box)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 60);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1116, 724);
            this.panel2.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.flowLayoutPanel4);
            this.groupBox4.Controls.Add(this.button7);
            this.groupBox4.Controls.Add(this.button8);
            this.groupBox4.Controls.Add(this.panel8);
            this.groupBox4.Controls.Add(this.panel9);
            this.groupBox4.Location = new System.Drawing.Point(0, 242);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(0, 0);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Location = new System.Drawing.Point(175, 21);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(839, 239);
            this.flowLayoutPanel4.TabIndex = 6;
            // 
            // button7
            // 
            this.button7.Dock = System.Windows.Forms.DockStyle.Right;
            this.button7.Location = new System.Drawing.Point(-315, 22);
            this.button7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(55, 0);
            this.button7.TabIndex = 5;
            this.button7.Text = "button7";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Dock = System.Windows.Forms.DockStyle.Left;
            this.button8.Location = new System.Drawing.Point(112, 22);
            this.button8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(55, 0);
            this.button8.TabIndex = 4;
            this.button8.Text = "button8";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // panel8
            // 
            this.panel8.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel8.Location = new System.Drawing.Point(-260, 22);
            this.panel8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(264, 0);
            this.panel8.TabIndex = 3;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.label1);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel9.Location = new System.Drawing.Point(4, 22);
            this.panel9.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(108, 0);
            this.panel9.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 25F);
            this.label1.Location = new System.Drawing.Point(31, 60);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // serverconnection_trm
            // 
            this.serverconnection_trm.Enabled = true;
            this.serverconnection_trm.Interval = 3000;
            this.serverconnection_trm.Tick += new System.EventHandler(this.serverconnection_trm_Tick);
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(62)))), ((int)(((byte)(76)))));
            this.panel16.Controls.Add(this.prompt_info_lbl);
            this.panel16.Controls.Add(this.main_station_lbl);
            this.panel16.Controls.Add(this.Exit_pic_Box);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel16.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel16.Location = new System.Drawing.Point(0, 0);
            this.panel16.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(1116, 60);
            this.panel16.TabIndex = 65;
            this.panel16.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel16_MouseDown);
            this.panel16.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel16_MouseMove);
            // 
            // prompt_info_lbl
            // 
            this.prompt_info_lbl.AutoSize = true;
            this.prompt_info_lbl.Font = new System.Drawing.Font("华文细黑", 12F);
            this.prompt_info_lbl.ForeColor = System.Drawing.Color.White;
            this.prompt_info_lbl.Location = new System.Drawing.Point(144, 34);
            this.prompt_info_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.prompt_info_lbl.Name = "prompt_info_lbl";
            this.prompt_info_lbl.Size = new System.Drawing.Size(875, 21);
            this.prompt_info_lbl.TabIndex = 69;
            this.prompt_info_lbl.Text = "黄色：即将呼叫的按灯   红色：正在呼叫的按灯   绿色：即将呼叫完成的按灯   蓝色：已响应的按灯";
            this.prompt_info_lbl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.prompt_info_lbl_MouseDown);
            this.prompt_info_lbl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.prompt_info_lbl_MouseMove);
            // 
            // main_station_lbl
            // 
            this.main_station_lbl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.main_station_lbl.AutoSize = true;
            this.main_station_lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(62)))), ((int)(((byte)(76)))));
            this.main_station_lbl.Font = new System.Drawing.Font("华文细黑", 20F);
            this.main_station_lbl.ForeColor = System.Drawing.Color.White;
            this.main_station_lbl.Location = new System.Drawing.Point(0, 6);
            this.main_station_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.main_station_lbl.Name = "main_station_lbl";
            this.main_station_lbl.Size = new System.Drawing.Size(153, 37);
            this.main_station_lbl.TabIndex = 68;
            this.main_station_lbl.Text = "按灯呼叫";
            this.main_station_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.main_station_lbl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.main_station_lbl_MouseDown);
            this.main_station_lbl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.main_station_lbl_MouseMove);
            // 
            // Exit_pic_Box
            // 
            this.Exit_pic_Box.BackColor = System.Drawing.Color.Transparent;
            this.Exit_pic_Box.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Exit_pic_Box.Image = ((System.Drawing.Image)(resources.GetObject("Exit_pic_Box.Image")));
            this.Exit_pic_Box.Location = new System.Drawing.Point(1055, 10);
            this.Exit_pic_Box.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Exit_pic_Box.Name = "Exit_pic_Box";
            this.Exit_pic_Box.Size = new System.Drawing.Size(48, 45);
            this.Exit_pic_Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Exit_pic_Box.TabIndex = 65;
            this.Exit_pic_Box.TabStop = false;
            this.Exit_pic_Box.Click += new System.EventHandler(this.Exit_pic_Box_Click);
            // 
            // ANDON
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1116, 784);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel16);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ANDON";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "按灯呼叫（黄色：即将呼叫的按灯   红色：正在呼叫的按灯   绿色：即将呼叫完成的按灯   蓝色：已响应的按灯）";
            this.panel2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Exit_pic_Box)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer serverconnection_trm;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Label prompt_info_lbl;
        private System.Windows.Forms.Label main_station_lbl;
        private System.Windows.Forms.PictureBox Exit_pic_Box;
    }
}

