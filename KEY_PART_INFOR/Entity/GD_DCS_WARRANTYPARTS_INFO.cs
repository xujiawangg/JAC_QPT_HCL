//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2017
// Software Developers @ HfutIE 2017
//=====================================================================================

using System;
using System.ComponentModel;
using System.Text;

namespace HfutIe
{
    /// <summary>
    /// ADON_TYPE
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.03.21 20:36</date>
    /// </author>
    /// </summary>
    public class GD_DCS_WARRANTYPARTS_INFO : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// GD_GUID
        /// </summary>
        /// <returns></returns>
        [DisplayName("GD_GUID")]
        public string GD_GUID { get; set; }
        /// <summary>
        /// GD_ASSAMBLE_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("GD_ASSAMBLE_CODE")]
        public string GD_ASSAMBLE_CODE { get; set; }
        /// <summary>
        /// GD_PART_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("GD_PART_CODE")]
        public string GD_PART_CODE { get; set; }
        /// <summary>
        /// GD_PART_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("GD_PART_NAME")]
        public string GD_PART_NAME { get; set; }
        /// <summary>
        /// GD_PART_VINCODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("GD_PART_VINCODE")]
        public string GD_PART_VINCODE { get; set; }
        /// <summary>
        /// GD_CFLG
        /// </summary>
        /// <returns></returns>
        [DisplayName("GD_CFLG")]
        public string GD_CFLG { get; set; }
        /// <summary>
        /// GD_RTMS
        /// </summary>
        /// <returns></returns>
        [DisplayName("GD_RTMS")]
        public string GD_RTMS { get; set; }
        /// <summary>
        /// GD_CREATEDATE
        /// </summary>
        /// <returns></returns>
        [DisplayName("GD_CREATEDATE")]
        public DateTime? GD_CREATEDATE { get; set; }
        /// <summary>
        /// GD_WDAT
        /// </summary>
        /// <returns></returns>
        [DisplayName("GD_WDAT")]
        public DateTime? GD_WDAT { get; set; }
        /// <summary>
        /// GD_RDAT
        /// </summary>
        /// <returns></returns>
        [DisplayName("GD_RDAT")]
        public DateTime? GD_RDAT { get; set; }
        /// <summary>
        /// 供應商代碼
        /// </summary>
        /// <returns></returns>
        [DisplayName("供應商代碼")]
        public string SUPPLIER_CODE { get; set; }
        /// <summary>
        /// USER_CHAR01
        /// </summary>
        /// <returns></returns>
        [DisplayName("USER_CHAR01")]
        public string USER_CHAR01 { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("供应商名称")]
        public string SUPPLIER_NAME { get; set; }
        /// <summary>
        /// USER_CHAR02
        /// </summary>
        /// <returns></returns>
        [DisplayName("USER_CHAR02")]
        public string USER_CHAR02 { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.GD_GUID = CommonHelper.GetGuid;
            this.GD_CREATEDATE = ServerTime.Now;
            this.GD_WDAT = ServerTime.Now;
            this.GD_CFLG = "N";
        }
        #endregion
    }
}