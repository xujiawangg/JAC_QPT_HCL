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
    /// 工位
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.01.21 17:00</date>
    /// </author>
    /// </summary>
    [Description("工位")]
    [PrimaryKey("STATION_KEY")]
    public class STATION : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("主键")]
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
        /// 工作中心key
        /// </summary>
        /// <returns></returns>
        [DisplayName("工作中心key")]
        public string WORK_CENTER_KEY { get; set; }
        /// <summary>
        /// 生产线key
        /// </summary>
        /// <returns></returns>
        [DisplayName("生产线key")]
        public string PRODUCTION_LINE_KEY { get; set; }
        /// <summary>
        /// 工位描述
        /// </summary>
        /// <returns></returns>
        [DisplayName("工位描述")]
        public string STATION_DESCRIPTION { get; set; }
        /// <summary>
        /// station_beat
        /// </summary>
        /// <returns></returns>
        [DisplayName("station_beat")]
        public int? STATION_BEAT { get; set; }
        /// <summary>
        /// 工位类型
        /// </summary>
        /// <returns></returns>
        [DisplayName("工位类型")]
        public string STATION_TYPE { get; set; }
        /// <summary>
        /// inst_list_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("inst_list_key")]
        public string INST_LIST_KEY { get; set; }
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
        /// 创建人名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建人名称")]
        public string CREATEUSERNAME { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改日期")]
        public string MODIFYDATE { get; set; }
        /// <summary>
        /// 修改人key
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改人key")]
        public string MODIFYUSERID { get; set; }
        /// <summary>
        /// 修改人名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改人名称")]
        public string MODIFYUSERNAME { get; set; }
        /// <summary>
        /// 预留1
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留1")]
        public string RESERVE01 { get; set; }
        /// <summary>
        /// 预留2
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留2")]
        public string RESERVE02 { get; set; }
        /// <summary>
        /// 预留3
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留3")]
        public string RESERVE03 { get; set; }
        /// <summary>
        /// 预留4
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留4")]
        public string RESERVE04 { get; set; }
        /// <summary>
        /// 预留5
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留5")]
        public string RESERVE05 { get; set; }
        /// <summary>
        /// 预留6
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留6")]
        public string RESERVE06 { get; set; }
        /// <summary>
        /// 预留7
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留7")]
        public string RESERVE07 { get; set; }
        /// <summary>
        /// 预留8
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留8")]
        public string RESERVE08 { get; set; }
        /// <summary>
        /// 预留9
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留9")]
        public string RESERVE09 { get; set; }
        /// <summary>
        /// 预留10
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留10")]
        public string RESERVE10 { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        /// <returns></returns>
        [DisplayName("删除标识")]
        public int? DELETEMARK { get; set; }
        /// <summary>
        /// 是否主工位
        /// </summary>
        /// <returns></returns>
        [DisplayName("是否主工位")]
        public string IS_MAIN { get; set; }
        #endregion
    }
}