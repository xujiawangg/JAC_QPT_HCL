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
    /// P_EQUIP_ALARM_INFOR
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.04.23 19:52</date>
    /// </author>
    /// </summary>
    [Description("P_EQUIP_ALARM_INFOR")]
 
    public class P_EQUIP_ALARM_INFOR : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// p_equip_alarm_infor_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("p_equip_alarm_infor_key")]
        public string p_equip_alarm_infor_key { get; set; }
        /// <summary>
        /// site_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("site_key")]
        public string site_key { get; set; }
        /// <summary>
        /// site_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("site_code")]
        public string site_code { get; set; }
        /// <summary>
        /// site_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("site_name")]
        public string site_name { get; set; }
        /// <summary>
        /// area_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("area_key")]
        public string area_key { get; set; }
        /// <summary>
        /// area_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("area_code")]
        public string area_code { get; set; }
        /// <summary>
        /// area_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("area_name")]
        public string area_name { get; set; }
        /// <summary>
        /// ws_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("ws_key")]
        public string ws_key { get; set; }
        /// <summary>
        /// ws_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("ws_code")]
        public string ws_code { get; set; }
        /// <summary>
        /// ws_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("ws_name")]
        public string ws_name { get; set; }
        /// <summary>
        /// p_line_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("p_line_key")]
        public string p_line_key { get; set; }
        /// <summary>
        /// p_line_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("p_line_code")]
        public string p_line_code { get; set; }
        /// <summary>
        /// p_line_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("p_line_name")]
        public string p_line_name { get; set; }
        /// <summary>
        /// wc_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("wc_key")]
        public string wc_key { get; set; }
        /// <summary>
        /// wc_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("wc_code")]
        public string wc_code { get; set; }
        /// <summary>
        /// wc_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("wc_name")]
        public string wc_name { get; set; }
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
        /// shift_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("shift_key")]
        public string shift_key { get; set; }
        /// <summary>
        /// shift_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("shift_code")]
        public string shift_code { get; set; }
        /// <summary>
        /// shift_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("shift_name")]
        public string shift_name { get; set; }
        /// <summary>
        /// team_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("team_key")]
        public string team_key { get; set; }
        /// <summary>
        /// team_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("team_code")]
        public string team_code { get; set; }
        /// <summary>
        /// team_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("team_name")]
        public string team_name { get; set; }
        /// <summary>
        /// staff_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("staff_key")]
        public string staff_key { get; set; }
        /// <summary>
        /// staff_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("staff_code")]
        public string staff_code { get; set; }
        /// <summary>
        /// staff_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("staff_name")]
        public string staff_name { get; set; }
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
        /// alarm_start_time
        /// </summary>
        /// <returns></returns>
        [DisplayName("alarm_start_time")]
        public DateTime? alarm_start_time { get; set; }
        /// <summary>
        /// alarm_end_time
        /// </summary>
        /// <returns></returns>
        [DisplayName("alarm_end_time")]
        public DateTime? alarm_end_time { get; set; }
        /// <summary>
        /// continue_time
        /// </summary>
        /// <returns></returns>
        [DisplayName("continue_time")]
        public double  continue_time { get; set; }
        /// <summary>
        /// alarm_addr_path
        /// </summary>
        /// <returns></returns>
        [DisplayName("alarm_addr_path")]
        public string alarm_addr_path { get; set; }
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
        public override void Create()
        {
            this.p_equip_alarm_infor_key = CommonHelper.GetGuid;
            this.CreateDate = DateTime.Now;
            this.CreateUserId = SystemLog.UserKey;
            this.CreateUserName= SystemLog.UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.p_equip_alarm_infor_key = KeyValue;
            this.ModifyDate = DateTime.Now;
        }
        #endregion
    }
}