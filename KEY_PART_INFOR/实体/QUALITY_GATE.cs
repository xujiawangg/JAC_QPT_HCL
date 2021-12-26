//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2019
// Software Developers @ HfutIE 2019
//=====================================================================================

using HfutIe;
using HfutIe.DataAccess.Attributes;
using HfutIe.Entity;
using System;
using System.ComponentModel;

namespace HfutIE.Entity
{
    /// <summary>
    /// �����������ű��
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.03.04 09:28</date>
    /// </author>
    /// </summary>
    [Description("�����������ű��")]
    [PrimaryKey("QUALITY_GATE_KEY")]
    public class QUALITY_GATE : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// ��������������Ϣ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������������Ϣ����")]
        public string QUALITY_GATE_KEY { get; set; }
        /// <summary>
        /// ��λ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��λ����")]
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
        /// ��λ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��λ����")]
        public string STATION_DESCRIPTIN { get; set; }
        /// <summary>
        /// ��λ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��λ����")]
        public string STATION_TYPE { get; set; }
        /// <summary>
        /// �Ƿ�����λ
        /// </summary>
        /// <returns></returns>
        [DisplayName("�Ƿ�����λ")]
        public string IS_MAIN { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ע")]
        public string REMARK { get; set; }
        /// <summary>
        /// ��������������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������������")]
        public string CFG_STATION_KEY { get; set; }
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
        /// Ԥ���ֶ�
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�")]
        public string RESERVE01 { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.QUALITY_GATE_KEY = CommonHelper.GetGuid;
            this.CREATEDATE = DateTime.Now;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="KeyValue"></param>
        #endregion
    }
}