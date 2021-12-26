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
    /// ����״̬���̱�
    /// <author>
    ///		<name>she</name>
    ///		<date>2018.12.20 22:22</date>
    /// </author>
    /// </summary>
    [Description("����״̬���̱�")]
    [PrimaryKey("ASSEMBLE_PRODUCT_STATE_KEY")]
    public class P_ASSEMBLE_PRODUCT_STATE : BaseEntity
    {
        #region ��ȡ/���� �ֶ�ֵ
        /// <summary>
        /// ����״̬���̱�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("����״̬���̱�����")]
        public string ASSEMBLE_PRODUCT_STATE_KEY { get; set; }
        /// <summary>
        /// �û����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�û����")]
        public string ACCOUNT_CODE { get; set; }
        /// <summary>
        /// �û�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("�û�����")]
        public string ACCOUNT_NAME { get; set; }
        /// <summary>
        /// ��˾����
        /// </summary>
        /// <returns></returns>
        [DisplayName("��˾����")]
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
        /// ���ͳ�������
        /// </summary>
        /// <returns></returns>
        [DisplayName("���ͳ�������")]
        public string DISTRIBUTION_WORKSHOP_KEY { get; set; }
        /// <summary>
        /// ���ͳ�����
        /// </summary>
        /// <returns></returns>
        [DisplayName("���ͳ�����")]
        public string DISTRIBUTION_WORKSHOP_CODE { get; set; }
        /// <summary>
        /// ���ͳ�������
        /// </summary>
        /// <returns></returns>
        [DisplayName("���ͳ�������")]
        public string DISTRIBUTION_WORKSHOP_NAME { get; set; }
        /// <summary>
        /// �ϸ�״̬
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ϸ�״̬")]
        public string IS_OK { get; set; }
        /// <summary>
        /// ����״̬
        /// </summary>
        /// <returns></returns>
        [DisplayName("����״̬")]
        public string OPERATED_STATE { get; set; }
        /// <summary>
        /// �Ƿ�Ϊ����Ʒ
        /// </summary>
        /// <returns></returns>
        [DisplayName("�Ƿ�Ϊ����Ʒ")]
        public string IS_REPAIR { get; set; }
        /// <summary>
        /// ����Ƶ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("����Ƶ��")]
        public int? REPAIR_FREQUENCY { get; set; }
        /// <summary>
        /// ��װ����ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��װ����ʱ��")]
        public DateTime? ASSEMBLE_ONLINE_TIME { get; set; }
        /// <summary>
        /// ��װ����ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��װ����ʱ��")]
        public DateTime? ASSEMBLE_OFFLINE_TIME { get; set; }
        /// <summary>
        /// �洢ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("�洢ʱ��")]
        public DateTime? STORAGE_TIME { get; set; }
        /// <summary>
        /// ��ע
        /// </summary>
        /// <returns></returns>
        [DisplayName("��ע")]
        public string REMARKS { get; set; }
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
        /// �ӹ�����id
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ӹ�����id")]
        public string MACHINING_CENTER_KEY { get; set; }
        /// <summary>
        /// �ӹ����ı��
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ӹ����ı��")]
        public string MACHINING_CENTER_CODE { get; set; }
        /// <summary>
        /// �ӹ���������
        /// </summary>
        /// <returns></returns>
        [DisplayName("�ӹ���������")]
        public string MACHINING_CENTER_NAME { get; set; }
        /// <summary>
        /// ��������ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������ʱ��")]
        public DateTime? HOT_TEST_ONLINE_TIME { get; set; }
        /// <summary>
        /// ��������ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("��������ʱ��")]
        public DateTime? HOT_TEST_OFFLINE_TIME { get; set; }
        /// <summary>
        /// Ϳװ����ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ϳװ����ʱ��")]
        public DateTime? PAINTING_ONLINE_TIME { get; set; }
        /// <summary>
        /// Ϳװ����ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("Ϳװ����ʱ��")]
        public DateTime? PAINTING_OFFLINE_TIME { get; set; }
        /// <summary>
        /// �Һϸ�֤ʱ��
        /// </summary>
        /// <returns></returns>
        [DisplayName("�Һϸ�֤ʱ��")]
        public DateTime? HANGING_CERTIFICATE_TIME { get; set; }
        #endregion

        #region ��չ����
        /// <summary>
        /// ��������
        /// </summary>
        //public override void Create()
        //{
        //    this.ASSEMBLE_PRODUCT_STATE_KEY = CommonHelper.GetGuid;
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
        //    this.ASSEMBLE_PRODUCT_STATE_KEY = KeyValue;
        //    this.MODIFYDATE = DateTime.Now;
        //    this.MODIFYUSERID = ManageProvider.Provider.Current().UserId;
        //    this.MODIFYUSERNAME = ManageProvider.Provider.Current().UserName;
        //}
        #endregion
    }
}