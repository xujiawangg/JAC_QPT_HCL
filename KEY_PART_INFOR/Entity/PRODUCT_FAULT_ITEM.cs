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
    /// 产品故障项
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.01.18 19:47</date>
    /// </author>
    /// </summary>
    [Description("产品故障项")]
    [PrimaryKey("PRODUCT_FAULT_ITEM_KEY")]
    public class PRODUCT_FAULT_ITEM : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// key
        /// </summary>
        /// <returns></returns>
        [DisplayName("key")]
        public string PRODUCT_FAULT_ITEM_KEY { get; set; }
        /// <summary>
        /// 产品故障编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品故障编号")]
        public string PRODUCT_FAULT_ITEM_CODE { get; set; }
        /// <summary>
        /// 产品故障名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品故障名称")]
        public string PRODUCT_FAULT_ITEM_NAME { get; set; }
        /// <summary>
        /// 产品故障描述
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品故障描述")]
        public string DISCRIBE { get; set; }
        /// <summary>
        /// 产品故障类型key
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品故障类型key")]
        public string PRODUCT_FAULT_TYPE_KEY { get; set; }
        /// <summary>
        /// 产品故障类型编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品故障类型编号")]
        public string PRODUCT_FAULT_TYPE_CODE { get; set; }
        /// <summary>
        /// 产品故障类型名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品故障类型名称")]
        public string PRODUCT_FAULT_TYPE_NAME { get; set; }
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
        /// 删除状态
        /// </summary>
        /// <returns></returns>
        [DisplayName("删除状态")]
        public int? DELETEMARK { get; set; }
        /// <summary>
        /// 预留字段1
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段1")]
        public string RESERVE1 { get; set; }
        /// <summary>
        /// 预留字段2
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段2")]
        public string RESERVE2 { get; set; }
        /// <summary>
        /// 预留字段3
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段3")]
        public string RESERVE3 { get; set; }
        /// <summary>
        /// 预留字段4
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段4")]
        public string RESERVE4 { get; set; }
        /// <summary>
        /// 预留字段5
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段5")]
        public string RESERVE5 { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.PRODUCT_FAULT_ITEM_KEY = CommonHelper.GetGuid;
            this.CREATEDATE = ServerTime.Now;
            this.CREATEUSERID = SystemLog.UserKey;
            this.CREATEUSERNAME = SystemLog.UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.PRODUCT_FAULT_ITEM_KEY = KeyValue;
            this.MODIFYDATE = ServerTime.Now;
            this.MODIFYUSERID = SystemLog.UserKey;
            this.MODIFYUSERNAME = SystemLog.UserName;
        }
        #endregion
    }
}