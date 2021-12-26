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
    /// ANDON��Ϣ���̱�
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.12.24 14:37</date>
    /// </author>
    /// </summary>
    [Description("ANDON��Ϣ�������󵵰���")]
    [PrimaryKey("ANDON_MATERIAL_NEED_KEY")]
    public class DOC_ANDON_MATERIAL_NEED : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// ANDON������������
        /// </summary>
        /// <returns></returns>
        [DisplayName("ANDON������������")]
        public string ANDON_MATERIAL_NEED_KEY { get; set; }
        /// <summary>
        /// ANDON��Ϣ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("ANDON��Ϣ����")]
        public string ANDON_INFOR_KEY { get; set; }
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
        /// ��˾����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��˾����")]
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
        /// ����key
        /// </summary>
        /// <returns></returns>
        [DisplayName("����key")]
        public string WORKSHOP_KEY { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [DisplayName("������")]
        public string WORKSHOP_CODE { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string WORKSHOP_NAME { get; set; }
        /// <summary>
        /// �ӹ�����key
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ӹ�����key")]
        public string MACHINING_CENTER_KEY { get; set; }
        /// <summary>
        /// �ӹ����ı��
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ӹ����ı��")]
        public string MACHINING_CENTER_CODE { get; set; }
        /// <summary>
        /// �ӹ���������
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ӹ���������")]
        public string MACHINING_CENTER_NAME { get; set; }
        /// <summary>
        /// ������key
        /// </summary>
        /// <returns></returns>
        [DisplayName("������key")]
        public string PRODUCTION_LINE_KEY { get; set; }
        /// <summary>
        /// �����߱��
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����߱��")]
        public string PRODUCTION_LINE_CODE { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        [DisplayName("����������")]
        public string PRODUCTION_LINE_NAME { get; set; }
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
        /// �豸key
        /// </summary>
        /// <returns></returns>
        [DisplayName("�豸key")]
        public string EQUIPMENT_KEY { get; set; }
        /// <summary>
        /// �豸���
        /// </summary>
        /// <returns></returns>
        [DisplayName("�豸���")]
        public string EQUIPMENT_CODE { get; set; }
        /// <summary>
        /// �豸����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�豸����")]
        public string EQUIPMENT_NAME { get; set; }
        /// <summary>
        /// andon����key
        /// </summary>
        /// <returns></returns>
        [DisplayName("andon����key")]
        public string ANDON_TYPE_KEY { get; set; }
        /// <summary>
        /// andon���ͱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("andon���ͱ��")]
        public string ANDON_TYPE_CODE { get; set; }
        /// <summary>
        /// andon��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("andon��������")]
        public string ANDON_TYPE_NAME { get; set; }
        /// <summary>
        /// andon��Ϣkey
        /// </summary>
        /// <returns></returns>
        [DisplayName("andon��Ϣkey")]
        public string ANDON_INFO_KEY { get; set; }
        /// <summary>
        /// andon��Ϣ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("andon��Ϣ���")]
        public string ANDON_INFO_CODE { get; set; }
        /// <summary>
        /// andon��Ϣ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("andon��Ϣ����")]
        public string ANDON_INFO_NAME { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ע")]
        public string REMARK { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [DisplayName("������")]
        public string PUSH_PEOPLE { get; set; }
        /// <summary>
        /// �ȼ�
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ȼ�")]
        public string RANK { get; set; }
        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ʼʱ��")]
        public DateTime? START_TIME { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("����ʱ��")]
        public DateTime? END_TIME { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("����ʱ��")]
        public int? CONTINUED_TIME { get; set; }
        /// <summary>
        /// ������key
        /// </summary>
        /// <returns></returns>
        [DisplayName("������key")]
        public string FEED_STAFF_KEY { get; set; }
        /// <summary>
        /// �����˱��
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����˱��")]
        public string FEED_STAFF_CODE { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        [DisplayName("����������")]
        public string FEED_STAFF_NAME { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("����ʱ��")]
        public DateTime? FEED_TIME { get; set; }
        /// <summary>
        /// ���ͳ���ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("���ͳ���ʱ��")]
        public int? FEED_CONTINUED_TIME { get; set; }
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
            this.ANDON_MATERIAL_NEED_KEY = CommonHelper.GetGuid;
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
            this.ANDON_MATERIAL_NEED_KEY = KeyValue;
            this.MODIFYDATE = ServerTime.Now;
            this.MODIFYUSERID = SystemLog.UserKey;
            this.MODIFYUSERNAME = SystemLog.UserName;
        }
        #endregion
    }
}