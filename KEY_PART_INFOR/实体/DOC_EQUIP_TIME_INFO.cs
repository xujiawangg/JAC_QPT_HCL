
using System;
using System.ComponentModel;
using System.Text;

namespace HfutIe.Entity
{
    /// <summary>
    /// P_ASSEMBLE_PRODUCT_STATE
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.04.06 17:39</date>
    /// </author>
    /// </summary>
 
    public class DOC_EQUIP_TIME_INFO
    {
        #region 获取/设置 字段值
        /// <summary>
        /// doc_equip_time_infor_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("doc_equip_time_infor_key")]
        public string doc_equip_time_infor_key { get; set; }
        #region 工厂建模数据
        /// <summary>
        /// account_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("account_code")]
        public string account_code { get; set; }
        /// <summary>
        /// account_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("account_name")]
        public string account_name { get; set; }
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
        #endregion
        #region 计划产品
        /// <summary>
        /// MES_plan_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("MES_plan_code")]
        public string MES_plan_code { get; set; }
        /// <summary>
        /// plan_num
        /// </summary>
        /// <returns></returns>
        [DisplayName("plan_num")]
        public string plan_num { get; set; }
        /// <summary>
        /// product_no
        /// </summary>
        /// <returns></returns>
        [DisplayName("product_no")]
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
        #endregion
        /// <summary>
        /// 开始时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("start_time")]
        public DateTime? start_time { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("end_time")]
        public DateTime? end_time { get; set; }
        /// <summary>
        /// 持续时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("continued_time")]
        public int continued_time { get; set; }
        /// <summary>
        /// Start_time
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
        /// Is_Ok
        /// </summary>
        /// <returns></returns>
        [DisplayName("Is_Ok")]
        public int? Is_Ok { get; set; }
        #endregion
        #region 创建和修改
        public void Create()
        {
            this.doc_equip_time_infor_key = CommonHelper.GetGuid;
            this.CreateDate = DateTime.Now;
        }
        public void Modify()
        {
            this.ModifyDate = DateTime.Now;
        }
        #endregion
    }
}