//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2018
// Software Developers @ HfutIE 2018
//=====================================================================================

using HfutIe;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HfutIE.Entity
{
    /// <summary>
    /// ��ȫ���ɼ����ñ�
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.11.28 18:48</date>
    /// </author>
    /// </summary>
    [Description("��ȫ���ɼ����ñ�")]
    [PrimaryKey("KEY_PART_C_KEY")]
    public class P_KEY_PART_C : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
        public string KEY_PART_C_KEY { get; set; }
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
        /// ����key
        /// </summary>
        /// <returns></returns>
        [DisplayName("����key")]
        public string PART_KEY { get; set; }
        /// <summary>
        /// ���ϱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("���ϱ��")]
        public string PART_CODE { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string PART_NAME { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
        public int? QUANTITY { get; set; }
        /// <summary>
        /// Ԥ���ֶ�1
        /// </summary>
        /// <returns></returns>
        [DisplayName("�Ƿ����")]
        public string RESERVE1 { get; set; }
        /// <summary>
        /// Ԥ���ֶ�2
        /// </summary>
        /// <returns></returns>
        [DisplayName("����������")]
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
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.KEY_PART_C_KEY = CommonHelper.GetGuid;
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
            this.KEY_PART_C_KEY = KeyValue;
            this.MODIFYDATE = ServerTime.Now;
            this.MODIFYUSERID = SystemLog.UserKey;
            this.MODIFYUSERNAME = SystemLog.UserName;
        }
        #endregion
    }
}