using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace HfutIe
{
    public partial class AgvForm : Form
    {
        string WcCode;
        T_ORDER_INFO agvmessage = new T_ORDER_INFO();
        public bool IsSend = false;//是否发送（是手动点击关闭窗体，还是发送后自动关闭）
        public int IsSendSuccess;//是否发送成功
        string agvConnetionSource = System.Windows.Forms.Application.StartupPath + "\\AgvConfig.xml";
        string agvConnetion = data.data.ConnStr;

        public AgvForm(string wc_code , string part_code)
        {
            InitializeComponent();
            WcCode = wc_code;
            if (!string.IsNullOrWhiteSpace(part_code))
            {
                this.Text = "选择取空点/送料点：" + part_code;
            }
        }

        #region 信息
        // WMS_ID 订单号
        // STEP_ID 订单步号
        //PRIORITY 优先级
        //GETEMP_ID 取空托盘点
        //PUTEMP_ID 送空托盘点
        //GETFULL_ID 取满料点
        //PUTFULL_ID 送满料点
        //JOB_STATUS 订单状态
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            //HandleClick(sender);
            btn_empty_position1.BackColor = Color.Yellow;
            btn_empty_position2.BackColor = Color.FromArgb(213, 231, 243);
            btn_empty_position3.BackColor = Color.FromArgb(213, 231, 243);
            agvmessage.GETEMP_ID = GetTempIdOrPutFullId(WcCode, sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //HandleClick(sender);
            btn_empty_position1.BackColor = Color.FromArgb(213, 231, 243);
            btn_empty_position2.BackColor = Color.Yellow;
            btn_empty_position3.BackColor = Color.FromArgb(213, 231, 243);
            agvmessage.GETEMP_ID = GetTempIdOrPutFullId(WcCode, sender);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //HandleClick(sender);
            btn_empty_position1.BackColor = Color.FromArgb(213, 231, 243);
            btn_empty_position2.BackColor = Color.FromArgb(213, 231, 243);
            btn_empty_position3.BackColor = Color.Yellow;
            agvmessage.GETEMP_ID = GetTempIdOrPutFullId(WcCode, sender);
        }
        
        private void btn_lack_position1_Click(object sender, EventArgs e)
        {
            btn_lack_position1.BackColor = Color.Yellow;
            btn_lack_position2.BackColor = Color.FromArgb(213, 231, 243);
            btn_lack_position3.BackColor = Color.FromArgb(213, 231, 243);
            agvmessage.PUTFULL_ID = GetTempIdOrPutFullId(WcCode, sender);
        }

        private void btn_lack_position2_Click(object sender, EventArgs e)
        {
            btn_lack_position1.BackColor = Color.FromArgb(213, 231, 243);
            btn_lack_position2.BackColor = Color.Yellow;
            btn_lack_position3.BackColor = Color.FromArgb(213, 231, 243);
            agvmessage.PUTFULL_ID = GetTempIdOrPutFullId(WcCode, sender);
        }

        private void btn_lack_position3_Click(object sender, EventArgs e)
        {
            btn_lack_position1.BackColor = Color.FromArgb(213, 231, 243);
            btn_lack_position2.BackColor = Color.FromArgb(213, 231, 243);
            btn_lack_position3.BackColor = Color.Yellow;
            agvmessage.PUTFULL_ID = GetTempIdOrPutFullId(WcCode, sender);
        }

        #region 处理按钮点击

        private void HandleClick(object sender, EventArgs e)
        {
            try
            {
                if (agvmessage.GETEMP_ID != null)
                {
                    if(agvmessage.PUTFULL_ID != null)
                    {

                        agvmessage.WMS_ID = GetWmsId();
                        agvmessage.STEP_ID = 1;
                        agvmessage.PRIORITY = 1;
                        //agvmessage.GETEMP_ID = GetTempIdOrPutFullId(WcCode, sender);
                        agvmessage.PUTEMP_ID = PutTempId(WcCode);
                        agvmessage.GETFULL_ID = GetFullId(WcCode);
                        //agvmessage.PUTFULL_ID = GetTempIdOrPutFullId(WcCode, sender);
                        agvmessage.JOB_STATUS = 0;
                        #region 判断是否连续两次相同叫料 如果是 弹出提醒框 否则 直接叫料
                        if (HasSameCall(agvmessage))
                        {
                            T_Log tlog = new T_Log();
                            tlog.wc_code = WcCode;
                            DateTime dtime = DateTime.Now;
                            tlog.LogContent = dtime + "时弹出了连续重复呼叫报警提醒，取空料位置为" + agvmessage.GETEMP_ID + " 送满料位置分别为 " + agvmessage.PUTFULL_ID;
                            DialogResult result = MessageBox.Show("当前呼叫重复", "连续重复呼叫报警提醒！是否继续呼叫？", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == DialogResult.No)
                            {
                                tlog.StaffOperation = "员工选择了取消呼叫";
                                tlog.Create();
                                new DataBaseOpByEntity().InsertToOtherDataBase(tlog, agvConnetion);
                                //放弃发送
                                return;
                            }
                            tlog.StaffOperation = "员工选择了强制呼叫";
                            tlog.Create();
                            new DataBaseOpByEntity().InsertToOtherDataBase(tlog, agvConnetion);
                        }
                        #endregion
                   //  IsSendSuccess = SendMessageToAgv(agvmessage);
                        IsSend = true;//提交数据后，自动关闭窗体
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("请先选择缺料点。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("请先选择空料点。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("andon呼叫失败！" + ex.Message);
            }
        }
        /// <summary>
        /// 判断此呼叫在agv控制台是否存在连续相同的呼叫
        /// </summary>
        /// <param name="agvmessage"></param>
        /// <returns></returns>
        private bool HasSameCall(T_ORDER_INFO agvmessage)
        {
            bool result = false;
            string sql = " select top(1) *  FROM [T_ORDER_INFO] where JOB_STATUS!='3' and JOB_STATUS!='4' and (GETEMP_ID='" + agvmessage .GETEMP_ID+ "' or PUTFULL_ID='" + agvmessage .PUTFULL_ID+ "')  order by WMS_ID desc";
            DataTable dt  =  new DataBaseOpByEntity().FindEntity(sql,agvConnetion);
            T_ORDER_INFO hasentity = EntityHelper<T_ORDER_INFO>.GetEntity(dt);
            if(dt.Rows.Count>0)
            {
                if (agvmessage.PUTFULL_ID== hasentity.PUTFULL_ID&& agvmessage.GETEMP_ID==hasentity.GETEMP_ID)
                {
                    result = true;
                }
            }
            return result;
        }
        #endregion

        #region 取空/送满料点

        private int? GetTempIdOrPutFullId(string wcCode, object sender)
        {
            try
            {
                int id = 0;
                switch (wcCode)
                {
                    case "QK-OP-A090-R":
                        switch (((Button)sender).Name)
                        {
                            case "btn_empty_position1":
                            case "btn_lack_position1":
                                id = 381;
                                break;
                            case "btn_empty_position2":
                            case "btn_lack_position2":
                                id = 100;
                                break;
                            case "btn_empty_position3":
                            case "btn_lack_position3":
                                id = 98;
                                break;
                        }
                        break;
                    case "QK-OP-A090-L":
                        switch (((Button)sender).Name)
                        {
                            case "btn_empty_position1":
                            case "btn_lack_position1":
                                id = 92;
                                break;
                            case "btn_empty_position2":
                            case "btn_lack_position2":
                                id = 94;
                                break;
                            case "btn_empty_position3":
                            case "btn_lack_position3":
                                id = 96;
                                break;
                        }
                        break;
                    case "QK-OP-A120-R":
                        switch (((Button)sender).Name)
                        {
                            case "btn_empty_position1":
                            case "btn_lack_position1":
                                id = 56;
                                break;
                            case "btn_empty_position2":
                            case "btn_lack_position2":
                                id = 54;
                                break;
                            case "btn_empty_position3":
                            case "btn_lack_position3":
                                id = 52;
                                break;
                        }
                        break;
                    case "QK-OP-A120-L":
                        switch (((Button)sender).Name)
                        {
                            case "btn_empty_position1":
                            case "btn_lack_position1":
                                id = 70;
                                break;
                            case "btn_empty_position2":
                            case "btn_lack_position2":
                                id = 68;
                                break;
                            case "btn_empty_position3":
                            case "btn_lack_position3":
                                id = 66;
                                break;
                        }
                        break;
                    case "ZK-OP-A090-R":
                        switch (((Button)sender).Name)
                        {
                            case "btn_empty_position1":
                            case "btn_lack_position1":
                                id = 104;
                                break;
                            case "btn_empty_position2":
                            case "btn_lack_position2":
                                id = 108;
                                break;
                            case "btn_empty_position3":
                            case "btn_lack_position3":
                                id = 106;
                                break;
                        }
                        break;
                    case "ZK-OP-A090-L":
                        switch (((Button)sender).Name)
                        {
                            case "btn_empty_position1":
                            case "btn_lack_position1":
                                id = 58;
                                break;
                            case "btn_empty_position2":
                            case "btn_lack_position2":
                                id = 60;
                                break;
                            case "btn_empty_position3":
                            case "btn_lack_position3":
                                id = 62;
                                break;
                        }
                        break;
                    case "ZK-OP-A120-R":
                        switch (((Button)sender).Name)
                        {
                            case "btn_empty_position1":
                            case "btn_lack_position1":
                                id = 86;
                                break;
                            case "btn_empty_position2":
                            case "btn_lack_position2":
                                id = 84;
                                break;
                            case "btn_empty_position3":
                            case "btn_lack_position3":
                                id = 82;
                                break;
                        }
                        break;
                    case "ZK-OP-A120-L":
                        switch (((Button)sender).Name)
                        {
                            case "btn_empty_position1":
                            case "btn_lack_position1":
                                id = 50;
                                break;
                            case "btn_empty_position2":
                            case "btn_lack_position2":
                                id = 48;
                                break;
                            case "btn_empty_position3":
                            case "btn_lack_position3":
                                id = 46;
                                break;
                        }
                        break;
                }
                return id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion

        #region 取满料点

        private int? GetFullId(string wcCode)
        {
            try
            {
                int id = 0;
                switch (wcCode)
                {
                    case "QK-OP-A090-L":
                    case "QK-OP-A120-L":
                        id = 703;
                        break;
                    case "QK-OP-A090-R":
                    case "QK-OP-A120-R":
                        id = 699;
                        break;
                    case "ZK-OP-A090-L":
                    case "ZK-OP-A120-L":
                        id = 174;
                        break;
                    case "ZK-OP-A090-R":
                    case "ZK-OP-A120-R":
                        id = 171;
                        break;
                }
                return id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion

        #region 送空料点

        private int? PutTempId(string wcCode)
        {
            try
            {
                int id = 0;
                switch (wcCode)
                {
                    case "QK-OP-A090-L":
                    case "QK-OP-A120-L":
                        id = 183;
                        break;
                    case "QK-OP-A090-R":
                    case "QK-OP-A120-R":
                        id = 180;
                        break;
                    case "ZK-OP-A090-L":
                    case "ZK-OP-A120-L":
                        id = 719;
                        break;
                    case "ZK-OP-A090-R":
                    case "ZK-OP-A120-R":
                        id = 715;
                        break;
                }
                return id;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion

        #region 获取订单号

        private int? GetWmsId()
        {
            try
            {
                agvConnetion = GetAgvConnection(agvConnetionSource);
                string sqlmax = "SELECT MAX(WMS_ID) AS maxcodenum FROM[T_ORDER_INFO]";
                object originalcode = new DataBaseOpByEntity().FindObject(sqlmax, agvConnetion);
                int orderCode = 0;
                if (originalcode != null)
                {
                    try
                    {
                        orderCode = Int32.Parse(originalcode.ToString()) + 1;
                    }
                    catch
                    {
                        orderCode = 1;
                    }
                } 
                return orderCode;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion

        #region 发送AGV信息

        public int SendMessageToAgv(T_ORDER_INFO agvmessage)
        {
            //int IsSuccess = new DataBaseOpByEntity().InsertToOtherDataBase(agvmessage, "Server=192.169.14.66;Initial Catalog=AGV;User ID=sa;Password=agv");
            int IsSuccess = new DataBaseOpByEntity().InsertToOtherDataBase(agvmessage, agvConnetion);
            if (IsSuccess < 0)
            {
                IsSuccess = 0;//如果失败，将之置为0，使返回值为0
            }
            return IsSuccess;
        }
        #endregion

        #region 获取AGV数据库的连接
        public string GetAgvConnection(string datasorucePath)
        {
            string connection = data.data.ConnStr;
            if (System.IO.File.Exists(datasorucePath))
            {
                XmlReader rdr = XmlReader.Create(datasorucePath);
                ArrayList al = new ArrayList();
                while (rdr.Read())
                {
                    if (rdr.Value != "")
                    {
                        al.Add(rdr.Value.ToString());
                    }
                }
                rdr.Close();
                data.data.dataSource = al[5].ToString();
                data.data.dataBase = al[11].ToString();
                data.data.uid = al[7].ToString();
                try
                {
                    //data.data.passWord = DESEncrypt.Decrypt(al[9].ToString());
                    data.data.passWord = al[9].ToString();
                }
                catch (Exception ex)
                {
                    data.data.passWord = "";
                }
                connection= "data source=" + al[5].ToString() + ";database=" + al[11].ToString() + ";user id=" + al[7].ToString() + ";password=" + data.data.passWord + "";
            }
            return connection;
        }
        #endregion
    }
}
