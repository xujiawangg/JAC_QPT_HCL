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
    /// �û���Ϣ��
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.02.19 15:55</date>
    /// </author>
    /// </summary>
    [Description("�û���Ϣ��")]
    [PrimaryKey("USERID")]
    public class BASE_USER : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// �û�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�û�����")]
        public string USERID { get; set; }
        /// <summary>
        /// ��˾����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��˾����")]
        public string COMPANYID { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string DEPARTMENTID { get; set; }
        /// <summary>
        /// �ڲ��û�
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ڲ��û�")]
        public int? INNERUSER { get; set; }
        /// <summary>
        /// �û�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�û�����")]
        public string CODE { get; set; }
        /// <summary>
        /// ��¼�˻�
        /// </summary>
        /// <returns></returns>
        [DisplayName("��¼�˻�")]
        public string ACCOUNT { get; set; }
        /// <summary>
        /// ��¼����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��¼����")]
        public string PASSWORD { get; set; }
        /// <summary>
        /// ������Կ
        /// </summary>
        /// <returns></returns>
        [DisplayName("������Կ")]
        public string SECRETKEY { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
        public string REALNAME { get; set; }
        /// <summary>
        /// ����ƴ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("����ƴ��")]
        public string SPELL { get; set; }
        /// <summary>
        /// �Ա�
        /// </summary>
        /// <returns></returns>
        [DisplayName("�Ա�")]
        public string GENDER { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string BIRTHDAY { get; set; }
        /// <summary>
        /// �ֻ�
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ֻ�")]
        public string MOBILE { get; set; }
        /// <summary>
        /// �绰
        /// </summary>
        /// <returns></returns>
        [DisplayName("�绰")]
        public string TELEPHONE { get; set; }
        /// <summary>
        /// QQ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("QQ����")]
        public string OICQ { get; set; }
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����ʼ�")]
        public string EMAIL { get; set; }
        /// <summary>
        /// ����޸���������
        /// </summary>
        /// <returns></returns>
        [DisplayName("����޸���������")]
        public string CHANGEPASSWORDDATE { get; set; }
        /// <summary>
        /// �����¼��ʶ
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����¼��ʶ")]
        public int? OPENID { get; set; }
        /// <summary>
        /// ��¼����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��¼����")]
        public int? LOGONCOUNT { get; set; }
        /// <summary>
        /// ��һ�η���ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��һ�η���ʱ��")]
        public string FIRSTVISIT { get; set; }
        /// <summary>
        /// ��һ�η���ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��һ�η���ʱ��")]
        public string PREVIOUSVISIT { get; set; }
        /// <summary>
        /// ������ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("������ʱ��")]
        public string LASTVISIT { get; set; }
        /// <summary>
        /// ���״̬
        /// </summary>
        /// <returns></returns>
        [DisplayName("���״̬")]
        public string AUDITSTATUS { get; set; }
        /// <summary>
        /// ���Ա����
        /// </summary>
        /// <returns></returns>
        [DisplayName("���Ա����")]
        public string AUDITUSERID { get; set; }
        /// <summary>
        /// ���Ա
        /// </summary>
        /// <returns></returns>
        [DisplayName("���Ա")]
        public string AUDITUSERNAME { get; set; }
        /// <summary>
        /// ���ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("���ʱ��")]
        public string AUDITDATETIME { get; set; }
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�Ƿ�����")]
        public int? IS_ONLINE { get; set; } 
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ע")]
        public string REMARK { get; set; }
        /// <summary>
        /// ��Ч
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ч")]
        public int? ENABLED { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [DisplayName("������")]
        public int? SORTCODE { get; set; }
        /// <summary>
        /// ɾ�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("ɾ�����")]
        public int? DELETEMARK { get; set; }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("����ʱ��")]
        public string CREATEDATE { get; set; }
        /// <summary>
        /// �����û�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����û�����")]
        public string CREATEUSERID { get; set; }
        /// <summary>
        /// �����û�
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����û�")]
        public string CREATEUSERNAME { get; set; }
        /// <summary>
        /// �޸�ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸�ʱ��")]
        public string MODIFYDATE { get; set; }
        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸��û�����")]
        public string MODIFYUSERID { get; set; }
        /// <summary>
        /// �޸��û�
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸��û�")]
        public string MODIFYUSERNAME { get; set; }
        /// <summary>
        /// WeChatID
        /// </summary>
        /// <returns></returns>
        [DisplayName("WeChatID")]
        public string WECHATID { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        //public override void Create()
        //{
        //    this.USERID = CommonHelper.GetGuid;
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
        //    this.USERID = KeyValue;
        //    this.MODIFYDATE = DateTime.Now;
        //    this.MODIFYUSERID = ManageProvider.Provider.Current().UserId;
        //    this.MODIFYUSERNAME = ManageProvider.Provider.Current().UserName;
        //}
        #endregion
    }
}