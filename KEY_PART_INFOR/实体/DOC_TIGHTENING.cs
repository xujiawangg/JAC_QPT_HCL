//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2017
// Software Developers @ HfutIE 2017
//=====================================================================================
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HfutIE.Entity
{
    /// <summary>
    /// DOC_TIGHTENING
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.12.01 15:42</date>
    /// </author>
    /// </summary>
    [Description("DOC_TIGHTENING")]
    public class DOC_TIGHTENING 
    {
        #region 获取/设置 字段值
        /// <summary>
        /// tightening_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("tightening_key")]
        public string tightening_key { get; set; }
        /// <summary>
        /// SITE_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("SITE_KEY")]
        public string site_key { get; set; }
        /// <summary>
        /// SITE_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("SITE_CODE")]
        public string site_code { get; set; }
        /// <summary>
        /// SITE_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("SITE_NAME")]
        public string site_name { get; set; }
        /// <summary>
        /// AREA_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("AREA_KEY")]
        public string area_key { get; set; }
        /// <summary>
        /// AREA_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("AREA_CODE")]
        public string area_code { get; set; }
        /// <summary>
        /// AREA_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("AREA_NAME")]
        public string area_name { get; set; }
        /// <summary>
        /// WORKSHOP_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("WORKSHOP_KEY")]
        public string ws_key { get; set; }
        /// <summary>
        /// WORKSHOP_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("WORKSHOP_CODE")]
        public string ws_code { get; set; }
        /// <summary>
        /// WORKSHOP_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("WORKSHOP_NAME")]
        public string ws_name { get; set; }
        /// <summary>
        /// PRODUCTION_LINE_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCTION_LINE_KEY")]
        public string p_line_key { get; set; }
        /// <summary>
        /// PRODUCTION_LINE_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCTION_LINE_CODE")]
        public string p_line_code { get; set; }
        /// <summary>
        /// PRODUCTION_LINE_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCTION_LINE_NAME")]
        public string p_line_name { get; set; }
        /// <summary>
        /// WORK_CENTER_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("WORK_CENTER_KEY")]
        public string wc_key { get; set; }
        /// <summary>
        /// WORK_CENTER_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("WORK_CENTER_CODE")]
        public string wc_code { get; set; }
        /// <summary>
        /// WORK_CENTER_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("WORK_CENTER_NAME")]
        public string wc_name { get; set; }
        /// <summary>
        /// EQUIPMENT_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("EQUIPMENT_KEY")]
        public string equip_key { get; set; }
        /// <summary>
        /// EQUIPMENT_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("EQUIPMENT_CODE")]
        public string equip_code { get; set; }
        /// <summary>
        /// EQUIPMENT_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("EQUIPMENT_NAME")]
        public string equip_name { get; set; }
        /// <summary>
        /// SHIFT_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("SHIFT_KEY")]
        public string shift_key { get; set; }
        /// <summary>
        /// SHIFT_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("SHIFT_CODE")]
        public string shift_code { get; set; }
        /// <summary>
        /// SHIFT_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("SHIFT_NAME")]
        public string shift_name { get; set; }
        /// <summary>
        /// TEAM_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("TEAM_KEY")]
        public string team_key { get; set; }
        /// <summary>
        /// TEAM_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("TEAM_CODE")]
        public string team_code { get; set; }
        /// <summary>
        /// TEAM_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("TEAM_NAME")]
        public string team_name { get; set; }
        /// <summary>
        /// squad_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("squad_key")]
        public string squad_key { get; set; }
        /// <summary>
        /// squad_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("squad_code")]
        public string squad_code { get; set; }
        /// <summary>
        /// squad_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("squad_name")]
        public string squad_name { get; set; }
        /// <summary>
        /// date
        /// </summary>
        /// <returns></returns>
        [DisplayName("date")]
        public DateTime? date { get; set; }
        /// <summary>
        /// time
        /// </summary>
        /// <returns></returns>
        [DisplayName("time")]
        public string time { get; set; }
        /// <summary>
        /// tightening_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("tightening_code")]
        public string tightening_code { get; set; }
        /// <summary>
        /// PLAN_NUM
        /// </summary>
        /// <returns></returns>
        [DisplayName("PLAN_NUM")]
        public string plan_num { get; set; }
        /// <summary>
        /// PLAN_NO
        /// </summary>
        /// <returns></returns>
        [DisplayName("PLAN_NO")]
        public string plan_no { get; set; }
        /// <summary>
        /// BOM_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("BOM_KEY")]
        public string bom_key { get; set; }
        /// <summary>
        /// PRODUCT_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_KEY")]
        public string product_key { get; set; }
        /// <summary>
        /// PRODUCT_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_CODE")]
        public string product_code { get; set; }
        /// <summary>
        /// PRODUCT_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_NAME")]
        public string product_name { get; set; }
        /// <summary>
        /// PRODUCT_ABB
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_ABB")]
        public string product_abb { get; set; }
        /// <summary>
        /// PRODUCT_BATCH_NO
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_BATCH_NO")]
        public string product_batch_no { get; set; }
        /// <summary>
        /// PRODUCT_BORN_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_BORN_CODE")]
        public string product_born_code { get; set; }
        /// <summary>
        /// PRODUCT_MODEL_NO
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_MODEL_NO")]
        public string product_model_no { get; set; }
        /// <summary>
        /// PRODUCT_SERIAL_NO
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_SERIAL_NO")]
        public string product_serial { get; set; }
        /// <summary>
        /// PRODUCT_STRUCT_NO
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_STRUCT_NO")]
        public string product_struct_no { get; set; }
        /// <summary>
        /// PRODUCT_TYPE
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_TYPE")]
        public string product_type { get; set; }
        /// <summary>
        /// upper_control_torque
        /// </summary>
        /// <returns></returns>
        [DisplayName("upper_control_torque")]
        public Single? upper_control_torque { get; set; }
        /// <summary>
        /// upper_value_torque
        /// </summary>
        /// <returns></returns>
        [DisplayName("upper_value_torque")]
        public Single? upper_value_torque { get; set; }
        /// <summary>
        /// target_value_torque
        /// </summary>
        /// <returns></returns>
        [DisplayName("target_value_torque")]
        public Single? target_value_torque { get; set; }
        /// <summary>
        /// lower_value_torque
        /// </summary>
        /// <returns></returns>
        [DisplayName("lower_value_torque")]
        public Single? lower_value_torque { get; set; }
        /// <summary>
        /// lower_control_torque
        /// </summary>
        /// <returns></returns>
        [DisplayName("lower_control_torque")]
        public Single? lower_control_torque { get; set; }
        /// <summary>
        /// check_value_torque
        /// </summary>
        /// <returns></returns>
        [DisplayName("check_value_torque")]
        public Single? check_value_torque { get; set; }
        /// <summary>
        /// upper_control_angle
        /// </summary>
        /// <returns></returns>
        [DisplayName("upper_control_angle")]
        public Single? upper_control_angle { get; set; }
        /// <summary>
        /// upper_value_angle
        /// </summary>
        /// <returns></returns>
        [DisplayName("upper_value_angle")]
        public Single? upper_value_angle { get; set; }
        /// <summary>
        /// target_value_angle
        /// </summary>
        /// <returns></returns>
        [DisplayName("target_value_angle")]
        public Single? target_value_angle { get; set; }
        /// <summary>
        /// lower_value_angle
        /// </summary>
        /// <returns></returns>
        [DisplayName("lower_value_angle")]
        public Single? lower_value_angle { get; set; }
        /// <summary>
        /// lower_control_angle
        /// </summary>
        /// <returns></returns>
        [DisplayName("lower_control_angle")]
        public Single? lower_control_angle { get; set; }
        /// <summary>
        /// check_value_angle
        /// </summary>
        /// <returns></returns>
        [DisplayName("check_value_angle")]
        public Single? check_value_angle { get; set; }
        /// <summary>
        /// IS_QUALIFIED
        /// </summary>
        /// <returns></returns>
        [DisplayName("IS_QUALIFIED")]
        public byte? is_qualified { get; set; }
        /// <summary>
        /// RESERVE01
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE01")]
        public string reserve1 { get; set; }
        /// <summary>
        /// RESERVE02
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE02")]
        public string reserve2 { get; set; }
        /// <summary>
        /// RESERVE03
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE03")]
        public string reserve3 { get; set; }
        /// <summary>
        /// RESERVE04
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE04")]
        public string reserve4 { get; set; }
        /// <summary>
        /// RESERVE05
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE05")]
        public string reserve5 { get; set; }
        /// <summary>
        /// reserve6
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve6")]
        public string reserve6 { get; set; }
        /// <summary>
        /// reserve7
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve7")]
        public string reserve7 { get; set; }
        /// <summary>
        /// reserve8
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve8")]
        public string reserve8 { get; set; }
        /// <summary>
        /// reserve9
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve9")]
        public string reserve9 { get; set; }
        /// <summary>
        /// reserve10
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve10")]
        public string reserve10 { get; set; }
        /// <summary>
        /// CREATEDATE
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEDATE")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// CREATEUSERID
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEUSERID")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// CREATEUSERNAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEUSERNAME")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// MODIFYDATE
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFYDATE")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// MODIFYUSERID
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFYUSERID")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// MODIFYUSERNAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFYUSERNAME")]
        public string ModifyUserName { get; set; }
        #endregion

    }
}