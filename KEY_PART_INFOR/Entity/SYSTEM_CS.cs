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
    /// 底层操作日志表
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.02.22 17:34</date>
    /// </author>
    /// </summary>
    [Description("底层操作日志表")]
    [PrimaryKey("RECORD_ID")]
    public class SYSTEM_CS : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 记录ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("记录ID")]
        public string RECORD_ID { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("用户编号")]
        public string USER_CODE { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("用户名称")]
        public string USER_NAME { get; set; }
        /// <summary>
        /// 操作
        /// </summary>
        /// <returns></returns>
        [DisplayName("操作")]
        public string CT { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("时间")]
        public DateTime? TIME { get; set; }
        /// <summary>
        /// 公共机名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("公共机名称")]
        public string PC_NAME { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        /// <returns></returns>
        [DisplayName("IP地址")]
        public string IP { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.RECORD_ID = CommonHelper.GetGuid;
            this.TIME = ServerTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.RECORD_ID = KeyValue;
        }
        #endregion
    }
}