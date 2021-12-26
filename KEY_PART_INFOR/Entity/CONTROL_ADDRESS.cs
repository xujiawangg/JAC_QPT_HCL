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
    /// ���Ƶ�ַ������
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.12.12 16:11</date>
    /// </author>
    /// </summary>
    [Description("���Ƶ�ַ������")]
    [PrimaryKey("CONTROL_ADDRESS_KEY")]
    public class CONTROL_ADDRESS : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// ��ַID
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ַID")]
        public string CONTROL_ADDRESS_KEY { get; set; }
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�Ƿ�����")]
        public int? HAS_CONFIGED { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string POSITION_KEY { get; set; }
        /// <summary>
        /// ��ַ���ʹ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ַ���ʹ���")]
        public string ADDRESS_CODE { get; set; }
        /// <summary>
        /// ��ַ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ַ����")]
        public string ADDRESS_NAME { get; set; }
        /// <summary>
        /// ��ַ·��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ַ·��")]
        public string ADDRESS_PATH { get; set; }
        /// <summary>
        /// ��ַ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ַ��������")]
        public string ADDRESS_DATA_TYPE { get; set; }
        /// <summary>
        /// ��ַ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ַ����")]
        public string ADDRESS_TYPE { get; set; }
        /// <summary>
        /// ��ַ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ַ����")]
        public string ADDRESS_DESCRIPTION { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ע")]
        public string REMARKS { get; set; }
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
        /// <summary>
        /// ɾ��״̬
        /// </summary>
        /// <returns></returns>
        [DisplayName("ɾ��״̬")]
        public int? DELETEMARK { get; set; }
        /// <summary>
        /// Ԥ��01
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��01")]
        public string RESERVE01 { get; set; }
        /// <summary>
        /// Ԥ��02
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��02")]
        public string RESERVE02 { get; set; }
        /// <summary>
        /// Ԥ��03
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��03")]
        public string RESERVE03 { get; set; }
        /// <summary>
        /// Ԥ��04
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��04")]
        public string RESERVE04 { get; set; }
        /// <summary>
        /// Ԥ��05
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��05")]
        public string RESERVE05 { get; set; }
        /// <summary>
        /// Ԥ��06
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��06")]
        public string RESERVE06 { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.CONTROL_ADDRESS_KEY = CommonHelper.GetGuid;
            this.CREATEDATE = ServerTime.Now;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="KeyValue"></param>
        //public override void Modify(string KeyValue)
        //{
        //    this.CONTROL_ADDRESS_KEY = KeyValue;
        //    this.MODIFYDATE = DateTime.Now;
        //    this.MODIFYUSERID = ManageProvider.Provider.Current().UserId;
        //    this.MODIFYUSERNAME = ManageProvider.Provider.Current().UserName;
        //}
        #endregion
    }
}