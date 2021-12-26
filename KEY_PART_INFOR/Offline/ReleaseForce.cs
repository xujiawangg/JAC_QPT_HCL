using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HfutIe.Offline
{
    public partial class ReleaseForce : Form
    {
        public int isOK = 0;
        public ReleaseForce()
        {
            InitializeComponent();
        }

        private void ReleaseForce_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.label1.Text = "强制放行";
            
            this.label1.Font = new Font("微软雅黑", 36);
            this.label1.ForeColor = Color.White;
        }

        private void btnG_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认工件以合格状态强制下线？", "提醒", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                isOK = 1;
                this.Close();
                return;
            }
            else
            {

            }
        }

        private void btnNG_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认工件以不合格状态强制下线？", "提醒", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.OK)
            {
                isOK = 2;
                this.Close();
                return;
            }
            else
            {

            }
        }
    }
}
