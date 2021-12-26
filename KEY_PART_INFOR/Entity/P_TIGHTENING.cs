//=====================================================================================
// All Rights Reserved , Copyright @ HfutIE 2019
// Software Developers @ HfutIE 2019
//=====================================================================================

using HfutIe.DataAccess.Attributes;
using HfutIe.Entity;
using HfutIe.Help;
using System;
using System.ComponentModel;
using System.Configuration;

namespace HfutIE.Entity
{
    /// <summary>
    /// DOC_TIGHTENING
    /// <author>
    ///		<name>she</name>
    ///		<date>2017.12.01 15:42</date>
    /// </author>
    /// </summary>
    [Description("P_TIGHTENING")]
    public class P_TIGHTENING : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// tightening_key
        /// </summary>
        /// <returns></returns>
        [DisplayName("tightening_key")]
        public string tightening_key { get; set; }
        public string SITE_KEY { get; set; }
        /// <summary>
        /// ��˾���
        /// </summary>
        /// <returns></returns>
        [DisplayName("��˾���")]
        public string SITE_CODE { get; set; }
        /// <summary>
        /// ��˾����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��˾����")]
        public string SITE_NAME { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string AREA_KEY { get; set; }
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        [DisplayName("�������")]
        public string AREA_CODE { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string AREA_NAME { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string WORKSHOP_KEY { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [DisplayName("������")]
        public string WORKSHOP_CODE { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string WORKSHOP_NAME { get; set; }
        /// <summary>
        /// ����������
        /// </summary>
        /// <returns></returns>
        [DisplayName("����������")]
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
        /// ������������
        /// </summary>
        /// <returns></returns>
        [DisplayName("������������")]
        public string WORK_CENTER_KEY { get; set; }
        /// <summary>
        /// �������ı��
        /// </summary>
        /// <returns></returns>
        [DisplayName("�������ı��")]
        public string WORK_CENTER_CODE { get; set; }
        /// <summary>
        /// ������������
        /// </summary>
        /// <returns></returns>
        [DisplayName("������������")]
        public string WORK_CENTER_NAME { get; set; }
        /// <summary>
        /// �豸����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�豸����")]
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
        /// ��λID
        /// </summary>
        /// <returns></returns>
        [DisplayName("��λID")]
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
        /// ֹͣ��ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("ֹͣ��ID")]
        public string STOPPER_KEY { get; set; }
        /// <summary>
        /// ֹͣ������
        /// </summary>
        /// <returns></returns>
        [DisplayName("ֹͣ������")]
        public string STOPPER_CODE { get; set; }
        /// <summary>
        /// ֹͣ������
        /// </summary>
        /// <returns></returns>
        [DisplayName("ֹͣ������")]
        public string STOPPER_NAME { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string SHIFT_KEY { get; set; }
        /// <summary>
        /// ���Ʊ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("���Ʊ��")]
        public string SHIFT_CODE { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string SHIFT_NAME { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string TEAM_KEY { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        [DisplayName("������")]
        public string TEAM_CODE { get; set; }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������")]
        public string TEAM_NAME { get; set; }
        /// <summary>
        /// ��Ա����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ա����")]
        public string STAFF_KEY { get; set; }
        /// <summary>
        /// ��Ա���
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ա���")]
        public string STAFF_CODE { get; set; }
        /// <summary>
        /// ��Ա����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ա����")]
        public string STAFF_NAME { get; set; }
        /// <summary>
        /// �ƻ�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ƻ�����")]
        public string MES_PLAN_KEY { get; set; }
        /// <summary>
        /// �ƻ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ƻ����")]
        public string MES_PLAN_CODE { get; set; }
        /// <summary>
        /// �ƻ�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ƻ�����")]
        public int? PLAN_NUM { get; set; }
        /// <summary>
        /// �ƻ����к�
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ƻ����к�")]
        public string PLAN_NO { get; set; }
        /// <summary>
        /// ����BOM����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����BOM����")]
        public string BOM_KEY { get; set; }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ����")]
        public string PRODUCT_KEY { get; set; }
        /// <summary>
        /// ��Ʒ���
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ���")]
        public string PRODUCT_CODE { get; set; }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ����")]
        public string PRODUCT_NAME { get; set; }
        /// <summary>
        /// ��Ʒ������
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ������")]
        public string PRODUCT_ABB { get; set; }
        /// <summary>
        /// ��Ʒ���κ�
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ���κ�")]
        public string PRODUCT_BATCH_NO { get; set; }
        /// <summary>
        /// ��Ʒ����֤
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ����֤")]
        public string PRODUCT_BORN_CODE { get; set; }
        /// <summary>
        /// ��Ʒģ�ͺ�
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒģ�ͺ�")]
        public string PRODUCT_MODEL_NO { get; set; }
        /// <summary>
        /// ��Ʒ��ˮ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ��ˮ��")]
        public string PRODUCT_SERIAL_NO { get; set; }
        /// <summary>
        /// ��Ʒ�ṹ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ�ṹ��")]
        public string PRODUCT_STRUCT_NO { get; set; }
        /// <summary>
        /// ��Ʒ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��Ʒ����")]
        public string PRODUCT_TYPE { get; set; }
        /// <summary>
        /// date
        /// </summary>
        /// <returns></returns>
        [DisplayName("date")]
        public string Check_Time { get; set; }
        public string bolt_name { get; set; }//��˨��
        public string Parm_Name { get; set; }//���
        /// <summary>
        /// upper_control_torque
        /// </summary>
        /// <returns></returns>
        [DisplayName("upper_control_torque")]
        public string upper_control_torque { get; set; }
        /// <summary>
        /// upper_value_torque
        /// </summary>
        /// <returns></returns>
        [DisplayName("upper_value_torque")]
        public string upper_value_torque { get; set; }
        /// <summary>
        /// target_value_torque
        /// </summary>
        /// <returns></returns>
        [DisplayName("target_value_torque")]
        public string target_value_torque { get; set; }
        /// <summary>
        /// lower_value_torque
        /// </summary>
        /// <returns></returns>
        [DisplayName("lower_value_torque")]
        public string lower_value_torque { get; set; }
        /// <summary>
        /// lower_control_torque
        /// </summary>
        /// <returns></returns>
        [DisplayName("lower_control_torque")]
        public string lower_control_torque { get; set; }
        /// <summary>
        /// check_value_torque
        /// </summary>
        /// <returns></returns>
        [DisplayName("check_value_torque")]
        public string  check_value_torque { get; set; }

        /// <summary>
        /// upper_control_angle
        /// </summary>
        /// <returns></returns>
        [DisplayName("upper_control_angle")]
        public string upper_control_angle { get; set; }
        /// <summary>
        /// upper_value_angle
        /// </summary>
        /// <returns></returns>
        [DisplayName("upper_value_angle")]
        public string upper_value_angle { get; set; }
        /// <summary>
        /// target_value_angle
        /// </summary>
        /// <returns></returns>
        [DisplayName("target_value_angle")]
        public string target_value_angle { get; set; }
        /// <summary>
        /// lower_value_angle
        /// </summary>
        /// <returns></returns>
        [DisplayName("lower_value_angle")]
        public string lower_value_angle { get; set; }
        /// <summary>
        /// lower_control_angle
        /// </summary>
        /// <returns></returns>
        [DisplayName("lower_control_angle")]
        public string lower_control_angle { get; set; }
        /// <summary>
        /// check_value_angle
        /// </summary>
        /// <returns></returns>
        [DisplayName("check_value_angle")]
        public string check_value_angle { get; set; }
        /// <summary>
        /// IS_QUALIFIED
        /// </summary>
        /// <returns></returns>
        [DisplayName("IS_QUALIFIED")]
        public string is_qualified { get; set; }
        /// <summary>
        /// ORDERID
        /// </summary>
        /// <returns></returns>
        [DisplayName("ORDERID")]
        public string ORDERID { get; set; }
        /// <summary>
        /// ������Դ����
        /// </summary>
        /// <returns></returns>
        [DisplayName("SOURCE_TYPE")]
        public string SOURCE_TYPE { get; set; }
        /// <summary>
        /// RESERVE01
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE01")]
        public string reserve1 { get; set; }
        /// <summary>
        /// RESERVE02
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE02")]
        public string reserve2 { get; set; }
        /// <summary>
        /// RESERVE03
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE03")]
        public string reserve3 { get; set; }
        /// <summary>
        /// RESERVE04
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE04")]
        public string reserve4 { get; set; }
        /// <summary>
        /// RESERVE05
        /// </summary>
        /// <returns></returns>
        [DisplayName("RESERVE05")]
        public string reserve5 { get; set; }
        /// <summary>
        /// CREATEDATE
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEDATE")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// CREATEUSERID
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEUSERID")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// CREATEUSERNAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("CREATEUSERNAME")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// MODIFYDATE
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFYDATE")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// MODIFYUSERID
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFYUSERID")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// MODIFYUSERNAME
        /// </summary>
        /// <returns></returns>
        [DisplayName("MODIFYUSERNAME")]
        public string ModifyUserName { get; set; }
        public int? DeleteMark { get; set; }
        #endregion

        //#region ��չ����
        ///// <summary>
        ///// ��������
        ///// </summary>
        //public override void Create()
        //{
        //    this.tightening_key = HfutIe.CommonHelper.GetGuid;
        //    this.CreateDate = ServerTime.Now;
        //    this.CreateUserId = ConfigurationManager.AppSettings["ProgramName"].ToString();
        //    this.CreateUserName = ConfigurationManager.AppSettings["ProgramName"].ToString();
        //}
        ///// <summary>
        ///// �༭����
        ///// </summary>
        ///// <param name="KeyValue"></param>
        //public override void Modify(string KeyValue)
        //{
        //    this.tightening_key = KeyValue;
        //    this.ModifyDate = ServerTime.Now;
        //    this.CreateUserId = ConfigurationManager.AppSettings["ProgramName"].ToString();
        //    this.CreateUserName = ConfigurationManager.AppSettings["ProgramName"].ToString();
        //}
        //#endregion
    }
}