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
using System.Drawing.Printing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing;
using ThoughtWorks.QRCode.Codec;

namespace HfutIe
{
    public partial class JAC_KEYPART_HCL : Form, IOPC
    {
        public JAC_KEYPART_HCL()
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
        RepositoryFactory<PART> PARTRepository = new RepositoryFactory<PART>();//零部件基本信息表
        RepositoryFactory<BOM> BOMRepository = new RepositoryFactory<BOM>();//BOM信息表
        RepositoryFactory<BOM_VERSION> BOM_VERSIONRepository = new RepositoryFactory<BOM_VERSION>();//BOM版本信息表
        RepositoryFactory<GD_DCS_WARRANTYPARTS_INFO> GD_DCS_WARRANTYPARTS_INFORepository = new RepositoryFactory<GD_DCS_WARRANTYPARTS_INFO>();//随机附件存储表
        RepositoryFactory<HCL_PRINT_RECORD> HCL_PRINT_RECORDRepository = new RepositoryFactory<HCL_PRINT_RECORD>();//后处理打印记录表
        static RepositoryFactory<P_HCL_SCAN_RECORD> P_HCL_SCAN_RECORDRepository = new RepositoryFactory<P_HCL_SCAN_RECORD>();//BOM信息表

        static RepositoryFactory<object> factory = new RepositoryFactory<object>();
        RepositoryFactory<Part_Supplier> PART_SUPPLIERRepository = new RepositoryFactory<Part_Supplier>();

        #endregion

        #region 内存公共变量
        List<CONTROL_ADDRESS> ControlAddressList;//该工位的停止器所配置的地址信息
        List<CHECK_INFO> CheckInfoLists;//主工位对应的设备点检信息

        string OpcName;
        string mainstationkey;//主工位主键
        static string strpath = "E:\\#1#纳威司达项目\\安全件1.png";
        static readonly object locker = new object();
        public string wc_code = ReadXML.GetHCLCode(System.Windows.Forms.Application.StartupPath + @"\WCConfig\WC.xml");//后处理岗位
        public string PrintName = ReadXML.GetPrintName(System.Windows.Forms.Application.StartupPath + @"\WCConfig\Print.xml");
        public int handleDatatime = Convert.ToInt32(ReadXML.GetTime(System.Windows.Forms.Application.StartupPath + @"\WCConfig\WC.xml"));//刷新时间
        string assembly_serial_no;//总成序列号
        List<STATION> StationList = new List<STATION>();//该PC对应的工作中心下所属工位的集合
        WorkCenterInfor wcinfor;
        string plan_code;//当前产品所属计划
        int collectedcount = 0;//判断已采集的安全件的数量
        int collectioncount = 10;
        //BYXJZ20201116
        string firstCode = "";
        string printAssembleCode = "";//生成二维码 序列号+物料号+供应商代码
        string printAssembleCode1 = "";//序列号+物料号
        string printAssembleCode2 = "";//
        string manualPrintAssembleCode = "";//生成二维码 序列号+物料号+供应商代码
        string manualAssembleCode1 = "";//序列号+物料号
        string manualprintAssembleCode2 = "";//
        string manualassembly_serial_no = "";
        string DATABASE_TYPE = "Oracle";
        string connectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.4.5.73)(PORT=1521))(CONNECT_DATA=(SERVER = DEDICATED)(SERVICE_NAME=JNDDMS)));User ID=JAC_NAVISTAR;Password=JAC_NAVISTAR;";
        //数据库连接语句

        //int finishcollected = 0;//finishcollected枚举值{0：未开始采集，1：一开始采集，未采集完成，2：采集完成}
        public BasicInfoDto basicinfor;//根据工位得出的基本信息
        public List<P_KEY_PART_C> key_part_c;//安全件采集配置
        public List<Part_Supplier> supplier;//安全件供应商信息
        //public DataTable team_shift;//班组班制信息
        public P_ASSEMBLE_PRODUCT_STATE plan_product_data;//通过产品出生证查到的该产品出生证代表的计划、产品信息
        List<P_KEY_PART_C> KeyPartData = new List<P_KEY_PART_C>();//根据产品出生证和工位号查询到本次需要采集的安全件信息，逐个多条(按数量分解成单个安全件，无上限限制，用预留字段1标识是否被加载到界面上过：0-未加载；1-已加载)
        List<PART> partlist = new List<PART>();//零部件集合
        List<PART> assemblypartlist = new List<PART>();//后处理总成零部件信息
        List<PART> current_annexs_list = new List<PART>();//当前代扫码附件集合
        List<BOM> bomlist = new List<BOM>();//BOM集合
        List<BOM> currentbomlist = new List<BOM>();//当前总成对应BOM集合
        List<BOM_VERSION> bomversionlist = new List<BOM_VERSION>();//BOM版本集合
        List<Part_Supplier> Part_SupplierList = new List<Part_Supplier>();//供應商信息
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
        Color NotCollectBtnColor = Color.FromArgb(5, 90, 150);//未采集安全件确认按钮颜色

        DOC_EQUIP_STATUS doc_equip_status = new DOC_EQUIP_STATUS();//加工记录表
        P_NOTOK_PRODUCT_INFOR p_notok_product_infor = new P_NOTOK_PRODUCT_INFOR();//不合格产品信息表
        string assemblytext = "==请选择==";
        DataTable dt_documents = new DataTable();
        DataTable dt_assemble_parts = new DataTable();//后处理总成信息
        Boolean isEXception = false;
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
                //SetViceStationLabel();
                Log.GetInstance.WriteLog("界面加载前1!");
                LoadAssemblyInfo();//获取后处理总成信息
                Log.GetInstance.WriteLog("界面加载前后1!");
                Task.Run(() => refresh());
                //showProductQueue();
                Log.GetInstance.WriteLog("打印记录获取后2!");
                Task.Run(() => RecordTimeShow());
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog(ex.Message);
            }
        }
        #region 初始化
        public void Initialization()
        {
            try
            {
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
            assemblytext = "==请选择==";
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
            #region 界面待显示控件集合(在线/离线)
            key_part_name_txts = new TextBox[] { key_part_name_txt, key_part_name2_txt, key_part_name3_txt, key_part_name4_txt, key_part_name5_txt, key_part_name6_txt, key_part_name7_txt, key_part_name8_txt, key_part_name9_txt, key_part_name10_txt };//安全件名称文本框
            key_part_code_txts = new TextBox[] { key_part_code_txt, key_part_code2_txt, key_part_code3_txt, key_part_code4_txt, key_part_code5_txt, key_part_code6_txt, key_part_code7_txt, key_part_code8_txt, key_part_code9_txt, key_part_code10_txt };//安全件编号文本框
            key_part_barcode_txts = new TextBox[] { key_part_barcode_txt, key_part_barcode2_txt, key_part_barcode3_txt, key_part_barcode4_txt, key_part_barcode5_txt, key_part_barcode6_txt, key_part_barcode7_txt, key_part_barcode8_txt, key_part_barcode9_txt, key_part_barcode10_txt };//安全件条码文本框
            confirm_part_btns = new Button[] { confirm_part_btn, confirm_part2_btn, confirm_part3_btn, confirm_part4_btn, confirm_part5_btn, confirm_part6_btn, confirm_part7_btn, confirm_part8_btn, confirm_part9_btn, confirm_part10_btn };//OK按钮文本框
            key_part_name_lbls = new Label[] { key_part_name_lbl, key_part_name2_lbl, key_part_name3_lbl, key_part_name4_lbl, key_part_name5_lbl, key_part_name6_lbl, key_part_name7_lbl, key_part_name8_lbl, key_part_name9_lbl, key_part_name10_lbl };//安全件名称label
            key_part_code_lbls = new Label[] { key_part_code_lbl, key_part_code2_lbl, key_part_code3_lbl, key_part_code4_lbl, key_part_code5_lbl, key_part_code6_lbl, key_part_code7_lbl, key_part_code8_lbl, key_part_code9_lbl, key_part_code10_lbl };//安全件编号label
            key_part_barcode_lbls = new Label[] { key_part_barcode_lbl, key_part_barcode2_lbl, key_part_barcode3_lbl, key_part_barcode4_lbl, key_part_barcode5_lbl, key_part_barcode6_lbl, key_part_barcode7_lbl, key_part_barcode8_lbl, key_part_barcode9_lbl, key_part_barcode10_lbl };//安全件条码label
            key_part_rectangleShapes = new RectangleShape[] { key_part_rectangleShape1, key_part_rectangleShape2, key_part_rectangleShape3, key_part_rectangleShape4 }; //形状label
            #endregion

            #region 打开扫描枪连接(在线/离线)
            try
            {
                scanport.PortName = ConfigurationManager.AppSettings["COM"].ToString().Trim(); ;//为串口赋名 
                scanport.BaudRate = 9600;//无线扫描枪
                scanport.NewLine = "\r\n";
                scanport.RtsEnable = true;
                scanport.DtrEnable = true;
                scanport.Open();
                Log.GetInstance.WriteLog("打开扫描枪成功!");
            }
            catch
            {
                Log.GetInstance.WriteLog("打开" + ConfigurationManager.AppSettings["COM"].ToString().Trim() + "失败");
            }
            #endregion
        }
        #endregion
        private void refresh() {
            while (true) {
                showProductQueue();
                Thread.Sleep(30000);
            }
        }

        /// <summary>
        /// 获取并加载后处理集合信息
        /// </summary>
        private void LoadAssemblyInfo()
        {
            try
            {
                Part_SupplierList = PART_SUPPLIERRepository.Repository().FindList().ToList();
                Log.GetInstance.WriteLog("1!");
                partlist = PARTRepository.Repository().FindList();//获取所有物料信息
                //partlist = PARTRepository.Repository().FindListBySql("select * from part");//获取所有物料信息
                Log.GetInstance.WriteLog("11!");
                //P_KEY_PART_INFORRepositoryFactory.Repository().FindList();
                bomversionlist = BOM_VERSIONRepository.Repository().FindList(" and REMARKS like '%后处理%'");//获取所有后处理总成BOM版本信息
                bomlist = BOMRepository.Repository().FindListBySql("select * from BOM b left join BOM_VERSION bv on b.VERSION_KEY=bv.VERSION_KEY where bv.REMARKS like '%后处理%'");//获取所有后处理BOM信息
                assemblypartlist = partlist.Where(s => bomversionlist.Select(t => t.PART_KEY).Contains(s.PART_KEY)).OrderBy(s => s.PART_CODE).ToList();//后处理总成对应物料信息
                Getdic();//故障类型和排故类型DataTable创建
                SetComDataSource();//DataTable数据源赋值
                Thread t1;
                t1 = new Thread(handle);
                t1.Start();
            }
            catch
            {

            }
        }

        #region 创建DataTable总成号
        private void Getdic()
        {
            try
            {
                #region 后处理总成信息
                dt_assemble_parts.Clear();
                dt_assemble_parts.Columns.Add("part_code");
                dt_assemble_parts.Columns.Add("part_key");
                DataRow dr1 = dt_assemble_parts.NewRow();
                dr1[0] = "==请选择==";
                dt_assemble_parts.Rows.Add(dr1);
                for (int i = 0; i < assemblypartlist.Count; i++)
                {
                    DataRow dr = dt_assemble_parts.NewRow();
                    dr[0] = assemblypartlist[i].PART_CODE;
                    dr[1] = assemblypartlist[i].PART_KEY;
                    dt_assemble_parts.Rows.Add(dr);
                }
                assembly_cobox.DisplayMember = "part_code";
                assembly_cobox.ValueMember = "part_key";
                #endregion
            }
            catch (Exception ex)
            {
                Log.Instance.WriteLog("Getdic  异常" + ex.Message);
            }
        }
        #endregion

        #region CoBox数据源赋值
        private void SetComDataSource()
        {
            SetComboBoxData(assembly_cobox, dt_assemble_parts);
        }
        #endregion

        #region 总成号选定触发事件
        private void assembly_cobox_SelectedValueChanged(object sender, EventArgs e)
        {
            InitializationForm(true);
            if (assembly_cobox.Text.ToString() != "==请选择==")
            {
                assemblytext = assembly_cobox.Text.ToString();
                currentbomlist = bomlist.Where(s => s.Parent_Part_Key == assembly_cobox.SelectedValue.ToString()).OrderBy(t => t.RESERVE01).ToList();//当前选中总成对应BOM信息
                count = currentbomlist.Count;
                sequence = 1;
                current_annexs_list = partlist.Where(s => currentbomlist.Select(t => t.Part_Key).Contains(s.PART_KEY)).ToList();//对应总成下的随机附件物料详细信息
                current_annexs_list = partlist.Where(s => currentbomlist.Select(t => t.Part_Key).Contains(s.PART_KEY)).OrderBy(y => y.PART_CODE).ToList();//对应总成下的随机附件物料详细信息
                string[] orderArr1 = new string[] { "否", "是" };
                current_annexs_list = current_annexs_list.OrderBy(t =>
                {
                    var i = 0; i = Array.IndexOf(orderArr1, t.RESERVE07); if (i != -1) { return i; }
                    else
                    {
                        return int.MaxValue;
                    }
                }).ThenBy(s => s.PART_CODE).ToList();//先按是否解析，再按顺序排序
                EnclosurePartShow();
            }
        }
        #region 界面显示需要采集的安全件个数和信息

        public void EnclosurePartShow()
        {
            try
            {
                if (current_annexs_list != null)
                {
                    int i = 0;
                    for (int j = 0; j < currentbomlist.Count; j++)//根据需要采集的安全件的种类数（i）,显示出相应的安全件信息采集控件
                    {
                        PART partEntity = current_annexs_list.Find(t => t.PART_KEY == currentbomlist[j].Part_Key);
                        if (partEntity != null && partEntity.PART_KEY != null)
                        {
                            if (i < 10)//最多10个
                            {
                                SetVisible(key_part_name_txts[i], true);//显示安全件名称text控件
                                SetText(key_part_name_txts[i], partEntity.PART_NAME);//安全件名称text控件赋值

                                SetVisible(key_part_code_txts[i], true);//显示安全件编号text控件
                                SetText(key_part_code_txts[i], partEntity.PART_CODE);//安全件编号text控件赋值

                                SetVisible(key_part_barcode_txts[i], true);//显示安全件条码text控件

                                SetVisible(confirm_part_btns[i], true);//显示OK  button控件

                                SetVisible(key_part_name_lbls[i], true);//显示安全件名称label控件

                                SetVisible(key_part_code_lbls[i], true);//显示安全件编号label控件

                                SetVisible(key_part_barcode_lbls[i], true);//显示安全件条码label控件
                                i++;
                            }
                        }
                    }
                    collectioncount = i;
                }
                else
                {
                    MyMsgBox.Show("当前选中总成未配置BOM信息。", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information, 10);
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
        #endregion

        #region 界面关闭
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
        public void Formclosing()//为了防止出现方法同名，此处用Formclosing命名，原定用FormClosing命名
        {
            try
            {
                ScannerHelper.CloseCom(scanport);
            }
            finally
            {
                this.Dispose();
                this.Close();
                Environment.Exit(0);
            }
        }
        #endregion

        #endregion

        #region 扫描枪获取并插入数据表

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
                        if (!isEXception)
                        {//是否发生异常
                            if (assemblytext != "==请选择==" && assemblytext != "")
                            {
                                ScanportDataReceive(scanport);
                            }

                        }
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

        int sequence = 1;
        int count = 0;
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
            CheckForIllegalCrossThreadCalls = false;
            barcode = obj.ToString().Trim();
            if (!string.IsNullOrEmpty(barcode))//只有在显示为扫描输入模式下（Text == "手动输入"），扫描安全件条码才有效
            {
                string assembleCode = assembly_cobox.Text.ToString();

                P_HCL_SCAN_RECORD entity = new P_HCL_SCAN_RECORD();
                entity.Create();
                entity.CREATEUSERID = "01";
                entity.CREATEUSERNAME = "测试";
                entity.PART_VINCODE = barcode;
                entity.ASSEMBLE_CODE = assembleCode;
                entity.RESERVE1 = assembly_cobox.SelectedValue.ToString();
                entity.SEQUENCE = sequence;
                entity.PART_KEY = currentbomlist[sequence - 1].Part_Key;
                entity.RESERVE3 = wc_code;
                if (sequence == count)
                {
                    sequence = 1;
                }
                else
                {
                    sequence++;
                }
                int isok = P_HCL_SCAN_RECORDRepository.Repository().Insert(entity);
                if (isok > 0)
                {
                    Log.GetInstance.WriteLog("扫描枪数据插入成功！" + barcode + "/" + ew1.ToString());
                }
                else
                {
                    Log.GetInstance.WriteLog("扫描枪数据插入失败！" + barcode + "/" + ew1.ToString());
                }
            }
            sw_all.Stop();
            long ew_all = sw_all.ElapsedMilliseconds;
            Log.GetInstance.WriteLog(barcode + "扫码处理总时间：" + ew_all.ToString());
        }
        public void allDataNtoC()
        {
            List<P_HCL_SCAN_RECORD> RecordList = P_HCL_SCAN_RECORDRepository.Repository().FindList("FLAG", "N").OrderBy(t => t.CREATEDATE).ToList();
            foreach (P_HCL_SCAN_RECORD item in RecordList)
            {
                item.FLAG = "C";
                item.HANDLEDATE = DateTime.Now;
                item.RESERVE2 = "异常后的无效数据";
                P_HCL_SCAN_RECORDRepository.Repository().Update(item);
            }
        }
        #endregion
        #region 提交安全件采集数据

        public void handle()
        {
            while (true)
            {
                handleDatas();
                Thread.Sleep(1000 * handleDatatime);
            }
        }
        int hasColleced = 0, needCollected = 0; //已采集 、需采集
        string lastAssemble = "";  //上一个总成号
        bool isAllNtoC = false;
        List<PART> handle_current_part_list = new List<PART>();//当前正在处理part信息
        List<BOM> handle_current_bom_list = new List<BOM>();//当前正在处理扫码bom信息
        public void handleDatas()
        {
            if (ServerCommunicationState == true)//在线状态提交安全件信息至数据库
            {
                //List<P_HCL_SCAN_RECORD> RecordList = P_HCL_SCAN_RECORDRepository.Repository().FindList("FLAG", "N").OrderBy(t => t.CREATEDATE).ToList();
                List<P_HCL_SCAN_RECORD> RecordList = P_HCL_SCAN_RECORDRepository.Repository().FindList(" and FLAG='N' and RESERVE3='" + wc_code + "'").OrderBy(t => t.CREATEDATE).ToList();

                foreach (P_HCL_SCAN_RECORD item in RecordList)
                {
                    string barcode = item.PART_VINCODE;
                    if (assemblytext != item.ASSEMBLE_CODE)
                    {
                        if (item.SEQUENCE == 1)
                        {
                            if (lastAssemble != item.ASSEMBLE_CODE)
                            {
                                lastAssemble = item.ASSEMBLE_CODE;
                                hasColleced = 0;
                                needCollected = handle_current_bom_list.Count;
                                handle_current_bom_list = bomlist.Where(s => s.Parent_Part_Key == item.RESERVE1).OrderBy(t => t.RESERVE01).ToList();//当前处理的BOM信息
                                handle_current_part_list = partlist.Where(s => handle_current_bom_list.Select(t => t.Part_Key).Contains(s.PART_KEY)).ToList();//当前BOM的PART信息
                            }
                        }
                        if (needCollected == 0)
                        {
                            handle_current_bom_list = bomlist.Where(s => s.Parent_Part_Key == item.RESERVE1).OrderBy(t => t.RESERVE01).ToList();//当前处理的BOM信息
                            handle_current_part_list = partlist.Where(s => handle_current_bom_list.Select(t => t.Part_Key).Contains(s.PART_KEY)).ToList();//当前BOM的PART信息
                            needCollected = handle_current_bom_list.Count;
                        }
                        PART part_entity = handle_current_part_list.FirstOrDefault(s => s.PART_KEY == item.PART_KEY);
                        Repository<GD_DCS_WARRANTYPARTS_INFO> tran = new Repository<GD_DCS_WARRANTYPARTS_INFO>();
                        GD_DCS_WARRANTYPARTS_INFO annex_infor = new GD_DCS_WARRANTYPARTS_INFO();//当前采集的安全件信息    
                        if (item.SEQUENCE == 1 || hasColleced == 0)//每次绑定第一个随机附件前检查序列号是否正确(是否为当天序列号)
                        {
                            firstCode = barcode; //绑定第一个条码打印
                            Base_DataDictionary UnitNo_Info = Base_DataRepository.Repository().FindEntity("Code", "HCL_NO");
                            Base_DataDictionaryDetail UnitNo_Info_De = Base_DataDetailRepository.Repository().FindList().Where(s => s.DataDictionaryId == UnitNo_Info.DataDictionaryId && s.FullName == "HCL_SERIAL_NO").FirstOrDefault();
                            string serial_code = UnitNo_Info_De.Code;
                            string year = ServerTime.Now.Year.ToString();
                            string month = ServerTime.Now.Month.ToString();
                            switch (month)
                            {
                                //此处待对接三个两位数月份对应表达方式
                                case "10":
                                    month = "A";
                                    break;
                                case "11":
                                    month = "B";
                                    break;
                                case "12":
                                    month = "C";
                                    break;
                                default:
                                    break;
                            }
                            string day = ServerTime.Now.Day.ToString().PadLeft(2, '0');
                            if (serial_code.Substring(0, 5) == year.Substring(2, 2) + month + day)//若序列号为当天，则直接赋值
                            {
                                assembly_serial_no = serial_code;
                            }
                            else//若序列号不为当天，则更新为当天序列号的001
                            {
                                assembly_serial_no = year.Substring(2, 2) + month + day + "001";
                                UnitNo_Info_De.Code = year.Substring(2, 2) + month + day + "001";
                                Base_DataDetailRepository.Repository().Update(UnitNo_Info_De);//更新数据字典UnitNo信息
                            }
                            SetText(product_born_code_txt, assembly_serial_no);//总成序列号
                        }
                        string supplierCode = null, supplierName = null;
                        string[] strArr = barcode.Split('^');
                        if (strArr.Length > 2)
                        {
                            supplierCode = barcode.Split('^')[1];
                        }
                        else
                        {
                            if (barcode.Length > 14)
                            {
                                supplierCode = barcode.Substring(8, 6);
                            }
                        }
                        Part_Supplier supplierEntity = Part_SupplierList.FindLast(t => t.supplier_code == supplierCode);
                        if (supplierEntity == null || supplierEntity.supplier_key == null)
                        {
                        }
                        else
                        {
                            supplierName = supplierEntity.supplier_name;
                        }
                        annex_infor.GD_PART_CODE = part_entity.PART_CODE;
                        annex_infor.GD_PART_NAME = part_entity.PART_NAME;
                        annex_infor.GD_PART_VINCODE = barcode;
                        annex_infor.GD_ASSAMBLE_CODE = assembly_serial_no + "001288" + item.ASSEMBLE_CODE;
                        annex_infor.SUPPLIER_CODE = supplierCode;
                        annex_infor.SUPPLIER_NAME = supplierName;
                        annex_infor.Create();
                        List<GD_DCS_WARRANTYPARTS_INFO> hasentity = GD_DCS_WARRANTYPARTS_INFORepository.Repository().FindList($" and GD_ASSAMBLE_CODE='{annex_infor.GD_ASSAMBLE_CODE}' and GD_PART_CODE='{annex_infor.GD_PART_CODE}' and GD_PART_VINCODE='{annex_infor.GD_PART_VINCODE}'");
                        int result = 0;
                        if (hasentity.Count > 0)
                        {
                            result = 0;
                        }
                        else
                        {
                            result = GD_DCS_WARRANTYPARTS_INFORepository.Repository().Insert(annex_infor);
                            //安全件接口至DCS系统

                            StringBuilder str = new StringBuilder();
                            str.Append($@"insert all ");//同时插入多条数据，避免一条一条插入

                            str.Append($@"into GD_DCS_WARRANTYPARTS_INFO (GD_GUID,GD_PART_CODE,GD_PART_NAME,GD_PART_VINCODE,GD_ASSAMBLE_CODE,GD_CFLG,GD_CREATEDATE,GD_WDAT,SUPPLIER_CODE,SUPPLIER_NAME) values('{annex_infor.GD_GUID}','{annex_infor.GD_PART_CODE}','{annex_infor.GD_PART_NAME}','{annex_infor.GD_PART_VINCODE}','{annex_infor.GD_ASSAMBLE_CODE}','{annex_infor.GD_CFLG}',to_date('{(annex_infor.GD_CREATEDATE).Value.ToString("yyyy-MM-dd HH:mm:ss")}', 'YYYY-MM-DD HH24:MI:SS'),to_date('{(annex_infor.GD_CREATEDATE).Value.ToString("yyyy-MM-dd HH:mm:ss")}', 'YYYY-MM-DD HH24:MI:SS'),'{annex_infor.SUPPLIER_CODE}','{annex_infor.SUPPLIER_NAME}' ) ");//拼接insert语句，同时插入多条信息
                            str.Append(" select 1 from dual");

                            int GDisOk = factory.Repository(DATABASE_TYPE, connectionString).ExecuteBySql(str);
                            //int GDisOk = 1;
                            if (GDisOk > 0)
                            {
                                Log.GetInstance.WriteLog("后处理接口插入成功！");//观察20201116XJZ
                            }
                        }
                        if (result == 0) //提交安全件绑定信息失败
                        {
                            isEXception = true;
                            isAllNtoC = true;
                            DialogResult = MyMsgBox.Show("条形码有误，请确认后重新扫描！", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error);
                            switch (DialogResult)
                            {
                                case DialogResult.None:
                                    isEXception = false;
                                    break;
                                case DialogResult.OK:
                                    isEXception = false;
                                    break;
                            }
                            item.FLAG = "C";
                            item.HANDLEDATE = DateTime.Now;
                            int updateok = P_HCL_SCAN_RECORDRepository.Repository().Update(item);
                            if (updateok == 0)
                            {
                                Log.GetInstance.WriteLog("RECORD更新失败/" + item.PART_VINCODE);
                            }
                            else
                            {
                                Log.GetInstance.WriteLog("RECORD更新成功/" + item.PART_VINCODE);
                            }
                        }
                        else
                        {
                            Log.GetInstance.WriteLog("提交安全件信息成功！" + annex_infor.GD_PART_CODE + "/" + annex_infor.GD_PART_NAME + "/" + annex_infor.GD_PART_VINCODE + "/" + annex_infor.GD_ASSAMBLE_CODE);//观察                              
                            if (item.SEQUENCE == needCollected)//为解决扫码过快数据库未提交界面按钮未变色导致数量有出入问题
                            {
                                #region 待采集随机附件全部完成，开始打印随机附件标签，并更新总成序列号信息
                                #region 插入打印记录
                                HCL_PRINT_RECORD hcl_record_entity = new HCL_PRINT_RECORD();
                                PART ASSEMBLE_part_entity = partlist.Find(s => s.PART_CODE == item.ASSEMBLE_CODE);//获得该条码对应的零部件信息
                                hcl_record_entity.GD_PART_CODE = ASSEMBLE_part_entity.PART_CODE;
                                hcl_record_entity.GD_PART_NAME = ASSEMBLE_part_entity.PART_NAME;
                                hcl_record_entity.GD_ASSAMBLE_CODE = assembly_serial_no + "001288" + ASSEMBLE_part_entity.PART_CODE;
                                hcl_record_entity.SERIAL_NO = assembly_serial_no;
                                hcl_record_entity.Create();
                                HCL_PRINT_RECORDRepository.Repository().Insert(hcl_record_entity);
                                printAssembleCode = assembly_serial_no + "001288" + part_entity.PART_CODE;
                                printAssembleCode1 = assembly_serial_no + "001288";
                                printAssembleCode2 = part_entity.PART_CODE;
                                Log.GetInstance.WriteLog("打印记录插入成功，" + hcl_record_entity.GD_ASSAMBLE_CODE);//观察
                                #endregion

                                #region 更新序列号
                                Base_DataDictionary UnitNo_Info = Base_DataRepository.Repository().FindEntity("Code", "HCL_NO");
                                Base_DataDictionaryDetail UnitNo_Info_De = Base_DataDetailRepository.Repository().FindList().Where(s => s.DataDictionaryId == UnitNo_Info.DataDictionaryId && s.FullName == "HCL_SERIAL_NO").FirstOrDefault();
                                UnitNo_Info_De.Code = UnitNo_Info_De.Code.Substring(0, 5) + (int.Parse(UnitNo_Info_De.Code.Substring(5, 3)) + 1).ToString().PadLeft(3, '0');
                                Base_DataDetailRepository.Repository().Update(UnitNo_Info_De);//更新数据字典HCL_NO信息
                                Log.GetInstance.WriteLog("更新序列号成功，" + UnitNo_Info_De.Code);//观察
                                #endregion

                                #region 打印标签
                                Myprinter(false);

                                #endregion
                                #endregion
                                hasColleced = -1;
                            }
                            item.FLAG = "C";
                            item.HANDLEDATE = DateTime.Now;
                            int updateok = P_HCL_SCAN_RECORDRepository.Repository().Update(item);
                            if (updateok == 0)
                            {
                                Log.GetInstance.WriteLog("RECORD更新失败/" + item.PART_VINCODE);
                            }
                            else
                            {
                                hasColleced++;
                                Log.GetInstance.WriteLog("RECORD更新成功/" + item.PART_VINCODE);
                            }
                        }
                        if (isAllNtoC)
                        {
                            allDataNtoC();
                            isAllNtoC = false;
                            break;
                        }
                    }
                    else
                    {
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
                        PART current_p_keypart_c = current_annexs_list.FirstOrDefault(s => s.PART_CODE == key_part_code_txts[m].Text.Trim());
                        int i = 0;
                        int a = 0;
                        //if (current_p_keypart_c.RESERVE07 == "否")//不需要校解析规则的图号，需要校验是否符合长度限制要求，符合长度要求则通过
                        //{
                        string limit = current_p_keypart_c.RESERVE10;
                        if (limit==null||limit.Split('-').Length != 2 || int.TryParse(limit.Split('-')[0], out a) == false || int.TryParse(limit.Split('-')[1], out a) == false)//若未维护长度限制或值不符合(a-a)格式，均不校验长度，直接绑定
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
                                i = 10;
                            }
                        }
                        //}
                        //else//需要校验规则的进入原先逻辑
                        //{
                        //    i = CheckBarcodeNum(barcode);
                        //}
                        Log.GetInstance.WriteLog("解析：" + current_p_keypart_c.RESERVE07 + "/" + i);//观察

                        sw2.Stop();
                        long ew2 = sw2.ElapsedMilliseconds;
                        Log.GetInstance.WriteLog("匹配待采集信息时间/" + i.ToString() + "/" + ew2.ToString());

                        if (i > 10)//如果i>4，即条形码与需要采集的安全件不匹配
                        {
                            for (int j = 0; j < key_part_barcode_txts.Length; j++)
                            {
                                //在扫描枪输入的情况下：所有确定按钮均不可交互
                                if (confirm_part_btns[j].Visible == true && confirm_part_btns[j].BackColor != HasCollectBtnColor)
                                {
                                    SetText(key_part_barcode_txts[j], "随机附件扫描错误！请重试。");
                                    isEXception = true;
                                    isAllNtoC = true;
                                    DialogResult = MyMsgBox.Show("条形码有误，请确认后重新扫描！", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error);
                                    switch (DialogResult)
                                    {
                                        case DialogResult.None:
                                            isEXception = false;
                                            break;
                                        case DialogResult.OK:
                                            isEXception = false;
                                            break;
                                    }
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

                                        break;
                                    }
                                }
                            }
                        }
                        else//找到匹配的安全件，可以开始绑定安全件信息
                        {
                            SetText(key_part_barcode_txts[i], barcode);

                            Stopwatch sw3 = new Stopwatch();
                            sw3.Start();

                            int Is_Ok = BindKeyPartInfor(barcode, i);  //进行安全件绑定
                            if (Is_Ok != 1)
                            {
                                SetText(key_part_barcode_txts[i], "随即附件绑定失败！请重试。");
                                isEXception = true;
                                isAllNtoC = true;
                                DialogResult = MyMsgBox.Show("条形码有误，请确认后重新扫描！", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error);
                                switch (DialogResult)
                                {
                                    case DialogResult.None:
                                        isEXception = false;
                                        break;
                                    case DialogResult.OK:
                                        isEXception = false;
                                        break;
                                }
                            }

                            item.FLAG = "C";
                            item.HANDLEDATE = DateTime.Now;
                            int updateok = P_HCL_SCAN_RECORDRepository.Repository().Update(item);
                            if (updateok == 0)
                            {
                                Log.GetInstance.WriteLog("RECORD更新失败/" + item.PART_VINCODE);
                            }
                            else
                            {
                                Log.GetInstance.WriteLog("RECORD更新成功/" + item.PART_VINCODE);
                            }

                            sw3.Stop();
                            long ew3 = sw3.ElapsedMilliseconds;
                            Log.GetInstance.WriteLog("提交采集信息时间/" + i.ToString() + "/" + ew3.ToString());

                        }
                        if (isAllNtoC)
                        {
                            allDataNtoC();
                            isAllNtoC = false;
                            break;
                        }
                    }
                }

            }

        }
        #region 获取即将绑定的安全件信息——开始绑定安全件信息
        /// <summary>
        /// 获取绑定安全件信息
        /// </summary>
        /// <param name="keypartbarcode">安全件条形码</param>
        /// <returns></returns>
        public int BindKeyPartInfor(string keypartbarcode, int i)
        {
            //i用来判断keypartbarcode是第几个条形码，即是由第几个OK调用此方法的
            Repository<GD_DCS_WARRANTYPARTS_INFO> tran = new Repository<GD_DCS_WARRANTYPARTS_INFO>();
            try
            {
                int IsOk = 0;
                GD_DCS_WARRANTYPARTS_INFO annex_infor = new GD_DCS_WARRANTYPARTS_INFO();//当前采集的安全件信息                                                           //key_part_barcode_txts[i].Enabled = false;
                annex_infor = Accept(annex_infor, keypartbarcode, i);
                if (ServerCommunicationState == true)//在线状态提交安全件信息至数据库
                {
                    List<GD_DCS_WARRANTYPARTS_INFO> hasentity = GD_DCS_WARRANTYPARTS_INFORepository.Repository().FindList($" and GD_ASSAMBLE_CODE='{annex_infor.GD_ASSAMBLE_CODE}' and GD_PART_CODE='{annex_infor.GD_PART_CODE}' and GD_PART_VINCODE='{annex_infor.GD_PART_VINCODE}'");
                    int result = 0;
                    if (hasentity.Count > 0)
                    {
                        result = 0;
                    }
                    else
                    {
                        result = GD_DCS_WARRANTYPARTS_INFORepository.Repository().Insert(annex_infor);
                        //安全件接口至DCS系统

                        StringBuilder str = new StringBuilder();
                        str.Append($@"insert all ");//同时插入多条数据，避免一条一条插入

                        str.Append($@"into GD_DCS_WARRANTYPARTS_INFO (GD_GUID,GD_PART_CODE,GD_PART_NAME,GD_PART_VINCODE,GD_ASSAMBLE_CODE,GD_CFLG,GD_CREATEDATE,GD_WDAT,SUPPLIER_CODE,SUPPLIER_NAME) values('{annex_infor.GD_GUID}','{annex_infor.GD_PART_CODE}','{annex_infor.GD_PART_NAME}','{annex_infor.GD_PART_VINCODE}','{annex_infor.GD_ASSAMBLE_CODE}','{annex_infor.GD_CFLG}',to_date('{(annex_infor.GD_CREATEDATE).Value.ToString("yyyy-MM-dd HH:mm:ss")}', 'YYYY-MM-DD HH24:MI:SS'),to_date('{(annex_infor.GD_CREATEDATE).Value.ToString("yyyy-MM-dd HH:mm:ss")}', 'YYYY-MM-DD HH24:MI:SS'),'{annex_infor.SUPPLIER_CODE}','{annex_infor.SUPPLIER_NAME}' ) ");//拼接insert语句，同时插入多条信息
                        str.Append(" select 1 from dual");

                        int GDisOk = factory.Repository(DATABASE_TYPE, connectionString).ExecuteBySql(str);
                        //int GDisOk = 1;

                        if (GDisOk > 0)
                        {
                            Log.GetInstance.WriteLog("后处理接口插入成功！");//观察20201116XJZ
                        }
                    }
                    if (result == 0) //提交安全件绑定信息失败
                    {
                        //SetText(key_part_barcode_txts[i], "提交随机附件失败！请重试。");
                        //isEXception = true;
                        //DialogResult = MyMsgBox.Show("条形码有误，请确认后重新扫描！", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error);
                        //switch (DialogResult)
                        //{
                        //    case DialogResult.None:
                        //        isEXception = false;
                        //        break;
                        //    case DialogResult.OK:
                        //        isEXception = false;
                        //        break;
                        //}
                        IsOk = 0;
                    }
                    else
                    {
                        Log.GetInstance.WriteLog("提交安全件信息成功！" + annex_infor.GD_PART_CODE + "/" + annex_infor.GD_PART_NAME + "/" + annex_infor.GD_PART_VINCODE + "/" + annex_infor.GD_ASSAMBLE_CODE);//观察
                        SetText(key_part_barcode_txts[i], keypartbarcode);
                        IsOk = 1;
                    }
                }
                if (IsOk == 1)//条码成功绑定，此条码之后不可操作
                {
                    if (confirm_part_btns[i].Visible == true)
                    {
                        if (collectedcount < collectioncount)//为解决扫码过快数据库未提交界面按钮未变色导致数量有出入问题
                        {
                            collectedcount += 1;//条码采集成功，采集数量加1
                        }
                    }
                    SetColor(confirm_part_btns[i], HasCollectBtnColor);//操作OK button控件,改变背景色
                    AfterBindPartBarCode();//绑定安全件数据之后如果全部采集完成 进行打印
                }
                return IsOk;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                SetText(key_part_barcode_txts[i], "绑定安全件失败！");
                isEXception = true;
                DialogResult = MyMsgBox.Show("条形码有误，请确认后重新扫描！", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error);
                switch (DialogResult)
                {
                    case DialogResult.None:
                        isEXception = false;
                        break;
                    case DialogResult.OK:
                        isEXception = false;
                        break;
                }
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("绑定安全件失败！" + ex.Message);
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
        private GD_DCS_WARRANTYPARTS_INFO Accept(GD_DCS_WARRANTYPARTS_INFO annex_infor, string key_part_barcode, int i)
        {
            try
            {
                if (collectedcount == 0)//每次绑定第一个随机附件前检查序列号是否正确(是否为当天序列号)
                {
                    firstCode = key_part_barcode; //绑定第一个条码打印
                    Base_DataDictionary UnitNo_Info = Base_DataRepository.Repository().FindEntity("Code", "HCL_NO");
                    Base_DataDictionaryDetail UnitNo_Info_De = Base_DataDetailRepository.Repository().FindList().Where(s => s.DataDictionaryId == UnitNo_Info.DataDictionaryId && s.FullName == "HCL_SERIAL_NO").FirstOrDefault();
                    string serial_code = UnitNo_Info_De.Code;
                    string year = ServerTime.Now.Year.ToString();
                    string month = ServerTime.Now.Month.ToString();
                    switch (month)
                    {
                        //此处待对接三个两位数月份对应表达方式
                        case "10":
                            month = "A";
                            break;
                        case "11":
                            month = "B";
                            break;
                        case "12":
                            month = "C";
                            break;
                        default:
                            break;
                    }
                    string day = ServerTime.Now.Day.ToString();
                    if (serial_code.Substring(0, 5) == year.Substring(2, 2) + month + day.ToString().PadLeft(2, '0'))//若序列号为当天，则直接赋值
                    {
                        assembly_serial_no = serial_code;
                    }
                    else//若序列号不为当天，则更新为当天序列号的001
                    {
                        assembly_serial_no = year.Substring(2, 2) + month + day.ToString().PadLeft(2, '0') + "001";
                        UnitNo_Info_De.Code = year.Substring(2, 2) + month + day.ToString().PadLeft(2, '0') + "001";
                        Base_DataDetailRepository.Repository().Update(UnitNo_Info_De);//更新数据字典UnitNo信息
                    }
                    SetText(product_born_code_txt, assembly_serial_no);//总成序列号
                }
                PART part_entity = current_annexs_list.Find(s => s.PART_CODE == key_part_code_txts[i].Text);//获得该条码对应的零部件信息
                string supplierCode = null, supplierName = null;
                string[] strArr = key_part_barcode.Split('^');
                if (strArr.Length > 2)
                {
                    supplierCode = key_part_barcode.Split('^')[1];
                }
                else
                {
                    if (key_part_barcode.Length > 14)
                    {
                        supplierCode = key_part_barcode.Substring(8, 6);
                    }
                }
                Part_Supplier supplierEntity = Part_SupplierList.FindLast(t => t.supplier_code == supplierCode);
                if (supplierEntity == null || supplierEntity.supplier_key == null)
                {
                }
                else
                {
                    supplierName = supplierEntity.supplier_name;
                }
                annex_infor.GD_PART_CODE = part_entity.PART_CODE;
                annex_infor.GD_PART_NAME = part_entity.PART_NAME;
                annex_infor.GD_PART_VINCODE = key_part_barcode;
                annex_infor.GD_ASSAMBLE_CODE = assembly_serial_no + supply_txt.Text + assembly_cobox.Text.ToString();
                annex_infor.SUPPLIER_CODE = supplierCode;
                annex_infor.SUPPLIER_NAME = supplierName;
                annex_infor.Create();
                return annex_infor;
            }
            catch (Exception ex)
            {
                SetText(key_part_barcode_txts[i], "提交安全件信息失败！");
                isEXception = true;
                DialogResult = MyMsgBox.Show("条形码有误，请确认后重新扫描！", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error);
                switch (DialogResult)
                {
                    case DialogResult.None:
                        isEXception = false;
                        break;
                    case DialogResult.OK:
                        isEXception = false;
                        break;
                }
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("提交安全件信息失败！" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
                return null;
            }
        }
        #endregion
        #endregion
        #region 绑定安全件数据之后如果全部采集完成 进行打印
        public void AfterBindPartBarCode()//主要是如果全部采集完成 进行打印
        {
            try
            {

                Log.GetInstance.WriteLog("已扫描安全件个数：" + collectedcount + ",一共需要：" + collectioncount);//观察
                if (collectedcount == collectioncount)//如果采集数量等于要求采集数量，采集完成
                {
                    #region 待采集随机附件全部完成，开始打印随机附件标签，并更新总成序列号信息
                    #region 插入打印记录
                    HCL_PRINT_RECORD hcl_record_entity = new HCL_PRINT_RECORD();
                    PART part_entity = partlist.Find(s => s.PART_CODE == assembly_cobox.Text);//获得该条码对应的零部件信息
                    hcl_record_entity.GD_PART_CODE = part_entity.PART_CODE;
                    hcl_record_entity.GD_PART_NAME = part_entity.PART_NAME;
                    hcl_record_entity.GD_ASSAMBLE_CODE = assembly_serial_no + supply_txt.Text + part_entity.PART_CODE;
                    hcl_record_entity.SERIAL_NO = assembly_serial_no;
                    hcl_record_entity.Create();
                    HCL_PRINT_RECORDRepository.Repository().Insert(hcl_record_entity);
                    printAssembleCode = assembly_serial_no + supply_txt.Text + part_entity.PART_CODE;
                    printAssembleCode1 = assembly_serial_no + supply_txt.Text;
                    printAssembleCode2 = part_entity.PART_CODE;

                    Log.GetInstance.WriteLog("打印记录插入成功，" + hcl_record_entity.GD_ASSAMBLE_CODE);//观察
                    #endregion

                    #region 更新序列号
                    Base_DataDictionary UnitNo_Info = Base_DataRepository.Repository().FindEntity("Code", "HCL_NO");
                    Base_DataDictionaryDetail UnitNo_Info_De = Base_DataDetailRepository.Repository().FindList().Where(s => s.DataDictionaryId == UnitNo_Info.DataDictionaryId && s.FullName == "HCL_SERIAL_NO").FirstOrDefault();
                    UnitNo_Info_De.Code = UnitNo_Info_De.Code.Substring(0, 5) + (int.Parse(UnitNo_Info_De.Code.Substring(5, 3)) + 1).ToString().PadLeft(3, '0');
                    Base_DataDetailRepository.Repository().Update(UnitNo_Info_De);//更新数据字典HCL_NO信息
                    Log.GetInstance.WriteLog("更新序列号成功，" + UnitNo_Info_De.Code);//观察
                    #endregion

                    #region 打印标签
                    Myprinter(false);

                    #endregion

                    #region 重新出发加载待采集信息，准备进行下一个随机附件包的采集
                    InitializationForm(true);//初始化后，产品出生证的值为空
                    if (assembly_cobox.Text.ToString() != "==请选择==")
                    {
                        currentbomlist = bomlist.Where(s => s.Parent_Part_Key == assembly_cobox.SelectedValue.ToString()).OrderBy(t => t.RESERVE01).ToList();//当前选中总成对应BOM信息
                        current_annexs_list = partlist.Where(s => currentbomlist.Select(t => t.Part_Key).Contains(s.PART_KEY)).ToList();//对应总成下的随机附件物料详细信息
                        current_annexs_list = partlist.Where(s => currentbomlist.Select(t => t.Part_Key).Contains(s.PART_KEY)).OrderBy(y => y.PART_CODE).ToList();//对应总成下的随机附件物料详细信息

                        string[] orderArr1 = new string[] { "否", "是" };
                        current_annexs_list = current_annexs_list.OrderBy(t =>
                        {
                            var i = 0; i = Array.IndexOf(orderArr1, t.RESERVE07); if (i != -1) { return i; }
                            else
                            {
                                return int.MaxValue;
                            }
                        }).ThenBy(s => s.PART_CODE).ToList();//先按是否解析，再按物料号排序
                        EnclosurePartShow();
                    }
                    #endregion
                    #region 刷新打印记录展示
                    //Log.GetInstance.WriteLog("更新前：");//观察

                    //showProductQueue();
                    //Log.GetInstance.WriteLog("更新后：");//观察

                    #endregion

                    #endregion
                }
                else if (collectedcount != 0 && collectedcount < KeyPartData.Count)//如果已经采集安全件了，但采集数量小于要求采集数量，即部分采集
                {

                }
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("绑定安全件数据之后出现错误：" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        #region 打印相关方法

        private void Myprinter(bool isManual)
        {
            int printok = 0;
            PrintDocument pd = new PrintDocument(); 
             if (isManual)
            {
                pd.PrintPage += new PrintPageEventHandler(printDocument_PrintA4Page1_Manual);

            }
            else
            {
                pd.PrintPage += new PrintPageEventHandler(printDocument_PrintA4Page);

            }
            pd.DefaultPageSettings.PrinterSettings.PrinterName = PrintName; //获取或设置要使用的打印机的名称
            #region 4.0
            pd.DefaultPageSettings.PaperSize = new PaperSize("Custum", 315, 78);//70*50
            #endregion
            int isok = ishasprint(PrintName);
            if (isok > 0)
            {
                pd.Print();
                if (isManual)
                {
                    Log.GetInstance.WriteLog("手动打印成功！"+ manualPrintAssembleCode);//观察

                }
                else
                {
                    Log.GetInstance.WriteLog("打印成功！"+ printAssembleCode);//观察

                }
                #region PB 预览
                //PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
                //printPreviewDialog1.Document = pd40;
                //printPreviewDialog1.FormBorderStyle = FormBorderStyle.Fixed3D;
                //DialogResult dr = printPreviewDialog1.ShowDialog();
                //if (dr != DialogResult.OK)
                //{
                //    //pd.Print();
                //}
                #endregion
                printok = 1;
            }
            else
            {
                printok = 0;
                MyMsgBox.Show("未安装打印机:" + PrintName + "", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error, 3);
            }
        }
        //判断本机是否已经安装该打印机
        public int ishasprint(string PrintName)
        {
            int isok = 0;
            List<string> AllPrintNames = GetPrintNames();
            if (AllPrintNames.Count > 0)
            {
                for (int i = 0; i < AllPrintNames.Count; i++)
                {
                    if (AllPrintNames[i] == PrintName)
                    {
                        isok = isok + 1;
                    }
                }
            }
            return isok;
        }
        public List<string> GetPrintNames()
        {
            PrintDocument print = new PrintDocument();
            string sDefault = print.PrinterSettings.PrinterName;//默认打印机名
            List<string> PrintNames = new List<string>();
            foreach (string sPrint in PrinterSettings.InstalledPrinters)//获取所有打印机名称
            {
                PrintNames.Add(sPrint);
            }
            return PrintNames;
        }
        private void printDocument_PrintA4Page(object sender, PrintPageEventArgs e)
        {
            Font f1 = new Font("黑体", 11, System.Drawing.FontStyle.Bold);
            Font f2 = new Font("黑体", 12, System.Drawing.FontStyle.Bold);

            // Font f2 = new Font("黑体", 18, System.Drawing.FontStyle.Bold);
            Font f3 = new Font("黑体", 9, System.Drawing.FontStyle.Bold);
            Font f4 = new Font("黑体", 10, System.Drawing.FontStyle.Bold);
            Font titleFont = new Font("黑体", 11, System.Drawing.FontStyle.Bold);//标题字体           
            Font fntTxt = new Font("宋体", 10, System.Drawing.FontStyle.Regular);//正文文字         
            Font fntTxt1 = new Font("宋体", 8, System.Drawing.FontStyle.Regular);//正文文字 
            Font fntTxt2 = new Font("宋体", 9, System.Drawing.FontStyle.Regular);//正文文字           
            Font fntTxt3 = new Font("宋体", 6, System.Drawing.FontStyle.Regular);//正文文字           

            System.Drawing.Brush brush = new SolidBrush(System.Drawing.Color.Black);//画刷           
            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black);           //线条颜色         

            float x = 0F;
            float y = 0F;
            float xmax = 68 * 4;
            float ymax = 45 * 4;//预留下边距20
            float ymax2 = 50 * 4;

            //绘图 表格
            float leftbianJu = 8;
            float topbianJu = 30;
            try
            {
                e.Graphics.DrawString("JAC江淮汽车 原装部件   安全件采集", f2, brush, new System.Drawing.Point(7, 7));
                Bitmap bitmap1 = CreateQRCode(printAssembleCode, 49, 49, true); //50，50
                e.Graphics.DrawImage(bitmap1, new System.Drawing.Point(3, 22));//条码位置
                e.Graphics.DrawImage(bitmap1, new System.Drawing.Point(197, 25));//条码位置
                string ECM = "NO. " + printAssembleCode1;
                e.Graphics.DrawString(ECM, fntTxt2, brush, new System.Drawing.Point(57, 28));//57,30
                e.Graphics.DrawString(printAssembleCode2, fntTxt2, brush, new System.Drawing.Point(57, 42));//57,43
                string str = "";
                int i1 = firstCode.Length;
                if (firstCode.Length > 26)
                {
                    str = firstCode.Substring(0, 26) + "\n" + firstCode.Substring(26, i1 - 26);
                }
                else
                {
                    str = firstCode;
                }
                e.Graphics.DrawString(str, fntTxt3, brush, new System.Drawing.Point(57, 53));//57,56
                e.Graphics.DrawString(assembly_serial_no, fntTxt2, brush, new System.Drawing.Point(248, 40));
              
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        public static Bitmap CreateQRCode(string asset, int windth, int height, bool is_ShowBarcode)
        {
            #region 生成条码方法1
            EncodingOptions options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8", //编码
                Width = windth,             //宽度
                Height = height,
                PureBarcode = is_ShowBarcode,//高度
                Margin = 2
            };
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            //writer.Format = BarcodeFormat.CODE_128;


            //QRCodeEncoder qrEncoder = new QRCodeEncoder();
            //qrEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            //qrEncoder.QRCodeScale = 2;
            ////设置编码版本
            //qrEncoder.QRCodeVersion = 6;
            //qrEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            //return qrEncoder.Encode(asset, Encoding.UTF8);
            writer.Options = options;
            return writer.Write(asset);
            #endregion

        }
        #endregion

        #endregion
        #region 如果需要解析 判断扫描枪扫描的条码是第几个需要采集的数据（假设有>=1个） 目前全部不解析 暂时屏蔽
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
                            //if (wc_code.Contains("2.7"))//判断是否满足采集条件(2.7)
                            //{
                            //    if(key_part_code_txts[i].Text.Trim() == barcode.Substring(14, barcode.Length - 14))
                            //    break;
                            //}
                            //else if (wc_code.Contains("4.0") )//判断是否满足采集条件(4.0)
                            //{
                            //   if(key_part_code_txts[i].Text == barcode.Substring(1, 7))
                            //   break;
                            //}
                            if (barcode.Contains(key_part_code_txts[i].Text))
                            {
                                break;
                            }
                            allcollected = false;//一旦发现有未采集的，使allcollected变为false
                        }
                    }
                }
                if (i == 10 && allcollected == true) //如果全部循环一遍，仍然没有出现：需要采集但在本次采集仍没有被采集的安全件，则认为安全件已被全部采集,或扫描条码不在待采集队列中
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
                        //SetText(keypartbarcode_lbl, "条码错误");
                        // SetColor(keypartbarcode_lbl, Color.Red);
                        break;
                    }
                }
                return 11;
            }
        }
        #endregion


        #region 手动输入

        #region 点击手动输入   总成号
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
                    //SetCollectProgress(finish_lbl, 0, 0);//采集进度初始化
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
        #endregion

        #region 点击手动输入 零部件手动
        private void inputmodel_part_btn_Click(object sender, EventArgs e)
        {
            if (inputmodel_part_btn.Text == "手动输入")//自动转手动
            {
                //if (new ValidateForm("JAC_MES_KEYPART", "KEYPART_HAND_MODEL", "手动模式权限验证").ShowDialog() == DialogResult.OK)
                //{
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
                //}
            }
            else if (inputmodel_part_btn.Text == "扫描输入")//手动转自动
            {
                key_part_barcode_txt.Enabled = false;
                key_part_barcode2_txt.Enabled = false;
                key_part_barcode3_txt.Enabled = false;
                key_part_barcode4_txt.Enabled = false;
                key_part_barcode5_txt.Enabled = false;
                key_part_barcode6_txt.Enabled = false;
                key_part_barcode7_txt.Enabled = false;
                key_part_barcode8_txt.Enabled = false;
                key_part_barcode9_txt.Enabled = false;
                key_part_barcode10_txt.Enabled = false;

                inputmodel_part_btn.Text = "手动输入";
                confirm_part_btn.Enabled = false;
                confirm_part2_btn.Enabled = false;
                confirm_part3_btn.Enabled = false;
                confirm_part4_btn.Enabled = false;
                confirm_part5_btn.Enabled = false;
                confirm_part6_btn.Enabled = false;
                confirm_part7_btn.Enabled = false;
                confirm_part8_btn.Enabled = false;
                confirm_part9_btn.Enabled = false;
                confirm_part10_btn.Enabled = false;

            }
        }
        #endregion

        #endregion
        #region 手动输入点击确认（OK）
        private void confirm_part_btn_Click(object sender, EventArgs e)//安全件模块确认
        {
            try
            {
                if (inputmodel_part_btn.Text == "扫描输入" && confirm_part_btn.BackColor != HasCollectBtnColor)//只有在手动输入的条件下，OK键才有用
                {
                    if (supply_txt.Text == "")
                    {
                        //没有输入产品信息，或者输入产品信息错误，两种情况下，产品信息均不能顺利成功加载，即全部为空
                        SetText(key_part_barcode_txt, "请先输入正确产品信息。");
                    }
                    else
                    {
                        if (key_part_barcode_txt.Enabled == true)//在安全件编号允许人机交互（即Enabled == true）的情况下，OK键才有用。如果安全件编号无数据，手动点击ok无意义
                        {
                            Log.GetInstance.WriteLog("Input", "输入零部件编号：" + key_part_barcode_txt.Text);
                            int IsOk = BindKeyPartInfor(key_part_barcode_txt.Text, 0);
                            if (IsOk != 1)
                            {
                                SetText(key_part_barcode_txt, "随机附件绑定失败！请重试。");
                                //SetText(keypartbarcode_lbl, "条码错误");
                                //SetColor(keypartbarcode_lbl, Color.Red);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetText(key_part_barcode_txt, "随机附件条码不正确！请重试。");
                //SetText(keypartbarcode_lbl, "条码错误");
                //SetColor(keypartbarcode_lbl, Color.Red);
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
                            SetText(key_part_barcode2_txt, "随机附件绑定失败！请重试。");
                            //SetText(keypartbarcode_lbl, "条码错误");
                            //SetColor(keypartbarcode_lbl, Color.Red);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetText(key_part_barcode2_txt, "随机附件条码不正确！请重试。");
                //SetColor(keypartbarcode_lbl, Color.Red);
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
                            SetText(key_part_barcode3_txt, "随机附件绑定失败！请重试。");
                            //SetText(keypartbarcode_lbl, "条码错误");
                            //SetColor(keypartbarcode_lbl, Color.Red);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetText(key_part_barcode3_txt, "随机附件条码不正确！请重试。");
                //SetText(keypartbarcode_lbl, "条码错误");
                //SetColor(keypartbarcode_lbl, Color.Red);
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
                            SetText(key_part_barcode4_txt, "随机附件绑定失败！请重试。");
                            //SetText(keypartbarcode_lbl, "条码错误");
                            //SetColor(keypartbarcode_lbl, Color.Red);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetText(key_part_barcode4_txt, "随机附件条码不正确！请重试。");
                //SetText(keypartbarcode_lbl, "条码错误");
                //SetColor(keypartbarcode_lbl, Color.Red);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("随机附件条码不正确！请重试。" + ex.Message);
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
                            SetText(key_part_barcode5_txt, "随机附件绑定失败！请重试。");
                            //SetColor(keypartbarcode_lbl, Color.Red);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetText(key_part_barcode5_txt, "随机附件条码不正确！请重试。");
                //SetColor(keypartbarcode_lbl, Color.Red);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("随机附件条码不正确！请重试。" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        private void confirm_part6_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (inputmodel_part_btn.Text == "扫描输入" && confirm_part6_btn.BackColor != HasCollectBtnColor)//只有在手动输入的条件下，OK键才有用
                {
                    if (key_part_barcode6_txt.Enabled == true)//在安全件编号允许人机交互（即Enabled == true）的情况下，OK键才有用。如果安全件编号无数据，手动点击ok无意义
                    {
                        Log.GetInstance.WriteLog("Input", "输入零部件编号：" + key_part_barcode6_txt.Text);
                        int IsOk = BindKeyPartInfor(key_part_barcode6_txt.Text, 5);
                        if (IsOk != 1)
                        {
                            SetText(key_part_barcode6_txt, "随机附件绑定失败！请重试。");
                            //SetColor(keypartbarcode_lbl, Color.Red);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetText(key_part_barcode6_txt, "随机附件条码不正确！请重试。");
                //SetColor(keypartbarcode_lbl, Color.Red);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("随机附件条码不正确！请重试。" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        private void confirm_part7_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (inputmodel_part_btn.Text == "扫描输入" && confirm_part7_btn.BackColor != HasCollectBtnColor)//只有在手动输入的条件下，OK键才有用
                {
                    if (key_part_barcode7_txt.Enabled == true)//在安全件编号允许人机交互（即Enabled == true）的情况下，OK键才有用。如果安全件编号无数据，手动点击ok无意义
                    {
                        Log.GetInstance.WriteLog("Input", "输入零部件编号：" + key_part_barcode7_txt.Text);
                        int IsOk = BindKeyPartInfor(key_part_barcode7_txt.Text, 6);
                        if (IsOk != 1)
                        {
                            SetText(key_part_barcode7_txt, "随机附件绑定失败！请重试。");
                            //SetColor(keypartbarcode_lbl, Color.Red);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetText(key_part_barcode7_txt, "随机附件条码不正确！请重试。");
                //SetColor(keypartbarcode_lbl, Color.Red);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("随机附件条码不正确！请重试。" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        private void confirm_part8_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (inputmodel_part_btn.Text == "扫描输入" && confirm_part8_btn.BackColor != HasCollectBtnColor)//只有在手动输入的条件下，OK键才有用
                {
                    if (key_part_barcode8_txt.Enabled == true)//在安全件编号允许人机交互（即Enabled == true）的情况下，OK键才有用。如果安全件编号无数据，手动点击ok无意义
                    {
                        Log.GetInstance.WriteLog("Input", "输入零部件编号：" + key_part_barcode8_txt.Text);
                        int IsOk = BindKeyPartInfor(key_part_barcode8_txt.Text, 7);
                        if (IsOk != 1)
                        {
                            SetText(key_part_barcode8_txt, "随机附件绑定失败！请重试。");
                            //SetColor(keypartbarcode_lbl, Color.Red);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetText(key_part_barcode8_txt, "随机附件条码不正确！请重试。");
                //SetColor(keypartbarcode_lbl, Color.Red);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("随机附件条码不正确！请重试。" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        private void confirm_part9_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (inputmodel_part_btn.Text == "扫描输入" && confirm_part9_btn.BackColor != HasCollectBtnColor)//只有在手动输入的条件下，OK键才有用
                {
                    if (key_part_barcode9_txt.Enabled == true)//在安全件编号允许人机交互（即Enabled == true）的情况下，OK键才有用。如果安全件编号无数据，手动点击ok无意义
                    {
                        Log.GetInstance.WriteLog("Input", "输入零部件编号：" + key_part_barcode8_txt.Text);
                        int IsOk = BindKeyPartInfor(key_part_barcode9_txt.Text, 8);
                        if (IsOk != 1)
                        {
                            SetText(key_part_barcode9_txt, "随机附件绑定失败！请重试。");
                            //SetColor(keypartbarcode_lbl, Color.Red);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetText(key_part_barcode9_txt, "随机附件条码不正确！请重试。");
                //SetColor(keypartbarcode_lbl, Color.Red);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("随机附件条码不正确！请重试。" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }

        private void confirm_part10_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (inputmodel_part_btn.Text == "扫描输入" && confirm_part10_btn.BackColor != HasCollectBtnColor)//只有在手动输入的条件下，OK键才有用
                {
                    if (key_part_barcode10_txt.Enabled == true)//在安全件编号允许人机交互（即Enabled == true）的情况下，OK键才有用。如果安全件编号无数据，手动点击ok无意义
                    {
                        Log.GetInstance.WriteLog("Input", "输入零部件编号：" + key_part_barcode8_txt.Text);
                        int IsOk = BindKeyPartInfor(key_part_barcode10_txt.Text, 9);
                        if (IsOk != 1)
                        {
                            SetText(key_part_barcode10_txt, "随机附件绑定失败！请重试。");
                            //SetColor(keypartbarcode_lbl, Color.Red);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetText(key_part_barcode10_txt, "随机附件条码不正确！请重试。");
                //SetColor(keypartbarcode_lbl, Color.Red);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("随机附件条码不正确！请重试。" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
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
                //SetText(supply_txt, "");
                //SetText(product_batch_no_txt, "");
                //SetText(plan_code_txt, "");
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

                SetText(key_part_name6_txt, "");
                SetText(key_part_code6_txt, "");
                SetText(key_part_barcode6_txt, "");
                SetEnabled(confirm_part6_btn, false);
                SetColor(confirm_part6_btn, NotCollectBtnColor);
                SetVisible(key_part_name6_lbl, false);
                SetVisible(key_part_code6_lbl, false);
                SetVisible(key_part_barcode6_lbl, false);
                SetVisible(key_part_name6_txt, false);
                SetVisible(key_part_code6_txt, false);
                SetVisible(key_part_barcode6_txt, false);
                SetVisible(confirm_part6_btn, false);
                SetEnabled(key_part_barcode6_txt, false);

                SetText(key_part_name7_txt, "");
                SetText(key_part_code7_txt, "");
                SetText(key_part_barcode7_txt, "");
                SetEnabled(confirm_part7_btn, false);
                SetColor(confirm_part7_btn, NotCollectBtnColor);
                SetVisible(key_part_name7_lbl, false);
                SetVisible(key_part_code7_lbl, false);
                SetVisible(key_part_barcode7_lbl, false);
                SetVisible(key_part_name7_txt, false);
                SetVisible(key_part_code7_txt, false);
                SetVisible(key_part_barcode7_txt, false);
                SetVisible(confirm_part7_btn, false);
                SetEnabled(key_part_barcode7_txt, false);

                SetText(key_part_name8_txt, "");
                SetText(key_part_code8_txt, "");
                SetText(key_part_barcode8_txt, "");
                SetEnabled(confirm_part8_btn, false);
                SetColor(confirm_part8_btn, NotCollectBtnColor);
                SetVisible(key_part_name8_lbl, false);
                SetVisible(key_part_code8_lbl, false);
                SetVisible(key_part_barcode8_lbl, false);
                SetVisible(key_part_name8_txt, false);
                SetVisible(key_part_code8_txt, false);
                SetVisible(key_part_barcode8_txt, false);
                SetVisible(confirm_part8_btn, false);
                SetEnabled(key_part_barcode8_txt, false);

                SetText(key_part_name9_txt, "");
                SetText(key_part_code9_txt, "");
                SetText(key_part_barcode9_txt, "");
                SetEnabled(confirm_part9_btn, false);
                SetColor(confirm_part9_btn, NotCollectBtnColor);
                SetVisible(key_part_name9_lbl, false);
                SetVisible(key_part_code9_lbl, false);
                SetVisible(key_part_barcode9_lbl, false);
                SetVisible(key_part_name9_txt, false);
                SetVisible(key_part_code9_txt, false);
                SetVisible(key_part_barcode9_txt, false);
                SetVisible(confirm_part9_btn, false);
                SetEnabled(key_part_barcode9_txt, false);

                SetText(key_part_name10_txt, "");
                SetText(key_part_code10_txt, "");
                SetText(key_part_barcode10_txt, "");
                SetEnabled(confirm_part10_btn, false);
                SetColor(confirm_part10_btn, NotCollectBtnColor);
                SetVisible(key_part_name10_lbl, false);
                SetVisible(key_part_code10_lbl, false);
                SetVisible(key_part_barcode10_lbl, false);
                SetVisible(key_part_name10_txt, false);
                SetVisible(key_part_code10_txt, false);
                SetVisible(key_part_barcode10_txt, false);
                SetVisible(confirm_part10_btn, false);
                SetEnabled(key_part_barcode10_txt, false);

                SetText(inputmodel_product_btn, "手动输入");
                SetText(inputmodel_part_btn, "手动输入");

                //SetColor(productarrival_lbl, SystemColors.ControlDark);
                //SetColor(productborncode_lbl, SystemColors.ControlDark);
                //SetText(keypartbarcode_lbl, "条码正确");
                //SetColor(keypartbarcode_lbl, SystemColors.ControlDark);
                //SetColor(finish_lbl, SystemColors.ControlDark);
                //SetColor(allowrelease_lbl, SystemColors.ControlDark);
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("界面恢复初始状态时失败：" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        #endregion

        public delegate void init_printqueue_grv_p(DataGridView print_record);
        /// <summary>
        /// 显示上线队列
        /// </summary>
        private void showProductQueue()
        {
            if (this.InvokeRequired)
            {
                init_printqueue_grv_p t = new init_printqueue_grv_p(this.showPrintQueue);
                this.Invoke(t, new object[] { this.print_record_dgv });
            }
            else
            {
                showPrintQueue(this.print_record_dgv);
            }
        }
        private void showPrintQueue(DataGridView print_record)
        {
            List<HCL_PRINT_RECORD> printList = HCL_PRINT_RECORDRepository.Repository().FindList().OrderByDescending(t => t.CREATEDATE).ToList();
            List<D_HCL_PRINT_RECORD> D_printList = EntityHelper.EntityHelper.ListMapper<HCL_PRINT_RECORD, D_HCL_PRINT_RECORD>(printList).ToList();
            print_record.DataSource = D_printList;
            print_record.Columns["GD_ASSAMBLE_CODE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            print_record.Columns["SERIAL_NO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            print_record.Columns["GD_PART_CODE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            print_record.Columns["GD_PART_NAME"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            print_record.Columns["CREATEDATE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

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
                if (plan_product_data == null)
                {
                    MyMsgBox.Show("产品信息为空，无法进入Andon操作", "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information, 3);
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

        #region 其他
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
                    //GetBornCode(OpcName, false);
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
            //SetLableText(main_station_lbl, StationList.FindAll(s => s.IS_MAIN == "是").Select(s => s.STATION_CODE).FirstOrDefault() + "工位");//设置程序主工位名称
            //SetViceStationLabel();
        }

        #endregion


        private void key_part_name5_lbl_Click(object sender, EventArgs e)
        {

        }

        private void key_part_name6_lbl_Click(object sender, EventArgs e)
        {

        }

        private void key_part_name7_lbl_Click(object sender, EventArgs e)
        {

        }

        private void key_part_name8_lbl_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string assemblecode = print_record_dgv.CurrentRow.Cells[0].Value.ToString().Trim();//唯一标识去查找
            HCL_PRINT_RECORD printList = HCL_PRINT_RECORDRepository.Repository().FindList("GD_ASSAMBLE_CODE", assemblecode).FirstOrDefault();
            manualPrintAssembleCode = assemblecode;
            manualAssembleCode1 = printList.SERIAL_NO + "001288";
            manualprintAssembleCode2 = printList.GD_PART_CODE;
            manualassembly_serial_no = printList.SERIAL_NO;
            Myprinter(true);

        }
        private void printDocument_PrintA4Page1_Manual(object sender, PrintPageEventArgs e)
        {
            Font f1 = new Font("黑体", 11, System.Drawing.FontStyle.Bold);
            Font f2 = new Font("黑体", 12, System.Drawing.FontStyle.Bold);

            // Font f2 = new Font("黑体", 18, System.Drawing.FontStyle.Bold);
            Font f3 = new Font("黑体", 9, System.Drawing.FontStyle.Bold);
            Font f4 = new Font("黑体", 10, System.Drawing.FontStyle.Bold);
            Font titleFont = new Font("黑体", 11, System.Drawing.FontStyle.Bold);//标题字体           
            Font fntTxt = new Font("宋体", 10, System.Drawing.FontStyle.Regular);//正文文字         
            Font fntTxt1 = new Font("宋体", 8, System.Drawing.FontStyle.Regular);//正文文字 
            Font fntTxt2 = new Font("宋体", 9, System.Drawing.FontStyle.Regular);//正文文字           
            Font fntTxt3 = new Font("宋体", 6, System.Drawing.FontStyle.Regular);//正文文字           

            System.Drawing.Brush brush = new SolidBrush(System.Drawing.Color.Black);//画刷           
            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black);           //线条颜色         

            float x = 0F;
            float y = 0F;
            float xmax = 68 * 4;
            float ymax = 45 * 4;//预留下边距20
            float ymax2 = 50 * 4;

            //绘图 表格
            float leftbianJu = 8;
            float topbianJu = 30;
            try
            {
                e.Graphics.DrawString("JAC江淮汽车 原装部件   安全件采集", f2, brush, new System.Drawing.Point(7, 7));
                Bitmap bitmap1 = CreateQRCode(manualPrintAssembleCode, 49, 49, true); //50，50
                e.Graphics.DrawImage(bitmap1, new System.Drawing.Point(3, 22));//条码位置
                e.Graphics.DrawImage(bitmap1, new System.Drawing.Point(197, 25));//条码位置
                string ECM = "NO. " + manualAssembleCode1;
                e.Graphics.DrawString(ECM, fntTxt2, brush, new System.Drawing.Point(57, 28));//57,30
                e.Graphics.DrawString(manualprintAssembleCode2, fntTxt2, brush, new System.Drawing.Point(57, 42));//57,43
                string str = "";

                //int i1 = firstCode.Length;
                //if (firstCode.Length > 26)
                //{
                //    str = firstCode.Substring(0, 26) + "\n" + firstCode.Substring(26, i1 - 26);
                //}
                //else
                //{
                //    str = firstCode;
                //}
                e.Graphics.DrawString(str, fntTxt3, brush, new System.Drawing.Point(57, 53));//57,56
                e.Graphics.DrawString(manualassembly_serial_no, fntTxt2, brush, new System.Drawing.Point(248, 40));
                #region 4.0
                #endregion
                #region 2.7
                //Bitmap bitmap = CreateQRCode(ESN, 2, 50,false);
                //e.Graphics.DrawImage(bitmap, new System.Drawing.Point(15, 15));//条码位置
                #endregion
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void confirm_product_btn_Click(object sender, EventArgs e)
        {

        }

        private void key_part_code5_lbl_Click(object sender, EventArgs e)
        {

        }


    }
}

