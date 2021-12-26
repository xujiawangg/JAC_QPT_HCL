//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2018
// Software Developers @ HfutIE 2018
//=====================================================================================

using HfutIe;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HfutIE.Entity
{
    /// <summary>
    /// �ƻ����̱�
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.10.25 16:59</date>
    /// </author>
    /// </summary>
    [Description("�ƻ����̱�")]
    [PrimaryKey("MES_PLAN_KEY")]
    public class P_PLAN : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// �ƻ�ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ƻ�ID")]
        public string MES_PLAN_KEY { get; set; }
        /// <summary>
        /// �ƻ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ƻ����")]
        public string MES_PLAN_CODE { get; set; }
        /// <summary>
        /// ������key
        /// </summary>
        /// <returns></returns>
        [DisplayName("������key")]
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
        /// ��Ʒ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ���")]
        public string PART_CODE { get; set; }
        /// <summary>
        /// �ƻ�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ƻ�����")]
        public int? PLAN_NUM { get; set; }
        /// <summary>
        /// �ƻ�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ƻ�����")]
        public DateTime? PLAN_DATE { get; set; }
        /// <summary>
        /// �ƻ���ʼʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ƻ���ʼʱ��")]
        public DateTime? PLAN_START_TIME { get; set; }
        /// <summary>
        /// �ƻ�����ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ƻ�����ʱ��")]
        public DateTime? PLAN_ENDING_TIME { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ע")]
        public string REMARKS { get; set; }
        /// <summary>
        /// �깤�ֿ�
        /// </summary>
        /// <returns></returns>
        [DisplayName("�깤�ֿ�")]
        public string COMPLETED_WAREHOUSE { get; set; }
        /// <summary>
        /// �ƻ���Դ
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ƻ���Դ")]
        public string PLAN_SOURCE { get; set; }
        /// <summary>
        /// �Ƿ��쳣
        /// </summary>
        /// <returns></returns>
        [DisplayName("�Ƿ��쳣")]
        public string EXCEPTION_FLAG { get; set; }
        /// <summary>
        /// �ƻ�ִ��״̬
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ƻ�ִ��״̬")]
        public string EXECUTION_STATUS { get; set; }
        /// <summary>
        /// �ƻ�������ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ƻ�������ʱ��")]
        public DateTime? LAST_UPDATE_DATE { get; set; }
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
        /// Ԥ���ֶ�6
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�6")]
        public string RESERVE6 { get; set; }
        /// <summary>
        /// Ԥ���ֶ�7
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�7")]
        public string RESERVE7 { get; set; }
        /// <summary>
        /// Ԥ���ֶ�8
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�8")]
        public string RESERVE8 { get; set; }
        /// <summary>
        /// Ԥ���ֶ�9
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�9")]
        public string RESERVE9 { get; set; }
        /// <summary>
        /// Ԥ���ֶ�10
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ԥ���ֶ�10")]
        public string RESERVE10 { get; set; }
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
        /// ʵ�ʿ�ʼʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("ʵ�ʿ�ʼʱ��")]
        public DateTime? ACTUAL_START_TIME { get; set; }
        /// <summary>
        /// ʵ�ʽ���ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("ʵ�ʽ���ʱ��")]
        public DateTime? ACTUAL_ENDING_TIME { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public int? ONLINE_NUM { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public int? OFFLINE_NUM { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string WORK_CENTER_KEY { get; set; }
        /// <summary>
        /// �ƻ����״̬
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ƻ����״̬")]
        public string COMPLETE_STATUS { get; set; }
        /// <summary>
        /// ����˳��
        /// </summary>
        /// <returns></returns>
        [DisplayName("����˳��")]
        public int? EXECUTION_QUEUE_NO { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.MES_PLAN_KEY = CommonHelper.GetGuid;
            this.CREATEDATE = ServerTime.Now;
            //this.CREATEUSERID = ManageProvider.Provider.Current().UserId;
            //this.CREATEUSERNAME = ManageProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.MES_PLAN_KEY = KeyValue;
            this.MODIFYDATE = ServerTime.Now;
            //this.MODIFYUSERID = ManageProvider.Provider.Current().UserId;
            //this.MODIFYUSERNAME = ManageProvider.Provider.Current().UserName;
        }
        #endregion
    }
}