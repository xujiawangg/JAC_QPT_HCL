//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2018
// Software Developers @ HfutIE 2018
//=====================================================================================

using HfutIe;using HfutIE.Utilities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HfutIE.Entity
{
    /// <summary>
    /// �㲿��������Ϣ
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.12.24 15:49</date>
    /// </author>
    /// </summary>
    [Description("�㲿��������Ϣ")]
    [PrimaryKey("PART_KEY")]
    public class PART : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
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
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string PART_TYPE { get; set; }
        /// <summary>
        /// �Ƿ��Ʒ
        /// </summary>
        /// <returns></returns>
        [DisplayName("�Ƿ��Ʒ")]
        public string IS_PRODUCT { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string PART_VARIETY { get; set; }
        /// <summary>
        /// ����ģ�ͺ�
        /// </summary>
        /// <returns></returns>
        [DisplayName("����ģ�ͺ�")]
        public string PART_MODEL_NO { get; set; }
        /// <summary>
        /// ����ƽ̨
        /// </summary>
        /// <returns></returns>
        [DisplayName("����ƽ̨")]
        public string PART_PLATFORM { get; set; }
        /// <summary>
        /// ����ͼ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("����ͼ��")]
        public string PART_DRAW_NO { get; set; }
        /// <summary>
        /// ���Ͻṹ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("���Ͻṹ��")]
        public string PART_STRUCT_NO { get; set; }
        /// <summary>
        /// ���ϼ�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("���ϼ�����")]
        public string PART_ABB { get; set; }
        /// <summary>
        /// ���ϵ�λ
        /// </summary>
        /// <returns></returns>
        [DisplayName("���ϵ�λ")]
        public string PART_UNIT { get; set; }
        /// <summary>
        /// �ɼ�ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ɼ�ʱ��")]
        public DateTime? SYNCHRO_TIME { get; set; }
        /// <summary>
        /// ERP��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("ERP��������")]
        public string ERP_PART_KEY { get; set; }
        /// <summary>
        /// ERP���ϱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("ERP���ϱ��")]
        public string ERP_PART_CODE { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("����ʱ��")]
        public DateTime? REVISE_TIME { get; set; }
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�Ƿ�����")]
        public string HAS_CONFIGED { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ע")]
        public string REMARKS { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public DateTime? CREATEDATE { get; set; }
        /// <summary>
        /// ������id
        /// </summary>
        /// <returns></returns>
        [DisplayName("������id")]
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
        /// �޸���id
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸���id")]
        public string MODIFYUSERID { get; set; }
        /// <summary>
        /// �޸�������
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸�������")]
        public string MODIFYUSERNAME { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
        public string RESERVE05 { get; set; }
        /// <summary>
        /// reserve06
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve06")]
        public string RESERVE06 { get; set; }
        /// <summary>
        /// reserve07
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve07")]
        public string RESERVE07 { get; set; }
        /// <summary>
        /// reserve08
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve08")]
        public string RESERVE08 { get; set; }
        /// <summary>
        /// reserve09
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve09")]
        public string RESERVE09 { get; set; }
        /// <summary>
        /// reserve10
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve10")]
        public string RESERVE10 { get; set; }
        /// <summary>
        /// ɾ�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("ɾ�����")]
        public int? DELETEMARK { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.PART_KEY = CommonHelper.GetGuid;
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
            this.PART_KEY = KeyValue;
            this.MODIFYDATE = ServerTime.Now;
            this.MODIFYUSERID = SystemLog.UserKey;
            this.MODIFYUSERNAME = SystemLog.UserName;
        }
        #endregion
    }
}