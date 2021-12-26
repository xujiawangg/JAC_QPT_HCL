namespace HfutIe.Offline
{
    partial class SafePartAdd
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dgvSafeInfo = new System.Windows.Forms.DataGridView();
            this.s_part_key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s_part_barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s_part_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s_part_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s_supplier_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.s_supplier_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSafeInfo)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(231)))), ((int)(((byte)(243)))));
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.dgvSafeInfo);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(754, 426);
            this.panel1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.button2.Location = new System.Drawing.Point(495, 375);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 39);
            this.button2.TabIndex = 3;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.button1.Location = new System.Drawing.Point(74, 375);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 39);
            this.button1.TabIndex = 2;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvSafeInfo
            // 
            this.dgvSafeInfo.AllowUserToAddRows = false;
            this.dgvSafeInfo.AllowUserToDeleteRows = false;
            this.dgvSafeInfo.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgvSafeInfo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSafeInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSafeInfo.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(231)))), ((int)(((byte)(242)))));
            this.dgvSafeInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSafeInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvSafeInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvSafeInfo.ColumnHeadersHeight = 30;
            this.dgvSafeInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.s_part_key,
            this.s_part_barcode,
            this.s_part_code,
            this.s_part_name,
            this.s_supplier_code,
            this.s_supplier_name});
            this.dgvSafeInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvSafeInfo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvSafeInfo.Location = new System.Drawing.Point(0, 81);
            this.dgvSafeInfo.Name = "dgvSafeInfo";
            this.dgvSafeInfo.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.dgvSafeInfo.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSafeInfo.RowTemplate.Height = 23;
            this.dgvSafeInfo.Size = new System.Drawing.Size(754, 288);
            this.dgvSafeInfo.TabIndex = 1;
            this.dgvSafeInfo.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.dgvSafeInfo_CellParsing);
            // 
            // s_part_key
            // 
            this.s_part_key.HeaderText = "零部件主键";
            this.s_part_key.Name = "s_part_key";
            this.s_part_key.ReadOnly = true;
            this.s_part_key.Visible = false;
            // 
            // s_part_barcode
            // 
            this.s_part_barcode.HeaderText = "零部件条码";
            this.s_part_barcode.Name = "s_part_barcode";
            // 
            // s_part_code
            // 
            this.s_part_code.HeaderText = "零部件编号";
            this.s_part_code.Name = "s_part_code";
            this.s_part_code.ReadOnly = true;
            // 
            // s_part_name
            // 
            this.s_part_name.HeaderText = "零部件名称";
            this.s_part_name.Name = "s_part_name";
            this.s_part_name.ReadOnly = true;
            // 
            // s_supplier_code
            // 
            this.s_supplier_code.HeaderText = "供应商编号";
            this.s_supplier_code.Name = "s_supplier_code";
            this.s_supplier_code.ReadOnly = true;
            // 
            // s_supplier_name
            // 
            this.s_supplier_name.HeaderText = "供应商名称";
            this.s_supplier_name.Name = "s_supplier_name";
            this.s_supplier_name.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(754, 81);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(229)))), ((int)(((byte)(242)))));
            this.label1.Location = new System.Drawing.Point(307, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "安全件补录";
            // 
            // SafePartAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 426);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SafePartAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "安全件补录";
            this.Load += new System.EventHandler(this.SafePartAdd_Load_1);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSafeInfo)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvSafeInfo;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn s_part_key;
        private System.Windows.Forms.DataGridViewTextBoxColumn s_part_barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn s_part_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn s_part_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn s_supplier_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn s_supplier_name;
    }
}