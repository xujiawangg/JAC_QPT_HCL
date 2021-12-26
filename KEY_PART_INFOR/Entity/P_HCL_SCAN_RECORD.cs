//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2017
// Software Developers @ HfutIE 2017
//=====================================================================================

using System;
using System.ComponentModel;
using System.Text;

namespace HfutIe
{
    /// <summary>
    /// ADON_TYPE
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.03.21 20:36</date>
    /// </author>
    /// </summary>
        [Description("P_HCL_SCAN_RECORD")]
    [PrimaryKey("HCL_SCAN_RECORD_KEY")]
    public class P_HCL_SCAN_RECORD : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("HCL_SCAN_RECORD_KEY")]
        public string HCL_SCAN_RECORD_KEY { get; set; }
        /// <summary>
        /// 条码号
        /// </summary>
        /// <returns></returns>
        [DisplayName("PART_VINCODE")]
        public string PART_VINCODE { get; set; }
        /// <summary>
        /// 处理标识
        /// </summary>
        /// <returns></returns>
        [DisplayName("FLAG")]
        public string FLAG { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEDATE")]
        public DateTime? CREATEDATE { get; set; }
        /// <summary>
        /// 创建人id
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEUSERID")]
        public string CREATEUSERID { get; set; }
        /// <summary>
        /// 创建人name
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEUSERNAME")]
        public string CREATEUSERNAME { get; set; }
        /// <summary>
        /// 处理时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("HANDLEDATE")]
        public DateTime? HANDLEDATE { get; set; }
        /// <summary>
        /// 预留1
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE1")]
        public string RESERVE1 { get; set; }
        /// 预留2
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE2")]
        public string RESERVE2 { get; set; }
        /// <summary>
        /// 后处理岗位
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE3")]
        public string RESERVE3 { get; set; }
        /// 预留2
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE4")]
        public string RESERVE4 { get; set; }
        /// 总成号
        /// </summary>
        /// <returns></returns>
        [DisplayName("ASSEMBLE_CODE")]
        public string ASSEMBLE_CODE { get; set; }
        /// 顺序
        /// </summary>
        /// <returns></returns>
        [DisplayName("SEQUENCE")]
        public int? SEQUENCE { get; set; }
        /// 物料主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("PART_KEY")]
        public string PART_KEY { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.HCL_SCAN_RECORD_KEY = CommonHelper.GetGuid;
            this.CREATEDATE = ServerTime.Now;
            this.FLAG = "N";
        }
        #endregion
    }
}