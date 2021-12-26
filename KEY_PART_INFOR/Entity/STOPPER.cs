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
    /// ֹͣ��������
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.02.21 15:14</date>
    /// </author>
    /// </summary>
    [Description("ֹͣ��������")]
    [PrimaryKey("STOPPER_KEY")]
    public class STOPPER : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// ֹͣ��key
        /// </summary>
        /// <returns></returns>
        [DisplayName("ֹͣ��key")]
        public string STOPPER_KEY { get; set; }
        /// <summary>
        /// ֹͣ�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("ֹͣ�����")]
        public string STOPPER_CODE { get; set; }
        /// <summary>
        /// ֹͣ������
        /// </summary>
        /// <returns></returns>
        [DisplayName("ֹͣ������")]
        public string STOPPER_NAME { get; set; }
        /// <summary>
        /// ��λkey
        /// </summary>
        /// <returns></returns>
        [DisplayName("��λkey")]
        public string STATION_KEY { get; set; }
        /// <summary>
        /// ������ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("������ID")]
        public string PRODUCTION_LINE_KEY { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ע")]
        public string REMARK { get; set; }
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
        /// �޸�ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸�ʱ��")]
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
        /// <summary>
        /// Ԥ��07
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��07")]
        public string RESERVE07 { get; set; }
        /// <summary>
        /// Ԥ��08
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��08")]
        public string RESERVE08 { get; set; }
        /// <summary>
        /// Ԥ��09
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��09")]
        public string RESERVE09 { get; set; }
        /// <summary>
        /// Ԥ��10
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ��10")]
        public string RESERVE10 { get; set; }
        /// <summary>
        /// ɾ��״̬
        /// </summary>
        /// <returns></returns>
        [DisplayName("ɾ��״̬")]
        public int? DELETEMARK { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        //public override void Create()
        //{
        //    this.STOPPER_KEY = CommonHelper.GetGuid;
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
        //    this.STOPPER_KEY = KeyValue;
        //    this.MODIFYDATE = DateTime.Now;
        //    this.MODIFYUSERID = ManageProvider.Provider.Current().UserId;
        //    this.MODIFYUSERNAME = ManageProvider.Provider.Current().UserName;
        //}
        #endregion
    }
}