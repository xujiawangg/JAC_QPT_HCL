using System;
using System.ComponentModel;

namespace HfutIe.Entity
{
    public  class BaseDcsData
    {
        #region 获取/设置 字段值  
        /// <summary>
        /// dcs_instance_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("dcs_instance_key")]
        public string dcs_instance_key { get; set; }
        #region 工厂建模数据
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
        #region 班制班组人员
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
        public string product_no { get; set; }
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
        /// product_serial
        /// </summary>
        /// <returns></returns>
        [DisplayName("product_serial")]
        public string product_serial { get; set; }
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
        #region dcsdata
        /// <summary>
        /// dcs_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("dcs_key")]
        public string dcs_key { get; set; }
        /// <summary>
        /// dcs_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("dcs_code")]
        public string dcs_code { get; set; }
        /// <summary>
        /// dcs_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("dcs_name")]
        public string dcs_name { get; set; }
        /// <summary>
        /// parm_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("parm_key")]
        public string parm_key { get; set; }
        /// <summary>
        /// parm_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("parm_code")]
        public string parm_code { get; set; }
        /// <summary>
        /// parm_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("parm_name")]
        public string parm_name { get; set; }
        /// <summary>
        /// text_mask
        /// </summary>
        /// <returns></returns>
        [DisplayName("text_mask")]
        public string text_mask { get; set; }
        /// <summary>
        /// text_length
        /// </summary>
        /// <returns></returns>
        [DisplayName("text_length")]
        public int? text_length { get; set; }
        /// <summary>
        /// upper_control
        /// </summary>
        /// <returns></returns>
        [DisplayName("upper_control")]
        public double? upper_control { get; set; }
        /// <summary>
        /// upper_value
        /// </summary>
        /// <returns></returns>
        [DisplayName("upper_value")]
        public double? upper_value { get; set; }
        /// <summary>
        /// target_value
        /// </summary>
        /// <returns></returns>
        [DisplayName("target_value")]
        public double? target_value { get; set; }
        /// <summary>
        /// lower_value
        /// </summary>
        /// <returns></returns>
        [DisplayName("lower_value")]
        public double? lower_value { get; set; }
        /// <summary>
        /// lower_control
        /// </summary>
        /// <returns></returns>
        [DisplayName("lower_control")]
        public double? lower_control { get; set; }
        /// <summary>
        /// check_value
        /// </summary>
        /// <returns></returns>
        [DisplayName("check_value")]
        public string  check_value { get; set; }
        /// <summary>
        /// 检测时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("检测时间")]
        public DateTime? check_time { get; set; }
        /// <summary>
        /// is_qualified
        /// </summary>
        /// <returns></returns>
        [DisplayName("is_qualified")]
        public int? is_qualified { get; set; }

        #endregion
        #endregion
        #region 创建和修改
        public void Create()
        {
            this.dcs_instance_key = CommonHelper.GetGuid;
        }
        #endregion

    }
}
