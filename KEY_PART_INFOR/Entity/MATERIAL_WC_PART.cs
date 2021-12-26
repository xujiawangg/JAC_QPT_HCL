//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2018
// Software Developers @ HfutIE 2018
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
    /// �߱߿⹤λ�������ñ�
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.12.24 10:03</date>
    /// </author>
    /// </summary>
    [Description("�߱߿⹤λ�������ñ�")]
    [PrimaryKey("MATERIAL_WC_PARTKEY")]
    public class MATERIAL_WC_PART : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
        public string MATERIAL_WC_PARTKEY { get; set; }
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
        /// ��λ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��λ����")]
        public string STATION_KEY { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
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
        /// ��ȫ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ȫ���")]
        public int? SAFETY_NUM { get; set; }
        /// <summary>
        /// �����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����")]
        public int? MAX_NUM { get; set; }
        /// <summary>
        /// ���͵�λ
        /// </summary>
        /// <returns></returns>
        [DisplayName("���͵�λ")]
        public string DELIVERY_UNIT { get; set; }
        /// <summary>
        /// ���͵�λ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("���͵�λ����")]
        public int? DELIVERY_UNIT_NUM { get; set; }
        /// <summary>
        /// �洢����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�洢����")]
        public int? STORAGE_NUM { get; set; }
        /// <summary>
        /// Delivery_Batch
        /// </summary>
        /// <returns></returns>
        [DisplayName("Delivery_Batch")]
        public int? DELIVERY_BATCH { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ע")]
        public string REMARK { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public DateTime? CREATEDATE { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        [DisplayName("����������")]
        public string CREATEUSERID { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        [DisplayName("����������")]
        public string CREATEUSERNAME { get; set; }
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸�����")]
        public DateTime? MODIFYDATE { get; set; }
        /// <summary>
        /// �޸�������
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸�������")]
        public string MODIFYUSERID { get; set; }
        /// <summary>
        /// �޸�������
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸�������")]
        public string MODIFYUSERNAME { get; set; }
        /// <summary>
        /// Ԥ���ֶ�01
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�01")]
        public string RESERVE01 { get; set; }
        /// <summary>
        /// Ԥ���ֶ�02
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�02")]
        public string RESERVE02 { get; set; }
        /// <summary>
        /// Ԥ���ֶ�03
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�03")]
        public string RESERVE03 { get; set; }
        /// <summary>
        /// Ԥ���ֶ�04
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�04")]
        public string RESERVE04 { get; set; }
        /// <summary>
        /// Ԥ���ֶ�05
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�05")]
        public string RESERVE05 { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.MATERIAL_WC_PARTKEY = CommonHelper.GetGuid;
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
            this.MATERIAL_WC_PARTKEY = KeyValue;
            this.MODIFYDATE = ServerTime.Now;
            this.MODIFYUSERID = SystemLog.UserKey;
            this.MODIFYUSERNAME = SystemLog.UserName;
        }
        #endregion
    }
}