//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2017
// Software Developers @ HfutIE 2017
//=====================================================================================

 
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HfutIe.Entity
{
    /// <summary>
    /// ������Ϣ
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.03.14 09:35</date>
    /// </author>
    /// </summary>
    [Description("������Ϣ")]
 
    public class ALARM_INFOR  
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// alarm_infor_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("alarm_infor_key")]
        public string alarm_infor_key { get; set; }
        /// <summary>
        /// alarm_code
        /// </summary>
        /// <returns></returns>
        [DisplayName("alarm_code")]
        public string alarm_code { get; set; }
        /// <summary>
        /// alarm_description
        /// </summary>
        /// <returns></returns>
        [DisplayName("alarm_description")]
        public string alarm_description { get; set; }
        /// <summary>
        /// alarm_grade
        /// </summary>
        /// <returns></returns>
        [DisplayName("alarm_grade")]
        public int? alarm_grade { get; set; }
        /// <summary>
        /// alarm_type
        /// </summary>
        /// <returns></returns>
        [DisplayName("alarm_type")]
        public string alarm_type { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("����ʱ��")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// �����û�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����û�����")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����û�")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// �޸�ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸�ʱ��")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸��û�����")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// �޸��û�
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸��û�")]
        public string ModifyUserName { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public  void Create()
        {
            this.alarm_infor_key = CommonHelper.GetGuid;
            this.CreateDate = DateTime.Now;
            this.CreateUserId = SystemLog.UserKey;
            this.CreateUserName = SystemLog.UserName;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="KeyValue"></param>
        public   void Modify(string KeyValue)
        {
            this.alarm_infor_key = KeyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = SystemLog.UserKey;
            this.ModifyUserName = SystemLog.UserName;
        }
        #endregion
    }
}