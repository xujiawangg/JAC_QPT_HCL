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

namespace HfutIe
{
    public partial class LoginForm1 : Form
    {
        #region 仓储
        RepositoryFactory<BASE_USER> Base_UserRepositoryFactory = new RepositoryFactory<BASE_USER>();//用户基本信息表
        #endregion

        #region 属性
        public int tt;//1表示可以操作，２表示浏览，３表示既没操作也没浏览
        public string mancode = "";//获取登陆人员编号
        private const string TXTPATH = @"\Login\";
        private const string TXTPOSTFIX = ".txt";
        private const string TXTPATH2 = @"\Update\";
        private string Wc_Code;
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
        public LoginForm1( string wcCode)
        {
            Wc_Code = wcCode;
            InitializeComponent();
    
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
                if (al1.Count>5)
                {
                    Password.Text = al1[5].ToString();//用户名
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
                //data.data.dataBase = al[11].ToString();
                //data.data.uid = al[7].ToString();
                //try
                //{
                //    data.data.passWord = DESEncrypt.Decrypt(al[9].ToString());
                //    //data.data.passWord = al[9].ToString();
                //}
                //catch (Exception ex)
                //{
                //    data.data.passWord = "";
                //    MessageBox.Show("请重新设定数据库连接！" + ex.ToString());
                //}
                //data.data.ConnStr = "data source=" + al[5].ToString() + ";database=" + al[11].ToString() + ";user id=" + al[7].ToString() + ";password=" + data.data.passWord + "";
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
            if (this.Nameid.Text == "")
            {
                MessageBox.Show("用户名不能为空", "信息提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                return;
            }
            //BASE_USER baseuser = Base_UserRepositoryFactory.Repository().FindEntity("Account", this.Nameid.Text);
            string sql = "select * from Base_User where Account='" + this.Nameid.Text + "'";
            DataTable dt = DBhelperOracle.OpenTable(sql);
            if (dt.Rows.Count>0)
            {
                SystemLog.UserKey = dt.Rows[0]["USERID"].ToString();
                SystemLog.LoginName = this.Nameid.Text;
                if (SystemLog.LoginName.ToString().Trim() == "sa")/////10.21
                {
                    SystemLog.personalRolesName = "超级管理员";
                }
                else
                {
                    SystemLog.personalPowerList = SystemLog.getPowerList(dt.Rows[0]["USERID"].ToString());
                }
            }

            if (!(HfutIe.SystemLog.personalRolesName == "超级管理员" || HfutIe.SystemLog.personalPowerList.Contains("G")))
            {
                MessageBox.Show("您没有操作该功能的权限！", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            HfutIe.SystemLog.AddLog("设置数据库信息");
            SetData dlg = new SetData();
            dlg.ShowDialog();
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
                LoginOp lop = new LoginOp();
                int result = lop.CheckPermission(this.Nameid.Text, this.Password.Text);
                LoginResult(result);
                SystemLog.InOrOut = result.ToString();
                if (result == 3)
                {
                    //记住密码

                    DataTable dt = new DataTable();
                    string sql = "select * from Base_User where Account='" + this.Nameid.Text + "'";
                    dt = DBhelperOracle.OpenTable(sql);
                    //BASE_USER baseuser = Base_UserRepositoryFactory.Repository().FindEntity("Account", this.Nameid.Text);
                    if (dt.Rows.Count>0)
                    {
                        SystemLog.UserCode = dt.Rows[0]["CODE"].ToString();
                        SystemLog.UserName = dt.Rows[0]["REALNAME"].ToString();
                        SystemLog.UserKey = dt.Rows[0]["USERID"].ToString();
                        SystemLog.LoginName = this.Nameid.Text;

                        string hostinfo = Dns.GetHostName();
                        IPHostEntry ipEntry = Dns.GetHostEntry(hostinfo);
                        string IpAddress = ipEntry.AddressList[0].ToString();
                        SystemLog.PCname = hostinfo;
                        SystemLog.IP = IpAddress;
                        if (SystemLog.LoginName.ToString().Trim() == "sa")/////10.21
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
                            writer.WriteElementString("Pass", Password.Text.ToString());
                        }      
                        writer.WriteEndElement();
                        writer.Flush();
                        writer.Close();
                        this.Close();
                        SystemLog.AddLog("登录系统");    
                    }
                }
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
            if (time >190)
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
                case 4:
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
    }
}