//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2017
// Software Developers @ HfutIE 2017
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
    /// BOM
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.05.15 16:06</date>
    /// </author>
    /// </summary>
    [Description("BOM")]
    [PrimaryKey("Bom_Key")]
    public class BOM : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// Bom_Key
        /// </summary>
        /// <returns></returns>
        [DisplayName("Bom_Key")]
        public string Bom_Key { get; set; }
        /// <summary>
        /// Parent_Part_Key
        /// </summary>
        /// <returns></returns>
        [DisplayName("Parent_Part_Key")]
        public string Parent_Part_Key { get; set; }
        /// <summary>
        /// VERSION_KEY
        /// </summary>
        /// <returns></returns>
        [DisplayName("VERSION_KEY")]
        public string VERSION_KEY { get; set; }
        /// <summary>
        /// Part_Key
        /// </summary>
        /// <returns></returns>
        [DisplayName("Part_Key")]
        public string Part_Key { get; set; }
        /// <summary>
        /// Remarks
        /// </summary>
        /// <returns></returns>
        [DisplayName("Remarks")]
        public string Remarks { get; set; }
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
        /// RESERVE01
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE01")]
        public int? RESERVE01 { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        //public override void Create()
        //{
        //    this.Bom_Key = CommonHelper.GetGuid;
        //    this.CreateDate = DateTime.Now;
        //    this.CreateUserId = ManageProvider.Provider.Current().UserId;
        //    this.CreateUserName = ManageProvider.Provider.Current().UserName;
        //}
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        //public override void Modify(string KeyValue)
        //{
        //    this.Bom_Key = KeyValue;
        //    this.ModifyDate = DateTime.Now;
        //    this.ModifyUserId = ManageProvider.Provider.Current().UserId;
        //    this.ModifyUserName = ManageProvider.Provider.Current().UserName;
        //}
        #endregion
    }
}