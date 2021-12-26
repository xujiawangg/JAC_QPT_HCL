//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2017
// Software Developers @ HfutIE 2017
//=====================================================================================

using System;
using System.ComponentModel;
using System.Text;

namespace HfutIe
{
    #region ��ȡ/���� �ֶ�ֵ
    /// <summary>
    /// ADON_INFO
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.03.21 20:35</date>
    /// </author>
    /// </summary>
    public class GUIDFILES : BaseEntity
    {
        /// <summary>
        /// �ĵ�ID
        /// </summary>
        [DisplayName("�ĵ�ID")]
        public string document_key { get; set; }
        /// <summary>
        /// ������ID
        /// </summary>
        [DisplayName("������ID")]
        public string p_line_key { get; set; }
        /// <summary>
        /// �����߱��
        /// </summary>
        [DisplayName("�����߱��")]
        public string p_line_code { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        [DisplayName("����������")]
        public string p_line_name { get; set; }
        /// <summary>
        /// ��λID
        /// </summary>
        [DisplayName("��λID")]
        public string wc_key { get; set; }
        /// <summary>
        /// ��λ���
        /// </summary>
        [DisplayName("��λ���")]
        public string wc_code { get; set; }
        /// <summary>
        /// ��λ����
        /// </summary>
        [DisplayName("��λ����")]
        public string wc_name { get; set; }
        /// <summary>
        /// ��ƷID
        /// </summary>
        [DisplayName("��ƷID")]
        public string product_key { get; set; }
        /// <summary>
        /// ��Ʒ���
        /// </summary>
        [DisplayName("��Ʒ���")]
        public string product_code { get; set; }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        [DisplayName("��Ʒ����")]
        public string product_name { get; set; }
        /// <summary>
        /// �ĵ����
        /// </summary>
        [DisplayName("�ĵ����")]
        public string document_code { get; set; }

        /// <summary>
        /// �ĵ�����
        /// </summary>
        [DisplayName("�ĵ�����")]
        public string document_name { get; set; }
        /// <summary>
        /// �ĵ����
        /// </summary>
        [DisplayName("�ĵ����")]
        public string document_type { get; set; }
        /// <summary>
        /// �ĵ��汾��
        /// </summary>
        [DisplayName("�ĵ��汾��")]
        public string document_edition { get; set; }
        /// <summary>
        /// �ĵ���С
        /// </summary>
        [DisplayName("�ĵ���С")]
        public string document_size { get; set; }
        /// <summary>
        /// �ĵ�����
        /// </summary>
        [DisplayName("�ĵ�����")]
        public byte[] document_file { get; set; }
        /// <summary>
        /// �ĵ�����
        /// </summary>
        [DisplayName("��ע")]
        public byte[] remarks { get; set; }

        #endregion
    }
}