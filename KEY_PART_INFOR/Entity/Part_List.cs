//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2018
// Software Developers @ HfutIE 2018
//=====================================================================================

using HfutIe;
using HfutIE.Utilities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HfutIE.Entity
{
    /// <summary>
    /// Part_List
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.10.25 18:23</date>
    /// </author>
    /// </summary>
    [Description("Part_List")]
    [PrimaryKey("Part_List_Key")]
    public class Part_List : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// Part_List_Key
        /// </summary>
        /// <returns></returns>
        [DisplayName("Part_List_Key")]
        public string Part_List_Key { get; set; }
        /// <summary>
        /// MES_PLAN_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("MES_PLAN_CODE")]
        public string MES_PLAN_CODE { get; set; }
        /// <summary>
        /// Part_Code
        /// </summary>
        /// <returns></returns>
        [DisplayName("Part_Code")]
        public string Part_Code { get; set; }
        /// <summary>
        /// Part_Name
        /// </summary>
        /// <returns></returns>
        [DisplayName("Part_Name")]
        public string Part_Name { get; set; }
        /// <summary>
        /// OP_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("OP_CODE")]
        public string OP_CODE { get; set; }
        /// <summary>
        /// OP_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("OP_NAME")]
        public string OP_NAME { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        /// <returns></returns>
        [DisplayName("Quantity")]
        public int? Quantity { get; set; }
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
        /// <summary>
        /// reserve06
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve06")]
        public string reserve06 { get; set; }
        /// <summary>
        /// reserve07
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve07")]
        public string reserve07 { get; set; }
        /// <summary>
        /// reserve08
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve08")]
        public string reserve08 { get; set; }
        /// <summary>
        /// reserve09
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve09")]
        public string reserve09 { get; set; }
        /// <summary>
        /// reserve10
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve10")]
        public string reserve10 { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Part_List_Key = CommonHelper.GetGuid;
            this.CreateDate = DateTime.Now;
            //this.CreateUserId = ManageProvider.Provider.Current().UserId;
            //this.CreateUserName = ManageProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.Part_List_Key = KeyValue;
            this.ModifyDate = DateTime.Now;
            //this.ModifyUserId = ManageProvider.Provider.Current().UserId;
            //this.ModifyUserName = ManageProvider.Provider.Current().UserName;
        }
        #endregion
    }
}