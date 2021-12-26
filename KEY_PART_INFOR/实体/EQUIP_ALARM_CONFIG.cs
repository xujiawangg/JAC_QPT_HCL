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
    /// EQUIP_ALARM_CONFIG
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.03.15 16:49</date>
    /// </author>
    /// </summary>
    [Description("EQUIP_ALARM_CONFIG")]
 
    public class EQUIP_ALARM_CONFIG 
    {
        #region 获取/设置 字段值
        /// <summary>
        /// equip_alarm_config_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("equip_alarm_config_key")]
        public string equip_alarm_config_key { get; set; }
        /// <summary>
        /// equip_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("equip_key")]
        public string equip_key { get; set; }
        /// <summary>
        /// equip_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("equip_code")]
        public string equip_code { get; set; }
        /// <summary>
        /// equip_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("equip_name")]
        public string equip_name { get; set; }
        /// <summary>
        /// alarm_infor_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("alarm_infor_key")]
        public string alarm_infor_key { get; set; }
        /// <summary>
        /// CreateDate
        /// </summary>
        /// <returns></returns>
        [DisplayName("CreateDate")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// CreateUserId
        /// </summary>
        /// <returns></returns>
        [DisplayName("CreateUserId")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// CreateUserName
        /// </summary>
        /// <returns></returns>
        [DisplayName("CreateUserName")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// ModifyDate
        /// </summary>
        /// <returns></returns>
        [DisplayName("ModifyDate")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// ModifyUserId
        /// </summary>
        /// <returns></returns>
        [DisplayName("ModifyUserId")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// ModifyUserName
        /// </summary>
        /// <returns></returns>
        [DisplayName("ModifyUserName")]
        public string ModifyUserName { get; set; }
        /// <summary>
        /// c_addr_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("c_addr_key")]
        public string c_addr_key { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public   void Create()
        {
            this.equip_alarm_config_key = CommonHelper.GetGuid;
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
            this.equip_alarm_config_key = KeyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = SystemLog.UserKey;
            this.ModifyUserName = SystemLog.UserName;
        }
        #endregion
    }
}