using DevExpress.XtraGrid.Columns;
using HfutIE.Entity;
using HfutIE.Repository;
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
    public partial class PlanSearch : Form
    {
        string product_line_code;
        public PlanSearch(BasicInfoDto basicinfor)
        {
            product_line_code = basicinfor.PRODUCTION_LINE_CODE;//产线编号
            InitializeComponent();
        }
        
        #region 仓储
        RepositoryFactory<P_PLAN> PlanRepositoryFactory = new RepositoryFactory<P_PLAN>();//计划信息
        #endregion
        private void search_btn_Click(object sender, EventArgs e)//计划检索
        {
            //string contition = "";
            //string keywords = "";
            //contition = condition_cmb.Text.Trim();
            //keywords = keywords_txt.Text.ToString().Trim();
            //DateTime begin_time = begintime_dtp.Value.Date;
            //DateTime end_time = endtime_dtp.Value.Date.AddDays(1);
            //if (contition == "" || contition == "--请选择--")
            //{
            //    MessageBox.Show("请选择检索条件!", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //List<P_PLAN> planlist = PlanRepositoryFactory.Repository().FindList().Where(s=>s.PLAN_DATE>=begin_time&&s.PLAN_DATE<=end_time).ToList();//获取所有计划信息，可修改
            //List<P_PLAN> showplan;
            //switch (contition)
            //{
            //    case "全部":
            //        showplan = planlist;
            //        break;
            //    case "计划编号":
            //        showplan = planlist.FindAll(s => s.MES_PLAN_CODE == keywords);
            //        break;
            //    case "产品编号":
            //        showplan = planlist.FindAll(s => s.PART_CODE == keywords);
            //        break;
            //    //case "产品明码 ":
            //    //    sqlstr = "select PRODUCTION_ORDER_CODE ,PLAN_CODE,PLAN_NUM,HM_ONLINE_NUM,HM_OFFLINE_NUM,HM_OFFLINE_PASS_NUM,PLAN_RELEASE_DATE,PLAN_PEND_DATE,REMARKS from P_PLAN ";
            //    //    sqlstr = sqlstr + "  where PLAN_CODE in( select PLAN_CODE from P_PRODUCT_STATE where PRODUCT_EXPOSURE_CODE like '%" + keywords + "%' )  and (EXECUTION_STATUS='01' or EXECUTION_STATUS='02')";
            //    //    break;
            //    default:
            //        break;
            //}
            //planinfor_dgv.DataSource = planlist;
        }

        #region 界面加载
        private void PlanSearch_Load(object sender, EventArgs e)
        {
            SetDataSource();//初始化Datagridview
        }
        #endregion

        private void SetDataSource()
        {
            List<P_PLAN> planlist = PlanRepositoryFactory.Repository().FindList().Where(s => s.PRODUCTION_LINE_CODE == product_line_code).OrderBy(s=>s.MES_PLAN_CODE).ToList();
            planinfor_dgv.DataSource = planlist;
        }

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
    }
}
