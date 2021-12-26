using HfutIe.Entity;
using HfutIE.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using data;
using Opc.Ua;
using System.Net.NetworkInformation;
using System.Threading;
using Opc.Da;
using Opc;
using SimotionHelper;
using System.Configuration;
using HfutIE.Repository;
using HfutIe;
using System.IO;
using HfutIE.Utilities;
using EntityHelper;
using MsgBox;
using HFUTIEMES;
using System.Net;

namespace HfutIe.Offline
{
    //1.正常下线；操作数据库
    //2.下线失败；在线销项/强制放行
    public partial class OffLine : Form
    {
        #region 0 基础变量
        #region 静态变量
        public string wc_code_C = "OP_A7850M";
        public string pro_line_name_C = ConfigurationManager.AppSettings.Get("pro_line_name_C");
        static string constpk = string.Empty;
        string OpcName;
        public P_ASSEMBLE_PRODUCT_STATE plan_product_data;//通过产品出生证查到的该产品出生证代表的计划、产品信息
        public string pro_line_code = ConfigurationManager.AppSettings.Get("pro_line_code");
        List<WORK_CENTER> wc = new List<WORK_CENTER>();//下线pc对应的工作中心
        List<STATION> station = new List<STATION>();//工作中心下面对应的工位信息
        List<CONTROL_ADDRESS> ControlAddressList;//该工位的停止器所配置的地址信息
        CONTROL_ADDRESS pro_born_address;
        #endregion
        #region 仓储
        RepositoryFactory<Stopper> StopperRepositoryFactory = new RepositoryFactory<Stopper>();//停止器信息
        RepositoryFactory<CONTROL_ADDRESS> Control_AddressRepositoryFactory = new RepositoryFactory<CONTROL_ADDRESS>();//控制地址
        RepositoryFactory<Base_DataDictionary> Base_DataRepository = new RepositoryFactory<Base_DataDictionary>();
        RepositoryFactory<Base_DataDictionaryDetail> Base_DataDetailRepository = new RepositoryFactory<Base_DataDictionaryDetail>();
        RepositoryFactory<P_ASSEMBLE_PRODUCT_STATE> P_ASSEMBLE_PRODUCT_STATERepository = new RepositoryFactory<P_ASSEMBLE_PRODUCT_STATE>();
        RepositoryFactory<PRODUCT_DCS_PARM_LIST> PRODUCT_DCS_PARM_LISTRepository = new RepositoryFactory<PRODUCT_DCS_PARM_LIST>();
        RepositoryFactory<P_PLAN> P_PLANRepository = new RepositoryFactory<P_PLAN>();
        RepositoryFactory<P_KEY_PART_C> P_KEY_PART_CRepository = new RepositoryFactory<P_KEY_PART_C>();
        RepositoryFactory<P_KEY_PART_INFOR> P_KEY_PART_INFORRepository = new RepositoryFactory<P_KEY_PART_INFOR>();
        RepositoryFactory<DCS> DCSRepository = new RepositoryFactory<DCS>();
        RepositoryFactory<P_QUALITY_ELIMINATION> P_QUALITY_ELIMINATIONRepository = new RepositoryFactory<P_QUALITY_ELIMINATION>();
        RepositoryFactory<P_SAFE_ELIMINATION> P_SAFE_ELIMINATIONRepository = new RepositoryFactory<P_SAFE_ELIMINATION>();
        RepositoryFactory<DCS_PARM> DCS_PARMRepository = new RepositoryFactory<DCS_PARM>(); 
        RepositoryFactory<QUALITY_GATE> QUALITY_GATERepository = new RepositoryFactory<QUALITY_GATE>(); 
        RepositoryFactory<QUALITY_GATE_FORCED_RELEASE> QUALITY_GATE_FORCED_RELEASERepository = new RepositoryFactory<QUALITY_GATE_FORCED_RELEASE>();
        RepositoryFactory<WORK_CENTER> WORK_CENTERRepository = new RepositoryFactory<WORK_CENTER>();
        RepositoryFactory<STATION> STATIONRepository = new RepositoryFactory<STATION>();
        RepositoryFactory<P_TIGHTENING> P_TIGHTENINGRepository = new RepositoryFactory<P_TIGHTENING>();
        #endregion 
        #region 服务器通讯常量
        public string localIP = "";
        private bool EquipCommunicationState = false;//设备通讯状态
        private string HeartSignalAddress = "";// 心跳信号地址
        private bool ServerCommunicationState = false;//服务器通讯状态
        private static string ServerIP = "10.4.1.245";//服务器的IP地址
        bool judge = true;
        #endregion
        #endregion

        #region 1 窗体显示，流程运行
        #region  加载窗体组件，服务器连接测试
        public OffLine()
        {
            InitializeComponent();
            titleLib.Text = pro_line_name_C + wc_code_C + "下线工位";//标头显示
        }
        #endregion

        #region  界面信息展示，自动流程运行
        private void OffLine_Load(object sender, EventArgs e)
        {
            SetDatagridview();
            //服务器通讯timer启动
            ShowServerConnectionState();
            serverconnection_trm.Enabled = true;
            #region 2.2.1 通过数据字典字典获取服务器IP端口号
            Base_DataDictionary L_Info = Base_DataRepository.Repository().FindEntity("FULLNAME", "服务器IP端口");
            List<Base_DataDictionaryDetail> L_Info_De = Base_DataDetailRepository.Repository().FindList("DATADICTIONARYID", L_Info.DataDictionaryId);
            foreach (var item in L_Info_De)
            {
                if (item.FullName == "主IP端口")
                {
                    SystemLog.IPCode = item.Code;//获取服务器IP端口号存入静态变量
                }
            }
            #endregion
            //登录人员显示
            staff_code_lbl.Text = SystemLog.UserCode.ToString();//系统登录员工编号
            staff_name_lbl.Text = SystemLog.UserName.ToString();//系统登录员工姓名
            confirm_btn.Enabled = false;
            DateTime dt = DateTime.Now;
            //DateTime dt = ServerTime.Now;
            string week = dt.DayOfWeek.ToString();
            SetLableText(showweek_lbl, week);
            switch (week)
            {
                case "Monday":
                    week = "星期一";
                    break;
                case "Tuesday":
                    week = "星期二";
                    break;
                case "Wednesday":
                    week = "星期三";
                    break;
                case "Thursday":
                    week = "星期四";
                    break;
                case "Friday":
                    week = "星期五";
                    break;
                case "Saturday":
                    week = "星期六";
                    break;
                case "Sunday":
                    week = "星期日";
                    break;
                default:
                    break;

            }
            SetLableText(showweek_lbl, week);
            #region 图标显示
            //string pages = WebApiHttp.HttpGet("http://10.4.1.245:9999/API/WebApiPage/GetPageInfo");//WebApi获取图标文件包
            //CS_PageInfo pageinfo = pages.ToJson<CS_PageInfo>();//转换为实体
            //Stream ms_system_pic = new MemoryStream(pageinfo.SystemPicture);//获取相应图片信息
            //SetPicture1(system_pic, ms_system_pic);
            //Stream ms_lab_pic = new MemoryStream(pageinfo.LaboratoryPicture);
            //SetPicture1(lab_pic, ms_lab_pic);
            #endregion

            #region 为Lable赋值
            SetLableText(LaboratoryName, ServerDictionary.LaboratoryName);
            SetLableText(ProgramName_CN_CS, ServerDictionary.ProgramName_CN_CS);
            SetLableText(ProgramName_CN_EN, ServerDictionary.ProgramName_CN_EN);
            #endregion

            #region PLC跳变读取信息
            Task getAddress_task = Task.Run(new Action(ReFreshAddress));//刷新地址
            while (true)
            {
                if (ControlAddressList != null)
                {
                    break;
                }
            }
            Task.WaitAll(getAddress_task);
            Task.Run(new Action(() =>//开启一个任务
            {
                OPCHelper.OPC_Connecion(ControlAddressList, Change);
            }
            ));
            Task.Run(new Action(ReFreshAddressByTiming));
            #endregion
        }
        #endregion

        #region 初始加载Datagridview
        private void SetDatagridview()
        {
            #region 01 声明列
            dgvQuaInfo.Columns.Add("q_num", "序号");
            dgvQuaInfo.Columns.Add("PRODUCT_BORN_CODE", "产品出生证");
            dgvQuaInfo.Columns.Add("DCS_KEY", "DCS主键");
            dgvQuaInfo.Columns.Add("DCS_CODE", "DCS编号");
            dgvQuaInfo.Columns.Add("DCS_NAME", "DCS名称");
            dgvQuaInfo.Columns.Add("PARM_KEY", "检测项主键");
            dgvQuaInfo.Columns.Add("PARM_TYPE", "检测类型");
            dgvQuaInfo.Columns.Add("PARM_NAME", "检测项名称");
            dgvQuaInfo.Columns.Add("UPPER_CONTROL", "上限值");
            dgvQuaInfo.Columns.Add("TARGET", "目标值");
            dgvQuaInfo.Columns.Add("LOWER_CONTROL", "下限值");
            dgvQuaInfo.Columns.Add("CLT_VALUE", "检测值");
            dgvQuaInfo.Columns.Add("CLT_DATE", "检测时间");
            dgvQuaInfo.Columns.Add("IS_CLT", "是否检测");
            dgvQuaInfo.Columns.Add("CLT_RESULT", "检测结果");
            dgvQuaInfo.Columns.Add("RESERVE", "备注");
            dgvSafeInfo.Columns.Add("Q_NUM", "序号");
            dgvSafeInfo.Columns.Add("STATION_KEY", "工位KEY");
            dgvSafeInfo.Columns.Add("STATION_CODE", "工位编号");
            dgvSafeInfo.Columns.Add("STATION_NAME", "工位名称");
            dgvSafeInfo.Columns.Add("PART_KEY", "物料主键");
            dgvSafeInfo.Columns.Add("PART_CODE", "物料编号");
            dgvSafeInfo.Columns.Add("PART_NAME", "物料名称");
            dgvSafeInfo.Columns.Add("PRODUCT_BORN_CODE", "产品出生证");
            dgvSafeInfo.Columns.Add("PART_BARCODE", "物料条码");
            dgvSafeInfo.Columns.Add("SUPPLIER_KEY", "供应商主键");
            dgvSafeInfo.Columns.Add("SUPPLIER_CODE", "供应商编号");
            dgvSafeInfo.Columns.Add("SUPPLIER_NAME", "供应商名称");
            dgvSafeInfo.Columns.Add("ASSMBLY_TIME", "检测时间");
            dgvSafeInfo.Columns.Add("S_CLT_RESULT", "检测结果");
            dgvSafeInfo.Columns.Add("RESERVE", "备注");
            dgvTightInfo.Columns.Add("Q_NUM", "序号");
            dgvTightInfo.Columns.Add("STATION_KEY", "工位KEY");
            dgvTightInfo.Columns.Add("STATION_CODE", "工位编号");
            dgvTightInfo.Columns.Add("STATION_NAME", "工位名称");
            dgvTightInfo.Columns.Add("UPPER_CONTROL_TORQUE", "扭矩上限值");
            dgvTightInfo.Columns.Add("LOWER_CONTROL_TORQUE", "扭矩下限值");
            dgvTightInfo.Columns.Add("CHECK_VALUE_TORQUE", "扭矩检测值");
            dgvTightInfo.Columns.Add("UPPER_CONTROL_ANGLE", "转角上限值");
            dgvTightInfo.Columns.Add("LOWER_CONTROL_ANGLE", "转角下限值");
            dgvTightInfo.Columns.Add("CHECK_VALUE_ANGLE", "转角检测值");
            dgvTightInfo.Columns.Add("CHECK_TIME", "检测时间");
            dgvTightInfo.Columns.Add("IS_QUALIFIED", "检测结果");
            dgvTightInfo.Columns.Add("RESERVE1", "备注");
            #endregion

            #region 02 绑定数据源设置
            dgvQuaInfo.Columns["PRODUCT_BORN_CODE"].DataPropertyName = "PRODUCT_BORN_CODE";
            dgvQuaInfo.Columns["DCS_KEY"].DataPropertyName = "DCS_KEY";
            dgvQuaInfo.Columns["DCS_CODE"].DataPropertyName = "DCS_CODE";
            dgvQuaInfo.Columns["DCS_NAME"].DataPropertyName = "DCS_NAME";
            dgvQuaInfo.Columns["PARM_KEY"].DataPropertyName = "PARM_KEY";
            dgvQuaInfo.Columns["PARM_NAME"].DataPropertyName = "PARM_NAME";
            dgvQuaInfo.Columns["PARM_TYPE"].DataPropertyName = "PARM_TYPE";
            dgvQuaInfo.Columns["UPPER_CONTROL"].DataPropertyName = "UPPER_CONTROL";
            dgvQuaInfo.Columns["TARGET"].DataPropertyName = "TARGET";
            dgvQuaInfo.Columns["LOWER_CONTROL"].DataPropertyName = "LOWER_CONTROL";
            dgvQuaInfo.Columns["CLT_VALUE"].DataPropertyName = "CHECK_VALUE";
            dgvQuaInfo.Columns["CLT_DATE"].DataPropertyName = "CHECK_TIME";
            dgvSafeInfo.Columns["STATION_KEY"].DataPropertyName = "STATION_KEY";
            dgvSafeInfo.Columns["STATION_CODE"].DataPropertyName = "STATION_CODE";
            dgvSafeInfo.Columns["STATION_NAME"].DataPropertyName = "STATION_NAME";
            dgvSafeInfo.Columns["PART_KEY"].DataPropertyName = "PART_KEY";
            dgvSafeInfo.Columns["PART_CODE"].DataPropertyName = "PART_CODE";
            dgvSafeInfo.Columns["PART_NAME"].DataPropertyName = "PART_NAME";
            dgvSafeInfo.Columns["PRODUCT_BORN_CODE"].DataPropertyName = "PRODUCT_BORN_CODE";
            dgvSafeInfo.Columns["PART_BARCODE"].DataPropertyName = "PART_BARCODE";
            dgvSafeInfo.Columns["SUPPLIER_KEY"].DataPropertyName = "SUPPLIER_KEY";
            dgvSafeInfo.Columns["SUPPLIER_CODE"].DataPropertyName = "SUPPLIER_CODE";
            dgvSafeInfo.Columns["SUPPLIER_NAME"].DataPropertyName = "SUPPLIER_NAME";
            dgvSafeInfo.Columns["ASSMBLY_TIME"].DataPropertyName = "ASSMBLY_TIME";
            dgvTightInfo.Columns["STATION_KEY"].DataPropertyName = "STATION_KEY";
            dgvTightInfo.Columns["STATION_CODE"].DataPropertyName = "STATION_CODE";
            dgvTightInfo.Columns["STATION_NAME"].DataPropertyName = "STATION_NAME";
            dgvTightInfo.Columns["UPPER_CONTROL_TORQUE"].DataPropertyName = "upper_control_torque";
            dgvTightInfo.Columns["LOWER_CONTROL_TORQUE"].DataPropertyName = "lower_control_torque";
            dgvTightInfo.Columns["CHECK_VALUE_TORQUE"].DataPropertyName = "check_value_torque";
            dgvTightInfo.Columns["UPPER_CONTROL_ANGLE"].DataPropertyName = "upper_control_angle";
            dgvTightInfo.Columns["LOWER_CONTROL_ANGLE"].DataPropertyName = "lower_value_angle";
            dgvTightInfo.Columns["CHECK_VALUE_ANGLE"].DataPropertyName = "check_value_angle";
            dgvTightInfo.Columns["CHECK_TIME"].DataPropertyName = "Check_Time";
            dgvTightInfo.Columns["IS_QUALIFIED"].DataPropertyName = "is_qualified";
            dgvTightInfo.Columns["RESERVE1"].DataPropertyName = "RESERVE01";
            #endregion

            #region 03 列可见性
            dgvQuaInfo.Columns["PRODUCT_BORN_CODE"].Visible = false;
            dgvQuaInfo.Columns["DCS_KEY"].Visible = false;
            dgvQuaInfo.Columns["DCS_CODE"].Visible = false;
            dgvQuaInfo.Columns["PARM_KEY"].Visible = false;
            dgvQuaInfo.Columns["PARM_TYPE"].Visible = false;
            dgvSafeInfo.Columns["PART_KEY"].Visible = false;
            dgvSafeInfo.Columns["STATION_KEY"].Visible = false;
            dgvSafeInfo.Columns["STATION_NAME"].Visible = false;
            dgvSafeInfo.Columns["PRODUCT_BORN_CODE"].Visible = false;
            dgvSafeInfo.Columns["PART_BARCODE"].Visible = false;
            dgvSafeInfo.Columns["SUPPLIER_KEY"].Visible = false;
            dgvSafeInfo.Columns["SUPPLIER_CODE"].Visible = false;
            dgvSafeInfo.Columns["SUPPLIER_NAME"].Visible = false;
            dgvTightInfo.Columns["STATION_KEY"].Visible = false;
            dgvTightInfo.Columns["STATION_NAME"].Visible = false;
            #endregion

            #region 界面格式
            dgvQuaInfo.DefaultCellStyle.Font = new Font("华文细黑", 12F);
            dgvQuaInfo.AlternatingRowsDefaultCellStyle.Font = new Font("华文细黑", 12F);
            dgvQuaInfo.RowsDefaultCellStyle.Font = new Font("华文细黑", 12F);
            dgvQuaInfo.ColumnHeadersDefaultCellStyle.Font = new Font("华文细黑", 12F);
            dgvSafeInfo.DefaultCellStyle.Font = new Font("华文细黑", 12F);
            dgvSafeInfo.RowsDefaultCellStyle.Font = new Font("华文细黑", 12F);
            dgvSafeInfo.AlternatingRowsDefaultCellStyle.Font = new Font("华文细黑", 12F);
            dgvSafeInfo.ColumnHeadersDefaultCellStyle.Font = new Font("华文细黑", 12F);
            dgvTightInfo.DefaultCellStyle.Font = new Font("华文细黑", 12F);
            dgvTightInfo.RowsDefaultCellStyle.Font = new Font("华文细黑", 12F);
            dgvTightInfo.AlternatingRowsDefaultCellStyle.Font = new Font("华文细黑", 12F);
            dgvTightInfo.ColumnHeadersDefaultCellStyle.Font = new Font("华文细黑", 12F);
            dgvQuaInfo.DefaultCellStyle.ForeColor = Color.Black;
            dgvQuaInfo.Columns["Q_NUM"].Width = 40;
            dgvQuaInfo.Columns["DCS_NAME"].Width = 75;
            dgvQuaInfo.Columns["PARM_NAME"].Width = 75;
            dgvQuaInfo.Columns["UPPER_CONTROL"].Width = 50;
            dgvQuaInfo.Columns["TARGET"].Width = 50;
            dgvQuaInfo.Columns["LOWER_CONTROL"].Width = 50;
            dgvQuaInfo.Columns["CLT_VALUE"].Width = 50;
            dgvQuaInfo.Columns["CLT_DATE"].Width = 100;
            dgvQuaInfo.Columns["IS_CLT"].Width = 50;
            dgvQuaInfo.Columns["CLT_RESULT"].Width = 60;
            dgvQuaInfo.Columns["RESERVE"].Width = 50;
            dgvSafeInfo.DefaultCellStyle.ForeColor = Color.Black;
            dgvSafeInfo.Columns["Q_NUM"].Width = 35;
            dgvSafeInfo.Columns["STATION_CODE"].Width = 60;
            dgvSafeInfo.Columns["PART_CODE"].Width = 80;
            dgvSafeInfo.Columns["PART_NAME"].Width = 120;
            dgvSafeInfo.Columns["ASSMBLY_TIME"].Width = 90;
            dgvSafeInfo.Columns["S_CLT_RESULT"].Width = 60;
            dgvSafeInfo.Columns["RESERVE"].Width = 50;
            dgvTightInfo.DefaultCellStyle.ForeColor = Color.Black;
            dgvTightInfo.Columns["Q_NUM"].Width = 35;
            dgvTightInfo.Columns["STATION_CODE"].Width = 60;
            dgvTightInfo.Columns["UPPER_CONTROL_TORQUE"].Width = 45;
            dgvTightInfo.Columns["LOWER_CONTROL_TORQUE"].Width = 45;
            dgvTightInfo.Columns["CHECK_VALUE_TORQUE"].Width = 45;
            dgvTightInfo.Columns["UPPER_CONTROL_ANGLE"].Width = 45;
            dgvTightInfo.Columns["LOWER_CONTROL_ANGLE"].Width = 45;
            dgvTightInfo.Columns["CHECK_VALUE_ANGLE"].Width = 45;
            dgvTightInfo.Columns["CHECK_TIME"].Width = 80;
            dgvTightInfo.Columns["IS_QUALIFIED"].Width = 60;
            dgvTightInfo.Columns["RESERVE1"].Width = 50;
            #endregion
        }
        #endregion

        #region 刷新地址信息
        /// <summary>
        /// 定时刷新数据库地址信息
        /// </summary>
        private void ReFreshAddress()
        {
            try
            {
                localIP = getIP();
                if (localIP != null)
                {
                    wc = WORK_CENTERRepository.Repository().FindList().Where(s => s.IP == localIP).ToList();//根据ip找到对应的工作中心
                    if (wc.Count > 0)
                    {
                        station = STATIONRepository.Repository().FindList().Where(s => s.WORK_CENTER_KEY == wc[0].WORK_CENTER_KEY).ToList();//根据工作中心找到对应的工位
                        if(station.Count>0)
                        {
                            Stopper stopentity = StopperRepositoryFactory.Repository().FindEntity("station_key", station[0].STATION_KEY);//根据工位信息找到对应的停止器信息
                            if (stopentity != null)
                            {
                                ControlAddressList = Control_AddressRepositoryFactory.Repository().FindList().Where(s => s.POSITION_KEY == stopentity.Stopper_Key).ToList();//停止器的控制地址
                            }
                            else
                            {
                                Log.GetInstance.WriteLog("未找到该停止器，请在上层检查停止器与工位配置关系!");
                            }
                        }
                        else
                        {
                            Log.GetInstance.WriteLog("未找到该工位，请在上层检查工位与工作中心配置关系!");
                        }
                    }
                    else
                    {
                        Log.GetInstance.WriteLog("未找到该工作中心，请在上层检查ip对应关系!");
                    }
                }
                else
                {
                    Log.GetInstance.WriteLog("未获取到本机的IP!");
                }
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog(ex.Message);
            }
        }
        private void ReFreshAddressByTiming()
        {
            while (true)
            {
                try
                {
                    ReFreshAddress();
                    int d = System.Convert.ToInt16(ServerDictionary.timeValue);
                    if (d <= 0)
                    {
                        break;
                    }
                    Thread.Sleep(1000 * 60 * d);
                }
                catch (Exception ex)
                {
                    Log.GetInstance.WriteLog(ex.Message);
                }
            }
        }
        #endregion

        #region 托盘到达后从plc读取产品出生证后及进行校验
        public void Change(string subscriptionHandle, string opcname, string opcvalue)
        {
            try
            {  //根据地址的类型，判断执行哪项逻辑操作
                OpcName = opcname;
                string[] values = OPCHelper.SynOpcItemsRead(opcname.Split(';'));//读取地址的value,即产品到位信号是否从0-1,即此时读到的信息为True时读取产品出生证信息
                if (values[0] == "True")
                {
                    GetBornCode(OpcName);
                }
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog(ex.Message);
            }
        }
        public void GetBornCode(string opcname)
        {

            CONTROL_ADDRESS OpcItem = Control_AddressRepositoryFactory.Repository().FindEntity("Address_Path", opcname);
            if (OpcItem != null)
            {
                if (OpcItem.ADDRESS_TYPE != null)
                {
                    int c = (int)AddressCategory.pallet_arrive;//跳变信号类型
                    if (OpcItem.ADDRESS_TYPE == c.ToString())
                    {
                        #region rfid读取产品出生证
                        //bool ok = BalluffRfidHelper.OpenRfidCom(scanport);//打开串口
                        //if (ok)
                        //{
                        //    string born_code = BalluffRfidHelper.ReadRfid(55, 40, scanport, 3).Trim();
                        //    if (born_code != null)
                        //    {
                        //        GetProductBornCode(born_code);
                        //    }
                        //    else
                        //    {
                        //        Log.GetInstance.WriteLog("读取产品出生证失败！");
                        //    }
                        //}
                        //else
                        //{
                        //    MyMsgBox.Show("串口打开失败！请检查配置文件和硬件连接！", "提醒", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information);
                        //    return;
                        //}
                        //BalluffRfidHelper.CloseRfidCom(scanport);//关闭串口
                        #endregion
                        #region plc读取产品出生证并显示在txt中
                        pro_born_address = Control_AddressRepositoryFactory.Repository().FindList().Where(s => s.POSITION_KEY == OpcItem.POSITION_KEY && s.ADDRESS_TYPE == ((int)AddressCategory.pro_born_code).ToString()).FirstOrDefault();//找到停止器对应的产品出生证地址
                        if (pro_born_address != null)
                        {
                            string[] values = OPCHelper.SynOpcItemsRead(pro_born_address.ADDRESS_PATH.Split(';'));//读取地址的value,即产品出生证信息
                            if (plan_product_data == null || (plan_product_data != null && plan_product_data.PRODUCT_BORN_CODE != values[0])) ;//如果读取的产品出生证与上一个产品出生证不一样时处理信息
                            GetProductBornCode(values[0]);
                        }
                        else
                        {

                        }
                        #endregion
                    }
                }
                else
                {
                    Log.GetInstance.WriteLog("地址类型为空");
                }

            }
            else
            {
                Log.GetInstance.WriteLog("没有找到跳变地址对应的地址信息");
            }
        }
        public void GetProductBornCode(string product_born_code)
        {
            ClearForm();
            SetText(productborncode_txt, product_born_code);
            try
            {
                SetLabel(palletstate_lbl, true);
                Boolean isValid = CheckValid(productborncode_txt.Text);
                if (isValid == true)
                {
                    Boolean result = CheckOfflineFirst(productborncode_txt.Text);
                    if (result == true)
                    {
                        SetLabel(barcodestate_lbl, true);
                        SetVisible(arrowpicture1_pic, true);
                        SetVisible(arrowpicture2_pic, true);
                        Process();
                    }
                    else
                    {
                        MyMsgBox.Show("当前产品已经下线，请重新输入！", "提醒", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information);
                        return;
                    }
                }
                else
                {
                    SetLabel(barcodestate_lbl, false);
                    MyMsgBox.Show("当前产品出生证无效！请重新输入！", "提醒", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                MyMsgBox.Show(ex.Message);
            }
        }
        #endregion
        #endregion

        #region 2 手动确定
        private void confirm_btn_Click(object sender, EventArgs e)
        {
            //0.产品出生证是否有效：有效，0.5；无效，提示，return;
            //0.5产品是否已上线，未上线，提示；上线，1；
            //1.产品是否下线：下线，提示,return；未下线，2；
            //2.清空窗体
            //3.流程开始
            try
            {
                if (productborncode_txt.Text.Trim() != null)
                {
                    SetLabel(palletstate_lbl, true);
                    SetDgv(dgvQuaInfo);
                    SetDgv(dgvSafeInfo);
                    SetDgv(dgvTightInfo);
                    Boolean isValid = CheckValid(productborncode_txt.Text);
                    if (isValid == true)
                    {
                        Boolean result = CheckOfflineFirst(productborncode_txt.Text);
                        if (result == true)
                        {
                            SetLabel(barcodestate_lbl, true);
                            SetVisible(arrowpicture1_pic, true);
                            Process();
                        }
                        else
                        {
                            SetLabel(barcodestate_lbl, false);
                            SetVisible(arrowpicture1_pic, true);
                            MyMsgBox.Show("当前产品已经下线，请重新输入！", "提醒", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        SetLabel(barcodestate_lbl, false);
                        MyMsgBox.Show("当前产品出生证无效！请重新输入！", "提醒", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information);
                        return;
                    }
                }
                else { MyMsgBox.Show("请输入产品出生证！", "提醒", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information); }
            }
            catch(Exception ex)
            {
                Log.GetInstance.WriteLog(ex.Message);
            }
        }
        #endregion

        #region 3 手自动切换
        private void inputmodel_btn_Click_1(object sender, EventArgs e)
        {
            if (productborncode_txt.ReadOnly == true)
            {
                ClearForm();
                productborncode_txt.ReadOnly = false;
                productborncode_txt.Focus();
                confirm_btn.Enabled = true;
                inputmodel_btn.Text = "自动读取";
                //ScannerHelper.CloseCom(scanport);
                productborncode_txt.Text = "3000000D6211-H10000000410";//测试用
            }
            else
            {
                ClearForm();
                productborncode_txt.ReadOnly = true;
                productborncode_txt.Text = "请扫描产品出生证";
                confirm_btn.Enabled = false;
                //ScannerHelper.OpenCom(scanport);
                inputmodel_btn.Text = "手动输入";
            }
        }
        #endregion

        #region 4 基类方法
        private Boolean CheckValid(string code)
        {
            Boolean result = true;
            List<P_ASSEMBLE_PRODUCT_STATE> list = new List<P_ASSEMBLE_PRODUCT_STATE>();
            list = P_ASSEMBLE_PRODUCT_STATERepository.Repository().FindList().Where(s => s.PRODUCT_BORN_CODE == code).ToList();
            if (list.Count > 0)
            {
                result = list[0].MES_PLAN_CODE == null ? false : true;
                return result;
            }
            else
            {
                Log.GetInstance.WriteLog("未找到该产品出生证！");
                return false;
            }
        }
        private delegate Boolean CheckOfflineRepeatDelegate(string code);
        private Boolean CheckOfflineFirst(string code)
        {
            Boolean result = true;
            List<P_ASSEMBLE_PRODUCT_STATE> list = new List<P_ASSEMBLE_PRODUCT_STATE>();
            list = P_ASSEMBLE_PRODUCT_STATERepository.Repository().FindList().Where(s => s.PRODUCT_BORN_CODE == code).ToList();
            if (list.Count > 0)
            {
                result = list[0].ASSEMBLE_OFFLINE_TIME == null ? true : false;
                return result;
            }
            else
            {
                Log.GetInstance.WriteLog("未找到该产品出生证！");
                return false;
            }
        }
        private delegate void SetFocusDelegate(Control control);
        private void SetFocus(Control control)
        {
            //string ID = Thread.CurrentThread.ManagedThreadId.ToString();//获取线程ID，可以进行跨线程调试
            if (control.InvokeRequired) Invoke(new SetFocusDelegate(SetFocus), control);
            else control.Focus();
        }
        public static string getIP()
        {
            //获取本地的IP地址
            string hostinfo = Dns.GetHostName();
            System.Net.IPAddress addr;
            addr = new System.Net.IPAddress(Dns.GetHostByName(hostinfo).AddressList[0].Address);
            return addr.ToString();
        }
        private delegate void SetDgvDelegate(DataGridView dgv);
        private void SetDgv(DataGridView dgv)
        {
            if (dgv.InvokeRequired) Invoke(new SetDgvDelegate(SetDgv), dgv);
            else dgv.Rows.Clear();
        }
        /// <summary>
        /// 使用委托为TextBox赋值
        /// </summary>
        /// <param name="txt">TextBox控件名</param>
        /// <param name="st">要赋值的字符串</param>
        private delegate void SetTextDelegate(TextBox txt, string st);
        private void SetText(TextBox txt, string st)
        {
            if (txt.InvokeRequired) Invoke(new SetTextDelegate(SetText), txt, st);
            else txt.Text = st;
        }
        /// <summary>
        /// 使用委托为Button赋值
        /// </summary>
        /// <param name="btn">Button</param>
        /// <param name="st">是否可见</param>
        private delegate void SetButtonDelegate(Button btn, bool st);
        private void SetButton(Button txt, bool st)
        {
            if (txt.InvokeRequired) Invoke(new SetButtonDelegate(SetButton), txt, st);
            else txt.Visible = st;
        }
        /// <summary>
        /// 使用委托为Label赋值
        /// </summary>
        /// <param name="pb">Label控件名</param>
        /// <param name="st">要赋值的字符串</param>
        private delegate void SetLableTextDelegate(Label pb, string st);
        private void SetLableText(Label lbl, string st)
        {
            if (lbl.InvokeRequired) Invoke(new SetLableTextDelegate(SetLableText), lbl, st);
            else lbl.Text = st;
        }
        private delegate void SetVisibleDelegate(Control control, bool bools);
        private void SetVisible(Control control, bool bools)
        {
            //string ID = Thread.CurrentThread.ManagedThreadId.ToString();//获取线程ID，可以进行跨线程调试
            if (control.InvokeRequired) Invoke(new SetVisibleDelegate(SetVisible), control, bools);
            else control.Visible = bools;
        }
        private delegate void SetPictureDelegate(PictureBox control, Stream str);
        private void SetPicture1(PictureBox control, Stream str)
        {
            if (control.InvokeRequired) Invoke(new SetPictureDelegate(SetPicture1), control, str);
            else control.Image = System.Drawing.Image.FromStream(str);
        }
        private delegate void SetLblDelegate(Label lbl, Boolean result);
        private void SetLabel(Label lbl, Boolean result)
        {
            if (lbl.InvokeRequired) Invoke(new SetLblDelegate(SetLabel), lbl, result);
            else
            {
                if (result == true)
                {
                    lbl.BackColor = Color.FromArgb(57, 204, 36);
                }
                else
                {
                    lbl.BackColor = Color.Red;
                }
            }
        }
        private void recordtime_tmr_Tick(object sender, EventArgs e)
        {
            //DateTime dt = ServerTime.Now;
            DateTime dt = DateTime.Now;
            string datetime = "时间" + ':' + dt.Year.ToString().Trim() + "年" + dt.Month.ToString("00").Trim() + "月" + dt.Day.ToString("00").Trim() + "日" + "  " + dt.Hour.ToString("00").Trim() + ":" + dt.Minute.ToString("00").Trim() + ":" + dt.Second.ToString("00").Trim();
            SetLableText(showtime_lbl, datetime);
        }
        private void pulseButton1_Click(object sender, EventArgs e)
        {
            if (OpcName != null)
            {
                GetBornCode(OpcName);
            }
        }
        #endregion

        #region 5 服务器通讯测试


        private delegate void ShowWorkstationServerStateDelegate();
        private void ShowServerConnectionState()
        {
            if (this.InvokeRequired)
            {
                ShowWorkstationServerStateDelegate d = new ShowWorkstationServerStateDelegate(ShowServerConnectionState);
                this.Invoke(d, new object[] { });
            }
            else
            {
                try
                {

                    Ping pingSender = new Ping();
                    PingOptions options = new PingOptions();
                    options.DontFragment = true;
                    string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                    byte[] buffer = Encoding.ASCII.GetBytes(data);
                    int timeout = 120;
                    //测试网络连接：目标计算机为192.168.1.1(可以换成你所需要的目标地址） 
                    //如果网络连接成功，PING就应该有返回；否则，网络连接有问题 
                    PingReply reply = pingSender.Send(ServerIP, timeout, buffer, options);
                    //System.Net.NetworkInformation.Ping p1 = new System.Net.NetworkInformation.Ping();
                    //System.Net.NetworkInformation.PingReply reply = p1.Send(ServerIP);
                    if (reply.Status == IPStatus.Success)
                    {

                        ServerCommunicationState = true;
                        ShowServerConnection(true);
                        //if (!listenEquipConnection.IsAlive)
                        //{
                        //    this.listenEquipConnection = new Thread(PLCToMES);
                        //    listenEquipConnection.Start();
                        //}
                    }
                    else
                    {
                        ServerCommunicationState = false;
                        ShowServerConnection(false);
                        //if (listenEquipConnection.IsAlive)
                        //{
                        //    listenEquipConnection.Abort();
                        //}
                    }
                }
                catch (Exception ex)
                {
                    //if (listenEquipConnection.IsAlive)
                    //{
                    //    listenEquipConnection.Abort();
                    //}
                    ServerCommunicationState = false;
                    ShowServerConnection(false);
                }
            }
        }
        private void serverconnection_trm_Tick(object sender, EventArgs e)
        {
            ShowServerConnectionState();
        }
        public void ShowServerConnection(bool state)
        {
            if (state)
            {
                //SetLblColors(serverconnectionstate_lbl, Color.ForestGreen);
                pulseButton2.Set_ColorBottom(Color.ForestGreen);
                pulseButton2.Set_ColorTop(Color.ForestGreen);
                pulseButton2.Set_ForeColor(Color.ForestGreen);
                pulseButton2.Set_PulseColor(Color.ForestGreen);

            }
            else
            {
                //SetLblColors(serverconnectionstate_lbl, Color.Red);
                pulseButton2.Set_ColorBottom(Color.Red);
                pulseButton2.Set_ColorTop(Color.Red);
                pulseButton2.Set_ForeColor(Color.Red);
                pulseButton2.Set_PulseColor(Color.Red);
            }

        }

        #endregion

        #region 6 清空窗体
        private void ClearForm()
        {
            SetText(plancode_txt, string.Empty);
            SetText(productcode_txt, string.Empty);
            SetText(productbatchno_txt, string.Empty);
            SetText(productborncode_txt, string.Empty);
            SetFocus(productborncode_txt);
            SetDgv(dgvQuaInfo);
            SetDgv(dgvSafeInfo);
            SetDgv(dgvTightInfo);
            palletstate_lbl.BackColor = Color.Gainsboro;
            barcodestate_lbl.BackColor = Color.Gainsboro;
            lblQua.BackColor = Color.Gainsboro;
            lblSafe.BackColor = Color.Gainsboro;
            lblTight.BackColor = Color.Gainsboro;
            planstate_lbl.BackColor = Color.Gainsboro;
            offlinestate_lbl.BackColor = Color.Gainsboro;
        }
        #endregion

        #region 7 校验数据
        private void Process()
        {
            List<P_ASSEMBLE_PRODUCT_STATE> list = new List<P_ASSEMBLE_PRODUCT_STATE>();
            try
            {
                list = P_ASSEMBLE_PRODUCT_STATERepository.Repository().FindList().Where(s => s.PRODUCT_BORN_CODE == productborncode_txt.Text).ToList();
                if (list.Count == 0)
                {
                    MyMsgBox.Show("当前产品出生证无关联信息，请重新输入！", "提醒", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information);
                    return;
                }
                else
                {
                    SetText(plancode_txt, list[0].MES_PLAN_CODE);
                    SetText(productbatchno_txt, list[0].PRODUCT_SERIAL_NO);
                    SetText(productcode_txt, list[0].PRODUCT_CODE);
                    LoadQuaInfo(list[0]);
                    LoadSafeInfo(list[0]);
                    LoadTightInfo(list[0]);
                    SetLabel(planstate_lbl, (lblQua.BackColor == Color.FromArgb(57, 204, 36)) && (lblSafe.BackColor == Color.FromArgb(57, 204, 36)) && (lblTight.BackColor == Color.FromArgb(57, 204, 36)) ? true : false);
                    SetVisible(arrowpicture5_pic, true);
                    if (planstate_lbl.BackColor == Color.FromArgb(57, 204, 36))
                    {
                        UpdatePlanInfo(list[0], 2);
                        SetLabel(offlinestate_lbl, true);
                        SetVisible(arrowpicture6_pic, true);
                        #region 写入放行信号
                        CONTROL_ADDRESS is_leave_ok = ControlAddressList.FirstOrDefault(s => s.ADDRESS_TYPE == ((int)AddressCategory.is_leaveOK).ToString());
                        if (is_leave_ok != null)
                        {
                            OPCHelper.SynOpcItemsWrite(is_leave_ok.ADDRESS_PATH.Split(';'), "1".Split(';'));//写入允许放行
                        }
                        else
                        {
                            Log.GetInstance.WriteLog("放行失败！");
                        }
                        #endregion
                        btnRelease.Enabled = false;
                    }
                    else
                    {
                        btnRelease.Enabled = true;
                        online_delete.Enabled = true;
                        SetVisible(arrowpicture6_pic, false);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.GetInstance.Equals("数据处理过程异常" + ex.Message);
            }
        }
        #endregion

        #region 8 显示安全件数据
        private delegate void LoadDgvDelegate(DataGridView dgv, P_ASSEMBLE_PRODUCT_STATE paps, List<P_KEY_PART_C> lkpc, List<P_KEY_PART_INFOR> lpkpi);
        public void LoadDgv(DataGridView dgvSafeInfo, P_ASSEMBLE_PRODUCT_STATE paps, List<P_KEY_PART_C> lkpc, List<P_KEY_PART_INFOR> lpkpi)
        {
            if (dgvSafeInfo.InvokeRequired)
            {
                LoadDgvDelegate dd = new LoadDgvDelegate(LoadDgv);
                dgvSafeInfo.Invoke(dd, new object[] { dgvSafeInfo, paps, lkpc, lpkpi });
            }
            else
            {
                for (int row = 0; row < lkpc.Count; row++)
                {
                    dgvSafeInfo.Rows.Add();
                    dgvSafeInfo.Rows[row].Cells["STATION_KEY"].Value = lkpc[row].STATION_KEY;
                    dgvSafeInfo.Rows[row].Cells["STATION_CODE"].Value = lkpc[row].STATION_CODE;
                    dgvSafeInfo.Rows[row].Cells["STATION_NAME"].Value = lkpc[row].STATION_NAME;
                    dgvSafeInfo.Rows[row].Cells["PART_KEY"].Value = lkpc[row].PART_KEY;
                    dgvSafeInfo.Rows[row].Cells["PART_CODE"].Value = lkpc[row].PART_CODE;
                    dgvSafeInfo.Rows[row].Cells["PART_NAME"].Value = lkpc[row].PART_NAME;
                    dgvSafeInfo.Rows[row].Cells["PRODUCT_BORN_CODE"].Value = paps.PRODUCT_BORN_CODE;
                    var pkpi = lpkpi.Where(s => (s.PRODUCT_BORN_CODE == paps.PRODUCT_BORN_CODE) && (s.PART_KEY == lkpc[row].PART_KEY)).FirstOrDefault();
                    dgvSafeInfo.Rows[row].Cells["PART_BARCODE"].Value = pkpi == null ? string.Empty : pkpi.PART_BARCODE;
                    dgvSafeInfo.Rows[row].Cells["SUPPLIER_KEY"].Value = pkpi == null ? string.Empty : pkpi.SUPPLIER_KEY;
                    dgvSafeInfo.Rows[row].Cells["SUPPLIER_CODE"].Value = pkpi == null ? string.Empty : pkpi.SUPPLIER_CODE;
                    dgvSafeInfo.Rows[row].Cells["SUPPLIER_NAME"].Value = pkpi == null ? string.Empty : pkpi.SUPPLIER_NAME;
                    dgvSafeInfo.Rows[row].Cells["ASSMBLY_TIME"].Value = pkpi == null ? string.Empty : pkpi.ASSMBLY_TIME.ToString();
                    //dgvSafeInfo.Rows[row].Cells["s_is_clt"].Value = pkpi == null ? "否" : "是";
                    if (pkpi == null)
                    {
                        dgvSafeInfo.Rows[row].DefaultCellStyle.BackColor = Color.Red;
                    }
                    var pkpil = lpkpi.Where(s => (s.PRODUCT_BORN_CODE == paps.PRODUCT_BORN_CODE) && (s.PART_KEY == lkpc[row].PART_KEY)).ToList();
                    int difference = lkpc[row].QUANTITY - pkpil.Count;
                    if (lkpc[row].QUANTITY > pkpil.Count)
                    {
                        dgvSafeInfo.Rows[row].Cells["S_CLT_RESULT"].Value = "少采集" + difference + "个";
                        dgvSafeInfo.Rows[row].DefaultCellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                        dgvSafeInfo.Rows[row].Cells["S_CLT_RESULT"].Value = "采集合格";
                        dgvSafeInfo.Rows[row].DefaultCellStyle.BackColor = Color.Empty;
                    }
                }
                dgvSafeInfo.Rows[0].Selected = false;
                for (int col = 0; col < dgvSafeInfo.Columns.Count; col++)
                {;
                    dgvSafeInfo.Columns[col].ReadOnly = true;
                }
                for (int row = 0; row < dgvSafeInfo.Rows.Count; row++)
                {
                    List<P_SAFE_ELIMINATION> se = new List<P_SAFE_ELIMINATION>();
                    se = P_SAFE_ELIMINATIONRepository.Repository().FindList().Where(s => s.PRODUCT_BORN_CODE == dgvSafeInfo.Rows[row].Cells["PRODUCT_BORN_CODE"].Value.ToString() && s.STATION_KEY == dgvSafeInfo.Rows[row].Cells["STATION_KEY"].Value.ToString() && s.PART_KEY == dgvSafeInfo.Rows[row].Cells["PART_KEY"].Value.ToString()).ToList();
                    if (se.Count > 0)
                    { dgvSafeInfo.Rows[row].DefaultCellStyle.BackColor = Color.Green; }
                    if (dgvSafeInfo.Rows[row].DefaultCellStyle.BackColor == Color.Red)
                    {
                        SetLabel(lblSafe, false);
                        return;
                    }
                }
            }
        }
        #endregion

        #region 9 显示质量数据
        private delegate void LoadDgvDelegatePc(DataGridView dgv, P_ASSEMBLE_PRODUCT_STATE paps, List<PRODUCT_DCS_PARM_LIST> lpc, List<P_DCS_QK_OP_A2740> lpd);
        public void LoadDgv(DataGridView dgvQuaInfo, P_ASSEMBLE_PRODUCT_STATE paps, List<PRODUCT_DCS_PARM_LIST> lpc, List<P_DCS_QK_OP_A2740> lpd)
        {
            List<DCS> dcs = new List<DCS>();
            List<DCS_PARM> dcs_p = new List<DCS_PARM>();
            if (dgvQuaInfo.InvokeRequired)
            {
                LoadDgvDelegatePc dd = new LoadDgvDelegatePc(LoadDgv);
                dgvQuaInfo.Invoke(dd, new object[] { dgvQuaInfo, paps, lpc, lpd });
            }
            else
            {
                for (int row = 0; row < lpc.Count; row++)
                {
                    dgvQuaInfo.Rows.Add();
                    dgvQuaInfo.Rows[row].Cells["PRODUCT_BORN_CODE"].Value = paps.PRODUCT_BORN_CODE;
                    dgvQuaInfo.Rows[row].Cells["DCS_KEY"].Value = lpc[row].DCS_KEY;
                    dcs = DCSRepository.Repository().FindList().Where(s => s.dcs_key == lpc[row].DCS_KEY).ToList();
                    dgvQuaInfo.Rows[row].Cells["DCS_CODE"].Value = dcs[0].dcs_code;
                    dgvQuaInfo.Rows[row].Cells["DCS_NAME"].Value = dcs[0].dcs_name;
                    dgvQuaInfo.Rows[row].Cells["PARM_KEY"].Value = lpc[row].PARM_KEY;
                    dcs_p = DCS_PARMRepository.Repository().FindList().Where(s => s.PARM_KEY == lpc[row].PARM_KEY).ToList();
                    dgvQuaInfo.Rows[row].Cells["PARM_TYPE"].Value = dcs_p[0].PARM_TYPE;
                    dgvQuaInfo.Rows[row].Cells["PARM_NAME"].Value = dcs_p[0].PARM_NAME;
                    var tempdt = lpd.Where(a => a.DCS_KEY == lpc[row].DCS_KEY && a.PARM_KEY == lpc[row].PARM_KEY).OrderByDescending(a => a.CHECK_TIME).ToList();
                    if (tempdt != null && tempdt.Count > 0 )
                    {
                        dgvQuaInfo.Rows[row].Cells["UPPER_CONTROL"].Value = tempdt[0].UPPER_CONTROL.ToString("F4");
                        dgvQuaInfo.Rows[row].Cells["TARGET"].Value = tempdt[0].TARGET.ToString("F4");
                        dgvQuaInfo.Rows[row].Cells["LOWER_CONTROL"].Value = tempdt[0].LOWER_CONTROL.ToString("F4");
                        dgvQuaInfo.Rows[row].Cells["CLT_VALUE"].Value = tempdt[0].CHECK_VALUE;
                        dgvQuaInfo.Rows[row].Cells["CLT_DATE"].Value = tempdt[0].CHECK_TIME.ToString();
                        dgvQuaInfo.Rows[row].Cells["IS_CLT"].Value= tempdt[row].CHECK_VALUE==null? "无":"是";
                        if (tempdt[0].CHECK_VALUE=="2"&& tempdt[0].CHECK_VALUE=="4")
                        {
                            dgvQuaInfo.Rows[row].Cells["CLT_RESULT"].Value = "合格";
                        }
                        else
                        {
                            dgvQuaInfo.Rows[row].Cells["CLT_RESULT"].Value = "不合格";
                        }
                        if (dgvQuaInfo.Rows[row].Cells["CLT_RESULT"].Value.ToString() == "不合格")
                        {
                            dgvQuaInfo.Rows[row].DefaultCellStyle.BackColor = Color.Red;
                        }
                    }
                    else
                    {
                        dgvQuaInfo.Rows[row].DefaultCellStyle.BackColor = Color.Red;
                        dgvQuaInfo.Rows[row].Cells["CLT_VALUE"].Value = "无";
                        dgvQuaInfo.Rows[row].Cells["CLT_DATE"].Value = "无";
                        dgvQuaInfo.Rows[row].Cells["CLT_RESULT"].Value = "无";
                        dgvQuaInfo.Rows[row].Cells["IS_CLT"].Value = "无";
                    }
                    dgvQuaInfo.Rows[0].Selected = false;
                }
            }
            label6.Visible = true;
            label6.Text = "(应采集" + lpc.Count().ToString() + "个,   实采集" + lpd.Count().ToString() + "个)";
            for (int row = 0; row < dgvQuaInfo.Rows.Count; row++)
            {
                List<P_QUALITY_ELIMINATION> qe = new List<P_QUALITY_ELIMINATION>();
                qe = P_QUALITY_ELIMINATIONRepository.Repository().FindList().Where(s => s.PRODUCT_BORN_CODE == dgvQuaInfo.Rows[row].Cells["PRODUCT_BORN_CODE"].Value.ToString() && s.DCS_KEY == dgvQuaInfo.Rows[row].Cells["DCS_KEY"].Value.ToString() && s.PARM_KEY == dgvQuaInfo.Rows[row].Cells["PARM_KEY"].Value.ToString()).ToList();
                if(qe.Count>0)
                { dgvQuaInfo.Rows[row].DefaultCellStyle.BackColor = Color.Green; }
                if (dgvQuaInfo.Rows[row].DefaultCellStyle.BackColor == Color.Red)
                {
                    SetLabel(lblQua, false);
                    return;
                }
            }
        }
        #endregion

        #region 10 显示拧紧数据
        private delegate void LoadDgvDelegatePtc(DataGridView dgv, P_ASSEMBLE_PRODUCT_STATE paps, List<P_TIGHTENING> lpt);
        public void LoadDgv(DataGridView dgvTightInfo, P_ASSEMBLE_PRODUCT_STATE paps, List<P_TIGHTENING> lptc)
        {
            if (dgvQuaInfo.InvokeRequired)
            {
                LoadDgvDelegatePc dd = new LoadDgvDelegatePc(LoadDgv);
                dgvQuaInfo.Invoke(dd, new object[] { dgvTightInfo, paps, lptc});
            }
            else
            {
                for (int row = 0; row < lptc.Count; row++)
                {
                    dgvTightInfo.Rows.Add();
                    dgvTightInfo.Rows[row].Cells["STATION_KEY"].Value = lptc[row].STATION_KEY;
                    dgvTightInfo.Rows[row].Cells["STATION_CODE"].Value = lptc[row].STATION_CODE;
                    dgvTightInfo.Rows[row].Cells["STATION_NAME"].Value = lptc[row].STATION_NAME;
                    dgvTightInfo.Rows[row].Cells["UPPER_CONTROL_TORQUE"].Value = lptc[row].upper_control_torque;
                    dgvTightInfo.Rows[row].Cells["LOWER_CONTROL_TORQUE"].Value = lptc[row].lower_control_torque;
                    dgvTightInfo.Rows[row].Cells["CHECK_VALUE_TORQUE"].Value = lptc[row].check_value_torque;
                    dgvTightInfo.Rows[row].Cells["UPPER_CONTROL_ANGLE"].Value = lptc[row].upper_control_angle;
                    dgvTightInfo.Rows[row].Cells["LOWER_CONTROL_ANGLE"].Value = lptc[row].lower_value_angle;
                    dgvTightInfo.Rows[row].Cells["CHECK_VALUE_ANGLE"].Value = lptc[row].check_value_angle;
                    dgvTightInfo.Rows[row].Cells["CHECK_TIME"].Value = lptc[row].Check_Time;
                    dgvTightInfo.Rows[row].Cells["IS_QUALIFIED"].Value = lptc[row].is_qualified=="2"|| lptc[row].is_qualified == "4"?"合格":"不合格";
                    if (dgvTightInfo.Rows[row].Cells["IS_QUALIFIED"].Value.ToString()=="合格")
                    {
                        dgvTightInfo.Rows[row].DefaultCellStyle.BackColor = Color.Red;
                    }
                    dgvTightInfo.Rows[0].Selected = false;
                }
            }
            for (int row = 0; row < dgvQuaInfo.Rows.Count; row++)
            {
                if (dgvTightInfo.Rows[row].DefaultCellStyle.BackColor == Color.Red)
                {
                    SetLabel(lblQua, false);
                    return;
                }
            }
        }
        #endregion

        #region 11 加载待采集和实际采集的质量数据
        private void LoadQuaInfo(P_ASSEMBLE_PRODUCT_STATE paps)
        {
            List<PRODUCT_DCS_PARM_LIST> lpc = new List<PRODUCT_DCS_PARM_LIST>();
            List<P_DCS_QK_OP_A2740> lpd = new List<P_DCS_QK_OP_A2740>();
            List<DCS> dcs = new List<DCS>();
            try
            {
                lpc = PRODUCT_DCS_PARM_LISTRepository.Repository().FindList().Where(s => s.PART_CODE == paps.PRODUCT_CODE && s.PRODUCTION_LINE_KEY== paps.PRODUCTION_LINE_KEY).ToList();
                if (lpc.Count > 0)
                {
                    for (int row = 0; row < lpc.Count; row++)
                    {
                        dcs = DCSRepository.Repository().FindList().Where(s => s.dcs_key == lpc[row].DCS_KEY).ToList();
                        string str3 = "select * from P_DCS_" + dcs[0].dcs_name.ToString() + " where DCS_KEY ='" + lpc[row].DCS_KEY + "' and PARM_KEY='" + lpc[row].PARM_KEY + "'and PRODUCT_BORN_CODE='" + paps.PRODUCT_BORN_CODE + "'";
                        DataTable dt3 = DBhelperOracle.OpenTable(str3);
                        if (dt3.Rows.Count > 0)
                        {
                            P_DCS_QK_OP_A2740 pd = EntityHelper<P_DCS_QK_OP_A2740>.GetEntity(dt3);//通用dcs类
                            lpd.Add(pd);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    SetLabel(lblQua, true);
                    SetVisible(arrowpicture2_pic, true);
                    LoadDgv(dgvQuaInfo, paps, lpc, lpd);
                }
                else
                {
                    MyMsgBox.Show("该产品质量检验信息未配置", "提醒", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information);
                    SetLabel(lblQua, false);
                    return;
                }
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog("质量数据处理异常" + ex.Message);
            }
        }
        #endregion

        #region 12 加载待采集和实际采集的安全件信息
        private void LoadSafeInfo(P_ASSEMBLE_PRODUCT_STATE paps)
        {
            try
            {
                SetLabel(lblSafe, true);
                SetVisible(arrowpicture3_pic, true);
                List<P_KEY_PART_C> lkpc = new List<P_KEY_PART_C>();
                lkpc = P_KEY_PART_CRepository.Repository().FindList().Where(s => s.PRODUCT_KEY == paps.PRODUCT_KEY && s.MES_PLAN_KEY == paps.MES_PLAN_KEY).ToList();
                if (lkpc.Count > 0)
                {
                    List<P_KEY_PART_INFOR> lpkpi = new List<P_KEY_PART_INFOR>();
                    lpkpi = P_KEY_PART_INFORRepository.Repository().FindList().Where(s => s.PRODUCT_BORN_CODE == paps.PRODUCT_BORN_CODE).ToList();
                    LoadDgv(dgvSafeInfo, paps, lkpc, lpkpi);
                }
                else
                {
                    MyMsgBox.Show("该产品安全件信息未配置", "提醒", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information);
                    SetLabel(lblSafe, false);
                    return;
                }
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog("安全件数据处理异常" + ex.Message);
            }
        }
        #endregion

        #region 13 加载拧紧数据
        private void LoadTightInfo(P_ASSEMBLE_PRODUCT_STATE paps)
        {
            try
            {
                SetLabel(lblTight, true);
                SetVisible(arrowpicture4_pic, true);
                List<P_TIGHTENING> lptc = new List<P_TIGHTENING>();
                lptc = P_TIGHTENINGRepository.Repository().FindList().Where(s => s.PRODUCT_KEY == paps.PRODUCT_KEY && s.PRODUCT_BORN_CODE == paps.PRODUCT_BORN_CODE).ToList();
                if (lptc.Count > 0)
                {
                    LoadDgv(dgvTightInfo, paps, lptc);
                }
                else
                {
                    MyMsgBox.Show("该产品拧紧质量数据信息未配置", "提醒", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information);
                    SetLabel(lblTight, false);
                    return;
                }
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog("拧紧数据处理异常" + ex.Message);
            }
        }
        #endregion

        #region 14 更新计划和在制品信息
        private void UpdatePlanInfo(P_ASSEMBLE_PRODUCT_STATE paps, int i)
        {

            try
            {
                var A = paps.MES_PLAN_CODE;
                List<P_PLAN> pp = new List<P_PLAN>();
                pp = P_PLANRepository.Repository().FindList().Where(s => s.MES_PLAN_CODE == A).ToList();
                if (pp.Count > 0)
                {
                    pp[0].OFFLINE_NUM += 1;
                    P_PLANRepository.Repository().Update(pp[0]);
                }
                else
                {
                    MyMsgBox.Show("无当前产品计划信息");
                }
                paps.ASSEMBLE_OFFLINE_TIME = DateTime.Now;
                //paps.ASSEMBLE_OFFLINE_TIME = ServerTime.Now;
                paps.IS_OK = i.ToString();
                P_ASSEMBLE_PRODUCT_STATERepository.Repository().Update(paps);
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog("下线数据更新处理异常" + ex.Message);
            }
        }
        #endregion

        #region 15 窗体操作
        private void size_pic_box_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Exit_pic_Box_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 16 datagridview加载行时，行级绘制
        private void dgvSafeInfo_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();
            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers  
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, new Font("华文细黑", 12F), SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void dgvQuaInfo_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();
            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers  
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, new Font("华文细黑", 12F), SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void dgvTightInfo_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();
            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers  
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, new Font("华文细黑", 12F), SystemBrushes.ControlText, headerBounds, centerFormat);
        }
        #endregion

        #region 17 在线销项
        private void online_delete_Click(object sender, EventArgs e)
        {
            try
            {
                Boolean ok = CheckOfflineFirst(productborncode_txt.Text);
                if (ok == true)
                {
                    SetLabel(barcodestate_lbl, true);
                    SetVisible(arrowpicture1_pic, true);
                }
                else
                {
                    MyMsgBox.Show("当前产品已经下线，请重新输入！", "提醒", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information);
                    return;
                }
                if (dgvQuaInfo.SelectedRows.Count == 0 && dgvSafeInfo.SelectedRows.Count == 0)
                {
                    MyMsgBox.Show("未选中销项数据！", "提示信息", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error);
                    return;
                }
                else if (dgvQuaInfo.SelectedRows.Count != 0)
                {
                    DialogResult result = MyMsgBox.Show("是否要销项当前行中单元格的内容?", "提示信息", MyMsgBox.MyButtons.YesNo, MyMsgBox.MyIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        if (dgvQuaInfo.CurrentRow.Cells["CLT_RESULT"].Value.ToString() == "合格")
                        {
                            MyMsgBox.Show("该项数据合格！", "提示信息", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error);
                            dgvQuaInfo.ClearSelection();
                            return;
                        }
                        List<P_QUALITY_ELIMINATION> qe = new List<P_QUALITY_ELIMINATION>();
                        qe = P_QUALITY_ELIMINATIONRepository.Repository().FindList().Where(s => s.PRODUCT_BORN_CODE == dgvQuaInfo.CurrentRow.Cells["PRODUCT_BORN_CODE"].Value.ToString() && s.DCS_KEY == dgvQuaInfo.CurrentRow.Cells["DCS_KEY"].Value.ToString() && s.PARM_KEY == dgvQuaInfo.CurrentRow.Cells["PARM_KEY"].Value.ToString()).ToList();
                        if (qe.Count > 0)
                        {
                            MyMsgBox.Show("该项数据已销项！", "提示信息", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error);
                            dgvQuaInfo.ClearSelection();
                            return;
                        }
                        else
                        {
                            dgvQuaInfo.CurrentRow.DefaultCellStyle.BackColor = Color.Green;
                            addQuaInfo(dgvQuaInfo.CurrentRow);
                            dgvQuaInfo.ClearSelection();
                        }
                    }
                }
                else if (dgvSafeInfo.SelectedRows.Count != 0)
                {
                    DialogResult result = MyMsgBox.Show("是否要销项当前行中单元格的内容?", "提示信息", MyMsgBox.MyButtons.YesNo, MyMsgBox.MyIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        if (dgvSafeInfo.CurrentRow.Cells["S_CLT_RESULT"].Value.ToString() == "采集合格")
                        {
                            MyMsgBox.Show("该项数据合格！", "提示信息", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error);
                            dgvSafeInfo.ClearSelection();
                            return;
                        }
                        List<P_SAFE_ELIMINATION> se = new List<P_SAFE_ELIMINATION>();
                        se = P_SAFE_ELIMINATIONRepository.Repository().FindList().Where(s => s.PRODUCT_BORN_CODE == dgvSafeInfo.CurrentRow.Cells["PRODUCT_BORN_CODE"].Value.ToString() && s.STATION_KEY == dgvSafeInfo.CurrentRow.Cells["STATION_KEY"].Value.ToString() && s.PART_KEY == dgvSafeInfo.CurrentRow.Cells["PART_KEY"].Value.ToString()).ToList();
                        if (se.Count > 0)
                        {
                            MyMsgBox.Show("该项数据已销项！", "提示信息", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error);
                            dgvQuaInfo.ClearSelection();
                            return;
                        }
                        else
                        {
                            dgvSafeInfo.CurrentRow.DefaultCellStyle.BackColor = Color.Green;
                            addSafeInfo(dgvSafeInfo.CurrentRow);
                            dgvSafeInfo.ClearSelection();
                        }
                    }
                }
                dgvQuaInfo.ClearRows();
                dgvSafeInfo.ClearRows();
                Process();
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog(ex.Message);
            }
        }
        #endregion

        #region 18 存储销项数据
        private void addQuaInfo(DataGridViewRow selectedRows)
        {
            DataTable data_source = null;
            data_source = GetDgvToTable(selectedRows);
            P_QUALITY_ELIMINATION QE = EntityHelper<P_QUALITY_ELIMINATION>.GetEntity(data_source);
            QE.Create();
            P_QUALITY_ELIMINATIONRepository.Repository().Insert(QE);

        }
        private void addSafeInfo(DataGridViewRow selectedRows)
        {
            DataTable data_source = null;
            data_source = GetDgvToTable(selectedRows);
            P_SAFE_ELIMINATION SE = EntityHelper<P_SAFE_ELIMINATION>.GetEntity(data_source);
            SE.Create();
            P_SAFE_ELIMINATIONRepository.Repository().Insert(SE);

        }
        private DataTable GetDgvToTable(DataGridViewRow selectedRows)
        {
            DataTable dt = new DataTable();
            for (int count = 0; count < selectedRows.Cells.Count; count++)
            {
                DataColumn dc = new DataColumn(selectedRows.DataGridView.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < selectedRows.Cells.Count; countsub++)
                {
                    dr[countsub] = System.Convert.ToString(selectedRows.DataGridView.Rows[selectedRows.Index].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            
            return dt;
        }
        #endregion

        #region 19 强制放行
        private void btnRelease_Click(object sender, EventArgs e)
        {
            //0 出生证是否合法
            //1.1 合法：执行强制放行逻辑；信息提示；界面刷新
            //2.1 不合法：信息提示；退回
            if (string.IsNullOrEmpty(productborncode_txt.Text))
            {
                MyMsgBox.Show("请输入产品出生证！", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information);
                return;
            }
            else
            {
                P_ASSEMBLE_PRODUCT_STATE paps = P_ASSEMBLE_PRODUCT_STATERepository.Repository().FindList().Where(s => s.PRODUCT_BORN_CODE == productborncode_txt.Text.Trim()).FirstOrDefault();
                if (paps == null)
                {
                    MyMsgBox.Show("系统无该出生证相关记录！", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information);
                    return;
                }
                else
                {
                    Boolean result = CheckOfflineFirst(productborncode_txt.Text);
                    if (result == true)
                    {
                        SetLabel(barcodestate_lbl, true);
                        SetVisible(arrowpicture1_pic, true);
                        DialogResult release = MyMsgBox.Show("是否将该产品以不合格状态下线?", "提示信息", MyMsgBox.MyButtons.YesNo, MyMsgBox.MyIcon.Question);
                        if (release == DialogResult.Yes)
                        {
                            UpdatePlanInfo(paps, 1);
                            InsertReleaseInfor(paps, 1);
                            SetLabel(offlinestate_lbl, true);
                            MyMsgBox.Show("该产品已强制放行！", "提醒", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information);
                            #region 对PLC写入允许放行信号
                            CONTROL_ADDRESS is_leave_ok = ControlAddressList.FirstOrDefault(s => s.ADDRESS_TYPE == ((int)AddressCategory.is_leaveOK).ToString());
                            if (is_leave_ok != null)
                            {
                                OPCHelper.SynOpcItemsWrite(is_leave_ok.ADDRESS_PATH.Split(';'), "1".Split(';'));//写入允许放行
                            }
                            #endregion
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        MyMsgBox.Show("当前产品已经下线，请重新输入！", "提醒", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information);
                        return;
                    }
                }
            }
        }
        #endregion

        #region 20 记录强制放行信息
        private void InsertReleaseInfor(P_ASSEMBLE_PRODUCT_STATE paps, int i)
        {

            try
            {
                QUALITY_GATE_FORCED_RELEASE qgfr = new QUALITY_GATE_FORCED_RELEASE();
                qgfr.PRODUCT_BORN_CODE = paps.PRODUCT_BORN_CODE;
                qgfr.STATION_KEY = station[0].STATION_KEY;//产品下线未过点，工位信息在制品表中未更新
                qgfr.RELEASE_RESULT = i.ToString();
                qgfr.REMARK = "总装下线";
                qgfr.Create();
                QUALITY_GATE_FORCED_RELEASERepository.Repository().Insert(qgfr);
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog("强制下线数据更新处理异常" + ex.Message);
            }
        }
        #endregion

        #region 21 点击panel移动窗口
        Point downPoint;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            downPoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
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
