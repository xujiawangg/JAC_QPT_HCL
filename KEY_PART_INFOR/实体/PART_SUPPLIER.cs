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
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// ��Ӧ������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ӧ������")]
        public string supplier_key { get; set; }
        /// <summary>
        /// ��Ӧ�̱��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ӧ�̱��")]
        public string supplier_code { get; set; }
        /// <summary>
        /// ��Ӧ������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ӧ������")]
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
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
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
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// �����û�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����û�����")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// �����û�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����û�����")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸�����")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸��û�����")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸��û�����")]
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