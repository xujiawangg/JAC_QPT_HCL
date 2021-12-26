using HfutIE.Entity;
using KEY_PART_INFOR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HfutIe 
{
    public class Info
    {
        public int allNum { get; set; }
        public int okNum { get; set; }
        public int notOkNum { get; set; }
        public int continueNotOkNum { get; set; }
    }
    class WorkCenterInfor
    {
        private string wc_code;
        //  private static string wc_key= GetData.GetWc_Key();
        private List<EQUIPMENT> wcequip_infor = new List<EQUIPMENT>();
        private int allNum = 0;
        private int okNum = 0;
        private int notOkNum = 0;
        private int continueNotOkNum = 0;
        private static string dcsTableName;
        public WorkCenterInfor(List<string> StationCodeList)
        {
            //this.wc_code = wc_code;
            wcequip_infor = GetData.GetEquipInforByWCcode(StationCodeList);

            //DataTable dt_num = GetData.GetWcWorkNum(wc_code);
            //if (dt_num != null)
            //{
            //    DataRow[] drArr = dt_num.Select("Is_Ok='0'");//查询
            //    if (drArr.Length > 0)
            //    {
            //        notOkNum = Int32.Parse(drArr[0]["Num"].ToString());
            //    }
            //    DataRow[] drArr2 = dt_num.Select("Is_Ok='1'");//查询
            //    if (drArr2.Length > 0)
            //    {
            //        okNum = Int32.Parse(drArr2[0]["Num"].ToString());
            //    }
            //    allNum = notOkNum + okNum;
            //}
        }
        /// <summary>
        /// 工位编号
        /// </summary>
        public string Wc_code
        {
            get{return wc_code;}
        }
        /// <summary>
        /// 工位key
        /// </summary>
        public string Wc_key
        {
            get
            {
                if (wcequip_infor.Count > 0)
                {
                    return wcequip_infor[0].STATION_KEY.ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// 工位当日加工总数量
        /// </summary>
        public string Wc_name
        {
            get
            {
                if (wcequip_infor.Count>0)
                {
                    return wcequip_infor[0].STATION_NAME.ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        public string Equip_key
        {
            get
            {
                if (wcequip_infor.Count > 0)
                {
                    return wcequip_infor[0].EQUIPMENT_KEY.ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        public string Equip_code
        {
            get
            {
                if (wcequip_infor.Count > 0)
                {
                    return wcequip_infor[0].EQUIPMENT_CODE.ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        public string Equip_name
        {
            get
            {
                if (wcequip_infor.Count > 0)
                {
                    return wcequip_infor[0].EQUIPMENT_NAME.ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        public int AllNum
        {
            get{return allNum;}
            set{allNum = value; }
        }
        /// <summary>
        /// 工位当日加工良品数量
        /// </summary>
        public int OkNum
        {
            get{return okNum;}
            set{okNum = value;}
        }
        /// <summary>
        /// 当日工位加工不良品数量
        /// </summary>
        public int NotOkNum
        {
            get{ return notOkNum;}
            set{notOkNum = value;}
        }
        /// <summary>
        /// 工位加工持续不良品数量
        /// </summary>
        public int ContinueNotOkNum
        {
            get{ return continueNotOkNum;}

            set{continueNotOkNum = value;}
        }
        /// <summary>
        /// 工位设备DCS表名
        /// </summary>
        public static string DcsTableName
        {
            get
            {
                return dcsTableName;
            }

            set
            {
                dcsTableName = value;
            }
        }
    }
}
