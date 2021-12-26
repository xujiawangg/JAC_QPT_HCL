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
    /// 返修Tag信息表
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.09.19 15:55</date>
    /// </author>
    /// </summary>
    [Description("返修Tag信息档案表")]
    [PrimaryKey("TAG_INFO_KEY")]
    public class DOC_REPAIR_TAG_INFO : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("主键")]
        public string TAG_INFO_KEY { get; set; }
        /// <summary>
        /// 地址路径
        /// </summary>
        /// <returns></returns>
        [DisplayName("地址路径")]
        public string OPC_ITEM_NAME { get; set; }
        /// <summary>
        /// 路径对应存储值
        /// </summary>
        /// <returns></returns>
        [DisplayName("路径对应存储值")]
        public string OPC_ITEM_VALUE { get; set; }
        /// <summary>
        /// 计划主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("计划主键")]
        public string MES_PLAN_KEY { get; set; }
        /// <summary>
        /// 计划编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("计划编号")]
        public string MES_PLAN_CODE { get; set; }
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
        /// 产品出生证
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品出生证")]
        public string PRODUCT_BORN_CODE { get; set; }
        /// <summary>
        /// 产线主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("产线主键")]
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
        /// 返修下线工位主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("返修下线工位主键")]
        public string REPAIR_OFFLINE_STATION_KEY { get; set; }
        /// <summary>
        /// 返修下线工位编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("返修下线工位编号")]
        public string REPAIR_OFFLINE_STATION_CODE { get; set; }
        /// <summary>
        /// 返修下线工位名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("返修下线工位名称")]
        public string REPAIR_OFFLINE_STATION_NAME { get; set; }
        /// <summary>
        /// 返修下线时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("返修下线时间")]
        public DateTime? REPAIR_OFFLINE_TIME { get; set; }
        /// <summary>
        /// 返修上线工位主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("返修上线工位主键")]
        public string REPAIR_ONLINE_STATION_KEY { get; set; }
        /// <summary>
        /// 返修上线工位编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("返修上线工位编号")]
        public string REPAIR_ONLINE_STATION_CODE { get; set; }
        /// <summary>
        /// 返修上线工位名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("返修上线工位名称")]
        public string REPAIR_ONLINE_STATION_NAME { get; set; }
        /// <summary>
        /// 返修上线时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("返修上线时间")]
        public DateTime? REPAIR_ONLINE_TIME { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建时间")]
        public DateTime? CREATEDATE { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建人ID")]
        public string CREATEUSERID { get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建人名称")]
        public string CREATEUSERNAME { get; set; }
        /// <summary>
        /// 预留1
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留1")]
        public string RESERVE1 { get; set; }
        /// <summary>
        /// 预留2
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留2")]
        public string RESERVE2 { get; set; }
        /// <summary>
        /// 预留3
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留3")]
        public string RESERVE3 { get; set; }
        /// <summary>
        /// 预留4
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留4")]
        public string RESERVE4 { get; set; }
        /// <summary>
        /// 预留5
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留5")]
        public string RESERVE5 { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.TAG_INFO_KEY = CommonHelper.GetGuid;
            this.CREATEDATE = DateTime.Now;
            this.CREATEUSERID = SystemLog.UserKey;
            this.CREATEUSERNAME = SystemLog.UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.TAG_INFO_KEY = KeyValue;
                                            }
        #endregion
    }
}