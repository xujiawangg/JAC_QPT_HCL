using HfutIE.Entity;
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
    public partial class QueueManage : Form
    {
        string pro_line_code = "QK_assembly_line";
        public QueueManage()
        {
            InitializeComponent();
        }

        private void QueueManage_Load(object sender, EventArgs e)
        {
            Load_dgvQueue();
        }
        private void btn_delect_Click(object sender, EventArgs e)
        {
            if (dgvQueue.SelectedRows.Count > 0)
            {
                for (int i = 0; i < dgvQueue.SelectedRows.Count; i++)
                {
                    string offline_queue_key = dgvQueue.SelectedRows[i].Cells["offline_queue_key"].Value.ToString();
                    string str = "select * from OfflineQueue where offline_queue_key='" + offline_queue_key + "'";
                    DataTable dt=DbHelperSQL.OpenTable(str);
                    DOC_OfflineQueue DOC_p_off = EntityHelper<DOC_OfflineQueue>.GetEntity(dt);
                    DOC_p_off.doc_offline_queue_key = dt.Rows[0]["offline_queue_key"].ToString();
                    DataBaseOpByEntity.Insert<DOC_OfflineQueue>(DOC_p_off);
                    string SQLstring = "DELETE  from   OfflineQueue where offline_queue_key='" + offline_queue_key + "'";//删除下线队列，转档案表
                    DbHelperSQL.ExecuteSql(SQLstring);
                }
                SetDgv(dgvQueue);
                Load_dgvQueue();
            }
            else
            {
                MessageBox.Show("请选择删除的行！");
            }
        }
        public void Load_dgvQueue()
        {
            string SQL = "select  * from  OfflineQueue where PRODUCTION_LINE_CODE='" + pro_line_code + "' order by num asc";
            DataTable dt_off = DbHelperSQL.OpenTable(SQL);
            if (dt_off.Rows.Count > 0)
                for (int row = 0; row < dt_off.Rows.Count; row++)
                {
                    dgvQueue.Rows.Add();
                    dgvQueue.Rows[row].Cells["offline_queue_key"].Value = dt_off.Rows[row]["offline_queue_key"].ToString();
                    dgvQueue.Rows[row].Cells["PRODUCT_BORN_CODE"].Value = dt_off.Rows[row]["PRODUCT_BORN_CODE"].ToString();
                    dgvQueue.Rows[row].Cells["front_axle_code"].Value = dt_off.Rows[row]["front_axle_code"].ToString();
                    dgvQueue.Rows[row].Cells["PRODUCT_SERIAL_NO"].Value = dt_off.Rows[row]["PRODUCT_SERIAL_NO"].ToString();
                    if (dt_off.Rows[row]["is_OK"].ToString() == "1")
                    {
                        dgvQueue.Rows[row].Cells["is_OK"].Value = "合格";
                    }
                    else
                    {
                        dgvQueue.Rows[row].Cells["is_OK"].Value = "不合格";
                    }
                }
        }
        private void btn_up_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection dgv = this.dgvQueue.SelectedRows;//获取选中行集合
            if(dgv.Count==1)
            {
                int Index = this.dgvQueue.SelectedRows[0].Index;
                if(Index >0)
                {
                    //操作数据库
                    string offline_queue_key = dgvQueue.Rows[Index].Cells["offline_queue_key"].Value.ToString ();
                    string offline_queue_keyup = dgvQueue.Rows[Index-1].Cells["offline_queue_key"].Value.ToString();
                    Int64 num = re_num(offline_queue_key);
                    Int64  num_up= re_num(offline_queue_keyup);
                    string str = "update OfflineQueue set num='" + num_up + "' where offline_queue_key='" + offline_queue_key + "'";
                    DbHelperSQL.ExecuteSql(str);
                    string str1 = "update OfflineQueue set num='" + num + "' where offline_queue_key='" + offline_queue_keyup + "'";
                    DbHelperSQL.ExecuteSql(str1);
                    //操作datagridview表
                    DataGridViewRow dgvr = this.dgvQueue.Rows[Index - 1];
                    this.dgvQueue.Rows.RemoveAt(Index - 1);//删除选中行的上一行
                    this.dgvQueue.Rows.Insert((Index), dgvr);//将选中行的上一行插到选中行的后面
                    this.dgvQueue.Rows[Index - 1].Selected = true; //选中移动后的行
                }
            }
            else
            {
                MessageBox.Show("请选择一行！");
            }
        }
        public Int64 re_num(string offline_queue_key)
        {
            string str = "select * from OfflineQueue where offline_queue_key='" + offline_queue_key + "'";
            DataTable dt = DbHelperSQL.OpenTable(str);
            Int64 num = int.Parse (dt.Rows[0]["num"].ToString());
            return num;
        }
        private void btn_down_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection dgv = this.dgvQueue.SelectedRows;//获取选中行集合
            if (dgv.Count == 1)
            {
                int Index = this.dgvQueue.SelectedRows[0].Index;
                if (Index <dgvQueue .Rows .Count-1)
                {
                    //操作数据库
                    string offline_queue_key = dgvQueue.Rows[Index].Cells["offline_queue_key"].Value.ToString();
                    string offline_queue_keydown = dgvQueue.Rows[Index + 1].Cells["offline_queue_key"].Value.ToString();
                    Int64 num = re_num(offline_queue_key);
                    Int64 num_down = re_num(offline_queue_keydown);
                    string str = "update OfflineQueue set num='" + num_down + "' where offline_queue_key='" + offline_queue_key + "'";
                    DbHelperSQL.ExecuteSql(str);
                    string str1 = "update OfflineQueue set num='" + num + "' where offline_queue_key='" + offline_queue_keydown + "'";
                    DbHelperSQL.ExecuteSql(str1);
                    //操作datagridview表
                    DataGridViewRow dgvr = this.dgvQueue.Rows[Index+1];
                    this.dgvQueue.Rows.RemoveAt(Index+1);//删除选中行
                    this.dgvQueue.Rows.Insert((Index), dgvr);//将选中行插到选中行的上面
                    this.dgvQueue.Rows[Index +1].Selected = true; //选中移动后的行
                }
            }
            else
            {
                MessageBox.Show("请选择一行！");
            }
        }
        private delegate void SetDgvDelegate(DataGridView dgvQueue);
        private void SetDgv(DataGridView dgvQueue)
        {
            if (dgvQueue.InvokeRequired) Invoke(new SetDgvDelegate(SetDgv), dgvQueue);
            else dgvQueue.Rows.Clear();
        }
    }
}
