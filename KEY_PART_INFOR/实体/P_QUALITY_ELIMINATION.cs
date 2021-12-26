//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2019
// Software Developers @ HfutIE 2019
//=====================================================================================
using HfutIe;
using HfutIE.Utilities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Text;

namespace HfutIE.Entity
{
    /// <summary>
    /// 在制品质量信息销项表
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.02.28 15:56</date>
    /// </author>
    /// </summary>
    [Description("在制品质量信息销项表")]
    [PrimaryKey("P_QUALITY_ELIMINATIONKEY")]
    public class P_QUALITY_ELIMINATION : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("主键")]
        public string P_QUALITY_ELIMINATIONKEY { get; set; }
        /// <summary>
        /// 产品出生证
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品出生证")]
        public string PRODUCT_BORN_CODE { get; set; }
        /// <summary>
        /// DCS主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("DCS主键")]
        public string DCS_KEY { get; set; }
        /// <summary>
        /// 检测项主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("检测项主键")]
        public string PARM_KEY { get; set; }
        /// <summary>
        /// 是否检测
        /// </summary>
        /// <returns></returns>
        [DisplayName("是否检测")]
        public string IS_CLT { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建日期")]
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
        /// 检测结果
        /// </summary>
        /// <returns></returns>
        [DisplayName("检测结果")]
        public string CLT_RESULT { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [DisplayName("备注")]
        public string REMARKS { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.P_QUALITY_ELIMINATIONKEY = CommonHelper.GetGuid;
            this.CREATEDATE = DateTime.Now;
            this.CREATEUSERID = ConfigurationManager.AppSettings["ProgramName"].ToString();
            this.CREATEUSERNAME = ConfigurationManager.AppSettings["ProgramName"].ToString(); ;
        }
        #endregion
    }
}