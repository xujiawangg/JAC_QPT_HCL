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
    public class HCL_PRINT_RECORD : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// RECORD_ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("RECORD_ID")]
        public string RECORD_ID { get; set; }
        /// <summary>
        /// GD_ASSAMBLE_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("GD_ASSAMBLE_CODE")]
        public string GD_ASSAMBLE_CODE { get; set; }
        /// <summary>
        /// SERIAL_NO
        /// </summary>
        /// <returns></returns>
        [DisplayName("SERIAL_NO")]
        public string SERIAL_NO { get; set; }
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
        /// GD_CREATEDATE
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEDATE")]
        public DateTime? CREATEDATE { get; set; }
        /// <summary>
        /// USER_CHAR01
        /// </summary>
        /// <returns></returns>
        [DisplayName("USER_CHAR01")]
        public string USER_CHAR01 { get; set; }
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
            this.RECORD_ID = CommonHelper.GetGuid;
            this.CREATEDATE = ServerTime.Now;
        }
        #endregion
    }
}