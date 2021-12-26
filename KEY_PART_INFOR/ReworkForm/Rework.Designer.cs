namespace KEY_PART_INFOR
{
    partial class Rework
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Rework));
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.rework_offline_btn = new System.Windows.Forms.Button();
            this.rework_online_btn = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.prompt_information_lbl = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.repair_offline_station_lbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sectionstation_dgv = new System.Windows.Forms.DataGridView();
            this.Is_checked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.oper_station_key = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oper_station_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.repair_online_station_cobox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.fail_info_panel = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.remark_ritchbox = new System.Windows.Forms.RichTextBox();
            this.maintain_item_lbl = new System.Windows.Forms.Label();
            this.maintain_item_cobox = new System.Windows.Forms.ComboBox();
            this.fa_main_info_insert_btn = new System.Windows.Forms.Button();
            this.maintain_type_lbl = new System.Windows.Forms.Label();
            this.maintain_type_cobox = new System.Windows.Forms.ComboBox();
            this.fault_item_lbl = new System.Windows.Forms.Label();
            this.fault_item_cobox = new System.Windows.Forms.ComboBox();
            this.fault_type_lbl = new System.Windows.Forms.Label();
            this.fault_type_cobox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.key_part_list_dgv = new System.Windows.Forms.DataGridView();
            this.panel16 = new System.Windows.Forms.Panel();
            this.main_station_lbl = new System.Windows.Forms.Label();
            this.Exit_pic_Box = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.scanport_rework = new System.IO.Ports.SerialPort(this.components);
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sectionstation_dgv)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.fail_info_panel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.key_part_list_dgv)).BeginInit();
            this.panel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Exit_pic_Box)).BeginInit();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 66);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(172, 739);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(62)))), ((int)(((byte)(76)))));
            this.groupBox2.Controls.Add(this.panel4);
            this.groupBox2.Controls.Add(this.panel8);
            this.groupBox2.Controls.Add(this.panel3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("华文细黑", 12F);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(172, 739);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "返修操作";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Control;
            this.panel4.Controls.Add(this.rework_offline_btn);
            this.panel4.Controls.Add(this.rework_online_btn);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(4, 32);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(164, 484);
            this.panel4.TabIndex = 4;
            // 
            // rework_offline_btn
            // 
            this.rework_offline_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.rework_offline_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rework_offline_btn.Font = new System.Drawing.Font("华文细黑", 12F, System.Drawing.FontStyle.Bold);
            this.rework_offline_btn.Image = ((System.Drawing.Image)(resources.GetObject("rework_offline_btn.Image")));
            this.rework_offline_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rework_offline_btn.Location = new System.Drawing.Point(9, 114);
            this.rework_offline_btn.Margin = new System.Windows.Forms.Padding(4);
            this.rework_offline_btn.Name = "rework_offline_btn";
            this.rework_offline_btn.Size = new System.Drawing.Size(148, 70);
            this.rework_offline_btn.TabIndex = 0;
            this.rework_offline_btn.Text = "返修下线";
            this.rework_offline_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rework_offline_btn.UseVisualStyleBackColor = false;
            this.rework_offline_btn.Click += new System.EventHandler(this.rework_offline_btn_Click);
            // 
            // rework_online_btn
            // 
            this.rework_online_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.rework_online_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rework_online_btn.Font = new System.Drawing.Font("华文细黑", 12F, System.Drawing.FontStyle.Bold);
            this.rework_online_btn.Image = ((System.Drawing.Image)(resources.GetObject("rework_online_btn.Image")));
            this.rework_online_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.rework_online_btn.Location = new System.Drawing.Point(9, 320);
            this.rework_online_btn.Margin = new System.Windows.Forms.Padding(4);
            this.rework_online_btn.Name = "rework_online_btn";
            this.rework_online_btn.Size = new System.Drawing.Size(145, 69);
            this.rework_online_btn.TabIndex = 1;
            this.rework_online_btn.Text = "返修上线";
            this.rework_online_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.rework_online_btn.UseVisualStyleBackColor = false;
            this.rework_online_btn.Click += new System.EventHandler(this.rework_online_btn_Click);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.panel8.Controls.Add(this.pictureBox1);
            this.panel8.Controls.Add(this.label3);
            this.panel8.Controls.Add(this.label4);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(4, 516);
            this.panel8.Margin = new System.Windows.Forms.Padding(4);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(164, 38);
            this.panel8.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(5, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 32);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.label3.Font = new System.Drawing.Font("华文细黑", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(45, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "信息提示";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(128, 25);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "\r\n";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Control;
            this.panel3.Controls.Add(this.prompt_information_lbl);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Font = new System.Drawing.Font("微软雅黑", 20F);
            this.panel3.Location = new System.Drawing.Point(4, 554);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(164, 181);
            this.panel3.TabIndex = 2;
            // 
            // prompt_information_lbl
            // 
            this.prompt_information_lbl.AutoSize = true;
            this.prompt_information_lbl.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.prompt_information_lbl.ForeColor = System.Drawing.Color.Black;
            this.prompt_information_lbl.Location = new System.Drawing.Point(-1, 9);
            this.prompt_information_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.prompt_information_lbl.Name = "prompt_information_lbl";
            this.prompt_information_lbl.Size = new System.Drawing.Size(0, 31);
            this.prompt_information_lbl.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Location = new System.Drawing.Point(246, 98);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(992, 717);
            this.panel2.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(62)))), ((int)(((byte)(76)))));
            this.groupBox4.Controls.Add(this.panel5);
            this.groupBox4.Font = new System.Drawing.Font("华文细黑", 12F);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(28, 32);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(266, 755);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "加工工位选择区域";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.repair_offline_station_lbl);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.sectionstation_dgv);
            this.panel5.Controls.Add(this.repair_online_station_cobox);
            this.panel5.Controls.Add(this.label1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(3, 31);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(260, 721);
            this.panel5.TabIndex = 0;
            // 
            // repair_offline_station_lbl
            // 
            this.repair_offline_station_lbl.AutoSize = true;
            this.repair_offline_station_lbl.ForeColor = System.Drawing.Color.Black;
            this.repair_offline_station_lbl.Location = new System.Drawing.Point(133, 21);
            this.repair_offline_station_lbl.Name = "repair_offline_station_lbl";
            this.repair_offline_station_lbl.Size = new System.Drawing.Size(110, 21);
            this.repair_offline_station_lbl.TabIndex = 4;
            this.repair_offline_station_lbl.Text = "未返修下线";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(2, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "返修下线工位:";
            // 
            // sectionstation_dgv
            // 
            this.sectionstation_dgv.AllowUserToAddRows = false;
            this.sectionstation_dgv.BackgroundColor = System.Drawing.SystemColors.Control;
            this.sectionstation_dgv.ColumnHeadersHeight = 30;
            this.sectionstation_dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.sectionstation_dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Is_checked,
            this.oper_station_key,
            this.oper_station_code});
            this.sectionstation_dgv.Location = new System.Drawing.Point(3, 115);
            this.sectionstation_dgv.Name = "sectionstation_dgv";
            this.sectionstation_dgv.RowTemplate.Height = 27;
            this.sectionstation_dgv.Size = new System.Drawing.Size(285, 584);
            this.sectionstation_dgv.TabIndex = 2;
            // 
            // Is_checked
            // 
            this.Is_checked.HeaderText = "选择";
            this.Is_checked.Name = "Is_checked";
            this.Is_checked.Width = 70;
            // 
            // oper_station_key
            // 
            this.oper_station_key.DataPropertyName = "oper_station_key";
            this.oper_station_key.HeaderText = "工位主键";
            this.oper_station_key.Name = "oper_station_key";
            this.oper_station_key.Visible = false;
            // 
            // oper_station_code
            // 
            this.oper_station_code.DataPropertyName = "oper_station_code";
            this.oper_station_code.HeaderText = "区间工位";
            this.oper_station_code.Name = "oper_station_code";
            this.oper_station_code.Width = 170;
            // 
            // repair_online_station_cobox
            // 
            this.repair_online_station_cobox.FormattingEnabled = true;
            this.repair_online_station_cobox.Location = new System.Drawing.Point(136, 64);
            this.repair_online_station_cobox.Name = "repair_online_station_cobox";
            this.repair_online_station_cobox.Size = new System.Drawing.Size(153, 29);
            this.repair_online_station_cobox.TabIndex = 1;
            this.repair_online_station_cobox.SelectionChangeCommitted += new System.EventHandler(this.repair_online_station_cobox_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(4, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "返修上线工位:";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(62)))), ((int)(((byte)(76)))));
            this.groupBox3.Controls.Add(this.fail_info_panel);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Font = new System.Drawing.Font("华文细黑", 12F);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(0, 523);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(891, 216);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "产品故障录入";
            // 
            // fail_info_panel
            // 
            this.fail_info_panel.BackColor = System.Drawing.SystemColors.Control;
            this.fail_info_panel.Controls.Add(this.label7);
            this.fail_info_panel.Controls.Add(this.remark_ritchbox);
            this.fail_info_panel.Controls.Add(this.maintain_item_lbl);
            this.fail_info_panel.Controls.Add(this.maintain_item_cobox);
            this.fail_info_panel.Controls.Add(this.fa_main_info_insert_btn);
            this.fail_info_panel.Controls.Add(this.maintain_type_lbl);
            this.fail_info_panel.Controls.Add(this.maintain_type_cobox);
            this.fail_info_panel.Controls.Add(this.fault_item_lbl);
            this.fail_info_panel.Controls.Add(this.fault_item_cobox);
            this.fail_info_panel.Controls.Add(this.fault_type_lbl);
            this.fail_info_panel.Controls.Add(this.fault_type_cobox);
            this.fail_info_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fail_info_panel.Location = new System.Drawing.Point(4, 32);
            this.fail_info_panel.Margin = new System.Windows.Forms.Padding(4);
            this.fail_info_panel.Name = "fail_info_panel";
            this.fail_info_panel.Size = new System.Drawing.Size(883, 180);
            this.fail_info_panel.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("华文细黑", 12F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(398, 10);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 21);
            this.label7.TabIndex = 14;
            this.label7.Text = "详细信息：";
            // 
            // remark_ritchbox
            // 
            this.remark_ritchbox.Font = new System.Drawing.Font("华文细黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.remark_ritchbox.ForeColor = System.Drawing.Color.Gray;
            this.remark_ritchbox.Location = new System.Drawing.Point(400, 49);
            this.remark_ritchbox.Margin = new System.Windows.Forms.Padding(4);
            this.remark_ritchbox.Name = "remark_ritchbox";
            this.remark_ritchbox.Size = new System.Drawing.Size(202, 119);
            this.remark_ritchbox.TabIndex = 13;
            this.remark_ritchbox.Text = "请录入故障详细信息(非必填项)";
            this.remark_ritchbox.Enter += new System.EventHandler(this.remark_ritchbox_Enter);
            this.remark_ritchbox.Leave += new System.EventHandler(this.remark_ritchbox_Leave);
            // 
            // maintain_item_lbl
            // 
            this.maintain_item_lbl.AutoSize = true;
            this.maintain_item_lbl.Font = new System.Drawing.Font("华文细黑", 12F, System.Drawing.FontStyle.Bold);
            this.maintain_item_lbl.ForeColor = System.Drawing.Color.Black;
            this.maintain_item_lbl.Location = new System.Drawing.Point(15, 142);
            this.maintain_item_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.maintain_item_lbl.Name = "maintain_item_lbl";
            this.maintain_item_lbl.Size = new System.Drawing.Size(115, 21);
            this.maintain_item_lbl.TabIndex = 11;
            this.maintain_item_lbl.Text = "排故措施：";
            // 
            // maintain_item_cobox
            // 
            this.maintain_item_cobox.BackColor = System.Drawing.Color.Azure;
            this.maintain_item_cobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.maintain_item_cobox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.maintain_item_cobox.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.maintain_item_cobox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.maintain_item_cobox.FormattingEnabled = true;
            this.maintain_item_cobox.Location = new System.Drawing.Point(135, 138);
            this.maintain_item_cobox.Margin = new System.Windows.Forms.Padding(4);
            this.maintain_item_cobox.Name = "maintain_item_cobox";
            this.maintain_item_cobox.Size = new System.Drawing.Size(248, 35);
            this.maintain_item_cobox.TabIndex = 12;
            // 
            // fa_main_info_insert_btn
            // 
            this.fa_main_info_insert_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(90)))), ((int)(((byte)(150)))));
            this.fa_main_info_insert_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fa_main_info_insert_btn.Font = new System.Drawing.Font("华文细黑", 14F, System.Drawing.FontStyle.Bold);
            this.fa_main_info_insert_btn.Location = new System.Drawing.Point(607, 62);
            this.fa_main_info_insert_btn.Margin = new System.Windows.Forms.Padding(4);
            this.fa_main_info_insert_btn.Name = "fa_main_info_insert_btn";
            this.fa_main_info_insert_btn.Size = new System.Drawing.Size(104, 89);
            this.fa_main_info_insert_btn.TabIndex = 10;
            this.fa_main_info_insert_btn.Text = "故障信息录入";
            this.fa_main_info_insert_btn.UseVisualStyleBackColor = false;
            this.fa_main_info_insert_btn.Click += new System.EventHandler(this.fa_main_info_insert_btn_Click);
            // 
            // maintain_type_lbl
            // 
            this.maintain_type_lbl.AutoSize = true;
            this.maintain_type_lbl.Font = new System.Drawing.Font("华文细黑", 12F, System.Drawing.FontStyle.Bold);
            this.maintain_type_lbl.ForeColor = System.Drawing.Color.Black;
            this.maintain_type_lbl.Location = new System.Drawing.Point(17, 100);
            this.maintain_type_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.maintain_type_lbl.Name = "maintain_type_lbl";
            this.maintain_type_lbl.Size = new System.Drawing.Size(115, 21);
            this.maintain_type_lbl.TabIndex = 8;
            this.maintain_type_lbl.Text = "排故类型：";
            // 
            // maintain_type_cobox
            // 
            this.maintain_type_cobox.BackColor = System.Drawing.Color.Azure;
            this.maintain_type_cobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.maintain_type_cobox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.maintain_type_cobox.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.maintain_type_cobox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.maintain_type_cobox.FormattingEnabled = true;
            this.maintain_type_cobox.Location = new System.Drawing.Point(135, 95);
            this.maintain_type_cobox.Margin = new System.Windows.Forms.Padding(4);
            this.maintain_type_cobox.Name = "maintain_type_cobox";
            this.maintain_type_cobox.Size = new System.Drawing.Size(248, 35);
            this.maintain_type_cobox.TabIndex = 9;
            this.maintain_type_cobox.SelectionChangeCommitted += new System.EventHandler(this.maintain_type_cobox_SelectionChangeCommitted);
            // 
            // fault_item_lbl
            // 
            this.fault_item_lbl.AutoSize = true;
            this.fault_item_lbl.Font = new System.Drawing.Font("华文细黑", 12F, System.Drawing.FontStyle.Bold);
            this.fault_item_lbl.ForeColor = System.Drawing.Color.Black;
            this.fault_item_lbl.Location = new System.Drawing.Point(18, 58);
            this.fault_item_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.fault_item_lbl.Name = "fault_item_lbl";
            this.fault_item_lbl.Size = new System.Drawing.Size(115, 21);
            this.fault_item_lbl.TabIndex = 6;
            this.fault_item_lbl.Text = "故障名称：";
            // 
            // fault_item_cobox
            // 
            this.fault_item_cobox.BackColor = System.Drawing.Color.Azure;
            this.fault_item_cobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fault_item_cobox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fault_item_cobox.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fault_item_cobox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.fault_item_cobox.FormattingEnabled = true;
            this.fault_item_cobox.Location = new System.Drawing.Point(135, 52);
            this.fault_item_cobox.Margin = new System.Windows.Forms.Padding(4);
            this.fault_item_cobox.Name = "fault_item_cobox";
            this.fault_item_cobox.Size = new System.Drawing.Size(248, 35);
            this.fault_item_cobox.TabIndex = 7;
            // 
            // fault_type_lbl
            // 
            this.fault_type_lbl.AutoSize = true;
            this.fault_type_lbl.Font = new System.Drawing.Font("华文细黑", 12F, System.Drawing.FontStyle.Bold);
            this.fault_type_lbl.ForeColor = System.Drawing.Color.Black;
            this.fault_type_lbl.Location = new System.Drawing.Point(17, 15);
            this.fault_type_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.fault_type_lbl.Name = "fault_type_lbl";
            this.fault_type_lbl.Size = new System.Drawing.Size(115, 21);
            this.fault_type_lbl.TabIndex = 4;
            this.fault_type_lbl.Text = "故障类型：";
            // 
            // fault_type_cobox
            // 
            this.fault_type_cobox.BackColor = System.Drawing.Color.Azure;
            this.fault_type_cobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fault_type_cobox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fault_type_cobox.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fault_type_cobox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.fault_type_cobox.FormattingEnabled = true;
            this.fault_type_cobox.Location = new System.Drawing.Point(135, 10);
            this.fault_type_cobox.Margin = new System.Windows.Forms.Padding(4);
            this.fault_type_cobox.Name = "fault_type_cobox";
            this.fault_type_cobox.Size = new System.Drawing.Size(248, 35);
            this.fault_type_cobox.TabIndex = 5;
            this.fault_type_cobox.SelectionChangeCommitted += new System.EventHandler(this.fault_type_cobox_SelectionChangeCommitted);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(62)))), ((int)(((byte)(76)))));
            this.groupBox1.Controls.Add(this.key_part_list_dgv);
            this.groupBox1.Font = new System.Drawing.Font("华文细黑", 12F);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(8, 54);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(655, 298);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "待补录安全件信息";
            // 
            // key_part_list_dgv
            // 
            this.key_part_list_dgv.BackgroundColor = System.Drawing.SystemColors.Control;
            this.key_part_list_dgv.ColumnHeadersHeight = 30;
            this.key_part_list_dgv.Location = new System.Drawing.Point(4, 32);
            this.key_part_list_dgv.Margin = new System.Windows.Forms.Padding(4);
            this.key_part_list_dgv.Name = "key_part_list_dgv";
            this.key_part_list_dgv.ReadOnly = true;
            this.key_part_list_dgv.RowTemplate.Height = 23;
            this.key_part_list_dgv.Size = new System.Drawing.Size(717, 487);
            this.key_part_list_dgv.TabIndex = 0;
            this.key_part_list_dgv.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.key_part_list_dgv_RowPostPaint);
            this.key_part_list_dgv.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.key_part_list_dgv_RowPrePaint);
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(62)))), ((int)(((byte)(76)))));
            this.panel16.Controls.Add(this.main_station_lbl);
            this.panel16.Controls.Add(this.Exit_pic_Box);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel16.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panel16.Location = new System.Drawing.Point(0, 0);
            this.panel16.Margin = new System.Windows.Forms.Padding(4);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(1193, 60);
            this.panel16.TabIndex = 65;
            // 
            // main_station_lbl
            // 
            this.main_station_lbl.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.main_station_lbl.AutoSize = true;
            this.main_station_lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(62)))), ((int)(((byte)(76)))));
            this.main_station_lbl.Font = new System.Drawing.Font("华文细黑", 25F);
            this.main_station_lbl.ForeColor = System.Drawing.Color.White;
            this.main_station_lbl.Location = new System.Drawing.Point(469, -1);
            this.main_station_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.main_station_lbl.Name = "main_station_lbl";
            this.main_station_lbl.Size = new System.Drawing.Size(188, 46);
            this.main_station_lbl.TabIndex = 67;
            this.main_station_lbl.Text = "产品返修";
            this.main_station_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Exit_pic_Box
            // 
            this.Exit_pic_Box.BackColor = System.Drawing.Color.Transparent;
            this.Exit_pic_Box.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Exit_pic_Box.Image = ((System.Drawing.Image)(resources.GetObject("Exit_pic_Box.Image")));
            this.Exit_pic_Box.Location = new System.Drawing.Point(1140, 7);
            this.Exit_pic_Box.Margin = new System.Windows.Forms.Padding(4);
            this.Exit_pic_Box.Name = "Exit_pic_Box";
            this.Exit_pic_Box.Size = new System.Drawing.Size(48, 45);
            this.Exit_pic_Box.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Exit_pic_Box.TabIndex = 65;
            this.Exit_pic_Box.TabStop = false;
            this.Exit_pic_Box.Click += new System.EventHandler(this.Exit_pic_Box_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(205)))), ((int)(((byte)(5)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBox1.Location = new System.Drawing.Point(0, 60);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(1193, 6);
            this.textBox1.TabIndex = 66;
            // 
            // scanport_rework
            // 
            this.scanport_rework.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.scanport_DataReceived);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.groupBox1);
            this.panel6.Controls.Add(this.groupBox3);
            this.panel6.Location = new System.Drawing.Point(526, 76);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(891, 739);
            this.panel6.TabIndex = 67;
            // 
            // Rework
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 805);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel16);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Rework";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "返修";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Rework_FormClosed);
            this.Load += new System.EventHandler(this.Rework_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sectionstation_dgv)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.fail_info_panel.ResumeLayout(false);
            this.fail_info_panel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.key_part_list_dgv)).EndInit();
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Exit_pic_Box)).EndInit();
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView key_part_list_dgv;
        private System.Windows.Forms.Button rework_online_btn;
        private System.Windows.Forms.Button rework_offline_btn;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.PictureBox Exit_pic_Box;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label main_station_lbl;
        public System.IO.Ports.SerialPort scanport_rework;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label prompt_information_lbl;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel fail_info_panel;
        private System.Windows.Forms.Label maintain_type_lbl;
        private System.Windows.Forms.ComboBox maintain_type_cobox;
        private System.Windows.Forms.Label fault_item_lbl;
        private System.Windows.Forms.ComboBox fault_item_cobox;
        private System.Windows.Forms.Label fault_type_lbl;
        private System.Windows.Forms.ComboBox fault_type_cobox;
        private System.Windows.Forms.Button fa_main_info_insert_btn;
        private System.Windows.Forms.RichTextBox remark_ritchbox;
        private System.Windows.Forms.Label maintain_item_lbl;
        private System.Windows.Forms.ComboBox maintain_item_cobox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DataGridView sectionstation_dgv;
        private System.Windows.Forms.ComboBox repair_online_station_cobox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Is_checked;
        private System.Windows.Forms.DataGridViewTextBoxColumn oper_station_key;
        private System.Windows.Forms.DataGridViewTextBoxColumn oper_station_code;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label repair_offline_station_lbl;
    }
}