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
    /// P_KEY_PART_INFOR
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.11.12 21:28</date>
    /// </author>
    /// </summary>
    [Description("P_KEY_PART_INFOR")]

    public class P_KEY_PART_INFOR 
    {
        #region 获取/设置 字段值
        /// <summary>
        /// KEY_PART_INFOR_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("KEY_PART_INFOR_KEY")]
        public string KEY_PART_INFOR_KEY { get; set; }
        /// <summary>
        /// ASSMBLY_TIME
        /// </summary>
        /// <returns></returns>
        [DisplayName("ASSMBLY_TIME")]
        public DateTime? ASSMBLY_TIME { get; set; }
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
        public string MES_PLAN_CODE { get; set; }
        /// <summary>
        /// PLAN_NUM
        /// </summary>
        /// <returns></returns>
        [DisplayName("PLAN_NUM")]
        public string PLAN_NUM { get; set; }
        /// <summary>
        /// PLAN_NO
        /// </summary>
        /// <returns></returns>
        [DisplayName("PLAN_NO")]
        public string PLAN_NO { get; set; }
        /// <summary>
        /// SITE_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("SITE_KEY")]
        public string SITE_KEY { get; set; }
        /// <summary>
        /// SITE_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("SITE_CODE")]
        public string SITE_CODE { get; set; }
        /// <summary>
        /// SITE_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("SITE_NAME")]
        public string SITE_NAME { get; set; }
        /// <summary>
        /// AREA_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("AREA_KEY")]
        public string AREA_KEY { get; set; }
        /// <summary>
        /// AREA_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("AREA_CODE")]
        public string AREA_CODE { get; set; }
        /// <summary>
        /// AREA_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("AREA_NAME")]
        public string AREA_NAME { get; set; }
        /// <summary>
        /// WORKSHOP_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("WORKSHOP_KEY")]
        public string WORKSHOP_KEY { get; set; }
        /// <summary>
        /// WORKSHOP_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("WORKSHOP_CODE")]
        public string WORKSHOP_CODE { get; set; }
        /// <summary>
        /// WORKSHOP_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("WORKSHOP_NAME")]
        public string WORKSHOP_NAME { get; set; }
        /// <summary>
        /// PRODUCTION_LINE_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCTION_LINE_KEY")]
        public string PRODUCTION_LINE_KEY { get; set; }
        /// <summary>
        /// PRODUCTION_LINE_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCTION_LINE_CODE")]
        public string PRODUCTION_LINE_CODE { get; set; }
        /// <summary>
        /// PRODUCTION_LINE_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCTION_LINE_NAME")]
        public string PRODUCTION_LINE_NAME { get; set; }
        /// <summary>
        /// WORK_CENTER_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("WORK_CENTER_KEY")]
        public string WORK_CENTER_KEY { get; set; }
        /// <summary>
        /// WORK_CENTER_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("WORK_CENTER_CODE")]
        public string WORK_CENTER_CODE { get; set; }
        /// <summary>
        /// WORK_CENTER_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("WORK_CENTER_NAME")]
        public string WORK_CENTER_NAME { get; set; }
        /// <summary>
        /// EQUIPMENT_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("EQUIPMENT_KEY")]
        public string EQUIPMENT_KEY { get; set; }
        /// <summary>
        /// EQUIPMENT_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("EQUIPMENT_CODE")]
        public string EQUIPMENT_CODE { get; set; }
        /// <summary>
        /// EQUIPMENT_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("EQUIPMENT_NAME")]
        public string EQUIPMENT_NAME { get; set; }
        /// <summary>
        /// KEY_PART_C_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("KEY_PART_C_KEY")]
        public string KEY_PART_C_KEY { get; set; }
        /// <summary>
        /// PRODUCT_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_KEY")]
        public string PRODUCT_KEY { get; set; }
        /// <summary>
        /// PRODUCT_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_CODE")]
        public string PRODUCT_CODE { get; set; }
        /// <summary>
        /// PRODUCT_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_NAME")]
        public string PRODUCT_NAME { get; set; }
        /// <summary>
        /// PRODUCT_ABB
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_ABB")]
        public string PRODUCT_ABB { get; set; }
        /// <summary>
        /// PRODUCT_BATCH_NO
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_BATCH_NO")]
        public string PRODUCT_BATCH_NO { get; set; }
        /// <summary>
        /// PRODUCT_BORN_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_BORN_CODE")]
        public string PRODUCT_BORN_CODE { get; set; }
        /// <summary>
        /// PRODUCT_MODEL_NO
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_MODEL_NO")]
        public string PRODUCT_MODEL_NO { get; set; }
        /// <summary>
        /// PRODUCT_SERIAL_NO
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_SERIAL_NO")]
        public string PRODUCT_SERIAL_NO { get; set; }
        /// <summary>
        /// PRODUCT_STRUCT_NO
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_STRUCT_NO")]
        public string PRODUCT_STRUCT_NO { get; set; }
        /// <summary>
        /// PRODUCT_TYPE
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_TYPE")]
        public string PRODUCT_TYPE { get; set; }
        /// <summary>
        /// front_axle_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("front_axle_code")]
        public string front_axle_code { get; set; }
        /// <summary>
        /// DISTRIBUTION_WORKSHOP_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("DISTRIBUTION_WORKSHOP_KEY")]
        public string DISTRIBUTION_WORKSHOP_KEY { get; set; }
        /// <summary>
        /// DISTRIBUTION_WORKSHOP_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("DISTRIBUTION_WORKSHOP_CODE")]
        public string DISTRIBUTION_WORKSHOP_CODE { get; set; }
        /// <summary>
        /// DISTRIBUTION_WORKSHOP_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("DISTRIBUTION_WORKSHOP_NAME")]
        public string DISTRIBUTION_WORKSHOP_NAME { get; set; }
        /// <summary>
        /// SHIFT_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("SHIFT_KEY")]
        public string SHIFT_KEY { get; set; }
        /// <summary>
        /// SHIFT_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("SHIFT_CODE")]
        public string SHIFT_CODE { get; set; }
        /// <summary>
        /// SHIFT_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("SHIFT_NAME")]
        public string SHIFT_NAME { get; set; }
        /// <summary>
        /// TEAM_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("TEAM_KEY")]
        public string TEAM_KEY { get; set; }
        /// <summary>
        /// TEAM_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("TEAM_CODE")]
        public string TEAM_CODE { get; set; }
        /// <summary>
        /// TEAM_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("TEAM_NAME")]
        public string TEAM_NAME { get; set; }
        /// <summary>
        /// STAFF_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("STAFF_KEY")]
        public string STAFF_KEY { get; set; }
        /// <summary>
        /// STAFF_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("STAFF_CODE")]
        public string STAFF_CODE { get; set; }
        /// <summary>
        /// STAFF_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("STAFF_NAME")]
        public string STAFF_NAME { get; set; }
        /// <summary>
        /// PART_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("PART_KEY")]
        public string PART_KEY { get; set; }
        /// <summary>
        /// PART_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("PART_CODE")]
        public string PART_CODE { get; set; }
        /// <summary>
        /// PART_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("PART_NAME")]
        public string PART_NAME { get; set; }
        /// <summary>
        /// PART_BATCH_NO
        /// </summary>
        /// <returns></returns>
        [DisplayName("PART_BATCH_NO")]
        public string PART_BATCH_NO { get; set; }
        /// <summary>
        /// PART_VINCODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("PART_VINCODE")]
        public string PART_VINCODE { get; set; }
        /// <summary>
        /// PART_BARCODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("PART_BARCODE")]
        public string PART_BARCODE { get; set; }
        /// <summary>
        /// QUANTITY
        /// </summary>
        /// <returns></returns>
        [DisplayName("QUANTITY")]
        public int? QUANTITY { get; set; }
        /// <summary>
        /// SUPPLIER_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("SUPPLIER_KEY")]
        public string SUPPLIER_KEY { get; set; }
        /// <summary>
        /// SUPPLIER_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("SUPPLIER_CODE")]
        public string SUPPLIER_CODE { get; set; }
        /// <summary>
        /// SUPPLIER_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("SUPPLIER_NAME")]
        public string SUPPLIER_NAME { get; set; }
        /// <summary>
        /// RESERVE01
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE01")]
        public string RESERVE01 { get; set; }
        /// <summary>
        /// RESERVE02
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE02")]
        public string RESERVE02 { get; set; }
        /// <summary>
        /// RESERVE03
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE03")]
        public string RESERVE03 { get; set; }
        /// <summary>
        /// RESERVE04
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE04")]
        public string RESERVE04 { get; set; }
        /// <summary>
        /// RESERVE05
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE05")]
        public string RESERVE05 { get; set; }
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
        public DateTime? CREATEDATE { get; set; }
        /// <summary>
        /// CREATEUSERID
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEUSERID")]
        public string CREATEUSERID { get; set; }
        /// <summary>
        /// CREATEUSERNAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEUSERNAME")]
        public string CREATEUSERNAME { get; set; }
        /// <summary>
        /// MODIFYDATE
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFYDATE")]
        public DateTime? MODIFYDATE { get; set; }
        /// <summary>
        /// MODIFYUSERID
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFYUSERID")]
        public string MODIFYUSERID { get; set; }
        /// <summary>
        /// MODIFYUSERNAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFYUSERNAME")]
        public string MODIFYUSERNAME { get; set; }
        #endregion
       public void creatkey()
        {
            KEY_PART_INFOR_KEY = Guid.NewGuid().ToString();
        }
    }
}