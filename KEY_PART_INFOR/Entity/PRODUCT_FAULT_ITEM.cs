//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2019
// Software Developers @ HfutIE 2019
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
    /// ��Ʒ������
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.01.18 19:47</date>
    /// </author>
    /// </summary>
    [Description("��Ʒ������")]
    [PrimaryKey("PRODUCT_FAULT_ITEM_KEY")]
    public class PRODUCT_FAULT_ITEM : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// key
        /// </summary>
        /// <returns></returns>
        [DisplayName("key")]
        public string PRODUCT_FAULT_ITEM_KEY { get; set; }
        /// <summary>
        /// ��Ʒ���ϱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ���ϱ��")]
        public string PRODUCT_FAULT_ITEM_CODE { get; set; }
        /// <summary>
        /// ��Ʒ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ��������")]
        public string PRODUCT_FAULT_ITEM_NAME { get; set; }
        /// <summary>
        /// ��Ʒ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ��������")]
        public string DISCRIBE { get; set; }
        /// <summary>
        /// ��Ʒ��������key
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ��������key")]
        public string PRODUCT_FAULT_TYPE_KEY { get; set; }
        /// <summary>
        /// ��Ʒ�������ͱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ�������ͱ��")]
        public string PRODUCT_FAULT_TYPE_CODE { get; set; }
        /// <summary>
        /// ��Ʒ������������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ������������")]
        public string PRODUCT_FAULT_TYPE_NAME { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("����ʱ��")]
        public DateTime? CREATEDATE { get; set; }
        /// <summary>
        /// ������key
        /// </summary>
        /// <returns></returns>
        [DisplayName("������key")]
        public string CREATEUSERID { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        [DisplayName("����������")]
        public string CREATEUSERNAME { get; set; }
        /// <summary>
        /// �޸�ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸�ʱ��")]
        public DateTime? MODIFYDATE { get; set; }
        /// <summary>
        /// �޸���key
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸���key")]
        public string MODIFYUSERID { get; set; }
        /// <summary>
        /// �޸�������
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸�������")]
        public string MODIFYUSERNAME { get; set; }
        /// <summary>
        /// ɾ��״̬
        /// </summary>
        /// <returns></returns>
        [DisplayName("ɾ��״̬")]
        public int? DELETEMARK { get; set; }
        /// <summary>
        /// Ԥ���ֶ�1
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�1")]
        public string RESERVE1 { get; set; }
        /// <summary>
        /// Ԥ���ֶ�2
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�2")]
        public string RESERVE2 { get; set; }
        /// <summary>
        /// Ԥ���ֶ�3
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�3")]
        public string RESERVE3 { get; set; }
        /// <summary>
        /// Ԥ���ֶ�4
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�4")]
        public string RESERVE4 { get; set; }
        /// <summary>
        /// Ԥ���ֶ�5
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�5")]
        public string RESERVE5 { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.PRODUCT_FAULT_ITEM_KEY = CommonHelper.GetGuid;
            this.CREATEDATE = ServerTime.Now;
            this.CREATEUSERID = SystemLog.UserKey;
            this.CREATEUSERNAME = SystemLog.UserName;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.PRODUCT_FAULT_ITEM_KEY = KeyValue;
            this.MODIFYDATE = ServerTime.Now;
            this.MODIFYUSERID = SystemLog.UserKey;
            this.MODIFYUSERNAME = SystemLog.UserName;
        }
        #endregion
    }
}