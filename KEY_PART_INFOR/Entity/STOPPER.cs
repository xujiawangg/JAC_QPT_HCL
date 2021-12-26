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
    /// 停止器基本表
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.02.21 15:14</date>
    /// </author>
    /// </summary>
    [Description("停止器基本表")]
    [PrimaryKey("STOPPER_KEY")]
    public class STOPPER : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 停止器key
        /// </summary>
        /// <returns></returns>
        [DisplayName("停止器key")]
        public string STOPPER_KEY { get; set; }
        /// <summary>
        /// 停止器编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("停止器编号")]
        public string STOPPER_CODE { get; set; }
        /// <summary>
        /// 停止器名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("停止器名称")]
        public string STOPPER_NAME { get; set; }
        /// <summary>
        /// 工位key
        /// </summary>
        /// <returns></returns>
        [DisplayName("工位key")]
        public string STATION_KEY { get; set; }
        /// <summary>
        /// 生产线ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("生产线ID")]
        public string PRODUCTION_LINE_KEY { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [DisplayName("备注")]
        public string REMARK { get; set; }
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
        //    this.STOPPER_KEY = CommonHelper.GetGuid;
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
        //    this.STOPPER_KEY = KeyValue;
        //    this.MODIFYDATE = DateTime.Now;
        //    this.MODIFYUSERID = ManageProvider.Provider.Current().UserId;
        //    this.MODIFYUSERNAME = ManageProvider.Provider.Current().UserName;
        //}
        #endregion
    }
}