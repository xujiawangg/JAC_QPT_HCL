using data;
using HfutIe.Entity;
using HfutIE.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HfutIe.Offline
{
    public partial class SafePartAdd : Form
    {
        public List<SafeAdd> safe_info = new List<SafeAdd>();
        public string product_born_code="";
        public SafePartAdd()
        {
            InitializeComponent();
        }         

        public  void SafePartAdd_Load_1(object sender, EventArgs e)
        {
            for (int row = 0; row < safe_info.Count; row++)
            {
                dgvSafeInfo.Rows.Add();
                dgvSafeInfo.Rows[row].Cells["s_part_barcode"].Value = "";
                dgvSafeInfo.Rows[row].Cells["s_part_key"].Value = safe_info[row].part_key;
                dgvSafeInfo.Rows[row].Cells["s_part_code"].Value = safe_info[row].part_code;
                dgvSafeInfo.Rows[row].Cells["s_part_name"].Value = safe_info[row].part_name;
            }
            dgvSafeInfo.Rows[0].Selected = false;
        }
        private void dgvSafeInfo_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
             string barcode = e.Value.ToString();
             string  PartCode =DeBarcode.GetPartCode(barcode);
             if (PartCode == dgvSafeInfo.Rows[e.RowIndex].Cells["s_part_code"].Value.ToString().Trim())
            {
                string SupplierCode = DeBarcode.Get_supplierCode(barcode);
                string str = "select * from PART_SUPPLIER where SUPPLIER_CODE='" + SupplierCode + "'";
                DataTable dt = DBQuery.OpenTable1(str);
                if (dt.Rows.Count > 0)
                {
                    dgvSafeInfo.Rows[e.RowIndex].Cells["s_supplier_code"].Value = SupplierCode;
                    dgvSafeInfo.Rows[e.RowIndex].Cells["s_supplier_name"].Value = dt.Rows[0]["SUPPLIER_NAME"].ToString();
                }
                else
                {
                    MessageBox.Show("无此零件条码的供应商信息，请检查条码是否正确");
                }
            }
             else
            {
                MessageBox.Show("零件条码与当前零件不匹配，请重新输入！", "提醒", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            for (int col = 0; col < dgvSafeInfo.Rows.Count; col++)
            {
                {
                    string barcode = dgvSafeInfo.Rows[col].Cells["s_part_barcode"].Value.ToString();
                    string PartCode = DeBarcode.GetPartCode(barcode);
                    string SupplierCode = DeBarcode.Get_supplierCode(barcode);
                    string str = "select * from PART_SUPPLIER where SUPPLIER_CODE='" + SupplierCode + "'";
                    DataTable dt = DBQuery.OpenTable1(str);
                    if (PartCode == dgvSafeInfo.Rows[col].Cells["s_part_code"].Value.ToString().Trim() && dt.Rows.Count > 0)
                    {
                        dgvSafeInfo.Rows[col].DefaultCellStyle.BackColor = Color.Lime;
                        string s_part_key = dgvSafeInfo.Rows[col].Cells["s_part_key"].Value.ToString().Trim();
                        Insert_kpif(barcode, s_part_key);
                    }
                    else
                    {
                        dgvSafeInfo.Rows[col].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
            this.Close();
            return;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }
        public void Insert_kpif( string barcode,string part_key)
        {
            string str1 = "select * from P_ASSEMBLE_PRODUCT_STATE where PRODUCT_BORN_CODE='" + product_born_code + "'";
            DataTable dt1 = DBQuery.OpenTable1(str1);
            P_ASSEMBLE_PRODUCT_STATE paps = EntityHelper<P_ASSEMBLE_PRODUCT_STATE>.GetEntity(dt1);
            P_KEY_PART_INFOR pkpi = EntityHelper<P_KEY_PART_INFOR>.GetEntity(paps);
            pkpi.ASSMBLY_TIME = DateTime.Now;
            string SupplierCode = DeBarcode.Get_supplierCode(barcode);
            string str = "select * from PART_SUPPLIER where SUPPLIER_CODE='" + SupplierCode + "'";
            DataTable dt = DBQuery.OpenTable1(str);
            string supplier_key = dt.Rows[0]["SUPPLIER_KEY"].ToString();
            string supplier_name = dt.Rows[0]["SUPPLIER_NAME"].ToString();
            pkpi.PART_KEY = part_key;
            pkpi.PART_BARCODE = barcode;
            pkpi.SUPPLIER_KEY = supplier_key;
            pkpi.SUPPLIER_CODE = SupplierCode;
            pkpi.SUPPLIER_NAME = supplier_name;
            pkpi.Create();
            DataBaseOpByEntity.Insert<P_KEY_PART_INFOR>(pkpi);
            DataBaseOpByEntity.InsertSql<P_KEY_PART_INFOR>(pkpi, "DOC_KEY_PART_INFOR");
        }

    }
}
