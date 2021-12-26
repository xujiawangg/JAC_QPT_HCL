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
    /// 设备基本表
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.12.11 19:58</date>
    /// </author>
    /// </summary>
    [Description("设备基本表")]
    [PrimaryKey("EQUIPMENT_KEY")]
    public class EQUIPMENT : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 设备ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("设备ID")]
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
        /// 设备描述
        /// </summary>
        /// <returns></returns>
        [DisplayName("设备描述")]
        public string EQUIPMENT_DESCRIPTION { get; set; }
        /// <summary>
        /// 设备类别
        /// </summary>
        /// <returns></returns>
        [DisplayName("设备类别")]
        public string EQUIPMENT_TYPE { get; set; }
        /// <summary>
        /// 工作模式
        /// </summary>
        /// <returns></returns>
        [DisplayName("工作模式")]
        public string EQUIPMENT_MODEL { get; set; }
        /// <summary>
        /// 设备IP地址
        /// </summary>
        /// <returns></returns>
        [DisplayName("设备IP地址")]
        public string EQUIPMENT_IP { get; set; }
        /// <summary>
        /// 理论节拍
        /// </summary>
        /// <returns></returns>
        [DisplayName("理论节拍")]
        public int? EQUIPMENT_BEAT { get; set; }
        /// <summary>
        /// 是否监控
        /// </summary>
        /// <returns></returns>
        [DisplayName("是否监控")]
        public string EQUIPMENT_MONITOR { get; set; }
        /// <summary>
        /// 供应商ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("供应商ID")]
        public string SUPPLIER_KEY { get; set; }
        /// <summary>
        /// 关联图片ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("关联图片ID")]
        public string INSTLIST_KEY { get; set; }
        /// <summary>
        /// 生产线ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("生产线ID")]
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
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建时间")]
        public string CREATEDATE { get; set; }
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
        public string MODIFYDATE { get; set; }
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
        /// DeleteStatus
        /// </summary>
        /// <returns></returns>
        [DisplayName("DeleteStatus")]
        public string DELETE_STATUS { get; set; }
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
        /// 删除状态
        /// </summary>
        /// <returns></returns>
        [DisplayName("删除状态")]
        public int? DELETEMARK { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        //public override void Create()
        //{
        //    this.EQUIPMENT_KEY = CommonHelper.GetGuid;
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
        //    this.EQUIPMENT_KEY = KeyValue;
        //    this.MODIFYDATE = DateTime.Now;
        //    this.MODIFYUSERID = ManageProvider.Provider.Current().UserId;
        //    this.MODIFYUSERNAME = ManageProvider.Provider.Current().UserName;
        //}
        #endregion
    }
}