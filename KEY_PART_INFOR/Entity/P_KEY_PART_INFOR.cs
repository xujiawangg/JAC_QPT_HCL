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
    /// 安全件采集过程表
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.11.28 18:48</date>
    /// </author>
    /// </summary>
    [Description("安全件采集过程表")]
    [PrimaryKey("KEY_PART_INFOR_KEY")]
    public class P_KEY_PART_INFOR : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("主键")]
        public string KEY_PART_INFOR_KEY { get; set; }
        /// <summary>
        /// 安全件配置主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("安全件配置主键")]
        public string KEY_PART_C_KEY { get; set; }
        /// <summary>
        /// 装配时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("装配时间")]
        public DateTime? ASSMBLY_TIME { get; set; }
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
        /// 设备编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("设备编号")]
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
        /// 设备key
        /// </summary>
        /// <returns></returns>
        [DisplayName("设备key")]
        public string EQUIP_KEY { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("设备编号")]
        public string EQUIP_CODE { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("设备名称")]
        public string EQUIP_NAME { get; set; }
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
        /// 班次key
        /// </summary>
        /// <returns></returns>
        [DisplayName("班次key")]
        public string SHIFT_KEY { get; set; }
        /// <summary>
        /// 班次编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("班次编号")]
        public string SHIFT_CODE { get; set; }
        /// <summary>
        /// 班次名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("班次名称")]
        public string SHIFT_NAME { get; set; }
        /// <summary>
        /// 班组key
        /// </summary>
        /// <returns></returns>
        [DisplayName("班组key")]
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
        /// 人员key
        /// </summary>
        /// <returns></returns>
        [DisplayName("人员key")]
        public string STAFF_KEY { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("人员编号")]
        public string STAFF_CODE { get; set; }
        /// <summary>
        /// 人员名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("人员名称")]
        public string STAFF_NAME { get; set; }
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
        /// 产品出生证
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品出生证")]
        public string PRODUCT_BORN_CODE { get; set; }
        /// <summary>
        /// 产品流水号
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品流水号")]
        public string PRODUCT_SERIAL_NO { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品ID")]
        public string PRODUCT_KEY { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品名称")]
        public string PRODUCT_NAME { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品编号")]
        public string PRODUCT_CODE { get; set; }
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
        /// 产品型号
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品型号")]
        public string PRODUCT_MODEL_NO { get; set; }
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
        /// 配送工位主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("配送工位主键")]
        public string DISTRIBUTION_STA_KEY { get; set; }
        /// <summary>
        /// 配送工位编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("配送工位编号")]
        public string DISTRIBUTION_STA_CODE { get; set; }
        /// <summary>
        /// 配送工位名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("配送工位名称")]
        public string DISTRIBUTION_STA_NAME { get; set; }
        /// <summary>
        /// 物料主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("物料主键")]
        public string PART_KEY { get; set; }
        /// <summary>
        /// 物料编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("物料编号")]
        public string PART_CODE { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("物料名称")]
        public string PART_NAME { get; set; }
        /// <summary>
        /// 物料批次号
        /// </summary>
        /// <returns></returns>
        [DisplayName("物料批次号")]
        public string PART_BATCH_NO { get; set; }
        /// <summary>
        /// 物料VIN码
        /// </summary>
        /// <returns></returns>
        [DisplayName("物料VIN码")]
        public string PART_VINCODE { get; set; }
        /// <summary>
        /// 物料条码
        /// </summary>
        /// <returns></returns>
        [DisplayName("物料条码")]
        public string PART_BARCODE { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        /// <returns></returns>
        [DisplayName("数量")]
        public int? QUANTITY { get; set; }
        /// <summary>
        /// 供应商主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("供应商主键")]
        public string SUPPLIER_KEY { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("供应商编号")]
        public string SUPPLIER_CODE { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("供应商名称")]
        public string SUPPLIER_NAME { get; set; }
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
            this.KEY_PART_INFOR_KEY = CommonHelper.GetGuid;
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
            this.KEY_PART_INFOR_KEY = KeyValue;
            this.MODIFYDATE = ServerTime.Now;
            this.MODIFYUSERID = SystemLog.UserKey;
            this.MODIFYUSERNAME = SystemLog.UserName;
        }
        #endregion
    }
}