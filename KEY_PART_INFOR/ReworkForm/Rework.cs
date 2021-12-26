using EntityHelper;
using HfutIe;
using HfutIE.Entity;
using HfutIE.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KEY_PART_INFOR
{
    public partial class Rework : Form
    {
        #region 全局变量
        P_ASSEMBLE_PRODUCT_STATE PlanProductDataEntity;
        List<CONTROL_ADDRESS> ControlAddressList;//该工位的停止器所配置的地址信息
        string station_key;
        int collection_sort_code = 1;//初始被采集编号为1，用于信息补录排序显示
        BasicInfoDto BasicInfo;
        List<STATION> stationlist = new List<STATION>();//工位集合
        List<PRODUCT_FAULT_TYPE> faulttypelist = new List<PRODUCT_FAULT_TYPE>();//故障类型
        List<PRODUCT_FAULT_ITEM> faultlist = new List<PRODUCT_FAULT_ITEM>();//故障名称
        List<PRODUCT_MAINTAIN_TYPE> maintaintypelist = new List<PRODUCT_MAINTAIN_TYPE>();//排故类型
        List<PRODUCT_MAINTAIN_ITEM> maintainlist = new List<PRODUCT_MAINTAIN_ITEM>();//排故名称
        DataTable dt_repaironline_stations = new DataTable();//返修上线工位集合
        DataTable dt_fault_type = new DataTable();//故障类型
        DataTable dt_fault_item = new DataTable();//故障名称
        DataTable dt_maintain_type = new DataTable();//排故类型
        DataTable dt_maintain_item = new DataTable();//排故名称
        #endregion

        #region 仓储
        static RepositoryFactory<P_KEY_PART_C> KEY_PART_CRepositoryFactory = new RepositoryFactory<P_KEY_PART_C>();//安全件采集配置表
        static RepositoryFactory<P_ASSEMBLE_PRODUCT_STATE> P_ASSEMBLE_PRODUCT_STATERepositoryFactory = new RepositoryFactory<P_ASSEMBLE_PRODUCT_STATE>();//生产状态过程表
        static RepositoryFactory<P_KEY_PART_INFOR> P_KEY_PART_INFORRepositoryFactory = new RepositoryFactory<P_KEY_PART_INFOR>();//安全件信息过程表
        static RepositoryFactory<DOC_KEY_PART_INFOR> DOC_KEY_PART_INFORRepositoryFactory = new RepositoryFactory<DOC_KEY_PART_INFOR>();//安全件信息档案表
        static RepositoryFactory<P_PRODUCT_REPAIR_INFO> P_PRODUCT_REPAIR_INFORepositoryFactory = new RepositoryFactory<P_PRODUCT_REPAIR_INFO>();//产品返修信息过程表
        static RepositoryFactory<PRODUCT_FAULT_TYPE> PRODUCT_FAULT_TYPEepositoryFactory = new RepositoryFactory<PRODUCT_FAULT_TYPE>();//故障类型表
        static RepositoryFactory<PRODUCT_FAULT_ITEM> PRODUCT_FAULT_ITEMRepositoryFactory = new RepositoryFactory<PRODUCT_FAULT_ITEM>();//故障信息表
        static RepositoryFactory<PRODUCT_MAINTAIN_TYPE> PRODUCT_MAINTAIN_TYPERepositoryFactory = new RepositoryFactory<PRODUCT_MAINTAIN_TYPE>();//排故类型表
        static RepositoryFactory<PRODUCT_MAINTAIN_ITEM> PRODUCT_MAINTAIN_ITEMRepositoryFactory = new RepositoryFactory<PRODUCT_MAINTAIN_ITEM>();//排故信息表
        static RepositoryFactory<PRODUCT_FA_MAIN_INFOR> PRODUCT_FA_MAIN_INFORepositoryFactory = new RepositoryFactory<PRODUCT_FA_MAIN_INFOR>();//故障排故信息录入表
        static RepositoryFactory<CONTROL_ADDRESS> Control_AddressRepositoryFactory = new RepositoryFactory<CONTROL_ADDRESS>();//控制地址
        static RepositoryFactory<STATION> StationRepositoryFactory = new RepositoryFactory<STATION>();//工位信息
        static RepositoryFactory<STOPPER> StopperRepositoryFactory = new RepositoryFactory<STOPPER>();//停止器信息
        #endregion

        public Rework(P_ASSEMBLE_PRODUCT_STATE plan_product_data, string wc_key, BasicInfoDto basicinfor)
        {
            InitializeComponent();
            PlanProductDataEntity = plan_product_data;//产品加工信息表
            station_key = wc_key;//工位主键
            BasicInfo = basicinfor;//基础信息
        }

        #region 界面加载
        private void Rework_Load(object sender, EventArgs e)
        {
            try
            {
                LoadFaultInfo();//获取并加载故障排故信息及返修上线工位
                //LoadRepairOffLineStation();//加载返修下线工位
                ShowServerConnectionState();
                SetDatagridview();
                key_part_list_dgv.AutoGenerateColumns = false;
                key_part_list_dgv.ClearSelection();

                RefreshDgv();//加载数据
                ScannerHelper.OpenCom(scanport_rework);//打开扫描枪连接

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
                    //OPCHelper.OPC_Connecion(ControlAddressList, Change);
                }
                ));
                Task.Run(new Action(ReFreshAddressByTiming));
                #endregion
            }
            catch(Exception ex)
            {

            }
        }
        #endregion

        #region 界面关闭
        private void Exit_pic_Box_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Com01 初始化Datagridview
        /// <summary>
        /// 初始化Datagridview
        /// </summary>
        private void SetDatagridview()
        {
            try
            {
                #region 01 声明列
                key_part_list_dgv.Columns.Add("PART_CODE", "安全件编号");
                key_part_list_dgv.Columns.Add("PART_NAME", "安全件名称");
                key_part_list_dgv.Columns.Add("RESERVE1", "安全件条码");//预留字段2做安全件条码临时用
                key_part_list_dgv.Columns.Add("RESERVE2", "排序码");//预留字段1做排序码临时用
                #endregion

                #region 02 绑定数据源设置
                key_part_list_dgv.Columns["PART_CODE"].DataPropertyName = "PART_CODE";
                key_part_list_dgv.Columns["PART_NAME"].DataPropertyName = "PART_NAME";
                key_part_list_dgv.Columns["RESERVE1"].DataPropertyName = "RESERVE1";
                key_part_list_dgv.Columns["RESERVE2"].DataPropertyName = "RESERVE2";
                #endregion

                #region 03 列可见性
                key_part_list_dgv.Columns["RESERVE2"].Visible = false;
                //dataGridView2.Columns["object_key"].Visible = false;
                #endregion

                #region 界面格式
                key_part_list_dgv.AlternatingRowsDefaultCellStyle.Font = new Font("华文细黑", 12F);
                key_part_list_dgv.DefaultCellStyle.Font = new Font("华文细黑", 12F);
                key_part_list_dgv.ColumnHeadersDefaultCellStyle.Font = new Font("华文细黑", 12F);

                sectionstation_dgv.AlternatingRowsDefaultCellStyle.Font = new Font("华文细黑", 12F);
                sectionstation_dgv.DefaultCellStyle.Font = new Font("华文细黑", 12F);
                sectionstation_dgv.DefaultCellStyle.ForeColor = Color.Black;
                sectionstation_dgv.ColumnHeadersDefaultCellStyle.Font = new Font("华文细黑", 12F);
                #endregion

                key_part_list_dgv.DefaultCellStyle.ForeColor = Color.Black;
                key_part_list_dgv.Columns["RESERVE1"].Width = 250;//安全件条码
                key_part_list_dgv.Columns["PART_CODE"].Width = 200;
                key_part_list_dgv.Columns["PART_NAME"].Width = 250;
                //dataGridView2.Columns["location_code"].Width = 120;
                //dataGridView2.Columns["location_name"].Width = 120;
                //dataGridView2.AutoGenerateColumns = false;
            }
            catch
            {

            }
        }
        #endregion

        /// <summary>
        /// 从扫描枪中读取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scanport_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                string barcodetype = "";
                string barcode = "";
                List<P_KEY_PART_C> list = new List<P_KEY_PART_C>();
                bool IsOk = false;//是否找到匹配的安全件
                bool is_config = false;//是否被匹配安全件
                int Is_Commit = 0;
                if (ServerCommunicationState)
                {
                    P_ASSEMBLE_PRODUCT_STATE assemble_state_entity = P_ASSEMBLE_PRODUCT_STATERepositoryFactory.Repository().FindEntity(PlanProductDataEntity.ASSEMBLE_PRODUCT_STATE_KEY);
                    if (assemble_state_entity.OPERATED_STATE == "返修下线")
                    {
                        object obj = ScannerHelper.dataReceive(scanport_rework, out barcodetype, out IsOk);
                        barcode = obj.ToString().Trim();
                        CheckForIllegalCrossThreadCalls = false;
                        if (!string.IsNullOrEmpty(barcode))
                        {
                            for (int i = 0; i < key_part_list_dgv.Rows.Count; i++)
                            {
                                if (key_part_list_dgv.Rows[i].Cells["PART_CODE"].Value.ToString() == barcode)
                                {
                                    collection_sort_code++;
                                    IsOk = true;
                                    is_config = true;
                                    string partcode = key_part_list_dgv.Rows[i].Cells["PART_CODE"].Value.ToString();
                                    Is_Commit = BindKeyPartInfor(partcode, barcode);//提交安全件信息
                                }
                                P_KEY_PART_C entity = (P_KEY_PART_C)key_part_list_dgv.Rows[i].DataBoundItem;
                                if (is_config == true)
                                {
                                    entity.RESERVE1 = barcode;
                                    entity.RESERVE2 = collection_sort_code.ToString();
                                    is_config = false;
                                }
                                list.Add(entity);
                            }
                            if (IsOk == false)//全部遍历也未找到匹配的物料信息
                            {
                                string info = "未找到匹配的\r\n安全件信息!!";
                                SetLableText(prompt_information_lbl, info);
                            }
                            else if (Is_Commit != 0)
                            {
                                list = list.OrderByDescending(s => s.RESERVE2).ToList();
                                SetDgvDataSource(key_part_list_dgv, list);//使用委托为dgv赋值
                                                                          //key_part_list_dgv.DataSource = list;
                                key_part_list_dgv.ClearSelection();//去除选中行
                            }
                        }
                    }
                    else
                    {
                        string info = "该产品未进行\r\n返修，无需补\r\n录信息!";
                        SetLableText(prompt_information_lbl, info);
                    }
                }
                else
                {
                    MessageBox.Show("网络异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                //ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                //log.Fatal("扫描枪获取数据时出现错误：" + ex.Message);
                ExLogHelper.Instance.WriteLog(ex, OperationType.Run);
            }
            finally
            {
                scanport_rework.DiscardInBuffer();//清理输入缓冲区
                scanport_rework.DiscardOutBuffer();//清理输出缓冲区
            }
        }

        #region 刷新地址信息
        /// <summary>
        /// 定时刷新数据库地址信息
        /// </summary>
        private void ReFreshAddress()
        {
            try
            {
                STOPPER stopentity = StopperRepositoryFactory.Repository().FindEntity("station_key", station_key);//主工位对应的停止器
                ControlAddressList = Control_AddressRepositoryFactory.Repository().FindList().Where(s => s.POSITION_KEY == stopentity.STOPPER_KEY).ToList();//
                Task.Run(new Action(UpdateLastActivityTime));
            }
            catch (Exception ex)
            {
                //Log.Instance.WriteLog(ex.Message);
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
                    //Log.Instance.WriteLog(ex.Message);
                }
            }
        }
        private void UpdateLastActivityTime()
        {
            //try
            //{
            //    string programName = ConfigurationManager.AppSettings["ProgramName"].ToString();//读取该程序的程序名
            //    Program_base modify = new Program_base();
            //    modify = Program_baseFactory.Repository().FindEntity("Program_name", programName);
            //    if (modify == null)
            //    {
            //        modify.Create();
            //        modify.Program_name = programName;
            //        modify.Program_key = Guid.NewGuid().ToString();
            //        modify.PC_IP = OPCHelper.getIP();
            //        modify.LAST_ACTIVATE_TIME = System.DateTime.Now;
            //        Program_baseFactory.Repository().Insert(modify);
            //    }
            //    modify.PC_IP = OPCHelper.getIP();
            //    modify.LAST_ACTIVATE_TIME = System.DateTime.Now;
            //    Program_baseFactory.Repository().Update(modify);
            //}
            //catch
            //{

            //}
        }
        #endregion

        #region 显示服务器通讯信号
        private bool ServerCommunicationState = false;//服务器通讯状态

        private readonly string ServerIP = DBhelperOracle.dataSource;//服务器的IP地址

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
                    }
                    else
                    {
                        ServerCommunicationState = false;
                    }
                }
                catch (Exception ex)
                {
                    //if (listenEquipConnection.IsAlive)
                    //{
                    //    listenEquipConnection.Abort();
                    //}
                    ServerCommunicationState = false;
                    //ShowServerConnection(false);
                }
            }
        }
        #endregion

        private void Rework_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
                ScannerHelper.CloseCom(scanport_rework);//打开扫描枪连接
            }
            catch
            {

            }
        }

        #region dgv序号
        private void key_part_list_dgv_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                    e.RowBounds.Location.Y,
                  key_part_list_dgv.RowHeadersWidth - 4,
                 e.RowBounds.Height);
                TextRenderer.DrawText(e.Graphics,
                   (e.RowIndex + 1).ToString(),
                    key_part_list_dgv.RowHeadersDefaultCellStyle.Font,
                    rectangle,
                    key_part_list_dgv.RowHeadersDefaultCellStyle.ForeColor,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
            }
            catch
            {

            }
        }
        #endregion

        #region 返修下线按钮
        private void rework_offline_btn_Click(object sender, EventArgs e)
        {
            Repository<P_KEY_PART_INFOR> tran = new Repository<P_KEY_PART_INFOR>();
            DbTransaction isOpenTrans = tran.BeginTrans();
            try
            {
                P_ASSEMBLE_PRODUCT_STATE assemble_state_entity = P_ASSEMBLE_PRODUCT_STATERepositoryFactory.Repository().FindEntity(PlanProductDataEntity.ASSEMBLE_PRODUCT_STATE_KEY);
                if (assemble_state_entity.OPERATED_STATE != "返修下线")
                {
                    assemble_state_entity.OPERATED_STATE = "返修下线";//操作状态变更为返修下线
                    assemble_state_entity.IS_REPAIR = "是";//是否返修状态变更
                    assemble_state_entity.REPAIR_FREQUENCY++;//返修频次+1
                    P_ASSEMBLE_PRODUCT_STATERepositoryFactory.Repository().Update(assemble_state_entity, isOpenTrans);

                    P_PRODUCT_REPAIR_INFO repairentity = new P_PRODUCT_REPAIR_INFO();
                    repairentity = EntityHelper.EntityHelper.EntityMapper(PlanProductDataEntity, repairentity);//为实体增加产品信息
                    STATION stationentity = stationlist.Where(s => s.STATION_KEY == station_key).FirstOrDefault();
                    repairentity.REPAIR_OFFLINE_STATION_KEY = station_key;
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

                    SetLableText(repair_offline_station_lbl, stationentity.STATION_CODE);//加载返修下线工位
                    string info = "产品返修下线\r\n成功!";
                    SetLableText(prompt_information_lbl, info);

                    RefreshDgv();//重新加载待采集安全件信息

                    #region 将产品进入返修下线状态写入OPC
                    //CONTROL_ADDRESS repair_off_line = ControlAddressList.Find(s => s.ADDRESS_TYPE == ((int)AddressCategory.repair_offline).ToString());
                    //OPCHelper.SynOpcItemsWrite(repair_off_line.ADDRESS_PATH.Split(';'), "1".Split(','));
                    #endregion
                }
                else
                {
                    string info = "产品已处于返\r\n修状态!";
                    SetLableText(prompt_information_lbl, info);
                }
                //ScannerHelper.CloseCom(scanport_rework);//打开扫描枪连接
                //ScannerHelper.OpenCom(scanport_rework);//打开扫描枪连接
            }
            catch (Exception ex)
            {
                if (ex.ToString() == "某一OPC项不存在。")
                {
                    SetLableText(prompt_information_lbl, "PLC写入不\r\n成功。返修下\r\n线失败。");
                }
                isOpenTrans.Rollback();
            }
        }
        #endregion

        #region 返修上线按钮
        private void rework_online_btn_Click(object sender, EventArgs e)
        {
            Repository<P_KEY_PART_INFOR> tran = new Repository<P_KEY_PART_INFOR>();
            DbTransaction isOpenTrans = tran.BeginTrans();
            try
            {
                P_ASSEMBLE_PRODUCT_STATE assemble_state_entity = P_ASSEMBLE_PRODUCT_STATERepositoryFactory.Repository().FindEntity(PlanProductDataEntity.ASSEMBLE_PRODUCT_STATE_KEY);
                if (assemble_state_entity.OPERATED_STATE == "返修下线")
                {
                    assemble_state_entity.OPERATED_STATE = "返修上线";//操作状态变更为返修下线
                    P_ASSEMBLE_PRODUCT_STATERepositoryFactory.Repository().Update(assemble_state_entity, isOpenTrans);

                    P_PRODUCT_REPAIR_INFO repairentity = P_PRODUCT_REPAIR_INFORepositoryFactory.Repository().FindList().Where(s => s.PRODUCT_BORN_CODE == PlanProductDataEntity.PRODUCT_BORN_CODE && s.REPAIR_STATE=="返修下线").FirstOrDefault();//获取返修信息表中对应返修件的信息
                    STATION stationentity = stationlist.Where(s => s.STATION_KEY == repair_online_station_cobox.SelectedValue.ToString()).FirstOrDefault();
                    repairentity.REPAIR_ONLINE_STATION_KEY = repair_online_station_cobox.SelectedValue.ToString();
                    repairentity.REPAIR_ONLINE_STATION_CODE = stationentity.STATION_CODE;
                    repairentity.REPAIR_ONLINE_STATION_NAME = stationentity.STATION_NAME;
                    repairentity.REPAIR_ONLINE_TIME = ServerTime.Now;
                    repairentity.REPAIR_STATE = "返修上线";
                    repairentity.MODIFYDATE = ServerTime.Now;
                    repairentity.MODIFYUSERID = SystemLog.UserKey;
                    repairentity.MODIFYUSERNAME = SystemLog.UserName;
                    P_PRODUCT_REPAIR_INFORepositoryFactory.Repository().Update(repairentity);

                    string info = "产品返修上线\r\n成功!";
                    SetLableText(prompt_information_lbl, info);

                    #region 将所有勾选需要加工工位写入PLC中
                    List<string> select_station_code = new List<string>();
                    for (int i = 0; i < sectionstation_dgv.Rows.Count; i++)
                    {
                        if ((bool)sectionstation_dgv.Rows[i].Cells["Is_checked"].Value ==true)
                        {
                            select_station_code.Add(sectionstation_dgv.Rows[i].Cells["station_code"].Value.ToString());
                        }
                    }
                    #endregion

                    #region 将产品进入返修上线状态写入OPC
                    //CONTROL_ADDRESS repair_on_line = ControlAddressList.Find(s => s.ADDRESS_TYPE == ((int)AddressCategory.repair_online).ToString());
                    //OPCHelper.SynOpcItemsWrite(repair_on_line.ADDRESS_PATH.Split(';'), "1".Split(','));
                    #endregion
                }
                else
                {
                    string info = "该产品未进行\r\n返修，无需返\r\n修上线!";
                    SetLableText(prompt_information_lbl, info);
                }
                isOpenTrans.Commit();
                //ScannerHelper.CloseCom(scanport_rework);//打开扫描枪连接
                //ScannerHelper.OpenCom(scanport_rework);//打开扫描枪连接

            }
            catch (Exception ex)
            {
                if (ex.ToString() == "某一OPC项不存在。")
                {
                    SetLableText(prompt_information_lbl, "PLC写入不\r\n成功。返修上\r\n线失败。");
                }
                else
                {
                    SetLableText(prompt_information_lbl, "返修上\r\n线失败。");
                }
                isOpenTrans.Rollback();
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
                    string info = "网络异常";
                    SetLableText(prompt_information_lbl, info);
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
                    P_KEY_PART_C KeyPartData = KEY_PART_CRepositoryFactory.Repository().FindList().Where(s => s.PRODUCT_CODE == PlanProductDataEntity.PRODUCT_CODE && s.PART_CODE == partcode && s.MES_PLAN_CODE == PlanProductDataEntity.MES_PLAN_CODE).FirstOrDefault();
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
                    p_key_part_infor = EntityHelper.EntityHelper.EntityMapper(BasicInfo, p_key_part_infor);//为实体增加基本信息
                    p_key_part_infor.EQUIP_KEY = BasicInfo.EQUIPMENT_KEY;
                    p_key_part_infor.EQUIP_CODE = BasicInfo.EQUIPMENT_CODE;
                    p_key_part_infor.EQUIP_NAME = BasicInfo.EQUIPMENT_NAME;
                    //p_key_part_infor = supplier_infor.EntityMapper<Part_Supplier, P_KEY_PART_INFOR>();//为实体增加供应商信息
                    //p_key_part_infor = EntityHelper<P_KEY_PART_INFOR>.GetPartEntity(p_key_part_infor, team_shift);//为实体增加班制班组信息
                    //p_key_part_infor = plan_product_data.EntityMapper<P_ASSEMBLE_PRODUCT_STATE, P_KEY_PART_INFOR>();//为实体增加计划、产品信息
                    p_key_part_infor = EntityHelper.EntityHelper.EntityMapper(PlanProductDataEntity, p_key_part_infor);//为实体增加计划、产品信息
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
                List<DOC_KEY_PART_INFOR> doc_key_part_inforlist = DOC_KEY_PART_INFORRepositoryFactory.Repository().FindList().Where(s => s.MES_PLAN_KEY == PlanProductDataEntity.MES_PLAN_KEY && s.PRODUCT_BORN_CODE == PlanProductDataEntity.PRODUCT_BORN_CODE && s.PART_CODE == p_key_part_infor.PART_CODE).ToList();
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
                List<P_KEY_PART_INFOR> p_key_part_inforlist = P_KEY_PART_INFORRepositoryFactory.Repository().FindList().Where(s => s.MES_PLAN_KEY == PlanProductDataEntity.MES_PLAN_KEY && s.PRODUCT_BORN_CODE == PlanProductDataEntity.PRODUCT_BORN_CODE && s.PART_CODE == p_key_part_infor.PART_CODE && s.PART_BARCODE == barcode_choose).ToList();//找到多应安全件条码的安全件配置信息
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

                #region 更新线边库

                //if (!string.IsNullOrWhiteSpace(material_wc_part.material_wc_part_key))
                //{
                //dbop.Update<Material_WC_Part>(material_wc_part, "material_wc_part_key", tran);
                //}
                #endregion

                isOpenTrans.Commit();
                //更新完成之后
                //Task.Run(() =>
                //{
                //    if (material_wc_part.storage_num < material_wc_part.safety_num)//线边库库存量低于安全库存
                //    {
                //        AndonClickEvent();
                //        SetText(storage_quntity_lbl, material_wc_part.part_code + "\r\n" + material_wc_part.part_name + "库存不足！");
                //        SetColor(storage_quntity_lbl, Color.Red);
                //        SetVisible(storage_quntity_lbl, true);
                //        //MessageBox.Show("线边库库存数量已低于安全库存。", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        #region 触发物料andon

                //        if (click != null)
                //        {
                //            Button bsender = new Button();
                //            bsender.Name = material_wc_part.part_code + "|" + materialandonnum;
                //            EventArgs e = new EventArgs();
                //            click(bsender, e);
                //        }

                //        #endregion
                //    }
                //    else
                //    {
                //        SetText(storage_quntity_lbl, "");
                //        SetColor(storage_quntity_lbl, Color.Red);
                //        SetVisible(storage_quntity_lbl, false);
                //    }
                //});
                return true;
            }
            catch (Exception ex)
            {
                isOpenTrans.Rollback();
                throw ex;
            }
        }

        private void key_part_list_dgv_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            try
            {
                if (key_part_list_dgv.DataSource != null)
                {
                    if (key_part_list_dgv.Rows[e.RowIndex].Cells["RESERVE2"].Value != null)
                    {
                        key_part_list_dgv.Rows[e.RowIndex].Cells["PART_CODE"].Style.BackColor = Color.ForestGreen;
                        key_part_list_dgv.Rows[e.RowIndex].Cells["PART_NAME"].Style.BackColor = Color.ForestGreen;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败:" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region 刷新界面待采集安全件数据
        public void RefreshDgv()
        {
            try
            {
                List<P_KEY_PART_C> keypartlist = KEY_PART_CRepositoryFactory.Repository().FindList().Where(s => s.PRODUCT_CODE == PlanProductDataEntity.PRODUCT_CODE && s.MES_PLAN_CODE == PlanProductDataEntity.MES_PLAN_CODE).ToList();
                SetDgvDataSource(key_part_list_dgv, keypartlist);//使用委托为dgv赋值
                key_part_list_dgv.ClearSelection();
            }
            catch
            {

            }
        }
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
        //    public delegate void MessageBoxHandler();
        //public void aaa()
        //{
        //    this.Invoke(new MessageBoxHandler(delegate() { MessageBox.Show("千一网络"); }));// MessageBox.Show((IWin32Window)this, "提示"); // 由于放在 Invoke 中，也可以这么用，但效果和上面的一样。}))
        //}
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
                stationlist = StationRepositoryFactory.Repository().FindList();
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

                #region 返修上线工位
                dt_repaironline_stations.Clear();
                dt_repaironline_stations.Columns.Add("station_code");
                dt_repaironline_stations.Columns.Add("station_key");
                DataRow dr5 = dt_repaironline_stations.NewRow();
                dr5[0] = "==请选择==";
                dt_repaironline_stations.Rows.Add(dr5);
                P_PRODUCT_REPAIR_INFO repairentity = P_PRODUCT_REPAIR_INFORepositoryFactory.Repository().FindList().Where(s => s.PRODUCT_BORN_CODE == PlanProductDataEntity.PRODUCT_BORN_CODE && s.REPAIR_STATE == "返修下线").FirstOrDefault();//获取返修信息表中对应返修件的信息
                List<STATION> stations_choose = new List<STATION>();
                if (repairentity != null && repairentity.PRODUCT_REPAIR_INFO_KEY != null)
                {
                    if (repairentity.REPAIR_OFFLINE_STATION_CODE.Contains("4.0OP_A"))
                    {
                        stations_choose = stationlist.Where(s => s.STATION_CODE.Contains("4.0OP_A") && int.Parse(s.STATION_CODE.Substring(7, 4)) <= int.Parse(repairentity.REPAIR_OFFLINE_STATION_CODE.Substring(7, 4))).ToList();
                    }
                    else if(repairentity.REPAIR_OFFLINE_STATION_CODE.Contains("2.7OPA"))
                    {
                        stations_choose = stationlist.Where(s => s.STATION_CODE.Contains("2.7OPA") && int.Parse(s.STATION_CODE.Substring(7, 4)) <= int.Parse(repairentity.REPAIR_OFFLINE_STATION_CODE.Substring(7, 4))).ToList();
                    }
                }
                for (int i = 0; i < stations_choose.Count; i++)
                {
                    DataRow dr = dt_repaironline_stations.NewRow();
                    dr[0] = stations_choose[i].STATION_CODE;
                    dr[1] = stations_choose[i].STATION_KEY;
                    dt_repaironline_stations.Rows.Add(dr);
                }
                repair_online_station_cobox.DisplayMember = "station_code";
                repair_online_station_cobox.ValueMember = "station_key";
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
            if(dt_repaironline_stations.Rows.Count>1)
            SetComboBoxData(repair_online_station_cobox, dt_repaironline_stations);
        }
        #endregion
        private void Getdic_fault_type(List<PRODUCT_FAULT_TYPE> faulttypelist)//故障类型
        {
            dt_fault_type.Clear();
            dt_fault_type.Columns.Add("fault_type_name");
            dt_fault_type.Columns.Add("fault_type_key");
            for (int i = 0; i < faulttypelist.Count; i++)
            {
                DataRow dr = dt_fault_type.NewRow();
                dr[0] = faulttypelist[i].PRODUCT_FAULT_TYPE_NAME;
                dr[1] = faulttypelist[i].PRODUCT_FAULT_TYPE_KEY;
                dt_fault_type.Rows.Add(dr);
            }
            //foreach (PRODUCT_FAULT_TYPE item in faulttypelist)
            //{
            //    DataRow dr = dt_fault_type.NewRow();
            //    dr[0] = item.PRODUCT_FAULT_TYPE_NAME;
            //    dr[1] = item.PRODUCT_FAULT_TYPE_KEY;
            //    dt_fault_type.Rows.Add(dr);
            //}
            fault_type_cobox.DataSource = dt_fault_type;
            fault_type_cobox.DisplayMember = "fault_type_name";
            fault_type_cobox.ValueMember = "fault_type_key";
        }
        private void Getdic_fault_item()//故障信息
        {
            List<PRODUCT_FAULT_ITEM> addlist = new List<PRODUCT_FAULT_ITEM>();
            if (fault_type_cobox.Text == "")
            {
                addlist = faultlist;
            }
            else
            {
                addlist = faultlist.FindAll(s => s.PRODUCT_FAULT_TYPE_KEY == fault_type_cobox.ValueMember);
            }
            dt_fault_item.Clear();
            dt_fault_item.Columns.Add("fault_item_name");
            dt_fault_item.Columns.Add("fault_item_key");
            for (int i = 0; i < addlist.Count; i++)
            {
                DataRow dr = dt_fault_item.NewRow();
                dr[0] = addlist[i].PRODUCT_FAULT_ITEM_NAME;
                dr[1] = addlist[i].PRODUCT_FAULT_ITEM_KEY;
                dt_fault_item.Rows.Add(dr);
            }
            fault_item_cobox.DisplayMember = "fault_item_name";
            fault_item_cobox.ValueMember = "fault_item_key";
        }
        private void Getdic_maintain_type(List<PRODUCT_MAINTAIN_TYPE> maintaintypelist)//排故类型
        {
            dt_maintain_type.Clear();
            dt_maintain_type.Columns.Add("maintain_type_name");
            dt_maintain_type.Columns.Add("maintain_type_key");
            for (int i = 0; i < maintaintypelist.Count; i++)
            {
                DataRow dr = dt_maintain_type.NewRow();
                dr[0] = maintaintypelist[i].PRODUCT_MAINTAIN_TYPE_NAME;
                dr[1] = maintaintypelist[i].PRODUCT_MAINTAIN_TYPE_KEY;
                dt_maintain_type.Rows.Add(dr);
            }
            maintain_type_cobox.DataSource = dt_maintain_type;
            maintain_type_cobox.DisplayMember = "maintain_type_name";
            maintain_type_cobox.ValueMember = "maintain_type_key";
        }
        private void Getdic_maintain_item()//排故信息
        {
            List<PRODUCT_MAINTAIN_ITEM> addlist = new List<PRODUCT_MAINTAIN_ITEM>();
            if (maintain_type_cobox.Text == "")
            {
                addlist = maintainlist;
            }
            else
            {
                addlist = maintainlist.FindAll(s => s.PRODUCT_MAINTAIN_ITEM_KEY == maintain_type_cobox.ValueMember);
            }
            dt_maintain_item.Clear();
            dt_maintain_item.Columns.Add("maintain_item_name");
            dt_maintain_item.Columns.Add("maintain_item_key");
            for (int i = 0; i < addlist.Count; i++)
            {
                DataRow dr = dt_maintain_item.NewRow();
                dr[0] = addlist[i].PRODUCT_MAINTAIN_TYPE_NAME;
                dr[1] = addlist[i].PRODUCT_MAINTAIN_TYPE_KEY;
                dt_maintain_item.Rows.Add(dr);
            }
            //maintain_item_cobox.DataSource = dt_maintain_item;
            maintain_item_cobox.DisplayMember = "maintain_item_name";
            maintain_item_cobox.ValueMember = "maintain_item_name";
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
                    string infor = "请选择故障信\r\n息!";
                    SetLableText(prompt_information_lbl, infor);
                    return;
                }
                //if ( string.IsNullOrEmpty(maintain_type))
                //{
                //    MessageBox.Show("排故类型不能为空!");
                //}
                if (string.IsNullOrEmpty(maintain_info) || maintain_info == "==请选择==")
                {
                    string infor = "请选择排故措\r\n施!";
                    SetLableText(prompt_information_lbl, infor);
                    return;
                }
                PRODUCT_FA_MAIN_INFOR fa_main_info = new PRODUCT_FA_MAIN_INFOR();
                fa_main_info = PlanProductDataEntity.EntityMapper<P_ASSEMBLE_PRODUCT_STATE, PRODUCT_FA_MAIN_INFOR>();
                fa_main_info.STATION_KEY = BasicInfo.STATION_KEY;
                fa_main_info.STATION_CODE = BasicInfo.STATION_CODE;
                fa_main_info.STATION_NAME = BasicInfo.STATION_NAME;
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
                string info = "故障信息录入\r\n成功!";
                SetLableText(prompt_information_lbl, info);
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败" + ex.ToString());
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

        #region 选取返修上线工位触发事件，加载返修上线及返修下线区间工位
        private void repair_online_station_cobox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(repair_online_station_cobox.SelectedValue.ToString()))
            {
                P_PRODUCT_REPAIR_INFO repairentity = P_PRODUCT_REPAIR_INFORepositoryFactory.Repository().FindList().Where(s => s.PRODUCT_BORN_CODE == PlanProductDataEntity.PRODUCT_BORN_CODE && s.REPAIR_STATE == "返修下线").FirstOrDefault();//获取返修信息表中对应返修件的信息
                STATION online_staentity = stationlist.Where(s => s.STATION_KEY == repair_online_station_cobox.SelectedValue.ToString()).FirstOrDefault();
                List<STATION> Sectionstationlist = stationlist.Where(s => s.STATION_CODE.Contains("4.0OPA") && int.Parse(s.STATION_CODE.Substring(7, 4)) >= int.Parse(online_staentity.STATION_CODE.Substring(7, 4)) && int.Parse(s.STATION_CODE.Substring(7, 4)) <= int.Parse(repairentity.REPAIR_OFFLINE_STATION_CODE.Substring(7, 4))).ToList();
                DataTable dt_section_sta = new DataTable();
                dt_section_sta.Columns.Add("oper_station_key");
                dt_section_sta.Columns.Add("oper_station_code");
                for (int i = 0; i < Sectionstationlist.Count; i++)
                {
                    DataRow dr = dt_section_sta.NewRow();
                    dr[0] = Sectionstationlist[i].STATION_KEY;
                    dr[1] = Sectionstationlist[i].STATION_CODE;
                    dt_section_sta.Rows.Add(dr);
                }
                sectionstation_dgv.DataSource = dt_section_sta;
            }
        }
        #endregion

        #region 加载返修下线工位
        private void LoadRepairOffLineStation()
        {
            P_PRODUCT_REPAIR_INFO repairentity = P_PRODUCT_REPAIR_INFORepositoryFactory.Repository().FindList().Where(s => s.PRODUCT_BORN_CODE == PlanProductDataEntity.PRODUCT_BORN_CODE && s.REPAIR_STATE=="返修下线").FirstOrDefault();//获取返修信息表中对应返修件的信息
            if(repairentity!=null&& repairentity.PRODUCT_REPAIR_INFO_KEY!=null)
            SetLableText(repair_offline_station_lbl, repairentity.REPAIR_OFFLINE_STATION_CODE);
        }
        #endregion
    }
}
