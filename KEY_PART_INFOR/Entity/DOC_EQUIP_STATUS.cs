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
    /// 设备历史状态表
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.12.12 10:21</date>
    /// </author>
    /// </summary>
    [Description("设备历史状态表")]
    [PrimaryKey("DOC_EQUIP_STATUS_KEY")]
    public class DOC_EQUIP_STATUS : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// key
        /// </summary>
        /// <returns></returns>
        [DisplayName("key")]
        public string DOC_EQUIP_STATUS_KEY { get; set; }
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
        /// 设备状态
        /// </summary>
        /// <returns></returns>
        [DisplayName("设备状态")]
        public string EQUIPMENT_STATUS { get; set; }
        /// <summary>
        /// 持续时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("持续时间")]
        public Single? DURATION { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("开始时间")]
        public DateTime? STARTDATE { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("结束时间")]
        public DateTime? ENDDATE { get; set; }
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
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        [DisplayName("删除标记")]
        public int? DELETEMARK { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DisplayName("")]
        public string RESERVE1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [DisplayName("")]
        public string RESERVE2 { get; set; }
        /// <summary>
        /// 地址名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("地址名称")]
        public string ADDRESS_NAME { get; set; }
        /// <summary>
        /// 地址路径
        /// </summary>
        /// <returns></returns>
        [DisplayName("地址路径")]
        public string ADDRESS_PATH { get; set; }
        /// <summary>
        /// 生产线key
        /// </summary>
        /// <returns></returns>
        [DisplayName("生产线key")]
        public string PRODUCTION_LINE_KEY { get; set; }
        /// <summary>
        /// 工位key
        /// </summary>
        /// <returns></returns>
        [DisplayName("工位key")]
        public string STATION_KEY { get; set; }
        /// <summary>
        /// 设备key
        /// </summary>
        /// <returns></returns>
        [DisplayName("设备key")]
        public string EQUIPMENT_KEY { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.DOC_EQUIP_STATUS_KEY = CommonHelper.GetGuid;
            this.DELETEMARK = 0;
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
            this.DOC_EQUIP_STATUS_KEY = KeyValue;
                                            }
        #endregion
    }
}