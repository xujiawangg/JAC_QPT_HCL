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
    /// AGVT_Log
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.8.21 12:47</date>
    /// </author>
    /// </summary>
    [Description("T_Log")]
    public class T_Log
    {
        #region 获取/设置 字段值
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("ID")]
        public string ID { get; set; }
        /// <summary>
        /// wc_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("wc_code")]
        public string wc_code { get; set; }

        /// <summary>
        /// LogContent
        /// </summary>
        /// <returns></returns>
        [DisplayName("LogContent")]
        public string LogContent { get; set; }
        /// <summary>
        /// StaffOperation
        /// </summary>
        /// <returns></returns>
        [DisplayName("StaffOperation")]
        public string StaffOperation { get; set; }
        /// <summary>
        /// CreateDate
        /// </summary>
        /// <returns></returns>
        [DisplayName("CreateDate")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// CreaterId
        /// </summary>
        /// <returns></returns>
        [DisplayName("CreaterId")]
        public string CreaterId { get; set; }
        /// <summary>
        /// CreaterName
        /// </summary>
        /// <returns></returns>
        [DisplayName("CreaterName")]
        public string CreaterName { get; set; }
        #endregion

        /// <summary>
        /// 新增调用
        /// </summary>
        public  void Create()
        {
            this.ID = CommonHelper.GetGuid;
            this.CreateDate = ServerTime.Now;
            this.CreaterId = SystemLog.UserKey;
            this.CreaterName = SystemLog.UserName;
        }
    }
}