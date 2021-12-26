using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HfutIe
{
    public partial class ChoseReplace : Form
    {
        List<string> BarCodeList;//已绑定的安全件条码集合
        string barcode_choose;//界面选中的安全件条码
        public ChoseReplace(List<string> barcodelist)
        {
            InitializeComponent();
            BarCodeList = barcodelist;
        }

        public string BarcodeChoose
        {
            get { return this.barcode_choose; }//界面返回选中需要更改的安全件条码
        }

        public delegate void GetMaterialNum(int num);
        public event GetMaterialNum ButtonClick;
        public string data;
        Dictionary<string, string> dicp = new Dictionary<string, string>();

        #region 界面加载
        private void ChoseReplace_Load(object sender, EventArgs e)
        {
            GetDicP();
        }
        #endregion

        private void yes_btn_Click(object sender, EventArgs e)
        {
            barcode_choose = barcode_cobox.Text.Trim();
            this.DialogResult = DialogResult.OK;
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GetDicP()//工单
        {
            dicp.Clear();
            foreach(var item in BarCodeList)//循环加载显示待选择安全件条码
            {
                dicp.Add(item, item);
            }
            SetCmb(barcode_cobox, dicp);
        }
        private void SetCmb(System.Windows.Forms.ComboBox cmb, Dictionary<string, string> dic)
        {
            cmb.Items.Clear();
            foreach (var i in dic)
            {
                if (!cmb.Items.Contains(i.Key))
                    cmb.Items.Add(i.Key);
            }

            cmb.SelectedIndex = 0;

        }

        #region 界面关闭
        private void Exit_pic_Box_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            label1.ForeColor = Color.Red;
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            label1.ForeColor = Color.Black;
        }
    }
}
