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
    /// P_PLAN
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.11.12 21:29</date>
    /// </author>
    /// </summary>
    [Description("P_PLAN")]
    public class P_PLAN 
    {
        #region 获取/设置 字段值
        /// <summary>
        /// MES_PLAN_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("MES_PLAN_KEY")]
        public string MES_PLAN_KEY { get; set; }
        /// <summary>
        /// MES_PLAN_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("MES_PLAN_CODE")]
        public string mes_plan_code { get; set; }
        /// <summary>
        /// PRODUCTION_LINE_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCTION_LINE_NAME")]
        public string p_line_name { get; set; }
        /// <summary>
        /// execution_queue_no
        /// </summary>
        /// <returns></returns>
        [DisplayName("execution_queue_no")]
        public int? execution_queue_no { get; set; }
        /// <summary>
        /// PRODUCT_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_CODE")]
        public string product_code { get; set; }
        /// <summary>
        /// PLAN_NUM
        /// </summary>
        /// <returns></returns>
        [DisplayName("PLAN_NUM")]
        public int? plan_num { get; set; }
        /// <summary>
        /// plan_date
        /// </summary>
        /// <returns></returns>
        [DisplayName("plan_date")]
        public DateTime? plan_date { get; set; }
        /// <summary>
        /// plan_start_time
        /// </summary>
        /// <returns></returns>
        [DisplayName("plan_start_time")]
        public DateTime? plan_start_time { get; set; }
        /// <summary>
        /// plan_ending_time
        /// </summary>
        /// <returns></returns>
        [DisplayName("plan_ending_time")]
        public DateTime? plan_ending_time { get; set; }
        /// <summary>
        /// actual_start_time
        /// </summary>
        /// <returns></returns>
        [DisplayName("actual_start_time")]
        public DateTime? actual_start_time { get; set; }
        /// <summary>
        /// actual_ending_time
        /// </summary>
        /// <returns></returns>
        [DisplayName("actual_ending_time")]
        public DateTime? actual_ending_time { get; set; }
        /// <summary>
        /// online_num
        /// </summary>
        /// <returns></returns>
        [DisplayName("online_num")]
        public int? online_num { get; set; }
        /// <summary>
        /// offline_num
        /// </summary>
        /// <returns></returns>
        [DisplayName("offline_num")]
        public int? offline_num { get; set; }
        /// <summary>
        /// REMARKS
        /// </summary>
        /// <returns></returns>
        [DisplayName("REMARKS")]
        public string remarks { get; set; }
        /// <summary>
        /// distribution_workshop
        /// </summary>
        /// <returns></returns>
        [DisplayName("distribution_workshop")]
        public string distribution_workshop { get; set; }
        /// <summary>
        /// reference_vehicle
        /// </summary>
        /// <returns></returns>
        [DisplayName("reference_vehicle")]
        public string reference_vehicle { get; set; }
        /// <summary>
        /// plan_source
        /// </summary>
        /// <returns></returns>
        [DisplayName("plan_source")]
        public string plan_source { get; set; }
        /// <summary>
        /// exception_flag
        /// </summary>
        /// <returns></returns>
        [DisplayName("exception_flag")]
        public string exception_flag { get; set; }
        /// <summary>
        /// execution_status
        /// </summary>
        /// <returns></returns>
        [DisplayName("execution_status")]
        public string execution_status { get; set; }
        /// <summary>
        /// complete_status
        /// </summary>
        /// <returns></returns>
        [DisplayName("complete_status")]
        public string complete_status { get; set; }
        /// <summary>
        /// last_update_date
        /// </summary>
        /// <returns></returns>
        [DisplayName("last_update_date")]
        public DateTime? last_update_date { get; set; }
        /// <summary>
        /// brake_drums_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("brake_drums_code")]
        public string brake_drums_code { get; set; }
        /// <summary>
        /// front_axle_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("front_axle_code")]
        public string front_axle_code { get; set; }
        /// <summary>
        /// brake_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("brake_code")]
        public string brake_code { get; set; }
        /// <summary>
        /// straight_bar_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("straight_bar_code")]
        public string straight_bar_code { get; set; }
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