//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2019
// Software Developers @ HfutIE 2019
//=====================================================================================

using HfutIe;
using System;
using System.ComponentModel;

namespace HfutIE.Entity
{
    /// <summary>
    /// 质量门强制放行信息表
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.03.04 09:28</date>
    /// </author>
    /// </summary>
    [Description("质量门强制放行信息表")]
    [PrimaryKey("QUALITY_GATE_FORCED_RELEASEKEY")]
    public class QUALITY_GATE_FORCED_RELEASE : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 已配置质量门信息主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("主键")]
        public string QUALITY_GATE_FORCED_RELEASEKEY { get; set; }
        /// <summary>
        /// 工位主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品出生证")]
        public string PRODUCT_BORN_CODE { get; set; }
        /// <summary>
        /// 工位编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("工位主键")]
        public string STATION_KEY { get; set; }
        /// <summary>
        /// 工位名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("放行结果")]
        public string RELEASE_RESULT { get; set; }
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
        /// 预留字段
        /// </summary>
        /// <returns></returns>
        [DisplayName("备注")]
        public string REMARK { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.QUALITY_GATE_FORCED_RELEASEKEY = CommonHelper.GetGuid;
            this.CREATEDATE = ServerTime.Now;
            this.CREATEUSERID = SystemLog.UserKey;
            this.CREATEUSERNAME = SystemLog.UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        #endregion
    }
}