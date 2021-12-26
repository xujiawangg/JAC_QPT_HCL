using DevExpress.XtraBars.Docking;
using DevExpress.XtraPdfViewer;
using DevExpress.XtraRichEdit;
using EntityHelper;
using HfutIE.Entity;
using HfutIE.Repository;
using HfutIE.Utilities;
using KEY_PART_INFOR;
using log4net;
using Opc.Da;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Card;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Layout;
using Microsoft.VisualBasic.PowerPacks;
using MsgBox;
using HfutIe.ClassStore;
using HfutIE.DataAccess;
using System.Data.SQLite;
using Newtonsoft.Json;
using HfutIe.DataAccess.Common;
using System.IO.Ports;
using System.Web;
using System.Xml;
using HfutIe.Online;

namespace HfutIe
{
    public partial class JAC_KEYPART : Form,IOPC
    {
        public JAC_KEYPART()
        {
            InitializeComponent();
        }

        #region 仓储
        RepositoryFactory<P_PLAN> PlanRepositoryFactory = new RepositoryFactory<P_PLAN>();
        RepositoryFactory<P_PRODUCT_SERIAL> SerialRepositoryFactory = new RepositoryFactory<P_PRODUCT_SERIAL>();
        RepositoryFactory<P_ASSEMBLE_PRODUCT_STATE> AssembleRepositoryFactory = new RepositoryFactory<P_ASSEMBLE_PRODUCT_STATE>();

        RepositoryFactory<CONTROL_ADDRESS> Control_AddressRepositoryFactory = new RepositoryFactory<CONTROL_ADDRESS>();//控制地址
        RepositoryFactory<STATION> StationRepositoryFactory = new RepositoryFactory<STATION>();//工位信息
        RepositoryFactory<WORK_CENTER> WorkCenterRepositoryFactory = new RepositoryFactory<WORK_CENTER>();//工作中心信息
        RepositoryFactory<STOPPER> StopperRepositoryFactory = new RepositoryFactory<STOPPER>();//停止器信息
        RepositoryFactory<EQUIPMENT> EquipmentRepository = new RepositoryFactory<EQUIPMENT>();
        RepositoryFactory<CHECK_EQUIP_RESULT> CHECK_EQUIP_RESULTRepository = new RepositoryFactory<CHECK_EQUIP_RESULT>();//设备点检结果表
        RepositoryFactory<P_KEY_PART_INFOR> P_KEY_PART_INFORRepositoryFactory = new RepositoryFactory<P_KEY_PART_INFOR>();//安全件信息过程表
        RepositoryFactory<DOC_KEY_PART_INFOR> DOC_KEY_PART_INFORRepositoryFactory = new RepositoryFactory<DOC_KEY_PART_INFOR>();//安全件信息档案表
        RepositoryFactory<DOC_EQUIP_STATUS> DOC_EQUIP_STATUSRepositoryFactory = new RepositoryFactory<DOC_EQUIP_STATUS>();//设备信息档案表
        RepositoryFactory<Part_Supplier> SupplierRepositoryFactory = new RepositoryFactory<Part_Supplier>();//供应商基本信息表
        RepositoryFactory<P_NOTOK_PRODUCT_INFOR> P_NOTOK_PRODUCT_INFORRepositoryFactory = new RepositoryFactory<P_NOTOK_PRODUCT_INFOR>();//不合格产品信息过程表
        RepositoryFactory<DOC_NOTOK_PRODUCT_INFOR> DOC_NOTOK_PRODUCT_INFORRepositoryFactory = new RepositoryFactory<DOC_NOTOK_PRODUCT_INFOR>();//不合格产品信息档案表
        RepositoryFactory<Base_DataDictionary> Base_DataRepository = new RepositoryFactory<Base_DataDictionary>();
        RepositoryFactory<Base_DataDictionaryDetail> Base_DataDetailRepository = new RepositoryFactory<Base_DataDictionaryDetail>();
        RepositoryFactory<MATERIAL_WC_PART> MATERIAL_WC_PARTRepository = new RepositoryFactory<MATERIAL_WC_PART>();//线边库物料
        RepositoryFactory<CONCESSION_RELEASE> CONCESSION_RELEASERepository = new RepositoryFactory<CONCESSION_RELEASE>();//产品让步放行记录表
        #endregion

        #region 内存公共变量
        List<CONTROL_ADDRESS> ControlAddressList;//该工位的停止器所配置的地址信息
        List<CHECK_INFO> CheckInfoLists;//主工位对应的设备点检信息

        string OpcName;
        string mainstationkey;//主工位主键
        static string strpath = "E:\\#1#纳威司达项目\\安全件1.png";
        static readonly object locker = new object();
        public string wc_code = ReadXML.GetWCCode(System.Windows.Forms.Application.StartupPath + @"\WCConfig\WC.xml");//工作中心编号
        List<STATION> StationList = new List<STATION>();//该PC对应的工作中心下所属工位的集合
        WorkCenterInfor wcinfor;
        string plan_code;//当前产品所属计划
        int collectedcount = 0;//判断已采集的安全件的数量
        //int finishcollected = 0;//finishcollected枚举值{0：未开始采集，1：一开始采集，未采集完成，2：采集完成}
        public BasicInfoDto basicinfor;//根据工位得出的基本信息
        public List<P_KEY_PART_C> key_part_c;//安全件采集配置
        public List<Part_Supplier> supplier;//安全件供应商信息
        //public DataTable team_shift;//班组班制信息
        public P_ASSEMBLE_PRODUCT_STATE plan_product_data;//通过产品出生证查到的该产品出生证代表的计划、产品信息
        List<P_KEY_PART_C> KeyPartData = new List<P_KEY_PART_C>();//根据产品出生证和工位号查询到本次需要采集的安全件信息，逐个多条(按数量分解成单个安全件，无上限限制，用预留字段1标识是否被加载到界面上过：0-未加载；1-已加载)

        TextBox[] key_part_name_txts;//安全件名称文本框
        TextBox[] key_part_code_txts;//安全件编号文本框
        TextBox[] key_part_barcode_txts;//安全件条码文本框
        Button[] confirm_part_btns;//OK按钮文本框
        Label[] key_part_name_lbls;//安全件名称label
        Label[] key_part_code_lbls;//安全件编号label
        Label[] key_part_barcode_lbls;//安全件条码label
        Label[] vice_station_code_lbls;//非主工位label
        Label[] equ_check_lbls;//设备点检项label
        RadioButton[] yes_radio_btns;//点检正常选项按钮
        RadioButton[] no_radio_btns;//点检异常选项按钮
        RectangleShape[] key_part_rectangleShapes;//形状label

        Color HasCollectBtnColor = Color.FromArgb(57, 204, 36);//已采集安全件确认按钮颜色
        Color NotCollectBtnColor =Color.FromArgb(5, 90, 150);//未采集安全件确认按钮颜色

        DOC_EQUIP_STATUS doc_equip_status = new DOC_EQUIP_STATUS();//加工记录表
        P_NOTOK_PRODUCT_INFOR p_notok_product_infor = new P_NOTOK_PRODUCT_INFOR();//不合格产品信息表

        DataTable dt_documents = new DataTable();
        #region 声明委托与事件，监控线边库物料数量

        public delegate void ClickDelegate(object sender, EventArgs e);
        public event ClickDelegate click;
        int materialandonnum = 0;//找到物料andon属于andon类型的第几项，后面andon呼叫时有用
        ANDON andon;

        public void AndonClickEvent()
        {
            try
            {
                andon = new ANDON(basicinfor);
                for (int t = 0; t < andon.andonTypeTable.Count; t++)
                {
                    if (andon.andonTypeTable[t].ANDON_TYPE_NAME.ToString().Contains("物料"))
                    {
                        materialandonnum = t;
                        break;
                    }
                }
                click = null;
                click += andon.materialButton_Click;//单击选中某一物料
                click += andon.Andon_Click;//提交ANDON数据
            }
            catch
            {

            }
        }
        #endregion
        #endregion

        #region 界面加载与关闭

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyPart_Load(object sender, EventArgs e)
        {
            try
            {
                ShowServerConnectionState();//服务器通信状态
                Initialization();
                //Task.Run(new Action(() => AndonClickEvent()));
                SetLableText(main_station_lbl, StationList.FindAll(s => s.IS_MAIN == "是").Select(s => s.STATION_CODE).FirstOrDefault() + "工位");//设置程序主工位名称
                SetViceStationLabel();
                #region SQLite操作
                Task.Run(() =>
                {
                    try
                    {
                        //SQLiteOperate.CreateSQLiteDataBase();//创建SQL数据库
                        //SQLiteOperate.SynchronizeUserInfo();//同步本地人员基本信息
                        //SQLiteOperate.SynchronizeControlAddress();//同步本地控制地址基本信息
                        //SQLiteOperate.SynchronizeStopper();//同步本地停止器基本信息
                        //SQLiteOperate.SynchronizeStation();//同步本地工位基本信息
                        SQLiteOperate.SynchronizeList(new BASE_USER());
                        SQLiteOperate.SynchronizeList(new CONTROL_ADDRESS());
                        SQLiteOperate.SynchronizeList(new STOPPER());
                        SQLiteOperate.SynchronizeList(new STATION());
                        SQLiteOperate.SynchronizeKeyPartInfor();//同步安全件采集过程信息(创建表，清除一周前已同步信息，同步未同步信息逻辑待定)
                    }
                    catch (Exception ex)
                    {
                        Log.GetInstance.WriteLog(ex.Message);
                    }
                });
                #endregion
                Task.Run(() => RecordTimeShow());
               // Task.Run(() => ShowLineConnectionState());
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog(ex.Message);
            }
        }

        private void SetViceStationLabel()
        {
            try
            {
                List<string> vice_station_list = StationList.FindAll(s => s.IS_MAIN != "是").Select(s => s.STATION_CODE).ToList();//获得该工作中心对应所有非主工位
                if (vice_station_list != null)
                {
                    for (int i = 0; i < vice_station_list.Count; i++)
                    {
                        if (i < 3)//最多三个
                        {
                            SetVisible(vice_station_code_lbls[i], true);//显示安全件名称text控件
                            SetText(vice_station_code_lbls[i], vice_station_list[i] + "工位");//安全件名称text控件赋值
                        }
                    }
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 界面关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEYPART1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Formclosing();
            }
            catch
            {

            }
        }

        #region 初始化
        public void Initialization()
        {
            try
            {
                /* 当前获取工位列表方案*/
                /* 通过工作中心主键获取对应工位列表集合*/
               // string localIP = OPCHelper.getIP();
                if (ServerCommunicationState == true)
                {
                    WORK_CENTER wcentity= WorkCenterRepositoryFactory.Repository().FindEntity("Work_Center_Code", wc_code);
                    StationList = StationRepositoryFactory.Repository().FindList().Where(s => s.WORK_CENTER_KEY == wcentity.WORK_CENTER_KEY).ToList();
                }
                else
                {
                    WORK_CENTER wcentity = WorkCenterRepositoryFactory.Repository(DatabaseType.SQLite).FindEntity("Work_Center_Code", wc_code);
                    StationList = StationRepositoryFactory.Repository(DatabaseType.SQLite).FindList().Where(s => s.WORK_CENTER_KEY == wcentity.WORK_CENTER_KEY).ToList();
                }
                mainstationkey = StationList.FindAll(s => s.IS_MAIN == "是").Select(s => s.STATION_KEY).FirstOrDefault();//主工位主键
                if (mainstationkey == null)
                {
                    MessageBox.Show("没有" + wc_code + "工位信息", "工位信息缺失", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                InitializationOnlineOrOffline();//在线/离线初始化
                if (ServerCommunicationState == true)
                {
                    InitializationOnline();//在线初始化
                }
                //Task.Run(new Action(() => GetAllFilesBySQL()));//获取作业指导文件
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog(ex.Message);
            }
        }
        private void InitializationOnline()//在线初始化
        {
            try
            {
                basicinfor = GetData.GetFactoryInforByWccode(StationList.Find(s => s.IS_MAIN == "是").STATION_CODE);//获取基本信息(StationCodeList);
                CheckInfoLists = GetData.GetCheckInforByStationKey(StationList.Find(s => s.IS_MAIN == "是").STATION_KEY);//获取基本信息(StationCodeList);
                Task.Run(() => SynchronizeKey_Part_C(StationList.Select(s => s.STATION_CODE).ToList()));
                supplier = GetData.GetSupplierInfor();
                
                //team_shift = GetData.GetTeamByWccode(wc_code);

                if (basicinfor == null || /*key_part_c.Rows.Count < 1 ||*/ supplier.Count < 1)
                {
                    DialogResult dr = MyMsgBox.Show("基本信息获取失败！是否终止启动系统？", "信息提示", MyMsgBox.MyButtons.YesNo, MyMsgBox.MyIcon.Error,10);
                    if (dr == DialogResult.Yes)
                    {
                        Formclosing();
                        //System.Windows.Forms.Application.Exit();
                    }
                }

                #region 图标显示(在线)
                //string pages = WebApiHttp.HttpGet($"http://{ServerDictionary.IPCode}/API/WebApiPage/GetPageInfo");//WebApi获取图标文件包
                //string pages = WebApiHttp.HttpGet($"http://localhost:28188/API/WebApiPage/GetPageInfo");//WebApi获取图标文件包
                //if (pages != null)
                //{
                //    CS_PageInfo pageinfo = pages.ToJson<CS_PageInfo>();//转换为实体
                //    Stream ms_system_pic = new MemoryStream(pageinfo.SystemPicture);//获取相应图片信息
                //    SetPicture(system_pic, ms_system_pic);
                //    Stream ms_lab_pic = new MemoryStream(pageinfo.LaboratoryPicture);
                //    SetPicture(lab_pic, ms_lab_pic);
                //}
                #endregion

                #region 为Lable赋值(在线)
                SetLableText(LaboratoryName, ServerDictionary.LaboratoryName);
                SetLableText(ProgramName_CN_CS, ServerDictionary.ProgramName_CN_CS);
                SetLableText(ProgramName_CN_EN, ServerDictionary.ProgramName_CN_EN);
                #endregion

                #region 显示工位加工情况
                wcinfor = new WorkCenterInfor(StationList.FindAll(s => s.IS_MAIN == "是").Select(s => s.STATION_CODE).ToList());//获取工位产品加工信息
                ShowWcInfor();//显示工位加工数量()
                #endregion

                #region 设备点检浮动框可见性及点检信息显示(在线/离线)
                CHECK_EQUIP_RESULT current_check_equipment = CHECK_EQUIP_RESULTRepository.Repository().FindList().Where(s => s.STATION_KEY == mainstationkey && DateTime.Parse(s.CREATEDATE.ToString()).ToShortDateString() == ServerTime.Now.ToShortDateString()).FirstOrDefault();
                if (current_check_equipment == null)
                {
                    #region 设备及点检信息显示
                    if (CheckInfoLists != null && CheckInfoLists.Count != 0)
                    {
                        dockPanel1.Visibility = DockVisibility.Visible;//浮动框可见,条件触发
                        SetLableText(equipmentname_lbl, basicinfor.EQUIPMENT_NAME);//设备名称label赋值
                        for (int i = 0; i < CheckInfoLists.Count; i++)
                        {
                            if (i < 3)//最多三个
                            {
                                SetVisible(equ_check_lbls[i], true);//显示设备点检项text控件
                                SetText(equ_check_lbls[i], CheckInfoLists[i].CHECK_INFO_NAME);//设备点检项text控件赋值
                                SetVisible(yes_radio_btns[i], true);//显示安全件编号text控件
                                SetVisible(no_radio_btns[i], true);//显示安全件编号text控件
                            }
                        }
                    }
                    #endregion
                }
                #endregion
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog(ex.Message);
            }
        }

        private void InitializationOnlineOrOffline()//在线/离线初始化
        {
            product_born_code_txt.Focus();
            //登录人员显示
            staff_code_lbl.Text = SystemLog.UserCode;//系统登录员工编号
            staff_name_lbl.Text = SystemLog.UserName;//系统登录员工姓名
            DateTime dt = ServerTime.Now;
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

            #region 生产状态初始化(在线/离线)
            productarrival_lbl.BackColor = SystemColors.ControlDark;
            productborncode_lbl.BackColor = SystemColors.ControlDark;
            keypartbarcode_lbl.BackColor = SystemColors.ControlDark;
            finish_lbl.BackColor = SystemColors.ControlDark;
            allowrelease_lbl.BackColor = SystemColors.ControlDark;
            #endregion

            #region 界面待显示控件集合(在线/离线)
            key_part_name_txts = new TextBox[] { key_part_name_txt, key_part_name2_txt, key_part_name3_txt, key_part_name4_txt, key_part_name5_txt };//安全件名称文本框
            key_part_code_txts = new TextBox[] { key_part_code_txt, key_part_code2_txt, key_part_code3_txt, key_part_code4_txt, key_part_code5_txt };//安全件编号文本框
            key_part_barcode_txts = new TextBox[] { key_part_barcode_txt, key_part_barcode2_txt, key_part_barcode3_txt, key_part_barcode4_txt, key_part_barcode5_txt };//安全件条码文本框
            confirm_part_btns = new Button[] { confirm_part_btn, confirm_part2_btn, confirm_part3_btn, confirm_part4_btn, confirm_part5_btn };//OK按钮文本框
            key_part_name_lbls = new Label[] { key_part_name_lbl, key_part_name2_lbl, key_part_name3_lbl, key_part_name4_lbl, key_part_name5_lbl };//安全件名称label
            key_part_code_lbls = new Label[] { key_part_code_lbl, key_part_code2_lbl, key_part_code3_lbl, key_part_code4_lbl, key_part_code5_lbl };//安全件编号label
            key_part_barcode_lbls = new Label[] { key_part_barcode_lbl, key_part_barcode2_lbl, key_part_barcode3_lbl, key_part_barcode4_lbl, key_part_barcode5_lbl };//安全件条码label
            key_part_rectangleShapes = new RectangleShape[] { key_part_rectangleShape1, key_part_rectangleShape2, key_part_rectangleShape3, key_part_rectangleShape4 }; //形状label
            vice_station_code_lbls = new Label[] { vice_station_lbl1, vice_station_lbl2, vice_station_lbl3 };//非主工位label
            equ_check_lbls = new Label[] { Equ_check_lbl1, Equ_check_lbl2, Equ_check_lbl3 };//设备点检项label
            yes_radio_btns = new RadioButton[] { yes_radio_btn1, yes_radio_btn2, yes_radio_btn3 };//点检正常选项按钮
            no_radio_btns = new RadioButton[] { no_radio_btn1, no_radio_btn2, no_radio_btn3 };//点检异常选项按钮
            #endregion

            #region 打开扫描枪连接(在线/离线)
            ScannerHelper.wc_code = StationList.Where(s => s.IS_MAIN == "是").Select(s => s.STATION_CODE).FirstOrDefault();
            ScannerHelper.OpenCom(scanport);//打开扫描枪连接
            #endregion

            #region PLC跳变读取信息(在线/离线)
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
                string IP = "";
                if (wc_code.Contains("2.7"))
                {
                    IP = ConfigurationManager.AppSettings["2.7KepServerIP"].ToString().Trim();
                }
                else if (wc_code.Contains("4.0"))
                {
                    IP = ConfigurationManager.AppSettings["4.0KepServerIP"].ToString().Trim();
                }
                OPCHelper<JAC_KEYPART>.OPC_Connect(this,IP, ControlAddressList);            
            }
            ));
            Task.Run(new Action(ReFreshAddressByTiming));
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
                if (ServerCommunicationState == true)
                {
                    STOPPER stopentity = StopperRepositoryFactory.Repository().FindEntity("station_key", mainstationkey);//主工位对应的停止器
                    ControlAddressList = Control_AddressRepositoryFactory.Repository().FindList($" and POSITION_KEY='{stopentity.STOPPER_KEY}'").ToList();
                }
                else
                {
                    STOPPER stopentity = StopperRepositoryFactory.Repository(DatabaseType.SQLite).FindList().Where(s => s.STATION_KEY == mainstationkey).FirstOrDefault();//主工位对应的停止器
                    ControlAddressList = Control_AddressRepositoryFactory.Repository(DatabaseType.SQLite).FindList($" and POSITION_KEY='{stopentity.STOPPER_KEY}'").ToList();
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
                    int d = Convert.ToInt16(ServerDictionary.timeValue);
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
        private void SynchronizeKey_Part_C(List<string> StationCodeList)
        {
            //key_part_c = KeyPartGetData.GetKeyPartConfig(StationCodeList);
        }
        /// <summary>
        /// 同步安全件信息(工位+计划)
        /// </summary>
        /// <param name="StationCodeList"></param>
        /// <param name="mes_plan_key"></param>
        /// <returns></returns>
        private List<P_KEY_PART_C> SynchronizeKey_Part_C(List<string> StationCodeList, string mes_plan_key)
        {
            try
            {
                List<P_KEY_PART_C> list = KeyPartGetData.GetKeyPartConfig(StationCodeList, mes_plan_key);
                Task.Run(() =>
                {
                    if (list.Count > 0)//如果能够从数据库查到数据，表示数据未同步，此时，应当进行数据同步；若此时查不到数据，则表示待采集安全件未配置，此时不需要同步
                    {
                        SynchronizeKey_Part_C(StationCodeList);
                    }
                });
                return list;
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("同步待采集安全件时出现异常：" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
                return null;
            }
        }
        public void Formclosing()//为了防止出现方法同名，此处用Formclosing命名，原定用FormClosing命名
        {
            try
            {
                ScannerHelper.CloseCom(scanport);
                //if (listener.IsAlive) listener.Abort();
                //if (OpcGroupClasssubscription == null) return;
                ////取消回调事件
                //OpcGroupClasssubscription.DataChanged -= new Opc.Da.DataChangedEventHandler(this.OnDataChange);
                ////移除组内item
                //OpcGroupClasssubscription.RemoveItems(OpcGroupClasssubscription.Items);
                ////结束：释放各资源
                //OpcGroupClassm_server.CancelSubscription(OpcGroupClasssubscription);//m_server前文已说明，通知服务器要求删除组        
                //OpcGroupClasssubscription.Dispose();//强制.NET资源回收站回收该subscription的所有资源。        
                ////OpcGroupClassm_server.Disconnect();//断开服务器连接   
            }
            finally
            {
                this.Dispose();
                this.Close();
            }
        }
        #endregion

        #region 手动输入

        #region 点击手动输入
        private void inputmodel_product_btn_Click(object sender, EventArgs e)//产品出生证手动输入与PLC信号跳变调整
        {

            if (inputmodel_product_btn.Text == "手动输入")
            {
                if (new ValidateForm("JAC_MES_KEYPART", "KEYPART_HAND_MODEL", "手动模式权限验证").ShowDialog() == DialogResult.OK)
                {
                    InitializationForm(false);
                    product_born_code_txt.Enabled = true;
                    product_born_code_txt.Text = "";
                    product_born_code_txt.Focus();
                    inputmodel_product_btn.Text = "自动读取";
                    confirm_product_btn.Enabled = true;
                    SetCollectProgress(finish_lbl, 0, 0);//采集进度初始化
                                                         //product_born_code_txt.Text = "84600112";//调试用
                    product_born_code_txt.Text = "";//调试用
                }
            }
            else if (inputmodel_product_btn.Text == "自动读取")
            {
                product_born_code_txt.Enabled = false;
                inputmodel_product_btn.Text = "手动输入";
                confirm_product_btn.Enabled = false;
            }
        }

        private void inputmodel_part_btn_Click(object sender, EventArgs e)//安全件条码手动输入与扫描输入调整
        {
            //keypartbarcode_txt.Enabled = true;
            if (inputmodel_part_btn.Text == "手动输入")//自动转手动
            {
                if (new ValidateForm("JAC_MES_KEYPART", "KEYPART_HAND_MODEL", "手动模式权限验证").ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < confirm_part_btns.Length; i++)
                    {
                        if (confirm_part_btns[i].BackColor != HasCollectBtnColor)//如果确认button为绿色，则表示该安全件信息已经提交，手动输入，自动输入切换无效
                        {
                            key_part_barcode_txts[i].Text = "";
                            key_part_barcode_txts[i].Focus();
                        }
                        key_part_barcode_txts[i].Enabled = true;
                        confirm_part_btns[i].Enabled = true;
                    }
                    inputmodel_part_btn.Text = "扫描输入";
                }
            }
            else if (inputmodel_part_btn.Text == "扫描输入")//手动转自动
            {
                key_part_barcode_txt.Enabled = false;
                key_part_barcode2_txt.Enabled = false;
                key_part_barcode3_txt.Enabled = false;
                key_part_barcode4_txt.Enabled = false;
                key_part_barcode5_txt.Enabled = false;
                inputmodel_part_btn.Text = "手动输入";
                confirm_part_btn.Enabled = false;
                confirm_part2_btn.Enabled = false;
                confirm_part3_btn.Enabled = false;
                confirm_part4_btn.Enabled = false;
                confirm_part5_btn.Enabled = false;
            }
        }
        #endregion

        #region 手动输入点击确认（OK）

        private void confirm_product_btn_Click(object sender, EventArgs e)//计划模块确认
        {
            if (ServerCommunicationState == true)
            {
                string product_born_code = product_born_code_txt.Text;//product_born_code作为中间变量，用来保存手动输入的产品出生证
                if (string.IsNullOrWhiteSpace(product_born_code))
                {
                    MyMsgBox.Show("请输入产品出生证!", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information,5);
                    return;
                }
                if (inputmodel_product_btn.Text == "自动读取")
                {
                    Log.GetInstance.WriteLog("Input", "输入产品出生证：" + product_born_code_txt.Text);
                    //ScannerHelper.OpenCom(scanport);//打开扫描枪连接
                    GetProductBornCode(product_born_code);
                }
            }
        }
        private void confirm_part_btn_Click(object sender, EventArgs e)//安全件模块确认
        {
            try
            {
                if (inputmodel_part_btn.Text == "扫描输入"&& confirm_part_btn.BackColor != HasCollectBtnColor)//只有在手动输入的条件下，OK键才有用
                {
                    if (product_code_txt.Text == "" && product_batch_no_txt.Text == "" && plan_code_txt.Text == "")
                    {
                        //没有输入产品信息，或者输入产品信息错误，两种情况下，产品信息均不能顺利成功加载，即全部为空
                        SetText(key_part_barcode_txt, "请先输入正确产品信息。");
                        SetColor(productarrival_lbl, Color.Red);
                    }
                    else
                    {
                        if (key_part_barcode_txt.Enabled == true)//在安全件编号允许人机交互（即Enabled == true）的情况下，OK键才有用。如果安全件编号无数据，手动点击ok无意义
                        {
                            Log.GetInstance.WriteLog("Input", "输入零部件编号：" + key_part_barcode_txt.Text);
                            int IsOk = BindKeyPartInfor(key_part_barcode_txt.Text, 0);
                            if (IsOk != 1)
                            {
                                SetText(key_part_barcode_txt, "安全件绑定失败！请重试。");
                                SetText(keypartbarcode_lbl, "条码错误");
                                SetColor(keypartbarcode_lbl, Color.Red);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetText(key_part_barcode_txt, "安全件条码不正确！请重试。");
                SetText(keypartbarcode_lbl, "条码错误");
                SetColor(keypartbarcode_lbl, Color.Red);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("安全件条码不正确！请重试。" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        private void confirm_part2_btn_Click(object sender, EventArgs e)//安全件模块确认
        {
            try
            {
                if (inputmodel_part_btn.Text == "扫描输入" && confirm_part2_btn.BackColor != HasCollectBtnColor)//只有在手动输入的条件下，OK键才有用
                {
                    if (key_part_barcode2_txt.Enabled == true)//在安全件编号允许人机交互（即Enabled == true）的情况下，OK键才有用。如果安全件编号无数据，手动点击ok无意义
                    {
                        Log.GetInstance.WriteLog("Input", "输入零部件编号：" + key_part_barcode2_txt.Text);
                        int IsOk = BindKeyPartInfor(key_part_barcode2_txt.Text, 1);
                        if (IsOk != 1)
                        {
                            SetText(key_part_barcode2_txt, "安全件绑定失败！请重试。");
                            SetText(keypartbarcode_lbl, "条码错误");
                            SetColor(keypartbarcode_lbl, Color.Red);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetText(key_part_barcode2_txt, "安全件条码不正确！请重试。");
                SetColor(keypartbarcode_lbl, Color.Red);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("安全件条码不正确！请重试。" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        private void confirm_part3_btn_Click(object sender, EventArgs e)//安全件模块确认
        {
            try
            {
                if (inputmodel_part_btn.Text == "扫描输入" && confirm_part3_btn.BackColor != HasCollectBtnColor)//只有在手动输入的条件下，OK键才有用
                {
                    if (key_part_barcode3_txt.Enabled == true)//在安全件编号允许人机交互（即Enabled == true）的情况下，OK键才有用。如果安全件编号无数据，手动点击ok无意义
                    {
                        Log.GetInstance.WriteLog("Input", "输入零部件编号：" + key_part_barcode3_txt.Text);
                        int IsOk = BindKeyPartInfor(key_part_barcode3_txt.Text, 2);
                        if (IsOk != 1)
                        {
                            SetText(key_part_barcode3_txt, "安全件绑定失败！请重试。");
                            SetText(keypartbarcode_lbl, "条码错误");
                            SetColor(keypartbarcode_lbl, Color.Red);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetText(key_part_barcode3_txt, "安全件条码不正确！请重试。");
                SetText(keypartbarcode_lbl, "条码错误");
                SetColor(keypartbarcode_lbl, Color.Red);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("安全件条码不正确！请重试。" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        private void confirm_part4_btn_Click(object sender, EventArgs e)//安全件模块确认
        {
            try
            {
                if (inputmodel_part_btn.Text == "扫描输入" && confirm_part4_btn.BackColor != HasCollectBtnColor)//只有在手动输入的条件下，OK键才有用
                {
                    if (key_part_barcode4_txt.Enabled == true)//在安全件编号允许人机交互（即Enabled == true）的情况下，OK键才有用。如果安全件编号无数据，手动点击ok无意义
                    {
                        Log.GetInstance.WriteLog("Input", "输入零部件编号：" + key_part_barcode4_txt.Text);
                        int IsOk = BindKeyPartInfor(key_part_barcode4_txt.Text, 3);
                        if (IsOk != 1)
                        {
                            SetText(key_part_barcode4_txt, "安全件绑定失败！请重试。");
                            SetText(keypartbarcode_lbl, "条码错误");
                            SetColor(keypartbarcode_lbl, Color.Red);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetText(key_part_barcode4_txt, "安全件条码不正确！请重试。");
                SetText(keypartbarcode_lbl, "条码错误");
                SetColor(keypartbarcode_lbl, Color.Red);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("安全件条码不正确！请重试。" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        private void confirm_part5_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (inputmodel_part_btn.Text == "扫描输入" && confirm_part5_btn.BackColor != HasCollectBtnColor)//只有在手动输入的条件下，OK键才有用
                {
                    if (key_part_barcode5_txt.Enabled == true)//在安全件编号允许人机交互（即Enabled == true）的情况下，OK键才有用。如果安全件编号无数据，手动点击ok无意义
                    {
                        Log.GetInstance.WriteLog("Input", "输入零部件编号：" + key_part_barcode5_txt.Text);
                        int IsOk = BindKeyPartInfor(key_part_barcode5_txt.Text, 4);
                        if (IsOk != 1)
                        {
                            SetText(key_part_barcode5_txt, "安全件绑定失败！请重试。");
                            SetColor(keypartbarcode_lbl, Color.Red);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetText(key_part_barcode5_txt, "安全件条码不正确！请重试。");
                SetColor(keypartbarcode_lbl, Color.Red);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("安全件条码不正确！请重试。" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        #endregion

        #endregion

        #region #1#获取到产品出生证
        public void GetProductBornCode(string product_born_code)//获取到新产品出生证
        {
            try
            {
                #region 显示上一个产品ID号
                if (plan_product_data != null&& plan_product_data.PRODUCT_BORN_CODE!=null)
                {
                    SetText(product_name_txt, plan_product_data.PRODUCT_BORN_CODE);//显示上一个产品ID号
                }
                #endregion
                //ScannerHelper.WriteToSerialPort(product_born_code, ReadXML.COM_code);//将产品出生证写入串口
                List<P_ASSEMBLE_PRODUCT_STATE> p_assemble_product_state = GetData.GetProductinforByborncode(product_born_code);
                if (p_assemble_product_state.Count == 0)
                {
                    SetText(product_born_code_txt, "产品出生证信息错误");
                    return;
                }
                //p_assemble_product_state[0] = basicinfor[0].EntityMapper<BasicInfoDto, P_ASSEMBLE_PRODUCT_STATE>();
                p_assemble_product_state[0] = EntityHelper.EntityHelper.EntityMapper(basicinfor, p_assemble_product_state[0]);
                //new DataBaseOpByEntity().Update(p_assemble_product_state);

                if (p_assemble_product_state != null && p_assemble_product_state.Count > 0)
                {
                    //LoadGuidFiles(string.IsNullOrEmpty(p_assemble_product_state[0].PRODUCT_KEY) ? "" : p_assemble_product_state[0].PRODUCT_KEY.ToString());
                    //LoadGuidFiles();
                }
                if (KeyPartData.Count != 0)//程序第一次运行时KeyPartData为空
                {
                    if (collectedcount != KeyPartData.Count)
                    {
                        NotOkProductinfor();//记录不合格产品信息
                        wcinfor.AllNum += 1;
                        wcinfor.NotOkNum += 1;
                        wcinfor.ContinueNotOkNum++;//连续不合格数加1
                        p_notok_product_infor = new P_NOTOK_PRODUCT_INFOR();//重新初始化
                    }
                    else
                    {
                        wcinfor.AllNum += 1;
                        wcinfor.OkNum += 1;
                        wcinfor.ContinueNotOkNum = 0;//将连续不合格数标记为0
                    }
                }
                if (doc_equip_status.STARTDATE != null)
                {
                    //EquipProcessingRecord();//设备加工记录
                    doc_equip_status = new DOC_EQUIP_STATUS();//重新初始化
                }
                ShowWcInfor();//显示工位加工数量
                doc_equip_status.STARTDATE = ServerTime.Now;
                InitializationForm(true);//初始化后，产品出生证的值为空
                SetText(product_born_code_txt, product_born_code);//重新为产品出生证赋值
                AfterGetProductBornCode();//产品到达界面变化
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("获取产品出生证后校验上个产品出现错误：" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        public void AfterGetProductBornCode()//获取到新产品出生证之后
        {
            try
            {
                //一旦获取到产品出生证【无论是手动输入还是扫描输入获得】，即表明产品到达
                SetColor(productarrival_lbl, HasCollectBtnColor);//生产状态的“产品到达”按钮颜色改变
                int IsOk = Data();//根据产品出生证显示相应数据，计划编号，产品编号，批次号等
                if (IsOk == 1)
                {
                    SetVisible(arrowpicture2_pic, false);//第二个箭头不可见
                    SetVisible(arrowpicture3_pic, false);//第三个箭头不可见
                    SetVisible(arrowpicture4_pic, false);//第四个箭头不可见
                    SetColor(productborncode_lbl, HasCollectBtnColor);//生产状态的“产品出生证扫描成功”按钮颜色改变
                    SetVisible(arrowpicture1_pic, true);//第一个箭头可见
                    SetEnabled(product_born_code_txt, false);//使产品出生证text控件不可操作

                    SetText(inputmodel_product_btn, "手动输入");//显示产品出生证输入模式控件

                }
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("获取产品出生证后显示数据出现错误：" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        #endregion

        #region 绑定安全件数据之后
        public void AfterBindPartBarCode()//主要是改变颜色
        {
            try
            {
                SetText(keypartbarcode_lbl, "条码正确");
                SetColor(keypartbarcode_lbl, HasCollectBtnColor);//只要本次扫描成功，生产状态的“安全件扫描成功”按钮颜色改变为绿色
                SetVisible(arrowpicture2_pic, true);//第二个箭头可见
                if (collectedcount == KeyPartData.Count)//如果采集数量等于要求采集数量，采集完成
                {
                    SetColor(finish_lbl, HasCollectBtnColor);//生产状态的“采集成功”按钮颜色改变为绿色
                                                                      //Thread.Sleep(500);
                    SetColor(allowrelease_lbl, HasCollectBtnColor);//生产状态的“允许放行”按钮颜色改变为绿色
                    SetVisible(arrowpicture3_pic, true);//第三个箭头可见
                    SetVisible(arrowpicture4_pic, true);//第四个箭头可见

                    #region 对PLC写入允许放行信号
                    CONTROL_ADDRESS is_leave_ok = ControlAddressList.FirstOrDefault(s => s.ADDRESS_TYPE == ((int)AddressCategory.is_leaveOK).ToString());
                    if (is_leave_ok != null)
                    {
                        OPCHelper<JAC_KEYPART>.SynWriteOpcItem(is_leave_ok.ADDRESS_PATH, "1");//写入允许放行
                    }
                    #endregion
                }
                else if (collectedcount != 0 && collectedcount < KeyPartData.Count)//如果已经采集安全件了，但采集数量小于要求采集数量，即部分采集
                {
                    SetColor(finish_lbl, Color.Yellow);//生产状态的“采集成功”按钮颜色改变为黄色
                    SetVisible(arrowpicture3_pic, true);//第三个箭头可见
                }

                ShowWcInfor();//显示工位加工数量
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("绑定安全件数据之后出现错误：" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        #endregion

        #region 提交安全件采集数据

        #region 获取即将绑定的安全件信息——开始绑定安全件信息
        /// <summary>
        /// 获取绑定安全件信息
        /// </summary>
        /// <param name="keypartbarcode">安全件条形码</param>
        /// <returns></returns>
        public int BindKeyPartInfor(string keypartbarcode, int i)
        {
            //i用来判断keypartbarcode是第几个条形码，即是由第几个OK调用此方法的
            Repository<P_KEY_PART_INFOR> tran = new Repository<P_KEY_PART_INFOR>();
            try
            {
                int IsOk = 0;
                //string keypartcode = DeBarcode.GetPartCode(keypartbarcode);//解析条码判断条码正确性
                //此处的条形码正不正确的判断主要是为了检验手动输入的条码的正确性，扫描的条码走到这一步，说明一定是有匹配的，且匹配的是第i个，验证扫描条码是否匹配的方法：CheckBarcodeNum()
                //if (keypartcode == key_part_code_txts[i].Text)//条形码正确
                //if ((wc_code.Contains("2.7") && keypartbarcode.Trim().Substring(14, keypartbarcode.Length - 14) == key_part_code_txts[i].Text.ToString().Trim())||(wc_code.Contains("4.0") && keypartbarcode.Trim().Substring(1, 7) == key_part_code_txts[i].Text.ToString().Trim()))//条形码正确(2.7L/4.0L)
                //{
                P_KEY_PART_INFOR p_key_part_infor = new P_KEY_PART_INFOR();//当前采集的安全件信息                                                           //key_part_barcode_txts[i].Enabled = false;
                p_key_part_infor = Accept(p_key_part_infor, keypartbarcode, i);
                if (ServerCommunicationState == true)//在线状态提交安全件信息至数据库，并扣除线边库数量
                {
                    bool result = KeyPartOperationDataBase(p_key_part_infor);//提交安全件信息
                    if (result == false) //提交安全件绑定信息失败
                    {
                        SetText(key_part_barcode_txts[i], "提交安全件信息失败！请重试。");
                        SetText(keypartbarcode_lbl, "条码错误");
                        SetColor(keypartbarcode_lbl, Color.Red);
                        IsOk = 0;
                    }
                    else
                    {
                        SetText(key_part_barcode_txts[i], keypartbarcode);
                        IsOk = 1;
                    }
                }
                else//离线状态下将绑定数据暂时缓存在本地SQLite
                {
                    p_key_part_infor.RESERVE1 = "0";//预留字段1作为本地缓存待同步信息0-未同步；1-已同步
                    p_key_part_infor.RESERVE2 = "1";//预留字段2作为区分直接插入数据还是需校验插入数据0-需校验；1-直接插入
                    P_KEY_PART_INFORRepositoryFactory.Repository().Insert(p_key_part_infor);
                    IsOk = 1;
                }
                //}
                //else//条形码不正确
                //{
                //    SetText(key_part_barcode_txts[i], "安全件条码不正确！请重试。");
                //    SetColor(keypartbarcode_lbl, Color.Red);
                //    SetColor(confirm_part_btns[i], NotCollectBtnColor);
                //}
                if (IsOk == 1)//条码成功绑定，此条码之后不可操作
                {
                    if (inputmodel_part_btn.Text == "手动输入")
                    {
                        SetEnabled(confirm_part_btns[i], false);//操作OK button控件,改变可交互性 
                        SetEnabled(key_part_barcode_txts[i], false);//操作安全件 text控件,改变可交互性
                    }
                    if (confirm_part_btns[i].Visible == true)
                    {
                        if (collectedcount < KeyPartData.Count)//为解决扫码过快数据库未提交界面按钮未变色导致数量有出入问题
                        {
                            collectedcount += 1;//条码采集成功，采集数量加1
                            SetCollectProgress(finish_lbl, KeyPartData.Count, collectedcount);
                        }
                    }
                    SetColor(confirm_part_btns[i], HasCollectBtnColor);//操作OK button控件,改变背景色
                    #region 查看是否有未被加载到界面上的安全件信息,如果存在则将该安全件信息填充在上一个被成功采集的安全件位置上
                    foreach (var itemkeypart in KeyPartData)
                    {
                        if (itemkeypart.RESERVE1 == "0")
                        {
                            SetVisible(key_part_name_txts[i], true);//显示安全件名称text控件
                            SetText(key_part_name_txts[i], itemkeypart.PART_NAME);//安全件名称text控件赋值

                            SetVisible(key_part_code_txts[i], true);//显示安全件编号text控件
                            SetText(key_part_code_txts[i], itemkeypart.PART_CODE);//安全件编号text控件赋值

                            SetVisible(key_part_barcode_txts[i], true);//显示安全件条码text控件
                            SetText(key_part_barcode_txts[i], "");//安全件条码text控件置为空值

                            SetVisible(confirm_part_btns[i], true);//显示OK  button控件
                            SetEnabled(confirm_part_btns[i], false);//OK  button控件不可操作

                            SetVisible(key_part_name_lbls[i], true);//显示安全件名称label控件

                            SetVisible(key_part_code_lbls[i], true);//显示安全件编号label控件

                            SetVisible(key_part_barcode_lbls[i], true);//显示安全件条码label控件

                            if (inputmodel_part_btn.Text == "扫描输入")
                            {
                                SetEnabled(confirm_part_btns[i], true);//操作OK button控件,改变可交互性
                                SetEnabled(key_part_barcode_txts[i], true);//操作安全件 text控件,改变可交互性
                            }
                            SetColor(confirm_part_btns[i], NotCollectBtnColor);//操作OK button控件,改变背景色

                            itemkeypart.RESERVE1 = "1";//该安全件已被加载到界面上，预留字段1属性值改为1
                            break;
                        }
                    }
                    #endregion
                    AfterBindPartBarCode();//改变“生产状态”的颜色
                }
                //}
                //else
                //{
                //    MyMsgBox.Show("网络异常", "异常");
                //}
                return IsOk;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                SetText(key_part_barcode_txts[i], "绑定安全件失败！");
                SetText(keypartbarcode_lbl, "条码错误");
                SetColor(keypartbarcode_lbl, Color.Red);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("绑定安全件失败！" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
                return 0;
            }
        }
        #endregion
        /// <summary>
        /// 提交安全件信息
        /// </summary>
        /// <param name="p_key_part_infor"></param>
        /// <param name="material_wc_part"></param>
        /// <returns></returns>
        private bool KeyPartOperationDataBase(P_KEY_PART_INFOR p_key_part_infor)
        {
            Repository<P_KEY_PART_INFOR> tran = new Repository<P_KEY_PART_INFOR>();
            DbTransaction isOpenTrans = tran.BeginTrans();
            try
            {
                #region 安全件数据
                Task.Run(() =>
                {
                    p_key_part_infor.Create();
                    P_KEY_PART_C key_part_entity = KeyPartData.Find(s => s.KEY_PART_C_KEY == p_key_part_infor.KEY_PART_C_KEY);
                    //判断过程表中是否已经存在此条记录
                    List<P_KEY_PART_INFOR> p_key_part_inforlist = P_KEY_PART_INFORRepositoryFactory.Repository().FindList($" and MES_PLAN_KEY='{plan_product_data.MES_PLAN_KEY}' and PRODUCT_BORN_CODE='{plan_product_data.PRODUCT_BORN_CODE}' and STATION_KEY='{key_part_entity.STATION_KEY}' and PART_KEY='{p_key_part_infor.PART_KEY}' and PART_BARCODE='{p_key_part_infor.PART_BARCODE}' and KEY_PART_C_KEY='{p_key_part_infor.KEY_PART_C_KEY}'");
                    //if ((key_part_entity.QUANTITY == 1&& p_key_part_inforlist.Count ==1)|| key_part_entity.QUANTITY>1&&(p_key_part_inforlist.Count >key_part_entity.QUANTITY))//数据库中有同计划、同产品、同零部件的记录,控制绑定数量不超过待采集数量,1-待采集数量为1，实采为1即更新，2-待采>1，实采>待采即更新
                    if (p_key_part_inforlist.Count!=0)//数据库中有同计划、同产品、同零部件的记录
                    {
                        p_key_part_infor.KEY_PART_INFOR_KEY = p_key_part_inforlist[0].KEY_PART_INFOR_KEY.ToString();//如果存在记录，则将本条记录的key赋值为从数据库中查到的key，从而更新记录
                        P_KEY_PART_INFORRepositoryFactory.Repository().Update(p_key_part_infor, isOpenTrans);//更新记录
                    }
                    else//数据库中没有相应记录
                    {
                        P_KEY_PART_INFORRepositoryFactory.Repository().Insert(p_key_part_infor, isOpenTrans);//插入记录
                    }
                    #endregion

                    isOpenTrans.Commit();
                });
                #region 更新线边库(屏蔽)
                //if (material_wc_part != null && material_wc_part.MATERIAL_WC_PARTKEY != null)
                //{
                //    material_wc_part.STORAGE_NUM -= 1;//线边库存量-1
                //    if (material_wc_part.STORAGE_NUM != 0)
                //    {
                //        MATERIAL_WC_PARTRepository.Repository().Update(material_wc_part, isOpenTrans);
                //    }
                //    else//库存为0，删除该记录
                //    {
                //        MATERIAL_WC_PARTRepository.Repository().Delete(material_wc_part, isOpenTrans);
                //    }
                //}
                #endregion
                
                #region 物料不足出发物料Andon(屏蔽)
                //Task.Run(() =>
                //{
                //    try
                //    {
                //        if (material_wc_part != null && material_wc_part.MATERIAL_WC_PARTKEY != null&&material_wc_part.STORAGE_NUM < material_wc_part.SAFETY_NUM)//线边库库存量低于安全库存
                //        {
                //            AndonClickEvent();
                //            SetText(storage_quntity_lbl, material_wc_part.PART_CODE + "\r\n" + material_wc_part.PART_NAME + "库存不足！");
                //            SetColor(storage_quntity_lbl, Color.Red);
                //            SetVisible(storage_quntity_lbl, true);
                //            //MessageBox.Show("线边库库存数量已低于安全库存。", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //            #region 触发物料andon
                //            if (click != null)
                //            {
                //                Button bsender = new Button();
                //                bsender.Name = material_wc_part.PART_CODE + "|" + materialandonnum;
                //                EventArgs e = new EventArgs();
                //                click(bsender, e);
                //            }
                //            #endregion
                //        }
                //        else
                //        {
                //            SetText(storage_quntity_lbl, "");
                //            SetColor(storage_quntity_lbl, Color.Red);
                //            SetVisible(storage_quntity_lbl, false);
                //        }
                //    }
                //    catch
                //    {

                //    }
                //});
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                isOpenTrans.Rollback();
                throw ex;
            }
        }
       
        #region 正在绑定——提交安全件信息
        /// <summary>
        /// 提交安全件与产品的绑定数据
        /// </summary>
        /// <param name="key_part_barcode">扫描的、或者手动输入的安全件条码</param>
        /// <param name="i">第几个需要采集的安全件条码，可能有多个需要采集的安全件条码，用i加以区分</param>
        /// <returns></returns>
        private P_KEY_PART_INFOR Accept(P_KEY_PART_INFOR p_key_part_infor, string key_part_barcode, int i)
        {
            try
            {
                int IsOk = 0;
                //if (ServerCommunicationState)
                //{
                P_KEY_PART_C key_part_entity = KeyPartData.Find(s => s.PART_CODE == key_part_code_txts[i].Text);//获得该条码对应的安全件信息
                                                                                                                //string supplier_code = DeBarcode.GetSupplierCode(key_part_barcode);
                                                                                                                //string part_batch_no = DeBarcode.GetPartBatchCode(key_part_barcode);
                                                                                                                //string part_code = DeBarcode.GetPartCode(key_part_barcode);
                                                                                                                //Part_Supplier supplier_infor = new Part_Supplier();                        
                                                                                                                //try
                                                                                                                //{
                                                                                                                //    supplier_infor = supplier.Find(s=>s.supplier_code== supplier_code);
                                                                                                                //}
                                                                                                                //catch
                                                                                                                //{
                                                                                                                //    //SetText(key_part_barcode_txts[i], "找不到供应商信息！");
                                                                                                                //    //SetColor(keypartbarcode_lbl, Color.Red);
                                                                                                                //    //return 0;
                                                                                                                //}
                string barcode = "";
                //barcode = part_batch_no + supplier_code + part_code;//安全件暗码
                //p_key_part_infor = basicinfor[0].EntityMapper<BasicInfoDto, P_KEY_PART_INFOR>();//为实体增加基本信息
                p_key_part_infor = EntityHelper.EntityHelper.EntityMapper(basicinfor, p_key_part_infor);//为实体增加基本信息
                p_key_part_infor.EQUIP_KEY = basicinfor.EQUIPMENT_KEY;
                p_key_part_infor.EQUIP_CODE = basicinfor.EQUIPMENT_CODE;
                p_key_part_infor.EQUIP_NAME = basicinfor.EQUIPMENT_NAME;
                //p_key_part_infor = supplier_infor.EntityMapper<Part_Supplier, P_KEY_PART_INFOR>();//为实体增加供应商信息
                //p_key_part_infor = EntityHelper<P_KEY_PART_INFOR>.GetPartEntity(p_key_part_infor, team_shift);//为实体增加班制班组信息
                //p_key_part_infor = plan_product_data.EntityMapper<P_ASSEMBLE_PRODUCT_STATE, P_KEY_PART_INFOR>();//为实体增加计划、产品信息
                p_key_part_infor = EntityHelper.EntityHelper.EntityMapper(plan_product_data, p_key_part_infor);//为实体增加计划、产品信息
                p_key_part_infor.Create();//为实体增加唯一主键信息
                p_key_part_infor.ASSMBLY_TIME = ServerTime.Now;//为实体增加装配时间信息
                p_key_part_infor.PART_VINCODE = key_part_barcode;//为实体增加安全件条形码信息
                p_key_part_infor.PART_BARCODE = key_part_barcode;//为实体增加安全件条形码信息
                p_key_part_infor.KEY_PART_C_KEY = key_part_entity.KEY_PART_C_KEY;//为实体增加安全件采集配置key信息
                p_key_part_infor.PART_KEY = key_part_entity.PART_KEY;//为实体增加安全件key信息
                p_key_part_infor.PART_CODE = key_part_entity.PART_CODE;//为实体增加安全件编号信息
                p_key_part_infor.PART_NAME = key_part_entity.PART_NAME;//为实体增加安全件名称信息
                                                                       //p_key_part_infor.PART_BATCH_NO = part_batch_no;//为实体增加安全件批次号信息
                p_key_part_infor.STATION_KEY = key_part_entity.STATION_KEY;
                p_key_part_infor.STATION_CODE = key_part_entity.STATION_CODE;
                p_key_part_infor.STATION_NAME = key_part_entity.STATION_NAME;
                p_key_part_infor.QUANTITY = Convert.ToInt32(key_part_entity.QUANTITY); //为实体增加安全件数量信息

                #region 设备加工记录信息
                //doc_equip_status = EntityToEntity<DOC_EQUIP_STATUS, P_KEY_PART_INFOR>.EntityChange(doc_equip_status, p_key_part_infor);
                //doc_equip_status = plan_product_data.EntityMapper<P_ASSEMBLE_PRODUCT_STATE, DOC_EQUIP_STATUS>();//这条语句主要是为了获取bom_key
                #endregion

                #region 不合格品信息
                p_notok_product_infor = p_key_part_infor.EntityMapper<P_KEY_PART_INFOR, P_NOTOK_PRODUCT_INFOR>();
                p_notok_product_infor = plan_product_data.EntityMapper<P_ASSEMBLE_PRODUCT_STATE, P_NOTOK_PRODUCT_INFOR>(); //这条语句主要是为了获取bom_key
                #endregion
                //}
                //else
                //{
                //    MyMsgBox.Show("网络异常", "异常", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error);
                //}
                return p_key_part_infor;
            }
            catch (Exception ex)
            {
                SetText(key_part_barcode_txts[i], "提交安全件信息失败！");
                SetText(keypartbarcode_lbl, "条码错误");
                SetColor(keypartbarcode_lbl, Color.Red);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("提交安全件信息失败！" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
                return null;
            }
        }
        #endregion

        #region 正在绑定——扣减线边库数量
        /// <summary>
        /// 扣减线边库数量并进行库存预警
        /// </summary>
        /// <param name="i">当前工位可能需要绑定多个安全件【编号为0,1,2,3……】，i代表此次扫描的安全件条码对应的需要采集的安全件</param>
        /// <returns></returns>
        public int ReduceQuantity(MATERIAL_WC_PART material_wc_part, int i)
        {
            try
            {
                int IsOk = 0;
                if (ServerCommunicationState)
                {
                    string part_key = KeyPartData[i].PART_KEY;//安全件key
                    int quantity = Convert.ToInt32(KeyPartData[i].QUANTITY); //本工位需要消耗的安全件数量信息
                    MATERIAL_WC_PART material_wc_part_dt = KeyPartGetData.GetMaterialWcPart(mainstationkey, part_key);
                    if (material_wc_part_dt != null)
                    {
                        //material_wc_part = EntityHelper<Material_WC_Part>.GetPartEntity(material_wc_part,material_wc_part_dt);
                        material_wc_part = material_wc_part_dt;
                        material_wc_part.STORAGE_NUM -= quantity;
                    }
                    else
                    {
                        //SetText(key_part_barcode_txts[i], "获取线边库库存信息失败！请重试。");
                        //SetColor(keypartbarcode_lbl, Color.Red);
                    }
                }
                else
                {
                    MyMsgBox.Show("网络异常", "异常");
                }
                return IsOk;
            }
            catch (Exception ex)
            {
                SetText(key_part_barcode_txts[i], "扣减线边库数量失败！");
                SetColor(keypartbarcode_lbl, Color.Red);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("扣减线边库数量失败！" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
                return 0;
            }
        }

        #endregion
        #endregion

        #region 根据产品出生证，得到产品信息和需要采集的安全件信息，并加以显示
        private int Data()
        {
            try
            {
                if (ServerCommunicationState)
                {
                    plan_product_data = KeyPartGetData.GetPlanDatabyProductBornCode(product_born_code_txt.Text);
                    if (plan_code != null && plan_product_data.MES_PLAN_CODE != plan_code)//若所属产品计划切换时，做提示,非登录程序后第一次读取产品出生证
                    {
                        MyMsgBox.Show("已切换到新计划" + plan_product_data.MES_PLAN_CODE + "!", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information,5);
                        int res = Handle_plan_product_data();//显示界面信息(计划，安全件等信息)
                        if (res == 0)
                        {
                            return 0;
                        }
                    }
                    else//登录程序后第一次获取产品出生证，不做判断
                    {
                        int res = Handle_plan_product_data();//显示界面信息(计划，安全件等信息)
                        if (res == 0)
                        {
                            return 0;
                        }
                    }
                    return 1;
                }
                else
                {
                    MyMsgBox.Show("网络异常", "异常", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error,3);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                SetText(product_born_code_txt, "产品出生证输入不正确！请重试。");
                SetColor(productborncode_lbl, Color.Red);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("产品出生证输入不正确！请重试。" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
                return 0;
            }
        }

        public int Handle_plan_product_data()
        {
            try
            {
                PlanShow(plan_product_data);//显示计划信息
                if (plan_product_data != null)
                {
                    //将待采集安全件数据查询改进，使之从select变为查询
                    //List<P_KEY_PART_C> need_key_part = key_part_c.FindAll(t => t.MES_PLAN_KEY == plan_product_data.MES_PLAN_KEY && t.STATION_KEY == basicinfor.STATION_KEY.ToString() && t.PRODUCT_KEY == plan_product_data.PRODUCT_KEY && t.QUANTITY != null && t.QUANTITY > 0).ToList();
                    ////KeyPartGetData.GetKeyPartConfig(StationCodeList, mes_plan_key);
                    //if (need_key_part == null || need_key_part.Count == 0) //如果本地查询不到，即从服务器同步数据
                    //{
                        //List<P_KEY_PART_C> need_key_part = SynchronizeKey_Part_C(StationList.Select(s => s.STATION_CODE).ToList(), plan_product_data.MES_PLAN_KEY);
                    List<P_KEY_PART_C> need_key_part = KeyPartGetData.GetKeyPartConfig(StationList.Select(s => s.STATION_CODE).ToList(), plan_product_data.MES_PLAN_KEY);
                    //}
                    #region 处理原始安全件数据
                    KeyPartData = new List<P_KEY_PART_C>();//清空待采集集合
                    foreach (var item in need_key_part)
                    {
                        //for (int i = 0; i < item.QUANTITY; i++)
                        //{
                            P_KEY_PART_C newentity = new P_KEY_PART_C();
                            newentity = EntityHelper.EntityHelper.EntityMapper(item, newentity);
                            //newentity.QUANTITY = 1;//分解后数量均为1
                            newentity.RESERVE3 = "0";//初始均为未加载
                            //if(KeyPartData.Where(s=>s.KEY_PART_C_KEY== newentity.KEY_PART_C_KEY).Count()==0)
                            //{
                                KeyPartData.Add(newentity);
                            //}
                        //}
                    }
                    string[] orderArr1 = new string[] { "否", "是" };
                    KeyPartData = KeyPartData.OrderBy(e => {
                        var i = 0; i = Array.IndexOf(orderArr1, e.RESERVE1); if (i != -1) { return i; }
                        else
                        {
                            return int.MaxValue;
                        }
                    }).ThenBy(s => s.PART_CODE).ToList();//先按是否解析，再按物料号排序
                    //KeyPartData = KeyPartData.OrderBy(s => s.PART_CODE).ToList();//按物料号排序显示
                    #region 显示安全件采集进度
                    SetCollectProgress(finish_lbl, KeyPartData.Count, 0);
                    #endregion

                    #endregion
                    KeyPartShow();//显示需要采集的安全件信息

                    #region 若待采集数量为0，则对PLC写入允许放行信号,表示该工位不需要扫码
                    if (KeyPartData.Count == 0)
                    {
                        CONTROL_ADDRESS is_leave_ok = ControlAddressList.FirstOrDefault(s => s.ADDRESS_TYPE == ((int)AddressCategory.is_leaveOK).ToString());
                        if (is_leave_ok != null)
                        {
                            OPCHelper<JAC_KEYPART>.SynWriteOpcItem(is_leave_ok.ADDRESS_PATH, "1");//写入允许放行
                            SetColor(allowrelease_lbl, HasCollectBtnColor);//生产状态的“允许放行”按钮颜色改变为绿色
                        }
                    }
                    #endregion
                    return 1;
                }
                else
                {
                    MyMsgBox.Show("产品未加工或已加工完成。", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information,10);
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region 界面显示产品信息
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt">显示的数据</param>
        public void PlanShow(P_ASSEMBLE_PRODUCT_STATE entity)
        {
            try
            {
                if (entity != null)
                {
                    //此处判断控件是否被创建它的线程调用，如果是（InvokeRequired=false）直接赋值，否则（InvokeRequired=true）调用SetTextDelegate方法，通过委托进行跨线程调用
                    SetText(plan_code_txt, entity.MES_PLAN_CODE);//显示计划编号
                    SetText(product_code_txt, entity.PRODUCT_CODE);//显示产品出生证
                    //SetText(product_name_txt, entity.PRODUCT_NAME);//显示产品名称
                    SetText(product_batch_no_txt, entity.PRODUCT_MODEL_NO.ToString());//显示产品型号
                    plan_code = entity.MES_PLAN_CODE;//记录当前计划编号信息，用于计划切换时提示
                }
                else
                {
                    SetText(plan_code_txt, "");//显示计划编号
                    SetText(product_code_txt, "");//显示产品出生证
                   // SetText(product_name_txt, "");//显示产品名称
                    SetText(product_batch_no_txt, "");//显示产品型号
                }
            }
            catch (Exception ex)
            {
                SetText(product_born_code_txt, "该产品信息不完整！请重试。");
                SetColor(productborncode_lbl, Color.Red);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("该产品信息不完整！请重试。" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        #endregion

        #region 界面显示需要采集的安全件个数和信息

        public void KeyPartShow()
        {
            try
            {
                if (KeyPartData != null)
                {
                    for (int i = 0; i < KeyPartData.Count; i++)//根据需要采集的安全件的种类数（i）,显示出相应的安全件信息采集控件
                    {
                        if (i < 5)//最多五个
                        {
                            SetVisible(key_part_name_txts[i], true);//显示安全件名称text控件
                            SetText(key_part_name_txts[i], KeyPartData[i].PART_NAME);//安全件名称text控件赋值

                            SetVisible(key_part_code_txts[i], true);//显示安全件编号text控件
                            SetText(key_part_code_txts[i], KeyPartData[i].PART_CODE);//安全件编号text控件赋值

                            SetVisible(key_part_barcode_txts[i], true);//显示安全件条码text控件

                            SetVisible(confirm_part_btns[i], true);//显示OK  button控件

                            SetVisible(key_part_name_lbls[i], true);//显示安全件名称label控件

                            SetVisible(key_part_code_lbls[i], true);//显示安全件编号label控件

                            SetVisible(key_part_barcode_lbls[i], true);//显示安全件条码label控件

                            KeyPartData[i].RESERVE3 = "1";//该安全件已被加载到界面上，预留字段1属性值改为1
                            //this.Invoke(new Action(() => key_part_rectangleShapes[i].Visible = true));//通过委托改变其可见性
                        }
                    }
                }
                else
                {
                    MyMsgBox.Show("产品未加工或已加工完成。", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information,10);
                }
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("安全件数据加载失败：" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        #endregion

        #region 扫描枪获取并显示数据

        /// <summary>
        /// 从扫描枪中读取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scanport_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                lock (locker)//防止重复扫码导致程序卡死
                {
                    if (ServerCommunicationState == true)
                    {
                        ScanportDataReceive(scanport);
                    }
                    else
                    {
                        MyMsgBox.Show("网络异常，请重试。", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information, 3);
                    }
                }
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("扫描枪获取数据时出现错误：" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
            finally
            {
                scanport.DiscardInBuffer();//清理输入缓冲区
                scanport.DiscardOutBuffer();//清理输出缓冲区
            }
        }

        public void ScanportDataReceive(SerialPort ScannerSerialPort)
        {
            Stopwatch sw_all = new Stopwatch();
            sw_all.Start();
            
            
            string barcodetype = "";
            string barcode = "";
            bool IsOk = false;

            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            object obj = ScannerHelper.dataReceive(scanport, out barcodetype, out IsOk);
            sw1.Stop();
            long ew1 = sw1.ElapsedMilliseconds;
            Log.GetInstance.WriteLog("读取解析条码时间"+ obj+"/"+ew1.ToString());
            CheckForIllegalCrossThreadCalls = false;
            barcode = obj.ToString().Trim();
            if (inputmodel_part_btn.Text == "手动输入" && !string.IsNullOrEmpty(barcode))//只有在显示为扫描输入模式下（Text == "手动输入"），扫描安全件条码才有效
            {
                if (product_code_txt.Text == "" && product_batch_no_txt.Text == "" && plan_code_txt.Text == "")
                {//没有输入产品信息，或者输入产品信息错误，两种情况下，产品信息均不能顺利成功加载，即全部为空
                    SetText(product_born_code_txt, "请先输入正确产品信息。");
                    SetColor(productarrival_lbl, Color.Red);
                }
                else
                {
                    if (string.IsNullOrEmpty(barcode))
                    {
                        return;
                    }

                    Stopwatch sw2 = new Stopwatch();
                    sw2.Start();
                    #region 从前往后判断目前待匹配的是第几个待采集信息
                    int m = 0;//在以下逻辑循环后则可获取m的值
                    for (m = 0; m < key_part_code_txts.Length; m++)//key_part_codes为安全件编号输入框的集合，循环判断扫描的条形码是属于哪一个安全件的
                    {
                        //输入条形码的text文本框可见，点击确定的按钮控件（OK）可操作，且安全件理论条形码与扫描条形码匹配
                        if (confirm_part_btns[m].Visible == true)//点击确定的按钮控件（OK）可见：代表这个安全件需要采集
                        {
                            if (confirm_part_btns[m].BackColor != HasCollectBtnColor)//点击确定的按钮控件（OK）可操作：代表这个安全件还没有被采集
                            {
                                break;
                            }
                        }
                    }
                    #endregion
                    P_KEY_PART_C current_p_keypart_c = KeyPartData.FirstOrDefault(s => s.PART_CODE == key_part_code_txts[m].Text.Trim());
                    int i = 0;
                    int a = 0;
                    if (current_p_keypart_c.RESERVE1 == "否")//不需要校解析规则的图号，需要校验是否符合长度限制要求，符合长度要求则通过
                    {
                        string limit = current_p_keypart_c.RESERVE2;
                        if (limit.Split('-').Length != 2 || int.TryParse(limit.Split('-')[0], out a) == false || int.TryParse(limit.Split('-')[1], out a) == false)//若未维护长度限制或值不符合(a-a)格式，均不校验长度，直接绑定
                        {
                            i = m;
                        }
                        else
                        {
                            if (barcode.Length >= int.Parse(limit.Split('-')[0]) && barcode.Length <= int.Parse(limit.Split('-')[1]))//符合长度限制，匹配成功
                            {
                                i = m;
                            }
                            else//不符合长度限制，返回错误代号
                            {
                                i = 5;
                            }
                        }
                    }
                    else//需要校验规则的进入原先逻辑
                    {
                        i = CheckBarcodeNum(barcode);
                    }
                    sw2.Stop();
                    long ew2 = sw2.ElapsedMilliseconds;
                    Log.GetInstance.WriteLog("匹配待采集信息时间/" + i.ToString() +"/"+ ew2.ToString());

                    if (i > 4)//如果i>4，即条形码与需要采集的安全件不匹配
                    {
                        for (int j = 0; j < key_part_barcode_txts.Length; j++)
                        {
                            //在扫描枪输入的情况下：所有确定按钮均不可交互
                            if (confirm_part_btns[j].Visible == true && confirm_part_btns[j].BackColor != HasCollectBtnColor)
                            {
                                SetText(key_part_barcode_txts[j], "安全件扫描错误！请重试。");
                                SetText(keypartbarcode_lbl, "条码错误");
                                SetColor(keypartbarcode_lbl, Color.Red);
                                break;
                            }
                        }
                    }
                    else if (i == -100)
                    {
                        if (!string.IsNullOrEmpty(product_born_code_txt.Text.Trim()) && KeyPartData.Count == 0 && ServerCommunicationState == false)//离线且界面无任务待采集安全件信息
                        {
                            for (int j = 0; j < key_part_barcode_txts.Length; j++)
                            {
                                if (key_part_barcode_txts[j].Visible == true && key_part_barcode_txts[j].Text.Trim() == barcode)//重复物料条码视为无效数据，不做任何记录
                                {
                                    break;
                                }
                                if (key_part_barcode_txts[j].Visible == false)
                                {
                                    SetVisible(key_part_name_txts[j], true);//显示安全件名称text控件

                                    SetVisible(key_part_code_txts[j], true);//显示安全件编号text控件

                                    SetVisible(key_part_barcode_txts[j], true);//显示安全件条码text控件

                                    SetVisible(confirm_part_btns[j], true);//显示OK  button控件

                                    SetVisible(key_part_name_lbls[j], true);//显示安全件名称label控件

                                    SetVisible(key_part_code_lbls[j], true);//显示安全件编号label控件

                                    SetVisible(key_part_barcode_lbls[j], true);//显示安全件条码label控件

                                    SetText(key_part_barcode_txts[j], barcode);
                                    collectedcount += 1;
                                    //SetLableText(collectprocess_lbl, (int.Parse(collectprocess_lbl.Text.Trim().Split('/')[0]) + 1).ToString() + "/" + KeyPartData.Count);
                                    SetCollectProgress(finish_lbl, KeyPartData.Count, collectedcount);
                                    #region (断网，且无待采集数据)绑定产品出生证，工位，及安全件条码等信息，直接将扫描信息缓存至本地SQLite
                                    P_KEY_PART_INFOR keypartinfoentity = new P_KEY_PART_INFOR();
                                    keypartinfoentity.Create();
                                    keypartinfoentity.ASSMBLY_TIME = ServerTime.Now;
                                    keypartinfoentity.STATION_KEY = StationList.Where(s => s.IS_MAIN == "是").Select(s => s.STATION_KEY).FirstOrDefault();
                                    keypartinfoentity.STATION_CODE = StationList.Where(s => s.IS_MAIN == "是").Select(s => s.STATION_CODE).FirstOrDefault();
                                    keypartinfoentity.STATION_NAME = StationList.Where(s => s.IS_MAIN == "是").Select(s => s.STATION_NAME).FirstOrDefault();
                                    keypartinfoentity.PRODUCT_BORN_CODE = product_born_code_txt.Text.Trim();//产品出生证
                                    keypartinfoentity.PART_BARCODE = barcode;//物料条码
                                    keypartinfoentity.PART_VINCODE = barcode;
                                    keypartinfoentity.RESERVE3 = "0";//预留字段1作为本地缓存待同步信息0-未同步；1-已同步
                                    keypartinfoentity.RESERVE2 = "0";//预留字段2作为区分直接插入数据还是需校验插入数据0-需校验；1-直接插入
                                    P_KEY_PART_INFORRepositoryFactory.Repository(DatabaseType.SQLite).Insert(keypartinfoentity);
                                    #endregion
                                    break;
                                }
                            }
                        }
                    }
                    else//找到匹配的安全件，可以开始绑定安全件信息
                    {
                        //Log.GetInstance.WriteLog("Input", "扫描零部件编号：" + barcode);
                        SetText(key_part_barcode_txts[i], barcode);
                        Task.Run(() =>
                        {
                            Stopwatch sw3 = new Stopwatch();
                            sw3.Start();

                            int Is_Ok = BindKeyPartInfor(barcode, i);
                            if (Is_Ok != 1)
                            {
                                SetText(key_part_barcode_txts[i], "安全件绑定失败！请重试。");
                                SetText(keypartbarcode_lbl, "条码错误");
                                SetColor(keypartbarcode_lbl, Color.Red);
                            }

                            sw3.Stop();
                            long ew3 = sw3.ElapsedMilliseconds;
                            Log.GetInstance.WriteLog("提交采集信息时间/" + i.ToString() + "/" + ew3.ToString());
                            
                        });
                    }
                }
            }
            else
            {
                //InitializationForm();
                for (int j = 0; j < key_part_barcode_txts.Length; j++)
                {
                    if (confirm_part_btns[j].Visible == true && confirm_part_btns[j].Enabled == true && confirm_part_btns[j].BackColor != HasCollectBtnColor)
                    {
                        SetText(key_part_barcode_txts[j], "条形码输入模式不正确！请重新选择。");
                        SetText(keypartbarcode_lbl, "条码错误");
                        SetColor(keypartbarcode_lbl, Color.Red);
                        break;
                    }
                }
            }
            //}
            //}
            //else
            //{
            //    MessageBox.Show("网络异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            sw_all.Stop();
            long ew_all = sw_all.ElapsedMilliseconds;
            Log.GetInstance.WriteLog(barcode+"扫码处理总时间：" + ew_all.ToString());
        }
        #endregion

        #region 使用委托进行跨线程处理控件
        /// <summary>
        /// 使用委托为组件赋值
        /// </summary>
        /// <param name="txt">控件名</param>
        /// <param name="st">要赋值的字符串</param>
        private delegate void SetTextDelegate(Control control, string st);
        private void SetText(Control control, string st)
        {
            //string ID = Thread.CurrentThread.ManagedThreadId.ToString();//获取线程ID，可以进行跨线程调试
            if (control.InvokeRequired) Invoke(new SetTextDelegate(SetText), control, st);
            else control.Text = st;
        }
        /// <summary>
        /// 使用委托为控件赋值
        /// </summary>
        /// <param name="component">组件名</param>
        /// <param name="bools">组件是否可见</param>
        //private delegate void SetVisibleDelegateComponent(IComponent component, bool bools);
        //private void SetVisibleComponent(Shape control, bool bools)
        //{
        //    if (control.InvokeRequired) Invoke(new SetVisibleDelegateComponent(SetVisible), control, bools);
        //    else control.VE:\1#纳威司达项目\安全件采集\KEY_PART_INFOR\ClassStore\isible = bools;
        /// </summary>        //}
        /// <summary>
        /// 使用委托为Label赋值

        /// <param name="pb">Label控件名</param>
        /// <param name="st">要赋值的字符串</param>
        private delegate void SetLableTextDelegate(Label pb, string st);
        private void SetLableText(Label lbl, string st)
        {
            //string ID = Thread.CurrentThread.ManagedThreadId.ToString();//获取线程ID，可以进行跨线程调试
            if (lbl.InvokeRequired) Invoke(new SetLableTextDelegate(SetLableText), lbl, st);
            else lbl.Text = st;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="control">要更改属性的控件名</param>
        /// <param name="bools">将控件的可见属性值改为bools，bools为true或者false</param>
        private delegate void SetVisibleDelegate(Control control, bool bools);
        private void SetVisible(Control control, bool bools)
        {
            //string ID = Thread.CurrentThread.ManagedThreadId.ToString();//获取线程ID，可以进行跨线程调试
            if (control.InvokeRequired) Invoke(new SetVisibleDelegate(SetVisible), control, bools);
            else control.Visible = bools;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="control">控件名</param>
        /// <param name="bools">控件Enabled属性更改后值</param>
        private delegate void SetEnabledDelegate(Control control, bool bools);
        private void SetEnabled(Control control, bool bools)
        {
            //string ID = Thread.CurrentThread.ManagedThreadId.ToString();//获取线程ID，可以进行跨线程调试
            if (control.InvokeRequired) Invoke(new SetEnabledDelegate(SetEnabled), control, bools);
            else control.Enabled = bools;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="control">控件名</param>
        /// <param name="color">控件更改后的颜色</param>
        private delegate void SetColorDelegate(Control control, Color color);
        private void SetColor(Control control, Color color)
        {
            //string ID = Thread.CurrentThread.ManagedThreadId.ToString();//获取线程ID，可以进行跨线程调试
            if (control.InvokeRequired) Invoke(new SetColorDelegate(SetColor), control, color);
            else control.BackColor = color;
        }
        /// <summary>
        /// 设置图片
        /// </summary>
        /// <param name="control">控件名</param>
        /// <param name="str">图片文件</param>
        private delegate void SetPictureDelegate(PictureBox control, Stream str);
        private void SetPicture(PictureBox control, Stream str)
        {
            //string ID = Thread.CurrentThread.ManagedThreadId.ToString();//获取线程ID，可以进行跨线程调试
            if (control.InvokeRequired) Invoke(new SetPictureDelegate(SetPicture), control, str);
            else control.Image = System.Drawing.Image.FromStream(str);
        }
        /// <summary>
        /// 设置采集进度
        /// </summary>
        /// <param name="lbl">控件名</param>
        /// <param name="all">总采集个数</param>
        /// <param name="col">已采集个数</param>
        private delegate void SetCollectProgressDelegate(Label lbl, int all, int col);
        private void SetCollectProgress(Label lbl, int all, int col)
        {
            //string ID = Thread.CurrentThread.ManagedThreadId.ToString();//获取线程ID，可以进行跨线程调试
            if (lbl.InvokeRequired) Invoke(new SetCollectProgressDelegate(SetCollectProgress), lbl, all, col);
            else
            {
                lbl.Text = "采集完成(" + col.ToString() + "/" + all.ToString() + ")";
            }
        }
        public delegate void MessageBoxHandler();
        public void aaa()
        {
            this.Invoke(new MessageBoxHandler(delegate () { MessageBox.Show("千一网络"); }));// MessageBox.Show((IWin32Window)this, "提示"); // 由于放在 Invoke 中，也可以这么用，但效果和上面的一样。}))
        }
        #endregion

        #region 判断扫描枪扫描的条码是第几个需要采集的数据（假设有>=1个）
        public int CheckBarcodeNum(string barcode)
        {
            try
            {
                int i = 0;
                //allcollected：初始假设需要采集的安全件已全部采集，只有在循环结束（i=4）且allcollected =true的情况下，才能证明安全件被全部采集，其余情况不能证明全部采集
                bool allcollected = true;//需要采集的安全件信息(假设有四个安全件信息需要采集)是否已被全部采集
                //Log.GetInstance.WriteLog("barcode:"+barcode);
                for (i = 0; i < key_part_code_txts.Length; i++)//key_part_codes为安全件编号输入框的集合，循环判断扫描的条形码是属于哪一个安全件的
                {
                    //输入条形码的text文本框可见，点击确定的按钮控件（OK）可操作，且安全件理论条形码与扫描条形码匹配
                    if (confirm_part_btns[i].Visible == true)//点击确定的按钮控件（OK）可见：代表这个安全件需要采集
                    {
                        if (confirm_part_btns[i].BackColor != HasCollectBtnColor)//点击确定的按钮控件（OK）可操作：代表这个安全件还没有被采集
                        {
                            if (wc_code.Contains("2.7"))//判断是否满足采集条件(2.7)
                            {
                                if(key_part_code_txts[i].Text.Trim() == barcode.Substring(14, barcode.Length - 14))
                                break;
                            }
                            else if (wc_code.Contains("4.0") )//判断是否满足采集条件(4.0)
                            {
                               if(key_part_code_txts[i].Text == barcode.Substring(1, 7))
                               break;
                            }
                            allcollected = false;//一旦发现有未采集的，使allcollected变为false
                        }
                    }
                }
                if (i == 5 && allcollected == true) //如果全部循环一遍，仍然没有出现：需要采集但在本次采集仍没有被采集的安全件，则认为安全件已被全部采集,或扫描条码不在待采集队列中
                {//allcollected：循环结束仍没发现有未采集的，则认为已经全部采集
                    i = -1;//代表安全件已经被采集完全，本次扫描无效
                }
                if (ServerCommunicationState == false && KeyPartData.Count == 0)//离线且界面不存在待采集信息时
                {
                    i = -100;
                }
                return i;
            }
            catch
            {
                for (int j = 0; j < key_part_barcode_txts.Length; j++)
                {
                    if (confirm_part_btns[j].Visible == true && confirm_part_btns[j].Enabled == true && key_part_barcode_txts[j].Text == "")
                    {
                        SetText(key_part_barcode_txts[j], "条形码解析错误！请重试。");
                        SetText(keypartbarcode_lbl, "条码错误");
                        SetColor(keypartbarcode_lbl, Color.Red);
                        break;
                    }
                }
                return 5;
            }
        }
        #endregion

        #region 界面恢复初始状态

        private void InitializationForm(bool resetcollectedcount)
        {
            try
            {
                if (resetcollectedcount == true)
                {
                    collectedcount = 0;
                }

                SetText(product_born_code_txt, "");
                SetText(product_code_txt, "");
                SetText(product_batch_no_txt, "");
                SetText(plan_code_txt, "");
                SetEnabled(product_born_code_txt, false);

                SetText(key_part_name_txt, "");
                SetText(key_part_code_txt, "");
                SetText(key_part_barcode_txt, "");
                SetEnabled(confirm_part_btn, false);
                SetColor(confirm_part_btn, NotCollectBtnColor);
                SetVisible(key_part_name_lbl, false);
                SetVisible(key_part_code_lbl, false);
                SetVisible(key_part_barcode_lbl, false);
                SetVisible(key_part_name_txt, false);
                SetVisible(key_part_code_txt, false);
                SetVisible(key_part_barcode_txt, false);
                SetVisible(confirm_part_btn, false);
                SetEnabled(key_part_barcode_txt, false);

                SetText(key_part_name2_txt, "");
                SetText(key_part_code2_txt, "");
                SetText(key_part_barcode2_txt, "");
                SetEnabled(confirm_part2_btn, false);
                SetColor(confirm_part2_btn, NotCollectBtnColor);
                SetVisible(key_part_name2_lbl, false);
                SetVisible(key_part_code2_lbl, false);
                SetVisible(key_part_barcode2_lbl, false);
                SetVisible(key_part_name2_txt, false);
                SetVisible(key_part_code2_txt, false);
                SetVisible(key_part_barcode2_txt, false);
                SetVisible(confirm_part2_btn, false);
                SetEnabled(key_part_barcode2_txt, false);

                SetText(key_part_name3_txt, "");
                SetText(key_part_code3_txt, "");
                SetText(key_part_barcode3_txt, "");
                SetEnabled(confirm_part3_btn, false);
                SetColor(confirm_part3_btn, NotCollectBtnColor);
                SetVisible(key_part_name3_lbl, false);
                SetVisible(key_part_code3_lbl, false);
                SetVisible(key_part_barcode3_lbl, false);
                SetVisible(key_part_name3_txt, false);
                SetVisible(key_part_code3_txt, false);
                SetVisible(key_part_barcode3_txt, false);
                SetVisible(confirm_part3_btn, false);
                SetEnabled(key_part_barcode3_txt, false);

                SetText(key_part_name4_txt, "");
                SetText(key_part_code4_txt, "");
                SetText(key_part_barcode4_txt, "");
                SetEnabled(confirm_part4_btn, false);
                SetColor(confirm_part4_btn, NotCollectBtnColor);
                SetVisible(key_part_name4_lbl, false);
                SetVisible(key_part_code4_lbl, false);
                SetVisible(key_part_barcode4_lbl, false);
                SetVisible(key_part_name4_txt, false);
                SetVisible(key_part_code4_txt, false);
                SetVisible(key_part_barcode4_txt, false);
                SetVisible(confirm_part4_btn, false);
                SetEnabled(key_part_barcode4_txt, false);

                SetText(key_part_name5_txt, "");
                SetText(key_part_code5_txt, "");
                SetText(key_part_barcode5_txt, "");
                SetEnabled(confirm_part5_btn, false);
                SetColor(confirm_part5_btn, NotCollectBtnColor);
                SetVisible(key_part_name5_lbl, false);
                SetVisible(key_part_code5_lbl, false);
                SetVisible(key_part_barcode5_lbl, false);
                SetVisible(key_part_name5_txt, false);
                SetVisible(key_part_code5_txt, false);
                SetVisible(key_part_barcode5_txt, false);
                SetVisible(confirm_part5_btn, false);
                SetEnabled(key_part_barcode5_txt, false);

                SetText(inputmodel_product_btn, "手动输入");
                SetText(inputmodel_part_btn, "手动输入");

                SetColor(productarrival_lbl, SystemColors.ControlDark);
                SetColor(productborncode_lbl, SystemColors.ControlDark);
                SetText(keypartbarcode_lbl, "条码正确");
                SetColor(keypartbarcode_lbl, SystemColors.ControlDark);
                SetColor(finish_lbl, SystemColors.ControlDark);
                SetColor(allowrelease_lbl, SystemColors.ControlDark);
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("界面恢复初始状态时失败：" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        #endregion

        #region 时间显示(Task)
        private void RecordTimeShow()
        {
            while (true)
            {
                try
                {
                    DateTime dt = ServerTime.Now;
                    string datetime = "时间" + ':' + dt.Year.ToString().Trim() + "年" + dt.Month.ToString("00").Trim() + "月" + dt.Day.ToString("00").Trim() + "日" + "  " + dt.Hour.ToString("00").Trim() + ":" + dt.Minute.ToString("00").Trim() + ":" + dt.Second.ToString("00").Trim();
                    //string datetime = dt.ToShortDateString().Trim() + " " + dt.ToLongTimeString().Trim();
                    SetText(showtime_lbl, datetime);

                    #region 每天12点将加工数量置为0

                    if (dt.Hour == 0 && dt.Minute == 0)
                    {
                        SetText(all_num_txt, "0");
                        SetText(ok_num_txt, "0");
                        SetText(notok_num_txt, "0");
                        SetText(continued_notok_num_txt, "0");
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                    log.Fatal("时间显示时出现错误：" + ex.Message);
                    ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
                }
                Thread.Sleep(1000);
            }
        }

        #endregion

        #region 显示服务器通讯信号

        private bool ServerCommunicationState = false;//服务器通讯状态
        private bool needRefresh = true;//需要刷新服务器通信状态
        
        private readonly string ServerIP = ConfigurationManager.AppSettings["ServerIP"].ToString().Trim();//服务器的IP地址

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
            try
            {
                if (needRefresh)
                {
                    ShowServerConnectionState();
                }
            }
            catch(Exception ex)
            {

            }
        }
        private void lineconnection_trm_Tick(object sender, EventArgs e)
        {
            try
            {
                ShowLineConnectionState();
            }
            catch (Exception ex)
            {

            }
        }
        public void SetLblColors(Label lbl, Color backcolor)
        {
            if (lbl == null) return;
            if (lbl.IsDisposed || !lbl.Parent.IsHandleCreated) return;
            lbl.BeginInvoke(new MethodInvoker(() =>
            {
                lbl.BackColor = backcolor;
            }));
        }
        public void ShowServerConnection(bool state)
        {
            try
            {
                if (state)
                {
                    //SetLblColors(serverconnectionstate_lbl, HasCollectBtnColor);
                    serverconnectionstate_pulseButton.Set_ColorBottom(HasCollectBtnColor);
                    serverconnectionstate_pulseButton.Set_ColorTop(HasCollectBtnColor);
                    serverconnectionstate_pulseButton.Set_ForeColor(HasCollectBtnColor);
                    serverconnectionstate_pulseButton.Set_PulseColor(HasCollectBtnColor);

                }
                else
                {
                    //SetLblColors(serverconnectionstate_lbl, Color.Red);
                    serverconnectionstate_pulseButton.Set_ColorBottom(Color.Red);
                    serverconnectionstate_pulseButton.Set_ColorTop(Color.Red);
                    serverconnectionstate_pulseButton.Set_ForeColor(Color.Red);
                    serverconnectionstate_pulseButton.Set_PulseColor(Color.Red);
                }
            }
            catch
            {

            }
        }
        #endregion

        #region 显示线体通信信号

        private bool LineCommunicationState = false;//线体通讯状态
        private bool needRefreshLine = true;//需要刷新线体通信状态

        //private readonly string ServerIP = DBhelperOracle.dataSource;//服务器的IP地址

        private delegate void ShowWorkstationLineStateDelegate();
        private void ShowLineConnectionState()
        {
            //CONTROL_ADDRESS pro_born_address = ControlAddressList.FirstOrDefault(s => s.ADDRESS_TYPE == ((int)AddressCategory.allet_arrive).ToString());//找到停止器对应的产品出生证地址
            //while (true)
            //{
            //    try
            //    {
            //        string result = OPCHelper<JAC_KEYPART>.SynReadOpcItem(pro_born_address.ADDRESS_PATH);
            //        if (result != null && (result == "True" || result == "False"))
            //        {

            //            LineCommunicationState = true;
            //            ShowLineConnection(true);

            //        }
            //        else
            //        {
            //            LineCommunicationState = false;
            //            ShowLineConnection(false);
            //        }
            //    }
            //    catch
            //    {
            //        ShowLineConnection(false);
            //    }
            //    Thread.Sleep(3000);
            //}
            if (this.InvokeRequired)
            {
                ShowWorkstationLineStateDelegate d = new ShowWorkstationLineStateDelegate(ShowLineConnectionState);
                this.Invoke(d, new object[] { });
            }
            else
            {
                try
                {
                    CONTROL_ADDRESS pro_born_address = ControlAddressList.FirstOrDefault(s => s.ADDRESS_TYPE == ((int)AddressCategory.allet_arrive).ToString());//找到停止器对应的产品出生证地址
                    string result = OPCHelper<JAC_KEYPART>.SynReadOpcItem(pro_born_address.ADDRESS_PATH);
                    if (result != null && (result == "True" || result == "False"))
                    {

                        LineCommunicationState = true;
                        ShowLineConnection(true);
                    }
                    else
                    {
                        LineCommunicationState = false;
                        ShowLineConnection(false);
                    }
                }
                catch (Exception ex)
                {
                    LineCommunicationState = false;
                    ShowLineConnection(false);
                }
            }
        }
        public void ShowLineConnection(bool state)
        {
            try
            {
                if (state)
                {
                    //SetLblColors(serverconnectionstate_lbl, HasCollectBtnColor);
                    equipstate_pulseButton.Set_ColorBottom(HasCollectBtnColor);
                    equipstate_pulseButton.Set_ColorTop(HasCollectBtnColor);
                    equipstate_pulseButton.Set_ForeColor(HasCollectBtnColor);
                    equipstate_pulseButton.Set_PulseColor(HasCollectBtnColor);

                }
                else
                {
                    //SetLblColors(serverconnectionstate_lbl, Color.Red);
                    equipstate_pulseButton.Set_ColorBottom(Color.Red);
                    equipstate_pulseButton.Set_ColorTop(Color.Red);
                    equipstate_pulseButton.Set_ForeColor(Color.Red);
                    equipstate_pulseButton.Set_PulseColor(Color.Red);
                }
            }
            catch
            {

            }
        }
        #endregion

        #region 窗体最大最小化,关闭按钮

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }
        private void panel16_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Normal;
                }
                else
                {
                    this.WindowState = FormWindowState.Maximized;
                }
            }
            catch
            {

            }
        }
        private void size_pic_box_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void Exit_pic_Box_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.Yes == MyMsgBox.Show("是否确定退出系统?", "信息提示", MyMsgBox.MyButtons.YesNo, MyMsgBox.MyIcon.Question))
                {
                    scanport.Dispose();
                    Formclosing();
                }
                    
            }
            catch
            {

            }
        }
        #endregion

        #region andon按钮

        private void andon_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (plan_product_data==null)
                {
                    MyMsgBox.Show("产品信息为空，无法进入Andon操作", "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information,3);
                    return;
                }
                andon = new ANDON(basicinfor, plan_product_data);
                andon.ShowDialog();
            }
            catch
            {

            }
        }
        #endregion

        #region 不合格产品信息
        public void NotOkProductinfor()
        {
            //Repository<P_KEY_PART_INFOR> tran = new Repository<P_KEY_PART_INFOR>();
            DbTransaction isOpenTrans = new Repository<P_KEY_PART_INFOR>().BeginTrans();
            try
            {
                p_notok_product_infor.REMARKS = "安全件采集未完成。";

                //p_notok_product_infor = basicinfor[0].EntityMapper<BasicInfoDto, P_NOTOK_PRODUCT_INFOR>();//为实体增加基本信息
                p_notok_product_infor = EntityHelper.EntityHelper.EntityMapper(basicinfor, p_notok_product_infor);//为实体增加基本信息
                //p_notok_product_infor = EntityHelper<P_NOTOK_PRODUCT_INFOR>.GetPartEntity(p_notok_product_infor, team_shift);//为实体增加班制班组信息
                //p_notok_product_infor = plan_product_data.EntityMapper<P_ASSEMBLE_PRODUCT_STATE, P_NOTOK_PRODUCT_INFOR>();//为实体增加计划、产品信息
                p_notok_product_infor = EntityHelper.EntityHelper.EntityMapper(plan_product_data, p_notok_product_infor);//为实体增加计划、产品信息
                p_notok_product_infor.Create();
                DOC_NOTOK_PRODUCT_INFOR doc_notok_product_infor = EntityToEntity<DOC_NOTOK_PRODUCT_INFOR, P_NOTOK_PRODUCT_INFOR>.EntityChange(p_notok_product_infor);
                P_NOTOK_PRODUCT_INFORRepositoryFactory.Repository().Insert(p_notok_product_infor, isOpenTrans);
                //DOC_NOTOK_PRODUCT_INFORRepositoryFactory.Repository().Insert(doc_notok_product_infor, isOpenTrans);
                isOpenTrans.Commit();
            }
            catch (Exception ex)
            {
                isOpenTrans.Rollback();
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("安全件采集未完成提交失败。" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        #endregion

        #region 设备加工记录信息
        public void EquipProcessingRecord()
        {
            try
            {
                doc_equip_status.ENDDATE = ServerTime.Now;
                doc_equip_status = EntityHelper.EntityHelper.EntityMapper(basicinfor, doc_equip_status);//为实体增加基本信息
                doc_equip_status = EntityHelper.EntityHelper.EntityMapper(plan_product_data, doc_equip_status);//为实体增加计划、产品信息
                doc_equip_status.DURATION = ExecDateDiff(Convert.ToDateTime(doc_equip_status.STARTDATE), Convert.ToDateTime(doc_equip_status.ENDDATE));
                doc_equip_status.Create();
                DOC_EQUIP_STATUSRepositoryFactory.Repository().Insert(doc_equip_status);
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("设备加工记录信息提交失败。" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        #endregion

        #region timespan转换为int型

        public static int ExecDateDiff(DateTime dateBegin, DateTime dateEnd)
        {
            TimeSpan ts1 = new TimeSpan(dateBegin.Ticks);
            TimeSpan ts2 = new TimeSpan(dateEnd.Ticks);
            TimeSpan ts3 = ts1.Subtract(ts2).Duration();
            //你想转的格式
            return (int)ts3.TotalSeconds;
            // return ts3.TotalMilliseconds.ToString();
        }
        #endregion

        #region 显示工位加工数量信息 
        private void ShowWcInfor()
        {
            try
            {
                //SetLableText(wc_code_lbl_1, WorkCenterInfor.Wc_code);
                SetText(all_num_txt, wcinfor.AllNum.ToString());
                SetText(ok_num_txt, wcinfor.OkNum.ToString());
                SetText(notok_num_txt, wcinfor.NotOkNum.ToString());
                SetText(continued_notok_num_txt, wcinfor.ContinueNotOkNum.ToString());
                //SetColor(continued_notok_num_bl, Color.FromArgb(213, 231, 243));
                //if (wcinfor.ContinueNotOkNum > 2)
                //{
                //    SetColor(continued_notok_num_bl, Color.Red);
                //}
            }
            catch
            {

            }
        }
        #endregion

        #region  作业指导文件显示
        private GUIDFILES current_file;//当前显示的文件
        private List<GUIDFILES> list_document = new List<GUIDFILES>();//作业指示文档
        /// <summary>
        /// 加载作业文档
        /// </summary>
        private void LoadGuidFiles()
        {
            try
            {
                //GUIDFILES gf = GetGuidFiles(product_key, mainstationkey);//检索作业文档
                string get = "";
                strpath = "D:\\合资ApiResources\\指导文件.jpg";
                //strpath = "D:\\合资ApiResources\\DB500.pdf";
                get = WebApiHttp.HttpGet($"http://{ServerDictionary.IPCode}/API/WebApiPage/filetobyte?strpath=" + strpath + "");
                //get = WebApiHttp.HttpGet($"http://localhost:28188/API/WebApiPage/filetobyte?strpath=" + strpath + "");
                if (get != null)
                {
                    current_file = get.ToJson<GUIDFILES>();
                }
                if (current_file != null && current_file.document_file != null)//如果有工艺指导文件
                {
                    SetVisible(guidfile_pcl, true);//显示文字指导控件
                    SetVisible(label13, false);//显示文字指导控件
                    ShowGuidFilesdelegate(current_file);//界面显示作业文档
                }
                else//如果没有工艺指导文件
                {
                    SetVisible(guidfile_pcl, false);//显示文字指导控件
                    SetVisible(label13, true);//显示文字指导控件
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 获取工艺指导文件
        /// </summary>
        /// <param name="wckey"></param>
        /// <returns></returns>
        private GUIDFILES GetGuidFiles(string product_key, string wckey)
        {
            GUIDFILES pd = new GUIDFILES();
            try
            {
                DataRow[] files_dr = dt_documents.Select("product_key='" + product_key + "'");
                if (dt_documents != null && files_dr.Length >= 1)
                {
                    list_document.Clear();
                    for (int i = 0; i < files_dr.Length; i++)
                    {
                        GUIDFILES gf = new GUIDFILES();
                        gf.document_code = files_dr[i]["document_code"] != null ? files_dr[i]["document_code"].ToString().Trim() : "";
                        gf.document_name = files_dr[i]["document_name"] != null ? files_dr[i]["document_name"].ToString().Trim() : "";
                        gf.document_file = files_dr[i]["document_file"].ToString() != "" ? (byte[])files_dr[i]["document_file"] : null;
                        gf.document_type = files_dr[i]["document_type"] != null ? files_dr[i]["document_type"].ToString().Trim() : "";
                        list_document.Add(gf);
                    }
                    pd = list_document[0];
                }
                return pd;
            }
            catch (Exception ex)
            {
                MyMsgBox.Show("文档检索失败" + ex.Message, "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error);
                Log.GetInstance.WriteLog(ex.Message);
                return pd;
            }
        }

        private delegate void ShowGuidFilesDelegate(GUIDFILES gf);
        private void ShowGuidFilesdelegate(GUIDFILES gf)
        {
            if (this.InvokeRequired)
            {
                ShowGuidFilesDelegate d = new ShowGuidFilesDelegate(ShowGuidFilesdelegate);
                this.Invoke(d, gf);
            }
            else
            {
                ShowGuidFiles(gf);
            }
        }
        /// <summary>
        /// 界面显示作业文档
        /// </summary>
        private void ShowGuidFiles(GUIDFILES gf)
        {
            try
            {
                if (gf.document_file != null)
                {
                    this.guidfile_pcl.Visible = true;
                    this.guidfile_pcl.Dock = System.Windows.Forms.DockStyle.Fill;
                    //this.guidfile_pcl.Visible = true;
                    Stream ms;
                    this.guidfile_pcl.Controls.Clear();
                    // string file_type = gf.document_type.ToLower();
                    string file_type = gf.document_type;
                    ms = new MemoryStream(gf.document_file);
                    switch (file_type)
                    {
                        case "pdf":
                            PdfViewer _viewer = new PdfViewer();
                            _viewer.Name = "PDF";
                            current_file = gf;
                            _viewer.LoadDocument(ms);
                            _viewer.NavigationPaneVisibility = DevExpress.XtraPdfViewer.PdfNavigationPaneVisibility.Hidden;
                            _viewer.Dock = DockStyle.Fill;
                            _viewer.Cursor = Cursors.Hand;
                            _viewer.ZoomMode = DevExpress.XtraPdfViewer.PdfZoomMode.FitToVisible;
                            _viewer.DoubleClick += new EventHandler(MouseDoubleClick);
                            this.guidfile_pcl.Controls.Add(_viewer);
                            break;
                        case "jpg":
                        case "jepg":
                        case "png":
                        case "gif":
                            Bitmap bmpt = new Bitmap(ms);
                            PictureBox pic = new PictureBox();
                            pic.Name = "PICTURE";
                            current_file = gf;
                            pic.Dock = DockStyle.Fill;
                            pic.Cursor = Cursors.Hand;
                            //PB.ErrorImage = .;
                            pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                            pic.Image = bmpt;
                            pic.DoubleClick += new EventHandler(MouseDoubleClick);
                            //AddControl(guidfile_pcl, pic);
                            this.guidfile_pcl.Controls.Add(pic);
                            break;
                        default:
                            MyMsgBox.Show("您选择的文档格式暂不支持！", "系统提示", MyMsgBox.MyButtons.OKCancel, MyMsgBox.MyIcon.Warning);
                            break;
                    }
                    //ms.Close();
                }
                else
                {
                    this.guidfile_pcl.Visible = false;
                }
            }
            catch (Exception err)
            {
                MyMsgBox.Show("指示文档显示错误:" + err.Message, "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error);
            }
        }
        /// <summary>
        /// 双击放大显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private new void MouseDoubleClick(object sender, EventArgs e)
        {
            try
            {
                MyFilesView gf = new MyFilesView();
                gf.current_file = current_file;
                gf.list_document = list_document;
                gf.ShowDialog();
            }
            catch
            {

            }
        }
        #endregion

        #region 线边库不足红色区域点击消失
        private void storage_quntity_lbl_Click(object sender, EventArgs e)
        {
            try
            {
                SetText(storage_quntity_lbl, "");
                SetColor(storage_quntity_lbl, Color.Red);
                SetVisible(storage_quntity_lbl, false);
            }
            catch
            {

            }
        }
        #endregion

        #region PLC地址跳变获取产品出生证

        public void GetBornCode(string opcname,bool isproid)
        {
            try
            {
                CONTROL_ADDRESS OpcItem = ControlAddressList.Where(s => s.ADDRESS_PATH == opcname).FirstOrDefault();
                if (OpcItem != null)
                {
                    if (OpcItem.ADDRESS_TYPE != null)
                    {
                        int c = (int)AddressCategory.readRFID_success;//跳变信号类型-读R
                        int pallet_leave= (int)AddressCategory.pallet_leave;//跳变信号类型-托盘离开
                        int pro_born_code = (int)AddressCategory.pro_born_code;//跳变信号类型-托盘离开
                        if (OpcItem.ADDRESS_TYPE == c.ToString())
                            //if (OpcItem.ADDRESS_TYPE == pro_born_code.ToString())
                            {
                            #region 产品到位时，复位PLC写入允许放行信号(1-0)，防止离开时未复位 -20200519
                            CONTROL_ADDRESS is_leave_ok = ControlAddressList.FirstOrDefault(s => s.ADDRESS_TYPE == ((int)AddressCategory.is_leaveOK).ToString());
                            if (is_leave_ok != null)
                            {
                                OPCHelper<JAC_KEYPART>.SynWriteOpcItem(is_leave_ok.ADDRESS_PATH, "0");//复位允许放行信号
                            }
                            #endregion

                            #region 读取产品出生证并显示在txt中
                            CONTROL_ADDRESS pro_born_address = ControlAddressList.Where(s =>s.ADDRESS_TYPE == ((int)AddressCategory.pro_born_code).ToString()).FirstOrDefault();//找到停止器对应的产品出生证地址
                            if (pro_born_address != null)
                            {
                                string values = OPCHelper<JAC_KEYPART>.SynReadOpcItem(pro_born_address.ADDRESS_PATH);//读取地址的value,即产品出生证信息
                                if(plan_product_data!=null&&values == plan_product_data.PRODUCT_BORN_CODE)//若读取重复产品ID信息，则不作任何处理
                                {
                                    return;
                                }
                                if (ServerCommunicationState == true && (plan_product_data == null || (plan_product_data != null && plan_product_data.PRODUCT_BORN_CODE != values)))//如果读取的产品出生证与上一个产品出生证不一样时处理信息
                                {
                                    GetProductBornCode(values);
                                    Log.GetInstance.WriteLog(values+"产品到位");
                                }
                                else
                                { 
                                    SetText(product_born_code_txt, values);//离线状态显示产品出生证
                                    SetCollectProgress(finish_lbl, 0, 0);//采集进度初始化
                                    SetColor(productarrival_lbl, HasCollectBtnColor);
                                }
                            }
                            #endregion
                        }
                        else if (OpcItem.ADDRESS_TYPE == pallet_leave.ToString())//托盘离开时复位允许放行信号
                        {
                            
                            #region 复位PLC写入允许放行信号
                            CONTROL_ADDRESS is_leave_ok = ControlAddressList.FirstOrDefault(s => s.ADDRESS_TYPE == ((int)AddressCategory.is_leaveOK).ToString());
                            if (is_leave_ok != null)
                            {
                                OPCHelper<JAC_KEYPART>.SynWriteOpcItem(is_leave_ok.ADDRESS_PATH, "0");//复位允许放行信号
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
            catch
            {

            }
        }
        #endregion

        class DataDictionaryDto
        {
            public string DicId { get; set; }
            public string DicCode { get; set; }
            public string DicName { get; set; }
            public string DicDetailId { get; set; }
            public string DicDetailCode { get; set; }
            public string DicDetailName { get; set; }
        }
        #region 计划查询跳转按钮
        private void plan_search_btn_Click(object sender, EventArgs e)//计划查询跳转按钮
        {
            try
            {

                //List<P_PRODUCT_SERIAL> seriallist = SerialRepositoryFactory.Repository().FindList();
                //List<P_ASSEMBLE_PRODUCT_STATE> assemblelist = AssembleRepositoryFactory.Repository().FindList();
                //var res = from u in planlist
                //          join p in seriallist on u.MES_PLAN_CODE equals p.MES_PLAN_CODE
                //          join s in assemblelist on p.PRODUCT_BORN_CODE equals s.PRODUCT_BORN_CODE
                //          select new
                //          {
                //              DicId = u.MES_PLAN_KEY,
                //              DicCode = p.SERIAL_KEY,
                //              DicName = s.ASSEMBLE_PRODUCT_STATE_KEY,
                //          };//多表联合查询
                //List<DataDictionaryDto> list = JsonConvert.DeserializeObject<List<DataDictionaryDto>>(res.ToJson());
                //PlanSearch plansearchform = new PlanSearch(basicinfor);
                //plansearchform.ShowDialog();

                //string body = new { content = ControlAddressList.ToJson() }.ToJson();
                //byte[] byteArray = System.Text.Encoding.Default.GetBytes(body);
                //string str = System.Text.Encoding.Default.GetString(byteArray);
                //dynamic a = JsonConvert.DeserializeObject<dynamic>(str);
                //string aa = a.content;
                //List<CONTROL_ADDRESS> list = JsonConvert.DeserializeObject<List<CONTROL_ADDRESS>>(aa);
                //string str2 = Environment.CurrentDirectory;//获取和设置当前目录（即该进程从中启动的目录）的完全限定路径。(备注:按照定义，如果该进程在本地或网络驱动器的根目录中启动，则此属性的值为驱动器名称后跟一个尾部反斜杠（如“C:\”）。如果该进程在子目录中启动，则此属性的值为不带尾部反斜杠的驱动器和子目录路径[如“C:\mySubDirectory”])。 
                //string str3 = Directory.GetCurrentDirectory(); //获取应用程序的当前工作目录。 
                //string str4 = AppDomain.CurrentDomain.BaseDirectory;//获取基目录，它由程序集冲突解决程序用来探测程序集。  
                //string str7 = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;//获取或设置包含该应用程序的目录的名称。
                //List<P_PLAN> list = new List<P_PLAN>();
                //P_PLAN p = new P_PLAN();
                //p.Create();
                //list.Add(p);
                //string pages = WebApiHttp.HttpPost($"http://localhost:28188/API/WebApiPlan/Insert", p.ToJson());//WebApi获取图标文件包

                RepositoryFactory<BASE_USER> T_RepositoryFactory = new RepositoryFactory<BASE_USER>();//定义泛型仓储
                List<BASE_USER> list = new List<BASE_USER>();
                for (int i = 1000000; i < 2000000; i++)
                {
                    BASE_USER useentity = new HfutIE.Entity.BASE_USER()
                    {
                        USERID = i.ToString(),
                        COMPANYID = "1111111111",
                        DEPARTMENTID = "111111111",
                        CODE = i.ToString(),
                        REALNAME = "11111"
                    };
                    list.Add(useentity);
                }
                T_RepositoryFactory.Repository(DatabaseType.SQLite).Insert(list);//本地插入服务器新增或修改数据
            }
            catch (Exception ex)
            {

            }
        }
        public string HttpPostWebService(string url, string method, string num1, string num2)
        {
            string result = string.Empty;
            string param = string.Empty;
            byte[] bytes = null;
            Stream writer = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;

            param = HttpUtility.UrlEncode("SerialNumber") + "=" + HttpUtility.UrlEncode(num1);
            bytes = Encoding.UTF8.GetBytes(param);

            request = (HttpWebRequest)WebRequest.Create(url + "/" + method);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = bytes.Length;

            try
            {
                writer = request.GetRequestStream();        //获取用于写入请求数据的Stream对象
            }
            catch (Exception ex)
            {
                return "";
            }

            writer.Write(bytes, 0, bytes.Length);       //把参数数据写入请求数据流
            writer.Close();

            try
            {
                response = (HttpWebResponse)request.GetResponse();      //获得响应
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                return "";
            }

            #region 这种方式读取到的是一个返回的结果字符串
            //Stream stream = response.GetResponseStream();        //获取响应流
            //XmlTextReader Reader = new XmlTextReader(stream);
            //Reader.MoveToContent();
            //result = Reader.ReadInnerXml();
            #endregion

            #region 这种方式读取到的是一个Xml格式的字符串
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            result = reader.ReadToEnd();
            #endregion 

            response.Dispose();
            response.Close();

            reader.Close();
            reader.Dispose();

            //Reader.Dispose();
            //Reader.Close();

            //stream.Dispose();
            //stream.Close();

            return result;
        }
        #endregion

        #region 返修按钮跳转
        private void repair_btn_Click(object sender, EventArgs e)//返修跳转按钮
        {
            try
            {
                if (plan_product_data != null)
                {
                    ScannerHelper.CloseCom(scanport);//关闭扫描枪连接
                    Rework reworkform = new Rework(plan_product_data, mainstationkey, basicinfor);
                    if (reworkform.ShowDialog() == DialogResult.OK)
                    {
                        ScannerHelper.OpenCom(scanport);
                    }
                }
                else//未加载产品信息，无法进行返修操作
                {
                    MyMsgBox.Show("请先录入产品信息!", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information);
                }
            }
            catch(Exception ex)
            {

            }
        }
        #endregion

        #region 设备点检提交按钮
        private void Equ_check_con_btn_Click(object sender, EventArgs e)//设备点检提交按钮
        {
            try
            {
                List<CHECK_EQUIP_RESULT> resultlist = new List<CHECK_EQUIP_RESULT>();
                for (int i = 0; i < CheckInfoLists.Count; i++)
                {
                    CHECK_EQUIP_RESULT entity = new CHECK_EQUIP_RESULT();
                    entity = EntityHelper.EntityHelper.EntityMapper(basicinfor, entity);//基础信息映射
                    entity = EntityHelper.EntityHelper.EntityMapper(CheckInfoLists[i], entity);//基础信息映射
                    entity.CHECK_RESULT = yes_radio_btns[i].Checked ? "正常" : "异常";
                    entity.CHECK_TIME = ServerTime.Now;
                    entity.Create();
                    resultlist.Add(entity);
                }
                CHECK_EQUIP_RESULTRepository.Repository().Insert(resultlist);
                MyMsgBox.Show("设备点检成功!", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information,5);
                dockPanel1.Visibility = DockVisibility.Hidden;//隐藏悬浮框
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("设备点检提交失败。" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
                MyMsgBox.Show("设备点检提交失败!", "提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error,5);
            }
        }
        #endregion

        #region 点击panel控件移动窗口
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
        #endregion
        
        #region 重新读取出生证
        private void reread_btn_Click(object sender, EventArgs e)
        {
            try
            {
                CONTROL_ADDRESS pro_born_address = ControlAddressList.Where(s =>s.ADDRESS_TYPE == ((int)AddressCategory.pro_born_code).ToString()).FirstOrDefault();//找到停止器对应的产品出生证地址
                if (pro_born_address != null)
                {
                    string BornCodeOpcName = pro_born_address.ADDRESS_PATH.Split(';')[0];
                    if (BornCodeOpcName != null)
                    {
                        #region 读取产品出生证并显示在txt中
                        if (pro_born_address != null)
                        {
                            string values = OPCHelper<JAC_KEYPART>.SynReadOpcItem(pro_born_address.ADDRESS_PATH);//读取地址的value,即产品出生证信息
                            if (plan_product_data != null && values == plan_product_data.PRODUCT_BORN_CODE)//若读取重复产品ID信息，则不作任何处理
                            {
                                return;
                            }
                            if (ServerCommunicationState == true && (plan_product_data == null || (plan_product_data != null && plan_product_data.PRODUCT_BORN_CODE != values)))//如果读取的产品出生证与上一个产品出生证不一样时处理信息
                            {
                                GetProductBornCode(values);
                            }
                            else
                            {
                                SetText(product_born_code_txt, values);//离线状态显示产品出生证
                                SetCollectProgress(finish_lbl, 0, 0);//采集进度初始化
                                SetColor(productarrival_lbl, HasCollectBtnColor);
                            }
                        }
                        #endregion
                    }
                }
            }
            catch
            {

            }
        }
        #endregion
    
        #region 辅助测试
        private void button2_Click(object sender, EventArgs e)
        {
            needRefresh = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                needRefresh = false;
                ServerCommunicationState = false;
                ShowServerConnection(false);
            }
            catch
            {

            }
        }
        #endregion

        #region 让步放行按钮
        private void compass_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (new ValidateForm("JAC_MES_KEYPART", "KEYPART_RELEASE", "让步放行权限验证").ShowDialog() == DialogResult.OK)
                {
                    if (plan_product_data != null)
                    {
                        CONCESSION_RELEASE entity = new CONCESSION_RELEASE();
                        entity = EntityHelper.EntityHelper.EntityMapper(plan_product_data, entity);
                        entity = EntityHelper.EntityHelper.EntityMapper(StationList.Select(s => s.IS_MAIN == "是"), entity);
                        entity.Create();
                        int isOk = CONCESSION_RELEASERepository.Repository().Insert(entity);
                        SetVisible(arrowpicture4_pic, true);//第四个箭头不可见
                        SetColor(allowrelease_lbl, HasCollectBtnColor);//生产状态的“允许放行”按钮颜色改变为绿色
                    }
                    #region 对PLC写入允许放行信号
                    CONTROL_ADDRESS is_leave_ok = ControlAddressList.FirstOrDefault(s => s.ADDRESS_TYPE == ((int)AddressCategory.is_leaveOK).ToString());
                    if (is_leave_ok != null)
                    {
                        Stopwatch sw = new Stopwatch();
                        sw.Start();
                        OPCHelper<JAC_KEYPART>.SynWriteOpcItem(is_leave_ok.ADDRESS_PATH, "1");//写入允许放行
                        sw.Stop();
                        long ew = sw.ElapsedMilliseconds;
                        Log.GetInstance.WriteLog("让步放行写入允许放行信号时间:" + ew.ToString() + "ms");
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                SetColor(allowrelease_lbl, SystemColors.ControlDark);//生产状态的“允许放行”按钮颜色改变为绿色
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("产品让步放行失败。" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
                MyMsgBox.Show("产品让步放行失败!", "提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error);
            }
        }
        #endregion

        #region 其他
        private void fa_main_info_insert_btn_Click(object sender, EventArgs e)
        {
            ScannerHelper.CloseCom(scanport);//关闭扫描枪连接
            if (plan_product_data != null)
            {
                Rework reworkform = new Rework(plan_product_data, mainstationkey, basicinfor);
                if (reworkform.ShowDialog() == DialogResult.OK)
                {
                    ScannerHelper.OpenCom(scanport);
                }
            }
            else//未加载产品信息，无法进行返修操作
            {
                MyMsgBox.Show("请先录入产品信息!", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information);
            }
        }

        public void DataChange(ItemValueResult item)
        {
            try
            {  //根据地址的类型，判断执行哪项逻辑操作
                OpcName = item.ItemName;

                //if (OpcName.Contains("pro_born_code"))
                //{
                //    GetBornCode(OpcName,true);
                //}
                if (item.Value.ToString() == "True")
                {
                    GetBornCode(OpcName, false);
                }
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog(ex.Message);
            }
        }

        #endregion

        #region 初始化按钮
        private void reset_btn_Click(object sender, EventArgs e)
        {
            Initialization();
            SetLableText(main_station_lbl, StationList.FindAll(s => s.IS_MAIN == "是").Select(s => s.STATION_CODE).FirstOrDefault() + "工位");//设置程序主工位名称
            SetViceStationLabel();
        }
        #endregion
    }
}

