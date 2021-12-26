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
    /// PART_SUPPLIER
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.11.13 00:30</date>
    /// </author>
    /// </summary>
    [Description("PART_SUPPLIER")]

    public class PART_SUPPLIER 
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 供应商主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("供应商主键")]
        public string supplier_key { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("供应商编号")]
        public string supplier_code { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("供应商名称")]
        public string supplier_name { get; set; }
        /// <summary>
        /// supplier_abb
        /// </summary>
        /// <returns></returns>
        [DisplayName("supplier_abb")]
        public string supplier_abb { get; set; }
        /// <summary>
        /// linkman_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("linkman_name")]
        public string linkman_name { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("名称")]
        public string remarks { get; set; }
        /// <summary>
        /// telephone
        /// </summary>
        /// <returns></returns>
        [DisplayName("telephone")]
        public string telephone { get; set; }
        /// <summary>
        /// email
        /// </summary>
        /// <returns></returns>
        [DisplayName("email")]
        public string email { get; set; }
        /// <summary>
        /// address
        /// </summary>
        /// <returns></returns>
        [DisplayName("address")]
        public string address { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建日期")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建用户主键")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建用户名称")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改日期")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改用户主键")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改用户名称")]
        public string ModifyUserName { get; set; }
        /// <summary>
        /// RESERVE01
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE01")]
        public string reserve01 { get; set; }
        #endregion

    }
}