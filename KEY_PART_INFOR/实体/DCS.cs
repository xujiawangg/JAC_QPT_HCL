
using System;
using System.ComponentModel;
using System.Text;

namespace HfutIe.Entity
{
    /// <summary>
    /// DCS
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.02.24 13:49</date>
    /// </author>
    /// </summary>
    public class DCS :BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// dcs_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("dcs_key")]
        public string dcs_key { get; set; }
        /// <summary>
        /// DCS����
        /// </summary>
        /// <returns></returns>
        public string dcs_name { get; set; }
        /// <summary>
        /// DCS����
        /// </summary>
        /// <returns></returns>
        [DisplayName("DCS����")]
        public string dcs_description { get; set; }
        /// <summary>
        /// DCS����
        /// </summary>
        /// <returns></returns>
        [DisplayName("DCS����")]
        public string dcs_type { get; set; }
        /// <summary>
        /// Ԥ��01
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��03")]
        public string reserve03 { get; set; }
        /// <summary>
        /// Ԥ��02
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��04")]
        public string reserve04 { get; set; }
        /// <summary>
        /// dcs_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("dcs_code")]
        public string dcs_code { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("����ʱ��")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// ������ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("������ID")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        [DisplayName("����������")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// �޸�ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸�ʱ��")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// �޸���ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸���ID")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// �޸�������
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸�������")]
        public string ModifyUserName { get; set; }
        #endregion
        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.dcs_key = CommonHelper.GetGuid;
            this.CreateDate = DateTime.Now;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.dcs_key = KeyValue;
            this.ModifyDate = DateTime.Now;
        }
        #endregion
    }
}