using HfutIe;
using MsgBox;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace KEY_PART_INFOR
{
    public partial class JAC_QPT_HCL_M : Form
    {
        #region 公共变量
        string test = "";
        string printAssembleCode = "";
        string manualPrintAssembleCode = "";
        public string PrintName = ReadXML.GetPrintName(System.Windows.Forms.Application.StartupPath + @"\Config1\Print.xml");
        #endregion
        public JAC_QPT_HCL_M()
        {
            InitializeComponent();
            
        }
        #region 界面加载与关闭，初始化dv
        private void JAC_QPT_HCL_M_Load(object sender, EventArgs e)
        {
            InitDataTable();
            //dateTimePicker1.Value = Convert.ToDateTime("2021-10-01");
            //dateTimePicker2.Value = Convert.ToDateTime("9998-10-01");
            dateTimePicker1.Value = dateTimePicker2.MinDate;
            dateTimePicker2.Value = dateTimePicker2.MaxDate;
            radioButton_all.Checked = true;
            radioButton_ok.Checked = false;
            radioButton_no.Checked = false;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            try
            {                                 
                    Formclosing1();               
            }
            catch
            {

            }
        }
        public void Formclosing1()//为了防止出现方法同名，此处用Formclosing命名，原定用FormClosing命名
        {
            try
            {
                //ScannerHelper.CloseCom(serialPort);
            }
            finally
            {
                this.Dispose();
                this.Close();
                //Environment.Exit(0);
            }
        }

        private DataTable dt = new DataTable();

        BindingSource bs = new BindingSource();



        /// <summary>
        /// 初始化DataTable
        /// </summary>
        public void InitDataTable()
        {
            string mysql1 = "select QPcode,PrintTime, ( case PrintStatue when '1' then '自动打印'  when '0' then '未打印' when '2' then '手动打印' END) PrintStatue ,PrintNum,StaffCode,StaffName from pro_printrecord LIMIT 300";
            dt= DbHelperMySql.OpenTable(mysql1);
            //不允许自动生成，若改为允许，界面会自动增加DataTable列，那么界面上既会包含DataGridView中定义的列，也会包含DataTable定义的列
            this.dataGridView2.AutoGenerateColumns = false;          

            bs.DataSource = dt;
            this.dataGridView2.DataSource = bs;

            //将DataGridView中的列与DataTable中的列进行数据绑定，this.cloNum为列名

            this.Column2.DataPropertyName = "QPcode";
            this.Column3.DataPropertyName = "PrintTime";
            this.Column4.DataPropertyName = "PrintStatue";
            this.Column5.DataPropertyName = "PrintNum";
            this.Column6.DataPropertyName = "StaffCode";
            this.Column7.DataPropertyName = "StaffName";
        }
        #endregion
        #region dataGridView2 checkbox
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToBoolean(dataGridView2.CurrentRow.Cells[0].Value) == true)
            {
                dataGridView2.CurrentRow.Cells[0].Value = false;
            }
            else if (Convert.ToBoolean(dataGridView2.CurrentRow.Cells[0].Value) == false)
            {
                dataGridView2.CurrentRow.Cells[0].Value = true;
            }
        }
        #endregion
        #region 手动打印
        private void button_print_m_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if ((Convert.ToBoolean(dataGridView2.Rows[i].Cells[0].Value) == true))
                {
                    printAssembleCode = dataGridView2.Rows[i].Cells[1].Value.ToString();
                    manualPrintAssembleCode = dataGridView2.Rows[i].Cells[1].Value.ToString();
                    Myprinter(true);
                    //插入mysql 打印记录表 pro_printrecord
                    string mysql1 = "update pro_printrecord SET PrintStatue = '2',PrintTime = '" + DateTime.Now + "' where QPcode = '"+ printAssembleCode + "'";
                    DbHelperMySql.ExecuteMySql(mysql1);
                    ////打印完删去行
                    //DataGridViewRow row = dataGridView2.Rows[i];
                    //dataGridView2.Rows.Remove(row);
                    //i--;
                }
                else
                    continue;
            }
            InitDataTable();
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
            //float leftbianJu = 8;
            //float topbianJu = 30;
            try
            {
                e.Graphics.DrawString("JAC江淮汽车 后处理质保件总成码", f2, brush, new System.Drawing.Point(7, 7));
                e.Graphics.DrawString("JAC江淮汽车 后处理质保件总成码", f2, brush, new System.Drawing.Point(300, 7));
                Bitmap bitmap1 = CreateQRCode(printAssembleCode, 100, 100, true); //50，50//总成码
                e.Graphics.DrawImage(bitmap1, new System.Drawing.Point(3, 22));//条码位置
                e.Graphics.DrawImage(bitmap1, new System.Drawing.Point(300, 25));//条码位置
                string ECM = "NO. " + printAssembleCode;
                e.Graphics.DrawString(ECM, fntTxt2, brush, new System.Drawing.Point(105, 28));//57,30
                e.Graphics.DrawString(ECM, fntTxt2, brush, new System.Drawing.Point(405, 28));//57,30
                //e.Graphics.DrawString(printAssembleCode2, fntTxt2, brush, new System.Drawing.Point(57, 42));//57,43
                //string str = "";
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
                //e.Graphics.DrawString(assembly_serial_no, fntTxt2, brush, new System.Drawing.Point(248, 40));

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
                Bitmap bitmap1 = CreateQRCode(printAssembleCode, 49, 49, true); //50，50
                e.Graphics.DrawImage(bitmap1, new System.Drawing.Point(3, 22));//条码位置
                e.Graphics.DrawImage(bitmap1, new System.Drawing.Point(197, 22));//条码位置
                string ECM = "NO. " + printAssembleCode.Substring(0,14);
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
      e.Substring(26, i1 - 26);
        //        //}  //private void printDocument_PrintA4Page1_Manual(object sender, PrintPageEventArgs e)
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
        //        //    str = firstCode.Substring(0, 26) + "\n" + firstCod
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
        #region 查询
        int m1 = 0;
        int m2 = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            string code = textBox_code.Text.ToString().Trim();
            string time1 = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string time2 = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            if (radioButton_all.Checked == true && radioButton_ok.Checked == false && radioButton_no.Checked == false)//全选
            {
                if (code != "" && time1 != dateTimePicker1.MinDate.ToString("yyyy-MM-dd") && time2 != dateTimePicker2.MaxDate.ToString("yyyy-MM-dd"))
                {
                    string time11 = time1;
                    string time22 = Convert.ToDateTime(time2).AddDays(1).ToString("yyyy-MM-dd");
                    string mysql1 = "select QPcode,PrintTime, ( case PrintStatue when '1' then '自动打印'  when '0' then '未打印' when '2' then '手动打印' END) PrintStatue ,PrintNum,StaffCode,StaffName from pro_printrecord where QPcode like '%" + code + "%' and PrintTime between '" + time11 + "' and '" + time22 + "'";
                    DataTable dt1 = DbHelperMySql.OpenTable(mysql1);
                    bs.DataSource = dt1;
                    dataGridView2.DataSource = bs;
                }
                else if (code == "" && time1 != dateTimePicker1.MinDate.ToString("yyyy-MM-dd") && time2 != dateTimePicker2.MaxDate.ToString("yyyy-MM-dd"))
                {
                    string time11 = time1;
                    string time22 = Convert.ToDateTime(time2).AddDays(1).ToString("yyyy-MM-dd");
                    string mysql2 = "select QPcode,PrintTime, ( case PrintStatue when '1' then '自动打印'  when '0' then '未打印' when '2' then '手动打印' END) PrintStatue ,PrintNum,StaffCode,StaffName from pro_printrecord where PrintTime between '" + time11 + "' and '" + time22 + "'";
                    DataTable dt2 = DbHelperMySql.OpenTable(mysql2);
                    bs.DataSource = dt2;
                    dataGridView2.DataSource = bs;
                }
                else if (code != "" && m1 == 0 && m2 == 0)//说明只输入code，没输入时间
                {
                    //string time11 = time1;
                    //string time22 = Convert.ToDateTime(time2).AddDays(1).ToString("yyyy-MM-dd");
                    string mysql3 = "select QPcode,PrintTime, ( case PrintStatue when '1' then '自动打印'  when '0' then '未打印' when '2' then '手动打印' END) PrintStatue ,PrintNum,StaffCode,StaffName from pro_printrecord where  QPcode like '%" + code + "%'";
                    DataTable dt3 = DbHelperMySql.OpenTable(mysql3);
                    bs.DataSource = dt3;
                    dataGridView2.DataSource = bs;
                }
            }
            else if (radioButton_all.Checked == false && radioButton_ok.Checked == true && radioButton_no.Checked == false)//已打印
            {
                if (code != "" && time1 != dateTimePicker1.MinDate.ToString("yyyy-MM-dd") && time2 != dateTimePicker2.MaxDate.ToString("yyyy-MM-dd"))
                {
                    string time11 = time1;
                    string time22 = Convert.ToDateTime(time2).AddDays(1).ToString("yyyy-MM-dd");
                    string mysql1 = "select QPcode,PrintTime, ( case PrintStatue when '1' then '自动打印'  when '0' then '未打印' when '2' then '手动打印' END) PrintStatue ,PrintNum,StaffCode,StaffName from pro_printrecord where QPcode like '%" + code + "%' and PrintStatue != '0' and PrintTime between '" + time11 + "' and '" + time22 + "'";
                    DataTable dt1 = DbHelperMySql.OpenTable(mysql1);
                    bs.DataSource = dt1;
                    dataGridView2.DataSource = bs;
                }
                else if (code == "" && time1 != dateTimePicker1.MinDate.ToString("yyyy-MM-dd") && time2 != dateTimePicker2.MaxDate.ToString("yyyy-MM-dd"))
                {
                    string time11 = time1;
                    string time22 = Convert.ToDateTime(time2).AddDays(1).ToString("yyyy-MM-dd");
                    string mysql2 = "select QPcode,PrintTime, ( case PrintStatue when '1' then '自动打印'  when '0' then '未打印' when '2' then '手动打印' END) PrintStatue ,PrintNum,StaffCode,StaffName from pro_printrecord where PrintTime between '" + time11 + "' and '" + time22 + "'and PrintStatue != '0'";
                    DataTable dt2 = DbHelperMySql.OpenTable(mysql2);
                    bs.DataSource = dt2;
                    dataGridView2.DataSource = bs;
                }
                else if (code != "" && m1 == 0 && m2 == 0)//说明只输入code，没输入时间
                {
                    //string time11 = time1;
                    //string time22 = Convert.ToDateTime(time2).AddDays(1).ToString("yyyy-MM-dd");
                    string mysql3 = "select QPcode,PrintTime, ( case PrintStatue when '1' then '自动打印'  when '0' then '未打印' when '2' then '手动打印' END) PrintStatue ,PrintNum,StaffCode,StaffName from pro_printrecord where  QPcode like '%" + code + "%' and PrintStatue != '0'";
                    DataTable dt3 = DbHelperMySql.OpenTable(mysql3);
                    bs.DataSource = dt3;
                    dataGridView2.DataSource = bs;
                }
            }
            else if (radioButton_all.Checked == false && radioButton_ok.Checked == false && radioButton_no.Checked == true)//未打印
            {
                if (code != "" && time1 != dateTimePicker1.MinDate.ToString("yyyy-MM-dd") && time2 != dateTimePicker2.MaxDate.ToString("yyyy-MM-dd"))
                {
                    string time11 = time1;
                    string time22 = Convert.ToDateTime(time2).AddDays(1).ToString("yyyy-MM-dd");
                    string mysql1 = "select QPcode,PrintTime, ( case PrintStatue when '1' then '自动打印'  when '0' then '未打印' when '2' then '手动打印' END) PrintStatue ,PrintNum,StaffCode,StaffName from pro_printrecord where QPcode like '%" + code + "%' and PrintStatue = '0' and PrintTime between '" + time11 + "' and '" + time22 + "'";
                    DataTable dt1 = DbHelperMySql.OpenTable(mysql1);
                    bs.DataSource = dt1;
                    dataGridView2.DataSource = bs;
                }
                else if (code == "" && time1 != dateTimePicker1.MinDate.ToString("yyyy-MM-dd") && time2 != dateTimePicker2.MaxDate.ToString("yyyy-MM-dd"))
                {
                    string time11 = time1;
                    string time22 = Convert.ToDateTime(time2).AddDays(1).ToString("yyyy-MM-dd");
                    string mysql2 = "select QPcode,PrintTime, ( case PrintStatue when '1' then '自动打印'  when '0' then '未打印' when '2' then '手动打印' END) PrintStatue ,PrintNum,StaffCode,StaffName from pro_printrecord where PrintTime between '" + time11 + "' and '" + time22 + "'and PrintStatue = '0'";
                    DataTable dt2 = DbHelperMySql.OpenTable(mysql2);
                    bs.DataSource = dt2;
                    dataGridView2.DataSource = bs;
                }
                else if (code != "" && m1 == 0 && m2 == 0)//说明只输入code，没输入时间
                {
                    //string time11 = time1;
                    //string time22 = Convert.ToDateTime(time2).AddDays(1).ToString("yyyy-MM-dd");
                    string mysql3 = "select QPcode,PrintTime, ( case PrintStatue when '1' then '自动打印'  when '0' then '未打印' when '2' then '手动打印' END) PrintStatue ,PrintNum,StaffCode,StaffName from pro_printrecord where  QPcode like '%" + code + "%' and PrintStatue = '0'";
                    DataTable dt3 = DbHelperMySql.OpenTable(mysql3);
                    bs.DataSource = dt3;
                    dataGridView2.DataSource = bs;
                }
            }
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if(dateTimePicker1.Value.ToString("yyyy-MM-dd")!= dateTimePicker1.MinDate.ToString("yyyy-MM-dd"))
            {

                this.dateTimePicker1.Format = DateTimePickerFormat.Long;

                this.dateTimePicker1.CustomFormat = null;
            }

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value.ToString("yyyy-MM-dd") != dateTimePicker2.MaxDate.ToString("yyyy-MM-dd"))
            {

                this.dateTimePicker2.Format = DateTimePickerFormat.Long;

                this.dateTimePicker2.CustomFormat = null;
            }
        }

        private void dateTimePicker1_DropDown(object sender, EventArgs e)
        {
            m1 = 1;
            //dateTimePicker1.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        }

        private void dateTimePicker2_DropDown(object sender, EventArgs e)
        {
            m2 = 1;
            //dateTimePicker2.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        }

       

        private void dateTimePicker1_MouseEnter(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.ToString("yyyy-MM-dd") == dateTimePicker1.MinDate.ToString("yyyy-MM-dd"))
            {
                dateTimePicker1.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());

            }
        }

        private void dateTimePicker2_MouseEnter(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value.ToString("yyyy-MM-dd") == dateTimePicker1.MaxDate.ToString("yyyy-MM-dd"))
            {
                dateTimePicker2.Value = Convert.ToDateTime(DateTime.Now.ToShortDateString());

            }
        }

        private void radioButton_all_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_all.Checked == true)
            {
                radioButton_ok.Checked = false;
                radioButton_no.Checked = false;
            }
        }

        private void radioButton_ok_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_ok.Checked == true)
            {
                radioButton_all.Checked = false;
                radioButton_no.Checked = false;
            }
        }

        private void radioButton_no_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_no.Checked == true)
            {
                radioButton_ok.Checked = false;
                radioButton_all.Checked = false;
            }
        }
        #endregion
    }
}
