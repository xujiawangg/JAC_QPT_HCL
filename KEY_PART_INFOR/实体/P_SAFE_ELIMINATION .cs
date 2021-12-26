//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2019
// Software Developers @ HfutIE 2019
//=====================================================================================
using HfutIe;
using HfutIE.Utilities;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Text;

namespace HfutIE.Entity
{
    /// <summary>
    /// ����Ʒ��ȫ��Ϣ�����
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.02.28 15:56</date>
    /// </author>
    /// </summary>
    [Description("����Ʒ��ȫ��Ϣ�����")]
    [PrimaryKey("P_SAFE_ELIMINATIONKEY")]
    public class P_SAFE_ELIMINATION : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
        public string P_SAFE_ELIMINATIONKEY { get; set; }
        /// <summary>
        /// ��Ʒ����֤
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ����֤")]
        public string PRODUCT_BORN_CODE { get; set; }
        /// <summary>
        /// ��λ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��λ����")]
        public string STATION_KEY { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string PART_KEY { get; set; }
        /// <summary>
        /// ��Ӧ������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ӧ������")]
        public string SUPPLIER_KEY { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
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
        /// �����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����")]
        public string S_CLT_RESULT { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ע")]
        public string REMARK { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.P_SAFE_ELIMINATIONKEY = CommonHelper.GetGuid;
            this.CREATEDATE = DateTime.Now;
            this.CREATEUSERID = ConfigurationManager.AppSettings["ProgramName"].ToString();
            this.CREATEUSERNAME = ConfigurationManager.AppSettings["ProgramName"].ToString(); 
        }
        #endregion
    }
}