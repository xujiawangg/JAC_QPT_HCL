//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2019
// Software Developers @ HfutIE 2019
//=====================================================================================

using HfutIe.DataAccess.Attributes;
using HfutIe.Entity;
using HfutIe.Help;
using System;
using System.ComponentModel;
using System.Configuration;

namespace HfutIE.Entity
{
    /// <summary>
    /// DOC_TIGHTENING
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.12.01 15:42</date>
    /// </author>
    /// </summary>
    [Description("P_TIGHTENING")]
    public class P_TIGHTENING : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// tightening_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("tightening_key")]
        public string tightening_key { get; set; }
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
        /// date
        /// </summary>
        /// <returns></returns>
        [DisplayName("date")]
        public string Check_Time { get; set; }
        public string bolt_name { get; set; }//螺栓号
        public string Parm_Name { get; set; }//轴号
        /// <summary>
        /// upper_control_torque
        /// </summary>
        /// <returns></returns>
        [DisplayName("upper_control_torque")]
        public string upper_control_torque { get; set; }
        /// <summary>
        /// upper_value_torque
        /// </summary>
        /// <returns></returns>
        [DisplayName("upper_value_torque")]
        public string upper_value_torque { get; set; }
        /// <summary>
        /// target_value_torque
        /// </summary>
        /// <returns></returns>
        [DisplayName("target_value_torque")]
        public string target_value_torque { get; set; }
        /// <summary>
        /// lower_value_torque
        /// </summary>
        /// <returns></returns>
        [DisplayName("lower_value_torque")]
        public string lower_value_torque { get; set; }
        /// <summary>
        /// lower_control_torque
        /// </summary>
        /// <returns></returns>
        [DisplayName("lower_control_torque")]
        public string lower_control_torque { get; set; }
        /// <summary>
        /// check_value_torque
        /// </summary>
        /// <returns></returns>
        [DisplayName("check_value_torque")]
        public string  check_value_torque { get; set; }

        /// <summary>
        /// upper_control_angle
        /// </summary>
        /// <returns></returns>
        [DisplayName("upper_control_angle")]
        public string upper_control_angle { get; set; }
        /// <summary>
        /// upper_value_angle
        /// </summary>
        /// <returns></returns>
        [DisplayName("upper_value_angle")]
        public string upper_value_angle { get; set; }
        /// <summary>
        /// target_value_angle
        /// </summary>
        /// <returns></returns>
        [DisplayName("target_value_angle")]
        public string target_value_angle { get; set; }
        /// <summary>
        /// lower_value_angle
        /// </summary>
        /// <returns></returns>
        [DisplayName("lower_value_angle")]
        public string lower_value_angle { get; set; }
        /// <summary>
        /// lower_control_angle
        /// </summary>
        /// <returns></returns>
        [DisplayName("lower_control_angle")]
        public string lower_control_angle { get; set; }
        /// <summary>
        /// check_value_angle
        /// </summary>
        /// <returns></returns>
        [DisplayName("check_value_angle")]
        public string check_value_angle { get; set; }
        /// <summary>
        /// IS_QUALIFIED
        /// </summary>
        /// <returns></returns>
        [DisplayName("IS_QUALIFIED")]
        public string is_qualified { get; set; }
        /// <summary>
        /// ORDERID
        /// </summary>
        /// <returns></returns>
        [DisplayName("ORDERID")]
        public string ORDERID { get; set; }
        /// <summary>
        /// 数据来源类型
        /// </summary>
        /// <returns></returns>
        [DisplayName("SOURCE_TYPE")]
        public string SOURCE_TYPE { get; set; }
        /// <summary>
        /// RESERVE01
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE01")]
        public string reserve1 { get; set; }
        /// <summary>
        /// RESERVE02
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE02")]
        public string reserve2 { get; set; }
        /// <summary>
        /// RESERVE03
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE03")]
        public string reserve3 { get; set; }
        /// <summary>
        /// RESERVE04
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE04")]
        public string reserve4 { get; set; }
        /// <summary>
        /// RESERVE05
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE05")]
        public string reserve5 { get; set; }
        /// <summary>
        /// CREATEDATE
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEDATE")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// CREATEUSERID
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEUSERID")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// CREATEUSERNAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEUSERNAME")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// MODIFYDATE
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFYDATE")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// MODIFYUSERID
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFYUSERID")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// MODIFYUSERNAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFYUSERNAME")]
        public string ModifyUserName { get; set; }
        public int? DeleteMark { get; set; }
        #endregion

        //#region 扩展操作
        ///// <summary>
        ///// 新增调用
        ///// </summary>
        //public override void Create()
        //{
        //    this.tightening_key = HfutIe.CommonHelper.GetGuid;
        //    this.CreateDate = ServerTime.Now;
        //    this.CreateUserId = ConfigurationManager.AppSettings["ProgramName"].ToString();
        //    this.CreateUserName = ConfigurationManager.AppSettings["ProgramName"].ToString();
        //}
        ///// <summary>
        ///// 编辑调用
        ///// </summary>
        ///// <param name="KeyValue"></param>
        //public override void Modify(string KeyValue)
        //{
        //    this.tightening_key = KeyValue;
        //    this.ModifyDate = ServerTime.Now;
        //    this.CreateUserId = ConfigurationManager.AppSettings["ProgramName"].ToString();
        //    this.CreateUserName = ConfigurationManager.AppSettings["ProgramName"].ToString();
        //}
        //#endregion
    }
}