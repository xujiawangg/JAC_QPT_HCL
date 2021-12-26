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
    /// ��Ʒ��ˮ��
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.02.21 18:54</date>
    /// </author>
    /// </summary>
    [Description("��Ʒ��ˮ��")]
    [PrimaryKey("SERIAL_KEY")]
    public class P_PRODUCT_SERIAL : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// key
        /// </summary>
        /// <returns></returns>
        [DisplayName("key")]
        public string SERIAL_KEY { get; set; }
        /// <summary>
        /// MES�ƻ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("MES�ƻ����")]
        public string MES_PLAN_CODE { get; set; }
        /// <summary>
        /// ��Ʒ����֤
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ����֤")]
        public string PRODUCT_BORN_CODE { get; set; }
        /// <summary>
        /// ��Ʒ��ˮ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ��ˮ��")]
        public string PRODUCT_SERIAL_NO { get; set; }
        /// <summary>
        /// ��ƷID
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ƷID")]
        public string PART_KEY { get; set; }
        /// <summary>
        /// ��Ʒ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ���")]
        public string PART_CODE { get; set; }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ����")]
        public string PART_NAME { get; set; }
        /// <summary>
        /// �ƻ����к�
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ƻ����к�")]
        public string PLAN_NO { get; set; }
        /// <summary>
        /// ��Ʒ������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ������")]
        public string PART_ABB { get; set; }
        /// <summary>
        /// ��Ʒ���κ�
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ���κ�")]
        public string PART_BATCH_NO { get; set; }
        /// <summary>
        /// ��Ʒ�ͺ�
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ�ͺ�")]
        public string PART_MODEL_NO { get; set; }
        /// <summary>
        /// ��Ʒ�ṹ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ�ṹ��")]
        public string PART_STRUCT_NO { get; set; }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ����")]
        public string PART_TYPE { get; set; }
        /// <summary>
        /// �Ƿ�������
        /// </summary>
        /// <returns></returns>
        [DisplayName("�Ƿ�������")]
        public string IS_ONLINE { get; set; }
        /// <summary>
        /// ��Ʒ����ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ����ʱ��")]
        public string ONLINE_TIME { get; set; }
        /// <summary>
        /// ��Ʒ����ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ����ʱ��")]
        public string OFFLINE_TIME { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ע")]
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
        /// ���������
        /// </summary>
        /// <returns></returns>
        [DisplayName("���������")]
        public string CYLINDER_BLOCK_BARCODE { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        //public override void Create()
        //{
        //    this.SERIAL_KEY = CommonHelper.GetGuid;
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
        //    this.SERIAL_KEY = KeyValue;
        //    this.MODIFYDATE = DateTime.Now;
        //    this.MODIFYUSERID = ManageProvider.Provider.Current().UserId;
        //    this.MODIFYUSERNAME = ManageProvider.Provider.Current().UserName;
        //}
        #endregion
    }
}