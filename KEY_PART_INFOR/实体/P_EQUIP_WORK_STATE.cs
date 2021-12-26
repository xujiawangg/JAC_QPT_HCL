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
    /// P_EQUIP_WORK_STATE
    /// <author>
    ///		<name>hang</name>
    ///		<date>2017.05.04 19:56</date>
    /// </author>
    /// </summary>
    [Description("P_EQUIP_WORK_STATE")]
    public class P_EQUIP_WORK_STATE : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// equip_work_state_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("equip_work_state_key")]
        public string equip_work_state_key { get; set; }
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.equip_work_state_key = CommonHelper.GetGuid;
         }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public void Modify()
        {
            this.ModifyDate = DateTime.Now;
        this.ModifyUserId = SystemLog.UserKey;
         this.ModifyUserName = SystemLog.UserName;
        }
        #endregion
    }
}