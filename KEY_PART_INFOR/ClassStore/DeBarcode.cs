using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace HfutIe
{
    class DeBarcode
    {
        #region 解析条形码的类型
        /// <summary>
        /// 获取条形码的类型，返回值为：产品出生证条形码、零部件条形码
        /// </summary>
        /// <param name="barcode">条形码文本</param>
        /// <returns></returns>
        public static string GetBarcodeType(string barcode)
        {
            string BarcodeType = "";
            string ninthChar = "";
            int BarcodeLenth = barcode.Length;
            //string FirstChar = barcode.Substring(0, 1);
            //零部件条码规则：
            //1、1位产线简码+2位零部件序号+8位批次号+1位供应商代码，其中根据前三位数共同确定物料编码
            //2、8位批次号+6位供应商代码（非J00001）+不定位数物料代码，其中第三部分（不定位数的物料代码）为其物料编码
            if (barcode == "OK"|| barcode == "OK\r\n")
            {
                BarcodeType = "采集确认码";
            }
            else if (BarcodeLenth > 13)
            {
                ninthChar = barcode.Substring(8, 2);
                if (ninthChar == "L2"||ninthChar == "J0")
                {
                    BarcodeType = "初始质保件总成码";
                }
                else
                {
                    BarcodeType = "追溯件码";
                    //BarcodeType = "其他";
                }
            }
            else
            {
                BarcodeType = "追溯件码";
            }
            return BarcodeType;
        }
        #endregion

        #region 解析产品出生证条码
        /// <summary>
        /// 获取产品出生证中的产品编号，返回值为产品编号
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public static string GetProductCode(string barcode)
        {
            string ProductCode = "";
            ProductCode = barcode.Substring(21);
            return ProductCode;
        }
        #endregion

        #region 解析零部件条码
        /// <summary>
        /// 根据零部件条码获取零部件编号
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public static string GetPartBatchCode(string barcode)
        {
            string PartBatchCode = "";
            if(barcode == "" || barcode == null)
            {
                MessageBox.Show("零部件条码为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return PartBatchCode = ""; ;
            }            
            int BarcodeLenth = barcode.Length;
            if (BarcodeLenth > 14 && BarcodeLenth< 35)
            {
                PartBatchCode = barcode.Substring(0, 8);//外购件编码与前桥产品的产品出生证编码规则一样，是8位流水号+6位供应商编码+不定位数的零部件编号
            }
            else
            {
                if (BarcodeLenth > 11)
                {
                    PartBatchCode = barcode.Substring(3, 8);//自制零部件编码规则为：一位生产线编码+两位可加工产品型号编码+四位日期编码+四位流水号+一位供应商编码。其中：日期编码为：一位年份编码（以2010年为A，下一年为B，以此类推）+一位月份编码（1 - 9，A - C）+两位日期编码（01 - 31）。流水号编码为0001 - 9999
                }
            }
            return PartBatchCode;
        }

        /// <summary>
        /// 根据零部件条码获取零部件编号
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public static string GetPartCode(string barcode)
        {
            string PartCode = "";
            if (barcode == "" || barcode == null)
            {
                MessageBox.Show("零部件条码为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return PartCode = "";
            }
            int BarcodeLenth = barcode.Length;
            if (BarcodeLenth > 14 && BarcodeLenth < 35)
            {
                PartCode = barcode.Substring(14);
            }
            else
            {   if (BarcodeLenth > 11)
                {
                    PartCode = barcode.Substring(0, 3);
                    string getpart = "select part_code,part_name from P_LINE_PART where abbreviation_code ='" + PartCode + "'  ";
                    DataTable dt_part = new DataTable();
                    dt_part = DbHelperSQL.OpenTable(getpart);
                    if (dt_part.Rows.Count > 0)
                    {
                        PartCode = dt_part.Rows[0]["part_code"].ToString().Trim();
                    }
                }
            }
            return PartCode;
        }

        /// <summary>
        /// 根据零部件条码获取供应商编号
        /// </summary>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public static string GetSupplierCode(string barcode)
        {
            string SupplierCode = "";
            if (barcode == "" || barcode == null)
            {
                MessageBox.Show("零部件条码为空！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return SupplierCode = "";
            }
            int BarcodeLenth = barcode.Length;
            if (BarcodeLenth > 14 && BarcodeLenth < 35)
            {
                SupplierCode = barcode.Substring(8, 6);
            }
            else
            {
                if (BarcodeLenth > 11)
                {
                    SupplierCode = barcode.Substring(11);
                    string getsupplier = "select supplier_code from PART_SUPPLIER where supplier_abb ='" + SupplierCode + "'  ";
                    DataTable dt_supplier = new DataTable();
                    dt_supplier = DbHelperSQL.OpenTable(getsupplier);
                    if (dt_supplier.Rows.Count > 0)
                    {
                        SupplierCode = dt_supplier.Rows[0]["supplier_code"].ToString().Trim();
                    }
                }
            }
            return SupplierCode;
        }
        #endregion
        
     }   
}
