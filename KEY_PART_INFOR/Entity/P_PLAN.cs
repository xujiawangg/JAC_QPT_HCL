//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2018
// Software Developers @ HfutIE 2018
//=====================================================================================

using HfutIe;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HfutIE.Entity
{
    /// <summary>
    /// 计划过程表
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.10.25 16:59</date>
    /// </author>
    /// </summary>
    [Description("计划过程表")]
    [PrimaryKey("MES_PLAN_KEY")]
    public class P_PLAN : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 计划ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("计划ID")]
        public string MES_PLAN_KEY { get; set; }
        /// <summary>
        /// 计划编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("计划编号")]
        public string MES_PLAN_CODE { get; set; }
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
        /// 产品编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品编号")]
        public string PART_CODE { get; set; }
        /// <summary>
        /// 计划数量
        /// </summary>
        /// <returns></returns>
        [DisplayName("计划数量")]
        public int? PLAN_NUM { get; set; }
        /// <summary>
        /// 计划日期
        /// </summary>
        /// <returns></returns>
        [DisplayName("计划日期")]
        public DateTime? PLAN_DATE { get; set; }
        /// <summary>
        /// 计划开始时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("计划开始时间")]
        public DateTime? PLAN_START_TIME { get; set; }
        /// <summary>
        /// 计划结束时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("计划结束时间")]
        public DateTime? PLAN_ENDING_TIME { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [DisplayName("备注")]
        public string REMARKS { get; set; }
        /// <summary>
        /// 完工仓库
        /// </summary>
        /// <returns></returns>
        [DisplayName("完工仓库")]
        public string COMPLETED_WAREHOUSE { get; set; }
        /// <summary>
        /// 计划来源
        /// </summary>
        /// <returns></returns>
        [DisplayName("计划来源")]
        public string PLAN_SOURCE { get; set; }
        /// <summary>
        /// 是否异常
        /// </summary>
        /// <returns></returns>
        [DisplayName("是否异常")]
        public string EXCEPTION_FLAG { get; set; }
        /// <summary>
        /// 计划执行状态
        /// </summary>
        /// <returns></returns>
        [DisplayName("计划执行状态")]
        public string EXECUTION_STATUS { get; set; }
        /// <summary>
        /// 计划最后更新时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("计划最后更新时间")]
        public DateTime? LAST_UPDATE_DATE { get; set; }
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
        /// 预留字段6
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段6")]
        public string RESERVE6 { get; set; }
        /// <summary>
        /// 预留字段7
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段7")]
        public string RESERVE7 { get; set; }
        /// <summary>
        /// 预留字段8
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段8")]
        public string RESERVE8 { get; set; }
        /// <summary>
        /// 预留字段9
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段9")]
        public string RESERVE9 { get; set; }
        /// <summary>
        /// 预留字段10
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段10")]
        public string RESERVE10 { get; set; }
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
        /// 实际开始时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("实际开始时间")]
        public DateTime? ACTUAL_START_TIME { get; set; }
        /// <summary>
        /// 实际结束时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("实际结束时间")]
        public DateTime? ACTUAL_ENDING_TIME { get; set; }
        /// <summary>
        /// 上线数量
        /// </summary>
        /// <returns></returns>
        [DisplayName("上线数量")]
        public int? ONLINE_NUM { get; set; }
        /// <summary>
        /// 下线数量
        /// </summary>
        /// <returns></returns>
        [DisplayName("下线数量")]
        public int? OFFLINE_NUM { get; set; }
        /// <summary>
        /// 工作中心
        /// </summary>
        /// <returns></returns>
        [DisplayName("工作中心")]
        public string WORK_CENTER_KEY { get; set; }
        /// <summary>
        /// 计划完成状态
        /// </summary>
        /// <returns></returns>
        [DisplayName("计划完成状态")]
        public string COMPLETE_STATUS { get; set; }
        /// <summary>
        /// 生产顺序
        /// </summary>
        /// <returns></returns>
        [DisplayName("生产顺序")]
        public int? EXECUTION_QUEUE_NO { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.MES_PLAN_KEY = CommonHelper.GetGuid;
            this.CREATEDATE = ServerTime.Now;
            //this.CREATEUSERID = ManageProvider.Provider.Current().UserId;
            //this.CREATEUSERNAME = ManageProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.MES_PLAN_KEY = KeyValue;
            this.MODIFYDATE = ServerTime.Now;
            //this.MODIFYUSERID = ManageProvider.Provider.Current().UserId;
            //this.MODIFYUSERNAME = ManageProvider.Provider.Current().UserName;
        }
        #endregion
    }
}