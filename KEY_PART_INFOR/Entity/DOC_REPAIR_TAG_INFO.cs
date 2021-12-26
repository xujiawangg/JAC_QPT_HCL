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
    /// ����Tag��Ϣ��
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.09.19 15:55</date>
    /// </author>
    /// </summary>
    [Description("����Tag��Ϣ������")]
    [PrimaryKey("TAG_INFO_KEY")]
    public class DOC_REPAIR_TAG_INFO : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
        public string TAG_INFO_KEY { get; set; }
        /// <summary>
        /// ��ַ·��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ַ·��")]
        public string OPC_ITEM_NAME { get; set; }
        /// <summary>
        /// ·����Ӧ�洢ֵ
        /// </summary>
        /// <returns></returns>
        [DisplayName("·����Ӧ�洢ֵ")]
        public string OPC_ITEM_VALUE { get; set; }
        /// <summary>
        /// �ƻ�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ƻ�����")]
        public string MES_PLAN_KEY { get; set; }
        /// <summary>
        /// �ƻ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ƻ����")]
        public string MES_PLAN_CODE { get; set; }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ����")]
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
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
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
        /// �������߹�λ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�������߹�λ����")]
        public string REPAIR_OFFLINE_STATION_KEY { get; set; }
        /// <summary>
        /// �������߹�λ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("�������߹�λ���")]
        public string REPAIR_OFFLINE_STATION_CODE { get; set; }
        /// <summary>
        /// �������߹�λ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�������߹�λ����")]
        public string REPAIR_OFFLINE_STATION_NAME { get; set; }
        /// <summary>
        /// ��������ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������ʱ��")]
        public DateTime? REPAIR_OFFLINE_TIME { get; set; }
        /// <summary>
        /// �������߹�λ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�������߹�λ����")]
        public string REPAIR_ONLINE_STATION_KEY { get; set; }
        /// <summary>
        /// �������߹�λ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("�������߹�λ���")]
        public string REPAIR_ONLINE_STATION_CODE { get; set; }
        /// <summary>
        /// �������߹�λ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�������߹�λ����")]
        public string REPAIR_ONLINE_STATION_NAME { get; set; }
        /// <summary>
        /// ��������ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������ʱ��")]
        public DateTime? REPAIR_ONLINE_TIME { get; set; }
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
            this.TAG_INFO_KEY = CommonHelper.GetGuid;
            this.CREATEDATE = DateTime.Now;
            this.CREATEUSERID = SystemLog.UserKey;
            this.CREATEUSERNAME = SystemLog.UserName;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.TAG_INFO_KEY = KeyValue;
                                            }
        #endregion
    }
}