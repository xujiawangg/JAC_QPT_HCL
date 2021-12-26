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
    /// 设备点检结果表
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.12.11 21:00</date>
    /// </author>
    /// </summary>
    [Description("设备点检结果表")]
    [PrimaryKey("CHECH_RES_KEY")]
    public class CHECK_EQUIP_RESULT : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// key
        /// </summary>
        /// <returns></returns>
        [DisplayName("key")]
        public string CHECH_RES_KEY { get; set; }
        /// <summary>
        /// 点检信息key
        /// </summary>
        /// <returns></returns>
        [DisplayName("点检信息key")]
        public string CHECK_INFO_KEY { get; set; }
        /// <summary>
        /// 点检信息编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("点检信息编号")]
        public string CHECK_INFO_CODE { get; set; }
        /// <summary>
        /// 点检信息名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("点检信息名称")]
        public string CHECK_INFO_NAME { get; set; }
        /// <summary>
        /// 点检信息描述
        /// </summary>
        /// <returns></returns>
        [DisplayName("点检信息描述")]
        public string CHECK_INFO_DESCRIPTION { get; set; }
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
        /// 点检结果
        /// </summary>
        /// <returns></returns>
        [DisplayName("点检结果")]
        public string CHECK_RESULT { get; set; }
        /// <summary>
        /// 点检时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("点检时间")]
        public DateTime? CHECK_TIME { get; set; }
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
        /// 创建人姓名
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建人姓名")]
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
            this.CHECH_RES_KEY = CommonHelper.GetGuid;
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
            this.CHECH_RES_KEY = KeyValue;
        }
        #endregion
    }
}