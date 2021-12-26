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
    public partial class MaterialAndon : Form
    {
        public MaterialAndon()
        {
            InitializeComponent();
        }
        public MaterialAndon(string part)
        {
            InitializeComponent();
            this.part_lbl.Text = "(" + part+")";
        }
        public delegate void GetMaterialNum(int num);
        public event GetMaterialNum ButtonClick;
        public string data;

        private void MaterialAndon_Load(object sender, EventArgs e)
        {
            try
            {
                int max_num = Convert.ToInt32(data.Split('|')[1]);//当前工位，andon呼叫的物料的线边最大库存量
                int storage_num = Convert.ToInt32(data.Split('|')[2]);//当前工位，andon呼叫的物料的线边实际库存量
                int delivery_unit_num = Convert.ToInt32(data.Split('|')[3]);//当前工位，andon呼叫的物料的配送数量
                DataTable dt = new DataTable();
                dt.Columns.Add("delivery_unit_num");
                for (int i = 0;i <= 100; i++)//最大配送数量为100
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = i + 1;
                    dt.Rows.Add(dr);
                }
                if (dt.Rows.Count == 0)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = 0;
                    dt.Rows.Add(dr);
                }
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "delivery_unit_num";
                comboBox1.ValueMember = "delivery_unit_num";
                comboBox1.Text = delivery_unit_num.ToString();//如果配送数量大于最大库存减去实际库存（即如果按配送数量配送，线边物料数量将超过线边最大库存量），则配送数量为：最大库存量-实际库存量，否则：配送数量为原定配送数量
            }
            catch (Exception ex)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("双击选择物料按灯数量失败！" + ex.Message);
            }
        }

        private void yes_btn_Click(object sender, EventArgs e)
        {
            if (ButtonClick != null)
            {
                ButtonClick(Convert.ToInt32(comboBox1.Text));
            }
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Exit_pic_Box_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
