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
using HFUTIEMES;

namespace HfutIe
{
    public partial class JAC_REPAIR_JJ : Form, IOPC
    {
        public JAC_REPAIR_JJ()
        {
            InitializeComponent();
        }

        #region 仓储
        RepositoryFactory<P_PLAN> PlanRepositoryFactory = new RepositoryFactory<P_PLAN>();
        RepositoryFactory<P_PRODUCT_SERIAL> SerialRepositoryFactory = new RepositoryFactory<P_PRODUCT_SERIAL>();
        RepositoryFactory<P_ASSEMBLE_PRODUCT_STATE> AssembleRepositoryFactory = new RepositoryFactory<P_ASSEMBLE_PRODUCT_STATE>();
        RepositoryFactory<CONTROL_ADDRESS> Control_AddressRepositoryFactory = new RepositoryFactory<CONTROL_ADDRESS>();//控制地址
        RepositoryFactory<WORK_CENTER> WorkCenterRepositoryFactory = new RepositoryFactory<WORK_CENTER>();//工作中心信息
        RepositoryFactory<STATION> StationRepositoryFactory = new RepositoryFactory<STATION>();//工位信息
        RepositoryFactory<STOPPER> StopperRepositoryFactory = new RepositoryFactory<STOPPER>();//停止器信息
        RepositoryFactory<EQUIPMENT> EquipmentRepository = new RepositoryFactory<EQUIPMENT>();
        RepositoryFactory<CHECK_EQUIP_RESULT> CHECK_EQUIP_RESULTRepository = new RepositoryFactory<CHECK_EQUIP_RESULT>();//设备点检结果表
        RepositoryFactory<P_KEY_PART_INFOR> P_KEY_PART_INFORRepositoryFactory = new RepositoryFactory<P_KEY_PART_INFOR>();//安全件信息过程表
        static RepositoryFactory<P_KEY_PART_C> KEY_PART_CRepositoryFactory = new RepositoryFactory<P_KEY_PART_C>();//安全件采集配置表
        RepositoryFactory<DOC_KEY_PART_INFOR> DOC_KEY_PART_INFORRepositoryFactory = new RepositoryFactory<DOC_KEY_PART_INFOR>();//安全件信息档案表
        RepositoryFactory<DOC_EQUIP_STATUS> DOC_EQUIP_STATUSRepositoryFactory = new RepositoryFactory<DOC_EQUIP_STATUS>();//设备信息档案表
        RepositoryFactory<Base_Tag> Base_TagRepositoryFactory = new RepositoryFactory<Base_Tag>();//TAG基本信息表
        RepositoryFactory<Part_Supplier> SupplierRepositoryFactory = new RepositoryFactory<Part_Supplier>();//供应商基本信息表
        RepositoryFactory<P_NOTOK_PRODUCT_INFOR> P_NOTOK_PRODUCT_INFORRepositoryFactory = new RepositoryFactory<P_NOTOK_PRODUCT_INFOR>();//不合格产品信息过程表
        RepositoryFactory<DOC_NOTOK_PRODUCT_INFOR> DOC_NOTOK_PRODUCT_INFORRepositoryFactory = new RepositoryFactory<DOC_NOTOK_PRODUCT_INFOR>();//不合格产品信息档案表
        RepositoryFactory<Base_DataDictionary> Base_DataRepository = new RepositoryFactory<Base_DataDictionary>();
        RepositoryFactory<Base_DataDictionaryDetail> Base_DataDetailRepository = new RepositoryFactory<Base_DataDictionaryDetail>();
        RepositoryFactory<MATERIAL_WC_PART> MATERIAL_WC_PARTRepository = new RepositoryFactory<MATERIAL_WC_PART>();//线边库物料
        RepositoryFactory<CONCESSION_RELEASE> CONCESSION_RELEASERepository = new RepositoryFactory<CONCESSION_RELEASE>();//产品让步放行记录表
        static RepositoryFactory<P_PRODUCT_REPAIR_INFO> P_PRODUCT_REPAIR_INFORepositoryFactory = new RepositoryFactory<P_PRODUCT_REPAIR_INFO>();//产品返修信息过程表
        static RepositoryFactory<PRODUCT_FAULT_TYPE> PRODUCT_FAULT_TYPEepositoryFactory = new RepositoryFactory<PRODUCT_FAULT_TYPE>();//故障类型表
        static RepositoryFactory<PRODUCT_FAULT_ITEM> PRODUCT_FAULT_ITEMRepositoryFactory = new RepositoryFactory<PRODUCT_FAULT_ITEM>();//故障信息表
        static RepositoryFactory<PRODUCT_MAINTAIN_TYPE> PRODUCT_MAINTAIN_TYPERepositoryFactory = new RepositoryFactory<PRODUCT_MAINTAIN_TYPE>();//排故类型表
        static RepositoryFactory<PRODUCT_MAINTAIN_ITEM> PRODUCT_MAINTAIN_ITEMRepositoryFactory = new RepositoryFactory<PRODUCT_MAINTAIN_ITEM>();//排故信息表
        static RepositoryFactory<PRODUCT_FA_MAIN_INFOR> PRODUCT_FA_MAIN_INFORepositoryFactory = new RepositoryFactory<PRODUCT_FA_MAIN_INFOR>();//故障排故信息录入表
        static RepositoryFactory<P_REPAIR_TAG_INFO> P_REPAIR_TAG_INFORepositoryFactory = new RepositoryFactory<P_REPAIR_TAG_INFO>();//返修Tag信息过程表表
        static RepositoryFactory<DOC_REPAIR_TAG_INFO> DOC_REPAIR_TAG_INFORepositoryFactory = new RepositoryFactory<DOC_REPAIR_TAG_INFO>();//返修Tag信息档案表表
        #endregion

        #region 全局变量
        string station_code;
        int collection_sort_code = 1;//初始被采集编号为1，用于信息补录排序显示
        List<STATION> stationlist = new List<STATION>();//所有工位集合
        List<PRODUCT_FAULT_TYPE> faulttypelist = new List<PRODUCT_FAULT_TYPE>();//故障类型
        List<PRODUCT_FAULT_ITEM> faultlist = new List<PRODUCT_FAULT_ITEM>();//故障名称
        List<PRODUCT_MAINTAIN_TYPE> maintaintypelist = new List<PRODUCT_MAINTAIN_TYPE>();//排故类型
        List<PRODUCT_MAINTAIN_ITEM> maintainlist = new List<PRODUCT_MAINTAIN_ITEM>();//排故名称
        DataTable dt_repaironline_stations = new DataTable();//返修上线工位集合
        DataTable dt_repairoffline_stations = new DataTable();//返修下线工位集合
        DataTable dt_fault_type = new DataTable();//故障类型
        DataTable dt_fault_item = new DataTable();//故障名称
        DataTable dt_maintain_type = new DataTable();//排故类型
        DataTable dt_maintain_item = new DataTable();//排故名称
        public static bool iswritetag = true;
        List<Base_Tag> basetaginfolist = new List<Base_Tag>();//TAG
        List<EQUIPMENT> equiplist = new List<EQUIPMENT>();
        #endregion

        #region 内存公共变量
        List<CONTROL_ADDRESS> ControlAddressList;//该工位的停止器所配置的地址信息
        List<CHECK_INFO> CheckInfoLists;//主工位对应的设备点检信息
        string station_type;//返修工位对应的工位类型，用于区分读写头为PLC控制或串口控制
        string OpcName;
        string mainstationkey;//主工位主键
        static string strpath = "E:\\#1#纳威司达项目\\安全件1.png";
        public string wc_code = ReadXML.GetWCCode(System.Windows.Forms.Application.StartupPath + @"\WCConfig\WC.xml");//工作中心编号
        List<STATION> StationList = new List<STATION>();//该PC对应的工作中心下所属工位的集合
        List<STATION> stationlist_productionline = new List<STATION>();//根据产品对应产线获取部分工位信息
        WorkCenterInfor wcinfor;
        string plan_code;//当前产品所属计划
        //int finishcollected = 0;//finishcollected枚举值{0：未开始采集，1：一开始采集，未采集完成，2：采集完成}
        public BasicInfoDto basicinfor;//根据工位得出的基本信息
        public List<P_KEY_PART_C> key_part_c;//安全件采集配置
        public List<Part_Supplier> supplier;//安全件供应商信息
        //public DataTable team_shift;//班组班制信息
        public P_ASSEMBLE_PRODUCT_STATE plan_product_data;//通过产品出生证查到的该产品出生证代表的计划、产品信息
        List<P_KEY_PART_C> KeyPartData = new List<P_KEY_PART_C>();//根据产品出生证和工位号查询到本次需要采集的安全件信息，逐个多条(按数量分解成单个安全件，无上限限制，用预留字段1标识是否被加载到界面上过：0-未加载；1-已加载)
        
        Label[] vice_station_code_lbls;//非主工位label
        Label[] equ_check_lbls;//设备点检项label

        Color HasCollectBtnColor = Color.FromArgb(57, 204, 36);//已采集安全件确认按钮颜色
        Color NotCollectBtnColor = Color.FromArgb(5, 90, 150);//未采集安全件确认按钮颜色

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

                LoadFaultInfo();//获取并加载故障排故信息及返修上线工位
                //LoadRepairOffLineStation();//加载返修下线工位
                ShowServerConnectionState();
                
                //SetViceStationLabel();

                Task.Run(() => RecordTimeShow());
                product_born_code_txt.Focus();
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
                if (ServerCommunicationState == true)
                {
                    WORK_CENTER wcentity = WorkCenterRepositoryFactory.Repository().FindEntity("Work_Center_Code", wc_code);
                    StationList = StationRepositoryFactory.Repository().FindList().Where(s => s.WORK_CENTER_KEY == wcentity.WORK_CENTER_KEY).ToList();
                }
                else
                {
                    WORK_CENTER wcentity = WorkCenterRepositoryFactory.Repository(DatabaseType.SQLite).FindEntity("Work_Center_Code", wc_code);
                    StationList = StationRepositoryFactory.Repository(DatabaseType.SQLite).FindList().Where(s => s.WORK_CENTER_KEY == "b77bbfaa-8384-4036-b6e5-3bd4b34e808e").ToList();
                }
                mainstationkey = StationList.FindAll(s => s.IS_MAIN == "是").Select(s => s.STATION_KEY).FirstOrDefault();//主工位主键
                InitializationOnlineOrOffline();//在线/离线初始化
                if (ServerCommunicationState == true)
                {
                    InitializationOnline();//在线初始化
                }
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
                station_type = StationList.Find(s => s.IS_MAIN == "是").STATION_TYPE;//获取工位类型
                //Task.Run(() => SynchronizeKey_Part_C(StationList.Select(s => s.STATION_CODE).ToList()));
                basetaginfolist = Base_TagRepositoryFactory.Repository().FindList();//获取所有tag信息
                equiplist = EquipmentRepository.Repository().FindList("EQUIPMENT_MONITOR", "是");//获取所有设备信息
                supplier = GetData.GetSupplierInfor();

                //team_shift = GetData.GetTeamByWccode(wc_code);

                if (basicinfor == null || /*key_part_c.Rows.Count < 1 ||*/ supplier.Count < 1)
                {
                    DialogResult dr = MyMsgBox.Show("基本信息获取失败！是否终止启动系统？", "信息提示", MyMsgBox.MyButtons.YesNo, MyMsgBox.MyIcon.Error);
                    if (dr == DialogResult.Yes)
                    {
                        Formclosing();
                        //System.Windows.Forms.Application.Exit();
                    }
                }

                #region 图标显示(在线)
                //string pages = WebApiHttp.HttpGet($"http://{ServerDictionary.IPCode}/API/WebApiPage/GetPageInfo");//WebApi获取图标文件包
                ////string pages = WebApiHttp.HttpGet($"http://localhost:28188/API/WebApiPage/GetPageInfo");//WebApi获取图标文件包
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

        /// <summary>
        /// 获取并加载故障排故信息
        /// </summary>
        private void LoadFaultInfo()
        {
            try
            {
                faulttypelist = PRODUCT_FAULT_TYPEepositoryFactory.Repository().FindList().Where(s => s.PRODUCT_FAULT_TYPE_KEY != "1").ToList();
                faultlist = PRODUCT_FAULT_ITEMRepositoryFactory.Repository().FindList();
                maintaintypelist = PRODUCT_MAINTAIN_TYPERepositoryFactory.Repository().FindList().Where(s => s.PRODUCT_MAINTAIN_TYPE_KEY != "1").ToList();
                maintainlist = PRODUCT_MAINTAIN_ITEMRepositoryFactory.Repository().FindList();
                stationlist = StationRepositoryFactory.Repository().FindList().OrderBy(s=>s.STATION_CODE).ToList();//获取所有工位信息
                Getdic();//故障类型和排故类型DataTable创建
                         //Getdic_fault_item();//故障信息DataTable创建
                         //Getdic_maintain_item();//排故信息DataTable创建
                SetComDataSource();//DataTable数据源赋值
            }
            catch
            {

            }
        }

        #region 创建DataTable
        private void Getdic()
        {
            try
            {
                #region 故障类型
                dt_fault_type.Clear();
                dt_fault_type.Columns.Add("fault_type_name");
                dt_fault_type.Columns.Add("fault_type_key");
                DataRow dr1 = dt_fault_type.NewRow();
                dr1[0] = "==请选择==";
                dt_fault_type.Rows.Add(dr1);
                for (int i = 0; i < faulttypelist.Count; i++)
                {
                    DataRow dr = dt_fault_type.NewRow();
                    dr[0] = faulttypelist[i].PRODUCT_FAULT_TYPE_NAME;
                    dr[1] = faulttypelist[i].PRODUCT_FAULT_TYPE_KEY;
                    dt_fault_type.Rows.Add(dr);
                }
                fault_type_cobox.DisplayMember = "fault_type_name";
                fault_type_cobox.ValueMember = "fault_type_key";
                #endregion

                #region 排故类型
                dt_maintain_type.Clear();
                dt_maintain_type.Columns.Add("maintain_type_name");
                dt_maintain_type.Columns.Add("maintain_type_key");
                DataRow dr2 = dt_maintain_type.NewRow();
                dr2[0] = "==请选择==";
                dt_maintain_type.Rows.Add(dr2);
                for (int i = 0; i < maintaintypelist.Count; i++)
                {
                    DataRow dr = dt_maintain_type.NewRow();
                    dr[0] = maintaintypelist[i].PRODUCT_MAINTAIN_TYPE_NAME;
                    dr[1] = maintaintypelist[i].PRODUCT_MAINTAIN_TYPE_KEY;
                    dt_maintain_type.Rows.Add(dr);
                }
                //foreach (PRODUCT_MAINTAIN_TYPE item in maintaintypelist)
                //{
                //    DataRow dr = dt_maintain_type.NewRow();
                //    dr[0] = item.PRODUCT_MAINTAIN_TYPE_NAME;
                //    dr[1] = item.PRODUCT_MAINTAIN_TYPE_KEY;
                //    dt_maintain_type.Rows.Add(dr);
                //}
                maintain_type_cobox.DisplayMember = "maintain_type_name";
                maintain_type_cobox.ValueMember = "maintain_type_key";
                #endregion

                #region 故障项
                dt_fault_item.Clear();
                dt_fault_item.Columns.Add("fault_item_name");
                dt_fault_item.Columns.Add("fault_item_key");
                DataRow dr3 = dt_fault_item.NewRow();
                dr3[0] = "==请选择==";
                dt_fault_item.Rows.Add(dr3);
                for (int i = 0; i < faultlist.Count; i++)
                {
                    DataRow dr = dt_fault_item.NewRow();
                    dr[0] = faultlist[i].PRODUCT_FAULT_ITEM_NAME;
                    dr[1] = faultlist[i].PRODUCT_FAULT_ITEM_KEY;
                    dt_fault_item.Rows.Add(dr);
                }
                fault_item_cobox.DisplayMember = "fault_item_name";
                fault_item_cobox.ValueMember = "fault_item_key";
                #endregion

                #region 排故项
                dt_maintain_item.Clear();
                dt_maintain_item.Columns.Add("maintain_item_name");
                dt_maintain_item.Columns.Add("maintain_item_key");
                DataRow dr4 = dt_maintain_item.NewRow();
                dr4[0] = "==请选择==";
                dt_maintain_item.Rows.Add(dr4);
                for (int i = 0; i < maintainlist.Count; i++)
                {
                    DataRow dr = dt_maintain_item.NewRow();
                    dr[0] = maintainlist[i].PRODUCT_MAINTAIN_TYPE_NAME;
                    dr[1] = maintainlist[i].PRODUCT_MAINTAIN_TYPE_KEY;
                    dt_maintain_item.Rows.Add(dr);
                }
                maintain_item_cobox.DisplayMember = "maintain_item_name";
                maintain_item_cobox.ValueMember = "maintain_item_key";
                #endregion
            }
            catch
            {

            }
        }
        #endregion

        #region CoBox数据源赋值
        private void SetComDataSource()
        {
            //fault_type_cobox.DataSource = dt_fault_type;
            //fault_item_cobox.DataSource = dt_fault_item;
            //maintain_type_cobox.DataSource = dt_maintain_type;
            //maintain_item_cobox.DataSource = dt_maintain_item;
            SetComboBoxData(fault_type_cobox, dt_fault_type);
            SetComboBoxData(fault_item_cobox, dt_fault_item);
            SetComboBoxData(maintain_type_cobox, dt_maintain_type);
            SetComboBoxData(maintain_item_cobox, dt_maintain_item);

        }
        #endregion

        private void SynchronizeKey_Part_C(List<string> StationCodeList)
        {
            key_part_c = KeyPartGetData.GetKeyPartConfig(StationCodeList);
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
        

        #region #1#获取到产品出生证
        public void GetProductBornCode(string product_born_code)//获取到新产品出生证
        {
            try
            {
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

                int IsOk = Data();//根据产品出生证显示相应数据，计划编号，产品编号，批次号等
                if (IsOk == 1)
                {
                    stationlist_productionline = stationlist.Where(s => s.PRODUCTION_LINE_KEY == plan_product_data.PRODUCTION_LINE_KEY).ToList();
                    #region 返修下线工位
                    dt_repairoffline_stations = new DataTable();
                    dt_repairoffline_stations.Clear();
                    dt_repairoffline_stations.Columns.Add("station_code");
                    dt_repairoffline_stations.Columns.Add("station_key");
                    DataRow dr6 = dt_repairoffline_stations.NewRow();
                    dr6[0] = "==请选择==";
                    dt_repairoffline_stations.Rows.Add(dr6);
                    for (int i = 0; i < stationlist_productionline.Count; i++)
                    {
                        DataRow dr = dt_repairoffline_stations.NewRow();
                        dr[0] = stationlist_productionline[i].STATION_CODE;
                        dr[1] = stationlist_productionline[i].STATION_KEY;
                        dt_repairoffline_stations.Rows.Add(dr);
                    }
                    SetComboBoxAttribute(repair_offline_station_cobox, "station_code", "station_key");//设置ComBox属性
                    if (dt_repairoffline_stations.Rows.Count > 1)
                        SetComboBoxData(repair_offline_station_cobox, dt_repairoffline_stations);
                    #endregion

                    #region 返修上线工位
                    dt_repaironline_stations = new DataTable();
                    dt_repaironline_stations.Clear();
                    dt_repaironline_stations.Columns.Add("station_code");
                    dt_repaironline_stations.Columns.Add("station_key");
                    DataRow dr5 = dt_repaironline_stations.NewRow();
                    dr5[0] = "==请选择==";
                    dt_repaironline_stations.Rows.Add(dr5);
                    P_PRODUCT_REPAIR_INFO repairentity = P_PRODUCT_REPAIR_INFORepositoryFactory.Repository().FindList().Where(s => s.PRODUCT_BORN_CODE == plan_product_data.PRODUCT_BORN_CODE && s.REPAIR_STATE == "返修下线").FirstOrDefault();//获取返修信息表中对应返修件的信息
                    List<STATION> stations_choose = stationlist_productionline;
                    if (repairentity != null && repairentity.PRODUCT_REPAIR_INFO_KEY != null)//产品已进入返修下线时进行相应操作
                    {
                        if (repairentity.REPAIR_OFFLINE_STATION_CODE.Contains("40OP"))
                        {
                            stations_choose = stationlist.Where(s => s.STATION_CODE.Contains("40OP") && int.Parse(s.STATION_CODE.Substring(6, 4)) <= int.Parse(repairentity.REPAIR_OFFLINE_STATION_CODE.Substring(6, 4))&& s.STATION_CODE.Contains("F")).OrderBy(s=>s.STATION_CODE).OrderBy(s=>s.STATION_CODE).ToList();
                            if (repairentity.REPAIR_OFFLINE_STATION_CODE.Contains("7195"))
                            {
                                stations_choose = stations_choose.Where(s => s.STATION_CODE.Contains("7195")).ToList();
                            }
                            else
                            {
                                stations_choose = stations_choose.Where(s => !s.STATION_CODE.Contains("7195")).ToList();
                            }
                        }
                        else if (repairentity.REPAIR_OFFLINE_STATION_CODE.Contains("27OP"))
                        {
                            stations_choose = stationlist.Where(s =>s.STATION_CODE.Length>=10&& s.STATION_CODE.Contains("27OP") && int.Parse(s.STATION_CODE.Substring(6, 4)) <= int.Parse(repairentity.REPAIR_OFFLINE_STATION_CODE.Substring(6, 4)) && s.STATION_CODE.Contains("F")).OrderBy(s => s.STATION_CODE).ToList();
                            if (repairentity.REPAIR_OFFLINE_STATION_CODE.Contains("A"))
                            {
                                stations_choose = stations_choose.Where(s => s.STATION_CODE.Contains("A")).ToList();
                            }
                            else if(repairentity.REPAIR_OFFLINE_STATION_CODE.Contains("C"))
                            {
                                stations_choose = stations_choose.Where(s => !s.STATION_CODE.Contains("C")).ToList();
                            }
                        }
                        for (int i = 0; i < stations_choose.Count; i++)
                        {
                            DataRow dr = dt_repaironline_stations.NewRow();
                            dr[0] = stations_choose[i].STATION_CODE;
                            dr[1] = stations_choose[i].STATION_KEY;
                            dt_repaironline_stations.Rows.Add(dr);
                        }
                        SetComboBoxAttribute(repair_online_station_cobox, "station_code", "station_key");//设置ComBox属性
                        if (dt_repaironline_stations.Rows.Count > 1)
                            SetComboBoxData(repair_online_station_cobox, dt_repaironline_stations);

                        #region 显示返修下线，上线工位
                        SetText(current_product_status_lbl, "返修下线");
                        SetText(repair_offline_station_cobox, repairentity.REPAIR_OFFLINE_STATION_CODE);//加载返修下线工位
                        SetEnabled(repair_offline_station_cobox, false);//产品处于返修下线状态时，无法操作返修下线工位选择下拉框
                        SetEnabled(repair_online_station_cobox, true);//产品处于返修下线状态时，可选择返修上线工位
                      
                        #endregion
                    }
                    else
                    {
                        SetText(current_product_status_lbl, "主线流转");
                    }
                    #endregion


                    //Thread.Sleep(1000);
                    
                    //SetEnabled(product_born_code_txt, false);//使产品出生证text控件不可操作

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

        #region 根据产品出生证，得到产品信息和需要采集的安全件信息，并加以显示
        private int Data()
        {
            try
            {
                if (ServerCommunicationState)
                {
                    plan_product_data = KeyPartGetData.GetPlanDatabyProductBornCode(product_born_code_txt.Text);
                    int res = Handle_plan_product_data();//显示界面信息(计划，安全件等信息)
                    if (res == 0)
                    {
                        return 0;
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
                SetText(product_born_code_txt, "产品出生证输入不正确！请重试。");
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
                    return 1;
                }
                else
                {
                    MyMsgBox.Show("产品未加工或已加工完成。", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information,5);
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
                    SetText(product_name_txt, entity.PRODUCT_NAME);//显示产品名称
                    plan_code = entity.MES_PLAN_CODE;//记录当前计划编号信息，用于计划切换时提示
                }
                else
                {
                    SetText(plan_code_txt, "");//显示计划编号
                    SetText(product_code_txt, "");//显示产品出生证
                    SetText(product_name_txt, "");//显示产品名称
                }
            }
            catch (Exception ex)
            {
                SetText(product_born_code_txt, "该产品信息不完整！请重试。");
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("该产品信息不完整！请重试。" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        #endregion
        
        #region 获取即将绑定的安全件信息——开始绑定安全件信息
        /// <summary>
        /// 获取绑定安全件信息
        /// </summary>
        /// <param name="keypartbarcode">安全件条形码</param>
        /// <returns></returns>
        public int BindKeyPartInfor(string partcode, string barcode)
        {
            //i用来判断keypartbarcode是第几个条形码，即是由第几个OK调用此方法的
            Repository<P_KEY_PART_INFOR> tran = new Repository<P_KEY_PART_INFOR>();
            try
            {
                int IsOk = 0;
                if (ServerCommunicationState)
                {
                    //string keypartcode = DeBarcode.GetPartCode(keypartbarcode);//解析条码判断条码正确性
                    //此处的条形码正不正确的判断主要是为了检验手动输入的条码的正确性，扫描的条码走到这一步，说明一定是有匹配的，且匹配的是第i个，验证扫描条码是否匹配的方法：CheckBarcodeNum()
                    //if (keypartcode == key_part_code_txts[i].Text)//条形码正确
                    P_KEY_PART_INFOR p_key_part_infor = new P_KEY_PART_INFOR();//当前采集的安全件信息
                    MATERIAL_WC_PART material_wc_part = new MATERIAL_WC_PART();
                    //key_part_barcode_txts[i].Enabled = false;
                    p_key_part_infor = Accept(p_key_part_infor, partcode, barcode);
                    // ReduceQuantity(material_wc_part, i);
                    bool result = KeyPartOperationDataBase(p_key_part_infor);//提交安全件信息
                    if (result == false) //提交安全件绑定信息失败
                    {
                        IsOk = 0;
                    }
                    else
                    {
                        IsOk = 1;
                    }
                }
                else
                {
                    MessageBox.Show("网络异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return IsOk;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                //ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                //log.Fatal("绑定安全件失败！" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
                return 0;
            }
        }
        #endregion

        #region 正在绑定——提交安全件信息
        /// <summary>
        /// 提交安全件与产品的绑定数据
        /// </summary>
        /// <param name="key_part_barcode">扫描的、或者手动输入的安全件条码</param>
        /// <param name="i">第几个需要采集的安全件条码，可能有多个需要采集的安全件条码，用i加以区分</param>
        /// <returns></returns>
        private P_KEY_PART_INFOR Accept(P_KEY_PART_INFOR p_key_part_infor, string partcode, string barcode)
        {
            try
            {
                int IsOk = 0;
                if (ServerCommunicationState)
                {
                    P_KEY_PART_C KeyPartData = KEY_PART_CRepositoryFactory.Repository().FindList().Where(s => s.PRODUCT_CODE == plan_product_data.PRODUCT_CODE && s.PART_CODE == partcode && s.MES_PLAN_CODE == plan_product_data.MES_PLAN_CODE).FirstOrDefault();
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
                    p_key_part_infor.PART_VINCODE = barcode;//为实体增加安全件条形码信息
                    p_key_part_infor.PART_BARCODE = barcode;//为实体增加安全件条形码信息
                    p_key_part_infor.KEY_PART_C_KEY = KeyPartData.KEY_PART_C_KEY;//为实体增加安全件采集配置key信息
                    p_key_part_infor.PART_KEY = KeyPartData.PART_KEY;//为实体增加安全件key信息
                    p_key_part_infor.PART_CODE = KeyPartData.PART_CODE;//为实体增加安全件编号信息
                    p_key_part_infor.PART_NAME = KeyPartData.PART_NAME;//为实体增加安全件名称信息
                    //p_key_part_infor.PART_BATCH_NO = part_batch_no;//为实体增加安全件批次号信息
                    p_key_part_infor.QUANTITY = Convert.ToInt32(KeyPartData.QUANTITY); //为实体增加安全件数量信息

                    #region 设备加工记录信息

                    //doc_equip_status = EntityToEntity<DOC_EQUIP_STATUS, P_KEY_PART_INFOR>.EntityChange(doc_equip_status, p_key_part_infor);
                    //doc_equip_status = plan_product_data.EntityMapper<P_ASSEMBLE_PRODUCT_STATE, DOC_EQUIP_STATUS>();//这条语句主要是为了获取bom_key
                    #endregion

                    #region 不合格品信息
                    //p_notok_product_infor = p_key_part_infor.EntityMapper<P_KEY_PART_INFOR, P_NOTOK_PRODUCT_INFOR>();
                    //p_notok_product_infor = plan_product_data.EntityMapper<P_ASSEMBLE_PRODUCT_STATE, P_NOTOK_PRODUCT_INFOR>(); //这条语句主要是为了获取bom_key
                    #endregion
                }
                else
                {
                    MessageBox.Show(this, "网络异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return p_key_part_infor;
            }
            catch (Exception ex)
            {
                //SetText(key_part_barcode_txts[i], "提交安全件信息失败！");
                //SetColor(keypartbarcode_lbl, Color.Red);
                //ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                //log.Fatal("提交安全件信息失败！" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
                return null;
            }
        }
        #endregion

        /// <summary>
        /// 提交安全件信息
        /// </summary>
        /// <param name="partcode"></param>安全件编号
        /// <returns></returns>
        private bool KeyPartOperationDataBase(P_KEY_PART_INFOR p_key_part_infor)
        {
            Repository<P_KEY_PART_INFOR> tran = new Repository<P_KEY_PART_INFOR>();
            DbTransaction isOpenTrans = tran.BeginTrans();
            string barcode_choose = p_key_part_infor.PART_BARCODE;
            try
            {
                P_KEY_PART_C KeyPartData = KEY_PART_CRepositoryFactory.Repository().FindList().Where(s => s.KEY_PART_C_KEY == p_key_part_infor.KEY_PART_C_KEY).FirstOrDefault();
                #region 安全件数据
                //p_key_part_infor = basicinfor[0].EntityMapper<BasicInfoDto, P_KEY_PART_INFOR>();
                p_key_part_infor.Create();
                DOC_KEY_PART_INFOR doc_key_part_infor = p_key_part_infor.EntityMapper<P_KEY_PART_INFOR, DOC_KEY_PART_INFOR>();

                //判断档案表中是否已经存在此条记录
                List<DOC_KEY_PART_INFOR> doc_key_part_inforlist = DOC_KEY_PART_INFORRepositoryFactory.Repository().FindList().Where(s => s.MES_PLAN_KEY == plan_product_data.MES_PLAN_KEY && s.PRODUCT_BORN_CODE == plan_product_data.PRODUCT_BORN_CODE && s.PART_CODE == p_key_part_infor.PART_CODE).ToList();
                if (doc_key_part_inforlist.Count > 0) //数据库中有同计划、同产品、同零部件的记录
                {
                    if (doc_key_part_inforlist.Count == 1)
                    {
                        doc_key_part_infor.KEY_PART_INFOR_KEY = doc_key_part_inforlist[0].KEY_PART_INFOR_KEY.ToString();//如果存在记录，则将本条记录的key赋值为从数据库中查到的key，从而更新记录
                        DOC_KEY_PART_INFORRepositoryFactory.Repository().Update(doc_key_part_infor, isOpenTrans);//更新记录
                    }
                    else//如果该产品已绑定的相同物料编号的安全件的个数>1，则弹框选择需要替换的安全件条码
                    {
                        List<string> barcodelist = doc_key_part_inforlist.Select(s => s.PART_BARCODE).ToList();
                        ChoseReplace choseform = new ChoseReplace(barcodelist);
                        if (choseform.ShowDialog() == DialogResult.OK)
                        {
                            barcode_choose = choseform.BarcodeChoose;//选择需要替换的安全件条码
                            DOC_KEY_PART_INFOR docentity = doc_key_part_inforlist.Find(s => s.PART_BARCODE == barcode_choose);
                            doc_key_part_infor.KEY_PART_INFOR_KEY = docentity.KEY_PART_INFOR_KEY.ToString();//如果存在记录，则将本条记录的key赋值为从数据库中查到的key，从而更新记录
                            DOC_KEY_PART_INFORRepositoryFactory.Repository().Update(doc_key_part_infor, isOpenTrans);//更新记录
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else//数据库中没有相应记录
                {
                    DOC_KEY_PART_INFORRepositoryFactory.Repository().Insert(doc_key_part_infor, isOpenTrans);//插入记录
                }
                //判断过程表中是否已经存在此条记录
                List<P_KEY_PART_INFOR> p_key_part_inforlist = P_KEY_PART_INFORRepositoryFactory.Repository().FindList().Where(s => s.MES_PLAN_KEY == plan_product_data.MES_PLAN_KEY && s.PRODUCT_BORN_CODE == plan_product_data.PRODUCT_BORN_CODE && s.PART_CODE == p_key_part_infor.PART_CODE && s.PART_BARCODE == barcode_choose).ToList();//找到多应安全件条码的安全件配置信息
                if (p_key_part_inforlist.Count > 0)//数据库中有同计划、同产品、同零部件的记录
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
                return true;
            }
            catch (Exception ex)
            {
                isOpenTrans.Rollback();
                throw ex;
            }
        }

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
        /// 使用委托为dgv赋值
        /// <param name="pb">dgv控件名</param>
        /// <param name="st">要赋值的List</param>
        private delegate void SetDgvDataSourceDelegate<T>(DataGridView pb, List<T> list);
        private void SetDgvDataSource<T>(DataGridView dgv, List<T> st)
        {
            //string ID = Thread.CurrentThread.ManagedThreadId.ToString();//获取线程ID，可以进行跨线程调试
            if (dgv.InvokeRequired) Invoke(new SetDgvDataSourceDelegate<T>(SetDgvDataSource), dgv, st);
            else dgv.DataSource = st;
        }

        /// 使用委托为dgv赋值
        /// <param name="pb">dgv控件名</param>
        /// <param name="st">要赋值的List</param>
        private delegate void SetDgvDataSourceByDataTableDelegate(DataGridView pb, DataTable dt);
        private void SetDgvDataSourceByDataTable(DataGridView dgv, DataTable dt)
        {
            //string ID = Thread.CurrentThread.ManagedThreadId.ToString();//获取线程ID，可以进行跨线程调试
            if (dgv.InvokeRequired) Invoke(new SetDgvDataSourceByDataTableDelegate(SetDgvDataSourceByDataTable), dgv, dt);
            else dgv.DataSource = dt;
        }

        /// <summary>
        /// 设置ComboBox属性
        /// </summary>
        /// <param name="combobox">控件名</param>
        /// <param name="dt">数据</param>
        private delegate void SetComboBoxAttributeSourceDelegate(ComboBox combobox, string displaymember , string valuemember);
        private void SetComboBoxAttribute(ComboBox combobox, string displaymember, string valuemember)
        {
            //string ID = Thread.CurrentThread.ManagedThreadId.ToString();//获取线程ID，可以进行跨线程调试
            if (combobox.InvokeRequired) Invoke(new SetComboBoxAttributeSourceDelegate(SetComboBoxAttribute), combobox, displaymember, valuemember);
            else combobox.DisplayMember = displaymember; combobox.ValueMember = valuemember;
        }
        /// <summary>
        /// 为ComboBox赋数据源
        /// </summary>
        /// <param name="combobox">控件名</param>
        /// <param name="dt">数据</param>
        private delegate void SetComboBoxDataSourceDelegate(ComboBox combobox, DataTable dt);
        private void SetComboBoxData(ComboBox combobox, DataTable dt)
        {
            //string ID = Thread.CurrentThread.ManagedThreadId.ToString();//获取线程ID，可以进行跨线程调试
            if (combobox.InvokeRequired) Invoke(new SetComboBoxDataSourceDelegate(SetComboBoxData), combobox, dt);
            else combobox.DataSource = dt;
        }
        #endregion
        
        #region 界面恢复初始状态

        private void InitializationForm(bool resetcollectedcount)
        {
            try
            {
                //SetText(product_born_code_txt, "");
                SetText(product_code_txt, "");
                SetText(product_name_txt, "");
                SetText(plan_code_txt, "");
                //SetEnabled(product_born_code_txt, false);
                
                DataTable dt_section_sta = new DataTable();
                dt_section_sta.Columns.Add("oper_station_key");
                dt_section_sta.Columns.Add("oper_station_code");
            
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
                    SetText(showtime_lbl, datetime); }
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
                    ShowServerConnectionState();

                Thread.Sleep(500);
            }
            catch
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
                    //SetLblColors(serverconnectionstate_lbl, Color.FromArgb(57, 204, 36));
                    serverconnectionstate_pulseButton.Set_ColorBottom(Color.FromArgb(57, 204, 36));
                    serverconnectionstate_pulseButton.Set_ColorTop(Color.FromArgb(57, 204, 36));
                    serverconnectionstate_pulseButton.Set_ForeColor(Color.FromArgb(57, 204, 36));
                    serverconnectionstate_pulseButton.Set_PulseColor(Color.FromArgb(57, 204, 36));

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
                    Formclosing();
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
        
        #region PLC地址跳变获取产品出生证
        /// <summary>
        /// 跳变
        /// </summary>
        /// <param name="subscriptionHandle">OPC组名</param>
        /// <param name="name">Item</param>
        /// <param name="value">跳变值</param>
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
        public void GetBornCode(string opcname)
        {
            try
            {
                CONTROL_ADDRESS OpcItem = ControlAddressList.Where(s => s.ADDRESS_PATH == opcname).FirstOrDefault();
                if (OpcItem != null)
                {
                    if (OpcItem.ADDRESS_TYPE != null)
                    {
                        Log.GetInstance.WriteLog(OpcItem.ADDRESS_TYPE);
                        //string a = AddressCategory.repair_offline.ToString();
                        int repair_offline_sign = (int)AddressCategory.repair_offline;//返修下线跳变信号类型
                        int repair_online_sign = (int)AddressCategory.repair_online;//返修上线跳变信号类型
                        int writeRFID_success_sign= (int)AddressCategory.writeRFID_success;//写RFID成功跳变信号类型
                        if (OpcItem.ADDRESS_TYPE == repair_offline_sign.ToString())//接收返修下线信号
                        {
                            #region 读取产品出生证并显示在txt中

                            #region 2.7返修下线读取出生证信息
                            if (wc_code.Contains("2.7"))//2.7返修下线读取出生证信息
                            {
                                if (station_type.Contains("PLC"))
                                {
                                    CONTROL_ADDRESS pro_born_address = ControlAddressList.Where(s => s.POSITION_KEY == OpcItem.POSITION_KEY && s.ADDRESS_PATH.Contains("Read.pro_born_code")).FirstOrDefault();//找到停止器对应的产品出生证地址(与返修上线不同地址位)
                                    if (pro_born_address != null)
                                    {
                                        string values = OPCHelper<JAC_REPAIR>.SynReadOpcItem(pro_born_address.ADDRESS_PATH);//读取地址的value,即产品出生证信息
                                        GetProductBornCode(values);
                                        RepairOffLine();//自动触发返修下线记录信息逻辑
                                    }
                                }
                                else//串口控制读写头
                                {
                                    bool ok = BalluffRfidHelper.OpenRfidCom(serialport_rfid);//打开串口
                                    if (ok)
                                    {
                                        string born_code = BalluffRfidHelper.ReadRfid(1046, 9, serialport_rfid, 5).Trim();
                                        Log.GetInstance.WriteLog("返修下线串口读取出生证：" + born_code);
                                        if (born_code != null)
                                        {
                                            GetProductBornCode(born_code);//获取到出生证信息并在页面加载相关信息
                                            RepairOffLine();//自动触发返修下线记录信息逻辑
                                        }
                                        else
                                        {
                                            Log.GetInstance.WriteLog(BalluffRfidHelper.LastErrorInfo);
                                            Log.GetInstance.WriteLog("读取产品出生证失败！");
                                        }
                                    }
                                    else
                                    {
                                        MyMsgBox.Show("串口打开失败！请检查配置文件和硬件连接！", "提醒", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information, 5);
                                        return;
                                    }
                                    BalluffRfidHelper.CloseRfidCom(serialport_rfid);//关闭串口
                                }
                            }
                            #endregion

                            #region 4.0返修下线读取出生证信息
                            else//4.0返修下线读取出生证信息
                            {
                                CONTROL_ADDRESS pro_born_address = ControlAddressList.Where(s => s.POSITION_KEY == OpcItem.POSITION_KEY && s.ADDRESS_PATH.Contains("Read.pro_born_code")).FirstOrDefault();//找到停止器对应的产品出生证地址(与返修上线不同地址位)
                                if (pro_born_address != null)
                                {
                                    string values = OPCHelper<JAC_REPAIR>.SynReadOpcItem(pro_born_address.ADDRESS_PATH);//读取地址的value,即产品出生证信息
                                    GetProductBornCode(values);
                                    RepairOffLine();//自动触发返修下线记录信息逻辑
                                }
                            }
                            #endregion

                            #endregion
                        }
                        else if(OpcItem.ADDRESS_TYPE == repair_online_sign.ToString())//接收返修上线信号
                        {
                            #region 读取产品出生证并显示在txt中

                            #region 2.7返修上线读取出生证信息
                            if (wc_code.Contains("2.7"))//2.7返修上线读取出生证信息
                            {
                                if (station_type.Contains("PLC"))
                                {
                                    CONTROL_ADDRESS pro_born_address = ControlAddressList.Where(s => s.POSITION_KEY == OpcItem.POSITION_KEY && s.ADDRESS_TYPE == ((int)AddressCategory.pro_born_code).ToString()).FirstOrDefault();//找到停止器对应的产品出生证地址(与返修下线不同地址位)
                                    if (pro_born_address != null)
                                    {
                                        string values = OPCHelper<JAC_REPAIR>.SynReadOpcItem(pro_born_address.ADDRESS_PATH);//读取地址的value,即产品出生证信息
                                        GetProductBornCode(values);
                                    }
                                }
                                else//串口控制读写头
                                {
                                    BalluffRfidHelper.wc_code = StationList.Where(s => s.IS_MAIN == "是").Select(s => s.STATION_CODE).FirstOrDefault();
                                    bool ok = BalluffRfidHelper.OpenRfidCom(serialport_rfid);//打开串口
                                    if (ok)
                                    {
                                        string born_code = BalluffRfidHelper.ReadRfid(1046, 9, serialport_rfid, 5).Trim();
                                        if (born_code != null)
                                        {
                                            GetProductBornCode(born_code);//获取到出生证信息并在页面加载相关信息
                                        }
                                        else
                                        {
                                            Log.GetInstance.WriteLog(BalluffRfidHelper.LastErrorInfo);
                                            Log.GetInstance.WriteLog("读取产品出生证失败！");
                                        }
                                    }
                                    else
                                    {
                                        MyMsgBox.Show("串口打开失败！请检查配置文件和硬件连接！", "提醒", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information, 5);
                                        return;
                                    }
                                }
                            }
                            #endregion

                            #region 4.0返修上线读取出生证信息
                            else//4.0返修上线读取出生证信息
                            {
                                CONTROL_ADDRESS pro_born_address = ControlAddressList.Where(s => s.POSITION_KEY == OpcItem.POSITION_KEY && s.ADDRESS_TYPE == ((int)AddressCategory.pro_born_code).ToString()).FirstOrDefault();//找到停止器对应的产品出生证地址(与返修下线不同地址位)
                                if (pro_born_address != null)
                                {
                                    string values = OPCHelper<JAC_REPAIR>.SynReadOpcItem(pro_born_address.ADDRESS_PATH);//读取地址的value,即产品出生证信息
                                    GetProductBornCode(values);
                                }
                            }
                            #endregion

                            #endregion
                        }
                        else if(OpcItem.ADDRESS_TYPE == writeRFID_success_sign.ToString())//返修上线流程中收到work_complete信号，写入online_success信号
                        {
                            CONTROL_ADDRESS online_success_address = ControlAddressList.Where(s => s.POSITION_KEY == OpcItem.POSITION_KEY && s.ADDRESS_TYPE == ((int)AddressCategory.online_success).ToString()).FirstOrDefault();//找到停止器对应的online_success地址
                            bool b = OPCHelper<JAC_REPAIR>.SynWriteOpcItem(online_success_address.ADDRESS_PATH, "1");
                            if (b)
                            {

                            }
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
                int repair_offline_sign = (int)AddressCategory.repair_offline;//返修下线跳变信号类型
                int repair_online_sign = (int)AddressCategory.repair_online;//返修上线跳变信号类型
                CONTROL_ADDRESS OpcItem_offline = ControlAddressList.Where(s => s.ADDRESS_TYPE == repair_offline_sign.ToString()).FirstOrDefault();
                CONTROL_ADDRESS OpcItem_online = ControlAddressList.Where(s => s.ADDRESS_TYPE == repair_offline_sign.ToString()).FirstOrDefault();
                if (OpcItem_offline != null || OpcItem_online != null)
                {
                    string values_offline = OPCHelper<JAC_REPAIR>.SynReadOpcItem(OpcItem_offline.ADDRESS_PATH);//读取地址的value
                    string values_online = OPCHelper<JAC_REPAIR>.SynReadOpcItem(OpcItem_offline.ADDRESS_PATH);//读取地址的value
                    //根据当前qc中的返修信号重新触发，相当于手动触发跳变信号
                    if (values_offline == "True")
                    {
                        GetBornCode(OpcItem_offline.ADDRESS_PATH);
                    }
                    if (values_online == "True")
                    {
                        GetBornCode(OpcItem_online.ADDRESS_PATH);
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

        #region 选取返修上线工位触发事件，加载返修上线及返修下线区间工位
        private void repair_online_station_cobox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                string select_repair_online_station = repair_online_station_cobox.SelectedValue.ToString();
                LoadProessStation(select_repair_online_station);
            }
            catch
            {

            }
        }

        void LoadProessStation(string select_repair_online_station)
        {
            List<STATION> Sectionstationlist = new List<HfutIE.Entity.STATION>();
            if (!string.IsNullOrEmpty(select_repair_online_station))
            {
                P_PRODUCT_REPAIR_INFO repairentity = P_PRODUCT_REPAIR_INFORepositoryFactory.Repository().FindList().Where(s => s.PRODUCT_BORN_CODE == plan_product_data.PRODUCT_BORN_CODE && s.REPAIR_STATE == "返修下线").FirstOrDefault();//获取返修信息表中对应返修件的信息
                STATION online_staentity = stationlist.Where(s => s.STATION_KEY == select_repair_online_station).FirstOrDefault();
                if (wc_code.Contains("2.7"))
                {
                    Sectionstationlist = stationlist.Where(s => s.STATION_CODE.Length >= 10 && s.STATION_CODE.Contains("27OP") && int.Parse(s.STATION_CODE.Substring(6, 4)) <= int.Parse(repairentity.REPAIR_OFFLINE_STATION_CODE.Substring(6, 4)) && !s.STATION_CODE.Contains("F")).OrderBy(s => s.STATION_CODE).OrderByDescending(s => s.STATION_CODE).ToList();
                    if (wc_code.Contains("A"))
                    {
                        Sectionstationlist = Sectionstationlist.Where(s => s.STATION_CODE.Contains("A")).ToList();
                    }
                    else if (wc_code.Contains("C"))
                    {
                        Sectionstationlist = Sectionstationlist.Where(s => s.STATION_CODE.Contains("C")).ToList();
                    }
                }
                else if (wc_code.Contains("4.0"))
                {
                    Sectionstationlist = stationlist.Where(s => s.STATION_CODE.Length >= 10 && s.STATION_CODE.Contains("40OP") && int.Parse(s.STATION_CODE.Substring(6, 4)) >= int.Parse(online_staentity.STATION_CODE.Substring(6, 4)) && int.Parse(s.STATION_CODE.Substring(6, 4)) <= int.Parse(repairentity.REPAIR_OFFLINE_STATION_CODE.Substring(6, 4)) && !s.STATION_CODE.Contains("F")).OrderBy(s => s.STATION_CODE).OrderByDescending(s => s.STATION_CODE).ToList();
                    if (wc_code.Contains("7195"))
                    {
                        Sectionstationlist = Sectionstationlist.Where(s => s.STATION_CODE.Contains("7195")).ToList();
                    }
                    else
                    {
                        Sectionstationlist = Sectionstationlist.Where(s => !s.STATION_CODE.Contains("7195")).ToList();
                    }
                }
                List<STATION> show_sectionsta = new List<STATION>();
                foreach(var item in Sectionstationlist)
                {
                    if(equiplist.Count(s=>s.STATION_KEY== item.STATION_KEY) != 0)
                    {
                        show_sectionsta.Add(item);
                    }
                }
                DataTable dt_section_sta = new DataTable();
                dt_section_sta.Columns.Add("oper_station_key");
                dt_section_sta.Columns.Add("oper_station_code");
                for (int i = 0; i < show_sectionsta.Count; i++)
                {
                    DataRow dr = dt_section_sta.NewRow();
                    dr[0] = show_sectionsta[i].STATION_KEY;
                    dr[1] = show_sectionsta[i].STATION_CODE;
                    dt_section_sta.Rows.Add(dr);
                }
            }
        }
        #endregion

        #region  故障类型选择或变换时触发事件
        private void fault_type_cobox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DataTable item_dt = new DataTable();
                List<PRODUCT_FAULT_ITEM> addlist = new List<PRODUCT_FAULT_ITEM>();
                if (fault_type_cobox.Text == "")
                {
                    addlist = faultlist;
                }
                else
                {
                    addlist = faultlist.FindAll(s => s.PRODUCT_FAULT_TYPE_KEY == fault_type_cobox.SelectedValue.ToString());
                }
                item_dt.Clear();
                item_dt.Columns.Add("fault_item_name");
                item_dt.Columns.Add("fault_item_key");
                for (int i = 0; i < addlist.Count; i++)
                {
                    DataRow dr = item_dt.NewRow();
                    dr[0] = addlist[i].PRODUCT_FAULT_ITEM_NAME;
                    dr[1] = addlist[i].PRODUCT_FAULT_ITEM_KEY;
                    item_dt.Rows.Add(dr);
                }
                SetComboBoxData(fault_item_cobox, item_dt);
            }
            catch
            {

            }
        }
        #endregion

        #region 排故类型选择或变换时触发事件
        private void maintain_type_cobox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DataTable item_dt = new DataTable();
                List<PRODUCT_MAINTAIN_ITEM> addlist = new List<PRODUCT_MAINTAIN_ITEM>();
                if (maintain_type_cobox.Text == "")
                {
                    addlist = maintainlist;
                }
                else
                {
                    addlist = maintainlist.FindAll(s => s.PRODUCT_MAINTAIN_TYPE_KEY == maintain_type_cobox.SelectedValue.ToString());
                }
                item_dt.Clear();
                item_dt.Columns.Add("maintain_item_name");
                item_dt.Columns.Add("maintain_item_key");
                for (int i = 0; i < addlist.Count; i++)
                {
                    DataRow dr = item_dt.NewRow();
                    dr[0] = addlist[i].PRODUCT_MAINTAIN_ITEM_NAME;
                    dr[1] = addlist[i].PRODUCT_MAINTAIN_ITEM_KEY;
                    item_dt.Rows.Add(dr);
                }
                SetComboBoxData(maintain_item_cobox, item_dt);
            }
            catch
            {

            }
        }
        #endregion

        #region 故障信息录入按钮
        private void fa_main_info_insert_btn_Click(object sender, EventArgs e)
        {
            try
            {
                string fault_type = fault_type_cobox.Text;//故障类型
                string fault_info = fault_item_cobox.Text;//故障信息
                string maintain_type = maintain_type_cobox.Text;//排故类型
                string maintain_info = maintain_item_cobox.Text;//排故信息
                if (string.IsNullOrEmpty(fault_info) || fault_info == "==请选择==")
                {
                    MyMsgBox.Show("请选择故障信息", "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error,3);
                    return;
                }
                //if ( string.IsNullOrEmpty(maintain_type))
                //{
                //    MessageBox.Show("排故类型不能为空!");
                //}
                if (string.IsNullOrEmpty(maintain_info) || maintain_info == "==请选择==")
                {
                    MyMsgBox.Show("请选择排故措施!", "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error,3);
                    return;
                }
                PRODUCT_FA_MAIN_INFOR fa_main_info = new PRODUCT_FA_MAIN_INFOR();
                fa_main_info = plan_product_data.EntityMapper<P_ASSEMBLE_PRODUCT_STATE, PRODUCT_FA_MAIN_INFOR>();
                fa_main_info.STATION_KEY = basicinfor.STATION_KEY;
                fa_main_info.STATION_CODE = basicinfor.STATION_CODE;
                fa_main_info.STATION_NAME = basicinfor.STATION_NAME;
                fa_main_info.PRODUCT_FAULT_ITEM_NAME = fault_info;
                fa_main_info.PRODUCT_FAULT_ITEM_KEY = fault_item_cobox.SelectedValue.ToString();
                PRODUCT_FAULT_ITEM faultitem = faultlist.Find(s => s.PRODUCT_FAULT_ITEM_KEY == fa_main_info.PRODUCT_FAULT_ITEM_KEY);
                fa_main_info.PRODUCT_FAULT_ITEM_CODE = faultitem.PRODUCT_FAULT_ITEM_CODE;
                fa_main_info.PRODUCT_FAULT_ITEM_NAME = faultitem.PRODUCT_FAULT_ITEM_NAME;
                fa_main_info.PRODUCT_MAINTAIN_ITEM_KEY = maintain_item_cobox.SelectedValue.ToString();
                PRODUCT_MAINTAIN_ITEM maintainitem = maintainlist.Find(s => s.PRODUCT_MAINTAIN_ITEM_KEY == fa_main_info.PRODUCT_MAINTAIN_ITEM_KEY);
                fa_main_info.PRODUCT_MAINTAIN_ITEM_CODE = maintainitem.PRODUCT_MAINTAIN_ITEM_CODE;
                fa_main_info.PRODUCT_MAINTAIN_ITEM_NAME = maintainitem.PRODUCT_MAINTAIN_ITEM_NAME;
                if (remark_ritchbox.Text.Trim() != "请录入故障详细信息(非必填项)")
                {
                    fa_main_info.REMARKS = remark_ritchbox.Text;
                }
                else
                {
                    fa_main_info.REMARKS = "";
                }
                fa_main_info.Create();
                PRODUCT_FA_MAIN_INFORepositoryFactory.Repository().Insert(fa_main_info);
                MyMsgBox.Show("故障信息录入成功!", "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information,4);
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败" + ex.ToString());
            }
        }
        #endregion

        #region 返修下线按钮
        private void rework_offline_btn_Click(object sender, EventArgs e)
        {
            RepairOffLine();
        }
        #endregion

        #region 返修上线按钮
        private void rework_online_btn_Click(object sender, EventArgs e)
        {
            Repository<P_KEY_PART_INFOR> tran = new Repository<P_KEY_PART_INFOR>();
            DbTransaction isOpenTrans = tran.BeginTrans();
            try
            {
                if (plan_product_data != null && plan_product_data.ASSEMBLE_PRODUCT_STATE_KEY != null)
                {
                    P_ASSEMBLE_PRODUCT_STATE assemble_state_entity = AssembleRepositoryFactory.Repository().FindEntity(plan_product_data.ASSEMBLE_PRODUCT_STATE_KEY);
                    if (assemble_state_entity.IS_REPAIR == "是")
                    {
                        P_PRODUCT_REPAIR_INFO repairentity = P_PRODUCT_REPAIR_INFORepositoryFactory.Repository().FindList().Where(s => s.PRODUCT_BORN_CODE == plan_product_data.PRODUCT_BORN_CODE && s.REPAIR_STATE == "返修下线").FirstOrDefault();//获取返修信息表中对应返修件的信息
                        if (repair_online_station_cobox.Text=="==请选择==")
                        {
                            MyMsgBox.Show("请选择返修上线工位!", "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information,3);
                            return;
                        }
                        STATION stationentity = stationlist.Where(s => s.STATION_KEY == repair_online_station_cobox.SelectedValue.ToString()).FirstOrDefault();//返修上线工位信息
                        repairentity.REPAIR_OFFLINE_STATION_KEY = repair_online_station_cobox.SelectedValue.ToString();
                        if (stationentity != null)
                        {
                            repairentity.REPAIR_ONLINE_STATION_CODE = stationentity.STATION_CODE;
                            repairentity.REPAIR_ONLINE_STATION_NAME = stationentity.STATION_NAME;
                        }
                        repairentity.REPAIR_ONLINE_TIME = ServerTime.Now;
                        repairentity.REPAIR_STATE = "返修上线";
                        repairentity.MODIFYDATE = ServerTime.Now;
                        repairentity.MODIFYUSERID = SystemLog.UserKey;
                        repairentity.MODIFYUSERNAME = SystemLog.UserName;
                        P_PRODUCT_REPAIR_INFORepositoryFactory.Repository().Update(repairentity,isOpenTrans);

                        assemble_state_entity.IS_OK = "2";//合格状态变更为合格
                        assemble_state_entity.IS_REPAIR = "否";//是否变更为返修下线
                        AssembleRepositoryFactory.Repository().Update(assemble_state_entity, isOpenTrans);
                        SetText(current_product_status_lbl, "返修上线");
                        MyMsgBox.Show("产品返修上线成功!", "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information,4);
                    }
                    else
                    {
                       MyMsgBox.Show("该产品未进行返修，无需返修上线!", "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information, 5);
                       return;
                    }
                    isOpenTrans.Commit();
                   
                }
                else
                {
                    if (iswritetag)
                    {
                        MyMsgBox.Show("未识别到工件到位信息!", "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error, 5);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString() == "某一OPC项不存在。")
                {
                    MyMsgBox.Show("PLC写入不成功。返修上线失败。", "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information,5);
                }
                else
                {
                    MyMsgBox.Show("返修上线失败。", "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information,5);
                }
                isOpenTrans.Rollback();
            }
        }
        #endregion

        #region 手动输入产品出生证确认按钮
        private void confirm_product_btn_Click(object sender, EventArgs e)
        {
            if (ServerCommunicationState == true)
            {
                string product_born_code = product_born_code_txt.Text;//product_born_code作为中间变量，用来保存手动输入的产品出生证
                if (string.IsNullOrWhiteSpace(product_born_code))
                {
                    MyMsgBox.Show("请输入产品出生证!", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information,3);
                    return;
                }
                GetProductBornCode(product_born_code);
            }
        }
        #endregion

        #region 选择上线工位及需要加工工位后确认，因存在离线返修及修改tag信息，固需与返修上线功能分离
        public static string Replace(string s, int sindex, string newstring)
        {
            string temp = "";

            temp = s.Substring(0, sindex) + newstring + s.Substring(sindex + newstring.Length);

            return temp;
        }

        private void confirm_online_station_btn_Click(object sender, EventArgs e)
        {
            Repository<P_KEY_PART_INFOR> tran = new Repository<P_KEY_PART_INFOR>();
            DbTransaction isOpenTrans = tran.BeginTrans();
            try
            {
                string select_repair_online_station = repair_online_station_cobox.Text.Trim().ToString();
                List<P_REPAIR_TAG_INFO> updatelist = new List<P_REPAIR_TAG_INFO>();//需要更新的tag缓存信息
                if (!string.IsNullOrEmpty(select_repair_online_station) && select_repair_online_station != "==请选择==")
                {
                    #region 将所有勾选需要加工工位写入返修下线时缓存的tag信息中
                    if (wc_code.Contains("2.7"))
                    {
                        P_REPAIR_TAG_INFO tag_info_entity = P_REPAIR_TAG_INFORepositoryFactory.Repository().FindEntity("PRODUCT_BORN_CODE", plan_product_data.PRODUCT_BORN_CODE);
                        if (tag_info_entity != null&& tag_info_entity.TAG_INFO_KEY!=null)
                        {
                            
                            P_REPAIR_TAG_INFORepositoryFactory.Repository().Update(tag_info_entity, isOpenTrans);
                        }
                    }
                    else
                    {
                        List<P_REPAIR_TAG_INFO> tag_info_list = P_REPAIR_TAG_INFORepositoryFactory.Repository().FindList($" and PRODUCT_BORN_CODE='{plan_product_data.PRODUCT_BORN_CODE}'").ToList();//获取该产品在返修下线时缓存的tag信息
                        if (tag_info_list.Count != 0)
                        {
                            P_REPAIR_TAG_INFO online_stationentity = tag_info_list.FirstOrDefault(s => s.OPC_ITEM_NAME.Contains("repair_online_station"));
                            online_stationentity.OPC_ITEM_VALUE = select_repair_online_station.Substring(5, select_repair_online_station.Length - 5);//修改返修上线工位西信息
                            updatelist.Add(online_stationentity);
                           
                            P_REPAIR_TAG_INFORepositoryFactory.Repository().Update(updatelist, isOpenTrans);
                        }
                    }
                    #endregion

                    #region 更新产品返修信息中返修上线工位信息
                    P_PRODUCT_REPAIR_INFO repairentity = P_PRODUCT_REPAIR_INFORepositoryFactory.Repository().FindList().Where(s => s.PRODUCT_BORN_CODE == plan_product_data.PRODUCT_BORN_CODE && s.REPAIR_STATE == "返修下线").FirstOrDefault();//获取返修信息表中对应返修件的信息
                    if (repairentity != null && repairentity.PRODUCT_REPAIR_INFO_KEY != null)
                    {
                        STATION stationentity = stationlist.FirstOrDefault(s => s.STATION_CODE == select_repair_online_station);
                        repairentity.REPAIR_ONLINE_STATION_KEY = stationentity.STATION_KEY;
                        repairentity.REPAIR_ONLINE_STATION_CODE = stationentity.STATION_CODE;
                        repairentity.REPAIR_ONLINE_STATION_NAME = stationentity.STATION_NAME;
                        P_PRODUCT_REPAIR_INFORepositoryFactory.Repository().Update(repairentity,isOpenTrans);
                    }
                    #endregion
                    
                    //SetLableText(repair_online_station_lbl, select_repair_online_station);//界面显示返修上线工位
                    isOpenTrans.Commit();
                }
                else
                {
                    MyMsgBox.Show("请选择返修上线工位", "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information);
                }
            }
            catch(Exception ex)
            {
                MyMsgBox.Show("选择返修上线工位失败。"+ex.ToString(), "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error);                
                isOpenTrans.Rollback();
            }
        }
        #endregion

        #region 返修下线处理逻辑
        void RepairOffLine()
        {
            Repository<P_KEY_PART_INFOR> tran = new Repository<P_KEY_PART_INFOR>();
            DbTransaction isOpenTrans = tran.BeginTrans();
            try
            {
                if (plan_product_data != null && plan_product_data.ASSEMBLE_PRODUCT_STATE_KEY != null)
                {
                    if(repair_offline_station_cobox.Text== "==请选择==")
                    {
                        MyMsgBox.Show("请选择返修下线工位！", "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information, 5);
                        return;
                    }
                    P_ASSEMBLE_PRODUCT_STATE assemble_state_entity = AssembleRepositoryFactory.Repository().FindEntity(plan_product_data.ASSEMBLE_PRODUCT_STATE_KEY);
                    if (assemble_state_entity.IS_REPAIR != "是")
                    {
                        //assemble_state_entity.OPERATED_STATE = "返修下线";//操作状态变更为返修下线
                        assemble_state_entity.IS_REPAIR = "是";//是否返修状态变更
                        assemble_state_entity.REPAIR_FREQUENCY++;//返修频次+1
                        AssembleRepositoryFactory.Repository().Update(assemble_state_entity, isOpenTrans);

                        P_PRODUCT_REPAIR_INFO repairentity = new P_PRODUCT_REPAIR_INFO();
                        repairentity = EntityHelper.EntityHelper.EntityMapper(plan_product_data, repairentity);//为实体增加产品信息
                        STATION stationentity = stationlist.Where(s => s.STATION_KEY == repair_offline_station_cobox.SelectedValue.ToString()).FirstOrDefault();//返修下线工位信息
                        repairentity.REPAIR_OFFLINE_STATION_KEY = repair_offline_station_cobox.SelectedValue.ToString();
                        if (stationentity != null)
                        {
                            repairentity.REPAIR_OFFLINE_STATION_CODE = stationentity.STATION_CODE;
                            repairentity.REPAIR_OFFLINE_STATION_NAME = stationentity.STATION_NAME;
                        }
                        repairentity.REPAIR_OFFLINE_TIME = ServerTime.Now;
                        repairentity.REPAIR_STATE = "返修下线";
                        repairentity.Create();
                        P_PRODUCT_REPAIR_INFORepositoryFactory.Repository().Insert(repairentity, isOpenTrans);
                        
                        isOpenTrans.Commit();
                        MyMsgBox.Show("返修下线成功!", "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information, 3);
                        //SetLableText(repair_offline_station_lbl, stationentity.STATION_CODE);//加载返修下线工位

                    }
                    else
                    {
                        MyMsgBox.Show("产品已处于返修状态!", "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error, 3);
                    }
                }
                else
                {
                    MyMsgBox.Show("请输入正确的在制品信息!", "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error, 3);
                }
            }
            catch (Exception ex)
            {
                if (ex.ToString() == "某一OPC项不存在。")
                {
                    MyMsgBox.Show("PLC写入不成功。返修下线失败。", "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error, 5);
                }
                isOpenTrans.Rollback();
            }
        }
        #endregion

        #region 详细信息录入框Enter事件
        private void remark_ritchbox_Enter(object sender, EventArgs e)
        {
            try
            {
                remark_ritchbox.ForeColor = Color.Black;
                if (remark_ritchbox.Text.Trim() == "请录入故障详细信息(非必填项)")
                    SetText(remark_ritchbox, "");
            }
            catch
            {

            }
        }
        #endregion

        #region 详细信息录入框Leave事件
        private void remark_ritchbox_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(remark_ritchbox.Text.Trim()))
                {
                    remark_ritchbox.ForeColor = Color.Gray;
                    SetText(remark_ritchbox, "请录入故障详细信息(非必填项)");
                }
            }
            catch
            {

            }
        }
        #endregion
    }
}

