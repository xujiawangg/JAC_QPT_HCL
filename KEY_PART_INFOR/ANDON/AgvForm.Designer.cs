namespace HfutIe
{
    partial class AgvForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgvForm));
            this.btn_confirm = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_empty_position1 = new System.Windows.Forms.Button();
            this.btn_lack_position3 = new System.Windows.Forms.Button();
            this.btn_lack_position2 = new System.Windows.Forms.Button();
            this.btn_lack_position1 = new System.Windows.Forms.Button();
            this.btn_empty_position3 = new System.Windows.Forms.Button();
            this.btn_empty_position2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_confirm
            // 
            this.btn_confirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(231)))), ((int)(((byte)(243)))));
            this.btn_confirm.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_confirm.Font = new System.Drawing.Font("宋体", 45F);
            this.btn_confirm.Location = new System.Drawing.Point(0, 416);
            this.btn_confirm.Margin = new System.Windows.Forms.Padding(2);
            this.btn_confirm.Name = "btn_confirm";
            this.btn_confirm.Size = new System.Drawing.Size(920, 122);
            this.btn_confirm.TabIndex = 8;
            this.btn_confirm.Text = "确定";
            this.btn_confirm.UseVisualStyleBackColor = false;
            this.btn_confirm.Click += new System.EventHandler(this.HandleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_empty_position1);
            this.panel1.Controls.Add(this.btn_lack_position3);
            this.panel1.Controls.Add(this.btn_lack_position2);
            this.panel1.Controls.Add(this.btn_lack_position1);
            this.panel1.Controls.Add(this.btn_empty_position3);
            this.panel1.Controls.Add(this.btn_empty_position2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(920, 416);
            this.panel1.TabIndex = 9;
            // 
            // btn_empty_position1
            // 
            this.btn_empty_position1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(231)))), ((int)(((byte)(243)))));
            this.btn_empty_position1.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_empty_position1.Location = new System.Drawing.Point(220, 65);
            this.btn_empty_position1.Name = "btn_empty_position1";
            this.btn_empty_position1.Size = new System.Drawing.Size(203, 109);
            this.btn_empty_position1.TabIndex = 8;
            this.btn_empty_position1.Text = "料框1";
            this.btn_empty_position1.UseVisualStyleBackColor = false;
            this.btn_empty_position1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_lack_position3
            // 
            this.btn_lack_position3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(231)))), ((int)(((byte)(243)))));
            this.btn_lack_position3.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_lack_position3.Location = new System.Drawing.Point(701, 243);
            this.btn_lack_position3.Name = "btn_lack_position3";
            this.btn_lack_position3.Size = new System.Drawing.Size(203, 109);
            this.btn_lack_position3.TabIndex = 15;
            this.btn_lack_position3.Text = "料框3";
            this.btn_lack_position3.UseVisualStyleBackColor = false;
            this.btn_lack_position3.Click += new System.EventHandler(this.btn_lack_position3_Click);
            // 
            // btn_lack_position2
            // 
            this.btn_lack_position2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(231)))), ((int)(((byte)(243)))));
            this.btn_lack_position2.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_lack_position2.Location = new System.Drawing.Point(461, 243);
            this.btn_lack_position2.Name = "btn_lack_position2";
            this.btn_lack_position2.Size = new System.Drawing.Size(203, 109);
            this.btn_lack_position2.TabIndex = 14;
            this.btn_lack_position2.Text = "料框2";
            this.btn_lack_position2.UseVisualStyleBackColor = false;
            this.btn_lack_position2.Click += new System.EventHandler(this.btn_lack_position2_Click);
            // 
            // btn_lack_position1
            // 
            this.btn_lack_position1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(231)))), ((int)(((byte)(243)))));
            this.btn_lack_position1.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_lack_position1.Location = new System.Drawing.Point(220, 243);
            this.btn_lack_position1.Name = "btn_lack_position1";
            this.btn_lack_position1.Size = new System.Drawing.Size(203, 109);
            this.btn_lack_position1.TabIndex = 13;
            this.btn_lack_position1.Text = "料框1";
            this.btn_lack_position1.UseVisualStyleBackColor = false;
            this.btn_lack_position1.Click += new System.EventHandler(this.btn_lack_position1_Click);
            // 
            // btn_empty_position3
            // 
            this.btn_empty_position3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(231)))), ((int)(((byte)(243)))));
            this.btn_empty_position3.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_empty_position3.Location = new System.Drawing.Point(701, 65);
            this.btn_empty_position3.Name = "btn_empty_position3";
            this.btn_empty_position3.Size = new System.Drawing.Size(203, 109);
            this.btn_empty_position3.TabIndex = 10;
            this.btn_empty_position3.Text = "料框3";
            this.btn_empty_position3.UseVisualStyleBackColor = false;
            this.btn_empty_position3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btn_empty_position2
            // 
            this.btn_empty_position2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(231)))), ((int)(((byte)(243)))));
            this.btn_empty_position2.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_empty_position2.Location = new System.Drawing.Point(461, 65);
            this.btn_empty_position2.Name = "btn_empty_position2";
            this.btn_empty_position2.Size = new System.Drawing.Size(203, 109);
            this.btn_empty_position2.TabIndex = 9;
            this.btn_empty_position2.Text = "料框2";
            this.btn_empty_position2.UseVisualStyleBackColor = false;
            this.btn_empty_position2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 36F);
            this.label2.Location = new System.Drawing.Point(16, 273);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(212, 48);
            this.label2.TabIndex = 12;
            this.label2.Text = "缺料点：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 36F);
            this.label1.Location = new System.Drawing.Point(16, 96);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 48);
            this.label1.TabIndex = 11;
            this.label1.Text = "空料点：";
            // 
            // AgvForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(920, 538);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_confirm);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AgvForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择取空点/送料点";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_confirm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_empty_position1;
        private System.Windows.Forms.Button btn_lack_position3;
        private System.Windows.Forms.Button btn_lack_position2;
        private System.Windows.Forms.Button btn_lack_position1;
        private System.Windows.Forms.Button btn_empty_position3;
        private System.Windows.Forms.Button btn_empty_position2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}

