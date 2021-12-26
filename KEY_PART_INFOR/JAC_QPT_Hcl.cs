using DevExpress.XtraCharts.Native;
using HfutIe;
using log4net;
using MsgBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO.Ports;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace KEY_PART_INFOR
{
    public partial class JAC_QPT_Hcl : Form
    {
        public JAC_QPT_Hcl()
        {
            InitializeComponent();
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                item.Height = 35;
            }
          
        }
        #region 公共变量
        static readonly object locker = new object();
        Boolean isEXception = false;
        string assemblytext = "==请选择==";
        public string PrintName = ReadXML.GetPrintName(System.Windows.Forms.Application.StartupPath + @"\Config1\Print.xml");
        string assembly_serial_no;//总成序列号
        string firstCode = "";
        string printAssembleCode = "";//生成二维码 序列号+物料号+供应商代码
        string printAssembleCode1 = "";//生成二维码 序列号+物料号+供应商代码
        string printAssembleCode2 = "";//
        string manualPrintAssembleCode = "";//生成二维码 序列号+物料号+供应商代码
        string manualAssembleCode1 = "";//序列号+物料号
        string manualprintAssembleCode2 = "";//
        string manualassembly_serial_no = "";
        #endregion
        #region 界面加载与关闭

        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void JAC_QPT_Hcl_Load(object sender, EventArgs e)
        {
            try
            {
                //ShowServerConnectionState();//服务器通信状态
                Initialization();
              
            }
            catch (Exception ex)
            {
                Log.GetInstance.WriteLog(ex.Message);
            }
        }
        public void Initialization()
        {
            try
            {

                serialPort.PortName = ScannerHelper.seriesport.PortName;
                serialPort.BaudRate = ScannerHelper.seriesport.BaudRate;
                serialPort.DataBits = ScannerHelper.seriesport.DataBits;
                serialPort.StopBits = (StopBits)Enum.Parse(typeof(StopBits), ScannerHelper.seriesport.StopBits);
                serialPort.Parity = (Parity)Enum.Parse(typeof(Parity), ScannerHelper.seriesport.Parity);
                serialPort.BaudRate = 9600;//无线扫描枪
                //ScannerSerialPort.BaudRate = 115200;//有线扫描枪
                serialPort.Open();

            }
            catch (Exception e)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                //log.Fatal(wc_code + "工位 扫描枪串口连接失败，请检查硬件连接！" + e);
                log.Fatal("工位扫描枪串口连接失败，请检查硬件连接！" + e);
                //MessageBox.Show(pro_line_name + "线 " + wc_code + " 工位扫描枪串口连接失败，请检查硬件连接！" + e.Message);
                MyMsgBox.Show(" 工位扫描枪串口连接失败，请检查硬件连接！", "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error, 5);
            }
        }
        private void Exit_pic_Box_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.Yes == MyMsgBox.Show("是否确定退出系统?", "信息提示", MyMsgBox.MyButtons.YesNo, MyMsgBox.MyIcon.Question))
                {
                    serialPort.Dispose();
                    Formclosing();
                  
                }

            }
            catch
            {

            }
        }
        public void Formclosing()//为了防止出现方法同名，此处用Formclosing命名，原定用FormClosing命名
        {
            try
            {
                ScannerHelper.CloseCom(serialPort);
            }
            finally
            {
                this.Dispose();
                this.Close();
                Environment.Exit(0);
            }
        }
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
                            //if (assemblytext != "==请选择==" && assemblytext != "")
                            //{
                                ScanportDataReceive(serialPort);
                            //}

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
                serialPort.DiscardInBuffer();//清理输入缓冲区
                serialPort.DiscardOutBuffer();//清理输出缓冲区
            }
        }

       
        int count_now = 0;
        int i = 1;
        int index=0;
        int j = 0;
        public void ScanportDataReceive(SerialPort ScannerSerialPort)
        {
            Stopwatch sw_all = new Stopwatch();
            sw_all.Start();

            string barcodetype = "";
            string barcode = "";
            bool IsOk = false;

            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            object obj = ScannerHelper.dataReceive(serialPort, out barcodetype, out IsOk);
            sw1.Stop();
            long ew1 = sw1.ElapsedMilliseconds;
            CheckForIllegalCrossThreadCalls = false;
            barcode =  ((obj.ToString()).Replace("\r\n", "")).Trim();
            // obj.ToString().Trim();
            if (!string.IsNullOrEmpty(barcode))//只有在显示为扫描输入模式下（Text == "手动输入"），扫描安全件条码才有效
            {
                
                if (barcodetype == "初始质保件总成码")
                {
                    index = dataGridView1.Rows.Add();
                    DataTable dt1 = new DataTable();
                    string serialcode = barcode.Substring(0, 8);
                    string drawno = barcode.Substring(14);
                    string sql1 = "select RatedQuantity from A_BS_ProductCfg where ProductCode like '%" + drawno + "%'";
                    dt1 = DbHelperSQL.OpenTable(sql1);
                    if (dt1.Rows.Count > 0)
                    {
                        //后处理总成号
                        string SNcode = serialcode + "L21102" + drawno;                       
                        dataGridView1.Rows[index].Cells[1].Value = SNcode.ToString();
                        if (dataGridView1.Rows[index].Cells[1].Value != null && dataGridView1.Rows[index].Cells[1].Value.ToString() != "")
                        {
                            dataGridView1.Rows[index].Cells[1].Style.BackColor = Color.LightGreen;
                        }
                        //额定采集数量
                        int ratedquantity = Convert.ToInt32(dt1.Rows[0][0]);
                        dataGridView1.Rows[index].Cells[2].Value = ratedquantity.ToString();
                        //当前采集数量
                        dataGridView1.Rows[index].Cells[3].Value = count_now.ToString();
                    }
                    else
                    {
                        MyMsgBox.Show("未找到配置信息，请先配置该总成件信息。", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information, 3);
                    }
                }
                else if (barcodetype == "追溯件码")
                {
                    dataGridView1.Rows[index].Cells[i+3].Value = barcode.ToString();
                    dataGridView1.Rows[index].Cells[i+3].Style.BackColor = Color.LightGreen;
                    count_now++;
                    dataGridView1.Rows[index].Cells[3].Value = count_now.ToString();
                    i++;
                    j++;
                }
                else if (barcodetype == "采集确认码")
                {                   
                    dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.LightGreen;
                    count_now = 0;
                    i = 1;
                    index++;
                    radioButton1.Checked = false;
                    radioButton1.Checked = true;
                    //插入mysql 打印记录表 pro_printrecord
                    printAssembleCode1 = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    string mysql1 = "insert into pro_printrecord (QPcode,PrintStatue,PrintNum) values ('" + printAssembleCode1 + "','0','0')";
                    DbHelperMySql.ExecuteMySql(mysql1);
                  
                    //插入mysql 总成件-追溯件采集表 pro_tracepartsdata
                    //printAssembleCode1 = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    for (int j1 = 0; j1 < j; j++)
                    {
                        string drawno = barcode.Substring(14);
                        string tracecode = dataGridView1.CurrentRow.Cells[j1 + 4].Value.ToString();
                        string mysql2 = "insert into pro_tracepartsdata (ProductCode,QPcode,TracePartsBarCode,CollectTime) values ('" + drawno + "','" + printAssembleCode1 + "','" + tracecode + "','" + DateTime.Now + "')";
                        DbHelperMySql.ExecuteMySql(mysql2);
                    }
                   

                }

                //int isok = P_HCL_SCAN_RECORDRepository.Repository().Insert(entity);
                //if (isok > 0)
                //{
                //    Log.GetInstance.WriteLog("扫描枪数据插入成功！" + barcode + "/" + ew1.ToString());
                //}
                //else
                //{
                //    Log.GetInstance.WriteLog("扫描枪数据插入失败！" + barcode + "/" + ew1.ToString());
                //}
            }
            sw_all.Stop();
            long ew_all = sw_all.ElapsedMilliseconds;
            Log.GetInstance.WriteLog(barcode + "扫码处理总时间：" + ew_all.ToString());
        }
        //public void allDataNtoC()
        //{
        //    List<P_HCL_SCAN_RECORD> RecordList = P_HCL_SCAN_RECORDRepository.Repository().FindList("FLAG", "N").OrderBy(t => t.CREATEDATE).ToList();
        //    foreach (P_HCL_SCAN_RECORD item in RecordList)
        //    {
        //        item.FLAG = "C";
        //        item.HANDLEDATE = DateTime.Now;
        //        item.RESERVE2 = "异常后的无效数据";
        //        P_HCL_SCAN_RECORDRepository.Repository().Update(item);
        //    }
        //}
        #endregion
        #region 显示服务器通讯信号

        private bool ServerCommunicationState = true;//服务器通讯状态
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
                        //ShowServerConnection(true);
                        //if (!listenEquipConnection.IsAlive)
                        //{
                        //    this.listenEquipConnection = new Thread(PLCToMES);
                        //    listenEquipConnection.Start();
                        //}
                    }
                    else
                    {
                        ServerCommunicationState = false;
                       // ShowServerConnection(false);
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
                    //ShowServerConnection(false);
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
        //public void ShowServerConnection(bool state)
        //{
        //    try
        //    {
        //        if (state)
        //        {
        //            //SetLblColors(serverconnectionstate_lbl, HasCollectBtnColor);
        //            serverconnectionstate_pulseButton.Set_ColorBottom(HasCollectBtnColor);
        //            serverconnectionstate_pulseButton.Set_ColorTop(HasCollectBtnColor);
        //            serverconnectionstate_pulseButton.Set_ForeColor(HasCollectBtnColor);
        //            serverconnectionstate_pulseButton.Set_PulseColor(HasCollectBtnColor);

        //        }
        //        else
        //        {
        //            //SetLblColors(serverconnectionstate_lbl, Color.Red);
        //            serverconnectionstate_pulseButton.Set_ColorBottom(Color.Red);
        //            serverconnectionstate_pulseButton.Set_ColorTop(Color.Red);
        //            serverconnectionstate_pulseButton.Set_ForeColor(Color.Red);
        //            serverconnectionstate_pulseButton.Set_PulseColor(Color.Red);
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}
        #endregion
        #region 正常打印
        private void print_button1_Click(object sender, EventArgs e)
        {
            if (collect_button.Text == "手动采集")//说明是自动采集
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if ((Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) == true))
                    {
                        printAssembleCode = dataGridView1.Rows[i].Cells[1].Value.ToString();
                        Myprinter(false);
                        //更新mysql 打印记录表 pro_printrecord
                        string mysql1 = "update pro_printrecord SET PrintStatue = '1',PrintTime = '" + DateTime.Now + "',PrintNum ='1', StaffCode = 'system',StaffName = 'system' where QPcode = '" + printAssembleCode + "' and PrintStatue ='0'";
                        DbHelperMySql.ExecuteMySql(mysql1);
                        //string mysql1 = "insert into pro_printrecord (QPcode,PrintTime,PrintStatue,PrintNum,StaffCode,StaffName) values ('" + printAssembleCode + "','" + DateTime.Now + "','1','1','System','System')";
                        //DbHelperMySql.ExecuteMySql(mysql1);
                        //打印完删去行
                        DataGridViewRow row = dataGridView1.Rows[i];
                        dataGridView1.Rows.Remove(row);
                        i--;
                    }
                    else
                        continue;
                }

            }
            else if (collect_button.Text == "自动采集")//说明是手动采集
            {
                for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                {
                    if ((Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) == true)&& dataGridView1.Rows[i].Cells[1].Value !=null)
                    {
                        printAssembleCode = dataGridView1.Rows[i].Cells[1].Value.ToString();
                        Myprinter(false);
                        //更新mysql 打印记录表 pro_printrecord
                        string mysql1 = "update pro_printrecord SET PrintStatue = '1',PrintTime = '" + DateTime.Now + "',PrintNum ='1', StaffCode = 'system',StaffName = 'system' where QPcode = '" + printAssembleCode + "' and PrintStatue ='0'";
                        DbHelperMySql.ExecuteMySql(mysql1);
                        ////插入mysql 打印记录表 pro_printrecord
                        //string mysql1 = "insert into pro_printrecord (QPcode,PrintTime,PrintStatue,PrintNum,StaffCode,StaffName) values ('" + printAssembleCode + "','" + DateTime.Now + "','1','1','System','System')";
                        //DbHelperMySql.ExecuteMySql(mysql1);

                        //打印完删去行
                        DataGridViewRow row = dataGridView1.Rows[i];
                        dataGridView1.Rows.Remove(row);
                        i--;
                    }
                    else
                        continue;
                }
               

            }
        }
        #endregion
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
                    Log.GetInstance.WriteLog("手动打印成功！" + manualPrintAssembleCode);//观察

                }
                else
                {
                    Log.GetInstance.WriteLog("打印成功！" + printAssembleCode);//观察
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
        //private void printDocument_PrintA4Page(object sender, PrintPageEventArgs e)
        //{
        //    Font f1 = new Font("黑体", 11, System.Drawing.FontStyle.Bold);
        //    Font f2 = new Font("黑体", 12, System.Drawing.FontStyle.Bold);

        //    // Font f2 = new Font("黑体", 18, System.Drawing.FontStyle.Bold);
        //    Font f3 = new Font("黑体", 9, System.Drawing.FontStyle.Bold);
        //    Font f4 = new Font("黑体", 10, System.Drawing.FontStyle.Bold);
        //    Font titleFont = new Font("黑体", 11, System.Drawing.FontStyle.Bold);//标题字体           
        //    Font fntTxt = new Font("宋体", 10, System.Drawing.FontStyle.Regular);//正文文字         
        //    Font fntTxt1 = new Font("宋体", 8, System.Drawing.FontStyle.Regular);//正文文字 
        //    Font fntTxt2 = new Font("宋体", 9, System.Drawing.FontStyle.Regular);//正文文字           
        //    Font fntTxt3 = new Font("宋体", 6, System.Drawing.FontStyle.Regular);//正文文字           

        //    System.Drawing.Brush brush = new SolidBrush(System.Drawing.Color.Black);//画刷           
        //    System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black);           //线条颜色         

        //    float x = 0F;
        //    float y = 0F;
        //    float xmax = 68 * 4;
        //    float ymax = 45 * 4;//预留下边距20
        //    float ymax2 = 50 * 4;

        //    //绘图 表格
        //    //float leftbianJu = 8;
        //    //float topbianJu = 30;
        //    try
        //    {
        //        e.Graphics.DrawString("JAC江淮汽车 后处理质保件总成码", f2, brush, new System.Drawing.Point(7, 7));
        //        e.Graphics.DrawString("JAC江淮汽车 后处理质保件总成码", f2, brush, new System.Drawing.Point(300, 7));
        //        Bitmap bitmap1 = CreateQRCode(printAssembleCode, 100, 100, true); //50，50//总成码
        //        e.Graphics.DrawImage(bitmap1, new System.Drawing.Point(3, 22));//条码位置
        //        e.Graphics.DrawImage(bitmap1, new System.Drawing.Point(300, 25));//条码位置
        //        string ECM = "NO. " + printAssembleCode;
        //        e.Graphics.DrawString(ECM, fntTxt2, brush, new System.Drawing.Point(105, 28));//57,30
        //        e.Graphics.DrawString(ECM, fntTxt2, brush, new System.Drawing.Point(405, 28));//57,30
        //        //e.Graphics.DrawString(printAssembleCode2, fntTxt2, brush, new System.Drawing.Point(57, 42));//57,43
        //        //string str = "";
        //        //int i1 = firstCode.Length;
        //        //if (firstCode.Length > 26)
        //        //{
        //        //    str = firstCode.Substring(0, 26) + "\n" + firstCode.Substring(26, i1 - 26);
        //        //}
        //        //else
        //        //{
        //        //    str = firstCode;
        //        //}
        //        //e.Graphics.DrawString(str, fntTxt3, brush, new System.Drawing.Point(57, 53));//57,56
        //        //e.Graphics.DrawString(assembly_serial_no, fntTxt2, brush, new System.Drawing.Point(248, 40));

        //    }
        //    catch (Exception ee)
        //    {
        //        MessageBox.Show(ee.Message);
        //    }
        //}
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
                e.Graphics.DrawString("JAC江淮汽车 原装部件   质保件采集", f2, brush, new System.Drawing.Point(7, 7));
                Bitmap bitmap1 = CreateQRCode(printAssembleCode, 49, 49, true); //50，50
                e.Graphics.DrawImage(bitmap1, new System.Drawing.Point(3, 22));//条码位置
                e.Graphics.DrawImage(bitmap1, new System.Drawing.Point(197, 22));//条码位置
                string ECM = "NO. " + printAssembleCode.Substring(0, 14);
                string printAssembleCode3 = printAssembleCode.Substring(14);
                e.Graphics.DrawString(ECM, fntTxt2, brush, new System.Drawing.Point(57, 35));//57,30
                e.Graphics.DrawString(printAssembleCode3, fntTxt2, brush, new System.Drawing.Point(57, 49));//57,43
                string str = "NO. " + printAssembleCode.Substring(0, 8);

                //int i1 = firstCode.Length;
                //if (firstCode.Length > 26)
                //{
                //    str = firstCode.Substring(0, 26) + "\n" + firstCode.Substring(26, i1 - 26);
                //}
                //else
                //{
                //    str = firstCode;
                //}
                //e.Graphics.DrawString(str, fntTxt3, brush, new System.Drawing.Point(57, 53));//57,56
                e.Graphics.DrawString(str, fntTxt2, brush, new System.Drawing.Point(248, 35));
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
                e.Graphics.DrawString("JAC江淮汽车 原装部件   质保件采集", f2, brush, new System.Drawing.Point(7, 7));
                Bitmap bitmap1 = CreateQRCode(manualPrintAssembleCode, 49, 49, true); //50，50
                e.Graphics.DrawImage(bitmap1, new System.Drawing.Point(3, 22));//条码位置
                e.Graphics.DrawImage(bitmap1, new System.Drawing.Point(197, 25));//条码位置
                string ECM = "NO. " + manualAssembleCode1;
                e.Graphics.DrawString(ECM, fntTxt2, brush, new System.Drawing.Point(57, 28));//57,30
                e.Graphics.DrawString(manualprintAssembleCode2, fntTxt2, brush, new System.Drawing.Point(57, 42));//57,43
                string str = "NO. " + manualPrintAssembleCode.Substring(0,8);

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
        //private void printDocument_PrintA4Page1_Manual(object sender, PrintPageEventArgs e)
        //{
        //    Font f1 = new Font("黑体", 11, System.Drawing.FontStyle.Bold);
        //    Font f2 = new Font("黑体", 12, System.Drawing.FontStyle.Bold);

        //    // Font f2 = new Font("黑体", 18, System.Drawing.FontStyle.Bold);
        //    Font f3 = new Font("黑体", 9, System.Drawing.FontStyle.Bold);
        //    Font f4 = new Font("黑体", 10, System.Drawing.FontStyle.Bold);
        //    Font titleFont = new Font("黑体", 11, System.Drawing.FontStyle.Bold);//标题字体           
        //    Font fntTxt = new Font("宋体", 10, System.Drawing.FontStyle.Regular);//正文文字         
        //    Font fntTxt1 = new Font("宋体", 8, System.Drawing.FontStyle.Regular);//正文文字 
        //    Font fntTxt2 = new Font("宋体", 9, System.Drawing.FontStyle.Regular);//正文文字           
        //    Font fntTxt3 = new Font("宋体", 6, System.Drawing.FontStyle.Regular);//正文文字           

        //    System.Drawing.Brush brush = new SolidBrush(System.Drawing.Color.Black);//画刷           
        //    System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black);           //线条颜色         

        //    float x = 0F;
        //    float y = 0F;
        //    float xmax = 68 * 4;
        //    float ymax = 45 * 4;//预留下边距20
        //    float ymax2 = 50 * 4;

        //    //绘图 表格
        //    float leftbianJu = 8;
        //    float topbianJu = 30;
        //    try
        //    {
        //        e.Graphics.DrawString("JAC江淮汽车 后处理质保件总成码", f2, brush, new System.Drawing.Point(7, 7));
        //        Bitmap bitmap1 = CreateQRCode(manualPrintAssembleCode, 49, 49, true); //50，50
        //        e.Graphics.DrawImage(bitmap1, new System.Drawing.Point(3, 22));//条码位置
        //        e.Graphics.DrawImage(bitmap1, new System.Drawing.Point(197, 25));//条码位置
        //        //string ECM = "NO. " + manualAssembleCode1;
        //        //e.Graphics.DrawString(ECM, fntTxt2, brush, new System.Drawing.Point(57, 28));//57,30
        //        //e.Graphics.DrawString(manualprintAssembleCode2, fntTxt2, brush, new System.Drawing.Point(57, 42));//57,43
        //        //string str = "";

        //        //int i1 = firstCode.Length;
        //        //if (firstCode.Length > 26)
        //        //{
        //        //    str = firstCode.Substring(0, 26) + "\n" + firstCode.Substring(26, i1 - 26);
        //        //}
        //        //else
        //        //{
        //        //    str = firstCode;
        //        //}
        //        //e.Graphics.DrawString(str, fntTxt3, brush, new System.Drawing.Point(57, 53));//57,56
        //        //e.Graphics.DrawString(manualassembly_serial_no, fntTxt2, brush, new System.Drawing.Point(248, 40));
        //        #region 4.0
        //        #endregion
        //        #region 2.7
        //        //Bitmap bitmap = CreateQRCode(ESN, 2, 50,false);
        //        //e.Graphics.DrawImage(bitmap, new System.Drawing.Point(15, 15));//条码位置
        //        #endregion
        //    }
        //    catch (Exception ee)
        //    {
        //        MessageBox.Show(ee.Message);
        //    }
        //}

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
        #region 全选、多选
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) == false))
                {
                    dataGridView1.Rows[i].Cells[0].Value = true;
                }
                else
                    continue;
            }
            radioButton3.Checked = false;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if ((Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value) == true))
                {
                    dataGridView1.Rows[i].Cells[0].Value = false;
                    dataGridView1.Rows[i].Cells[0].ReadOnly = true;
                }
                else
                    continue;
            }
            
            radioButton1.Checked = false;
            
        }
       
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //复选框
            if (Convert.ToBoolean(dataGridView1.CurrentRow.Cells[0].Value) == true)
            {
                dataGridView1.CurrentRow.Cells[0].Value = false;
            }
            else if (Convert.ToBoolean(dataGridView1.CurrentRow.Cells[0].Value) == false)
            {
                dataGridView1.CurrentRow.Cells[0].Value = true;
            }


        }
        #endregion
        #region 手动打印界面
        private void print_button2_Click(object sender, EventArgs e)
        {
            JAC_QPT_HCL_M form1 = new JAC_QPT_HCL_M();
            form1.Show();
        }
        #endregion
        #region 手/自动采集切换
        private void collect_button_Click(object sender, EventArgs e)
        {
            
            if (collect_button.Text == "手动采集")
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    dataGridView1.Rows.Add();
                }
                collect_button.Text = "自动采集";
                dataGridView1.ReadOnly = false;
                dataGridView1.Columns["Column3"].ReadOnly = true;
                dataGridView1.Columns["Column4"].ReadOnly = true;
                button_ok.Visible = true;
                button_ok.Enabled = true;
            }
            else if (collect_button.Text == "自动采集")
            {
                collect_button.Text = "手动采集";
                dataGridView1.ReadOnly = true;
                button_ok.Visible = false;
                button_ok.Enabled = false;
                dataGridView1.Rows.Clear();
            }
        }
        #endregion
        #region 手动输入，dv单元格改变时变色
        private void dataGridView1_CellStyleContentChanged(object sender, DataGridViewCellStyleContentChangedEventArgs e)
        {
           
        }
        

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex != -1)
            {
                if (collect_button.Text == "自动采集")//说明当前模式为手动采集
                {
                    string qpcode = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    string code = qpcode.Substring(8,6);//供应商代码
                    string code1 = qpcode.Substring(0, 8);//批次号
                    string code2 = qpcode.Substring(14);//图号、总成件编码
                    //变色
                    int indexNum = dataGridView1.SelectedCells[0].ColumnIndex;
                    string ColumnText = dataGridView1.Columns[indexNum].HeaderText.ToString();
                    if (ColumnText != "质保总成件编码" && ColumnText != "额定采集数量" && ColumnText != "当前采集数量")
                    {
                        dataGridView1.CurrentCell.Style.BackColor = Color.LightGreen;
                    }
                    else if (ColumnText == "质保总成件编码")
                    {
                        //string qpcode = dataGridView1.CurrentCell.Value.ToString();
                        //string code = qpcode.Substring(8, 13);
                        //string code1 = qpcode.Substring(0, 7);
                        string sql = "select * from A_BS_ProductCfg where ProductCode ='" + code2 + "'";
                        DataTable dt = DbHelperSQL.OpenTable(sql);
                        if (code == "L21102" && dt.Rows.Count > 0)
                        {
                            dataGridView1.CurrentCell.Style.BackColor = Color.LightGreen;
                            //额定采集数量
                            string sql1 = "select RatedQuantity from A_BS_ProductCfg where ProductCode ='" + code2 + "'";
                            DataTable dt1 = DbHelperSQL.OpenTable(sql1);
                            if (dt1.Rows.Count > 0)
                            {
                                int ratedquantity = Convert.ToInt32(dt1.Rows[0][0]);
                                dataGridView1.CurrentRow.Cells[2].Value = ratedquantity;
                            }
                            else
                            {
                                MyMsgBox.Show("未找到配置信息，请先配置该总成件信息。", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information, 3);
                            }
                        }
                        else
                        {
                            dataGridView1.CurrentCell.Style.BackColor = Color.Red;
                        }
                    }
                    
                    ////额定采集数量
                    //string sql1 = "select RatedQuantity from A_BS_ProductCfg where ProductCode ='" + code2 + "'";
                    //DataTable dt1 = DbHelperSQL.OpenTable(sql1);
                    //if (dt1.Rows.Count > 0)
                    //{
                    //    int ratedquantity = Convert.ToInt32(dt1.Rows[0][0]);
                    //    dataGridView1.CurrentRow.Cells[2].Value = ratedquantity;
                    //}
                    //else
                    //{
                    //    MyMsgBox.Show("未找到配置信息，请先配置该总成件信息。", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information, 3);
                    //}
                    //当前采集数量
                    int collectcount = 0;
                    for (int i = 4; i < dataGridView1.ColumnCount; i++)
                    {
                        if (dataGridView1.CurrentRow.Cells[i].Value !=null && !dataGridView1.CurrentRow.Cells[i].Value.Equals("") && !dataGridView1.CurrentRow.Cells[i].Value.Equals("\r\n"))
                        {
                            collectcount++;
                        }
                    }
                    dataGridView1.CurrentRow.Cells[3].Value = collectcount;
                }
            }
        }
        #endregion
        #region 手动采集确认
        private void button_ok_Click(object sender, EventArgs e)
        {
            //插入mysql 打印记录表 pro_printrecord
            printAssembleCode2 = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            string mysql1 = "insert into pro_printrecord (QPcode,PrintStatue,PrintNum) values ('" + printAssembleCode2 + "','0','0')";
            DbHelperMySql.ExecuteMySql(mysql1);
            //插入mysql 总成件-追溯件采集表 pro_tracepartsdata
            for (int j1 = 0; j1 < 15; j1++)
            {
                //string value = dataGridView1.CurrentRow.Cells[j1 + 4].Value.ToString();
                if (dataGridView1.CurrentRow.Cells[j1 + 4].Value != null )
                { 
                    string drawno = printAssembleCode2.Substring(14);
                    string tracecode = dataGridView1.CurrentRow.Cells[j1 + 4].Value.ToString();
                    string mysql2 = "insert into pro_tracepartsdata (ProductCode,QPcode,TracePartsBarCode,CollectTime) values ('" + drawno + "','" + printAssembleCode2 + "','" + tracecode + "','" + DateTime.Now + "')";
                    DbHelperMySql.ExecuteMySql(mysql2);
                }
            }
            dataGridView1.CurrentRow.DefaultCellStyle.BackColor = Color.LightGreen;
            radioButton1.Checked = false;
            radioButton1.Checked = true;
            dataGridView1.Rows.Add();
        }
        #endregion

      
    }
}
