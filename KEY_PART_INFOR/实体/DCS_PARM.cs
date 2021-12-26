//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2019
// Software Developers @ HfutIE 2019
//=====================================================================================

using HfutIe;
using System;
using System.ComponentModel;
using System.Configuration;

namespace HfutIE.Entity
{
    /// <summary>
    /// DCS检测项表
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.03.02 09:39</date>
    /// </author>
    /// </summary>
    [Description("DCS检测项表")]
    [PrimaryKey("PARM_KEY")]
    public class DCS_PARM : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 检测项key
        /// </summary>
        /// <returns></returns>
        [DisplayName("检测项key")]
        public string PARM_KEY { get; set; }
        /// <summary>
        /// DCS_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("DCS_key")]
        public string DCS_KEY { get; set; }
        /// <summary>
        /// ParmCode
        /// </summary>
        /// <returns></returns>
        [DisplayName("ParmCode")]
        public string PARM_CODE { get; set; }
        /// <summary>
        /// 检测项名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("检测项名称")]
        public string PARM_NAME { get; set; }
        /// <summary>
        /// TextMask
        /// </summary>
        /// <returns></returns>
        [DisplayName("TextMask")]
        public string TEXT_MASK { get; set; }
        /// <summary>
        /// 检测项类型
        /// </summary>
        /// <returns></returns>
        [DisplayName("检测项类型")]
        public string PARM_TYPE { get; set; }
        /// <summary>
        /// 检测项描述
        /// </summary>
        /// <returns></returns>
        [DisplayName("检测项描述")]
        public string PARM_DESCRIPTION { get; set; }
        /// <summary>
        /// 上控制界限
        /// </summary>
        /// <returns></returns>
        [DisplayName("上控制界限")]
        public Single? UPPER_CONTROL { get; set; }
        /// <summary>
        /// 标准值
        /// </summary>
        /// <returns></returns>
        [DisplayName("标准值")]
        public Single? TARGET { get; set; }
        /// <summary>
        /// 下控制界限
        /// </summary>
        /// <returns></returns>
        [DisplayName("下控制界限")]
        public Single? LOWER_CONTROL { get; set; }
        /// <summary>
        /// ProcessUpperLimit
        /// </summary>
        /// <returns></returns>
        [DisplayName("ProcessUpperLimit")]
        public Single? PROCESS_UPPER_LIMIT { get; set; }
        /// <summary>
        /// ProcessLowerLimit
        /// </summary>
        /// <returns></returns>
        [DisplayName("ProcessLowerLimit")]
        public Single? PROCESS_LOWER_LIMIT { get; set; }
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
        public override void Create()
        {
            this.PARM_KEY = CommonHelper.GetGuid;
            this.CREATEDATE = DateTime.Now.ToString();
            this.CREATEUSERID = ConfigurationManager.AppSettings["ProgramName"].ToString();
            this.CREATEUSERNAME = ConfigurationManager.AppSettings["ProgramName"].ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.PARM_KEY = KeyValue;
            this.MODIFYDATE = DateTime.Now.ToString();
            this.CREATEUSERID = ConfigurationManager.AppSettings["ProgramName"].ToString();
            this.CREATEUSERNAME = ConfigurationManager.AppSettings["ProgramName"].ToString();
        }
        #endregion
    }
}