namespace HfutIe.Offline
{
    partial class QueueManage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_delect = new System.Windows.Forms.Button();
            this.btn_down = new System.Windows.Forms.Button();
            this.btn_up = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvQueue = new System.Windows.Forms.DataGridView();
            this.offline_queue_key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.product_born_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.product_serial_no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.front_axle_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_OK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.assemble_offline_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(231)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.btn_delect);
            this.panel1.Controls.Add(this.btn_down);
            this.panel1.Controls.Add(this.btn_up);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.dgvQueue);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(802, 417);
            this.panel1.TabIndex = 0;
            // 
            // btn_delect
            // 
            this.btn_delect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.btn_delect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_delect.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_delect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btn_delect.Location = new System.Drawing.Point(562, 344);
            this.btn_delect.Name = "btn_delect";
            this.btn_delect.Size = new System.Drawing.Size(115, 39);
            this.btn_delect.TabIndex = 5;
            this.btn_delect.Text = "删除";
            this.btn_delect.UseVisualStyleBackColor = false;
            this.btn_delect.Click += new System.EventHandler(this.btn_delect_Click);
            // 
            // btn_down
            // 
            this.btn_down.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.btn_down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_down.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_down.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btn_down.Location = new System.Drawing.Point(340, 344);
            this.btn_down.Name = "btn_down";
            this.btn_down.Size = new System.Drawing.Size(115, 39);
            this.btn_down.TabIndex = 4;
            this.btn_down.Text = "下移";
            this.btn_down.UseVisualStyleBackColor = false;
            this.btn_down.Click += new System.EventHandler(this.btn_down_Click);
            // 
            // btn_up
            // 
            this.btn_up.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.btn_up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_up.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_up.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.btn_up.Location = new System.Drawing.Point(115, 344);
            this.btn_up.Name = "btn_up";
            this.btn_up.Size = new System.Drawing.Size(115, 39);
            this.btn_up.TabIndex = 3;
            this.btn_up.Text = "上移";
            this.btn_up.UseVisualStyleBackColor = false;
            this.btn_up.Click += new System.EventHandler(this.btn_up_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(802, 75);
            this.panel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.label1.Location = new System.Drawing.Point(294, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 46);
            this.label1.TabIndex = 1;
            this.label1.Text = "待抓取队列";
            // 
            // dgvQueue
            // 
            this.dgvQueue.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.dgvQueue.AllowUserToAddRows = false;
            this.dgvQueue.AllowUserToDeleteRows = false;
            this.dgvQueue.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvQueue.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvQueue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvQueue.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(231)))), ((int)(((byte)(242)))));
            this.dgvQueue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvQueue.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvQueue.ColumnHeadersHeight = 29;
            this.dgvQueue.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.offline_queue_key,
            this.product_born_code,
            this.product_serial_no,
            this.front_axle_code,
            this.is_OK,
            this.assemble_offline_time});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvQueue.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvQueue.Location = new System.Drawing.Point(0, 75);
            this.dgvQueue.Name = "dgvQueue";
            this.dgvQueue.RowHeadersVisible = false;
            this.dgvQueue.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvQueue.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvQueue.RowTemplate.Height = 23;
            this.dgvQueue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQueue.Size = new System.Drawing.Size(802, 246);
            this.dgvQueue.TabIndex = 0;
            // 
            // offline_queue_key
            // 
            this.offline_queue_key.HeaderText = "队列主键";
            this.offline_queue_key.Name = "offline_queue_key";
            this.offline_queue_key.ReadOnly = true;
            this.offline_queue_key.Visible = false;
            // 
            // PRODUCT_BORN_CODE
            // 
            this.product_born_code.FillWeight = 162.4366F;
            this.product_born_code.HeaderText = "产品出生证";
            this.product_born_code.Name = "PRODUCT_BORN_CODE";
            this.product_born_code.ReadOnly = true;
            // 
            // PRODUCT_SERIAL_NO
            // 
            this.product_serial_no.HeaderText = "产品序列号";
            this.product_serial_no.Name = "PRODUCT_SERIAL_NO";
            // 
            // front_axle_code
            // 
            this.front_axle_code.FillWeight = 79.18782F;
            this.front_axle_code.HeaderText = "前桥型号";
            this.front_axle_code.Name = "front_axle_code";
            this.front_axle_code.ReadOnly = true;
            // 
            // is_OK
            // 
            this.is_OK.FillWeight = 79.18782F;
            this.is_OK.HeaderText = "是否合格";
            this.is_OK.Name = "is_OK";
            this.is_OK.ReadOnly = true;
            // 
            // assemble_offline_time
            // 
            this.assemble_offline_time.FillWeight = 79.18782F;
            this.assemble_offline_time.HeaderText = "下线时间";
            this.assemble_offline_time.Name = "assemble_offline_time";
            this.assemble_offline_time.ReadOnly = true;
            this.assemble_offline_time.Visible = false;
            // 
            // QueueManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 417);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QueueManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "下线队列管理";
            this.Load += new System.EventHandler(this.QueueManage_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQueue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvQueue;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_delect;
        private System.Windows.Forms.Button btn_down;
        private System.Windows.Forms.Button btn_up;
        private System.Windows.Forms.DataGridViewTextBoxColumn offline_queue_key;
        private System.Windows.Forms.DataGridViewTextBoxColumn product_born_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn product_serial_no;
        private System.Windows.Forms.DataGridViewTextBoxColumn front_axle_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_OK;
        private System.Windows.Forms.DataGridViewTextBoxColumn assemble_offline_time;
    }
}