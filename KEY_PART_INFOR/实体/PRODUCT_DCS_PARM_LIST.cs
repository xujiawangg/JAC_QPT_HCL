//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2017
// Software Developers @ HfutIE 2017
//=====================================================================================
using System;
using System.ComponentModel;
using System.Text;

namespace HfutIe.Entity
{
    /// <summary>
    /// 产品数据检测项关联信息
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.03.04 10:52</date>
    /// </author>
    /// </summary>
    public class PRODUCT_DCS_PARM_LIST 
    {
        #region 获取/设置 字段值
        /// <summary>
        /// pro_dcs_parm_list_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_DCS_KEY")]
        public string PRODUCT_DCS_KEY { get; set; }
        /// <summary>
        /// part_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("PART_KEY")]
        public string PART_KEY { get; set; }
        /// <summary>
        /// PRODUCTION_LINE_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCTION_LINE_KEY")]
        public string PRODUCTION_LINE_KEY { get; set; }
        /// <summary>
        /// WORK_CENTER_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("STATION_KEY")]
        public string STATION_KEY { get; set; }
        /// <summary>
        /// dcs_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("DCS_KEY")]
        public string DCS_KEY { get; set; }
        /// <summary>
        /// parm_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("PARM_KEY")]
        public string PARM_KEY { get; set; }
        /// <summary>
        /// RESERVE01
        /// </summary>
        /// <returns></returns>
        [DisplayName("PART_CODE")]
        public string PART_CODE { get; set; }
        /// <summary>
        /// RESERVE02
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEDATE")]
        public DateTime ? CREATEDATE { get; set; }
        /// <summary>
        /// RESERVE03
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEUSERID")]
        public string  CREATEUSERID { get; set; }
        /// <summary>
        /// RESERVE04
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEUSERNAME")]
        public string  CREATEUSERNAME { get; set; }
        /// <summary>
        /// RESERVE05
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFYDATE")]
        public DateTime? MODIFYDATE { get; set; }
        /// <summary>
        /// CREATEDATE
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFYUSERID")]
        public string MODIFYUSERID { get; set; }
        /// <summary>
        /// CREATEUSERID
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFYUSERNAME")]
        public string MODIFYUSERNAME { get; set; }
        /// <summary>
        /// CREATEUSERNAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("DELETEMARK")]
        public int? DELETEMARK { get; set; }
        /// <summary>
        /// MODIFYDATE
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE02")]
        public string  RESERVE02 { get; set; }
        /// <summary>
        /// MODIFYUSERID
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE03")]
        public string RESERVE03 { get; set; }
        /// <summary>
        /// MODIFYUSERNAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE04")]
        public string RESERVE04 { get; set; }
        /// <summary>
        /// MODIFYUSERNAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE05")]
        public string RESERVE05 { get; set; }
        #endregion
    }
}