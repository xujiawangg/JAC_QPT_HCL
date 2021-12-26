//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2018
// Software Developers @ HfutIE 2018
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
    /// 产品返修信息过程表
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.12.21 22:01</date>
    /// </author>
    /// </summary>
    [Description("产品返修信息过程表")]
    [PrimaryKey("PRODUCT_REPAIR_INFO_KEY")]
    public class P_PRODUCT_REPAIR_INFO : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 返修信息主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("返修信息主键")]
        public string PRODUCT_REPAIR_INFO_KEY { get; set; }
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
        [DisplayName("返修下线工位key")]
        public string REPAIR_OFFLINE_STATION_KEY { get; set; }
        /// <summary>
        /// 工位编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("返修下线工位编号")]
        public string REPAIR_OFFLINE_STATION_CODE { get; set; }
        /// <summary>
        /// 工位名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("返修下线工位名称")]
        public string REPAIR_OFFLINE_STATION_NAME { get; set; }
        /// <summary>
        /// 工位key
        /// </summary>
        /// <returns></returns>
        [DisplayName("返修上线工位key")]
        public string REPAIR_ONLINE_STATION_KEY { get; set; }
        /// <summary>
        /// 工位编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("返修上线工位编号")]
        public string REPAIR_ONLINE_STATION_CODE { get; set; }
        /// <summary>
        /// 工位名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("返修上线工位名称")]
        public string REPAIR_ONLINE_STATION_NAME { get; set; }
        /// <summary>
        /// 返修下线时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("返修下线时间")]
        public DateTime? REPAIR_OFFLINE_TIME { get; set; }
        /// <summary>
        /// 返修上线时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("返修上线时间")]
        public DateTime? REPAIR_ONLINE_TIME { get; set; }
        /// <summary>
        /// 返修状态
        /// </summary>
        /// <returns></returns>
        [DisplayName("返修状态")]
        public string REPAIR_STATE { get; set; }
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.PRODUCT_REPAIR_INFO_KEY = CommonHelper.GetGuid;
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
            this.PRODUCT_REPAIR_INFO_KEY = KeyValue;
            this.MODIFYDATE = ServerTime.Now;
            this.MODIFYUSERID = SystemLog.UserKey;
            this.MODIFYUSERNAME = SystemLog.UserName;
        }
        #endregion
    }
}