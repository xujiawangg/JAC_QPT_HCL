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
    /// PRODUCT_CHECK
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.11.20 10:58</date>
    /// </author>
    /// </summary>
    [Description("PRODUCT_CHECK")]
    public class PRODUCT_CHECK 
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("主键")]
        public string PRODUCT_CHECK_KEY { get; set; }
        /// <summary>
        /// 产品主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品主键")]
        public string PRODUCT_KEY { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品编号")]
        public string PRODUCT_CODE { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品名称")]
        public string PRODUCT_NAME { get; set; }
        /// <summary>
        /// PRODUCT_BORN_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_BORN_CODE")]
        public string PRODUCT_BORN_CODE { get; set; }
        /// <summary>
        /// DCS主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("DCS主键")]
        public string DCS_KEY { get; set; }
        /// <summary>
        /// DCS编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("DCS编号")]
        public string DCS_CODE { get; set; }
        /// <summary>
        /// DCS名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("DCS名称")]
        public string DCS_NAME { get; set; }
        /// <summary>
        /// 检测项主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("检测项主键")]
        public string PARM_KEY { get; set; }
        /// <summary>
        /// 检测项编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("检测项编号")]
        public string PARM_TYPE { get; set; }
        /// <summary>
        /// 检测项名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("检测项名称")]
        public string PARM_NAME { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        /// <returns></returns>
        [DisplayName("描述")]
        public string REMARKS { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建日期")]
        public DateTime? CREATEDATE { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建用户主键")]
        public string CREATEUSERID { get; set; }
        /// <summary>
        /// 创建用户名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建用户名称")]
        public string CREATEUSERNAME { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改日期")]
        public DateTime? MODIFYDATE { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改用户主键")]
        public string MODIFYUSERID { get; set; }
        /// <summary>
        /// 修改用户名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改用户名称")]
        public string MODIFYUSERNAME { get; set; }
        /// <summary>
        /// RESERVE01
        /// </summary>
        /// <returns></returns>
        [DisplayName("UPPER_CONTROL")]
        public double? UPPER_CONTROL { get; set; }
        /// <summary>
        /// RESERVE02
        /// </summary>
        /// <returns></returns>
        [DisplayName("TARGET")]
        public double? TARGET { get; set; }
        /// <summary>
        /// RESERVE03
        /// </summary>
        /// <returns></returns>
        [DisplayName("LOWER_CONTROL")]
        public double? LOWER_CONTROL { get; set; }
        /// <summary>
        /// RESERVE04
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE01")]
        public string RESERVE01 { get; set; }
        #endregion
    }
}