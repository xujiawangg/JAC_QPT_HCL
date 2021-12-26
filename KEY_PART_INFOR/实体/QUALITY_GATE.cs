//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2019
// Software Developers @ HfutIE 2019
//=====================================================================================

using HfutIe;
using HfutIe.DataAccess.Attributes;
using HfutIe.Entity;
using System;
using System.ComponentModel;

namespace HfutIE.Entity
{
    /// <summary>
    /// 已配置质量门表格
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.03.04 09:28</date>
    /// </author>
    /// </summary>
    [Description("已配置质量门表格")]
    [PrimaryKey("QUALITY_GATE_KEY")]
    public class QUALITY_GATE : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 已配置质量门信息主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("已配置质量门信息主键")]
        public string QUALITY_GATE_KEY { get; set; }
        /// <summary>
        /// 工位主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("工位主键")]
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
        /// 工位描述
        /// </summary>
        /// <returns></returns>
        [DisplayName("工位描述")]
        public string STATION_DESCRIPTIN { get; set; }
        /// <summary>
        /// 工位类型
        /// </summary>
        /// <returns></returns>
        [DisplayName("工位类型")]
        public string STATION_TYPE { get; set; }
        /// <summary>
        /// 是否主工位
        /// </summary>
        /// <returns></returns>
        [DisplayName("是否主工位")]
        public string IS_MAIN { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [DisplayName("备注")]
        public string REMARK { get; set; }
        /// <summary>
        /// 所属质量门主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("所属质量门主键")]
        public string CFG_STATION_KEY { get; set; }
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
        /// 创建人名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建人名称")]
        public string CREATEUSERNAME { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改时间")]
        public DateTime? MODIFYDATE { get; set; }
        /// <summary>
        /// 修改人主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改人主键")]
        public string MODIFYUSERID { get; set; }
        /// <summary>
        /// 修改人名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改人名称")]
        public string MODIFYUSERNAME { get; set; }
        /// <summary>
        /// 预留字段
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段")]
        public string RESERVE01 { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.QUALITY_GATE_KEY = CommonHelper.GetGuid;
            this.CREATEDATE = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        #endregion
    }
}