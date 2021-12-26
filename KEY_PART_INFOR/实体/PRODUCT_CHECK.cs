//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2017
// Software Developers @ HfutIE 2017
//=====================================================================================

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HfutIE.Entity
{
    /// <summary>
    /// PRODUCT_CHECK
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.11.20 10:58</date>
    /// </author>
    /// </summary>
    [Description("PRODUCT_CHECK")]
    public class PRODUCT_CHECK 
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
        public string PRODUCT_CHECK_KEY { get; set; }
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
        /// PRODUCT_BORN_CODE
        /// </summary>
        /// <returns></returns>
        [DisplayName("PRODUCT_BORN_CODE")]
        public string PRODUCT_BORN_CODE { get; set; }
        /// <summary>
        /// DCS����
        /// </summary>
        /// <returns></returns>
        [DisplayName("DCS����")]
        public string DCS_KEY { get; set; }
        /// <summary>
        /// DCS���
        /// </summary>
        /// <returns></returns>
        [DisplayName("DCS���")]
        public string DCS_CODE { get; set; }
        /// <summary>
        /// DCS����
        /// </summary>
        /// <returns></returns>
        [DisplayName("DCS����")]
        public string DCS_NAME { get; set; }
        /// <summary>
        /// ���������
        /// </summary>
        /// <returns></returns>
        [DisplayName("���������")]
        public string PARM_KEY { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        [DisplayName("�������")]
        public string PARM_TYPE { get; set; }
        /// <summary>
        /// ���������
        /// </summary>
        /// <returns></returns>
        [DisplayName("���������")]
        public string PARM_NAME { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
        public string REMARKS { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public DateTime? CREATEDATE { get; set; }
        /// <summary>
        /// �����û�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����û�����")]
        public string CREATEUSERID { get; set; }
        /// <summary>
        /// �����û�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����û�����")]
        public string CREATEUSERNAME { get; set; }
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸�����")]
        public DateTime? MODIFYDATE { get; set; }
        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸��û�����")]
        public string MODIFYUSERID { get; set; }
        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸��û�����")]
        public string MODIFYUSERNAME { get; set; }
        /// <summary>
        /// RESERVE01
        /// </summary>
        /// <returns></returns>
        [DisplayName("UPPER_CONTROL")]
        public double? UPPER_CONTROL { get; set; }
        /// <summary>
        /// RESERVE02
        /// </summary>
        /// <returns></returns>
        [DisplayName("TARGET")]
        public double? TARGET { get; set; }
        /// <summary>
        /// RESERVE03
        /// </summary>
        /// <returns></returns>
        [DisplayName("LOWER_CONTROL")]
        public double? LOWER_CONTROL { get; set; }
        /// <summary>
        /// RESERVE04
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE01")]
        public string RESERVE01 { get; set; }
        #endregion
    }
}