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
    /// ������ñ�
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.01.21 16:42</date>
    /// </author>
    /// </summary>
    [Description("������ñ�")]
    [PrimaryKey("CHECK_STATION_INFO_KEY")]
    public class CHECK_STATION_INFO : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// key
        /// </summary>
        /// <returns></returns>
        [DisplayName("key")]
        public string CHECK_STATION_INFO_KEY { get; set; }
        /// <summary>
        /// �����Ϣkey
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����Ϣkey")]
        public string CHECK_INFO_KEY { get; set; }
        /// <summary>
        /// ��λkey
        /// </summary>
        /// <returns></returns>
        [DisplayName("��λkey")]
        public string STATION_KEY { get; set; }
        /// <summary>
        /// ɾ�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("ɾ�����")]
        public int? DELETEMARK { get; set; }
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
        /// Ԥ���ֶ�
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�")]
        public string RESERVE01 { get; set; }
        /// <summary>
        /// Ԥ���ֶ�
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�")]
        public string RESERVE02 { get; set; }
        /// <summary>
        /// Ԥ���ֶ�
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�")]
        public string RESERVE03 { get; set; }
        /// <summary>
        /// Ԥ���ֶ�
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�")]
        public string RESERVE04 { get; set; }
        /// <summary>
        /// Ԥ���ֶ�
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�")]
        public string RESERVE05 { get; set; }
        /// <summary>
        /// �豸key
        /// </summary>
        /// <returns></returns>
        [DisplayName("�豸key")]
        public string EQUIPMENT_KEY { get; set; }
        /// <summary>
        /// ��ʶID
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ʶID")]
        public string ID { get; set; }
        #endregion
    }
}