//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2017
// Software Developers @ HfutIE 2017
//=====================================================================================

using HfutIe;
using System;
using System.ComponentModel;

namespace HfutIE.Entity
{
    /// <summary>
    /// Part_Supplier
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.03.08 22:20</date>
    /// </author>
    /// </summary>
    [Description("Part_Supplier")]
    [PrimaryKey("supplier_key")]
    public class Part_Supplier : BaseEntity
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
        /// ��Ӧ������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ӧ�̼�����")]
        public string supplier_abb { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
        public string remarks { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
        public string linkman_name { get; set; }
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
        /// reserve01
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve01")]
        public string reserve01 { get; set; }
        /// <summary>
        /// reserve02
        /// </summary>
        /// <returns></returns>
        [DisplayName("telephone")]
        public string telephone { get; set; }
        /// <summary>
        /// reserve03
        /// </summary>
        /// <returns></returns>
        [DisplayName("email")]
        public string email { get; set; }
        /// <summary>
        /// reserve04
        /// </summary>
        /// <returns></returns>
        [DisplayName("address")]
        public string address { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.supplier_key = CommonHelper.GetGuid;
            this.CreateDate = ServerTime.Now;
            this.CreateUserId = SystemLog.UserKey;
            this.CreateUserName = SystemLog.UserName;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.supplier_key = KeyValue;
            this.ModifyDate = ServerTime.Now;
            this.ModifyUserId = SystemLog.UserKey;
            this.ModifyUserName = SystemLog.UserName;
        }
        #endregion
    }
}