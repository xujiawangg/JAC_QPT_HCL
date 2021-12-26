using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HfutIe.Entity
{
    public class SafeAdd
    {
        #region 获取/设置 字段值
        /// <summary>
        /// KEY_PART_C_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("KEY_PART_C_KEY")]
        public string key_part_c_key { get; set; }
        /// <summary>
        /// MES_PLAN_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("MES_PLAN_KEY")]
        public string mes_plan_key { get; set; }
        /// <summary>
        /// MES_PLAN_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("MES_PLAN_CODE")]
        public string mes_plan_code { get; set; }
        /// <summary>
        /// PRODUCT_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_KEY")]
        public string product_key { get; set; }
        /// <summary>
        /// PRODUCT_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_CODE")]
        public string product_code { get; set; }
        /// <summary>
        /// PRODUCT_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_NAME")]
        public string product_name { get; set; }
        /// <summary>
        /// PRODUCTION_LINE_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCTION_LINE_KEY")]
        public string p_line_key { get; set; }
        /// <summary>
        /// PRODUCTION_LINE_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCTION_LINE_CODE")]
        public string p_line_code { get; set; }
        /// <summary>
        /// PRODUCTION_LINE_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCTION_LINE_NAME")]
        public string p_line_name { get; set; }
        /// <summary>
        /// WORK_CENTER_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("WORK_CENTER_KEY")]
        public string wc_key { get; set; }
        /// <summary>
        /// WORK_CENTER_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("WORK_CENTER_CODE")]
        public string wc_code { get; set; }
        /// <summary>
        /// WORK_CENTER_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("WORK_CENTER_NAME")]
        public string wc_name { get; set; }
        /// <summary>
        /// PART_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("PART_KEY")]
        public string part_key { get; set; }
        /// <summary>
        /// PART_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("PART_CODE")]
        #region 核心字段
        public string part_code { get; set; }

        /// <summary>
        /// bar_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("bar_code")]
        public string bar_code { get; set; }

        /// <summary>
        /// PART_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("PART_NAME")]
        public string part_name { get; set; }

        /// <summary>
        /// SUPPLIER_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("SUPPLIER_NAME")]
        public string supplier_name { get; set; }

        /// <summary>
        /// SUPPLIER_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("SUPPLIER_CODE")]
        public string supplier_code { get; set; }

        /// <summary>
        /// clt_time
        /// </summary>
        /// <returns></returns>
        [DisplayName("clt_time")]
        public DateTime clt_time { get; set; }

        /// <summary>
        /// is_reclt
        /// </summary>
        /// <returns></returns>
        [DisplayName("is_reclt")]
        public string is_reclt { get; set; }

        #endregion
        /// <summary>
        /// QUANTITY
        /// </summary>
        /// <returns></returns>
        [DisplayName("QUANTITY")]
        public int? quantity { get; set; }
        /// <summary>
        /// RESERVE01
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE01")]
        public string reserve1 { get; set; }
        /// <summary>
        /// RESERVE02
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE02")]
        public string reserve2 { get; set; }
        /// <summary>
        /// RESERVE03
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE03")]
        public string reserve3 { get; set; }
        /// <summary>
        /// RESERVE04
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE04")]
        public string reserve4 { get; set; }
        /// <summary>
        /// RESERVE05
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE05")]
        public string reserve5 { get; set; }
        /// <summary>
        /// reserve6
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve6")]
        public string reserve6 { get; set; }
        /// <summary>
        /// reserve7
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve7")]
        public string reserve7 { get; set; }
        /// <summary>
        /// reserve8
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve8")]
        public string reserve8 { get; set; }
        /// <summary>
        /// reserve9
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve9")]
        public string reserve9 { get; set; }
        /// <summary>
        /// reserve10
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve10")]
        public string reserve10 { get; set; }
        /// <summary>
        /// CREATEDATE
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEDATE")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// CREATEUSERID
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEUSERID")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// CREATEUSERNAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEUSERNAME")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// MODIFYDATE
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFYDATE")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// MODIFYUSERID
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFYUSERID")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// MODIFYUSERNAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFYUSERNAME")]
        public string ModifyUserName { get; set; }
        #endregion
    }
}
