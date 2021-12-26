//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2017
// Software Developers @ HfutIE 2017
//=====================================================================================


using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HfutIE.Entity
{
    /// <summary>
    /// CHECK_INFOR
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.11.10 18:03</date>
    /// </author>
    /// </summary>
    [Description("CHECK_INFOR")]

    public class CHECK_INFOR 
    {
        #region 获取/设置 字段值
        /// <summary>
        /// CHECK_INFO_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("CHECK_INFO_KEY")]
        public int? CHECK_INFO_KEY { get; set; }
        /// <summary>
        /// MES_plan_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("MES_plan_code")]
        public string MES_plan_code { get; set; }
        /// <summary>
        /// PLAN_NUM
        /// </summary>
        /// <returns></returns>
        [DisplayName("PLAN_NUM")]
        public string plan_num { get; set; }
        /// <summary>
        /// product_no
        /// </summary>
        /// <returns></returns>
        [DisplayName("product_no")]
        public string product_no { get; set; }
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
        /// PRODUCT_ABB
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_ABB")]
        public string product_abb { get; set; }
        /// <summary>
        /// PRODUCT_BATCH_NO
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_BATCH_NO")]
        public string product_batch_no { get; set; }
        /// <summary>
        /// PRODUCT_BORN_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_BORN_CODE")]
        public string product_born_code { get; set; }
        /// <summary>
        /// PRODUCT_MODEL_NO
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_MODEL_NO")]
        public string product_model_no { get; set; }
        /// <summary>
        /// PRODUCT_SERIAL_NO
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_SERIAL_NO")]
        public string product_serial { get; set; }
        /// <summary>
        /// PRODUCT_STRUCT_NO
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_STRUCT_NO")]
        public string product_struct_no { get; set; }
        /// <summary>
        /// PRODUCT_TYPE
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_TYPE")]
        public string product_type { get; set; }
        /// <summary>
        /// SITE_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("SITE_KEY")]
        public string site_key { get; set; }
        /// <summary>
        /// SITE_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("SITE_CODE")]
        public string site_code { get; set; }
        /// <summary>
        /// SITE_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("SITE_NAME")]
        public string site_name { get; set; }
        /// <summary>
        /// AREA_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("AREA_KEY")]
        public string area_key { get; set; }
        /// <summary>
        /// AREA_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("AREA_CODE")]
        public string area_code { get; set; }
        /// <summary>
        /// AREA_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("AREA_NAME")]
        public string area_name { get; set; }
        /// <summary>
        /// WORKSHOP_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("WORKSHOP_KEY")]
        public string ws_key { get; set; }
        /// <summary>
        /// WORKSHOP_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("WORKSHOP_CODE")]
        public string ws_code { get; set; }
        /// <summary>
        /// WORKSHOP_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("WORKSHOP_NAME")]
        public string ws_name { get; set; }
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
        /// point_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("point_key")]
        public string point_key { get; set; }
        /// <summary>
        /// point_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("point_CODE")]
        public string point_CODE { get; set; }
        /// <summary>
        /// point_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("point_name")]
        public string point_name { get; set; }
        /// <summary>
        /// is_check
        /// </summary>
        /// <returns></returns>
        [DisplayName("is_check")]
        public string is_check { get; set; }
        /// <summary>
        /// check_date
        /// </summary>
        /// <returns></returns>
        [DisplayName("check_date")]
        public DateTime? check_date { get; set; }
        /// <summary>
        /// check_result
        /// </summary>
        /// <returns></returns>
        [DisplayName("check_result")]
        public string check_result { get; set; }
        /// <summary>
        /// REMARKS
        /// </summary>
        /// <returns></returns>
        [DisplayName("REMARKS")]
        public string remarks { get; set; }
        /// <summary>
        /// CREATEDATE
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEDATE")]
        public DateTime? CREATEDATE { get; set; }
        /// <summary>
        /// CREATEUSERID
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEUSERID")]
        public string CREATEUSERID { get; set; }
        /// <summary>
        /// CREATEUSERNAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEUSERNAME")]
        public string CREATEUSERNAME { get; set; }
        /// <summary>
        /// MODIFYDATE
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFYDATE")]
        public DateTime? MODIFYDATE { get; set; }
        /// <summary>
        /// MODIFYUSERID
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFYUSERID")]
        public string MODIFYUSERID { get; set; }
        /// <summary>
        /// MODIFYUSERNAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFYUSERNAME")]
        public string MODIFYUSERNAME { get; set; }
        /// <summary>
        /// RESERVE01
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE01")]
        public string RESERVE01 { get; set; }
        /// <summary>
        /// RESERVE02
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE02")]
        public string RESERVE02 { get; set; }
        /// <summary>
        /// RESERVE03
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE03")]
        public string RESERVE03 { get; set; }
        /// <summary>
        /// RESERVE04
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE04")]
        public string RESERVE04 { get; set; }
        /// <summary>
        /// RESERVE05
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE05")]
        public string RESERVE05 { get; set; }
        /// <summary>
        /// CHECK_INFO_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("CHECK_INFO_CODE")]
        public string CHECK_INFO_CODE { get; set; }
        /// <summary>
        /// CHECK_INFO_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("CHECK_INFO_NAME")]
        public string CHECK_INFO_NAME { get; set; }
        /// <summary>
        /// CHECK_INFO_DESCRIPTION
        /// </summary>
        /// <returns></returns>
        [DisplayName("CHECK_INFO_DESCRIPTION")]
        public string CHECK_INFO_DESCRIPTION { get; set; }
        /// <summary>
        /// DELETEMARK
        /// </summary>
        /// <returns></returns>
        [DisplayName("DELETEMARK")]
        public string DELETEMARK { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>

        #endregion
    }
}