//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2021
// Software Developers @ HfutIE 2021
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
    /// BS_StaffInfo
    /// <author>
    ///		<name>she</name>
    ///		<date>2021.11.19 17:01</date>
    /// </author>
    /// </summary>
    [Description("A_BS_StaffInfo")]
    [PrimaryKey("ID")]
    public class A_BS_StaffInfo : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("ID")]
        public string ID { get; set; }
        /// <summary>
        /// StaffCode
        /// </summary>
        /// <returns></returns>
        [DisplayName("StaffCode")]
        public string StaffCode { get; set; }
        /// <summary>
        /// StaffName
        /// </summary>
        /// <returns></returns>
        [DisplayName("StaffName")]
        public string StaffName { get; set; }
        /// <summary>
        /// StaffAccount
        /// </summary>
        /// <returns></returns>
        [DisplayName("StaffAccount")]
        public string StaffAccount { get; set; }
        /// <summary>
        /// AccountPassword
        /// </summary>
        /// <returns></returns>
        [DisplayName("AccountPassword")]
        public string AccountPassword { get; set; }
        /// <summary>
        /// Secretkey
        /// </summary>
        /// <returns></returns>
        [DisplayName("Secretkey")]
        public string Secretkey { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        /// <returns></returns>
        [DisplayName("Status")]
        public Boolean Status { get; set; }
        /// <summary>
        /// CREATE_TIME
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATE_TIME")]
        public DateTime? CREATE_TIME { get; set; }
        /// <summary>
        /// CREATOR_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATOR_CODE")]
        public string CREATOR_CODE { get; set; }
        /// <summary>
        /// CREATOR_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATOR_NAME")]
        public string CREATOR_NAME { get; set; }
        /// <summary>
        /// MODIFIER_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFIER_CODE")]
        public string MODIFIER_CODE { get; set; }
        /// <summary>
        /// MODIFIER_NAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFIER_NAME")]
        public string MODIFIER_NAME { get; set; }
        /// <summary>
        /// MODIFY_TIME
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFY_TIME")]
        public DateTime? MODIFY_TIME { get; set; }
        /// <summary>
        /// REMARKS
        /// </summary>
        /// <returns></returns>
        [DisplayName("REMARKS")]
        public string REMARKS { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        //public override void Create()
        //{
        //    this.ID = CommonHelper.GetGuid;
        //    this.CREATE_TIME = DateTime.Now;
        //    this.CREATOR_CODE = ManageProvider.Provider.Current().UserId;
        //    this.CREATOR_NAME = ManageProvider.Provider.Current().UserName;
        //    this.Secretkey = Md5Helper.MD5(CommonHelper.CreateNo(), 16).ToLower();
        //    this.AccountPassword = Md5Helper.MD5(DESEncrypt.Encrypt(Md5Helper.MD5(this.AccountPassword, 32).ToLower(), this.Secretkey).ToLower(), 32).ToLower();
        //}
        ///// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="KeyValue"></param>
        //public override void Modify(string KeyValue)
        //{
        //    this.ID = KeyValue;
        //    this.MODIFY_TIME = DateTime.Now;
        //    this.MODIFIER_CODE = ManageProvider.Provider.Current().UserId;
        //    this.MODIFIER_NAME = ManageProvider.Provider.Current().UserName;
        //    //this.Password = null;
        //}
        #endregion
    }
}
