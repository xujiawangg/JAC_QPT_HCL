using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Xml;
using System.Collections;
using System.Net;
using log4net;
using System.Xml.Linq;
using HfutIe.DataAccess.Common;
using HfutIE.Repository;
using HfutIE.Entity;
using System.Net.NetworkInformation;
using HfutIE.DataAccess;
using MsgBox;

namespace HfutIe
{
    public partial class LoginForm : Form
    {

        #region 仓储
        RepositoryFactory<Base_DataDictionary> Base_DataRepository = new RepositoryFactory<Base_DataDictionary>();
        RepositoryFactory<Base_DataDictionaryDetail> Base_DataDetailRepository = new RepositoryFactory<Base_DataDictionaryDetail>();
        RepositoryFactory<BASE_USER> BASE_USERRepositoryFactory = new RepositoryFactory<BASE_USER>();//人员基本表
        RepositoryFactory<A_BS_StaffInfo> a_BS_StaffInfoRepositoryFactory = new RepositoryFactory<A_BS_StaffInfo>();//人员基本表 JAC_QPT
        #endregion

        #region 属性
        public int tt;//1表示可以操作，２表示浏览，３表示既没操作也没浏览
        public string mancode = "";//获取登陆人员编号
        private const string TXTPATH = @"\Login\";
        private const string TXTPOSTFIX = ".txt";
        private const string TXTPATH2 = @"\Update\";
        private string StationKey;
        DateTime edition;//版本号
        //DecryptEncrypt DeEn = new DecryptEncrypt();

        //动画窗体调用,关闭时将向上移出屏幕
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        const int AW_HOR_POSITIVE = 0x0001;
        const int AW_HOR_NEGATIVE = 0x0002;
        const int AW_VER_POSITIVE = 0x0004;
        const int AW_VER_NEGATIVE = 0x0008;
        const int AW_CENTER = 0x0010;
        const int AW_HIDE = 0x10000;
        const int AW_ACTIVATE = 0x20000;
        const int AW_SLIDE = 0x40000;
        const int AW_BLEND = 0x80000;
        public long time;
        //…………………………………………………………
        #endregion

        #region 事件
        //public LoginForm(string wcCode)
        //{
        //    StationKey = wcCode;
        //    InitializeComponent();
        //    ShowServerConnectionState();//服务器通讯状态
        //}
        public LoginForm()
        {
           
            InitializeComponent();
            ShowServerConnectionState();//服务器通讯状态
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            AnimateWindow(this.Handle, 500, AW_CENTER | AW_ACTIVATE);
            //主界面渐变设置
            this.jianbian.Enabled = true;//让jianbian的timer值有效
            this.Opacity = 0;
            //………………………………………………

            string str = Application.StartupPath;
            string filepath = str + TXTPATH + "login" + TXTPOSTFIX;
            if (System.IO.File.Exists(filepath))
            {
                string content = File.ReadAllText(filepath);
                Nameid.Text = content;
            }
            string datasorucePath = str + "\\config.xml";
            string datasorucePath1 = str + "\\config2.xml";//记录登录名
            if (System.IO.File.Exists(datasorucePath1))
            {
                XmlReader rdr1 = XmlReader.Create(datasorucePath1);
                ArrayList al1 = new ArrayList();
                while (rdr1.Read())
                {
                    if (rdr1.Value != "")
                    {
                        al1.Add(rdr1.Value.ToString());
                    }
                }
                rdr1.Close();
                Nameid.Text = al1[3].ToString();//用户名
                if (al1.Count > 5)
                {
                    //Password.Text = al1[5].ToString();//密码
                    Password.Text = DESEncrypt.Decrypt(al1[5].ToString());//解密读取
                }


            }
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
                int count = al.Count;
                for (int m = 0; m < al.Count; m++)
                {
                    string tt = al[m].ToString();
                }
                data.data.dataSource = al[5].ToString();
                data.data.dataBase = al[11].ToString();
                data.data.uid = al[7].ToString();
                try
                {
                    data.data.passWord = DESEncrypt.Decrypt(al[9].ToString());

                    //data.data.passWord = "sa123";
                    //data.data.passWord = al[9].ToString();
                    //string a = "sa123";
                    //string dePasswork1 = DESEncrypt.Encrypt(a);//对输入的明文密码加密
                }
                catch (Exception ex)
                {
                    data.data.passWord = "";
                    MessageBox.Show("请重新设定数据库连接！" + ex.ToString());
                }
                data.data.ConnStr = "data source=" + al[5].ToString() + ";database=" + al[11].ToString() + ";user id=" + al[7].ToString() + ";password=" + data.data.passWord + "";
            }
            Password.Focus();
        }
        # region ★★判断是否有实例在运行★★
        private bool CheckProcess()
        {
            Process[] myProcesses;
            myProcesses = Process.GetProcessesByName("JAC_fAxle");
            if (myProcesses.Length > 1)
            {
                MessageBox.Show("已有一个实例在运行!\r\n请在任务栏关闭单击右键退出系统后重新启动!");
                this.Dispose();
                this.Close();
                return false;
            }
            return true;
        }
        # endregion
        # region ★★检查版本更新★★
        private void CheckEdition()
        {
            //try
            {
                string str = Application.StartupPath;
                string filepath = str + TXTPATH2 + "UnitId" + TXTPOSTFIX;
                string UnitId = "";//工作站ID
                if (System.IO.File.Exists(filepath))
                {
                    //try
                    //{
                    //    string content = File.ReadAllText(filepath);
                    //    UnitId = content;
                    //}
                    //catch
                    //{
                    //    return;
                    //}
                }
                /*****************************************************/
                filepath = str + TXTPATH + "edition" + TXTPOSTFIX;
                if (System.IO.File.Exists(filepath))
                {
                    try
                    {
                        string content = File.ReadAllText(filepath);
                        edition = Convert.ToDateTime(content);
                    }
                    catch
                    {
                        return;
                    }

                }
                /*****************************************************/
                DataTable dt = new DataTable();
                string sql = "select Edition from ApplicationTable where Edition >'" + edition.ToString() + "'";
                dt = data.DBQuery.OpenTable1(sql);
                if (dt.Rows.Count > 0)
                {
                    //this.Close();
                    string templetFile = Application.StartupPath + "\\北奔程序更新.exe";
                    System.Diagnostics.Process.Start(templetFile);
                    this.Dispose();
                    System.Environment.Exit(System.Environment.ExitCode);
                }
                /*****************************************************/
            }
            //catch
            //{


            //}

        }
        # endregion

        private void retreat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void jianbian_Tick(object sender, EventArgs e)
        {
            //让背景由0变到1
            if (this.Opacity < 1)
            {
                this.Opacity = this.Opacity + 0.2;
            }
            else
            {
                this.jianbian.Enabled = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //关闭时动画
            AnimateWindow(this.Handle, 300, AW_SLIDE | AW_HIDE | AW_VER_NEGATIVE);
        }

        private void label15_Click(object sender, EventArgs e)
        {
            About dlg = new About();
            dlg.Show();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Nameid.Text == "")
                {
                    MessageBox.Show("用户名不能为空", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    return;
                }
                //BASE_USER userinfo = userinfo = BASE_USERRepositoryFactory.Repository().FindEntity("ACCOUNT", this.Nameid.Text);
                //if (userinfo != null)
                //{
                //    SystemLog.UserKey = userinfo.USERID.ToString();
                //    SystemLog.LoginName = this.Nameid.Text;
                //    if (SystemLog.LoginName.ToString().Trim() == "sa")/////10.21
                //    {
                //        SystemLog.personalRolesName = "超级管理员";
                //    }
                //    else
                //    {
                //        //SystemLog.personalPowerList = SystemLog.getPowerList(userinfo.USERID.ToString());
                //    }
                //}
                ////if (!(HfutIe.SystemLog.personalRolesName == "超级管理员" || HfutIe.SystemLog.personalPowerList.Contains("G")))
                //if (SystemLog.personalRolesName != "超级管理员")
                //{
                //    MyMsgBox.Show("您没有操作该功能的权限！", "信息提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Information);
                //    return;
                //}
                //HfutIe.SystemLog.AddLog1("设置数据库信息");
                SetData dlg = new SetData();
                dlg.ShowDialog();
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("设置数据库连接失败！", ex);
                MyMsgBox.Show("进入数据库连接设置功能失败，可能原因:数据库连接失败!", "系统提示", MyMsgBox.MyButtons.OK, MyMsgBox.MyIcon.Error);
            }
        }

        private void Ok_Click(object sender, EventArgs e)
        { 
            if (this.Nameid.Text == "")
            {
                MessageBox.Show("用户名不能为空", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            if (this.Password.Text == "")
            {
                MessageBox.Show("请输入用户密码！", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();

                LoginOp lop = new LoginOp();
                int result = lop.CheckPermission(this.Nameid.Text, this.Password.Text/*, StationKey*/);
                LoginResult(result);
                SystemLog.InOrOut = result.ToString();
                if (result == 3||result==5||result==4 || result == 7 || result == 8)
                {
                    //记住密码
                    A_BS_StaffInfo userinfo = new A_BS_StaffInfo();
                    if (ServerCommunicationState == true)//在线状态获取登录人信息
                    {
                        userinfo = a_BS_StaffInfoRepositoryFactory.Repository().FindEntity("StaffAccount", this.Nameid.Text);
                    }
                    else//离线状态获取登录人信息
                    {
                        List<A_BS_StaffInfo> userlist = a_BS_StaffInfoRepositoryFactory.Repository(DatabaseType.SQLite).FindList();
                        userinfo = userlist.Find(x => x.StaffAccount == this.Nameid.Text);
                    }
                    if (userinfo.ID != null)
                    {
                        SystemLog.UserCode = userinfo.StaffCode;
                        SystemLog.UserName = userinfo.StaffName;
                        SystemLog.UserKey = userinfo.ID;
                        SystemLog.LoginName = this.Nameid.Text;

                        string hostinfo = Dns.GetHostName();
                        IPHostEntry ipEntry = Dns.GetHostEntry(hostinfo);
                        string IpAddress = "";
                        if (ipEntry.AddressList.Length > 1)
                        {
                            IpAddress = ipEntry.AddressList[1].ToString();
                        }
                        SystemLog.PCname = hostinfo;
                        SystemLog.IP = IpAddress;
                        if (SystemLog.LoginName.ToString().Trim() == "sa")
                        {
                            SystemLog.personalRolesName = "超级管理员";
                        }
                        else
                        {
                            //SystemLog.personalPowerList = SystemLog.getPowerList(dt.Rows[0]["UserId"].ToString());
                        }
                        ExportToTxt();
                        //CheckEdition();//检查程序更新
                        if (!CheckProcess()) //判断是否有实例存在
                        {
                            return;
                        }
                        XmlWriterSettings settings = new XmlWriterSettings();
                        settings.Indent = true;
                        settings.IndentChars = ("");
                        settings.ConformanceLevel = ConformanceLevel.Document;
                        settings.CloseOutput = false;
                        settings.OmitXmlDeclaration = false;
                        string outfilename = Application.StartupPath + "\\config2.xml";
                        XmlWriter writer = XmlWriter.Create(outfilename, settings);
                        writer.WriteStartDocument();
                        writer.WriteStartElement("data");
                        writer.WriteElementString("login", Nameid.Text.ToString());
                        if (is_remember_password_lbl.Checked)
                        {
                            string dePasswork = DESEncrypt.Encrypt(Password.Text.Trim().ToString());
                            writer.WriteElementString("Pass", dePasswork);//将明文密码加密存入配置文件config2中
                        }
                        writer.WriteEndElement();
                        writer.Flush();
                        writer.Close();
                        this.Close();
                        SystemLog.AddLog("登录系统");
                    }
                }
                sw.Stop();
                long s = sw.ElapsedMilliseconds;
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("登陆失败！", ex);
            }
        }
        private void RememberPassword(string Account, string Secretkey, string StoragePassWord)
        {

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = ("   ");
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.CloseOutput = false;
            settings.OmitXmlDeclaration = false;
            string outfilename = Application.StartupPath + "\\RememberPassWord.xml";

            XmlWriter writer = XmlWriter.Create(outfilename, settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("PasswordInfo");
            writer.WriteElementString("Account", Account);
            writer.WriteElementString("Secretkey", Secretkey);
            writer.WriteElementString("StoragePassWord", StoragePassWord);
            writer.WriteEndElement();
            writer.Flush();
            writer.Close();
            this.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            stopwatch.Stop();
            time += stopwatch.ElapsedTicks;
            if (time > 190)
            {
                this.pictureBox7.Visible = false;
                timer1.Stop();
            }

        }

        private void lnkbaidu_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            lnkbaidu.LinkVisited = true;
            Process.Start(lnkbaidu.Text.Substring(0));

        }
        #endregion

        #region 方法
        private void LoginResult(int result)
        {
            switch (result)
            {
                case 0:
                    MessageBox.Show("该用户名不存在,请重新输入", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    this.Nameid.Clear();
                    this.Password.Clear();
                    break;
                case 1:
                    MessageBox.Show("密码错误,请输入正确的密码！", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    this.Password.Clear();
                    break;
                case 2:
                    MessageBox.Show("此用户没有访问权限！");
                    break;
                case 3:
                    //MessageBox.Show("登陆成功！");
                    break;
                case 6:
                    MessageBox.Show("登陆异常,请检查网线是否连接正常！");
                    break;
            }
        }
        private void ExportToTxt()
        {
            string str = Application.StartupPath;
            string filepath = str + TXTPATH + "login" + TXTPOSTFIX;
            FileInfo file = new FileInfo(filepath);
            StreamWriter textFile = null;
            try
            {
                textFile = file.CreateText();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("系统找不到指定目录下的文件： " + filepath);
                return;
            }
            textFile.Write(Nameid.Text);
            textFile.Close();
        }
        #endregion
        #region 显示服务器通讯信号
        private bool ServerCommunicationState = false;//服务器通讯状态

        private readonly string ServerIP = DBhelperOracle.dataSource;//服务器的IP地址

        private void ShowServerConnectionState()
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
                ServerCommunicationState = false;
            }
        }
        #endregion
    }
}