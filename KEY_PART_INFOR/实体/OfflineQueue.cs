//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2017
// Software Developers @ HfutIE 2017
//=====================================================================================


using HfutIe;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HfutIE.Entity
{
    /// <summary>
    /// OfflineQueue
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.11.10 18:03</date>
    /// </author>
    /// </summary>
    [Description("OfflineQueue")]

    public class OfflineQueue
    {
        #region 获取/设置 字段值
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
        /// PLAN_NUM
        /// </summary>
        /// <returns></returns>
        [DisplayName("PLAN_NUM")]
        public string plan_num { get; set; }
        /// <summary>
        /// PLAN_NO
        /// </summary>
        /// <returns></returns>
        [DisplayName("PLAN_NO")]
        public string plan_no { get; set; }
        /// <summary>
        /// PRODUCT_BORN_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_BORN_CODE")]
        public string product_born_code { get; set; }
        /// <summary>
        /// PRODUCT_SERIAL_NO
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_SERIAL_NO")]
        public string product_serial_no { get; set; }
        /// <summary>
        /// PRODUCT_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_CODE")]
        public string product_code { get; set; }
        /// <summary>
        /// PRODUCT_MODEL_NO
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_MODEL_NO")]
        public string product_model_no { get; set; }
        /// <summary>
        /// front_axle_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("front_axle_code")]
        public string front_axle_code { get; set; }
        /// <summary>
        /// ASSEMBLE_OFFLINE_TIME
        /// </summary>
        /// <returns></returns>
        [DisplayName("ASSEMBLE_OFFLINE_TIME")]
        public DateTime? assemble_offline_time { get; set; }
        /// <summary>
        /// IS_OK
        /// </summary>
        /// <returns></returns>
        [DisplayName("IS_OK")]
        public string is_OK { get; set; }
        /// <summary>
        /// REMARKS
        /// </summary>
        /// <returns></returns>
        [DisplayName("REMARKS")]
        public string remarks { get; set; }
        /// <summary>
        /// RESERVE01
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE01")]
        public string reserve01 { get; set; }
        /// <summary>
        /// RESERVE02
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE02")]
        public string reserve02 { get; set; }
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
        /// num
        /// </summary>
        /// <returns></returns>
        [DisplayName("num")]
        public Int64  num { get; set; }

        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        #endregion
    }
}