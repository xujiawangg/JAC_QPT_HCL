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
    /// ��λ
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.01.21 17:00</date>
    /// </author>
    /// </summary>
    [Description("��λ")]
    [PrimaryKey("STATION_KEY")]
    public class STATION : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
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
        /// ��������key
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������key")]
        public string WORK_CENTER_KEY { get; set; }
        /// <summary>
        /// ������key
        /// </summary>
        /// <returns></returns>
        [DisplayName("������key")]
        public string PRODUCTION_LINE_KEY { get; set; }
        /// <summary>
        /// ��λ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��λ����")]
        public string STATION_DESCRIPTION { get; set; }
        /// <summary>
        /// station_beat
        /// </summary>
        /// <returns></returns>
        [DisplayName("station_beat")]
        public int? STATION_BEAT { get; set; }
        /// <summary>
        /// ��λ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��λ����")]
        public string STATION_TYPE { get; set; }
        /// <summary>
        /// inst_list_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("inst_list_key")]
        public string INST_LIST_KEY { get; set; }
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
        /// �޸�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸�����")]
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
        /// Ԥ��1
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��1")]
        public string RESERVE01 { get; set; }
        /// <summary>
        /// Ԥ��2
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��2")]
        public string RESERVE02 { get; set; }
        /// <summary>
        /// Ԥ��3
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��3")]
        public string RESERVE03 { get; set; }
        /// <summary>
        /// Ԥ��4
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��4")]
        public string RESERVE04 { get; set; }
        /// <summary>
        /// Ԥ��5
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��5")]
        public string RESERVE05 { get; set; }
        /// <summary>
        /// Ԥ��6
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��6")]
        public string RESERVE06 { get; set; }
        /// <summary>
        /// Ԥ��7
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��7")]
        public string RESERVE07 { get; set; }
        /// <summary>
        /// Ԥ��8
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��8")]
        public string RESERVE08 { get; set; }
        /// <summary>
        /// Ԥ��9
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��9")]
        public string RESERVE09 { get; set; }
        /// <summary>
        /// Ԥ��10
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��10")]
        public string RESERVE10 { get; set; }
        /// <summary>
        /// ɾ����ʶ
        /// </summary>
        /// <returns></returns>
        [DisplayName("ɾ����ʶ")]
        public int? DELETEMARK { get; set; }
        /// <summary>
        /// �Ƿ�����λ
        /// </summary>
        /// <returns></returns>
        [DisplayName("�Ƿ�����λ")]
        public string IS_MAIN { get; set; }
        #endregion
    }
}