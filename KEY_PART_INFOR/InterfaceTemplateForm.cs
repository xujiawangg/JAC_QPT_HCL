using DevExpress.XtraBars.Docking;
using DevExpress.XtraPdfViewer;
using DevExpress.XtraRichEdit;
using EntityHelper;
using HfutIE.Entity;
using HfutIE.Repository;
using HfutIE.Utilities;
using KEY_PART_INFOR;
using log4net;
using Microsoft.VisualBasic.PowerPacks;
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

namespace HfutIe
{
    public partial class InterfaceTemplateForm : Form
    {
        public InterfaceTemplateForm()
        {
            InitializeComponent();
        }

        #region 仓储
        RepositoryFactory<CONTROL_ADDRESS> Control_AddressRepositoryFactory = new RepositoryFactory<CONTROL_ADDRESS>();//控制地址
        RepositoryFactory<Station> StationRepositoryFactory = new RepositoryFactory<Station>();//工位
        RepositoryFactory<EQUIPMENT> EquipmentRepository = new RepositoryFactory<EQUIPMENT>();

        RepositoryFactory<P_KEY_PART_INFOR> P_KEY_PART_INFORRepositoryFactory = new RepositoryFactory<P_KEY_PART_INFOR>();//安全件信息过程表
        RepositoryFactory<DOC_KEY_PART_INFOR> DOC_KEY_PART_INFORRepositoryFactory = new RepositoryFactory<DOC_KEY_PART_INFOR>();//安全件信息档案表
        RepositoryFactory<DOC_EQUIP_STATUS> DOC_EQUIP_TIME_INFORepositoryFactory = new RepositoryFactory<DOC_EQUIP_STATUS>();//设备信息档案表
        RepositoryFactory<Part_Supplier> SupplierRepositoryFactory = new RepositoryFactory<Part_Supplier>();//供应商基本信息表
        RepositoryFactory<P_NOTOK_PRODUCT_INFOR> P_NOTOK_PRODUCT_INFORRepositoryFactory = new RepositoryFactory<P_NOTOK_PRODUCT_INFOR>();//不合格产品信息过程表
        RepositoryFactory<DOC_NOTOK_PRODUCT_INFOR> DOC_NOTOK_PRODUCT_INFORRepositoryFactory = new RepositoryFactory<DOC_NOTOK_PRODUCT_INFOR>();//不合格产品信息档案表
        RepositoryFactory<Base_DataDictionary> Base_DataRepository = new RepositoryFactory<Base_DataDictionary>();
        RepositoryFactory<Base_DataDictionaryDetail> Base_DataDetailRepository = new RepositoryFactory<Base_DataDictionaryDetail>();
        #endregion

        #region 内存公共变量
        List<CONTROL_ADDRESS> ControlAddressList;//该程序控制的停止器所配置的地址信息
        List<Station> StationLists;//加载程序控制的工位基本信息
        List<EQUIPMENT> EquipmentList;//该程序控制的所有设备基本信息

        static string strpath = "E:\\#1#纳威司达项目\\安全件1.png";
        public string wc_code = ReadXML.GetWCCode(System.Windows.Forms.Application.StartupPath + @"\WCConfig\WC.xml");//工位编号
        List<string> StationCodeList=new List<string>();//该PC对应的工作中心下所属工位的集合(数组形式)
        WorkCenterInfor wcinfor;
        string wc_key;
        string plan_code;//当前产品所属计划
        int collectedcount = 0;//判断已采集的安全件的数量
        //int finishcollected = 0;//finishcollected枚举值{0：未开始采集，1：一开始采集，未采集完成，2：采集完成}
        public List<BasicInfoDto> basicinfor;//根据工位得出的基本信息
        public List<P_KEY_PART_C> key_part_c;//安全件采集配置
        public List<Part_Supplier> supplier;//安全件供应商信息
        //public DataTable team_shift;//班组班制信息
        public P_ASSEMBLE_PRODUCT_STATE plan_product_data;//通过产品出生证查到的该产品出生证代表的计划、产品信息
        List<P_KEY_PART_C> KeyPartData;//根据产品出生证和工位号查询到需要采集的安全件信息
        
        TextBox[] key_part_name_txts;//安全件名称文本框
        TextBox[] key_part_code_txts;//安全件编号文本框
        TextBox[] key_part_barcode_txts;//安全件条码文本框
        Button[] confirm_part_btns;//OK按钮文本框
        Label[] key_part_name_lbls;//安全件名称label
        Label[] key_part_code_lbls;//安全件编号label
        Label[] key_part_barcode_lbls;//安全件条码label
        RectangleShape[] key_part_rectangleShapes;//形状label

        DOC_EQUIP_STATUS doc_equip_time_info = new DOC_EQUIP_STATUS();//加工记录表
        P_NOTOK_PRODUCT_INFOR p_notok_product_infor = new P_NOTOK_PRODUCT_INFOR();//不合格产品信息表

        DataTable dt_documents = new DataTable();
        #region 声明委托与事件，监控线边库物料数量

        public delegate void ClickDelegate(object sender, EventArgs e);
        public event ClickDelegate click;
        int materialandonnum = 0;//找到物料andon属于andon类型的第几项，后面andon呼叫时有用
        ANDON andon;

        public void AndonClickEvent()
        {
            andon = new ANDON(StationCodeList);
            for (int t = 0; t < andon.andonTypeTable.Rows.Count; t++)
            {
                if (andon.andonTypeTable.Rows[t]["andon_type_name"].ToString().Contains("物料"))
                {
                    materialandonnum = t;
                    break;
                }
            }
            click = null;
            click += andon.materialButton_Click;//单击选中某一物料
            click += andon.Andon_Click;//提交ANDON数据
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
            Stopwatch sw = new Stopwatch();
            sw.Start();
            StationCodeList.Add("2");
            ShowServerConnectionState();
            wcinfor = new WorkCenterInfor(StationCodeList);
            sw.Stop();
            long s = sw.ElapsedMilliseconds;
            sw.Start();
            Initialization();
            sw.Stop();
            long s2 = sw.ElapsedMilliseconds;
            Task.Run(new Action(() => AndonClickEvent()));
            SetLableText(main_station_lbl, wc_code + "工位");//设置程序工位名称
        }
        /// <summary>
        /// 界面关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEYPART1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Formclosing();
        }
        public void Initialization()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //team_shift = GetData.GetTeamByWccode(wc_code);
            sw.Stop();
            long s = sw.ElapsedMilliseconds;

            //登录人员显示
            usercode_txt.Text = SystemLog.UserCode;//系统登录员工编号
            username_txt.Text = SystemLog.UserName;//系统登录员工姓名
            DateTime dt = DateTime.Now;
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
            string get = HttpGet("http://localhost:28188/API/WebApi/GetPageInfo");
            CS_PageInfo pageinfo = get.ToJson<CS_PageInfo>();
            //MemoryStream myStream = new MemoryStream();
            //myStream.WriteByte(pageinfo.SystemPicture);
            Stream ms_system_pic = new MemoryStream(pageinfo.SystemPicture);
            //system_pic.Image = System.Drawing.Image.FromStream(ms_system_pic);//系统图标赋值
            SetPicture(system_pic, ms_system_pic);
            //Stream ms_lab_pic = new MemoryStream(pageinfo.LaboratoryPicture);//赋值不同图片时存在问题，待修改
            //SetPicture(lab_pic, ms_lab_pic);
            //lab_pic.Image= System.Drawing.Image.FromStream(ms_system_pic);//实验室图标赋值
            #endregion

            #region 生产状态初始化
            productarrival_lbl.BackColor = Color.LightGray;
            productborncode_lbl.BackColor = Color.LightGray;
            keypartbarcode_lbl.BackColor = Color.LightGray;
            finish_lbl.BackColor = Color.LightGray;
            allowrelease_lbl.BackColor = Color.LightGray;
            #endregion

            if (basicinfor.Count > 0)
            {
                wc_key = basicinfor[0].STATION_KEY.ToString();
            }
            else
            {
                MessageBox.Show("没有" + wc_code + "工位信息", "工位信息缺失", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (basicinfor.Count < 1 || /*key_part_c.Rows.Count < 1 ||*/ supplier.Count < 1 )
            {
                DialogResult dr = MessageBox.Show("基本信息获取失败！是否终止启动系统？", "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (dr == DialogResult.Yes)
                {
                    Formclosing();
                    //System.Windows.Forms.Application.Exit();
                }
            }
            ScannerHelper.wc_code = wc_code;

            #region 数据字典读取为Lable赋值
            Base_DataDictionary L_Info = Base_DataRepository.Repository().FindEntity("FULLNAME", "LableName");
            List<Base_DataDictionaryDetail> L_Info_De = Base_DataDetailRepository.Repository().FindList("DATADICTIONARYID", L_Info.DataDictionaryId);
            foreach(var item_lable in L_Info_De)
            {
                if(item_lable.FullName== "LaboratoryName")
                {
                    //LaboratoryName.Text = item_lable.Code;
                }
                if(item_lable.FullName == "ProgramName_CN_CS")
                {
                    ProgramName_CN_CS.Text= item_lable.Code;
                }
                if(item_lable.FullName == "ProgramName_CN_EN")
                {
                    ProgramName_CN_EN.Text = item_lable.Code;
                }
            }
            #endregion
        }
        public void Formclosing()//为了防止出现方法同名，此处用Formclosing命名，原定用FormClosing命名
        {
            try
            {

            }
            finally
            {
                this.Dispose();
                this.Close();
            }
        }
        /// <summary>
        /// 点击关闭按钮关闭程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Formclose_btn_Click(object sender, EventArgs e)
        {
            Formclosing();
        }
        #endregion

        #region 手动输入

        #region 点击手动输入

        private void inputmodel_product_btn_Click(object sender, EventArgs e)//产品出生证手动输入与PLC信号跳变调整
        {
            if (inputmodel_product_btn.Text == "手动输入")
            {
                
            }
            else if (inputmodel_product_btn.Text == "自动读取")
            {
               
            }
        }

        private void inputmodel_part_btn_Click(object sender, EventArgs e)//安全件条码手动输入与扫描输入调整
        {
            if (inputmodel_part_btn.Text == "手动输入")//自动转手动
            {
                
            }
            else if (inputmodel_part_btn.Text == "扫描输入")//手动转自动
            {

            }
        }
        #endregion

        #region 手动输入点击确认（OK）

        private void confirm_product_btn_Click(object sender, EventArgs e)//计划模块确认
        {
           
        }
        private void confirm_part_btn_Click(object sender, EventArgs e)//安全件模块确认
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                
            }
        }
        
        #endregion

        #endregion
        
        #region 使用委托进行跨线程处理控件
        /// <summary>
        /// 使用委托为组件改变可见性
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
        /// 
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
        //    public delegate void MessageBoxHandler();
        //public void aaa()
        //{
        //    this.Invoke(new MessageBoxHandler(delegate() { MessageBox.Show("千一网络"); }));// MessageBox.Show((IWin32Window)this, "提示"); // 由于放在 Invoke 中，也可以这么用，但效果和上面的一样。}))
        //}
        #endregion

        #region 界面恢复初始状态

        private void InitializationForm(bool resetcollectedcount)
        {
            try
            {
                SetText(inputmodel_product_btn, "手动输入");
                SetText(inputmodel_part_btn, "手动输入");
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("界面恢复初始状态时失败：" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        #endregion

        #region 时间显示

        private void recordtime_tmr_Tick(object sender, EventArgs e)
        {
            try
            {
                DateTime dt = DateTime.Now;
                string datetime = "时间" + ':' + dt.Year.ToString().Trim() + "年" + dt.Month.ToString("00").Trim() + "月" + dt.Day.ToString("00").Trim() + "日" + "  " + dt.Hour.ToString("00").Trim() + ":" + dt.Minute.ToString("00").Trim() + ":" + dt.Second.ToString("00").Trim();
                //string datetime = dt.ToShortDateString().Trim() + " " + dt.ToLongTimeString().Trim();
                SetText(showtime_lbl, datetime);

                #region 每天12点将加工数量置为0

                if (dt.Hour == 0 && dt.Minute == 0)
                {

                }
                #endregion
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("时间显示时出现错误：" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
        }
        #endregion

        #region 显示服务器通讯信号
        private bool ServerCommunicationState = false;//服务器通讯状态

        private readonly string ServerIP = data.data.dataSource;//服务器的IP地址

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
                        ShowServerConnection( true);
                        //if (!listenEquipConnection.IsAlive)
                        //{
                        //    this.listenEquipConnection = new Thread(PLCToMES);
                        //    listenEquipConnection.Start();
                        //}
                    }
                    else
                    {
                        ServerCommunicationState = false;
                        ShowServerConnection( false);
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

        public void SetLblColors(Label lbl, Color backcolor)
        {
            if (lbl == null) return;
            if (lbl.IsDisposed || !lbl.Parent.IsHandleCreated) return;
            lbl.BeginInvoke(new MethodInvoker(() => {
                lbl.BackColor = backcolor;
            }));
        }

        public void ShowServerConnection(bool state)
        {
            if (state)
            {
                SetLblColors(serverconnectionstate_lbl, Color.ForestGreen);
            }
            else
            {
                SetLblColors(serverconnectionstate_lbl, Color.Red); ;
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
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }
        private void size_pic_box_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void Exit_pic_Box_Click(object sender, EventArgs e)
        {
            Formclosing();
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

        private void storage_quntity_lbl_Click(object sender, EventArgs e)
        {
            SetText(storage_quntity_lbl, "");
            SetColor(storage_quntity_lbl, Color.Red);
            SetVisible(storage_quntity_lbl, false);
        }

        public static string HttpGet(string url)
        {
            try
            {
                //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                Encoding encoding = Encoding.UTF8;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.ContentType = "application/xml";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}

