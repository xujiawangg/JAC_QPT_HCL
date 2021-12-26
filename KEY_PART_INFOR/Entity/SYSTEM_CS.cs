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
    /// �ײ������־��
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.02.22 17:34</date>
    /// </author>
    /// </summary>
    [Description("�ײ������־��")]
    [PrimaryKey("RECORD_ID")]
    public class SYSTEM_CS : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// ��¼ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("��¼ID")]
        public string RECORD_ID { get; set; }
        /// <summary>
        /// �û����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�û����")]
        public string USER_CODE { get; set; }
        /// <summary>
        /// �û�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�û�����")]
        public string USER_NAME { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
        public string CT { get; set; }
        /// <summary>
        /// ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("ʱ��")]
        public DateTime? TIME { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        [DisplayName("����������")]
        public string PC_NAME { get; set; }
        /// <summary>
        /// IP��ַ
        /// </summary>
        /// <returns></returns>
        [DisplayName("IP��ַ")]
        public string IP { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.RECORD_ID = CommonHelper.GetGuid;
            this.TIME = ServerTime.Now;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.RECORD_ID = KeyValue;
        }
        #endregion
    }
}