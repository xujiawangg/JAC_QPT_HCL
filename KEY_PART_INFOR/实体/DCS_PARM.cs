//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2019
// Software Developers @ HfutIE 2019
//=====================================================================================

using HfutIe;
using System;
using System.ComponentModel;
using System.Configuration;

namespace HfutIE.Entity
{
    /// <summary>
    /// DCS������
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.03.02 09:39</date>
    /// </author>
    /// </summary>
    [Description("DCS������")]
    [PrimaryKey("PARM_KEY")]
    public class DCS_PARM : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// �����key
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����key")]
        public string PARM_KEY { get; set; }
        /// <summary>
        /// DCS_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("DCS_key")]
        public string DCS_KEY { get; set; }
        /// <summary>
        /// ParmCode
        /// </summary>
        /// <returns></returns>
        [DisplayName("ParmCode")]
        public string PARM_CODE { get; set; }
        /// <summary>
        /// ���������
        /// </summary>
        /// <returns></returns>
        [DisplayName("���������")]
        public string PARM_NAME { get; set; }
        /// <summary>
        /// TextMask
        /// </summary>
        /// <returns></returns>
        [DisplayName("TextMask")]
        public string TEXT_MASK { get; set; }
        /// <summary>
        /// ���������
        /// </summary>
        /// <returns></returns>
        [DisplayName("���������")]
        public string PARM_TYPE { get; set; }
        /// <summary>
        /// ���������
        /// </summary>
        /// <returns></returns>
        [DisplayName("���������")]
        public string PARM_DESCRIPTION { get; set; }
        /// <summary>
        /// �Ͽ��ƽ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("�Ͽ��ƽ���")]
        public Single? UPPER_CONTROL { get; set; }
        /// <summary>
        /// ��׼ֵ
        /// </summary>
        /// <returns></returns>
        [DisplayName("��׼ֵ")]
        public Single? TARGET { get; set; }
        /// <summary>
        /// �¿��ƽ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("�¿��ƽ���")]
        public Single? LOWER_CONTROL { get; set; }
        /// <summary>
        /// ProcessUpperLimit
        /// </summary>
        /// <returns></returns>
        [DisplayName("ProcessUpperLimit")]
        public Single? PROCESS_UPPER_LIMIT { get; set; }
        /// <summary>
        /// ProcessLowerLimit
        /// </summary>
        /// <returns></returns>
        [DisplayName("ProcessLowerLimit")]
        public Single? PROCESS_LOWER_LIMIT { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ע")]
        public string REMARK { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("����ʱ��")]
        public string CREATEDATE { get; set; }
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
        public string MODIFYDATE { get; set; }
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
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.PARM_KEY = CommonHelper.GetGuid;
            this.CREATEDATE = DateTime.Now.ToString();
            this.CREATEUSERID = ConfigurationManager.AppSettings["ProgramName"].ToString();
            this.CREATEUSERNAME = ConfigurationManager.AppSettings["ProgramName"].ToString();
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.PARM_KEY = KeyValue;
            this.MODIFYDATE = DateTime.Now.ToString();
            this.CREATEUSERID = ConfigurationManager.AppSettings["ProgramName"].ToString();
            this.CREATEUSERNAME = ConfigurationManager.AppSettings["ProgramName"].ToString();
        }
        #endregion
    }
}