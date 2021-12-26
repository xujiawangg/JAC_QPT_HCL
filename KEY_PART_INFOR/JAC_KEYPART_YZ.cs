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

namespace HfutIe
{
    public partial class JAC_KEYPART_YZ : Form,IOPC
    {
        public JAC_KEYPART_YZ()
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
        List<CONTROL_ADDRESS> ControlAddressList = new List<CONTROL_ADDRESS>();//该工位的停止器所配置的地址信息
        List<CONTROL_ADDRESS> ControlAddressList_L1020;
        List<CONTROL_ADDRESS> ControlAddressList_L1030;
        List<CONTROL_ADDRESS> ControlAddressList_L1050;
        List<CONTROL_ADDRESS> ControlAddressList_L1060;
        List<CHECK_INFO> CheckInfoLists;//主工位对应的设备点检信息

        string OpcName;
        string mainstationkey;//主工位主键
        static string strpath = "E:\\#1#纳威司达项目\\安全件1.png";
        static readonly object locker1 = new object();
        static readonly object locker2 = new object();
        static readonly object locker3 = new object();
        static readonly object locker4 = new object();
        static readonly object locker5 = new object();
        public string wc_code = ReadXML.GetWCCode(System.Windows.Forms.Application.StartupPath + @"\WCConfig\WC.xml");//工位编号
        List<STATION> StationList = new List<STATION>();//该PC对应的工作中心下所属工位的集合
        WorkCenterInfor wcinfor;
        string plan_code;//当前产品所属计划
        int collectedcount = 0;//判断已采集的安全件的数量
        //int finishcollected = 0;//finishcollected枚举值{0：未开始采集，1：一开始采集，未采集完成，2：采集完成}
        public BasicInfoDto basicinfor;//根据工位得出的基本信息
        public BasicInfoDto basicinfor_L1020;//根据工位得出的基本信息
        public BasicInfoDto basicinfor_L1025;//根据工位得出的基本信息
        public BasicInfoDto basicinfor_L1030;//根据工位得出的基本信息
        public BasicInfoDto basicinfor_L1050;//根据工位得出的基本信息
        public BasicInfoDto basicinfor_L1060;//根据工位得出的基本信息
        public List<P_KEY_PART_C> key_part_c;//安全件采集配置
        public List<Part_Supplier> supplier;//安全件供应商信息
        //public DataTable team_shift;//班组班制信息
        public P_ASSEMBLE_PRODUCT_STATE plan_product_data;//通过产品出生证查到的该产品出生证代表的计划、产品信息
        public P_ASSEMBLE_PRODUCT_STATE plan_product_data_L1020;//通过产品出生证查到的该产品出生证代表的计划、产品信息
        public P_ASSEMBLE_PRODUCT_STATE plan_product_data_L1025;//通过产品出生证查到的该产品出生证代表的计划、产品信息
        public P_ASSEMBLE_PRODUCT_STATE plan_product_data_L1030;//通过产品出生证查到的该产品出生证代表的计划、产品信息
        public P_ASSEMBLE_PRODUCT_STATE plan_product_data_L1050;//通过产品出生证查到的该产品出生证代表的计划、产品信息
        public P_ASSEMBLE_PRODUCT_STATE plan_product_data_L1060;//通过产品出生证查到的该产品出生证代表的计划、产品信息
        List<P_KEY_PART_C> KeyPartData = new List<P_KEY_PART_C>();//根据产品出生证和工位号查询到本次需要采集的安全件信息，逐个多条(按数量分解成单个安全件，无上限限制，用预留字段1标识是否被加载到界面上过：0-未加载；1-已加载)
        List<P_KEY_PART_C> KeyPartData_L1020 = new List<P_KEY_PART_C>();//L1020待采集信息,RESERVE1:0-未采集，1-已采集
        List<P_KEY_PART_C> KeyPartData_L1025 = new List<P_KEY_PART_C>();//L1025待采集信息,RESERVE1:0-未采集，1-已采集
        List<P_KEY_PART_C> KeyPartData_L1030 = new List<P_KEY_PART_C>();//L1030待采集信息,RESERVE1:0-未采集，1-已采集
        List<P_KEY_PART_C> KeyPartData_L1050 = new List<P_KEY_PART_C>();//L1050待采集信息,RESERVE1:0-未采集，1-已采集
        List<P_KEY_PART_C> KeyPartData_L1060 = new List<P_KEY_PART_C>();//L1060待采集信息,RESERVE1:0-未采集，1-已采集

        Panel[] collect_panels;//采集面板panel
        Label[] product_borncode_lbls;//产品出生证label
        Label[] productcode_lbls;//产品编号label
        Label[] finish_lbls;//采集状态label
        Label[] op_status_lbls;//工位采集状态label
        BasicInfoDto[] basicinfors;//基础信息dto
        P_ASSEMBLE_PRODUCT_STATE[] plan_product_datas;//在制品信息entity
        List<P_KEY_PART_C>[] KeyPartDatas;//待采集安全件集合List
        SerialPort[] serialports;//扫描枪串口SerialPort
        string[] stations;//工位集合
        string[] stopper_keys;//停止器主键集合
 

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
        HZ_Keypart_Search Keypart_Search;

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
                //SetLableText(main_station_lbl, StationList.FindAll(s => s.IS_MAIN == "是").Select(s => s.STATION_CODE).FirstOrDefault() + "工位");//设置程序主工位名称
                #region SQLite操作(PB)
                //Task.Run(() =>
                //{
                //    try
                //    {
                //        SQLiteOperate.CreateSQLiteDataBase();//创建SQL数据库
                //        SQLiteOperate.SynchronizeUserInfo();//同步本地人员基本信息
                //        SQLiteOperate.SynchronizeControlAddress();//同步本地控制地址基本信息
                //        SQLiteOperate.SynchronizeStopper();//同步本地停止器基本信息
                //        SQLiteOperate.SynchronizeStation();//同步本地工位基本信息
                //        SQLiteOperate.SynchronizeList(new BASE_USER());
                //        SQLiteOperate.SynchronizeList(new CONTROL_ADDRESS());
                //        SQLiteOperate.SynchronizeList(new STOPPER());
                //        SQLiteOperate.SynchronizeList(new STATION());
                //        SQLiteOperate.SynchronizeKeyPartInfor();//同步安全件采集过程信息(创建表，清除一周前已同步信息，同步未同步信息逻辑待定)
                //    }
                //    catch (Exception ex)
                //    {
                //        Log.GetInstance.WriteLog(ex.Message);
                //    }
                //});
                #endregion
                Task.Run(() => RecordTimeShow());
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog(ex.Message);
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
                    InitializationOnline();//在线初始化
                }
                InitializationOnlineOrOffline();//在线/离线初始化
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
                basicinfor_L1020 = GetData.GetFactoryInforByWccode(ConfigurationManager.AppSettings["HZ_Station1"].ToString().Trim());
                basicinfor_L1025 = GetData.GetFactoryInforByWccode(ConfigurationManager.AppSettings["HZ_Station2"].ToString().Trim());
                basicinfor_L1030 = GetData.GetFactoryInforByWccode(ConfigurationManager.AppSettings["HZ_Station3"].ToString().Trim());
                basicinfor_L1050 = GetData.GetFactoryInforByWccode(ConfigurationManager.AppSettings["HZ_Station4"].ToString().Trim());
                basicinfor_L1060 = GetData.GetFactoryInforByWccode(ConfigurationManager.AppSettings["HZ_Station5"].ToString().Trim());
                basicinfors = new BasicInfoDto[] { basicinfor_L1020, basicinfor_L1025, basicinfor_L1030, basicinfor_L1050, basicinfor_L1060 };//基础信息dto
                //CheckInfoLists = GetData.GetCheckInforByStationKey(StationList.Find(s => s.IS_MAIN == "是").STATION_KEY);//获取基本信息(StationCodeList);
                //Task.Run(() => SynchronizeKey_Part_C(StationList.Select(s => s.STATION_CODE).ToList()));
                //supplier = GetData.GetSupplierInfor();

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
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog(ex.Message);
            }
        }

        private void InitializationOnlineOrOffline()//在线/离线初始化
        {
            //product_born_code_txt.Focus();
            ////登录人员显示
            //staff_code_lbl.Text = SystemLog.UserCode;//系统登录员工编号
            //staff_name_lbl.Text = SystemLog.UserName;//系统登录员工姓名
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

            #region 界面待显示控件集合(在线/离线)
            collect_panels= new Panel[] { collect_panel_L1020, collect_panel_L1025, collect_panel_L1030, collect_panel_L1050, collect_panel_L1060 };//采集面板panel
            product_borncode_lbls = new Label[] { product_borncode_lbl_L1020, product_borncode_lbl_L1025, product_borncode_lbl_L1030, product_borncode_lbl_L1050, product_borncode_lbl_L1060 };//产品出生证文本框
            productcode_lbls = new Label[] { productcode_lbl_L1020, productcode_lbl_L1025, productcode_lbl_L1030, productcode_lbl_L1050, productcode_lbl_L1060 };//产品编号label
            finish_lbls = new Label[] { finish_lbl_L1020, finish_lbl_L1025, finish_lbl_L1030, finish_lbl_L1050, finish_lbl_L1060 };//采集进度label
            op_status_lbls = new Label[] { op_status_lbl_L1020, op_status_lbl_L1025, op_status_lbl_L1030, op_status_lbl_L1050, op_status_lbl_L1060 };//工位状态label
            
            plan_product_datas = new P_ASSEMBLE_PRODUCT_STATE[] { plan_product_data_L1020, plan_product_data_L1025, plan_product_data_L1030, plan_product_data_L1050, plan_product_data_L1060 };//在制品信息entity
            KeyPartDatas = new List<P_KEY_PART_C>[] { KeyPartData_L1020, KeyPartData_L1025, KeyPartData_L1030, KeyPartData_L1050, KeyPartData_L1060 };//待采集信息List
            serialports = new SerialPort[] { scanport1, scanport2, scanport3, scanport4, scanport5 };//串口SerialPort
            stations = new string[] {"27OP_L1020", "27OP_L1025", "27OP_L1030", "27OP_L1050" , "27OP_L1060" };
            #endregion

            #region 打开扫描枪连接(在线/离线)
            try
            {
                scanport1.PortName = ConfigurationManager.AppSettings["COM_1"].ToString().Trim(); ;//为串口赋名 
                scanport1.BaudRate = 9600;//无线扫描枪
                scanport1.Open();
            }
            catch
            {
                Log.GetInstance.WriteLog("打开" + ConfigurationManager.AppSettings["COM_1"].ToString().Trim() + "失败");
            }

            try
            {
                scanport2.PortName = ConfigurationManager.AppSettings["COM_2"].ToString().Trim(); //为串口赋名 
                scanport2.BaudRate = 9600;//无线扫描枪
                scanport2.Open();
            }
            catch
            {
                Log.GetInstance.WriteLog("打开" + ConfigurationManager.AppSettings["COM_2"].ToString().Trim() + "失败");
            }

            try
            {
                scanport3.PortName = ConfigurationManager.AppSettings["COM_3"].ToString().Trim();//为串口赋名 
                scanport3.BaudRate = 9600;//无线扫描枪
                scanport3.Open();
            }
            catch
            {
                Log.GetInstance.WriteLog("打开" + ConfigurationManager.AppSettings["COM_3"].ToString().Trim() + "失败");
            }

            try
            {
                scanport4.PortName = ConfigurationManager.AppSettings["COM_4"].ToString().Trim();//为串口赋名 
                scanport4.BaudRate = 9600;//无线扫描枪
                scanport4.Open();
            }
            catch
            {
                Log.GetInstance.WriteLog("打开" + ConfigurationManager.AppSettings["COM_4"].ToString().Trim() + "失败");
            }

            try
            {
                scanport5.PortName = ConfigurationManager.AppSettings["COM_5"].ToString().Trim();//为串口赋名 
                scanport5.BaudRate = 9600;//无线扫描枪
                scanport5.Open();
            }
            catch
            {
                Log.GetInstance.WriteLog("打开" + ConfigurationManager.AppSettings["COM_5"].ToString().Trim() + "失败");
            }
            #endregion

            #region 清空界面残留信息
            for (int i = 0; i< product_borncode_lbls.Length; i++)
            {
                SetLableText(product_borncode_lbls[i], "");
                SetLableText(productcode_lbls[i], "");
            }
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
                OPCHelper<JAC_KEYPART_YZ>.OPC_Connect(this, IP, ControlAddressList);
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
                List<STOPPER> stopperlist = StopperRepositoryFactory.Repository().FindList();
                STOPPER stopentity_L1020 = stopperlist.FirstOrDefault(s => s.STATION_KEY == basicinfor_L1020.STATION_KEY);
                ControlAddressList_L1020 = Control_AddressRepositoryFactory.Repository().FindList("POSITION_KEY", stopentity_L1020.STOPPER_KEY);
                STOPPER stopentity_L1030 = stopperlist.FirstOrDefault(s => s.STATION_KEY == basicinfor_L1030.STATION_KEY);
                ControlAddressList_L1030 = Control_AddressRepositoryFactory.Repository().FindList("POSITION_KEY", stopentity_L1030.STOPPER_KEY);
                STOPPER stopentity_L1050 = stopperlist.FirstOrDefault(s => s.STATION_KEY == basicinfor_L1050.STATION_KEY);
                ControlAddressList_L1050 = Control_AddressRepositoryFactory.Repository().FindList("POSITION_KEY", stopentity_L1050.STOPPER_KEY);
                STOPPER stopentity_L1060 = stopperlist.FirstOrDefault(s => s.STATION_KEY == basicinfor_L1060.STATION_KEY);
                ControlAddressList_L1060 = Control_AddressRepositoryFactory.Repository().FindList("POSITION_KEY", stopentity_L1060.STOPPER_KEY);
                ControlAddressList.AddRange(ControlAddressList_L1020);
                ControlAddressList.AddRange(ControlAddressList_L1030);
                ControlAddressList.AddRange(ControlAddressList_L1050);
                ControlAddressList.AddRange(ControlAddressList_L1060);
                stopper_keys= new string[] { stopentity_L1020.STOPPER_KEY, "27OP_L1025", stopentity_L1030.STOPPER_KEY, stopentity_L1050.STOPPER_KEY, stopentity_L1060.STOPPER_KEY };
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
                ScannerHelper.CloseCom(scanport1);
            }
            finally
            {
                this.Dispose();
                this.Close();
            }
        }
        #endregion

        #region 手动输入
        
        #endregion

        #region #1#获取到产品出生证
        public void GetProductBornCode(string product_born_code)//获取到新产品出生证
        {
            try
            {
                #region 复位PLC写入允许放行信号
                CONTROL_ADDRESS is_leave_ok = ControlAddressList.FirstOrDefault(s => s.ADDRESS_TYPE == ((int)AddressCategory.is_leaveOK).ToString());
                if (is_leave_ok != null)
                {
                    OPCHelper<JAC_KEYPART>.SynWriteOpcItem(is_leave_ok.ADDRESS_PATH, "0");//写入允许放行
                }
                #endregion

                #region 显示上一个产品ID号
                if (plan_product_data != null&& plan_product_data.PRODUCT_BORN_CODE!=null)
                {
                    //SetText(product_name_txt, plan_product_data.PRODUCT_BORN_CODE);//显示上一个产品ID号
                }
                #endregion
                //ScannerHelper.WriteToSerialPort(product_born_code, ReadXML.COM_code);//将产品出生证写入串口
                List<P_ASSEMBLE_PRODUCT_STATE> p_assemble_product_state = GetData.GetProductinforByborncode(product_born_code);
                if (p_assemble_product_state.Count == 0)
                {
                    //SetText(product_born_code_txt, "产品出生证信息错误");
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
                //SetText(product_born_code_txt, product_born_code);//重新为产品出生证赋值
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
                //SetColor(productarrival_lbl, HasCollectBtnColor);//生产状态的“产品到达”按钮颜色改变
                //int IsOk = Data();//根据产品出生证显示相应数据，计划编号，产品编号，批次号等
                //if (IsOk == 1)
                //{
                //    SetVisible(arrowpicture2_pic, false);//第二个箭头不可见
                //    SetVisible(arrowpicture3_pic, false);//第三个箭头不可见
                //    SetVisible(arrowpicture4_pic, false);//第四个箭头不可见
                //    SetColor(productborncode_lbl, HasCollectBtnColor);//生产状态的“产品出生证扫描成功”按钮颜色改变
                //    SetVisible(arrowpicture1_pic, true);//第一个箭头可见
                //    SetEnabled(product_born_code_txt, false);//使产品出生证text控件不可操作

                //    SetText(inputmodel_product_btn, "手动输入");//显示产品出生证输入模式控件

                //}
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
                //SetColor(keypartbarcode_lbl, HasCollectBtnColor);//只要本次扫描成功，生产状态的“安全件扫描成功”按钮颜色改变为绿色
                //SetVisible(arrowpicture2_pic, true);//第二个箭头可见
                //if (collectedcount == KeyPartData.Count)//如果采集数量等于要求采集数量，采集完成
                //{
                //    SetColor(finish_lbl, HasCollectBtnColor);//生产状态的“采集成功”按钮颜色改变为绿色
                //                                                      //Thread.Sleep(500);
                //    SetColor(allowrelease_lbl, HasCollectBtnColor);//生产状态的“允许放行”按钮颜色改变为绿色
                //    SetVisible(arrowpicture3_pic, true);//第三个箭头可见
                //    SetVisible(arrowpicture4_pic, true);//第四个箭头可见

                //    #region 对PLC写入允许放行信号
                //    CONTROL_ADDRESS is_leave_ok = ControlAddressList.FirstOrDefault(s => s.ADDRESS_TYPE == ((int)AddressCategory.is_leaveOK).ToString());
                //    if (is_leave_ok != null)
                //    {
                //        OPCHelper<JAC_KEYPART>.SynWriteOpcItem(is_leave_ok.ADDRESS_PATH, "1");//写入允许放行
                //    }
                //    #endregion
                //}
                //else if (collectedcount != 0 && collectedcount < KeyPartData.Count)//如果已经采集安全件了，但采集数量小于要求采集数量，即部分采集
                //{
                //    SetColor(finish_lbl, Color.Yellow);//生产状态的“采集成功”按钮颜色改变为黄色
                //    SetVisible(arrowpicture3_pic, true);//第三个箭头可见
                //}

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
                    //p_key_part_infor = Accept(p_key_part_infor, keypartbarcode, i);
                    if (ServerCommunicationState == true)//在线状态提交安全件信息至数据库，并扣除线边库数量
                    {
                       // bool result = KeyPartOperationDataBase(p_key_part_infor);//提交安全件信息
                        bool result = true;
                        if (result == false) //提交安全件绑定信息失败
                        {
                            //SetColor(keypartbarcode_lbl, Color.Red);
                            IsOk = 0;
                        }
                        else
                        {
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
                if (IsOk == 1)//条码成功绑定，此条码之后不可操作
                {
                    #region 查看是否有未被加载到界面上的安全件信息,如果存在则将该安全件信息填充在上一个被成功采集的安全件位置上
                    foreach (var itemkeypart in KeyPartData)
                    {
                        if (itemkeypart.RESERVE1 == "0")
                        {
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
                //SetColor(keypartbarcode_lbl, Color.Red);
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
        private bool KeyPartOperationDataBase(P_KEY_PART_INFOR p_key_part_infor,List<P_KEY_PART_C> p_kp_c,P_ASSEMBLE_PRODUCT_STATE p_a_p_s)
        {
            Repository<P_KEY_PART_INFOR> tran = new Repository<P_KEY_PART_INFOR>();
            DbTransaction isOpenTrans = tran.BeginTrans();
            try
            {
                #region 安全件数据
                Task.Run(() =>
                {
                    p_key_part_infor.Create();
                    P_KEY_PART_C key_part_entity = p_kp_c.Find(s => s.KEY_PART_C_KEY == p_key_part_infor.KEY_PART_C_KEY);
                    //判断过程表中是否已经存在此条记录
                    List<P_KEY_PART_INFOR> p_key_part_inforlist = P_KEY_PART_INFORRepositoryFactory.Repository().FindList($" and MES_PLAN_KEY='{p_a_p_s.MES_PLAN_KEY}' and PRODUCT_BORN_CODE='{p_a_p_s.PRODUCT_BORN_CODE}' and STATION_KEY='{key_part_entity.STATION_KEY}' and PART_KEY='{p_key_part_infor.PART_KEY}' and PART_BARCODE='{p_key_part_infor.PART_BARCODE}' and KEY_PART_C_KEY='{p_key_part_infor.KEY_PART_C_KEY}'");
                    if ((key_part_entity.QUANTITY == 1&& p_key_part_inforlist.Count ==1)|| key_part_entity.QUANTITY>1&&(p_key_part_inforlist.Count >key_part_entity.QUANTITY))//数据库中有同计划、同产品、同零部件的记录,控制绑定数量不超过待采集数量,1-待采集数量为1，实采为1即更新，2-待采>1，实采>待采即更新
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
        private P_KEY_PART_INFOR Accept(P_KEY_PART_INFOR p_key_part_infor, string key_part_barcode,string part_code,P_ASSEMBLE_PRODUCT_STATE p_a_p_s, BasicInfoDto b_i_d,List<P_KEY_PART_C> p_k_d)
        {
            try
            {
                int IsOk = 0;
                P_KEY_PART_C key_part_entity = p_k_d.Find(s => s.PART_CODE == part_code);//获得该条码对应的安全件信息
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
                p_key_part_infor = EntityHelper.EntityHelper.EntityMapper(b_i_d, p_key_part_infor);//为实体增加基本信息
                p_key_part_infor.EQUIP_KEY = b_i_d.EQUIPMENT_KEY;
                p_key_part_infor.EQUIP_CODE = b_i_d.EQUIPMENT_CODE;
                p_key_part_infor.EQUIP_NAME = b_i_d.EQUIPMENT_NAME;
                //p_key_part_infor = supplier_infor.EntityMapper<Part_Supplier, P_KEY_PART_INFOR>();//为实体增加供应商信息
                //p_key_part_infor = EntityHelper<P_KEY_PART_INFOR>.GetPartEntity(p_key_part_infor, team_shift);//为实体增加班制班组信息
                //p_key_part_infor = plan_product_data.EntityMapper<P_ASSEMBLE_PRODUCT_STATE, P_KEY_PART_INFOR>();//为实体增加计划、产品信息
                p_key_part_infor = EntityHelper.EntityHelper.EntityMapper(p_a_p_s, p_key_part_infor);//为实体增加计划、产品信息
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
                
                #region 不合格品信息
                p_notok_product_infor = p_key_part_infor.EntityMapper<P_KEY_PART_INFOR, P_NOTOK_PRODUCT_INFOR>();
                p_notok_product_infor = p_a_p_s.EntityMapper<P_ASSEMBLE_PRODUCT_STATE, P_NOTOK_PRODUCT_INFOR>(); //这条语句主要是为了获取bom_key
                #endregion
                return p_key_part_infor;
            }
            catch (Exception ex)
            {
                //SetColor(keypartbarcode_lbl, Color.Red);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("提交安全件信息失败！" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
                return null;
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
                    plan_product_data = KeyPartGetData.GetPlanDatabyProductBornCode("");
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
                    MyMsgBox.Show("网络异常", "异常", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                //SetText(product_born_code_txt, "产品出生证输入不正确！请重试。");
                //SetColor(productborncode_lbl, Color.Red);
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
                            newentity.RESERVE1 = "0";//初始均为未加载
                            //if(KeyPartData.Where(s=>s.KEY_PART_C_KEY== newentity.KEY_PART_C_KEY).Count()==0)
                            //{
                                KeyPartData.Add(newentity);
                            //}
                        //}
                    }
                    KeyPartData = KeyPartData.OrderBy(s => s.PART_CODE).ToList();//按物料号排序显示
                    #region 显示安全件采集进度
                    //SetCollectProgress(finish_lbl, KeyPartData.Count, 0);
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
                            //SetColor(allowrelease_lbl, HasCollectBtnColor);//生产状态的“允许放行”按钮颜色改变为绿色
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
                    //SetText(plan_code_txt, entity.MES_PLAN_CODE);//显示计划编号
                    //SetText(product_code_txt, entity.PRODUCT_CODE);//显示产品出生证
                    //SetText(product_name_txt, entity.PRODUCT_NAME);//显示产品名称
                    //SetText(product_batch_no_txt, entity.PRODUCT_MODEL_NO.ToString());//显示产品型号
                    plan_code = entity.MES_PLAN_CODE;//记录当前计划编号信息，用于计划切换时提示
                }
                else
                {
                    //SetText(plan_code_txt, "");//显示计划编号
                    //SetText(product_code_txt, "");//显示产品出生证
                   // SetText(product_name_txt, "");//显示产品名称
                    //SetText(product_batch_no_txt, "");//显示产品型号
                }
            }
            catch (Exception ex)
            {
                //SetText(product_born_code_txt, "该产品信息不完整！请重试。");
                //SetColor(productborncode_lbl, Color.Red);
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
                            KeyPartData[i].RESERVE1 = "1";//该安全件已被加载到界面上，预留字段1属性值改为1
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
        private void scanport1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                lock (locker1)//防止重复扫码导致程序卡死
                {
                    ScanportDataReceive(scanport1,1);
                }
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("扫描枪1获取数据时出现错误：" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
            finally
            {
                scanport1.DiscardInBuffer();//清理输入缓冲区
                scanport1.DiscardOutBuffer();//清理输出缓冲区
            }
        }
        private void scanport2_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                lock (locker2)//防止重复扫码导致程序卡死
                {
                    ScanportDataReceive(scanport2, 2);
                }
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("扫描枪2获取数据时出现错误：" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
            finally
            {
                scanport2.DiscardInBuffer();//清理输入缓冲区
                scanport2.DiscardOutBuffer();//清理输出缓冲区
            }
        }

        private void scanport3_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                lock (locker3)//防止重复扫码导致程序卡死
                {
                    ScanportDataReceive(scanport3, 3);
                }
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("扫描枪3获取数据时出现错误：" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
            finally
            {
                scanport3.DiscardInBuffer();//清理输入缓冲区
                scanport3.DiscardOutBuffer();//清理输出缓冲区
            }
        }

        private void scanport4_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                lock (locker4)//防止重复扫码导致程序卡死
                {
                    ScanportDataReceive(scanport4, 4);
                }
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("扫描枪4获取数据时出现错误：" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
            finally
            {
                scanport4.DiscardInBuffer();//清理输入缓冲区
                scanport4.DiscardOutBuffer();//清理输出缓冲区
            }
        }
        private void scanport5_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                lock (locker5)//防止重复扫码导致程序卡死
                {
                    ScanportDataReceive(scanport5, 5);
                }
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("扫描枪5获取数据时出现错误：" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
            finally
            {
                scanport5.DiscardInBuffer();//清理输入缓冲区
                scanport5.DiscardOutBuffer();//清理输出缓冲区
            }
        }

        public void ScanportDataReceive(SerialPort ScannerSerialPort,int ScannerNo)
        {
            string barcodetype = "";
            string barcode = "";
            bool IsOk = false;
            object obj = ScannerHelper.dataReceive(ScannerSerialPort, out barcodetype, out IsOk);
            CheckForIllegalCrossThreadCalls = false;
            barcode = obj.ToString().Trim();
            if (!string.IsNullOrEmpty(barcode))
            {
                if (barcodetype == "产品出生证条形码")
                {
                    barcode = barcode.Substring(0, 8);//总成码前8位为产品出生证信息
                    Log.GetInstance.WriteLog("获取出生证条码:" + barcode);
                    GetProductInfo(barcode, ScannerNo-1);
                }
                else
                {
                    Log.GetInstance.WriteLog("获取零部件条码:" + barcode);
                    BindBarcode(barcode, ScannerNo-1);
                }
            }
        }
        #region 获取通过产品出生证获取产品信息
        public void GetProductInfo(string barcode,int i)
        {
            plan_product_datas[i] = KeyPartGetData.GetPlanDatabyProductBornCode(barcode);
            List<string> stationlist = new List<string>();
            stationlist.Add(stations[i]);
            KeyPartDatas[i] = KeyPartGetData.GetKeyPartConfig(stationlist, plan_product_datas[i].MES_PLAN_KEY);//待采集安全件信息
            #region 界面显示更新
            SetLableText(product_borncode_lbls[i], barcode);
            SetLableText(productcode_lbls[i], plan_product_datas[i].PRODUCT_CODE);
            SetCollectProgress(finish_lbls[i], KeyPartDatas[i].Count, 0);
            if (KeyPartDatas[i].Count == 0)//该总成码无带采集安全件
            {
                SetLableText(op_status_lbls[i], "状态:无需采集");
                SetColor(collect_panels[i], Color.Red);
                #region 写入对应工位允许放行信号
                CONTROL_ADDRESS is_leave_ok = ControlAddressList.FirstOrDefault(s => s.POSITION_KEY == stopper_keys[i] && s.ADDRESS_TYPE == ((int)AddressCategory.is_leaveOK).ToString());//获取对应停止器的允许放行信号
                if (is_leave_ok != null)
                {
                    OPCHelper<JAC_KEYPART_YZ>.SynWriteOpcItem(is_leave_ok.ADDRESS_PATH, "1");//写入允许放行信号
                }
                #endregion
            }
            else
            {
                SetLableText(op_status_lbls[i], "状态:等待采集");
                SetColor(collect_panels[i], Color.Yellow);

                #region 复位对应工位允许放行信号
                CONTROL_ADDRESS is_leave_ok = ControlAddressList.FirstOrDefault(s =>s.POSITION_KEY==stopper_keys[i]&&s.ADDRESS_TYPE == ((int)AddressCategory.is_leaveOK).ToString());//获取对应停止器的允许放行信号
                if (is_leave_ok != null)
                {
                    OPCHelper<JAC_KEYPART_YZ>.SynWriteOpcItem(is_leave_ok.ADDRESS_PATH, "0");//复位允许放行信号
                }
                #endregion
            }
            #endregion
        }
        #endregion

        #region 绑定安全件数据
        public void BindBarcode(string barcode,int i)
        {
            if (KeyPartDatas[i].Count != 0)
            {
                foreach (var item in KeyPartDatas[i].Where(s => s.RESERVE1 != "1"))//遍历未采集信息
                {
                    if (barcode.Contains(item.PART_CODE))//若匹配到条码信息包含待采集零部件编号，则采集成功
                    {
                        P_KEY_PART_INFOR p_key_part_infor = new P_KEY_PART_INFOR();//当前采集的安全件信息
                        p_key_part_infor = Accept(p_key_part_infor, barcode, item.PART_CODE, plan_product_datas[i], basicinfors[i], KeyPartDatas[i]);
                        bool result = KeyPartOperationDataBase(p_key_part_infor, KeyPartDatas[i], plan_product_datas[i]);//提交安全件信息
                        if (result)
                        {
                            item.RESERVE1 = "1";//标识为已采集
                            SetCollectProgress(finish_lbls[i], KeyPartDatas[i].Count, KeyPartDatas[i].Where(s => s.RESERVE1 == "1").ToList().Count);
                            if (KeyPartDatas[i].Where(s => s.RESERVE1 == "1").ToList().Count < KeyPartDatas[i].Count)
                            {
                                SetLableText(op_status_lbls[i], "状态:正在采集");
                            }
                            else
                            {
                                SetLableText(op_status_lbls[i], "状态:允许放行");
                                SetColor(collect_panels[i], Color.Green);
                                #region 写入对应工位允许放行信号
                                CONTROL_ADDRESS is_leave_ok = ControlAddressList.FirstOrDefault(s => s.POSITION_KEY == stopper_keys[i] && s.ADDRESS_TYPE == ((int)AddressCategory.is_leaveOK).ToString());//获取对应停止器的允许放行信号
                                if (is_leave_ok != null)
                                {
                                    OPCHelper<JAC_KEYPART_YZ>.SynWriteOpcItem(is_leave_ok.ADDRESS_PATH, "1");//写入允许放行信号
                                }
                                #endregion
                            }
                        }
                        return;
                    }
                }
                SetLableText(op_status_lbls[i], "状态:条码格式错误。\n"+ barcode+"无法匹配。");
                SetColor(collect_panels[i], Color.Red);
            }
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
                lbl.Text = "采集进度(" + col.ToString() + "/" + all.ToString() + ")";
            }
        }
        public delegate void MessageBoxHandler();
        public void aaa()
        {
            this.Invoke(new MessageBoxHandler(delegate () { MessageBox.Show("千一网络"); }));// MessageBox.Show((IWin32Window)this, "提示"); // 由于放在 Invoke 中，也可以这么用，但效果和上面的一样。}))
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
                        //SetText(all_num_txt, "0");
                        //SetText(ok_num_txt, "0");
                        //SetText(notok_num_txt, "0");
                        //SetText(continued_notok_num_txt, "0");
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
                    string result = OPCHelper<JAC_KEYPART_YZ>.SynReadOpcItem(pro_born_address.ADDRESS_PATH);
                    if (result != null && (result == "True" || result == "False"))
                    {

                        LineCommunicationState = true;
                        //Task.Run(() =>
                        //{
                        //    try
                        //    {
                        ShowLineConnection(true);
                        //    }
                        //    catch
                        //    {

                        //    }
                        //});

                    }
                    else
                    {
                        LineCommunicationState = false;
                        //Task.Run(() =>
                        //{
                        //    try
                        //    {
                        ShowLineConnection(false);
                        //}
                        //catch
                        //{

                        //}
                        //});
                    }
                }
                catch (Exception ex)
                {
                    LineCommunicationState = false;
                    //Task.Run(() =>
                    //{
                    //    try
                    //    {
                    ShowLineConnection(false);
                    //    }
                    //    catch
                    //    {

                    //    }
                    //});
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
                    scanport1.Dispose();
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
                andon = new ANDON(basicinfor);
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
                //SetText(all_num_txt, wcinfor.AllNum.ToString());
                //SetText(ok_num_txt, wcinfor.OkNum.ToString());
                //SetText(notok_num_txt, wcinfor.NotOkNum.ToString());
                //SetText(continued_notok_num_txt, wcinfor.ContinueNotOkNum.ToString());
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
                    //SetVisible(guidfile_pcl, true);//显示文字指导控件
                    //SetVisible(label13, false);//显示文字指导控件
                    ShowGuidFilesdelegate(current_file);//界面显示作业文档
                }
                else//如果没有工艺指导文件
                {
                    //SetVisible(guidfile_pcl, false);//显示文字指导控件
                    //SetVisible(label13, true);//显示文字指导控件
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
                //ShowGuidFiles(gf);
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
        
        #region PLC地址跳变获取产品出生证

        public void GetBornCode(string opcname)
        {
            try
            {
                //CONTROL_ADDRESS OpcItem = ControlAddressList.Where(s => s.ADDRESS_PATH == opcname).FirstOrDefault();
                //if (OpcItem != null)
                //{
                //    if (OpcItem.ADDRESS_TYPE != null)
                //    {
                //        string a = AddressCategory.readRFID_success.ToString();
                //        int c = (int)AddressCategory.readRFID_success;//跳变信号类型
                //        if (OpcItem.ADDRESS_TYPE == c.ToString())
                //        {
                //            #region 读取产品出生证并显示在txt中
                //            CONTROL_ADDRESS pro_born_address = ControlAddressList.Where(s => s.POSITION_KEY == OpcItem.POSITION_KEY && s.ADDRESS_TYPE == ((int)AddressCategory.pro_born_code).ToString()).FirstOrDefault();//找到停止器对应的产品出生证地址
                //            if (pro_born_address != null)
                //            {
                //                string values = OPCHelper<JAC_KEYPART>.SynReadOpcItem(pro_born_address.ADDRESS_PATH);//读取地址的value,即产品出生证信息
                //                if(plan_product_data!=null&&values == plan_product_data.PRODUCT_BORN_CODE)//若读取重复产品ID信息，则不作任何处理
                //                {
                //                    return;
                //                }
                //            }
                //            #endregion
                //        }
                //    }
                //    else
                //    {
                //        Log.GetInstance.WriteLog("地址类型为空");
                //    }

                //}
                //else
                //{
                //    Log.GetInstance.WriteLog("没有找到跳变地址对应的地址信息");
                //}
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
                                //SetText(product_born_code_txt, values);//离线状态显示产品出生证
                                //SetCollectProgress(finish_lbl, 0, 0);//采集进度初始化
                                //SetColor(productarrival_lbl, HasCollectBtnColor);
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
    
        #region 其他
        
        public void DataChange(ItemValueResult item)
        {
            try
            {  //根据地址的类型，判断执行哪项逻辑操作
                OpcName = item.ItemName;
              
                if (item.Value.ToString() == "True")
                {
                    GetBornCode(OpcName);
                }
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog(ex.Message);
            }
        }

        #endregion

        #endregion

        #region 数据查询按钮
        private void keypart_infor_search_btn_Click(object sender, EventArgs e)
        {
            Keypart_Search = new HZ_Keypart_Search();
            Keypart_Search.ShowDialog();
        }
        #endregion

        private void collect_panel_L1020_Click(object sender, EventArgs e)
        {
            Keypart_Search = new HZ_Keypart_Search("L1020");
            Keypart_Search.ShowDialog();
        }

        private void collect_panel_L1025_Click(object sender, EventArgs e)
        {
            Keypart_Search = new HZ_Keypart_Search("L1025");
            Keypart_Search.ShowDialog();
        }

        private void collect_panel_L1030_Click(object sender, EventArgs e)
        {
            Keypart_Search = new HZ_Keypart_Search("L1030");
            Keypart_Search.ShowDialog();
        }

        private void collect_panel_L1050_Click(object sender, EventArgs e)
        {
            Keypart_Search = new HZ_Keypart_Search("L1050");
            Keypart_Search.ShowDialog();
        }

        private void collect_panel_L1060_Click(object sender, EventArgs e)
        {
            Keypart_Search = new HZ_Keypart_Search("L1060");
            Keypart_Search.ShowDialog();
        }
    }
}

