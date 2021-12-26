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
    /// �豸�������
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.12.11 21:00</date>
    /// </author>
    /// </summary>
    [Description("�豸�������")]
    [PrimaryKey("CHECH_RES_KEY")]
    public class CHECK_EQUIP_RESULT : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// key
        /// </summary>
        /// <returns></returns>
        [DisplayName("key")]
        public string CHECH_RES_KEY { get; set; }
        /// <summary>
        /// �����Ϣkey
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����Ϣkey")]
        public string CHECK_INFO_KEY { get; set; }
        /// <summary>
        /// �����Ϣ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����Ϣ���")]
        public string CHECK_INFO_CODE { get; set; }
        /// <summary>
        /// �����Ϣ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����Ϣ����")]
        public string CHECK_INFO_NAME { get; set; }
        /// <summary>
        /// �����Ϣ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����Ϣ����")]
        public string CHECK_INFO_DESCRIPTION { get; set; }
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
        /// �����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����")]
        public string CHECK_RESULT { get; set; }
        /// <summary>
        /// ���ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("���ʱ��")]
        public DateTime? CHECK_TIME { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("����ʱ��")]
        public DateTime? CREATEDATE { get; set; }
        /// <summary>
        /// ������ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("������ID")]
        public string CREATEUSERID { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        [DisplayName("����������")]
        public string CREATEUSERNAME { get; set; }
        /// <summary>
        /// Ԥ��1
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��1")]
        public string RESERVE1 { get; set; }
        /// <summary>
        /// Ԥ��2
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��2")]
        public string RESERVE2 { get; set; }
        /// <summary>
        /// Ԥ��3
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��3")]
        public string RESERVE3 { get; set; }
        /// <summary>
        /// Ԥ��4
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��4")]
        public string RESERVE4 { get; set; }
        /// <summary>
        /// Ԥ��5
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��5")]
        public string RESERVE5 { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.CHECH_RES_KEY = CommonHelper.GetGuid;
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
            this.CHECH_RES_KEY = KeyValue;
        }
        #endregion
    }
}