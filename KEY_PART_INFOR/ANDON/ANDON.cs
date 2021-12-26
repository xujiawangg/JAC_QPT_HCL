using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using HfutIe;
using System.Reflection;
using log4net;
using System.Net.NetworkInformation;
using static HfutIe.MaterialAndon;
using System.Data.Common;
using KEY_PART_INFOR;
using EntityHelper;
using HfutIE.Entity;
using HfutIE.Repository;
using System.Net;
using System.IO;
using HfutIE.Utilities;

namespace HfutIe
{
    public partial class ANDON : Form
    {

        #region 仓储
        static RepositoryFactory<PART> PARTRepositoryFactory = new RepositoryFactory<PART>();//零部件基本信息表
        static RepositoryFactory<P_ANDON_INFOR> P_ANDON_INFORRepositoryFactory = new RepositoryFactory<P_ANDON_INFOR>();//andon信息过程表
        static RepositoryFactory<DOC_ANDON_INFOR> DOC_ANDON_INFORRepositoryFactory = new RepositoryFactory<DOC_ANDON_INFOR>();//andon信息档案表
        static RepositoryFactory<P_ANDON_MATERIAL_NEED> P_ANDON_MATERIAL_NEEDRepositoryFactory = new RepositoryFactory<P_ANDON_MATERIAL_NEED>();//andon物料需求过程表
        static RepositoryFactory<DOC_ANDON_MATERIAL_NEED> DOC_ANDON_MATERIAL_NEEDRepositoryFactory = new RepositoryFactory<DOC_ANDON_MATERIAL_NEED>();//andon物料需求档案表
        static RepositoryFactory<MATERIAL_WC_PART> MATERIAL_WC_PARTRepositoryFactory = new RepositoryFactory<MATERIAL_WC_PART>();//线边库工位物料配置表
        static RepositoryFactory<Part_List> Part_ListRepositoryFactory = new RepositoryFactory<Part_List>();//物料清单表
        #endregion

        #region 公共变量

        public string wc_code = "";//工位编号
        string wc_key;//工位key
        public string stationcode;//工位集合
        public BasicInfoDto basicInfor;//根据工位得出的基本信息
        public P_ASSEMBLE_PRODUCT_STATE plan_product_data;//
        public DataTable key_part_c;//安全件采集配置
        List<MATERIAL_WC_PART> material_wc_part_dt;//线边库信息
        List<Part_List> part_list_dt;//物料清单信息
        List<AndonInfoDto> andonInfor;//andon项的具体信息（上层配置）
        public List<AndonTypeDto> andonTypeTable=new List<AndonTypeDto> ();//andon类型信息（上层配置）
        object lastsender = new object();//用于判断是否双击，记录上一次点击的button控件
        DateTime lasttime = new DateTime();//用于判断是否双击，记录上一次点击button空控件的时间
        
        List<Control> conList = new List<Control>();//所有的控件集合
        List<Control>[] beforeAndonButton;//所有准备提交的andon的集合
        List<Control>[] beforeResetAndonButton;//所有准备重置的andon的集合
        List<P_ANDON_INFOR> andonButton = new List<P_ANDON_INFOR>();//正在andon呼叫的所有button的集合

        #endregion

        #region 界面初始化加载

        public ANDON()
        {
            InitializeComponent();
            ShowServerConnectionState();
            Initialization();
        }
        public ANDON(BasicInfoDto baseinfo)
        {
            InitializeComponent();
            basicInfor = baseinfo;
            ShowServerConnectionState();
            Initialization();
        }
        public ANDON(BasicInfoDto baseinfo,P_ASSEMBLE_PRODUCT_STATE plan_data)
        {
            InitializeComponent();
            basicInfor = baseinfo;
            plan_product_data = plan_data;
            ShowServerConnectionState();
            Initialization();
        }
        public void Initialization()
        {
            try
            {
                DateTime current_time = ServerTime.Now;
                //basicInfor = GetData.GetFactoryInforByWccode(stationcode);//获取基本信息
                andonInfor = AndonGetData.GetAndonItemInfor(basicInfor.STATION_KEY);//获取所有andon项的信息

                if (basicInfor == null)
                {
                    DialogResult dr = MessageBox.Show("基本信息获取失败！是否终止启动系统？", "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (dr == DialogResult.Yes)
                    {
                        Formclosing();
                        //System.Windows.Forms.Application.Exit();
                    }
                }
                if (basicInfor != null)
                {
                    wc_key = basicInfor.STATION_KEY.ToString();
                    stationcode = basicInfor.STATION_CODE.ToString();
                }
                //material_wc_part_dt = AndonGetData.GetMaterialWcPart(wc_key);//获取本工位线边库信息
                part_list_dt = Part_ListRepositoryFactory.Repository().FindList($" and MES_PLAN_CODE='{plan_product_data.MES_PLAN_CODE}' and OP_CODE='{stationcode.Substring(5, stationcode.Length-5)}'").OrderBy(s=>s.Part_Code).ToList();
                GetAndonTypeTable();
                beforeAndonButton = new List<Control>[andonTypeTable.Count];
                beforeResetAndonButton = new List<Control>[andonTypeTable.Count];
                AddGroupBox(andonTypeTable);//新增groupbox
                GetAllControls(this);//获取所有的控件
                GetAndonButton();//获取正在andon呼叫的button
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化失败！" + ex.ToString(), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("初始化失败！" + ex.Message);
            }
        }

        public void Formclosing()//为了防止出现方法同名，此处用Formclosing命名，原定用FormClosing命名
        {
            try
            {
                //ScannerHelper.CloseCom(scanport);
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

        #region 获取上层系统中配置的andon类型

        public void GetAndonTypeTable()
        {
            List<string> andon_type_keylist = andonInfor.Select(s => s.ANDON_TYPE_KEY).Distinct().ToList();
            foreach (var item in andon_type_keylist)
            {
                AndonTypeDto typeentity = new AndonTypeDto();
                typeentity = andonInfor.Find(s => s.ANDON_TYPE_KEY == item).EntityMapper<AndonInfoDto, AndonTypeDto>();
                andonTypeTable.Add(typeentity);
            }
        }
        #endregion

        #region 界面加载时，从数据库中读取正在执行的andon呼叫信息

        public void GetAndonButton()
        {
            try
            {
                List<P_ANDON_INFOR> andonButton = AndonGetData.GetAndonInfor(wc_key);//获取正在执行的andon信息
                //andonButton = EntityHelper<P_ANDON_INFOR>.GetEntityList(getandonbutton);//将datatable转化为list集合
                foreach (P_ANDON_INFOR andon in andonButton)
                {
                    //Control button = conlist.Find(btn => btn.Name.Contains(andon.andon_info_code) && btn.Parent.Name.Contains(andon.andon_type_code));
                    Control button = conList.Find(btn => btn.Tag != null && btn.Tag.ToString().Split('|')[0] == andon.ANDON_INFO_KEY && btn.GetType().FullName == "System.Windows.Forms.Button");//对于每一项正在呼叫的andon，在界面控件中查找该条andon记录对应的控件，查找条件为，控件tag不为空，且tag以‘|’分割后的第一段字符串等于andon_info_key，同时该控件必须是button控件
                    if (button != null)
                    {
                        //string fullname = button.GetType().FullName;
                        if (!string.IsNullOrEmpty(andon.FEED_STAFF_KEY))
                        {
                            button.BackColor = Color.CornflowerBlue;//将有相应的andon背景色变蓝色
                        }
                        else
                        {
                            button.BackColor = Color.Red;//将查找到的控件背景色变红
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ANDON呼叫信息显示错误！" + ex.ToString(), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("ANDON呼叫信息显示错误！" + ex.Message);
            }
        }
        #endregion

        #region 界面各控件代码

        public void AddGroupBox(List<AndonTypeDto> list)
        {
            try
            {
                int i = 0;
                foreach (var item in list)
                {
                    GroupBox groupBox = new GroupBox();
                    this.Controls.Add(groupBox);
                    this.panel2.Controls.Add(groupBox);
                    groupBox.Dock = System.Windows.Forms.DockStyle.Top;
                    groupBox.Name = item.ANDON_TYPE_CODE + "_groupBox|" + i;//控件名称存储andon类型的编号
                    groupBox.Tag = item.ANDON_TYPE_NAME;//控件tag存储andon类型的名称
                                                                            //li.Add(groupBox);
                                                                            //此处需加判断，使groupbox得宽度调整
                    groupBox.Size = new System.Drawing.Size(1008, 182);
                    groupBox.TabIndex = list.Count - i;//form加载完成，聚焦TabIndex最小的控件
                    groupBox.TabStop = false;
                    List<Control> beforeAndon = new List<Control>();
                    beforeAndonButton[i] = beforeAndon;
                    List<Control> beforeResetAndon = new List<Control>();
                    beforeResetAndonButton[i] = beforeResetAndon;
                    groupBox.Paint += groupBox1_Paint;
                    groupBox.Margin = new Padding(0, -2, 0, -2);
                    AddControl(groupBox, i);//key为andon类型的key
                                            //this.groupBox4.Controls.Add(this.button7);
                                            //this.groupBox4.Controls.Add(this.button8);
                                            //this.groupBox4.Controls.Add(this.panel8);
                                            //this.groupBox4.Controls.Add(this.panel9);
                                            //this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
                                            //this.groupBox4.Location = new System.Drawing.Point(0, 560);
                                            //this.groupBox4.Name = "groupBox4";
                                            //this.groupBox4.Size = new System.Drawing.Size(1008, 167);
                                            //this.groupBox4.TabIndex = 4;
                                            //this.groupBox4.TabStop = false;
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("界面加载失败！" + ex.ToString(), "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("界面加载失败！" + ex.Message);
            }
        }

        //根据不同andon类型添加控件
        public void AddControl(GroupBox gb, int i)
        {
            //参数i目的是为了快速寻找到对应的控件集合，不同类型的ANDON的i值不同，故根据控件名称的最后一位可以快速确定是哪种ANDON类型

            string code = gb.Name.Split('|')[0];
            string name = gb.Tag.ToString();
            //向左的按钮
            Button btn = new Button();
            btn.Name = code + "pulltoleft_btn|" + i;
            btn.Text = "<<";
            btn.Size = new System.Drawing.Size(41, 147);
            btn.Dock = System.Windows.Forms.DockStyle.Left;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Margin = new Padding(1, 1, 1, 1);
            gb.Controls.Add(btn);

            //最左边的panel
            Panel panel = new Panel();
            panel.Name = code + "left_pnl|" + i;
            panel.Size = new System.Drawing.Size(120, 147);
            panel.Dock = System.Windows.Forms.DockStyle.Left;
            gb.Controls.Add(panel);

            //最左边的panel里面的panel
            Panel panel_blue = new Panel();
            panel_blue.Name = code + "left_pnl_blue|" + i;
            panel_blue.Size = new System.Drawing.Size(70, 162);
            panel_blue.Location = new Point(25, 0);
            //panel_blue.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\2.2.JPG");
            //panel_blue.Dock = System.Windows.Forms.DockStyle.Left;
            panel.Controls.Add(panel_blue);

            //最左边的panel里面的label控件，显示安灯名称
            Label label = new Label();
            label.TextAlign = ContentAlignment.MiddleCenter;
            //text.Size = new System.Drawing.Size(81, 147);
            label.Name = code + "name_lbl|" + i;
            //label.Font = new Font("宋体", 23, label.Font.Style);
            label.Font = new Font("宋体", 20, FontStyle.Bold);
            label.ForeColor = Color.White;//字体颜色
            panel_blue.Controls.Add(label);
            label.Text = name;
            label.BackgroundImage = Image.FromFile(Application.StartupPath + "\\Resources\\3.2.PNG");
            label.Dock = System.Windows.Forms.DockStyle.Fill;

            //向右的按钮
            Button btn1 = new Button();
            btn1.Name = code + "pulltoright_btn|" + i;
            btn1.Text = ">>";
            btn1.Size = new System.Drawing.Size(41, 147);
            btn1.Dock = System.Windows.Forms.DockStyle.Right;
            btn1.FlatStyle = FlatStyle.Flat;
            btn1.Margin = new Padding(1, 1, 1, 1);
            gb.Controls.Add(btn1);

            //最右边的panel
            Panel pane2 = new Panel();
            pane2.Name = code + "right_pnl|" + i;
            pane2.Size = new System.Drawing.Size(198, 147);
            pane2.Dock = System.Windows.Forms.DockStyle.Right;
            gb.Controls.Add(pane2);

            //最右边的panel里面的装andon呼叫的pic的panel
            Panel pane3 = new Panel();
            pane3.Name = code + "right_pnl|" + i;
            pane3.Size = new System.Drawing.Size(73, 72);
            pane3.Location = new System.Drawing.Point(16, 18);
            pane2.Controls.Add(pane3);

            //最右边panel2里面的panel3里面的安灯呼叫的picturebox
            PictureBox pic1 = new PictureBox();
            pic1.Name = code + "andon_pic|" + i;
            pic1.Size = new System.Drawing.Size(70, 70);
            pic1.Image = Image.FromFile(Application.StartupPath + "\\Resources\\呼叫.png");
            pic1.SizeMode = PictureBoxSizeMode.Zoom;
            pane3.Controls.Add(pic1);
            pic1.Location = new System.Drawing.Point(2, 1);
            pic1.Click += new EventHandler(Andon_Click);

            //最右边panel3里面的panel3里面的安灯呼叫的label
            Label label1 = new Label();
            label1.Name = code + "andon_lbl|" + i;
            pane2.Controls.Add(label1);
            label1.Text = "呼叫";
            label1.Font = new Font("华文细黑", 13, label1.Font.Style);
            label1.Location = new System.Drawing.Point(5, 105);
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += new EventHandler(Andon_Click);


            //最右边panel2里面的panel3里面的重置安灯呼叫的picturebox
            Panel pane4 = new Panel();
            pane4.Name = code + "right_pnl|" + i;
            pane4.Size = new System.Drawing.Size(73, 72);
            pane4.Location = new System.Drawing.Point(106, 18);
            pane2.Controls.Add(pane4);

            //最右边panel2里面的panel3里面的重置安灯呼叫的picturebox
            PictureBox pic2 = new PictureBox();
            pic2.Name = code + "resetandon_pic|" + i;
            pic2.Size = new System.Drawing.Size(70, 70);
            pic2.Image = Image.FromFile(Application.StartupPath + "\\Resources\\重置.png");
            pic2.SizeMode = PictureBoxSizeMode.Zoom;
            pane2.Controls.Add(pic2);
            pic2.Location = new System.Drawing.Point(2, 1);
            pic2.Click += new EventHandler(ResetAndon_Click);
            pane4.Controls.Add(pic2);

            //最右边panel3里面的panel3里面的重置安灯呼叫的label
            Label label2 = new Label();
            label2.Name = code + "resetandon_lbl|" + i;
            pane2.Controls.Add(label2);
            label2.Text = "重置";
            label2.Font = new Font("华文细黑", 13, label2.Font.Style);
            label2.Location = new System.Drawing.Point(95, 105);
            label2.TextAlign = ContentAlignment.MiddleCenter;
            label2.Click += new EventHandler(ResetAndon_Click);

            //中间存放具体安灯项的FlowLayoutPanel
            FlowLayoutPanel flowlayoutpanel = new FlowLayoutPanel();
            flowlayoutpanel.Name = code + "_flp|" + i;
            gb.Controls.Add(flowlayoutpanel);
            flowlayoutpanel.Size = new System.Drawing.Size(600, 182);
            flowlayoutpanel.WrapContents = true;
            flowlayoutpanel.AutoScroll = true;
            flowlayoutpanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flowlayoutpanel.Location = new System.Drawing.Point(165, 17);

            if (name.Contains("物料") == false)//除物料andon之外的其他andon
            {
                AddButtonToFlowlayoutpanel(flowlayoutpanel, i);//向Flowlayoutpanel中添加Button，Flowlayoutpanel中存储andon项
            }
            else if (name.Contains("物料") == true)//物料andon
            {
                AddButtonToFlowlayoutpanel(flowlayoutpanel, i, name);//向Flowlayoutpanel中添加Button，Flowlayoutpanel中存储andon项，name只是为了区别两个方法，并没有实际作用
            }


            btn.Click += new System.EventHandler((btn2, e) => { PullToLeft(btn, e, flowlayoutpanel); });
            btn1.Click += new System.EventHandler((btn2, e) => { PullToRight(btn, e, flowlayoutpanel); });
            pic1.MouseDown += new System.Windows.Forms.MouseEventHandler(AndonDownEffects);
            pic1.MouseUp += new System.Windows.Forms.MouseEventHandler(AndonUpEffects);
            label1.MouseDown += new System.Windows.Forms.MouseEventHandler(AndonDownEffects);
            label1.MouseUp += new System.Windows.Forms.MouseEventHandler(AndonUpEffects);
            pic2.MouseDown += new System.Windows.Forms.MouseEventHandler(AndonDownEffects);
            pic2.MouseUp += new System.Windows.Forms.MouseEventHandler(AndonUpEffects);
            label2.MouseDown += new System.Windows.Forms.MouseEventHandler(AndonDownEffects);
            label2.MouseUp += new System.Windows.Forms.MouseEventHandler(AndonUpEffects);
            //for (int i = 0; i< 3; i++)
            //{
            //    TextBox txt = new TextBox();
            //   txt.Name =gb.Name+ name + i;
            //    txt.Text =gb.Name+"|"+ name + i;
            //   txt.Location = new Point(12, 15 + i* 30);
            //    gb.Controls.Add(txt);
            //}
        }

        #region 为各个andon类型的Flowlayoutpanel中增加andon项（button）

        public void AddButtonToFlowlayoutpanel(FlowLayoutPanel flowlayoutpanel, int i)//增加除物料andon外以外的andon项
        {
            //flowlayoutpanel为需要添加到的panel，t为该类型的andon在andon类型表（andontypetable）中的行数
            int t = 0;
            string andontypekey = andonTypeTable[i].ANDON_TYPE_KEY.ToString();
            List<AndonInfoDto> andonitem = andonInfor.FindAll(s => s.ANDON_TYPE_KEY == andontypekey);
            foreach (var item in andonitem)
            {
                //物料andon的button的tag为：part_key|max_num|storage_num|delivery_unit_num|color的形式，color的存在是为了区别已相应andon和未响应andon
                //其他andon的button的tag为：andon_info_key|color的形式，color的存在是为了区别已相应andon和未响应andon
                if (!string.IsNullOrEmpty(item.ANDON_INFO_KEY))
                {
                    Button button = new Button();
                    button.Height = 81;
                    button.Width = 132;
                    button.Name = item.ANDON_INFO_CODE + "|" + i;
                    button.TabIndex = t;
                    button.Text = item.ANDON_INFO_NAME;
                    button.TextAlign = ContentAlignment.MiddleCenter;
                    button.Font = new Font("华文细黑", 16, button.Font.Style);
                    button.Tag = item.ANDON_INFO_KEY + "|normal";
                    button.Parent = flowlayoutpanel;
                    button.Margin = new Padding(1, 1, 1, 1);
                    button.FlatStyle = FlatStyle.Flat;
                    if ((t + 1) % 2 == 0)
                    {
                        flowlayoutpanel.SetFlowBreak(button, true);//使 FlowLayoutPanel 控件停止在当前流方向布局控件并换到下一行或下一列
                    }
                    button.Click += new System.EventHandler(this.button_Click);
                    t++;
                }
            }
        }
        public void AddButtonToFlowlayoutpanel(FlowLayoutPanel flowlayoutpanel, int i, string name)//增加物料andon项
        {
            //flowlayoutpanel为需要添加到的panel，t为该类型的andon在andon类型表（andontypetable）中的行数
            int t = 0;
            string andontypekey = andonTypeTable[i].ANDON_TYPE_KEY.ToString();
            //var andonitem = from item in material_wc_part_dt.Select()
            //                    //where item["andon_type_key"].ToString() == andontypekey
            //                select new { part_key = item["part_key"].ToString(), part_code = item["part_code"].ToString(), part_name = item["part_name"].ToString(), max_num = item["max_num"].ToString(), storage_num = item["storage_num"].ToString(), delivery_unit_num = item["delivery_unit_num"].ToString() };
            //foreach (var item in material_wc_part_dt)
            //{
            foreach (var item in part_list_dt)
            {
                //物料andon的button的tag为：part_key | max_num | storage_num | delivery_unit_num | color的形式，color的存在是为了区别已相应andon和未响应andon
                //其他andon的button的tag为：andon_info_key | color的形式，color的存在是为了区别已相应andon和未响应andon
                Button button = new Button();
                button.Height = 80;
                button.Width = 132;
                button.Name = item.Part_Code + "|" + i;
                button.TabIndex = t;
                button.Text = item.Part_Code + "(" + item.Part_Name + ")";
                button.TextAlign = ContentAlignment.MiddleCenter;

                button.Font = new Font("华文细黑", 10, button.Font.Style);
                button.Tag = item.Part_Code+ "|" + "0" + "|" + "0" + "|" + "0" + "|normal";
                button.Parent = flowlayoutpanel;
                button.Margin = new Padding(1, 1, 1, 1);
                button.FlatStyle = FlatStyle.Flat;
                if ((t + 1) % 2 == 0)
                {
                    flowlayoutpanel.SetFlowBreak(button, true);//使 FlowLayoutPanel 控件停止在当前流方向布局控件并换到下一行或下一列
                }
                button.Click += new System.EventHandler(this.materialButton_Click);
                t++;
            }
       }
        #endregion
        #endregion

        #region 实现点击拉动进度条的方法

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hwnd, Int32 wMsg, IntPtr wParam, IntPtr lParam);
        enum ScrollBarMessage
        {
            WM_HSCROLL = 0x0114,    // WM_HSCROLL消息  
            WM_VSCROLL = 0x0115     // WM_VSCROLL消息  
        };

        enum HScrollBarCommands
        {
            SB_LINELEFT = 0,        // 向左滚动一个单元  
            SB_LINERIGHT = 1,       // 向右滚动一个单元  
            SB_PAGELEFT = 2,        // 向左滚动一个窗口宽度  
            SB_PAGERIGHT = 3,       // 向右滚动一个窗口宽度  
            SB_THUMBPOSITION = 4,
            SB_THUMBTRACK = 5,
            SB_LEFT = 6,            // 滚动到最左边  
            SB_RIGHT = 7,           // 滚动到最右边  
            SB_ENDSCROLL = 8
        };

        enum VScrollBarCommands
        {
            SB_LINEUP = 0,          // 上滚一行  
            SB_LINEDOWN = 1,        // 下滚一行  
            SB_PAGEUP = 2,          // 上滚一页  
            SB_PAGEDOWN = 3,        // 下滚一页  
            SB_THUMBPOSITION = 4,
            SB_THUMBTRACK = 5,
            SB_TOP = 6,             // 滚动到顶部  
            SB_BOTTOM = 7,          // 滚动到底部  
            SB_ENDSCROLL = 8,
            SB_PAGEright = 9
        };

        private void PullToRight(object sender, EventArgs e, FlowLayoutPanel flowLayoutPanel)
        {
            SendMessage(flowLayoutPanel.Handle, (Int32)ScrollBarMessage.WM_HSCROLL, (IntPtr)HScrollBarCommands.SB_PAGERIGHT, IntPtr.Zero);
        }
        private void PullToLeft(object sender, EventArgs e, FlowLayoutPanel flowLayoutPanel)
        {
            SendMessage(flowLayoutPanel.Handle, (Int32)ScrollBarMessage.WM_HSCROLL, (IntPtr)HScrollBarCommands.SB_PAGELEFT, IntPtr.Zero);
        }
        //private void PullToRight(object sender, EventArgs e)
        //{
        //    SendMessage(flowLayoutPanel1.Handle, (Int32)ScrollBarMessage.WM_HSCROLL, (IntPtr)HScrollBarCommands.SB_PAGERIGHT, IntPtr.Zero);
        //}
        //private void PullToLeft(object sender, EventArgs e)
        //{
        //    SendMessage(flowLayoutPanel1.Handle, (Int32)ScrollBarMessage.WM_HSCROLL, (IntPtr)HScrollBarCommands.SB_PAGELEFT, IntPtr.Zero);
        //}
        #endregion

        #region 点击除物料andon外的其他andon项的button

        private void button_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = conList.Find(ctl => ctl.Name == ((Control)sender).Name);//在全部空间中找到点击的控件
                int i = Convert.ToInt32(control.Name.Split('|')[control.Name.Split('|').Length - 1]);//获取将控件名以‘|’分割后的最后一串字符串的数值
                if (control.BackColor == SystemColors.Control)//如果button本身为系统色，则将之变为黄色，并将它加入即将进行andon呼叫的集合
                {
                    control.BackColor = Color.Yellow;
                    beforeAndonButton[i].Add(control);
                }
                else if (control.BackColor == Color.Yellow)//如果button本身为黄色，则将之变为系统色，并将它从即将进行andon呼叫的集合中移除
                {
                    control.BackColor = SystemColors.Control;
                    beforeAndonButton[i].Remove(control);
                }
                else if (control.BackColor == Color.Red)//如果button本身为红色，则将之变为绿色，并把它加入即将进行重置andon呼叫的集合
                {
                    control.Tag = control.Tag.ToString().Split('|')[0] + "|Red";
                    control.BackColor = Color.LawnGreen;
                    beforeResetAndonButton[i].Add(control);
                }
                else if (control.BackColor == Color.CornflowerBlue)//如果button本身为蓝色，则将之变为绿色，重写tag信息，标识其原本为蓝色，并把它加入即将进行重置andon呼叫的集合
                {
                    control.Tag = control.Tag.ToString().Split('|')[0] + "|CornflowerBlue";
                    control.BackColor = Color.LawnGreen;
                    beforeResetAndonButton[i].Add(control);
                }
                else if (control.BackColor == Color.LawnGreen)//如果button本身为绿色，判断其原本为蓝色还是红色，将之变为相应颜色，并将它从即将进行重置andon呼叫的集合中移除
                {
                    if (control.Tag.ToString().Split('|')[1] == "CornflowerBlue")//如果tag中有标记为CornflowerBlue的，则将之变为蓝色
                    {
                        control.BackColor = Color.CornflowerBlue;
                    }
                    else//如果tag中没有标记为CornflowerBlue的，则将之变为红色
                    {
                        control.BackColor = Color.Red;
                    }
                    beforeResetAndonButton[i].Remove(control);
                }
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("其他ANDON项button点击失败！" + ex.Message);
            }
        }
        #endregion

        #region 点击物料andon项，内有判断双击的方法

        public void materialButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (ServerCommunicationState)
                {
                    bool doubleclick = false;//判断本次点击是否是双击
                    Type t1 = sender.GetType();
                    Control c = (Control)sender;

                    Control control = conList.Find(ctl => ctl.Name == ((Control)sender).Name);//根据名称找到本次点击的控件
                    int i = Convert.ToInt32(control.Name.Split('|')[control.Name.Split('|').Length - 1]);//获取将控件名以‘|’分割后的最后一串字符串的数值
                    Task.Run(
                        new Action(() =>
                        {
                            material_wc_part_dt = AndonGetData.GetMaterialWcPart(wc_key);//更新线边库
                            if (material_wc_part_dt.AsEnumerable().Where(t => t.PART_KEY.ToString() == control.Tag.ToString().Split('|')[0]).Count() > 0)
                            {
                                //DataRow relateToTag = material_wc_part_dt.Select("part_key='" + control.Tag.ToString().Split('|')[0] + "'")[0];//从线边库信息中提取与本次点击的button有关的数据，主要是获得库存数值
                                MATERIAL_WC_PART relateToTag = material_wc_part_dt.Find(s => s.PART_KEY == control.Tag.ToString().Split('|')[0]);
                                control.Tag = control.Tag.ToString().Split('|')[0] + "|" + relateToTag.MAX_NUM + "|" + relateToTag.STORAGE_NUM + "|" + control.Tag.ToString().Split('|')[3] + "|" + control.Tag.ToString().Split('|')[4];//更新button的tag属性，tag属性为：part_key|max_num|storage_num|delivery_unit_num，因为线边物料数量可能汇编，所有需要在每次点击该控件时更新其tag信息
                            }
                        }
                    )
                    );
                    #region 判断是否是双击

                    if (lastsender == sender && (control.BackColor == Color.Yellow || control.BackColor == SystemColors.Control)) //只有在andon呼叫时才能有双击功能，andon复位时不能有双击功能
                    {
                        DateTime now = ServerTime.Now;//本次点击时间
                        TimeSpan tp = now - lasttime;//本次点击时间与上次点击时间之间的时间间隔
                        if (tp.TotalSeconds < 0.6)//当两次点击时间间隔小于0.6秒时，可判定这两次点击构成双击
                        {
                            doubleclick = true;//本次点击是双击
                        }
                    }
                    lastsender = sender;//将本次点击控件标为上次点击控件
                    lasttime = ServerTime.Now;//将此刻标为上次点击时间
                    #endregion
                    if (doubleclick == false)//如果是单击的话，和其他andon一样处理
                    {
                        if (control.BackColor == SystemColors.Control)//如果button本身为系统色，则将之变为黄色，并将它加入即将进行andon呼叫的集合
                        {
                            control.BackColor = Color.Yellow;
                            beforeAndonButton[i].Add(control);
                        }
                        else if (control.BackColor == Color.Yellow)//如果button本身为黄色，则将之变为系统色，并将它从即将进行andon呼叫的集合中移除
                        {
                            control.BackColor = SystemColors.Control;
                            beforeAndonButton[i].Remove(control);
                        }
                        else if (control.BackColor == Color.Red)//如果button本身为红色，则将之变为绿色，并把它加入即将进行重置andon呼叫的集合
                        {
                            control.Tag = control.Tag.ToString().Split('|')[0] + '|' + control.Tag.ToString().Split('|')[1] + '|' + control.Tag.ToString().Split('|')[2] + '|' + control.Tag.ToString().Split('|')[3] + "|red";
                            control.BackColor = Color.LawnGreen;
                            beforeResetAndonButton[i].Add(control);
                        }
                        else if (control.BackColor == Color.CornflowerBlue)//如果button本身为蓝色，则将之变为绿色，重写tag信息，标识其原本为蓝色，并把它加入即将进行重置andon呼叫的集合
                        {
                            control.Tag = control.Tag.ToString().Split('|')[0] + '|' + control.Tag.ToString().Split('|')[1] + '|' + control.Tag.ToString().Split('|')[2] + '|' + control.Tag.ToString().Split('|')[3] + "|CornflowerBlue";
                            control.BackColor = Color.LawnGreen;
                            beforeResetAndonButton[i].Add(control);
                        }
                        else if (control.BackColor == Color.LawnGreen)//如果button本身为绿色，判断其原本为蓝色还是红色，将之变为相应颜色，并将它从即将进行重置andon呼叫的集合中移除
                        {
                            if (control.Tag.ToString().Split('|')[4] == "CornflowerBlue")//如果tag中有标记为CornflowerBlue的，则将之变为蓝色
                            {
                                control.BackColor = Color.CornflowerBlue;
                            }
                            else//如果tag中没有标记为CornflowerBlue的，则将之变为红色
                            {
                                control.BackColor = Color.Red;
                            }
                            beforeResetAndonButton[i].Remove(control);
                        }
                    }
                    else if (doubleclick == true)//如果本次点击是双击
                    {
                        #region 通过事件委托实现编辑配送物料数量

                        MaterialAndon ma = new MaterialAndon(control.Text);
                        ma.data = control.Tag == null ? "||||" : control.Tag.ToString();//向打开的子窗体传递数据
                        ma.ButtonClick += new GetMaterialNum(//通过事件委托，将子窗体的确定按钮增加事件，即一点button，将子页面中的combobox数据传递回父窗体
                            (num) =>
                            {
                                control.Tag = control.Tag.ToString().Split('|')[0] + "|" + control.Tag.ToString().Split('|')[1] + "|" + control.Tag.ToString().Split('|')[2] + "|" + num + "|" + control.Tag.ToString().Split('|')[4];//子窗体中选择配送数量，将之重写回父窗体，且下次的默认选项是本次选择的配送数量
                                ma.Close();//子窗体关闭
                            }
                        );
                        ma.FormClosed += new FormClosedEventHandler(
                            (sender2, e2) => { ma = null; }
                        );
                        ma.ShowDialog(this);
                        if (control.BackColor == SystemColors.Control)//如果button颜色为系统色，说明button已经在beforeAndonButton[i]中，需要添加，如果是yellow色，说明已经添加，不需要再重复添加了（颜色和是否在beforeAndonButton[i]中是匹配的，如果button背景色是黄色则button一定在beforeAndonButton[i]中，如果button背景色是系统色则button一定不在beforeAndonButton[i]中）
                        {
                            control.BackColor = Color.Yellow;
                            beforeAndonButton[i].Add(control);
                        }
                        #endregion
                    }
                }
                else
                {
                    MessageBox.Show("网络异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("物料ANDON项button点击失败！" + ex.Message);
            }
        }
        #endregion

        #region 将button转化为entity实体

        public P_ANDON_INFOR ButtonToEntity(Button button)
        {
            P_ANDON_INFOR p_andon_infor = new P_ANDON_INFOR();
            try
            {
                if (button.BackColor == Color.Yellow)//andon呼叫时，将button创建成实体，插入数据库
                {
                    p_andon_infor.Create();
                    EntityHelper.EntityHelper.EntityMapper(basicInfor, p_andon_infor);
                    int i = Convert.ToInt32(button.Name.Split('|')[button.Name.Split('|').Length - 1]);//这是ANDON类型中第几个ANDON类型，主要通过这个参数来确定ANDON的类型，原理是：将button的name按照“|”进行分割，最后一个即是i
                    if (andonTypeTable[i].ANDON_TYPE_NAME.ToString().Contains("物料") == false)//除物料andon之外的其他andon
                    {
                        AndonInfoDto ad_info = andonInfor.Find(s => s.ANDON_TYPE_KEY == andonTypeTable[i].ANDON_TYPE_KEY && s.ANDON_INFO_KEY == button.Tag.ToString().Split('|')[0]);
                        if (ad_info != null)
                        {
                            EntityHelper.EntityHelper.EntityMapper(ad_info, p_andon_infor);
                        }
                    }
                    else if (andonTypeTable[i].ANDON_TYPE_NAME.ToString().Contains("物料") == true)//物料andon
                    {
                        AndonInfoDto ad_info = andonInfor.Find(s => s.ANDON_TYPE_KEY == andonTypeTable[i].ANDON_TYPE_KEY);
                        if (ad_info != null)
                        {
                            EntityHelper.EntityHelper.EntityMapper(ad_info, p_andon_infor);
                        }
                        //物料andon没有推送人push_people（推送人）和rank（andon等级）数据源，总是为空
                        p_andon_infor.ANDON_INFO_KEY = button.Tag == null ? "" : button.Tag.ToString().Split('|')[0];
                        p_andon_infor.ANDON_INFO_CODE = button.Name.Split('|')[0];
                        p_andon_infor.ANDON_INFO_NAME = button.Text;
                        MATERIAL_WC_PART material_wc_partentity = material_wc_part_dt.Find(t => t.PART_KEY.ToString() == (button.Tag == null ? "" : button.Tag.ToString().Split('|')[0]));
                        if (material_wc_partentity!=null)
                        {
                            p_andon_infor.REMARK = material_wc_partentity.REMARK;
                        }
                    }
                    p_andon_infor.START_TIME = ServerTime.Now;
                }
                else if (button.BackColor == Color.LawnGreen)//andon复位时，根据button从数据库中加载出 对应实体
                {
                    if (ServerCommunicationState)
                    {
                        List<P_ANDON_INFOR> andonButton = AndonGetData.GetAndonInfor(wc_key);
                        p_andon_infor = andonButton.Find(s => s.ANDON_INFO_KEY == button.Tag.ToString().Split('|')[0]);
                        //List<P_ANDON_INFOR> p_andon_infor = andonButton.Select("andon_info_key ='" + button.Tag.ToString().Split('|')[0] + "'");
                        //if (selectAndonItem.Length > 0)
                        //{
                        //    p_andon_infor = EntityHelper<P_ANDON_INFOR>.GetPartEntity(p_andon_infor, selectAndonItem[0]);
                        //}
                        //from item in andonButton.Select()
                        //where item["andon_info_key"].ToString() == button.Tag.ToString()
                        //select item["andon_info_key"];
                        //P_ANDON_INFORRepositoryFactory.Repository().Delete(p_andon_infor.ANDON_INFOR_KEY);
                        //DbHelperSQL.ExecuteSql("delete from P_ANDON_INFOR where andon_info_key ='" + button.Tag + "'");
                    }
                    else
                    {
                        MessageBox.Show("网络异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return p_andon_infor;
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("将button转化为entity失败！" + ex.Message);
                return p_andon_infor;
            }
        }
        #endregion

        #region 按下andon和andon复位按钮的特效

        public void AndonDownEffects(object sender, EventArgs e)
        {
            Control control = conList.Find(ctl => ctl.Name == ((Control)sender).Name);
            if (control.GetType() == typeof(PictureBox))
            {
                control.Parent.BackColor = Color.Red;
            }
            else if (control.GetType() == typeof(Label))
            {
                control.BackColor = Color.Red;
            }
        }

        public void AndonUpEffects(object sender, EventArgs e)
        {
            Control control = conList.Find(ctl => ctl.Name == ((Control)sender).Name);
            if (control.GetType() == typeof(PictureBox))
            {
                control.Parent.BackColor = SystemColors.Control;
            }
            else if (control.GetType() == typeof(Label))
            {
                control.BackColor = SystemColors.Control;
            }
        }
        #endregion

        #region andon呼叫与重置
        public void Andon_Click(object sender, EventArgs e)
        {
            try
            {
                if (ServerCommunicationState)
                {
                    string message = "";
                    Control control = conList.Find(ctl => ctl.Name == ((Control)sender).Name);
                    int i = Convert.ToInt32(control.Name.Split('|')[control.Name.Split('|').Length - 1]);//这是ANDON类型中第几个ANDON类型，主要通过这个参数来确定ANDON的类型，原理是：将button的name按照“|”进行分割，最后一个即是i
                    List<Control> selectControls = beforeAndonButton[i];
                    if (selectControls.Count > 0) //如果selectcontrols.Count=0说明，selectcontrols里面没数据，即没有选中需要提交的andon
                    {
                        for (int t = 0; t < selectControls.Count;)//每条ANDON数据单独用一个事务处理
                        {
                            DbTransaction tran = new Repository<P_KEY_PART_INFOR>().BeginTrans();//事务开始
                            Button selectbutton = (Button)selectControls[t];
                            P_ANDON_INFOR p_andon_infor = ButtonToEntity(selectbutton);
                            int IsOk = 1;
                            if (andonTypeTable[i].ANDON_TYPE_NAME.ToString().Contains("物料"))//如果是物料andon，需要另外插入物料andon记录
                            {
                                int quantity = selectbutton.Tag == null ? 0 : Convert.ToInt32(selectbutton.Tag.ToString().Split('|')[3]);//首先判断tag是否为空，为空则返回0，否则，再判断 配送数量+线边库数量是否大于线边最大库存，是：则返回最大库存-线边数量，否：返回配送数量
                                //DataTable andon_part = DbHelperSQL.OpenTable("SELECT [part_key],[part_code],[part_name],[part_type],[part_category],[part_model_no],[part_draw_no],[part_struct_no],[part_abb],[part_unit],[revise_time],[hasconfiged],[part_variety]  FROM[JAC_FrontAxle].[dbo].[PART] where part_key='" + p_andon_infor.ANDON_INFO_KEY + "'");
                                //PART andon_part = PARTRepositoryFactory.Repository().FindEntity(p_andon_infor.ANDON_INFO_KEY);
                                //if ((!(andon_part.Rows[0]["part_variety"] is DBNull) && (andon_part.Rows[0]["part_variety"].ToString().Contains("制动器") || andon_part.Rows[0]["part_variety"].ToString().Contains("制动钳"))) && (wc_code == "QK-OP-A090-R" || wc_code == "QK-OP-A090-L" || wc_code == "QK-OP-A120-R" || wc_code == "QK-OP-A120-L" || wc_code == "ZK-OP-A090-R" || wc_code == "ZK-OP-A090-L" || wc_code == "ZK-OP-A120-R" || wc_code == "ZK-OP-A120-L"))
                                //{
                                //    bool is_exist = false;
                                //    AgvForm agvform = CreateAGVForm.GetAGVForm(wc_code, andon_part.Rows[0]["part_code"] is DBNull ? "" : andon_part.Rows[0]["part_code"].ToString().Trim(), out is_exist);
                                //    if (is_exist == false)//新开的AGV呼叫界面，需要绑定事件
                                //    {
                                //        agvform.FormClosed += new FormClosedEventHandler((sender2, e2) =>
                                //        {
                                //            IsOk *= agvform.IsSendSuccess;
                                //            agvform.Close();
                                //        });
                                //    }
                                //    if (!agvform.InvokeRequired)
                                //    {
                                //        agvform.ShowDialog();
                                //    }
                                //    else
                                //    {
                                //        IntPtr intptr = this.Handle;//获取句柄
                                //        this.Invoke(new Action(() => { agvform.Visible = false; agvform.ShowDialog(); }));
                                //    }
                                //    if (agvform.IsSend == false)
                                //    {
                                //        //return;
                                //        t++;
                                //        tran.Rollback();
                                //        continue;
                                //    }
                                //    if (IsOk > 0)//向AGV数据库发送数据成功
                                //    {
                                //        IsOk *= InsertAndonMaterialNeed(p_andon_infor, quantity, tran);
                                //    }
                                //    else//插入AGV信息失败，记录失败的andon项信息
                                //    {
                                //        message += selectbutton.Text + "呼叫失败。可能的原因是：AGV数据库连接出现问题。\n";
                                //        t++;
                                //        tran.Rollback();
                                //        continue;
                                //    }
                                //}
                                //else//不需要插入AGV信息
                                //{
                                    IsOk *= InsertAndonMaterialNeed(p_andon_infor, quantity, tran);
                                //}
                            }

                            #region 插入ANDON呼叫的数据

                            if (IsOk > 0)//成功插入ANDON物料需求
                            {
                                IsOk *= P_ANDON_INFORRepositoryFactory.Repository().Insert(p_andon_infor, tran);
                                tran.Commit();
                                Task.Run(() =>
                                {
                                    WebApiHttp.InsertPushInfo(p_andon_infor.ANDON_INFOR_KEY, p_andon_infor.GetType().Name);//传入参数为带推送信息主键和信息所在表名(实体名称)
                                }
                                );
                            }
                            else//插入andon物料需求失败，记录失败的andon项信息
                            {
                                message += selectbutton.Text + "呼叫失败。可能的原因是：数据库连接出现问题。\n";
                                t++;
                                tran.Rollback();
                                continue;
                            }
                            #endregion

                            if (IsOk != 0)
                            {
                                selectControls[t].BackColor = Color.Red;
                                selectControls.Remove(selectControls[t]);
                            }
                            else//插入andon信息失败，记录失败的andon项信息
                            {
                                message += selectbutton.Text + "呼叫失败。可能的原因是：数据库连接出现问题。\n";
                                t++;
                                continue;
                            }
                        }
                        GetAndonButton();
                    }
                    if (!string.IsNullOrWhiteSpace(message))//一旦呼叫失败
                    {
                        MessageBox.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("网络异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                //tran.Rollback();
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("andon呼叫失败！" + ex.Message);
            }
        }
        private int InsertAndonMaterialNeed(P_ANDON_INFOR p_andon_infor, int quantity, DbTransaction tran)
        {
            try
            {
                P_ANDON_MATERIAL_NEED p_andon_material_need = p_andon_infor.EntityMapper<P_ANDON_INFOR, P_ANDON_MATERIAL_NEED>();
                p_andon_material_need.Create();
                p_andon_material_need.PART_KEY = p_andon_material_need.ANDON_INFO_KEY;
                p_andon_material_need.PART_CODE = p_andon_material_need.ANDON_INFO_CODE;
                p_andon_material_need.PART_NAME = p_andon_material_need.ANDON_INFO_NAME;
                p_andon_material_need.QUANTITY = quantity;
                int IsOk = P_ANDON_MATERIAL_NEEDRepositoryFactory.Repository().Insert(p_andon_material_need, tran);
                return IsOk;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("andon呼叫失败！" + ex.Message);
                return 0;
            }
        }

        private void ResetAndon_Click(object sender, EventArgs e)
        {
            try
            {
                if (ServerCommunicationState)
                {
                    Control control = conList.Find(ctl => ctl.Name == ((Control)sender).Name);
                    int i = Convert.ToInt32(control.Name.Split('|')[control.Name.Split('|').Length - 1]);//这是ANDON类型中第几个ANDON类型，主要通过这个参数来确定ANDON的类型，原理是：将button的name按照“|”进行分割，最后一个即是i
                    List<Control> selectControls = beforeResetAndonButton[i];
                    if (selectControls.Count > 0) //如果selectcontrols.Count=0说明，selectcontrols里面没数据，即没有选中需要提交重置的andon
                    {
                        for (int t = 0; t < selectControls.Count;)
                        {
                            DbTransaction tran = new Repository<P_KEY_PART_INFOR>().BeginTrans();//事务开始
                            int IsOk = 1;
                            P_ANDON_INFOR p_andon_infor = ButtonToEntity((Button)selectControls[t]);
                            DOC_ANDON_INFOR doc_andon_infor = EntityHelper.EntityHelper.EntityMapper<P_ANDON_INFOR, DOC_ANDON_INFOR>(p_andon_infor);
                            doc_andon_infor.END_TIME = ServerTime.Now;
                            doc_andon_infor.CONTINUED_TIME = ExecDateDiff(Convert.ToDateTime(doc_andon_infor.END_TIME), Convert.ToDateTime(doc_andon_infor.START_TIME));
                            if (andonTypeTable[i].ANDON_TYPE_NAME.ToString().Contains("物料"))//如果是物料andon，需要另外处理物料andon记录
                            {
                                List<P_ANDON_MATERIAL_NEED> materialandoninfor = AndonGetData.GetMaterialAndonInfor(wc_key);
                                P_ANDON_MATERIAL_NEED p_andon_material_need = materialandoninfor.Find(s => s.ANDON_INFO_KEY == p_andon_infor.ANDON_INFO_KEY);
                                if (p_andon_material_need != null)
                                {
                                    DOC_ANDON_MATERIAL_NEED doc_andon_material_need = p_andon_material_need.EntityMapper<P_ANDON_MATERIAL_NEED, DOC_ANDON_MATERIAL_NEED>();
                                    doc_andon_material_need.END_TIME = doc_andon_infor.END_TIME;
                                    doc_andon_material_need.CONTINUED_TIME = doc_andon_infor.CONTINUED_TIME;
                                    IsOk *= DOC_ANDON_MATERIAL_NEEDRepositoryFactory.Repository().Insert(doc_andon_material_need, tran);
                                    IsOk *= P_ANDON_MATERIAL_NEEDRepositoryFactory.Repository().Delete(p_andon_material_need.ANDON_MATERIAL_NEED_KEY,tran);

                                    #region 更新线边库数量
                                    //MATERIAL_WC_PART material_wc_part = KeyPartGetData.GetMaterialWcPart(wc_key, p_andon_material_need.PART_KEY);//获取线边库信息
                                    //material_wc_part.STORAGE_NUM += p_andon_material_need.QUANTITY;
                                    //IsOk *= MATERIAL_WC_PARTRepositoryFactory.Repository().Update(material_wc_part, tran);
                                    #endregion

                                    #region 更新订单物料需求

                                    #region 检索语句
//                                    DataTable material_order_requirement_dt = DbHelperSQL.OpenTable(
//@"select distinct mes_plan_key,requirement.mes_plan_code,[material_order_requirement_key],total_demand_num,sum_delivery_num from (
//                                                    SELECT top 1000000 [mes_plan_key]
//                                                                      ,[mes_plan_code]
//                                                                      ,[assemble_online_time]
//                                                                  FROM [JAC_FrontAxle].[dbo].[P_ASSEMBLE_PRODUCT_STATE] order by  assemble_online_time asc
//												    ) on_line_queue
//join (
//      SELECT [order_code] as mes_plan_code,total_demand_num,sum_delivery_num,[material_order_requirement_key] FROM [JAC_FrontAxle].[dbo].[Material_Order_Requirement] 
//               where  total_demand_num>sum_delivery_num and wc_key='" + wc_key + "' and part_key='" + p_andon_material_need.PART_KEY + @"'
//      )requirement
//on requirement.mes_plan_code=on_line_queue.mes_plan_code");
                                    #endregion

                                    int? quantity = p_andon_material_need.QUANTITY;
                                    //for (int k = 0; k < material_order_requirement_dt.Rows.Count; k++)
                                    //{
                                    //    Material_Order_Requirement material_order_requirement = new Material_Order_Requirement();
                                    //    //本次配送数量与已配送数量之和，仍小于总配送数量
                                    //    if (quantity + Convert.ToInt16(material_order_requirement_dt.Rows[k]["sum_delivery_num"]) <= Convert.ToInt16(material_order_requirement_dt.Rows[k]["total_demand_num"]))
                                    //    {
                                    //        material_order_requirement = EntityHelper<Material_Order_Requirement>.GetEntity(AndonGetData.GetMaterialOrderRequirementInfor(material_order_requirement_dt.Rows[k]["material_order_requirement_key"].ToString()));
                                    //        material_order_requirement.sum_delivery_num = (Convert.ToInt16(material_order_requirement.sum_delivery_num) + quantity).ToString();
                                    //        break;
                                    //    }
                                    //    else//本次配送数量与已配送数量之和，大于总配送数量
                                    //    {
                                    //        material_order_requirement = EntityHelper<Material_Order_Requirement>.GetEntity(AndonGetData.GetMaterialOrderRequirementInfor(material_order_requirement_dt.Rows[k]["material_order_requirement_key"].ToString()));
                                    //        material_order_requirement.sum_delivery_num = material_order_requirement.total_demand_num;//配送数量等于总数量
                                    //        quantity -= Convert.ToInt16(material_order_requirement.total_demand_num) - Convert.ToInt16(material_order_requirement.sum_delivery_num);
                                    //    }
                                    //    IsOk *= dbop.Update(material_order_requirement, tran);
                                    //}
                                    #endregion
                                }
                            }
                            IsOk *= DOC_ANDON_INFORRepositoryFactory.Repository().Insert(doc_andon_infor,tran);
                            IsOk *= P_ANDON_INFORRepositoryFactory.Repository().Delete(p_andon_infor.ANDON_INFOR_KEY,tran);
                            if (IsOk != 0)
                            {
                                tran.Commit();//删除P_ANDON_INFOR中的相应数据成功后，提交事务
                                selectControls[t].BackColor = SystemColors.Control;
                                selectControls.Remove(selectControls[t]);
                            }
                            else
                            {
                                tran.Rollback();//删除P_ANDON_INFOR中的相应数据失败，回滚事务
                                t++;
                            }
                        }
                        GetAndonButton();
                    }
                }
                else
                {
                    MessageBox.Show("网络异常", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                //tran.Rollback();//出现异常，回滚事务
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("重置andon呼叫失败！" + ex.Message);
            }
        }
        #endregion

        #region 获取容器内控件信息和窗体内控件信息

        //获取容器控件内的控件
        public void GetAllControls(Control control)
        {
            foreach (Control con in control.Controls)
            {
                if (con.Controls.Count > 0)
                {
                    GetAllControls(con);
                }
                conList.Add(con);
                //声明一个数组在这里接收一下con就可以了
            }
        }
        //获取窗体内的控件
        public void GetAllControls(Form control)
        {
            foreach (Control con in control.Controls)
            {
                if (con.Controls.Count > 0)
                {
                    GetAllControls(con);
                }
                conList.Add(con);
                //声明一个数组在这里接收一下con就可以了
            }
        }
        #endregion

        #region 显示服务器通讯信号
        private bool ServerCommunicationState = false;//服务器通讯状态

        private static string ServerIP = DBhelperOracle.dataSource;//服务器的IP地址

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
                        //ShowServerConnection(false);
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
            ShowServerConnectionState();
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
        //    if (state)
        //    {
        //        SetLblColors(serverconnectionstate_lbl, Color.LawnGreen);
        //    }
        //    else
        //    {
        //        SetLblColors(serverconnectionstate_lbl, Color.Red); ;
        //    }
        //}
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

        #region 重绘窗体

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            if (((Control)sender).Name.ToString().Substring(((Control)sender).Name.ToString().Length - 1) != (andonTypeTable.Count - 1).ToString())
            {
                Pen pen = new Pen(Color.LightSteelBlue, 2);
                e.Graphics.Clear(((Control)sender).BackColor);
                e.Graphics.DrawString(((Control)sender).Text, ((Control)sender).Font, Brushes.Blue, 10, 1);
                e.Graphics.DrawLine(pen, 1, 7, 8, 7);
                //e.Graphics.DrawLine(pen, ((Control)sender).Width - 2, 7, ((Control)sender).Width - 2, ((Control)sender).Height - 2);
                e.Graphics.DrawLine(pen, e.Graphics.MeasureString(((Control)sender).Text, ((Control)sender).Font).Width + 8, 7, ((Control)sender).Width - 2, 7);
                //e.Graphics.DrawLine(pen, 1, 7, 1, ((Control)sender).Height - 2);
                //e.Graphics.DrawLine(pen, 1, ((Control)sender).Height - 2, ((Control)sender).Width - 2, ((Control)sender).Height - 2);
                //e.Graphics.DrawLine(pen, ((Control)sender).Width - 2, 7, ((Control)sender).Width - 2, ((Control)sender).Height - 2);
            }
            else
            {
                Pen pen = new Pen(SystemColors.Control, 2);
                e.Graphics.Clear(((Control)sender).BackColor);
                e.Graphics.DrawString(((Control)sender).Text, ((Control)sender).Font, Brushes.Blue, 10, 1);
                e.Graphics.DrawLine(pen, 1, 7, 8, 7);
                //e.Graphics.DrawLine(pen, ((Control)sender).Width - 2, 7, ((Control)sender).Width - 2, ((Control)sender).Height - 2);
                e.Graphics.DrawLine(pen, e.Graphics.MeasureString(((Control)sender).Text, ((Control)sender).Font).Width + 8, 7, ((Control)sender).Width - 2, 7);
                //e.Graphics.DrawLine(pen, 1, 7, 1, ((Control)sender).Height - 2);
                //e.Graphics.DrawLine(pen, 1, ((Control)sender).Height - 2, ((Control)sender).Width - 2, ((Control)sender).Height - 2);
                //e.Graphics.DrawLine(pen, ((Control)sender).Width - 2, 7, ((Control)sender).Width - 2, ((Control)sender).Height - 2);
            }
        }
        //private void Paint()
        //{
        //    Pen pen = new Pen(Color.Blue, 5);
        //    e.Graphics.Clear(((Control)sender).BackColor);
        //    e.Graphics.DrawString(((Control)sender).Text, ((Control)sender).Font, Brushes.Blue, 10, 1);
        //    e.Graphics.DrawLine(pen, 1, 7, 8, 7);
        //    e.Graphics.DrawLine(pen, e.Graphics.MeasureString(((Control)sender).Text, ((Control)sender).Font).Width + 8, 7, ((Control)sender).Width - 2, 7);
        //    e.Graphics.DrawLine(pen, 1, 7, 1, ((Control)sender).Height - 2);
        //    e.Graphics.DrawLine(pen, 1, ((Control)sender).Height - 2, ((Control)sender).Width - 2, ((Control)sender).Height - 2);
        //    e.Graphics.DrawLine(pen, ((Control)sender).Width - 2, 7, ((Control)sender).Width - 2, ((Control)sender).Height - 2);
        //}
        #endregion

        #region 发送AGV信息

        public int SendMessageToAgv(T_ORDER_INFO cc)
        {
            cc.GETEMP_ID = 2;
            int c = new DataBaseOpByEntity().InsertToOtherDataBase(cc, "Server=192.169.14.66;Initial Catalog=AGV;User ID=sa;Password=agv");
            return c;
        }
        #endregion

        private void Exit_pic_Box_Click(object sender, EventArgs e)
        {
            this.Close();
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

        #region 点击Label移动窗体
        private void prompt_info_lbl_MouseDown(object sender, MouseEventArgs e)
        {
            downPoint = new Point(e.X, e.Y);
        }

        private void prompt_info_lbl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - downPoint.X,
                    this.Location.Y + e.Y - downPoint.Y);
            }
        }

        private void main_station_lbl_MouseDown(object sender, MouseEventArgs e)
        {
            downPoint = new Point(e.X, e.Y);
        }

        private void main_station_lbl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + e.X - downPoint.X,
                    this.Location.Y + e.Y - downPoint.Y);
            }
        }
        #endregion
    }

    class EntityToEntity<TEntity, T> where TEntity : new()
    {
        #region 通过反射的方式将一个类型转化为另一个类型
        /// <summary>
        /// 将t实体转化为tentity实体
        /// </summary>
        /// <param name="tentity"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static TEntity EntityChange(TEntity tentity, T t)
        {
            Type type = t.GetType();//获得p_andon_infor的类型type
            PropertyInfo[] PropertyList = type.GetProperties();//获得类型type的所有属性集合
            foreach (PropertyInfo item in PropertyList)//变量属性集合
            {
                string name = item.Name;//属性名
                object value = item.GetValue(t);//属性值
                PropertyInfo propertyInfo = tentity.GetType().GetProperty(name);//获得p_andon_material_need存在属性name，则返回属性name，不存在则返回null，可调试验证
                if (propertyInfo != null && value != DBNull.Value)
                    propertyInfo.SetValue(tentity, value);//对p_andon_material_need的propertyInfo属性赋值value
            }
            return tentity;
        }
        /// <summary>
        /// 将t实体转化为TEntity类型的实体并返回
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static TEntity EntityChange(T t)
        {
            TEntity tentity = new TEntity();
            Type type = t.GetType();//获得p_andon_infor的类型type
            PropertyInfo[] PropertyList = type.GetProperties();//获得类型type的所有属性集合
            foreach (PropertyInfo item in PropertyList)//变量属性集合
            {
                string name = item.Name;//属性名
                object value = item.GetValue(t);//属性值
                PropertyInfo propertyInfo = tentity.GetType().GetProperty(name);//获得p_andon_material_need存在属性name，则返回属性name，不存在则返回null，可调试验证
                if (propertyInfo != null && value != DBNull.Value)
                    propertyInfo.SetValue(tentity, value);//对p_andon_material_need的propertyInfo属性赋值value
            }
            return tentity;
        }
        #endregion
    }
}
