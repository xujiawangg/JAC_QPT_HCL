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
    /// 产品故障处理信息表
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.01.19 11:14</date>
    /// </author>
    /// </summary>
    [Description("产品故障处理信息表")]
    [PrimaryKey("PRODUCT_FA_MAIN_INFOR_KEY")]
    public class PRODUCT_FA_MAIN_INFOR : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 产品故障处理信息主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品故障处理信息主键")]
        public string PRODUCT_FA_MAIN_INFOR_KEY { get; set; }
        /// <summary>
        /// 故障信息主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("故障信息主键")]
        public string PRODUCT_FAULT_ITEM_KEY { get; set; }
        /// <summary>
        /// 故障信息编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("故障信息编号")]
        public string PRODUCT_FAULT_ITEM_CODE { get; set; }
        /// <summary>
        /// 故障信息名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("故障信息名称")]
        public string PRODUCT_FAULT_ITEM_NAME { get; set; }
        /// <summary>
        /// 故障信息描述
        /// </summary>
        /// <returns></returns>
        [DisplayName("故障信息描述")]
        public string DISCRIBE { get; set; }
        /// <summary>
        /// 排故措施主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("排故措施主键")]
        public string PRODUCT_MAINTAIN_ITEM_KEY { get; set; }
        /// <summary>
        /// 排故措施编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("排故措施编号")]
        public string PRODUCT_MAINTAIN_ITEM_CODE { get; set; }
        /// <summary>
        /// 排故措施名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("排故措施名称")]
        public string PRODUCT_MAINTAIN_ITEM_NAME { get; set; }
        /// <summary>
        /// 计划key
        /// </summary>
        /// <returns></returns>
        [DisplayName("计划key")]
        public string MES_PLAN_KEY { get; set; }
        /// <summary>
        /// 计划编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("计划编号")]
        public string MES_PLAN_CODE { get; set; }
        /// <summary>
        /// 产品key
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品key")]
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
        /// 产品出生证
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品出生证")]
        public string PRODUCT_BORN_CODE { get; set; }
        /// <summary>
        /// 产线key
        /// </summary>
        /// <returns></returns>
        [DisplayName("产线key")]
        public string PRODUCTION_LINE_KEY { get; set; }
        /// <summary>
        /// 产线编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("产线编号")]
        public string PRODUCTION_LINE_CODE { get; set; }
        /// <summary>
        /// 产线名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("产线名称")]
        public string PRODUCTION_LINE_NAME { get; set; }
        /// <summary>
        /// 工位key
        /// </summary>
        /// <returns></returns>
        [DisplayName("工位key")]
        public string STATION_KEY { get; set; }
        /// <summary>
        /// 工位编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("工位编号")]
        public string STATION_CODE { get; set; }
        /// <summary>
        /// 工位名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("工位名称")]
        public string STATION_NAME { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [DisplayName("备注")]
        public string REMARKS { get; set; }
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.PRODUCT_FA_MAIN_INFOR_KEY = CommonHelper.GetGuid;
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
            this.PRODUCT_FA_MAIN_INFOR_KEY = KeyValue;
                                            }
        #endregion
    }
}