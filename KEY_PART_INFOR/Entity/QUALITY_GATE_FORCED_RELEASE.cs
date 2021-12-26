//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2019
// Software Developers @ HfutIE 2019
//=====================================================================================

using HfutIe;
using System;
using System.ComponentModel;

namespace HfutIE.Entity
{
    /// <summary>
    /// ������ǿ�Ʒ�����Ϣ��
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.03.04 09:28</date>
    /// </author>
    /// </summary>
    [Description("������ǿ�Ʒ�����Ϣ��")]
    [PrimaryKey("QUALITY_GATE_FORCED_RELEASEKEY")]
    public class QUALITY_GATE_FORCED_RELEASE : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// ��������������Ϣ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
        public string QUALITY_GATE_FORCED_RELEASEKEY { get; set; }
        /// <summary>
        /// ��λ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ����֤")]
        public string PRODUCT_BORN_CODE { get; set; }
        /// <summary>
        /// ��λ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("��λ����")]
        public string STATION_KEY { get; set; }
        /// <summary>
        /// ��λ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("���н��")]
        public string RELEASE_RESULT { get; set; }
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
        /// Ԥ���ֶ�
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ע")]
        public string REMARK { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.QUALITY_GATE_FORCED_RELEASEKEY = CommonHelper.GetGuid;
            this.CREATEDATE = ServerTime.Now;
            this.CREATEUSERID = SystemLog.UserKey;
            this.CREATEUSERNAME = SystemLog.UserName;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="KeyValue"></param>
        #endregion
    }
}