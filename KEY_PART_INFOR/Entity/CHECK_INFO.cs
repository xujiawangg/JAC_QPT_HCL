//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2019
// Software Developers @ HfutIE 2019
//=====================================================================================

using HfutIe;
using HfutIE.Utilities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HfutIE.Entity
{
    /// <summary>
    /// 点检信息表
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.01.21 16:41</date>
    /// </author>
    /// </summary>
    [Description("点检信息表")]
    [PrimaryKey("CHECK_INFO_KEY")]
    public class CHECK_INFO : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// key
        /// </summary>
        /// <returns></returns>
        [DisplayName("key")]
        public string CHECK_INFO_KEY { get; set; }
        /// <summary>
        /// 点检信息编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("点检信息编号")]
        public string CHECK_INFO_CODE { get; set; }
        /// <summary>
        /// 点检信息名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("点检信息名称")]
        public string CHECK_INFO_NAME { get; set; }
        /// <summary>
        /// 点检信息描述
        /// </summary>
        /// <returns></returns>
        [DisplayName("点检信息描述")]
        public string CHECK_INFO_DESCRIPTION { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        [DisplayName("删除标记")]
        public int? DELETEMARK { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建时间")]
        public DateTime? CREATEDATE { get; set; }
        /// <summary>
        /// 创建人key
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建人key")]
        public string CREATEUSERID { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建人姓名")]
        public string CREATEUSERNAME { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改时间")]
        public DateTime? MODIFYDATE { get; set; }
        /// <summary>
        /// 修改人key
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改人key")]
        public string MODIFYUSERID { get; set; }
        /// <summary>
        /// 修改人姓名
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改人姓名")]
        public string MODIFYUSERNAME { get; set; }
        /// <summary>
        /// 预留字段
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段")]
        public string RESERVE01 { get; set; }
        /// <summary>
        /// 预留字段
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段")]
        public string RESERVE02 { get; set; }
        /// <summary>
        /// 预留字段
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段")]
        public string RESERVE03 { get; set; }
        /// <summary>
        /// 预留字段
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段")]
        public string RESERVE04 { get; set; }
        /// <summary>
        /// 预留字段
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段")]
        public string RESERVE05 { get; set; }
        #endregion

    }
}