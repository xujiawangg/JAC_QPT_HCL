//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2017
// Software Developers @ HfutIE 2017
//=====================================================================================

using System;
using System.ComponentModel;
using System.Text;

namespace HfutIe
{
    /// <summary>
    /// Material_WC_Part
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.04.10 20:03</date>
    /// </author>
    /// </summary>
    [Description("Material_WC_Part")]
    public class Material_WC_Part : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����")]
        public string material_wc_part_key { get; set; }
        /// <summary>
        /// ��λ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("��λ���")]
        public string wc_code { get; set; }
        /// <summary>
        /// ��λ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��λ����")]
        public string wc_name { get; set; }
        /// <summary>
        /// ��λ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��λ����")]
        public string wc_key { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string part_key { get; set; }
        /// <summary>
        /// ���ϱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("���ϱ��")]
        public string part_code { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string part_name { get; set; }
        /// <summary>
        /// ��ȫ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ȫ���")]
        public int? safety_num { get; set; }
        /// <summary>
        /// �����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����")]
        public int? max_num { get; set; }
        /// <summary>
        /// ���͵�λ
        /// </summary>
        /// <returns></returns>
        [DisplayName("���͵�λ")]
        public string delivery_unit { get; set; }
        /// <summary>
        /// ���͵�λ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("���͵�λ����")]
        public int? delivery_unit_num { get; set; }
        /// <summary>
        /// �洢����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�洢����")]
        public int? storage_num { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ע")]
        public string remark { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// �����û�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����û�����")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// �����û�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�����û�����")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸�����")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸��û�����")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// �޸��û�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�޸��û�����")]
        public string ModifyUserName { get; set; }
        /// <summary>
        /// reserve01
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve01")]
        public string reserve01 { get; set; }
        /// <summary>
        /// reserve02
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve02")]
        public string reserve02 { get; set; }
        /// <summary>
        /// reserve03
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve03")]
        public string reserve03 { get; set; }
        /// <summary>
        /// reserve04
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve04")]
        public string reserve04 { get; set; }
        /// <summary>
        /// reserve05
        /// </summary>
        /// <returns></returns>
        [DisplayName("reserve05")]
        public string reserve05 { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        public override void Create()
        {
            this.material_wc_part_key = CommonHelper.GetGuid;
            this.CreateDate = DateTime.Now;
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="KeyValue"></param>
        public override void Modify(string KeyValue)
        {
            this.material_wc_part_key = KeyValue;
            this.ModifyDate = DateTime.Now;
        }
        #endregion
    }
}