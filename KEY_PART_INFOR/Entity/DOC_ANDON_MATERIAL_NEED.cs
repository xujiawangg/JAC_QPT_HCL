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
    /// ANDON信息过程表
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.12.24 14:37</date>
    /// </author>
    /// </summary>
    [Description("ANDON信息物料需求档案表")]
    [PrimaryKey("ANDON_MATERIAL_NEED_KEY")]
    public class DOC_ANDON_MATERIAL_NEED : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// ANDON物料需求主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("ANDON物料需求主键")]
        public string ANDON_MATERIAL_NEED_KEY { get; set; }
        /// <summary>
        /// ANDON信息主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("ANDON信息主键")]
        public string ANDON_INFOR_KEY { get; set; }
        /// <summary>
        /// 公司key
        /// </summary>
        /// <returns></returns>
        [DisplayName("公司key")]
        public string SITE_KEY { get; set; }
        /// <summary>
        /// 公司编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("公司编号")]
        public string SITE_CODE { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("公司名称")]
        public string SITE_NAME { get; set; }
        /// <summary>
        /// 工厂key
        /// </summary>
        /// <returns></returns>
        [DisplayName("工厂key")]
        public string AREA_KEY { get; set; }
        /// <summary>
        /// 工厂编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("工厂编号")]
        public string AREA_CODE { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("工厂名称")]
        public string AREA_NAME { get; set; }
        /// <summary>
        /// 车间key
        /// </summary>
        /// <returns></returns>
        [DisplayName("车间key")]
        public string WORKSHOP_KEY { get; set; }
        /// <summary>
        /// 车间编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("车间编号")]
        public string WORKSHOP_CODE { get; set; }
        /// <summary>
        /// 车间名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("车间名称")]
        public string WORKSHOP_NAME { get; set; }
        /// <summary>
        /// 加工中心key
        /// </summary>
        /// <returns></returns>
        [DisplayName("加工中心key")]
        public string MACHINING_CENTER_KEY { get; set; }
        /// <summary>
        /// 加工中心编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("加工中心编号")]
        public string MACHINING_CENTER_CODE { get; set; }
        /// <summary>
        /// 加工中心名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("加工中心名称")]
        public string MACHINING_CENTER_NAME { get; set; }
        /// <summary>
        /// 生产线key
        /// </summary>
        /// <returns></returns>
        [DisplayName("生产线key")]
        public string PRODUCTION_LINE_KEY { get; set; }
        /// <summary>
        /// 生产线编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("生产线编号")]
        public string PRODUCTION_LINE_CODE { get; set; }
        /// <summary>
        /// 生产线名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("生产线名称")]
        public string PRODUCTION_LINE_NAME { get; set; }
        /// <summary>
        /// 工作中心key
        /// </summary>
        /// <returns></returns>
        [DisplayName("工作中心key")]
        public string WORK_CENTER_KEY { get; set; }
        /// <summary>
        /// 工作中心编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("工作中心编号")]
        public string WORK_CENTER_CODE { get; set; }
        /// <summary>
        /// 工作中心名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("工作中心名称")]
        public string WORK_CENTER_NAME { get; set; }
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
        /// 设备key
        /// </summary>
        /// <returns></returns>
        [DisplayName("设备key")]
        public string EQUIPMENT_KEY { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("设备编号")]
        public string EQUIPMENT_CODE { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("设备名称")]
        public string EQUIPMENT_NAME { get; set; }
        /// <summary>
        /// andon类型key
        /// </summary>
        /// <returns></returns>
        [DisplayName("andon类型key")]
        public string ANDON_TYPE_KEY { get; set; }
        /// <summary>
        /// andon类型编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("andon类型编号")]
        public string ANDON_TYPE_CODE { get; set; }
        /// <summary>
        /// andon类型名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("andon类型名称")]
        public string ANDON_TYPE_NAME { get; set; }
        /// <summary>
        /// andon信息key
        /// </summary>
        /// <returns></returns>
        [DisplayName("andon信息key")]
        public string ANDON_INFO_KEY { get; set; }
        /// <summary>
        /// andon信息编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("andon信息编号")]
        public string ANDON_INFO_CODE { get; set; }
        /// <summary>
        /// andon信息名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("andon信息名称")]
        public string ANDON_INFO_NAME { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [DisplayName("备注")]
        public string REMARK { get; set; }
        /// <summary>
        /// 推送人
        /// </summary>
        /// <returns></returns>
        [DisplayName("推送人")]
        public string PUSH_PEOPLE { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        /// <returns></returns>
        [DisplayName("等级")]
        public string RANK { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("开始时间")]
        public DateTime? START_TIME { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("结束时间")]
        public DateTime? END_TIME { get; set; }
        /// <summary>
        /// 持续时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("持续时间")]
        public int? CONTINUED_TIME { get; set; }
        /// <summary>
        /// 配送人key
        /// </summary>
        /// <returns></returns>
        [DisplayName("配送人key")]
        public string FEED_STAFF_KEY { get; set; }
        /// <summary>
        /// 配送人编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("配送人编号")]
        public string FEED_STAFF_CODE { get; set; }
        /// <summary>
        /// 配送人姓名
        /// </summary>
        /// <returns></returns>
        [DisplayName("配送人姓名")]
        public string FEED_STAFF_NAME { get; set; }
        /// <summary>
        /// 配送时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("配送时间")]
        public DateTime? FEED_TIME { get; set; }
        /// <summary>
        /// 配送持续时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("配送持续时间")]
        public int? FEED_CONTINUED_TIME { get; set; }
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
            this.ANDON_MATERIAL_NEED_KEY = CommonHelper.GetGuid;
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
            this.ANDON_MATERIAL_NEED_KEY = KeyValue;
            this.MODIFYDATE = ServerTime.Now;
            this.MODIFYUSERID = SystemLog.UserKey;
            this.MODIFYUSERNAME = SystemLog.UserName;
        }
        #endregion
    }
}