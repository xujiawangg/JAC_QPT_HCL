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
    /// Bom�汾��Ϣ
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.02.28 16:27</date>
    /// </author>
    /// </summary>
    [Description("Bom�汾��Ϣ")]
    [PrimaryKey("VERSION_KEY")]
    public class BOM_VERSION : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// �汾ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("�汾ID")]
        public string VERSION_KEY { get; set; }
        /// <summary>
        /// �汾���
        /// </summary>
        /// <returns></returns>
        [DisplayName("�汾���")]
        public string VERSION_CODE { get; set; }
        /// <summary>
        /// �汾����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�汾����")]
        public string VERSION_NAME { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string PART_KEY { get; set; }
        /// <summary>
        /// �Ƿ�Ĭ�ϰ汾
        /// </summary>
        /// <returns></returns>
        [DisplayName("�Ƿ�Ĭ�ϰ汾")]
        public string IS_DEFAULT { get; set; }
        /// <summary>
        /// �Ƿ��ύ
        /// </summary>
        /// <returns></returns>
        [DisplayName("�Ƿ��ύ")]
        public string IS_SUBMIT { get; set; }
        /// <summary>
        /// ��Чʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Чʱ��")]
        public DateTime? EFFECTIVE_TIME { get; set; }
        /// <summary>
        /// ʧЧʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("ʧЧʱ��")]
        public DateTime? FAILURE_TIME { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
        public string REMARKS { get; set; }
        /// <summary>
        /// Ԥ���ֶ�1
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�1")]
        public string RESERVE1 { get; set; }
        /// <summary>
        /// Ԥ���ֶ�2
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�2")]
        public string RESERVE2 { get; set; }
        /// <summary>
        /// Ԥ���ֶ�3
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�3")]
        public string RESERVE3 { get; set; }
        /// <summary>
        /// Ԥ���ֶ�4
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�4")]
        public string RESERVE4 { get; set; }
        /// <summary>
        /// Ԥ���ֶ�5
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�5")]
        public string RESERVE5 { get; set; }
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
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        //public override void Create()
        //{
        //    this.VERSION_KEY = CommonHelper.GetGuid;
        //    this.CREATEDATE = DateTime.Now;
        //    this.CREATEUSERID = ManageProvider.Provider.Current().UserId;
        //    this.CREATEUSERNAME = ManageProvider.Provider.Current().UserName;
        //}
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="KeyValue"></param>
        //public override void Modify(string KeyValue)
        //{
        //    this.VERSION_KEY = KeyValue;
        //    this.MODIFYDATE = DateTime.Now;
        //    this.MODIFYUSERID = ManageProvider.Provider.Current().UserId;
        //    this.MODIFYUSERNAME = ManageProvider.Provider.Current().UserName;
        //}
        #endregion
    }
}