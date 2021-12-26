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
    /// ��Ʒ���ϴ�����Ϣ��
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.01.19 11:14</date>
    /// </author>
    /// </summary>
    [Description("��Ʒ���ϴ�����Ϣ��")]
    [PrimaryKey("PRODUCT_FA_MAIN_INFOR_KEY")]
    public class PRODUCT_FA_MAIN_INFOR : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// ��Ʒ���ϴ�����Ϣ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ���ϴ�����Ϣ����")]
        public string PRODUCT_FA_MAIN_INFOR_KEY { get; set; }
        /// <summary>
        /// ������Ϣ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("������Ϣ����")]
        public string PRODUCT_FAULT_ITEM_KEY { get; set; }
        /// <summary>
        /// ������Ϣ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("������Ϣ���")]
        public string PRODUCT_FAULT_ITEM_CODE { get; set; }
        /// <summary>
        /// ������Ϣ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("������Ϣ����")]
        public string PRODUCT_FAULT_ITEM_NAME { get; set; }
        /// <summary>
        /// ������Ϣ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("������Ϣ����")]
        public string DISCRIBE { get; set; }
        /// <summary>
        /// �Źʴ�ʩ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�Źʴ�ʩ����")]
        public string PRODUCT_MAINTAIN_ITEM_KEY { get; set; }
        /// <summary>
        /// �Źʴ�ʩ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("�Źʴ�ʩ���")]
        public string PRODUCT_MAINTAIN_ITEM_CODE { get; set; }
        /// <summary>
        /// �Źʴ�ʩ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�Źʴ�ʩ����")]
        public string PRODUCT_MAINTAIN_ITEM_NAME { get; set; }
        /// <summary>
        /// �ƻ�key
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ƻ�key")]
        public string MES_PLAN_KEY { get; set; }
        /// <summary>
        /// �ƻ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ƻ����")]
        public string MES_PLAN_CODE { get; set; }
        /// <summary>
        /// ��Ʒkey
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒkey")]
        public string PRODUCT_KEY { get; set; }
        /// <summary>
        /// ��Ʒ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ���")]
        public string PRODUCT_CODE { get; set; }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ����")]
        public string PRODUCT_NAME { get; set; }
        /// <summary>
        /// ��Ʒ����֤
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ����֤")]
        public string PRODUCT_BORN_CODE { get; set; }
        /// <summary>
        /// ����key
        /// </summary>
        /// <returns></returns>
        [DisplayName("����key")]
        public string PRODUCTION_LINE_KEY { get; set; }
        /// <summary>
        /// ���߱��
        /// </summary>
        /// <returns></returns>
        [DisplayName("���߱��")]
        public string PRODUCTION_LINE_CODE { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string PRODUCTION_LINE_NAME { get; set; }
        /// <summary>
        /// ��λkey
        /// </summary>
        /// <returns></returns>
        [DisplayName("��λkey")]
        public string STATION_KEY { get; set; }
        /// <summary>
        /// ��λ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("��λ���")]
        public string STATION_CODE { get; set; }
        /// <summary>
        /// ��λ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��λ����")]
        public string STATION_NAME { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ע")]
        public string REMARKS { get; set; }
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
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.PRODUCT_FA_MAIN_INFOR_KEY = CommonHelper.GetGuid;
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
            this.PRODUCT_FA_MAIN_INFOR_KEY = KeyValue;
                                            }
        #endregion
    }
}