using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Text;

namespace MsgBox
{
    public partial class MyMsgBox : Form
    {
        static System.Windows.Forms.Timer timer1;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool MessageBeep(uint type);

        [DllImport("Shell32.dll")]
        public extern static int ExtractIconEx(string libName, int iconIndex, IntPtr[] largeIcon, IntPtr[] smallIcon, int nIcons);

        static private IntPtr[] largeIcon;
        static private IntPtr[] smallIcon;

        static private MyMsgBox newMessageBox;
        static private Panel frmTitlePanel;
        static private Label frmTitle;
        static private PictureBox closePicture;
        static private Label frmMessage;
        static private PictureBox pIcon;
        static private FlowLayoutPanel flpButtons;
        static private Icon frmIcon;
        static private Image frmImage;

        static private Button btnOK;
        static private Button btnAbort;
        static private Button btnRetry;
        static private Button btnIgnore;
        static private Button btnCancel;
        static private Button btnYes;
        static private Button btnNo;

        static private DialogResult CYReturnButton;

        static int delaytime;
        static bool IsClosed = false;
        static readonly object locker = new object();

        public enum MyIcon
        {
            Error,
            Explorer,
            Find,
            Information,
            Mail,
            Media,
            Print,
            Question,
            RecycleBinEmpty,
            RecycleBinFull,
            Stop,
            User,
            Warning
        }

        public enum MyButtons
        {
            AbortRetryIgnore,
            OK,
            OKCancel,
            RetryCancel,
            YesNo,
            YesNoCancel
        }

        static private void BuildMessageBox(string title)
        {
            newMessageBox = new MyMsgBox();
            newMessageBox.Text = title;
            newMessageBox.Size = new System.Drawing.Size(300, 150);
            newMessageBox.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            newMessageBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            newMessageBox.Paint += new PaintEventHandler(newMessageBox_Paint);
            newMessageBox.BackColor = System.Drawing.Color.White;

            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.RowCount = 3;
            tlp.ColumnCount = 0;
            tlp.Dock = System.Windows.Forms.DockStyle.Fill;
            tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22));
            tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50));
            tlp.BackColor = System.Drawing.Color.Transparent;
            //tlp.BackColor = System.Drawing.Color.Gray;
            tlp.Padding = new Padding(0, 0, 0, 0);

            frmTitlePanel = new Panel();//页面顶端Panel
            //frmTitlePanel.Size = new Size(300,70);
            frmTitlePanel.Dock = DockStyle.Fill;
            frmTitlePanel.BackColor = System.Drawing.Color.Transparent;
            frmTitlePanel.MouseDown += new MouseEventHandler(frmTitlePanel_MouseDown);
            frmTitlePanel.MouseMove += new MouseEventHandler(frmTitlePanel_MouseMove);

            frmTitle = new Label();
            frmTitle.Dock = DockStyle.Left;
            frmTitle.Size=new Size(200, 20);
            frmTitle.BackColor = System.Drawing.Color.Transparent;
            //frmTitle.BackColor = System.Drawing.Color.Gray;
            frmTitle.ForeColor = System.Drawing.Color.White;
            frmTitle.Font = new Font("Tahoma", 9, FontStyle.Bold);
            frmTitle.MouseDown += new MouseEventHandler(frmTitle_MouseDown);
            frmTitle.MouseMove += new MouseEventHandler(frmTitle_MouseMove);

            closePicture = new PictureBox();
            closePicture.Image = Image.FromFile(Application.StartupPath + "\\Resources\\叉1.png");
            closePicture.Size = new System.Drawing.Size(18, 18);
            closePicture.SizeMode = PictureBoxSizeMode.Zoom;
            closePicture.Click += new EventHandler(closepic_Click);
            closePicture.Dock = DockStyle.Right;
            frmTitlePanel.Controls.Add(frmTitle);
            frmTitlePanel.Controls.Add(closePicture);

            frmMessage = new Label();
            frmMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            frmMessage.BackColor = System.Drawing.Color.White;
            frmMessage.Font = new Font("Tahoma", 9, FontStyle.Regular);
            frmMessage.Text = "hiii";

            largeIcon = new IntPtr[250];
            smallIcon = new IntPtr[250];
            pIcon = new PictureBox();
            ExtractIconEx("shell32.dll", 0, largeIcon, smallIcon, 250);

            flpButtons = new FlowLayoutPanel();
            flpButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            flpButtons.Padding = new Padding(0, 5, 5, 0);
            flpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            flpButtons.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

            TableLayoutPanel tlpMessagePanel = new TableLayoutPanel();
            tlpMessagePanel.BackColor = System.Drawing.Color.White;
            tlpMessagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            tlpMessagePanel.ColumnCount = 2;
            tlpMessagePanel.RowCount = 0;
            tlpMessagePanel.Padding = new Padding(4, 5, 4, 4);
            tlpMessagePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50));
            tlpMessagePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tlpMessagePanel.Controls.Add(pIcon);
            tlpMessagePanel.Controls.Add(frmMessage);

            tlp.Controls.Add(frmTitlePanel);
            tlp.Controls.Add(tlpMessagePanel);
            tlp.Controls.Add(flpButtons);
            newMessageBox.Controls.Add(tlp);
        }

        /// <summary>
        /// Message: Text to display in the message box.
        /// </summary>
        static public DialogResult Show(string Message)
        {
            lock (locker)
            {
                BuildMessageBox("");
                frmMessage.Text = Message;
                AutoSizeControl(frmMessage);//根据显示信息长度自动调整Form窗体大小
                ShowOKButton();
                newMessageBox.StartPosition = FormStartPosition.CenterScreen;
                newMessageBox.ShowDialog();
                return CYReturnButton;
            }
        }

        /// <summary>
        /// Title: Text to display in the title bar of the messagebox.
        /// </summary>
        static public DialogResult Show(string Message, string Title)
        {
            lock (locker)
            {
                BuildMessageBox(Title);
                frmTitle.Text = Title;
                frmMessage.Text = Message;
                AutoSizeControl(frmMessage);//根据显示信息长度自动调整Form窗体大小
                ShowOKButton();
                newMessageBox.StartPosition = FormStartPosition.CenterScreen;
                newMessageBox.ShowDialog();
                return CYReturnButton;
            }
        }

        /// <summary>
        /// MButtons: Display MyButtons on the message box.
        /// </summary>
        static public DialogResult Show(string Message, string Title, MyButtons MButtons)
        {
            lock (locker)
            {
                BuildMessageBox(Title); // BuildMessageBox method, responsible for creating the MessageBox
                frmTitle.Text = Title; // Set the title of the MessageBox
                frmMessage.Text = Message; //Set the text of the MessageBox
                AutoSizeControl(frmMessage);//根据显示信息长度自动调整Form窗体大小
                ButtonStatements(MButtons); // ButtonStatements method is responsible for showing the appropreiate buttons
                newMessageBox.StartPosition = FormStartPosition.CenterScreen;
                newMessageBox.ShowDialog(); // Show the MessageBox as a Dialog.
                return CYReturnButton; // Return the button click as an Enumerator
            }
        }

        /// <summary>
        /// MIcon: Display MyIcon on the message box.
        /// </summary>
        static public DialogResult Show(string Message, string Title, MyButtons MButtons, MyIcon MIcon)
        {
            lock (locker)
            {
                //IsClosed = false;//关闭状态重置
                BuildMessageBox(Title);
                frmTitle.Text = Title;
                frmMessage.Text = Message;
                AutoSizeControl(frmMessage);//根据显示信息长度自动调整Form窗体大小
                ButtonStatements(MButtons);
                IconStatements(MIcon);
                //Image imageIcon = new Bitmap(frmIcon.ToBitmap(), 38, 38);
                pIcon.Image = frmImage;
                newMessageBox.ShowInTaskbar = false;
                newMessageBox.StartPosition = FormStartPosition.CenterScreen;
                //FormShowDialog(newMessageBox);
                newMessageBox.ShowDialog();
                return CYReturnButton;
            }
        }
        /// <summary>
        /// Message: Text to display in the message box.
        /// </summary>
        static public DialogResult Show(string Message,int Delaytime)
        {
            lock (locker)
            {
                // IsClosed = false;//关闭状态重置
                delaytime = Delaytime;
                BuildMessageBox("");
                frmTitle.Text = "(" + delaytime.ToString() + "秒后自动关闭)";
                frmMessage.Text = Message;
                AutoSizeControl(frmMessage);//根据显示信息长度自动调整Form窗体大小
                ShowOKButton();
                newMessageBox.StartPosition = FormStartPosition.CenterScreen;
                delaytime = Delaytime;
                TimeClose(delaytime);
                newMessageBox.ShowDialog();
                return CYReturnButton;
            }
        }

        /// <summary>
        /// Title: Text to display in the title bar of the messagebox.
        /// </summary>
        static public DialogResult Show(string Message, string Title, int Delaytime)
        {
            lock (locker)
            {
                // IsClosed = false;//关闭状态重置
                delaytime = Delaytime;
                BuildMessageBox(Title);
                frmTitle.Text = Title + "(" + delaytime.ToString() + "秒后自动关闭)";
                frmMessage.Text = Message;
                AutoSizeControl(frmMessage);//根据显示信息长度自动调整Form窗体大小
                ShowOKButton();
                newMessageBox.StartPosition = FormStartPosition.CenterScreen;
                TimeClose(delaytime);
                newMessageBox.ShowDialog();
                return CYReturnButton;
            }
        }

        /// <summary>
        /// MButtons: Display MyButtons on the message box.
        /// </summary>
        static public DialogResult Show(string Message, string Title, MyButtons MButtons, int Delaytime)
        {
            lock (locker)
            {
                // IsClosed = false;//关闭状态重置
                delaytime = Delaytime;
                BuildMessageBox(Title); // BuildMessageBox method, responsible for creating the MessageBox
                frmTitle.Text = Title + "(" + delaytime.ToString() + "秒后自动关闭)"; // Set the title of the MessageBox
                frmMessage.Text = Message; //Set the text of the MessageBox
                AutoSizeControl(frmMessage);//根据显示信息长度自动调整Form窗体大小
                ButtonStatements(MButtons); // ButtonStatements method is responsible for showing the appropreiate buttons
                newMessageBox.StartPosition = FormStartPosition.CenterScreen;
                TimeClose(delaytime);
                newMessageBox.ShowDialog(); // Show the MessageBox as a Dialog.
                return CYReturnButton; // Return the button click as an Enumerator
            }
        }

        /// <summary>
        /// MIcon: Display MyIcon on the message box.
        /// </summary>
        static public DialogResult Show(string Message, string Title, MyButtons MButtons, MyIcon MIcon, int Delaytime)
        {
            lock (locker)
            {
                IsClosed = false;//关闭状态重置
                delaytime = Delaytime;
                BuildMessageBox(Title);
                frmTitle.Text = Title + "(" + delaytime.ToString() + "秒后自动关闭)";
                frmMessage.Text = Message;
                AutoSizeControl(frmMessage);//根据显示信息长度自动调整Form窗体大小
                ButtonStatements(MButtons);
                IconStatements(MIcon);
                pIcon.Image = frmImage;
                newMessageBox.ShowInTaskbar = false;
                newMessageBox.StartPosition = FormStartPosition.CenterScreen;
                TimeClose(delaytime);
                newMessageBox.ShowDialog();
                return CYReturnButton;
            }
        }

        static void btnOK_Click(object sender, EventArgs e)
        {
            CYReturnButton = DialogResult.OK;
            ControlDispose();
        }

        static void btnAbort_Click(object sender, EventArgs e)
        {
            CYReturnButton = DialogResult.Abort;
            ControlDispose();
        }

        static void btnRetry_Click(object sender, EventArgs e)
        {
            CYReturnButton = DialogResult.Retry;
            ControlDispose();
        }

        static void btnIgnore_Click(object sender, EventArgs e)
        {
            CYReturnButton = DialogResult.Ignore;
            ControlDispose();
        }

        static void btnCancel_Click(object sender, EventArgs e)
        {
            CYReturnButton = DialogResult.Cancel;
            ControlDispose();
        }

        static void btnYes_Click(object sender, EventArgs e)
        {
            CYReturnButton = DialogResult.Yes;
            ControlDispose();
        }

        static void btnNo_Click(object sender, EventArgs e)
        {
            CYReturnButton = DialogResult.No;
            ControlDispose();
        }
        static void ControlDispose()
        {
            if (timer1 != null)
            {
                timer1.Dispose();
            }
            SetControlClose(newMessageBox);
            //newMessageBox.Close();
            //IsClosed = true;
        }

        static private void ShowOKButton()
        {
            btnOK = new Button();
            btnOK.Text = "确定";
            btnOK.Size = new System.Drawing.Size(80, 25);
            btnOK.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            btnOK.Font = new Font("Tahoma", 8, FontStyle.Regular);
            btnOK.FlatStyle = FlatStyle.Flat;
            btnOK.FlatAppearance.BorderColor = Color.FromArgb(5, 90, 150);
            btnOK.Click += new EventHandler(btnOK_Click);
            flpButtons.Controls.Add(btnOK);
        }

        static private void ShowAbortButton()
        {
            btnAbort = new Button();
            btnAbort.Text = "退出";
            btnAbort.Size = new System.Drawing.Size(80, 25);
            btnAbort.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            btnAbort.Font = new Font("Tahoma", 8, FontStyle.Regular);
            btnAbort.FlatStyle = FlatStyle.Flat;
            btnAbort.FlatAppearance.BorderColor = Color.FromArgb(5, 90, 150);
            btnAbort.Click += new EventHandler(btnAbort_Click);
            flpButtons.Controls.Add(btnAbort);
        }

        static private void ShowRetryButton()
        {
            btnRetry = new Button();
            btnRetry.Text = "重试";
            btnRetry.Size = new System.Drawing.Size(80, 25);
            btnRetry.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            btnRetry.Font = new Font("Tahoma", 8, FontStyle.Regular);
            btnRetry.FlatStyle = FlatStyle.Flat;
            btnRetry.FlatAppearance.BorderColor = Color.FromArgb(5, 90, 150);
            btnRetry.Click += new EventHandler(btnRetry_Click);
            flpButtons.Controls.Add(btnRetry);
        }

        static private void ShowIgnoreButton()
        {
            btnIgnore = new Button();
            btnIgnore.Text = "忽略";
            btnIgnore.Size = new System.Drawing.Size(80, 25);
            btnIgnore.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            btnIgnore.Font = new Font("Tahoma", 8, FontStyle.Regular);
            btnIgnore.FlatStyle = FlatStyle.Flat;
            btnIgnore.FlatAppearance.BorderColor = Color.FromArgb(5, 90, 150);
            btnIgnore.Click += new EventHandler(btnIgnore_Click);
            flpButtons.Controls.Add(btnIgnore);
        }

        static private void ShowCancelButton()
        {
            btnCancel = new Button();
            btnCancel.Text = "取消";
            btnCancel.Size = new System.Drawing.Size(80, 25);
            btnCancel.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            btnCancel.Font = new Font("Tahoma", 8, FontStyle.Regular);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(5, 90, 150);
            btnCancel.Click += new EventHandler(btnCancel_Click);
            flpButtons.Controls.Add(btnCancel);
        }

        static private void ShowYesButton()
        {
            btnYes = new Button();
            btnYes.Text = "是";
            btnYes.Size = new System.Drawing.Size(80, 25);
            btnYes.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            btnYes.Font = new Font("Tahoma", 8, FontStyle.Regular);
            btnYes.FlatStyle = FlatStyle.Flat;
            btnYes.FlatAppearance.BorderColor = Color.FromArgb(5, 90, 150);
            btnYes.Click += new EventHandler(btnYes_Click);
            flpButtons.Controls.Add(btnYes);
        }

        static private void ShowNoButton()
        {
            btnNo = new Button();
            btnNo.Text = "否";
            btnNo.Size = new System.Drawing.Size(80, 25);
            btnNo.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
            btnNo.Font = new Font("Tahoma", 8, FontStyle.Regular);
            btnNo.FlatStyle = FlatStyle.Flat;
            btnNo.FlatAppearance.BorderColor = Color.FromArgb(5, 90, 150);
            btnNo.Click += new EventHandler(btnNo_Click);
            flpButtons.Controls.Add(btnNo);
        }

        static private void ButtonStatements(MyButtons MButtons)
        {
            if (MButtons == MyButtons.AbortRetryIgnore)
            {
                ShowIgnoreButton();
                ShowRetryButton();
                ShowAbortButton();
            }

            if (MButtons == MyButtons.OK)
            {
                ShowOKButton();
            }

            if (MButtons == MyButtons.OKCancel)
            {
                ShowCancelButton();
                ShowOKButton();
            }

            if (MButtons == MyButtons.RetryCancel)
            {
                ShowCancelButton();
                ShowRetryButton();
            }

            if (MButtons == MyButtons.YesNo)
            {
                ShowNoButton();
                ShowYesButton();
            }

            if (MButtons == MyButtons.YesNoCancel)
            {
                ShowCancelButton();
                ShowNoButton();
                ShowYesButton();
            }
        }

        static private void IconStatements(MyIcon MIcon)
        {
            if (MIcon == MyIcon.Error)
            {
                MessageBeep(30);
                frmImage = Image.FromFile(Application.StartupPath + "\\Resources\\Error.png");
                //frmIcon = Icon.FromHandle(largeIcon[109]);
            }

            if (MIcon == MyIcon.Explorer)
            {
                MessageBeep(0);
                //frmIcon = Icon.FromHandle(largeIcon[220]);
            }

            if (MIcon == MyIcon.Find)
            {
                MessageBeep(0);
                //frmIcon = Icon.FromHandle(largeIcon[22]);
            }

            if (MIcon == MyIcon.Information)
            {
                MessageBeep(0);
                frmImage = Image.FromFile(Application.StartupPath + "\\Resources\\Information.png");
                //frmIcon = Icon.FromHandle(largeIcon[221]);
            }

            if (MIcon == MyIcon.Mail)
            {
                MessageBeep(0);
                //frmIcon = Icon.FromHandle(largeIcon[156]);
            }

            if (MIcon == MyIcon.Media)
            {
                MessageBeep(0);
                //frmIcon = Icon.FromHandle(largeIcon[116]);
            }

            if (MIcon == MyIcon.Print)
            {
                MessageBeep(0);
                //frmIcon = Icon.FromHandle(largeIcon[136]);
            }

            if (MIcon == MyIcon.Question)
            {
                MessageBeep(0);
                frmImage = Image.FromFile(Application.StartupPath + "\\Resources\\Question.png");
                //frmIcon = Icon.FromHandle(largeIcon[23]);
            }

            if (MIcon == MyIcon.RecycleBinEmpty)
            {
                MessageBeep(0);
                //frmIcon = Icon.FromHandle(largeIcon[31]);
            }

            if (MIcon == MyIcon.RecycleBinFull)
            {
                MessageBeep(0);
                //frmIcon = Icon.FromHandle(largeIcon[32]);
            }

            if (MIcon == MyIcon.Stop)
            {
                MessageBeep(0);
                //frmIcon = Icon.FromHandle(largeIcon[27]);
            }

            if (MIcon == MyIcon.User)
            {
                MessageBeep(0);
                //frmIcon = Icon.FromHandle(largeIcon[170]);
            }

            if (MIcon == MyIcon.Warning)
            {
                MessageBeep(30);
                frmImage = Image.FromFile(Application.StartupPath + "\\Resources\\Warning.png");
                //frmIcon = Icon.FromHandle(largeIcon[217]);
            }
        }

        #region 绘制矩形(边框，线条)，颜色渐变
        static void newMessageBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle frmTitleL = new Rectangle(0, 0, (newMessageBox.Width / 2), 22);
            Rectangle frmTitleR = new Rectangle((newMessageBox.Width / 2), 0, (newMessageBox.Width / 2), 22);
            Rectangle frmMessageBox = new Rectangle(0, 0, (newMessageBox.Width - 1), (newMessageBox.Height - 1));
            //LinearGradientBrush frmLGBL = new LinearGradientBrush(frmTitleL, Color.FromArgb(87, 148, 160), Color.FromArgb(209, 230, 243), LinearGradientMode.Horizontal);
            //LinearGradientBrush frmLGBR = new LinearGradientBrush(frmTitleR, Color.FromArgb(209, 230, 243), Color.FromArgb(87, 148, 160), LinearGradientMode.Horizontal);
            LinearGradientBrush frmLGBL = new LinearGradientBrush(frmTitleL, Color.FromArgb(5, 90, 150), Color.FromArgb(5, 90, 150), LinearGradientMode.Horizontal);
            LinearGradientBrush frmLGBR = new LinearGradientBrush(frmTitleR, Color.FromArgb(5, 90, 150), Color.FromArgb(5, 90, 150), LinearGradientMode.Horizontal);
            //Pen frmPen = new Pen(Color.FromArgb(63, 119, 143), 1);
            Pen frmPen = new Pen(Color.FromArgb(5, 90, 150), 1);
            g.FillRectangle(frmLGBL, frmTitleL);
            g.FillRectangle(frmLGBR, frmTitleR);
            g.DrawRectangle(frmPen, frmMessageBox);
        }
        #endregion

        #region 关闭图片点击事件
        static void closepic_Click(object sender, EventArgs e)
        {
            ControlDispose();
        }
        #endregion

        #region 点击panel控件移动窗口
        static Point downPoint;
        private static void frmTitlePanel_MouseDown(object sender, MouseEventArgs e)
        {
            downPoint = new Point(e.X, e.Y);
        }

        private static void frmTitlePanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newMessageBox.Location = new Point(newMessageBox.Location.X + e.X - downPoint.X,
                    newMessageBox.Location.Y + e.Y - downPoint.Y);
            }
        }
        #endregion

        #region 点击Label控件移动窗口
        private static void frmTitle_MouseDown(object sender, MouseEventArgs e)
        {
            downPoint = new Point(e.X, e.Y);
        }

        private static void frmTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                newMessageBox.Location = new Point(newMessageBox.Location.X + e.X - downPoint.X,
                    newMessageBox.Location.Y + e.Y - downPoint.Y);
            }
        }
        #endregion

        public static void TimeClose(int time)
        {
            #region Timer刷(方案一:单线程)
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += timer1_Tick;
            timer1.Interval = 1000;
            timer1.Enabled = true;
            #endregion

            #region Task(方案二:多线程)
            //Stopwatch sw = new Stopwatch();
            //sw.Start();//启动计时器
            //long s = 0;
            //Task.Run(new Action(() =>
            //{
            //    while (true)
            //    {
            //        s = delaytime - (int)(sw.ElapsedMilliseconds / 1000);
            //        SetText(frmTitle, frmTitle.Text.Split('(')[0] + "(" + s.ToString() + "秒后自动关闭)");
            //        //Thread.Sleep(500);
            //        if (s<1||IsClosed)
            //        {
            //            SetControlClose(newMessageBox);
            //            break;
            //        }
            //    }
            //}));
            #endregion
        }

        private static void timer1_Tick(object sender, EventArgs e)
        {
            delaytime -= 1;
            frmTitle.Text = frmTitle.Text.Split('(')[0] + "(" + delaytime.ToString() + "秒后自动关闭)";
            if(delaytime <= 0)
            {
                //CYReturnButton = DialogResult.Yes;
                ControlDispose();
            }
        }
        /// <summary>
        /// 使用委托为组件赋值
        /// </summary>
        /// <param name="txt">控件名</param>
        /// <param name="st">要赋值的字符串</param>
        private delegate void SetTextDelegate(Control control, string st);
        private static void SetText(Control control, string st)
        {
            //string ID = Thread.CurrentThread.ManagedThreadId.ToString();//获取线程ID，可以进行跨线程调试
            if (control.InvokeRequired) control.Invoke(new SetTextDelegate(SetText), control, st);
            else control.Text = st;
        }
        /// <summary>
        /// 使用委托为组件赋值
        /// </summary>
        /// <param name="txt">控件名</param>
        /// <param name="st">要赋值的字符串</param>
        private delegate void SetControlCloseDelegate(Form control);
        private static void SetControlClose(Form control)
        {
            //string ID = Thread.CurrentThread.ManagedThreadId.ToString();//获取线程ID，可以进行跨线程调试
            if (control.InvokeRequired) control.Invoke(new SetControlCloseDelegate(SetControlClose),control);
            else control.Close();IsClosed = true;
        }

        private delegate void FormShowDialogDelegate(Form control);
        private static void FormShowDialog(Form control)
        {
            //string ID = Thread.CurrentThread.ManagedThreadId.ToString();//获取线程ID，可以进行跨线程调试
            if (control.InvokeRequired) control.Invoke(new FormShowDialogDelegate(FormShowDialog), control);
            else control.ShowDialog();
        }

        private static void AutoSizeControl(Control control)
        {
            // Create a Graphics object for the Control.
            Graphics g = control.CreateGraphics();

            // Get the Size needed to accommodate the formatted Text.
            Size preferredSize = g.MeasureString(
               control.Text, control.Font).ToSize();

            if (preferredSize.Width <= 955)
            {
                return;
            }
            else if(preferredSize.Width <= 2818)
            {
                newMessageBox.Size = new System.Drawing.Size(400, 200);
            }
            else
            {
                newMessageBox.Size = new System.Drawing.Size(500, 250);
            }
            // Clean up the Graphics object.
            g.Dispose();
        }
    }
}
