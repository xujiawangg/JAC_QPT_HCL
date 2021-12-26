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
    /// 设备过程状态信息
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.05.04 19:52</date>
    /// </author>
    /// </summary>
    [Description("P_EQUIP_STATE_INFO")]
    public class P_EQUIP_STATE_INFO : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// equip_state_infor_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("equip_state_infor_key")]
        public string equip_state_infor_key { get; set; }
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
        /// equip_state
        /// </summary>
        /// <returns></returns>
        [DisplayName("equip_state")]
        public string equip_state { get; set; }
        /// addr_path
        /// </summary>
        /// <returns></returns>
        [DisplayName("addr_path")]
        public string addr_path { get; set; }
        /// <summary>
        /// start_time
        /// </summary>
        /// <returns></returns>
        [DisplayName("start_time")]
        public DateTime?  start_time { get; set; }
        /// <summary>
        /// end_time
        /// </summary>
        /// <returns></returns>
        [DisplayName("end_time")]
        public DateTime? end_time { get; set; }
        /// <summary>
        /// continued_time
        /// </summary>
        /// <returns></returns>
        [DisplayName("continued_time")]
        public int? continued_time { get; set; }
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.equip_state_infor_key = CommonHelper.GetGuid;
            this.CreateDate = DateTime.Now;
        //    this.CreateUserId = ManageProvider.Provider.Current().UserId;
           //this.CreateUserName = ManageProvider.Provider.Current().UserName;
        }
        #endregion
    }
}