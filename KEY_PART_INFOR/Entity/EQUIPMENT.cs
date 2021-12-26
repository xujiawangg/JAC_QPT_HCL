//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2018
// Software Developers @ HfutIE 2018
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
    /// �豸������
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.12.11 19:58</date>
    /// </author>
    /// </summary>
    [Description("�豸������")]
    [PrimaryKey("EQUIPMENT_KEY")]
    public class EQUIPMENT : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// �豸ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("�豸ID")]
        public string EQUIPMENT_KEY { get; set; }
        /// <summary>
        /// �豸���
        /// </summary>
        /// <returns></returns>
        [DisplayName("�豸���")]
        public string EQUIPMENT_CODE { get; set; }
        /// <summary>
        /// �豸����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�豸����")]
        public string EQUIPMENT_NAME { get; set; }
        /// <summary>
        /// �豸����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�豸����")]
        public string EQUIPMENT_DESCRIPTION { get; set; }
        /// <summary>
        /// �豸���
        /// </summary>
        /// <returns></returns>
        [DisplayName("�豸���")]
        public string EQUIPMENT_TYPE { get; set; }
        /// <summary>
        /// ����ģʽ
        /// </summary>
        /// <returns></returns>
        [DisplayName("����ģʽ")]
        public string EQUIPMENT_MODEL { get; set; }
        /// <summary>
        /// �豸IP��ַ
        /// </summary>
        /// <returns></returns>
        [DisplayName("�豸IP��ַ")]
        public string EQUIPMENT_IP { get; set; }
        /// <summary>
        /// ���۽���
        /// </summary>
        /// <returns></returns>
        [DisplayName("���۽���")]
        public int? EQUIPMENT_BEAT { get; set; }
        /// <summary>
        /// �Ƿ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("�Ƿ���")]
        public string EQUIPMENT_MONITOR { get; set; }
        /// <summary>
        /// ��Ӧ��ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ӧ��ID")]
        public string SUPPLIER_KEY { get; set; }
        /// <summary>
        /// ����ͼƬID
        /// </summary>
        /// <returns></returns>
        [DisplayName("����ͼƬID")]
        public string INSTLIST_KEY { get; set; }
        /// <summary>
        /// ������ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("������ID")]
        public string PRODUCTION_LINE_KEY { get; set; }
        /// <summary>
        /// �����߱��
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����߱��")]
        public string PRODUCTION_LINE_CODE { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        [DisplayName("����������")]
        public string PRODUCTION_LINE_NAME { get; set; }
        /// <summary>
        /// ��λkey
        /// </summary>
        /// <returns></returns>
        [DisplayName("��λkey")]
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
        /// DeleteStatus
        /// </summary>
        /// <returns></returns>
        [DisplayName("DeleteStatus")]
        public string DELETE_STATUS { get; set; }
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
        //    this.EQUIPMENT_KEY = CommonHelper.GetGuid;
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
        //    this.EQUIPMENT_KEY = KeyValue;
        //    this.MODIFYDATE = DateTime.Now;
        //    this.MODIFYUSERID = ManageProvider.Provider.Current().UserId;
        //    this.MODIFYUSERNAME = ManageProvider.Provider.Current().UserName;
        //}
        #endregion
    }
}