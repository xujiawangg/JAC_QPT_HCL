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
    /// ��ȫ���ɼ����̱�
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.11.28 18:48</date>
    /// </author>
    /// </summary>
    [Description("��ȫ���ɼ����̱�")]
    [PrimaryKey("KEY_PART_INFOR_KEY")]
    public class P_KEY_PART_INFOR : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
        public string KEY_PART_INFOR_KEY { get; set; }
        /// <summary>
        /// ��ȫ����������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ȫ����������")]
        public string KEY_PART_C_KEY { get; set; }
        /// <summary>
        /// װ��ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("װ��ʱ��")]
        public DateTime? ASSMBLY_TIME { get; set; }
        /// <summary>
        /// ��˾key
        /// </summary>
        /// <returns></returns>
        [DisplayName("��˾key")]
        public string SITE_KEY { get; set; }
        /// <summary>
        /// ��˾���
        /// </summary>
        /// <returns></returns>
        [DisplayName("��˾���")]
        public string SITE_CODE { get; set; }
        /// <summary>
        /// �豸���
        /// </summary>
        /// <returns></returns>
        [DisplayName("�豸���")]
        public string SITE_NAME { get; set; }
        /// <summary>
        /// ����key
        /// </summary>
        /// <returns></returns>
        [DisplayName("����key")]
        public string AREA_KEY { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        [DisplayName("�������")]
        public string AREA_CODE { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string AREA_NAME { get; set; }
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
        /// �豸key
        /// </summary>
        /// <returns></returns>
        [DisplayName("�豸key")]
        public string EQUIP_KEY { get; set; }
        /// <summary>
        /// �豸���
        /// </summary>
        /// <returns></returns>
        [DisplayName("�豸���")]
        public string EQUIP_CODE { get; set; }
        /// <summary>
        /// �豸����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�豸����")]
        public string EQUIP_NAME { get; set; }
        /// <summary>
        /// ��������key
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������key")]
        public string WORK_CENTER_KEY { get; set; }
        /// <summary>
        /// �������ı��
        /// </summary>
        /// <returns></returns>
        [DisplayName("�������ı��")]
        public string WORK_CENTER_CODE { get; set; }
        /// <summary>
        /// ������������
        /// </summary>
        /// <returns></returns>
        [DisplayName("������������")]
        public string WORK_CENTER_NAME { get; set; }
        /// <summary>
        /// ���key
        /// </summary>
        /// <returns></returns>
        [DisplayName("���key")]
        public string SHIFT_KEY { get; set; }
        /// <summary>
        /// ��α��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��α��")]
        public string SHIFT_CODE { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        [DisplayName("�������")]
        public string SHIFT_NAME { get; set; }
        /// <summary>
        /// ����key
        /// </summary>
        /// <returns></returns>
        [DisplayName("����key")]
        public string TEAM_KEY { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [DisplayName("������")]
        public string TEAM_CODE { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string TEAM_NAME { get; set; }
        /// <summary>
        /// ��Աkey
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Աkey")]
        public string STAFF_KEY { get; set; }
        /// <summary>
        /// ��Ա���
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ա���")]
        public string STAFF_CODE { get; set; }
        /// <summary>
        /// ��Ա����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ա����")]
        public string STAFF_NAME { get; set; }
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
        /// �ƻ�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ƻ�����")]
        public int? PLAN_NUM { get; set; }
        /// <summary>
        /// �ƻ����к�
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ƻ����к�")]
        public string PLAN_NO { get; set; }
        /// <summary>
        /// ��Ʒ����֤
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ����֤")]
        public string PRODUCT_BORN_CODE { get; set; }
        /// <summary>
        /// ��Ʒ��ˮ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ��ˮ��")]
        public string PRODUCT_SERIAL_NO { get; set; }
        /// <summary>
        /// ��ƷID
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ƷID")]
        public string PRODUCT_KEY { get; set; }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ����")]
        public string PRODUCT_NAME { get; set; }
        /// <summary>
        /// ��Ʒ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ���")]
        public string PRODUCT_CODE { get; set; }
        /// <summary>
        /// ��Ʒ������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ������")]
        public string PRODUCT_ABB { get; set; }
        /// <summary>
        /// ��Ʒ���κ�
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ���κ�")]
        public string PRODUCT_BATCH_NO { get; set; }
        /// <summary>
        /// ��Ʒ�ͺ�
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ�ͺ�")]
        public string PRODUCT_MODEL_NO { get; set; }
        /// <summary>
        /// ��Ʒ�ṹ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ�ṹ��")]
        public string PRODUCT_STRUCT_NO { get; set; }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ����")]
        public string PRODUCT_TYPE { get; set; }
        /// <summary>
        /// ���͹�λ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("���͹�λ����")]
        public string DISTRIBUTION_STA_KEY { get; set; }
        /// <summary>
        /// ���͹�λ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("���͹�λ���")]
        public string DISTRIBUTION_STA_CODE { get; set; }
        /// <summary>
        /// ���͹�λ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("���͹�λ����")]
        public string DISTRIBUTION_STA_NAME { get; set; }
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
        /// �������κ�
        /// </summary>
        /// <returns></returns>
        [DisplayName("�������κ�")]
        public string PART_BATCH_NO { get; set; }
        /// <summary>
        /// ����VIN��
        /// </summary>
        /// <returns></returns>
        [DisplayName("����VIN��")]
        public string PART_VINCODE { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string PART_BARCODE { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
        public int? QUANTITY { get; set; }
        /// <summary>
        /// ��Ӧ������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ӧ������")]
        public string SUPPLIER_KEY { get; set; }
        /// <summary>
        /// ��Ӧ�̱��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ӧ�̱��")]
        public string SUPPLIER_CODE { get; set; }
        /// <summary>
        /// ��Ӧ������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ӧ������")]
        public string SUPPLIER_NAME { get; set; }
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
            this.KEY_PART_INFOR_KEY = CommonHelper.GetGuid;
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
            this.KEY_PART_INFOR_KEY = KeyValue;
            this.MODIFYDATE = ServerTime.Now;
            this.MODIFYUSERID = SystemLog.UserKey;
            this.MODIFYUSERNAME = SystemLog.UserName;
        }
        #endregion
    }
}