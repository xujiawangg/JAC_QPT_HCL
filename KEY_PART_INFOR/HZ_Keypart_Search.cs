using DevExpress.XtraGrid.Columns;
using HfutIE.Entity;
using HfutIE.Repository;
using MsgBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEY_PART_INFOR
{
    public partial class HZ_Keypart_Search : Form
    {
        string condition, keywords;
        DateTime starttime, endtime;
        public HZ_Keypart_Search()
        {
            InitializeComponent();
        }

        public HZ_Keypart_Search(string station_code)
        {
            InitializeComponent();

            StringBuilder sb = new StringBuilder();
            sb.Append($"select * from (select * from P_KEY_PART_INFOR where station_code like '%L%' union select * from DOC_KEY_PART_INFOR where station_code like '%L%') where station_code like '%{station_code}%'");
            List<P_KEY_PART_INFOR> keypart_infor_list = P_KEY_PART_INFORRepositoryFactory.Repository().FindListBySql(sb.ToString()).Where(s=>s.CREATEDATE>DateTime.Now.Date&&s.CREATEDATE< DateTime.Now.Date.AddDays(1)).OrderByDescending(s=>s.CREATEDATE).ToList();
            SetdevgcDataSource(keypartinfor_dgv, keypart_infor_list);
        }

        #region 仓储
        RepositoryFactory<P_KEY_PART_INFOR> P_KEY_PART_INFORRepositoryFactory = new RepositoryFactory<P_KEY_PART_INFOR>();//计划信息
        #endregion
       
        #region 界面加载
        
        #endregion
        
        #region 界面关闭
        private void Exit_pic_Box_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 点击Panel和Label移动窗体
        Point downPoint;
        private void panel16_MouseDown(object sender, MouseEventArgs e)
        {
            downPoint = new Point(e.X, e.Y);
        }

        private void panel16_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - downPoint.X,
                    this.Location.Y + e.Y - downPoint.Y);
            }
        }

        private void main_station_lbl_MouseDown(object sender, MouseEventArgs e)
        {
            downPoint = new Point(e.X, e.Y);
        }

        private void main_station_lbl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - downPoint.X,
                    this.Location.Y + e.Y - downPoint.Y);
            }
        }
        #endregion

        #region 查询按钮点击事件
        private void search_btn_Click_1(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            condition = condition_cbox.Text;
            string real_condition="";
            keywords = keywords_txt.Text;
            if (searchbytime_cbox.Checked == true)//按时间查询被勾选时生效
            {
                starttime = dateTimePicker1.Value.Date;
                endtime = dateTimePicker2.Value.Date.AddDays(1);
                if (starttime > endtime)
                {
                    MyMsgBox.Show("开始时间不能晚于结束时间", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information, 3);
                    return;
                }
            }
            sb.Append("select * from (select * from P_KEY_PART_INFOR where station_code like '%L%' union select * from DOC_KEY_PART_INFOR where station_code like '%L%') where 1=1");
            if (string.IsNullOrEmpty(keywords))
            {
                condition = "全部";
            }
            switch (condition)
            {
                case "全部":
                    real_condition = "";
                    break;
                case "产品ID号":
                    real_condition = "PRODUCT_BORN_CODE";
                    break;
                case "物料编号":
                    real_condition = "PART_CODE";
                    break;
                case "工位编号":
                    real_condition = "STATION_CODE";
                    break;
                default:
                    real_condition = "";
                    break;

            }
            if (!string.IsNullOrEmpty(real_condition))
            {
                sb.Append($@" and {real_condition} LIKE '%{keywords.Trim()}%' ");
            }
            sb.Append(@" order by CREATEDATE desc");
            List<P_KEY_PART_INFOR> keypart_infor_list = P_KEY_PART_INFORRepositoryFactory.Repository().FindListBySql(sb.ToString()).ToList();
            if (searchbytime_cbox.Checked == true)
            {
                keypart_infor_list = keypart_infor_list.Where(s => s.CREATEDATE > starttime && s.CREATEDATE < endtime).ToList();
            }
            SetdevgcDataSource(keypartinfor_dgv, keypart_infor_list);
        }
        #endregion

        /// 使用委托为dgv赋值
        /// <param name="pb">dgv控件名</param>
        /// <param name="st">要赋值的List</param>
        private delegate void SetdevgcDataSourceDelegate<T>(DevExpress.XtraGrid.GridControl gc, List<T> list);

        private void panel16_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void SetdevgcDataSource<T>( DevExpress.XtraGrid.GridControl gc, List<T> st)
        {
            //string ID = Thread.CurrentThread.ManagedThreadId.ToString();//获取线程ID，可以进行跨线程调试
            if (gc.InvokeRequired) Invoke(new SetdevgcDataSourceDelegate<T>(SetdevgcDataSource), gc, st);
            else gc.DataSource = st;
        }

        private void searchbytime_cbox_CheckedChanged(object sender, EventArgs e)
        {
            if (searchbytime_cbox.Checked == true)
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
            }
            else
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
        }
    }
}
