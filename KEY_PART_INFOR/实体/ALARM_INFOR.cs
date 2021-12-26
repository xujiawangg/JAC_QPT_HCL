//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2017
// Software Developers @ HfutIE 2017
//=====================================================================================

 
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HfutIe.Entity
{
    /// <summary>
    /// 报警信息
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.03.14 09:35</date>
    /// </author>
    /// </summary>
    [Description("报警信息")]
 
    public class ALARM_INFOR  
    {
        #region 获取/设置 字段值
        /// <summary>
        /// alarm_infor_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("alarm_infor_key")]
        public string alarm_infor_key { get; set; }
        /// <summary>
        /// alarm_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("alarm_code")]
        public string alarm_code { get; set; }
        /// <summary>
        /// alarm_description
        /// </summary>
        /// <returns></returns>
        [DisplayName("alarm_description")]
        public string alarm_description { get; set; }
        /// <summary>
        /// alarm_grade
        /// </summary>
        /// <returns></returns>
        [DisplayName("alarm_grade")]
        public int? alarm_grade { get; set; }
        /// <summary>
        /// alarm_type
        /// </summary>
        /// <returns></returns>
        [DisplayName("alarm_type")]
        public string alarm_type { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建时间")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建用户主键")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建用户")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改时间")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改用户主键")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改用户")]
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public  void Create()
        {
            this.alarm_infor_key = CommonHelper.GetGuid;
            this.CreateDate = DateTime.Now;
            this.CreateUserId = SystemLog.UserKey;
            this.CreateUserName = SystemLog.UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public   void Modify(string KeyValue)
        {
            this.alarm_infor_key = KeyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = SystemLog.UserKey;
            this.ModifyUserName = SystemLog.UserName;
        }
        #endregion
    }
}