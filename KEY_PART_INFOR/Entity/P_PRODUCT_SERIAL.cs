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
    /// 产品流水表
    /// <author>
    ///		<name>she</name>
    ///		<date>2019.02.21 18:54</date>
    /// </author>
    /// </summary>
    [Description("产品流水表")]
    [PrimaryKey("SERIAL_KEY")]
    public class P_PRODUCT_SERIAL : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// key
        /// </summary>
        /// <returns></returns>
        [DisplayName("key")]
        public string SERIAL_KEY { get; set; }
        /// <summary>
        /// MES计划编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("MES计划编号")]
        public string MES_PLAN_CODE { get; set; }
        /// <summary>
        /// 产品出生证
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品出生证")]
        public string PRODUCT_BORN_CODE { get; set; }
        /// <summary>
        /// 产品流水号
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品流水号")]
        public string PRODUCT_SERIAL_NO { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品ID")]
        public string PART_KEY { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品编号")]
        public string PART_CODE { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品名称")]
        public string PART_NAME { get; set; }
        /// <summary>
        /// 计划序列号
        /// </summary>
        /// <returns></returns>
        [DisplayName("计划序列号")]
        public string PLAN_NO { get; set; }
        /// <summary>
        /// 产品简易码
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品简易码")]
        public string PART_ABB { get; set; }
        /// <summary>
        /// 产品批次号
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品批次号")]
        public string PART_BATCH_NO { get; set; }
        /// <summary>
        /// 产品型号
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品型号")]
        public string PART_MODEL_NO { get; set; }
        /// <summary>
        /// 产品结构号
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品结构号")]
        public string PART_STRUCT_NO { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品类型")]
        public string PART_TYPE { get; set; }
        /// <summary>
        /// 是否已上线
        /// </summary>
        /// <returns></returns>
        [DisplayName("是否已上线")]
        public string IS_ONLINE { get; set; }
        /// <summary>
        /// 产品上线时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品上线时间")]
        public string ONLINE_TIME { get; set; }
        /// <summary>
        /// 产品下线时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("产品下线时间")]
        public string OFFLINE_TIME { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [DisplayName("备注")]
        public string REMARKS { get; set; }
        /// <summary>
        /// 预留字段1
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段1")]
        public string RESERVE1 { get; set; }
        /// <summary>
        /// 预留字段2
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段2")]
        public string RESERVE2 { get; set; }
        /// <summary>
        /// 预留字段3
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段3")]
        public string RESERVE3 { get; set; }
        /// <summary>
        /// 预留字段4
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段4")]
        public string RESERVE4 { get; set; }
        /// <summary>
        /// 预留字段5
        /// </summary>
        /// <returns></returns>
        [DisplayName("预留字段5")]
        public string RESERVE5 { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建时间")]
        public string CREATEDATE { get; set; }
        /// <summary>
        /// 创建人key
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建人key")]
        public string CREATEUSERID { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        /// <returns></returns>
        [DisplayName("创建人姓名")]
        public string CREATEUSERNAME { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改时间")]
        public string MODIFYDATE { get; set; }
        /// <summary>
        /// 修改人key
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改人key")]
        public string MODIFYUSERID { get; set; }
        /// <summary>
        /// 修改人姓名
        /// </summary>
        /// <returns></returns>
        [DisplayName("修改人姓名")]
        public string MODIFYUSERNAME { get; set; }
        /// <summary>
        /// 缸体条码号
        /// </summary>
        /// <returns></returns>
        [DisplayName("缸体条码号")]
        public string CYLINDER_BLOCK_BARCODE { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        //public override void Create()
        //{
        //    this.SERIAL_KEY = CommonHelper.GetGuid;
        //    this.CREATEDATE = DateTime.Now;
        //    this.CREATEUSERID = ManageProvider.Provider.Current().UserId;
        //    this.CREATEUSERNAME = ManageProvider.Provider.Current().UserName;
        //}
        /// <summary>
        /// 编辑调用
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