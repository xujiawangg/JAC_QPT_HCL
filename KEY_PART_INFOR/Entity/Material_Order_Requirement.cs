//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2018
// Software Developers @ HfutIE 2018
//=====================================================================================

using System;
using System.ComponentModel;

namespace HfutIe
{
    /// <summary>
    /// Material_Order_Requirement
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.01.26 14:40</date>
    /// </author>
    /// </summary>
    [Description("Material_Order_Requirement")]
    [PrimaryKey("material_order_requirement_key")]
    public class Material_Order_Requirement : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// material_order_requirement_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("material_order_requirement_key")]
        public string material_order_requirement_key { get; set; }
        /// <summary>
        /// order_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("order_code")]
        public string order_code { get; set; }
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
        /// product_model_no
        /// </summary>
        /// <returns></returns>
        [DisplayName("product_model_no")]
        public string product_model_no { get; set; }
        /// <summary>
        /// plan_num
        /// </summary>
        /// <returns></returns>
        [DisplayName("plan_num")]
        public int? plan_num { get; set; }
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
        /// wc_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("wc_key")]
        public string wc_key { get; set; }
        /// <summary>
        /// part_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("part_key")]
        public string part_key { get; set; }
        /// <summary>
        /// part_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("part_code")]
        public string part_code { get; set; }
        /// <summary>
        /// part_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("part_name")]
        public string part_name { get; set; }
        /// <summary>
        /// unit
        /// </summary>
        /// <returns></returns>
        [DisplayName("unit")]
        public string unit { get; set; }
        /// <summary>
        /// total_demand_num
        /// </summary>
        /// <returns></returns>
        [DisplayName("total_demand_num")]
        public string total_demand_num { get; set; }
        /// <summary>
        /// sum_delivery_num
        /// </summary>
        /// <returns></returns>
        [DisplayName("sum_delivery_num")]
        public string sum_delivery_num { get; set; }
        /// <summary>
        /// remark
        /// </summary>
        /// <returns></returns>
        [DisplayName("remark")]
        public string remark { get; set; }
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
        /// reserve01
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve01")]
        public string reserve01 { get; set; }
        /// <summary>
        /// reserve02
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve02")]
        public string reserve02 { get; set; }
        /// <summary>
        /// reserve03
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve03")]
        public string reserve03 { get; set; }
        /// <summary>
        /// reserve04
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve04")]
        public string reserve04 { get; set; }
        /// <summary>
        /// reserve05
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve05")]
        public string reserve05 { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.material_order_requirement_key = CommonHelper.GetGuid;
            this.CreateDate = ServerTime.Now;
            this.CreateUserId = SystemLog.UserKey;
            this.CreateUserName = SystemLog.UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.material_order_requirement_key = KeyValue;
            this.ModifyDate = ServerTime.Now;
            this.ModifyUserId = SystemLog.UserKey;
            this.ModifyUserName = SystemLog.UserName;
        }
        #endregion
    }
}