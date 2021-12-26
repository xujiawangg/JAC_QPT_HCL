//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2014
// Software Developers @ HfutIE 2014
//=====================================================================================


using log4net;
using System;
using System.ComponentModel;


namespace HfutIe
{
    /// <summary>
    /// ϵͳ��־��
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.06.23 22:43</date>
    /// </author>
    /// </summary>
    class ExLogHelper
    {
        #region ��̬ʵ����
        private static ExLogHelper item;
        public static ExLogHelper Instance
        {
            get
            {
                if (item == null)
                {
                    item = new ExLogHelper();
                }
                return item;
            }
        }
        #endregion
        private Base_SysLog SysLog = new Base_SysLog();
        #region д�������־
        /// <summary>
        /// д����ҵ��־������������
        /// </summary>
        /// <param name="entity">ʵ�����</param>
        /// <param name="OperationType">��������</param>
        /// <param name="Status">״̬</param>
        /// <param name="Remark">����˵��</param>
        /// <returns></returns>
        public void WriteLog(Exception ex, OperationType OperationType)
        { 
            try
            {
                SysLog.SysLogId = CommonHelper.GetGuid;
                SysLog.ObjectId = "System";
                SysLog.LogType = CommonHelper.GetString((int)OperationType);
                SysLog.IPAddress = SystemLog.IP;
                SysLog.IPAddressName = SystemLog.PCname;
                SysLog.CompanyId = "";
                SysLog.DepartmentId = "";
                SysLog.CreateUserId = SystemLog.UserKey;
                SysLog.CreateUserName = SystemLog.UserName;
                SysLog.ModuleId ="84575917-032d-472e-951d-9cac54de7de1";
                SysLog.Remark = ex.Message;
                SysLog.Status = "-1";
                new DataBaseOpByEntity().Insert(SysLog);
            }
            catch (Exception exp)
            {
                ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                log.Fatal("�ײ��쳣��־��¼ʧ��!", exp);
            }
             
        }
 
        #endregion
    }
    /// <summary>
    /// ��������
    /// </summary>
    public enum OperationType 
    {
        /// <summary>
        /// ��½
        /// </summary>  
        Login = 0,
        /// <summary>
        ///  ɨ��
        /// </summary>   
        Scan = 9,
        /// <summary>
        ///  ����
        /// </summary>
        Run = 10,
        /// <summary>
        /// ��ȫ�˳�
        /// </summary>
        Exit = 8,
    }
    class Base_SysLog : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// ��־����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��־����")]
        public string SysLogId { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string ObjectId { get; set; }
        /// <summary>
        /// ��־����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��־����")]
        public string LogType { get; set; }
        /// <summary>
        /// ����IP
        /// </summary>
        /// <returns></returns>
        [DisplayName("����IP")]
        public string IPAddress { get; set; }
        /// <summary>
        /// IP��ַ���ڵ�ַ
        /// </summary>
        /// <returns></returns>
        [DisplayName("IP��ַ���ڵ�ַ")]
        public string IPAddressName { get; set; }
        /// <summary>
        /// ��˾����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��˾����")]
        public string CompanyId { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string DepartmentId { get; set; }
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
        /// ģ������
        /// </summary>
        /// <returns></returns>
        [DisplayName("ģ������")]
        public string ModuleId { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
        public string Remark { get; set; }
        /// <summary>
        /// ״̬
        /// </summary>
        /// <returns></returns>
        [DisplayName("״̬")]
        public string Status { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.SysLogId = CommonHelper.GetGuid;
            this.CreateDate = ServerTime.Now;

        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.SysLogId = KeyValue;
        }
        #endregion
    }
}