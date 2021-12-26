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
    /// P_NOTOK_PRODUCT_INFOR
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.05.05 21:08</date>
    /// </author>
    /// </summary>
    [Description("P_NOTOK_PRODUCT_INFOR")]
    public class P_NOTOK_PRODUCT_INFOR : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// p_notok_pro_infor_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("p_notok_pro_infor_key")]
        public string p_notok_pro_infor_key { get; set; }
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
        /// mes_plan_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("mes_plan_key")]
        public string mes_plan_key { get; set; }
        /// <summary>
        /// mes_plan_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("mes_plan_code")]
        public string mes_plan_code { get; set; }
        /// <summary>
        /// plan_num
        /// </summary>
        /// <returns></returns>
        [DisplayName("plan_num")]
        public string plan_num { get; set; }
        /// <summary>
        /// plan_no
        /// </summary>
        /// <returns></returns>
        [DisplayName("plan_no")]
        public string plan_no { get; set; }
        /// <summary>
        /// bom_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("bom_key")]
        public string bom_key { get; set; }
        /// <summary>
        /// product_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("product_key")]
        public string product_key { get; set; }
        /// <summary>
        /// product_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("product_code")]
        public string product_code { get; set; }
        /// <summary>
        /// product_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("product_name")]
        public string product_name { get; set; }
        /// <summary>
        /// product_abb
        /// </summary>
        /// <returns></returns>
        [DisplayName("product_abb")]
        public string product_abb { get; set; }
        /// <summary>
        /// product_batch_no
        /// </summary>
        /// <returns></returns>
        [DisplayName("product_batch_no")]
        public string product_batch_no { get; set; }
        /// <summary>
        /// product_born_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("product_born_code")]
        public string product_born_code { get; set; }
        /// <summary>
        /// product_model_no
        /// </summary>
        /// <returns></returns>
        [DisplayName("product_model_no")]
        public string product_model_no { get; set; }
        /// <summary>
        /// product_serial_no
        /// </summary>
        /// <returns></returns>
        [DisplayName("product_serial_no")]
        public string product_serial_no { get; set; }
        /// <summary>
        /// product_struct_no
        /// </summary>
        /// <returns></returns>
        [DisplayName("product_struct_no")]
        public string product_struct_no { get; set; }
        /// <summary>
        /// product_type
        /// </summary>
        /// <returns></returns>
        [DisplayName("product_type")]
        public string product_type { get; set; }
        /// <summary>
        /// notok_reason
        /// </summary>
        /// <returns></returns>
        [DisplayName("notok_reason")]
        public string notok_reason { get; set; }
        /// <summary>
        /// remarks
        /// </summary>
        /// <returns></returns>
        [DisplayName("remarks")]
        public string remarks { get; set; }
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
            this.p_notok_pro_infor_key = CommonHelper.GetGuid;
            this.CreateDate = DateTime.Now;
            this.CreateUserId = SystemLog.UserKey;
            this.CreateUserName = SystemLog.UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.p_notok_pro_infor_key = KeyValue;
                                            }
        #endregion
    }
}