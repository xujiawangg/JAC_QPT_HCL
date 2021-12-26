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
    /// Area
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.02.14 15:25</date>
    /// </author>
    /// </summary>
    [Description("Base_Tag")]
    [PrimaryKey("Tag_key")]
    public class Base_Tag : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// Tag_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("Tag_key")]
        public string Tag_key { get; set; }
        /// <summary>
        /// MSB
        /// </summary>
        /// <returns></returns>
        [DisplayName("MSB")]
        public string MSB { get; set; }
        /// <summary>
        /// LSB
        /// </summary>
        /// <returns></returns>
        [DisplayName("LSB")]
        public string LSB { get; set; }
        /// <summary>
        /// Length
        /// </summary>
        /// <returns></returns>
        [DisplayName("Length")]
        public string Length { get; set; }
        /// <summary>
        /// Format
        /// </summary>
        /// <returns></returns>
        [DisplayName("Format")]
        public string Format { get; set; }
      
        /// <summary>
        /// DeleteMark
        /// </summary>
        /// <returns></returns>
        [DisplayName("DeleteMark")]
        public int DeleteMark { get; set; }
      
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
        /// reserve01
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve01")]
        public string Station { get; set; }
        /// <summary>
        /// reserve02
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve02")]
        public string Userss { get; set; }
        /// <summary>
        /// reserve01
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve01")]
        public string Description { get; set; }
        /// <summary>
        /// reserve02
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve02")]
        public string Remark { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.DeleteMark = 0;
            this.Tag_key = CommonHelper.GetGuid;
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
            this.Tag_key = KeyValue;
            this.ModifyDate = DateTime.Now;
            //this.ModifyUserId = ManageProvider.Provider.Current().UserId;
            //this.ModifyUserName = ManageProvider.Provider.Current().UserName;
            this.DeleteMark = 0;
        }
        #endregion
    }
}