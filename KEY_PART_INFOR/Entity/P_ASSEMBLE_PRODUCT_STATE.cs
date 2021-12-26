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
    /// 生产状态过程表
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.12.20 22:22</date>
    /// </author>
    /// </summary>
    [Description("生产状态过程表")]
    [PrimaryKey("ASSEMBLE_PRODUCT_STATE_KEY")]
    public class P_ASSEMBLE_PRODUCT_STATE : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 生产状态过程表主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("生产状态过程表主键")]
        public string ASSEMBLE_PRODUCT_STATE_KEY { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("用户编号")]
        public string ACCOUNT_CODE { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        /// <returns></returns>
        [DisplayName("用户姓名")]
        public string ACCOUNT_NAME { get; set; }
        /// <summary>
        /// 公司主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("公司主键")]
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
        /// 工厂主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("工厂主键")]
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
        /// 车间主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("车间主键")]
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
        /// 生产线主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("生产线主键")]
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
        /// 工作中心主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("工作中心主键")]
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
        /// 设备主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("设备主键")]
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
        /// 班制主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("班制主键")]
        public string SHIFT_KEY { get; set; }
        /// <summary>
        /// 班制编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("班制编号")]
        public string SHIFT_CODE { get; set; }
        /// <summary>
        /// 班制名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("班制名称")]
        public string SHIFT_NAME { get; set; }
        /// <summary>
        /// 班组主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("班组主键")]
        public string TEAM_KEY { get; set; }
        /// <summary>
        /// 班组编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("班组编号")]
        public string TEAM_CODE { get; set; }
        /// <summary>
        /// 班组名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("班组名称")]
        public string TEAM_NAME { get; set; }
        /// <summary>
        /// 人员主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("人员主键")]
        public string STAFF_KEY { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("人员编号")]
        public string STAFF_CODE { get; set; }
        /// <summary>
        /// 人员姓名
        /// </summary>
        /// <returns></returns>
        [DisplayName("人员姓名")]
        public string STAFF_NAME { get; set; }
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
        /// 计划数量
        /// </summary>
        /// <returns></returns>
        [DisplayName("计划数量")]
        public int? PLAN_NUM { get; set; }
        /// <summary>
        /// 计划序列号
        /// </summary>
        /// <returns></returns>
        [DisplayName("计划序列号")]
        public string PLAN_NO { get; set; }
        /// <summary>
        /// 物料BOM主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("物料BOM主键")]
        public string BOM_KEY { get; set; }
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
        /// 产品简易码
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品简易码")]
        public string PRODUCT_ABB { get; set; }
        /// <summary>
        /// 产品批次号
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品批次号")]
        public string PRODUCT_BATCH_NO { get; set; }
        /// <summary>
        /// 产品出生证
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品出生证")]
        public string PRODUCT_BORN_CODE { get; set; }
        /// <summary>
        /// 产品模型号
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品模型号")]
        public string PRODUCT_MODEL_NO { get; set; }
        /// <summary>
        /// 产品流水号
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品流水号")]
        public string PRODUCT_SERIAL_NO { get; set; }
        /// <summary>
        /// 产品结构号
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品结构号")]
        public string PRODUCT_STRUCT_NO { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品类型")]
        public string PRODUCT_TYPE { get; set; }
        /// <summary>
        /// 配送车间主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("配送车间主键")]
        public string DISTRIBUTION_WORKSHOP_KEY { get; set; }
        /// <summary>
        /// 配送车间编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("配送车间编号")]
        public string DISTRIBUTION_WORKSHOP_CODE { get; set; }
        /// <summary>
        /// 配送车间名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("配送车间名称")]
        public string DISTRIBUTION_WORKSHOP_NAME { get; set; }
        /// <summary>
        /// 合格状态
        /// </summary>
        /// <returns></returns>
        [DisplayName("合格状态")]
        public string IS_OK { get; set; }
        /// <summary>
        /// 操作状态
        /// </summary>
        /// <returns></returns>
        [DisplayName("操作状态")]
        public string OPERATED_STATE { get; set; }
        /// <summary>
        /// 是否为返修品
        /// </summary>
        /// <returns></returns>
        [DisplayName("是否为返修品")]
        public string IS_REPAIR { get; set; }
        /// <summary>
        /// 返修频次
        /// </summary>
        /// <returns></returns>
        [DisplayName("返修频次")]
        public int? REPAIR_FREQUENCY { get; set; }
        /// <summary>
        /// 总装上线时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("总装上线时间")]
        public DateTime? ASSEMBLE_ONLINE_TIME { get; set; }
        /// <summary>
        /// 总装下线时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("总装下线时间")]
        public DateTime? ASSEMBLE_OFFLINE_TIME { get; set; }
        /// <summary>
        /// 存储时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("存储时间")]
        public DateTime? STORAGE_TIME { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [DisplayName("备注")]
        public string REMARKS { get; set; }
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
        /// 预留01
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留01")]
        public string RESERVE01 { get; set; }
        /// <summary>
        /// 预留02
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留02")]
        public string RESERVE02 { get; set; }
        /// <summary>
        /// 预留03
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留03")]
        public string RESERVE03 { get; set; }
        /// <summary>
        /// 预留04
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留04")]
        public string RESERVE04 { get; set; }
        /// <summary>
        /// 预留05
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留05")]
        public string RESERVE05 { get; set; }
        /// <summary>
        /// 预留06
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留06")]
        public string RESERVE06 { get; set; }
        /// <summary>
        /// 预留07
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留07")]
        public string RESERVE07 { get; set; }
        /// <summary>
        /// 预留08
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留08")]
        public string RESERVE08 { get; set; }
        /// <summary>
        /// 预留09
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留09")]
        public string RESERVE09 { get; set; }
        /// <summary>
        /// 预留10
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留10")]
        public string RESERVE10 { get; set; }
        /// <summary>
        /// 工位ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("工位ID")]
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
        /// 停止器ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("停止器ID")]
        public string STOPPER_KEY { get; set; }
        /// <summary>
        /// 停止器编码
        /// </summary>
        /// <returns></returns>
        [DisplayName("停止器编码")]
        public string STOPPER_CODE { get; set; }
        /// <summary>
        /// 停止器名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("停止器名称")]
        public string STOPPER_NAME { get; set; }
        /// <summary>
        /// 加工中心id
        /// </summary>
        /// <returns></returns>
        [DisplayName("加工中心id")]
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
        /// 热试上线时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("热试上线时间")]
        public DateTime? HOT_TEST_ONLINE_TIME { get; set; }
        /// <summary>
        /// 热试下线时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("热试下线时间")]
        public DateTime? HOT_TEST_OFFLINE_TIME { get; set; }
        /// <summary>
        /// 涂装上线时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("涂装上线时间")]
        public DateTime? PAINTING_ONLINE_TIME { get; set; }
        /// <summary>
        /// 涂装下线时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("涂装下线时间")]
        public DateTime? PAINTING_OFFLINE_TIME { get; set; }
        /// <summary>
        /// 挂合格证时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("挂合格证时间")]
        public DateTime? HANGING_CERTIFICATE_TIME { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        //public override void Create()
        //{
        //    this.ASSEMBLE_PRODUCT_STATE_KEY = CommonHelper.GetGuid;
        //    this.CREATEDATE = DateTime.Now;
        //    this.CREATEUSERID = ManageProvider.Provider.Current().UserId;
        //    this.CREATEUSERNAME = ManageProvider.Provider.Current().UserName;
        //}
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        //public override void Modify(string KeyValue)
        //{
        //    this.ASSEMBLE_PRODUCT_STATE_KEY = KeyValue;
        //    this.MODIFYDATE = DateTime.Now;
        //    this.MODIFYUSERID = ManageProvider.Provider.Current().UserId;
        //    this.MODIFYUSERNAME = ManageProvider.Provider.Current().UserName;
        //}
        #endregion
    }
}