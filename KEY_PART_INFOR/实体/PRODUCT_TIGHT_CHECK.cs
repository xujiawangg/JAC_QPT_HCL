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
    /// PRODUCT_TIGHT_CHECK
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.12.01 15:50</date>
    /// </author>
    /// </summary>
    [Description("PRODUCT_TIGHT_CHECK")]
    public class PRODUCT_TIGHT_CHECK
    {
        #region 获取/设置 字段值
        /// <summary>
        /// product_tight_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("product_tight_key")]
        public string product_tight_key { get; set; }
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
        /// PRODUCT_MODEL_NO
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_MODEL_NO")]
        public string product_model_no { get; set; }
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
        /// tightening_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("tightening_code")]
        public string tightening_code { get; set; }
        /// <summary>
        /// tight_num
        /// </summary>
        /// <returns></returns>
        [DisplayName("tight_num")]
        public byte? tight_num { get; set; }
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
        /// RESERVE03
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE03")]
        public string reserve03 { get; set; }
        /// <summary>
        /// RESERVE04
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE04")]
        public string reserve04 { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>

        #endregion
    }
}