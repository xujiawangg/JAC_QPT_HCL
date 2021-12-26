//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2017
// Software Developers @ HfutIE 2017
//=====================================================================================
 
using System;
using System.ComponentModel;
 
using System.Text;

namespace HfutIe.Entity
{
    /// <summary>
    /// DCS_ADDR_LIST
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.02.24 13:54</date>
    /// </author>
    /// </summary>
 
    public class DCS_ADDR_LIST 
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// dcs_addr_list_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("dcs_addr_list_key")]
        public string dcs_addr_list_key { get; set; }
        /// <summary>
        /// dcs_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("dcs_key")]
        public string dcs_key { get; set; }
        /// <summary>
        /// addr_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("addr_key")]
        public string c_addr_key { get; set; }
        /// <summary>
        /// addr_name
        /// </summary>
        /// <returns></returns>
        [DisplayName("addr_name")]
        public string addr_name { get; set; }
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
        #endregion
    }
}